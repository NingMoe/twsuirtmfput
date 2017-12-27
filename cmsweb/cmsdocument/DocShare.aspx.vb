Option Strict On
Option Explicit On 

Imports System.Data
Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web



Partial Class DocShare
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgShare As System.Web.UI.WebControls.DataGrid

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Protected dtDocumentCenter as New DataTable 

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindData()
    End Sub

    Protected Sub BindData()
        dtDocumentCenter = ResFactory.TableService("DOC").GetShareRecord(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_RECID"))
        For i As Integer = 0 To dtDocumentCenter.Rows.Count - 1
            DbField.GetStr(dtDocumentCenter.Rows(i), "")
        Next

        'dgShare.DataSource = dtDocumentCenter
        'dgShare.DataBind()
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_RECID") = 0 Then
            ViewState("PAGE_RECID") = RLng("mnurecid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnChooseRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseRes.Click
        Dim lngDestResID As Long = RLng("noderesid")
        If lngDestResID = 0 Or lngDestResID = VLng("PAGE_RESID") Then
                PromptMsg("请选择正确的文档共享目标资源/Please select the correct document sharing target resources！")
            Return
        End If

        If CmsPass.GetDataRes(lngDestResID).ResTableType <> "DOC" Then
                PromptMsg("文档不能被共享至非文档管理表单中/The document can not be shared to non-document management in the form！")
            Return
        End If

        Try
            ResFactory.TableService("DOC").ShareRecord(CmsPass, VLng("PAGE_RESID"), lngDestResID, VStr("PAGE_RECID"))
            Dim strUrl As String = VStr("PAGE_BACKPAGE")
            strUrl = CmsUrl.AppendParam(strUrl, "noderesid=" & VLng("PAGE_RESID"))
            Response.Redirect(strUrl, False)
        Catch ex As Exception
                PromptMsg("无法共享文档，请稍后再试/Unable to share documents, please try again later！", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnCancelShare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelShare.Click
        ResFactory.TableService("DOC").CancelShare(CmsPass, Convert.ToInt64(txtDocID.Text.Trim()), txtColumnName.Text.Trim(), Convert.ToInt64(txtShareRecordID.Text.Trim()))
        BindData()
    End Sub
End Class
End Namespace
