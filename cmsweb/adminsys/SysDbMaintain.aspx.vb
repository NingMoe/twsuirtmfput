Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SysDbMaintain
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
        lbtnCheckColumn.Attributes.Add("onClick", "return CmsPrmoptConfirm('当前操作需要较长时间，确定要执行吗？');")
        lbtnCheckDoc.Attributes.Add("onClick", "return CmsPrmoptConfirm('当前操作需要较长时间，确定要执行吗？');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub lbtnCheckColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCheckColumn.Click
        Try
            '被删除的非一致性字段
            Dim strWhere As String = ""
            Dim blnError As Boolean = False

            Dim lngOneNum1 As Long = 0
            Dim lngOneNum2 As Long = 0
            Dim lngOneNum3 As Long = 0
            Dim lngOneNum4 As Long = 0
            Dim lngOneNum5 As Long = 0
            Dim lngOneNum6 As Long = 0
            Dim lngOneNum7 As Long = 0
            Dim lngOneNum8 As Long = 0
            Dim lngOneNum9 As Long = 0
            Dim lngOneNum10 As Long = 0
            Dim lngOneNum11 As Long = 0
            Dim lngOneNum12 As Long = 0

            '遍历所有资源
            Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), CmsTables.Resource, "")
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            For Each drv In dv
                Try
                    Dim lngResID As Long = DbField.GetLng(drv, "ID")
                    Dim lngIndepResPID As Long = CmsPass.GetDataRes(lngResID).IndepParentResID

                    '校验显示设置
                    strWhere = "CS_RESID=" & lngResID & " AND (CS_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum1 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ColShow, strWhere)

                    '绝对不能删除窗体设计中的任何信息，因为窗体中除字段定义外，还有其它如Label等的定义
                    ''校验窗体设计
                    'strWhere = "CI_RESID=" & lngResID & " AND CI_COLNAME<> '' AND (NOT (CI_COLNAME IS NULL)) AND (CI_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngDelNum += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ColInput, strWhere)

                    '校验自动编码
                    strWhere = "CDC_RESID=" & lngResID & " AND (CDC_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum2 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldAutoCoding, strWhere)

                    '校验计算公式，不能删除校验公式
                    strWhere = "CDJ_RESID=" & lngResID & " AND (CDJ_TYPE=0 OR CDJ_TYPE IS NULL) AND (CDJ_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum3 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldCalculation, strWhere)

                    '校验定制编码
                    strWhere = "CDD_RESID=" & lngResID & " AND (CDD_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum4 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldCustomizeCoding, strWhere)

                    '校验高级字典
                    strWhere = "CDZ2_RESID1=" & lngResID & " AND CDZ2_MAINCOL<> '' AND (NOT (CDZ2_MAINCOL IS NULL)) AND (CDZ2_MAINCOL NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum5 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldDictionary, strWhere)
                    strWhere = "CDZ2_RESID1=" & lngResID & " AND CDZ2_COL1<> '' AND (NOT (CDZ2_COL1 IS NULL)) AND (CDZ2_COL1 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum5 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldDictionary, strWhere)
                    strWhere = "CDZ2_RESID2=" & lngResID & " AND CDZ2_COL2<> '' AND (NOT (CDZ2_COL2 IS NULL)) AND (CDZ2_COL2 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum5 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldDictionary, strWhere)

                    '校验下拉框选择项
                    strWhere = "CDX_RESID=" & lngResID & " AND (CDX_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum6 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldOption, strWhere)

                    '校验多选一选择项
                    strWhere = "CDR_RESID=" & lngResID & " AND (CDR_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum7 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldRadio, strWhere)

                    '校验关联表
                    strWhere = "RT_TAB1_RESID=" & lngResID & " AND RT_TAB1_COLNAME<> '' AND (NOT (RT_TAB1_COLNAME IS NULL)) AND (RT_TAB1_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum8 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RelatedTable, strWhere)
                    strWhere = "RT_TAB2_RESID=" & lngResID & " AND RT_TAB2_COLNAME<> '' AND (NOT (RT_TAB2_COLNAME IS NULL)) AND (RT_TAB2_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum8 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RelatedTable, strWhere)

                    '校验字典模板
                    'strWhere = "DICTT_RESID=" & lngResID & " AND DICTT_COLNAME<> '' AND (NOT (DICTT_COLNAME IS NULL)) AND (DICTT_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngOneNum9 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.DictTemplate, strWhere)

                    '校验数据模板
                    'strWhere = "DTMPL_HOSTRESID=" & lngResID & " AND DTMPL_COL1<> '' AND (NOT (DTMPL_COL1 IS NULL)) AND (DTMPL_COL1 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngOneNum10 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RecTemplate, strWhere)
                    'strWhere = "DTMPL_DATARESID=" & lngResID & " AND DTMPL_COL2<> '' AND (NOT (DTMPL_COL2 IS NULL)) AND (DTMPL_COL2 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngOneNum10 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RecTemplate, strWhere)

                    '校验多应用表
                    strWhere = "MTS_RESID=" & lngResID & " AND MTS_COLNAME<> '' AND (NOT (MTS_COLNAME IS NULL)) AND (MTS_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum11 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.MTableHost, strWhere)
                    strWhere = "MTSCOL_RESID=" & lngResID & " AND MTSCOL_COLNAME<> '' AND (NOT (MTSCOL_COLNAME IS NULL)) AND (MTSCOL_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum11 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.MTableColDef, strWhere)

                    '校验数据同步
                    strWhere = "SYNCOL_HOSTRESID=" & lngResID & " AND SYNCOL_COLNAME1<> '' AND (NOT (SYNCOL_COLNAME1 IS NULL)) AND (SYNCOL_COLNAME1 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum12 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ResSyncCol, strWhere)
                    strWhere = "SYNCOL_SYNCRESID=" & lngResID & " AND SYNCOL_COLNAME2<> '' AND (NOT (SYNCOL_COLNAME2 IS NULL)) AND (SYNCOL_COLNAME2 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum12 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ResSyncCol, strWhere)
                Catch ex As Exception
                    blnError = True
                    SLog.Err("删除非一致性字段定义异常失败，但操作继续。", ex)
                End Try
            Next
            CmsConfig.ReloadAll()

            Dim strNumMsg As String = ""
            strNumMsg &= " 显示设置(" & lngOneNum1 & ") "
            strNumMsg &= " 自动编码(" & lngOneNum2 & ") "
            strNumMsg &= " 计算公式(" & lngOneNum3 & ") "
            strNumMsg &= " 定制编码(" & lngOneNum4 & ") "
            strNumMsg &= " 高级字典(" & lngOneNum5 & ") "
            strNumMsg &= " 下拉框选择项(" & lngOneNum6 & ") "
            strNumMsg &= " 多选一选择项(" & lngOneNum7 & ") "
            strNumMsg &= " 关联表(" & lngOneNum8 & ") "
            strNumMsg &= " 字典模板(" & lngOneNum9 & ") "
            strNumMsg &= " 数据模板(" & lngOneNum10 & ") "
            strNumMsg &= " 多应用表(" & lngOneNum11 & ") "
            strNumMsg &= " 数据同步(" & lngOneNum12 & ") "
            Dim lngDelNum As Long = lngOneNum1 + lngOneNum2 + lngOneNum3 + lngOneNum4 + lngOneNum5 + lngOneNum6 + lngOneNum7 + lngOneNum8 + lngOneNum9 + lngOneNum10 + lngOneNum11 + lngOneNum12

            If blnError = True Then
                PromptMsg("校验部分失败，删除" & lngDelNum & "个非一致性字段定义。详细信息：" & strNumMsg)
            Else
                PromptMsg("校验完成，删除" & lngDelNum & "个非一致性字段定义。详细信息：" & strNumMsg)
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnCheckDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCheckDoc.Click
        Dim strSql As String = "SELECT DOC2_ID, DOC2_RESID1, DOC2_RESID2, DOC2_RESID3, DOC2_RESID4, DOC2_RESID5 FROM " & CmsDatabase.DocDatabaseLink
        Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        Dim lngDelNum As Long = 0
        For Each drv In dv
            Try
                Dim strWhere As String = ""
                Dim lngResID1 As Long = DbField.GetLng(drv, "DOC2_RESID1")
                If lngResID1 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID1)
                Dim lngResID2 As Long = DbField.GetLng(drv, "DOC2_RESID2")
                If lngResID2 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID2)
                Dim lngResID3 As Long = DbField.GetLng(drv, "DOC2_RESID3")
                If lngResID3 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID3)
                Dim lngResID4 As Long = DbField.GetLng(drv, "DOC2_RESID4")
                If lngResID4 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID4)
                Dim lngResID5 As Long = DbField.GetLng(drv, "DOC2_RESID5")
                If lngResID5 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID5)
                Dim lngNum As Long = CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), CmsTables.Resource, strWhere)
                If lngNum <= 0 Then
                    Dim lngDocID As Long = DbField.GetLng(drv, "DOC2_ID")
                    lngDelNum += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsDatabase.DocDatabaseLink, "DOC2_ID=" & lngDocID)
                End If
            Catch ex As Exception
                SLog.Err("删除无所属资源的文档时异常出错，但操作继续。", ex)
            End Try
        Next

        PromptMsg("文档校验完成，删除" & lngDelNum & "个文档")
    End Sub
End Class

End Namespace
