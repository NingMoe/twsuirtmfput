Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class BatchSendContent
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

        If VLng("PAGE_MTSHOSTID") = 0 Then
            ViewState("PAGE_MTSHOSTID") = RLng("mtshostid")
        End If
        If VInt("PAGE_BSEND_TYPE") = 0 Then
            ViewState("PAGE_BSEND_TYPE") = RInt("bsend_type")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
            lblTitle.Text = "�ʼ���������"
            lblSmsPort.Visible = False
            txtSmsPort.Visible = False
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            lblTitle.Text = "������������"
            lblEmailTitle.Visible = False
            txtEmailTitle.Visible = False
            lblEmailType.Visible = False
            rdoText.Visible = False
            rdoHtml.Visible = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim hashFieldVal As Hashtable = CmsDbBase.GetRecordByWhere(CmsPass, CmsTables.MTableBatchSend, "BSEND_HOSTID=" & VLng("PAGE_MTSHOSTID"))
        If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
            txtEmailTitle.Text = HashField.GetStr(hashFieldVal, "BSEND_EMAIL_TITLE")
            txtConstant.Text = HashField.GetStr(hashFieldVal, "BSEND_EMAIL_CONTENT")
            If HashField.GetInt(hashFieldVal, "BSEND_EMAIL_TYPE") = Mail.MailFormat.Text Then
                rdoText.Checked = True
            ElseIf HashField.GetInt(hashFieldVal, "BSEND_EMAIL_TYPE") = Mail.MailFormat.Html Then
                rdoHtml.Checked = True
            End If

            lblSmsPort.Visible = False
            txtSmsPort.Visible = False
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            txtConstant.Text = HashField.GetStr(hashFieldVal, "BSEND_SMS_CONTENT")
            txtSmsPort.Text = CStr(HashField.GetInt(hashFieldVal, "BSEND_SMS_PORT"))

            lblEmailType.Visible = False
            txtEmailTitle.Visible = False
            rdoText.Visible = False
            rdoHtml.Visible = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim hashFieldVal As New Hashtable
        If VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.Email Then
            hashFieldVal.Add("BSEND_EMAIL_TITLE", txtEmailTitle.Text.Trim())
            hashFieldVal.Add("BSEND_EMAIL_CONTENT", txtConstant.Text.Trim())
            hashFieldVal.Add("BSEND_EMAIL_TYPE", IIf(rdoText.Checked = True, Mail.MailFormat.Text, Mail.MailFormat.Html))
        ElseIf VInt("PAGE_BSEND_TYPE") = BATCHSEND_TYPE.SMS Then
            hashFieldVal.Add("BSEND_SMS_CONTENT", txtConstant.Text.Trim())
            If IsNumeric(txtSmsPort.Text.Trim()) = False Then
                PromptMsg("��������Ч�Ķ��ŷ��Ͷ˿�")
                Return
            End If
            If CInt(txtSmsPort.Text.Trim()) < 0 Or CInt(txtSmsPort.Text.Trim()) > 4 Then
                PromptMsg("��������Ч�Ķ��ŷ��Ͷ˿�")
                Return
            End If
            hashFieldVal.Add("BSEND_SMS_PORT", txtSmsPort.Text.Trim())
        End If
        hashFieldVal.Add("BSEND_HOSTID", VLng("PAGE_MTSHOSTID"))
        CmsDbBase.AddOrEditRecordByWhere(CmsPass, CmsTables.MTableBatchSend, hashFieldVal, "BSEND_HOSTID=" & VLng("PAGE_MTSHOSTID"), "BSEND_ID", "BSEND_SHOWORDER")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
