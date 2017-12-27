<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.processes" CodeFile="processes.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>processes</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script src="../script/loading.js" type="text/javascript"></script>
    <link href="css/document.css" rel="stylesheet" type="text/css">
  </head>
  <body>

    <form id="Form1" method="post" runat="server">
    <table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td width="10"></td>
			<td width="*">
				<table width="600" cellspacing="1" cellpadding="0">
					<%GenerateWorkflowCategories(0)%>
				</table> 		
			</td>
		</tr>
	</table>
    </form>

  </body>
</html>

