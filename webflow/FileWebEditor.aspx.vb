Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class FileWebEditor
    Inherits UserPageBase

    Protected Url As String                 '�ļ���Url
    Protected lngIsCheckOut As Long = 0
    Protected strCurrentName As String

    Protected strDocumentExtName As String = ""

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
        Dim lngResourceID As Long = CLng(Request.QueryString("ResourceID"))
        Dim lngDocumentID As Long = CLng(Request.QueryString("DocumentID"))
        strCurrentName = Me.CurrentUser.Name
        lngIsCheckOut = CLng(Request.QueryString("IsCheckOut"))
        Dim datDoc As DataDocument = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
        'Dim datDoc As DataDocument = CmsDocFlow.GetOneAttachment((New CmsResource(MyBase.CurrentUser.Code, MyBase.CurrentUser.Password)).GetPst, lngResourceID, lngDocumentID, True)
        If Not datDoc Is Nothing Then
            Me.txtDocumentInfo.Text = lngResourceID & "_" & lngDocumentID
            strDocumentExtName = datDoc.strDOC2_EXT.ToLower()
        End If


        Url = "DownloadFile.aspx?ResourceID=" & lngResourceID & "&DocumentID=" & lngDocumentID

        Me.btnUpdateFile.Attributes.Add("onClick", "TANGER_OCX_SaveEditToServerDisk();return false;")

        '�ӻ����л�ȡ���ĵ����������ʱ��,�������ʱ����10������,��ô�Ͳ����������ٸ�������ļ�
        Me.btnUpdateFile.Enabled = True
        If lngIsCheckOut <> 3 Then
            If Configuration.DocumentAccessCache.Contains(CStr(lngDocumentID)) Then
                If CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode <> Me.CurrentUser.Code Then
                    If DateDiff(DateInterval.Minute, CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).AccessTime, Now) <= 10 Then
                        Me.btnUpdateFile.Enabled = False
                        Me.btnUpdateFile.Text = "���ļ����ڱ�[" & OrganizationFactory.Implementation.GetEmployee(CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode).Name & "]�༭"
                    Else
                        Configuration.DocumentAccessCache(CStr(lngDocumentID)) = New DocumentAccessLog(MyBase.CurrentUser.Code)
                    End If
                End If
            Else
                Configuration.DocumentAccessCache.Add(CStr(lngDocumentID), New DocumentAccessLog(MyBase.CurrentUser.Code))
            End If
        Else
            Me.btnUpdateFile.Enabled = False
        End If
    End Sub

End Class

End Namespace
