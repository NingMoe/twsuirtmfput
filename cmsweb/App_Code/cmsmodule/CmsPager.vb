'---------------------------------------------------------------------------------------
'通用分页控件，可用于DataGrid、Repeater等控件中
'---------------------------------------------------------------------------------------
Option Explicit On 
Option Strict On

Imports System.Web.UI
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class CmsPager
    Inherits Control
    Implements IPostBackEventHandler

    'Private m_intPageRows As Integer = 0 '每页行数
    'Private m_intCurrentPage As Integer = 0 '当前页，从0计数
    'Private m_intRecStartOnCurPage As Integer = 0 '当前页的第一条记录的序号，从0计数
    'Private m_intTotalPageNumber As Integer = 0 '总页数
    'Private m_intTotalRecordNumber As Integer = 0 '总记录数
    'Private m_strLanguage As String = "cn" '当前语言
    'Private m_strAlign As String = "right" '页信息显示的对齐方式

    'Private m_intTotalWidth As Integer = 250 '总宽度，单位 px
    'Private m_intButtonsWidth As Integer = 100 '分页按钮的宽度，单位 px

    Public Event Click(ByVal sender As Object, ByVal eventArgument As String)

    '当前页号
    Public Property CurrentPage() As Integer
        Get
            Return CInt(ViewState("m_intCurrentPage"))
        End Get
        Set(ByVal Value As Integer)
            ViewState("m_intCurrentPage") = Value
            ViewState("m_intRecStartOnCurPage") = Value * PageRows()
        End Set
    End Property

    '当前页号
    Public ReadOnly Property RecStartOnCurPage() As Integer
        Get
            Return CInt(ViewState("m_intRecStartOnCurPage"))
        End Get
    End Property

    '总页数
    Public ReadOnly Property TotalPageNumber() As Integer
        Get
            Return CInt(ViewState("m_intTotalPageNumber"))
        End Get
    End Property

    Public Property TotalRecordNumber() As Integer
        Get
            Return CInt(ViewState("m_intTotalRecordNumber"))
        End Get
        Set(ByVal Value As Integer)
            ViewState("m_intTotalRecordNumber") = Value

            Dim intTotalPageNum As Integer = CInt(Value \ PageRows()) + CInt(IIf(Value Mod PageRows() > 0, 1, 0))
            ViewState("m_intTotalPageNumber") = intTotalPageNum
        End Set
    End Property

    '#############################################################################################
    '#############################################################################################

    Public Property PageRows() As Integer
        Get
            Return CInt(ViewState("m_intPageRows"))
        End Get
        Set(ByVal Value As Integer)
            ViewState("m_intPageRows") = Value
        End Set
    End Property

    Public Property TableHeight() As String
        Get
            Return CStr(ViewState("PAGING_TABLEHEIGHT"))
        End Get
        Set(ByVal Value As String)
            ViewState("PAGING_TABLEHEIGHT") = Value
        End Set
    End Property

    Public Property TotalWidth() As String
        Get
            Dim strRtn As String = CStr(ViewState("m_intTotalWidth"))
            If strRtn = "" Then strRtn = "280"
            Return strRtn
        End Get
        Set(ByVal Value As String)
            ViewState("m_intTotalWidth") = Value
        End Set
    End Property

    Public Property ButtonsWidth() As String
        Get
            Dim strRtn As String = CStr(ViewState("m_intButtonsWidth"))
            If strRtn = "" Then strRtn = "130"
            Return strRtn
        End Get
        Set(ByVal Value As String)
            ViewState("m_intButtonsWidth") = Value
        End Set
    End Property

    '对齐方式
    Public Property TableAlign() As String
        Get
            Return CStr(ViewState("PAGING_TABLEALIGN"))
        End Get
        Set(ByVal Value As String)
            ViewState("PAGING_TABLEALIGN") = Value
        End Set
    End Property

    '对齐方式
    Public Property ButtonAlign() As String
        Get
            Return CStr(ViewState("PAGING_BTNALIGN"))
        End Get
        Set(ByVal Value As String)
            ViewState("PAGING_BTNALIGN") = Value
        End Set
    End Property

    '对齐方式
    Public Property WordAlign() As String
        Get
            Return CStr(ViewState("m_strAlign"))
        End Get
        Set(ByVal Value As String)
            ViewState("m_strAlign") = Value
        End Set
    End Property

    '对齐方式
    Public Property BgColor() As String
        Get
            Return CStr(ViewState("PAGING_BGCOLOR"))
        End Get
        Set(ByVal Value As String)
            ViewState("PAGING_BGCOLOR") = Value
        End Set
    End Property

    '当前语言
    Public Property Language() As String
        Get
            Return CStr(ViewState("m_strLanguage"))
        End Get
        Set(ByVal Value As String)
            ViewState("m_strLanguage") = Value
            End Set

    End Property


    Public Sub RaisePostBackEvent(ByVal eventArgument As String) Implements System.Web.UI.IPostBackEventHandler.RaisePostBackEvent
        RaiseEvent Click(Me, eventArgument)
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Dim strBriefLanguage As String = CmsMessage.GetBriefLanguage(Language())
        Dim strTableAlign As String = TableAlign()
        If strTableAlign = "" Then strTableAlign = "right"
        Dim strBtnAlign As String = ButtonAlign()
        If strBtnAlign = "" Then strBtnAlign = "left"
        Dim strWordAlign As String = WordAlign()
        If strWordAlign = "" Then strWordAlign = "right"
        'Dim strBgColor As String = BgColor()
        'If strBgColor <> "" Then strBgColor = "bgcolor=""" & strBgColor & """"
        If strBriefLanguage = "cn" Then
            writer.WriteLine("<table bgcolor=""" & BgColor() & """ width=""" & TotalWidth() & """ height='" & TableHeight() & "' align=""" & strTableAlign & """ valign='middle' border=""0"" cellpadding=""0"" cellspacing=""0"">")
            writer.WriteLine("<tr>")
                writer.WriteLine("    <td nowrap align=""" & strBtnAlign & """ width=""" & ButtonsWidth() & """>")
                writer.WriteLine("<a style='color:#009;text-decoration:none;border-bottom:2px solid #F00; line-height:150%; font-size:13px;' href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveFirstPage") & """>首页</a>&nbsp;")
                writer.WriteLine("<a style='color:#009;text-decoration:none;border-bottom:2px solid #F00; line-height:150%; font-size:13px;' href=""javascript:" & Page.GetPostBackEventReference(Me, "MovePreviousPage") & """>上页</a>&nbsp;")
                writer.WriteLine("<a style='color:#009;text-decoration:none;border-bottom:2px solid #F00; line-height:150%; font-size:13px;' href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveNextPage") & """>下页</a>&nbsp;")
                writer.WriteLine("<a style='color:#009;text-decoration:none;border-bottom:2px solid #F00; line-height:150%; font-size:13px;' href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveLastPage") & """>末页</a>&nbsp;")
            writer.WriteLine("    </td>")
            writer.WriteLine("    <td nowrap align=""" & strWordAlign & """>")
            writer.WriteLine("       &nbsp;" & TotalRecordNumber() & "&nbsp;条,&nbsp;页&nbsp;" & (CurrentPage() + 1) & "/" & TotalPageNumber())
                writer.WriteLine("    &nbsp;</td>")
            writer.WriteLine("</tr>")
            writer.WriteLine("</table>")
        Else
            writer.WriteLine("<table bgcolor=""" & BgColor() & """ width=""" & TotalWidth() & """ height='" & TableHeight() & "' align=""" & strTableAlign & """ valign='middle' border=""0"" cellpadding=""0"" cellspacing=""0"">")
            writer.WriteLine("<tr>")
            writer.WriteLine("    <td nowrap align=""" & strBtnAlign & """ width=""" & ButtonsWidth() & """>")
            writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveFirstPage") & """>First</a>")
            writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MovePreviousPage") & """>Prev</a>")
            writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveNextPage") & """>Next</a>")
            writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveLastPage") & """>Last</a>")
            writer.WriteLine("    </td>")
            writer.WriteLine("    <td nowrap align=""" & strWordAlign & """>")
            writer.WriteLine("       &nbsp;" & TotalRecordNumber() & "&nbsp;Recs,&nbsp;Page&nbsp;" & (CurrentPage() + 1) & "/" & TotalPageNumber())
            writer.WriteLine("    </td>")
            writer.WriteLine("</tr>")
            writer.WriteLine("</table>")
        End If
    End Sub

    '----------------------------------------------------------
    '提取Dataset数据前配置页数控件
    '----------------------------------------------------------
    Public Sub AdjustCurrentPage()
        Dim intCurrentPage As Integer = CurrentPage()
        Dim intTotalPageNum As Integer = TotalPageNumber()
        If intCurrentPage > (intTotalPageNum - 1) Then CurrentPage = intTotalPageNum - 1
    End Sub

    '----------------------------------------------------------
    '提取Dataset数据前配置页数控件
    '----------------------------------------------------------
    Public Sub DealPageCommandBeforeBind(ByVal strPageCommand As String)
        Dim intCurrentPage As Integer = 0
        Select Case strPageCommand
            Case "MoveFirstPage" '首页
                intCurrentPage = 0

            Case "MovePreviousPage" '上页
                intCurrentPage = CInt(IIf(CurrentPage() <= 0, 0, CurrentPage() - 1))

            Case "MoveNextPage" '下页
                intCurrentPage = CInt(IIf((CurrentPage() + 1) <= (TotalPageNumber() - 1), CurrentPage() + 1, TotalPageNumber() - 1)) '不能超过最后一页

            Case "MoveLastPage" '末页
                intCurrentPage = TotalPageNumber() - 1

            Case "MoveCurrentPage"
                intCurrentPage = CurrentPage()
                Dim intTotalPageNum As Integer = TotalPageNumber()
                If intCurrentPage > (intTotalPageNum - 1) Then intCurrentPage = intTotalPageNum - 1
                Return

            Case Else
                Return
        End Select

        ViewState("m_intCurrentPage") = intCurrentPage
        ViewState("m_intRecStartOnCurPage") = intCurrentPage * PageRows()
    End Sub
End Class

End Namespace
