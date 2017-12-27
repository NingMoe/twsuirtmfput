<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmployeeFrmDepTree" CodeFile="EmployeeFrmDepTree.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>人员管理</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="236" height="100%" border="0" style="PADDING-LEFT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px">
				<tr>
					<td width="4" valign="top"></td>
					<td valign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0" class="table_level2">
							<TR>
								<TD height="25" class="header_level2">&nbsp;人员管理</TD>
							</TR>
							<TR>
								<TD height="100%" valign="top"><asp:Panel id="panelDepTree" style="OVERFLOW: auto" Width="220" Height="100%" runat="server"><%EmployeeFrmDepTree.LoadTreeView(CmsPass, Request, Response)%></asp:Panel>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
