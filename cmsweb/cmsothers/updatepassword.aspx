<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.UpdatePassword" validateRequest="false" CodeFile="UpdatePassword.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">�޸��û�����</TITLE>
		<meta http-equiv="Pragma" content="no-cache" />
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<SCRIPT>
		</SCRIPT>
		<FORM id="Form1" action="" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="500" border="0">
							<TR>
								<TD class="header_level2" width="400" colSpan="2" height="22"><b><asp:Label ID="lblTitle" runat="server" Text="�������(Update Password)"></asp:Label> </b></TD>
							</TR>
							<TR>
								<td width="220" height="4px"></td>
								<TD width="280" height="4px"></TD>
							</TR>
							<TR height="22">
								<td align="right" width="220"><asp:Label ID="lblOldPwd" runat="server" Text="������(Old Password)"></asp:Label>��</td>
								<TD width="280"><asp:textbox id="txtOldPass" runat="server" TextMode="Password" Width="212px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="220"><asp:Label ID="lblNewPwd" runat="server" Text="������(New Password)"></asp:Label>��</TD>
								<TD width="280"><asp:textbox id="txtNewPass1" runat="server" TextMode="Password" Width="212px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="220"><asp:Label ID="lblConfirmPwd" runat="server" Text="������ȷ��(Confirm Password)"></asp:Label>��</TD>
								<TD width="280"><asp:textbox id="txtNewPass2" runat="server" TextMode="Password" Width="212px"></asp:textbox></TD>
							</TR>
							<TR>
								<td width="220" height="4px"></td>
								<TD width="280" height="4px"></TD>
							</TR>
							<TR height="22">
								<td width="220"></td>
								<TD width="280"><asp:button id="btnConfirm" runat="server" Width="108px" Text="�޸�����(Update)"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
		<script language="javascript">
<!--

	self.document.forms(0).txtOldPass.focus();
-->
		</script>
	</BODY>
</HTML>
