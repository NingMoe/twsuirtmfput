'===========================================================================
' ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
' �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\cmsdocument\Stub_doccheckin_aspx_vb.vb���ĳ������ 
' �̳С�
' ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
' ��������ҳ��
' ����������ҳ��cmsdocument\doccheckin.aspx��Ҳ���޸ģ��������µ�������
' �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Option Strict On
Option Explicit On 

Imports System.IO
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


'Partial Class DocCheckin
Partial Class Migrated_DocCheckin

Inherits DocCheckin

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

        Private strOperation As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_RECID") = 0 Then
            ViewState("PAGE_RECID") = RLng("mnurecid")
            End If

            If Request("operation") IsNot Nothing Then strOperation = Request("operation")

            If strOperation.Trim <> "" Then
                btnCheckin.Text = "�ϴ�"
                lblTitle.Text = "�ϴ��ĵ�"
            End If
        End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnCheckin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckin.Load
        Dim m_button As Button = CType(sender, Button)
            'Dim m_upload As Uploader = New Uploader
            'm_upload.RegisterProgressBar(m_button)
    End Sub
        Private Sub btnCheckin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckin.Click
            Try
                'If File1.PostedFile Is Nothing Then
                '    'û���ļ��ϴ���Checkinʧ��
                'Else
                '    If File1.PostedFile.InputStream.Length > 0 Then
                '        ResFactory.TableService("DOC").Checkin(CmsPass(), VLng("PAGE_RESID"), VLng("PAGE_RECID"), File1.PostedFile.InputStream, File1.PostedFile.FileName)
                '    Else
                '        '"������Ϣ��û���ļ��ϴ���"
                '    End If
                'End If
                ' Dim lngResID As Long = TextboxName.GetResID(strFileCtrlName)
                'Dim m_upload As Uploader = New Uploader
                Dim m_file As HttpPostedFile = File1.PostedFile

                Dim s As Stream = m_file.InputStream
                ResFactory.TableService("DOC").Checkin(CmsPass(), VLng("PAGE_RESID"), VLng("PAGE_RECID"), s, m_file.FileName.Substring(m_file.FileName.LastIndexOf("\") + 1), , , strOperation)
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try
        End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
