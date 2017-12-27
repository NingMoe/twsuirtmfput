Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CodeMgrLicense
    Inherits AspPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtCurrentLicenseNum.ReadOnly = True
        txtCreatedUserNum.ReadOnly = True

        'lblCorpName.Text = OemConfig.GetString("CORP_INFO", "COPR_BRIEFNAME")
        'lblServicePhone.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_PHONE")
        'lblServiceEmail.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_EMAIL")

        Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库
        lblLicCodeNotes.Text = CmsMessage.GetNotes(pstTemp, "NOTES_LICCODE_INTRODUCE")

        If Me.IsPostBack = False Then
            Try
                Dim lngCurEmpNum As Long = CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), CmsTables.Employee, "") - 3  '获取当前用户数: 减去3个系统管理员帐号
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
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库
            Dim intTotalLicNum As Integer = CmsCodeLicense.AddLicenseCode(pstTemp, txtCode1.Text.Trim())
            txtCurrentLicenseNum.Text = CStr(intTotalLicNum) '更新当前用户许可数
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
