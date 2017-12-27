Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


    Partial Class MTableSearchColDef_1
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

            If VLng("PAGE_MTSHOSTID") = 0 Then
                ViewState("PAGE_MTSHOSTID") = RLng("mtshostid")
            Else

            End If
            If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.Unknown Then
                ViewState("PAGE_MTSLIST_TYPE") = RInt("mtstype")
            End If

            If VStr("PAGE_EMPID") = "" Then '用于：行记录权限
                ViewState("PAGE_EMPID") = RStr("mnuempid")
                If VStr("PAGE_EMPID") = "" Then
                    ViewState("PAGE_EMPID") = CmsPass.Employee.ID
                End If
            End If

            If VStr("PAGE_COLNAME") = "" Then '用于：高级字典行过滤设置
                ViewState("PAGE_COLNAME") = RStr("mnucolname")
            End If
            If VLng("PAGE_ADVDICT_HOSTRES") = 0 Then '用于：高级字典行过滤设置
                ViewState("PAGE_ADVDICT_HOSTRES") = RStr("advdict_hostresid")
            End If

            If VStr("PAGE_COLURLRECID") = "" Then
                ViewState("PAGE_COLURLRECID") = RStr("colurlid")
            End If
        End Sub

        Protected Overrides Sub CmsPageInitialize()
            ddlResList.AutoPostBack = True
            btnDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前设置吗！');")

            Select Case VInt("PAGE_MTSLIST_TYPE")
                Case MTSearchType.ColumnUrlFilter
                    lblTitle.Text = "字段链接条件设置"
                    btnAddEmail.Visible = False
                    btnAddMobile.Visible = False

                    EnableSaveTitle(False)
                    lbtnSelHostRes.Enabled = False
                    btnStartSearch.Visible = False
                    btnAddShow.Visible = False
                    btnAddOrderASC.Visible = False
                    btnAddOrderDesc.Visible = False


                    Dim lngMTSHostID As Long = CmsDbBase.GetFieldLng(CmsPass, CmsTables.MTableHost, "MTS_ID", "MTS_TYPE=" & MTSearchType.ColumnUrlFilter & " AND MTS_RESID=" + VLng("PAGE_RESID").ToString + " and MTS_COLURLID=" & VLng("PAGE_COLURLRECID") & " AND MTS_EMPID='" & VStr("PAGE_EMPID") & "'")
                    If lngMTSHostID <> 0 Then
                        ViewState("PAGE_MTSHOSTID") = lngMTSHostID
                    End If

            End Select
        End Sub

        Protected Overrides Sub CmsPageDealFirstRequest()
            Dim hashFieldVal As Hashtable = Nothing
            If VLng("PAGE_MTSHOSTID") <> 0 Then '有已设计的设置
                hashFieldVal = CmsDbBase.GetRecordByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & VLng("PAGE_MTSHOSTID"))
                ViewState("PAGE_RESID") = HashField.GetLng(hashFieldVal, "MTS_RESID")
            Else
                '检查是否刚从选择资源窗体回来
                If RLng("selresid") <> 0 Then
                    ViewState("PAGE_RESID") = RLng("selresid")
                End If
            End If

            txtMTSearchTitle.Text = HashField.GetStr(hashFieldVal, "MTS_TITLE")
            ShowAfterHostResSelected(VLng("PAGE_RESID")) '在主资源选中后显示相关的信息

            '显示默认值列表
            dllDefaultVal.Items.Clear()
            dllDefaultVal.Items.Add(New ListItem("", ""))
            dllDefaultVal.Items.Add(New ListItem("当前用户帐号", "[CUR_USERID]"))
            dllDefaultVal.Items.Add(New ListItem("当前用户名称", "[CUR_USERNAME]"))
            dllDefaultVal.Items.Add(New ListItem("当前用户部门", "[CUR_USERDEPNAME]"))
            dllDefaultVal.Items.Add(New ListItem("上年年份", "[PREV_YEAR]"))
            dllDefaultVal.Items.Add(New ListItem("当前年份", "[CUR_YEAR]"))
            dllDefaultVal.Items.Add(New ListItem("下年年份", "[NEXT_YEAR]"))
            dllDefaultVal.Items.Add(New ListItem("上月月份", "[PREV_MONTH]"))
            dllDefaultVal.Items.Add(New ListItem("当前月份", "[CUR_MONTH]"))
            dllDefaultVal.Items.Add(New ListItem("下月月份", "[NEXT_MONTH]"))
            dllDefaultVal.Items.Add(New ListItem("当前日期", "[CUR_DATE]"))
            dllDefaultVal.Items.Add(New ListItem("当前月日", "[CUR_MONTHDATE]"))
            dllDefaultVal.Items.Add(New ListItem("当前日期(生日比较)", "[BIRTHDAY]"))
            dllDefaultVal.Items.Add(New ListItem("明天日期", "[TOMORROW]"))
            dllDefaultVal.Items.Add(New ListItem("当前日期几", "[CUR_DAYOFWEEK]"))
            dllDefaultVal.Items.Add(New ListItem("当前小时", "[CUR_HOUR]"))
            dllDefaultVal.Items.Add(New ListItem("上月第一天", "[PREV_MONTH_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("本月第一天", "[CUR_MONTH_FSTDAY]"))
            'dllDefaultVal.Items.Add(New ListItem("当月最后一天", "[CUR_MONTH_LSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("下月第一天", "[NEXT_MONTH_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("下下月第一天", "[NNEXT_MONTH_FSTDAY]"))

            dllDefaultVal.Items.Add(New ListItem("上周星期一", "[PREVWK_MON]"))
            dllDefaultVal.Items.Add(New ListItem("上周星期六", "[PREVWK_SAT]"))
            dllDefaultVal.Items.Add(New ListItem("本周星期一", "[THISWK_MON]"))
            dllDefaultVal.Items.Add(New ListItem("上周星期六", "[THISWK_SAT]"))
            dllDefaultVal.Items.Add(New ListItem("下周星期一", "[NEXTWK_MON]"))
            dllDefaultVal.Items.Add(New ListItem("下周星期六", "[NEXTWK_SAT]"))
            dllDefaultVal.Items.Add(New ListItem("下下周星期一", "[NNEXTWK_MON]"))

            dllDefaultVal.Items.Add(New ListItem("上季第一天", "[PREV_QTR_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("本季第一天", "[CUR_QTR_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("下季第一天", "[NEXT_QTR_FSTDAY]"))

            GridDataBind() '绑定数据
        End Sub

        Protected Overrides Sub CmsPageDealPostBack()
        End Sub

        Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
            If CmsPass.EmpIsSysAdmin = False Then  'And CmsPass.EmpIsDepAdmin = False Then
                If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableView Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableViewForTemp Then
                    '非系统管理员则需要判断权限
                    If MultiTableSearch.HasRightsOnMTSResources(CmsPass, VLng("PAGE_MTSHOSTID")) = False Then
                        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "checkrights_fail=yes"), False)
                        Return
                    End If
                End If
            End If

            If VLng("PAGE_MTSHOSTID") <> 0 Or VLng("PAGE_RESID") <> 0 Then '是编辑状态
                lbtnSelHostRes.Enabled = False
            End If
        End Sub

        Private Sub btnAddShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddShow.Click
            AddColumnForHostRes(MTSearchColumnType.Show)
            GridDataBind() '绑定数据
        End Sub

        Private Sub btnAddCond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCond.Click
            AddColumnForHostRes(MTSearchColumnType.Condition)

            '组装所有条件并保存
            'Dim strWhere As String = MultiTableSearchColumn.GetWhereOfColCondition(CmsPass, VLng("PAGE_MTSHOSTID"))
            'Dim hashFieldVal As New Hashtable
            'hashFieldVal.Add("MTS_WHERE", strWhere)
            'CmsDbBase.EditRecord(CmsPass, CmsTables.MTableHost, VLng("PAGE_MTSHOSTID"), hashFieldVal)

            GridDataBind() '绑定数据
        End Sub

        Private Sub btnAddOrderASC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrderASC.Click
            AddColumnForHostRes(MTSearchColumnType.Order, True)
            GridDataBind() '绑定数据
        End Sub

        Private Sub btnAddOrderDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrderDesc.Click
            AddColumnForHostRes(MTSearchColumnType.Order, False)
            GridDataBind() '绑定数据
        End Sub

        Private Sub btnAddEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEmail.Click
            AddColumnForHostRes(MTSearchColumnType.Email)
            GridDataBind() '绑定数据
        End Sub

        Private Sub btnAddMobile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMobile.Click
            AddColumnForHostRes(MTSearchColumnType.MobilePhone)
            GridDataBind() '绑定数据
        End Sub

        Private Sub ddlResList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlResList.SelectedIndexChanged
            '显示关联子资源的字段信息
            Dim strCurResID As String = ddlResList.SelectedValue
            If IsNumeric(strCurResID) = False Then Return
            ViewState("PAGE_COLTYPE_" & CLng(strCurResID)) = WebUtilities.LoadResColumnsInDdlist(CmsPass, CLng(strCurResID), ddlHostResCol, , , False, , , )
            WebUtilities.LoadResColumnsInDdlist(CmsPass, CLng(strCurResID), ddlValCol, , , False, , , )
            GridDataBind() '绑定数据
        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            If txtMTSearchTitle.Text.Trim() = "" Then
                PromptMsg("请输入有效的标题！")
                Return
            End If
            If VLng("PAGE_MTSHOSTID") = 0 Then '还没有添加过多表统计和行过滤设置字段定义
                PromptMsg("请先添加有效的显示字段。")
                Return
            Else
                Dim hashFieldVal As New Hashtable
                hashFieldVal.Add("MTS_TITLE", txtMTSearchTitle.Text.Trim())
                hashFieldVal.Add("MTS_TYPE", VInt("PAGE_MTSLIST_TYPE"))
                hashFieldVal.Add("MTS_EMPID", VStr("PAGE_EMPID"))
                'Dim strWhere As String = MultiTableSearchColumn.GetWhereOfColCondition(CmsPass, VLng("PAGE_MTSHOSTID"))
                'hashFieldVal.Add("MTS_WHERE", strWhere)
                'hashFieldVal.Add("MTS_TABLELOGIC", IIf(rdoTableAnd.Checked = True, "AND", "OR"))
                hashFieldVal.Add("MTS_TABLELOGIC", "AND")
                CmsDbBase.EditRecordByWhere(CmsPass, CmsTables.MTableHost, hashFieldVal, "MTS_ID=" & VLng("PAGE_MTSHOSTID"), "MTS_EDTID", "MTS_EDTTIME")
                If Not Request("mtspid") Is Nothing Then
                    SDbStatement.Execute("update " & CmsTables.MTableHost & " set MTS_PID=" & RLng("mtspid") & " where MTS_ID=" & VLng("PAGE_MTSHOSTID"))
                End If
            End If
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Try
                Dim lngRecID As Long = VLng("PAGE_MTSHOSTID")
                MultiTableSearch.DelMTSRecordByID(CmsPass, lngRecID)
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                'ViewState("PAGE_MTSHOSTID") = 0
                'GridDataBind() '绑定数据
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

        Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        End Sub

        Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
            Dim lngRecID As Long = Me.GetRecIDOfGrid()
            If lngRecID = 0 Then
                PromptMsg("请先选择有效的字段设置记录！")
                Return
            End If
            MultiTableSearchColumn.MoveUp(CmsPass, CmsTables.MTableColDef, VLng("PAGE_MTSHOSTID"), lngRecID)
            GridDataBind() '绑定数据
        End Sub

        Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
            Dim lngRecID As Long = Me.GetRecIDOfGrid()
            If lngRecID = 0 Then
                PromptMsg("请先选择有效的字段设置记录！")
                Return
            End If
            MultiTableSearchColumn.MoveDown(CmsPass, CmsTables.MTableColDef, VLng("PAGE_MTSHOSTID"), lngRecID)
            GridDataBind() '绑定数据
        End Sub

        Private Sub lbtnSelHostRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelHostRes.Click
            Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/MTableSearchColDef.aspx?mtshostid=" & VLng("PAGE_MTSHOSTID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID")
            Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx", False)
        End Sub

        Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
            Try
                If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                    Response.Redirect("/cmsweb/Logout.aspx", True)
                    Return
                End If
                CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

                WebUtilities.InitialDataGrid(DataGrid1) '初始化DataGrid属性
                CreateDataGridColumn()
            Catch ex As Exception
                SLog.Err("多表查询列表显示异常出错！", ex)
            End Try
        End Sub

        Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            GridDataBind()
        End Sub

        Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
            DataGrid1.EditItemIndex = -1
            GridDataBind()
        End Sub

        Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
            Try
                Dim lngRecID As Long = CLng(e.Item.Cells(0).Text)
                CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.MTableColDef, "MTSCOL_ID=" & lngRecID)
                CmsDbBase.ClearTableCache(CmsTables.MTableColDef)
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
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
                        Dim strColName As String
                        If HashField.ContainsKey(hashColumnNames, DataGrid1.Columns(i).HeaderText) Then
                            strColName = CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText))
                            If strColName.EndsWith("22") Then
                                strColName = strColName.Substring(0, strColName.Length - 2)
                            End If
                        End If

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
                Next i

                '----------------------------------------------------------------
                '保存行信息至数据库
                Dim lngRecID As Long = CLng(e.Item.Cells(0).Text)  '此Cell不显示，作为判断新增还是修改记录
                CmsDbBase.EditRecordByWhere(CmsPass, CmsTables.MTableColDef, hashColKeyValue, "MTSCOL_ID=" & lngRecID, "MTSCOL_EDTID", "MTSCOL_EDTTIME")
                '----------------------------------------------------------------
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            DataGrid1.EditItemIndex = -1
            GridDataBind()
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
            Try
                '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中部分代码联合使用
                If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                    Dim lngRecIDClicked As Long = GetRecIDOfGrid()
                    Dim row As DataGridItem
                    For Each row In DataGrid1.Items
                        '设置客户端的记录ID和Javascript方法，第三列是关联表的资源ID
                        Dim strRecID As String = row.Cells(0).Text.Trim()
                        If IsNumeric(strRecID) Then
                            row.Attributes.Add("RECID", strRecID) '在客户端保存记录ID
                            row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '在客户端生成：点击记录的响应方法OnClick()

                            If lngRecIDClicked > 0 And lngRecIDClicked = CLng(strRecID) Then
                                row.Attributes.Add("bgColor", "#C4D9F9") '修改被点击记录的背景颜色
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                SLog.Err("多表查询列表显示异常出错！", ex)
            End Try
        End Sub

        Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            Try
                If e.Item.ItemIndex <> -1 Then
                    If e.Item.ItemType = ListItemType.EditItem Then
                        Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
                        Dim i As Integer
                        For i = 0 To DataGrid1.Columns.Count - 1
                            Dim ctl As System.Web.UI.Control
                            Try
                                ctl = e.Item.Cells(i).Controls(0) '隐藏列会在此行产生Exception
                            Catch ex As Exception
                            End Try
                            If Not (ctl Is Nothing) Then
                                If TypeOf ctl Is TextBox Then
                                    Dim ctlCell As TextBox = CType(ctl, TextBox)
                                    ctlCell.Width = Unit.Percentage(100)
                                ElseIf TypeOf ctl Is DropDownList Then
                                    Dim ctlCell As DropDownList = CType(ctl, DropDownList)
                                    ctlCell.Width = Unit.Percentage(100)

                                    '必须先取消DropDownList的选中，否则在未更新前点击其它行的“编辑”时会出错。
                                    ctlCell.SelectedIndex = -1

                                    '---------------------------------------------------------------
                                    '获取DropDownList的Cell中原来的值
                                    Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                                    Dim strOldFieldValue As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))

                                    '为DropDownList赋予原来的值
                                    Dim item As ListItem = ctlCell.Items.FindByText(strOldFieldValue)
                                    If Not item Is Nothing Then item.Selected = True
                                    '---------------------------------------------------------------
                                ElseIf TypeOf ctl Is CheckBox Then
                                    Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                                    Dim strOldFieldValue As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                                    Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                                    If strOldFieldValue = "是" Then
                                        ctlCell.Checked = True
                                    ElseIf strOldFieldValue = "否" Then
                                        ctlCell.Checked = False
                                    End If
                                    ctlCell.Width = Unit.Percentage(100)
                                End If
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                SLog.Err("多表查询列表显示异常出错！", ex)
            End Try
        End Sub

        '------------------------------------------------------------------
        '创建修改表结构的DataGrid的列
        '------------------------------------------------------------------
        Private Sub CreateDataGridColumn()
            '用于保存字段名称和显示名称对，以便在其它操作时使用
            Dim hashColumnNames As New Hashtable

            DataGrid1.AutoGenerateColumns = False
            Dim intWidth As Integer = 0

            '第1列必须是记录ID
            Dim col As BoundColumn = New BoundColumn
            col.HeaderText = "MTSCOL_ID"
            col.DataField = "MTSCOL_ID"
            col.ItemStyle.Width = Unit.Pixel(1)
            col.Visible = False
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("MTSCOL_ID", "MTSCOL_ID")

            Dim colEdit As EditCommandColumn = New EditCommandColumn
            colEdit.HeaderText = "编辑"
            colEdit.EditText = "编辑"
            colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
            colEdit.CancelText = "取消"
            colEdit.ButtonType = ButtonColumnType.LinkButton
            colEdit.ItemStyle.Width = Unit.Pixel(65)
            DataGrid1.Columns.Add(colEdit)
            intWidth += 65

            col = New BoundColumn
            col.HeaderText = "类型"
            col.DataField = "MTSCOL_TYPE2"
            col.ItemStyle.Width = Unit.Pixel(70)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("类型", "MTSCOL_TYPE2")
            intWidth += 70

            col = New BoundColumn
            col.HeaderText = "资源名称"
            col.DataField = "MTSCOL_RESID22"
            col.ItemStyle.Width = Unit.Pixel(100)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("资源名称", "MTSCOL_RESID22")
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "字段名称"
            col.DataField = "MTSCOL_COLDISPNAME"
            col.ItemStyle.Width = Unit.Pixel(100)
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("字段名称", "MTSCOL_COLDISPNAME")
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "字段条件"
            col.DataField = "MTSCOL_COLCOND"
            col.ItemStyle.Width = Unit.Pixel(65)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("字段条件", "MTSCOL_COLCOND")
            intWidth += 65

            col = New BoundColumn
            col.HeaderText = "字段条件值"
            col.DataField = "MTSCOL_COLVALUE"
            col.ItemStyle.Width = Unit.Pixel(90)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("字段条件值", "MTSCOL_COLVALUE")
            intWidth += 90

            col = New BoundColumn
            col.HeaderText = "逻辑"
            col.DataField = "MTSCOL_LOGIC"
            col.ItemStyle.Width = Unit.Pixel(40)
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("逻辑", "MTSCOL_LOGIC")
            intWidth += 40

            If VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.GeneralRowWhere And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.PersonalRowWhere And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.RowRights And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.AdvDictFilter And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.BatchSend Then
                col = New BoundColumn
                col.HeaderText = "显示格式"
                col.DataField = "MTSCOL_SHOWFORMAT"
                col.ItemStyle.Width = Unit.Pixel(90)
                DataGrid1.Columns.Add(col)
                hashColumnNames.Add("显示格式", "MTSCOL_SHOWFORMAT")
                intWidth += 90

                col = New BoundColumn
                col.HeaderText = "显示宽度"
                col.DataField = "MTSCOL_SHOWWIDTH"
                col.ItemStyle.Width = Unit.Pixel(65)
                DataGrid1.Columns.Add(col)
                hashColumnNames.Add("显示宽度", "MTSCOL_SHOWWIDTH")
                intWidth += 65
            End If

            If VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.RowRights And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.AdvDictFilter Then
                col = New BoundColumn
                col.HeaderText = "排序"
                col.DataField = "MTSCOL_ORDERBY2"
                col.ItemStyle.Width = Unit.Pixel(40)
                col.ReadOnly = True
                DataGrid1.Columns.Add(col)
                hashColumnNames.Add("排序", "MTSCOL_ORDERBY2")
                intWidth += 40
            End If

            Dim colDel As ButtonColumn = New ButtonColumn
            colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
            colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '或者用图标，如："<img src='/cmsweb/images/common/delete.gif' border=0>"
            colDel.CommandName = "Delete"
            colDel.ButtonType = ButtonColumnType.LinkButton
            colDel.ItemStyle.Width = Unit.Pixel(40)
            colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            DataGrid1.Columns.Add(colDel)
            intWidth += 40

            DataGrid1.Width = Unit.Pixel(intWidth)

            '用于保存字段名称和显示名称对，以便在其它操作时使用
            ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
        End Sub

        '---------------------------------------------------------------
        '绑定数据
        '---------------------------------------------------------------
        Private Sub GridDataBind()
            Try
                Dim ds As DataSet = MultiTableSearchColumn.GetMTSearchColdefByDataSet(CmsPass, VLng("PAGE_MTSHOSTID"))
                DataGrid1.DataSource = ds.Tables(0).DefaultView
                DataGrid1.DataBind()
            Catch ex As Exception
                SLog.Err("绑定多表统计和行过滤设置信息时出错！", ex)
            End Try
        End Sub

        '------------------------------------------------------------------------------
        '为多表统计记录表添加主索引记录
        '------------------------------------------------------------------------------
        Private Function AddMTSearchRecordForTemp(ByVal lngCurResID As Long, ByVal intMTSType As Integer) As Long
            Dim hashFieldVal As New Hashtable
            hashFieldVal.Add("MTS_RESID", lngCurResID)
            hashFieldVal.Add("MTS_TITLE", txtMTSearchTitle.Text.Trim())
            If intMTSType <> MTSearchType.MultiTableView Then
                hashFieldVal.Add("MTS_TYPE", intMTSType)
            Else
                hashFieldVal.Add("MTS_TYPE", MTSearchType.MultiTableViewForTemp)
            End If
            hashFieldVal.Add("MTS_EMPID", VStr("PAGE_EMPID"))
            hashFieldVal.Add("MTS_COLNAME", VStr("PAGE_COLNAME"))
            'hashFieldVal.Add("MTS_TABLELOGIC", IIf(rdoTableAnd.Checked = True, "AND", "OR"))
            hashFieldVal.Add("MTS_TABLELOGIC", "AND")
            hashFieldVal.Add("MTS_DICTRESID", VLng("PAGE_ADVDICT_HOSTRES"))
            hashFieldVal.Add("MTS_COLURLID", VLng("PAGE_COLURLRECID"))
            CmsDbBase.AddOrEditRecordByWhere(CmsPass, CmsTables.MTableHost, hashFieldVal, "", "MTS_ID", "MTS_SHOWORDER", "MTS_EDTID", "MTS_EDTTIME")
            Return HashField.GetLng(hashFieldVal, "MTS_ID")
        End Function

        '------------------------------------------------------------------------------
        '添加主资源字段定义
        '------------------------------------------------------------------------------
        Private Sub AddColumnForHostRes(ByVal intMTSColType As MTSearchColumnType, Optional ByVal blnIsAscOrder As Boolean = True)
            Dim hashFieldVal As New Hashtable

            If VLng("PAGE_RESID") = 0 Then
                PromptMsg("请选择正确的主资源")
                Return
            End If
            If IsNumeric(ddlResList.SelectedValue) = False Then
                PromptMsg("请选择正确的资源")
                Return
            End If
            Dim lngCurResID As Long = CLng(ddlResList.SelectedValue)
            Dim strCurResTable As String = CmsPass.GetDataRes(lngCurResID).ResTable

            hashFieldVal.Add("MTSCOL_RESID", lngCurResID)

            Dim lngHostID As Long = VLng("PAGE_MTSHOSTID")
            Dim MTSCOL_EDTID As String = Request.QueryString("mnuempid")
            If lngHostID = 0 Then  '是新增
                '为多表统计记录表添加主索引记录
               lngHostID = AddMTSearchRecordForTemp(lngCurResID, VInt("PAGE_MTSLIST_TYPE"))

                ViewState("PAGE_MTSHOSTID") = lngHostID
            End If

            hashFieldVal.Add("MTSCOL_HOSTID", lngHostID)
            hashFieldVal.Add("MTSCOL_TYPE", intMTSColType)
            If (MTSCOL_EDTID.Trim() <> "") Then
                hashFieldVal.Add("MTSCOL_EDTID", MTSCOL_EDTID)
                hashFieldVal.Add("MTSCOL_EDTTIME", DateTime.Now)
            End If

            If ddlHostResCol.SelectedValue = "" Then
                PromptMsg("请选择正确的资源字段")
                Return
            End If
            hashFieldVal.Add("MTSCOL_COLNAME", ddlHostResCol.SelectedValue)

            hashFieldVal.Add("MTSCOL_COLDISPNAME", ddlHostResCol.SelectedItem.Text)

            If intMTSColType = MTSearchColumnType.Show Then
                'CmsPass.HostResData.ResTableType 未取到值??
                Dim dv As DataView = ResFactory.TableService("TWOD").GetTableColumns(CmsPass, lngCurResID)
                dv.RowFilter = "CD_COLNAME='" & ddlHostResCol.SelectedValue & "'"
                If dv.Count > 0 Then
                    hashFieldVal.Add("MTSCOL_SHOWFORMAT", DbField.GetStr(dv(0), "cs_format")) '初始添加显示格式
                Else
                    hashFieldVal.Add("MTSCOL_SHOWFORMAT", "") '初始添加时不设置显示格式
                End If

                hashFieldVal.Add("MTSCOL_SHOWWIDTH", "70") '初始添加时显示宽度为70
            ElseIf intMTSColType = MTSearchColumnType.Order Then
                hashFieldVal.Add("MTSCOL_COLVALUE", CLng(IIf(blnIsAscOrder = True, 0, 1)))
            ElseIf intMTSColType = MTSearchColumnType.Condition Then
                If ddlHostConditions.SelectedValue = "" Then
                    PromptMsg("请选择正确的主资源字段的判断条件")
                    Return
                End If
                hashFieldVal.Add("MTSCOL_COLCOND", ddlHostConditions.SelectedItem.Text)

                '获取条件值
                Dim strOneWhere As String = ""
                Dim intColValType As CondValType = CondValType.IsText
                If dllDefaultVal.SelectedValue <> "" Then
                    intColValType = CondValType.IsDefaultValue
                    Dim strValue As String = ""
                    If txtColValue.Text.Trim() <> "" Then
                        hashFieldVal.Add("MTSCOL_COLVALUE", dllDefaultVal.SelectedItem.Text & txtColValue.Text.Trim())
                        strValue = dllDefaultVal.SelectedValue & txtColValue.Text.Trim()
                    Else
                        hashFieldVal.Add("MTSCOL_COLVALUE", dllDefaultVal.SelectedItem.Text)
                        strValue = dllDefaultVal.SelectedValue
                    End If
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, strValue, CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, False)
                ElseIf txtColValue.Text.Trim() <> "" Then
                    intColValType = CondValType.IsText
                    hashFieldVal.Add("MTSCOL_COLVALUE", txtColValue.Text.Trim())
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, txtColValue.Text.Trim(), CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, False)
                ElseIf ddlValCol.SelectedValue <> "" Then
                    intColValType = CondValType.IsColumnName
                    hashFieldVal.Add("MTSCOL_COLVALUE", ddlValCol.SelectedItem.Text)
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, strCurResTable & "." & ddlValCol.SelectedValue, CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, True)
                Else '空值
                    intColValType = CondValType.IsText
                    hashFieldVal.Add("MTSCOL_COLVALUE", "")
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, "", CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, False)
                End If

                hashFieldVal.Add("MTSCOL_WHERE", strOneWhere)
                hashFieldVal.Add("MTSCOL_LOGIC", "AND") '默认为“与”关系
            ElseIf intMTSColType = MTSearchColumnType.Email Then
            ElseIf intMTSColType = MTSearchColumnType.MobilePhone Then
            End If

            CmsDbBase.AddOrEditRecordByWhere(CmsPass, CmsTables.MTableColDef, hashFieldVal, "", "MTSCOL_ID", "MTSCOL_SHOWORDER", "MTS_EDTID", "MTS_EDTTIME")
        End Sub

        '------------------------------------------------------------------------------
        '在主资源选中后显示相关的信息
        '------------------------------------------------------------------------------
        Private Sub ShowAfterHostResSelected(ByVal lngHostResID As Long)
            If lngHostResID <> 0 Then
                WebUtilities.LoadConditionsInDdlist(CmsPass, ddlHostConditions, True) '初始化DropDownList中查找条件项

                Dim blnShowSubResList As Boolean = True
                If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.GeneralRowWhere Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.PersonalRowWhere Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.RowRights Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.AdvDictFilter Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.ColumnUrlFilter Then
                    blnShowSubResList = False '从行过滤设置过来的不显示子资源列表
                End If
                WebUtilities.LoadSubResourcesInDdlist(CmsPass, lngHostResID, ddlResList, False, , True, blnShowSubResList)

                '显示主资源和所有子资源列表
                ViewState("PAGE_COLTYPE_" & lngHostResID) = WebUtilities.LoadResColumnsInDdlist(CmsPass, lngHostResID, ddlHostResCol, , , False, , , )
                WebUtilities.LoadResColumnsInDdlist(CmsPass, CLng(lngHostResID), ddlValCol, , , False, , , )
            End If
        End Sub

        '------------------------------------------------------------------------------
        '显示保存标题相关元素
        '------------------------------------------------------------------------------
        Private Sub EnableSaveTitle(ByVal blnEnable As Boolean)
            If blnEnable Then
                lblSearchTitle.Visible = True
                txtMTSearchTitle.Visible = True
                btnSave.Visible = True
            Else
                lblSearchTitle.Visible = False
                txtMTSearchTitle.Visible = False
                btnSave.Visible = False
            End If
        End Sub
    End Class

End Namespace
