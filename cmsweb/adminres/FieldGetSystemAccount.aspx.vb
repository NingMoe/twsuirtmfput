Option Explicit On 
Option Strict On

Imports NetReusables
Imports Microsoft.Web.UI.WebControls

Imports Unionsoft.Implement
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldGetSystemAccount
    Inherits CmsPage

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
        '在此处放置初始化页的用户代码
        If Not IsPostBack Then
            Me.tvwDeptEmp.Attributes.Add("oncheck", "tree_oncheck(this)")
        End If
        GeneralTree()
    End Sub
    Private Sub GeneralTree()
        'Dim tvwDeptEmp As TreeView = New TreeView
        'tvwDeptEmp.ID = "tvwDeptEmp"
        'Me.Controls.Add(tvwDeptEmp)
        Dim deptds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "select * from CMS_DEPARTMENT")
        Dim empds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "select * from CMS_EMPLOYEE")
        Dim drs As DataRow() = deptds.Tables(0).Select("PID=-1")
        Dim drv As DataRow
        For Each drv In drs
            Dim node As TreeNode = New TreeNode
            node.ID = DbField.GetStr(drv, ("ID"))
            node.Text = DbField.GetStr(drv, ("NAME"))
            node.ImageUrl = "\cmsweb\images\tree\enterprise.gif"
            node.CheckBox = True
            node.Expanded = True
            GeneralEmpNode(node, empds.Tables(0))
            GeneralDeptNode(node, deptds.Tables(0), empds.Tables(0))
            tvwDeptEmp.Nodes.Add(node)
        Next
    End Sub
    Private Sub GeneralDeptNode(ByRef mNode As TreeNode, ByVal dep As DataTable, ByVal emp As DataTable)
        Dim drs As DataRow() = dep.Select("PId=" & mNode.ID)
        Dim drv As DataRow
        For Each drv In drs
            Dim node As TreeNode = New TreeNode
            node.ID = DbField.GetStr(drv, ("ID"))
            node.Text = DbField.GetStr(drv, ("NAME"))
            node.ImageUrl = GetDeptImage(DbField.GetInt(drv, ("Dep_Type")))
            node.CheckBox = True
            GeneralEmpNode(node, emp)
            GeneralDeptNode(node, dep, emp)
            mNode.Nodes.Add(node)
        Next
    End Sub
    Private Sub GeneralEmpNode(ByRef mNode As TreeNode, ByVal emp As DataTable)
        Dim drs As DataRow() = emp.Select("HOST_ID=" & mNode.ID)
        Dim drv As DataRow
        For Each drv In drs
            Dim node As TreeNode = New TreeNode
            node.Text = DbField.GetStr(drv, ("EMP_NAME"))
            node.NodeData = "emp"
            node.ImageUrl = "\cmsweb\images\tree\emp1.gif"
            node.CheckBox = True
            mNode.Nodes.Add(node)
        Next
    End Sub
    Private Function GetDeptImage(ByVal type As Int32) As String
        If type = 0 Then
            Return "\cmsweb\images\tree\dep_real.gif"
        Else
            Return "\cmsweb\images\tree\dep_virtual.gif"
        End If
    End Function
End Class

End Namespace
