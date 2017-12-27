<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.Default1" CodeFile="Default.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>Unionsoft 企业管理应用平台</TITLE>
		<meta http-equiv="Pragma" content="no-cache">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
			<style type="text/css">
		BODY { MARGIN: 0px; BACKGROUND-COLOR: #425d89 }
		.copyright { FONT-SIZE: 10px; COLOR: #6e83a8; LINE-HEIGHT: 20px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }
		</style>
	</HEAD>
	<BODY>
		<form name="Form1" method="post" runat="server" ID="Form1">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="29%" height="84">&nbsp;</td>
					<td width="38%">&nbsp;</td>
					<td width="33%">&nbsp;</td>
				</tr>
				<tr>
					<td height="154">&nbsp;</td>
					<td valign="top">
						<table width="519" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="129" height="31">
									<img src="images/logo_login.gif" width="95" height="53" alt="">
								</td>
								<td width="390" align="right" valign="bottom" class="white">领 航 中 国 管 理 软 
									件&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<tr align="right" valign="middle">
								<td height="191" colspan="2" background="images/login.jpg">
									<table width="230" height="176" border="0" align="right" cellpadding="0" cellspacing="0">
										<tr>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td height="78" valign="top">
												<table width="100%" height="47" border="0" cellpadding="0" cellspacing="2">
													<tr>
														<td height="13" align="right" class="words">用户名:</td>
														<td>
															<asp:TextBox id="txtUserName" runat="server" Width="124px" Height="20px"></asp:TextBox>
														</td>
													</tr>
													<tr>
														<td align="right" class="words">密&nbsp;&nbsp;&nbsp;&nbsp;码:</td>
														<td>
															<asp:TextBox id="txtPassword" runat="server" Width="124px" TextMode="Password" Height="20px"></asp:TextBox></td>
													</tr>
													<tr>
													   <td></td><td><asp:LinkButton ID="lbtnCreateDb" Runat="server" ForeColor="red">请创建数据库</asp:LinkButton></td>
													</tr>
													<tr>
														<td align="right">&nbsp;</td>
														<td><asp:ImageButton ImageUrl="images/ok.gif" ID="btnLogin" Runat="server" OnClick="btnLogin_Click"></asp:ImageButton></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan="2" class="copyright">
									<asp:Label id="lblCopyright" runat="server"></asp:Label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
