Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class MTableSearchResult2Column
    Inherits Cms.Web.MTableSearchResultBase

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
        '显示报表表单
        Dim lngRecNumber As Long = GridDataBind(DataGrid1)

        Dim strQueryTables As String = VStr("PAGE_QUERY_TABLES")
        Dim strQueryWhere As String = VStr("PAGE_QUERY_WHERE")

        Dim datRptTable As DataReportTable = CmsReportTable.GetReportTable(CmsPass, VLng("PAGE_MTSHOSTID"))
        If Not datRptTable Is Nothing Then
            lblHeader.Text = datRptTable.strHeader.Replace(Environment.NewLine, "<br>")
            lblTail.Text = datRptTable.strTail.Replace(Environment.NewLine, "<br>")
            lblF1.Text = datRptTable.strF1Name
            If lblF1.Text <> "" Then
                lblF1Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF1Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF1.Height = Unit.Pixel(24)
                lblF1Val.Height = Unit.Pixel(24)
            End If
            lblF2.Text = datRptTable.strF2Name
            If lblF2.Text <> "" Then
                lblF2Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF2Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF2.Height = Unit.Pixel(24)
                lblF2Val.Height = Unit.Pixel(24)
            End If
            lblF3.Text = datRptTable.strF3Name
            If lblF3.Text <> "" Then
                lblF3Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF3Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF3.Height = Unit.Pixel(24)
                lblF3Val.Height = Unit.Pixel(24)
            End If
            lblF4.Text = datRptTable.strF4Name
            If lblF4.Text <> "" Then
                lblF4Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF4Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF4.Height = Unit.Pixel(24)
                lblF4Val.Height = Unit.Pixel(24)
            End If
            lblF5.Text = datRptTable.strF5Name
            If lblF5.Text <> "" Then
                lblF5Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF5Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF5.Height = Unit.Pixel(24)
                lblF5Val.Height = Unit.Pixel(24)
            End If
            lblF6.Text = datRptTable.strF6Name
            If lblF6.Text <> "" Then
                lblF6Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF6Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF6.Height = Unit.Pixel(24)
                lblF6Val.Height = Unit.Pixel(24)
            End If
            lblF7.Text = datRptTable.strF7Name
            If lblF7.Text <> "" Then
                lblF7Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF7Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF7.Height = Unit.Pixel(24)
                lblF7Val.Height = Unit.Pixel(24)
            End If
            lblF8.Text = datRptTable.strF8Name
            If lblF8.Text <> "" Then
                lblF8Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF8Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF8.Height = Unit.Pixel(24)
                lblF8Val.Height = Unit.Pixel(24)
            End If
            lblF9.Text = datRptTable.strF9Name
            If lblF9.Text <> "" Then
                lblF9Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF9Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF9.Height = Unit.Pixel(24)
                lblF9Val.Height = Unit.Pixel(24)
            End If
            lblF10.Text = datRptTable.strF10Name
            If lblF10.Text <> "" Then
                lblF10Val.Text = CmsReportTable.FilterFieldValue(CmsPass, datRptTable.strF10Val, lngRecNumber, strQueryTables, strQueryWhere)
                lblF10.Height = Unit.Pixel(24)
                lblF10Val.Height = Unit.Pixel(24)
            End If
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

            WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
            CreateDataGridColumn(DataGrid1)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
