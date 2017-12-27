'===========================================================================
' 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
' 类名已更改，且类已修改为从文件“App_Code\Migrated\Stub_PrintWorkflow_aspx_vb.vb”的抽象基类 
' 继承。
' 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
' 代码隐藏页。
' 关联的内容页“PrintWorkflow.aspx”也已修改，以引用新的类名。
' 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
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

    Protected lngWorkflowID As String = "0"
    Protected oWorklistItem As WorklistItem
    Protected lngPrintTimes As Long = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim strPrintFormName As String
        Dim lngUserTaskID As Long

        If Request.QueryString("UserTaskID") Is Nothing Then
            Response.Write("非法进入!")
            Response.End()
        End If

        oWorklistItem = Worklist.GetWorklistItem(Request.QueryString("UserTaskID"))
        If oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID = 0 Then
            Response.Write("此流程的表单未设置!")
            Response.End()
        End If

        '获取在流程定义时设置的打印窗体名称
        'strPrintFormName = oWorklistItem.ActivityInstance.NodeTemplate.PrintFormName
        'If strPrintFormName = "" Then
        '    Response.Write("未设置打印窗体!")
        '    Response.End()
        'End If

        '当前流程的ID 
        lngWorkflowID = oWorklistItem.ActivityInstance.WorkflowInstance.Key

        '生成打印窗体
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        Dim forceRights As New ForceRightsInForm
        forceRights.ForceEnableRecView = True
        FormManager.LoadForm(CmsRes.ActiveCmsPassport, pnlPrintWorkflow, Nothing, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, InputMode.PrintInHostTable, strPrintFormName, , oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, , , True, forceRights, , False)

        '设置打印窗体高度
        Response.Write("<style>.UserForm1{width:700px;height:" & CStr(CmsRes.GetPrintFormHeight(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, strPrintFormName)) & "px;}</style>")

        '更新打印次数
        'Dim strSql As String
        'strSql = "UPDATE WF_TASK SET PrintTimes=ISNULL(PrintTimes,0)+1 WHERE [ID]=" & oWorklistItem.ActivityInstance.Key
        'SDbStatement.Execute(strSql)
        ''获取当前记录的打印次数
        'strSql = "SELECT PrintTimes FROM WF_TASK WHERE [ID]=" & oWorklistItem.ActivityInstance.Key
        'lngPrintTimes = DbField.GetLng(SDbStatement.Query(strSql).Tables(0).Rows(0), "PrintTimes")
    End Sub

End Class

End Namespace
