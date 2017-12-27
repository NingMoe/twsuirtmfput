<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CodeMgrLicense" CodeFile="CodeMgrLicense.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">用户许可管理</TITLE>
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
									<asp:Label id="Label2" runat="server" Font-Bold="True" Font-Names="宋体" Font-Size="14px" ForeColor="Red">用户许可码管理</asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="4"></TD>
								<TD vAlign="middle" align="left" width="460" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="19">当前用户许可数：</TD>
								<TD vAlign="middle" align="left" width="460" height="19"><asp:TextBox id="txtCurrentLicenseNum" runat="server" Width="200px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="19">
									<asp:Label id="Label1" runat="server">已建用户帐号数：</asp:Label></TD>
								<TD vAlign="middle" align="left" width="460" height="19">
									<asp:TextBox id="txtCreatedUserNum" runat="server" Width="200px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="4"></TD>
								<TD vAlign="middle" align="left" width="460" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="19">用户许可码：</TD>
								<TD vAlign="middle" align="left" height="19"><asp:textbox id="txtCode1" runat="server" Width="200px"></asp:textbox><asp:button id="btnAddLicense" runat="server" Text="添加用户许可"></asp:button></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="140" height="4"></TD>
								<TD vAlign="middle" align="left" width="460" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="right" width="140" height="19">用户许可码介绍：</TD>
								<TD vAlign="top" align="left" width="460" height="19">
									<P>
										<asp:Label id="lblLicCodeNotes" runat="server"></asp:Label><BR>
										<BR>
										<asp:Label id="lblCorpName" runat="server"></asp:Label>
										<BR>
										客户服务邮箱：
										<asp:Label id="lblServiceEmail" runat="server"></asp:Label><BR>
										客户服务电话：
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
