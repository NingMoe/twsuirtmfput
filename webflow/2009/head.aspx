<%@ Reference Page="~/admin/adminprocessweekanalyse.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.head" CodeFile="head.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>head</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="../script/prototype.js"></script>
    <script type="text/javascript">
    function search()
    {
		try
		{
			//alert($("keyvalue"));
			if ($("keyvalue").value!="") top.listframe.document.location.href = "listsearchresult.aspx?key=" + escape($("keyvalue").value);
		}
		catch (e)
		{
			alert(e);
		}
		finally
		{
			return false;
		}
    }
    </script>
  </head>
  <body>
	<OBJECT id=WebAttachment height=20 width=176 classid=CLSID:7649C363-E9F5-4D35-AFE1-363762170CE6 name=WebAttachment  style="display:none" CODEBASE="UnionsoftControls.CAB#version=1,0,0,0" >
	<PARAM NAME="_ExtentX" VALUE="4657">
	<PARAM NAME="_ExtentY" VALUE="529">
	</OBJECT>
    <form id="Form1" method="post" runat="server">
	<table width="100%" border="0" cellpadding="0" cellspacing="0" class="topbg" style="background: url(images/top-bg2.jpg) repeat-x left">
		<tr>
			<td width="263" align="left" valign="top"><img src="images/t-logo.jpg" width="263" height="55"></td>
			<td align="right" valign="top" style="background: url(images/top-bg.jpg) no-repeat left">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td width="74%" height="20">&nbsp;</td>
				<td width="26%" align="right">
					<table width="140" border="0" cellspacing="0" cellpadding="0"  class="link-t1">
					<tr>
						<td align="center"><a href="/cmsweb/cmshost/CmsFrame.aspx" target="_top" class="text1" style="color:white">返回CMS首页</a></td>
						<td align="left"><a href="#" onclick="window.top.close();" class="text1" style="color:white">退出</a></td>
					</tr>
					</table>
			  </td>
			</tr>
			<tr>
				<td height="30"></td>
				<td>
					
					<table border="0" cellpadding="0" cellspacing="0" class="text1">
					<tr>
						<td width="*"></td>
						<td align="left"><a href="../UnionsoftControls.exe" target="_blank">OFFICE编辑组件</a></td>
					</tr>
					</table>
					
				</td>
			</tr>
			</table>
			</td>
		</tr>
	</table>
    </form>

  </body>
</html>
