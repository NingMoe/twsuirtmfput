Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ReportSimple
    Inherits Cms.Web.RecordEditBase

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_MTSHOSTID") = 0 Then
            ViewState("PAGE_MTSHOSTID") = RLng("mtshostid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '显示页眉窗体
        PageDealFirstRequest(CmsPass, panelForm, 0, 0, VLng("PAGE_RESID"), VLng("PAGE_RECID"), InputMode.PrintInHostTable, "默认窗体", Nothing, True)      '处理第一个GET请求中的事务

        Dim DataGrid1 As DataGrid = InitialDataGrid() '初始化DataGrid
        WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
        CreateDataGridColumn(DataGrid1)
        GridDataBind(DataGrid1)

        '显示页脚窗体
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    '----------------------------------------------------------
    '初始化DataGrid
    '----------------------------------------------------------
    Private Function InitialDataGrid() As DataGrid
        Dim DataGrid1 As New DataGrid
        DataGrid1.ID = "DataGrid_" & TimeId.CurrentMilliseconds()
        DataGrid1.Visible = True
        'AddHandler DataGrid1.Init, AddressOf DataGrid1_Init

        Dim strGridStyle As String = "POSITION: absolute; top:" & 100 & "px;left:" & 0 & "px"
        DataGrid1.Attributes.Add("style", strGridStyle)
        panelForm.Controls.Add(DataGrid1)

        Return DataGrid1
    End Function

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn(ByRef DataGrid1 As DataGrid)
        Dim intWidth As Integer = 0

        Dim col As BoundColumn
        Dim dv As DataView = CmsDbBase.GetTableView(CmsPass, CmsTables.MTableColDef, "MTSCOL_HOSTID=" & VLng("PAGE_MTSHOSTID"), "MTSCOL_SHOWORDER ASC")
        Dim drv As DataRowView
        For Each drv In dv
            Dim intMTSColType As MTSearchColumnType = CType(DbField.GetLng(drv, "MTSCOL_TYPE"), MTSearchColumnType)
            If intMTSColType = MTSearchColumnType.Show Then
                col = New BoundColumn
                col.HeaderText = DbField.GetStr(drv, "MTSCOL_COLDISPNAME")
                col.DataField = DbField.GetStr(drv, "MTSCOL_COLNAME")
                Dim intW As Integer = DbField.GetInt(drv, "MTSCOL_SHOWWIDTH")
                col.ItemStyle.Width = Unit.Pixel(intW)
                Dim strFormat As String = DbField.GetStr(drv, "MTSCOL_SHOWFORMAT")
                If strFormat <> "" Then
                    col.DataFormatString = strFormat
                End If
                DataGrid1.Columns.Add(col)
                intWidth += intW
            End If
        Next

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByRef DataGrid1 As DataGrid)
        Try
            Dim blnUseLogicAnd As Boolean = CBool(IIf(RStr("mtslogicand") = "1", True, False))
            Dim strSql As String = MultiTableSearchColumn.GenerateSql(CmsPass, VLng("PAGE_MTSHOSTID"), blnUseLogicAnd, VLng("PAGE_RESID"), "", "")
            Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub
End Class

End Namespace
