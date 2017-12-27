Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocRetrieve
    Inherits CmsPage

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
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim lngResID As Long = RLng("mnuresid")
        Dim lngRecID As Long = RLng("mnurecid")
        If lngRecID = 0 Then Throw New CmsException("��ѡ����Ҫ�������ĵ���")

        Dim strCmd As String = RStr("cmsaction")
        Select Case strCmd
            Case "MenuDocCheckout"
                DocCheckout(CmsPass, lngResID, lngRecID)

            Case "MenuDocGet"
                DocGet(CmsPass, lngResID, lngRecID)
        End Select
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '----------------------------------------------------------
    '�ĵ�ǩ��
    '----------------------------------------------------------
    Protected Sub DocCheckout(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long)
        Dim strStatus As String = ResFactory.TableService(pst.GetDataRes(lngResID).ResTableType).GetStatus(pst, lngResID, lngRecID)
        If strStatus = DocVersion.StatusCheckout Then Throw New CmsException("�ļ���ǩ��״̬�������ظ�ǩ����") '�Ѿ���Checkout״̬

        Try
            Dim datDoc As DataDocument = ResFactory.TableService(pst.GetDataRes(lngResID).ResTableType).Checkout(pst, lngResID, lngRecID)
                FileTransfer.DownloadDoc(Response, datDoc)
        Catch ex As Exception
            '�߳�������ֹ�������κβ���
            'CmsError.ThrowEx("ǩ���ĵ�ʧ��", ex, True)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    '----------------------------------------------------------
    '���������ĵ�
    '----------------------------------------------------------
    Protected Sub DocGet(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long)
        Try
            Dim datDoc As DataDocument = ResFactory.TableService(pst.GetDataRes(lngResID).ResTableType).GetDocument(pst, lngResID, lngRecID, True)
                FileTransfer.DownloadDoc(Response, datDoc)
        Catch ex As Exception
            '�߳�������ֹ�������κβ���
            'CmsError.ThrowEx("�����ĵ�ʧ��", ex, True)
        End Try
    End Sub
End Class

End Namespace
