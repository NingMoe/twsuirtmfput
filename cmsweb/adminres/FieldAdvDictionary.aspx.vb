Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvDictionary
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

        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResName2.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblFieldName.Text = VStr("PAGE_COLDISPNAME")
        lblFieldType.Text = CTableStructure.GetColTypeDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Try
            '-----------------------------------------------------------------
            If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
                ViewState("PAGE_RESID2") = RLng("selresid")
            Else
                ViewState("PAGE_RESID2") = CTableColAdvDictionary.GetDictResID(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            End If
            lblDictResName.Text = ResFactory.ResService.GetResName(CmsPass, VLng("PAGE_RESID2"))
            LoadResColumns(VLng("PAGE_RESID"), ListBox1)    'Load主表字段列表
            LoadResColumns(VLng("PAGE_RESID2"), ListBox2)  'Load从表字段列表
            '-----------------------------------------------------------------

            chkAddDictRes.Checked = CTableColAdvDictionary.IsAddDictResEnabled(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            chkEditDictRes.Checked = CTableColAdvDictionary.IsEditDictResEnabled(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnSelDictRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelDictRes.Click
        Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME")
        Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CTableColAdvDictionary.SaveAdvDictSettings(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), chkAddDictRes.Checked, chkEditDictRes.Checked)
    End Sub

    Private Sub btnAddDicField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDicField.Click
        If ListBox1.SelectedItem Is Nothing Or ListBox2.SelectedItem Is Nothing Then
            PromptMsg("请选择字典所需的匹配字段！")
            Return
        End If
        If ListBox1.SelectedItem.Text Is Nothing Or ListBox2.SelectedItem.Text Is Nothing Then
            PromptMsg("请选择字典所需的匹配字段！")
            Return
        End If

        Try
            CTableColAdvDictionary.SaveDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), ListBox1.SelectedItem.Value, VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, ADVDICT_COLTYPE.Matching, chkAddDictRes.Checked, chkEditDictRes.Checked)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
    End Sub

    Private Sub btnAddDictRefCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDictRefCol.Click
        If ListBox2.SelectedItem Is Nothing Then
            PromptMsg("请选择字典参考字段！")
            Return
        End If
        If ListBox2.SelectedItem.Text Is Nothing Then
            PromptMsg("请选择字典参考字段！")
            Return
        End If

        Try
            CTableColAdvDictionary.SaveDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), "", VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, ADVDICT_COLTYPE.Reference, chkAddDictRes.Checked, chkEditDictRes.Checked)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
    End Sub

    Private Sub btnAddFilterCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFilterCol.Click
        If ListBox2.SelectedItem Is Nothing Then
            PromptMsg("请选择字典参考字段！")
            Return
        End If
        If ListBox2.SelectedItem.Text Is Nothing Then
            PromptMsg("请选择字典参考字段！")
            Return
        End If

        Try
            CTableColAdvDictionary.SaveDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), ListBox1.SelectedItem.Value, VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, ADVDICT_COLTYPE.Filter, chkAddDictRes.Checked, chkEditDictRes.Checked)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
    End Sub

    Private Sub btnDictWhere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDictWhere.Click
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME")
            Dim strUrl As String = "/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID2") & "&advdict_hostresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.AdvDictFilter & "&mnuempid=" + CmsPass.Employee.ID.Trim + "&mnucolname=" & VStr("PAGE_COLNAME")
        Response.Redirect(strUrl, True)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

            WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中部分代码联合使用
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '设置客户端的记录ID和Javascript方法，第1列是高级字段定义的资源ID
                Dim strRecID As String = row.Cells(0).Text.Trim()
                If IsNumeric(strRecID) Then
                    row.Attributes.Add("RECID", strRecID) '在客户端保存记录ID
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '在客户端生成：点击记录的响应方法OnClick()

                    If lngRecIDClicked > 0 And lngRecIDClicked = CLng(strRecID) Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '修改被点击记录的背景颜色
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            '获取关联信息的记录ID
            Dim strTemp As String = e.Item.Cells(0).Text
            If IsNumeric(strTemp.Trim()) Then
                Dim lngAiid As Long = CLng(strTemp.Trim())
                CTableColAdvDictionary.DelDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), lngAiid)    '删除关联信息
                CTableColAdvDictionary.SaveAdvDictSettings(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), chkAddDictRes.Checked, chkEditDictRes.Checked)
            Else
                PromptMsg("删除失败，数据库数据不一致！", Nothing, True)
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
    End Sub

    '---------------------------------------------------------------
    '显示资源字段列表
    '---------------------------------------------------------------
    Private Sub LoadResColumns(ByVal lngResID As Long, ByRef lstCols As ListBox)
        If lngResID = 0 Then Return

        If lstCols.Items.Count <= 0 Then
            lstCols.Items.Clear() '清空列表

            Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(CmsPass, lngResID, True, True)
            Dim datCol As DataTableColumn
            For Each datCol In alistColumns
                lstCols.Items.Add(New ListItem(datCol.ColDispName, datCol.ColName))
            Next
            alistColumns.Clear()
            alistColumns = Nothing
        End If
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "CDZ2_AIID" '关键字段
        col.DataField = "CDZ2_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "主表字段"
        col.DataField = "CDZ2_COL1_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "字典字段"
        col.DataField = "CDZ2_COL2_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "字段关系"
        col.DataField = "CDZ2_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 80

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(colDel)
        col = Nothing
        intWidth += 100

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID1 As Long, ByVal strMainColName As String, ByVal lngResID2 As Long)
        If lngResID1 = 0 Or lngResID2 = 0 Then Return

        Try
            Dim ds As DataSet = CTableColAdvDictionary.GetDictionaryByDictRes(CmsPass, lngResID1, strMainColName, lngResID2)
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            ds.Tables(0).Columns.Add("CDZ2_COL1_NAME")
            ds.Tables(0).Columns.Add("CDZ2_COL2_NAME")
            ds.Tables(0).Columns.Add("CDZ2_TYPE2")
            For Each drv In dv
                '获取字段1显示名称
                If DbField.GetStr(drv, "CDZ2_COL1") = "" Then
                    drv("CDZ2_COL1_NAME") = ""
                Else
                    drv("CDZ2_COL1_NAME") = CTableStructure.GetColDispName(CmsPass, lngResID1, DbField.GetStr(drv, "CDZ2_COL1"))
                End If

                '获取字段2显示名称
                drv("CDZ2_COL2_NAME") = CTableStructure.GetColDispName(CmsPass, lngResID2, DbField.GetStr(drv, "CDZ2_COL2"))

                '获取字段关系
                Dim intType As ADVDICT_COLTYPE = CType(DbField.GetInt(drv, "CDZ2_TYPE"), ADVDICT_COLTYPE)
                If intType = ADVDICT_COLTYPE.Matching Then
                    drv("CDZ2_TYPE2") = "匹配字段"
                ElseIf intType = ADVDICT_COLTYPE.Reference Then
                    drv("CDZ2_TYPE2") = "参考字段"
                ElseIf intType = ADVDICT_COLTYPE.Filter Then
                    drv("CDZ2_TYPE2") = "过滤字段"
                End If
            Next

            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("请选择需要操作的高级字段定义！")
                Return
            End If
            CTableColAdvDictionary.MoveUp(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("请选择需要操作的高级字段定义！")
                Return
            End If
            CTableColAdvDictionary.MoveToFirst(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("请选择需要操作的高级字段定义！")
                Return
            End If
            CTableColAdvDictionary.MoveDown(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("请选择需要操作的高级字段定义！")
                Return
            End If
            CTableColAdvDictionary.MoveToLast(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '绑定数据
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
