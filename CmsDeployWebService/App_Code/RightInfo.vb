Imports Microsoft.VisualBasic

Public Class RightInfo
    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Private _value As Boolean
    Public Property Value() As Boolean
        Get
            Return _value
        End Get
        Set(ByVal value As Boolean)
            _value = value
        End Set
    End Property
End Class
