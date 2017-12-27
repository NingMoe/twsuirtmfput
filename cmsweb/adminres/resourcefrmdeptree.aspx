<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceFrmDepTree" CodeFile="ResourceFrmDepTree.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResourceFrmDepTree</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmstree.css" rel="stylesheet" type="text/css">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="cmsaction">
			<TABLE style="PADDING-LEFT: 0px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px" height="100%" cellSpacing="0"
				cellPadding="0" width="236" border="0">
				<tr>
					<td vAlign="top" width="4"></td>
					<td vAlign="top">
						<TABLE class="table_level2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD class="header_level2" width="145" height="25" style="WIDTH: 145px">
									<IMG src="/cmsweb/images/tree/dep_real.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;&nbsp;<asp:Label id="lblDepTitle" runat="server"></asp:Label></TD>
								<TD class="header_level2" align="right" width="2" height="25"><asp:Panel id="Panel1" runat="server" Width="1px" Height="1px">&nbsp;</asp:Panel>
								</TD>
							</TR>
							<TR>
								<TD height="100%" valign="top" colspan="2"><%ResourceFrmDepTree.LoadTreeView(CmsPass, Request, Response)%></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
