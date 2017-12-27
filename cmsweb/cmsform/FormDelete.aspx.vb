Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FormDelete
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

        If VStr("PAGE_FORMNAME") = "" Then
            ViewState("PAGE_FORMNAME") = RStr("mnuformname")
        End If
        If VLng("PAGE_FORMTYPE") = 0 Then
            ViewState("PAGE_FORMTYPE") = RLng("mnuformtype")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResult.Attributes.Add("align", "center")

        'ɾ������
        CTableForm.DelDesignedForms(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_FORMNAME"), VLng("PAGE_FORMTYPE"))
        lblResult.Text = "ɾ��������Ƴɹ���"

        If Not IsStartupScriptRegistered("startup") Then
            Dim strScript As String = "<script language=""javascript"">window.returnValue = '$SUCCESS';</script>"
            RegisterStartupScript("startup", strScript)
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
