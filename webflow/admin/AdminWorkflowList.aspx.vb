Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class AdminWorkflowList
    Inherits AdminPageBase

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
        Dim CategoryId As Long = CLng(Request.QueryString("GroupID"))
        Me.FlowRepeater.DataSource = PermissionUtility.GetPermissionWorkflowItems(MyBase.CurrentUser.Code, CategoryId)
        FlowRepeater.DataBind()
    End Sub

    Protected Sub GenerateWorkflowInstProfile(ByVal WorkflowId As Long, ByRef ActivityAmount As Integer, ByRef PauseAmount As Integer, ByRef FinishAmount As Integer)
        Dim strSql As String = "SELECT count(*) AS CT,TaskStatus FROM WF_INSTANCE WHERE FLOWID=" & WorkflowId & " GROUP BY TaskStatus"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            If CInt(dt.Rows(i)("TaskStatus")) = TaskStatusConstants.Actived Then
                ActivityAmount = CInt(dt.Rows(i)("CT"))
            ElseIf CInt(dt.Rows(i)("TaskStatus")) = TaskStatusConstants.Finished Then
                FinishAmount = CInt(dt.Rows(i)("CT"))
            ElseIf CInt(dt.Rows(i)("TaskStatus")) = TaskStatusConstants.Paused Then
                PauseAmount = CInt(dt.Rows(i)("CT"))
            End If
        Next
    End Sub

    Private Sub FlowRepeater_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles FlowRepeater.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim WorkflowId As Long = 0
            Dim ActivityAmount As Integer = 0
            Dim PauseAmount As Integer = 0
            Dim FinishAmount As Integer = 0

            WorkflowId = CLng(CType(e.Item.DataItem, DataRowView)("Id"))
            GenerateWorkflowInstProfile(WorkflowId, ActivityAmount, PauseAmount, FinishAmount)

            If Not e.Item.FindControl("lnkActivityAmount") Is Nothing Then
                Dim lnkActivityAmount As System.Web.UI.WebControls.HyperLink = CType(e.Item.FindControl("lnkActivityAmount"), System.Web.UI.WebControls.HyperLink)
                lnkActivityAmount.Text = ActivityAmount.ToString()
                lnkActivityAmount.NavigateUrl = "AdminWorkflowInstances.aspx?action=active&WorkflowId=" & WorkflowId
            End If

            If Not e.Item.FindControl("lnkPauseAmount") Is Nothing Then
                Dim lnkPauseAmount As System.Web.UI.WebControls.HyperLink = CType(e.Item.FindControl("lnkPauseAmount"), System.Web.UI.WebControls.HyperLink)
                lnkPauseAmount.Text = PauseAmount.ToString()
                lnkPauseAmount.NavigateUrl = "AdminWorkflowInstances.aspx?action=pause&WorkflowId=" & WorkflowId
            End If

            If Not e.Item.FindControl("lnkPauseAmount") Is Nothing Then
                Dim lnkFinishAmount As System.Web.UI.WebControls.HyperLink = CType(e.Item.FindControl("lnkFinishAmount"), System.Web.UI.WebControls.HyperLink)
                lnkFinishAmount.Text = FinishAmount.ToString()
                lnkFinishAmount.NavigateUrl = "AdminWorkflowInstances.aspx?action=finish&WorkflowId=" & WorkflowId
            End If

            If Not e.Item.FindControl("lnkAduitAmount") Is Nothing Then
                Dim lnkAduitAmount As System.Web.UI.WebControls.HyperLink = CType(e.Item.FindControl("lnkAduitAmount"), System.Web.UI.WebControls.HyperLink)
                lnkAduitAmount.Text = Worklist.GetWorklistItems(MyBase.CurrentUser.Code, "WorkflowInstState=" & WorkflowStatus.Active & " and FlowID=" & WorkflowId & " and ID in (select U.ID from WF_TASK t join WF_USERTASK U on T.ID=U.TASKID and NodeName<>'开始')").Rows.Count.ToString()
                lnkAduitAmount.NavigateUrl = "ActivityList.aspx?action=finish&WorkflowId=" & WorkflowId
            End If

        End If
    End Sub

End Class

End Namespace
