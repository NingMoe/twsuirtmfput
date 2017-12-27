<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Cms.Implement"%>
<%@ Import namespace="Unionsoft.Cms.Platform"%>
<%@ Import namespace="System.Data"%>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<HTML>
	<HEAD>
		<title>项目字典</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
			body{
					margin:0px;
					font-family: Arial, Helvetica, sans-serif;
					font-size:12px;
				}
			.table{
				border: 1px solid #67b2ec;
				font-size: 12px;
				line-height: 18px;
				color: #666666;
				text-decoration: none;
				text-indent: 4px;
			}
			.table td{
				border:#67b2ec solid;
				border-width:0 1px 1px 0;
			}
		</style>
		<script>
			var Code="";
			var Name="";
			function selectedRow(code,name,obj)
			{
				var trList=document.getElementById("table1").getElementsByTagName("tr");
				
				for(var i=1;i<trList.length;i++)
				{
					trList[i].style.backgroundColor="#fff";
				}
				obj.style.backgroundColor="#cccccc";
				
				Code=code
				Name=name;
			}
			
			function RetValue()
			{
				if(Code=="")
				{
					alert("请选择有效人员！");
					return;
				}
				document.parentWindow.parent.selectProject(Code,Name)
				document.parentWindow.parent.closeWindow1();
			}
			
		</script>
		
	</HEAD>
	<body MS_POSITIONING="GridLayout" style="overflow:hidden;">
		<form id="Form1" method="post" runat="server">
			<div style="width:100%;height:470px;overflow:auto;">
				<table cellpadding="0" cellspacing="0" border="0" width="90%" class="table" align="center" id="table1">
					<tr style="background:#E3EFFC;">
						<td><strong>人员姓名</strong></td>
					</tr>
					<%
					    Dim dt As DataTable = NetReusables.SDbStatement.Query("select * from CMS_EMPLOYEE").Tables(0)
						for i as integer =0 to dt.Rows.Count-1
					%>
					
					<tr onclick="selectedRow('<%=DbField.GetStr(dt.Rows(i),"EMP_ID") %>','<%=DbField.GetStr(dt.Rows(i),"EMP_NAME") %>',this);">
						<td><%=DbField.GetStr(dt.Rows(i), "EMP_NAME")%></td>
					</tr>
					<%Next%>
				</table>
			</div>
			<table cellpadding="0" cellspacing="0" border="0" width="90%" align="center">
				<tr>
					<td align="center"><input type="button" value="选择" onclick="RetValue();"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
