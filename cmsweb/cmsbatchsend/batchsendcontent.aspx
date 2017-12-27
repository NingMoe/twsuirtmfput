<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.BatchSendContent" validateRequest="false" CodeFile="BatchSendContent.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>邮件短信内容</title>
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
						<TABLE class="table_level2" style="WIDTH: 716px" cellSpacing="0" cellPadding="0" width="716"
							border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b><asp:label id="lblTitle" runat="server">Label</asp:label></b></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 79px; HEIGHT: 20px" align="right" width="79"></TD>
								<TD style="HEIGHT: 20px" align="left"></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 79px" align="right" width="79"><FONT face="宋体"><asp:label id="lblEmailTitle" runat="server">邮件标题：</asp:label></FONT></TD>
								<TD align="left"><asp:textbox id="txtEmailTitle" runat="server" Width="616px"></asp:textbox></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 79px" vAlign="top" align="right" width="79"><FONT face="宋体"><asp:label id="lblContent" runat="server" DESIGNTIMEDRAGDROP="30">内容：</asp:label></FONT></TD>
								<TD vAlign="top"><asp:textbox id="txtConstant" runat="server" Width="616px" Height="416px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 79px; HEIGHT: 15px" vAlign="middle" align="right" width="79"><asp:label id="lblEmailType" runat="server">邮件格式：</asp:label></TD>
								<TD style="HEIGHT: 15px"><FONT face="宋体"><asp:radiobutton id="rdoText" runat="server" Checked="True" GroupName="EMAIL_FORMAT" Text="Text格式"></asp:radiobutton>&nbsp;<asp:radiobutton id="rdoHtml" runat="server" GroupName="EMAIL_FORMAT" Text="Html格式"></asp:radiobutton></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 79px; HEIGHT: 9px" vAlign="middle" align="right" width="79"><asp:label id="lblSmsPort" runat="server">短信端口：</asp:label></TD>
								<TD style="HEIGHT: 9px"><asp:textbox id="txtSmsPort" runat="server" Width="104px" MaxLength="1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 79px; HEIGHT: 9px" vAlign="middle" align="right" width="79"></TD>
								<TD style="HEIGHT: 9px"></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 79px"></TD>
								<td><asp:button id="btnConfirm" runat="server" Width="100px" Text="保存"></asp:button><asp:button id="btnCancel" runat="server" Width="80px" Text="退出"></asp:button></td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
