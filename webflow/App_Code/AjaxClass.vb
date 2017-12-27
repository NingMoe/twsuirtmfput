Imports Microsoft.VisualBasic
Imports NetReusables
Public Class AjaxClass
    <AjaxPro.AjaxMethod()> _
    Public Function getAttriType() As String
        Dim str As String = ""
        Dim strsql As String = "select * FROM PM_ProMember "
        Dim dt As DataTable = SDbStatement.Query(strsql).Tables(0)
        For index As Integer = 0 To dt.Rows.Count - 1
            str += dt.Rows(index)("ProjectName").ToString + "," + dt.Rows(index)("ProjectCode").ToString
            If index <> dt.Rows.Count - 1 Then
                str += "|"
            End If
        Next
        Return str


    End Function
End Class
