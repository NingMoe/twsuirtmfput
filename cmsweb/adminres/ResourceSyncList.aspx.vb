Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceSyncList
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
        lblPageTitle.Text = "数据同步"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind(VLng("PAGE_RESID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_ResourceSyncListEdit") = "/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=0"
        Response.Redirect("/cmsweb/adminres/ResourceSyncListEdit.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=0", False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("请选择需要操作的数据同步设置！")
            Return
        End If

        Session("CMSBP_ResourceSyncListEdit") = "/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & lngSyncListID
        Response.Redirect("/cmsweb/adminres/ResourceSyncListEdit.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & lngSyncListID, False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("请选择需要操作的数据同步设置！")
            Return
        End If

        Try
            CmsResourceSync.DelSyncResource(CmsPass, lngSyncListID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnSyncCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyncCol.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("请选择需要操作的数据同步设置！")
            Return
        End If

        Session("CMSBP_ResourceSyncColumn") = "/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & lngSyncListID
        Response.Redirect("/cmsweb/adminres/ResourceSyncColumn.aspx?mnuresid=" & VLng("PAGE_RESID") & "&sync_listid=" & lngSyncListID, False)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("请选择需要操作的数据同步设置！")
            Return
        End If

        Try
            CmsResourceSync.MoveUpSyncList(CmsPass, lngSyncListID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("请选择需要操作的数据同步设置！")
            Return
        End If

        Try
            CmsResourceSync.MoveDownSyncList(CmsPass, lngSyncListID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
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

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中部分代码联合使用
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '设置客户端的记录ID和Javascript方法，第三列是关联表的资源ID
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

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        '第1列必须是记录ID
        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "SYNLST_ID"
        col.DataField = "SYNLST_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "主表资源"
        col.DataField = "SYNLST_HOSTRESID22"
        col.ItemStyle.Width = Unit.Pixel(200)
        DataGrid1.Columns.Add(col)
        intWidth += 200

        col = New BoundColumn
        col.HeaderText = "同步资源"
        col.DataField = "SYNLST_SYNCRESID22"
        col.ItemStyle.Width = Unit.Pixel(200)
        DataGrid1.Columns.Add(col)
        intWidth += 200

        col = New BoundColumn
        col.HeaderText = "同步操作"
        col.DataField = "SYNLST_ACTION22"
        col.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(col)
        intWidth += 80

        col = New BoundColumn
        col.HeaderText = "同步类型"
        col.DataField = "SYNLST_SYNCTYPE22"
        col.ItemStyle.Width = Unit.Pixel(200)
        DataGrid1.Columns.Add(col)
        intWidth += 200

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID As Long)
        Dim ds As DataSet = GetDataSetForGrid(lngResID)
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub

    '------------------------------------------------------------------
    '获取所有关联表的列表
    '------------------------------------------------------------------
    Private Function GetDataSetForGrid(ByVal lngResID As Long) As DataSet
        Dim ds As DataSet = CmsResourceSync.GetSyncList(CmsPass, lngResID)
        ds.Tables(0).Columns.Add("SYNLST_HOSTRESID22")
        ds.Tables(0).Columns.Add("SYNLST_SYNCRESID22")
        ds.Tables(0).Columns.Add("SYNLST_ACTION22")
        ds.Tables(0).Columns.Add("SYNLST_SYNCTYPE22")
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            drv.BeginEdit()
            drv("SYNLST_HOSTRESID22") = CmsPass.GetDataRes(lngResID).ResName
            drv("SYNLST_SYNCRESID22") = CmsPass.GetDataRes(DbField.GetLng(drv, "SYNLST_SYNCRESID")).ResName

            Dim intSyncAction As ResSyncAction = CType(DbField.GetInt(drv, "SYNLST_ACTION"), ResSyncAction)
            If intSyncAction = ResSyncAction.AddRecord Then
                drv("SYNLST_ACTION22") = "添加记录"
            ElseIf intSyncAction = ResSyncAction.EditRecord Then
                drv("SYNLST_ACTION22") = "修改记录"
            ElseIf intSyncAction = ResSyncAction.DelRecord Then
                drv("SYNLST_ACTION22") = "删除记录"
            End If

            Dim intSyncType As ResSyncType = CType(DbField.GetInt(drv, "SYNLST_SYNCTYPE"), ResSyncType)
            If intSyncType = ResSyncType.AddOrEditRecord Then
                drv("SYNLST_SYNCTYPE22") = "在同步资源中添加或修改记录"
            ElseIf intSyncType = ResSyncType.AddRecordOnly Then
                drv("SYNLST_SYNCTYPE22") = "在同步资源中总是添加记录"
            End If

            drv.EndEdit()
        Next
        Return ds
    End Function
End Class

End Namespace
