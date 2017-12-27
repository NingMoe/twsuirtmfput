Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceLogExtension
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
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        txtResName.ReadOnly = True

        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol1, True, True, True, datRes.LogColumn1) '初始化DropDownList中字段列表项
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol2, True, True, True, datRes.LogColumn2) '初始化DropDownList中字段列表项
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol3, True, True, True, datRes.LogColumn3) '初始化DropDownList中字段列表项
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol4, True, True, True, datRes.LogColumn4) '初始化DropDownList中字段列表项
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol5, True, True, True, datRes.LogColumn5) '初始化DropDownList中字段列表项
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol6, True, True, True, datRes.LogColumn6) '初始化DropDownList中字段列表项
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
        If CmsFunc.IsEnable("FUNC_LOG_EXTCOLUMN") = False Then
            btnConfirm.Enabled = False
            PromptMsg("当前系统不支持日志扩展功能")
        End If
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            ResFactory.ResService().SaveLogColumn(CmsPass, VLng("PAGE_RESID"), ddlCol1.SelectedValue, ddlCol2.SelectedValue, ddlCol3.SelectedValue, ddlCol4.SelectedValue, ddlCol5.SelectedValue, ddlCol6.SelectedValue)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            ddlCol1.SelectedValue = ""
            ddlCol2.SelectedValue = ""
            ddlCol3.SelectedValue = ""
            ddlCol4.SelectedValue = ""
            ddlCol5.SelectedValue = ""
            ddlCol6.SelectedValue = ""
            ResFactory.ResService().SaveLogColumn(CmsPass, VLng("PAGE_RESID"), ddlCol1.SelectedValue, ddlCol2.SelectedValue, ddlCol3.SelectedValue, ddlCol4.SelectedValue, ddlCol5.SelectedValue, ddlCol6.SelectedValue)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
