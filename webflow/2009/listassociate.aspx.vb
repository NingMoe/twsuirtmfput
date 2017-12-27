Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class listassociate
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
        txtkeyvalue.Attributes.Add("onkeydown", "return execSearch()")

        If Me.IsPostBack Then Return
        BindData()
    End Sub

    Private Sub BindData()
        Dim strCondition As String = ""
        Dim strValue As String = "FlowName LIKE '%{0}%' OR MAINFIELDVALUE LIKE '%{0}%' OR CREATORNAME LIKE '%{0}%' OR CreateTime LIKE '%{0}%' "
        If Me.txtkeyvalue.Text.Trim() <> "" Then
            strCondition = String.Format(strValue, Me.txtkeyvalue.Text.Trim())
        End If
        Me.DataGrid1.DataSource = Worklist.GetAssociateWorklistItems(Me.CurrentUser.Code, strCondition)
        Me.DataGrid1.DataBind()

        Pager1.CurrentPage = DataGrid1.CurrentPageIndex
        Pager1.PageCount = DataGrid1.PageCount
    End Sub

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

        BindData()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        BindData()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        e.Item.Attributes.Add("onclick", "tbllistColorClear(this)")
    End Sub

End Class

End Namespace
