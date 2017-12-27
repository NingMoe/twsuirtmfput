<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>��ٱ�</title>
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
    hstFormValues = GetFormValues(oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, "CT184270067595")

	If Not Me.IsPostBack Then Return
	If ActionKey="" Then Return  

    
	ProcessWorklistItem(oWorklistItem,ActionKey,hstFormValues,Request.Form("memo"))
 End Sub
</script>

<form id="Form1" method="post" runat="server">
        <%GenerateActInstCommand(Convert.ToInt64(WorklistItemId),"")%>
         <h1 align="center">��������</h1>
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center" style="height:30px;">
                    ����</td>
                <td width="250" align="left" valign="middle">
                    <input name="C1" type="text" class="noborder" id="C1"  value='<%=hstFormValues("C1") %>' readonly="readonly" style="width: 70px"
                          /></td>
                <td width="100" class="F_center">
                    ������</td>
                <td width="250" align="left" valign="middle">
                    <input name="C2" type="text" class="noborder" id="C2"  value='<%=hstFormValues("C2") %>' readonly="readonly" style="width: 100px"
                        value="<%=CurrentUser.Name%>" readonly /></td>
            </tr>
            <tr>
                <td class="F_center" >��������</td>
                <td align="left" colspan="3" ><input name="C3" type="text" value='<%=hstFormValues("C3") %>' readonly="readonly"  FieldTitle="��������" class="noborder" id="C3" style="width: 98%" /></td>
            </tr>
            <tr>
                <td class="F_center">
                    ����˵��</td>
                <td align="left" colspan="3" align="left" valign="top">
                    <textarea name="C6"  type="text" class="noborder" id="C6" mInput="true" FieldTitle="����˵��" readonly="readonly" style="width: 98%;height:60px;" ><%=hstFormValues("C3") %></textarea></td>
            </tr>
            <tr>
                <td class="F_center" style="height:30px;">�������</td>  
                <td >
                    <input name="C4" class="noborder" id="C4" value='<%=hstFormValues("C4") %>' readonly="readonly" style="width: 98%;" mInput="true" FieldTitle="�������" />
                </td>   
                <td class="F_center">ƾ֤����</td>  
                <td >
                    <input name="C5" class="noborder" id="C5" value='<%=hstFormValues("C5") %>' readonly="readonly"  style="width: 98%;" mInput="true" FieldTitle="ƾ֤����" />
                </td>
            </tr>
              <tr>
                <td class="F_center"  style="height:30px;">������Ŀ</td>  
                <td ><%=hstFormValues("C3_381946879924") %>
                </td>   
                <td class="F_center">�Ƿ��������</td>  
                <td ><%=hstFormValues("C3_284723762234") %>
                </td>
            </tr>
            <tr>
                <td class="F_center">
                    ��ע</td>
                <td align="left" colspan="3" align="left" valign="top">
                    <textarea name="C7"  type="text" class="noborder" id="C7" readonly="readonly" style="width: 98%;height:60px;" ><%=hstFormValues("C7") %></textarea></td>
            </tr>  
            <tr style="height: 40px;">
                <td class="F_center" style="height: 30px;">�����Ŀ���</td>
                <td align="left" colspan="3" valign="center" >
                     <input name="C3_386163718156" id="C3_386163718156" style="width: 98%;height:30px;" readonly="readonly" value='<%=hstFormValues("C3_386163718156") %>' />
                </td>
            </tr> 
             <tr style="height: 40px;">
                <td class="F_center" colspan="4"  style="height: 30px;">����״̬</td>
            </tr> 
            <tr style="height: 40px;">
                <td class="F_center"  style="height: 30px;">�Ƿ��ѻ��</td>
                <td valign="middle">&nbsp;&nbsp;<select id="C3_284723586906" name="C3_284723586906" style="width:95%;height:30px;"><option></option><option value="��">��</option><option value="��">��</option></select></td>
                <td class="F_center"  style="height: 30px;">ƾ֤�Ƿ��ѵ�����</td>
                <td valign="middle">&nbsp;&nbsp;<select id="C3_284723656796" name="C3_284723656796" style="width:95%;height:30px;"><option></option><option value="��">��</option><option value="��">��</option></select></td>
            </tr> 
        </table>
</form>
</body>
</html>
