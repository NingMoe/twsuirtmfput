Option Explicit On 
Option Strict On

'--------------------------------------------------------------
'���������
'����ID:TaskID
'������:ActionID
'--------------------------------------------------------------
'CHENYU 2010/4/15 18:56 V2
'�Ľ���ԱѡȡĿ¼��ΪMzTreeView10���֣�ȡ����ԭ��ʹ�õ�ASPNET�Դ����οؼ�
'--------------------------------------------------------------

Imports System.IO
Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class EmployeeSelect
    Inherits UserPageBase

    Private _url As String = ""
    Private WorklistItemId As Long = 0
    Private actionid As Long = 0
    Private _link As String = ""

    Private oWorklistItem As WorklistItem        '��ǰ�������
    Protected EmployeeSelectMode As Integer = 0  '0 ��ʾֻ��ѡһ����1��ʾ����ѡ���

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'Protected WithEvents tvwEmployees As Microsoft.Web.UI.WebControls.TreeView
    'Protected WithEvents lblSelectEmpInfo As System.Web.UI.WebControls.Label

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region


#Region "�����¼�"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Request.QueryString("url") <> "" Then _url = Request.QueryString("url")
        If Request.QueryString("WorklistItemId") <> "" Then WorklistItemId = CLng(Request.QueryString("WorklistItemId"))
        If Request.QueryString("ActionID") <> "" Then actionid = CLng(Request.QueryString("ActionID"))
        If Request.QueryString("link") <> "" Then _link = Request.QueryString("link")

        oWorklistItem = Worklist.GetWorklistItem(WorklistItemId.ToString())

        Dim link As LinkItem = oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Links(_link)
        Dim node As NodeBase = oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Nodes(link.AdjustDstNodeID.ToString())

        Select Case node.Type
            Case NodeTypeConstants.MiddleNode : If CType(node, NodeItem).IsSelectOne = False Then EmployeeSelectMode = 1
        End Select

    End Sub

    Protected Sub InitSubmitButton(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        btn.Attributes.Add("onclick", "return EmployeeValidate();")
    End Sub

    '-------------------------------------------------------------------------
    'ѡ����Ա�����ת
    '-------------------------------------------------------------------------
    Protected Sub Submit(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim hstEmployees As Hashtable = New Hashtable
        Dim hstCcEmployees As Hashtable = New Hashtable

        Dim strEmployees As String = Request.Form("chkEmployee")
        If strEmployees <> "" Then
            Dim aryEmployees() As String = Split(strEmployees & ",", ",")
            For i As Integer = 0 To aryEmployees.Length - 1
                If aryEmployees(i) <> "" Then
                    hstEmployees.Add(aryEmployees(i), OrganizationFactory.Implementation.GetEmployee(aryEmployees(i)))
                End If
            Next
        End If

        strEmployees = Request.Form("chkCcEmployee")
        If strEmployees <> "" Then
            Dim aryEmployees() As String = Split(strEmployees & ",", ",")
            For i As Integer = 0 To aryEmployees.Length - 1
                If aryEmployees(i) <> "" Then
                    hstCcEmployees.Add(aryEmployees(i), OrganizationFactory.Implementation.GetEmployee(aryEmployees(i)))
                End If
            Next
        End If

        If hstEmployees.Count = 0 Then
            Me.MessageBox("��ѡ����Ա!")
            Exit Sub
        End If

        '��ȡ��ǰ�û����������
        'Dim oCmsResource As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        'If oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Form.IsCustmizeForm = False Then
        '    oWorklistItem.FormFieldValues = oCmsResource.GetRecordFieldValue(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)
        'Else
        '    oWorklistItem.FormFieldValues = CType(Session("FormFieldValues"), Hashtable)
        'End If

        Select Case oWorklistItem.ActivityInstance.NodeTemplate.Type
            Case NodeTypeConstants.StartNode : oWorklistItem.Action = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions(CStr(actionid))
            Case NodeTypeConstants.MiddleNode : oWorklistItem.Action = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem).Actions(CStr(actionid))
        End Select

        oWorklistItem.Memo = CStr(Session("TASK_MEMO"))
        oWorklistItem.DestinationEmployees = hstEmployees
        oWorklistItem.CcEmployees = hstCcEmployees

        Try
            Worklist.TransactWorklistItem(oWorklistItem, _link)       '��������
            Response.Redirect(_url)
        Catch ex As Exception
            SLog.Err("ִ�����̴���", ex)
            Me.MessageBox("�������̴���")
        End Try
    End Sub

    Protected Sub Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.SetLocation(CStr(Session.Item("EntryUrl")))
    End Sub

#End Region


#Region "������������ѡȡĿ¼"

    '-----------------------------------------------------
    '������ԱѡȡĿ¼
    '-----------------------------------------------------
    Protected Sub GenerateEmployeeTree()
        Dim oLink As LinkItem = oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Links(_link)
        Dim Node As NodeItem = CType(oWorklistItem.ActivityInstance.WorkflowInstance.WorkflowTemplate.Nodes(oLink.AdjustDstNodeID.ToString()), NodeItem)

        Response.Write("tree.nodes['0_-1']='id:0;text:��������ѡ��;target:_self;icon:workflow;';" & vbCrLf)

        If Node.IsOrgSql Then
            GenerateProcedureEmployeeTree("-1", oWorklistItem.Key, Node.OrgSql)
        Else
            For i As Integer = 0 To Node.Organizations.Count - 1
                Dim Org As OrgItem = CType(Node.Organizations(i), OrgItem)
                Select Case Org.Type
                    Case OrganizationTypeConstants.Department
                        GenerateDepartmentTree("-1", Org.OrgCode)
                    Case OrganizationTypeConstants.Person
                        Dim oEmployee As Employee = OrganizationFactory.Implementation.GetEmployee(Org.OrgCode)
                        If Not oEmployee Is Nothing Then GenerateEmployeeTreeNode("-1", oEmployee)
                    Case OrganizationTypeConstants.Tache
                        GenerateActivityInstEmployeeTree("-1", oWorklistItem.ActivityInstance.WorkflowInstance.Key, Org.OrgCode)
                End Select
            Next
        End If

        If Node.IsCcOrgSelect = False Then Exit Sub

        Response.Write("tree.nodes['0_-2']='id:0;text:����֪ͨ��ѡ��;target:_self;icon:workflow;';" & vbCrLf)
        For i As Integer = 0 To Node.CCOrganizations.Count - 1
            Dim Org As OrgItem = CType(Node.CCOrganizations(i), OrgItem)
            Select Case Org.Type
                Case OrganizationTypeConstants.Department
                    GenerateCcDepartmentTree("-2", Org.OrgCode)
                Case OrganizationTypeConstants.Person
                    Dim oEmployee As Employee = OrganizationFactory.Implementation.GetEmployee(Org.OrgCode)
                    If Not oEmployee Is Nothing Then GenerateCcEmployeeTreeNode("-2", oEmployee)
            End Select
        Next

    End Sub

    Private Sub GenerateDepartmentTree(ByVal NodeId As String, ByVal DepartmentId As String)
        Dim oDepartment As Department = OrganizationFactory.Implementation.GetDepartment(DepartmentId)
        If Not oDepartment Is Nothing Then
            '���ɵ�ǰ���Žڵ�
            Response.Write("tree.nodes['" & DepartmentIdConvert(NodeId) & "_" & DepartmentIdConvert(oDepartment.Code) & "']='id:" & DepartmentIdConvert(oDepartment.Code) & ";text:" & oDepartment.Name & ";icon:department;';" & vbCrLf)
            '���ɵ�ǰ��������Ա�ڵ�
            GenerateDepartmentEmployeeTree(DepartmentIdConvert(oDepartment.Code), oDepartment.Code)
            '�����Ӳ��Žڵ�
                Dim oDepartments As System.Collections.Generic.List(Of Department) = OrganizationFactory.Implementation.GetChildDepartments(DepartmentId)
                'For Each item As DictionaryEntry In hst
                For i As Integer = 0 To oDepartments.Count - 1
                    oDepartment = oDepartments(i)
                    GenerateDepartmentTree(DepartmentId, oDepartment.Code)
                Next
            End If
    End Sub

    Private Sub GenerateDepartmentEmployeeTree(ByVal NodeId As String, ByVal DepartmentId As String)
        Dim hst As EmployeeCollection
        Dim oDepartment As Department = OrganizationFactory.Implementation.GetDepartment(DepartmentId)
        If oDepartment.Type = DepartmentType.ACTUAL Then
            hst = OrganizationFactory.Implementation.GetEmployees(DepartmentId)
        Else
            hst = OrganizationFactory.Implementation.GetGroupEmployees(DepartmentId)
        End If
        For i As Integer = 0 To hst.Count - 1
            Dim oEmployee As Employee = hst(i)
            If Not oEmployee Is Nothing Then GenerateEmployeeTreeNode(NodeId, oEmployee)
        Next
    End Sub

    Private Sub GenerateActivityInstEmployeeTree(ByVal NodeId As String, ByVal WorkflowInstKey As String, ByVal ActivityInstKey As String)
        Dim oEmployees As ArrayList = OrganizationUtility.GetActivityInstanceEmployees(WorkflowInstKey, ActivityInstKey)
        For i As Integer = 0 To oEmployees.Count - 1
            Dim oEmployee As Employee = OrganizationFactory.Implementation.GetEmployee(CType(oEmployees(i), Employee).Code)
            If Not oEmployee Is Nothing Then GenerateEmployeeTreeNode(NodeId, oEmployee)
        Next
    End Sub

    Private Sub GenerateProcedureEmployeeTree(ByVal NodeId As String, ByVal WorkflowInstKey As String, ByVal ProcedureText As String)
        Dim hstEmployees As Hashtable = OrganizationUtility.GetProcedureEmployees(WorkflowInstKey, ProcedureText)
        For Each dic As DictionaryEntry In hstEmployees
            Dim oEmployee As Employee = CType(dic.Value, Employee)
            GenerateEmployeeTreeNode(NodeId, oEmployee)
        Next
    End Sub
    'CStr(oEmployee.Attributes("C3_337790108500")) & "-" & 
    Private Sub GenerateEmployeeTreeNode(ByVal NodeId As String, ByVal oEmployee As Employee)
        Response.Write("tree.nodes['" & NodeId & "_" & oEmployee.Id & "']='id:" & oEmployee.Code & ";text:" & CStr(oEmployee.Attributes("C3_337790108500")) & "-" & oEmployee.Name & ";check:true;check-name:chkEmployee;icon:employee;';" & vbCrLf)
    End Sub
#End Region


#Region "������������ѡȡĿ¼"

    Private Sub GenerateCcDepartmentTree(ByVal NodeId As String, ByVal DepartmentId As String)
        Dim oDepartment As Department = OrganizationFactory.Implementation.GetDepartment(DepartmentId)
        If Not oDepartment Is Nothing Then
            '���ɵ�ǰ���Žڵ�
            Response.Write("tree.nodes['" & CcDepartmentIdConvert(NodeId) & "_" & CcDepartmentIdConvert(oDepartment.Code) & "']='id:" & DepartmentIdConvert(oDepartment.Code) & ";text:" & oDepartment.Name & ";icon:department;';" & vbCrLf)
            '���ɵ�ǰ��������Ա�ڵ�
            GenerateCcDepartmentEmployeeTree(DepartmentIdConvert(oDepartment.Code), oDepartment.Code)
            '�����Ӳ��Žڵ�
            Dim hst As Hashtable = OrganizationFactory.Implementation.GetDepartments(DepartmentId)
            For Each item As DictionaryEntry In hst
                oDepartment = CType(item.Value, Department)
                GenerateCcDepartmentTree(DepartmentId, oDepartment.Code)
            Next
        End If
    End Sub

    Private Sub GenerateCcDepartmentEmployeeTree(ByVal NodeId As String, ByVal DepartmentId As String)
        'Dim hst As EmployeeCollection = OrganizationFactory.Implementation.GetEmployees(DepartmentId)
        Dim employees As EmployeeCollection
        Dim oDepartment As Department = OrganizationFactory.Implementation.GetDepartment(DepartmentId)
        If oDepartment.Type = DepartmentType.ACTUAL Then
            employees = OrganizationFactory.Implementation.GetEmployees(DepartmentId)
        Else
            employees = OrganizationFactory.Implementation.GetGroupEmployees(DepartmentId)
        End If
        For i As Integer = 0 To employees.Count - 1
            Dim oEmployee As Employee = employees(i)
            If Not oEmployee Is Nothing Then GenerateCcEmployeeTreeNode(NodeId, oEmployee)
        Next
    End Sub

    Private Sub GenerateCcEmployeeTreeNode(ByVal NodeId As String, ByVal oEmployee As Employee)
        Response.Write("tree.nodes['" & CcDepartmentIdConvert(NodeId) & "_" & oEmployee.Id & "00001']='id:" & oEmployee.Code & ";text:" & oEmployee.Name & ";check:true;check-name:chkCcEmployee;icon:employee;';" & vbCrLf)
    End Sub



#End Region


#Region "�ڲ�����"

    Private Function DepartmentIdConvert(ByVal DepartmentId As String) As String
        If DepartmentId = "0" Then
            Return "101"
        Else
            Return DepartmentId
        End If
    End Function

    Private Function CcDepartmentIdConvert(ByVal DepartmentId As String) As String
        If DepartmentId = "0" Or DepartmentId = "-2" Then
            Return "-2"
        Else
            Return DepartmentId & "00002"
        End If
    End Function

#End Region



End Class

End Namespace
