<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCustomizeCoding" validateRequest="false" CodeFile="FieldAdvCustomizeCoding.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>���Ʊ���</title>
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
								<TD class="header_level2" colSpan="2" height="22"><b>�ֶθ߼����ã����Ʊ���</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165"><FONT face="����"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px" align="right" width="165">��ǰ��Դ��</TD>
								<TD align="left"><asp:label id="lblResName" runat="server"></asp:label></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px" align="right" width="165">��ǰ�ֶΣ�</TD>
								<TD align="left"><asp:label id="lblFieldName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165"></TD>
								<TD align="left"><FONT face="����"><asp:radiobutton id="rdoUrl" runat="server" Checked="True" GroupName="Group1" Text="�����´���"></asp:radiobutton>&nbsp;&nbsp;
										<asp:radiobutton id="rdoJavascript" runat="server" GroupName="Group1" Text="Javascript����ִ��"></asp:radiobutton></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">���Ʊ�����Ϣ1��</TD>
								<TD align="left"><asp:textbox id="txtValue1" runat="server" Width="328px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">���Ʊ�����Ϣ2��</TD>
								<TD align="left"><asp:textbox id="txtValue2" runat="server" Width="328px"></asp:textbox></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px" align="right" width="165">�´���X���꣺</TD>
								<TD><FONT face="����"><asp:textbox id="txtFormLeft" runat="server" Width="72px"></asp:textbox>&nbsp;����λ��Pixel��</FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">�´���Y���꣺</TD>
								<TD><FONT face="����"><FONT face="����"><asp:textbox id="txtFormTop" runat="server" Width="72px"></asp:textbox>&nbsp;����λ��Pixel��</FONT></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">�´����ȣ�</TD>
								<TD><FONT face="����"><FONT face="����"><asp:textbox id="txtFormWidth" runat="server" Width="72px"></asp:textbox>&nbsp;����λ��Pixel��</FONT></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">�´���߶ȣ�</TD>
								<TD><FONT face="����"><FONT face="����"><asp:textbox id="txtFormHeight" runat="server" Width="72px"></asp:textbox>&nbsp;����λ��Pixel��</FONT></FONT></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 165px"></TD>
								<td><FONT face="����"><BR>
									</FONT>
									<asp:button id="btnConfirm" runat="server" Text="����" Width="80px"></asp:button><asp:button id="btnCancel" runat="server" Text="�˳�" Width="80px"></asp:button></td>
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
