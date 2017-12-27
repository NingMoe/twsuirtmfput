Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceRelationList
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblPageTitle.Text = "关联表设置 （" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName & "）"

        If CmsFunc.IsEnable("FUNC_WORKFLOW") = False Then
            lbtnSetToDocFlow.Visible = False
            lbtnClearDocFlow.Visible = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind(VLng("PAGE_RESID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_ResourceRelationEdit") = "/cmsweb/adminres/ResourceRelationList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & GetRecIDOfGrid()
        Response.Redirect("/cmsweb/adminres/ResourceRelationEdit.aspx?mnuresid1=" & VLng("PAGE_RESID") & "&mnuresid2=0", False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngSubResID As Long = GetRecIDOfGrid()
        If lngSubResID = 0 Then
            PromptMsg("请选择需要操作的关联表！")
            Return
        End If

        Session("CMSBP_ResourceRelationEdit") = "/cmsweb/adminres/ResourceRelationList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & GetRecIDOfGrid()
        Response.Redirect("/cmsweb/adminres/ResourceRelationEdit.aspx?mnuresid1=" & VLng("PAGE_RESID") & "&mnuresid2=" & lngSubResID, False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngSubResID As Long = GetRecIDOfGrid() '是关联表的记录ID
        If lngSubResID = 0 Then
            PromptMsg("请选择需要操作的关联表！")
            Return
        End If

        Try
            '当前删除的关联表如果是工作流文档附件，则不能删除。
            If CmsPass.GetDataRes(VLng("PAGE_RESID")).WorkFlowSubResID = lngSubResID Then
                PromptMsg("作为工作流文档附件的关联表关系不能被删除！")
            Else
                CmsTableRelation.DelRelatedResource(CmsPass, VLng("PAGE_RESID"), lngSubResID)
            End If

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnSetToDocFlow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSetToDocFlow.Click
        Try
            Dim lngSubResID As Long = GetRecIDOfGrid()
            If lngSubResID = 0 Then
                PromptMsg("请选择需要设置为工作流附件文档的关联表！")
                Return
            End If

            '设置工作流附件文档关联表
            ResFactory.ResService.SetWorkflowSubRes(CmsPass, VLng("PAGE_RESID"), lngSubResID)

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnClearDocFlow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearDocFlow.Click
        Try
            '清除工作流附件文档关联表
            ResFactory.ResService.ClearWorkflowSubRes(CmsPass, VLng("PAGE_RESID"))

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnEnableRelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnEnableRelEdit.Click
        Try
            Dim lngSubResID As Long = GetRecIDOfGrid()
            If lngSubResID = 0 Then
                PromptMsg("请选择需要设置为工作流附件文档的关联表！")
                Return
            End If

            '设置关联编辑
            CmsTableRelation.SetRelationEdit(CmsPass, VLng("PAGE_RESID"), lngSubResID, True)

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnClearRelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearRelEdit.Click
        Try
            Dim lngSubResID As Long = GetRecIDOfGrid()
            If lngSubResID = 0 Then
                PromptMsg("请选择需要设置为工作流附件文档的关联表！")
                Return
            End If

            '设置关联编辑
            CmsTableRelation.SetRelationEdit(CmsPass, VLng("PAGE_RESID"), lngSubResID, False)

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Try
            Dim lngSubResID As Long = GetRecIDOfGrid()
            If lngSubResID = 0 Then
                PromptMsg("请选择需要操作的关联表！")
                Return
            End If
            CmsTableRelation.MoveUp(CmsPass, VLng("PAGE_RESID"), lngSubResID)

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Try
            Dim lngSubResID As Long = GetRecIDOfGrid()
            If lngSubResID = 0 Then
                PromptMsg("请选择需要操作的关联表！")
                Return
            End If
            CmsTableRelation.MoveDown(CmsPass, VLng("PAGE_RESID"), lngSubResID)

            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

        WebUtilities.InitialDataGrid(DataGrid1) '初始化DataGrid属性
        CreateDataGridColumn()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中部分代码联合使用
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '设置客户端的记录ID和Javascript方法，第三列是关联表的资源ID
                Dim strRecID As String = row.Cells(2).Text.Trim()
                If IsNumeric(strRecID) Then
                    row.Attributes.Add("RECID", strRecID) '在客户端保存记录ID
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '在客户端生成：点击记录的响应方法OnClick()

                    If lngRecIDClicked > 0 And lngRecIDClicked = CLng(strRecID) Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '修改被点击记录的背景颜色
                    End If
                End If
            Next
        End If
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        '第1列必须是记录ID
        'Dim col As BoundColumn = New BoundColumn
        'col.HeaderText = "RT_AIID"
        'col.DataField = "RT_AIID"
        'col.ItemStyle.Width = Unit.Pixel(1)
        'col.Visible = False
        'col.ReadOnly = True
        'DataGrid1.Columns.Add(col)

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "TEMP_RESID1"
        col.DataField = "TEMP_RESID1"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "主表资源名称"
        col.DataField = "TEMP_RESNAME1"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        intWidth += 250

        col = New BoundColumn
        col.HeaderText = "TEMP_RESID2"
        col.DataField = "TEMP_RESID2"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "关联表资源名称"
        col.DataField = "TEMP_RESNAME2"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        intWidth += 250

        col = New BoundColumn
        col.HeaderText = "关联编辑"
        col.DataField = "RT_EDIT_HOSTRES2"
        col.ItemStyle.Width = Unit.Pixel(120)
        DataGrid1.Columns.Add(col)
        intWidth += 120

        If CmsFunc.IsEnable("FUNC_WORKFLOW") = True Then
            col = New BoundColumn
            col.HeaderText = "流程文档表"
            col.DataField = "WORKFLOW_DOC"
            col.ItemStyle.Width = Unit.Pixel(120)
            DataGrid1.Columns.Add(col)
            intWidth += 120
        End If

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID As Long)
        Dim ds As DataSet = GetRelResDataSet(lngResID)
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub

    '------------------------------------------------------------------
    '获取所有关联表的列表
    '------------------------------------------------------------------
    Private Function GetRelResDataSet(ByVal lngResID As Long) As DataSet
        Dim alistRelRes As ArrayList = CmsTableRelation.GetSubRelatedResources(CmsPass, lngResID, False, True)
        Dim ds As New DataSet
        ds.Tables.Add(New DataTable)
        ds.Tables(0).Columns.Add("TEMP_RESID1")
        ds.Tables(0).Columns.Add("TEMP_RESNAME1")
        ds.Tables(0).Columns.Add("TEMP_RESID2")
        ds.Tables(0).Columns.Add("TEMP_RESNAME2")
        ds.Tables(0).Columns.Add("WORKFLOW_DOC")
        ds.Tables(0).Columns.Add("RT_EDIT_HOSTRES2")

        Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)
        Dim datResTemp As DataResource
        For Each datResTemp In alistRelRes
            Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew()
            drv.BeginEdit()
            drv("TEMP_RESID1") = lngResID
            drv("TEMP_RESNAME1") = datRes.ResName
                drv("TEMP_RESID2") = datResTemp.ResID
            drv("TEMP_RESNAME2") = datResTemp.ResName
            If datRes.WorkFlowSubResID <> 0 And datRes.WorkFlowSubResID = datResTemp.ResID Then
                drv("WORKFLOW_DOC") = "是" '是工作流文档附件
            Else
                drv("WORKFLOW_DOC") = ""
            End If

            '查看是否Enable关联编辑
            If CmsTableRelation.IsRelationEditEnabled(CmsPass, lngResID, datResTemp.ResID) = True Then
                drv("RT_EDIT_HOSTRES2") = "是"
            Else
                drv("RT_EDIT_HOSTRES2") = ""
            End If

            drv.EndEdit()
        Next
        Return ds
    End Function
End Class

End Namespace
