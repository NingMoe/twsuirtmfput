<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CodeMgrLicense" CodeFile="CodeMgrLicense.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">�û���ɹ���</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="600" border="0">
							<TR>
								<TD class="header_level2" align="center" colSpan="2" height="19">
									<asp:Label id="Label2" runat="server" Font-Bold="True" Font-Names="����" Font-Size="14px" ForeColor="Red">�û���������</asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="4"></TD>
								<TD vAlign="middle" align="left" width="460" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="19">��ǰ�û��������</TD>
								<TD vAlign="middle" align="left" width="460" height="19"><asp:TextBox id="txtCurrentLicenseNum" runat="server" Width="200px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="19">
									<asp:Label id="Label1" runat="server">�ѽ��û��ʺ�����</asp:Label></TD>
								<TD vAlign="middle" align="left" width="460" height="19">
									<asp:TextBox id="txtCreatedUserNum" runat="server" Width="200px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="4"></TD>
								<TD vAlign="middle" align="left" width="460" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="19">�û�����룺</TD>
								<TD vAlign="middle" align="left" height="19"><asp:textbox id="txtCode1" runat="server" Width="200px"></asp:textbox><asp:button id="btnAddLicense" runat="server" Text="����û����"></asp:button></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="4"></TD>
								<TD vAlign="middle" align="left" width="460" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="right" width="140" height="19">�û��������ܣ�</TD>
								<TD vAlign="top" align="left" width="460" height="19">
									<P>
										<asp:Label id="lblLicCodeNotes" runat="server"></asp:Label><BR>
										<BR>
										<asp:Label id="lblCorpName" runat="server"></asp:Label>
										<BR>
										�ͻ��������䣺
										<asp:Label id="lblServiceEmail" runat="server"></asp:Label><BR>
										�ͻ�����绰��
										<asp:Label id="lblServicePhone" runat="server"></asp:Label></P>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</BODY>
</HTML>
