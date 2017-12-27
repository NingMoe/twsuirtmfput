<%@ Reference Page="~/admin/adminprocessweekanalyse.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.listframe" CodeFile="listframe.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>listframe</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    
  </head>
  <frameset rows="40%,*" frameborder="no" framespacing="8" bordercolor="#3399ff" class="">
	<frame name="worklist" src="<%=url%>">
	<frame name="workitem" src="../common/blank.aspx">
  </frameset>
</html>
