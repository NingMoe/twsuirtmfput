Imports Microsoft.VisualBasic

Public Class LCSettings
    ''' <summary>
    ''' ���̱��
    ''' </summary>
    ''' <remarks></remarks>
    Private _ModelNum As Integer
    Public Property ModelNum() As Integer
        Get
            Return _ModelNum
        End Get
        Set(ByVal value As Integer)
            _ModelNum = value
        End Set
    End Property

    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private _ParentFlow As String
    Public Property ParentFlow() As String
        Get
            Return _ParentFlow
        End Get
        Set(ByVal value As String)
            _ParentFlow = value
        End Set
    End Property

    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChildFlow As String
    Public Property ChildFlow() As String
        Get
            Return _ChildFlow
        End Get
        Set(ByVal value As String)
            _ChildFlow = value
        End Set
    End Property

    ''' <summary>
    ''' �Ƿ��ѡ����
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsRequired As Boolean
    Public Property IsRequired() As Boolean
        Get
            Return _IsRequired
        End Get
        Set(ByVal value As Boolean)
            _IsRequired = value
        End Set
    End Property

    ''' <summary>
    ''' �Ƿ���ط�
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsRetry As Boolean
    Public Property IsRetry() As Boolean
        Get
            Return _IsRetry
        End Get
        Set(ByVal value As Boolean)
            _IsRetry = value
        End Set
    End Property

    ''' <summary>
    ''' ǰ������
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prepose As String
    Public Property Prepose() As String
        Get
            Return _Prepose
        End Get
        Set(ByVal value As String)
            _Prepose = value
        End Set
    End Property

    ''' <summary>
    ''' ����������
    ''' </summary>
    ''' <remarks></remarks>
    Private _YZFlowName As String
    Public Property YZFlowName() As String
        Get
            Return _YZFlowName
        End Get
        Set(ByVal value As String)
            _YZFlowName = value
        End Set
    End Property

    ''' <summary>
    ''' ����������
    ''' </summary>
    ''' <remarks></remarks>
    Private _SMFlowName As String
    Public Property SMFlowName() As String
        Get
            Return _SMFlowName
        End Get
        Set(ByVal value As String)
            _SMFlowName = value
        End Set
    End Property

    ''' <summary>
    ''' ��Դ��
    ''' </summary>
    ''' <remarks></remarks>
    Private _ResourceTable As String
    Public Property ResourceTable() As String
        Get
            Return _ResourceTable
        End Get
        Set(ByVal value As String)
            _ResourceTable = value
        End Set
    End Property

    ''' <summary>
    ''' ��ԴID
    ''' </summary>
    ''' <remarks></remarks>
    Private _ResourceID As String
    Public Property ResourceID() As String
        Get
            Return _ResourceID
        End Get
        Set(ByVal value As String)
            _ResourceID = value
        End Set
    End Property

    ''' <summary>
    ''' ��������Դ��
    ''' </summary>
    ''' <remarks></remarks>
    Private _ParentResourceTable As String
    Public Property ParentResourceTable() As String
        Get
            Return _ParentResourceTable
        End Get
        Set(ByVal value As String)
            _ParentResourceTable = value
        End Set
    End Property


    ''' <summary>
    ''' ��������ԴID
    ''' </summary>
    ''' <remarks></remarks>
    Private _ParentResourceID As String
    Public Property ParentResourceID() As String
        Get
            Return _ParentResourceID
        End Get
        Set(ByVal value As String)
            _ParentResourceID = value
        End Set
    End Property
End Class
