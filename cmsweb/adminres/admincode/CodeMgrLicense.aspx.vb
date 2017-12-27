Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CodeMgrLicense
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
        txtCurrentLicenseNum.ReadOnly = True
        txtCreatedUserNum.ReadOnly = True

        'lblCorpName.Text = OemConfig.GetString("CORP_INFO", "COPR_BRIEFNAME")
        'lblServicePhone.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_PHONE")
        'lblServiceEmail.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_EMAIL")

        Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�
        lblLicCodeNotes.Text = CmsMessage.GetNotes(pstTemp, "NOTES_LICCODE_INTRODUCE")

        If Me.IsPostBack = False Then
            Try
                Dim lngCurEmpNum As Long = CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), CmsTables.Employee, "") - 3  '��ȡ��ǰ�û���: ��ȥ3��ϵͳ����Ա�ʺ�
                txtCreatedUserNum.Text = CStr(lngCurEmpNum)

                Dim intTotalLicNum As Integer = CmsCodeLicense.GetTotalLicenseNumber(pstTemp)
                txtCurrentLicenseNum.Text = CStr(intTotalLicNum)
            Catch ex As Exception
                txtCurrentLicenseNum.Text = "0"
                PromptMsg("", ex, True)
            End Try
        End If
    End Sub

    Private Sub btnAddLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLicense.Click
        Try
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�
            Dim intTotalLicNum As Integer = CmsCodeLicense.AddLicenseCode(pstTemp, txtCode1.Text.Trim())
            txtCurrentLicenseNum.Text = CStr(intTotalLicNum) '���µ�ǰ�û������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
