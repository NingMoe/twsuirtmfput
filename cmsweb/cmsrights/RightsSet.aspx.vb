Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSet
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
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

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnDeleteAllRights.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����ǰ��Դ������������Դ�ϵ�������Ա��Ȩ��������');")
        btnDeleteSubResRights.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����ǰ��Դ����������Դ�ϵ�������Ա��Ȩ��������');")
        btnDelUserRights.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����ǰ�û��ڵ�ǰ��Դ����������Դ�ϵ�Ȩ����');")

        btnDeleteAllRights.ToolTip = "ɾ����ǰ��Դ������������Դ�ϵ�������Ա��Ȩ������"
        btnDeleteSubResRights.ToolTip = "ɾ����ǰ��Դ��������������Դ��(������ǰ��Դ)��������Ա��Ȩ������"
        btnDelUserRights.ToolTip = "ɾ����ǰѡ���û��ڵ�ǰ��Դ������������Դ�ϵ�����Ȩ������"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("depempcmd") <> "selected_depemp" And RStr("mnufrom") <> "colrights" And RStr("mnufrom") <> "rowrights" And RStr("mnufrom") <> "projrights" Then 'GET�����Ǵӡ����Ӳ�����Ա��ҳ������������Ӧ���Ż���Ա��Ȩ����Ȩ�ޱ���
            '���ʱ������Դ�������ͬʱ��ϵͳ��ȫԱ�ʺŲſɽ���
            If Session("CMS_QXSECURITY_VERIFIED") Is Nothing Then
                If ResFactory.ResService.GetResSecurityLevel(CmsPass, VLng("PAGE_RESID")) >= ResSecurityLevel.Secret Then
                    Session("CMSBP_RightsSetSecurity1") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") ' & "&backpage=" & VStr("PAGE_BACKPAGE")
                    Session("CMSBP_RightsSetSecurity2") = VStr("PAGE_BACKPAGE")
                    Response.Redirect("/cmsweb/cmsrights/RightsSetSecurity.aspx", False)
                    Return
                End If
            End If
            Session("CMS_QXSECURITY_VERIFIED") = Nothing '��ϵͳ��ȫԱ�����־�������������
        End If

        '���б������IsPostBack�ж�֮��
        If RStr("depempcmd") = "selected_depemp" Then 'GET�����Ǵӡ����Ӳ�����Ա��ҳ������������Ӧ���Ż���Ա��Ȩ����Ȩ�ޱ���
            '�Ȼָ���ǰ����ID����Ϊ�ڡ�������Աѡ��ҳ�桱�е�ǰ����ID�Ѿ����޸�
            Dim strDepID As String = RStr("depid")
            Dim strEmpAIID As String = RStr("empaiid")
            If strEmpAIID <> "" Then '����Ա
                Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, CLng(strEmpAIID))
                CmsRights.AddRightsGainer(CmsPass, VLng("PAGE_RESID"), strEmpID, RightsGainerType.IsEmployee, CmsRightsDefine.RecView)
                ViewState("QXSET_GAINERID") = strEmpID
            ElseIf strDepID <> "" Then '�ǲ��Ż���ҵ
                CmsRights.AddRightsGainer(CmsPass, VLng("PAGE_RESID"), strDepID, RightsGainerType.IsDepartment, CmsRightsDefine.RecView)
                ViewState("QXSET_GAINERID") = strDepID
            End If
        ElseIf RStr("mnufrom") = "colrights" Or RStr("mnufrom") = "rowrights" Or RStr("mnufrom") = "projrights" Then
            ViewState("QXSET_GAINERID") = RStr("gainerid")
        End If

        '��ʾ��ǰѡ���û���Ȩ����Ϣ
        Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), VStr("QXSET_GAINERID"))
        Dim lngRightsValue As Long = RightsValue.lngQX_VALUE
        ShowUserRights(lngRightsValue)
        'Me.drpResourceForms.SelectedValue = RightsValue.PermissionForm

        GridDataBind() '��Ȩ����Ϣ��DataGrid��

        '��ʾ��Դ����
        lblResDispName.Text = "Ȩ�����ã�" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        AdjustFunctionsSwitch() '�������ܿ��ؾ����Ĺ���֧�����

        '��ʾ�����µĴ������
        'drpResourceForms.Items.Clear()
        'drpResourceForms.Items.Add(New ListItem("��ʹ������", ""))
        'Dim alistForms As ArrayList = CTableForm.GetDesignedFormNames(CmsPass, VLng("PAGE_RESID"), CType(0, FormType))
        'For i As Integer = 0 To alistForms.Count - 1
        '    drpResourceForms.Items.Add(CStr(alistForms(i)))
        'Next
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        If RStr("qxaction") = "qxrowclicked" Then 'POST�����û������Ȩ�޻���ߵ�ĳ����¼
            GridDataBind() '��Ȩ����Ϣ��DataGrid��

            Dim strRightsGainerID As String = GetGainerID()
            Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID)
            'Dim lngRightsValue As Long = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID).lngQX_VALUE
            Dim lngRightsValue As Long = RightsValue.lngQX_VALUE
            ShowUserRights(lngRightsValue)
            'Me.drpResourceForms.SelectedValue = RightsValue.PermissionForm
        End If

        AdjustFunctionsSwitch() '�������ܿ��ؾ����Ĺ���֧�����
    End Sub

    Private Sub lbtnColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnColumn.Click
        '��ȡ��ǰѡ�е�Ȩ�޻����ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("����ѡ��Ȩ�޻���ߣ�")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("����ͬʱ�Զ����Ա�������ֶ�Ȩ�ޣ�")
            Return
        End If

        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTable <> "" Then
            Session("CMSBP_RightsSetColumn") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=colrights&gainerid=" & strRightsGainerIDs '& "&backpage=" & VStr("PAGE_BACKPAGE")
            Response.Redirect("/cmsweb/cmsrights/RightsSetColumn.aspx?mnuresid=" & VLng("PAGE_RESID") & "&gainerid=" & strRightsGainerIDs, False)
        Else
            PromptMsg("����Դû�ж�ά��Ϣ�������ܽ������ֶ�Ȩ�����ã�")
            Return
        End If
    End Sub

    Private Sub lbtnRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRow.Click
        '��ȡ��ǰѡ�е�Ȩ�޻����ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("����ѡ��Ȩ�޻���ߣ�")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("����ͬʱ�Զ����Ա�����м�¼Ȩ�ޣ�")
            Return
        End If

        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTable <> "" Then
            Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=rowrights&gainerid=" & strRightsGainerIDs
            Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.RowRights & "&mnuempid=" & strRightsGainerIDs, False)
        Else
            PromptMsg("����Դû�ж�ά��Ϣ�������ܽ����м�¼Ȩ�����ã�")
            Return
        End If
    End Sub

    Private Sub lbtnRowFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRowFilter.Click
        '��ȡ��ǰѡ�е�Ȩ�޻����ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("����ѡ��Ȩ�޻���ߣ�")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("����ͬʱ�Զ����Ա�����й���������")
            Return
        End If

        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTable <> "" Then
            Session("CMSBP_MTableSearch") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=rowrights&gainerid=" & strRightsGainerIDs
            Response.Redirect("/cmsweb/adminres/MTableSearch.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.PersonalRowWhere & "&mnuempid=" & strRightsGainerIDs & "&mnufromadmin=rights", False)
        Else
            PromptMsg("����Դû�ж�ά��Ϣ�������ܽ����м�¼Ȩ�����ã�")
            Return
        End If
    End Sub

    Private Sub lbtnMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMenu.Click
        '��ȡ��ǰѡ�е�Ȩ�޻����ID
        Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
        If strRightsGainerIDs = "" Then
            PromptMsg("����ѡ��Ȩ�޻���ߣ�")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("����ͬʱ�Զ����Ա�����м�¼Ȩ�ޣ�")
            Return
        End If

        Session("CMSBP_RightsSetProject") = "/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnufrom=projrights&gainerid=" & strRightsGainerIDs '& "&backpage=" & VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsrights/RightsSetProject.aspx?mnuresid=" & VLng("PAGE_RESID") & "&gainerid=" & strRightsGainerIDs, False)
    End Sub

    '--------------------------------------------------------------------------
    '��Ȩ����Ϣ��DataGrid��
    '--------------------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = CmsRights.GetRightsByDataSet(CmsPass, VLng("PAGE_RESID"))
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("��ȡ�Զ�����ֶ���ʾ��Ϣ����ʾʱ����", ex)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            If RStr("qxaction") = "qxrowclicked" Then 'POST�����û������Ȩ�޻���ߵ�ĳ����¼
                ViewState("QXSET_GAINERID") = RStr("RECID")
            Else
                'Ŀǰֻ��ѡ�ж�����¼���ύ�Żᵽ������
                If RStr("RECID") <> "" Then
                    If RStr("RECID").IndexOf(VStr("QXSET_GAINERID")) >= 0 Then
                        ViewState("QXSET_GAINERID") = RStr("RECID")
                    Else
                        ViewState("QXSET_GAINERID") = VStr("QXSET_GAINERID") & "," & RStr("RECID")
                    End If
                End If
            End If

            WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
            CreateDataGridColumn()
        Catch ex As Exception
            SLog.Err("Ȩ����Ϣ��DataGrid1_Init()����ʾʱ�쳣����", ex)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnDeleteSubResRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSubResRights.Click
        Try
            CmsRights.DelRightsOfSubRes(CmsPass, VLng("PAGE_RESID"), False)

            GridDataBind() '��Ȩ����Ϣ��DataGrid��
        Catch ex As Exception
            PromptMsg("ɾ��Ȩ���쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnDeleteAllRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAllRights.Click
        Try
            CmsRights.DelRightsOfSubRes(CmsPass, VLng("PAGE_RESID"), True)

            GridDataBind() '��Ȩ����Ϣ��DataGrid��
        Catch ex As Exception
            PromptMsg("ɾ��Ȩ���쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnDelUserRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelUserRights.Click
        Try
            Dim strRightsGainerIDs As String = VStr("QXSET_GAINERID")
            strRightsGainerIDs = StringDeal.Trim(strRightsGainerIDs, ",", ",")
            If strRightsGainerIDs = "" Then
                PromptMsg("����ѡ��Ȩ�޻���ߣ�")
                Return
            End If

            If strRightsGainerIDs.IndexOf(",") > 0 Then
                strRightsGainerIDs = strRightsGainerIDs.Replace(",", "','")
                strRightsGainerIDs = "'" & strRightsGainerIDs & "'"
            End If

            CmsRights.DelRightsOfSubResOnUsers(CmsPass, VLng("PAGE_RESID"), strRightsGainerIDs, True)

            GridDataBind() '��Ȩ����Ϣ��DataGrid��
        Catch ex As Exception
            PromptMsg("ɾ��Ȩ���쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        Try
            If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                Dim row As DataGridItem
                For Each row In DataGrid1.Items
                    '---------------------------------------------------------------
                    '����DataGridÿ�е�ΨһID��OnClick���������ڴ��ط�������Ϣ����Ӧ���������޸ġ�ɾ����
                    Dim strRecID As String = Trim(row.Cells(1).Text)
                    row.Attributes.Add("RECID", strRecID)
                    row.Attributes.Add("OnClick", "RowLeftClickPost(this)")
                    '---------------------------------------------------------------

                    '---------------------------------------------------------------
                    'ʼ��ѡ���û�������ļ�¼
                    If strRecID <> "" And GetGainerID() = strRecID Then
                        row.Attributes.Add("bgColor", "#C4D9F9")
                    End If
                    '---------------------------------------------------------------
                Next
            End If
        Catch ex As Exception
            SLog.Err("Ȩ����Ϣ��DataGrid1_ItemCreated()����ʾʱ�쳣����", ex)
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

            ViewState("QXSET_GAINERID") = Nothing '��յ�ǰѡ�е�Ȩ�޻����
            ShowUserRights(0) '�������Ȩ��ѡ��

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            DataGrid1.EditItemIndex = -1
            GridDataBind() '��Ȩ����Ϣ��DataGrid��
        Catch ex As Exception
            SLog.Err("Ȩ����Ϣ��DataGrid1_DeleteCommand()����ʾʱ�쳣����", ex)
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

        GridDataBind() '��Ȩ����Ϣ��DataGrid��
    End Sub


    '------------------------------------------------------------------
    '����Ȩ��
    '------------------------------------------------------------------
    Private Sub SaveRights(ByVal lngDefaultRightsValue As Long)
        '��ȡ��ǰѡ�е�Ȩ�޻����ID
        If VStr("QXSET_GAINERID") = "" Then
            PromptMsg("����ѡ��Ȩ�޻���ߣ�")
            Return
        End If

        Dim alistGainerIDs As ArrayList = StringDeal.Split(VStr("QXSET_GAINERID"), ",")
        Dim strOneGainerID As String
        For Each strOneGainerID In alistGainerIDs
            '����Ȩ����Ϣ����ʱֻ֧�ֱ�����Ȩ��
            CmsRights.EditRights(CmsPass, VLng("PAGE_RESID"), strOneGainerID, RightsName.Resource, lngDefaultRightsValue)
            'CmsRights.UpdateResourcePermissionForm(VLng("PAGE_RESID"), strOneGainerID, RightsName.Resource, Me.drpResourceForms.SelectedValue)
        Next

        If VStr("QXSET_GAINERID").IndexOf(",") >= 0 Then
            '�Ƕ�ѡ�������������ѡ�е�Ȩ�޻���ߺ�Ȩ����Ϣ
            ShowUserRights(0) '��ն�Ȩ�޻���ߵ�ѡ��
            ViewState("QXSET_GAINERID") = Nothing '��յ�ǰѡ�е�Ȩ�޻����
        Else
            '�ǵ�ѡ�������ʾ��ȷ��Ȩ����Ϣ
            Dim strRightsGainerID As String = VStr("QXSET_GAINERID")
            Dim lngRightsValue As Long = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID).lngQX_VALUE
            ShowUserRights(lngRightsValue)
        End If

        GridDataBind() '��Ȩ����Ϣ��DataGrid��
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "Ȩ�޻����"
        col.DataField = "QX_GAINER_NAME"
        'col.ItemStyle.ForeColor = Color.Blue
        col.ItemStyle.Width = Unit.Pixel(140)
        intWidth += 130
        DataGrid1.Columns.Add(col)

        '��Դ�����ID�����ǵ�2�У��޸ġ�ɾ����Դʱ�õ�
        col = New BoundColumn
        col.HeaderText = "ID"
        col.DataField = "QX_GAINER_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "��¼�ʺ�"
        col.DataField = "QX_GAINER_ID2"
        col.ItemStyle.Width = Unit.Pixel(100)
        intWidth += 100
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "��������"
        col.DataField = "QX_GAINER_DEP"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "����"
        col.DataField = "QX_INHERIT_NAME"
        col.ItemStyle.Width = Unit.Pixel(35)
        intWidth += 35
        DataGrid1.Columns.Add(col)

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '������ͼ�꣬�磺"<img src='/cmsweb/images/common/delete.gif' border=0>"
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        colDel.ItemStyle.Width = Unit.Pixel(35)
        intWidth += 35
        DataGrid1.Columns.Add(colDel)

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '----------------------------------------------------------------
    '��ȡ�����ϵ�Ȩ��������Ϣ
    '----------------------------------------------------------------
    Private Function GetUserRightsFromUI() As Long
        'AdjustFunctionsSwitch() '�������ܿ��ؾ����Ĺ���֧�����

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
    '�ڽ�������ʾȨ����Ϣ
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



        AdjustFunctionsSwitch() '�������ܿ��ؾ����Ĺ���֧�����
    End Sub

    '----------------------------------------------------------------------
    '�������ܿ��ؾ����Ĺ���֧�����
    '----------------------------------------------------------------------
    Private Sub AdjustFunctionsSwitch()
        '�жϵ�ǰ��Դ������ĿȨ�޶���
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

        '��������ĵ�
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_ONLINEVIEW")
        If bln = False Then
            chkDocViewOnline.Enabled = False
            chkDocViewOnline.Checked = False
        End If

        '���ߴ�ӡ�ĵ�
        bln = CmsFunc.IsEnable("FUNC_ONLINEPRINT")
        If bln = False Then
            chkDocPrint.Enabled = False
            chkDocPrint.Checked = False
        End If

        '�й�������
        bln = CmsFunc.IsEnable("FUNC_ROWWHERE")
        If bln = False Then lbtnRowFilter.Visible = False

        'Ȩ�޹�����֧������/�޸�/ɾ����Դ
        bln = CmsFunc.IsEnable("FUNC_RESEDIT_RIGHTS")
        If bln = False Then
            chkResAdd.Enabled = False
            chkResAdd.Checked = False
            chkResEdit.Enabled = False
            chkResEdit.Checked = False
            chkResDel.Enabled = False
            chkResDel.Checked = False
        End If

        '�Ƿ�֧���ĵ���
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
