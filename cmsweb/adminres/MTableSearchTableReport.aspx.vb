Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class MTableSearchTableReport
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

        If VLng("PAGE_MTSHOSTID") = 0 Then
            ViewState("PAGE_MTSHOSTID") = RLng("mtshostid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '初始化系统常量
        CmsReportTable.InitSysParams(ddlF1)
        CmsReportTable.InitSysParams(ddlF2)
        CmsReportTable.InitSysParams(ddlF3)
        CmsReportTable.InitSysParams(ddlF4)
        CmsReportTable.InitSysParams(ddlF5)
        CmsReportTable.InitSysParams(ddlF6)
        CmsReportTable.InitSysParams(ddlF7)
        CmsReportTable.InitSysParams(ddlF8)
        CmsReportTable.InitSysParams(ddlF9)
        CmsReportTable.InitSysParams(ddlF10)

        '初始化聚合函数列表
        CmsReportTable.InitQueryFunc(ddlFunc1)
        CmsReportTable.InitQueryFunc(ddlFunc2)
        CmsReportTable.InitQueryFunc(ddlFunc3)
        CmsReportTable.InitQueryFunc(ddlFunc4)
        CmsReportTable.InitQueryFunc(ddlFunc5)
        CmsReportTable.InitQueryFunc(ddlFunc6)
        CmsReportTable.InitQueryFunc(ddlFunc7)
        CmsReportTable.InitQueryFunc(ddlFunc8)
        CmsReportTable.InitQueryFunc(ddlFunc9)
        CmsReportTable.InitQueryFunc(ddlFunc10)

        '初始化字段列表
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol1)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol2)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol3)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol4)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol5)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol6)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol7)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol8)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol9)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol10)

        '初始化界面信息
        LoadInitValue()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            '保存设置
            Dim datRptTable As New DataReportTable
            datRptTable.lngMtsHostID = VLng("PAGE_MTSHOSTID")
            datRptTable.lngResID = VLng("PAGE_RESID")
            datRptTable.strHeader = txtHeader.Text.Trim()
            datRptTable.strTail = txtTail.Text.Trim()
            datRptTable.strF1Name = txtF1.Text.Trim()
            datRptTable.strF1Val = CStr(IIf(txtF1Val.Text.Trim() <> "", txtF1Val.Text.Trim(), ddlF1.SelectedValue))
            If datRptTable.strF1Val = "" AndAlso ddlFunc1.SelectedValue <> "" AndAlso ddlCol1.SelectedValue <> "" Then datRptTable.strF1Val = ddlFunc1.SelectedValue & "{" & ddlCol1.SelectedValue & "}"
            datRptTable.strF2Name = txtF2.Text.Trim()
            datRptTable.strF2Val = CStr(IIf(txtF2Val.Text.Trim() <> "", txtF2Val.Text.Trim(), ddlF2.SelectedValue))
            If datRptTable.strF2Val = "" AndAlso ddlFunc2.SelectedValue <> "" AndAlso ddlCol2.SelectedValue <> "" Then datRptTable.strF2Val = ddlFunc2.SelectedValue & "{" & ddlCol2.SelectedValue & "}"
            datRptTable.strF3Name = txtF3.Text.Trim()
            datRptTable.strF3Val = CStr(IIf(txtF3Val.Text.Trim() <> "", txtF3Val.Text.Trim(), ddlF3.SelectedValue))
            If datRptTable.strF3Val = "" AndAlso ddlFunc3.SelectedValue <> "" AndAlso ddlCol3.SelectedValue <> "" Then datRptTable.strF3Val = ddlFunc3.SelectedValue & "{" & ddlCol3.SelectedValue & "}"
            datRptTable.strF4Name = txtF4.Text.Trim()
            datRptTable.strF4Val = CStr(IIf(txtF4Val.Text.Trim() <> "", txtF4Val.Text.Trim(), ddlF4.SelectedValue))
            If datRptTable.strF4Val = "" AndAlso ddlFunc4.SelectedValue <> "" AndAlso ddlCol4.SelectedValue <> "" Then datRptTable.strF4Val = ddlFunc4.SelectedValue & "{" & ddlCol4.SelectedValue & "}"
            datRptTable.strF5Name = txtF5.Text.Trim()
            datRptTable.strF5Val = CStr(IIf(txtF5Val.Text.Trim() <> "", txtF5Val.Text.Trim(), ddlF5.SelectedValue))
            If datRptTable.strF5Val = "" AndAlso ddlFunc5.SelectedValue <> "" AndAlso ddlCol5.SelectedValue <> "" Then datRptTable.strF5Val = ddlFunc5.SelectedValue & "{" & ddlCol5.SelectedValue & "}"
            datRptTable.strF6Name = txtF6.Text.Trim()
            datRptTable.strF6Val = CStr(IIf(txtF6Val.Text.Trim() <> "", txtF6Val.Text.Trim(), ddlF6.SelectedValue))
            If datRptTable.strF6Val = "" AndAlso ddlFunc6.SelectedValue <> "" AndAlso ddlCol6.SelectedValue <> "" Then datRptTable.strF6Val = ddlFunc6.SelectedValue & "{" & ddlCol6.SelectedValue & "}"
            datRptTable.strF7Name = txtF7.Text.Trim()
            datRptTable.strF7Val = CStr(IIf(txtF7Val.Text.Trim() <> "", txtF7Val.Text.Trim(), ddlF7.SelectedValue))
            If datRptTable.strF7Val = "" AndAlso ddlFunc7.SelectedValue <> "" AndAlso ddlCol7.SelectedValue <> "" Then datRptTable.strF7Val = ddlFunc7.SelectedValue & "{" & ddlCol7.SelectedValue & "}"
            datRptTable.strF8Name = txtF8.Text.Trim()
            datRptTable.strF8Val = CStr(IIf(txtF8Val.Text.Trim() <> "", txtF8Val.Text.Trim(), ddlF8.SelectedValue))
            If datRptTable.strF8Val = "" AndAlso ddlFunc8.SelectedValue <> "" AndAlso ddlCol8.SelectedValue <> "" Then datRptTable.strF8Val = ddlFunc8.SelectedValue & "{" & ddlCol8.SelectedValue & "}"
            datRptTable.strF9Name = txtF9.Text.Trim()
            datRptTable.strF9Val = CStr(IIf(txtF9Val.Text.Trim() <> "", txtF9Val.Text.Trim(), ddlF9.SelectedValue))
            If datRptTable.strF9Val = "" AndAlso ddlFunc9.SelectedValue <> "" AndAlso ddlCol9.SelectedValue <> "" Then datRptTable.strF9Val = ddlFunc9.SelectedValue & "{" & ddlCol9.SelectedValue & "}"
            datRptTable.strF10Name = txtF10.Text.Trim()
            datRptTable.strF10Val = CStr(IIf(txtF10Val.Text.Trim() <> "", txtF10Val.Text.Trim(), ddlF10.SelectedValue))
            If datRptTable.strF10Val = "" AndAlso ddlFunc10.SelectedValue <> "" AndAlso ddlCol10.SelectedValue <> "" Then datRptTable.strF10Val = ddlFunc10.SelectedValue & "{" & ddlCol10.SelectedValue & "}"
            datRptTable.RowHeight = Convert.ToInt32(txtRowHeight.text)
            datRptTable.RowCount = Convert.ToInt32(txtRowCount.text)

            CmsReportTable.AddOrEditReportSettings(CmsPass, datRptTable)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '从数据库中清除设置
        CmsReportTable.DelReportTable(CmsPass, VLng("PAGE_MTSHOSTID"))

        '初始化界面信息
        LoadInitValue()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------------------------------------
    '初始化界面信息
    '------------------------------------------------------------------------------------
    Private Sub LoadInitValue()
        Dim datRptTable As DataReportTable = CmsReportTable.GetReportTable(CmsPass, VLng("PAGE_MTSHOSTID"))
        If Not datRptTable Is Nothing Then
            txtHeader.Text = datRptTable.strHeader
            txtTail.Text = datRptTable.strTail
            txtF1.Text = datRptTable.strF1Name
            CmsReportTable.Load4FieldValue(datRptTable.strF1Val, txtF1Val, ddlF1, ddlFunc1, ddlCol1)
            txtF2.Text = datRptTable.strF2Name
            CmsReportTable.Load4FieldValue(datRptTable.strF2Val, txtF2Val, ddlF2, ddlFunc2, ddlCol2)
            txtF3.Text = datRptTable.strF3Name
            CmsReportTable.Load4FieldValue(datRptTable.strF3Val, txtF3Val, ddlF3, ddlFunc3, ddlCol3)
            txtF4.Text = datRptTable.strF4Name
            CmsReportTable.Load4FieldValue(datRptTable.strF4Val, txtF4Val, ddlF4, ddlFunc4, ddlCol4)
            txtF5.Text = datRptTable.strF5Name
            CmsReportTable.Load4FieldValue(datRptTable.strF5Val, txtF5Val, ddlF5, ddlFunc5, ddlCol5)
            txtF6.Text = datRptTable.strF6Name
            CmsReportTable.Load4FieldValue(datRptTable.strF6Val, txtF6Val, ddlF6, ddlFunc6, ddlCol6)
            txtF7.Text = datRptTable.strF7Name
            CmsReportTable.Load4FieldValue(datRptTable.strF7Val, txtF7Val, ddlF7, ddlFunc7, ddlCol7)
            txtF8.Text = datRptTable.strF8Name
            CmsReportTable.Load4FieldValue(datRptTable.strF8Val, txtF8Val, ddlF8, ddlFunc8, ddlCol8)
            txtF9.Text = datRptTable.strF9Name
            CmsReportTable.Load4FieldValue(datRptTable.strF9Val, txtF9Val, ddlF9, ddlFunc9, ddlCol9)
            txtF10.Text = datRptTable.strF10Name
            CmsReportTable.Load4FieldValue(datRptTable.strF10Val, txtF10Val, ddlF10, ddlFunc10, ddlCol10)
            txtRowHeight.Text = datRptTable.RowHeight.ToString()
            txtRowCount.Text = datRptTable.RowCount.ToString()
        Else
            '清空界面信息
            txtHeader.Text = ""
            txtTail.Text = ""
            txtF1.Text = ""
            txtF1Val.Text = ""
            txtF2.Text = ""
            txtF2Val.Text = ""
            txtF3.Text = ""
            txtF3Val.Text = ""
            txtF4.Text = ""
            txtF4Val.Text = ""
            txtF5.Text = ""
            txtF5Val.Text = ""
            txtF6.Text = ""
            txtF6Val.Text = ""
            txtF7.Text = ""
            txtF7Val.Text = ""
            txtF8.Text = ""
            txtF8Val.Text = ""
            txtF9.Text = ""
            txtF9Val.Text = ""
            txtF10.Text = ""
            txtF10Val.Text = ""
            ddlF1.SelectedIndex = 0
            ddlF2.SelectedIndex = 0
            ddlF3.SelectedIndex = 0
            ddlF4.SelectedIndex = 0
            ddlF5.SelectedIndex = 0
            ddlF6.SelectedIndex = 0
            ddlF7.SelectedIndex = 0
            ddlF8.SelectedIndex = 0
            ddlF9.SelectedIndex = 0
            ddlF10.SelectedIndex = 0
            ddlFunc1.SelectedIndex = 0
            ddlFunc2.SelectedIndex = 0
            ddlFunc3.SelectedIndex = 0
            ddlFunc4.SelectedIndex = 0
            ddlFunc5.SelectedIndex = 0
            ddlFunc6.SelectedIndex = 0
            ddlFunc7.SelectedIndex = 0
            ddlFunc8.SelectedIndex = 0
            ddlFunc9.SelectedIndex = 0
            ddlFunc10.SelectedIndex = 0
            ddlCol1.SelectedIndex = 0
            ddlCol2.SelectedIndex = 0
            ddlCol3.SelectedIndex = 0
            ddlCol4.SelectedIndex = 0
            ddlCol5.SelectedIndex = 0
            ddlCol6.SelectedIndex = 0
            ddlCol7.SelectedIndex = 0
            ddlCol8.SelectedIndex = 0
            ddlCol9.SelectedIndex = 0
            ddlCol10.SelectedIndex = 0
        End If
    End Sub
End Class

End Namespace
