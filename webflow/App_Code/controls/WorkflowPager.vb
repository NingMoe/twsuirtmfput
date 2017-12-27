Option Explicit On 
Option Strict On


Namespace Unionsoft.Workflow.Web



Public Class WorkflowPager
    Inherits Control
    Implements IPostBackEventHandler

    Private lngCurrentPage As Long = 0
    Private lngPageCount As Long = 1
    Private lngRecordCount As Long = 0
    Private _CssClass As String = ""

    Public Event Click(ByVal sender As Object, ByVal eventArgument As String)

    '��ǰҳ��
    Public Property CurrentPage() As Long
        Get
            Return lngCurrentPage
        End Get
        Set(ByVal Value As Long)
            lngCurrentPage = Value
        End Set
    End Property

    '��ҳ��
    Public Property PageCount() As Long
        Get
            Return lngPageCount
        End Get
        Set(ByVal Value As Long)
            lngPageCount = Value
        End Set
    End Property

    Public Property RecordCount() As Long
        Get
            Return lngRecordCount
        End Get
        Set(ByVal Value As Long)
            lngRecordCount = Value
        End Set
    End Property

    Public WriteOnly Property CssClass() As String
        Set(ByVal Value As String)
            _CssClass = Value
        End Set
    End Property

    Public Sub RaisePostBackEvent(ByVal eventArgument As String) Implements System.Web.UI.IPostBackEventHandler.RaisePostBackEvent
        RaiseEvent Click(Me, eventArgument)
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        writer.WriteLine("<table width=""100%"" align=""center"" border=""0"" class=""" & _CssClass & """ cellpadding=""0"" cellspacing=""0"">")
        writer.WriteLine("<tr>")
        'writer.WriteLine("    <td nowrap width=""200"">")

        'writer.WriteLine("    </td>")
        writer.WriteLine("    <td nowrap align=""right"">")
        writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveFirstPage") & """>��һҳ</a>")
        writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MovePreviousPage") & """>��һҳ</a>")
        writer.WriteLine("       &nbsp;�� " & (lngCurrentPage + 1) & "/" & lngPageCount & " ҳ")
        writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveNextPage") & """>��һҳ</a>")
        writer.WriteLine("        <a href=""javascript:" & Page.GetPostBackEventReference(Me, "MoveLastPage") & """>���ҳ</a>")
        writer.WriteLine("    </td>")
        writer.WriteLine("</tr>")
        writer.WriteLine("</table>")
    End Sub


End Class

End Namespace
