<%@ Reference Page="~/admin/adminprocessweekanalyse.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web._default1" CodeFile="default.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>default</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="../css/flowstyle.css" type="text/css" rel="stylesheet">
    <!--<div>正在检查数据库需要的更新......</div>-->
    <script src="../scripts/loading.js" type="text/javascript"></script>
</HEAD>
  <body>
	<form id="Form1" method="post" runat="server">
		<table width="98%" cellpadding="0" cellspacing="0" align="center">
			<tr>
				<td height="10"></td>
			</tr>
			<tr>
				<td><b>流程部分数据库更新检查结果...</b></td>
			</tr>
			<tr>
				<td>
				<%DatabaseUpdateCheck()%>
				</td>
			</tr>
			<tr>
				<td height="20"></td>
			</tr>
			<tr>
				<td><asp:Button id=Button1 runat="server" Text="安装数据库更新" OnClick="DatabaseUpdateInstall"></asp:Button></td>
			</tr>
		</table>
	</form>
  </body>
</HTML>
