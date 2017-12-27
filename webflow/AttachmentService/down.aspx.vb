Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class down
    Inherits System.Web.UI.Page

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
        '��ȡ�ĵ�����
        Dim lngResourceID As Long = CLng(Request.QueryString("ResourceID"))
        Dim lngDocumentID As Long = CLng(Request.QueryString("DocumentID"))
        Dim checkout As Long = 0
        If IsNumeric(Request.QueryString("checkout")) = True Then
            checkout = CLng(Request.QueryString("checkout"))
        End If

            Dim pst As Unionsoft.Platform.CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("")
        If checkout = 1 Then ResFactory.TableService(pst.GetDataRes(lngResourceID).ResTableType).Checkout(pst, lngResourceID, lngDocumentID)

        Dim datDoc As DataDocument = CmsDocFlow.GetOneAttachment(pst, lngResourceID, lngDocumentID, True)
        FileTransfer.DownloadDoc(Response, datDoc)

    End Sub

End Class

End Namespace
