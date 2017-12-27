Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Diagnostics
Imports System.Threading
Imports NetReusables
Imports Unionsoft.Platform
'Imports SMLibrary
Imports System.Text.RegularExpressions


Namespace Unionsoft.Cms.Web


Partial Class ResMailSend
    Inherits CmsPage

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

    Dim CommColEmail As String = ""
    Dim dtPerson As DataTable = New DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        Dim res As DataResource = CmsPass.GetDataRes(RLng("mnuresid"))
        CommColEmail = res.CommColEmail
        dtPerson = BindData().Tables(0)
    End Sub

    Private Function BindData() As DataSet
        Dim strWhere As String = SStr("CMS_HOSTTABLE_WHERE")
        strWhere = CmsWhere.AppendAnd(strWhere, "")
        If Not Request("selectedrecid") Is Nothing Then
            If Request("selectedrecid").ToString() <> "" Then

                strWhere = "  Id in(" & Request("selectedrecid").ToString().Trim(CChar(",")) & ")"

            End If
        End If
        Dim strOrderBy As String = SStr("CMS_HOSTTABLE_ORDERBY")
        Dim strMoreTables As String = SStr("CMS_HOSTTABLE_MORETABLES")
        Dim ds As DataSet = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostTableData(CmsPass, CmsPass.HostResData.ResID, strWhere, strOrderBy, , , , , strMoreTables)
        Return ds
    End Function

    Protected Overrides Sub CmsPageDealFirstRequest()
        Panel1.Visible = False
        Panel2.Visible = False
        '加载收件人地址字段列表
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpRecipient, True, True, True, , True, True)

        '加载字收件人姓名字段列表
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpRecipientName, True, True, True, , True, True)

        '加载关键词替换
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey1, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey2, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey3, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey4, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey5, True, True, True, , True, True)

        LoadConfig() '载入配置信息

        'SMLibrary.LoadConfig.SetDb = CmsPass.Dbc '设置数据源
    End Sub

  
    Private Sub SendMail()
            Dim MailBody As String = ""
            Dim strSubject As String = txtSubject.Text                   '邮件标题
            If (chkIsKey.Checked = False) Then
                MailBody = txtBody.Text                 '邮件内容
            End If
            Dim strSmtpServer As String = CmsConfig.GetString("MailServer", "SmtpServer")
            Dim strUserName As String = CmsConfig.GetString("MailServer", "User")
            Dim strUserPass As String = CmsConfig.GetString("MailServer", "Password")
            Dim strEmailFrom As String = CmsConfig.GetString("MailServer", "SendFrom")

            '邮件写入队列
        Try
            For i As Int32 = 0 To dtPerson.Rows.Count - 1
                If (chkIsKey.Checked) Then '邮件主题
                    MailBody = GetMailBody(dtPerson.Rows(i))
                End If
                Dim strTo As String = DbField.GetStr(dtPerson.Rows(i), CommColEmail) '收件人地址
                Try
                    NetReusables.SimpleEmail.SendEmail(strEmailFrom, strTo, "", strSubject, MailBody, strSmtpServer, strUserName, strUserPass)
                Catch ex As Exception
                    SLog.Err("邮件发送失败！失败邮箱：" & strTo)
                End Try
            Next
        Catch ex As Exception
            Throw ex.InnerException
        End Try
    End Sub
    '开始发送邮件
    Private Sub bntSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSend.Click
        Dim workThread As Thread = New Thread(AddressOf SendMail)
        workThread.Start()
        Response.Write("邮件发送成功!")
    End Sub

    '获取邮件内容
    Public Function GetMailBody(ByVal drv As DataRow) As String
        Try
            Dim tmpBody As String = txtBody.Text
            If (drpKey1.SelectedIndex <> 0 Or txtKey1.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey1.Text.Trim, DbField.GetStr(drv, drpKey1.SelectedValue))
            End If

            If (drpKey2.SelectedIndex <> 0 Or txtKey2.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey2.Text.Trim, DbField.GetStr(drv, drpKey2.SelectedValue))
            End If

            If (drpKey3.SelectedIndex <> 0 Or txtKey3.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey3.Text.Trim, DbField.GetStr(drv, drpKey3.SelectedValue))
            End If

            If (drpKey4.SelectedIndex <> 0 Or txtKey4.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey4.Text.Trim, DbField.GetStr(drv, drpKey4.SelectedValue))
            End If

            If (drpKey5.SelectedIndex <> 0 Or txtKey5.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey5.Text.Trim, DbField.GetStr(drv, drpKey5.SelectedValue))
            End If
            Return tmpBody
        Catch ex As Exception
            Return txtBody.Text
        End Try
    End Function

