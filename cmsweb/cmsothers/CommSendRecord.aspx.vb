Option Strict On
Option Explicit On 

Imports System.Web.Mail
Imports System.IO

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class CommSendRecord
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox3 As System.Web.UI.WebControls.TextBox

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Dim strPhones As String = ""
            Dim res As DataResource = CmsPass.GetDataRes(RLng("mnuresid"))
            Dim dt As DataTable = BindData().Tables(0)
            For i As Int32 = 0 To dt.Rows.Count - 1
                Dim strTo As String = DbField.GetStr(dt.Rows(i), res.CommColHandphone) '短信号码
                strPhones += strTo & ","
            Next
            Me.txtPhones.Text = strPhones.Trim(CChar(","))
        End If
    End Sub
    Private Function BindData() As DataSet
        Dim strWhere As String = SStr("CMS_HOSTTABLE_WHERE")
        strWhere = CmsWhere.AppendAnd(strWhere, "")
        Dim strOrderBy As String = SStr("CMS_HOSTTABLE_ORDERBY")
        Dim strMoreTables As String = SStr("CMS_HOSTTABLE_MORETABLES")
        Dim ds As DataSet = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostTableData(CmsPass, CmsPass.HostResData.ResID, strWhere, strOrderBy, , , , , strMoreTables)
        Return ds
    End Function
    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_RECID") = 0 Then
            ViewState("PAGE_RECID") = RLng("mnurecid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '---------------------------------------------------------------
        '以下界面信息初始化代码必须放在IsPostBack之后
        If RStr("commfrom") = "addrbook" Then
            txtEmails.Text = SStr("CMSCOMM_EMAILS")
            txtPhones.Text = SStr("CMSCOMM_PHONES")
            txtContent.Text = SStr("CMSCOMM_CONTENT") '从地址薄回来
        Else
            Session("CMSCOMM_EMAILS") = ""
            Session("CMSCOMM_PHONES") = ""
            If VLng("PAGE_RECID") = 0 Then
                txtContent.Text = "" '没有选中记录而进入邮件短信发送页面
            Else
                txtContent.Text = LoadInitValue(VLng("PAGE_RESID"), VLng("PAGE_RECID"))
            End If
        End If

        '判断是否是文档管理
        If CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTableType = "DOC" Then
            chkAddAttach.Enabled = True
        Else
            chkAddAttach.Checked = False
            chkAddAttach.Enabled = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnAddrbookEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddrbookEmail.Click
        EnterAddrbook()
    End Sub

    Private Sub lbtnAddrbookSms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddrbookSms.Click
        EnterAddrbook()
    End Sub

    Private Sub btnSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Try
            '-----------------------------------------------------------
            '校验输入信息的正确性
            Dim strEmailSender As String = ""
            Dim strSmtpServer As String = ""
            Dim strSmtpUser As String = ""
            Dim strSmtpPass As String = ""
            If CmsPass.Employee.Email = "" Or CmsPass.Employee.EmailSmtp = "" Or CmsPass.Employee.EmailAccount = "" Then
                '使用系统统一SMTP信息
                strEmailSender = CmsConfig.GetSmtpDispUser(CmsPass)
                strSmtpServer = CmsConfig.GetSmtpServer(CmsPass)
                strSmtpUser = CmsConfig.GetSmtpUser(CmsPass)
                strSmtpPass = CmsConfig.GetSmtpPass(CmsPass)
            Else
                '使用用户SMTP信息
                strEmailSender = CmsPass.Employee.Email
                strSmtpServer = CmsPass.Employee.EmailSmtp
                strSmtpUser = CmsPass.Employee.EmailAccount
                strSmtpPass = CmsPass.Employee.EmailPassword
            End If
            If strEmailSender.StartsWith("<") = True Or strEmailSender.IndexOf("<") < 0 Or strEmailSender.IndexOf(">") < 0 Or strEmailSender.IndexOf("@") < 0 Or strEmailSender.IndexOf(".") < 0 Or strSmtpServer = "" Or strSmtpServer.IndexOf(".") < 0 Or strSmtpUser = "" Then
                PromptMsg("发送邮件前请先配置您的个人邮箱设置（点击系统菜单""工具""中的""修改用户信息""进入），或者配置系统统一SMTP设置（由系统管理员配置）。")
                Return
            End If
            If txtContent.Text.Trim() = "" And txtEmailTitle.Text.Trim() = "" Then
                PromptMsg("标题和内容不能全部为空！")
                Return
            End If
            If txtEmails.Text.Trim().Length < 5 Then '邮件不可能小于5位
                PromptMsg("请输入正确的电子邮件！")
                Return
            End If
            '-----------------------------------------------------------

            '-----------------------------------------------------------
            '生成附件
            Dim strAttachFile As String = ""
            If chkAddAttach.Checked Then strAttachFile = GenerateEmailFile()
            '-----------------------------------------------------------

            SimpleEmail.SendEmail(strEmailSender, txtEmails.Text.Trim(), "", txtEmailTitle.Text.Trim(), txtContent.Text, strSmtpServer, strSmtpUser, strSmtpPass, strAttachFile)

            '删除临时文件
            If strAttachFile <> "" Then File.Delete(strAttachFile)

            PromptMsg(CmsMessage.GetMsg(CmsPass, "TIP_COMM_EMAILSUCC"))
        Catch ex As Exception
            PromptMsg(CmsMessage.GetMsg(CmsPass, "ERR_COMM_EMAILFAIL"), ex, True)
        End Try
    End Sub

    Private Sub btnSendSms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSms.Click
        Try

            '-----------------------------------------------------------
            '校验输入信息的正确性
            If txtContent.Text.Trim() = "" Then
                PromptMsg("请输入待发送的短信内容！")
                Return
            End If

            If txtPhones.Text.Trim().Length <= 6 Then '不可能有手机号码小于6位
                PromptMsg("请输入正确的手机号码！")
                Return
            End If
            '-----------------------------------------------------------

            Dim intCommPort As Integer = CmsConfig.GetInt("SYS_CONFIG", "SMS_COMMPORT")
            If intCommPort < 1 Then intCommPort = 1

            Dim strFailedPhones As String = SimpleSms.GroupSend(txtContent.Text, txtPhones.Text, intCommPort)

            If strFailedPhones = "" Then '发送成功
                PromptMsg(CmsMessage.GetMsg(CmsPass, "TIP_COMM_SMSSUCC"))
            Else '有发送失败的手机
                strFailedPhones = strFailedPhones.Substring(0, strFailedPhones.Length - 2)
                PromptMsg(CmsMessage.GetMsg(CmsPass, "ERR_COMM_SMSFAILED_PHONES") & strFailedPhones, Nothing, True)
            End If
        Catch ex As Exception
            PromptMsg(CmsMessage.GetMsg(CmsPass, "ERR_COMM_SMSFAIL"), ex, True)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtContent.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------------------
    '获取指定资源和记录的所有字段的值
    '------------------------------------------------------------------
    Private Function LoadInitValue(ByVal lngResID As Long, ByVal lngRecID As Long) As String
        Dim strValue As String = ""

        Dim alistColumn As New ArrayList
        Dim hashFieldVal As New Hashtable
        ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetRecordData(CmsPass, lngResID, lngRecID, Nothing, Nothing, alistColumn, hashFieldVal)
        Dim strColDispName As String
        For Each strColDispName In alistColumn
            Try
                strValue &= strColDispName & "：" & CStr(hashFieldVal(strColDispName)) & Environment.NewLine
            Catch ex As Exception
                '图片型等字段的值在CStr转换时会抛出Exception，忽略这些Exception
            End Try
        Next

        Return strValue
    End Function


    '------------------------------------------------------------------
    '将待发送的邮件附件文档保存为临时文档
    '------------------------------------------------------------------
    Private Function GenerateEmailFile() As String
        Try
            If VLng("PAGE_RECID") = 0 Then Return ""
            Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
            If datRes Is Nothing Then Return ""
            If datRes.ResTableType <> "DOC" Or datRes.ResID = 0 Then Return ""

            '获取记录内的文档
            Dim datDoc As DataDocument = ResFactory.TableService(datRes.ResTableType).GetDocument(CmsPass, datRes.ResID, VLng("PAGE_RECID"), True)
            If datDoc.bytDOC2_FILE_BIN Is Nothing Then Return ""
            If datDoc.bytDOC2_FILE_BIN.Length = 0 Then Return ""

            '创建临时文件路径
            Dim strFileFolder As String = CmsConfig.TempFolder & "email\" & VLng("PAGE_RECID") & "\"
            If Directory.Exists(strFileFolder) = False Then Directory.CreateDirectory(strFileFolder)

            '保存临时文件
            Dim strFilePath As String = strFileFolder & datDoc.strDOC2_NAME & "." & datDoc.strDOC2_EXT
            Dim fs As FileStream = New FileStream(strFilePath, FileMode.Create, FileAccess.Write)
            fs.Write(datDoc.bytDOC2_FILE_BIN, 0, datDoc.bytDOC2_FILE_BIN.Length)
            fs.Flush()
            fs.Close()

            Return strFilePath
        Catch ex As Exception
            SLog.Err(CmsMessage.GetMsg(CmsPass, "ERR_COMM_SAVEFILE"), ex)
            Return ""
        End Try
    End Function

    '-----------------------------------------------------------------------
    '进入地址薄页面
    '-----------------------------------------------------------------------
    Private Sub EnterAddrbook()
        Session("CMSCOMM_CONTENT") = txtContent.Text '先备份，以便从地址薄回来后恢复

        Session("CMSCOMM_EMAILS") = txtEmails.Text.Trim()
        Session("CMSCOMM_PHONES") = txtPhones.Text.Trim()
        Session("CMSBP_CommAddrbook") = "/cmsweb/cmsothers/CommSendRecord.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnurecid=" & VLng("PAGE_RECID")
        Response.Redirect("/cmsweb/cmsothers/CommAddrbook.aspx", False)
    End Sub
End Class

End Namespace
