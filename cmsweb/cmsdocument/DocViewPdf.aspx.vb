Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocViewPdf
        Inherits Page
#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

        Public cmsurldoc As String = ""

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim DocID As String = Request("DOCID")

            Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(DocID)

          cmsurldoc = CMSDocument.CreateDocumentOnServer(datDoc)
        End Sub
End Class

End Namespace
