Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class MTableSearch
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
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
        btnDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����ǰ������');")
        btnCopy.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫ���Ƶ�ǰ���б�ͳ����');")

        'ע��ͻ���Form�������������������������ѡ�е��м�¼ID
        Dim strSelRec As String = RStr("RECID")
        If strSelRec = "" Then
            strSelRec = RStr("URLRECID")
        End If
        RegisterHiddenField("RECID", strSelRec)

        '�ж��Ƿ�ǰ��Դ���ڲ��ŵĲ��Ź���Ա
        Dim blnIsDepAdmin As Boolean = False
            If OrgFactory.DepDriver.GetDepAdmin(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).DepID, True) = CmsPass.Employee.ID Then
                blnIsDepAdmin = True
            End If

        Select Case VInt("PAGE_MTSLIST_TYPE")
            Case MTSearchType.MultiTableView
                lblTitle.Text = "�б�ͳ��"
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

                '��ʱ���������û��༭�б�ͳ��
                panelMove.Visible = True
                'If CmsPass.EmpIsSysAdmin OrElse CmsPass.EmpIsAspAdmin OrElse blnIsDepAdmin = True Then
                '    If VLng("PAGE_RESID") <> 0 Then
                '        'ֻ�д�ѡ����Դ�������б�ͳ�Ʋ���ʾ�����ƶ����ܣ��Ӳ˵���ϵͳ�����С��б�ͳ�ơ�����Ĳ�����ʾ�����ƶ�����Ϊ��ͬ��Դ�䲻�������ƶ�
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
                lblTitle.Text = "ͨ���й���"
                btnCopy.Visible = True
                If VStr("PAGE_FROMADMIN") = "admin" Then   '����ϵͳ�������
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
                lblTitle.Text = "�����й���"
                If VStr("PAGE_FROMADMIN") = "admin" Then '����ϵͳ�������
                    btnTempApply.Visible = False '��������в�����ʱӦ��
                    btnAdd.Visible = False '������ѡ���û������Բ����������
                    btnCopy.Visible = True
                ElseIf VStr("PAGE_FROMADMIN") = "rights" Then '����Ȩ�޹������
                    btnTempApply.Visible = False '��������в�����ʱӦ��
                    btnCopy.Visible = False
                Else '���Ը������ݹ������
                End If
        End Select
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        MultiTableSearch.DelMTSRecordByExpired(CmsPass) 'ɾ������1�����ʱ���ͳ�ƺ��й�������

        ''��ʼ����ǰ���ͳ�ƺ��й������õ������б�
        'ddlSearchType.Items.Clear()
        'ddlSearchType.Items.Add(New ListItem("�Զ�����ͳ��", "1"))
        'ddlSearchType.Items.Add(New ListItem("ͨ���й�������", "3"))
        'ddlSearchType.Items.Add(New ListItem("�����й�������", "4"))

        If RStr("checkrights_fail") = "yes" Then
            PromptMsg("�޷�����ָ���Ķ��ͳ�ƺ��й������ã���Ϊ��û�ж�����������Դӵ�����Ȩ�ޣ�")
        End If

        GridDataBind() '������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    'Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
    '    If ddlSearchType.SelectedValue = CStr(MTSearchType.MultiTableView) Then
    '        RegisterHiddenField("AllowStartSearch", "1")
    '    Else
    '        RegisterHiddenField("AllowStartSearch", "0")
    '    End If
    '    GridDataBind() '������
    'End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/adminres/MTableSearch.aspx?mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&URLRECID=" & Me.GetRecIDOfGrid() & "&mnuempid=" & VStr("PAGE_EMPID") & "&mnufromadmin=" & VStr("PAGE_FROMADMIN")
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID"), False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/adminres/MTableSearch.aspx?URLRECID=" & lngRecID & "&mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID") & "&mnufromadmin=" & VStr("PAGE_FROMADMIN")
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & lngRecID & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID"), False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        MultiTableSearch.DelMTSRecordByID(CmsPass, lngRecID)
        GridDataBind() '������
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        MultiTableSearch.CopyMTSRecordByID(CmsPass, lngRecID)
        GridDataBind() '������
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        Session("CMSBP_MTableSearchTableReport") = "/cmsweb/adminres/MTableSearch.aspx?URLRECID=" & lngRecID & "&mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID") & "&mnufromadmin=" & VStr("PAGE_FROMADMIN")
        Response.Redirect("/cmsweb/adminres/MTableSearchTableReport.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & lngRecID, True)
    End Sub

    Private Sub btnTempApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTempApply.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If

        Dim strWhere As String = MultiTableSearchColumn.AssembleWhereForOneTable(CmsPass, lngRecID)
        If VLng("PAGE_HOST_RESID") = 0 OrElse VLng("PAGE_HOST_RESID") = VLng("PAGE_RESID") Then '������Դ
            Session("CMS_HOSTTABLE_WHERE") = strWhere
        Else '�ǹ�������Դ
            Session("CMS_SUBTABLE_WHERE") = strWhere
        End If
        Session("CMS_HOSTTABLE_PAGE" & VLng("PAGE_RESID")) = 0
        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "timeid=" & TimeId.CurrentMilliseconds()), False)
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        MultiTableSearch.StartRowFilter(CmsPass, lngRecID, VInt("PAGE_MTSLIST_TYPE"), VStr("PAGE_EMPID"))
        GridDataBind() '������
        Session("CMS_HOSTTABLE_PAGE" & VLng("PAGE_RESID")) = 0
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        MultiTableSearch.StopRowFilter(CmsPass, lngRecID)
        GridDataBind() '������
        Session("CMS_HOSTTABLE_PAGE" & VLng("PAGE_RESID")) = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        CmsDbBase.MoveUpByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & VInt("PAGE_MTSLIST_TYPE"), "MTS_SHOWORDER")
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        CmsDbBase.MoveDownByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & VInt("PAGE_MTSLIST_TYPE"), "MTS_SHOWORDER")
        GridDataBind() '������
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ���в��ִ�������ʹ��
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '���ÿͻ��˵ļ�¼ID��Javascript�������������ǹ��������ԴID
                Dim strRecID As String = row.Cells(0).Text.Trim()
                If IsNumeric(strRecID) Then
                    row.Attributes.Add("RECID", strRecID) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()

                    If lngRecIDClicked > 0 And lngRecIDClicked = CLng(strRecID) Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '�޸ı������¼�ı�����ɫ
                    End If
                End If
            Next
        End If
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "MTS_ID" '�ؼ��ֶ�
        col.DataField = "MTS_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "����״̬"
        col.DataField = "MTS_START2"
        col.ItemStyle.Width = Unit.Pixel(70)
        If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableView Then col.Visible = False '���ͳ���²�����ʾ
        DataGrid1.Columns.Add(col)
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "��ѯ����"
        col.DataField = "MTS_TITLE"
        col.ItemStyle.Width = Unit.Pixel(300)
        DataGrid1.Columns.Add(col)
        intWidth += 300

        col = New BoundColumn
        col.HeaderText = "����Դ"
        col.DataField = "RESID1_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "����"
        col.DataField = "MTS_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(120)
        DataGrid1.Columns.Add(col)
        intWidth += 120

        col = New BoundColumn
        col.HeaderText = "��Ա"
        col.DataField = "MTS_EMPID2"
        col.ItemStyle.Width = Unit.Pixel(100)
        If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableView Then col.Visible = False '���ͳ���²�����ʾ
        DataGrid1.Columns.Add(col)
        intWidth += 100

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
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
