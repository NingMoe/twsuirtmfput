Imports Microsoft.VisualBasic

Public Class IntegralInfo


    Private _UserID As String
    Public Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property

    Private _Action As String
    Public Property Action() As String
        Get
            Return _Action
        End Get
        Set(ByVal value As String)
            _Action = value
        End Set
    End Property

    Private _CreateTime As DateTime
    Public Property CreateTime() As DateTime
        Get
            Return _CreateTime
        End Get
        Set(ByVal value As DateTime)
            _CreateTime = value
        End Set
    End Property


   
End Class
