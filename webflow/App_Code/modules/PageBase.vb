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

    '当前登陆的用户
    Protected CurrentUser As Employee = Nothing

    '向页面输出javascript
    Protected Sub RegisterClientScript(ByVal Key As String, ByVal Script As String)
        RegisterClientScriptBlock(Key, "<Script Language=""JavaScript"">" & Script & "</Script>")
    End Sub

    '已alert的形式显示消息
    Protected Sub MessageBox(ByVal text As String)
        text = text.Replace("'", "\'")
        RegisterClientScript("MessageBox", "alert('" & text & "');")
    End Sub

    '已html页面的形式显示消息
    Protected Sub SendMessage(ByVal Text As String, ByVal SrcUrl As String)
        Response.Redirect("SysInfomation.aspx?Subject=" & Server.UrlDecode(Text) & "&SrcUrl=" & Server.UrlDecode(SrcUrl))
    End Sub

    '关闭
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
    '    SLog.Err("页面错误.", Server.GetLastError())
    '    Response.Write("<div>系统发生异常：</div>")
    '    Response.Write("<div>" & Server.GetLastError().Message & "</div>")
    '    Response.Write("<div>如有疑问，请及时和系统管理员联系，以便及时解决您所遇到的问题。</div>")
    '    Response.End()
    'End Sub

End Class

End Namespace
