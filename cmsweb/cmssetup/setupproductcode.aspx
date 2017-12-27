<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SetupProductCode" CodeFile="SetupProductCode.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">系统初始化－输入产品码</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<TR>
					<TD>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header" align="left" colSpan="2" height="19"><asp:Label id="Label1" runat="server">请输入软件产品码来激活本软件系统</asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="121" height="4"></TD>
								<TD vAlign="bottom" align="left" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="121" height="19"><asp:Label id="lblProductCode" runat="server">软件产品码：</asp:Label></TD>
								<TD vAlign="bottom" align="left" height="19"><asp:textbox id="txtCode1" runat="server" Width="240px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="121"></TD>
								<TD vAlign="bottom" align="left">
									<asp:button id="btnConfirm" runat="server" Text="确认" Width="80px"></asp:button>
									<asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button>
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="121" height="4"></TD>
								<TD vAlign="bottom" align="left" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="121" height="25"></TD>
								<TD vAlign="bottom" align="left" height="25"><asp:Label id="lblProdCodeNotes" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>
