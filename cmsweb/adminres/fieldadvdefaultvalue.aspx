<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvDefaultValue" validateRequest="false" CodeFile="FieldAdvDefaultValue.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>字段默认值设置</title>
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
								<TD height="22" colspan="2" class="header_level2"><b>字段默认值设置</b></TD>
							</TR>
							<TR>
								<TD align="right" width="90"><FONT face="宋体"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR height="25">
								<TD width="90" align="right">当前资源:</TD>
								<TD><asp:Label id="lblResName" runat="server"></asp:Label></TD>
							</TR>
							<TR height="25">
								<TD width="90" align="right">当前字段:</TD>
								<TD><asp:Label id="lblFieldName" runat="server"></asp:Label></TD>
							</TR>
							<TR height="25">
								<TD width="90" align="right">常量值:</TD>
								<TD><asp:DropDownList id="DropDownList1" runat="server" Width="160px"></asp:DropDownList></TD>
							</TR>
							<TR height="25">
								<TD></TD>
								<td><FONT face="宋体"><BR>
									</FONT>
									<asp:Button id="btnConfirm" runat="server" Width="72px" Text="确认"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Width="56px" Text="取消"></asp:Button>
								</td>
							</TR>
							<TR>
								<td colspan="2" height="5"></td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
