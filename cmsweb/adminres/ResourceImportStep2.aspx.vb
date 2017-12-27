Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceImportStep2
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSrcColType As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSrcColSize As System.Web.UI.WebControls.TextBox

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
            'ddlTableList.AutoPostBack = True
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Session("PAGE_COLDEF_DS") = Nothing '清空字段列表

        ShowDbTables() '显示数据库连接配置中能拿到的所有表单列表
        ShowSrcTableCols() '在Listbox中显示源表所有字段信息
        ShowDestTableCols() '在Listbox中显示目标资源表单所有字段信息

        GridDataBind()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub ddlTableList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTableList.SelectedIndexChanged
        Session("PAGE_COLDEF_DS") = Nothing '清空字段列表

        ShowSrcTableCols() '在Listbox中显示源表所有字段信息

        GridDataBind()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

        WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
        CreateDataGridColumn()
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            Dim ds As DataSet = GetColdefDataset()
            ds.Tables(0).Rows.RemoveAt(CInt(e.Item.ItemIndex))
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub btnAddSrcField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSrcField.Click
        '获取源和目标表单的字段名称
        Dim strSrcColName As String
        If ListBox1.SelectedItem Is Nothing Then
            PromptMsg("请选择有效的源表单字段！")
            Return
        Else
            strSrcColName = ListBox1.SelectedItem.Value.Trim()
        End If
        Dim strDestColName As String = strSrcColName

        '检验选中字段是否已经添加
        Dim ds As DataSet = GetColdefDataset()
        If IsSrcColExist(ds, strSrcColName) Then
            PromptMsg("源表单字段已经添加！")
            Return
        End If

        '添加指定字段对至临时DataSet中
        Dim hashSrcColumns As Hashtable = CType(Session("PAGE_SRCCOLS"), Hashtable)
        Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew
        drv.BeginEdit()
        drv("COL_NAME_SRC") = strSrcColName
        drv("COL_DISPNAME_SRC") = strSrcColName
        drv("COL_NAME_DEST") = strDestColName
        drv("COL_DISPNAME_DEST") = strDestColName
        drv("COL_TYPE") = CType(hashSrcColumns(strSrcColName), DataTableColumn).ColTypeDispName
        drv("COL_TYPE2") = CType(hashSrcColumns(strSrcColName), DataTableColumn).ColType
        drv("COL_SIZE") = CType(hashSrcColumns(strSrcColName), DataTableColumn).ColSize
        drv("COL_NEW") = "新建"
        drv.EndEdit()

        '显示最新的字段匹配信息
        GridDataBind()
    End Sub

    Private Sub btnAddAllSrcField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAllSrcField.Click
        Dim ds As DataSet = GetColdefDataset()

        Dim li As ListItem
        For Each li In ListBox1.Items
            Dim strSrcColName As String = li.Value
            Dim strDestColName As String = strSrcColName

            '检验选中字段是否已经添加
            If IsSrcColExist(ds, strSrcColName) = False Then
                '添加指定字段对至临时DataSet中
                Dim hashSrcColumns As Hashtable = CType(Session("PAGE_SRCCOLS"), Hashtable)
                Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew
                drv.BeginEdit()
                drv("COL_NAME_SRC") = strSrcColName
                drv("COL_DISPNAME_SRC") = strSrcColName
                drv("COL_NAME_DEST") = strDestColName
                drv("COL_DISPNAME_DEST") = strDestColName
                drv("COL_TYPE") = CType(hashSrcColumns(strSrcColName), DataTableColumn).ColTypeDispName
                drv("COL_TYPE2") = CType(hashSrcColumns(strSrcColName), DataTableColumn).ColType
                drv("COL_SIZE") = CType(hashSrcColumns(strSrcColName), DataTableColumn).ColSize
                drv("COL_NEW") = "新建"
                drv.EndEdit()
            End If
        Next

        '显示最新的字段匹配信息
        GridDataBind()
    End Sub

    Private Sub btnAddSrcDestField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSrcDestField.Click
        '获取源和目标表单的字段名称
        Dim strSrcColName As String
        If ListBox1.SelectedItem Is Nothing Then
            PromptMsg("请选择有效的源表单字段！")
            Return
        Else
            strSrcColName = ListBox1.SelectedItem.Value
        End If
        Dim strDestColName As String = ""
        If ListBox2.SelectedItem Is Nothing Then
            PromptMsg("请选择有效的目标资源表单的字段！")
            Return
        Else
            strDestColName = ListBox2.SelectedItem.Value
            If strDestColName = "" Then
                PromptMsg("请选择有效的目标资源表单的字段！")
                Return
            End If
        End If

        '检验选中字段是否已经添加
        Dim ds As DataSet = GetColdefDataset()
        If IsSrcColExist(ds, strSrcColName) Then
            PromptMsg("源表单字段已经添加！")
            Return
        End If
        If IsDestColExist(ds, strDestColName) Then
            PromptMsg("目标资源表单的字段已经添加！")
            Return
        End If

        '添加指定字段对至临时DataSet中
        Dim hashDestColumns As Hashtable = CType(Session("PAGE_DESTCOLS"), Hashtable)
        Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew
        drv.BeginEdit()
        drv("COL_NAME_SRC") = strSrcColName
        drv("COL_DISPNAME_SRC") = strSrcColName
        drv("COL_NAME_DEST") = strDestColName
        drv("COL_DISPNAME_DEST") = CType(hashDestColumns(strDestColName), DataTableColumn).ColDispName
        drv("COL_TYPE") = CType(hashDestColumns(strDestColName), DataTableColumn).ColTypeDispName
        drv("COL_TYPE2") = CType(hashDestColumns(strDestColName), DataTableColumn).ColType
        drv("COL_SIZE") = CType(hashDestColumns(strDestColName), DataTableColumn).ColSize
        drv("COL_NEW") = ""
        drv.EndEdit()

        '显示最新的字段匹配信息
        GridDataBind()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim ds As DataSet = GetColdefDataset()
            If ds.Tables(0).DefaultView.Count <= 0 Then
                PromptMsg("请先选择需要导入的字段！")
                Return
            End If

            CreateNewColumns() '创建需新建的表单字段

            Dim intOKLines As Integer = 0 '行记录保存成功次数
            Dim intErrLines As Integer = 0 '行记录保存出错次数
            If chkImportData.Checked Then
                ImportData(intOKLines, intErrLines) '开始导入数据
            End If

            ShowDestTableCols() '在Listbox中显示目标资源表单所有字段信息
            GridDataBind() '显示最新的字段匹配信息

            CmsConfig.ReloadAll()

            btnConfirm.Enabled = False
            btnAddSrcDestField.Enabled = False
            btnAddSrcField.Enabled = False
            btnAddAllSrcField.Enabled = False
            ddlTableList.Enabled = False

            PromptMsg("导入外部表单数据完成，导入成功行记录数：" & intOKLines & "；导入失败行记录数：" & intErrLines)
        Catch ex As Exception
            PromptMsg("导入外部表单失败！", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Session("PAGE_COLDEF_DS") = Nothing '清空字段列表
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Function GetColdefDataset() As DataSet
        If Session("PAGE_COLDEF_DS") Is Nothing Then
            Dim ds As New DataSet
            ds.Tables.Add(New DataTable)
            ds.Tables(0).Columns.Add("COL_NAME_SRC")
            ds.Tables(0).Columns.Add("COL_DISPNAME_SRC")
            ds.Tables(0).Columns.Add("COL_NAME_DEST")
            ds.Tables(0).Columns.Add("COL_DISPNAME_DEST")
            ds.Tables(0).Columns.Add("COL_TYPE")
            ds.Tables(0).Columns.Add("COL_TYPE2")
            ds.Tables(0).Columns.Add("COL_SIZE")
            ds.Tables(0).Columns.Add("COL_NEW")

            Session("PAGE_COLDEF_DS") = ds
        End If

        Return CType(Session("PAGE_COLDEF_DS"), DataSet)
    End Function

    '--------------------------------------------------------
    '判断ds中是否包含指定字段
    '--------------------------------------------------------
    Private Function IsSrcColExist(ByRef ds As DataSet, ByVal strSrcColName As String) As Boolean
        Dim lngCount As Long = ds.Tables(0).Select("COL_NAME_SRC='" & strSrcColName & "'").Length
        'Dim dv As New DataView
        'dv.Table = ds.Tables(0)
        'dv.RowFilter = "COL_NAME_SRC='" & strSrcColName & "'"
        If lngCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    '--------------------------------------------------------
    '判断ds中是否包含指定字段
    '--------------------------------------------------------
    Private Function IsDestColExist(ByRef ds As DataSet, ByVal strDestColName As String) As Boolean
        Dim lngCount As Long = ds.Tables(0).Select("COL_DISPNAME_DEST='" & strDestColName & "'").Length
        If lngCount > 0 Then
            Return True
        Else
            Return False
        End If

        'Dim dv As New DataView
        'dv.Table = ds.Tables(0)
        'dv.RowFilter = "COL_DISPNAME_DEST='" & strDestColName & "'"
        'If dv.Count > 0 Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "源字段ID"
        col.DataField = "COL_NAME_SRC"
        col.ItemStyle.Width = Unit.Pixel(0)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 0

        col = New BoundColumn
        col.HeaderText = "源字段"
        col.DataField = "COL_DISPNAME_SRC"
        col.ItemStyle.Width = Unit.Pixel(110)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 110

        col = New BoundColumn
        col.HeaderText = "目标字段ID"
        col.DataField = "COL_NAME_DEST"
        col.ItemStyle.Width = Unit.Pixel(0)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 0

        col = New BoundColumn
        col.HeaderText = "目标字段"
        col.DataField = "COL_DISPNAME_DEST"
        col.ItemStyle.Width = Unit.Pixel(110)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 110

        col = New BoundColumn
        col.HeaderText = "字段类型"
        col.DataField = "COL_TYPE"
        col.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "字段类型2"
        col.DataField = "COL_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(0)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 0

        col = New BoundColumn
        col.HeaderText = "字段长度"
        col.DataField = "COL_SIZE"
        col.ItemStyle.Width = Unit.Pixel(60)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 60

        col = New BoundColumn
        col.HeaderText = "字段创建"
        col.DataField = "COL_NEW"
        col.ItemStyle.Width = Unit.Pixel(60)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 60

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(40)
        DataGrid1.Columns.Add(colDel)
        col = Nothing
        intWidth += 40

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = GetColdefDataset()
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("获取自定义表字段显示信息并显示时出错！", ex)
        End Try
    End Sub

    '---------------------------------------------------------------
    '在Listbox中显示源表所有字段信息
    '---------------------------------------------------------------
    Private Sub ShowSrcTableCols()
        Dim strTableName As String = ddlTableList.SelectedValue

        Dim dbc As DbConfig = CType(Session("CMS_RESIMP_DBC"), DbConfig)
        Dim alistColumns As ArrayList = AdoxDbManager.GetTableColumnsByArraylist(dbc, strTableName)
        Dim hashSrcColumns As Hashtable = AdoxDbManager.GetTableColumnsByHashtable(dbc, strTableName)
        Session("PAGE_SRCCOLS") = hashSrcColumns

        FillFields(ListBox1, alistColumns)
    End Sub

    '---------------------------------------------------------------
    '在Listbox中显示目标资源表单所有字段信息
    '---------------------------------------------------------------
    Private Sub ShowDestTableCols()
        Dim alistColumns As New ArrayList
        Dim hashColumns As New Hashtable
        CTableStructure.GetColumnsByCollection(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).ResID, alistColumns, hashColumns)
        Session("PAGE_DESTCOLS") = hashColumns
        FillFields(ListBox2, alistColumns)
    End Sub

    '---------------------------------------------------------------
    '在Listbox中填充字段信息
    '---------------------------------------------------------------
    Private Shared Sub FillFields(ByRef lbox As ListBox, ByRef alistColumns As ArrayList)
        lbox.Items.Clear()
        Dim datCol As DataTableColumn
        For Each datCol In alistColumns
            Dim strTemp As String
            strTemp = datCol.ColDispName & " [" & datCol.ColTypeDispName & "] [" & datCol.ColSize & "]"
            Dim li As ListItem = New ListItem(strTemp, datCol.ColName)
            lbox.Items.Add(li)
            li = Nothing
        Next
    End Sub

    '----------------------------------------------------------------------
    '创建需新建的表单字段
    '----------------------------------------------------------------------
    Private Sub CreateNewColumns()
        Dim blnLimit255 As Boolean = CmsConfig.GetBool("SYS_CONFIG", "TEXTCOLUMN_LIMIT_SIZE255")

        Dim ds As DataSet = GetColdefDataset()
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            If DbField.GetStr(drv, "COL_NEW") = "新建" Then '新建字段
                Dim hashColKeyValue As New Hashtable
                'hashColKeyValue.Add("CD_COLNAME", DbField.GetStr(drv, "COL_NAME_DEST")) '字段内部名称应该自动创建
                hashColKeyValue.Add("CD_DISPNAME", DbField.GetStr(drv, "COL_DISPNAME_DEST"))
                Dim typeText As String = DbField.GetStr(drv, "COL_TYPE2")
                Dim typeLng As Long = GetTypeValue(typeText)
                hashColKeyValue.Add("CD_TYPE", typeLng)
                hashColKeyValue.Add("CD_SIZE", DbField.GetInt(drv, "COL_SIZE"))
                Dim strColName As String = CTableStructure.AddColumn(CmsPass, VLng("PAGE_RESID"), hashColKeyValue, blnLimit255)
                drv("COL_NAME_DEST") = strColName
            End If
        Next
    End Sub

