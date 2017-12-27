Imports Unionsoft.Platform
Imports Unionsoft.Implement

Partial Class cmsform_FormDirect
    Inherits CmsPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lngResID As Long = Convert.ToInt64(Request("mnuresid"))
        Dim FormType As Integer = Convert.ToInt32(Request("mnuformtype"))

        Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)


        If datRes.ResType = ResInheritType.IsIndependent Then
            Response.Redirect("/cmsweb/cmsform/FormDesign.aspx?mnuresid=" & lngResID & "&mnuformtype=" & FormType.ToString & "&mnuformname=&backpage=" & Request("backpage"), False)
        Else
            Response.Redirect("/cmsweb/cmsform/InheritFormDesign.aspx?mnuresid=" & lngResID & "&mnuformtype=" & FormType.ToString & "&mnuformname=&backpage=" & Request("backpage"), False)
        End If


    End Sub
End Class
