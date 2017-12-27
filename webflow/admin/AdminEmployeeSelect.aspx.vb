
Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


Partial Class AdminEmployeeSelect
    Inherits AdminPageBase

    Private _WorkflowId As Long

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _WorkflowId = CLng(Request.QueryString("WorkflowId"))

        If Page.IsPostBack Then Exit Sub
        Dim strConfigFilePath As String = System.Web.HttpContext.Current.Request.PhysicalApplicationPath & Configuration.PAGE_STYLE_CONFIG
        Dim PageConfig As New DataServiceSection
        PageConfig.LoadSource(strConfigFilePath)
        Dim Node As New TreeNode
        Dim Depart As Department = OrganizationFactory.Implementation.GetDepartment("0")
        Node.Text = Depart.Name
        'Node.CheckBox = True
        Node.NodeData = Depart.Code
        Node.Type = CStr(OrganizationTypeConstants.Department)
        Node.ImageUrl = PageConfig.GetKeyAttr("OrganizationTree", "Department", "Image")
        Node.Expanded = True
        PageConfig = Nothing
        Me.tvwEmployees.Nodes.Add(Node)
        OrganizationTree.GenDepartmentTree(Node, "0", True)
    End Sub

    Private Sub GetSelectedEmployees(ByVal Node As TreeNode, ByRef hashSelectedEmployees As ArrayList)
        Dim tvwNode As TreeNode
        For Each tvwNode In Node.Nodes
            If tvwNode.CheckBox = True Then
                If tvwNode.Checked Then
                    If tvwNode.Type = CStr(OrganizationTypeConstants.Department) Then
                        hashSelectedEmployees.Add(OrganizationFactory.Implementation.GetDepartment(tvwNode.NodeData))
                    ElseIf tvwNode.Type = CStr(OrganizationTypeConstants.Person) Then
                        hashSelectedEmployees.Add(OrganizationFactory.Implementation.GetEmployee(tvwNode.NodeData))
                    End If
                    GetSelectedEmployees(tvwNode, hashSelectedEmployees)
                Else
                    GetSelectedEmployees(tvwNode, hashSelectedEmployees)
                End If
            Else
                GetSelectedEmployees(tvwNode, hashSelectedEmployees)
            End If
        Next
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim i As Integer
        Dim arrySelectEmployees As New ArrayList
        Dim tvwNode As TreeNode
        For Each tvwNode In Me.tvwEmployees.Nodes
            If tvwNode.CheckBox = True Then
                If tvwNode.Checked Then
                    arrySelectEmployees.Add(OrganizationFactory.Implementation.GetEmployee(tvwNode.NodeData))
                End If
            Else
                GetSelectedEmployees(tvwNode, arrySelectEmployees)
            End If
        Next
        If arrySelectEmployees.Count = 0 Then
            Me.MessageBox("����ѡ��һ�������ϵĲ��Ż���Ա.")
        Else
            Try
                For i = 0 To arrySelectEmployees.Count - 1
                    If arrySelectEmployees(i).GetType().ToString() = "Unionsoft.Workflow.Platform.Department" Then
                        Dim Dept As Department = CType(arrySelectEmployees(i), Department)
                        PermissionUtility.Add(_WorkflowId, Dept.Code, OrganizationTypeConstants.Department)
                    Else
                        Dim Emp As Employee = CType(arrySelectEmployees(i), Employee)
                        PermissionUtility.Add(_WorkflowId, Emp.Code, OrganizationTypeConstants.Person)
                    End If
                Next
                Me.MessageBox("���óɹ�!")
            Catch ex As Exception
                Me.MessageBox("����ʧ��,���ܵ�ԭ����:���Ѿ����������Ȩ��.")
                SLog.Err("Ȩ������:��������ʹ����.", ex)
            End Try
            Me.SetLocation("AdminEditPermissoin.aspx?WorkflowID=" & _WorkflowId.ToString())
        End If
    End Sub

End Class

End Namespace
