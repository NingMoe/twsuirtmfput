Imports Microsoft.VisualBasic

Public Class RecordInfo
    Private _ResourceID As String
    Public Property ResourceID() As String
        Get
            Return _ResourceID
        End Get
        Set(ByVal value As String)
            _ResourceID = value
        End Set
    End Property

    Private _RecordID As String
    Public Property RecordID() As String
        Get
            Return _RecordID
        End Get
        Set(ByVal value As String)
            _RecordID = value
        End Set
    End Property



    Private _ResourceType As String
    Public Property ResourceType() As String
        Get
            Return _ResourceType
        End Get
        Set(ByVal value As String)
            _ResourceType = value
        End Set
    End Property



    Private _FieldInfoList As FieldInfo()
    Public Property FieldInfoList() As FieldInfo()
        Get
            Return _FieldInfoList
        End Get
        Set(ByVal value As FieldInfo())
            _FieldInfoList = value
        End Set
    End Property
End Class
