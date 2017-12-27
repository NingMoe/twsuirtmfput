Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web



Partial Class BatchSendEmailSetting
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
            lblTitle.Text = "系统SMTP邮件服务器设置"
            btnExit.Visible = False
        ElseIf VStr("PAGE_SETTING_NAME") = DbParameter.BSEND_SMTP Then
            lblTitle.Text = "群发SMTP邮件服务器设置"
            btnExit.Visible = True
        Else
            PromptMsg("不能识别的邮件设置来源！")
            Return
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'Load原信息
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
            PromptMsg("不能识别的邮件设置来源！")
            Return
        End If

        Try
            '校验发送方电子邮件
            Dim strTemp As String = txtSmtpDispUser.Text.Trim()
            If strTemp <> "" AndAlso (strTemp.StartsWith("<") = True Or strTemp.IndexOf("<") < 0 Or strTemp.IndexOf(">") < 0 Or strTemp.IndexOf("@") < 0 Or strTemp.IndexOf(".") < 0) Then
                PromptMsg("请输入有效的发送方电子邮件")
                Return
            End If

            Dim datParam As New DataParameter
            datParam.strPARM_NAME = VStr("PAGE_SETTING_NAME")
            datParam.strPARM_STR1 = txtSmtpServer.Text.Trim()
            datParam.strPARM_STR2 = txtSmtpUser.Text.Trim()
            datParam.strPARM_STR3 = txtSmtpPass.Text.Trim()
            datParam.strPARM_STR4 = txtSmtpDispUser.Text.Trim()
            DbParameter.SetParameter(SDbConnectionPool.GetDbConfig(), datParam)

            PromptMsg("邮件服务器信息设置成功！")
        Catch ex As Exception
            PromptMsg("邮件服务器信息设置异常失败！", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
