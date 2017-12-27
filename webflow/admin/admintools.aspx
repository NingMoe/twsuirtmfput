<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.AdminTools" CodeFile="AdminTools.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.AdminPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminTools</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../script/MzTreeView10.js"></script>
		<script type="text/javascript" src="../script/prototype.js"></script>
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">		
		<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="5" width="2"></td>
		</tr>
		</table>
		<div id="treeviewarea" style="height:100%;overflow:auto;"></div>
		<SCRIPT LANGUAGE="JavaScript">
		<!--
		var tree = new MzTreeView("tree");
		tree.setIconPath("../images/tree/");
		<%LoadTreeView()%>
		tree.nodes['-1_10']='id:10;text:当前待办任务统计;target:AdminPlatform;url:AdminProcessWeekAnalyse.aspx;icon:icon;';
		document.getElementById('treeviewarea').innerHTML = tree.toString();
		tree.expandAll();
		-->
		</SCRIPT>
		</table>
		</form>
	</body>
</HTML>

