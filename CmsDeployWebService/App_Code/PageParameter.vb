Imports Microsoft.VisualBasic

Public Class PageParameter
    Private _PageIndex As Integer = 0
    ''' <summary>
    ''' ҳ��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageIndex() As Integer
        Get
            Return _PageIndex
        End Get
        Set(ByVal value As Integer)
            _PageIndex = value
        End Set
    End Property
    Private _SortField As String = "id"
    ''' <summary>
    ''' �����ֶ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SortField() As String
        Get
            Return _SortField
        End Get
        Set(ByVal value As String)
            _SortField = value
        End Set
    End Property


    Private _SortBy As String = "desc"

    ''' <summary>
    ''' �����ֶ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Property SortBy() As String
        Get
            Return _SortBy
        End Get
        Set(ByVal value As String)
            _SortBy = value
        End Set
    End Property

    Private _PageSize As Integer = 10
    ''' <summary>
    ''' ÿҳ��ʾ������
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageSize() As Integer
        Get
            Return _PageSize
        End Get
        Set(ByVal value As Integer)
            _PageSize = value
        End Set
    End Property
   
    
End Class
