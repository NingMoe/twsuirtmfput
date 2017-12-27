<%@ Control Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.CtlFlowHistory" CodeFile="CtlFlowHistory.ascx.vb" %>
<%@ Import Namespace="Unionsoft.Workflow.Items"%>
<%@ Import Namespace="Unionsoft.Workflow.Platform"%>
<%@ Import Namespace="Unionsoft.Workflow.Engine"%>
<%
If Not oWorkflowInstance Is Nothing Then
For i As Integer = 0 To oWorkflowInstance.Activities.Count - 1
	For j As Integer = 0 To oWorkflowInstance.Activities(i).WorklistItems.Count - 1
%>
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
			<td width="70" align="center">环节名称</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).Name%><%If oWorkflowInstance.Activities(i).WorklistItems(j).IsCc Then%>(抄送)<%End If%></td>
			<td width="70" align="center">任务处理人</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).Transactor.Name%></td>
		</tr>
		<tr>
	<%--		<td width="70" align="center">到达时间</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).CreateTime%></td>
			<td width="70" align="center">查看时间</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).ViewTime%></td>
		</tr>--%>
		<tr>
			<td width="70" align="center">处理结果</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).Action.Name%></td>
			<td width="70" align="center">处理意见</td>
			<td width="315">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).Memo%></td>
		</tr>
<%--		<tr>
			<td width="70" align="center">处理意见</td>
			<td colspan="3">&nbsp;<%=oWorkflowInstance.Activities(i).WorklistItems(j).Memo%></td>
		</tr>--%>
	</table>
	<table cellSpacing=0 cellPadding=0 border=0>
		<tr><td height="5"></td></tr>
	</table>
<%
	Next
Next
End If
%>
