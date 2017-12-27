Option Strict On
Option Explicit On 

Imports NetReusables

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvanceSetting
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label

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

        '提取字段的高级设置选项，是选择项、或者自动编码...，并转到与高级选项对应的也没
        LoadFieldAdvSettingOptions()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim strOptVal As String = ddlFieldAdvType.SelectedItem.Value
        Select Case CLng(strOptVal)
            Case FieldValueType.Input
                CTableColInput.SetInputType(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)

            Case FieldValueType.Constant
                Session("CMSBP_FieldAdvConstant") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvConstant.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.DefaultValue
                Session("CMSBP_FieldAdvDefaultValue") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvDefaultValue.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.OptionValue
                Session("CMSBP_FieldAdvOption") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvOption.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.RadioGroup
                Session("CMSBP_FieldAdvRadio") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvRadio.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.Checkbox
                Session("CMSBP_FieldAdvCheckbox") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvCheckbox.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.AutoCoding
                Session("CMSBP_FieldAdvAutoCoding") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvAutoCoding.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.AdvDictionary
                Session("CMSBP_FieldAdvDictionary") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.Calculation
                Session("CMSBP_FieldAdvCalculation") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME") & "&urlfmltype=" & FormulaType.IsCalculation, False)

            Case FieldValueType.CustomizeCoding
                Session("CMSBP_FieldAdvCustomizeCoding") = VStr("PAGE_BACKPAGE")
                Response.Redirect("/cmsweb/adminres/FieldAdvCustomizeCoding.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME"), False)

            Case FieldValueType.IncrementalCoding
                CTableColIncrementalCoding.SetCoding(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)

            Case FieldValueType.DirectoryFile
                Dim strFolder As String = "\cmsdoc\client\" & CmsConfig.GetClientCode & "\" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName & "_" & VLng("PAGE_RESID") & "\"
                CTableColDirectoryFile.Save(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), strFolder)
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)

            Case FieldValueType.SystemAccount
                CTableColSystemAccount.SetSystemAccount(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        End Select
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------
    '初始化DropDownList中字段列表项
    '-----------------------------------------------------------
    Private Sub LoadFieldAdvSettingOptions()
        ddlFieldAdvType.Items.Clear()

        Dim li As ListItem
        Dim alistValTypes As ArrayList = CTableStructure.GetFieldValTypes(VLng("PAGE_RESID"), CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTableType)
        Dim strValTypeDispName As String
        For Each strValTypeDispName In alistValTypes
            li = New ListItem(strValTypeDispName, CStr(CTableStructure.GetValTypeDefine(strValTypeDispName)))
            ddlFieldAdvType.Items.Add(li)
            li = Nothing
        Next
    End Sub
End Class

End Namespace
