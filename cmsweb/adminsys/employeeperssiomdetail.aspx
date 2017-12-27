<%@ Page Language="vb" AutoEventWireup="false" Inherits="System.Web.UI.Page" %>
<%@ Import NameSpace="System.Data"%>
<%@ Import NameSpace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>查看人员权限</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="vb" runat="server">
		'取得人员所在部门的ID
		Public Function GetEmployeeDepartmentId(ByVal EmployeeId As String) As String
			Dim strSql As String = "Select * From CMS_EMPLOYEE Where EMP_ID='" & EmployeeId & "'"
			Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
			If dt.Rows.Count > 0 Then
				Return DbField.GetStr(dt.Rows(0), "HOST_ID")
			Else
				Return ""
			End If
		End Function
		
		'获取所有部门的全路径
		Public Sub GetFullDepartmentId(ByVal DepartmentId As String, ByRef FullDepartmentId As String)
			Dim strSql As String = "select * from CMS_DEPARTMENT where ID=" & DepartmentId
			Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
			If dt.Rows.Count > 0 Then
				FullDepartmentId = "'" & DbField.GetStr(dt.Rows(0), "PID") & "'," & FullDepartmentId
				GetFullDepartmentId(DbField.GetStr(dt.Rows(0), "PID"), FullDepartmentId)
			End If
		End Sub
		
		'获取部门名
		    Public Function GetDepartmentName(ByVal DepartmentId As String) As String
		        Dim strSql As String = "select * from CMS_DEPARTMENT where ID=" & DepartmentId
		        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
		        If dt.Rows.Count > 0 Then
		            Return DbField.GetStr(dt.Rows(0), "Name")
		        Else
		            Return ""
		        End If
		    End Function
		
		'获取分类名
		Public Sub GetResourceName(ByVal ResourceId As Long,ByRef ResourceName As String)
			Dim strSql As String = "Select * From CMS_RESOURCE Where ID=" & ResourceId
			Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
			If dt.Rows.Count > 0 Then
				ResourceName =  "\" & DbField.GetStr(dt.Rows(0), "NAME") & ResourceName
		            If DbField.GetLng(dt.Rows(0), "PID") = 0 Then
		                ResourceName = GetDepartmentName(DbField.GetStr(dt.Rows(0), "HOST_ID")) & ResourceName
		            Else
		                GetResourceName(DbField.GetLng(dt.Rows(0), "PID"), ResourceName)
		            End If
			End If
		End Sub

		'获取权限名
		Public Function GetRightsName(ByVal a As Long) As String
			Dim QxName As String = ""
			If (a And 1) = 1 Then
				QxName = QxName & "|" & "浏览权限"
			End If
			If (a And 2) = 2 Then
				QxName = QxName & "|" & "增加权限"
			End If
			If (a And 4) = 4 Then
				QxName = QxName & "|" & "修改权限"
			End If
			If (a And 8) = 8 Then
				QxName = QxName & "|" & "删除"
			End If
			If (a And 16) = 16 Then
				QxName = QxName & "|" & "打印记录"
			End If
			If (a And 4096) = 4096 Then
				QxName = QxName & "|" & "打印列表"
			End If
			If (a And 268435456) = 268435456 Then
				QxName = QxName & "|" & "发送邮件短信"
			End If
			If (a And 1073741824) = 1073741824 Then
				QxName = QxName & "|" & "批量更新字段"
			End If
			If (a And 16777216) = 16777216 Then
				QxName = QxName & "|" & "批量更新记录"
			End If
			If (a And 32) = 32 Then
				QxName = QxName & "|" & "签入/签出文档"
			End If
			If (a And 33554432) = 33554432 Then
				QxName = QxName & "|" & "取消签出状态"
			End If
			If (a And 64) = 64 Then
				QxName = QxName & "|" & "在线编辑"
			End If
			If (a And 128) = 128 Then
				QxName = QxName & "|" & "提取文档"
			End If
			If (a And 256) = 256 Then
				QxName = QxName & "|" & "在线浏览文档"
			End If
			If (a And 512) = 512 Then
				QxName = QxName & "|" & "查阅历史版本"
			End If
			If (a And 1024) = 1024 Then
				QxName = QxName & "|" & "共享文档"
			End If
			If (a And 2048) = 2048 Then
				QxName = QxName & "|" & "打印文档"
			End If
			If (a And 32768) = 32768 Then
				QxName = QxName & "|" & "计算公式设置"
			End If
			If (a And 65536) = 65536 Then
				QxName = QxName & "|" & "权限设置"
			End If
			If (a And 131072) = 131072 Then
				QxName = QxName & "|" & "字段设置"
			End If
			If (a And 262144) = 262144 Then
				QxName = QxName & "|" & "显示设置"
			End If
			If (a And 524288) = 524288 Then
				QxName = QxName & "|" & "输入窗体设计"
			End If
			If (a And 1048576) = 1048576 Then
				QxName = QxName & "|" & "打印窗体设计"
			End If
			If (a And 2097152) = 2097152 Then
				QxName = QxName & "|" & "关联表设置"
			End If
			If (a And 4194304) = 4194304 Then
				QxName = QxName & "|" & "行颜色设置"
			End If
			If (a And 67108864) = 67108864 Then
				QxName = QxName & "|" & "导出资源数据"
			End If
			If (a And 134217728) = 134217728 Then
				QxName = QxName & "|" & "导入资源数据"
			End If
			If (a And 8192) = 8192 Then
				QxName = QxName & "|" & "增加资源"
			End If
			If (a And 16384) = 16384 Then
				QxName = QxName & "|" & "修改资源"
			End If
			If (a And 8388608) = 8388608 Then
				QxName = QxName & "|" & "删除资源"
			End If
			If (a And 2147483648) = 2147483648 Then
				QxName = QxName & "|" & "列表统计"
			End If
			Return QxName
		End Function
		</script>
	</HEAD>
	<body>
	<%
	Dim EmployeeId As String = "cy"
	Dim EmployeeName As String 
	Dim DepartmentId As String = ""
	Dim FullDepartmentId As String = ""
	Dim dtEmployees As DataTable	
    
    dtEmployees = SDbStatement.Query("select * from CMS_EMPLOYEE where EMP_TYPE IS NULL").Tables(0)
    For k As Integer = 0 To dtEmployees.Rows.Count-1
    %>
    <table width="2000">
    <%
		EmployeeId = DbField.GetStr(dtEmployees.Rows(k), "EMP_ID")
		EmployeeName = DbField.GetStr(dtEmployees.Rows(k), "EMP_NAME")
		
		DepartmentId = GetEmployeeDepartmentId(EmployeeId)
		FullDepartmentId = ""
		
		GetFullDepartmentId(DepartmentId,FullDepartmentId)
		FullDepartmentId = FullDepartmentId & "'" & DepartmentId & "'"
		
		Dim strSql As String = "Select * From CMS_RIGHTS Where (QX_GAINER_ID='" & EmployeeId & "' AND QX_GAINER_TYPE=0) OR QX_GAINER_ID IN (" & FullDepartmentId & ") AND QX_GAINER_TYPE=1"
		Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
	    
		For i As Integer = 0 To dt.Rows.Count-1
		%>
		<tr <%if i mod 2 = 0 then %>bgColor="#cccccc"<%End If%>>
			<td width="100"><%=EmployeeName%>(<%=EmployeeId%>)</td>
			<td width="310">
			<%
			Dim strResourceName As String = ""
			GetResourceName(DbField.GetLng(dt.Rows(i), "QX_OBJECT_ID"),strResourceName)
			Response.Write(strResourceName)
			%>
			</td>
			<td><%=GetRightsName(DbField.GetLng(dt.Rows(i), "QX_VALUE"))%></td>
		</tr>
		<%
		Response.Flush
		Next
	%>
	</table>
	<%
	Next
	%>
	
	</body>
</html>
