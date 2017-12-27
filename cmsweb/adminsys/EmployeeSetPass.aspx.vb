Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmployeeSetPass
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label

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
        lblEmpID.Text = RStr("empid") 'SStr("EMPPASS_EMPID")
        lblEmpName.Text = RStr("empname")
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strOldPass As String = txtOldPass.Text.Trim()
            Dim strNewPass1 As String = txtNewPass1.Text.Trim()
            Dim strNewPass2 As String = txtNewPass2.Text.Trim()
            If strNewPass1 <> strNewPass2 Then
                PromptMsg("新密码确认错误，请重新输入！")
                Return
            End If
            If strNewPass1.Trim().Length > 20 Then
                Throw New CmsException("用户密码长度不能超过20位！")
            End If

            'If OrgFactory.EmpDriver.ValidatePass(SDbConnectionPool.GetDbConfig(), lblEmpID.Text.Trim(), strOldPass) Then
            '    OrgFactory.EmpDriver.SetPass(CmsPass, lblEmpID.Text.Trim(), strNewPass1)
            '    PromptMsg("密码修改成功！") '成功信息，并非错误
            '    Return
            'Else
            '    PromptMsg("旧密码校验失败，请重新输入！")
            '    Return
            'End If
            'OrgFactory.EmpDriver.UpdatePassword(CmsPass.Employee.ID, strOldPass, strNewPass1)

            OrgFactory.EmpDriver.SetPass(CmsPass, lblEmpID.Text.Trim(), strNewPass1)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(SStr("EMPPASS_BACKPAGE"), False)
    End Sub
End Class

End Namespace
