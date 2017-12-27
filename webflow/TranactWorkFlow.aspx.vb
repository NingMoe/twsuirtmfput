Option Explicit On 
Option Strict On


'�����������������̵�������
'���������TaskID ��ǰ�û������ID ���������е����̵ġ�

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

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub

    Protected ResourceId As Long = 0


    Protected WithEvents AttachmentList As System.Web.UI.WebControls.DataGrid

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Protected FormWidth As Long = 790    '�����Ĭ�Ͽ��

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

        PageSaveParametersToViewState()                                                         'Banks Jin 2006-04-24 ����һЩRequest�еı���

        _WorklistItemId = Request.QueryString("WorklistItemId")

        oWorklistItem = Worklist.GetWorklistItem(_WorklistItemId)
        ResourceId = oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then node = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem)
        '���̲�������
        If oWorklistItem.Status = TaskStatusConstants.Actived Or oWorklistItem.Status = TaskStatusConstants.Reserved Then
            If oWorklistItem.IsCc Then
                Dim act As New ActionItem("0", "ǩ��")
                NodeActionBar1.Actions.Add(act)
            Else
                If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                    NodeActionBar1.Actions = node.Actions '��ȡ��ʼ���ڵĳ��ڶ���
                ElseIf oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.StartNode Then
                    NodeActionBar1.Actions = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions
                    'oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode
                    'node = CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem)
                End If
            End If
        End If


        '������޲鿴������ʷ��Ȩ��
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
                Me.txtMemo.Attributes.Add("errmsg", "�������")
            End If
        End If

        '����������Ӹ�����Ȩ��,��ʼ���ڲ��ж�Ȩ��
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.StartNode Then
            Me.PanelAttachmentAdd.Visible = True
            Me.btnUpload.Visible = True
        Else
            If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                Me.PanelAttachmentAdd.Visible = node.Permission.CanAddAttach
                Me.btnUpload.Visible = node.Permission.CanAddAttach

            '������޸����༭��Ȩ��
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

        '������ʾ����
        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)

        '���������д˵����Ȩ��
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.Permission.CanWriteMemo = True Then
                Me.txtMemo.Enabled = True
            Else
                Me.txtMemo.Enabled = False
            End If
        End If

        '��ʾΪ�������õĴ���
        Dim strDefFormName As String = CTableForm.DEF_DESIGN_FORM
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.UseForm <> "" Then
                strDefFormName = node.UseForm
            End If
        End If
        '*****��ȡ���û���ָ������*****
        Dim strOrganizationForm As String = oWorklistItem.WorkFormName
        If strOrganizationForm <> "" Then strDefFormName = strOrganizationForm

        '����Ƿ��б༭����¼��Ȩ��
        Dim lngInputMode As Long
        lngInputMode = InputMode.EditInHostTable
        If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
            If node.Permission.CanEditFormData = True Then
                lngInputMode = InputMode.EditInHostTable
            Else
                lngInputMode = InputMode.ViewInHostTable
            End If
        End If
        '��ʾ������ת��ʷ
        CtlFlowHistory1.BindData(oWorklistItem.ActivityInstance.WorkflowInstance.Key)

        If Not Page.IsPostBack Then
            '��ʾ�����̵ĸ���.
            BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)

            '��ȡ��ǰ����ID
            If oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID = 0 Then
                Me.MessageBox("�����̵ı�δ���ã�")
            End If

            '�ж���û�и�����
            If CmsRes.HasAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) <= 0 Then
                Me.PanelAttachment.Visible = False
            Else
                Me.PanelAttachment.Visible = True
            End If
            '--------------------------------------------------------------------------------------------------
            'Banks 2006-04-04 ȡ�������ֵ�
            Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, CType(lngInputMode, InputMode), strDefFormName, New ForceRightsInForm)    '����Postback֮���������
            Session("CUSTTAB_INPUT_CTLS") = datForm.hashUICtls
            Session("PAGE_POPREL_DATAFORM" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID) = datForm
            '--------------------------------------------------------------------------------------------------
        Else
            Try
                '--------------------------------------------------------------------------------------------------
                '���������.
                'Banks Jin 2006-03-01 A)ȡ�������ֵ� B)������Ӧ����Դ�ġ����桱�͡�ɾ������ť
                Dim strCommand As String = RStr("isfrom")
                If strCommand <> "delrelrec" Then
                    strCommand = "savehostrec"
                End If
                Dim forceRights As New ForceRightsInForm
                forceRights.ForceEnableRecView = True
                PageDealPostBack(CmsRes.ActiveCmsPassport, Request, ViewState, Panel1, Nothing, CType(Session("PAGE_POPREL_DATAFORM" & oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID), DataInputForm), strCommand, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, RLng("subtabresid"), CType(lngInputMode, InputMode), strDefFormName, forceRights, 0)
                If strCommand = "delrelrec" Then
                    Dim datForm As DataInputForm = PageDealFirstRequest(CmsRes.ActiveCmsPassport, Panel1, 0, 0, oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, CType(lngInputMode, InputMode), strDefFormName, New ForceRightsInForm)    '����Postback֮���������
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

        '����ҳ����
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

        oWorklistItem.FormFieldValues = oCmsResource.GetRecordFieldValue(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID) '����Դ���ֶ���Ϣ
        If e = "0" Then
            oWorklistItem.Action = New ActionItem("0", "ǩ��")
        Else
            If oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.MiddleNode Then
                oWorklistItem.Action = CType(CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeItem).Actions.Item(e), ActionItem)
            ElseIf oWorklistItem.ActivityInstance.NodeTemplate.Type = NodeTypeConstants.StartNode Then
                oWorklistItem.Action = CType(CType(oWorklistItem.ActivityInstance.NodeTemplate, NodeStart).Actions.Item(e), ActionItem)
            End If

        End If

        oWorklistItem.Memo = Me.txtMemo.Text


        '����������ڲ���Ҫ������Ա��ѡ��,�������Զ����������
        If oWorklistItem.ActivityInstance.WorkflowInstance.Status = NodeStatusConstants.NoActive Then
            Worklist.StartWorkflowInstance(oWorklistItem, AddressOf RedirectEmployeeSelect)       '��������
        Else
            Worklist.TransactWorklistItem(oWorklistItem, AddressOf RedirectEmployeeSelect)        '��������
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
    '���渽��
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
    '��ʾ�����̵ĸ���.
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
    '�Ը����Ĳ���
    '--------------------------------------------------------------------------
    'Private Sub AttachmentList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles AttachmentList.ItemCommand
    '    'ɾ��һ������.
    '    If e.CommandName = "Delete" Then
    '        Dim lngDocID As Long = Convert.ToInt64(AttachmentList.DataKeys.Item(e.Item.ItemIndex))
    '        Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
    '        CmsRes.DeleteAttachment(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, lngDocID)
    '        BindAttachmentTable(oWorklistItem.ActivityInstance.WorkflowInstance.ResourceID, oWorklistItem.ActivityInstance.WorkflowInstance.RecordID)
    '    End If
    '    'ǩ�븽��
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
    '            '�������ɾ��������Ȩ��
    '            btn = CType(e.Item.FindControl("btnDelete"), Button)
    '            btn.Attributes.Add("onClick", "return window.confirm('ȷʵҪɾ����?')")
    '            If oWorklistItem.ActivityInstance.NodeTemplate.Permission.CanDelAttach = False Then
    '                btn.Enabled = False
    '            Else
    '                btn.Enabled = True
    '            End If
    '        End If
    '        If Not e.Item.FindControl("btnOpen") Is Nothing Then
    '            '������޲鿴������Ȩ��
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
            SLog.Err("ɾ������ʧ��,������Ϣ  ResourceId-" & ResourceId & ",DocumentId-" & DocumentId, ex)
        End Try
        Return "0"
    End Function

End Class

End Namespace
