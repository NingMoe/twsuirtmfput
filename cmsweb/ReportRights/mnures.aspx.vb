Imports NetReusables
Imports System.Data

Partial Class mnures
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim mnuresid As String = Request.QueryString("mnuresid")
            Dim mnurecid As String = Request.QueryString("mnurecid")

            Dim sqlStr As String = "select name=(select EMP_NAME from CMS_EMPLOYEE where EMP_ID=C.EmpCode) from RP_ReportRight C where Rresid='" + mnuresid + "' and Rrecid='" + mnurecid + "'"
            Dim ds As DataSet = SDbStatement.Query(sqlStr)
            Me.DataMan.DataSource = ds
            Me.DataMan.DataBind()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim mnuresid As String = Request.QueryString("mnuresid")
        Dim mnurecid As String = Request.QueryString("mnurecid")

        Response.Redirect("userSelect.aspx?mnuresid=" + mnuresid + "&mnurecid=" + mnurecid)
    End Sub
End Class
