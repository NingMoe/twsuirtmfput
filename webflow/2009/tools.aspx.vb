
Imports AjaxPro
Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class tools
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

    Private _dt As DataTable = Nothing

    Protected oWorkflowShortcuts As WorkflowShortcuts

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AjaxPro.Utility.RegisterTypeForAjax(GetType(Unionsoft.Workflow.Web.tools))

        oWorkflowShortcuts = New WorkflowShortcuts(Me.CurrentUser.Code)
        _dt = PermissionUtility.GetPermissionWorkflowItems(Me.CurrentUser.Code)
    End Sub

    Protected Function IsPermissionWorkflowItem(ByVal WorkflowItemId As String) As Boolean
        Return (_dt.Select("ID=" & WorkflowItemId).Length = 1)
    End Function

    <AjaxMethod(HttpSessionStateRequirement.Read)> _
    Public Function WorklistAmount() As Integer
        Return Worklist.GetWorklistItems(CType(Session.Item("User"), Unionsoft.Workflow.Platform.Employee).Code, "").Rows.Count
    End Function

    <AjaxMethod(HttpSessionStateRequirement.Read)> _
    Public Function ProxyWorklistAmount() As Integer
        Return Worklist.GetProxyWorklistItems(CType(Session.Item("User"), Unionsoft.Workflow.Platform.Employee).Code, "").Rows.Count
    End Function

    <AjaxMethod(HttpSessionStateRequirement.Read)> _
    Public Function DraftWorklistAmount() As Integer
        Return Worklist.GetDraftWorklistItems(CType(Session.Item("User"), Unionsoft.Workflow.Platform.Employee).Code, "").Rows.Count
    End Function

End Class

End Namespace
