

Imports System.Text
Imports System.IO
Imports FlowDiagram
Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports AddFlow3Lib


Namespace Unionsoft.Workflow.Web


Partial Class CtlFlowDiagram
    Inherits System.Web.UI.UserControl

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Repeater1 As Repeater

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Const AREA_COORDS As String = "<area href=""#"" onclick=""javascript:FlowDiagram_OnClick(this,{0});"" shape=RECT coords=""{1},{2},{3},{4}"">"

    Const strTempFloder As String = "flowtemp"
    '设置生成临时文件夹
    Private strTempPath As String = System.Web.HttpContext.Current.Request.PhysicalApplicationPath & strTempFloder
    '用于生成流程图的控件
    Private objDiagram As New DiagramMapClass

    Protected oWorkflowInstance As WorkflowInstance


    '-------------------------------------------------------------------------------------
    '生成Map脚本
    '-------------------------------------------------------------------------------------
    Private Sub GenerateNodeInfoScript()
        Dim sb As New StringBuilder
        Dim i As Integer
        Dim lngMinTop As Long = objDiagram.MinOffsetTop
        Dim lngMinLeft As Long = objDiagram.MinOffsetLeft
        For i = 1 To objDiagram.Diagram.Nodes.Count
            sb.Append(vbCrLf)
            sb.Append(vbTab & vbTab & vbTab)
            Dim strNodeKey As String = objDiagram.GetNodeKeyByIndex(i)
            sb.AppendFormat(AREA_COORDS, strNodeKey, (objDiagram.GetNodeLeftOffset(strNodeKey) - lngMinLeft + 1) / 15, (objDiagram.GetNodeTopOffset(strNodeKey) - lngMinTop + 1) / 15, (objDiagram.GetNodeLeftOffset(strNodeKey) - lngMinLeft + 860) / 15, (objDiagram.GetNodeTopOffset(strNodeKey) - lngMinTop + 860) / 15)
            sb.Append(vbCrLf)
            sb.Append(vbTab & vbTab)
        Next
        Me.DiagramMap.InnerHtml = sb.ToString()
    End Sub


    '-------------------------------------------------------------------------------------
    '生成流程模板的流程图
    '-------------------------------------------------------------------------------------
    Public Sub GenerateWorkflowTemplatePicture(ByVal WorkflowTemplateID As Long)
        Dim strFileName As String
        Dim strPictureName As String
        Dim strFlowImageName As String
        Dim FlowItem As WorkflowItem
        Try
            FlowItem = WorkflowManager.GetWorkflowItem(CStr(WorkflowTemplateID))
            strFileName = strTempPath & "\" & FlowItem.Key & ".data"
            strPictureName = strTempPath & "\" & FlowItem.Key & ".png"
            strFlowImageName = FlowItem.Key & ".png"
            If Not Directory.Exists(strTempPath) Then Directory.CreateDirectory(strTempPath)
            If File.Exists(strFileName) Then File.Delete(strFileName)
            FlowItem.SaveFlowFile(strFileName)
            objDiagram.LoadFromFile(strFileName)
            objDiagram.ExportPicture(strPictureName)
        Catch ex As Exception
            SLog.Err("生成流程流转历史信息发生错误.", ex)
        End Try
        Me.Image1.ImageUrl = "../" & strTempFloder & "/" & strFlowImageName
    End Sub

    '-------------------------------------------------------------------------------------
    '生成流程实例的流程图
    '-------------------------------------------------------------------------------------
    Public Sub GenerateWorkflowInstancePicture(ByVal WorkflowInstId As Long)
        Dim strFileName As String
        Dim strPictureName As String
        Dim strFlowImageName As String
        'Dim Workflow As WorkflowInstance = Nothing

        Try
            oWorkflowInstance = WorkflowFactory.LoadInstance(CStr(WorkflowInstId))
            strFileName = strTempPath & "\" & oWorkflowInstance.Key & ".data"
            strPictureName = strTempPath & "\" & oWorkflowInstance.Key & ".png"
            strFlowImageName = oWorkflowInstance.Key & ".png"
            If Not Directory.Exists(strTempPath) Then Directory.CreateDirectory(strTempPath)
            If File.Exists(strFileName) Then File.Delete(strFileName)
            If File.Exists(strPictureName) Then File.Delete(strPictureName)
            oWorkflowInstance.ExportDiagram(strFileName)
            objDiagram.LoadFromFile(strFileName)
            'Dim Dict As DictionaryEntry
            For i As Integer = 0 To oWorkflowInstance.Activities.Count - 1
                Dim act As ActivityInstance = oWorkflowInstance.Activities(i) 'CType(Dict.Value, ActivityInstance)
                If act.Status = TaskStatusConstants.Actived Then
                    objDiagram.SetNodeColor(act.NodeTemplate.Key, 500)
                ElseIf act.Status = TaskStatusConstants.Finished Then
                    objDiagram.SetNodeColor(act.NodeTemplate.Key, 65280)
                Else

                End If
            Next
            objDiagram.ExportPicture(strPictureName)
        Catch ex As Exception
            SLog.Err("生成流程图发生错误.", ex)
        End Try

        GenerateNodeInfoScript()

        Me.Image1.ImageUrl = "../" & strTempFloder & "/" & strFlowImageName

        'oWorkflowInstance = WorkflowFactory.LoadInstance(WorkflowInstId)

    End Sub


End Class

End Namespace
