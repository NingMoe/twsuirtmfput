<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CodeMgrExpired" CodeFile="CodeMgrExpired.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>产品码已过期</TITLE>
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
									<TD class="header_level2" align="center" colSpan="2">
										<asp:Label id="Label1" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Red" Font-Names="宋体">产品码已过期</asp:Label></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="120" height="4"></TD>
									<TD vAlign="middle" align="left" width="480" height="4"></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="120" height="19">用户许可码：</TD>
									<TD vAlign="middle" align="left" width="480" height="19"><asp:textbox id="txtCode1" runat="server" Width="256px"></asp:textbox><asp:button id="btnAddLicense" runat="server" Text="添加用户许可"></asp:button></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="120" height="4"></TD>
									<TD vAlign="middle" align="left" width="480" height="4"></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="right" width="120" height="19"></TD>
									<TD vAlign="top" align="left" width="480" height="19">
										<P>
											<asp:Label id="lblExpireNotes" runat="server"></asp:Label>
											<BR>
											<FONT face="宋体">
												<BR>
											</FONT>
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
  </body>
</HTML>
