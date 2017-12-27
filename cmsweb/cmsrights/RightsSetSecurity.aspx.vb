Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSetSecurity
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtOldPass As System.Web.UI.WebControls.TextBox

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
        SetFocusOnTextbox("txtSecurityPass") '���ü��̹��Ĭ��ѡ�е������
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        '����ϵͳ��ȫԱ����
        If OrgFactory.EmpDriver.ValidatePass(SDbConnectionPool.GetDbConfig(), "security", txtSecurityPass.Text.Trim()) Then
            Session("CMS_QXSECURITY_VERIFIED") = "OK"
            Response.Redirect(SStr("CMSBP_RightsSetSecurity1"), False)
        Else
            PromptMsg("ϵͳ��ȫԱ�ʺ�����������������룡")
            Session("CMS_QXSECURITY_VERIFIED") = Nothing
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Session("CMS_QXSECURITY_VERIFIED") = Nothing
        Response.Redirect(SStr("CMSBP_RightsSetSecurity2"), False)
    End Sub
End Class

End Namespace
