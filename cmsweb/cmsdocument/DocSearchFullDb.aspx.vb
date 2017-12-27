Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocSearchFullDb
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
        If CmsFunc.IsEnable("FUNC_TABLETYPE_DOC") = True AndAlso CmsFunc.IsEnable("FUNC_FULLTEXT_SEARCH") = True Then
            txtDocContent.Enabled = True
        Else
            txtDocContent.Enabled = False
            txtDocContent.Text = "(��ǰ�汾��֧��ȫ�ļ���)"
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strDocName As String = txtDocName.Text.Trim()
            Dim strDocExt As String = txtDocExt.Text.Trim()
            Dim strDocKeyword As String = txtDocKeyword.Text.Trim()
            Dim strDocComments As String = txtDocComments.Text.Trim()
            Dim strDocContent As String = txtDocContent.Text.Trim()
            If strDocName = "" AndAlso strDocExt = "" AndAlso strDocKeyword = "" AndAlso strDocComments = "" AndAlso strDocContent = "" Then
                    PromptMsg("����������һ���ѯ��Ϣ/Please enter at least one query information��")
                Return
            End If
            MenuRepeater.DataSource = DocSearch.GetDatasetOfDocFullDbSearch(CmsPass, strDocName, strDocExt, strDocKeyword, strDocComments, strDocContent).Tables(0)
            MenuRepeater.DataBind()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
