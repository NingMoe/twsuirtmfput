Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CodeMgrExpired
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
        Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库
        lblExpireNotes.Text = CmsMessage.GetNotes(pstTemp, "NOTES_PRODCODE_EXPIRE")

        'lblCorpName.Text = OemConfig.GetString("CORP_INFO", "COPR_BRIEFNAME")
        'lblServicePhone.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_PHONE")
        'lblServiceEmail.Text = OemConfig.GetString("CORP_INFO", "COPR_SERVICE_EMAIL")
    End Sub

    Private Sub btnAddLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLicense.Click
        Try
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库
            Dim intLicNum As Integer = CmsCodeLicense.AddLicenseCode(pstTemp, txtCode1.Text.Trim())
            If intLicNum > 0 Then
                Response.Redirect("/cmsweb/Default.htm", False)
            Else
                PromptMsg("请输入有效的用户许可码。")
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
