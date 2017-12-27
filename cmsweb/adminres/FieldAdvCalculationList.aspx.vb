Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculationList
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

        GetSelectedFormulaResID()
        GetColName()
        GetSelectedFormulaType()
        GetSelectedFormulaAiid()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        '删除公式前提醒
        btnDelFormula.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除计算公式吗？');")

        btnAddVerifyFormula.Visible = CmsFunc.IsEnable("FUNC_FORMULA_VERIFY")
        btnFmlOption.Visible = CmsFunc.IsEnable("FUNC_FORMULA_OPTIONS")
        btnSchedule.Visible = CmsFunc.IsEnable("FUNC_FORMULA_SCHEDULE")

        If VLng("PAGE_RESID") = 0 Then
            '显示本系统所有计算公式时以下功能不可用
            btnAddVerifyFormula.Enabled = False
        End If

        If RStr("isrightres") = "1" Then
            '显示表间计算公式时以下功能不可用
            lbtnMoveup.Enabled = False
            lbtnMoveToFirst.Enabled = False
            lbtnMovedown.Enabled = False
            lbtnMoveToLast.Enabled = False
            btnAddVerifyFormula.Enabled = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind() '绑定数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()

    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = GetFormulaDataset()
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("获取自定义表字段显示信息并显示时出错！", ex)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

        WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
        CreateDataGridColumn()
    End Sub

    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        GridDataBind()
    End Sub

    Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Try
            Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
            Dim hashColKeyValue As New Hashtable '存放需要增加/修改的行信息
            Dim strColNameField As String = ""
            Dim i As Integer
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim ctl As System.Web.UI.Control
                Try
                    ctl = e.Item.Cells(i).Controls(0) '隐藏列会在此行产生Exception
                Catch ex As Exception
                    ctl = Nothing
                End Try
                If Not (ctl Is Nothing) Then
                    Dim strColName As String = ""
                    If HashField.ContainsKey(hashColumnNames, DataGrid1.Columns(i).HeaderText) Then
                        strColName = CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText))
                        strColName = StringDeal.Trim(strColName, "", "22")
                    End If
                    If strColName <> "" Then
                        If TypeOf ctl Is TextBox Then
                            Dim ctlCell As TextBox = CType(ctl, TextBox)
                            hashColKeyValue.Add(strColName, ctlCell.Text)
                        ElseIf TypeOf ctl Is DropDownList Then
                            Dim ctlCell As DropDownList = CType(ctl, DropDownList)
                            hashColKeyValue.Add(strColName, ctlCell.SelectedItem.Value)
                        ElseIf TypeOf ctl Is CheckBox Then
                            Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                            If ctlCell.Checked = True Then
                                hashColKeyValue.Add(strColName, 1)
                            Else
                                hashColKeyValue.Add(strColName, 0)
                            End If
                        End If
                    End If
                End If
            Next i

            '开始修改计算公式
            Dim lngAiid As Long = CLng(e.Item.Cells(0).Text) '此Cell不显示，作为判断新增还是修改记录
            CTableColCalculation.EditFormula(CmsPass, lngAiid, hashColKeyValue)
        Catch ex As Exception
            SLog.Err("更新字段属性异常失败", ex)
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                Dim strColName As String = row.Cells(2).Text.Trim() '第2列必须是字段内部名称
                row.Attributes.Add("fmlaiid", row.Cells(0).Text.Trim())
                row.Attributes.Add("fmltype", row.Cells(1).Text.Trim())
                row.Attributes.Add("fmlcolname", strColName)
                row.Attributes.Add("fmlresid", row.Cells(3).Text.Trim())
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")

                Dim strColNameClicked As String = GetColName()
                If strColNameClicked <> "" And strColNameClicked = strColName Then
                    row.Attributes.Add("bgColor", "#C4D9F9") '用户点击了某条记录，修改被点击记录的背景颜色
                End If
            Next
        End If
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '用于保存字段名称和显示名称对，以便在其它操作时使用
        Dim hashColumnNames As New Hashtable

        DataGrid1.AutoGenerateColumns = False

        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "CDJ_AIID"
        col.DataField = "CDJ_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        col = New BoundColumn
        col.HeaderText = "CDJ_TYPE"
        col.DataField = "CDJ_TYPE"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        col = New BoundColumn
        col.HeaderText = "CDJ_COLNAME"
        col.DataField = "CDJ_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        col = New BoundColumn
        col.HeaderText = "资源ID"
        col.DataField = "CDJ_RESID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "编辑"
        colEdit.EditText = "编辑"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "取消"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(65)
        DataGrid1.Columns.Add(colEdit)
        intWidth += 65
        colEdit = Nothing

        col = New BoundColumn
        col.HeaderText = "资源名称"
        col.DataField = "CDJ_RESID22"
        col.ItemStyle.Width = Unit.Pixel(110)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 110

        col = New BoundColumn
        col.HeaderText = "字段名称"
        col.DataField = "CDJ_COLNAME22"
        col.ItemStyle.Width = Unit.Pixel(140)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 140

        col = New BoundColumn
        col.HeaderText = "公式类型"
        col.DataField = "CDJ_TYPE22"
        col.ItemStyle.Width = Unit.Pixel(70)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "计算公式外部表达式"
        col.DataField = "CDJ_FORMULA_DESC"
        col.ItemStyle.Width = Unit.Pixel(800)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("计算公式外部表达式", "CDJ_FORMULA_DESC")
        intWidth += 800

        'If CmsConfig.DebugingMode() Then
        'End If
        '内部公式仅在Debug模式下显示
        col = New BoundColumn
        col.HeaderText = "计算公式内部表达式"
        col.DataField = "CDJ_FMLRIGHT_EXPR"
        col.ItemStyle.Width = Unit.Pixel(1500)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("计算公式内部表达式", "CDJ_FMLRIGHT_EXPR")
        intWidth += 1500

        col = New BoundColumn
        col.HeaderText = "校验提示信息"
        col.DataField = "CDJ_VERIFY_TIP"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("校验提示信息", "CDJ_VERIFY_TIP")
        intWidth += 250

        'col = New BoundColumn
        'col.HeaderText = "计算执行时间"
        'col.DataField = "CDJ_CALTIME22"
        'col.ItemStyle.Width = Unit.Pixel(100)
        'col.ReadOnly = True
        'DataGrid1.Columns.Add(col)
        'intWidth += 100

        col = New BoundColumn
        col.HeaderText = "含四则运算"
        col.DataField = "CDJ_NO_ARITHMETIC22"
        col.ItemStyle.Width = Unit.Pixel(90)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 90

        col = New BoundColumn
        col.HeaderText = "添加记录时运行"
        col.DataField = "CDJ_CALOCCASION_ADD"
        col.ItemStyle.Width = Unit.Pixel(100)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "修改记录时运行"
        col.DataField = "CDJ_CALOCCASION_EDIT"
        col.ItemStyle.Width = Unit.Pixel(100)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "删除记录时运行"
        col.DataField = "CDJ_CALOCCASION_DEL"
        col.ItemStyle.Width = Unit.Pixel(100)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 100

        DataGrid1.Width = Unit.Pixel(intWidth)

        '用于保存字段名称和显示名称对，以便在其它操作时使用
        ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If

        'CTableColCalculation.MoveUp(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveUp(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If

        'CTableColCalculation.MoveToFirst(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveToFirst(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If

        'CTableColCalculation.MoveDown(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveDown(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If

        'CTableColCalculation.MoveToLast(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveToLast(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '绑定数据
    End Sub

    '------------------------------------------------------------------
    '获取字段定义信息的数据集DataSet，如果指定资源没有独立的字段定义表则返回Nothing
    '------------------------------------------------------------------
    Private Function GetFormulaDataset() As DataSet
        Dim ds As DataSet = Nothing
        If RStr("isrightres") = "1" Then
            ds = CTableColCalculation.GetAllFormulaDatasetByFmlRightResID(CmsPass, VLng("PAGE_RESID"))
        Else
            ds = CTableColCalculation.GetAllFormulaDatasetByFmlLeftResID(CmsPass, VLng("PAGE_RESID"))
        End If

        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        ds.Tables(0).Columns.Add("CDJ_RESID22")
        ds.Tables(0).Columns.Add("CDJ_COLNAME22")
        ds.Tables(0).Columns.Add("CDJ_TYPE22")
        'ds.Tables(0).Columns.Add("CDJ_FORMULA_DESC22")
        'ds.Tables(0).Columns.Add("CDJ_CALTIME22")
        ds.Tables(0).Columns.Add("CDJ_NO_ARITHMETIC22")
        ds.Tables(0).Columns.Add("CDJ_CALOCCASION_ADD")
        ds.Tables(0).Columns.Add("CDJ_CALOCCASION_EDIT")
        ds.Tables(0).Columns.Add("CDJ_CALOCCASION_DEL")
        For Each drv In dv
            Dim lngFmlResID As Long = DbField.GetLng(drv, "CDJ_RESID")
            drv("CDJ_RESID22") = CmsPass.GetDataRes(lngFmlResID).ResName

            Dim intType As Integer = DbField.GetInt(drv, "CDJ_TYPE")
            If intType = FormulaType.IsCalculation Then
                drv("CDJ_COLNAME22") = CTableStructure.GetColDispName(CmsPass, lngFmlResID, DbField.GetStr(drv, "CDJ_COLNAME"))
                If DbField.GetStr(drv, "CDJ_SCHEDULE") <> "" Then '是定时计算
                    drv("CDJ_TYPE22") = "定时计算"
                Else '是计算公式
                    Dim strTemp As String = DbField.GetStr(drv, "CDJ_FORMULA_DESC")
                    If strTemp.IndexOf("::") > 0 Then
                        drv("CDJ_TYPE22") = "表间计算" '公式描述中含有::的即是表间计算，因为这是资源和字段名称的分隔符
                    Else
                        drv("CDJ_TYPE22") = "表内计算"
                    End If
                End If

                'Dim strDesc As String = DbField.GetStr(drv, "CDJ_FORMULA_DESC")
                'drv("CDJ_FORMULA_DESC22") = strDesc.Substring(strDesc.IndexOf("=") + 1)
            ElseIf intType = FormulaType.IsVerify Then
                drv("CDJ_COLNAME22") = DbField.GetStr(drv, "CDJ_COLNAME")
                drv("CDJ_TYPE22") = "校验公式"
                'ElseIf intType = FormulaType.IsSchedule Then
                '    drv("CDJ_COLNAME22") = CTableStructure.GetColDispName(CmsPass, lngFmlResID, DbField.GetStr(drv, "CDJ_COLNAME"))
                '    drv("CDJ_TYPE22") = "定时计算"

                'drv("CDJ_FORMULA_DESC22") = DbField.GetStr(drv, "CDJ_FORMULA_DESC")
            End If

            'If DbField.GetInt(drv, "CDJ_CALTIME") = 1 Then
            '    drv("CDJ_CALTIME22") = "保存后计算"
            'Else
            '    drv("CDJ_CALTIME22") = "保存前计算"
            'End If

            If DbField.GetInt(drv, "CDJ_NO_ARITHMETIC") = 0 Then
                drv("CDJ_NO_ARITHMETIC22") = "是"
            Else
                drv("CDJ_NO_ARITHMETIC22") = "否"
            End If

            Dim intTemp As Integer = DbField.GetInt(drv, "CDJ_CALOCCASION")
            If (intTemp And FormulaOccasion.RecordAdd) = FormulaOccasion.RecordAdd Then
                drv("CDJ_CALOCCASION_ADD") = "是"
            Else
                drv("CDJ_CALOCCASION_ADD") = "否"
            End If
            If (intTemp And FormulaOccasion.RecordEdit) = FormulaOccasion.RecordEdit Then
                drv("CDJ_CALOCCASION_EDIT") = "是"
            Else
                drv("CDJ_CALOCCASION_EDIT") = "否"
            End If
            If (intTemp And FormulaOccasion.RecordDel) = FormulaOccasion.RecordDel Then
                drv("CDJ_CALOCCASION_DEL") = "是"
            Else
                drv("CDJ_CALOCCASION_DEL") = "否"
            End If
        Next
        Return ds
    End Function

    Private Sub btnSetFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFormula.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If

        Dim lngResID As Long = GetSelectedFormulaResID()
        Session("CMSBP_FieldAdvCalculation") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlfmlcolname=" & strColName & "&urlfmlresid=" & GetSelectedFormulaResID() & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & lngResID & "&colname=" & strColName & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid(), False)
    End Sub

    Private Sub btnSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedule.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If
        If GetSelectedFormulaType() = FormulaType.IsVerify Then
            PromptMsg("校验公式不支持定时计算！")
            Return
        End If

        Dim lngResID As Long = GetSelectedFormulaResID()
        Session("CMSBP_FieldAdvCalculationSchedule") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlfmlcolname=" & strColName & "&urlfmlresid=" & GetSelectedFormulaResID() & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculationSchedule.aspx?mnuresid=" & lngResID & "&colname=" & strColName & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid(), False)
    End Sub

    Private Sub btnDelFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFormula.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If
        Dim lngResID As Long = GetSelectedFormulaResID()

        CTableColCalculation.DelFormula(CmsPass, lngResID, strColName)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnFmlOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFmlOption.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("请先选择需要操作的计算公式")
            Return
        End If

        Dim lngResID As Long = GetSelectedFormulaResID()
        Session("CMSBP_FieldAdvCalculationSettings") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlfmlcolname=" & strColName & "&urlfmlresid=" & GetSelectedFormulaResID() & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculationSettings.aspx?mnuresid=" & lngResID & "&colname=" & strColName & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid(), False)
    End Sub

    Private Sub btnAddVerifyFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddVerifyFormula.Click
        Session("CMSBP_FieldAdvCalculation") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlcolname=" & GetColName() & "&urlresid=" & GetSelectedFormulaResID()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & VStr("PAGE_RESID") & "&urlfmltype=" & FormulaType.IsVerify, False)
    End Sub

    '-------------------------------------------------------------
    '获取当前选中的字段名称
    '-------------------------------------------------------------
    Private Function GetSelectedFormulaResID() As Long
        Dim lngResID As Long = RLng("fmlresid")
        If lngResID <> 0 Then
            ViewState("PAGE_FMLRESID") = lngResID
        Else
            If VLng("PAGE_FMLRESID") = 0 Then
                ViewState("PAGE_FMLRESID") = RLng("urlfmlresid")
            End If
        End If

        Return VLng("PAGE_FMLRESID")
    End Function

    '-------------------------------------------------------------
    '获取当前选中的字段名称
    '-------------------------------------------------------------
    Private Function GetColName() As String
        Dim strRecID As String = RStr("fmlcolname")
        If strRecID <> "" Then
            ViewState("PAGE_FMLCOLNAME") = strRecID
        Else
            If VStr("PAGE_FMLCOLNAME") = "" Then
                ViewState("PAGE_FMLCOLNAME") = RStr("urlfmlcolname")
            End If
        End If
        Return VStr("PAGE_FMLCOLNAME")
    End Function

    '-------------------------------------------------------------
    '获取当前选中的公式类型
    '-------------------------------------------------------------
    Private Function GetSelectedFormulaType() As Long
        Dim strType As String = RStr("fmltype")
        If IsNumeric(strType) Then
            ViewState("PAGE_FMLTYPE") = strType
        Else
            If VStr("PAGE_FMLTYPE") = "" Then
                ViewState("PAGE_FMLTYPE") = RLng("urlfmltype")
            End If
        End If

        Return VLng("PAGE_FMLTYPE")
    End Function

    '-------------------------------------------------------------
    '获取当前选中的公式的AIID
    '-------------------------------------------------------------
    Private Function GetSelectedFormulaAiid() As Long
        Dim lngAiid As Long = RLng("fmlaiid")
        If lngAiid <> 0 Then
            ViewState("PAGE_FMLAIID") = lngAiid
        Else
            If VLng("PAGE_FMLAIID") = 0 Then
                ViewState("PAGE_FMLAIID") = RLng("urlfmlaiid")
            End If
        End If

        Return VLng("PAGE_FMLAIID")
    End Function
End Class

End Namespace
