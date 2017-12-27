<%@ Control Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.CtlFlowDiagram" CodeFile="CtlFlowDiagram.ascx.vb" %>
<%@ Import Namespace="Unionsoft.Workflow.Items"%>
<%@ Import Namespace="Unionsoft.Workflow.Platform"%>
<%@ Import Namespace="Unionsoft.Workflow.Engine"%>
<script type="text/javascript">
//filter history data 
function FlowDiagram_OnClick(oSrc,nodeid)
{
	for (var i=0;i<WorkFlowHistory.rows.length;i++)
	{
		if (nodeid == WorkFlowHistory.rows[i].ItemID)
		{
			WorkFlowHistory.rows[i].style.display = "block";
		}
		else
		{
			WorkFlowHistory.rows[i].style.display = "none";
		}
	}
}
</script>
<div class="Diagram">
	<asp:Image id="Image1" useMap="#CtlFlowDiagram1_DiagramMap" Runat="Server" style="POSITION:static;"></asp:Image>
</div>
<map id="DiagramMap" runat="server"></map>

<table width="100%" bgcolor="1" cellpadding="0" cellspacing="0">
<tr>
	<td bgcolor="#C4D9F9" height="22">&nbsp;<b>������Ϣ</b></td>
</tr>
</table>

<table width="100%" bgcolor="1" cellpadding="0" cellspacing="0" id="WorkFlowHistory" bgcolor="#ffffff">
<%
If Not oWorkflowInstance Is Nothing Then
For i As Integer = 0 To oWorkflowInstance.Activities.Count - 1
%>
<%
	For j As Integer = 0 To oWorkflowInstance.Activities(i).WorklistItems.Count - 1
%>
	<tr ItemID="<%=oWorkflowInstance.Activities(i).NodeTemplate.Key%>" style="display:none">
	<td>
	<table cellSpacing=1 cellPadding=0 width="100%" border=0 
		<%
		If oWorkflowInstance.Activities(i).WorklistItems(j).Status = TaskStatusConstants.Actived Then
			Response.Write("class=""ActiveWorkflowItem""")
		Else
			Response.Write("class=""FlowHistoryTable""")
		End If
		%>
	>
		<tr>
			<td width="70" align="center">��������</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).Name%><%If oWorkflowInstance.Activities(i).WorklistItems(j).IsCc Then%>(����)<%End If%></td>
			<td width="70" align="center">��������</td>
			<td width="315">&nbsp;<%If Not oWorkflowInstance.Activities(i).WorklistItems(j).Transactor Is Nothing Then%><%=oWorkflowInstance.Activities(i).WorklistItems(j).Transactor.Name%><%Else%>��Ч����Ա<%End If%></td>
		</tr>
		<tr>
			<td width="70" align="center">����ʱ��</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).CreateTime%></td>
			<td width="70" align="center">�鿴ʱ��</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).ViewTime%></td>
		</tr>
		<tr>
			<td width="70" align="center">����ʱ��</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).DealTime%></td>
			<td width="70" align="center">������</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).Action.Name%></td>
		</tr>
		<tr>
			<td width="70" align="center">�������</td>
			<td colspan="3">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).Memo%></td>
		</tr>
	</table>
	</td>
	</tr>
<%
	Next
Next
End If
%>
</table>
