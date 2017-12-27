Option Explicit On 
Option Strict On

'功能描述：工作流程的查看
'传入参数：WorkFlowID 用户查看的工作流程的ID ，是运行的流程的。

Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class ViewWorkFlow
    Inherits RecordEditBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents WorkFlowHistory As System.Web.UI.WebControls.Table
    Protected WithEvents DiagramMap As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region


    Protected FormWidth As Long = 790    '窗体的默认宽度
    Private oWorklistItem As WorklistItem


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim node As NodeItem
        Dim nodeS As NodeStart
        PageSaveParametersToViewState() 'Banks Jin 2006-04-24 保存一些Request中的变量

        Dim WorklistItemId As Long
        WorklistItemId = CLng(Request.QueryString("WorklistItemId"))           '当前流程的ID
        'oWorklistItem = New WorklistItem(Request.QueryString("UserTaskID"))
        oWorklistItem = Worklist.GetWorklistItem(WorklistItemId.ToString())
        Select Case oWorklistItem.ActivityInstance.NodeTemplate.Type
            Case NodeTypeConstants.MiddleNode : node = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem)
                If node.Permission.CanUrgeTransact = True Then
                    'Me.NodeActionBar1.ShowUntreadTask = True
                Else
                    Me.NodeActionBar1.ShowUntreadTask = False
                End If
            Case NodeTypeConstants.StartNode : nodeS = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart)
                Me.NodeActionBar1.ShowUntreadTask = False
        End Select



        '--------------------------------------------------------------------------------------------------
        '检查能否回收的权限

        '--------------------------------------------------------------------------------------------------

        '显示流程流转历史
        CtlFlowHistory1.BindData(oWorklistItem.ActivityInstance.WorkflowInstance.Key)

        If Not Page.IsPostBack Then

            '获取当前表单的ID
            If oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID = 0 Then
                Me.MessageBox("此流程的表单未设置！")
            End If
            '检查有无查看流程历史的权限

            NodeActionBar1.ViewDiagramHref = "ViewFlowHistroy.aspx?WorkFlowID=" & CStr(oWorklistItem.ActivityInstance.WorkflowInstance.Key)
            NodeActionBar1.PrintWorkflowPageUrl = "PrintWorkflow.aspx?UserTaskID=" & WorklistItemId
            If Not node Is Nothing Then
                NodeActionBar1.ShowViewHistory = node.Permission.CanViewHistory
                CtlFlowHistory1.Visible = node.Permission.CanViewHistory
                NodeActionBar1.PrintWorkflow = node.Permission.CanPrintWorkflow
            End If
            '加载显示窗体
            Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
            'CmsRes.GenCoustmizeForm(Me.Panel1, usertsk.WorkNode.WorkFlow.ResourceID, Session, usertsk.WorkNode.WorkFlow.RecordID, InputMode.ViewInRelTable)

            '--------------------------------------------------------------------------------------------------
            'banks add 2005-11-29
            'PageDealAfterPostback() '处理Postback之后的事务处理。
            '设置此页面使用的窗体
            Dim DefFormName As String = CTableForm.DEF_DESIGN_FORM
            If Not node Is Nothing Then
                If node.UseForm <> "" Then DefFormName = node.UseForm
            End If
            If Not nodeS Is Nothing Then
                If nodeS.UseForm.Trim() <> "" Then DefFormName = nodeS.UseForm
            End If

            'Banks 2006-04-04 取消表内字典
            Dim forceRights As New ForceRightsInForm
            forceRights.ForceEnableRecView = True
            Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, InputMode.ViewInHostTable, DefFormName, forceRights)  '处理Postback之后的事务处理。
            Session("PAGE_POPREL_DATAFORM" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) = datForm
            '--------------------------------------------------------------------------------------------------
            'Me.txtMemo.Enabled = False
            'Me.txtMemo.Text = objUserTask.Memo

            '判断有没有附件表
            If CmsRes.HasAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) <= 0 Then
                Me.PanelAttachment.Visible = False
            Else
                Me.PanelAttachment.Visible = True
                BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)
            End If

            'Response.Write("<style>.UserForm1{width:790px;height:" & CStr(CmsRes.GetFormHeight(objUserTask.WorkNode.Workflow.ResourceID, DefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}</style>")
            Response.Write("<style>")
            If CmsRes.GetFormWidth(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, DefFormName) > CLng(FormWidth) - 15 Then
                FormWidth = CmsRes.GetFormWidth(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, DefFormName) + 15
                Response.Write(".UserForm1{width:" & FormWidth.ToString() & "px;height:" & CStr(CmsRes.GetFormHeight(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, DefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}")
            Else
                Response.Write(".UserForm1{width:790px;height:" & CStr(CmsRes.GetFormHeight(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, DefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}")
            End If
            Me.NodeActionBar1.Width = FormWidth
            Response.Write("</style>")
        End If
    End Sub

    'Private Sub NodeActionBar1_ItemCommand(ByVal sender As Object, ByVal e As String) Handles NodeActionBar1.ItemCommand
    '    Dim lngTaskID As Long
    '    lngTaskID = CLng(Request.QueryString("TaskID")) '当前流程的ID
    '    Select Case e.ToLower()
    '        Case "backtask"

    '        Case "untreadtask"
    '            Dim Task As New UserTask(lngTaskID)     '获取当前任务
    '            Task.UntreadTask()
    '        Case Else

    '    End Select
    'End Sub

    '绑定到附件列表
    Private Sub BindAttachmentTable(ByVal ResourceID As Long, ByVal RecordID As Long)
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        If CmsRes.HasAttachmentTable(ResourceID) > 0 Then
            Dim AttachmentTable As DataTable = CmsRes.GetAttachments(ResourceID, RecordID)
            Me.AttachmentList.DataSource = AttachmentTable
            Me.AttachmentList.DataBind()
        End If
    End Sub

    Private Sub AttachmentList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles AttachmentList.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dr As DataRowView
            Dim lnk As HyperLink
            dr = CType(e.Item.DataItem, DataRowView)
            If Not e.Item.FindControl("lnkViewDocument") Is Nothing Then
                lnk = CType(e.Item.FindControl("lnkViewDocument"), HyperLink)
                '检查有无查看附件的权限
                'If node.Permission.CanViewAttach = True Then
                lnk.Enabled = True
                lnk.Attributes.Add("onClick", "OpenDocFileWindow(" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID & "," & DbField.GetLng(dr, "ID") & ",3)")
                'Else
                '    lnk.Enabled = False
                'End If
            End If

            If Not e.Item.FindControl("lnkDownloadFile") Is Nothing Then
                lnk = CType(e.Item.FindControl("lnkDownloadFile"), HyperLink)
                '检查有无查看附件的权限
                'If node.Permission.CanViewAttach = True Then
                '    lnk.Enabled = True
                    lnk.Attributes.Add("onClick", "window.open('DownloadFile.aspx?ResourceID=" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID & "&DocumentID=" & DbField.GetLng(dr, "ID") & "','','height=700,width=1000,resizable=yes');return false;")
            Else
                '    lnk.Enabled = False
                'End If
            End If
        End If
    End Sub

End Class

End Namespace

