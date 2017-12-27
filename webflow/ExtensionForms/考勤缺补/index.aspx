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
	<title>���ڱ�</title>
	<script type="text/javascript" src="scripts/DataConvert.js"></script>
	<script type="text/javascript" src="scripts/FormValidate.js"></script>
	<script type="text/javascript" src="scripts/DateCalendar.js"></script>
	
	
	
	<!--������js�ļ�--------->

    <script type="text/javascript" language="JavaScript" src="../scripts/dragDiv.js"></script>

    <!--��֤ҳ���ı��������� js�ļ�----------->

    <script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>

    <!--���ڿؼ�-->

    <script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>

    <!--����дjs�ļ�------------->

    <script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>

<!--#include file="../includes/WorkflowUtilies.aspx"-->

<script language="vb" runat="server">
Private ActionKey As String
Private WorkflowId As String

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	If ViewState.Item("_Id") Is Nothing  Then ViewState("_Id")=TimeId.CurrentMilliseconds(30)
	If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
	If Request("WorkflowId") <>"" Then WorkflowId = Request("WorkflowId")
	
	If Not Me.IsPostBack Then Return
	 Dim Code As String = TimeId.CurrentMilliseconds(30).ToString
	If ActionKey = "" Then Return

	Dim hstFormValues As New Hashtable()
	hstFormValues.Add("Id",ViewState("_Id"))
     hstFormValues.Add("RESID", 341940814109)
            hstFormValues.Add("RELID", 0)
         hstFormValues.Add("CRTID", CurrentUser.Code)
        hstFormValues.Add("CRTTIME", DateTime.Now)
           hstFormValues.Add("EDTID", CurrentUser.Code)
        hstFormValues.Add("EDTTIME", DateTime.Now)
	hstFormValues.Add("recorddate",Request.Form("recorddate"))
	hstFormValues.Add("name",Request.Form("name"))
	hstFormValues.Add("duties",Request.Form("duties"))
	hstFormValues.Add("department",Request.Form("department"))
	hstFormValues.Add("date1",Request.Form("date1")+" "+Request.Form("hour1")+":"+Request.Form("minute1"))
	hstFormValues.Add("date2",Request.Form("date2")+" "+Request.Form("hour2")+":"+Request.Form("minute2"))
	hstFormValues.Add("comments",Request.Form("comments"))
	hstFormValues.Add("Type","ȱ��")
	hstFormValues.Add("State","�����")
	hstFormValues.Add("Attbution",GetEmployeeAttr(CurrentUser.Code,"C3_338226335937"))	
	
	hstFormValues.Add("CodeIndex",Convert.ToInt32(CodeGenerate("WORKFLOW_FORM_QQSP")))
	SDBStatement.InsertRow(hstFormValues,"WORKFLOW_FORM_QQSP")

	Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId), Convert.ToInt64(ViewState("_Id")), ActionKey, hstFormValues,"")
            hstFormValues.Add("WorkflowInstId", oWorklistItem.ActivityInstance.WorkflowInstance.Key)
            StartWorkflowInstance(oWorklistItem)
End Sub
</script>
<body>

<form id="Form1" method="post" runat="server">

  <%GenerateCommand(Convert.ToInt64(WorkflowId), "return ValidateLeaveDay();")%>

<center>
	<h1>���˳��ڼ�¼ȱʧ˵��</h1>
</center>

<%--<table width="700" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td ><input type="hidden" class="noborder" readonly id="Code"  style="width:153px;" name="Code" /></td>
    <td ><input name="recorddate" type="hidden" class="noborder" ReadOnly style="width:100px" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>"/></td>
  </tr>
</table>--%>
<table width="700" border="1" cellpadding="0" cellspacing="0" class="bold_box"  bordercolorlight="#67b2ec" bordercolordark="#FFFFFF">
  <tr>
    <td width="107" class="F_center">����</td>
    <td width="107"><input name="name" type="text" class="noborder" style="width:100px" ReadOnly value="<%=CurrentUser.Name%>"/><input type="hidden" class="noborder" readonly id="Code"  style="width:153px;" name="Code" /><input name="recorddate" type="hidden" class="noborder" ReadOnly style="width:100px" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>"/></td>
    <td width="100" class="F_center">��λ</td>
    <td width="117" align="left" valign="middle"><input name="duties" type="text" class="noborder" ReadOnly style="width:100px" value="<%=CurrentUser.HeadShip%>"/></td>
    <td width="97" class="F_center">����</td>
    <td width="119" align="left" valign="middle"><input name="department" type="text" class="noborder" ReadOnly style="width:100px" value="<%=GetDepartment(CurrentUser.Code)%>"/></td>
  </tr>
  <tr >
    <td class="F_center">ʱ��</td>
    <td colspan="5" align="left" valign="middle">
	<input name="date1" type="text" class="box1" style="width:80px" mInput="true" FieldTitle="��ʼ����" onfocus="Cal_dropdown(this);"/>&nbsp;<input name="hour1" type="text" class="box1" style="width:30px" mInput="true" FieldTitle="��ʼСʱ"/>ʱ&nbsp;<input name="minute1" type="text" class="box1" style="width:25px" mInput="true" FieldTitle="��ʼ����"/>��&nbsp;&nbsp;��&nbsp;&nbsp;
	<input name="date2" type="text" class="box1" style="width:80px" mInput="true" FieldTitle="��ֹ����" onfocus="Cal_dropdown(this);"/>&nbsp;<input name="hour2" type="text" class="box1" style="width:30px" mInput="true" FieldTitle="��ʼСʱ"/>ʱ&nbsp;<input name="minute2" type="text" class="box1" style="width:25px" mInput="true" FieldTitle="��ֹ����"/>��&nbsp;&nbsp;ֹ��
	</td>
  </tr>
  <tr>
    <th height="87" colspan="6" align="left" class="F_center" valign="top" style="text-align: left">
		��¼ȱʧԭ��ʵ�ʳ���˵����<br />
		<textarea name="comments" class="box2" style="width:99%" rows="8" mInput="true" FieldTitle="��¼ȱʧԭ��ʵ�ʳ���˵��"></textarea>
	</th>
  </tr>

</table>

</form>

</body>
</html>
