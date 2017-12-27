Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCustomizeCoding
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

        LoadInitValue() '获取初始值
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim datCustcode As New DataCustCoding
            datCustcode.lngResID = VLng("PAGE_RESID")
            datCustcode.strColName = VStr("PAGE_COLNAME")

            If rdoUrl.Checked Then
                datCustcode.lngType = CustcodingType.PopupWindow
            Else
                datCustcode.lngType = CustcodingType.Javascript
            End If

            datCustcode.strValue1 = txtValue1.Text.Trim()
            datCustcode.strValue2 = txtValue2.Text.Trim()

            If IsNumeric(txtFormLeft.Text) Then datCustcode.lngFormLeft = CLng(txtFormLeft.Text)
            If IsNumeric(txtFormTop.Text) Then datCustcode.lngFormTop = CLng(txtFormTop.Text)
            If IsNumeric(txtFormWidth.Text) Then datCustcode.lngFormWidth = CLng(txtFormWidth.Text)
            If IsNumeric(txtFormHeight.Text) Then datCustcode.lngFormHeight = CLng(txtFormHeight.Text)

            CTableColCustomizeCoding.SetCustCoding(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), datCustcode)

            PromptMsg("定制编码信息保存成功！")
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-------------------------------------------------------------
    'Load初始值
    '-------------------------------------------------------------
    Private Sub LoadInitValue()
        Dim datCustcode As DataCustCoding = CTableColCustomizeCoding.GetCustCoding(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        If Not (datCustcode Is Nothing) Then
            rdoUrl.Checked = True
            If datCustcode.lngType = CustcodingType.PopupWindow Then
                rdoUrl.Checked = True
            ElseIf datCustcode.lngType = CustcodingType.Javascript Then
                rdoJavascript.Checked = True
            End If

            txtValue1.Text = datCustcode.strValue1
            txtValue2.Text = datCustcode.strValue2

            txtFormLeft.Text = CStr(datCustcode.lngFormLeft)
            txtFormTop.Text = CStr(datCustcode.lngFormTop)
            txtFormWidth.Text = CStr(datCustcode.lngFormWidth)
            txtFormHeight.Text = CStr(datCustcode.lngFormHeight)
        End If
    End Sub
End Class

End Namespace
