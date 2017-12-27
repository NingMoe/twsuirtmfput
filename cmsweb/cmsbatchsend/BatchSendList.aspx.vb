Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web



Partial Class BatchSendList
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        'btnBSendEmail.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫ��ʼȺ�������ʼ���');")
        'btnBSendSms.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫ��ʼȺ���ֻ�������');")
        btnDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����ǰѡ�е�Ⱥ��������');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind() '������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnBSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBSendEmail.Click
        RunBatchSend(BATCHSEND_TYPE.Email)
    End Sub

    Private Sub btnBSendSms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBSendSms.Click
        RunBatchSend(BATCHSEND_TYPE.SMS)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mtstype=" & MTSearchType.BatchSend & "&URLRECID=" & Me.GetRecIDOfGrid()
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.BatchSend, False)

        'Session("CMSBP_BatchSendSetting") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & Me.GetRecIDOfGrid()
        'Response.Redirect("/cmsweb/BatchSendSetting.aspx", False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.BatchSend
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & lngRecID & "&mtstype=" & MTSearchType.BatchSend, False)

        'Session("CMSBP_BatchSendSetting") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        'Response.Redirect("/cmsweb/BatchSendSetting.aspx?mtshostid=" & lngRecID, False)
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

    Private Sub btnEmailContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailContent.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        Session("CMSBP_BatchSendContent") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendContent.aspx?mtshostid=" & lngRecID & "&bsend_type=" & BATCHSEND_TYPE.Email, False)
    End Sub

    Private Sub btnSmsContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmsContent.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        Session("CMSBP_BatchSendContent") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendContent.aspx?mtshostid=" & lngRecID & "&bsend_type=" & BATCHSEND_TYPE.SMS, False)
    End Sub

    Private Sub btnEmailServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailServer.Click
        Session("CMSBP_BatchSendEmailSetting") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & Me.GetRecIDOfGrid()
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendEmailSetting.aspx?emailset_type=" & DbParameter.BSEND_SMTP, False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        'CmsDbBase.MoveUpByRecID(CmsPass, CmsTables.MTableHost, lngRecID, "MTS_TYPE=" & MTSearchType.BatchSend)
        CmsDbBase.MoveUpByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & MTSearchType.BatchSend, "MTS_SHOWORDER")
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        'CmsDbBase.MoveDownByRecID(CmsPass, CmsTables.MTableHost, lngRecID, "MTS_TYPE=" & MTSearchType.BatchSend)
        CmsDbBase.MoveDownByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & MTSearchType.BatchSend, "MTS_SHOWORDER")
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
        col.HeaderText = "Ⱥ������"
        col.DataField = "MTS_TITLE"
        col.ItemStyle.Width = Unit.Pixel(400)
        DataGrid1.Columns.Add(col)
        intWidth += 400

        col = New BoundColumn
        col.HeaderText = "����Դ"
        col.DataField = "RESID1_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "������Ա"
        col.DataField = "MTS_EMPID2"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = MultiTableSearch.GetMTSearchByDataSet(CmsPass, "", MTSearchType.BatchSend, 0, CmsPass.Employee.ID)
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    '---------------------------------------------------------------
    '��ʼ����Ⱥ������
    '---------------------------------------------------------------
    Private Sub RunBatchSend(ByVal intBSendType As BATCHSEND_TYPE)
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч�ļ�¼��")
            Return
        End If
        If lngRecID <> BatchSendThread.MtsHostID() Then
            If BatchSendThread.GetStatus = ThreadStatus.Pause Or BatchSendThread.GetStatus = ThreadStatus.Running Then
                Dim strTitle As String = CmsDbBase.GetFieldStr(CmsPass, CmsTables.MTableHost, "MTS_TITLE", "MTS_ID=" & BatchSendThread.MtsHostID())
                PromptMsg("��ǰ������������Ⱥ�����������⣺" & strTitle & "��������ͬʱ����2����2�����ϵ�Ⱥ��������")
                Return
            Else
                BatchSendThread.ThreadMessage = ""
                BatchSendThread.TotalNum = 0
                BatchSendThread.SentNum = 0
            End If
        End If

        Session("CMSBP_BatchSendWorking") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendWorking.aspx?mtshostid=" & lngRecID & "&bsend_type=" & intBSendType, False)
    End Sub
End Class

End Namespace
