Option Strict On
Option Explicit On 

Imports System.IO
Imports System.Web

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SvcDocRemoteImport
    Inherits AspPage

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

    Private Const MSG_SIGN1 As String = "<!--[[CMS_RESPONSE_MSG=" '"[[CMS_RESPONSE_MSG="
    Private Const MSG_SIGN2 As String = "]]-->" '"]]"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IsPostBack Then Return

            ''----------------------------------------------------------
            ''�����Ƿ����ĵ��ϴ�����û�����˳�
            'Dim lngFileNum As Long = Request.Files.Count()
            'If lngFileNum <= 0 Then
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("û���ĵ�����"))
            '    Return
            'ElseIf lngFileNum > 1 Then
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("����ͬʱ�������ĵ�"))
            '    Return
            'End If
            ''----------------------------------------------------------

            ''----------------------------------------------------------
            ''�����û���½�ʺź�����
            'Dim pst As New CmsPassport
            'pst.EmpID = RStr("UFILE_USERID")
            'pst.EmpPass = RStr("UFILE_USERPASS")
            ''----------------------------------------------------------

            ''----------------------------------------------------------
            ''��ȡ�ĵ��ϴ�����ԴID�ͱ���
            'Dim strCmd As String = RStr("UFILE_CMD")
            'Dim lngResID As Long = RLng("UFILE_RESID")
            'If lngResID <= 0 Then Return
            'Dim strResHostTable As String = ResFactory.ResService.GetResTable(lngResID)
            ''----------------------------------------------------------

            ''----------------------------------------------------------
            ''���ÿյ��ĵ�˵����Ϣ
            'Dim hashFieldVal As New Hashtable
            'hashFieldVal.Add("DOC2_COMMENTS", "")
            'hashFieldVal.Add("DOC2_KEYWORDS", "")
            ''----------------------------------------------------------

            ''����ĵ������ݿ���
            'If strCmd = "checkin" Then '��ǩ��
            '    ResFactory.TableService("DOC").CheckinByName(pst, lngResID, Request.Files(0).InputStream, Request.Files(0).FileName)
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("ǩ���ĵ��ɹ�"))
            'ElseIf strCmd = "addnew" Then '���롢����
            '    ResFactory.TableService("DOC").AddRecord(pst, lngResID, 0, hashFieldVal, Nothing, Request.Files(0).InputStream, Request.Files(0).FileName)
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("�����ĵ��ɹ�"))
            'End If
        Catch ex As Exception
            Response.Write(DocuploadGenerateRespMsg(ex.Message))
            SLog.Err("�ϴ��ĵ�ʧ�ܣ�", ex)
        End Try
    End Sub

    Public Shared Function DocuploadGenerateRespMsg(ByVal strRespMsg As String) As String
        Return MSG_SIGN1 & strRespMsg & MSG_SIGN2
    End Function

    Public Shared Function DocuploadRetrieveRespMsg(ByVal strRespMsg As String) As String
        Dim pos1 As Integer = strRespMsg.IndexOf(MSG_SIGN1)
        Dim pos2 As Integer = strRespMsg.IndexOf(MSG_SIGN2, pos1)
        Return strRespMsg.Substring(pos1 + MSG_SIGN1.Length, pos2 - pos1 - MSG_SIGN1.Length)
    End Function
End Class

End Namespace
