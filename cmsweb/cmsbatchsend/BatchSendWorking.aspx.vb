Option Strict On
Option Explicit On 

Imports System.Threading

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class BatchSendWorking
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblComments As System.Web.UI.WebControls.Label

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Shared g_lngLock As Queue = New Queue '仅用于系统锁
    Private Shared g_thdBatchSend As Thread = Nothing '将线程对象保存在全局变量中，确保不会被垃圾回收

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
            lblTitle.Text = "邮件群发"
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            lblTitle.Text = "短信群发"
        End If
        txtTitle.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.MTableHost, "MTS_TITLE", "MTS_ID=" & VLng("PAGE_MTSHOSTID"))
        Dim hashFieldVal As Hashtable = CmsDbBase.GetRecordByWhere(CmsPass, CmsTables.MTableBatchSend, "BSEND_HOSTID=" & VLng("PAGE_MTSHOSTID"))
        If BatchSendThread.GetStatus = ThreadStatus.Pause Or BatchSendThread.GetStatus = ThreadStatus.Running Then
            '显示当前正在运行的群发操作的参数信息
            txtTotalNum.Text = CStr(BatchSendThread.TotalNum())
            txtCounter.Text = CStr(BatchSendThread.SentNum())
        Else
            '显示最近一次完成的群发操作的参数信息
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
        CheckButtonStatus() '设置功能按钮的状态
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            Dim datParam As DataParameter = DbParameter.GetParameter(SDbConnectionPool.GetDbConfig(), DbParameter.BSEND_SMTP)
            If datParam Is Nothing OrElse datParam.strPARM_STR1 = "" Or datParam.strPARM_STR2 = "" Or datParam.strPARM_STR4 = "" Or datParam.strPARM_STR4.StartsWith("<") = True Or datParam.strPARM_STR4.IndexOf("<") < 0 Or datParam.strPARM_STR4.IndexOf(">") < 0 Or datParam.strPARM_STR4.IndexOf("@") < 0 Or datParam.strPARM_STR4.IndexOf(".") < 0 Then
                PromptMsg("群发邮件前请先设置群发邮件SMTP服务器信息")
                Return
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
            Return
        End Try
        Try
            Monitor.Enter(g_lngLock) '锁定

            '启动后台线程
            If g_thdBatchSend Is Nothing Then
                StartThread()
            Else
                If BatchSendThread.GetStatus = ThreadStatus.Idle Then
                    StartThread()
                Else
                    PromptMsg("群发线程正在运行，不能重复启动！")
                    Return
                End If
            End If
            Thread.Sleep(1000)

            txtTotalNum.Text = CStr(BatchSendThread.TotalNum())
            txtCounter.Text = CStr(BatchSendThread.SentNum())
            CheckButtonStatus() '设置功能按钮的状态
            PromptMsg(BatchSendThread.ThreadMessage)
        Catch ex As Exception
            SLog.Fatal("启动计算公式后台线程异常出错！", ex)
        Finally
            Monitor.Exit(g_lngLock) '解锁
        End Try
    End Sub

    Private Sub btnPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPause.Click
        BatchSendThread.PauseThread()
        CheckButtonStatus() '设置功能按钮的状态
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    Private Sub btnResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResume.Click
        BatchSendThread.ResumeThread()
        CheckButtonStatus() '设置功能按钮的状态
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        BatchSendThread.StopThread()
        CheckButtonStatus() '设置功能按钮的状态
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRefresh.Click
        txtTotalNum.Text = CStr(BatchSendThread.TotalNum())
        txtCounter.Text = CStr(BatchSendThread.SentNum())
        CheckButtonStatus() '设置功能按钮的状态
        PromptMsg(BatchSendThread.ThreadMessage)
    End Sub

    '--------------------------------------------------------------------
    '设置功能按钮的状态
    '--------------------------------------------------------------------
    Private Sub CheckButtonStatus()
        If BatchSendThread.GetStatus() = ThreadStatus.Running Then
            txtStatus.Text = "运行中"

            btnStart.Enabled = False
            btnPause.Enabled = True
            btnResume.Enabled = False
            btnStop.Enabled = True
        ElseIf BatchSendThread.GetStatus() = ThreadStatus.Idle Then
            txtStatus.Text = "闲置中"

            btnStart.Enabled = True
            btnPause.Enabled = False
            btnResume.Enabled = False
            btnStop.Enabled = False
        ElseIf BatchSendThread.GetStatus() = ThreadStatus.Pause Then
            txtStatus.Text = "暂停中"

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
    '启动线程
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
