Imports Unionsoft.Platform

Partial Class cmsdocument_DocViewCAD
    Inherits System.Web.UI.Page
    Public cmsurldoc As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DocID As String = Request("DOCID")

        Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(DocID)

       cmsurldoc = CMSDocument.CreateDocumentOnServer(datDoc)
    End Sub
End Class
