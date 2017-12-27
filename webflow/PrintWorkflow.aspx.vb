'===========================================================================
' ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
' �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\Stub_PrintWorkflow_aspx_vb.vb���ĳ������ 
' �̳С�
' ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
' ��������ҳ��
' ����������ҳ��PrintWorkflow.aspx��Ҳ���޸ģ��������µ�������
' �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


'Partial Class PrintWorkflow
Partial Class Migrated_PrintWorkflow

Inherits PrintWorkflow

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

    Protected lngWorkflowID As String = "0"
    Protected oWorklistItem As WorklistItem
    Protected lngPrintTimes As Long = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strPrintFormName As String
        Dim lngUserTaskID As Long

        If Request.QueryString("UserTaskID") Is Nothing Then
            Response.Write("�Ƿ�����!")
            Response.End()
        End If

        oWorklistItem = Worklist.GetWorklistItem(Request.QueryString("UserTaskID"))
        If oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID = 0 Then
            Response.Write("�����̵ı�δ����!")
            Response.End()
        End If

        '��ȡ�����̶���ʱ���õĴ�ӡ��������
        'strPrintFormName = oWorklistItem.ActivityInstance.NodeTemplate.PrintFormName
        'If strPrintFormName = "" Then
        '    Response.Write("δ���ô�ӡ����!")
        '    Response.End()
        'End If

        '��ǰ���̵�ID 
        lngWorkflowID = oWorklistItem.ActivityInstance.WorkflowInstance.Key

        '���ɴ�ӡ����
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        Dim forceRights As New ForceRightsInForm
        forceRights.ForceEnableRecView = True
        FormManager.LoadForm(CmsRes.ActiveCmsPassport, pnlPrintWorkflow, Nothing, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, InputMode.PrintInHostTable, strPrintFormName, , oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, , , True, forceRights, , False)

        '���ô�ӡ����߶�
        Response.Write("<style>.UserForm1{width:700px;height:" & CStr(CmsRes.GetPrintFormHeight(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, strPrintFormName)) & "px;}</style>")

        '���´�ӡ����
        'Dim strSql As String
        'strSql = "UPDATE WF_TASK SET PrintTimes=ISNULL(PrintTimes,0)+1 WHERE [ID]=" & oWorklistItem.ActivityInstance.Key
        'SDbStatement.Execute(strSql)
        ''��ȡ��ǰ��¼�Ĵ�ӡ����
        'strSql = "SELECT PrintTimes FROM WF_TASK WHERE [ID]=" & oWorklistItem.ActivityInstance.Key
        'lngPrintTimes = DbField.GetLng(SDbStatement.Query(strSql).Tables(0).Rows(0), "PrintTimes")
    End Sub

End Class

End Namespace
