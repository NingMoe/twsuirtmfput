Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class UpdateUserProfile
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        btnConfirm.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")
        'txtEmpSender.Attributes.Add("fType", "email")
        'txtEmpSender.Attributes.Add("errmsg", "[" & CmsPass.EmpEmailSender & "]")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'Load用户Profile原信息
        txtEmpID.Text = CmsPass.Employee.ID
        txtEmpName.Text = CmsPass.Employee.Name
        txtEmpHandphone.Text = CmsPass.Employee.Handphone
        txtEmpSender.Text = CmsPass.Employee.Email
        txtSmtpServer.Text = CmsPass.Employee.EmailSmtp
        txtSMTPAccount.Text = CmsPass.Employee.EmailAccount
        txtSMTPPass.Text = CmsPass.Employee.EmailPassword

        ddlLanguage.Items.Clear()
        'ddlLanguage.Items.Add(New ListItem("", ""))
        ddlLanguage.Items.Add(New ListItem("中文", "中文"))
        If CmsFunc.MultiLanguageEnable() = True Then '支持动态多语言
            ddlLanguage.Items.Add(New ListItem("English", "English"))
        End If
        Try
            ddlLanguage.SelectedValue = CmsPass.Employee.Language
        Catch ex As Exception
        End Try

        ddlLoginPage.Items.Clear()
        'ddlLoginPage.Items.Add(New ListItem("", ""))
        ddlLoginPage.Items.Add(New ListItem("内容管理CMS", "CMS"))
        If CmsFunc.IsEnable("FUNC_WORKFLOW") = True Then  '支持工作流
            ddlLoginPage.Items.Add(New ListItem("工作流WORKFLOW", "FLOW"))
        End If
        Try
            ddlLoginPage.SelectedValue = CmsPass.Employee.Mainpage
        Catch ex As Exception
        End Try

        If CmsFunc.MultiLanguageEnable() = False Then '不支持多语言
            ddlLanguage.Visible = False
            lblLanguage.Visible = False
        End If
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

            '更新数据库
            OrgFactory.EmpDriver.SetUserProfile(CmsPass, CmsPass.Employee.ID, txtEmpName.Text.Trim(), txtEmpHandphone.Text.Trim(), txtEmpSender.Text.Trim(), txtSmtpServer.Text.Trim(), txtSMTPAccount.Text.Trim(), txtSMTPPass.Text.Trim(), ddlLanguage.SelectedValue, ddlLoginPage.SelectedValue)

            '更新缓存
            CmsPassport.RetrieveUserInfoWithEncryptPass(CmsPass, SDbConnectionPool.GetDbConfig(), CmsPass.Employee.ID, CmsPass.Employee.Password, Request.UserHostAddress())

            'CmsPass.Employee.Name = txtEmpName.Text.Trim()
            'CmsPass.Employee.Handphone = txtEmpHandphone.Text.Trim()
            'CmsPass.Employee.Email = txtEmpSender.Text.Trim()
            'CmsPass.Employee.EmailSmtp = txtSmtpServer.Text.Trim()
            'CmsPass.Employee.EmailAccount = txtSMTPAccount.Text.Trim()
            'CmsPass.Employee.EmailPassword = txtSMTPPass.Text.Trim()
            'CmsPass.Employee.Language = ddlLanguage.SelectedValue
            'CmsPass.Employee.Mainpage = ddlLoginPage.SelectedValue

            'OrgFactory.EmpDriver.UpdateEmployee(CmsPass.Employee)

            PromptMsg("用户基本信息修改成功！")
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub
End Class
End Namespace
