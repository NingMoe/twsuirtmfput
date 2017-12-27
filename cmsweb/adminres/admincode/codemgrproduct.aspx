<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CodeMgrProduct" CodeFile="CodeMgrProduct.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">产品码管理</TITLE>
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
									<asp:Label id="Label6" runat="server" Font-Bold="True" Font-Names="宋体" Font-Size="14px" ForeColor="Red">产品码信息</asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="120" height="4"></TD>
								<TD vAlign="middle" align="left" width="480" height="4">
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="120" height="19"><asp:Label id="Label4" runat="server">产品码：</asp:Label></TD>
								<TD vAlign="middle" align="left" width="480" height="19"><asp:textbox id="txtCode1" runat="server" Width="300px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="120" height="19"><asp:Label id="Label3" runat="server">产品代号：</asp:Label></TD>
								<TD vAlign="middle" align="left" width="480" height="19"><asp:TextBox id="txtProdSymbol" runat="server" Width="300px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="120" height="19"><asp:Label id="Label1" runat="server">版本号：</asp:Label></TD>
								<TD vAlign="middle" align="left" width="480" height="19"><asp:TextBox id="txtProdVer" runat="server" Width="300px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="120" height="19"><asp:Label id="Label2" runat="server">序列号：</asp:Label></TD>
								<TD vAlign="middle" align="left" width="480" height="19"><asp:TextBox id="txtProdSNum" runat="server" Width="300px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="120" height="4"></TD>
								<TD vAlign="middle" align="left" width="480" height="4">
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" align="right" width="120" height="19">
									<asp:Label id="Label5" runat="server">产品码介绍：</asp:Label></TD>
								<TD vAlign="top" align="left" width="480" height="19"><P>
										<asp:Label id="lblProdCodeNotes" runat="server"></asp:Label></P>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</BODY>
</HTML>
