<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CmsFrmLeftTree" CodeFile="CmsFrmLeftTree.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>资源分类</title>
		<meta http-equiv="Pragma" content="no-cache" />
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/cmsweb/script/poslib.js"></script>
		<script language="JavaScript" src="/cmsweb/script/menu4.js"></script>
		<script language="JavaScript" src="/cmsweb/script/scrollbutton.js"></script>
		<SCRIPT language="JavaScript" src="/cmsweb/script/CmsScript.js"></SCRIPT>
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		<%CmsFrmLeftTree.LoadTreeView(CmsPass, Request, Response)%>
		</form>
	</body>
</HTML>
