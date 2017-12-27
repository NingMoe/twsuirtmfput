<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.list" CodeFile="list.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>list</title>
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
		for (var i=1;i<$("tbllist").rows.length;i++) $("tbllist").rows[i].bgColor="#FFFFFF"; 
		row.bgColor="#ffff00";
	}
    </script>
    <script language="vb" runat="server">
    
    </script>
  </HEAD>
  <body onresize="divlistResize();" scroll="no">
    <form id="Form1" method="post" runat="server" onkeydown="execSearch()">
	<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td width="1%" height="20" align="left" ><img src="images/bk_04.gif" width="13" height="23"></td>
			<td width="97%" align="left" background="images/bk_06.gif" class="biaoti1">
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
					<tr>
					<td width="120" align="left"><img src="images/ico-file2.gif" width="16" height="16" align="absmiddle"> <span class="biaoti1">待处理工作</span></td>
					<td width="300" align="right">
						<asp:TextBox ID="txtkeyvalue" Runat="server" Width="200" CssClass="input" AutoPostBack="false"></asp:TextBox>
						<asp:ImageButton ImageAlign="AbsMiddle" ImageUrl="images/bar_search.gif" ID="btnSearch" Runat="server" CssClass="input"></asp:ImageButton>
					</td>
					<td width="*" align="right"><asp:Label ID="lblInfo" Runat="server" Font-Size="9"></asp:Label>&nbsp;</td>
					</tr>
				</table>
			</td>
			<td width="2%" align="right" background="images/bk_06.gif"><img src="images/bk_05.gif" width="14" height="23"></td>
		</tr>
		<tr>
			<td colspan="3" valign="top">
				<div style="overflow:auto;height:170px;width:100%;vertical-align:top" id="divlist">
				<table border="0" cellpadding="0" cellspacing="1" bgcolor="#b7d1e2"  width="100%" align="center" style="table-layout:fixed;" id="tbllist">
					<tr background="images/btbg05.gif" class="Freezing" bgcolor="#b7d1e2">
						<td width="150" align="center" background="images/btbg05.gif" class="text1">流程</td>
						<td width="*" height="20" align="center" background="images/btbg05.gif" class="text1">主题</td>
						<td width="80" align="center" background="images/btbg05.gif" class="text1">来源</td>
						<td width="110" align="center" background="images/btbg05.gif" class="text1">时间</td>
						<td width="110" align="center" background="images/btbg05.gif" class="text1">查看时间</td>
					</tr>
					<asp:Repeater ID="Repeater1" Runat="server">
					<ItemTemplate>
					<tr bgcolor="#FFFFFF" class="text1" height="22" onclick="tbllistColorClear(this)">
						<td width="150"><a href="../process/director.aspx?action=transtract&WorkflowInstId=<%#DataBinder.Eval(Container.DataItem,"FLOWINSTID")%>&WorklistItemId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&url=2009/message.aspx" target="workitem"><%#DataBinder.Eval(Container.DataItem,"FlowName")%></a></td>
						<td align="left" style="overflow:hidden;text-overflow:ellipsis" onmousemove="this.title=this.innerText">&nbsp;<nobr><a href="../process/director.aspx?action=transtract&WorkflowInstId=<%#DataBinder.Eval(Container.DataItem,"FLOWINSTID")%>&WorklistItemId=<%#DataBinder.Eval(Container.DataItem,"ID")%>&url=2009/message.aspx" target="workitem"><%#DataBinder.Eval(Container.DataItem,"MAINFIELDVALUE")%></a></nobr></td>
						<td align="center" width="80"><%#DataBinder.Eval(Container.DataItem,"CREATORNAME")%></td>
						<td align="center" width="110" ><%#DataBinder.Eval(Container.DataItem,"CreateTime")%></td>
						<td align="center" width="110" ><%#DataBinder.Eval(Container.DataItem,"ViewTime")%></td>
					</tr>
					</ItemTemplate>
					</asp:Repeater>
				</table>
				</div>
			</td>
		</tr>
	</table>	
    </form>

  </body>
</HTML>
<script>
divlistResize();
</script>
