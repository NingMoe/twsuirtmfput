
Imports NetReusables
Imports Unionsoft.Workflow.Platform
Imports Unionsoft.Workflow.Engine
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


Partial Class proxyselector
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Page.IsPostBack Then Exit Sub

        Dim Node As TreeNode = OrganizationTree.GenerateRootDeptNode()
        Me.tvwEmployees.Nodes.Add(Node)
        Node.Expanded = True
        OrganizationTree.GenDepartmentTree(Node, Node.NodeData, False)
    End Sub

    Protected Sub UpdateProxyEmployee(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '---------------------------------------------------------------------
        '��ȡ��ǰѡ�����Ա
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
        '---------------------------------------------------------------------
        '���ݼ��   
        If arrySelectEmployees.Count = 0 Then
            Me.MessageBox("����ѡ��һ��������.")
        ElseIf arrySelectEmployees.Count = 1 Then
            Dim oEmployee As Employee = CType(arrySelectEmployees(0), Employee)
            If oEmployee.Code = CurrentUser.Code Then
                Me.MessageBox("�����˲�������Ϊ�Լ�")
            Else
                ProxyManager.AddEmployee(CurrentUser.Code, oEmployee.Code)
                Response.Write("<script>parent.document.location.href='proxymain.aspx';</script>")
            End If
        Else
            Me.MessageBox("ֻ��ѡ��һ��������.")
        End If
    End Sub

    Private Sub GetSelectedEmployees(ByVal Node As TreeNode, ByRef hashSelectedEmployees As ArrayList)
        Dim tvwNode As TreeNode
        For Each tvwNode In Node.Nodes
            If tvwNode.CheckBox = True Then
                If tvwNode.Checked Then
                    hashSelectedEmployees.Add(OrganizationFactory.Implementation.GetEmployee(tvwNode.NodeData))
                End If
            Else
                GetSelectedEmployees(tvwNode, hashSelectedEmployees)
            End If
        Next
    End Sub

End Class

End Namespace
