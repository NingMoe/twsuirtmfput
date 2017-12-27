Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SetupProductCode
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
        Try
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库
            lblProdCodeNotes.Text = CmsMessage.GetNotes(pstTemp, "NOTES_PRODCODE_INTRODUCE")

            If IsPostBack Then Return

            '检验产品码是否已经存在
            Dim strProdCode As String = CmsCodeProduct.GetProductCode(pstTemp)
            If strProdCode <> "" Then '产品码验证成功
                txtCode1.Text = strProdCode.Substring(0, 4) & "-" & strProdCode.Substring(4, 4) & "-" & strProdCode.Substring(8, 4) & "-" & strProdCode.Substring(12, 4) & "-" & strProdCode.Substring(16, 4)
                btnConfirm.Enabled = False
                txtCode1.ReadOnly = True
            Else
                btnConfirm.Enabled = True
                txtCode1.ReadOnly = False
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strCode As String = txtCode1.Text.Trim()
            strCode = strCode.Replace("-", "")
            strCode = strCode.Replace(" ", "")
            If strCode.Length <> 20 Then
                PromptMsg("请输入正确的产品码！")
                Return
            End If

            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库
            Dim strProductSymbol As String = CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODID'")
            Dim strVer As String = CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODVER'")
            If CmsCodeProduct.ValidateProductCode(strCode, strVer, strProductSymbol) = False Then
                PromptMsg("请输入正确的产品码！")
                Return
            Else '产品码验证成功
                '保存验证码
                CmsCodeProduct.SaveProductCodeToDb(pstTemp, strCode)
                PromptMsg("本软件系统已被激活，欢迎使用！")
                txtCode1.ReadOnly = True
                btnConfirm.Enabled = False

                'Response.Redirect("/cmsweb/Default.htm", False)
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'Response.Redirect("/cmsweb/Default.htm", False)
        Response.Write("<script type='text/javascript'>top.location.href='/cmsweb/Default.htm' </script>")
    End Sub
End Class

End Namespace
