<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceExportResult" CodeFile="ResourceExportResult.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>资源导出结果</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" rel="stylesheet" type="text/css">
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>资源导出</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 44px" align="right" width="44"><FONT face="宋体"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 44px" align="right" width="44"></TD>
								<TD>
									<asp:Label id="lblSuccess" runat="server" Font-Bold="True">资源导出详细信息：</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 44px" align="right" width="44"></TD>
								<TD><FONT face="宋体"><BR>
									</FONT>
									<asp:Label id="lblResult" runat="server" Width="392px" Height="368px"></asp:Label></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 44px"></TD>
								<td><FONT face="宋体"><BR>
									</FONT>
									<asp:Button id="btnDownload" runat="server" Text="下载导出文件"></asp:Button>
									<asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button>
								</td>
							</TR>
							<TR>
								<TD colSpan="2" height="5"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
