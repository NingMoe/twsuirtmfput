<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SetupDatabase" CodeFile="SetupDatabase.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE>系统初始化－创建数据库</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<TR>
					<TD>
						<TABLE class="form_table" width="620" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header" align="left" colSpan="2" height="19">
									系统安装初始化－创建数据库</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="4"></TD>
								<TD vAlign="middle" align="left" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="19">SQL数据库地址：</TD>
								<TD vAlign="middle" align="left" height="19"><asp:textbox id="txtHost" runat="server" Width="200px">127.0.0.1</asp:textbox><asp:label id="Label4" runat="server">（数据库服务器域名或IP地址）</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="19">SQL数据库端口：
								</TD>
								<TD vAlign="middle" align="left" height="19"><asp:textbox id="txtPort" runat="server" Width="200px">1433</asp:textbox><asp:label id="Label5" runat="server">（请不要随意更改此端口号）</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="19">SQL数据库sa密码：
								</TD>
								<TD vAlign="middle" align="left" height="19"><asp:textbox id="txtSaPass" runat="server" Width="200px" TextMode="Password"></asp:textbox><asp:label id="Label3" runat="server">（请向数据库管理员所取）</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="22"></TD>
								<TD vAlign="middle" align="left" height="22"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="19">内容数据库名称：</TD>
								<TD vAlign="middle" align="left" height="19"><asp:textbox id="txtDb1" runat="server" Width="200px"></asp:textbox><asp:label id="lblDb1Comments" runat="server">（请不要随意更改此数据库名称）</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="18">文档数据库名称：</TD>
								<TD vAlign="middle" align="left" height="18"><asp:textbox id="txtDb2" runat="server" Width="200px"></asp:textbox><asp:label id="lblDb2Comments" runat="server">（请不要随意更改此数据库名称）</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="4"></TD>
								<TD vAlign="middle" align="left" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137"></TD>
								<TD vAlign="middle" align="left"><FONT face="宋体">
										<asp:Button id="btnConfirm" runat="server" Text="创建数据库" Width="100px"></asp:Button>
									</FONT>
									<asp:button id="btnExit" runat="server" Width="100px" Text="退出"></asp:button></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="right" width="137" height="4"></TD>
								<TD vAlign="middle" align="left" height="4"></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="right" width="137"><asp:label id="Label9" runat="server">数据库初始化结果：</asp:label></TD>
								<TD vAlign="middle" align="left">
									<asp:textbox id="txtSetupInfo" runat="server" Width="752px" TextMode="MultiLine" Height="160px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
