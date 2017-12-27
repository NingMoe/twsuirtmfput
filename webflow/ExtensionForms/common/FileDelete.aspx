<%@ Page Language="vb" AutoEventWireup="false" Inherits="System.Web.UI.Page"%>
<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>

<script language="vb" runat="server">
Private _Id As Long = 0


Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load	
	_Id = CLng(Request.QueryString("Id"))
	SDbStatement.Execute("delete from WORKFLOW_FORM_ATTACHMENTS WHERE ID=" & _Id)
End Sub

</script>
