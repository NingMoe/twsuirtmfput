Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSetColumn
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

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

        If VStr("PAGE_GAINERID") = "" Then
            ViewState("PAGE_GAINERID") = RStr("gainerid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = CTableStructure.GetColumnShowsByDataset(CmsPass, VLng("PAGE_RESID"), True, True)
            PrepareDataSet(ds) '准备字段显示状态的DataSet
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

            '更新列定义时需要的参数
            Dim hashColKeyValue As New Hashtable
            'Dim strColName As String = ""
            Dim i As Integer
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim ctl As System.Web.UI.Control
                Try
                    ctl = e.Item.Cells(i).Controls(0) '隐藏列会在此行产生Exception
                Catch ex As Exception
                    ctl = Nothing
                End Try
                If Not (ctl Is Nothing) Then
                    If TypeOf ctl Is TextBox Then
                        Dim ctlCell As TextBox = CType(ctl, TextBox)
                        hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), ctlCell.Text)
                    ElseIf TypeOf ctl Is CheckBox Then
                        Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                        'hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), ctlCell.Text)
                        If ctlCell.Checked = True Then
                            hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), "显示")
                        Else
                            hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), "不显示")
                        End If
                    End If
                End If
            Next i

            Dim strColName As String = e.Item.Cells(0).Text
            If CStr(hashColKeyValue("CS_SHOW_COLUMN")) = "显示" Then
                CmsRights.SetColumnHasRights(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID"), RightsName.Resource, strColName)
            Else
                CmsRights.SetColumnHasNoRights(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID"), RightsName.Resource, strColName)
            End If
        Catch ex As Exception
            SLog.Err("更新权限列设置失败！", ex)
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
                        ElseIf TypeOf ctl Is CheckBox Then
                            Dim oneRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                            Dim strOldFieldValue As String = DbField.GetStr(oneRow, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                            Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                            If strOldFieldValue = "显示" Then
                                ctlCell.Checked = True
                            Else
                                ctlCell.Checked = False
                            End If
                            ctlCell.Width = Unit.Percentage(100)
                        End If
                    End If
                Next
            End If
        Else
        End If
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '用于保存字段名称和显示名称对，以便在其它操作时使用
        Dim hashColumnNames As New Hashtable

        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "内部字段名"
        col.DataField = "CS_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("内部字段名", "CS_COLNAME")

        col = New BoundColumn
        col.HeaderText = "字段名称"
        col.DataField = "CD_DISPNAME"
        col.ItemStyle.Width = Unit.Pixel(300)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 300
        hashColumnNames.Add("字段名称", "CD_DISPNAME")

        Dim colTemplate As New TemplateColumn
        colTemplate.HeaderText = "显示"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CS_SHOW_COLUMN", "显示")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CS_SHOW_COLUMN", "显示")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("显示"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CS_SHOW_COLUMN", "显示")
        colTemplate.ItemStyle.Width = Unit.Pixel(60)
        DataGrid1.Columns.Add(colTemplate)
        intWidth += 60
        hashColumnNames.Add("显示", "CS_SHOW_COLUMN")

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "编辑"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "取消"
        colEdit.EditText = "编辑"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colEdit)
        intWidth += 70

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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------------
    '准备字段显示状态的DataSet
    '-----------------------------------------------------------------
    Private Sub PrepareDataSet(ByRef ds As DataSet)
        '在权限表中获取权限获得者单独的字段显示设置
        Dim strColsOfNoRights As String = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID")).strQX_ACCESS_COLS
        If strColsOfNoRights <> "" Then strColsOfNoRights = "," & strColsOfNoRights & ","

        Dim strNewGainerColumns As String = ""
        '增加1个显示与否的字段
        ds.Tables(0).Columns.Add("CS_SHOW_COLUMN")
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            If strColsOfNoRights = "" Then
                drv("CS_SHOW_COLUMN") = "显示" '不应该继承显示设置，权限设置和显示设置无关
                ''之前未设置过字段显示控制，则继承资源现有的显示设置
                'If DbField.GetLng(drv, "CS_SHOW_ENABLE") = 1 Then
                '    strNewGainerColumns &= strColName & ","
                '    drv("CS_SHOW_COLUMN") = "显示"
                'Else
                '    drv("CS_SHOW_COLUMN") = "不显示"
                'End If
            Else
                Dim strColName As String = DbField.GetStr(drv, "CS_COLNAME")
                If strColsOfNoRights.IndexOf("," & strColName & ",") >= 0 Then
                    drv("CS_SHOW_COLUMN") = "不显示"
                Else
                    drv("CS_SHOW_COLUMN") = "显示"
                End If
            End If
        Next

        'If strColsOfNoRights = "" Then '之前未设置过字段显示控制，则继承资源现有的显示设置
        '    CmsRights.SetGainerColumns(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID"), RightsName.Resource, strNewGainerColumns)
        'End If
    End Sub
End Class
End Namespace
