Imports System.IO
Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


Partial Class RedirectEmployeeSelect
    Inherits UserPageBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSelectEmpInfo As System.Web.UI.WebControls.Label

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�ڴ˴����ó�ʼ��ҳ���û�����
        If Not Page.IsPostBack Then
                ' InitTree()
        End If
    End Sub

        Public Sub InitTree()
            Dim oDepartments As System.Collections.Generic.List(Of Department) = OrganizationFactory.Implementation.GetChildDepartments("0")

            'Dim em As IEnumerator = htDept.GetEnumerator()
            'Dim tn As TreeNode = New TreeNode
            'tn.Text = "��Աѡ��"
            ' Me.tvwEmployees.Nodes.Add(tn)
            Response.Write("tree.nodes['0_-1']='id:0;text:��Աѡ��;target:_self;icon:workflow;';" & vbCrLf)
            For i As Integer = 0 To oDepartments.Count - 1
                Dim Dept As Department = oDepartments(i)
                If Dept.Type = DepartmentType.ACTUAL Then
                    'Dim tnDept As TreeNode = New TreeNode
                    'tnDept.Text = Dept.Name
                    'tnDept.Expanded = True
                    'tn.Nodes.Add(tnDept)
                    Response.Write("tree.nodes['-1_" & Dept.Code & "']='id:" & Dept.Code & ";text:" & Dept.Name & ";icon:department;';" & vbCrLf)
                    GenerateDeptTreeNode(Dept.Code)
                    GenerateEmployeeTreeNode(Dept.Code)
                End If
            Next
            '   tn.Expanded = True
        End Sub

    '���ɲ��Ž��
        Private Sub GenerateDeptTreeNode(ByVal DeptId As String)
            Dim oDepartments As System.Collections.Generic.List(Of Department) = OrganizationFactory.Implementation.GetChildDepartments(DeptId)

            For i As Integer = 0 To oDepartments.Count - 1
                Dim Dept As Department = oDepartments(i)
                If Dept.Type = DepartmentType.ACTUAL Then
                    Response.Write("tree.nodes['" & Dept.ParentDepartCode & "_" & Dept.Code & "']='id:" & Dept.Code & ";text:" & Dept.Name & ";icon:department;';" & vbCrLf)
                    GenerateDeptTreeNode(Dept.Code)
                    GenerateEmployeeTreeNode(Dept.Code)
                End If
            Next
        End Sub

    '������Ա���
        Private Sub GenerateEmployeeTreeNode(ByVal DeptId As String)
            'Dim CurrentTaskUser As String = ViewState("CurrentTaskUser").ToString()
            Dim hst As EmployeeCollection = OrganizationFactory.Implementation.GetEmployees(DeptId)
            For i As Integer = 0 To hst.Count - 1
                Dim emp As Employee = hst(i)
                If Not emp Is Nothing Then GenerateEmployeeTreeNode(DeptId, emp)
            Next
        End Sub


        Private Sub GenerateEmployeeTreeNode(ByVal NodeId As String, ByVal oEmployee As Employee)
            Response.Write("tree.nodes['" & NodeId & "_" & oEmployee.Id & "']='id:" & oEmployee.Code & ";text:" & CStr(oEmployee.Attributes("C3_337790108500")) & "-" & oEmployee.Name & ";check:true;check-name:chkEmployee;icon:employee;';" & vbCrLf)
        End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
            ' Dim strSelectedUser As String = ""
            ' GetSelectedUser(Me.tvwEmployees.Nodes(0), strSelectedUser)
            Dim strSelectedUser As String = Request.Form("chkEmployee")
        Dim SelectedUsers() As String = strSelectedUser.Split(CChar(","))
            Try
                If SelectedUsers.Length = 0 Then
                    MessageBox("��ѡ��������!")
                    Return
                End If

                '�ҳ���ǰ�Ļ����ID
                Dim ActInstKey As String = "0"
                Dim strWorkflowInstId As String = Request.QueryString("WorkflowInstId")
                Dim oWorkflowInstance As WorkflowInstance = WorkflowFactory.LoadInstance(strWorkflowInstId)
                For i As Integer = 0 To oWorkflowInstance.Activities.Count - 1
                    If oWorkflowInstance.Activities(i).Status = TaskStatusConstants.Actived Then
                        ActInstKey = oWorkflowInstance.Activities(i).Key
                        Exit For
                    End If
                Next

                'ѡ���������
                Dim employees As New Hashtable
                For i As Integer = 0 To SelectedUsers.Length - 1
                    If SelectedUsers(i) <> "" Then
                        employees.Add(SelectedUsers(i), OrganizationFactory.Implementation.GetEmployee(SelectedUsers(i)))
                    End If
                Next

                WorkflowManager.RedirectWorklistItem(ActInstKey, employees)
                Response.Redirect("../2009/message.aspx")

            Catch ex As Exception
                Response.Write("�����ض���ʱ��������.")
            End Try
    End Sub


        'Private Sub GetSelectedUser(ByVal tn As TreeNode, ByRef SelectedUser As String)
        '    Dim oTreeNodeCollection As TreeNodeCollection = tn.Nodes
        '    For Each oTreeNode As TreeNode In oTreeNodeCollection
        '        If oTreeNode.Checked Then
        '            SelectedUser = SelectedUser & oTreeNode.NodeData & ","
        '        Else
        '            GetSelectedUser(oTreeNode, SelectedUser)
        '        End If
        '    Next
        'End Sub

End Class

End Namespace
