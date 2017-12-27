<%@ Page Language="VB" AutoEventWireup="false" CodeFile="adminprocessweekanalyse.aspx.vb" Inherits="admin_adminprocessweekanalyse" %>
<%@ import namespace="System.Data"%>
<%@ import namespace="NetReusables"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
		<script type="text/javascript" src="../script/DateCalendar.js"></script>
    
    <script language="vb" runat="server">
		Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			
		End Sub
	    
		 Protected Function GetTaskQty(ByVal EmployeeCode As String) As Integer
			Dim strSql As String = "SELECT * FROM VIEW_WF_RECEIVEFILES WHERE EMPCODE='" & EmployeeCode & "'"
			'if  not ViewState("Condition")  is nothing  then 
				'if ViewState("Condition").ToString()<>"" then strSql &= " and " & ViewState("Condition").ToString()
			'end if
			Return SDbStatement.Query(strSql).Tables(0).Rows.Count
		End Function

		Protected Function GetUnViewTaskQty(ByVal EmployeeCode As String) As Integer
			Dim strSql As String = "SELECT * FROM VIEW_WF_RECEIVEFILES WHERE EMPCODE='" & EmployeeCode & "' AND VIEWTIME IS NULL"
			'if  not ViewState("Condition")  is nothing  then 
				'if ViewState("Condition").ToString()<>"" then strSql &= " and " & ViewState("Condition").ToString()
			'end if
			Return SDbStatement.Query(strSql).Tables(0).Rows.Count
		End Function
		
		Protected Function GetTaskDate(ByVal EmployeeCode As String) As string
			Dim strSql As String = "SELECT Min(CreateTime) CreateTime FROM VIEW_WF_RECEIVEFILES WHERE EMPCODE='" & EmployeeCode & "'"
			'if  not ViewState("Condition")  is nothing  then 
				'if ViewState("Condition").ToString()<>"" then strSql &= " and " & ViewState("Condition").ToString()
			'end if
			
			dim dt as DataTable=SDbStatement.Query(strSql).Tables(0)	
			if DbField.GetDtm(dt.Rows(0),"CreateTime").ToString("yyyy-MM-dd")="0001-01-01"  then
				return ""
			else 
				Return DbField.GetDtm(dt.Rows(0),"CreateTime").ToString("yyyy-MM-dd")
			end if
			
		End Function
		
		
		
		
		
		Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
			if txtStart.Text<>"" and txtEnd.Text <> "" then 
				ViewState("Condition")=" CreateTime between '" & txtStart.Text & " 00:00:00' and '" & txtEnd.Text & " 23:59:59'"
			else
				ViewState("Condition")=""
			end if
		End Sub
		
		
		Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
			
		End Sub
		
		</script>
</head>
<body>
   <form id="Form1" method="post" runat="server">
			<FONT face="宋体"></FONT>
			<!-----------暂不使用----------------------------------->
			<table cellpadding='0' cellspacing='1' border='0' class='ListBox' bgcolor='#3366cc' style="DISPLAY:none">
				<tr bgcolor='white' class='ListItem'>
					<td colspan="5">
						请选择日期：<asp:TextBox ID="txtStart" Runat="server" onfocus='Cal_dropdown(this);'></asp:TextBox>—<asp:TextBox ID="txtEnd" Runat="server" onfocus='Cal_dropdown(this);'></asp:TextBox>
						<asp:Button ID="btnSubmit" Runat="server" Text="统计" OnClick="btnSubmit_Click"></asp:Button>
					</td>
					<td align="right">
						<asp:Button ID="btnSave" Runat="server" Text="保存统计" OnClick="btnSave_Click"></asp:Button>
					</td>
				</tr>
				<tr class='ListHeader'>
					<td width='100' height='30' align='center'>部门</td>
					<td width='100'>姓名</td>
					<td width='100' align='center'>待办数</td>
					<td width='100' align='center'>未查看数</td>
					<td width='100' align='center'>其中最早的时间</td>
					<td width='100' align='center'>对比前次统计数量</td>
				</tr>
			</table>
			<!--------------------------------------------------------------------->
			<table cellpadding='0' cellspacing='1' border='0' class='ListBox' bgcolor='#3366cc'>
				<tr class='ListHeader'>
					<td width='100' height='30' align='center'>部门</td>
					<td width='100'>姓名</td>
					<td width='100' align='center'>待办数</td>
					<td width='100' align='center'>未查看数</td>
					<td width='100' align='center'>其中最早的时间</td>
				</tr>
				<%
				Dim strSql As String
				strSql = "SELECT * FROM CMS_DEPARTMENT WHERE DEP_TYPE=0 AND PID<>-1"
				
				
				Dim dtDepartent As DataTable = SDbStatement.Query(strSql).Tables(0)

				strSql = "SELECT * FROM CMS_EMPLOYEE WHERE EMP_TYPE IS NULL"
				
				
				Dim dtEmployee As DataTable = SDbStatement.Query(strSql).Tables(0)

				For i As Integer = 0 To dtDepartent.Rows.Count - 1
					Dim dr() As DataRow = dtEmployee.Select("HOST_ID=" & CStr(dtDepartent.Rows(i)("ID")))			
				%>
				<tr bgcolor='white' class='ListItem'>
					<td align="center" rowspan="<%=dr.Length %>" width="100"><%=dtDepartent.Rows(i)("Name")%></td>
					<%
					For j As Integer = 0 To dr.Length - 1	
					dim TaskQty as string =GetTaskQty(CStr(dr(j)("EMP_ID"))).ToString()
					dim UnViewTaskQty as string =GetUnViewTaskQty(CStr(dr(j)("EMP_ID"))).ToString()
					dim TaskDate as string =GetTaskDate(CStr(dr(j)("EMP_ID")))
					%>
					<%if j<>0 then%>
				<tr>
					<%end if%>
					<td bgcolor='white' height='25' width="100"><%=CStr(dr(j)("EMP_NAME"))%></td>
					<td bgcolor='white' align='right' width="100"><%=TaskQty %>&nbsp;</td>
					<td bgcolor='white' align='right' width="100"><%=UnViewTaskQty%>&nbsp;</td>
					<td bgcolor='white' align='center' width="100"><%=TaskDate%></td>
					<%if j<>0 then%>
				</tr>
				<%end if%>
				<%next%>
				</tr>
				<%next%>
			</table>
			<DIV></DIV>
		</form>
</body>
</html>
