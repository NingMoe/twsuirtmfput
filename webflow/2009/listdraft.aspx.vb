Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class listdraft
    Inherits UserPageBase

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
        If Me.IsPostBack Then Return

        Me.DataGrid1.DataSource = GenerateQueryDataTable()
        Me.DataGrid1.DataBind()
    End Sub

    Private Function GenerateQueryDataTable() As DataTable
        Return Worklist.GetDraftWorklistItems(Me.CurrentUser.Code, "")
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
        Pager1.CurrentPage = DataGrid1.CurrentPageIndex
        Pager1.PageCount = DataGrid1.PageCount
    End Sub

    Private Sub DataGrid1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        WorkflowManager.DeleteWorkflowInstance(CStr(e.CommandArgument))
        Me.DataGrid1.DataSource = GenerateQueryDataTable()
        Me.DataGrid1.DataBind()
        Pager1.CurrentPage = DataGrid1.CurrentPageIndex
        Pager1.PageCount = DataGrid1.PageCount
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        e.Item.Attributes.Add("onclick", "tbllistColorClear(this)")
    End Sub

End Class

End Namespace