#Region "共用方法"
    '邮件服务器配置验证
    Public Function CheckMailInfo() As Boolean
        If Not IsEmail(txtFrom.Text.Trim) Then
            Response.Write("发件人地址不正确!")
            Return False
        End If
        If Not IsEmail(txtReplyTo.Text.Trim) Then
            Response.Write("回复地址不下在确!")
            Return False
        End If
        If txtSmtpServer.Text.Trim.Length < 5 Then
            Response.Write("邮件服务器不正确!")
            Return False
        End If
        If txtUser.Text.Trim = "" Or txtPass.Text.Trim = "" Or txtFromName.Text.Trim = "" Then
            Response.Write("请检查账号,密码,发件人姓名是否正确!")
            Return False
        End If
        Return True
    End Function

    '是否电子邮件
    Public Function IsEmail(ByVal Value As String) As Boolean
        Dim RegEmail As New Regex("\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        Dim m As Match = RegEmail.Match(Value)
        Return m.Success
    End Function


    '载入邮件信息
    Public Sub LoadConfig()
        'txtSmtpServer.Text = SMLibrary.LoadConfig.CONF_MAIL_SMTP
        'txtUser.Text = SMLibrary.LoadConfig.CONF_MAIL_USER
        'txtPass.Text = SMLibrary.LoadConfig.CONF_MAIL_PASS
        'txtReplyTo.Text = SMLibrary.LoadConfig.CONF_MAIL_REPLYTO
        'txtFrom.Text = SMLibrary.LoadConfig.CONF_MAIL_FROM
        'txtFromName.Text = SMLibrary.LoadConfig.CONF_MAIL_FROMNAME
        'txtLogTable.Text = SMLibrary.LoadConfig.CONF_LOGTABLE
        'txtInterval.Text = SMLibrary.LoadConfig.CONF_THREAD_GROPUINTERVAL.ToString
        'txtNumber.Text = SMLibrary.LoadConfig.CONF_THREAD_GROPUNUMBER.ToString
    End Sub


    '设置邮件信息
    Public Sub SetConfig()
        'SMLibrary.LoadConfig.CONF_MAIL_SMTP = txtSmtpServer.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_USER = txtUser.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_PASS = txtPass.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_REPLYTO = txtReplyTo.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_FROM = txtFrom.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_FROMNAME = txtFromName.Text.Trim
        'SMLibrary.LoadConfig.CONF_LOGTABLE = txtLogTable.Text.Trim
        'SMLibrary.LoadConfig.CONF_THREAD_GROPUINTERVAL = CInt(txtInterval.Text.Trim)
        'SMLibrary.LoadConfig.CONF_THREAD_GROPUNUMBER = CInt(txtNumber.Text.Trim)
        'SMLibrary.LoadConfig.SaveXmlConfig() 'jmail
    End Sub

#End Region

    '保存配置信息
    Private Sub bntSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSave.Click
        If (CheckMailInfo()) And IsNumeric(txtNumber.Text.Trim) And IsNumeric(txtInterval.Text.Trim) Then
            SetConfig()
        End If
    End Sub

    '是否显示邮件服务器配制
    Private Sub chkShowConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowConfig.CheckedChanged
        If (chkShowConfig.Checked) Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    '是否启用关键词替换
    Private Sub chkIsKey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsKey.CheckedChanged
        If (chkIsKey.Checked) Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub
End Class

End Namespace
