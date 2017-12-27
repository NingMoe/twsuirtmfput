Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class DocOpen
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
        '��ȡ�ĵ�����
        Dim lngResID As Long = RLng("mnuresid")
        If lngResID = 0 Then
            lngResID = RLng("resid")
        End If
        Dim lngRecID As Long = RLng("docrecid")
        If lngRecID = 0 Then
            lngRecID = RLng("mnurecid")
        End If
        Dim datDoc As DataDocument = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetDocument(CmsPass, lngResID, lngRecID, True)

        '��ʾ�ĵ�����
            Dim lngDocOpenStyle As Long = RLng("docopenstyle")
            If lngDocOpenStyle = 1 Then '��ȡ�ĵ�
                FileTransfer.DownloadDoc(Response, datDoc)
            ElseIf lngDocOpenStyle = 2 Then '��������ĵ�
                FileTransfer.ShowDoc(Response, datDoc)
            Else '�������ļ���Ϣ������δ��ĵ�
                If CmsConfig.GetInt("SYS_CONFIG", "DOCFILE_GET_STYLE") = 0 Then
                    FileTransfer.DownloadDoc(Response, datDoc)
                Else
                    FileTransfer.ShowDoc(Response, datDoc)
                End If
            End If

    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
