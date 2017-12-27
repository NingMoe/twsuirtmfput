Imports Microsoft.VisualBasic

Public Class Field
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

    Private _IsRequired As Boolean
    Public Property IsRequired() As Boolean
        Get
            Return _IsRequired
        End Get
        Set(ByVal value As Boolean)
            _IsRequired = value
        End Set
    End Property

    Private _DataType As String
    Public Property DataType() As String
        Get
            Return _DataType
        End Get
        Set(ByVal value As String)
            _DataType = value
        End Set
    End Property
    Private _length As String
    Public Property DataLength() As String
        Get
            Return _length
        End Get
        Set(ByVal value As String)
            _length = value
        End Set
    End Property
    Private _ValueType As FieldValueType
    Public Property ValueType() As FieldValueType
        Get
            Return _ValueType
        End Get
        Set(ByVal value As FieldValueType)
            _ValueType = value
        End Set
    End Property
    Private _Width As Integer
    Public Property Width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)
            _Width = value
        End Set
    End Property

    Private _Align As HorizontalAlign
    Public Property Align() As HorizontalAlign
        Get
            Return _Align
        End Get
        Set(ByVal value As HorizontalAlign)
            _Align = value
        End Set
    End Property


    Private _Format As String
    Public Property Format() As String
        Get
            Return _Format
        End Get
        Set(ByVal value As String)
            _Format = value
        End Set
    End Property


    Public Enum FieldValueType As Integer
        Input = 0
        OptionValue = 1
        AutoCoding = 2
        Calculation = 3
        CustomizeCoding = 5
        Constant = 6
        DefaultValue = 7
        AdvDictionary = 8
        IncrementalCoding = 10
        RadioGroup = 11
        Checkbox = 12
        DirectoryFile = 13
        SystemAccount = 14
    End Enum

    Public Shared Function GetValTypeDispName(ByVal lngValType As Long) As String
        Select Case lngValType
            Case FieldValueType.Input
                Return "输入型"
            Case FieldValueType.Constant
                Return "常量"
            Case FieldValueType.DefaultValue
                Return "系统默认值"
            Case FieldValueType.OptionValue
                Return "下拉框选择项"
            Case FieldValueType.RadioGroup
                Return "多选一选择项"
            Case FieldValueType.Checkbox
                Return "是否选择项"
            Case FieldValueType.AutoCoding
                Return "自动编码"
            Case FieldValueType.IncrementalCoding
                Return "递增编码"
            Case FieldValueType.DirectoryFile
                Return "目录文件"
            Case FieldValueType.Calculation
                Return "计算公式"
            Case FieldValueType.AdvDictionary
                Return "高级字典"
            Case FieldValueType.CustomizeCoding
                Return "定制编码"
            Case FieldValueType.SystemAccount
                Return "系统帐号字典"
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function GetValTypeDefine(ByVal strValTypeDispName As String) As Long
        Select Case strValTypeDispName
            Case "输入型"
                Return FieldValueType.Input
            Case "常量"
                Return FieldValueType.Constant
            Case "系统默认值"
                Return FieldValueType.DefaultValue
            Case "下拉框选择项"
                Return FieldValueType.OptionValue
            Case "多选一选择项"
                Return FieldValueType.RadioGroup
            Case "是否选择项"
                Return FieldValueType.Checkbox
            Case "自动编码"
                Return FieldValueType.AutoCoding
            Case "递增编码"
                Return FieldValueType.IncrementalCoding
            Case "目录文件"
                Return FieldValueType.DirectoryFile
            Case "计算公式"
                Return FieldValueType.Calculation
            Case "高级字典"
                Return FieldValueType.AdvDictionary
            Case "定制编码"
                Return FieldValueType.CustomizeCoding
            Case "系统帐号字典"
                Return FieldValueType.SystemAccount
            Case Else
                Return -1
        End Select
    End Function

End Class
