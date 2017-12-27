Option Strict On
Option Explicit On 

Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DevLogin
    Inherits AspPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnExit As System.Web.UI.WebControls.Button

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
            PageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改
            PageInitialize() '初始化页面

            If Not IsPostBack Then
                PageDealFirstRequest() '处理第一个GET请求中的事务
            Else
                PageDealPostBack() '处理POST中的命令
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------
    '将传入的参数保留为ViewState变量，便于在页面中提取和修改。
    '--------------------------------------------------------------------------
    Private Sub PageSaveParametersToViewState()
    End Sub

    '--------------------------------------------------------------------------
    '初始化页面
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        Session("DEV_MANAGER") = "0"
    End Sub

    '--------------------------------------------------------------------------
    '处理第一个GET请求中的事务
    '--------------------------------------------------------------------------
    Private Sub PageDealFirstRequest()
    End Sub

    '--------------------------------------------------------------------------
    '处理POST中的命令。返回：True：退出本接口后直接退出窗体；False：退出本接口后继续之后的处理
    '--------------------------------------------------------------------------
    Private Function PageDealPostBack() As Boolean
        Response.Write("<br>")
        Response.Write(CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE"))
        Response.Write("<br>")
            Response.Write(Encrypt.Encrypt(txtCode.Text.Trim()))
        Response.Write("<br>")

        'If CmsEncrypt.Encrypt(txtCode.Text.Trim()) = CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE") Then
        '    Session("DEV_MANAGER") = "1"
        '    Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
        'Else
        '    Session("DEV_MANAGER") = "0"
        '    PromptMsg("校验码错误！")
        'End If
        If OrgFactory.EmpDriver.EncryptPass(txtCode.Text.Trim()) = CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE") Then
            Session("DEV_MANAGER") = "1"
            Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
        Else
            Session("DEV_MANAGER") = "0"
            PromptMsg("校验码错误！")
        End If
    End Function
End Class

End Namespace
