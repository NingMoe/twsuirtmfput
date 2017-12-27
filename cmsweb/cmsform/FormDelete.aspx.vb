Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FormDelete
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VStr("PAGE_FORMNAME") = "" Then
            ViewState("PAGE_FORMNAME") = RStr("mnuformname")
        End If
        If VLng("PAGE_FORMTYPE") = 0 Then
            ViewState("PAGE_FORMTYPE") = RLng("mnuformtype")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResult.Attributes.Add("align", "center")

        '删除窗体
        CTableForm.DelDesignedForms(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_FORMNAME"), VLng("PAGE_FORMTYPE"))
        lblResult.Text = "删除窗体设计成功！"

        If Not IsStartupScriptRegistered("startup") Then
            Dim strScript As String = "<script language=""javascript"">window.returnValue = '$SUCCESS';</script>"
            RegisterStartupScript("startup", strScript)
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
