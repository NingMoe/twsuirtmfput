Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceMove
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
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnChooseRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseRes.Click
        Try
            Dim lngSelResID As Long = RLng("noderesid")
            If VLng("PAGE_RESID") = lngSelResID Then
                PromptMsg("�����ƶ���Դ������֮�£�")
                Return
            End If

            Dim lngSelDepID As Long = RLng("depid")
            'If lngSelDepID = 0 Then
            '    PromptMsg("��ѡ����ʵĲ��ţ�")
            '    Return
            'End If

            Dim lngMovingResID As Long = VLng("PAGE_RESID")
            ResFactory.ResService.MoveRes(CmsPass, lngMovingResID, lngSelDepID, lngSelResID)
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
