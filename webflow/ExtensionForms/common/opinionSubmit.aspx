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

	Dim strSql As String = "select * from  WF_OPINITIONCOLLECTION where Id=" & WorklistItemId
	dt = SDbStatement.Query(strSql).Tables(0)

End Sub

</script>
<form id="Form1" method="post" runat="server">
<fieldset style="background:menu;width:99%">
<table cellspacing="0" cellpadding="0" height="22" width="100%" align="center">
	<tr>
		<td width=80><asp:Button id="btnOK" runat="server" Text="确定" OnClick="OpinionSubmit" style="width:75px;"></asp:Button></td>
		<td width="*"></td>
	</tr>
</table>
</fieldset>
<table cellspacing="0" cellpadding="0" height="22" width="100%" align="center">
	<tr>
		<td><textarea name="OpinionComments" cols="20" rows="13" style="width:99%;"></textarea></td>
	</tr>
</table>
</form>
</body>
</html>