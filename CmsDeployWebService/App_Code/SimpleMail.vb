Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Collections
Imports System.Net.Mail
Imports NetReusables
Imports System.Xml
Imports CDO



Public NotInheritable Class SimpleMail
    '发送邮件事件
    Public Shared Function Send(ByVal DisplayName As String, ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String) As Boolean
        Dim mobjXmlDoc As XmlDocument
        Dim mxmlAcc As New XMLAccess '初始化一个发邮件发件人地址
        Dim path As String = System.Web.HttpContext.Current.Server.MapPath(".") + "\conf\app_config.xml"
        mobjXmlDoc = mxmlAcc.LoadSource(path)
        Dim ServerType As String = mxmlAcc.GetNodeByPath("MailServer/ServerType").InnerText
        Dim mailinfo As New MailInfo
        mailInfo.FromDisplayName = DisplayName
        mailInfo.FromMail = strFrom
        mailInfo.CredentialUser = mxmlAcc.GetNodeByPath("MailServer/User").InnerText
        mailInfo.CredentialPassword = mxmlAcc.GetNodeByPath("MailServer/Password").InnerText
        mailInfo.ServerName = mxmlAcc.GetNodeByPath("MailServer/ServerName").InnerText
        mailInfo.ToEmail = strTo
        mailInfo.MailSubject = strSubject
        mailInfo.MailBody = strBody

        If ServerType.ToLower = "exchange" Then
            ' SendMailForExchange(mailinfo)

        ElseIf ServerType.ToLower = "smtp" Then

            SendMailForSmtp(mailinfo)

        End If

    
    End Function



    'Public Shared Function SendMailForExchange(ByVal _MainInfo As MailInfo) As Boolean



    '    Dim Configuration As CDO.Configuration = New CDO.ConfigurationClass()

    '    Configuration.Fields(CdoConfiguration.cdoSendUsingMethod).Value = CdoSendUsing.cdoSendUsingPort
    '    Configuration.Fields(CdoConfiguration.cdoSMTPServer).Value = _MainInfo.ServerName
    '    ' Configuration.Fields(CdoConfiguration.cdoSMTPServerPort).Value = 25
    '    Configuration.Fields(CdoConfiguration.cdoSMTPAccountName).Value = _MainInfo.CredentialUser
    '    Configuration.Fields(CdoConfiguration.cdoSendUserReplyEmailAddress).Value = _MainInfo.FromDisplayName + " <" + _MainInfo.FromMail + ">"
    '    Configuration.Fields(CdoConfiguration.cdoSendEmailAddress).Value = _MainInfo.FromMail
    '    Configuration.Fields(CdoConfiguration.cdoSMTPAuthenticate).Value = CdoProtocolsAuthentication.cdoBasic
    '    Configuration.Fields(CdoConfiguration.cdoSendUserName).Value = _MainInfo.CredentialUser
    '    Configuration.Fields(CdoConfiguration.cdoSendPassword).Value = _MainInfo.CredentialPassword
    '    Configuration.Fields(CdoConfiguration.cdoLanguageCode).Value = CDO.CdoCharset.cdoGB2312

    '    Configuration.Fields.Update()

    '    Dim Mail As CDO.MessageClass = New CDO.MessageClass()
    '    Mail.Configuration = Configuration
    '    Mail.To = _MainInfo.ToEmail
    '    Mail.Subject = _MainInfo.MailSubject
    '    Mail.From = _MainInfo.FromMail
    '    Mail.TextBody = _MainInfo.MailBody
    '    Try
    '        Mail.Send()
    '        Return True
    '        Mail = Nothing

    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function


    Public Shared Function SendMailForSmtp(ByVal _MainInfo As MailInfo) As Boolean
        Dim from As New MailAddress(_MainInfo.FromMail, _MainInfo.FromDisplayName)
        '发邮件用到协议
        Dim client As New SmtpClient(_MainInfo.ServerName)
        client.UseDefaultCredentials = False

        '发邮件身份验证
        client.Credentials = New System.Net.NetworkCredential(_MainInfo.CredentialUser, _MainInfo.CredentialPassword)
        ' 发邮件的处理方式为使用网络方式
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        '创建一个发邮件的对象
        Dim message As New MailMessage()

        message.From = from

        '收信人地址
        For Each strTo As String In _MainInfo.ToEmail.Split(";")
            message.[To].Add(strTo)
        Next


        '邮件主题
        message.SubjectEncoding = System.Text.Encoding.[Default]
        message.Subject = _MainInfo.MailSubject
        '编码
        message.BodyEncoding = System.Text.Encoding.UTF8
        '邮件正文
        message.Body = _MainInfo.MailBody
        '是否是HTML代码
        message.IsBodyHtml = True
        Try
            '发送
            client.Send(message)
            message.Dispose()
            Return True
        Catch
            message.Dispose()
            Return False
        End Try
    End Function



End Class

