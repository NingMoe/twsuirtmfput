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



