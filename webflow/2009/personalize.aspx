<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.personalize" CodeFile="personalize.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>personalize</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
    <script src="../script/loading.js" type="text/javascript"></script>
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center" style="border-bottom:1px solid #cccccc">
		<tr>
			<td height="25" width="10"></td>
			<td height="25" width="100" class="biaoti1"><b>常用流程设置</b></td>
			<td height="25" width="*" align="right"><asp:Button ID="btnUpdate" Runat="server" Text="应用设置" OnClick="btnUpdate_Click"></asp:Button></td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td width="10"></td>
			<td width="*">
				<table width="100%" cellspacing="1" cellpadding="0">
					<%GenerateWorkflowCategories(0)%>
				</table> 		
			</td>
		</tr>
	</table>
    </form>
  </body>
</HTML>
