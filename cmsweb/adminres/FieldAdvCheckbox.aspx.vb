Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCheckbox
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

        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblFieldName.Text = VStr("PAGE_COLDISPNAME")

        txtOptionDesc.Text = CTableStructure.GetColValue(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        txtYes.Text = CTableStructure.GetFieldStr(CmsPass, VLng("PAGE_RESID"), "CD_STR3", VStr("PAGE_COLNAME"))
        If txtYes.Text = "" Then txtYes.Text = "1"
        txtNo.Text = CTableStructure.GetFieldStr(CmsPass, VLng("PAGE_RESID"), "CD_STR4", VStr("PAGE_COLNAME"))
        If txtNo.Text = "" Then txtNo.Text = "0"
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        'If txtOptionDesc.Text.Trim() = "" Then
        '    PromptMsg("请输入有效的选项描述信息")
        '    Return
        'End If
        If txtYes.Text.Trim() = "" Or txtNo.Text.Trim() = "" Then
            PromptMsg("请输入有效的选项选中和不选中时的保存值")
            Return
        End If
        Try
            CTableStructure.SetColumnDefinition(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), txtOptionDesc.Text.Trim(), FieldValueType.Checkbox, , , , txtYes.Text.Trim(), txtNo.Text.Trim())
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
