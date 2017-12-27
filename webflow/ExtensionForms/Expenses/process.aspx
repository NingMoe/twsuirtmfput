<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>报销申请单</title>
	<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/FormValidate.js"></SCRIPT>
		<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/dragDiv.js"></SCRIPT>
				<!--金额大写js文件------------->
		<script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>
	<link   href="../css/YYTys.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.STYLE1 {font-size: 12px}
.input_ReadOnly
	{
		font-size: 13px;
		color: #000000;
		border-top-style:none;
		border-right-style: none; 
		border-left-style: none; 
		border-bottom-style: none;
		width: 292px;
		t:expression(this.disabled=true)
	}
-->
</style>
</head>
<!--#include file="../includes/WorkflowUtilies.aspx"-->
<!--#include file="../includes/WorkflowAttachment.aspx"-->

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
        hstFormValues = GetFormValues(oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, "PM_Expenses")
	

	If Not Me.IsPostBack Then Return
	If ActionKey="" Then Return

	ProcessWorklistItem(oWorklistItem,ActionKey,hstFormValues,Request.Form("memo"))
End Sub
</script>
<body>
<form id="Form1" method="post" runat="server">

<%GenerateActInstCommand(Convert.ToInt64(WorklistItemId),"")%>

<h1 align="center">报销申请单</h1>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td>&nbsp;</td>
	<td align="center" style="text-align:center;display:none">
	    <div align="center">
	      <blockquote>
	        <p>
	          <%=hstFormValues("ExpensesID")%>&nbsp;
            </p>
          </blockquote>
      </div></td>
  </tr>
</table>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0" class="ziti">

  <tr>
    <td width="49" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">报销人:</td>
    <td width="80" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">&nbsp;<%=hstFormValues("EmpName") %></td>
    <td width="32" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">部门:</td>
    <td width="105" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">&nbsp;<%=hstFormValues("Department") %></td>
	<td width="33" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">岗位:</td>
    <td width="119" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">&nbsp;<%=hstFormValues("Headship") %></td>
	<td width="58" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">申请日期:</td>
    <td width="85" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">&nbsp;<%=hstFormValues("AppDate") %></td>
  </tr>
</table>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box" id="tbDetails" style="border-bottom-width:0px">
<tr style="height: 30px;">
    <td width="95" class="F_center" style="height: 30px">项目名称</td>
    <td colspan="3" align="left" valign="middle" style="height: 30px; width: 318px;" >&nbsp;<%=hstFormValues("ProjectName")%></td>
    <td width="105" class="F_center" style="height: 30px" >项目编号</td>
    <td width="171" align="left" valign="middle" colspan="3" style="height: 30px">&nbsp;<%=hstFormValues("ProjectCode")%></td>    
</tr>

  <tr>
    <td width="47" class="F_center">序号</td>
    <td width="99" class="F_center">用款日期</td>
    <td width="392" class="F_center">说明</td>
    <td width="80" class="F_center">单价</td>
    <td width="76" class="F_center">数量</td>
    <td class="F_center" style="width: 115px">金额</td>
  </tr>
  <%  Dim sql As String = String.Format("SELECT * FROM PM_ExpDetail WHERE expid={0}", hstFormValues("ExpensesID"))
                      Dim table As DataTable = SDbStatement.Query(sql).Tables(0)
					  
					  Dim i As Int32 
					   For i = 0 To table.Rows.Count-1				 
					 %> 
  <tr>
    <td><%=i+1%>
    <td><%=Convert.ToDateTime (table.Rows(i).Item("ExpDate")).ToString ("yyyy-MM-dd") & ""%>&nbsp;</td>
	<td><%=table.Rows(i).Item("Explain").ToString &" "%>&nbsp;</td>
	<td><%=table.Rows(i).Item("Number").ToString & " "%>&nbsp;</td>
	<td><%=table.Rows(i).Item("Price").ToString & " "%>&nbsp;</td>
	<td style="width: 115px"><%=table.Rows(i).Item("Amount").ToString & " "%>&nbsp;</td> 
  </tr>
  <% Next%>
  </table>
  <table width="900" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box" style="border-top-width:0px;">
  <tr>
	<td width="156" class="F_center">合计</td>
    <td width="247" ><%=hstFormValues("Sum")%>&nbsp;</td>
	<td width="146" class="F_center">合计大写</td>
	<td width="336"><%=hstFormValues("CapitalSum")%>&nbsp;</td>
  </tr>
  <tr>
	<td class="F_center">备注</td>
	<td colspan="3"><%=hstFormValues("Remarks")%>&nbsp;</td>
  </tr>
</table>



<center>
   <%DisplayAuditInfo(Convert.ToInt64(WorklistItemId))%>
<input type="text" style="display:none;" id="txtRowCount" size="4" name="txtRowCount" value="<%=table.Rows.Count%>"/>
</center>
</form>
</body>
</html>