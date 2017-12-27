Option Strict On
Option Explicit On 

Imports System.IO
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceExportResult
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
        lblResult.Text = SStr("CMS_IMPRES_MESSAGE")

        If RLng("result") <> 1 Then
            btnDownload.Enabled = False
        Else
            btnDownload.Enabled = True
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DownloadDoc()
    End Sub

    Public Sub DownloadDoc()
        Try
            '����Encode�ļ����ƣ���������
            Dim strFilePath As String = SStr("CMSTRANS_EXPORTFILE")
            Dim strFileName As String = Path.GetFileNameWithoutExtension(strFilePath) ' & "." & Path.GetExtension(strFilePath)
            strFileName = strFileName.Substring(0, strFileName.IndexOf("__"))
            strFileName = strFileName & Path.GetExtension(strFilePath)
            strFileName = HttpUtility.UrlEncode(strFileName)
            Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
            Response.ContentType = "application/octet-stream"

            '��ȡ�ļ�
            Dim fs As FileStream = New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)

            '��ʼ��ͻ���д�ļ���
            Response.BinaryWrite(br.ReadBytes(CInt(fs.Length)))
            Response.Flush()

            '�����Response.End()֮ǰ����
            br.Close()
            fs.Close()

            Response.End()
        Catch ex As Exception
            '�߳�������ֹ�������κβ���
            '����Response.End()�󣬱�Ȼ��������
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
