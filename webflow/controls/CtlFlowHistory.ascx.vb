Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Platform
Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web



Partial Class CtlFlowHistory
    Inherits System.Web.UI.UserControl

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
