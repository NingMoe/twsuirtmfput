Option Explicit On 
Option Strict On


Imports NetReusables
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Public Class AdminPageBase
    Inherits PageBase

    '�û���½��֤,�ж��Ƿ�ʱ 
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim IsValid As Boolean = False

        If Not Session.Item("User") Is Nothing Then
            CurrentUser = CType(Session.Item("User"), Employee)
            If CurrentUser.Code.ToLower() = "admin" Then
                IsValid = True
            End If
        End If

        '��Ч�ĵ�½,ת�����µ�½ҳ��
        If IsValid = False Then
            Response.Write("<script>window.top.document.location.href='../'</script>")
            Response.End()
        End If

    End Sub

End Class

End Namespace
