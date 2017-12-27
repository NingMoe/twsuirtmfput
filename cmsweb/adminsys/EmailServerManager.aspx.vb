Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmailServerManager
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAdmin As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAdminHandphone As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAdminEmail As System.Web.UI.WebControls.TextBox

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
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'Loadԭ��Ϣ
        txtSmtpServer.Text = CmsConfig.GetSmtpServer(CmsPass)
        txtEmpSender.Text = CmsConfig.GetSmtpDispUser(CmsPass)
        txtSmtpUser.Text = CmsConfig.GetSmtpUser(CmsPass)
        txtSmtpPass.Text = CmsConfig.GetSmtpPass(CmsPass)
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            'У�鷢�ͷ������ʼ�
            Dim strTemp As String = txtEmpSender.Text.Trim()
            If strTemp <> "" AndAlso (strTemp.StartsWith("<") = True Or strTemp.IndexOf("<") < 0 Or strTemp.IndexOf(">") < 0 Or strTemp.IndexOf("@") < 0 Or strTemp.IndexOf(".") < 0) Then
                PromptMsg("��������Ч�ķ��ͷ������ʼ�")
                Return
            End If

            CmsConfig.SetString("SYS_CONFIG", "SMTP_SERVER", txtSmtpServer.Text.Trim())
            CmsConfig.SetString("SYS_CONFIG", "SMTP_DISPUSER", txtEmpSender.Text.Trim())
            CmsConfig.SetString("SYS_CONFIG", "SMTP_USER", txtSmtpUser.Text.Trim())
            CmsConfig.SetString("SYS_CONFIG", "SMTP_PASS", txtSmtpPass.Text.Trim())
            CmsConfig.ReloadConfig()

            PromptMsg("�ʼ���Ϣ���óɹ���")
        Catch ex As Exception
            PromptMsg("�ʼ���Ϣ�����쳣ʧ�ܣ����������ļ���д����", ex, True)
        End Try
    End Sub

    Private Sub txtEmpSender_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpSender.TextChanged

    End Sub
End Class

End Namespace
