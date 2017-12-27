Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class MTableSearchResult2Column
    Inherits Cms.Web.MTableSearchResultBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
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
        '��ʾ�����
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
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
            CreateDataGridColumn(DataGrid1)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
