<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.listfavorite" CodeFile="listfavorite.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Workflow.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>listfavorite</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
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
		<td width="1%" height="23" align="left" background="images/bk_06.gif"><img src="images/bk_04.gif" width="13" height="33"></td>
		<td width="97%" align="left" background="images/bk_06.gif" class="biaoti1">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
			<td width="91%" align="left"><img src="images/ico-file2.gif" width="16" height="16"> <span class="biaoti1">已发事宜</span></td>
			<td width="4%" align="right"></td>
			<td width="5%" align="right"><img src="images/b-list.gif" alt="列表排列" width="16" height="16" border="0"></td>
			</tr>
		</table>
		</td>
		<td width="2%" align="right" background="images/bk_06.gif"><img src="images/bk_05.gif" width="14" height="33"></td>
		</tr>
	</table>
	<asp:DataGrid ID="DataGrid1" Runat="server" Width="98%" PageSize="15" AllowPaging="True" DataKeyField="WORKFLOWID" AutoGenerateColumns="False" HorizontalAlign="Center" CssClass="text1">
		<Columns>
			<asp:BoundColumn DataField="FLOWNAME" HeaderText="流程名称" SortExpression="FLOWNAME">
				<HeaderStyle HorizontalAlign="Left" Width="110px"></HeaderStyle>
			</asp:BoundColumn>
			<asp:TemplateColumn HeaderText="主题" SortExpression="MAINFIELDVALUE">
				<ItemTemplate>
					&nbsp;<a href="../ViewWorkflow.aspx?UserTaskID=<%#DataBinder.Eval(Container.DataItem,"USERTASKID")%>" target="workitem"><%#DataBinder.Eval(Container.DataItem,"MAINFIELDVALUE")%></a>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="DEALTIME" HeaderText="时间" SortExpression="DEALTIME" DataFormatString="{0:yy-M-d  HH:m}">
				<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
			</asp:BoundColumn>
		</Columns>
		<PagerStyle HorizontalAlign="Left" Height="21px" CssClass="ListHeader" Visible="False" Mode="NumericPages"></PagerStyle>
	</asp:DataGrid>
	<table cellpadding="0" cellspacing="0" border="0" width="98%" align="center">
		<tr class="text1">
			<td width="300">
				&nbsp;<asp:CheckBox ID="chSelectAll" Runat="server" Text="选中所有" AutoPostBack="True"></asp:CheckBox>
				&nbsp;<asp:LinkButton id=lnkDelete Runat="server"><img src="../images/del.gif" border="0" align="absbottom">删除</asp:LinkButton>
				&nbsp;<a href="SendedFilesQuery.aspx"><img src="../images/query1.gif" border="0" align="absBottom"> 查询</a>
			</td>
			<td><Pager:WorkflowPager id="Pager1" runat="server" CssClass="text1"></Pager:WorkflowPager>	</td>
		</tr>
	</table>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
    </form>

  </body>
</html>
