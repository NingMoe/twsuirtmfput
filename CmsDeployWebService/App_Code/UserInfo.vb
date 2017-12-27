Imports Microsoft.VisualBasic

Public Class UserInfo
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
    Private _DepartmentID As String
    Public Property DepartmentID() As String
        Get
            Return _DepartmentID
        End Get
        Set(ByVal value As String)
            _DepartmentID = value
        End Set
    End Property
    Private _DepartmentName As String
    Public Property DepartmentName() As String
        Get
            Return _DepartmentName
        End Get
        Set(ByVal value As String)
            _DepartmentName = value
        End Set
    End Property
    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Private _Password As String
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Private _Company As String
    Public Property Company() As String
        Get
            Return _Company
        End Get
        Set(ByVal value As String)
            _Company = value
        End Set
    End Property

    Private _CompanyID As String
    Public Property CompanyID() As String
        Get
            Return _CompanyID
        End Get
        Set(ByVal value As String)
            _CompanyID = value
        End Set

    End Property
    Private _Telephone As String = ""
    Public Property TelephoneNumber() As String
        Get
            Return _Telephone
        End Get
        Set(ByVal value As String)
            _Telephone = value
        End Set

    End Property
    Private _Mobile As String = ""
    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal value As String)
            _Mobile = value
        End Set
    End Property
    Private _Title As String = ""
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property

    Private _LDAPUserID As String = ""
    Public Property LDAPUserID() As String
        Get
            Return _LDAPUserID
        End Get
        Set(ByVal value As String)
            _LDAPUserID = value
        End Set
    End Property

    Private _HeadShip As String = ""
    Public Property HeadShip() As String
        Get
            Return _HeadShip
        End Get
        Set(ByVal value As String)
            _HeadShip = value
        End Set
    End Property

    Private _Status As Boolean = False
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property
End Class
