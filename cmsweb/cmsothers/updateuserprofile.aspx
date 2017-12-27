<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.UpdateUserProfile" validateRequest="false" CodeFile="UpdateUserProfile.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE id="onetidTitle">修改用户信息</TITLE>
		<meta http-equiv="Pragma" content="no-cache">
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
			<SCRIPT language="JavaScript" src="/cmsweb/script/Valid.js"></SCRIPT>
			<SCRIPT language="JavaScript" src="/cmsweb/script/CmsScript.js"></SCRIPT>
	</HEAD>
	<BODY>
		<FORM id="Form1" action="" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" height="176" cellSpacing="0" cellPadding="0" width="750" border="0">
							<TR>
								<TD class="header_level2" width="600" colSpan="2" height="22"><b>
										<asp:Label id="Label11" runat="server">更新用户基本信息 Update User Profile</asp:Label></b></TD>
							</TR>
							<TR>
								<td width="230" height="4"></td>
								<TD width="520" height="4"></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label2" runat="server">用户帐号(User Account)：</asp:label></TD>
								<TD width="520"><asp:textbox id="txtEmpID" runat="server" Width="200px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label3" runat="server">用户姓名(User Name)：</asp:label></TD>
								<TD width="520"><asp:textbox id="txtEmpName" runat="server" Width="200px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR height="22">
								<td align="right" width="230"><asp:label id="Label4" runat="server">手机(Mobile Phone)：</asp:label></td>
								<TD width="520"><asp:textbox id="txtEmpHandphone" runat="server" Width="200px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label5" runat="server">发送方电子邮件(Sender Email)：</asp:label></TD>
								<TD width="520"><asp:textbox id="txtEmpSender" runat="server" Width="200px"></asp:textbox>
									<asp:Label id="Label9" runat="server">格式Format:&nbsp;&nbsp;&nbsp;Jane&lt;zhangsan@test.com&gt;</asp:Label></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label1" runat="server">SMTP服务器(SMTP Server)：</asp:label></TD>
								<TD width="520"><asp:textbox id="txtSmtpServer" runat="server" Width="200px"></asp:textbox>
									<asp:Label id="Label10" runat="server">格式Format:&nbsp;&nbsp;&nbsp;smtp.test.com</asp:Label></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label6" runat="server">SMTP校验帐号(SMTP Sender Account)：</asp:label></TD>
								<TD width="520"><asp:textbox id="txtSMTPAccount" runat="server" Width="200px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label7" runat="server">SMTP校验密码(SMTP Sender Pass)：</asp:label></TD>
								<TD width="520"><asp:textbox id="txtSMTPPass" runat="server" Width="200px" TextMode="Password"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="230" height="15"><asp:label id="lblLanguage" runat="server">语言(Language)：</asp:label></TD>
								<TD width="520" height="15"><asp:dropdownlist id="ddlLanguage" runat="server" Width="200px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="right" width="230"><asp:label id="Label8" runat="server">登录页面(Login Page)：</asp:label></TD>
								<TD width="520"><asp:dropdownlist id="ddlLoginPage" runat="server" Width="200px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<td width="230" height="4"></td>
								<TD width="520" height="4"></TD>
							</TR>
							<TR height="22">
								<td width="230"></td>
								<TD width="520"><asp:button id="btnConfirm" runat="server" Width="112px" Text="保存设置(Save)"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
