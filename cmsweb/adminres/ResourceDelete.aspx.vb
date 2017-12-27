Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceDelete
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
        Dim lngResID As Long = VLng("PAGE_RESID")
        Try
            'ɾ�����ݿ��нڵ���Ϣ
            Dim lngParentResID As Long = CmsPass.GetDataRes(lngResID).ResPID
            ResFactory.ResService.DeleteResource(CmsPass, lngResID)
            CmsPass.SetHostResID(lngParentResID) '�������ñ�ɾ����Դ�ĸ���ԴΪ��ǰ��Դ����Ϊ��ǰ��Դ�ձ�ɾ��

            '�޸ĵ�ǰѡ�нڵ�Ϊ��ɾ���ڵ�ĸ��ڵ�
            Dim strUrl As String = VStr("PAGE_BACKPAGE")
            Dim pos As Integer = strUrl.IndexOf("noderesid=")
            Dim len As Integer = 10
            If pos > 0 Then
                Dim strNodeID As String = ""
                Dim pos2 As Integer = strUrl.IndexOf("&", pos)
                If pos2 > 0 Then
                    strNodeID = strUrl.Substring(pos + len, pos2 - pos - len)
                Else
                    strNodeID = strUrl.Substring(pos + len)
                End If
                If strNodeID <> "" Then
                    strUrl = strUrl.Replace(strNodeID, CStr(lngParentResID))
                Else
                    strUrl = strUrl.Substring(0, pos + len) & lngParentResID & strUrl.Substring(pos2)
                End If
            Else
                strUrl = CmsUrl.AppendParam(strUrl, "noderesid=" & lngParentResID)
            End If
            Response.Redirect(strUrl, False)
        Catch ex As Exception
            lblNotes.Text = "ɾ����Դ (" & CmsPass.GetDataRes(lngResID).ResName & ") ʧ�ܣ�������Ϣ��" & ex.Message
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
