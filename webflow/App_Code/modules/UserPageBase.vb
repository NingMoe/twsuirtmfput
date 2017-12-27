Option Strict On
Imports System
Imports System.Web

Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Public Class UserPageBase
    Inherits PageBase

    '�û���½��֤,�ж��Ƿ�ʱ 
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Session.Item("User") Is Nothing Then
            CurrentUser = CType(Session.Item("User"), Employee)
        Else
            '��½��ʱ,ת�����µ�½ҳ��
            Response.Write("<script>window.top.document.location.href='" + Request.ApplicationPath + "/default.htm'</script>")
            Response.End()
        End If
    End Sub

    Protected Function FormatString(ByVal value As Object, ByVal length As Integer) As String
        Dim str As String
        If IsDBNull(value) = False Then
            str = CStr(value)
            If str.Length >= length Then
                Return Left(str, length) & "..."
            Else
                Return str
            End If
        Else
            Return "..."
        End If
    End Function

End Class

End Namespace
