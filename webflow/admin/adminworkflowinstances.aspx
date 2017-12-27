<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Workflow.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.AdminWorkflowInstances" CodeFile="AdminWorkflowInstances.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.AdminPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>AdminActivities</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
		<script type="text/javascript" src="../script/dragDiv.js"></script>
		<script type="text/javascript" src="../script/prototype.js"></script>
		<script language="javascript">		
		function showWorklfowInstance(WorkflowId)
		{
			var url = "AdminWorkflowInstDetail.aspx?WorkflowId=" + WorkflowId;
			showMessageBox(700,450,url,"流程详细信息");
		}
		</script>
  </HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="5" width="2"></td>
		</tr>
		</table>
		<asp:DataGrid id="ActiveFlows" Width="98%" Runat="server" BorderColor="#3366CC" BorderStyle="None" HorizontalAlign="Center"
			BorderWidth="1px" BackColor="White" CellPadding="0" AutoGenerateColumns="False" CssClass="ListBox"
			PageSize="18" AllowPaging="True" DataKeyField="ID">
			<ItemStyle Height="21px" ForeColor="#003399" CssClass="ListItem" BackColor="White"></ItemStyle>
			<HeaderStyle Font-Bold="True" CssClass="ListHeader"></HeaderStyle>
			<Columns>
				<asp:TemplateColumn ItemStyle-Width="20">
					<ItemTemplate>
						<asp:CheckBox ID="chkItemID" Runat="server"></asp:CheckBox>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="ID" DataNavigateUrlFormatString="javascript:showWorklfowInstance('{0}');" DataTextField="FlowName" HeaderText="流程名称">
					<HeaderStyle HorizontalAlign="Left" Width="110px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Left" Width="110px"></ItemStyle>
				</asp:HyperLinkColumn>
				<asp:BoundColumn DataField="MAINFIELDVALUE" HeaderText="主题"></asp:BoundColumn>
				<asp:BoundColumn DataField="CreatorName" HeaderText="创建人">
					<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="CreateTime" HeaderText="创建时间">
					<HeaderStyle HorizontalAlign="Center" Width="110px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
				</asp:BoundColumn>
			</Columns>
			<PagerStyle Height="21px" HorizontalAlign="Left" CssClass="ListHeader" Mode="NumericPages" Visible="False"></PagerStyle>
		</asp:DataGrid>
				
		<table cellpadding="0" cellspacing="0" border="0" width="98%" align="center">
			<tr>
				<td width="380">
					<asp:CheckBox ID="chSelectAll" Runat="server" Text="选中所有" AutoPostBack="True"></asp:CheckBox>
					<asp:LinkButton id="lnkSearch" Runat="server"><img src="../images/query1.gif" border="0" align="absbottom">查询</asp:LinkButton>
					&nbsp;<a href="AdminActivities.aspx"><img src="../images/oOutlook.gif" border="0" align="absBottom">撤销查询</a>
				</td>
				<td><Pager:WorkflowPager id="Pager1" runat="server"></Pager:WorkflowPager></td>
			</tr>
		</table>
		</form>
	</body>
</HTML>
