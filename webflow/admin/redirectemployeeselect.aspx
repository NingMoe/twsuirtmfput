<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.RedirectEmployeeSelect" CodeFile="RedirectEmployeeSelect.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RedirectEmployeeSelect</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/flowstyle.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../script/MzTreeView10.js"></script>
		<script type="text/javascript" src="../script/prototype.js"></script>
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
		
		function EmployeeValidate(){
			var result = GetCheckedEmployee();
			if (result==0) {
				alert("必须选择人员!");
				return false;
			}
			return true;
		} 
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" border="0" width="100%" align="left" style="BORDER-RIGHT:1px solid; BORDER-LEFT:1px solid">
				<tr>
					<td>
						 <div id="treeviewarea" style="OVERFLOW:auto;HEIGHT:100%"></div>
                        <SCRIPT LANGUAGE="JavaScript">
                        <!--
                        var tree = new MzTreeView("tree");
                        tree.icons["workflow"]="res_workflow.gif";
                        tree.icons["department"]="department.gif";
                        tree.icons["employee"]="worker.gif";
                        tree.setIconPath("../images/tree/");
                        <%InitTree()%>
                        document.getElementById('treeviewarea').innerHTML = tree.toString();
                        //tree.expandAll();
                        -->
                        </SCRIPT>
					</td>
				</tr>
				<tr>
					<td align="center" class="ToolBar">
						<asp:Button id="btnOK" runat="server" Text="确定" OnClientClick="return EmployeeValidate();"></asp:Button>
						<input type="button" value="取消" onclick="parent.closeWindow()">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
