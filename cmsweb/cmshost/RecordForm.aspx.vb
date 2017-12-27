Imports System.Collections
Imports NetReusables
Imports Unionsoft.Platform

Partial Class cmshost_RecordForm
    Inherits CmsPage



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim RecID As Long = RLng("mnurecid")
        Dim ResID As Long = VLng("PAGE_RESID")
        Dim RecordFormUrl As String = "RecordEdit.aspx"
        Dim hst As Hashtable = FormManager.GetRouterForm(CmsPass, ResID, RecID)
        Dim intFormCategory As FormCategory = Unionsoft.Platform.FormCategory.SystemForm
        Dim FORMURL As String = ""
        Dim FormName As String = ""
        If hst IsNot Nothing Then
            For Each en As DictionaryEntry In hst
                If en.Key.ToString.Trim = "FormCategory" Then
                    intFormCategory = CType(CInt(en.Value), FormCategory)
                ElseIf en.Key.ToString.Trim = "FORMURL" Then
                    FORMURL = en.Value.ToString
                ElseIf en.Key.ToString.Trim = "FormName" Then
                    FormName = en.Value.ToString
                End If
            Next
        End If

        If intFormCategory = FormCategory.SystemForm Then
            Response.Redirect("/cmsweb/cmshost/" + RecordFormUrl + "?mnuhostresid=" & Request("mnuhostresid") & "&mnuhostrecid=" + Request("mnuhostrecid") + "&mnuresid=" & VLng("PAGE_RESID").ToString & "&mnurecid=" & RecID.ToString & "&mnuformname=" & Server.UrlEncode(FormName) & "&mnuinmode=" & Request("mnuinmode") & IIf(Request("MenuSection") IsNot Nothing, "&MenuSection=" + Request("MenuSection"), "").ToString & IIf(Request("MenuKey") IsNot Nothing, "&MenuKey=" + Request("MenuKey"), "").ToString & IIf(Request("MNURESLOCATE") IsNot Nothing, "&MNURESLOCATE=" + Request("MNURESLOCATE"), "").ToString & IIf(Request("cmsaction") IsNot Nothing, "&cmsaction=" + Request("cmsaction"), "").ToString & IIf(Request("mnuformresid") IsNot Nothing, "&mnuformresid=" + Request("mnuformresid"), "").ToString & IIf(Request("backpage") IsNot Nothing, "&backpage=" + Request("backpage"), "").ToString)
        Else
            If FORMURL.IndexOf("?") > 0 Then
                FORMURL += "&"
            Else
                FORMURL += "?"
            End If
            FORMURL += "resid=" + VLng("PAGE_RESID").ToString + "&recid=" + RecID.ToString
            Response.Redirect(FORMURL)
        End If
    End Sub

End Class
