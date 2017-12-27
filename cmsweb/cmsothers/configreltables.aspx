<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ConfigRelTables" CodeFile="ConfigRelTables.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>关联子资源设置</title>
		<meta http-equiv="Pragma" content="no-cache" />
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">关联子资源显示设置</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:Label id="Label8" runat="server">目标资源</asp:Label></TD>
								<TD style="HEIGHT: 25px" align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 11px" align="right"></TD>
								<TD style="HEIGHT: 11px" align="left"></TD>
							</TR>
							<TR class="module_header">
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Label id="Label3" runat="server">关联子资源显示设置</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right" valign="top"></TD>
								<TD align="left">
									<asp:RadioButton id="rdoShowRelTables" runat="server" Text="显示关联子资源" GroupName="RELTABLES" Checked="True"></asp:RadioButton><FONT face="宋体"><BR>
										<asp:RadioButton id="rdoNotShowRelTables" runat="server" Text="不显示关联子资源" GroupName="RELTABLES"></asp:RadioButton></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="确认"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="取消"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
