Option Strict On
Option Explicit On 

Imports NetReusables
Imports System.Data.OleDb
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceColumnSet
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtColDispName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtColSize As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAddOneColumn As System.Web.UI.WebControls.Button
    Protected WithEvents ddlColType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtColWidth As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtColComments As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkColShow As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkColIndex As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkColReadonly As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents btnFormulaList As System.Web.UI.WebControls.Button

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private m_blnAddDelAttribute As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        GetColName()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        btnDelCol.Attributes.Add("onClick", "return CmsPrmoptConfirm('删除字段后该字段中的所有内容将被删除，确定要删除字段吗？');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind() '绑定数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = ColdefGetColumnsForColMgr()
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("绑定字段信息并显示时出错！", ex)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

        WebUtilities.InitialDataGrid(DataGrid1) '初始化DataGrid属性
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

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            Dim strColName As String = e.Item.Cells(2).Text '字段内部名称是第3列
            CTableStructure.DeleteColumn(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Try
            Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
            Dim hashColKeyValue As New Hashtable '存放需要增加/修改的行信息
            Dim strColNameField As String = ""
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

            '----------------------------------------------------------------
            '保存行信息至数据库
            Dim strCD_ID As String = e.Item.Cells(0).Text '此Cell不显示，作为判断新增还是修改记录
            If strCD_ID = "0" Then '新增记录
                '设置默认值类型为：输入型 0
                If HashField.ContainsKey(hashColKeyValue, "CD_VALTYPE") Then
                    hashColKeyValue("CD_VALTYPE") = FieldValueType.Input
                Else
                    hashColKeyValue.Add("CD_VALTYPE", FieldValueType.Input)
                End If

                Dim blnLimit255 As Boolean = CmsConfig.GetBool("SYS_CONFIG", "TEXTCOLUMN_LIMIT_SIZE255")
                CTableStructure.AddColumn(CmsPass, VLng("PAGE_RESID"), hashColKeyValue, blnLimit255)
            Else '修改记录
                If HashField.ContainsKey(hashColKeyValue, "CD_VALTYPE") Then
                    hashColKeyValue.Remove("CD_VALTYPE") '高级设置（值类型）不能修改
                End If

                Dim strColName As String
                If CmsConfig.ShowColumnName = True Then
                    '字段内部名称若显示，则已经在Hashtable中
                    strColName = CStr(hashColKeyValue("CD_COLNAME"))
                Else
                    '只有在字段内部名称不显示的情况下，才可以这样获得，否则获得空值。
                    strColName = e.Item.Cells(2).Text '字段内部名称必须是第3列
                End If
                CTableStructure.EditColumn(CmsPass, VLng("PAGE_RESID"), strColName, hashColKeyValue, CmsConfig.GetBool("SYS_CONFIG", "TEXTCOLUMN_LIMIT_SIZE255"))
            End If
            '----------------------------------------------------------------
        Catch ex As Exception
            SLog.Err("更新字段属性异常失败", ex)
            PromptMsg(ex.Message)
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
                            Dim strOldFieldValue As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))

                            '为DropDownList赋予原来的值
                            Dim item As ListItem = ctlCell.Items.FindByText(strOldFieldValue)
                            If Not item Is Nothing Then item.Selected = True
                            '---------------------------------------------------------------
                        ElseIf TypeOf ctl Is CheckBox Then
                            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                            Dim strOldFieldValue As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                            Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                            If strOldFieldValue = "是" Then
                                ctlCell.Checked = True
                            ElseIf strOldFieldValue = "否" Then
                                ctlCell.Checked = False
                            End If
                            ctlCell.Width = Unit.Percentage(100)
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnAdvSetting2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvSetting2.Click
        btnAdvSetting_Click(sender, e)
    End Sub

    Private Sub btnAddCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCol.Click
        Dim ds As DataSet = ColdefGetColumnsForColMgr()
        Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew()
        drv.BeginEdit()
        drv("CD_ID") = 0 '特殊标记，以便在更新时知道这是新增1条记录，而不是修改记录，此信息不会记录入数据库中
        drv("CD_COLNAME") = ""
        drv("CD_DISPNAME") = ""
        drv("CD_TYPE") = 0
        drv("CD_TYPE22") = ""
        drv("CD_SIZE") = 8
        drv.EndEdit()

        DataGrid1.EditItemIndex = ds.Tables(0).DefaultView.Count - 1
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub

    Private Sub btnAddCol2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCol2.Click
        btnAddCol_Click(sender, e)
    End Sub

    Private Sub btnAdvSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvSetting.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        '获取字段的值类型，如果是非输入型，则直接跳到相应的值类型管理界面
        Dim lngColValType As Long = CTableStructure.GetColValType(CmsPass, VLng("PAGE_RESID"), strColName)
        Select Case lngColValType
            Case FieldValueType.Input
                Session("CMSBP_FieldAdvanceSetting") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvanceSetting.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.OptionValue
                Session("CMSBP_FieldAdvOption") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvOption.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.RadioGroup
                Session("CMSBP_FieldAdvRadio") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvRadio.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.Checkbox
                Session("CMSBP_FieldAdvCheckbox") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvCheckbox.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.AutoCoding
                Session("CMSBP_FieldAdvAutoCoding") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvAutoCoding.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.Calculation
                Session("CMSBP_FieldAdvCalculation") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName & "&urlfmltype=" & FormulaType.IsCalculation, False)

            Case FieldValueType.AdvDictionary
                Session("CMSBP_FieldAdvDictionary") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.Constant
                Session("CMSBP_FieldAdvConstant") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvConstant.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.DefaultValue
                Session("CMSBP_FieldAdvDefaultValue") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvDefaultValue.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.CustomizeCoding
                Session("CMSBP_FieldAdvCustomizeCoding") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvCustomizeCoding.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.IncrementalCoding
                PromptMsg("递增编码类型没有设置界面！")

            Case FieldValueType.DirectoryFile
                PromptMsg("目录文件类型没有设置界面！")

            Case Else
                PromptMsg("不能识别的字段类型，数据库数据不一致！")
        End Select
    End Sub

    Private Sub btnCopyColDef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyColDef.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            Dim hashColKeyValue As New Hashtable
            CTableStructure.CopyColumn(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try

        GridDataBind() '绑定数据
    End Sub

    Private Sub btnCopyColDef2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyColDef2.Click
        btnCopyColDef_Click(sender, e)
    End Sub

    Private Sub btnDelCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelCol.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.DeleteColumn(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind() '绑定数据
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnExit2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit2.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '用于保存字段名称和显示名称对，以便在其它操作时使用
        Dim hashColumnNames As New Hashtable

        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        '第1列必须是记录ID
        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "字段ID"
        col.DataField = "CD_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("字段ID", "CD_ID")
        col = Nothing

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "编辑"
        colEdit.EditText = "编辑"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "取消"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(65)
        DataGrid1.Columns.Add(colEdit)
        intWidth += 65
        colEdit = Nothing

        '第2列必须是字段内部名称
        col = New BoundColumn
        col.HeaderText = "内部字段名"
        col.DataField = "CD_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(115)
        If CmsConfig.ShowColumnName = True Then
            col.Visible = True
            col.ReadOnly = False
            intWidth += 115
        Else
            col.Visible = False
            col.ReadOnly = True
        End If
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("内部字段名", "CD_COLNAME")
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "字段名称"
        col.DataField = "CD_DISPNAME"
        col.ItemStyle.Width = Unit.Pixel(180)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("字段名称", "CD_DISPNAME")
        intWidth += 180
        col = Nothing

        Dim colTemplate As New TemplateColumn
        colTemplate.HeaderText = "字段类型"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_TYPE22", "字段类型")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_TYPE22", "字段类型")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeDDList())
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_TYPE22", "字段类型")
        colTemplate.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("字段类型", "CD_TYPE22")
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "长度"
        col.DataField = "CD_SIZE"
        col.ItemStyle.Width = Unit.Pixel(40)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("长度", "CD_SIZE")
        intWidth += 40
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "高级设置"
        col.DataField = "CD_VALTYPE22"
        col.ItemStyle.Width = Unit.Pixel(90)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("高级设置", "CD_VALTYPE22")
        intWidth += 90
        col = Nothing

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "唯一值"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNIQUE22", "唯一值")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNIQUE22", "唯一值")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("唯一值"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNIQUE22", "唯一值")
        colTemplate.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("唯一值", "CD_IS_UNIQUE22")
        intWidth += 70

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "联合唯一"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNITE_UNIQUE22", "联合唯一")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNITE_UNIQUE22", "联合唯一")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("联合唯一"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNITE_UNIQUE22", "联合唯一")
        colTemplate.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("联合唯一", "CD_IS_UNITE_UNIQUE22")
        intWidth += 80

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "查询字段"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_SEARCH22", "查询字段")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_SEARCH22", "查询字段")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("查询字段"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_SEARCH22", "查询字段")
        colTemplate.ItemStyle.Width = Unit.Pixel(85)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("查询字段", "CD_IS_SEARCH22")
        intWidth += 85

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "只读"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_READONLY22", "只读")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_READONLY22", "只读")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("只读"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_READONLY22", "只读")
        colTemplate.ItemStyle.Width = Unit.Pixel(55)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("只读", "CD_IS_READONLY22")
        intWidth += 55

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "必填项"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_NONULL22", "必填项")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_NONULL22", "必填项")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("必填项"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_NONULL22", "必填项")
        colTemplate.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("必填项", "CD_IS_NONULL22")
        intWidth += 70

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "不可修改"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNEDITABLE22", "不可修改")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNEDITABLE22", "不可修改")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("不可修改"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNEDITABLE22", "不可修改")
        colTemplate.ItemStyle.Width = Unit.Pixel(85)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("不可修改", "CD_IS_UNEDITABLE22")
        intWidth += 85

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "有值后不可修改"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNMODIFIED22", "有值后不可修改")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNMODIFIED22", "有值后不可修改")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("有值后不可修改"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNMODIFIED22", "有值后不可修改")
        colTemplate.ItemStyle.Width = Unit.Pixel(130)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("有值后不可修改", "CD_IS_UNMODIFIED22")
        intWidth += 130

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "是索引"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_INDEX_NAME22", "是索引")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_INDEX_NAME22", "是索引")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("是索引"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_INDEX_NAME22", "是索引")
        colTemplate.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("是索引", "CD_INDEX_NAME22")
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "小数精度"
        col.DataField = "CD_FLOAT_PRECISION"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("小数精度", "CD_FLOAT_PRECISION")
        intWidth += 70
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "记录锁定值"
        col.DataField = "CD_LOCKVALUE"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("记录锁定值", "CD_LOCKVALUE")
        intWidth += 150
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "参数1"
        col.DataField = "CD_INT1"
        col.ItemStyle.Width = Unit.Pixel(70)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("参数1", "CD_INT1")
        intWidth += 70
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "参数2"
        col.DataField = "CD_INT2"
        col.ItemStyle.Width = Unit.Pixel(70)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("参数2", "CD_INT2")
        intWidth += 70
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "参数3"
        col.DataField = "CD_STR3"
        col.ItemStyle.Width = Unit.Pixel(250)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("参数3", "CD_STR3")
        intWidth += 250
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "参数4"
        col.DataField = "CD_STR4"
        col.ItemStyle.Width = Unit.Pixel(250)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("参数4", "CD_STR4")
        intWidth += 250
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "字段注释"
        col.DataField = "CD_NOTES"
        col.ItemStyle.Width = Unit.Pixel(400)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("字段注释", "CD_NOTES")
        intWidth += 400
        col = Nothing

        'Dim colDel As ButtonColumn = New ButtonColumn
        'colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        'colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        'colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '或者用图标，如："<img src='/cmsweb/images/common/delete.gif' border=0>"
        'colDel.CommandName = "Delete"
        'colDel.ButtonType = ButtonColumnType.LinkButton
        'colDel.ItemStyle.Width = Unit.Pixel(40)
        'colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        'DataGrid1.Columns.Add(colDel)
        'intWidth += 40

        DataGrid1.Width = Unit.Pixel(intWidth)

        '用于保存字段名称和显示名称对，以便在其它操作时使用
        ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
    End Sub

    '------------------------------------------------------------------
    '填充支持的数据类型给DropDownList
    '------------------------------------------------------------------
    Private Function GetColumnTypeDDList() As DropDownList
        Dim objCtrl As New DropDownList
        Dim alistColTypes As ArrayList = CTableStructure.GetColumnTypes()

        Dim fldTypePair As FieldTypePair
        For Each fldTypePair In alistColTypes
            Dim li As ListItem = New ListItem(fldTypePair.strFieldTypeDispName, CStr(fldTypePair.lngFieldType))
            objCtrl.Items.Add(li)
            li = Nothing
        Next

        alistColTypes.Clear()
        alistColTypes = Nothing

        Return objCtrl
    End Function

    '------------------------------------------------------------------
    '填充支持的数据类型给DropDownList
    '------------------------------------------------------------------
    Private Function GetDefaultValueDDList() As DropDownList
        Dim objCtrl As New DropDownList
        Dim alistDefVal As ArrayList = CTableStructure.GetDefaultValNames()

        Dim en As IEnumerator = alistDefVal.GetEnumerator()
        Do While en.MoveNext
            objCtrl.Items.Add(CType(en.Current, String))
        Loop
        en = Nothing
        alistDefVal.Clear()
        alistDefVal = Nothing

        Return objCtrl
    End Function

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

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中
        '2部分代码联合使用：1）Javascript方法RowLeftClickNoPost()。2）添加一个hidden变量：<input type="hidden" name="RECID">
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim i As Integer
            Dim strColNameClicked As String = GetColName()
            For i = 0 To DataGrid1.Items.Count - 1
                Dim row As DataGridItem = DataGrid1.Items(i)
                '为第二列的删除按钮添加确认操作
                Dim strColName As String = Trim(row.Cells(2).Text) '字段内部名称必须是第3列
                row.Attributes.Add("RECID", strColName)
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")

                If strColName <> "" And strColNameClicked = strColName Then
                    row.Attributes.Add("bgColor", "#C4D9F9") '用户点击了某条记录，修改被点击记录的背景颜色
                End If
            Next
        End If
    End Sub

    Private Sub btnDelAdvSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelAdvSetting.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            '获取原先的高级设置类型，以便删除所有高级设置
            Dim lngAdvType As Long = CTableColInput.GetInputType(CmsPass, VLng("PAGE_RESID"), strColName)
            Select Case lngAdvType
                Case FieldValueType.AutoCoding
                    CTableColACoding.DeleteAll(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.Calculation
                    CTableColCalculation.DelFormula(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.Constant
                    CTableColConstant.DelConstant(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.CustomizeCoding
                    CTableColCustomizeCoding.DelCustCoding(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.IncrementalCoding
                    CTableColIncrementalCoding.DelCoding(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.DirectoryFile
                    CTableColDirectoryFile.Delete(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.DefaultValue
                    CTableColDefaultValue.DelDefaultVal(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.AdvDictionary
                    CTableColAdvDictionary.DelDictionary(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.Input
                    CTableStructure.SetColumnDefinition(CmsPass, VLng("PAGE_RESID"), strColName, "", FieldValueType.Input)

                Case FieldValueType.OptionValue
                    CTableColOption.Delete(CmsPass, VLng("PAGE_RESID"), strColName)

                Case Else
                    CTableStructure.SetColumnDefinition(CmsPass, VLng("PAGE_RESID"), strColName, "", FieldValueType.Input)

            End Select
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind() '绑定数据
    End Sub

    Private Sub btnDelAdvSetting2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelAdvSetting2.Click
        btnDelAdvSetting_Click(sender, e)
    End Sub

    '------------------------------------------------------------------
    '获取字段定义信息的数据集DataSet，如果指定资源没有独立的字段定义表则返回Nothing
    '------------------------------------------------------------------
    Public Function ColdefGetColumnsForColMgr() As DataSet
        Dim ds As DataSet = CTableStructure.GetColumnsByDataset(CmsPass, VLng("PAGE_RESID"), True, True)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        ds.Tables(0).Columns.Add("CD_IS_UNIQUE22")
        ds.Tables(0).Columns.Add("CD_IS_UNITE_UNIQUE22")

        ds.Tables(0).Columns.Add("CD_IS_SEARCH22")

        ds.Tables(0).Columns.Add("CD_IS_READONLY22")
        ds.Tables(0).Columns.Add("CD_IS_NONULL22")
        ds.Tables(0).Columns.Add("CD_IS_UNEDITABLE22")
        ds.Tables(0).Columns.Add("CD_IS_UNMODIFIED22")
        ds.Tables(0).Columns.Add("CD_INDEX_NAME22")
        ds.Tables(0).Columns.Add("CD_VALTYPE22")
        ds.Tables(0).Columns.Add("CD_TYPE22")
        For Each drv In dv
            If DbField.GetLng(drv, "CD_IS_UNIQUE") = 1 Then
                drv("CD_IS_UNIQUE22") = "是"
            Else
                drv("CD_IS_UNIQUE22") = "否"
            End If

            If DbField.GetLng(drv, "CD_IS_UNITE_UNIQUE") = 1 Then
                drv("CD_IS_UNITE_UNIQUE22") = "是"
            Else
                drv("CD_IS_UNITE_UNIQUE22") = "否"
            End If

            If DbField.GetLng(drv, "CD_IS_SEARCH") = 1 Then
                drv("CD_IS_SEARCH22") = "是"
            Else
                drv("CD_IS_SEARCH22") = "否"
            End If

            If DbField.GetLng(drv, "CD_IS_READONLY") = 1 Then
                drv("CD_IS_READONLY22") = "是"
            Else
                drv("CD_IS_READONLY22") = "否"
            End If

            If DbField.GetLng(drv, "CD_IS_NONULL") = 1 Then
                drv("CD_IS_NONULL22") = "是"
            Else
                drv("CD_IS_NONULL22") = "否"
            End If

            If DbField.GetLng(drv, "CD_IS_UNEDITABLE") = 1 Then
                drv("CD_IS_UNEDITABLE22") = "是"
            Else
                drv("CD_IS_UNEDITABLE22") = "否"
            End If

            If DbField.GetLng(drv, "CD_IS_UNMODIFIED") = 1 Then
                drv("CD_IS_UNMODIFIED22") = "是"
            Else
                drv("CD_IS_UNMODIFIED22") = "否"
            End If

            If DbField.GetStr(drv, "CD_INDEX_NAME") <> "" Then
                drv("CD_INDEX_NAME22") = "是"
            Else
                drv("CD_INDEX_NAME22") = "否"
            End If

            drv("CD_VALTYPE22") = CTableStructure.GetValTypeDispName(DbField.GetLng(drv, "CD_VALTYPE"))

            drv("CD_TYPE22") = CTableStructure.ConvColTypeToDispName(DbField.GetLng(drv, "CD_TYPE"))
        Next

        Return ds
    End Function

    Private Sub btnColShowSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColShowSet.Click
        Response.Redirect("/cmsweb/adminres/ResourceColumnShowSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&backpage=" & VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnInputFormSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInputFormSet.Click
        Session("CMSBP_FormDesign") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsform/FormDesign.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnuformtype=" & FormType.InputForm, False)
    End Sub

    Private Sub btnRightsSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRightsSet.Click
        Session("CMSBP_RightsSet") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

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

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.MoveUp(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveUp5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.MoveUp5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.MoveToFirst(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.MoveDown(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveDown5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.MoveDown5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的字段")
            Return
        End If

        Try
            CTableStructure.MoveToLast(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '绑定数据
    End Sub
End Class
End Namespace
