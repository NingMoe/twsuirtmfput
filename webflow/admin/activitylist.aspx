<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.ActivityList" CodeFile="ActivityList.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Import namespace="System.Data"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ActivityList</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
		<script src="../script/prototype.js" type="text/javascript"></script>
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
			<table cellpadding="1" cellspacing="1" border="0" width="100%" class="ListBox" bgcolor="#3366CC">
				<tr class="ListHeader">
					<td>流程</td>
					<td>主题</td>
					<td align="center" width="110px">所在环节名称</td>
					<td align="center" width="110px">处理人</td>
					<td align="center" width="110px">创建时间</td>
					<td align="center" width="110px">查看时间</td>
				</tr>
				<%
				if (dt is nothing) then return

                    dim endRowIndex as Integer = (PageIndex+1) * PageSize

                    if endRowIndex>dt.Rows.Count then endRowIndex = dt.Rows.Count
				
				For i As Integer = PageIndex*PageSize  To endRowIndex - 1
					dim ActivitiesName as string=""
					dim TransactorName as string =""
				    Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(DbField.GetStr(dt.Rows(i), "ID"))
					'Dim oWorkflowInstance As WorkflowInstance = WorkflowFactory.LoadInstance(oWorklistItem.ActivityInstance.WorkflowInstance.Key)
					dim dtUSERTASK as DataTable= SDbStatement.Query("select U.*,T.WF_INSTANCE_ID,T.NodeName from WF_USERTASK U join WF_TASK T on U.TASKID=T.ID where U.TASKSTATUS=" & TaskStatusConstants.Actived & " and WF_INSTANCE_ID=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key).Tables(0)
					if dtUSERTASK.Rows.Count>0 then 
						ActivitiesName=DbField.GetStr(dtUSERTASK.Rows(0), "NodeName")
						dim oEmployee as Employee=OrganizationFactory.Implementation.GetEmployee(DbField.GetStr(dtUSERTASK.Rows(0), "EMPCODE"))
						if oEmployee is nothing then  
							TransactorName=DbField.GetStr(dtUSERTASK.Rows(0), "EMPNAME")
						else 
							TransactorName=	oEmployee.Name
						end if
					end if
					'if ActivitiesName.Trim()<>"开始" then
				%>
				<tr bgcolor="White" class="ListItem">
					<td><a href="#" onclick="showWorklfowInstance('<%=DbField.GetStr(dt.Rows(i), "FLOWINSTID")%>');"><%=DbField.GetStr(dt.Rows(i), "FlowName")%></a> </td>					
					<td><a href="#" onclick="showWorklfowInstance('<%=DbField.GetStr(dt.Rows(i), "FLOWINSTID")%>');"><%=DbField.GetStr(dt.Rows(i), "MAINFIELDVALUE")%></a></td>
					<td><%=ActivitiesName %></td>
					<td><%=TransactorName %></td>
					<td><%=DbField.GetDtm(dt.Rows(i), "CreateTime").ToString("yyyy-MM-dd HH:mm") %></td>
					<td><%=DbField.GetDtm(dt.Rows(i), "ViewTime").ToString("yyyy-MM-dd HH:mm") %></td>
				</tr>
				<%Next%>
			</table>
			<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ListBox">
				<tr>
					<td align="right">
						<asp:LinkButton ID="lbtnFirstPage" runat="server" CommandArgument="first"    OnClick="LinkButton_Click">首页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnUpPage"    runat="server" CommandArgument="previous" OnClick="LinkButton_Click">上一页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnNextPage"  runat="server" CommandArgument="next"     OnClick="LinkButton_Click">下一页</asp:LinkButton>
                        <asp:LinkButton ID="lbtnLastPage"  runat="server" CommandArgument="last"     OnClick="LinkButton_Click">末页</asp:LinkButton>    
						<%= (PageIndex+1) & "/" & PageCount & "(" & dt.Rows.Count.ToString() & ")"%>&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
