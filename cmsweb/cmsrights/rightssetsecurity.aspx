<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RightsSetSecurity" CodeFile="RightsSetSecurity.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE id="onetidTitle">Ȩ������</TITLE>
		
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
								<TD class="header_level2" width="320" colSpan="2" height="22"><b>�������</b></TD>
							</TR>
							<TR>
								<TD colspan="2" align="left"></TD>
							</TR>
							<TR>
								<TD colspan="2" align="left"><BR>
									Ϊ�������ϵȼ�����Դ����Ȩ��ʱ������ͬʱ��Ҫϵͳ��ȫԱ�ʺ�(security)���롣<BR>
									<BR>
								</TD>
							</TR>
							<TR height="22">
								<td align="right" width="215">ϵͳ��ȫԱ��</td>
								<TD width="200"><asp:textbox id="txtSecurityAccount" runat="server" Enabled="False">security</asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="215">ϵͳ��ȫԱ���룺</TD>
								<TD width="200"><asp:textbox id="txtSecurityPass" runat="server" TextMode="Password"></asp:textbox></TD>
							</TR>
							<TR>
								<td width="215"></td>
								<TD width="200" height="22"><BR>
								</TD>
							</TR>
							<TR height="22">
								<td width="215"></td>
								<TD width="200"><asp:button id="btnConfirm" runat="server" Width="72px" Text="ȷ��"></asp:button>
									<asp:Button id="btnExit" runat="server" Text="ȡ��" Width="56px"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
