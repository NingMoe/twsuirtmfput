Imports Unionsoft.Workflow
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform
Imports NetReusables
Imports System.Data


Namespace Unionsoft.Workflow.Web




Partial Class ActivityList
    Inherits UserPageBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents ActiveFlows As System.Web.UI.WebControls.DataGrid

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private FlowID As Long
    Public dt As New DataTable
    Protected PageSize As Integer = 16
    Protected PageIndex As Integer = 0
    Protected PageCount As Integer = 0
    Protected RowCount As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        FlowID = CLng(Request.QueryString("WorkflowId"))
        If Me.IsPostBack Then Return
        ViewState("PageIndex") = 0
        BindData()
    End Sub
    Private Sub BindData()
        PageIndex = Convert.ToInt32(ViewState("PageIndex"))

        dt = Worklist.GetWorklistItems(MyBase.CurrentUser.Code, "WorkflowInstState=" & WorkflowStatus.Active & " and FlowID=" & FlowID & " and ID in (select U.ID from WF_TASK t join WF_USERTASK U on T.ID=U.TASKID and NodeName<>'开始')")

        RowCount = dt.Rows.Count
        If RowCount Mod PageSize <> 0 And RowCount > PageSize Then
            PageCount = Convert.ToInt32(RowCount / PageSize) + 1
        Else
            PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(RowCount / PageSize)))
        End If
        ViewState("PageCount") = PageCount
    End Sub

    Protected Sub LinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PageCount = Convert.ToInt32(ViewState("PageCount"))
        PageIndex = Convert.ToInt32(ViewState("PageIndex"))
        Dim oSrc As LinkButton = CType(sender, LinkButton)

        Select Case (oSrc.CommandArgument)

        Case "first"
                PageIndex = 0
            Case "previous"
                If (PageIndex > 0) Then PageIndex = PageIndex - 1
            case "next"
                If (PageIndex < PageCount - 1) Then PageIndex = PageIndex + 1
            case "last"
                PageIndex = PageCount - 1
        End Select
        ViewState("PageIndex") = PageIndex
        BindData()
    End Sub
End Class

End Namespace
