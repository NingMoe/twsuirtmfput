Option Explicit On 
Option Strict On

Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Platform
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


'--------------------------------------------------------------------
'��ʾ��֯����
'--------------------------------------------------------------------

Public Class OrganizationTree

    '--------------------------------------------------------------------
    'ͨ���ݹ����ɲ�����
    '�������:���ڵ�,����ID
    '--------------------------------------------------------------------
    Public Shared Sub GenDepartmentTree(ByVal RootNode As TreeNode, ByVal DepartmentId As String, Optional ByVal CheckBox As Boolean = False)
        Dim hst As Hashtable = OrganizationFactory.Implementation.GetDepartments(DepartmentId)
        InitEmployees(RootNode, DepartmentId, CheckBox)
        For Each Dict As DictionaryEntry In hst
            Dim Node As TreeNode = New TreeNode
            Dim oDeptrtment As Department = CType(Dict.Value, Department)
            Node.Text = oDeptrtment.Name
            RootNode.Nodes.Add(Node)
            GenDepartmentTree(Node, oDeptrtment.Code)
        Next
    End Sub


    '-------------------------------------------------------------------------
    '�ڲ��Ż����¼�����Ա
    '-------------------------------------------------------------------------
    Public Shared Function InitEmployees(ByVal DepartNode As TreeNode, ByVal DepartCode As String, Optional ByVal CheckBox As Boolean = False) As TreeNode
        Dim hashEmployees As EmployeeCollection = OrganizationFactory.Implementation.GetEmployees(DepartCode)
        Dim Dict As DictionaryEntry
        Dim Node As New TreeNode
        If Not OrganizationFactory.Implementation.GetDepartment(DepartCode) Is Nothing Then
            For i As Integer = 0 To hashEmployees.Count - 1
                Dim emp As Employee = hashEmployees(i)
                Node = New TreeNode
                Node.CheckBox = True
                Node.Text = emp.Name
                Node.Type = CStr(OrganizationTypeConstants.Person)
                Node.NodeData = emp.Code
                DepartNode.Nodes.Add(Node)
            Next
            Return DepartNode
        Else
            Return Nothing
        End If
    End Function


    '-------------------------------------------------------------------------
    '��ȡ�����ŷ���Ľڵ�
    '-------------------------------------------------------------------------
    Public Shared Function GenerateRootDeptNode() As TreeNode
        Dim Node As New TreeNode
        Dim Depart As Department = OrganizationFactory.Implementation.GetDepartment("0")
        Node.Text = Depart.Name
        Node.NodeData = Depart.Code
        Return Node
    End Function

End Class

End Namespace
