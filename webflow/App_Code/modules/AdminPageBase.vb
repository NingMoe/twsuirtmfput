Option Explicit On 
Option Strict On


Imports NetReusables
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Public Class AdminPageBase
    Inherits PageBase

    '用户登陆验证,判断是否超时 
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim IsValid As Boolean = False

        If Not Session.Item("User") Is Nothing Then
            CurrentUser = CType(Session.Item("User"), Employee)
            If CurrentUser.Code.ToLower() = "admin" Then
                IsValid = True
            End If
        End If

        '无效的登陆,转向重新登陆页面
        If IsValid = False Then
            Response.Write("<script>window.top.document.location.href='../'</script>")
            Response.End()
        End If

    End Sub

End Class

End Namespace
