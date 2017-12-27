Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ChartList
        Inherits Unionsoft.Platform.CmsPage

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

    Dim dt As DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�ڴ˴����ó�ʼ��ҳ���û�����
        'If Not Page.IsPostBack Then
        GridDataBind()
        'End If
    End Sub
    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            WebUtilities.InitialDataGrid(DataGrid1) '��ʼ��DataGrid����
            'CreateDataGridColumn()
        Catch ex As Exception
            SLog.Err("�����б���ʾ�쳣����", ex)
        End Try
    End Sub

    Private Sub GridDataBind()
        dt = ChartBasicCode.GetChartDefineList(RStr("mnuresid"))
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()

    End Sub
    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            Dim lngChartID As Long = CLng(e.Item.Cells(0).Text)
            CmsDbStatement.DelRows(NetReusables.SDbConnectionPool.GetDbConfig(), ChartBasicCode.ChartDefineTable, "ID=" & lngChartID)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
        GridDataBind()
    End Sub
    Private Sub DataGrid1_SelectCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim lngChartID As Long = CLng(e.Item.Cells(0).Text)
        If e.CommandName.ToLower() = "select" Then

            Response.Redirect("ChartDefine.aspx?mnuresid=" & RStr("mnuresid") & "&chartid=" & lngChartID)
        ElseIf e.CommandName.ToLower() = "view" Then

            Response.Redirect("Chartview.aspx?chartid=" & lngChartID)

        End If
    End Sub

    Private Sub btnAddCond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCond.Click
        Response.Redirect("ChartDefine.aspx?mnuresid=" & RStr("mnuresid"))
    End Sub
End Class

End Namespace
