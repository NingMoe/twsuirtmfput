<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.proxyworklist" CodeFile="proxyworklist.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Workflow.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>proxyworklist</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/document.css" rel="stylesheet" type="text/css">
    <script src="../script/prototype.js" type="text/javascript"></script>
    <script>
	function divlistResize()
	{
		$("divlist").style.height=document.body.clientHeight-25;
	}
	
	function execSearch()
	{
		if (event.keyCode==13) {$("btnSearch").click();return false;}
	}
	
	function tbllistColorClear(row)
	{
		for (var i=1;i<$("DataGrid1").rows.length;i++) $("DataGrid1").rows[i].style.backgroundColor="#FFFFFF"; 
		row.style.backgroundColor="#ffff00";
	}
    </script>
  </HEAD>
  <body onresize="divlistResize();" scroll="no">
    <form id="form1" method="post" runat="server">
	<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
		<td width="1%" height="20" align="left" ><img src="images/bk_04.gif" width="13" height="23"></td>
		<td width="97%" align="left" background="images/bk_06.gif" class="biaoti1">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
			<td width="120" align="left"><img src="images/ico-file2.gif" width="16" height="16" align="absmiddle"> <span class="biaoti1">待处理工作</span></td>
			<td width="300" align="right">
				<asp:TextBox ID="txtkeyvalue" Runat="server" Width="200" CssClass="input"></asp:TextBox>
				<asp:ImageButton ImageAlign="AbsMiddle" ImageUrl="images/bar_search.gif" ID="btnSearch" Runat="server" CssClass="input"></asp:ImageButton>
			</td>
			<td width="*" align="right"><Pager:WorkflowPager id="Pager1" runat="server" CssClass="text1"></Pager:WorkflowPager></td>
			</tr>
		</table>
		</td>
		<td width="2%" align="right" background="images/bk_06.gif"><img src="images/bk_05.gif" width="14" height="23"></td>
		</tr>
		<tr>
			<td colspan="3">
				<div style="overflow:auto;height:170px;width:100%" id="divlist">
				<asp:DataGrid ID="DataGrid1" Runat="server" Width="100%" PageSize="15" AllowPaging="True" DataKeyField="ID" AutoGenerateColumns="False" HorizontalAlign="Center" CssClass="text1" CellPadding="0" CellSpacing="1" BorderWidth="0" BackColor="#b7d1e2">
					<Columns>
						<asp:TemplateColumn ItemStyle-Width="180" HeaderStyle-Width="180" HeaderText="流程名称" SortExpression="FLOWNAME">
							<ItemTemplate>
								&nbsp;<a href="../process/director.aspx?action=transtract&WorklistItemId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&url=2009/message.aspx" target="workitem"><%#DataBinder.Eval(Container.DataItem,"FLOWNAME")%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="主题" SortExpression="MAINFIELDVALUE">
							<ItemTemplate>
								&nbsp;<a href="../process/director.aspx?action=transtract&WorklistItemId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&url=2009/message.aspx" target="workitem"><%#DataBinder.Eval(Container.DataItem,"MAINFIELDVALUE")%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="EMPNAME" HeaderText="原任务人" SortExpression="EMPNAME">
							<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="CREATORNAME" HeaderText="发送人" SortExpression="CREATORNAME">
							<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="CreateTime" HeaderText="时间" SortExpression="CreateTime" DataFormatString="{0:yy-M-d  HH:m}">
							<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="ViewTime" HeaderText="查看时间" SortExpression="ViewTime" DataFormatString="{0:yy-M-d  HH:m}">
							<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Left" Height="21px" CssClass="ListHeader" Visible="False" Mode="NumericPages"></PagerStyle>
					<HeaderStyle CssClass="Freezing" BackColor="#b7d1e2"></HeaderStyle>
					<ItemStyle BackColor="#ffffff"></ItemStyle>
				</asp:DataGrid>
				</div>
			</td>
		</tr>
	</table>	
    </form>
  </body>
</HTML>
