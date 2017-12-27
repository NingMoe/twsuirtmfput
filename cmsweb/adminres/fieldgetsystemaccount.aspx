<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldGetSystemAccount" CodeFile="FieldGetSystemAccount.aspx.vb" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>人员选取</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="/cmsweb/script/TreeNodeSelect.js"></script>
		<link href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="toolbar_table" cellSpacing="0" border="0">
				<TR>
					<TD width="8"></TD>
					<TD noWrap align="left" width="75"><IMG src="/cmsweb/images/icons/xpFolder.gif" align="absMiddle" width="16" height="16">&nbsp;<A onclick="javascript:ReturnSelectedValue(tvwDeptEmp);" href="#">确认</A></TD>
					<TD noWrap align="left" width="71"><IMG src="/cmsweb/images/icons/xpFolder.gif" align="absMiddle" width="16" height="16">&nbsp;<A onclick="window.close();" href="#">取消</A></TD>
					<td width="100%"></td>
				</TR>
			</TABLE>
			<iewc:TreeView id="tvwDeptEmp" runat="server"></iewc:TreeView>
		</form>
	</body>
</HTML>
