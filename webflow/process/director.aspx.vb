'-------------------------------------------------------------------
'�������̵ı�����ʾ
'CHENYU 2009/10/14 V1.0
'-------------------------------------------------------------------

Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports NetReusables


Namespace Unionsoft.Workflow.Web


Partial Class director
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

    Private _action As String = ""
    Private _backurl As String = ""
    Private _WorkflowId As String = "0"
    Private _WorklistItemId As String = "0"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim node As NodeBase
        Dim form As String

        _action = Request.QueryString("action")

        If _action = "create" Then
            Dim oWorkflow As WorkflowItem

            _WorkflowId = Request.QueryString("WorkflowId")

            If PermissionUtility.ExistsWorkflowItemPermission(CLng(_WorkflowId), Me.CurrentUser.Code) = False Then
                Response.Write("��Դ�����û��Ȩ��!")
                Response.End()
            End If

            oWorkflow = WorkflowManager.GetWorkflowItem(_WorkflowId)
            If oWorkflow.Form.IsCustmizeForm = True Then
                If oWorkflow.StartNode.UseForm.IndexOf("?") > 0 Then
                    Response.Redirect(oWorkflow.StartNode.UseForm & "&action=create&WorkflowId=" & _WorkflowId)
                Else
                    Response.Redirect(oWorkflow.StartNode.UseForm & "?action=create&WorkflowId=" & _WorkflowId)
                End If
            Else
                Response.Redirect("../CreateWorkflow.aspx?action=create&WorkflowId=" & _WorkflowId)
            End If
        End If

        If _action = "transtract" Then
            _WorklistItemId = Request.QueryString("WorklistItemId")
            Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(_WorklistItemId)
            If oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Form.IsCustmizeForm Then
                node = oWorklistItem.ActivityInstance.NodeTemplate
                Select Case node.Type
                    Case NodeTypeConstants.StartNode : form = CType(node, NodeStart).UseForm
                    Case NodeTypeConstants.MiddleNode : form = CType(node, NodeItem).UseForm
                End Select

                If form.IndexOf("?") > 0 Then
                    Response.Redirect(form & "&action=transtract&WorkflowInstId=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key & "&WorklistItemId=" & _WorklistItemId)
                Else
                    Response.Redirect(form & "?action=transtract&WorkflowInstId=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key & "&WorklistItemId=" & _WorklistItemId)
                End If
            Else
                Response.Redirect("../TranactWorkflow.aspx?WorklistItemId=" & _WorklistItemId)
            End If
        End If

        If _action = "view" Then
            _WorklistItemId = Request.QueryString("WorklistItemId")
            Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(_WorklistItemId)
            If oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Form.IsCustmizeForm Then
                node = oWorklistItem.ActivityInstance.NodeTemplate
                Select Case node.Type
                    Case NodeTypeConstants.StartNode : form = CType(node, NodeStart).UseForm
                    Case NodeTypeConstants.MiddleNode : form = CType(node, NodeItem).UseForm
                End Select

                If form.IndexOf("?") > 0 Then
                    Response.Redirect(form & "&action=view&WorkflowInstId=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key & "&WorklistItemId=" & _WorklistItemId)
                Else
                    Response.Redirect(form & "?action=view&WorkflowInstId=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key & "&WorklistItemId=" & _WorklistItemId)
                End If
            Else
                Response.Redirect("../ViewWorkFlow.aspx?action=view&WorkflowInstId=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key & "&WorklistItemId=" & _WorklistItemId)
            End If
        End If

        Response.Write("���ʲ�������")
        Response.End()
    End Sub

End Class

End Namespace
