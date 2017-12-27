<%@ Reference Page="~/admin/adminprocessweekanalyse.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web._default" CodeFile="default.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>工作流程管理</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
  </head>
  
  <%
  'Response.Write(Request.ApplicationPath)
  'Response.End()
  %>
  <frameset rows="50,*" frameborder="no" framespacing="0">
	<frame src="head.aspx" scrolling="no">
	<frameset cols="180,*" frameborder="no" framespacing="0">
		<frame name="tools" src="tools.aspx">
		<frame name="listframe" src="listframe.aspx">
	</frameset>
  </frameset>
  </body>
</html>
