Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocSearchFullDb
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If CmsFunc.IsEnable("FUNC_TABLETYPE_DOC") = True AndAlso CmsFunc.IsEnable("FUNC_FULLTEXT_SEARCH") = True Then
            txtDocContent.Enabled = True
        Else
            txtDocContent.Enabled = False
            txtDocContent.Text = "(当前版本不支持全文检索)"
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strDocName As String = txtDocName.Text.Trim()
            Dim strDocExt As String = txtDocExt.Text.Trim()
            Dim strDocKeyword As String = txtDocKeyword.Text.Trim()
            Dim strDocComments As String = txtDocComments.Text.Trim()
            Dim strDocContent As String = txtDocContent.Text.Trim()
            If strDocName = "" AndAlso strDocExt = "" AndAlso strDocKeyword = "" AndAlso strDocComments = "" AndAlso strDocContent = "" Then
                    PromptMsg("请至少输入一项查询信息/Please enter at least one query information。")
                Return
            End If
            MenuRepeater.DataSource = DocSearch.GetDatasetOfDocFullDbSearch(CmsPass, strDocName, strDocExt, strDocKeyword, strDocComments, strDocContent).Tables(0)
            MenuRepeater.DataBind()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
