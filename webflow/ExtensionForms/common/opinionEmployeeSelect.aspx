<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
	<title>EmployeeSelect</title>
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="MzTreeView10.js"></script>

<script type="text/javascript">
//获取选取任务处理人的数量
function GetCheckedEmployee(){
	var result = 0;
	var employees = document.getElementsByName("chkEmployee");
	for (var i=0;i<employees.length;i++){
		if (employees[i].checked){
			result++;
			break;
		}
	}
	return result;
}
</script>
<script language="vb" runat="server">

Private ResId As String
Private RecId As String

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	ResId = Request.QueryString("mnuresid")
	RecId = Request.QueryString("mnurecid")
End Sub

'-----------------------------------------------------
'生成人员选取目录
'-----------------------------------------------------
Protected Sub GenerateEmployeeTree()
	Response.Write("tree.nodes['0_-1']='id:0;text:许可查看人选择;target:_self;icon:workflow;';" & vbCrLf)
	GenerateDepartmentTree("-1",0)
End Sub

Private Sub GenerateDepartmentTree(ByVal NodeId As String, ByVal DepartmentId As String)
	Dim Id As Long,Name As String
	Dim oDepartment As DataTable = SDbStatement.Query("SELECT * FROM CMS_DEPARTMENT WHERE PID=" & DepartmentId & " ORDER BY SHOW_ORDER").Tables(0)
	For i As Integer = 0 To oDepartment.Rows.Count-1
		Id = oDepartment.Rows(i)("Id")
		Name = oDepartment.Rows(i)("Name")
		'生成当前部门节点
		Response.Write("tree.nodes['" & DepartmentIdConvert(NodeId) & "_" & DepartmentIdConvert(Id) & "']='id:" & DepartmentIdConvert(Id) & ";text:" & Name & ";check:false;check-name:chkDepartment;icon:department;';" & vbCrLf)
		'生成当前部门下人员节点
		GenerateDepartmentEmployeeTree(DepartmentIdConvert(Id), Id)
		'生成子部门节点
		GenerateDepartmentTree(Id,Id)
	Next
End Sub

Private Sub GenerateDepartmentEmployeeTree(ByVal NodeId As String, ByVal DepartmentId As String)
	Dim oEmployees As DataTable = SDbStatement.Query("SELECT * FROM CMS_EMPLOYEE WHERE HOST_ID=" & DepartmentId & " ORDER BY SHOW_ORDER").Tables(0)
	Dim Id As Long,Name As String,Code As String,Station As String
	For i As Integer = 0 To oEmployees.Rows.Count - 1
		Id = oEmployees.Rows(i)("Id")
		Code = oEmployees.Rows(i)("EMP_ID")
		Name = oEmployees.Rows(i)("EMP_NAME")
		Station = DbField.GetStr(oEmployees.Rows(i),"C3_337790108500")
		GenerateEmployeeTreeNode(NodeId, Id, Station & " - " & Name, Code)
	Next
End Sub

Private Sub GenerateEmployeeTreeNode(ByVal NodeId As String, ByVal EmployeeId As String, ByVal EmployeeName As String, ByVal EmployeeCode As String)
	Response.Write("tree.nodes['" & NodeId & "_" & EmployeeId & "']='id:" & EmployeeCode & ";text:" & EmployeeName & ";check:true;check-name:chkEmployee;icon:employee;';" & vbCrLf)
End Sub

Private Function DepartmentIdConvert(ByVal DepartmentId As String) As String
	If DepartmentId = "0" Then
		Return "101"
	Else
		Return DepartmentId
	End If
End Function

Private Sub UpdateOpinionEmployee(ByVal sender As System.Object, ByVal e As System.EventArgs)
	Dim hst As Hashtable = New Hashtable()
	Dim strEmployees() As String = Split(Request.Form("chkEmployee") & ",",",")

	hst.Add("TransactorCode","")
	hst.Add("Comments",Session("OpinionComments"))
	hst.Add("SrcWorklistItemId",Request.QueryString("WorklistItemId"))
	hst.Add("SrcWorkflowIntanceId",Request.QueryString("WorkflowInstanceId"))
	hst.Add("CreatorCode",CurrentUser.Code)
	hst.Add("CreatorName",CurrentUser.Name)
	hst.Add("CreateDate",DateTime.Now)
	hst.Add("State",0)
	For i As Integer = 0 To strEmployees.Length-1
		If strEmployees(i)<>"" Then
		    hst("ID") = TimeId.CurrentMilliseconds(30)
			hst("TransactorCode") = strEmployees(i)
			hst("TransactorName") = GetEmployeeName(strEmployees(i))
			SDbStatement.InsertRow(hst,"WF_OPINIONCOLLECTION")			
		End If
	Next
	Response.Write("发送成功!")
	Response.End
End Sub

Private Function GetEmployeeName(Code As String) As String
	Dim oEmployees As DataTable = SDbStatement.Query("SELECT * FROM CMS_EMPLOYEE WHERE EMP_ID='" & Code & "'").Tables(0)
	If oEmployees.Rows.Count = 1 Then
		Return DbField.GetStr(oEmployees.Rows(0),"EMP_name")
	Else
		Return ""
	End If
End Function
</script>

</HEAD>
<body>
	<form id="Form1" method="post" runat="server">
	<table cellpadding="0" cellspacing="0" border="0" width="99%" align="left" style="BORDER-RIGHT:1px solid; BORDER-LEFT:1px solid">
		<tr>
			<td>
				<div id="treeviewarea" style="OVERFLOW:auto;HEIGHT:100%"></div>
<SCRIPT LANGUAGE="JavaScript">
<!--
var tree = new MzTreeView("tree");
tree.icons["workflow"]="res_workflow.gif";
tree.icons["department"]="department.gif";
tree.icons["employee"]="worker.gif";
tree.setIconPath("tree/");
<%GenerateEmployeeTree()%>
document.getElementById('treeviewarea').innerHTML = tree.toString();
//tree.expandAll();
-->
</SCRIPT>
			</td>
		</tr>
		<tr>
			<td align="center" class="ToolBar">
				<asp:Button id="btnOK" runat="server" Text="确定" OnClick="UpdateOpinionEmployee"></asp:Button>
				<asp:Button id="btnCancel" runat="server" Text="取消"></asp:Button>
			</td>
		</tr>
	</table>
	</form>
</body>
</HTML>