Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class UpdatePassword
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

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        If txtNewPass1.Text <> txtNewPass2.Text Then
                PromptMsg("请输入相同的新密码/Please enter the same new password!")
            Return
        End If

        Try
            'OrgFactory.EmpDriver.UpdatePassword(CmsPass.Employee.ID, txtOldPass.Text, txtNewPass2.Text)
            OrgFactory.EmpDriver.SetPass(CmsPass, CmsPass.Employee.ID, txtNewPass2.Text)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

    End Sub
End Class

End Namespace
