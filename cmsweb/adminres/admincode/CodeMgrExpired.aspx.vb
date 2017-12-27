Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CodeMgrExpired
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�
        lblExpireNotes.Text = CmsMessage.GetNotes(pstTemp, "NOTES_PRODCODE_EXPIRE")

        'lblCorpName.Text = OemConfig.GetString("CORP_INFO", "COPR_BRIEFNAME")
        'lblServicePhone.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_PHONE")
        'lblServiceEmail.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_EMAIL")
    End Sub

    Private Sub btnAddLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLicense.Click
        Try
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�
            Dim intLicNum As Integer = CmsCodeLicense.AddLicenseCode(pstTemp, txtCode1.Text.Trim())
            If intLicNum > 0 Then
                Response.Redirect("/cmsweb/Default.htm", False)
            Else
                PromptMsg("��������Ч���û�����롣")
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
