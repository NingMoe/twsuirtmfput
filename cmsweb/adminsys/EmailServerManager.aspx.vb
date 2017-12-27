Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmailServerManager
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAdmin As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAdminHandphone As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtAdminEmail As System.Web.UI.WebControls.TextBox

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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'Load原信息
        txtSmtpServer.Text = CmsConfig.GetSmtpServer(CmsPass)
        txtEmpSender.Text = CmsConfig.GetSmtpDispUser(CmsPass)
        txtSmtpUser.Text = CmsConfig.GetSmtpUser(CmsPass)
        txtSmtpPass.Text = CmsConfig.GetSmtpPass(CmsPass)
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            '校验发送方电子邮件
            Dim strTemp As String = txtEmpSender.Text.Trim()
            If strTemp <> "" AndAlso (strTemp.StartsWith("<") = True Or strTemp.IndexOf("<") < 0 Or strTemp.IndexOf(">") < 0 Or strTemp.IndexOf("@") < 0 Or strTemp.IndexOf(".") < 0) Then
                PromptMsg("请输入有效的发送方电子邮件")
                Return
            End If

            CmsConfig.SetString("SYS_CONFIG", "SMTP_SERVER", txtSmtpServer.Text.Trim())
            CmsConfig.SetString("SYS_CONFIG", "SMTP_DISPUSER", txtEmpSender.Text.Trim())
            CmsConfig.SetString("SYS_CONFIG", "SMTP_USER", txtSmtpUser.Text.Trim())
            CmsConfig.SetString("SYS_CONFIG", "SMTP_PASS", txtSmtpPass.Text.Trim())
            CmsConfig.ReloadConfig()

            PromptMsg("邮件信息设置成功！")
        Catch ex As Exception
            PromptMsg("邮件信息设置异常失败，服务器端文件读写出错！", ex, True)
        End Try
    End Sub

    Private Sub txtEmpSender_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpSender.TextChanged

    End Sub
End Class

End Namespace
