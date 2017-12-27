Imports Unionsoft.Platform
Imports NetReusables
Imports Unionsoft.Cms.Web

Partial Class exdtc_transfer
    Inherits System.Web.UI.Page

    Private fromresid As String
    Private toresid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim hst As Hashtable = New Hashtable
        Dim tablename As String, strSql As String

        fromresid = Request.QueryString("FromResid")

        Dim sql As String = "select id from cms_resource where res_table='WORKFLOW_FORM_Document' and res_type=0"
        toresid = SDbStatement.Query(sql).Tables(0).Rows(0)(0).ToString

        Dim fromRecid As String = Request.QueryString("FromRecid")
        Dim ToFileNum As String = Request.QueryString("ToFileNum")

        Dim dtRelation As DataTable = SDbStatement.Query("SELECT * FROM CMS_DTC_RELATION WHERE FromResId=" & fromresid).Tables(0)
      

        tablename = ResFactory.ResService.GetResTable(CmsPassport.GenerateCmsPassportForInnerUse(""), fromresid)
        strSql = "SELECT * FROM " & tablename + " where id=" + fromRecid
        Dim dtFrom As DataTable = SDbStatement.Query(strSql).Tables(0)
      
        hst.Clear()
        For j As Integer = 0 To dtRelation.Rows.Count - 1
            hst.Add(Trim(dtRelation.Rows(j)("ToFieldName")), dtFrom.Rows(0)(Trim(dtRelation.Rows(j)("FromFieldName"))))
        Next
        hst.Add("RESID", toresid)
        hst.Add("ID", TimeId.GetCurrentMilliseconds())
        hst.Add("FileNum", ToFileNum)
        Try
            SDbStatement.InsertRow(hst, "WORKFLOW_FORM_Document")
            SDbStatement.Execute("delete " + tablename + " where id=" + fromRecid)

            Response.Write("成功")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        ' Next
    End Sub

    Private Sub DebugWrite(ByVal s As String)
        Response.Write(s)
        Response.Write("<br>")
    End Sub

End Class
