Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSet
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents chk2DAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDocAdd As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDocView As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDocAll As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDocEdit As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnDelQxGainer As System.Web.UI.WebControls.LinkButton
    Protected WithEvents panel2DRights As System.Web.UI.WebControls.Panel
    Protected WithEvents panelDocRights As System.Web.UI.WebControls.Panel
    Protected WithEvents chkMgrEmailSmsSet As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkDocCheckout As System.Web.UI.WebControls.CheckBox
    'Protected WithEvents drpResourceForms As System.Web.UI.WebControls.DropDownList

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnDeleteAllRights.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前资源及其所有子资源上的所有人员的权限设置吗？');")
        btnDeleteSubResRights.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前资源的所有子资源上的所有人员的权限设置吗？');")
        btnDelUserRights.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前用户在当前资源和所有子资源上的权限吗？');")

        btnDeleteAllRights.ToolTip = "删除当前资源及其所有子资源上的所有人员的权限设置"
        btnDeleteSubResRights.ToolTip = "删除当前资源下属的所有子资源上(不含当前资源)的所有人员的权限设置"
        btnDelUserRights.ToolTip = "删除当前选中用户在当前资源及其所有子资源上的所有权限设置"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("depempcmd") <> "selected_depemp" And RStr("mnufrom") <> "colrights" And RStr("mnufrom") <> "rowrights" And RStr("mnufrom") <> "projrights" Then 'GET请求，是从“增加部门人员”页面回来，添加相应部门或人员的权限至权限表中
            '如果时机密资源，则必须同时有系统安全员帐号才可进入
            If Session("CMS_QXSECURITY_VERIFIED") Is Nothing Then
                If ResFactory.ResService.GetResSecurityLevel(CmsPass, VLng("PAGE_RESID")) >= ResSecurityLevel.Secret Then
                    Session("CMSBP_RightsSetSecurity1") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") ' & "&backpage=" & VStr("PAGE_BACKPAGE")
                    Session("CMSBP_RightsSetSecurity2") = VStr("PAGE_BACKPAGE")
                    Response.Redirect("/cmsweb/cmsrights/RightsSetSecurity.aspx", False)
                    Return
                End If
            End If
            Session("CMS_QXSECURITY_VERIFIED") = Nothing '“系统安全员检验标志”必须立刻清空
        End If

        '下行必须放在IsPostBack判断之后
        If RStr("depempcmd") = "selected_depemp" Then 'GET请求，是从“增加部门人员”页面回来，添加相应部门或人员的权限至权限表中
            '先恢复当前部门ID，因为在“部门人员选择页面”中当前部门ID已经被修改
            Dim strDepID As String = RStr("depid")
            Dim strEmpAIID As String = RStr("empaiid")
            If strEmpAIID <> "" Then '是人员
                Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, CLng(strEmpAIID))
                CmsRights.AddRightsGainer(CmsPass, VLng("PAGE_RESID"), strEmpID, RightsGainerType.IsEmployee, CmsRightsDefine.RecView)
                ViewState("QXSET_GAINERID") = strEmpID
            ElseIf strDepID <> "" Then '是部门或企业
                CmsRights.AddRightsGainer(CmsPass, VLng("PAGE_RESID"), strDepID, RightsGainerType.IsDepartment, CmsRightsDefine.RecView)
                ViewState("QXSET_GAINERID") = strDepID
            End If
        ElseIf RStr("mnufrom") = "colrights" Or RStr("mnufrom") = "rowrights" Or RStr("mnufrom") = "projrights" Then
            ViewState("QXSET_GAINERID") = RStr("gainerid")
        End If

        '显示当前选中用户的权限信息
        Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), VStr("QXSET_GAINERID"))
        Dim lngRightsValue As Long = RightsValue.lngQX_VALUE
        ShowUserRights(lngRightsValue)
        'Me.drpResourceForms.SelectedValue = RightsValue.PermissionForm

        GridDataBind() '绑定权限信息至DataGrid中

        '显示资源名称
        lblResDispName.Text = "权限设置－" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        AdjustFunctionsSwitch() '调整功能开关决定的功能支持与否

        '显示分类下的窗体设计
        'drpResourceForms.Items.Clear()
        'drpResourceForms.Items.Add(New ListItem("不使用设置", ""))
        'Dim alistForms As ArrayList = CTableForm.GetDesignedFormNames(CmsPass, VLng("PAGE_RESID"), CType(0, FormType))
        'For i As Integer = 0 To alistForms.Count - 1
        '    drpResourceForms.Items.Add(CStr(alistForms(i)))
        'Next
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        If RStr("qxaction") = "qxrowclicked" Then 'POST请求，用户点击了权限获得者的某条记录
            GridDataBind() '绑定权限信息至DataGrid中

            Dim strRightsGainerID As String = GetGainerID()
            Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID)
            'Dim lngRightsValue As Long = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID).lngQX_VALUE
            Dim lngRightsValue As Long = RightsValue.lngQX_VALUE
            ShowUserRights(lngRightsValue)
            'Me.drpResourceForms.SelectedValue = RightsValue.PermissionForm
        End If

        AdjustFunctionsSwitch() '调整功能开关决定的功能支持与否
    End Sub

    Private Sub lbtnColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnColumn.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置列字段权限！")
            Return
        End If

        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTable <> "" Then
            Session("CMSBP_RightsSetColumn") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=colrights&gainerid=" & strRightsGainerIDs '& "&backpage=" & VStr("PAGE_BACKPAGE")
            Response.Redirect("/cmsweb/cmsrights/RightsSetColumn.aspx?mnuresid=" & VLng("PAGE_RESID") & "&gainerid=" & strRightsGainerIDs, False)
        Else
            PromptMsg("此资源没有二维信息表单，不能进行列字段权限设置！")
            Return
        End If
    End Sub

    Private Sub lbtnRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRow.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置行记录权限！")
            Return
        End If

        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTable <> "" Then
            Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=rowrights&gainerid=" & strRightsGainerIDs
            Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.RowRights & "&mnuempid=" & strRightsGainerIDs, False)
        Else
            PromptMsg("此资源没有二维信息表单，不能进行行记录权限设置！")
            Return
        End If
    End Sub

    Private Sub lbtnRowFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRowFilter.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置行过滤条件！")
            Return
        End If

        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTable <> "" Then
            Session("CMSBP_MTableSearch") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=rowrights&gainerid=" & strRightsGainerIDs
            Response.Redirect("/cmsweb/adminres/MTableSearch.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.PersonalRowWhere & "&mnuempid=" & strRightsGainerIDs & "&mnufromadmin=rights", False)
        Else
            PromptMsg("此资源没有二维信息表单，不能进行行记录权限设置！")
            Return
        End If
    End Sub

    Private Sub lbtnMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMenu.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置行记录权限！")
            Return
        End If

        Session("CMSBP_RightsSetProject") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=projrights&gainerid=" & strRightsGainerIDs '& "&backpage=" & VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsrights/RightsSetProject.aspx?mnuresid=" & VLng("PAGE_RESID") & "&gainerid=" & strRightsGainerIDs, False)
    End Sub

    '--------------------------------------------------------------------------
    '绑定权限信息至DataGrid中
    '--------------------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = CmsRights.GetRightsByDataSet(CmsPass, VLng("PAGE_RESID"))
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("获取自定义表字段显示信息并显示时出错！", ex)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

            If RStr("qxaction") = "qxrowclicked" Then 'POST请求，用户点击了权限获得者的某条记录
                ViewState("QXSET_GAINERID") = RStr("RECID")
            Else
                '目前只有选中多条记录后提交才会到达这里
                If RStr("RECID") <> "" Then
                    If RStr("RECID").IndexOf(VStr("QXSET_GAINERID")) >= 0 Then
                        ViewState("QXSET_GAINERID") = RStr("RECID")
                    Else
                        ViewState("QXSET_GAINERID") = VStr("QXSET_GAINERID") & "," & RStr("RECID")
                    End If
                End If
            End If

            WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
            CreateDataGridColumn()
        Catch ex As Exception
            SLog.Err("权限信息在DataGrid1_Init()中显示时异常出错！", ex)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnDeleteSubResRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSubResRights.Click
        Try
            CmsRights.DelRightsOfSubRes(CmsPass, VLng("PAGE_RESID"), False)

            GridDataBind() '绑定权限信息至DataGrid中
        Catch ex As Exception
            PromptMsg("删除权限异常失败！", ex, True)
        End Try
    End Sub

    Private Sub btnDeleteAllRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAllRights.Click
        Try
            CmsRights.DelRightsOfSubRes(CmsPass, VLng("PAGE_RESID"), True)

            GridDataBind() '绑定权限信息至DataGrid中
        Catch ex As Exception
            PromptMsg("删除权限异常失败！", ex, True)
        End Try
    End Sub

    Private Sub btnDelUserRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelUserRights.Click
        Try
            Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
            strRightsGainerIDs = StringDeal.Trim(strRightsGainerIDs, ",", ",")
            If strRightsGainerIDs = "" Then
                PromptMsg("请先选择权限获得者！")
                Return
            End If

            If strRightsGainerIDs.IndexOf(",") > 0 Then
                strRightsGainerIDs = strRightsGainerIDs.Replace(",", "','")
                strRightsGainerIDs = "'" & strRightsGainerIDs & "'"
            End If

            CmsRights.DelRightsOfSubResOnUsers(CmsPass, VLng("PAGE_RESID"), strRightsGainerIDs, True)

            GridDataBind() '绑定权限信息至DataGrid中
        Catch ex As Exception
            PromptMsg("删除权限异常失败！", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Try
            If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                Dim row As DataGridItem
                For Each row In DataGrid1.Items
                    '---------------------------------------------------------------
                    '设置DataGrid每行的唯一ID和OnClick方法，用于传回服务器信息做相应操作，如修改、删除等
                    Dim strRecID As String = Trim(row.Cells(1).Text)
                    row.Attributes.Add("RECID", strRecID)
                    row.Attributes.Add("OnClick", "RowLeftClickPost(this)")
                    '---------------------------------------------------------------

                    '---------------------------------------------------------------
                    '始终选中用户点击过的记录
                    If strRecID <> "" And GetGainerID() = strRecID Then
                        row.Attributes.Add("bgColor", "#C4D9F9")
                    End If
                    '---------------------------------------------------------------
                Next
            End If
        Catch ex As Exception
            SLog.Err("权限信息在DataGrid1_ItemCreated()中显示时异常出错！", ex)
        End Try
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            Try
                Dim strRightsGainerID As String = e.Item.Cells(1).Text
                CmsRights.DelRightsGainer(CmsPass, VLng("PAGE_RESID"), strRightsGainerID)
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try

            ViewState("QXSET_GAINERID") = Nothing '清空当前选中的权限获得者
            ShowUserRights(0) '清空所有权限选项

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            DataGrid1.EditItemIndex = -1
            GridDataBind() '绑定权限信息至DataGrid中
        Catch ex As Exception
            SLog.Err("权限信息在DataGrid1_DeleteCommand()中显示时异常出错！", ex)
        End Try
    End Sub

    Private Sub btnAddRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRights.Click
        Dim lngDefDepID As Long = ResFactory.ResService.GetResDepartment(CmsPass, VLng("PAGE_RESID"))
        Session("CMSBP_DepEmpList") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") '& "&backpage=" & VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/adminsys/DepEmpList.aspx?depid=" & lngDefDepID, False)
    End Sub

    Private Sub btnSaveRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRights.Click
        Dim lngRightsValue As Long = GetUserRightsFromUI()
        SaveRights(lngRightsValue)

        GridDataBind() '绑定权限信息至DataGrid中
    End Sub


    '------------------------------------------------------------------
    '保存权限
    '------------------------------------------------------------------
    Private Sub SaveRights(ByVal lngDefaultRightsValue As Long)
        '获取当前选中的权限获得者ID
        If VStr("QXSET_GAINERID") = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        End If

        Dim alistGainerIDs As ArrayList = StringDeal.Split(VStr("QXSET_GAINERID"), ",")
        Dim strOneGainerID As String
        For Each strOneGainerID In alistGainerIDs
            '保存权限信息，暂时只支持表单操作权限
            CmsRights.EditRights(CmsPass, VLng("PAGE_RESID"), strOneGainerID, RightsName.Resource, lngDefaultRightsValue)
            'CmsRights.UpdateResourcePermissionForm(VLng("PAGE_RESID"), strOneGainerID, RightsName.Resource, Me.drpResourceForms.SelectedValue)
        Next

        If VStr("QXSET_GAINERID").IndexOf(",") >= 0 Then
            '是多选情况，则必须清除选中的权限获得者和权限信息
            ShowUserRights(0) '清空对权限获得者的选中
            ViewState("QXSET_GAINERID") = Nothing '清空当前选中的权限获得者
        Else
            '是单选情况，显示正确的权限信息
            Dim strRightsGainerID As String = VStr("QXSET_GAINERID")
            Dim lngRightsValue As Long = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID).lngQX_VALUE
            ShowUserRights(lngRightsValue)
        End If

        GridDataBind() '绑定权限信息至DataGrid中
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "权限获得者"
        col.DataField = "QX_GAINER_NAME"
        'col.ItemStyle.ForeColor = Color.Blue
        col.ItemStyle.Width = Unit.Pixel(140)
        intWidth += 130
        DataGrid1.Columns.Add(col)

        '资源获得者ID必须是第2列，修改、删除资源时用到
        col = New BoundColumn
        col.HeaderText = "ID"
        col.DataField = "QX_GAINER_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "登录帐号"
        col.DataField = "QX_GAINER_ID2"
        col.ItemStyle.Width = Unit.Pixel(100)
        intWidth += 100
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "所属部门"
        col.DataField = "QX_GAINER_DEP"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "类型"
        col.DataField = "QX_INHERIT_NAME"
        col.ItemStyle.Width = Unit.Pixel(35)
        intWidth += 35
        DataGrid1.Columns.Add(col)

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '或者用图标，如："<img src='/cmsweb/images/common/delete.gif' border=0>"
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        colDel.ItemStyle.Width = Unit.Pixel(35)
        intWidth += 35
        DataGrid1.Columns.Add(colDel)

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '----------------------------------------------------------------
    '获取界面上的权限设置信息
    '----------------------------------------------------------------
    Private Function GetUserRightsFromUI() As Long
        'AdjustFunctionsSwitch() '调整功能开关决定的功能支持与否

        Dim lngRightsValue As Long = 0
        If chkRecView.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecView
        If chkRecAdd.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecAdd
        If chkRecEdit.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecEdit
        If chkRecDel.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecDel
        If chkRecPrint.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecPrint
        If chkRecPrintList.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecPrintList

        If chkDocCheckin.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocCheckIn
        If chkDocCheckoutCancel.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocCheckoutCancel
        'If chkDocCheckout.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocCheckOut
        If chkDocGet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocGet
        If chkDocViewOnline.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocViewOnline
        If chkDocPrint.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocPrint
        If chkDocViewHistory.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocViewHistory
        If chkDocShare.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocShare
        'If chkDocMove.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocMove

        If chkMgrRightsSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrRightsSet
        If chkMgrColumnSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrColumnSet
        If chkMgrColumnShowSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrColumnShowSet
        If chkMgrInputFormDesign.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrInputFormDesign
        If chkMgrPrintFormDesign.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrPrintFormDesign
        If chkMgrRelatedTable.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrRelatedTable
        If chkMgrRowColor.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrRowColor
        If chkFormula.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrFormula
        'If chkMgrEmailSmsSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrEmailSmsSet

        If chkResExport.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResExport
        If chkResImport.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResImport
        If chkResEmailSmsNotify.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecSendEmailSms
        'If chkResEmailSmsBatchSend.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.ResEmailSmsBatchSend
        If chkResBatchUpdateField.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecBatchUpdateField
        If chkRecBatchUpdateRecords.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecBatchUpdateRecords

        If chkResAdd.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResAdd
        If chkResEdit.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResEdit
        If chkResDel.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResDel

        If chkRecSearchMultitableList.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecSearchMultitableList
        Return lngRightsValue
    End Function

    '----------------------------------------------------------------
    '在界面上显示权限信息
    '----------------------------------------------------------------
    Private Sub ShowUserRights(ByVal lngRightsValue As Long)
        If (lngRightsValue And CmsRightsDefine.RecView) = CmsRightsDefine.RecView Then
            chkRecView.Checked = True
        Else
            chkRecView.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecAdd) = CmsRightsDefine.RecAdd Then
            chkRecAdd.Checked = True
        Else
            chkRecAdd.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecEdit) = CmsRightsDefine.RecEdit Then
            chkRecEdit.Checked = True
        Else
            chkRecEdit.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecDel) = CmsRightsDefine.RecDel Then
            chkRecDel.Checked = True
        Else
            chkRecDel.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecPrint) = CmsRightsDefine.RecPrint Then
            chkRecPrint.Checked = True
        Else
            chkRecPrint.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecPrintList) = CmsRightsDefine.RecPrintList Then
            chkRecPrintList.Checked = True
        Else
            chkRecPrintList.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.DocCheckIn) = CmsRightsDefine.DocCheckIn Then
            chkDocCheckin.Checked = True
        Else
            chkDocCheckin.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.DocCheckoutCancel) = CmsRightsDefine.DocCheckoutCancel Then
            chkDocCheckoutCancel.Checked = True
        Else
            chkDocCheckoutCancel.Checked = False
        End If

        'If (lngRightsValue And CmsRightsDefine.DocCheckOut) = CmsRightsDefine.DocCheckOut Then
        '    chkDocCheckout.Checked = True
        'Else
        '    chkDocCheckout.Checked = False
        'End If
        If (lngRightsValue And CmsRightsDefine.DocGet) = CmsRightsDefine.DocGet Then
            chkDocGet.Checked = True
        Else
            chkDocGet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocViewOnline) = CmsRightsDefine.DocViewOnline Then
            chkDocViewOnline.Checked = True
        Else
            chkDocViewOnline.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocPrint) = CmsRightsDefine.DocPrint Then
            chkDocPrint.Checked = True
        Else
            chkDocPrint.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocViewHistory) = CmsRightsDefine.DocViewHistory Then
            chkDocViewHistory.Checked = True
        Else
            chkDocViewHistory.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocShare) = CmsRightsDefine.DocShare Then
            chkDocShare.Checked = True
        Else
            chkDocShare.Checked = False
        End If
        'If (lngRightsValue And CmsRightsDefine.DocMove) = CmsRightsDefine.DocMove Then
        '    chkDocMove.Checked = True
        'Else
        '    chkDocMove.Checked = False
        'End If

        If (lngRightsValue And CmsRightsDefine.MgrRightsSet) = CmsRightsDefine.MgrRightsSet Then
            chkMgrRightsSet.Checked = True
        Else
            chkMgrRightsSet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrColumnSet) = CmsRightsDefine.MgrColumnSet Then
            chkMgrColumnSet.Checked = True
        Else
            chkMgrColumnSet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrColumnShowSet) = CmsRightsDefine.MgrColumnShowSet Then
            chkMgrColumnShowSet.Checked = True
        Else
            chkMgrColumnShowSet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrInputFormDesign) = CmsRightsDefine.MgrInputFormDesign Then
            chkMgrInputFormDesign.Checked = True
        Else
            chkMgrInputFormDesign.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrPrintFormDesign) = CmsRightsDefine.MgrPrintFormDesign Then
            chkMgrPrintFormDesign.Checked = True
        Else
            chkMgrPrintFormDesign.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrRelatedTable) = CmsRightsDefine.MgrRelatedTable Then
            chkMgrRelatedTable.Checked = True
        Else
            chkMgrRelatedTable.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrRowColor) = CmsRightsDefine.MgrRowColor Then
            chkMgrRowColor.Checked = True
        Else
            chkMgrRowColor.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrFormula) = CmsRightsDefine.MgrFormula Then
            chkFormula.Checked = True
        Else
            chkFormula.Checked = False
        End If
        'If (lngRightsValue And CmsRightsDefine.MgrEmailSmsSet) = CmsRightsDefine.MgrEmailSmsSet Then
        '    chkMgrEmailSmsSet.Checked = True
        'Else
        '    chkMgrEmailSmsSet.Checked = False
        'End If
        If (lngRightsValue And CmsRightsDefine.MgrResExport) = CmsRightsDefine.MgrResExport Then
            chkResExport.Checked = True
        Else
            chkResExport.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrResImport) = CmsRightsDefine.MgrResImport Then
            chkResImport.Checked = True
        Else
            chkResImport.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecSendEmailSms) = CmsRightsDefine.RecSendEmailSms Then
            chkResEmailSmsNotify.Checked = True
        Else
            chkResEmailSmsNotify.Checked = False
        End If
        'If (lngRightsValue And CmsRightsDefine.ResEmailSmsBatchSend) = CmsRightsDefine.ResEmailSmsBatchSend Then
        '    chkResEmailSmsBatchSend.Checked = True
        'Else
        '    chkResEmailSmsBatchSend.Checked = False
        'End If
        If (lngRightsValue And CmsRightsDefine.RecBatchUpdateField) = CmsRightsDefine.RecBatchUpdateField Then
            chkResBatchUpdateField.Checked = True
        Else
            chkResBatchUpdateField.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecBatchUpdateRecords) = CmsRightsDefine.RecBatchUpdateRecords Then
            chkRecBatchUpdateRecords.Checked = True
        Else
            chkRecBatchUpdateRecords.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.MgrResAdd) = CmsRightsDefine.MgrResAdd Then
            chkResAdd.Checked = True
        Else
            chkResAdd.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrResEdit) = CmsRightsDefine.MgrResEdit Then
            chkResEdit.Checked = True
        Else
            chkResEdit.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrResDel) = CmsRightsDefine.MgrResDel Then
            chkResDel.Checked = True
        Else
            chkResDel.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.RecSearchMultitableList) = CmsRightsDefine.RecSearchMultitableList Then
            chkRecSearchMultitableList.Checked = True
        Else
            chkRecSearchMultitableList.Checked = False
        End If



        AdjustFunctionsSwitch() '调整功能开关决定的功能支持与否
    End Sub

    '----------------------------------------------------------------------
    '调整功能开关决定的功能支持与否
    '----------------------------------------------------------------------
    Private Sub AdjustFunctionsSwitch()
        '判断当前资源有无项目权限定义
        lbtnMenu.Enabled = CmsMenu.HasProjRightsOfResource(CmsPass, VLng("PAGE_RESID"))

        If CmsFunc.IsEnable("FUNC_DOCHISTORY") = False Then
            chkDocViewHistory.Enabled = False
            chkDocViewHistory.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_RELATION_TABLE") = False Then
            chkMgrRelatedTable.Enabled = False
            chkMgrRelatedTable.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_COLUMN_SET") = False Then
            chkMgrColumnSet.Enabled = False
            chkMgrColumnSet.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_COLUMNSHOW_SET") = False Then
            chkMgrColumnShowSet.Enabled = False
            chkMgrColumnShowSet.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_RES_ROWCOLOR") = False Then
            chkMgrRowColor.Enabled = False
            chkMgrRowColor.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_FORMULA") = False Then
            chkFormula.Enabled = False
            chkFormula.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_COMM_EMAILPHONE") = False Then
            'chkMgrEmailSmsSet.Enabled = False
            'chkMgrEmailSmsSet.Checked = False
            chkResEmailSmsNotify.Enabled = False
            chkResEmailSmsNotify.Checked = False
        End If

        'If CmsFunc.IsEnable("FUNC_BATCHSEND") = False Then
        '    chkResEmailSmsBatchSend.Enabled = False
        '    chkResEmailSmsBatchSend.Checked = False
        'End If

        If CmsFunc.IsEnable("FUNC_RES_IMP_OTHERTABLE") = False Then
            chkResImport.Enabled = False
            chkResImport.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") = False Then
            chkResExport.Enabled = False
            chkResExport.Checked = False
        End If

        '在线浏览文档
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_ONLINEVIEW")
        If bln = False Then
            chkDocViewOnline.Enabled = False
            chkDocViewOnline.Checked = False
        End If

        '在线打印文档
        bln = CmsFunc.IsEnable("FUNC_ONLINEPRINT")
        If bln = False Then
            chkDocPrint.Enabled = False
            chkDocPrint.Checked = False
        End If

        '行过滤条件
        bln = CmsFunc.IsEnable("FUNC_ROWWHERE")
        If bln = False Then lbtnRowFilter.Visible = False

        '权限管理中支持增加/修改/删除资源
        bln = CmsFunc.IsEnable("FUNC_RESEDIT_RIGHTS")
        If bln = False Then
            chkResAdd.Enabled = False
            chkResAdd.Checked = False
            chkResEdit.Enabled = False
            chkResEdit.Checked = False
            chkResDel.Enabled = False
            chkResDel.Checked = False
        End If

        '是否支持文档表
        bln = CmsFunc.IsEnable("FUNC_TABLETYPE_DOC")
        If bln = False Then
            chkDocCheckin.Enabled = False
            chkDocCheckoutCancel.Enabled = False
            chkDocGet.Enabled = False
            chkDocShare.Enabled = False
            chkDocViewHistory.Enabled = False
            chkDocViewOnline.Enabled = False
            chkDocPrint.Enabled = False

            chkDocCheckin.Checked = False
            chkDocCheckoutCancel.Checked = False
            chkDocGet.Checked = False
            chkDocShare.Checked = False
            chkDocViewHistory.Checked = False
            chkDocViewOnline.Checked = False
            chkDocPrint.Checked = False
        End If
    End Sub

    Private Function GetGainerID() As String
        If RStr("RECID") <> "" Then
            ViewState("QXSET_GAINERID") = RStr("RECID")
        End If

        Return VStr("QXSET_GAINERID")
    End Function
End Class
End Namespace
