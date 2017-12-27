Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class MTableSearchResultSimple
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
        GridDataBind(DataGrid1)
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If

            If CmsReportTable.Is4FieldReportTable(CmsPass, RLng("mtshostid")) Then
                '�Ǻ�4�ֶα�ͷ��β��ͳ�Ʊ�
                Response.Redirect("/cmsweb/adminres/MTableSearchResult2Column.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & RLng("mtshostid"), True)
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
