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
	<title>加班表</title>
	<script type="text/javascript" src="scripts/DataConvert.js"></script>
	<script type="text/javascript" src="scripts/FormValidate.js"></script>
    <link href="css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
<!--#include file="../includes/WorkflowUtilies.aspx"-->
<script language="vb" runat="server">
Private ActionKey As String
Private WorkflowInstId As String
Private WorklistItemId As String
Private hstFormValues As Hashtable
Private table As DataTable
Public aaa As String
Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
	If Request("WorkflowInstId") <>"" Then WorkflowInstId = Request("WorkflowInstId")
	If Request("WorklistItemId") <>"" Then WorklistItemId = Request("WorklistItemId")
	
	Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(WorklistItemId)
	ViewState.Item("_Id") = oWorklistItem.ActivityInstance.WorkflowInstance.RecordId
        hstFormValues = GetFormValues(oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, "HR_OverTime")
	
	If Worklist.GetWorklistItem(WorklistItemId).ActivityInstance.Name.Trim() = "知会" Then
	    If hstFormValues.Contains("IsArchive") Then
		    hstFormValues.Remove("IsArchive")
		    hstFormValues.Add("IsArchive","1")
	    End If	
            SDbStatement.UpdateRows(hstFormValues, "HR_OverTime", "ID=" & oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)
	end if
        'If hstFormValues("Toneoff") IS NOTHING OR hstFormValues("Toneoff").tostring="" Then
        '	aaa="1"
        'ELSE
        '	aaa=hstFormValues("Toneoff").tostring
        'End if
	If Not Me.IsPostBack Then Return
	If ActionKey="" Then Return  
	

    
	ProcessWorklistItem(oWorklistItem,ActionKey,hstFormValues,Request.Form("memo"))
	
	
 End Sub
</script>

<form id="Form1" method="post" runat="server">
<%GenerateActInstCommand(Convert.ToInt64(WorklistItemId),"")%>
<h1 align="center">加班表</h1>
<table width="700" border="0" align="center" cellspacing="0" class="Bold_box">
 <tr style="height: 30px;">
    <td width="105" class="F_center">申请时间</td>
    <td width="171" align="left" valign="middle"><%=hstFormValues("CRTTIME")%></td>    
    <td width="95" class="F_center">项目名称</td>
    <td width="318" colspan="2" align="left" valign="middle">&nbsp;<%=hstFormValues("ProjectName") %></td>
  </tr>
  <tr>
    <td width="105" class="F_center">年度</td>
    <td width="171" align="left" valign="middle">&nbsp;<%=hstFormValues("Years") %></td>    
    <td width="95" class="F_center">加班人</td>
    <td width="318" colspan="2" align="left" valign="middle">&nbsp;<%=hstFormValues("EmpName")%></td>
  </tr>
  <tr>
    <td class="F_center">开始时间</td>
    <td align="left">&nbsp;<%=hstFormValues("StartDate")%>&nbsp;&nbsp;<%=hstFormValues("StartHour")%>:<%=hstFormValues("StartMinute") %>　</td>
   
    <td class="F_center">结束时间</td>
    <td colspan="2" align="left" valign="middle">&nbsp;<%=hstFormValues("EndDate")%> &nbsp;&nbsp;<%=hstFormValues("EndHour")%>:<%=hstFormValues("EndMinute") %></td>
  </tr>
  <tr>
    <td class="F_center">加班时间</td>
    <td align="left" valign="middle" colspan="4">&nbsp;<%=hstFormValues("Overtime") %>小时</td>

  </tr>
  <tr>
    <td class="F_center">事由</td>
    <td height="98" colspan="4" align="left" valign="top">&nbsp;<%=hstFormValues("Explain")%></td>
  </tr>
</table> 
 <%DisplayAuditInfo(Convert.ToInt64(WorklistItemId))%>
</form>
</body>
</html>
