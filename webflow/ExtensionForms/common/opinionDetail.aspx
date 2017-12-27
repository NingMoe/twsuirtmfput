<%@ Page Language="vb" AutoEventWireup="false" Inherits="System.Web.UI.Page"%>
<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ Import namespace="NetReusables"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>意见收集</title>
	<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/FormValidate.js"></SCRIPT>
	<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/Wo_Modal.js"></SCRIPT>
	<link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<script language="vb" runat="server">
Private WorklistItemId As String = "0"
Private dt As DataTable

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load	
	WorklistItemId = Request.QueryString("WorklistItemId")

	Dim strSql As String = "select * from  WF_OPINIONCOLLECTION where SrcWorklistItemId=" & WorklistItemId
	dt = SDbStatement.Query(strSql).Tables(0)

End Sub

</script>
<form id="Form1" method="post" runat="server">
<table width="100%" class="bold_box" border="1" cellpadding="0" cellspacing="0" bordercolorlight="#67b2ec" bordercolordark="#FFFFFF">
	<%For i As Integer = 0 To dt.Rows.Count-1%>
	<tr height="22">
		<td width="80"><%=DbField.GetStr(dt.Rows(i),"CreatorName")%></td>
		<td width="120"><%=DbField.GetStr(dt.Rows(i),"CreateDate")%></td>
		<td><%=DbField.GetStr(dt.Rows(i),"Comments")%></td>
	</tr>
	<%Next%>
</table>
</form>
</body>
</html>