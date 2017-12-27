<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCustomizeCoding" validateRequest="false" CodeFile="FieldAdvCustomizeCoding.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>定制编码</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 536px; HEIGHT: 212px" cellSpacing="0" cellPadding="0" width="536"
				border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" style="WIDTH: 520px; HEIGHT: 196px" cellSpacing="0" cellPadding="0"
							width="520" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b>字段高级设置－定制编码</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165"><FONT face="宋体"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px" align="right" width="165">当前资源：</TD>
								<TD align="left"><asp:label id="lblResName" runat="server"></asp:label></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px" align="right" width="165">当前字段：</TD>
								<TD align="left"><asp:label id="lblFieldName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165"></TD>
								<TD align="left"><FONT face="宋体"><asp:radiobutton id="rdoUrl" runat="server" Checked="True" GroupName="Group1" Text="跳出新窗体"></asp:radiobutton>&nbsp;&nbsp;
										<asp:radiobutton id="rdoJavascript" runat="server" GroupName="Group1" Text="Javascript方法执行"></asp:radiobutton></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">定制编码信息1：</TD>
								<TD align="left"><asp:textbox id="txtValue1" runat="server" Width="328px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">定制编码信息2：</TD>
								<TD align="left"><asp:textbox id="txtValue2" runat="server" Width="328px"></asp:textbox></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px" align="right" width="165">新窗体X坐标：</TD>
								<TD><FONT face="宋体"><asp:textbox id="txtFormLeft" runat="server" Width="72px"></asp:textbox>&nbsp;（单位：Pixel）</FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">新窗体Y坐标：</TD>
								<TD><FONT face="宋体"><FONT face="宋体"><asp:textbox id="txtFormTop" runat="server" Width="72px"></asp:textbox>&nbsp;（单位：Pixel）</FONT></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">新窗体宽度：</TD>
								<TD><FONT face="宋体"><FONT face="宋体"><asp:textbox id="txtFormWidth" runat="server" Width="72px"></asp:textbox>&nbsp;（单位：Pixel）</FONT></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">新窗体高度：</TD>
								<TD><FONT face="宋体"><FONT face="宋体"><asp:textbox id="txtFormHeight" runat="server" Width="72px"></asp:textbox>&nbsp;（单位：Pixel）</FONT></FONT></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px"></TD>
								<td><FONT face="宋体"><BR>
									</FONT>
									<asp:button id="btnConfirm" runat="server" Text="保存" Width="80px"></asp:button><asp:button id="btnCancel" runat="server" Text="退出" Width="80px"></asp:button></td>
							</TR>
							<TR>
								<td colSpan="2" height="5"></td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
