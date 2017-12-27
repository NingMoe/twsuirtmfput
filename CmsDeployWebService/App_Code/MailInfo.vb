Imports Microsoft.VisualBasic

Public Class MailInfo

    Private _ServerName As String
    Public Property ServerName() As String
        Get
            Return _ServerName
        End Get
        Set(ByVal value As String)
            _ServerName = value
        End Set
    End Property


    Private _FromMail As String
    Public Property FromMail() As String
        Get
            Return _FromMail
        End Get
        Set(ByVal value As String)
            _FromMail = value
        End Set
    End Property

    Private _FromDisplayName As String
    Public Property FromDisplayName() As String
        Get
            Return _FromDisplayName
        End Get
        Set(ByVal value As String)
            _FromDisplayName = value
        End Set
    End Property

    Private _ToEmail As String
    Public Property ToEmail() As String
        Get
            Return _ToEmail
        End Get
        Set(ByVal value As String)
            _ToEmail = value
        End Set
    End Property

    Private _CredentialUser As String
    Public Property CredentialUser() As String
        Get
            Return _CredentialUser
        End Get
        Set(ByVal value As String)
            _CredentialUser = value
        End Set
    End Property

    Private _CredentialPassword As String
    Public Property CredentialPassword() As String
        Get
            Return _CredentialPassword
        End Get
        Set(ByVal value As String)
            _CredentialPassword = value
        End Set
    End Property


    Private _MailSubject As String
    Public Property MailSubject() As String
        Get
            Return _MailSubject
        End Get
        Set(ByVal value As String)
            _MailSubject = value
        End Set
    End Property

    Private _MailBody As String
    Public Property MailBody() As String
        Get
            Return _MailBody
        End Get
        Set(ByVal value As String)
            _MailBody = value
        End Set
    End Property


End Class
