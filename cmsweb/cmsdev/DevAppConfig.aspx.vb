Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DevAppConfig
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
            PageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改
            PageInitialize() '初始化页面

            If Not IsPostBack Then
                PageDealFirstRequest() '处理第一个GET请求中的事务
            Else
                PageDealPostBack() '处理POST中的命令。返回：True：退出本接口后直接退出窗体；False：退出本接口后继续之后的处理
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------
    '将传入的参数保留为ViewState变量，便于在页面中提取和修改。
    '--------------------------------------------------------------------------
    Private Sub PageSaveParametersToViewState()
        If VStr("PAGE_ISFROM") = "" Then
            ViewState("PAGE_ISFROM") = RStr("isfrom")
        End If

        If VStr("PAGE_CONFIGFILE") = "" Then
            ViewState("PAGE_CONFIGFILE") = CmsConfig.ProjectRootFolder & "conf\" & RStr("conffile")
        End If
    End Sub

    '--------------------------------------------------------------------------
    '初始化页面
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        If VStr("PAGE_ISFROM") = "admin" Then
            If Session("CMS_PASSPORT") Is Nothing Then
                Response.Redirect("/cmsweb/cmsdev/DevLogin.aspx", False)
                Return
            End If
        ElseIf VStr("PAGE_ISFROM") = "sysuser" Then  '校验是否正确登录
            If SStr("DEV_MANAGER") <> "1" Then
                Response.Redirect("/cmsweb/cmsdev/DevLogin.aspx", False)
                Return
            End If
        End If
    End Sub

    '--------------------------------------------------------------------------
    '处理第一个GET请求中的事务
    '--------------------------------------------------------------------------
    Private Sub PageDealFirstRequest()
        CmsConfig.ReloadAll()

        Dim alistTexts As New ArrayList
        Dim alistChecks As New ArrayList

        Dim i As Integer = 0

        ''----------------------------------------------------------------------------------------------
        ''显示Section说明
        'Dim strTitle As String
        'Dim strConfigFile As String = VStr("PAGE_CONFIGFILE").ToLower()
        'If strConfigFile.IndexOf("app_function.xml") >= 0 Then
        '    strTitle = "重要说明：下述开关配置中红色字体表示在一般产品中应该关闭的功能！蓝色字体表示需要单独购买的功能模块！"
        'Else
        '    strTitle = "重要说明：下述配置信息中红色字体表示在系统发布时可能需要改动的配置信息！"
        'End If
        'Dim lblTitle As New System.Web.UI.WebControls.Label
        'lblTitle.ID = "lblTitle"
        'lblTitle.Font.Bold = True
        'lblTitle.ForeColor = Color.FromName("red")
        'lblTitle.Text = strTitle
        'lblTitle.EnableViewState = True
        'Dim strStyle As String = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 10 & "px"
        'lblTitle.Attributes.Add("style", strStyle)
        'Panel1.Controls.Add(lblTitle)
        'i += 1
        ''----------------------------------------------------------------------------------------------

        Dim datSvc As New DataServiceSection(VStr("PAGE_CONFIGFILE"))
        Dim alistSections As ArrayList = datSvc.GetSections()
        Dim strStyle As String = ""
        Dim strSec As String
        For Each strSec In alistSections
            Dim strEnable As String = datSvc.GetSecAttr(strSec, "SHOWENABLE")
            If strEnable <> "0" AndAlso VStr("PAGE_ISFROM") = "admin" Then
                strEnable = datSvc.GetSecAttr(strSec, "SHOWFORADMIN")
            End If
            If strEnable <> "0" Then '只显示已支持的功能
                Dim alistFuncs As ArrayList = datSvc.GetKeys(strSec)

                '显示Section说明
                Dim lblSection As New System.Web.UI.WebControls.Label
                lblSection.ID = "lblSection" & strSec
                lblSection.Font.Bold = True
                lblSection.ForeColor = Color.FromName("blue")
                lblSection.Text = datSvc.GetSecAttr(strSec, "DESC")
                lblSection.EnableViewState = True
                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 10 & "px"
                lblSection.Attributes.Add("style", strStyle)
                Panel1.Controls.Add(lblSection)
                i += 1

                Dim strKey As String
                For Each strKey In alistFuncs
                    If strKey = "FUNC_SEP" Then
                        i += 1
                    Else
                        Dim strDesc As String = datSvc.GetKeyAttr(strSec, strKey, "DESC")
                        If strDesc = "" Then strDesc = "(未知)"
                        Dim strEnable2 As String = datSvc.GetKeyAttr(strSec, strKey, "SHOWENABLE")
                        If strEnable2 <> "0" AndAlso VStr("PAGE_ISFROM") = "admin" Then
                            strEnable2 = datSvc.GetKeyAttr(strSec, strKey, "SHOWFORADMIN")
                        End If
                        If strEnable2 <> "0" Then '只显示已支持的功能
                            If datSvc.GetKeyAttr(strSec, strKey, "ISSWITCH") = "1" Then
                                '生成CheckBox
                                Dim intEnable As Integer = datSvc.GetInt(strSec, strKey)

                                Dim chk As New System.Web.UI.WebControls.CheckBox
                                chk.ID = strSec & "___" & strKey
                                chk.Text = strDesc
                                chk.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 30 & "px"
                                chk.Attributes.Add("style", strStyle)
                                If intEnable = 1 Then '默认已打开
                                    chk.Checked = True
                                Else '默认关闭
                                    chk.Checked = False
                                End If
                                Dim strColor As String = datSvc.GetKeyAttr(strSec, strKey, "DESCCOLOR")
                                If strColor <> "" Then chk.ForeColor = Color.FromName(strColor)
                                Panel1.Controls.Add(chk)
                                alistChecks.Add(chk.ID)
                            Else
                                '生成TextBox和Label
                                Dim strText As String = datSvc.GetString(strSec, strKey)

                                Dim txt As New System.Web.UI.WebControls.TextBox
                                txt.ID = strSec & "___" & strKey
                                txt.Text = strText
                                txt.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 30 & "px; width: 350px"
                                txt.Attributes.Add("style", strStyle)
                                Panel1.Controls.Add(txt)
                                alistTexts.Add(txt.ID)

                                Dim lbl As New System.Web.UI.WebControls.Label
                                lbl.ID = "lbl" & strSec & "___" & strKey
                                lbl.Text = strDesc
                                lbl.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 386 & "px"
                                lbl.Attributes.Add("style", strStyle)
                                Dim strColor As String = datSvc.GetKeyAttr(strSec, strKey, "DESCCOLOR")
                                If strColor <> "" Then lbl.ForeColor = Color.FromName(strColor)
                                Panel1.Controls.Add(lbl)
                            End If

                            i += 1
                        End If
                    End If
                Next

                i += 1
            End If
        Next

        Panel1.Width = Unit.Pixel(900)
        Panel1.Height = Unit.Pixel(i * 24 + 10)

        ViewState("PAGE_TEXTS") = alistTexts
        ViewState("PAGE_CHECKS") = alistChecks
    End Sub

    '--------------------------------------------------------------------------
    '处理POST中的命令。返回：True：退出本接口后直接退出窗体；False：退出本接口后继续之后的处理
    '--------------------------------------------------------------------------
    Private Function PageDealPostBack() As Boolean
    End Function

    Private Sub lbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSave.Click
        Try
            Dim alistTexts As ArrayList = CType(ViewState("PAGE_TEXTS"), ArrayList)
            Dim alistChecks As ArrayList = CType(ViewState("PAGE_CHECKS"), ArrayList)

            Dim datSvc As New DataServiceSection(VStr("PAGE_CONFIGFILE"))

            '保存功能设置信息
            Dim strCtrl As String
            For Each strCtrl In alistChecks
                Dim strVal As String = RStr(strCtrl)
                Dim pos As Integer = strCtrl.IndexOf("___")
                Dim strSec As String = strCtrl.Substring(0, pos)
                Dim strKey As String = strCtrl.Substring(pos + 3)
                If strVal.ToLower.Trim() = "on" Then
                    datSvc.SetInt(strSec, strKey, 1)
                Else
                    datSvc.SetInt(strSec, strKey, 0)
                End If
            Next
            For Each strCtrl In alistTexts
                Dim strVal As String = RStr(strCtrl)
                Dim pos As Integer = strCtrl.IndexOf("___")
                Dim strSec As String = strCtrl.Substring(0, pos)
                Dim strKey As String = strCtrl.Substring(pos + 3)
                datSvc.SetString(strSec, strKey, strVal)
            Next

            '重新Load所有功能设置信息
            PageDealFirstRequest()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnExit.Click
        Response.Redirect(RStr("backpage"), False)
    End Sub
End Class

End Namespace
