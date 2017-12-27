
Imports NetReusables
Imports System.Text
Imports System.IO
Imports System.Xml

Imports Unionsoft.Implement
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


Partial Class ResourceImport
    Inherits CmsPage

    Private mstrDataSource As String
    Private lngResID As Long
    Private lngDepID As Long

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
        If Me.IsPostBack = False Then
            lngResID = CType(Request.QueryString("mnuresid"), Long)
            lngDepID = CType(Request.QueryString("depId"), Long)
        End If
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Me.SetFocusOnTextbox("txtResName") '���ü��̹��Ĭ��ѡ�е������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click

        'If File1.PostedFile.FileName <> Nothing Or File1.PostedFile.FileName <> "" Then
            ' Dim m_upload As Uploader = New Uploader
            Dim m_file As HttpPostedFile = File1.PostedFile
        Dim ext As String = m_file.FileName.Substring(m_file.FileName.LastIndexOf(".") + 1)

        If ext.ToLower() = "xml" Then
            Try
                ' mstrDataSource = File1.PostedFile.FileName
                mstrDataSource = Server.MapPath("../temp/xml")
                If Not Directory.Exists(mstrDataSource) Then
                    Directory.CreateDirectory(mstrDataSource)
                End If
                mstrDataSource = mstrDataSource + "\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"
                m_file.SaveAs(mstrDataSource)
                Dim ResIE As New ResourceImportExport
                ResIE.ImportResource(CmsPass, RLng("mnuresid"), RLng("depId"), mstrDataSource)
                Response.Write("<script language='javascript'>alert('������Դ�ɹ�������'); </script>")
            Catch ex As Exception
                SLog.Err("������Դʧ�� " & ex.Message)
                Response.Write("<script language='javascript'>alert('������Դʧ�ܣ�����'); </script>")
            End Try
        Else
            Response.Write("<script language='javascript'>alert('��ѡ��.xml��ʽ����Ч���ļ�������'); </script>")
        End If
        'Else
        'Response.Write("<script language='javascript'>alert('��ѡ����Ч���ļ�������'); </script>")
        'End If
    End Sub

    Public Function FileType(ByVal fileName As String) As Boolean
        Dim name As String() = fileName.Split(CType(".", Char))
        If name(name.Length - 1).ToString() = "xml" Or name(name.Length - 1).ToString() = "XML" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        'Dim aa As String = Session("PAGE_BACKPAGE").ToString()
        Response.Redirect(Session("CMSBP_ResourceCopy").ToString(), False)
    End Sub
End Class

End Namespace
