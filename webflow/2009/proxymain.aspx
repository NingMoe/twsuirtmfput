<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.proxymain" CodeFile="proxymain.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>proxymain</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="../script/dragDiv.js"></script>
	<script type="text/javascript" src="../script/prototype.js"></script>
	<script>
	function showProxyWindow(WorkflowId)
	{
		var url = "proxyselector.aspx";
		showMessageBox(650,450,url,"代理人设置");
		return false;
	}
	</script>
</HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center" style="BORDER-BOTTOM:#cccccc 1px solid">
		<tr>
			<td height="25" width="10"></td>
			<td height="25" width="100" class="biaoti1"><b>业务代理人设置</b></td>
			<td height="25" width="*">&nbsp;</td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<table width="98%" align="center" border="0" cellpadding="0" cellspacing="0" class="text1">
		<asp:Repeater ID="Repeater1" Runat="server">
			<ItemTemplate>
		<tr height="22">
			<td width="10"></td>
			<td width="100">代理人</td>
			<td width="200"><%#DataBinder.Eval(Container.DataItem,"ProxyName")%>(<%#DataBinder.Eval(Container.DataItem,"ProxyCode")%>)</td>
			<td width="*">&nbsp;<asp:LinkButton ID="lnkDelete" Runat="server" CommandName="delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ProxyCode")%>' ForeColor="#3300ff">删除</asp:LinkButton> </td>
		</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td width="10"></td>
			<td colspan="3">
				<input type="button" onclick="showProxyWindow()" value="设置代理人">
				<asp:Button id="btnClearSettings" runat="server" Text="清除设置" OnClick="btnClearSettings_Click"></asp:Button>
			</td>
		</tr>
	</table>
    </form>
  </body>
</HTML>
