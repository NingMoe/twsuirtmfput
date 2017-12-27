Public Class FieldInfo
    Private _ResourceID As String
    Public Property ResourceID() As String
        Get
            Return _ResourceID
        End Get
        Set(ByVal value As String)
            _ResourceID = value
        End Set
    End Property
    Private _FieldName As String
    Public Property FieldName() As String
        Get
            Return _FieldName
        End Get
        Set(ByVal value As String)
            _FieldName = value
        End Set
    End Property

    Private _FieldValue As Object
    Public Property FieldValue() As Object
        Get
            Return _FieldValue
        End Get
        Set(ByVal value As Object)
            _FieldValue = value
        End Set
    End Property


    Private _FieldDescription As String
    Public Property FieldDescription() As String
        Get
            Return _FieldDescription
        End Get
        Set(ByVal value As String)
            _FieldDescription = value
        End Set
    End Property

End Class
