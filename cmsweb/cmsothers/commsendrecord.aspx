<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CommSendRecord" CodeFile="CommSendRecord.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>���ͼ�¼</title>
		<meta http-equiv="Pragma" content="no-cache">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" style="WIDTH: 664px" cellSpacing="0" cellPadding="0" width="664"
							border="0">
							<TR>
								<TD class="header_level2" style="WIDTH: 144px" colSpan="3" height="22"><b>�ֻ�֪ͨ</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 12px" height="5"></TD>
								<TD style="WIDTH: 110px" height="5"></TD>
								<TD height="5"></TD>
							</TR>
							<TR style="DISPLAY: none" height="22">
								<TD style="WIDTH: 12px" align="right"></TD>
								<TD style="WIDTH: 110px" align="right"><FONT face="����"><asp:label id="Label1" runat="server">�ʼ��ռ��ˣ�</asp:label></FONT></TD>
								<TD><asp:textbox id="txtEmails" runat="server" Width="428px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:linkbutton id="lbtnAddrbookEmail" runat="server">�ʼ���</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 12px" align="right"></TD>
								<TD style="WIDTH: 110px" align="right"><asp:label id="Label2" runat="server">�����ռ��ˣ�</asp:label></TD>
								<TD><asp:textbox id="txtPhones" runat="server" Width="428px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:linkbutton id="lbtnAddrbookSms" runat="server" Visible="False">�ֻ���</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 12px" align="right" height="10"></TD>
								<TD style="WIDTH: 110px" align="right" height="10"></TD>
								<TD height="10"></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD style="WIDTH: 12px" align="right" height="10"></TD>
								<TD style="WIDTH: 110px" align="right" height="10"><asp:label id="Label3" runat="server">�ʼ����⣺</asp:label></TD>
								<TD height="10"><asp:textbox id="txtEmailTitle" runat="server" Width="428px"></asp:textbox><asp:label id="Label5" runat="server">���������ʼ���</asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 12px" align="right"></TD>
								<TD style="WIDTH: 110px" vAlign="top" align="right"><asp:label id="Label4" runat="server">�������ݣ�</asp:label></TD>
								<TD><asp:textbox id="txtContent" runat="server" Width="512px" TextMode="MultiLine" Height="344px"></asp:textbox></TD>
							</TR>
							<TR style="DISPLAY: none">
								<TD style="WIDTH: 12px" align="right"></TD>
								<TD style="WIDTH: 110px" align="right"></TD>
								<TD><FONT face="����"><asp:checkbox id="chkAddAttach" runat="server" Text="����¼�а������ĵ���Ϊ�������ͣ�ע�����Ų�֧�ַ��͸�����"></asp:checkbox></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 12px" align="right"></TD>
								<TD style="WIDTH: 110px" align="right"></TD>
								<TD><FONT face="����"><BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 12px" align="right"></TD>
								<TD style="WIDTH: 110px" align="right"></TD>
								<TD><asp:button id="btnSendEmail" runat="server" Width="88px" Text="�����ʼ�" Visible="False"></asp:button><asp:button id="btnSendSms" runat="server" Width="88px" Text="���Ͷ���"></asp:button><FONT face="����">&nbsp;</FONT>
									<asp:button id="btnClear" runat="server" Width="88px" Text="�������"></asp:button><FONT face="����">&nbsp;</FONT>
									<asp:button id="btnExit" runat="server" Width="64px" Text="�˳�"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
