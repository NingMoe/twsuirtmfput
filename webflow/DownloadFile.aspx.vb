Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web



Partial Class DownloadFile
    Inherits PageBase


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
        Try
            Dim datDoc As DataDocument
            datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
            'datDoc = CmsDocFlow.GetOneAttachment((New CmsResource(MyBase.CurrentUser.Code, MyBase.CurrentUser.Password)).GetPst, lngResourceID, lngDocumentID, True)
            'Dim datDoc As DataDocument = ResFactory.TableService(New CmsResource(MyBase.CurrentUser.Code, MyBase.CurrentUser.Password)).GetPst.GetDataRes(lngResourceID).ResTableType).GetDocument(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
            FileTransfer.DownloadDoc(Response, datDoc)
        Catch ex As Exception
            SLog.Err("���ظ��������쳣.", ex)
        End Try

    End Sub

End Class

End Namespace
