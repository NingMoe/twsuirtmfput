<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RecordPrintSimple" CodeFile="RecordPrintSimple.aspx.vb" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>打印记录</title>
		
		<meta http-equiv="Pragma" content="no-cache">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="/cmsweb/script/base.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="/cmsweb/script/Valid.js"></SCRIPT>
		<style>
		@media print {
		.notprint {
				display:none;
				}
		}

		@media screen {
		.notprint {
				display:inline;
				cursor:hand;
				}
		}
		</style>
		<script language="JavaScript">
		window.onload = function (){
			return;
			Panel1.innerHTML = Panel1.innerHTML.toLowerCase().replace(/(textarea)/g,"div");
			Panel1.innerHTML = Panel1.innerHTML.toLowerCase().replace(/(\&quot;)/g,"\'");
			Panel1.innerHTML = Panel1.innerHTML.toLowerCase().replace(/(\&lt;)/g,"<");
			Panel1.innerHTML = Panel1.innerHTML.toLowerCase().replace(/(\&gt;)/g,">");
			
			alert(Panel1.innerHTML.toLowerCase());
		}
		</script>
	</HEAD>
	<body>
		<script language="vb" runat="server">
		Function HasSignPicture(ByVal UserCode As String) As Boolean
			Dim strPicture As Object
			Dim strSql As String = "SELECT SIGNPICTURE FROM CMS_EMPLOYEE WHERE EMP_ID='" & UserCode & "'"
			Dim dt As DataTable = SDBStatement.Query(strSql).Tables(0)
			If Not dt.Rows.Count<=0 Then
				If Not DBField.GetObj(dt.Rows(0),"SIGNPICTURE") Is Nothing Then
					Return True
				End If
			End IF
			Return False
		End Function
		</script>
		<form id="Form1" method="post" runat="server">
		<OBJECT id=WebBrowser classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 width=0 VIEWASTEXT></OBJECT> 
			<p align="center"><input type="button" id="btn_PreView" title="预览" onclick="WebBrowser.ExecWB(7,1);" value="预览" class='notprint'>
			<input type="button" id="btn_Print" title="打印" onclick="WebBrowser.ExecWB(6,1);" value="打印" class='notprint'>
			<input type="button" id="btn_Next" title="下一页" onclick="Form1.submit();" value="下一页" class='notprint' runat=server style="Z-INDEX: 99">
			<input type="button" id="btn_PrintNext" title="打印并转到下一页" onclick="WebBrowser.ExecWB(6,1);Form1.submit();" runat=server value="打印并转到下一页" class='notprint'></p>
			<asp:panel id="Panel1" style="Z-INDEX: -99; LEFT: 0px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; POSITION: absolute; TOP: 0px; BORDER-BOTTOM-STYLE: none"
				runat="server" Height="112px" Width="216px" BorderColor="#D7E7FF"></asp:panel>
			<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
			<%
Dim i as Integer
If Not dtWorkFlowTasks is Nothing Then

	For i=0 To dtWorkFlowTasks.Rows.Count-1
	Dim strStyle2 As String
	If i=0 Then
	strStyle2= "POSITION: absolute;width:" & Panel1.width.ToString() & "; top:" & Heigth2 & "px;left:" & 0 & "px"
	else
	strStyle2= "POSITION: absolute;width:" & Panel1.width.ToString() & "; top:" & Heigth2+i*100 & "px;left:" & 0 & "px"
	End If
	 
	%>
			<table cellSpacing=1 cellPadding=0 border=0 
		<%			
			Response.Write("class=""FlowHistoryTable""")
			
			Response.Write(" Style='" & strStyle2 & "'")
		%>
	>
				<tr>
					<td width="70" align="center">环节名称</td>
					<td>&nbsp;<%=DBField.GetStr(dtWorkFlowTasks.Rows(i),"NODENAME")%></td>
					<td width="70" align="center">任务处理人</td>
					<td>&nbsp;<%If HasSignPicture(DBField.GetStr(dtWorkFlowTasks.Rows(i),"EMPCODE"))=True Then%>
							    <img src="SignPicture.aspx?UserCode=<%=DBField.GetStr(dtWorkFlowTasks.Rows(i),"EMPCODE")%>">
							  <%Else%>
								<%=DBField.GetStr(dtWorkFlowTasks.Rows(i),"EMPNAME")%>
							  <%End If%></td>
				</tr>
				<tr>
					<td width="70" align="center">到达时间</td>
					<td>&nbsp;<%=DBField.GetDtm(dtWorkFlowTasks.Rows(i),"CreateTime")%></td>
					<td width="70" align="center">查看时间</td>
					<td>&nbsp;<%=DBField.GetDtm(dtWorkFlowTasks.Rows(i),"ViewTime")%></td>
				</tr>
				<tr>
					<td width="70" align="center">处理时间</td>
					<td>&nbsp;<%=DBField.GetDtm(dtWorkFlowTasks.Rows(i),"DealTime")%></td>
					<td width="70" align="center">处理结果</td>
					<td>&nbsp;<%=DBField.GetStr(dtWorkFlowTasks.Rows(i),"ACTIONNAME")%></td>
				</tr>
				<tr>
					<td width="70" align="center">处理意见</td>
					<td colspan="3">&nbsp;<%=DBField.GetStr(dtWorkFlowTasks.Rows(i),"Memo")%></td>
				</tr>
			</table>
			<%
	Next
End If
%>
		</form>
	</body>
</HTML>
