<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.celendar" CodeFile="celendar.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>celendar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
  </head>
  <body style="background-color:#FFFFD5">
    <form id="Form1" method="post" runat="server">
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td bgcolor="LightGrey" height="30">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="biaoti1">
				<tr>
					<td width="90">
						&nbsp;<a href="celendar.aspx?year=<%=_year-1%>"><image src="../images/arrow_left.gif" border="0" align="absmiddle"></a>
						&nbsp;&nbsp;<%=_year%>Äê
						&nbsp;<a href="celendar.aspx?year=<%=_year+1%>"><image src="../images/arrow_right.gif"  border="0" align="absmiddle"></a>&nbsp;
					</td>
					<%GenerateDateList(_year)%>
					<td width="*">&nbsp;</td>
				</tr>
			</table>
			</td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<%GenerateCelendar(_year,_month)%>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
    </form>
  </body>
</html>
