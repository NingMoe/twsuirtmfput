<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>请假表</title>
	<script type="text/javascript" src="../scripts/DataConvert.js"></script>
	<script type="text/javascript" src="../scripts/FormValidate.js"></script>
	<script type="text/javascript" src="../scripts/DateCalendar.js"></script>
	<script type="text/javascript" src="../scripts/dragDiv.js"></script>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>

<!--#include file="../includes/WorkflowUtilies.aspx"-->

<script language="vb" runat="server">
Private ActionKey As String
Private WorkflowInstId As String
Private WorklistItemId As String
Private hstFormValues As Hashtable

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
	If Request("WorkflowInstId") <>"" Then WorkflowInstId = Request("WorkflowInstId")
	If Request("WorklistItemId") <>"" Then WorklistItemId = Request("WorklistItemId")
	
	Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(WorklistItemId)
	ViewState.Item("_Id") = oWorklistItem.ActivityInstance.WorkflowInstance.RecordId
	hstFormValues = GetFormValues(oWorklistItem.ActivityInstance.WorkflowInstance.RecordId,"WORKFLOW_FORM_QQSP")
	
	If Not Me.IsPostBack Then Return
	If ActionKey="" Then Return

	ProcessWorklistItem(oWorklistItem,ActionKey,hstFormValues,Request.Form("memo"))
End Sub
</script>

<body>

<form id="Form1" method="post" runat="server">

  <%GenerateActInstCommand(Convert.ToInt64(WorklistItemId),"")%>

<center>
	<h1>个人出勤记录缺失说明</h1>
</center>

<%--<table width="700" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>流水号:<input type="text" class="box2" readonly id="Code" ReadOnly style="width:70px;" name="Code" value="<%=hstFormValues("Code")%>" /></td>
    <td align="left">人力资源备案日期：<input name="recorddate" type="text" class="box2" ReadOnly style="width:100px" value="<%=hstFormValues("Recorddate")%>" /></td>
 </tr>
</table>--%>
<table width="700" border="1" cellpadding="0" cellspacing="0" class="Bold_box"  bordercolorlight="#67b2ec" bordercolordark="#FFFFFF">
  <tr>
    <td width="107"  class="F_center">姓名</td>
    <td width="107">
      <input name="name" type="text" class="noborder" style="width:100px" ReadOnly value="<%=hstFormValues("Name")%>"/>
    </td>
    <td width="100" class="F_center">岗位</td>
    <td width="117" align="left" valign="middle"><input name="duties" type="text" class="noborder" style="width:100px" ReadOnly value="<%=hstFormValues("Duties")%>"/></td>
    <td width="97" class="F_center">部门</td>
    <td width="119" align="left" valign="middle"><input name="department" type="text" class="noborder" style="width:100px" ReadOnly value="<%=hstFormValues("Department")%>"/></td>
  </tr>
  <tr>
    <td class="left">时间</td>
    <td colspan="5" align="left" valign="middle">
	<input name="date1" type="text" class="noborder" style="width:100px" value="<%=hstFormValues("Date1")%>"/>&nbsp;&nbsp;起&nbsp;&nbsp;<input name="date2" type="text" class="noborder" style="width:100px" value="<%=hstFormValues("Date2")%>"/>止
	</td>
  </tr>
  <tr>
    <th height="67" colspan="6" align="left" valign="top">
		记录缺失原因及实际出勤说明：<br />
		<textarea name="comments" class="noborder" style="width:99%" rows="8"><%=hstFormValues("Comments")%></textarea>
	</th>
  </tr>
 <%-- <tr>   
	<td colspan="6" height="60" valign="top" align="left" class="box2"><%DisplayAttachment(ViewState.Item("_Id"))%></td>
   </tr>--%>
</table>
<%--
<%DisplayAuditInfo(WorklistItemId)%>--%>

</form>

</body>
</html>
