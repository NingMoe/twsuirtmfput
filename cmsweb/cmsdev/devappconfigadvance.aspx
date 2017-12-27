<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DevAppConfigAdvance" validateRequest="false" CodeFile="DevAppConfigAdvance.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">系统工具－更改系统配置</TITLE>
		<meta http-equiv="Pragma" content="no-cache" />
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" method="post" runat="server">
			<TABLE style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px"
				width="100%" border="0">
				<TBODY>
					<TR>
						<!--class="toolbar_table"  系统安装初始化-->
						<TD vAlign="middle" align="center" width="220" colSpan="2">&nbsp;</TD>
					</TR>
					<TR>
						<TD width="2"></TD>
						<TD align="center" width="99%">
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="500" border="0">
								<TR>
									<TD class="header_level2" align="center" colSpan="2" height="19">
										系统工具－更改配置信息(app_config)</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="19"></TD>
									<TD vAlign="middle" align="left" width="340" height="19"><BR>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="19">Section：</TD>
									<TD vAlign="middle" align="left" width="340" height="19">
										<asp:TextBox id="txtSection" runat="server" Width="240px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="18">Key：</TD>
									<TD vAlign="middle" align="left" width="340" height="18">
										<asp:TextBox id="txtKey" runat="server" Width="240px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="18">Value：</TD>
									<TD vAlign="middle" align="left" width="340" height="18">
										<asp:TextBox id="txtValue" runat="server" Width="240px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="160" height="55"></TD>
									<TD vAlign="middle" align="left" width="340" height="55">
										<asp:Button id="btnGetOldValue" runat="server" Width="92px" Text="提取原值"></asp:Button><asp:button id="btnConfirm" runat="server" Text="确认修改" Width="80px"></asp:button>
										<asp:Button id="btnExit" runat="server" Width="76px" Text="退出"></asp:Button>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
