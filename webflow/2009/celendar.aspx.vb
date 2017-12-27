Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class celendar
    Inherits UserPageBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Protected _year As Integer = DateTime.Now.Year
    Protected _month As Integer = DateTime.Now.Month

    Private _dtAssociate As DataTable
    Private _dtStart As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Request.QueryString("year") <> "" Then _year = CInt(Request.QueryString("year"))
        If Request.QueryString("month") <> "" Then _month = CInt(Request.QueryString("month"))
        'Response.Write(Request.QueryString("_month"))
        'Response.Write(Request.QueryString("_month"))

        _dtAssociate = Worklist.GetAssociateWorklistItems(Me.CurrentUser.Code, "CREATETIME>='" & _year & "-" & _month & "-1 00:00:00' AND CREATETIME<='" & _year & "-" & _month & "-" & CStr(DateTime.DaysInMonth(_year, _month)) & " 23:59:59'")
        _dtStart = Worklist.GetStartWorklistItems(Me.CurrentUser.Code, "CREATETIME>='" & _year & "-" & _month & "-1 00:00:00' AND CREATETIME<='" & _year & "-" & _month & "-" & CStr(DateTime.DaysInMonth(_year, _month)) & " 23:59:59'")
    End Sub

    Protected Sub GenerateCelendar(ByVal year As Integer, ByVal month As Integer)
        For i As Integer = 1 To DateTime.DaysInMonth(year, month)
            Response.Write("<tr height='25' bgColor='#FFFFD5'>")
            Response.Write("<td class='biaoti1' width='20'>" & CStr(i) & "</td>")
            Response.Write("<td class='text1' style='border-bottom:1px solid #cccccc'>&nbsp;")
            Dim rows As DataRow() = _dtAssociate.Select("CREATETIME>='" & year & "-" & month & "-" & i.ToString() & " 00:00:00' AND CREATETIME<='" & _year & "-" & _month & "-" & i.ToString() & " 23:59:59'")
            If rows.Length > 0 Then
                For j As Integer = 0 To rows.Length - 1
                    Response.Write("<a href='../process/director.aspx?action=view&WorkflowInstId=" & CStr(rows(j)("FLOWINSTID")) & "&WorklistItemId=" & CStr(rows(j)("ID")) & "' target='_balnk'>")
                    Response.Write(rows(j)("FLOWNAME"))
                    Response.Write("&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;")
                    Response.Write(rows(j)("MAINFIELDVALUE"))
                    Response.Write("</a>")
                    Response.Write("<br>" & vbCrLf)
                Next
            End If
            Response.Write("</td>")
            Response.Write("</tr>" & vbCrLf)
        Next
    End Sub

    Protected Sub GenerateDateList(ByVal year As Integer)
        For i As Integer = 1 To 12
            If _month = i Then
                Response.Write("<td class='biaoti1' width='30'>" & CStr(i) & "</td>")
            Else
                Response.Write("<td class='biaoti1' width='30'><a href='celendar.aspx?year=" & year & "&month=" & i & "'>" & CStr(i) & "</a></td>")
            End If
        Next
    End Sub

End Class

End Namespace
