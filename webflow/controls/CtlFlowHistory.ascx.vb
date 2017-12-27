Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Platform
Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web



Partial Class CtlFlowHistory
    Inherits System.Web.UI.UserControl

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

    'Protected dtWorkFlowTasks As DataTable = Nothing

    Protected oWorkflowInstance As WorkflowInstance

    Public Sub BindData(ByVal WorkflowInstId As String)
        oWorkflowInstance = WorkflowFactory.LoadInstance(WorkflowInstId)

        'If oWorkflowInstance.Activities(0).Key Then

        'End If

        'For i As Integer = 0 To oWorkflowInstance.Activities.Count - 1
        '    'oWorkflowInstance.Activities(i).Name()
        '    oWorkflowInstance.Activities(i).Status = TaskStatusConstants.Actived
        '    For j As Integer = 0 To oWorkflowInstance.Activities(i).WorklistItems.Count - 1
        '        oWorkflowInstance.Activities(i).WorklistItems(j).Status = TaskStatusConstants.Actived
        '    Next
        'Next

        'oWorkflowInstance.Activities(0).NodeTemplate.Key()
    End Sub

End Class

End Namespace
