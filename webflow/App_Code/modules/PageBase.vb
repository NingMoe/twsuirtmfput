Option Explicit On 
Option Strict On

Imports System
Imports System.Web
Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Public Class PageBase
    Inherits System.Web.UI.Page

    '��ǰ��½���û�
    Protected CurrentUser As Employee = Nothing

    '��ҳ�����javascript
    Protected Sub RegisterClientScript(ByVal Key As String, ByVal Script As String)
        RegisterClientScriptBlock(Key, "<Script Language=""JavaScript"">" & Script & "</Script>")
    End Sub

    '��alert����ʽ��ʾ��Ϣ
    Protected Sub MessageBox(ByVal text As String)
        text = text.Replace("'", "\'")
        RegisterClientScript("MessageBox", "alert('" & text & "');")
    End Sub

    '��htmlҳ�����ʽ��ʾ��Ϣ
    Protected Sub SendMessage(ByVal Text As String, ByVal SrcUrl As String)
        Response.Redirect("SysInfomation.aspx?Subject=" & Server.UrlDecode(Text) & "&SrcUrl=" & Server.UrlDecode(SrcUrl))
    End Sub

    '�ر�
    Protected Sub Close()
        RegisterClientScript("CloseMe", "top.close();")
    End Sub

    Protected Sub SetLocation(ByVal Url As String)
        If Url <> "" Then
            RegisterClientScript("SetLocation", "document.location.href='" & Url & "'")
        Else
            Close()
        End If
    End Sub

    'Protected Overrides Sub OnError(ByVal e As System.EventArgs)
    '    SLog.Err("ҳ�����.", Server.GetLastError())
    '    Response.Write("<div>ϵͳ�����쳣��</div>")
    '    Response.Write("<div>" & Server.GetLastError().Message & "</div>")
    '    Response.Write("<div>�������ʣ��뼰ʱ��ϵͳ����Ա��ϵ���Ա㼰ʱ����������������⡣</div>")
    '    Response.End()
    'End Sub

End Class

End Namespace
