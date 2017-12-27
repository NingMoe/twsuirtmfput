<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.Migrated_PrintWorkflow" CodeFile="PrintWorkflow.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.RecordEditBase" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="NetReusables"%>
<%@ Import Namespace="Unionsoft.Workflow.Items"%>
<%@ Import Namespace="Unionsoft.Workflow.Platform"%>
<%@ Import Namespace="Unionsoft.Workflow.Engine"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>流程打印(已打印<%=lngPrintTimes%>次)</title>
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="css/flowstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
	<script language="vb" runat="server">
	Function HasSignPicture(ByVal UserCode As String) As Boolean
		Dim strPicture As Object
		Dim strSql As String = "SELECT SIGNPICTURE FROM CMS_EMPLOYEE WHERE EMP_ID='" & UserCode & "'"
		Dim dt As DataTable = SDBStatement.Query(strSql).Tables(0)
		If Not dt.Rows.Count<=0 Then
			If Not DBField.GetObj(dt.Rows(0),"SIGNPICTURE") Is Nothing Then
				Return True
			End If
		End IF
		Return False
	End Function
	</script>
	
    <form id="Form1" method="post" runat="server">
	<table class="UserForm1" cellSpacing="0" cellPadding="0" border="0">
		<tr>
			<td vAlign="top" class="UserForm1" colspan="4">
				<asp:panel id="pnlPrintWorkflow" style="LEFT: 12px; POSITION: absolute; TOP: 43px" runat="server" Height="400" BorderWidth="0"></asp:panel>
			</td>
		</tr>
		<tr>
			<td height="15" colspan="4"></td>
		</tr>
<tr>
			<td colspan="4">
<%
if oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowId<>309025511734 then 
    For i As Integer = 0 To oWorklistItem.ActivityInstance.WorkflowInstance.Activities.Count - 1
       ' Response.Write("<script> alert('" + oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).NodeTemplate.NodeKey + "')</script>")
         
        '  If oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).NodeTemplate.IsPrint Then
        For j As Integer = 0 To oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems.Count - 1
%>
<table cellSpacing="0" cellPadding="0" border="0" style="border:1px solid #000000">
		<tr>
			<td width="70" align="center">环节名称</td>
			<td width="315">&nbsp;<%=oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).Name%><%If oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).IsCc Then%>(抄送)<%End If%></td>
			<td width="70" align="center">任务处理人</td>
			<td width="315">&nbsp;<%'=oWorkflowInstance.Activities(i).WorklistItems(j).Transactor.Name%>
				<%If HasSignPicture(oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).Transactor.Code)=True Then%>
				<img src="SignPicture.aspx?UserCode=<%=oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).Transactor.Code%>">
				<%Else%>
				<%=oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).Transactor.Name%>
				<%End If%>
			</td>
		</tr>
		<tr>
			<td width="70" align="center">处理时间</td>
			<td width="315">&nbsp;<%=oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).DealTime%></td>
			<td width="70" align="center">处理结果</td>
			<td width="315">&nbsp;<%=oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).Action.Name%></td>
		</tr>
		<tr>
			<td width="70" align="center">处理意见</td>
			<td colspan="3">&nbsp;<%=oWorklistItem.ActivityInstance.WorkflowInstance.Activities(i).WorklistItems(j).Memo%></td>
		</tr>
		<tr>
			<td height="15" colspan="4"></td>
		</tr>
</table>		
<%
	Next
'End If
Next
end if
%>	


			</td>
		</tr>		
	</table>

	<table cellSpacing="0" cellPadding="0" border="0" style="width:700px">
		<tr>
			<td>
				<br>
				&nbsp;&nbsp;打印时间:<%=Now()%>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				已打印<%=lngPrintTimes%>次
			</td>
		</tr>
	</table>
    </form>
  </body>
</HTML>
