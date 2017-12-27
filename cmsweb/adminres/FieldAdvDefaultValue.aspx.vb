Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvDefaultValue
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

        LoadDefaultValues() 'Load所有默认值
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            CTableColDefaultValue.SetDefaultVal(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), DropDownList1.SelectedValue)

            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------------------
    'Load所有默认值
    '------------------------------------------------------------------
    Private Sub LoadDefaultValues()
        If DropDownList1.Items.Count <= 0 Then
            Dim alistDefVal As ArrayList = CTableStructure.GetDefaultValNames()
            Dim en As IEnumerator = alistDefVal.GetEnumerator()
            Do While en.MoveNext
                DropDownList1.Items.Add(CType(en.Current, String))
            Loop
        End If

        Dim strOldVal As String = CTableColDefaultValue.GetDefaultVal(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        Try
            If strOldVal <> "" Then DropDownList1.SelectedValue = strOldVal
        Catch ex As Exception
            '可能系统默认值改了，不必记录错误
        End Try
    End Sub
End Class

End Namespace
