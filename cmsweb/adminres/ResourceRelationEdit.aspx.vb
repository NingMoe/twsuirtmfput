Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceRelationEdit
    Inherits CmsPage

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

        If VLng("PAGE_RESID1") = 0 Then
            ViewState("PAGE_RESID1") = RLng("mnuresid1")
        End If
        If VLng("PAGE_RESID2") = 0 Then
            ViewState("PAGE_RESID2") = RLng("mnuresid2")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '主关联资源的名称
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID1")).ResName

        If RLng("selresid") <> 0 Then
            ViewState("PAGE_RESID2") = RLng("selresid") '刚从选择资源窗体回来
        End If

        LoadResColumns(VLng("PAGE_RESID1"), ListBox1)   'Load主表字段列表
        LoadResColumns(VLng("PAGE_RESID2"), ListBox2)  'Load从表字段列表

        'Load DataGrid
        WebUtilities.InitialDataGrid(DataGrid1) '初始化DataGrid属性
        CreateDataGridColumn()
        GridDataBind(VLng("PAGE_RESID1"), VLng("PAGE_RESID2"))  '绑定数据

        '-----------------------------------------------------------
        '判断是否提供输入关联功能
        btnAddInputRelatedCol.Visible = CmsFunc.IsEnable("FUNC_RELTABLE_INPUT")

        '判断是否提供显示关联功能
        btnAddShowRelation.Visible = CmsFunc.IsEnable("FUNC_RELTABLE_SHOW")

        '判断是否提供计算关联功能
        btnAddCalcRelation.Visible = CmsFunc.IsEnable("FUNC_RELTABLE_CALC")
        '-----------------------------------------------------------
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAddMainRelatedCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMainRelatedCol.Click
        AddRelatedCol(TabRelationType.MainRelationCol)
    End Sub

    Private Sub btnAddInputRelatedCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddInputRelatedCol.Click
        AddRelatedCol(TabRelationType.InputRelationCol)
    End Sub

    Private Sub btnAddShowRelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddShowRelation.Click
        AddRelatedCol(TabRelationType.ShowRelationCol)
    End Sub

    Private Sub btnAddCalcRelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCalcRelation.Click
        AddRelatedCol(TabRelationType.CalcRelationCol)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnSelectRelRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelectRelRes.Click
        Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/ResourceRelationEdit.aspx?mnuresid1=" & VLng("PAGE_RESID1") '& "&mnuresid2=" & VLng("PAGE_RESID2")
        Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx?mnuresid=" & VLng("PAGE_RESID1"), False)
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            '获取关联信息的记录ID
            Dim strTemp As String = e.Item.Cells(0).Text
            If IsNumeric(strTemp.Trim()) Then
                Dim lngAiid As Long = CLng(strTemp.Trim())
                CmsTableRelation.DelRelatedColumn(CmsPass, lngAiid) '删除关联信息
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind(VLng("PAGE_RESID1"), VLng("PAGE_RESID2"))  '绑定数据
    End Sub

    '--------------------------------------------------------------
    '添加表相关关系
    '--------------------------------------------------------------
    Private Sub AddRelatedCol(ByVal lngRelType As Long)
        Try
            CmsTableRelation.AddRelatedColumn(CmsPass, VLng("PAGE_RESID1"), ListBox1.SelectedItem.Value, VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, lngRelType)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID1"), VLng("PAGE_RESID2"))  '绑定数据
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "RT_AIID" '关键字段
        col.DataField = "RT_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "主表字段"
        col.DataField = "RT_TAB1_COLDISPNAME"
        col.ItemStyle.Width = Unit.Pixel(170)
        DataGrid1.Columns.Add(col)
        intWidth += 170

        col = New BoundColumn
        col.HeaderText = "从表字段"
        col.DataField = "RT_TAB2_COLDISPNAME"
        col.ItemStyle.Width = Unit.Pixel(170)
        DataGrid1.Columns.Add(col)
        intWidth += 170

        col = New BoundColumn
        col.HeaderText = "关联类型"
        col.DataField = "RT_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(col)
        intWidth += 100

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(50)
        DataGrid1.Columns.Add(colDel)
        intWidth += 50

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID1 As Long, ByVal lngResID2 As Long)
        Try
            If lngResID1 > 0 And lngResID2 > 0 Then
                Dim ds As DataSet = CmsTableRelation.GetAllRelatedColumnsForDesign(CmsPass, lngResID1, lngResID2)
                DataGrid1.DataSource = ds.Tables(0).DefaultView
                DataGrid1.DataBind()
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub LoadResColumns(ByVal lngResID As Long, ByRef lstCols As ListBox)
        If lngResID = 0 Then Return

        If lstCols.Items.Count <= 0 Then
            lstCols.Items.Clear() '清空列表

            Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(CmsPass, lngResID, True, True)
            Dim datCol As DataTableColumn
            For Each datCol In alistColumns
                lstCols.Items.Add(New ListItem(datCol.ColDispName, datCol.ColName))
            Next
            alistColumns.Clear()
            alistColumns = Nothing
        End If
    End Sub
End Class

End Namespace
