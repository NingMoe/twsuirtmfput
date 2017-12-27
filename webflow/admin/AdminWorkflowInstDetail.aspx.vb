Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web



Partial Class AdminWorkflowInstDetail
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

    Dim strWorkflowInstId As String


    '-------------------------------------------------------------------------------   
    ' 目的          : '当前流程图的显示类型
    ' 传入参数      : 
    ' 传出参数      : 
    ' Author        : CHENYU   Date　: 2006-4-3
    '-------------------------------------------------------------------------------   
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Expires = 0
        Response.Buffer = False

        strWorkflowInstId = Request.QueryString("WorkflowId")
        Dim oWorkflowInst As WorkflowInstance = WorkflowManager.GetWorkflowInstance(strWorkflowInstId)

        Me.btnRollBack.Attributes.Add("onClick", "return window.confirm('确实要将流程回退至前一环节吗?')")
        Me.btnFinish.Attributes.Add("onClick", "return window.confirm('确实要将设置流程为结束吗?')")
        Me.btnDelete.Attributes.Add("onClick", "return window.confirm('确实要删除该流程实例吗?')")

        If oWorkflowInst.Activities.Count > 0 AndAlso oWorkflowInst.Activities(0).WorklistItems.Count > 0 Then
            Me.btnDisplayForm.Attributes.Add("onClick", "window.open('../process/director.aspx?action=view&WorkflowInstId=" & strWorkflowInstId & "&WorklistItemId=" & oWorkflowInst.Activities(0).WorklistItems(0).Key & "');return false;")
        Else
            Me.btnDisplayForm.Enabled = False
        End If

        If Not Me.IsPostBack Then CtlFlowDiagram1.GenerateWorkflowInstancePicture(CLng(strWorkflowInstId))
    End Sub

    '-------------------------------------------------------------------------------   
    ' 目的          : 从当前的活动环节开始回退一个环节
    ' 传入参数      : 
    ' 传出参数      : 
    ' Author        : CHENYU   Date　: 2006-4-4
    '-------------------------------------------------------------------------------   
    Private Sub btnRollBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRollBack.Click
        Try
            WorkflowManager.RollBackInstance(CLng(strWorkflowInstId))
            Response.Redirect("../2009/message.aspx")
        Catch ex As Exception
            MyBase.MessageBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        WorkflowManager.FinishWorkflowInstance(strWorkflowInstId)
        Response.Redirect("../2009/message.aspx")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        WorkflowManager.DeleteWorkflowInstance(strWorkflowInstId)
        Response.Redirect("../2009/message.aspx")
    End Sub

    Private Sub btnRedirect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRedirect.Click
        Response.Redirect("RedirectEmployeeSelect.aspx?WorkflowInstId=" & strWorkflowInstId)
    End Sub

End Class

End Namespace
