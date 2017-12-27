<%@ Import namespace="System.Data"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import namespace="NetReusables"%>
<%@ Page Language="vb" AutoEventWireup="false" %>
<%

'---------------------------------------------------------------
'实现流程模块的登陆转向
'CHENYU 2010-4-1 CREATE
'---------------------------------------------------------------
If Not Session("CMS_PASSPORT") Is Nothing Then
	Dim action As String = Request.QueryString("action")
	Dim url As String = Request.QueryString("url")
	Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"),CmsPassport)
        Response.Redirect("/webflow/UserValidate.aspx?user=" & pst.Employee.ID & "&ucode=" & pst.Employee.Password & "&url=" & Server.UrlEncode(url))         
Else
	Response.Write("登录超时,请重新登录.")
End If
%>
<%= Not Session("CMS_PASSPORT") Is Nothing%>