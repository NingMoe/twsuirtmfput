<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.tools" CodeFile="tools.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<%@ Import NameSpace="Unionsoft.Workflow.Web" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>tools</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
    <script src="../script/prototype.js" type="text/javascript"></script>
    <base target="listframe">
    <script>
    function updateWorklistAmount()
    {
		var WorklistAmount = Unionsoft.Workflow.Web.tools.WorklistAmount().value
		var ProxyWorklistAmount = Unionsoft.Workflow.Web.tools.ProxyWorklistAmount().value
		var DraftWorklistAmount = Unionsoft.Workflow.Web.tools.DraftWorklistAmount().value
		if (!isNaN(WorklistAmount)) $("lblWorklistAmount").innerText = WorklistAmount;
		if (!isNaN(ProxyWorklistAmount)) $("lblProxyWorklistAmount").innerText = ProxyWorklistAmount;
		if (!isNaN(DraftWorklistAmount)) $("lblDraftWorklistAmount").innerText = DraftWorklistAmount;
		window.setTimeout("updateWorklistAmount()",120000);//每2分钟跟新一次待办任务的数量
	}
    </script>
  </head>
  <body style="background-color:#FFFFFF;border-right:1px solid #b2bfc6;" onload="updateWorklistAmount()">
    <form id="Form1" method="post" runat="server">
	<table width="170" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>

	<table width="170" border="0" cellpadding="0" cellspacing="1" bgcolor="#b2bfc6" align="center">
		<tr>
			<td height="30" class="side_nav"><img src="images/ico-chaozuo.gif" width="16" height="16" border="0" align="absmiddle">日常操作</td>
		</tr>
		<tr>
			<td height="78" align="center" bgcolor="#FFFFFF">
			<table width="98%" border="0" cellspacing="4" cellpadding="0" class="text1">
				<tr>
					<td width="19%" align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td width="81%" height="20" align="left" class="link2"><a href="listframe.aspx">待办事宜</a>(<Label id="lblWorklistAmount">0</label>)</td>
				</tr>
				<tr>
					<td width="19%" align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td width="81%" height="20" align="left" class="link2"><a href="listframe.aspx?url=proxyworklist.aspx">代理事宜</a>(<Label id="lblProxyWorklistAmount">0</label>)</td>
				</tr>
				<tr>
					<td align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td height="20" align="left" class="link2"><a href="listframe.aspx?url=listassociate.aspx">已办事宜</a></td>
				</tr>
				<tr>
					<td align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td height="20" align="left" class="link2"><a href="listframe.aspx?url=liststart.aspx">已发事宜</a></td>
				</tr>
				<tr>
					<td align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td height="20" align="left" class="link2"><a href="listframe.aspx?url=listdraft.aspx">草稿箱</a>(<Label id="lblDraftWorklistAmount">0</label>)</td>
				</tr>
				<!--
				<tr>
					<td align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td height="20" align="left" class="link2"><a href="listframe.aspx?url=listfavorite.aspx">收藏夹</a></td>
				</tr>
				-->
			</table>
			</td>
		</tr>
		<tr>
			<td height="30" class="side_nav"><img src="images/ico-2.gif" width="16" height="16" border="0" align="absmiddle"> 常用流程</td>
		</tr>
		<tr>
			<td height="30" bgcolor="#FFFFFF" >
			<table width="98%" border="0" cellspacing="4" cellpadding="0">
				<%
				For Each item As DictionaryEntry In oWorkflowShortcuts.Shortcuts
				        If IsPermissionWorkflowItem(item.Key.ToString) Then
				%>
				<tr>
					<td width="19%" align="center"><img src="images/org_arrow.gif" width="4" height="5"></td>
					<td width="81%" height="20" align="left" class="link2"><a href='../process/director.aspx?action=create&WorkflowId=<%=item.Key%>'><%=item.Value%></a></td>
				</tr>
				<%	
					End If
				Next
				%>
			</table>
			</td>
		</tr>	
		<tr>
			<td height="30" class="side_nav"><a href="processes.aspx"><img src="images/ico-links.gif" width="16" height="16" border="0" align="absmiddle"> 更多流程</a></td>
		</tr>
		<tr>
			<td height="30" class="side_nav"><a href="celendar.aspx"><img src="images/ico-rili.gif" width="16" height="16" border="0" align="absmiddle"> 事务日历</a></td>
		</tr>
		<tr>
			<td height="30" class="side_nav"><a href="personalize.aspx"><img src="images/ico-renwu.gif" width="16" height="16" border="0" align="absmiddle"> 个人设置</a></td>
		</tr>
		<tr>
			<td height="30" class="side_nav"><a href="proxymain.aspx"><img src="../images/department.gif" width="16" height="16" border="0" align="absmiddle">事务代理设置</a></td>
		</tr>
	</table>
    </form>
  </body>
</html>
