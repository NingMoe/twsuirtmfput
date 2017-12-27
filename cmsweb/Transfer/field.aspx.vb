Imports Unionsoft.Platform
Imports NetReusables
Imports Unionsoft.Cms.Web


Partial Class exdtc_field
    Inherits System.Web.UI.Page

    Protected TableName = "WORKFLOW_FORM_Document"
    Protected ToResid As String
    Private fromresid As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dv As DataView, p As CmsPassport
        fromresid = Request.QueryString("mnuresid")
        Dim sql As String = "select id from cms_resource where res_table='" + TableName + "' and res_type=0"
        ToResid = SDbStatement.Query(sql).Tables(0).Rows(0)(0)
        p = CmsPassport.GenerateCmsPassportForInnerUse("")

        If Not Me.IsPostBack Then
            dv = ResFactory.TableService("TWOD").GetTableColumns(p, fromresid)
            For i As Integer = 0 To dv.Count - 1
                Me.drpDataFrom.Items.Add(New ListItem(dv(i)("CD_DISPNAME"), dv(i)("CD_COLNAME")))
            Next

            dv = ResFactory.TableService("TWOD").GetTableColumns(p, ToResid)
            For i As Integer = 0 To dv.Count - 1
                Me.drpDataTo.Items.Add(New ListItem(dv(i)("CD_DISPNAME"), dv(i)("CD_COLNAME")))
            Next
        Else
            SaveRelation(Request.Form("relation"), Request.Form("relation1"))
        End If

        Me.fieldrelation.Items.Clear()
        Dim dt As DataTable = SDbStatement.Query("SELECT * FROM CMS_DTC_RELATION WHERE FromResId=" & fromresid).Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.fieldrelation.Items.Add(New ListItem(dt.Rows(i)("FromFieldDescription") & " -> " & dt.Rows(i)("ToFieldDescription"), dt.Rows(i)("FromFieldName") & " -> " & dt.Rows(i)("ToFieldName")))
        Next
    End Sub

    Private Sub SaveRelation(ByVal relation As String, ByVal relation1 As String)
        Dim hst As Hashtable = New Hashtable
        Dim r1() As String = Split(relation, "|")
        Dim r2() As String = Split(relation1, "|")

        SDbStatement.Execute("DELETE FROM CMS_DTC_RELATION WHERE FromResId=" & fromresid)
        For i As Integer = 0 To r1.Length - 1
            If Trim(r1(i)) <> "" Then
                hst.Clear()
                hst.Add("FromResId", fromresid)
                hst.Add("FromFieldName", Split(r1(i), "->")(0))
                hst.Add("FromFieldDescription", Split(r2(i), "->")(0))
                hst.Add("ToResId", toresid)
                hst.Add("ToFieldName", Split(r1(i), "->")(1))
                hst.Add("ToFieldDescription", Split(r2(i), "->")(1))

                SDbStatement.InsertRow(hst, "CMS_DTC_RELATION")
            End If
        Next

    End Sub

End Class
