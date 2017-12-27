Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceEditName
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
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        Me.SetFocusOnTextbox("txtResName") '���ü��̹��Ĭ��ѡ�е������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnEditRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditRes.Click
        Try
            ResFactory.ResService.EditResName(CmsPass, VLng("PAGE_RESID"), txtResName.Text.Trim())
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("�޸���Դ����ʧ�ܣ��޷��������ݿ⣡")
        End Try
    End Sub

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
