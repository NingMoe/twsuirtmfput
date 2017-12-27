<%@ Page Language="vb" AutoEventWireup="false" Inherits="System.Web.UI.Page"%>
<%@ Import NameSpace="Unionsoft.Workflow.Platform" %>
<%@ Import NameSpace="Unionsoft.WebControls.Uploader" %>
<%@ Import Namespace="System.Data"%>
<%@ import namespace="NetReusables"%>

<script language="vb" runat="server">
Private id As String
Private filename As String

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	id = Request.QueryString("id")
	
	Dim strSql As String
	Dim dt As DataTable
	strSql = "SELECT * FROM WORKFLOW_FORM_ATTACHMENTS WHERE ID=" & Id
	dt =  SDbStatement.Query(strSql).Tables(0)
	If dt.Rows.Count > 0 Then
		Response.AddHeader("Content-Disposition", "inline;filename=" & DbField.GetStr(dt.Rows(0),"FileName"))
		Response.ContentType = "application/octet-stream"
		Response.BinaryWrite(CType(DbField.GetObj(dt.Rows(0),"FileImage"),byte()))
		Response.Flush()
	End If
	Response.End()
End Sub

</script>
