<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RightsSetColumn" CodeFile="RightsSetColumn.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>字段级权限设置</title>
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
								<TD height="22" colspan="2" class="header_level2">
									<b>字段级权限设置 （资源：
										<asp:Label id="lblResDispName" runat="server"></asp:Label>) </b>
								</TD>
							</TR>
							<tr>
								<td height="2px"></td>
							</tr>
							<TR height="22">
								<TD>
									<asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button>
								</TD>
							</TR>
							<tr>
								<td height="2px"></td>
							</tr>
							<TR height="22">
								<TD>
									<asp:DataGrid id="DataGrid1" runat="server" Width="510px" Height="45px" AutoGenerateColumns="False"
										CellPadding="3">
										<HeaderStyle Height="20px"></HeaderStyle>
									</asp:DataGrid>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
