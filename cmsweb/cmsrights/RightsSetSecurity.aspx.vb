Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSetSecurity
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtOldPass As System.Web.UI.WebControls.TextBox

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
        SetFocusOnTextbox("txtSecurityPass") '设置键盘光标默认选中的输入框
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        '检验系统安全员密码
        If OrgFactory.EmpDriver.ValidatePass(SDbConnectionPool.GetDbConfig(), "security", txtSecurityPass.Text.Trim()) Then
            Session("CMS_QXSECURITY_VERIFIED") = "OK"
            Response.Redirect(SStr("CMSBP_RightsSetSecurity1"), False)
        Else
            PromptMsg("系统安全员帐号密码错误，请重新输入！")
            Session("CMS_QXSECURITY_VERIFIED") = Nothing
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Session("CMS_QXSECURITY_VERIFIED") = Nothing
        Response.Redirect(SStr("CMSBP_RightsSetSecurity2"), False)
    End Sub
End Class

End Namespace
