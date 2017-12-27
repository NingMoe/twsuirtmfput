Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class APageTemplate
    Inherits CmsPage


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        'If VStr("PAGE_COLNAME") = "" Then
        '    ViewState("PAGE_COLNAME") = RStr("colname")
        'End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

        Protected Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
                txtResName.ReadOnly = True

                Session("CMSBP_bbb") = "aaa.aspx?mnuresid=" & VStr("PAGE_RESID")
                Response.Redirect("bbb.aspx?mnuresid=" & VStr("PAGE_RESID"), False)
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

        Protected Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        End Sub
End Class

End Namespace
