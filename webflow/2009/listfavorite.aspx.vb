Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class listfavorite
    Inherits UserPageBase

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
        If Me.IsPostBack Then Return

        Me.DataGrid1.DataSource = GenerateQueryDataTable()
        Me.DataGrid1.DataBind()
    End Sub

    Private Function GenerateQueryDataTable() As DataTable
        Return Worklist.GetFavoriteWorklistItems(Me.CurrentUser.Code, "")
    End Function

    Private Sub Pager1_Click(ByVal sender As Object, ByVal eventArgument As String) Handles Pager1.Click
        Select Case eventArgument
            Case "MoveFirstPage"
                DataGrid1.CurrentPageIndex = 0
            Case "MovePreviousPage"
                If DataGrid1.CurrentPageIndex > 0 Then DataGrid1.CurrentPageIndex = DataGrid1.CurrentPageIndex - 1
            Case "MoveNextPage"
                If DataGrid1.CurrentPageIndex < DataGrid1.PageCount - 1 Then DataGrid1.CurrentPageIndex = DataGrid1.CurrentPageIndex + 1
            Case "MoveLastPage"
                DataGrid1.CurrentPageIndex = DataGrid1.PageCount - 1
        End Select

        Me.DataGrid1.DataSource = GenerateQueryDataTable()
        Me.DataGrid1.DataBind()
        Pager1.PageCount = DataGrid1.PageCount
    End Sub

End Class

End Namespace
