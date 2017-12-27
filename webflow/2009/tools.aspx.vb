
Imports AjaxPro
Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class tools
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
