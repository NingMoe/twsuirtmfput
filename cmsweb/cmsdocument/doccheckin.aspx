<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.Migrated_DocCheckin" CodeFile="DocCheckin.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DocCheckin</title>
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
								<TD height="22" class="header_level2"><b><asp:Literal ID="lblTitle" runat="server" Text="签入文档"></asp:Literal></b></TD>
							</TR>
							<TR>
								<TD height="5"></TD>
							</TR>
							<TR height="22">
								<TD> 
                                    <asp:FileUpload ID="File1" runat="server" /></TD>
							</TR>
							<TR>
								<TD height="5"></TD>
							</TR>
							<TR height="22">
								<TD>
									<asp:Button id="btnCheckin" runat="server" Text="签入" Width="80px"></asp:Button>
									<asp:Button id="btnExit" runat="server" Text="取消" Width="56px"></asp:Button>
								</TD>
							</TR>
							<TR>
								<TD height="5"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
