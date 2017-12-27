Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class MTableSearchResultBase
    Inherits CmsPage
    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Protected Sub CreateDataGridColumn(ByRef DataGrid1 As System.Web.UI.WebControls.DataGrid)
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

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Protected Sub CreateDataGridColumnII(ByRef DataGrid1 As System.Web.UI.WebControls.DataGrid)
        Dim intWidth As Integer = 0

        Dim col As BoundColumn
        Dim dv As DataView = CmsDbBase.GetTableView(CmsPass, CmsTables.MTableColDef, "MTSCOL_HOSTID=" & VLng("PAGE_MTSHOSTID"), "MTSCOL_SHOWORDER ASC")
        Dim drv As DataRowView
        For Each drv In dv
            Dim intMTSColType As MTSearchColumnType = CType(DbField.GetLng(drv, "MTSCOL_TYPE"), MTSearchColumnType)
            If intMTSColType = MTSearchColumnType.Show Then
                Dim intW As Integer = DbField.GetInt(drv, "MTSCOL_SHOWWIDTH")
                Dim strFormat As String = DbField.GetStr(drv, "MTSCOL_SHOWFORMAT")
                col = New BoundColumn
                col.HeaderText = DbField.GetStr(drv, "MTSCOL_COLDISPNAME")
                col.DataField = DbField.GetStr(drv, "MTSCOL_COLNAME")
                col.HeaderStyle.Width = Unit.Pixel(intW)
                col.ItemStyle.Width = Unit.Pixel(intW)
                col.HeaderStyle.Wrap = False
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
    Protected Function GridDataBind(ByRef DataGrid1 As System.Web.UI.WebControls.DataGrid) As Long
        Dim lngRtnCount As Long = 0
        Try
            Dim blnUseLogicAnd As Boolean = CBool(IIf(RStr("mtslogicand") = "1", True, False))
            Dim strRtnTables As String = ""
            Dim strRtnWhere As String = ""
            Dim strSql As String = MultiTableSearchColumn.GenerateSql(CmsPass, VLng("PAGE_MTSHOSTID"), blnUseLogicAnd, VLng("PAGE_RESID"), strRtnTables, strRtnWhere)
            ViewState("PAGE_QUERY_TABLES") = strRtnTables
            ViewState("PAGE_QUERY_WHERE") = strRtnWhere
            Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
            lngRtnCount = ds.Tables(0).DefaultView.Count
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        Return lngRtnCount
    End Function
End Class

End Namespace
