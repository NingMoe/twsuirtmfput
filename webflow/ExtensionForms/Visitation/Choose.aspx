<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>

<HTML>
	<HEAD>
		<title>中锐地产协同办公系统</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
		<script type=text/javascript language="javascript">
		function choose(ProName,ProCode)
		{	
			var no=document.getElementById("txtNO").value;
			document.parentWindow.parent.document.getElementById("ProjectCode").value=ProCode;
			document.parentWindow.parent.document.getElementById("ProjectName").value=ProName;
		    document.parentWindow.parent.closeNotReload();		  
		}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
<script language="vb" runat="server">
 Public table As DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        'If Not IsPostBack Then
        Dim sql As String = "select a.* from (select ProjectCode,ProjectName from PM_ProjectInfo where CreateDate > '2011-12-31' and type!='其他的销售' union all select mid,ProjectName from dbo.PM_MRenewal where isnull(M_state,'')='维护中') a"
         table = SDbStatement.Query(sql).Tables(0)           	
    End Sub 

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ProjectName As String = txtProjectName.Text.Trim
        Dim sql As String = "select a.* from (select ProjectCode,ProjectName from PM_ProjectInfo where CreateDate > '2011-12-31' and type!='其他的销售' union all select mid,ProjectName from dbo.PM_MRenewal where isnull(M_state,'')='维护中') a"
        If ProjectName <> "" Then
            sql += " where a.ProjectName like '%" + ProjectName + "%'"
        End If
        table = SDbStatement.Query(sql).Tables(0)
    End Sub
</script>
		<form id="Form1" method="post" runat="server" onSubmit="">
		<table  width="700">
            <tr>
                <td>项目名称：<asp:TextBox ID="txtProjectName" runat="server" Width="390px"></asp:TextBox><asp:Button
                        ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
		<table width="700" align="center" class="Bold_box" border="1" cellpadding="0" cellspacing="0"bordercolorlight="#67b2ec" bordercolordark="#ffffff">
				<tr>
					
					<td class="F_center" ><strong>项目名称</strong></td>	
				    <td class="F_center" ><strong>项目编号</strong></td>	
				    <td class="F_center" ><strong>操作</strong></td>
				</tr>
				<%  Dim i As Int32 
					   For i = 0 To table.Rows.Count - 1  %>
				<tr id="tr1" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#99cc33'"
                onmouseout="this.style.background='#ffffff'">
                <td >
                    <%=table.Rows(i).Item("ProjectName").ToString & " "%>
                    &nbsp;</td>
                <td>
                    <%=table.Rows(i).Item("ProjectCode").ToString & " "%>
                    &nbsp;</td>
                <td >
                    <input type="button" name="btnChose" value="选择" onclick="choose('<%=table.Rows(i).Item("ProjectName")%>','<%=table.Rows(i).Item("ProjectCode") %>')" /></td>
            </tr>
				<%
					next
					%>
			</table>
			 <input type="hidden" id="txtNO" value="<%=Request.QueryString("NO")%>"/>
		</form>
	</body>
</HTML>

