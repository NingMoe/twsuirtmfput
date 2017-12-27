Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceColumnShowSet
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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

        GetColName()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If ResFactory.ResService.IsIndependentResource(CmsPass, VLng("PAGE_RESID")) = False Then
            btnReset.Visible = True '是继承型子资源
        Else
            btnReset.Visible = False '是独立型子资源
        End If
        btnReset.ToolTip = "恢复为父资源的显示设置"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        GridDataBind() '绑定数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = GetColShowDataSet()
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("获取自定义表字段显示信息并显示时出错！", ex)
        End Try
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

    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        GridDataBind()
    End Sub

    Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Try
            Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
            Dim hashColKeyValue As New Hashtable '存放需要增加/修改的行信息
            Dim i As Integer
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim ctl As System.Web.UI.Control
                Try
                    ctl = e.Item.Cells(i).Controls(0) '隐藏列会在此行产生Exception
                Catch ex As Exception
                    ctl = Nothing
                End Try
                If Not (ctl Is Nothing) Then
                    Dim strColName As String
                    If HashField.ContainsKey(hashColumnNames, DataGrid1.Columns(i).HeaderText) Then
                        strColName = CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText))
                        If strColName.EndsWith("22") Then
                            strColName = strColName.Substring(0, strColName.Length - 2)
                        End If
                    End If

                    If TypeOf ctl Is TextBox Then
                        Dim ctlCell As TextBox = CType(ctl, TextBox)
                        hashColKeyValue.Add(strColName, ctlCell.Text)
                    ElseIf TypeOf ctl Is DropDownList Then
                        Dim ctlCell As DropDownList = CType(ctl, DropDownList)
                        hashColKeyValue.Add(strColName, ctlCell.SelectedItem.Value)
                    ElseIf TypeOf ctl Is CheckBox Then
                        Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                        If ctlCell.Checked = True Then
                            hashColKeyValue.Add(strColName, 1)
                        Else
                            hashColKeyValue.Add(strColName, 0)
                        End If
                    End If
                End If
            Next i

            Dim strFieldName As String = e.Item.Cells(0).Text
            hashColKeyValue.Add("CS_COLNAME", strFieldName)

            '多媒体型字段不能设置为“显示”
            Dim intColType As Integer = CTableStructure.GetColType(CmsPass, VLng("PAGE_RESID"), strFieldName)
            If intColType = FieldDataType.LongBinary Then
                Dim intShowEnable As Integer = CInt(hashColKeyValue("CS_SHOW_ENABLE"))
                If intShowEnable = 1 Then
                    DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
                    DataGrid1.EditItemIndex = -1
                    GridDataBind()

                    PromptMsg("多媒体型字段不能在资源表单中显示！")
                    Return
                End If
            End If
            CTableStructure.EditShowSettings(CmsPass, VLng("PAGE_RESID"), hashColKeyValue)
        Catch ex As CmsException
            PromptMsg(ex.Message)
        Catch ex As Exception
            SLog.Err("更新字段显示设置时异常失败！", ex)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

        Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            If e.Item.ItemIndex <> -1 Then
                If e.Item.ItemType = ListItemType.EditItem Then
                    Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
                    Dim i As Integer
                    For i = 0 To DataGrid1.Columns.Count - 1
                        Dim ctl As System.Web.UI.Control
                        Try
                            ctl = e.Item.Cells(i).Controls(0) '隐藏列会在此行产生Exception
                        Catch ex As Exception
                        End Try
                        If Not (ctl Is Nothing) Then
                            If TypeOf ctl Is TextBox Then
                                Dim ctlCell As TextBox = CType(ctl, TextBox)
                                ctlCell.Width = Unit.Percentage(100)
                            ElseIf TypeOf ctl Is DropDownList Then
                                Dim ctlCell As DropDownList = CType(ctl, DropDownList)
                                ctlCell.Width = Unit.Percentage(100)

                                '必须先取消DropDownList的选中，否则在未更新前点击其它行的“编辑”时会出错。
                                ctlCell.SelectedIndex = -1

                                '---------------------------------------------------------------
                                '获取DropDownList的Cell中原来的值
                                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                                Dim strAlignType As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                                Dim lngAlignType As Long = 0
                                If strAlignType = "左对齐" Then
                                    lngAlignType = 0
                                ElseIf strAlignType = "中对齐" Then
                                    lngAlignType = 1
                                ElseIf strAlignType = "右对齐" Then
                                    lngAlignType = 2
                                End If
                                ctlCell.SelectedValue = CStr(lngAlignType) '为DropDownList赋予原来的值
                                '---------------------------------------------------------------
                            ElseIf TypeOf ctl Is CheckBox Then
                                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                                Dim lngOldFieldValue As Long = DbField.GetLng(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                                Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                                If lngOldFieldValue = 0 Then
                                    ctlCell.Checked = False
                                Else
                                    ctlCell.Checked = True
                                End If
                                ctlCell.Width = Unit.Percentage(100)
                            End If
                        End If
                    Next
                End If
            Else
            End If
        End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
            If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                Dim row As DataGridItem
                Dim strColNameClicked As String = GetColName()
                For Each row In DataGrid1.Items
                    Dim strColName As String = Trim(row.Cells(0).Text) '第1列必须是字段内部名称
                    row.Attributes.Add("RECID", strColName)
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")

                    If strColName <> "" And strColNameClicked = strColName Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '用户点击了某条记录，修改被点击记录的背景颜色
                    End If
                Next
            End If
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '用于保存字段名称和显示名称对，以便在其它操作时使用
        Dim hashColumnNames As New Hashtable

            DataGrid1.AutoGenerateColumns = False
            DataGrid1.DataKeyField = ""

        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "内部字段名"
        col.DataField = "CS_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("内部字段名", "CS_COLNAME")
        col = Nothing

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "编辑"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "取消"
        colEdit.EditText = "编辑"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(65)
        DataGrid1.Columns.Add(colEdit)
        colEdit = Nothing
        intWidth += 65

       

        col = New BoundColumn
        col.HeaderText = "字段名称"
        col.DataField = "CD_DISPNAME"
        col.ItemStyle.Width = Unit.Pixel(180)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("字段名称", "CD_DISPNAME")
        col = Nothing
        intWidth += 180

            Dim colTemplate As New TemplateColumn
            'colTemplate = New TemplateColumn
        colTemplate.HeaderText = "显示"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CS_SHOW_ENABLE", "显示")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CS_SHOW_ENABLE", "显示")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("显示"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CS_SHOW_ENABLE", "显示")
        colTemplate.ItemStyle.Width = Unit.Pixel(60)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("显示", "CS_SHOW_ENABLE")
        colTemplate = Nothing
        intWidth += 60

        col = New BoundColumn
        col.HeaderText = "显示宽度"
        col.DataField = "CS_SHOW_WIDTH"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("显示宽度", "CS_SHOW_WIDTH")
        col = Nothing
        intWidth += 70

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "对齐方式"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CS_ALIGN22", "对齐方式")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CS_ALIGN22", "对齐方式")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeDDList())
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CS_ALIGN22", "对齐方式")
        colTemplate.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("对齐方式", "CS_ALIGN22")
        colTemplate = Nothing
        intWidth += 80

        col = New BoundColumn
        col.HeaderText = "字段前缀"
        col.DataField = "CS_PREFIX"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("字段前缀", "CS_PREFIX")
        col = Nothing
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "字段后缀"
        col.DataField = "CS_SUFFIX"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("字段后缀", "CS_SUFFIX")
        col = Nothing
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "显示格式化设置"
        col.DataField = "CS_FORMAT"
        col.ItemStyle.Width = Unit.Pixel(135)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("显示格式化设置", "CS_FORMAT")
        col = Nothing
            intWidth += 135




            Dim colTemplate1 As New TemplateColumn
            colTemplate1.HeaderText = ""
            colTemplate1.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "", "显示")
            colTemplate1.ItemTemplate = New DataGridTemplate(ListItemType.Item, "", "", "", "<a href='#' onclick='SetColumnUrl(this);'>设置地址</a>")
            colTemplate1.ItemStyle.Width = Unit.Pixel(60)
            DataGrid1.Columns.Add(colTemplate1)
            '  hashColumnNames.Add("显示", "CS_SHOW_ENABLE")
            colTemplate1 = Nothing
            intWidth += 60

        DataGrid1.Width = Unit.Pixel(intWidth)

        '用于保存字段名称和显示名称对，以便在其它操作时使用
        ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
    End Sub

    '------------------------------------------------------------------
    '获取DataGrid中需要显示的CheckBox
    '------------------------------------------------------------------
    Private Function GetColumnTypeCheckbox(ByVal strCheckboxText As String) As CheckBox
        Dim objCtrl As New CheckBox
        objCtrl.Checked = False
        objCtrl.Text = strCheckboxText

        GetColumnTypeCheckbox = objCtrl
        objCtrl = Nothing
        End Function

        Private Function GetColumnTypeLiteral(ByVal strText As String) As Literal
            Dim objCtrl As New Literal
            ' objCtrl.Checked = False
            objCtrl.Text = strText
            objCtrl.ID = "ltlSetUrl"

            GetColumnTypeLiteral = objCtrl
            objCtrl = Nothing
        End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        CTableStructure.MoveUp(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveUp5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        CTableStructure.MoveUp5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        CTableStructure.MoveToFirst(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        CTableStructure.MoveDown(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveDown5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        CTableStructure.MoveDown5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        CTableStructure.MoveToLast(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnColSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColSet.Click
        Response.Redirect("/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&backpage=" & VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnInputFormSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInputFormSet.Click
        Session("CMSBP_FormDesign") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsform/FormDesign.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnuformtype=" & FormType.InputForm, False)
    End Sub

    Private Sub btnRightsSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRightsSet.Click
        Session("CMSBP_RightsSet") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

    Private Sub btnShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAll.Click
        ShowAllColumns(True)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnShowNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowNone.Click
        ShowAllColumns(False)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        CTableStructure.ResetShowSettingsFromParentResource(CmsPass, VLng("PAGE_RESID"))
        GridDataBind() '绑定数据
    End Sub

    '------------------------------------------------------------------
    '填充支持的数据类型给DropDownList
    '------------------------------------------------------------------
    Private Function GetColumnTypeDDList() As DropDownList
        Dim objCtrl As New DropDownList

        Dim li As New ListItem
        li.Text = "左对齐"
        li.Value = "0"
        objCtrl.Items.Add(li)
        li = Nothing

        li = New ListItem
        li.Text = "中对齐"
        li.Value = "1"
        objCtrl.Items.Add(li)
        li = Nothing

        li = New ListItem
        li.Text = "右对齐"
        li.Value = "2"
        objCtrl.Items.Add(li)

        Return objCtrl
    End Function

    '------------------------------------------------------------------
    '获取字段定义信息的数据集DataSet，如果指定资源没有独立的字段定义表则返回Nothing
    '------------------------------------------------------------------
    Private Function GetColShowDataSet() As DataSet
        Dim ds As DataSet = CTableStructure.GetColumnShowsByDataset(CmsPass, VLng("PAGE_RESID"), True, True)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        ds.Tables(0).Columns.Add("CS_ALIGN22")
        For Each drv In dv
            If DbField.GetLng(drv, "CS_ALIGN") = 0 Then
                drv("CS_ALIGN22") = "左对齐"
            ElseIf DbField.GetLng(drv, "CS_ALIGN") = 1 Then
                drv("CS_ALIGN22") = "中对齐"
            ElseIf DbField.GetLng(drv, "CS_ALIGN") = 2 Then
                drv("CS_ALIGN22") = "右对齐"
            End If
        Next

        Return ds
    End Function

    '-------------------------------------------------------------
    '获取当前选中的字段名称
    '-------------------------------------------------------------
    Private Function GetColName() As String
        Dim strRecID As String = RStr("RECID")
        If strRecID <> "" Then
            ViewState("PAGE_RECID") = strRecID
        Else
            If VStr("PAGE_RECID") = "" Then
                ViewState("PAGE_RECID") = RStr("columnid")
            End If
        End If
        Return VStr("PAGE_RECID")
    End Function

    '------------------------------------------------------------------
    '设置当前所有字段是否显示
    '------------------------------------------------------------------
    Private Sub ShowAllColumns(ByVal blnShow As Boolean)
        Dim intShow As Integer = CInt(IIf(blnShow = True, 1, 0))
        Dim lngResID As Long = VLng("PAGE_RESID")
        Dim ds As DataSet = CTableStructure.GetColumnShowsByDataset(CmsPass, lngResID, True, True)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            Dim hashColKeyValue As New Hashtable '存放需要增加/修改的行信息
            hashColKeyValue.Add("CS_COLNAME", DbField.GetStr(drv, "CS_COLNAME"))
            hashColKeyValue.Add("CS_SHOW_ENABLE", intShow)
            CTableStructure.EditShowSettings(CmsPass, lngResID, hashColKeyValue)
        Next
    End Sub
End Class
End Namespace
