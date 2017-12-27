Imports System.IO
Imports Microsoft.Web.UI.WebControls

Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class ViewFlowHistroy
    Inherits RecordEditBase

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

    Protected ResourceId As Long = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strAction As String = Request.QueryString("action")

        If strAction Is Nothing Then strAction = ""

        Select Case strAction.ToLower()
            Case "create"
                Dim WorkflowTemplateId As Long = CType(Request.QueryString("TemplateflowId"), Long)
                CtlFlowDiagram1.GenerateWorkflowTemplatePicture(WorkflowTemplateId)
            Case Else
                Dim WorkflowInstId As Long = CType(Request.QueryString("WorkflowId"), Long)
                CtlFlowDiagram1.GenerateWorkflowInstancePicture(WorkflowInstId)
                CtlFlowHistory1.BindData(WorkflowInstId.ToString())
        End Select
    End Sub

End Class

End Namespace



