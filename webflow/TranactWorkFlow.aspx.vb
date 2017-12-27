Option Explicit On 
Option Strict On


'功能描述：工作流程的任务处理
'传入参数：TaskID 当前用户任务的ID ，是运行中的流程的。

'Imports Unionsoft.WebControls.Uploader 'Webb.WAVE.Controls.Upload
Imports AjaxPro

Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web



Partial Class TranactWorkFlow
    Inherits RecordEditBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub

    Protected ResourceId As Long = 0


    Protected WithEvents AttachmentList As System.Web.UI.WebControls.DataGrid

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
    Private IsPageError As Boolean = False
    Private _url As String = "2009/message.aspx"
    Private _WorklistItemId As String = "0"

    Protected WebOfficeExceptant As Boolean = False
    Protected _AttachmentEdit As Boolean = True
    Protected _AttachmentDelete As Boolean = True

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim node As NodeItem

        AjaxPro.Utility.RegisterTypeForAjax(GetType(Unionsoft.Workflow.Web.TranactWorkFlow))

        Dim WebOfficeExceptantEmployees As String = Configuration.CurrentConfig.GetString("OFFICECONTROLEXCEPTANT", "EMPLOYEECODE")
        If WebOfficeExceptantEmployees.IndexOf("," & Me.CurrentUser.Code & ",") >= 0 Or WebOfficeExceptantEmployees = "*" Then
            WebOfficeExceptant = True
        End If

        PageSaveParametersToViewState()                                                         'Banks Jin 2006-04-24 保存一些Request中的变量

        _WorklistItemId = Request.QueryString("WorklistItemId")

        oWorklistItem = Worklist.GetWorklistItem(_WorklistItemId)
        ResourceId = oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then node = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem)
        '流程操作定义
        If oWorklistItem.Status = TaskStatusConstants.Actived Or oWorklistItem.Status = TaskStatusConstants.Reserved Then
            If oWorklistItem.IsCc Then
                Dim act As New ActionItem("0", "签阅")
                NodeActionBar1.Actions.Add(act)
            Else
                If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                    NodeActionBar1.Actions = node.Actions '获取起始环节的出口动作
                ElseIf oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.StartNode Then
                    NodeActionBar1.Actions = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions
                    'oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode
                    'node = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem)
                End If
            End If
        End If


        '检查有无查看流程历史的权限
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            NodeActionBar1.ShowViewHistory = node.Permission.CanViewHistory
            CtlFlowHistory1.Visible = node.Permission.CanViewHistory
        End If


        NodeActionBar1.ViewDiagramHref = "ViewFlowHistroy.aspx?WorkFlowID=" & oWorklistItem.ActivityInstance.WorkflowInstance.Key
        NodeActionBar1.PrintWorkflowPageUrl = "PrintWorkflow.aspx?UserTaskID=" & _WorklistItemId
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then NodeActionBar1.PrintWorkflow = node.Permission.CanPrintWorkflow
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.Permission.MustInputMemo = True Then
                Me.txtMemo.Attributes.Add("mInput", "true")
                Me.txtMemo.Attributes.Add("errmsg", "处理意见")
            End If
        End If

        '检查有无增加附件的权限,起始环节不判断权限
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.StartNode Then
            Me.PanelAttachmentAdd.Visible = True
            Me.btnUpload.Visible = True
        Else
            If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                Me.PanelAttachmentAdd.Visible = node.Permission.CanAddAttach
                Me.btnUpload.Visible = node.Permission.CanAddAttach

            '检查有无附件编辑的权限
                If node.Permission.CanAddAttach = True Then
                    Me.PanelAttachmentAdd.Visible = True
                    Me.btnUpload.Visible = True
                Else
                    Me.PanelAttachmentAdd.Visible = False
                    Me.btnUpload.Visible = False
                End If
            End If
            If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                _AttachmentEdit = node.Permission.CanEditAttach
                _AttachmentDelete = node.Permission.CanDelAttach
            End If
            End If

        '加载显示窗体
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)

        '检查有无填写说明的权限
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.Permission.CanWriteMemo = True Then
                Me.txtMemo.Enabled = True
            Else
                Me.txtMemo.Enabled = False
            End If
        End If

        '显示为环节设置的窗体
        Dim strDefFormName As String = CTableForm.DEF_DESIGN_FORM
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.UseForm <> "" Then
                strDefFormName = node.UseForm
            End If
        End If
        '*****获取该用户的指定窗体*****
        Dim strOrganizationForm As String = oWorklistItem.WorkFormName
        If strOrganizationForm <> "" Then strDefFormName = strOrganizationForm

        '检查是否有编辑表单记录的权限
        Dim lngInputMode As Long
        lngInputMode = InputMode.EditInHostTable
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.Permission.CanEditFormData = True Then
                lngInputMode = InputMode.EditInHostTable
            Else
                lngInputMode = InputMode.ViewInHostTable
            End If
        End If
        '显示流程流转历史
        CtlFlowHistory1.BindData(oWorklistItem.ActivityInstance.WorkflowInstance.Key)

        If Not Page.IsPostBack Then
            '显示此流程的附件.
            BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)

            '获取当前表单的ID
            If oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID = 0 Then
                Me.MessageBox("此流程的表单未设置！")
            End If

            '判断有没有附件表
            If CmsRes.HasAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) <= 0 Then
                Me.PanelAttachment.Visible = False
            Else
                Me.PanelAttachment.Visible = True
            End If
            '--------------------------------------------------------------------------------------------------
            'Banks 2006-04-04 取消表内字典
            Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, CType(lngInputMode, InputMode), strDefFormName, New ForceRightsInForm)    '处理Postback之后的事务处理。
            Session("CUSTTAB_INPUT_CTLS") = datForm.hashUICtls
            Session("PAGE_POPREL_DATAFORM" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) = datForm
            '--------------------------------------------------------------------------------------------------
        Else
            Try
                '--------------------------------------------------------------------------------------------------
                '保存表单数据.
                'Banks Jin 2006-03-01 A)取消表内字典 B)必须响应子资源的“保存”和“删除”按钮
                Dim strCommand As String = RStr("isfrom")
                If strCommand <> "delrelrec" Then
                    strCommand = "savehostrec"
                End If
                Dim forceRights As New ForceRightsInForm
                forceRights.ForceEnableRecView = True
                PageDealPostBack(CmsRes.ActiveCmsPassport, Request, ViewState, Panel1, Nothing, CType(Session("PAGE_POPREL_DATAFORM" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID), DataInputForm), strCommand, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, RLng("subtabresid"), CType(lngInputMode, InputMode), strDefFormName, forceRights, 0)
                If strCommand = "delrelrec" Then
                    Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, CType(lngInputMode, InputMode), strDefFormName, New ForceRightsInForm)    '处理Postback之后的事务处理。
                    Session("CUSTTAB_INPUT_CTLS") = datForm.hashUICtls
                    Session("PAGE_POPREL_DATAFORM" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) = datForm
                End If
                '--------------------------------------------------------------------------------------------------
            Catch ex As Exception
                Me.MessageBox(ex.Message)
                Me.SetLocation("TranactWorkFlow.aspx?WorklistItemId=" & oWorklistItem.Key)
                IsPageError = True
            End Try
        End If

        '设置页面宽度
        Response.Write("<style>")
        If CmsRes.GetFormWidth(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, strDefFormName) > CLng(FormWidth) - 15 Then
            FormWidth = CmsRes.GetFormWidth(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, strDefFormName) + 15
            Response.Write(".UserForm1{width:" & FormWidth.ToString() & "px;height:" & CStr(CmsRes.GetFormHeight(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, strDefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}")
        Else
            Response.Write(".UserForm1{width:" + FormWidth.ToString() + "px;height:" & CStr(CmsRes.GetFormHeight(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, strDefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}")
        End If
        Response.Write("</style>")
        Me.NodeActionBar1.Width = FormWidth
    End Sub

    Private Sub NodeActionBar1_Click(ByVal sender As Object, ByVal e As String) Handles NodeActionBar1.ItemCommand

        If IsPageError = True Then Return

        'Me.NodeActionBar1.IsExigence = (Request.Form.Item("chkSetIsExigence") = "1")

        Dim oCmsResource As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        'Dim node As NodeItem
        'If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then node = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem)

        oWorklistItem.FormFieldValues = oCmsResource.GetRecordFieldValue(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID) '本资源的字段信息
        If e = "0" Then
            oWorklistItem.Action = New ActionItem("0", "签阅")
        Else
            If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                oWorklistItem.Action = CType(CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem).Actions.Item(e), ActionItem)
            ElseIf oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.StartNode Then
                oWorklistItem.Action = CType(CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions.Item(e), ActionItem)
            End If

        End If

        oWorklistItem.Memo = Me.txtMemo.Text


        '如果后续环节不需要进行人员的选择,则按条件自动处理此流程
        If oWorklistItem.ActivityInstance.WorkflowInstance.Status = NodeStatusConstants.NoActive Then
            Worklist.StartWorkflowInstance(oWorklistItem, AddressOf RedirectEmployeeSelect)       '处理任务
        Else
            Worklist.TransactWorklistItem(oWorklistItem, AddressOf RedirectEmployeeSelect)        '处理任务
        End If

        Response.Redirect(_url)
    End Sub

    Private Sub btnUpload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Load
        Dim m_button As Button = CType(sender, Button)
            'Dim m_upload As Uploader = New Uploader
            'm_upload.RegisterProgressBar(m_button)
    End Sub

    Private Sub RedirectEmployeeSelect(ByVal oWorklistItem As WorklistItem, ByVal link As LinkItem)
        Session("TASK_MEMO") = Me.txtMemo.Text
        Response.Redirect("EmployeeSelect.aspx?ActionId=" + oWorklistItem.Action.Key + "&WorklistItemId=" & oWorklistItem.Key & "&link=" & link.Key & "&url=" & _url, True)
        Response.End()
    End Sub

    '--------------------------------------------------------------------------
    '保存附件
    '--------------------------------------------------------------------------
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim strFileName As String
            'Dim m_upload As Uploader = New Uploader
            'For i As Integer = 0 To m_upload.Request.Headers.Count - 1
            '    SLog.Err(m_upload.Request.Headers.Item(i))
            'Next

            Dim m_file As HttpPostedFile = UploadFile.PostedFile ' m_upload.GetUploadFile("UploadFile")
            If m_file.FileName.IndexOf("\") > 0 Then
                strFileName = m_file.FileName.Substring(m_file.FileName.LastIndexOf("\") + 1)
            Else
                strFileName = m_file.FileName
            End If

        If Not m_file Is Nothing Then
            Dim FileName As String = strFileName
            Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
                CmsRes.AddAttachment(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, FileName, m_file.InputStream, "")
            CmsRes = Nothing
        End If
        BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)

    End Sub

    '--------------------------------------------------------------------------
    '显示此流程的附件.
    '--------------------------------------------------------------------------
    Private Sub BindAttachmentTable(ByVal ResourceID As Long, ByVal RecordID As Long)
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        If CmsRes.HasAttachmentTable(ResourceID) > 0 Then
            Dim AttachmentTable As DataTable = CmsRes.GetAttachments(ResourceID, RecordID)
            'Me.AttachmentList.DataSource = AttachmentTable
            'Me.AttachmentList.DataBind()

            Me.rptAttachmentList.DataSource = AttachmentTable
            Me.rptAttachmentList.DataBind()
        End If
    End Sub

    '--------------------------------------------------------------------------
    '对附件的操作
    '--------------------------------------------------------------------------
    'Private Sub AttachmentList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles AttachmentList.ItemCommand
    '    '删除一个附件.
    '    If e.CommandName = "Delete" Then
    '        Dim lngDocID As Long = Convert.ToInt64(AttachmentList.DataKeys.Item(e.Item.ItemIndex))
    '        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
    '        CmsRes.DeleteAttachment(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, lngDocID)
    '        BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)
    '    End If
    '    '签入附件
    '    If e.CommandName = "CheckIn" Then
    '        BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)
    '    End If
    'End Sub

    'Private Sub AttachmentList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles AttachmentList.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim ResourceId As Long = oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID
    '        Dim dr As DataRowView
    '        Dim btn As Button
    '        dr = CType(e.Item.DataItem, DataRowView)
    '        If Not e.Item.FindControl("btnDelete") Is Nothing Then
    '            '检查有无删除附件的权限
    '            btn = CType(e.Item.FindControl("btnDelete"), Button)
    '            btn.Attributes.Add("onClick", "return window.confirm('确实要删除吗?')")
    '            If oWorklistItem.ActivityInstance.NodeTemplate.Permission.CanDelAttach = False Then
    '                btn.Enabled = False
    '            Else
    '                btn.Enabled = True
    '            End If
    '        End If
    '        If Not e.Item.FindControl("btnOpen") Is Nothing Then
    '            '检查有无查看附件的权限
    '            btn = CType(e.Item.FindControl("btnOpen"), Button)
    '            If oWorklistItem.ActivityInstance.NodeTemplate.Permission.CanViewAttach = False Then
    '                btn.Enabled = False
    '            Else
    '                btn.Enabled = True
    '                'If oWorklistItem.IsCc = True Then
    '                '    btn.Attributes.Add("onClick", "OpenDocFileWindow(" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID & "," & DbField.GetLng(dr, "ID") & ",3);return false;")
    '                'Else
    '                '    btn.Attributes.Add("onClick", "OpenDocFileWindow(" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID & "," & DbField.GetLng(dr, "ID") & ",0);return false;")
    '                'End If
    '                btn.Attributes.Add("onClick", "openfile(" & ResourceId & "," & DbField.GetLng(dr, "ID") & ",'" & DbField.GetStr(dr, "Name") & "');return false;")
    '            End If
    '        End If

    '        If Not e.Item.FindControl("lnkDownloadFile") Is Nothing Then
    '            Dim lnk As LinkButton = CType(e.Item.FindControl("lnkDownloadFile"), LinkButton)
    '            lnk.Text = CStr(dr.Item("Name"))
    '            If oWorklistItem.ActivityInstance.NodeTemplate.Permission.CanViewAttach = True Then
    '                'lnk.Attributes.Add("onClick", "OpenDocFileWindow(" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID & "," & DbField.GetLng(dr, "ID") & ",0);return false;")
    '                lnk.Attributes.Add("onClick", "openfile(" & ResourceId & "," & DbField.GetLng(dr, "ID") & ",'" & DbField.GetStr(dr, "Name") & "." & DbField.GetStr(dr, "ext") & "');return false;")
    '                lnk.Enabled = True
    '            Else
    '                lnk.Enabled = False
    '            End If
    '        End If
    '    End If
    'End Sub

    <AjaxMethod(HttpSessionStateRequirement.Read)> _
    Public Function DeleteAttachment(ByVal ResourceId As Long, ByVal DocumentId As Long) As String
        Try
            Dim CurrentUser As Employee = CType(Session("User"), Employee)
            Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
            CmsRes.DeleteAttachment(ResourceId, DocumentId)
            Return "1"
        Catch ex As Exception
            SLog.Err("删除附件失败,附件信息  ResourceId-" & ResourceId & ",DocumentId-" & DocumentId, ex)
        End Try
        Return "0"
    End Function

End Class

End Namespace
