Imports Unionsoft.Components.Charting
Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ChartView
    Inherits AbstractChartPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        
    End Sub

    Public Overrides Function generateChart() As Unionsoft.Components.Charting.AbstractChart
        Dim dtChart As DataTable = ChartBasicCode.GetChart(Request("chartid").ToString)
        If dtChart.Rows.Count > 0 Then

            Dim strSql As String = DbField.GetStr(dtChart.Rows(0), "Sqlstring")
            Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)


            Select Case DbField.GetInt(dtChart.Rows(0), "ChartKind")
                Case ChartKind.BarChart
                    Return GenerateBarChart(DbField.GetStr(dtChart.Rows(0), "chartname"), dt)
                Case ChartKind.PieChart
                    Return GeneratePieChart(DbField.GetStr(dtChart.Rows(0), "chartname"), dt)
                Case ChartKind.LineChart
                    Return GenerateLineChart(DbField.GetStr(dtChart.Rows(0), "chartname"), dt)
            End Select
        End If
    End Function

    '
    Private Function GeneratePieChart(ByVal title As String, ByVal dt As DataTable) As Unionsoft.Components.Charting.AbstractChart
        Dim al As ArrayList = GenerateColorList(dt.Rows.Count)
        Dim chart As Piechart = New Piechart(800, 600)
        chart.Enable3D = True
        chart.EnableLegend = True
        Dim ds As DefaultDataSource = New DefaultDataSource(title)
        Dim Series1 As Series = New Series("Data 0")
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Columns.Count <= 2 Then
                Series1.add(New DataPoint(dt.Rows(i)(0).ToString, CInt(dt.Rows(i)(1)), CType(al(i), Color)))
            Else
                Series1.add(New DataPoint(dt.Rows(i)(0).ToString & dt.Rows(i)(1).ToString, CInt(dt.Rows(i)(2)), CType(al(i), Color)))
            End If
        Next
        ds.add(Series1)
        chart.DataSource = ds
        Return chart
    End Function
    Private Function GenerateBarChart(ByVal title As String, ByVal dt As DataTable) As Unionsoft.Components.Charting.AbstractChart
        Dim al As ArrayList = GenerateColorList(dt.Rows.Count)
        Dim chart As Barchart = New Barchart(800, 600)
        chart.Enable3D = True
        chart.EnableLegend = True
        Dim ds As DefaultDataSource = New DefaultDataSource(title)

        If dt.Columns.Count <= 2 Then
            Dim Series1 As Series = New Series("Data 0")
            For i As Integer = 0 To dt.Rows.Count - 1
                Series1.add(New DataPoint(dt.Rows(i)(0).ToString, CInt(dt.Rows(i)(1)), CType(al(i), Color)))
            Next
            ds.add(Series1)
        Else
            Dim strTempName As String = dt.Rows(0)(0).ToString
            Dim Series1 As Series = New Series(strTempName, CType(al(0), Color))
            Dim j As Integer = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                If DbField.GetStr(dt.Rows(i), dt.Columns(0).ColumnName) = strTempName Then
                    Series1.add(New DataPoint(dt.Rows(i)(1).ToString, CInt(dt.Rows(i)(2))))
                Else
                    ds.add(Series1)
                    strTempName = dt.Rows(i)(0).ToString
                    j = j + 1
                    Series1 = New Series(strTempName, CType(al(j), Color))
                    Series1.add(New DataPoint(dt.Rows(i)(1).ToString, CInt(dt.Rows(i)(2))))
                End If
            Next
            ds.add(Series1)
        End If
        
        chart.DataSource = ds
        Return chart
    End Function

    Private Function GenerateLineChart(ByVal title As String, ByVal dt As DataTable) As Unionsoft.Components.Charting.AbstractChart
        Dim al As ArrayList = GenerateColorList(dt.Rows.Count)
        Dim chart As LineChart = New LineChart(800, 600)
        chart.Enable3D = True
        chart.EnableLegend = True
        Dim ds As DefaultDataSource = New DefaultDataSource(title)

        If dt.Columns.Count <= 2 Then
            Dim Series1 As Series = New Series("Data 0")
            For i As Integer = 0 To dt.Rows.Count - 1
                Series1.add(New DataPoint(dt.Rows(i)(0).ToString, CInt(dt.Rows(i)(1)), CType(al(i), Color)))
            Next
            ds.add(Series1)
        Else
            Dim strTempName As String = dt.Rows(0)(0).ToString
            Dim Series1 As Series = New Series(strTempName, CType(al(0), Color))
            Dim j As Integer = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                If DbField.GetStr(dt.Rows(i), dt.Columns(0).ColumnName) = strTempName Then
                    Series1.add(New DataPoint(dt.Rows(i)(1).ToString, CInt(dt.Rows(i)(2))))
                Else
                    ds.add(Series1)
                    strTempName = dt.Rows(i)(0).ToString
                    j = j + 1
                    Series1 = New Series(strTempName, CType(al(j), Color))
                    Series1.add(New DataPoint(dt.Rows(i)(1).ToString, CInt(dt.Rows(i)(2))))
                End If
            Next
            ds.add(Series1)
        End If
        chart.DataSource = ds
        Return chart
    End Function
    Private Function GenerateColorList(ByVal count As Integer) As ArrayList
        Dim al As New ArrayList
        al.Add(Color.FromArgb(255, 255, 0))
        al.Add(Color.FromArgb(153, 153, 255))
        al.Add(Color.FromArgb(153, 51, 102))
        al.Add(Color.FromArgb(255, 255, 204))
        al.Add(Color.FromArgb(204, 255, 255))
        al.Add(Color.FromArgb(102, 0, 102))
        al.Add(Color.FromArgb(255, 128, 128))
        al.Add(Color.FromArgb(0, 102, 204))
        al.Add(Color.FromArgb(204, 204, 255))
        al.Add(Color.FromArgb(0, 0, 128))
        al.Add(Color.FromArgb(255, 0, 255))
        al.Add(Color.FromArgb(0, 255, 255))
        al.Add(Color.FromArgb(128, 0, 128))

        For i As Integer = 13 To count
            al.Add(Color.FromArgb(CInt((New Random).NextDouble() * 255), CInt((New Random).NextDouble() * 255), CInt((New Random).NextDouble() * 255)))
        Next
        Return al
    End Function
End Class

End Namespace
