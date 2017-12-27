<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCalculationSettings" validateRequest="false" CodeFile="FieldAdvCalculationSettings.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>计算公式选项</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b>计算公式选项设置</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 108px; HEIGHT: 13px" align="right" width="108"></TD>
								<TD style="HEIGHT: 13px"></TD>
							</TR>
							<TR height="21">
								<TD align="right" width="108" style="WIDTH: 108px"><asp:Label id="Label2" runat="server">当前资源：</asp:Label></TD>
								<TD align="left"><asp:label id="lblResName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD align="right" width="108" style="WIDTH: 108px"><asp:Label id="Label3" runat="server">当前字段：</asp:Label></TD>
								<TD align="left"><asp:Label id="lblFieldName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD align="right" width="108" style="WIDTH: 108px; HEIGHT: 14px"></TD>
								<TD align="left" style="HEIGHT: 14px"></TD>
							</TR>
							<TR>
								<TD align="right" width="108" style="WIDTH: 108px"><asp:Label id="Label9" runat="server">运算时机：</asp:Label></TD>
								<TD><asp:CheckBox id="chkRunRecordAdd" runat="server" Text="本表资源添加记录时运行" Checked="True"></asp:CheckBox><FONT face="宋体">&nbsp;</FONT><asp:CheckBox id="chkRunRecordEdit" runat="server" Text="本表资源修改记录时运行" Checked="True"></asp:CheckBox><FONT face="宋体">&nbsp;</FONT><FONT face="宋体"><BR>
									</FONT><FONT face="宋体">
										<asp:CheckBox id="chkRunRecordAdd2" runat="server" Checked="True" Text="表间资源添加记录时运行"></asp:CheckBox>&nbsp;<asp:CheckBox id="chkRunRecordEdit2" runat="server" Checked="True" Text="表间资源修改记录时运行"></asp:CheckBox>&nbsp;<asp:CheckBox id="chkRunRecordDel2" runat="server" Checked="True" Text="表间资源删除记录时运行"></asp:CheckBox></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 108px; HEIGHT: 13px" align="right" width="108"></TD>
								<TD style="HEIGHT: 13px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 108px; HEIGHT: 20px" align="right" width="108"></TD>
								<TD style="HEIGHT: 20px"><FONT face="宋体"><asp:Button id="btnConfirm" runat="server" Width="72px" Text="确认"></asp:Button><asp:Button id="btnCancel" runat="server" Width="72px" Text="退出"></asp:Button></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
