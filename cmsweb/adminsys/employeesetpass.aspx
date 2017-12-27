<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmployeeSetPass" CodeFile="EmployeeSetPass.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>密码设置</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>密码修改</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 114px" align="right" width="114"><FONT face="宋体"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR height="22">
								<td width="114" align="right" style="WIDTH: 114px">登陆帐号：</td>
								<TD>
									<asp:Label id="lblEmpID" runat="server"></asp:Label><FONT face="宋体">&nbsp;（
										<asp:Label id="lblEmpName" runat="server">lblEmpName</asp:Label>）</FONT>
								</TD>
							</TR>
							<TR height="22">
								<td width="114" align="right" style="WIDTH: 114px">旧密码：</td>
								<TD>
									<asp:TextBox id="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
								</TD>
							</TR>
							<TR height="22">
								<td width="114" align="right" style="WIDTH: 114px">新密码：</td>
								<TD>
									<asp:TextBox id="txtNewPass1" runat="server" TextMode="Password"></asp:TextBox>
								</TD>
							</TR>
							<TR height="22">
								<td width="114" align="right" style="WIDTH: 114px">新密码确认：</td>
								<TD>
									<asp:TextBox id="txtNewPass2" runat="server" TextMode="Password"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<td width="114" style="WIDTH: 114px"></td>
								<TD height="22"></TD>
							</TR>
							<TR height="22">
								<td style="WIDTH: 114px"></td>
								<TD>
									<asp:Button id="btnConfirm" runat="server" Text="确认" Width="64px"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Text="退出" Width="48px"></asp:Button>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
