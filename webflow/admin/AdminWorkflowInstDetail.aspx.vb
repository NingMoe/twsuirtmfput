Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web



Partial Class AdminWorkflowInstDetail
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

    Dim strWorkflowInstId As String


    '-------------------------------------------------------------------------------   
    ' Ŀ��          : '��ǰ����ͼ����ʾ����
    ' �������      : 
    ' ��������      : 
    ' Author        : CHENYU   Date��: 2006-4-3
    '-------------------------------------------------------------------------------   
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Expires = 0
        Response.Buffer = False

        strWorkflowInstId = Request.QueryString("WorkflowId")
        Dim oWorkflowInst As WorkflowInstance = WorkflowManager.GetWorkflowInstance(strWorkflowInstId)

        Me.btnRollBack.Attributes.Add("onClick", "return window.confirm('ȷʵҪ�����̻�����ǰһ������?')")
        Me.btnFinish.Attributes.Add("onClick", "return window.confirm('ȷʵҪ����������Ϊ������?')")
        Me.btnDelete.Attributes.Add("onClick", "return window.confirm('ȷʵҪɾ��������ʵ����?')")

        If oWorkflowInst.Activities.Count > 0 AndAlso oWorkflowInst.Activities(0).WorklistItems.Count > 0 Then
            Me.btnDisplayForm.Attributes.Add("onClick", "window.open('../process/director.aspx?action=view&WorkflowInstId=" & strWorkflowInstId & "&WorklistItemId=" & oWorkflowInst.Activities(0).WorklistItems(0).Key & "');return false;")
        Else
            Me.btnDisplayForm.Enabled = False
        End If

        If Not Me.IsPostBack Then CtlFlowDiagram1.GenerateWorkflowInstancePicture(CLng(strWorkflowInstId))
    End Sub

    '-------------------------------------------------------------------------------   
    ' Ŀ��          : �ӵ�ǰ�Ļ���ڿ�ʼ����һ������
    ' �������      : 
    ' ��������      : 
    ' Author        : CHENYU   Date��: 2006-4-4
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
