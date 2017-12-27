Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web



Partial Class Default1
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
        If Me.IsPostBack Then Return
            'If CmsConfig.GetString("DomainServer", "Authentication").ToLower = "true" Then
            'Response.Redirect("Login.aspx", False)
            ' End If
            If CmsDatabase.IsDbInitialized(CmsDatabase.GetDbConfig()) Then
                Me.lbtnCreateDb.Visible = False
            Else

                Me.lbtnCreateDb.Visible = True
            End If
    End Sub

    'Private Sub lbtnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRegister.Click
    '    Response.Redirect("admincode/CodeMgrLicense.aspx", False)
    'End Sub

    Private Sub lbtnCreateDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCreateDb.Click
        Response.Redirect("cmssetup/SetupDatabase.aspx", False)
    End Sub

    '------------------------------------------------------------------
    '开始登录
    '------------------------------------------------------------------
    Private Sub Login(ByVal strUserName As String, ByVal strUserPass As String)
        Dim blnLoginSuccess As Boolean = False

        Try
            Session("CMS_PASSPORT") = Nothing
            'If Not OnlineUser.IsAvailableUser(strUserName, Request.UserHostAddress) Then
            '    PromptMsg("该用户已在他处登陆，如需登陆请联系管理员！")
            '    Return
            'End If
            Dim pst As CmsPassport = CmsPassport.GenerateCmsPassport(strUserName, strUserPass, Request.UserHostAddress)
            If Not (pst Is Nothing) Then
                '登录成功后清除其失败次数
                'Session("CMS_LOGINFAIL_RECORD") = Nothing

                Session("CMS_PASSPORT") = pst
                '将当前用户插入在线用户列表
                'OnlineUser.AddOnlineUser(strUserName, Request.UserHostAddress)
                'CmsLog.Save(pst, LogTitle.Login, "")

                blnLoginSuccess = True
                If pst.Employee.ID.ToLower() = "sysuser" Then '是系统内部用户
                        Response.Redirect("cmsdev/DevMain.aspx", False)
                    Return
                ElseIf pst.Employee.ID.ToLower() = "admin" Then '是系统管理员
                    pst.EmpIsSysAdmin = True
                        Session("CMS_PASSPORT") = pst
                        '  Response.Redirect("ShowMe.aspx", False)
                        Response.Redirect("cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminres/ResourceFrameBody.aspx&timeid=" & TimeId.CurrentMilliseconds(1), False)
                    Return
                ElseIf pst.EmpIsSysSecurity Then  '是系统安全员
                        Response.Redirect("cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminsys/SysLogManager.aspx", False)
                    Return
                Else '是普通人员
                        Dim strFlowEntryUrl As String = CmsConfig.GetString("SYS_CONFIG", "LOGIONPAGE")
                        If pst.Employee.Status Then
                            PromptMsg("您的帐号已被禁用，请与管理员联系/Your account has been disabled, please contact administrator！")
                            Return
                        Else
                            If (strFlowEntryUrl.Trim() = "") Then
                                Response.Redirect("cmshost/CmsFrame.aspx", False)
                            Else
                                Response.Redirect(strFlowEntryUrl & "?user= " & pst.Employee.ID.Trim() & " &ucode=" & pst.Employee.Password.Trim(), False)
                            End If
                            Return
                        End If
                    End If
                End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        '记录“登录失败次数”和“登录失败时间”
        'If blnLoginSuccess = False Then
        '    If Session("CMS_LOGINFAIL_RECORD") Is Nothing Then
        '        Dim logf As New LoginFail
        '        logf.intTimes = 1
        '        logf.lngTime = TimeId.CurrentMilliseconds(1)
        '        Session("CMS_LOGINFAIL_RECORD") = logf
        '    Else
        '        Dim logf As LoginFail = CType(Session("CMS_LOGINFAIL_RECORD"), LoginFail)
        '        logf.intTimes += 1
        '        logf.lngTime = TimeId.CurrentMilliseconds(1)
        '        Session("CMS_LOGINFAIL_RECORD") = logf
        '    End If
        'End If

    End Sub

    Protected Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Login(txtUserName.Text, txtPassword.Text)
    End Sub


End Class

End Namespace
