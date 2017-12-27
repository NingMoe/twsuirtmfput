<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CmsFrame" CodeFile="CmsFrame.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Unionsoft 企业管理应用平台</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK  href="../css/cmsstyle.css" rel="stylesheet" type="text/css">
  </head>
  <frameset rows="28,*" frameborder="no">
	<frame src="title.aspx" name="title" frameborder="no" noresize="true" scrolling="no">
	<%if AspPage.RStr("Favorite",Request)<>"" then%>
	<frame src="CmsFrmFavorite.aspx?<%= AspPage.GetBodyPage(Request)%>&noderesid=<%=AspPage.RStr("noderesid", Request)%>&type=<%=AspPage.RStr("type", Request)%>" name="cmsbody" frameborder="no">
	<%else%>
	<frame src="<%= AspPage.GetBodyPage(Request)%>?noderesid=<%=AspPage.RStr("noderesid", Request)%>&type=<%=AspPage.RStr("type", Request)%>&RecId=<%=AspPage.RStr("RecId", Request)%>" name="cmsbody" frameborder="no">
	<%end if%>
  </frameset>
</html>
