<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceSelect_View" CodeFile="ResourceSelect_View.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>资源选择</title>
		<meta http-equiv="Pragma" content="no-cache" />
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmstree.css" rel="stylesheet" type="text/css">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
				<tr>
					<td width="4"></td>
					<td valign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="600" border="0" class="table_level2" style="WIDTH: 600px; HEIGHT: 500px">
							<TR height="23">
								<td style="WIDTH: 457px">
									<TABLE cellSpacing="0" cellPadding="0" width="592" border="0" style="WIDTH: 592px; HEIGHT: 460px">
										<TR>
											<TD style="WIDTH: 295px" vAlign="top"></TD>
											<TD vAlign="top"><FONT face="宋体">
													<asp:Button id="btnChooseRes" runat="server" Text="选取资源" Width="80px"></asp:Button>
													<asp:Button id="btnCancel" runat="server" Text="取消" Width="80px"></asp:Button>
												</FONT>
											</TD>
										</TR>
										<tr>
											<td valign="top" style="WIDTH: 295px"><asp:Panel id="panelDepTree" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; OVERFLOW: auto; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
													Width="284" Height="460" runat="server"><%WebTreeDepartment.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/cmsothers/ResourceSelect_View.aspx", "_self")%></asp:Panel>
											</td>
											<td valign="top"><asp:Panel id="panelResTree" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; OVERFLOW: auto; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid"
													Width="284" Height="460" runat="server"><%WebTreeResource.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/cmsothers/ResourceSelect_View.aspx", "_self", "/cmsweb/cmsothers/ResourceSelect_View.aspx", "_self", , , , True, False)%></asp:Panel>
											</td>
										</tr>
									</TABLE>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
