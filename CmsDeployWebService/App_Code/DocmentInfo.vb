Imports Microsoft.VisualBasic

Public Class DocmentInfo
    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Private _ID As String
    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property
    Private _Size As String
    Public Property Size() As String
        Get
            Return _Size
        End Get
        Set(ByVal value As String)
            _Size = value
        End Set
    End Property
  
    Private _Ext As String
    Public Property Ext() As String
        Get
            Return _Ext
        End Get
        Set(ByVal value As String)
            _Ext = value
        End Set
    End Property
    Public Property Path() As String
        Get

            Return _Path
        End Get
        Set(ByVal value As String)
            _Path = value
        End Set
    End Property
    Private _Path As String
    Public Property DownLoadPath() As String
        Get

            Return _DownLoadPath
        End Get
        Set(ByVal value As String)
            _DownLoadPath = value
        End Set
    End Property
    Private _DownLoadPath As String
    Public Property DownLoadFullPath() As String
        Get

            Return _DownLoadFullPath
        End Get
        Set(ByVal value As String)
            _DownLoadFullPath = value
        End Set
    End Property
    Private _DownLoadFullPath As String
    Public Property StoreType() As String
        Get

            Return _StoreType
        End Get
        Set(ByVal value As String)
            _StoreType = value
        End Set
    End Property
    Private _StoreType As Boolean



    Private _DocResid As String
    Public Property DocResid() As String
        Get

            Return _DocResid
        End Get
        Set(ByVal value As String)
            _DocResid = value
        End Set
    End Property
   

End Class
