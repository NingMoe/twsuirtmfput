Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SysConfigUpdate
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

    Protected Overrides Sub CmsPageInitialize()
        '��ϵͳ����Ա����ʹ�õĹ�����Ҫ���ô˷���У��
        If CmsPass.EmpIsSysAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        ddlFileType.Items.Clear()
        ddlFileType.Items.Add("")
        ddlFileType.Items.Add("ϵͳ�����ļ�")
        ddlFileType.Items.Add("�ͻ������ļ�")
        ddlFileType.Items.Add("sql")
        ddlFileType.Items.Add("bin")
        ddlFileType.Items.Add("css")
        ddlFileType.Items.Add("script")
        ddlFileType.Items.Add("images")
        ddlFileType.Items.Add("help")
    End Sub


    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            '��ȡ�ϴ��ļ���Ŀ¼
            Dim strDestFileFolder As String = CmsConfig.ProjectRootFolder
            If ddlFileType.SelectedValue <> "" Then
                Select Case ddlFileType.SelectedValue
                    Case "ϵͳ�����ļ�"
                        strDestFileFolder &= "conf\"
                    Case "�ͻ������ļ�"
                        strDestFileFolder &= "conf\client\" & CmsConfig.GetClientCode() & "\"
                    Case "sql"
                        strDestFileFolder &= "sql\"
                    Case "script"
                        strDestFileFolder &= "script\"
                    Case "css"
                        strDestFileFolder &= "css\"
                    Case "bin"
                        strDestFileFolder &= "bin\"
                    Case "images"
                        strDestFileFolder &= "images\"
                    Case "help"
                        strDestFileFolder &= "help\"
                End Select
            ElseIf txtFolder.Text.Trim() <> "" Then
                Dim strTemp As String = txtFolder.Text.Trim()
                strTemp = strTemp.Replace("/", "\")
                strTemp = StringDeal.Trim(strTemp, "\", "\")
                strTemp = StringDeal.Trim(strTemp, "\", "\")
                strDestFileFolder &= strTemp & "\"
            Else
                PromptMsg("��ѡ����Ҫ�ϴ����ļ�����")
                Return
            End If
            Dim strSrcFileName As String = Path.GetFileName(Request.Files(0).FileName)
            Dim strDestFilePath As String = strDestFileFolder & strSrcFileName

            '��ȡ�ϴ����ļ�����
            If Request.Files Is Nothing Then
                PromptMsg("���ϴ���ȷ���ļ���")
                Return
            End If
            If Request.Files.Count <= 0 Then
                PromptMsg("���ϴ���ȷ���ļ���")
                Return
            End If
            Dim sm As System.IO.Stream = Request.Files(0).InputStream
            If sm Is Nothing Then
                PromptMsg("���ϴ���ȷ���ļ���")
                Return
            End If
            If sm.Length <= 0 Then
                PromptMsg("���ϴ���ȷ���ļ���")
                Return
            End If

            'д��Ŀ���ļ�
            Dim br As BinaryReader = New BinaryReader(sm)
            Dim binFile() As Byte = br.ReadBytes(CInt(sm.Length))
            Dim fs As FileStream = New FileStream(strDestFilePath, FileMode.Create, FileAccess.Write)
            fs.Write(binFile, 0, binFile.Length)
            fs.Flush()
            fs.Close()

            CmsConfig.ReloadAll()
            PromptMsg("ϵͳ������Ϣ�ϴ��ɹ���")
        Catch ex As Exception
            PromptMsg("�ļ�ʹ���У��ϴ�ʧ�ܣ�", ex, True)
        End Try
    End Sub
End Class

End Namespace
