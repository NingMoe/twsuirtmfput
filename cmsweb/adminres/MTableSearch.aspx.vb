Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class MTableSearch
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

        If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.Unknown Then
            ViewState("PAGE_MTSLIST_TYPE") = RInt("mtstype")
        End If
        If VLng("PAGE_HOST_RESID") = 0 Then
            ViewState("PAGE_HOST_RESID") = RLng("mnuhostresid")
        End If
        If VStr("PAGE_EMPID") = "" Then
            ViewState("PAGE_EMPID") = RStr("mnuempid")
            If VStr("PAGE_EMPID") = "" Then
                ViewState("PAGE_EMPID") = CmsPass.Employee.ID
            End If
        End If
        If VStr("PAGE_FROMADMIN") = "" Then
            ViewState("PAGE_FROMADMIN") = RStr("mnufromadmin")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        btnStartSearch.Attributes.Add("onClick", "return OpenMultiTableSearchResultWindow();")
        btnStartSearch.Visible = False
        btnReport.Visible = False
        btnDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前设置吗？');")
        btnCopy.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要复制当前的列表统计吗？');")

        '注册客户端Form隐含变量，用于向服务器传回选中的行记录ID
        Dim strSelRec As String = RStr("RECID")
        If strSelRec = "" Then
            strSelRec = RStr("URLRECID")
        End If
        RegisterHiddenField("RECID", strSelRec)

        '判断是否当前资源所在部门的部门管理员
        Dim blnIsDepAdmin As Boolean = False
            If OrgFactory.DepDriver.GetDepAdmin(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).DepID, True) = CmsPass.Employee.ID Then
                blnIsDepAdmin = True
            End If

        Select Case VInt("PAGE_MTSLIST_TYPE")
            Case MTSearchType.MultiTableView
                lblTitle.Text = "列表统计"
                btnStartSearch.Visible = True
                btnReport.Visible = True

                If CmsPass.EmpIsSysAdmin OrElse blnIsDepAdmin Then
                    btnCopy.Visible = True
                Else
                    btnCopy.Visible = False
                End If

                btnTempApply.Visible = False
                btnStart.Visible = False
                btnStop.Visible = False

                '暂时允许所有用户编辑列表统计
                panelMove.Visible = True
                'If CmsPass.EmpIsSysAdmin OrElse CmsPass.EmpIsAspAdmin OrElse blnIsDepAdmin = True Then
                '    If VLng("PAGE_RESID") <> 0 Then
                '        '只有从选中资源后进入的列表统计才显示上下移动功能，从菜单“系统管理”中“列表统计”进入的不能显示上下移动，因为不同资源间不能上下移动
                '        panelMove.Visible = True
                '    Else
                '        panelMove.Visible = False
                '    End If
                'Else
                '    btnAdd.Visible = False
                '    btnEdit.Visible = False
                '    btnDelete.Visible = False

                '    panelMove.Visible = False
                'End If

            Case MTSearchType.GeneralRowWhere
                lblTitle.Text = "通用行过滤"
                btnCopy.Visible = True
                If VStr("PAGE_FROMADMIN") = "admin" Then   '来自系统管理界面
                    btnTempApply.Visible = False
                End If
                If VStr("PAGE_FROMADMIN") <> "admin" AndAlso blnIsDepAdmin = False Then
                    btnAdd.Visible = False
                    btnEdit.Visible = False
                    btnDelete.Visible = False

                    btnStart.Visible = False
                    btnStop.Visible = False
                    panelMove.Visible = False

                    btnCopy.Visible = False
                End If

            Case MTSearchType.PersonalRowWhere
                lblTitle.Text = "个人行过滤"
                If VStr("PAGE_FROMADMIN") = "admin" Then '来自系统管理界面
                    btnTempApply.Visible = False '管理界面中不能临时应用
                    btnAdd.Visible = False '不方便选择用户，所以不能添加设置
                    btnCopy.Visible = True
                ElseIf VStr("PAGE_FROMADMIN") = "rights" Then '来自权限管理界面
                    btnTempApply.Visible = False '管理界面中不能临时应用
                    btnCopy.Visible = False
                Else '来自个人内容管理界面
                End If
        End Select
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        MultiTableSearch.DelMTSRecordByExpired(CmsPass) '删除超过1天的临时多表统计和行过滤设置

        ''初始化当前多表统计和行过滤设置的类型列表
        'ddlSearchType.Items.Clear()
        'ddlSearchType.Items.Add(New ListItem("自定义多表统计", "1"))
        'ddlSearchType.Items.Add(New ListItem("通用行过滤设置", "3"))
        'ddlSearchType.Items.Add(New ListItem("个人行过滤设置", "4"))

        If RStr("checkrights_fail") = "yes" Then
            PromptMsg("无法访问指定的多表统计和行过滤设置，因为您没有对其中所有资源拥有浏览权限！")
        End If

        GridDataBind() '绑定数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    'Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
    '    If ddlSearchType.SelectedValue = CStr(MTSearchType.MultiTableView) Then
    '        RegisterHiddenField("AllowStartSearch", "1")
    '    Else
    '        RegisterHiddenField("AllowStartSearch", "0")
    '    End If
    '    GridDataBind() '绑定数据
    'End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/adminres/MTableSearch.aspx?mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&URLRECID=" & Me.GetRecIDOfGrid() & "&mnuempid=" & VStr("PAGE_EMPID") & "&mnufromadmin=" & VStr("PAGE_FROMADMIN")
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID"), False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/adminres/MTableSearch.aspx?URLRECID=" & lngRecID & "&mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID") & "&mnufromadmin=" & VStr("PAGE_FROMADMIN")
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & lngRecID & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID"), False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        MultiTableSearch.DelMTSRecordByID(CmsPass, lngRecID)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        MultiTableSearch.CopyMTSRecordByID(CmsPass, lngRecID)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        Session("CMSBP_MTableSearchTableReport") = "/cmsweb/adminres/MTableSearch.aspx?URLRECID=" & lngRecID & "&mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID") & "&mnufromadmin=" & VStr("PAGE_FROMADMIN")
        Response.Redirect("/cmsweb/adminres/MTableSearchTableReport.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & lngRecID, True)
    End Sub

    Private Sub btnTempApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTempApply.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If

        Dim strWhere As String = MultiTableSearchColumn.AssembleWhereForOneTable(CmsPass, lngRecID)
        If VLng("PAGE_HOST_RESID") = 0 OrElse VLng("PAGE_HOST_RESID") = VLng("PAGE_RESID") Then '是主资源
            Session("CMS_HOSTTABLE_WHERE") = strWhere
        Else '是关联子资源
            Session("CMS_SUBTABLE_WHERE") = strWhere
        End If
        Session("CMS_HOSTTABLE_PAGE" & VLng("PAGE_RESID")) = 0
        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "timeid=" & TimeId.CurrentMilliseconds()), False)
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        MultiTableSearch.StartRowFilter(CmsPass, lngRecID, VInt("PAGE_MTSLIST_TYPE"), VStr("PAGE_EMPID"))
        GridDataBind() '绑定数据
        Session("CMS_HOSTTABLE_PAGE" & VLng("PAGE_RESID")) = 0
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        MultiTableSearch.StopRowFilter(CmsPass, lngRecID)
        GridDataBind() '绑定数据
        Session("CMS_HOSTTABLE_PAGE" & VLng("PAGE_RESID")) = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        CmsDbBase.MoveUpByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & VInt("PAGE_MTSLIST_TYPE"), "MTS_SHOWORDER")
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        CmsDbBase.MoveDownByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & VInt("PAGE_MTSLIST_TYPE"), "MTS_SHOWORDER")
        GridDataBind() '绑定数据
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
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "MTS_ID" '关键字段
        col.DataField = "MTS_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "启用状态"
        col.DataField = "MTS_START2"
        col.ItemStyle.Width = Unit.Pixel(70)
        If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableView Then col.Visible = False '多表统计下不用显示
        DataGrid1.Columns.Add(col)
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "查询标题"
        col.DataField = "MTS_TITLE"
        col.ItemStyle.Width = Unit.Pixel(300)
        DataGrid1.Columns.Add(col)
        intWidth += 300

        col = New BoundColumn
        col.HeaderText = "主资源"
        col.DataField = "RESID1_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "类型"
        col.DataField = "MTS_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(120)
        DataGrid1.Columns.Add(col)
        intWidth += 120

        col = New BoundColumn
        col.HeaderText = "人员"
        col.DataField = "MTS_EMPID2"
        col.ItemStyle.Width = Unit.Pixel(100)
        If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableView Then col.Visible = False '多表统计下不用显示
        DataGrid1.Columns.Add(col)
        intWidth += 100

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            'Dim intFrom As Integer = VInt("PAGE_MTSLIST_TYPE")
            'Dim blnShowMTView As Boolean = CBool(IIf(intFrom = MTSearchType.MultiTableView, True, False))
            'Dim blnShowGeneralRowWhere As Boolean = CBool(IIf(intFrom = MTSearchType.GeneralRowWhere, True, False))
            'Dim blnShowPersonalRowWhere As Boolean = CBool(IIf(intFrom = MTSearchType.PersonalRowWhere, True, False))
            Dim ds As DataSet = MultiTableSearch.GetMTSearchByDataSet(CmsPass, VStr("PAGE_FROMADMIN"), CType(VInt("PAGE_MTSLIST_TYPE"), MTSearchType), VLng("PAGE_RESID"), VStr("PAGE_EMPID"))
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub
End Class

End Namespace
