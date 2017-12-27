Imports Microsoft.VisualBasic

Public Class ResourceInfo
    Private _ID As String
    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _TableName As String
    Public Property TableName() As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    Private _Type As String
    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    Private _TableType As String
    Public Property TableType() As String
        Get
            Return _TableType
        End Get
        Set(ByVal value As String)
            _TableType = value
        End Set
    End Property

    Private _ShowChild As String
    Public Property ShowChild() As Integer
        Get
            Return _ShowChild
        End Get
        Set(ByVal value As Integer)
            _ShowChild = value
        End Set
    End Property

    Private _ParentID As String
    Public Property ParentID() As String
        Get
            Return _ParentID
        End Get
        Set(ByVal value As String)
            _ParentID = value
        End Set
    End Property

    Private _ServerUrl As String
    Public Property ServerUrl() As String
        Get
            Return _ServerUrl
        End Get
        Set(ByVal value As String)
            _ServerUrl = value
        End Set
    End Property

    Private _ResourceLinkUrl As String
    Public Property ResourceLinkUrl() As String
        Get
            Return _ResourceLinkUrl
        End Get
        Set(ByVal value As String)
            _ResourceLinkUrl = value
        End Set
    End Property

    Private _ResourceLinkTarget As String
    Public Property ResourceLinkTarget() As String
        Get
            Return _ResourceLinkTarget
        End Get
        Set(ByVal value As String)
            _ResourceLinkTarget = value
        End Set
    End Property

    Private _IsUseParentShow As Boolean
    Public Property IsUseParentShow() As Boolean
        Get
            Return _IsUseParentShow
        End Get
        Set(ByVal value As Boolean)
            _IsUseParentShow = value
        End Set
    End Property


    Private _IsView As Boolean
    Public Property IsView() As Boolean
        Get
            Return _IsView
        End Get
        Set(ByVal value As Boolean)
            _IsView = value
        End Set
    End Property



    Private _Res_Order As String = ""
    Public Property ResOrder() As String
        Get
            Return _Res_Order
        End Get
        Set(ByVal value As String)
            _Res_Order = value
        End Set
    End Property

    ''' <summary>
    ''' 用户存储，菜单图标路径
    ''' </summary>
    ''' <remarks></remarks>

    Private _RES_ROWCOLOR3_WHERE As String
    Public Property RES_ROWCOLOR3_WHERE() As String
        Get
            Return _RES_ROWCOLOR3_WHERE
        End Get
        Set(ByVal value As String)
            _RES_ROWCOLOR3_WHERE = value
        End Set
    End Property

   

End Class
