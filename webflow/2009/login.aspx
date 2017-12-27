<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.login1" CodeFile="login.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.PageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>login</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <style type="text/css">
	BODY { BACKGROUND: url(images/bg_login.jpg) #75c1ef no-repeat center center }
	.t-login { FONT-SIZE: 12px; COLOR: #006699; LINE-HEIGHT: 18px; FONT-FAMILY: Arial, Helvetica, sans-serif; TEXT-DECORATION: none }
	#warp { MARGIN-TOP: -195px; LEFT: 50%; MARGIN-LEFT: -220px; WIDTH: 440px; POSITION: absolute; TOP: 50%; HEIGHT: 390px }
	</style>
</HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
	<div id="warp"> 
	<table width="439" border="0" align="center" cellpadding="0" cellspacing="0">
	<tr>
		<td><img src="images/login_1.jpg" width="439" height="87"></td>
	</tr>
	<tr>
		<td align="right" style="BACKGROUND: url(images/login_2.jpg) no-repeat" >
		<table width="246" height="107" border="0" cellpadding="0" cellspacing="0" class="t-login">
		<tr>
			<td width="64" height="31" align="center" valign="top">&nbsp;</td>
			<td width="42" align="left" valign="bottom">用户名</td>
			<td width="140" align="left" valign="bottom"><asp:TextBox id="txtUserName" runat="server" Width="100" Height="18"></asp:TextBox></td>
		</tr>
		<tr>
			<td height="31" align="center" valign="top">&nbsp;</td>
			<td align="left" valign="middle">密　码</td>
			<td align="left" valign="middle"><asp:TextBox id="txtPassWord" runat="server" TextMode="Password" Width="100" Height="18"></asp:TextBox></td>
		</tr>
		<tr>
			<td height="45" align="center" valign="top">&nbsp;</td>
			<td align="left" valign="middle">&nbsp;</td>
			<td align="left" valign="top">
				<asp:Button id="Button1" runat="server" Text="登录" OnClick="Login"></asp:Button>
				<input name="Submit322" type="reset"  value="清空">
			</td>
		</tr>
		</table>
		</td>
	</tr>
	<tr>
		<td height="165" align="center" valign="middle" style="BACKGROUND: url(images/login_3.jpg) no-repeat">
		<table width="319" height="65" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td height="40" align="center" valign="top" class="t-login">Copyright 2001 <a href="http://www.unionsoft.cn" target="_blank">Unionsoft</a> All Rights reserved.</td>
		</tr>
		</table></td>
	</tr>
	</table>
	</div>  
    </form>

  </body>
</HTML>
