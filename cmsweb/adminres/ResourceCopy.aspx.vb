Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement


Namespace Unionsoft.Cms.Web


Partial Class ResourceCopy
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Me.SetFocusOnTextbox("txtResName") '设置键盘光标默认选中的输入框
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strNewResName As String = txtResName.Text.Trim()
            If strNewResName = "" Then
                PromptMsg("请输入有效的新资源的名称")
                Return
            End If

            CopyResource(CmsPass, RLng("mnuresid"), strNewResName)



            btnConfirm.Enabled = False
            PromptMsg("成功复制资源的基本定义到新资源：" & strNewResName)
            'Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnEditRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ResFactory.ResService.EditResName(CmsPass, VLng("PAGE_RESID"), txtResName.Text.Trim())
        Catch ex As Exception
            PromptMsg("修改资源名称失败，无法连接数据库！")
        End Try
    End Sub

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '--------------------------------------------------------------------------------------------------
    '复制资源
    '--------------------------------------------------------------------------------------------------
    Private Shared Sub CopyResource(ByRef pst As CmsPassport, ByVal lngSrcResID As Long, ByVal strDestResName As String)
        '字段内部名称的列表。Key：原字段内部名称；Value：新字段内部名称
        Dim hashCol As New Hashtable

        '先创建资源
        Dim datResSrc As DataResource = pst.GetDataRes(lngSrcResID)
        If datResSrc.IndepParentResID <> datResSrc.ResID Then
            Throw New CmsException("不能复制继承型资源，请选择有效的独立型资源。")
        End If
        Dim lngNewResID As Long = ResFactory.ResService.AddResource(pst, strDestResName, ResInheritType.IsIndependent, datResSrc.ResPID, datResSrc.DepID, , True)
            ResFactory.ResService.CreateResTable(pst, lngNewResID, datResSrc.ResPID, datResSrc.ResTableType)


        Dim dbs As DbStatement = New DbStatement(SDbConnectionPool.GetDbConfig())
        Try
            dbs.TransactionBegin()

            '复制字段定义
            CopyRecords(pst, dbs, CmsTables.ColDefine, "CD_ID", "", "CD_RESID", "", "CD_COLNAME", "", lngNewResID, lngSrcResID, hashCol, True)

            '添加字段
            AlterTable(pst, dbs, CmsTables.ColDefine, "CD_DISPNAME", "CD_COLNAME", "CD_TYPE", "CD_SIZE", lngNewResID)

            '复制字段显示设置
            CopyRecords(pst, dbs, CmsTables.ColShow, "", "", "CS_RESID", "CS_RESINPID", "CS_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            '复制窗体设计
            CopyRecordsForForm(pst, dbs, lngNewResID, lngSrcResID, hashCol)

            '复制定制窗体设计
            'CopyRecords(pst, dbs, CmsTables.ColInputCust, "CCF_ID", "", "CCF_RESID", "", "", "", lngNewResID, lngSrcResID, hashCol)

            '复制字段定义－下拉框选择项
            CopyRecords(pst, dbs, CmsTables.FieldOption, "", "CDX_AIID", "CDX_RESID", "", "CDX_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            '复制字段定义－多选一
            CopyRecords(pst, dbs, CmsTables.FieldRadio, "", "CDR_AIID", "CDR_RESID", "", "CDR_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            '复制字段定义－自动编码表
            CopyRecords(pst, dbs, CmsTables.FieldAutoCoding, "", "CDC_AIID", "CDC_RESID", "", "CDC_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            '复制字段定义－高级字典表
            CopyRecords(pst, dbs, CmsTables.FieldDictionary, "", "CDZ2_AIID", "CDZ2_RESID1", "", "CDZ2_COL1", "CDZ2_MAINCOL", lngNewResID, lngSrcResID, hashCol)

            '复制字段定义－计算公式表
            'CopyRecords(pst, dbs, CmsTables.FieldCalculation, "", "CDJ_AIID", "CDJ_RESID", "", "CDJ_COLNAME", "", lngNewResID, lngSrcResID, hashCol)
            CopyRecords(pst, dbs, CmsTables.FieldCalculation, "CDJ_AIID", "CDJ_RESID", "CDJ_COLNAME", "CDJ_FMLRIGHT_EXPR", lngNewResID, lngSrcResID, hashCol)

            '复制字段定义－定制编码表
            CopyRecords(pst, dbs, CmsTables.FieldCustomizeCoding, "", "CDD_AIID", "CDD_RESID", "", "CDD_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            '复制作为主表的关联表定义
            CopyRecords(pst, dbs, CmsTables.RelatedTable, "", "RT_AIID", "RT_TAB1_RESID", "", "RT_TAB1_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            '复制作为子表的关联表定义
            CopyRecords(pst, dbs, CmsTables.RelatedTable, "", "RT_AIID", "RT_TAB2_RESID", "", "RT_TAB2_COLNAME", "", lngNewResID, lngSrcResID, hashCol)

            dbs.TransactionCommit()

            CmsConfig.ReloadAll() '清空缓存
        Catch ex As Exception
            dbs.TransactionRollback()
            CmsError.ThrowEx("复制资源异常失败。", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------------------------------
    '复制窗体设计2个表单的信息
    '--------------------------------------------------------------------------------------------------
    Private Shared Sub CopyRecordsForForm(ByRef pst As CmsPassport, ByRef dbs As DbStatement, ByVal lngNewResID As Long, ByVal lngSrcResID As Long, ByRef hashCol As Hashtable)
        Dim hashSpecID As New Hashtable

        '复制窗体列表定义
        If lngNewResID > 0 Then
            Dim ds1 As DataSet = dbs.Query("SELECT * FROM " & CmsTables.ColInputList & " WHERE CILST_RESID=" & lngSrcResID)
            Dim dv1 As DataView = ds1.Tables(0).DefaultView
            Dim strSql2 As String = "SELECT * FROM " & CmsTables.ColInputList & " WHERE 11=22"
            Dim ds2 As DataSet = dbs.Query(strSql2)
            Dim dv2 As DataView = ds2.Tables(0).DefaultView
            Dim drv1 As DataRowView = Nothing
            For Each drv1 In dv1
                Dim drv2 As DataRowView = dv2.AddNew()
                drv2.BeginEdit()
                Dim col As DataColumn
                For Each col In ds1.Tables(0).Columns
                    Select Case col.ColumnName
                        Case "CILST_ID"
                            Dim lngHostID As Long = TimeId.CurrentMilliseconds()
                            hashSpecID.Add(DbField.GetStr(drv1, col.ColumnName), CStr(lngHostID))
                            drv2(col.ColumnName) = lngHostID
                        Case "CILST_RESID", "CILST_RESINPID"
                            drv2(col.ColumnName) = lngNewResID

                        Case Else
                            drv2(col.ColumnName) = drv1(col.ColumnName)
                    End Select
                Next col
                drv2.EndEdit()
            Next
            dbs.QueryUpdate(ds2, False, True, False)
        End If

        '复制窗体元素定义
        If lngNewResID > 0 AndAlso hashSpecID.Count > 0 Then
            Dim ds1 As DataSet = dbs.Query("SELECT * FROM " & CmsTables.ColInput & " WHERE CI_RESID=" & lngSrcResID)
            Dim dv1 As DataView = ds1.Tables(0).DefaultView
            Dim strSql2 As String = "SELECT * FROM " & CmsTables.ColInput & " WHERE 11=22"
            Dim ds2 As DataSet = dbs.Query(strSql2)
            Dim dv2 As DataView = ds2.Tables(0).DefaultView
            Dim drv1 As DataRowView = Nothing
            For Each drv1 In dv1
                Dim drv2 As DataRowView = dv2.AddNew()
                drv2.BeginEdit()
                Dim col As DataColumn
                For Each col In ds1.Tables(0).Columns
                    Select Case col.ColumnName
                        Case "CI_HOSTID"
                            Dim strNewHostID As String = HashField.GetStr(hashSpecID, DbField.GetStr(drv1, col.ColumnName))
                            If strNewHostID = "" Then Throw New CmsException("窗体设计的列表和元素定义表不匹配，无法复制窗体设计表单。")
                            drv2(col.ColumnName) = strNewHostID
                        Case "CI_RESID", "CI_RESINPID"
                            drv2(col.ColumnName) = lngNewResID
                        Case "CI_COLRESID"
                            If DbField.GetLng(drv1, col.ColumnName) = lngSrcResID Then
                                '本表字段
                                drv2(col.ColumnName) = lngNewResID
                            Else
                                '其它表单的字段
                                drv2(col.ColumnName) = drv1(col.ColumnName)
                            End If
                        Case "CI_COLNAME"
                            If DbField.GetStr(drv1, "CI_COLRESID") = DbField.GetStr(drv1, "CI_RESID") Then
                                '本表字段
                                Dim strNewColID As String = HashField.GetStr(hashCol, DbField.GetStr(drv1, col.ColumnName))
                                If strNewColID <> "" Then
                                    drv2(col.ColumnName) = strNewColID
                                Else
                                    drv2(col.ColumnName) = DbField.GetStr(drv1, col.ColumnName)
                                End If
                            Else
                                '其它表单的字段
                                drv2(col.ColumnName) = drv1(col.ColumnName)
                            End If

                        Case Else
                            drv2(col.ColumnName) = drv1(col.ColumnName)
                    End Select
                Next col
                drv2.EndEdit()
            Next
            'CmsDbStatement.QueryUpdate(SDbConnectionPool.GetDbConfig(), ds2, strSql2, False, True, False)
            dbs.QueryUpdate(ds2, False, True, False)
        End If
    End Sub

    '--------------------------------------------------------------------------------------------------
    '复制制定系统表单的记录
    '--------------------------------------------------------------------------------------------------
    Private Shared Sub CopyRecords(ByRef pst As CmsPassport, ByRef dbs As DbStatement, ByVal strTable As String, ByVal strColTimeid As String, ByVal strColAiid As String, ByVal strColResID As String, ByVal strColParentResID As String, ByVal strColField1 As String, ByVal strColField2 As String, ByVal lngNewResID As Long, ByVal lngSrcResID As Long, ByRef hashCol As Hashtable, Optional ByVal blnCreateColHash As Boolean = False)  ', Optional ByVal strColSpecID As String = "", Optional ByRef hashSpecID As Hashtable = Nothing)
        Dim ds1 As DataSet = dbs.Query("SELECT * FROM " & strTable & " WHERE " & strColResID & "=" & lngSrcResID)
        Dim dv1 As DataView = ds1.Tables(0).DefaultView
        Dim strSql2 As String = "SELECT * FROM " & strTable & " WHERE 11=22"
        Dim ds2 As DataSet = dbs.Query(strSql2)
        Dim dv2 As DataView = ds2.Tables(0).DefaultView
        Dim drv1 As DataRowView = Nothing
        For Each drv1 In dv1
            Dim drv2 As DataRowView = dv2.AddNew()
            drv2.BeginEdit()
            Dim col As DataColumn
            For Each col In ds1.Tables(0).Columns
                Select Case col.ColumnName
                    Case strColTimeid
                        drv2(col.ColumnName) = TimeId.CurrentMilliseconds()
                    Case strColAiid
                        '自增长ID，Nothing to do
                    Case strColResID, strColParentResID
                        drv2(col.ColumnName) = lngNewResID

                        SLog.Err(strTable & ":" & col.ColumnName & "," & lngNewResID.ToString())
                    Case strColField1, strColField2
                        If blnCreateColHash = True Then
                            Dim strNewColID As String = CTableStructure.GenerateColName()
                            HashField.SetStr(hashCol, DbField.GetStr(drv1, col.ColumnName), strNewColID)
                            drv2(col.ColumnName) = strNewColID
                            SLog.Err(strTable & ":" & col.ColumnName & "," & strNewColID.ToString())
                        Else
                            Dim strNewColID As String = HashField.GetStr(hashCol, DbField.GetStr(drv1, col.ColumnName))
                            drv2(col.ColumnName) = strNewColID
                            SLog.Err(strTable & ":" & col.ColumnName & "," & strNewColID.ToString())
                        End If
                        'Case strColSpecID
                        '    If strColSpecID <> "" AndAlso Not hashSpecID Is Nothing AndAlso hashSpecID.Count > 0 Then
                        '        Dim strNewSpecID As String = HashField.GetStr(hashSpecID, DbField.GetStr(drv1, col.ColumnName))
                        '        drv2(col.ColumnName) = strNewSpecID
                        '    End If
                    Case Else
                        drv2(col.ColumnName) = drv1(col.ColumnName)
                End Select
            Next col
            drv2.EndEdit()
        Next
        'CmsDbStatement.QueryUpdate(SDbConnectionPool.GetDbConfig(), ds2, strSql2, False, True, False)
        dbs.QueryUpdate(ds2, False, True, False)
    End Sub
    Public Shared Sub AlterTable(ByRef pst As CmsPassport, ByRef dbs As DbStatement, ByVal strTable As String, ByVal ColDispName As String, ByVal columnName As String, ByVal columnType As String, ByVal columnSize As String, ByVal lngNewResID As Long)
        Dim strSql As String = "SELECT " & ColDispName & "," & columnName & "," & columnType & "," & columnSize & " FROM " & strTable & " WHERE  CD_RESID = " & lngNewResID
        Dim ds As DataSet = dbs.Query(strSql) 'NetReusables.SDbStatement.Query(strSql)
        Dim dt As DataTable = ds.Tables(0)
        Dim cmsRes As New CmsResource
        Dim tableName As String = cmsRes.AssembleTableName(lngNewResID)
        For i As Integer = 0 To dt.Rows.Count - 1
            'CTableStructure.AddShowSettings(lngNewResID, lngNewResID, Convert.ToString(dt.Rows(i)(columnName)), Convert.ToString(dt.Rows(i)(ColDispName)), Convert.ToInt64(dt.Rows(i)(columnType)), Convert.ToInt64(dt.Rows(i)(columnSize)), dbs)
            strSql = "ALTER TABLE " & tableName & " ADD " & Convert.ToString(dt.Rows(i)(columnName)) & " " & CTableStructure.ConvColDispTypeToSqlType(Convert.ToInt64(dt.Rows(i)(columnType))) & " " & CTableStructure.GetColSizeDesc(Convert.ToInt64(dt.Rows(i)(columnType)), Convert.ToInt64(dt.Rows(i)(columnSize))) & " NULL"
            dbs.Execute(strSql)
            'SDbStatement.Execute(strSql)
        Next
        'dbs.TransactionCommit()
    End Sub
    Private Shared Sub CopyRecords(ByRef pst As CmsPassport, ByRef dbs As DbStatement, ByVal strTable As String, ByVal strColAiid As String, ByVal strColResID As String, ByVal strColField1 As String, ByVal strColField2 As String, ByVal lngNewResID As Long, ByVal lngSrcResID As Long, ByRef hashCol As Hashtable, Optional ByVal blnCreateColHash As Boolean = False)
        ', Optional ByVal strColSpecID As String = "", Optional ByRef hashSpecID As Hashtable = Nothing)
        Dim ds1 As DataSet = dbs.Query("SELECT * FROM " & strTable & " WHERE " & strColResID & "=" & lngSrcResID)
        Dim dv1 As DataView = ds1.Tables(0).DefaultView
        Dim strSql2 As String = "SELECT * FROM " & strTable & " WHERE 11=22"
        Dim ds2 As DataSet = dbs.Query(strSql2)
        Dim dv2 As DataView = ds2.Tables(0).DefaultView
        Dim drv1 As DataRowView = Nothing
        For Each drv1 In dv1
            Dim drv2 As DataRowView = dv2.AddNew()
            drv2.BeginEdit()
            Dim col As DataColumn
            For Each col In ds1.Tables(0).Columns
                Select Case col.ColumnName
                    Case strColAiid
                        '自增长ID，Nothing to do
                    Case strColResID
                        drv2(col.ColumnName) = lngNewResID
                    Case strColField1
                        If blnCreateColHash = True Then
                            Dim strNewColID As String = CTableStructure.GenerateColName()
                            HashField.SetStr(hashCol, DbField.GetStr(drv1, col.ColumnName), strNewColID)
                            drv2(col.ColumnName) = strNewColID
                        Else
                            Dim strNewColID As String = HashField.GetStr(hashCol, DbField.GetStr(drv1, col.ColumnName))
                            drv2(col.ColumnName) = strNewColID
                        End If
                    Case strColField2
                        Dim strColValue As String = DbField.GetStr(drv1, col.ColumnName)
                        Dim en As IDictionaryEnumerator = hashCol.GetEnumerator()
                        While en.MoveNext
                            Dim strColNameWithBracket As String = CStr(en.Key)
                            If strColValue.IndexOf("[" & strColNameWithBracket & "]") >= 0 Then
                                Dim strNewColID As String = "[" & HashField.GetStr(hashCol, strColNameWithBracket) & "]"
                                strColValue = strColValue.Replace("[" & strColNameWithBracket & "]", strNewColID)
                            End If
                        End While
                        drv2(col.ColumnName) = strColValue
                    Case Else
                        drv2(col.ColumnName) = drv1(col.ColumnName)
                End Select
            Next col
            drv2.EndEdit()
        Next
        'CmsDbStatement.QueryUpdate(SDbConnectionPool.GetDbConfig(), ds2, strSql2, False, True, False)
        dbs.QueryUpdate(ds2, False, True, False)
    End Sub

End Class

End Namespace
