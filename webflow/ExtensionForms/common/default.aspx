<%@ Page Language="vb" AutoEventWireup="false" Inherits="System.Web.UI.Page"%>
<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>

<script language="vb" runat="server">
Private _Id As Long = 0
private _action As String

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load	
	Dim dt As DataTable,filename As String
	_Id = CLng(Request.QueryString("id"))
	_action = Request.QueryString("action")
	
	dt = SDbStatement.Query("select FileName from workflow_form_attachments where id=" & _Id).Tables(0)
	filename = DbFIeld.GetStr(dt.Rows(0),"filename")
	If _action="edit" Then
		If Right(filename,3) = "doc" Or  Right(filename,3)="xls" Then
			Response.Redirect("OfficeEditor.aspx?id=" & _Id)
		Else
			Response.Redirect("FileDownload.aspx?id=" & _Id)
		End If
	Else
		Response.Redirect("FileDownload.aspx?id=" & _Id)
	End If
End Sub

</script>
