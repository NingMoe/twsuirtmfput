<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RightsSetSecurity" CodeFile="RightsSetSecurity.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE id="onetidTitle">权限设置</TITLE>
		
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<SCRIPT>
		</SCRIPT>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="352" border="0">
							<TR>
								<TD class="header_level2" width="320" colSpan="2" height="22"><b>密码更新</b></TD>
							</TR>
							<TR>
								<TD colspan="2" align="left"></TD>
							</TR>
							<TR>
								<TD colspan="2" align="left"><BR>
									为机密以上等级的资源设置权限时，必须同时需要系统安全员帐号(security)密码。<BR>
									<BR>
								</TD>
							</TR>
							<TR height="22">
								<td align="right" width="215">系统安全员：</td>
								<TD width="200"><asp:textbox id="txtSecurityAccount" runat="server" Enabled="False">security</asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="215">系统安全员密码：</TD>
								<TD width="200"><asp:textbox id="txtSecurityPass" runat="server" TextMode="Password"></asp:textbox></TD>
							</TR>
							<TR>
								<td width="215"></td>
								<TD width="200" height="22"><BR>
								</TD>
							</TR>
							<TR height="22">
								<td width="215"></td>
								<TD width="200"><asp:button id="btnConfirm" runat="server" Width="72px" Text="确认"></asp:button>
									<asp:Button id="btnExit" runat="server" Text="取消" Width="56px"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
