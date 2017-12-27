
Imports NetReusables
Imports Unionsoft.Workflow.Engine
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


Partial Class AdminTools
    Inherits AdminPageBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents tvwAdminTools As Microsoft.Web.UI.WebControls.TreeView

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Sub LoadTreeView()
        Dim i As Integer
        Dim strConfigFilePath As String = System.Web.HttpContext.Current.Request.PhysicalApplicationPath & Configuration.PAGE_STYLE_CONFIG
        Dim PageConfig As New DataServiceSection

        Response.Write("tree.nodes['0_-1']='id:0;text:�������̹���;target:_self;icon:icon;';" & vbCrLf)

        InitTreeView("0")
        'PageConfig.LoadSource(strConfigFilePath)
        'Dim Sections As ArrayList = PageConfig.GetKeys("AdminTools")
        'For i = 0 To Sections.Count - 1
        '    If PageConfig.GetKeyAttr("AdminTools", CStr(Sections(i)), "Enable") = "1" Then
        '        Dim text As String = PageConfig.GetString("AdminTools", CStr(Sections(i)))
        '        Dim url As String = PageConfig.GetKeyAttr("AdminTools", CStr(Sections(i)), "Url")
        '        Dim ico As String = PageConfig.GetKeyAttr("AdminTools", CStr(Sections(i)), "Image")
        '        Response.Write("tree.nodes['-1_" & CStr(i + 1) & "']='id:" & CStr(i) & ";text:" & text & ";target:AdminPlatform;url:" & url & ";icon:icon;';" & vbCrLf)
        '    End If
        'Next
        'PageConfig = Nothing
    End Sub

    '-------------------------------------------------------------------------------
    '�Եݹ�ķ�ʽ�������Ľڵ�
    '-------------------------------------------------------------------------------
    Private Sub InitTreeView(ByVal NodeId As String)
        Dim dt As DataTable = WorkflowManager.GetWorkflowChieldCategories(CLng(NodeId))
        For i As Integer = 0 To dt.Rows.Count - 1
            Response.Write("tree.nodes['" & CStr(IIf(CStr(dt.Rows(i)("PID")) = "0", "-1", CStr(dt.Rows(i)("PID")))) & "_" & CStr(dt.Rows(i)("ID")) & "']='id:" & CStr(dt.Rows(i)("ID")) & ";text:" & CStr(dt.Rows(i)("DESCR")) & ";target:AdminPlatform;url:" & "AdminWorkflowList.aspx?GroupID=" & CStr(dt.Rows(i)("ID")) & ";icon:icon;';" & vbCrLf)
            InitTreeView(CStr(dt.Rows(i)("ID")))
        Next
    End Sub

End Class

End Namespace
