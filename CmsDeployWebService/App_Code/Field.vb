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
                Return "������"
            Case FieldValueType.Constant
                Return "����"
            Case FieldValueType.DefaultValue
                Return "ϵͳĬ��ֵ"
            Case FieldValueType.OptionValue
                Return "������ѡ����"
            Case FieldValueType.RadioGroup
                Return "��ѡһѡ����"
            Case FieldValueType.Checkbox
                Return "�Ƿ�ѡ����"
            Case FieldValueType.AutoCoding
                Return "�Զ�����"
            Case FieldValueType.IncrementalCoding
                Return "��������"
            Case FieldValueType.DirectoryFile
                Return "Ŀ¼�ļ�"
            Case FieldValueType.Calculation
                Return "���㹫ʽ"
            Case FieldValueType.AdvDictionary
                Return "�߼��ֵ�"
            Case FieldValueType.CustomizeCoding
                Return "���Ʊ���"
            Case FieldValueType.SystemAccount
                Return "ϵͳ�ʺ��ֵ�"
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function GetValTypeDefine(ByVal strValTypeDispName As String) As Long
        Select Case strValTypeDispName
            Case "������"
                Return FieldValueType.Input
            Case "����"
                Return FieldValueType.Constant
            Case "ϵͳĬ��ֵ"
                Return FieldValueType.DefaultValue
            Case "������ѡ����"
                Return FieldValueType.OptionValue
            Case "��ѡһѡ����"
                Return FieldValueType.RadioGroup
            Case "�Ƿ�ѡ����"
                Return FieldValueType.Checkbox
            Case "�Զ�����"
                Return FieldValueType.AutoCoding
            Case "��������"
                Return FieldValueType.IncrementalCoding
            Case "Ŀ¼�ļ�"
                Return FieldValueType.DirectoryFile
            Case "���㹫ʽ"
                Return FieldValueType.Calculation
            Case "�߼��ֵ�"
                Return FieldValueType.AdvDictionary
            Case "���Ʊ���"
                Return FieldValueType.CustomizeCoding
            Case "ϵͳ�ʺ��ֵ�"
                Return FieldValueType.SystemAccount
            Case Else
                Return -1
        End Select
    End Function

End Class
