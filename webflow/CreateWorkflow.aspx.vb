'------------------------------------------------------------------------------
'功能描述：工作流程的创建
'传入参数：TemplateFlowID 用户需要创建的流程ID ，是流程模板的。
'------------------------------------------------------------------------------
'Imports Unionsoft.WebControls.Uploader 'Webb.WAVE.Controls.Upload
Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web



Partial Class CreateWorkflow
    Inherits RecordEditBase

    Protected WithEvents AttachmentList As System.Web.UI.WebControls.Repeater
    Private Workflow As WorkflowInstance
    Private IsPageError As Boolean = False

    Private _WorkflowId As String = "0"               '当前流程模板的ID
    Public IsSaveed As Boolean = False
    Public lngRecordID As Long = 0
    Protected FormWidth As Long = 790    '窗体的默认宽度
    Private oWorklistItem As WorklistItem

    Private _url As String = "2009/message.aspx"

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

        'Banks Jin 2006-04-24 保存一些Request中的变量
        PageSaveParametersToViewState()

        If Request.QueryString("url") <> "" Then _url = Request.QueryString("url")

        _WorkflowId = Request.QueryString("WorkflowId")        '当前流程的ID
        Workflow = WorkflowFactory.CreateInstance(_WorkflowId, CurrentUser)   '当前的工作流对象
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        If Not Page.IsPostBack Then
            '获取当前表单的ID
            If Workflow.ResourceID = 0 Then
                Me.MessageBox("此流程的表单未设置！")
            End If

            '流程操作定义
            NodeActionBar1.Actions = Workflow.WorkflowTemplate.StartNode.Actions      '获取起始环节的出口动作
            NodeActionBar1.ViewDiagramHref = "ViewFlowHistroy.aspx?action=create&TemplateflowId=" & _WorkflowId

            '判断有没有流程附件表.如果有,那么就显示
            If CmsRes.HasAttachmentTable(Workflow.ResourceID) <> 0 Then
                Me.PanelAttachment.Visible = True
            Else
                Me.PanelAttachment.Visible = False
            End If

            'If Not Workflow.WorkflowTemplate.StartNode.Permission.CanWriteMemo Then
            '    Me.PanelMemo.Visible = False
            'Else
            '    Me.PanelMemo.Visible = True
            'End If

            '-------------------------------------------------------------------------------------------
            'banks add 2005-11-29
            '设置此页面使用的窗体
            Dim DefFormName As String = CTableForm.DEF_DESIGN_FORM
            If Workflow.WorkflowTemplate.StartNode.UseForm <> "" Then DefFormName = Workflow.WorkflowTemplate.StartNode.UseForm

            'Banks 2006-04-04 取消表内字典
            Dim forceRights As New ForceRightsInForm
            forceRights.ForceEnableRecView = True
            Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, Workflow.ResourceID, Workflow.RecordID, InputMode.AddInHostTable, DefFormName, forceRights)  '处理Postback之后的事务处理。

            Response.Write("<style>")
            If CmsRes.GetFormWidth(Workflow.ResourceID, DefFormName) >= CLng(FormWidth) - 15 Then
                FormWidth = CmsRes.GetFormWidth(Workflow.ResourceID, DefFormName) + 15
                Response.Write(".UserForm1{width:" & FormWidth.ToString() & "px;height:" & CStr(CmsRes.GetFormHeight(Workflow.ResourceID, DefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}")
            Else
                Response.Write(".UserForm1{width:790px;height:" & CStr(CmsRes.GetFormHeight(Workflow.ResourceID, DefFormName)) & "px;border:1px solid #cccccc;overflow:auto;}")
            End If
            Me.NodeActionBar1.Width = FormWidth
            Response.Write("</style>")

            Session("CUSTTAB_INPUT_CTLS") = datForm.hashUICtls
            Dim SessionId As Long = TimeId.CurrentMilliseconds()
            Viewstate("SessionId") = SessionId
            Session("PAGE_POPREL_DATAFORM" & Workflow.ResourceID & "_" & SessionId) = datForm

        Else
            'Banks added 2005-11-30
            'Dim lngRecordID As Long = 0
            '处理GET或POST中的命令。返回：True：退出本接口后直接退出窗体；False：退出本接口后继续之后的处理
            'Banks 2006-04-04 取消表内字典
            Try
                Dim forceRights As New ForceRightsInForm
                forceRights.ForceEnableRecView = True
                Dim blnRtn As Boolean = PageDealPostBack(CmsRes.ActiveCmsPassport, Request, ViewState, Panel1, Nothing, CType(Session("PAGE_POPREL_DATAFORM" & Workflow.ResourceID & "_" & Viewstate("SessionId").ToString()), DataInputForm), "savehostrec", 0, 0, Workflow.ResourceID, lngRecordID, RLng("subtabresid"), InputMode.AddInHostTable, CTableForm.DEF_DESIGN_FORM, forceRights, 0)

                Workflow.RecordID = lngRecordID
                Dim hashFieldValue As Hashtable = CmsRes.GetRecordFieldValue(Workflow.ResourceID, lngRecordID)

                Dim oWorkflowInstance As WorkflowInstance = WorkflowFactory.CreateInstance(_WorkflowId, Me.CurrentUser)
                oWorkflowInstance.RecordID = lngRecordID
                oWorklistItem = oWorkflowInstance.Create(hashFieldValue)

                'Banks Jin 2006-03-03 解决BUG：保存子资源时左上角的提交按钮消失
                If RStr("isfrom") = "savehostrec" Then Response.Redirect("TranactWorkFlow.aspx?WorklistItemId=" & oWorklistItem.Key)

            Catch ex As Exception
                Me.MessageBox(ex.Message)
                Me.SetLocation("CreateWorkFlow.aspx?WorkflowId=" & _WorkflowId)
                IsPageError = True
            End Try
        End If
    End Sub

    '创建并激活此流程
    Private Sub NodeActionBar1_ItemCommand1(ByVal sender As Object, ByVal e As String) Handles NodeActionBar1.ItemCommand
        If IsPageError = True Then Return

        'Me.NodeActionBar1.IsExigence = (Request.Form.Item("chkSetIsExigence") = "1")

        Dim oCmsResource As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        oWorklistItem.FormFieldValues = oCmsResource.GetRecordFieldValue(Workflow.ResourceID, Workflow.RecordID) '从数据库中取得刚才保存的数据
        Select Case oWorklistItem.ActivityInstance.NodeTemplate.Type
            Case NodeTypeConstants.StartNode : oWorklistItem.Action = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions(e)
            Case NodeTypeConstants.MiddleNode : oWorklistItem.Action = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem).Actions(e)
        End Select

        'oWorklistItem.Action = oWorklistItem.ActivityInstance.NodeTemplate.Actions(e)

        '如果后续环节不需要选择人员,那么直接进行以下处理
        If oWorklistItem.ActivityInstance.WorkflowInstance.Status = NodeStatusConstants.NoActive Then
            Worklist.StartWorkflowInstance(oWorklistItem, AddressOf RedirectEmployeeSelect)      '处理任务
        Else
            Worklist.TransactWorklistItem(oWorklistItem, AddressOf RedirectEmployeeSelect)       '处理任务
        End If

        Response.Redirect(_url)
    End Sub

    Private Sub RedirectEmployeeSelect(ByVal oWorklistItem As WorklistItem, ByVal link As LinkItem)
        Session("TASK_MEMO") = Me.txtMemo.Text
        Response.Redirect("EmployeeSelect.aspx?ActionId=" + oWorklistItem.Action.Key + "&WorklistItemId=" & oWorklistItem.Key & "&link=" & link.Key & "&url=" & _url, True)
        Response.End()
    End Sub

    Private Sub btnUpload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Load
        Dim m_button As Button = CType(sender, Button)
            'Dim m_upload As Uploader = New Uploader
            'm_upload.RegisterProgressBar(m_button)
    End Sub

    '-------------------------------------------------------------------------------------------
    '上传一个附件
    '-------------------------------------------------------------------------------------------
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If IsPageError = True Then Exit Sub

        Dim strFileName As String
            'Dim m_upload As Uploader = New Uploader

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
            Response.Redirect("TranactWorkFlow.aspx?WorklistItemId=" & oWorklistItem.Key)
        End If
    End Sub

End Class

End Namespace
