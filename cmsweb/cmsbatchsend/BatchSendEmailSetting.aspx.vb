Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web



Partial Class BatchSendEmailSetting
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

        If VStr("PAGE_SETTING_NAME") = "" Then
            ViewState("PAGE_SETTING_NAME") = RStr("emailset_type")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If CmsPass.EmpIsSysAdmin = False AndAlso CmsPass.EmpIsDepAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        If VStr("PAGE_SETTING_NAME") = DbParameter.SYS_SMTP Then
            lblTitle.Text = "ϵͳSMTP�ʼ�����������"
            btnExit.Visible = False
        ElseIf VStr("PAGE_SETTING_NAME") = DbParameter.BSEND_SMTP Then
            lblTitle.Text = "Ⱥ��SMTP�ʼ�����������"
            btnExit.Visible = True
        Else
            PromptMsg("����ʶ����ʼ�������Դ��")
            Return
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'Loadԭ��Ϣ
        Dim datParam As DataParameter = DbParameter.GetParameter(SDbConnectionPool.GetDbConfig(), VStr("PAGE_SETTING_NAME"))
        If Not datParam Is Nothing Then
            txtSmtpServer.Text = datParam.strPARM_STR1
            txtSmtpUser.Text = datParam.strPARM_STR2
            txtSmtpPass.Text = datParam.strPARM_STR3
            txtSmtpDispUser.Text = datParam.strPARM_STR4
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If VStr("PAGE_SETTING_NAME") = "" Then
            PromptMsg("����ʶ����ʼ�������Դ��")
            Return
        End If

        Try
            'У�鷢�ͷ������ʼ�
            Dim strTemp As String = txtSmtpDispUser.Text.Trim()
            If strTemp <> "" AndAlso (strTemp.StartsWith("<") = True Or strTemp.IndexOf("<") < 0 Or strTemp.IndexOf(">") < 0 Or strTemp.IndexOf("@") < 0 Or strTemp.IndexOf(".") < 0) Then
                PromptMsg("��������Ч�ķ��ͷ������ʼ�")
                Return
            End If

            Dim datParam As New DataParameter
            datParam.strPARM_NAME = VStr("PAGE_SETTING_NAME")
            datParam.strPARM_STR1 = txtSmtpServer.Text.Trim()
            datParam.strPARM_STR2 = txtSmtpUser.Text.Trim()
            datParam.strPARM_STR3 = txtSmtpPass.Text.Trim()
            datParam.strPARM_STR4 = txtSmtpDispUser.Text.Trim()
            DbParameter.SetParameter(SDbConnectionPool.GetDbConfig(), datParam)

            PromptMsg("�ʼ���������Ϣ���óɹ���")
        Catch ex As Exception
            PromptMsg("�ʼ���������Ϣ�����쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
