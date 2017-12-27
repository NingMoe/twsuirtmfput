<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<Html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>请假表</title>
	<script type="text/javascript" src="../scripts/DataConvert.js"></script>
	<script type="text/javascript" src="../scripts/FormValidate.js"></script>
	<script type="text/javascript" src="../scripts/DateCalendar.js"></script>
	<script type="text/javascript" src="../scripts/dragDiv.js"></script>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<!--#include file="../includes/WorkflowUtilies.aspx"-->
        <!--#include file="../includes/WorkflowAttachment.aspx"-->
<script language="vb" runat="server">
Private ActionKey As String
Private WorkflowInstId As String
Private WorklistItemId As String
Private hstFormValues As Hashtable
Private table As DataTable

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
	If Request("WorkflowInstId") <>"" Then WorkflowInstId = Request("WorkflowInstId")
	If Request("WorklistItemId") <>"" Then WorklistItemId = Request("WorklistItemId")
	
	Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(WorklistItemId)
	ViewState.Item("_Id") = oWorklistItem.ActivityInstance.WorkflowInstance.RecordId
        hstFormValues = GetFormValues(oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, "HR_Leave")

	If Not Me.IsPostBack Then Return
	If ActionKey="" Then Return  

    
	ProcessWorklistItem(oWorklistItem,ActionKey,hstFormValues,Request.Form("memo"))
 End Sub
</script>

<form id="Form1" method="post" runat="server">

<%GenerateActInstCommand(Convert.ToInt64(WorklistItemId),"")%>
<h1 align="center">请假单</h1>
<table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
  <tr>
    <td width="100" class="F_center">请假日期</td>
    <td width="250" align="left" valign="middle">&nbsp;<%=hstFormValues("Askdate") %></td>
    <td width="100" class="F_center">请假人</td>
    <td width="250" align="left" valign="middle">&nbsp;<%=hstFormValues("EmpName") %></td>
  </tr>
  <tr>
    <td class="F_center">开始日期</td>
    <td align="left" valign="middle">&nbsp;<%=hstFormValues("Begindate")%>&nbsp;&nbsp;<%=hstFormValues("StartHour").ToString().PadLeft(2, Convert.ToChar("0"))%>:<%=hstFormValues("StartMinute").ToString().PadLeft(2, Convert.ToChar ("0"))%></td>
    <td class="F_center">结束日期</td>
    <td align="left" valign="middle">&nbsp;<%=hstFormValues("Enddate")%>&nbsp;&nbsp;<%=hstFormValues("EndHour").ToString().PadLeft(2, Convert.ToChar("0"))%>:<%=hstFormValues("EndMinute").ToString().PadLeft(2, Convert.ToChar("0"))%></td>
  </tr>
  <tr>
	 <td class="F_center">请假小时数</td>
     <td align="left" valign="middle">&nbsp;<%=hstFormValues("Hours").ToString%></td>
     <td class="F_center">请假天数</td>
     <td align="left" valign="middle">&nbsp;<%=hstFormValues("LeaveDays").ToString %></td>
  </tr>
  <tr>
    <td height="32" class="F_center">假期类型</td>
    <td colspan="4" align="left" valign="middle">&nbsp;<%=hstFormValues("Leavetype") %>&nbsp;</td>
 </tr> 
 <tr>
    <td class="F_center">事由</td>
    <td height="98" colspan="4" align="left" valign="top">&nbsp;<%=hstFormValues("Leavedesc") %> </td>
 </tr>
 <tr style="height: 30px;">
    <td colspan="6"><%DisplayAttachment(Convert.ToInt64(ViewState.Item("_Id")))%> </td>
  </tr>
</table>
<center >
 <%DisplayAuditInfo(Convert.ToInt64(WorklistItemId))%>
</center>
</form>
</body>
</html>
