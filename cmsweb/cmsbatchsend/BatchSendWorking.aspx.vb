Option Strict On
Option Explicit On 

Imports System.Threading

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class BatchSendWorking
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblComments As System.Web.UI.WebControls.Label

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Shared g_lngLock As Queue = New Queue '������ϵͳ��
    Private Shared g_thdBatchSend As Thread = Nothing '���̶߳��󱣴���ȫ�ֱ����У�ȷ�����ᱻ��������

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_MTSHOSTID") = 0 Then
            ViewState("PAGE_MTSHOSTID") = RLng("mtshostid")
        End If
        If VInt("PAGE_BSEND_TYPE") = 0 Then
            ViewState("PAGE_BSEND_TYPE") = RInt("bsend_type")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
            lblTitle.Text = "�ʼ�Ⱥ��"
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            lblTitle.Text = "����Ⱥ��"
        End If
        txtTitle.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.MTableHost, "MTS_TITLE", "MTS_ID=" & VLng("PAGE_MTSHOSTID"))
        Dim hashFieldVal As Hashtable = CmsDbBase.GetRecordByWhere(CmsPass, CmsTables.MTableBatchSend, "BSEND_HOSTID=" & VLng("PAGE_MTSHOSTID"))
        If BatchSendThread.GetStatus = ThreadStatus.Pause Or BatchSendThread.GetStatus = ThreadStatus.Running Then
            '��ʾ��ǰ�������е�Ⱥ�������Ĳ�����Ϣ
            txtTotalNum.Text = CStr(BatchSendThread.TotalNum())
            txtCounter.Text = CStr(BatchSendThread.SentNum())
        Else
            '��ʾ���һ����ɵ�Ⱥ�������Ĳ�����Ϣ
            If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
                txtTotalNum.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_EMAIL_TOTALNUM"))
                txtCounter.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_EMAIL_SENTNUM"))
            ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
                txtTotalNum.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_SMS_TOTALNUM"))
                txtCounter.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_SMS_SENTNUM"))
            End If
        End If
        If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
            txtBSendTimes.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_EMAIL_TIMES"))
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            txtBSendTimes.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_SMS_TIMES"))
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
        CheckButtonStatus() '���ù��ܰ�ť��״̬
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            Dim datParam As DataParameter = DbParameter.GetParameter(SDbConnectionPool.GetDbConfig(), DbParameter.BSEND_SMTP)
            If datParam Is Nothing OrElse datParam.strPARM_STR1 = "" Or datParam.strPARM_STR2 = "" Or datParam.strPARM_STR4 = "" Or datParam.strPARM_STR4.StartsWith("<") = True Or datParam.strPARM_STR4.IndexOf("<") < 0 Or datParam.strPARM_STR4.IndexOf(">") < 0 Or datParam.strPARM_STR4.IndexOf("@") < 0 Or datParam.strPARM_STR4.IndexOf(".") < 0 Then
                PromptMsg("Ⱥ���ʼ�ǰ��������Ⱥ���ʼ�SMTP��������Ϣ")
                Return
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
            Return
        End Try
        Try
            Monitor.Enter(g_lngLock) '����

            '������̨�߳�
            If g_thdBatchSend Is Nothing Then
                StartThread()
            Else
                If BatchSendThread.GetStatus = ThreadStatus.Idle Then
                    StartThread()
                Else
                    PromptMsg("Ⱥ���߳��������У������ظ�������")
                    Return
                End If
            End If
            Thread.Sleep(1000)

            txtTotalNum.Text = CStr(BatchSendThread.TotalNum())
            txtCounter.Text = CStr(BatchSendThread.SentNum())
            CheckButtonStatus() '���ù��ܰ�ť��״̬
            PromptMsg(BatchSendThread.ThreadMessage)
        Catch ex As Exception
            SLog.Fatal("�������㹫ʽ��̨�߳��쳣����", ex)
        Finally
            Monitor.Exit(g_lngLock) '����
        End Try
    End Sub

    Private Sub btnPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPause.Click
        BatchSendThread.PauseThread()
        CheckButtonStatus() '���ù��ܰ�ť��״̬
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    Private Sub btnResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResume.Click
        BatchSendThread.ResumeThread()
        CheckButtonStatus() '���ù��ܰ�ť��״̬
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        BatchSendThread.StopThread()
        CheckButtonStatus() '���ù��ܰ�ť��״̬
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRefresh.Click
        txtTotalNum.Text = CStr(BatchSendThread.TotalNum())
        txtCounter.Text = CStr(BatchSendThread.SentNum())
        CheckButtonStatus() '���ù��ܰ�ť��״̬
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    '--------------------------------------------------------------------
    '���ù��ܰ�ť��״̬
    '--------------------------------------------------------------------
    Private Sub CheckButtonStatus()
        If BatchSendThread.GetStatus() = ThreadStatus.Running Then
            txtStatus.Text = "������"

            btnStart.Enabled = False
            btnPause.Enabled = True
            btnResume.Enabled = False
            btnStop.Enabled = True
        ElseIf BatchSendThread.GetStatus() = ThreadStatus.Idle Then
            txtStatus.Text = "������"

            btnStart.Enabled = True
            btnPause.Enabled = False
            btnResume.Enabled = False
            btnStop.Enabled = False
        ElseIf BatchSendThread.GetStatus() = ThreadStatus.Pause Then
            txtStatus.Text = "��ͣ��"

            btnStart.Enabled = False
            btnPause.Enabled = False
            btnResume.Enabled = True
            btnStop.Enabled = True
        End If

        If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
            txtBSendTimes.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.MTableBatchSend, "BSEND_EMAIL_TIMES", "BSEND_HOSTID=" & VLng("PAGE_MTSHOSTID"))
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            txtBSendTimes.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.MTableBatchSend, "BSEND_SMS_TIMES", "BSEND_HOSTID=" & VLng("PAGE_MTSHOSTID"))
        End If
    End Sub

    '--------------------------------------------------------------------
    '�����߳�
    '--------------------------------------------------------------------
    Private Sub StartThread()
        BatchSendThread.MtsHostID = VLng("PAGE_MTSHOSTID")
        BatchSendThread.BSendType = CType(VInt("PAGE_BSEND_TYPE"), BatchSendType)
        BatchSendThread.CmsPass = CmsPass()
        g_thdBatchSend = New Thread(AddressOf BatchSendThread.Run)
        g_thdBatchSend.Start()
    End Sub
End Class

End Namespace
