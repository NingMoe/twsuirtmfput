Imports System.Reflection
Imports Unionsoft.Workflow.Platform

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '  LoadLibrary(
        ' Dim Dll As Assembly = Assembly.LoadFrom("D:\UnionsoftWorkflow2012\Unionsoft.Workflow.Web\bin\MessageSend.dll")


        'Dim T As Type = Dll.GetType("SimpleMail")
        'Dim Obj As Object = Activator.CreateInstance(T)
        'Dim m As MethodInfo = T.GetMethod("Send")
        'Dim str As Object = m.Invoke(Obj, New Object() {"tq@unionsoft.cn", "tq_643@126.com", "test", "test mail", "mail.unionsoft.cn", "tq@unionsoft.cn", "123456"}) '注意参数按顺序用逗号隔开，这里计算2+3



        'Dim T As Type = Dll.GetType("MessageSend.SendSms")
        'Dim Obj As Object = Activator.CreateInstance(T)
        'Dim m As MethodInfo = T.GetMethod("Send")
        'Dim str As Object = m.Invoke(Obj, New Object() {"805860", "admin", "3LH14M", "13917143894", "Test SMS"}) '注意参数按顺序用逗号隔开，这里计算2+3

        'Dim Obj As Object = MailFactory.Implementation

        'If MailFactory.Enable = 1 Then

        '    ' Dim str As Object = Obj.GetMethod("Send").Invoke(Obj, New Object() {MailFactory.From, "tq_643@126.com", "test", "test mail", MailFactory.SmtpServer, MailFactory.AuthenticateUserName, MailFactory.AuthenticateUserPassword}) '注意参数按顺序用逗号隔开，这里计算2+3
        'End If


        '  MailFactory.SendMail("tq_643@126.com", "test", "test mail")
        MessageSendFactory.SendSms("13917143894", "SMS Test")


    End Sub
End Class
