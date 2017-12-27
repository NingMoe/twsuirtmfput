'------------------------------------------------------------------------------
'�����������������̵Ĵ���
'���������TemplateFlowID �û���Ҫ����������ID ��������ģ��ġ�
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

    Private _WorkflowId As String = "0"               '��ǰ����ģ���ID
    Public IsSaveed As Boolean = False
    Public lngRecordID As Long = 0
    Protected FormWidth As Long = 790    '�����Ĭ�Ͽ��
    Private oWorklistItem As WorklistItem

    Private _url As String = "2009/message.aspx"

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Banks Jin 2006-04-24 ����һЩRequest�еı���
        PageSaveParametersToViewState()

        If Request.QueryString("url") <> "" Then _url = Request.QueryString("url")

        _WorkflowId = Request.QueryString("WorkflowId")        '��ǰ���̵�ID
        Workflow = WorkflowFactory.CreateInstance(_WorkflowId, CurrentUser)   '��ǰ�Ĺ���������
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        If Not Page.IsPostBack Then
            '��ȡ��ǰ����ID
            If Workflow.ResourceID = 0 Then
                Me.MessageBox("�����̵ı�δ���ã�")
            End If

            '���̲�������
            NodeActionBar1.Actions = Workflow.WorkflowTemplate.StartNode.Actions      '��ȡ��ʼ���ڵĳ��ڶ���
            NodeActionBar1.ViewDiagramHref = "ViewFlowHistroy.aspx?action=create&TemplateflowId=" & _WorkflowId

            '�ж���û�����̸�����.�����,��ô����ʾ
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
            '���ô�ҳ��ʹ�õĴ���
            Dim DefFormName As String = CTableForm.DEF_DESIGN_FORM
            If Workflow.WorkflowTemplate.StartNode.UseForm <> "" Then DefFormName = Workflow.WorkflowTemplate.StartNode.UseForm

            'Banks 2006-04-04 ȡ�������ֵ�
            Dim forceRights As New ForceRightsInForm
            forceRights.ForceEnableRecView = True
            Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, Workflow.ResourceID, Workflow.RecordID, InputMode.AddInHostTable, DefFormName, forceRights)  '����Postback֮���������

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
            '����GET��POST�е�������أ�True���˳����ӿں�ֱ���˳����壻False���˳����ӿں����֮��Ĵ���
            'Banks 2006-04-04 ȡ�������ֵ�
            Try
                Dim forceRights As New ForceRightsInForm
                forceRights.ForceEnableRecView = True
                Dim blnRtn As Boolean = PageDealPostBack(CmsRes.ActiveCmsPassport, Request, ViewState, Panel1, Nothing, CType(Session("PAGE_POPREL_DATAFORM" & Workflow.ResourceID & "_" & Viewstate("SessionId").ToString()), DataInputForm), "savehostrec", 0, 0, Workflow.ResourceID, lngRecordID, RLng("subtabresid"), InputMode.AddInHostTable, CTableForm.DEF_DESIGN_FORM, forceRights, 0)

                Workflow.RecordID = lngRecordID
                Dim hashFieldValue As Hashtable = CmsRes.GetRecordFieldValue(Workflow.ResourceID, lngRecordID)

                Dim oWorkflowInstance As WorkflowInstance = WorkflowFactory.CreateInstance(_WorkflowId, Me.CurrentUser)
                oWorkflowInstance.RecordID = lngRecordID
                oWorklistItem = oWorkflowInstance.Create(hashFieldValue)

                'Banks Jin 2006-03-03 ���BUG����������Դʱ���Ͻǵ��ύ��ť��ʧ
                If RStr("isfrom") = "savehostrec" Then Response.Redirect("TranactWorkFlow.aspx?WorklistItemId=" & oWorklistItem.Key)

            Catch ex As Exception
                Me.MessageBox(ex.Message)
                Me.SetLocation("CreateWorkFlow.aspx?WorkflowId=" & _WorkflowId)
                IsPageError = True
            End Try
        End If
    End Sub

    '���������������
    Private Sub NodeActionBar1_ItemCommand1(ByVal sender As Object, ByVal e As String) Handles NodeActionBar1.ItemCommand
        If IsPageError = True Then Return

        'Me.NodeActionBar1.IsExigence = (Request.Form.Item("chkSetIsExigence") = "1")

        Dim oCmsResource As New CmsResource(CurrentUser.Code, CurrentUser.Password)
        oWorklistItem.FormFieldValues = oCmsResource.GetRecordFieldValue(Workflow.ResourceID, Workflow.RecordID) '�����ݿ���ȡ�øղű��������
        Select Case oWorklistItem.ActivityInstance.NodeTemplate.Type
            Case NodeTypeConstants.StartNode : oWorklistItem.Action = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions(e)
            Case NodeTypeConstants.MiddleNode : oWorklistItem.Action = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem).Actions(e)
        End Select

        'oWorklistItem.Action = oWorklistItem.ActivityInstance.NodeTemplate.Actions(e)

        '����������ڲ���Ҫѡ����Ա,��ôֱ�ӽ������´���
        If oWorklistItem.ActivityInstance.WorkflowInstance.Status = NodeStatusConstants.NoActive Then
            Worklist.StartWorkflowInstance(oWorklistItem, AddressOf RedirectEmployeeSelect)      '��������
        Else
            Worklist.TransactWorklistItem(oWorklistItem, AddressOf RedirectEmployeeSelect)       '��������
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
    '�ϴ�һ������
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
