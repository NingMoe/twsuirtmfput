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
						<TD class="header_level2"><FONT face="宋体"><STRONG>邮件群发组件配置:</STRONG></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="宋体"><FONT face="宋体"><FONT color="#ff3333">服 务 器:</FONT>
									<asp:textbox id="txtSmtpServer" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp; 
										线&nbsp; 程:</FONT>
									<asp:TextBox id="txtNumber" runat="server" Width="48px"></asp:TextBox><FONT color="#ff3333">线程间隔
									</FONT>
									<asp:TextBox id="txtInterval" runat="server" Width="48px"></asp:TextBox></FONT></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="宋体"><FONT face="宋体"><FONT color="#ff3333">用 户 名:</FONT>
									<asp:textbox id="txtUser" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp;
									</FONT><FONT face="宋体"><FONT color="#ff3333">密&nbsp; 码: </FONT>
										<asp:textbox id="txtPass" runat="server"></asp:textbox></FONT></FONT></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="宋体"><FONT color="#ff3333">发件信箱:</FONT>
								<asp:textbox id="txtFrom" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp; 
									发件人:</FONT>
								<asp:textbox id="txtFromName" runat="server"></asp:textbox></FONT></TD>
					</TR>
					<TR>
						<TD><FONT face="宋体"><FONT color="#ff3333">回复址址:</FONT>
								<asp:textbox id="txtReplyTo" runat="server"></asp:textbox><FONT color="#ff3333">&nbsp;&nbsp; 
									日志表:</FONT>
								<asp:TextBox id="txtLogTable" runat="server"></asp:TextBox></FONT></TD>
					</TR>
					<TR>
						<TD>
							<asp:button id="bntSave" runat="server" Text="保存"></asp:button></TD>
					</TR>
				</TABLE>
				<BR>
			</asp:Panel>
			<!-----关键词替换-------->
			<asp:Panel id="Panel2" runat="server">
				<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="header_level2" colSpan="2"><FONT face="宋体"><STRONG>关键词定义:</STRONG></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="宋体" color="#ff0000">关键词1</FONT></TD>
						<TD>
							<asp:TextBox id="txtKey1" runat="server">{KEY1}</asp:TextBox>=
							<asp:DropDownList id="drpKey1" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="宋体"><FONT face="宋体" color="#ff3300">关键词2</FONT></FONT></TD>
						<TD>
							<asp:TextBox id="txtKey2" runat="server">{KEY2}</asp:TextBox>=
							<asp:DropDownList id="drpKey2" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="宋体" color="#ff3300">关键词3</FONT></TD>
						<TD>
							<asp:TextBox id="txtKey3" runat="server">{KEY3}</asp:TextBox>=
							<asp:DropDownList id="drpKey3" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px; HEIGHT: 17px"><FONT face="宋体" color="#ff3300">关键词4</FONT></TD>
						<TD style="HEIGHT: 17px">
							<asp:TextBox id="txtKey4" runat="server">{KEY4}</asp:TextBox>=
							<asp:DropDownList id="drpKey4" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 77px"><FONT face="宋体" color="#ff6600">关键词5</FONT></TD>
						<TD>
							<asp:TextBox id="txtKey5" runat="server">{KEY5}</asp:TextBox>=
							<asp:DropDownList id="drpKey5" runat="server" Width="120px"></asp:DropDownList></TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<br>
			<table width="100%" class="table_level2" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td class="header_level2" colSpan="2"><STRONG>邮件信息：
							<asp:CheckBox id="chkShowConfig" runat="server" Text="是否显示群发组件配制" ForeColor="Red" AutoPostBack="True" Visible="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
							<asp:CheckBox id="chkIsKey" runat="server" Text="是否启用关键词替换" AutoPostBack="True"></asp:CheckBox></STRONG></td>
				</tr>
				<tr style="display:none">
					<td style="WIDTH: 132px; HEIGHT: 10px"><FONT face="宋体">收件人邮箱字段:</FONT></td>
					<td style="HEIGHT: 10px"><asp:dropdownlist id="drpRecipient" runat="server" Width="152px"></asp:dropdownlist><FONT face="宋体">&nbsp; 
							收件人姓名: </FONT>
						<asp:dropdownlist id="drpRecipientName" runat="server" Width="152px"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="WIDTH: 132px"><FONT face="宋体">邮件主题：</FONT></td>
					<td><asp:textbox id="txtSubject" runat="server" Width="520px"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 132px; HEIGHT: 17px"><FONT face="宋体">邮件编码</FONT></td>
					<td style="HEIGHT: 17px"><FONT face="宋体">
							<asp:DropDownList id="drpCharset" runat="server" Width="88px">
								<asp:ListItem Value="GB2312">GB2312</asp:ListItem>
								<asp:ListItem Value="UTF8">UTF8</asp:ListItem>
							</asp:DropDownList>邮件类型
							<asp:dropdownlist id="drpBodyType" runat="server">
								<asp:ListItem Value="2">HTML</asp:ListItem>
								<asp:ListItem Value="1">TEXT</asp:ListItem>
							</asp:dropdownlist></FONT></td>
				</tr>
				<tr>
					<td style="WIDTH: 132px"><FONT face="宋体">邮件内容:</FONT></td>
					<td>
						<IFRAME ID="eWebEditor1" SRC="../ewebeditor/eWebEditor.asp?id=txtBody&amp;style=s_blue1"
							FRAMEBORDER="0" SCROLLING="no" WIDTH="550" HEIGHT="450"></IFRAME>
						<asp:textbox id="txtBody" runat="server" Width="0px" Height="0px" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:button id="bntSend" runat="server" Text="发送邮件"></asp:button>&nbsp;<INPUT type="button" onclick="history.go(-1);" value="退出" style="WIDTH: 80px; HEIGHT: 24px"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