#Region "根据获得的类型返回相对应的值,2007年11月13日 Add By zhangjian"
    '----------------------------------------------------------------------
    '根据获得的类型返回相对应的值,2007年11月13日 Add By zhangjian
    '----------------------------------------------------------------------
    Private Shared Function GetTypeValue(ByVal typeText As String) As Long
        Select Case typeText
            Case "Text"
                Return FieldDataType.Text
            Case "Float"
                Return FieldDataType.Float
            Case "Int32"
                Return FieldDataType.Int32
            Case "Date"
                Return FieldDataType.Date
            Case "LongText"
                Return FieldDataType.LongText
            Case "LongBinary"
                Return FieldDataType.LongBinary
            Case "Money"
                Return FieldDataType.Money
            Case "Time"
                Return FieldDataType.Time
            Case "Bit"
                Return FieldDataType.Bit
            Case Else
                Return FieldDataType.Unknown
        End Select
    End Function
#End Region

    '----------------------------------------------------------------------
    '导入表单数据，返回行记录保存出错次数
    '----------------------------------------------------------------------
    Protected Sub ImportData(ByRef intOKLines As Integer, ByRef intErrLines As Integer)
        Dim hashSrcDestCols As Hashtable = GetDestColumns() '获取待导入数据的所有字段

        '遍历源表单数据
        Dim strTableName As String = ddlTableList.SelectedValue
        Dim dbc As DbConfig = CType(Session("CMS_RESIMP_DBC"), DbConfig)
        Dim alistColumns As ArrayList = AdoxDbManager.GetTableColumnsByArraylist(dbc, strTableName)
        Dim strSql2 As String = "SELECT * FROM [" & strTableName & "]" '加[]目的：有些外部表名称中可能有空格
        Dim ds2 As DataSet = CmsDbStatement.Query(dbc, strSql2)
        Dim dv2 As DataView = ds2.Tables(0).DefaultView
        Dim drv2 As DataRowView
        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))

        Dim alistACodeFields As ArrayList = CTableColACoding.GetAutocodeFields(CmsPass, datRes.ResID)  '存放需返回的自动编码字段名称列表
        For Each drv2 In dv2
            Dim hashFieldValToSave As New Hashtable
            Try
                Dim en As IDictionaryEnumerator = hashSrcDestCols.GetEnumerator()
                While en.MoveNext
                    Dim strSrcColName As String = CStr(en.Key)
                    Dim strDestColName As String = CStr(en.Value)
                    Dim obj As Object = DbField.GetObj(drv2, strSrcColName)
                    If TypeOf obj Is String Then
                        hashFieldValToSave.Add(strDestColName, ObjField.GetStr(obj).Trim())
                    Else
                        hashFieldValToSave.Add(strDestColName, DbField.GetObj(drv2, strSrcColName))
                    End If
                End While

                '获取自动编码值
                Dim hashACodeValue As New Hashtable
                Dim strACodeColName As String = ""
                For Each strACodeColName In alistACodeFields
                    Dim strACode As String = CTableColACoding.GenerateAutoCode(CmsPass, VLng("PAGE_RESID"), strACodeColName)
                    hashACodeValue.Add(strACodeColName, strACode)
                Next

                '保存记录
                ResFactory.TableService(datRes.ResTableType).AddRecord(CmsPass, datRes.ResID, 0, hashFieldValToSave, hashACodeValue, Nothing)
                'If (intOKLines Mod 20) = 0 Then
                '    Response.Write("已传送记录" & intOKLines & "条！")
                '    Response.Flush()
                'End If

                '清内存
                hashFieldValToSave.Clear()
                hashFieldValToSave = Nothing
                intOKLines += 1
            Catch ex As Exception
                intErrLines += 1
                Dim strErr As String = ""
                If Not (hashFieldValToSave Is Nothing) Then
                    Dim en As IDictionaryEnumerator = hashFieldValToSave.GetEnumerator()
                    While en.MoveNext
                        strErr &= CStr(en.Key) & "=" & CStr(en.value) & Environment.NewLine
                    End While
                End If
                SLog.Err("导入外部表单数据时一行记录导入出错！记录内容：" & Environment.NewLine & strErr, ex)
            End Try
        Next
    End Sub

    '----------------------------------------------------------------------
    '获取待导入数据的所有字段
    '----------------------------------------------------------------------
    Protected Function GetDestColumns() As Hashtable
        Dim hashSrcDestCols As New Hashtable

        Dim ds As DataSet = GetColdefDataset()
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            hashSrcDestCols.Add(DbField.GetStr(drv, "COL_NAME_SRC", False), DbField.GetStr(drv, "COL_NAME_DEST", False))
        Next

        Return hashSrcDestCols
    End Function

    '----------------------------------------------------------------------
    '显示数据库连接配置中能拿到的所有表单列表
    '----------------------------------------------------------------------
    Private Sub ShowDbTables()
        Dim dbc As DbConfig = CType(Session("CMS_RESIMP_DBC"), DbConfig)
        Dim alistTables As ArrayList = AdoxDbManager.GetTables(dbc)
        ddlTableList.Items.Clear()
        Dim strOneTable As String
        For Each strOneTable In alistTables
            ddlTableList.Items.Add(strOneTable)
        Next
        ddlTableList.SelectedIndex = 0
    End Sub
End Class

End Namespace
