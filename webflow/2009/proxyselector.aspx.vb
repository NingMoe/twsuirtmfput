
Imports NetReusables
Imports Unionsoft.Workflow.Platform
Imports Unionsoft.Workflow.Engine
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


Partial Class proxyselector
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Page.IsPostBack Then Exit Sub

        Dim Node As TreeNode = OrganizationTree.GenerateRootDeptNode()
        Me.tvwEmployees.Nodes.Add(Node)
        Node.Expanded = True
        OrganizationTree.GenDepartmentTree(Node, Node.NodeData, False)
    End Sub

    Protected Sub UpdateProxyEmployee(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '---------------------------------------------------------------------
        '获取当前选择的人员
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
        '数据检查   
        If arrySelectEmployees.Count = 0 Then
            Me.MessageBox("必须选择一个代理人.")
        ElseIf arrySelectEmployees.Count = 1 Then
            Dim oEmployee As Employee = CType(arrySelectEmployees(0), Employee)
            If oEmployee.Code = CurrentUser.Code Then
                Me.MessageBox("代理人不能设置为自己")
            Else
                ProxyManager.AddEmployee(CurrentUser.Code, oEmployee.Code)
                Response.Write("<script>parent.document.location.href='proxymain.aspx';</script>")
            End If
        Else
            Me.MessageBox("只能选择一个代理人.")
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
