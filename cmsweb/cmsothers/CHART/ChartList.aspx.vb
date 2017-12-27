Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ChartList
        Inherits Unionsoft.Platform.CmsPage

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

    Dim dt As DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        'If Not Page.IsPostBack Then
        GridDataBind()
        'End If
    End Sub
    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

            WebUtilities.InitialDataGrid(DataGrid1) '初始化DataGrid属性
            'CreateDataGridColumn()
        Catch ex As Exception
            SLog.Err("提醒列表显示异常出错！", ex)
        End Try
    End Sub

    Private Sub GridDataBind()
        dt = ChartBasicCode.GetChartDefineList(RStr("mnuresid"))
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()

    End Sub
    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            Dim lngChartID As Long = CLng(e.Item.Cells(0).Text)
            CmsDbStatement.DelRows(NetReusables.SDbConnectionPool.GetDbConfig(), ChartBasicCode.ChartDefineTable, "ID=" & lngChartID)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
        GridDataBind()
    End Sub
    Private Sub DataGrid1_SelectCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.ItemCommand
        Dim lngChartID As Long = CLng(e.Item.Cells(0).Text)
        If e.CommandName.ToLower() = "select" Then

            Response.Redirect("ChartDefine.aspx?mnuresid=" & RStr("mnuresid") & "&chartid=" & lngChartID)
        ElseIf e.CommandName.ToLower() = "view" Then

            Response.Redirect("Chartview.aspx?chartid=" & lngChartID)

        End If
    End Sub

    Private Sub btnAddCond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCond.Click
        Response.Redirect("ChartDefine.aspx?mnuresid=" & RStr("mnuresid"))
    End Sub
End Class

End Namespace
