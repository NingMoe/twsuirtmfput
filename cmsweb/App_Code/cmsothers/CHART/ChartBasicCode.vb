Imports NetReusables


Namespace Unionsoft.Cms.Web


Public Enum ChartKind
    BarChart = 1
    PieChart = 2
    LineChart = 3
End Enum

Public Class ChartBasicCode
    Public Shared ChartDefineTable As String = "CMS_Chart_Define"

    Public Shared Function GetChartDefineList(ByVal strResId As String) As DataTable
        Dim strSql As String
        strSql = "select * from " & ChartDefineTable & " where ACTIVEFLAG=1 and DELETEDFLAG=0 and resid=" & strResId & " order by CREATETIME"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        Return dt
    End Function

    Public Shared Function GetChart(ByVal strChartId As String) As DataTable
        Dim strSql As String
        strSql = "select * from " & ChartDefineTable & " where id=" & strChartId
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        Return dt
    End Function
End Class

End Namespace
