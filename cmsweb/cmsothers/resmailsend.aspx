<%@ Page Language="vb" validateRequest="false" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResMailSend" CodeFile="ResMailSend.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ResMailSend</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="Panel1" runat="server">
				<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="header_level2"><FONT face="����"><STRONG>�ʼ�Ⱥ���������:</STRONG></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="����"><FONT face="����"><FONT color="#ff3333">�� �� ��:</FONT>
									<asp:textbox id="txtSmtpServer" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp; 
										��&nbsp; ��:</FONT>
									<asp:TextBox id="txtNumber" runat="server" Width="48px"></asp:TextBox><FONT color="#ff3333">�̼߳��
									</FONT>
									<asp:TextBox id="txtInterval" runat="server" Width="48px"></asp:TextBox></FONT></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="����"><FONT face="����"><FONT color="#ff3333">�� �� ��:</FONT>
									<asp:textbox id="txtUser" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp;
									</FONT><FONT face="����"><FONT color="#ff3333">��&nbsp; ��: </FONT>
										<asp:textbox id="txtPass" runat="server"></asp:textbox></FONT></FONT></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="����"><FONT color="#ff3333">��������:</FONT>
								<asp:textbox id="txtFrom" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp; 
									������:</FONT>
								<asp:textbox id="txtFromName" runat="server"></asp:textbox></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="����"><FONT color="#ff3333">�ظ�ַַ:</FONT>
								<asp:textbox id="txtReplyTo" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp; 
									��־��:</FONT>
								<asp:TextBox id="txtLogTable" runat="server"></asp:TextBox></FONT></TD>
					</TR>
					<TR>
						<TD>
							<asp:button id="bntSave" runat="server" Text="����"></asp:button></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:Panel>
			<!-----�ؼ����滻-------->
			<asp:Panel id="Panel2" runat="server">
				<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="header_level2" colSpan="2"><FONT face="����"><STRONG>�ؼ��ʶ���:</STRONG></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="����" color="#ff0000">�ؼ���1</FONT></TD>
						<TD>
							<asp:TextBox id="txtKey1" runat="server">{KEY1}</asp:TextBox>=
							<asp:DropDownList id="drpKey1" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="����"><FONT face="����" color="#ff3300">�ؼ���2</FONT></FONT></TD>
						<TD>
							<asp:TextBox id="txtKey2" runat="server">{KEY2}</asp:TextBox>=
							<asp:DropDownList id="drpKey2" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="����" color="#ff3300">�ؼ���3</FONT></TD>
						<TD>
							<asp:TextBox id="txtKey3" runat="server">{KEY3}</asp:TextBox>=
							<asp:DropDownList id="drpKey3" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px; HEIGHT: 17px"><FONT face="����" color="#ff3300">�ؼ���4</FONT></TD>
						<TD style="HEIGHT: 17px">
							<asp:TextBox id="txtKey4" runat="server">{KEY4}</asp:TextBox>=
							<asp:DropDownList id="drpKey4" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="����" color="#ff6600">�ؼ���5</FONT></TD>
						<TD>
							<asp:TextBox id="txtKey5" runat="server">{KEY5}</asp:TextBox>=
							<asp:DropDownList id="drpKey5" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<br>
			<table width="100%" class="table_level2" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td class="header_level2" colSpan="2"><STRONG>�ʼ���Ϣ��
							<asp:CheckBox id="chkShowConfig" runat="server" Text="�Ƿ���ʾȺ���������" ForeColor="Red" AutoPostBack="True" Visible="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
							<asp:CheckBox id="chkIsKey" runat="server" Text="�Ƿ����ùؼ����滻" AutoPostBack="True"></asp:CheckBox></STRONG></td>
				</tr>
				<tr style="display:none">
					<td style="WIDTH: 132px; HEIGHT: 10px"><FONT face="����">�ռ��������ֶ�:</FONT></td>
					<td style="HEIGHT: 10px"><asp:dropdownlist id="drpRecipient" runat="server" Width="152px"></asp:dropdownlist><FONT face="����">&nbsp; 
							�ռ�������: </FONT>
						<asp:dropdownlist id="drpRecipientName" runat="server" Width="152px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 132px"><FONT face="����">�ʼ����⣺</FONT></td>
					<td><asp:textbox id="txtSubject" runat="server" Width="520px"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 132px; HEIGHT: 17px"><FONT face="����">�ʼ�����</FONT></td>
					<td style="HEIGHT: 17px"><FONT face="����">
							<asp:DropDownList id="drpCharset" runat="server" Width="88px">
								<asp:ListItem Value="GB2312">GB2312</asp:ListItem>
								<asp:ListItem Value="UTF8">UTF8</asp:ListItem>
							</asp:DropDownList>�ʼ�����
							<asp:dropdownlist id="drpBodyType" runat="server">
								<asp:ListItem Value="2">HTML</asp:ListItem>
								<asp:ListItem Value="1">TEXT</asp:ListItem>
							</asp:dropdownlist></FONT></td>
				</tr>
				<tr>
					<td style="WIDTH: 132px"><FONT face="����">�ʼ�����:</FONT></td>
					<td>
						<IFRAME ID="eWebEditor1" SRC="../ewebeditor/eWebEditor.asp?id=txtBody&amp;style=s_blue1"
							FRAMEBORDER="0" SCROLLING="no" WIDTH="550" HEIGHT="450"></IFRAME>
						<asp:textbox id="txtBody" runat="server" Width="0px" Height="0px" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:button id="bntSend" runat="server" Text="�����ʼ�"></asp:button>&nbsp;<INPUT type="button" onclick="history.go(-1);" value="�˳�" style="WIDTH: 80px; HEIGHT: 24px"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
