<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.AdminWorkflowList" CodeFile="AdminWorkflowList.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.AdminPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminFlowList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
		<script type="text/javascript" src="../script/dragDiv.js"></script>
		<script type="text/javascript" src="../script/prototype.js"></script>
		<script language="javascript">		
		function showEditorWindow(WorkflowId)
		{
			var url = "AdminEditPermissoin.aspx?WorkflowId=" + WorkflowId;
			showMessageBox(650,450,url,"权限设置");
			return false;
		}
		</script>
	</HEAD>
	<body>
	<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
		<tr>
			<td height="5" width="2"></td>
		</tr>
		<tr>
			<td>
				<table border="1" cellpadding="0" cellspacing="0" class="ListBox">
					<tr class="ListHeader">
						<td width="180" align="center">流程名称</td>
						<td width="80" align="center">活动流程</td>
						<td width="80" align="center">挂起流程</td>
						<td width="80" align="center">结束流程</td>
						<td width="80" align="center">流程监控</td>
						<td></td>
					</tr>
					<asp:Repeater id="FlowRepeater" runat="server">
						<ItemTemplate>
							<tr class="ListItem">
								<td>
									<img src="../images/flow.ico" align="absmiddle"><%#DataBinder.Eval(Container.DataItem,"NAME")%>
								</td>
								<TD align="center"><asp:HyperLink ID="lnkActivityAmount" Runat="server"></asp:HyperLink></TD>
								<td align="center"><asp:HyperLink ID="lnkPauseAmount" Runat="server"></asp:HyperLink></td>
								<td align="center"><asp:HyperLink ID="lnkFinishAmount" Runat="server"></asp:HyperLink></td>
								<td align="center"><asp:HyperLink ID="lnkAduitAmount" Runat="server"></asp:HyperLink></td>
								<td width="60" align="center"><a href="javascript:showEditorWindow('<%#DataBinder.Eval(Container.DataItem,"ID")%>');void(0);">设置权限</a></td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
			</td>
		</tr>
	</table>
	
	</body>
</HTML>

