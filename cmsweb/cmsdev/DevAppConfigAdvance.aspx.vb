Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DevAppConfigAdvance
    Inherits AspPage

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
        Try
            If SStr("DEV_MANAGER") <> "1" Then '校验是否正确登录
                Response.Redirect("/cmsweb/cmsdev/DevLogin.aspx", False)
                Return
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnGetOldValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetOldValue.Click
        Try
            txtValue.Text = CmsConfig.GetString(txtSection.Text.Trim(), txtKey.Text.Trim())
        Catch ex As Exception
            PromptMsg("提取配置信息异常失败！错误信息：" & ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            CmsConfig.SetString(txtSection.Text.Trim(), txtKey.Text.Trim(), txtValue.Text)

            CmsConfig.ReloadAll()

            'PromptMsg("配置信息修改成功！")
        Catch ex As Exception
            PromptMsg("配置信息修改异常失败！错误信息：" & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
    End Sub
End Class

End Namespace
