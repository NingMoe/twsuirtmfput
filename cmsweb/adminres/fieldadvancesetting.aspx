<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvanceSetting" CodeFile="FieldAdvanceSetting.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FieldAdvanceSetting</title>
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
								<TD height="22" colspan="2" class="header_level2"><b>字段高级设置</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 129px" align="right" width="129"><FONT face="宋体"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR height="25">
								<TD width="129" align="right" style="WIDTH: 129px">当前资源：</TD>
								<TD><asp:Label id="lblResName" runat="server"></asp:Label></TD>
							</TR>
							<TR height="25">
								<td width="129" align="right" style="WIDTH: 129px">当前字段：</td>
								<TD><asp:Label id="lblFieldName" runat="server"></asp:Label></TD>
							</TR>
							<TR height="25">
								<td width="129" align="right" style="WIDTH: 129px">高级设置类型：</td>
								<TD>
									<asp:DropDownList id="ddlFieldAdvType" runat="server" Width="136px"></asp:DropDownList>
								</TD>
							</TR>
							<TR height="25">
								<td width="129" align="right" style="WIDTH: 129px"></td>
								<TD height="22"><FONT face="宋体"><BR>
									</FONT>
									<asp:Button id="btnConfirm" runat="server" Text="确认" Width="72px"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Text="取消" Width="56px"></asp:Button>
								</TD>
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
