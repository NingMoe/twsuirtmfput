<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceEditName" CodeFile="ResourceEditName.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>修改资源</title>
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
								<TD height="22" colspan="2" class="header_level2"><b>修改资源分类</b></TD>
							</TR>
							<TR height="22">
								<td width="99" align="right" style="WIDTH: 99px">资源名称：</td>
								<TD>
									<asp:TextBox id="txtResName" runat="server"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<td width="99" style="WIDTH: 99px"></td>
								<TD height="22"></TD>
							</TR>
							<TR height="22">
								<td style="WIDTH: 99px"></td>
								<TD>
									<asp:Button id="btnEditRes" runat="server" Text="确定" Width="80px"></asp:Button>
									<asp:Button id="btnCancle" runat="server" Text="取消" Width="80px"></asp:Button>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
