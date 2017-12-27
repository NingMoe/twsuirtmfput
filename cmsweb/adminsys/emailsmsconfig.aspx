<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmailSmsConfig" CodeFile="EmailSmsConfig.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FieldAdvanceSetting</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing=0 cellPadding=0>
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header" colSpan="2"><b>电子邮件和手机设置</b></TD>
							</TR>
							<TR height="28">
								<TD style="WIDTH: 165px" align="right" width="165">当前资源</TD>
								<TD><asp:label id="lblResName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">邮件手机字段所在资源</TD>
								<TD><asp:dropdownlist id="ddlRelHostRes" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<td style="WIDTH: 165px" align="right" width="165">电子邮件字段</td>
								<TD><asp:dropdownlist id="ddlEmail" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">手机字段</TD>
								<TD><asp:dropdownlist id="ddlHandphone" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">参考字段</TD>
								<TD><asp:dropdownlist id="ddlRefColumn" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px; HEIGHT: 12px" align="right" width="165"></TD>
								<TD style="HEIGHT: 12px"></TD>
							</TR>
							<TR >
								<TD style="WIDTH: 165px" align="right" width="165"><FONT face=宋体></FONT></TD>
								<TD ><asp:button id="btnConfirm" runat="server" Width="104px" Text="保存设置"></asp:button><asp:button id="btnClearSetting" runat="server" Text="清空设置"></asp:button><asp:button id="btnExit" runat="server" Width="56px" Text="退出"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
