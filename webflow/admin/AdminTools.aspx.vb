
Imports NetReusables
Imports Unionsoft.Workflow.Engine
Imports Microsoft.Web.UI.WebControls


Namespace Unionsoft.Workflow.Web


Partial Class AdminTools
    Inherits AdminPageBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents tvwAdminTools As Microsoft.Web.UI.WebControls.TreeView

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Sub LoadTreeView()
        Dim i As Integer
        Dim strConfigFilePath As String = System.Web.HttpContext.Current.Request.PhysicalApplicationPath & Configuration.PAGE_STYLE_CONFIG
        Dim PageConfig As New DataServiceSection

        Response.Write("tree.nodes['0_-1']='id:0;text:工作流程管理;target:_self;icon:icon;';" & vbCrLf)

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
    '以递归的方式生成树的节点
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
