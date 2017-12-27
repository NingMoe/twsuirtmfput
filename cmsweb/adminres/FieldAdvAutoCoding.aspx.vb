Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvAutoCoding
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents pnlAutoCoding As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelOneACoding As System.Web.UI.WebControls.Button
    Protected WithEvents btnEditOneACoding As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label

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

        If RStr("acodeaction") = "rowclick" Then
            ViewState("PAGE_RECID") = RLng("RECID")
        Else
            If VLng("PAGE_RECID") = 0 Then
                ViewState("PAGE_RECID") = RLng("RECID")
            End If
        End If
        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '默认不显示所有“修改”Button
        btnSNumEditConstant.Enabled = False
        btnSNumEditDate.Enabled = False
        btnSNumEditSNum.Enabled = False

        txtSNumDigitNum.Attributes.Add("style", "TEXT-ALIGN: right;")
        txtNumToSkip.Attributes.Add("style", "TEXT-ALIGN: right;")
        txtSNumValue.Attributes.Add("style", "TEXT-ALIGN: right;")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblFieldName.Text = VStr("PAGE_COLDISPNAME")

        ShowDataGridData() '完整Load的列表和数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        If RStr("acodeaction") = "rowclick" Then
            Try
                If VLng("PAGE_RECID") > 0 Then LoadValueForEdit(VLng("PAGE_RECID")) '编辑模式下Load原先的值

                ShowDataGridData() '完整Load的列表和数据
            Catch ex As Exception
                PromptMsg("编辑自动编码异常失败！", ex, True)
            End Try
        End If
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

        WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
        CreateDataGridColumns() '必须将创建Column放在DataGrid1_Init()事件入口中，否则不能使用DataGrid自带的行“编辑”、“删除”等功能
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中部分代码联合使用
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim row As DataGridItem
            Dim lngRecIDClicked As Long = VLng("PAGE_RECID")
            For Each row In DataGrid1.Items
                '第1列必须是记录ID
                Dim strRecID As String = row.Cells(0).Text.Trim()
                If IsNumeric(strRecID) Then
                    row.Attributes.Add("RECID", strRecID) '在客户端保存记录ID
                    row.Attributes.Add("OnClick", "RowLeftClickPost(this)") '在客户端生成：点击记录的响应方法OnClick()

                    If RStr("acodeaction") = "rowclick" And lngRecIDClicked > 0 Then
                        If lngRecIDClicked = CLng(strRecID) Then
                            row.Attributes.Add("bgColor", "#C1D2EE") '修改被点击记录的背景颜色
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            CTableColACoding.Delete(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), CLng(e.Item.Cells(0).Text))
            ViewState("PAGE_RECID") = 0

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            DataGrid1.EditItemIndex = -1
            ShowDataGridData() '完整Load的列表和数据

            '点击删除键时等同于选中该行，所以“修改...”键会自动Enable
            btnSNumEditConstant.Enabled = False
            btnSNumEditDate.Enabled = False
            btnSNumEditSNum.Enabled = False
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnSNumAddConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumAddConstant.Click
        Try
            Dim strConstant As String = txtSNumConstant.Text.Trim()
            If strConstant.Length = 0 Then
                PromptMsg("请输入正确的常量值")
            Else
                Dim pst As CmsPassport = CmsPass()
                CTableColACoding.AddConstant(pst, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), strConstant)
            End If
        Catch ex As Exception
            PromptMsg("添加常量失败！", ex, True)
        End Try

        ShowDataGridData() '完整Load的列表和数据
    End Sub

    Private Sub btnSNumEditConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumEditConstant.Click
        Try
            If VLng("PAGE_RECID") <= 0 Then
                PromptMsg("页面处理逻辑错误", Nothing, True)
                Return
            End If

            Dim strConstant As String = txtSNumConstant.Text.Trim()
            If strConstant.Length = 0 Then
                PromptMsg("请输入正确的常量值")
            Else
                CTableColACoding.EditConstant(CmsPass, VLng("PAGE_RECID"), strConstant)
            End If

            btnSNumEditConstant.Enabled = False

            ShowDataGridData() '完整Load的列表和数据
        Catch ex As Exception
            PromptMsg("编辑常量失败！", ex, True)
        End Try
    End Sub

    Private Sub btnSNumAddDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumAddDate.Click
        Try
            Dim strConstant As String = GetACodingDate()
            If strConstant.Length = 0 Then
                PromptMsg("请选择正确的日期表达式")
            Else
                CTableColACoding.AddDate(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), strConstant)
            End If

            ShowDataGridData() '完整Load的列表和数据
        Catch ex As Exception
            PromptMsg("添加日期失败！", ex, True)
        End Try
    End Sub

    Private Sub btnSNumEditDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumEditDate.Click
        Try
            If VLng("PAGE_RECID") <= 0 Then
                PromptMsg("页面处理逻辑错误")
                Return
            End If

            Dim strConstant As String = GetACodingDate()
            If strConstant.Length = 0 Then
                PromptMsg("请选择正确的日期表达式")
            Else
                CTableColACoding.EditDate(CmsPass, VLng("PAGE_RECID"), strConstant)
            End If

            btnSNumEditDate.Enabled = False

            ShowDataGridData() '完整Load的列表和数据
        Catch ex As Exception
            PromptMsg("变价日期失败！", ex, True)
        End Try
    End Sub

    Private Sub btnSNumAddSNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumAddSNum.Click
        Try
            '获取重置时间
            Dim lngResetTime As Long = GetResetTime()
            If lngResetTime = -1 Then
                PromptMsg("请选择正确的日期表达式")
                Return
            End If
            Dim lngResetTag As Long = GetResetTag()

            '获取流水号位数
            If Not IsNumeric(txtSNumDigitNum.Text.Trim()) Then
                PromptMsg("请选择正确的流水号位数")
                Return
            End If
            Dim lngDigitNum As Long = CLng(txtSNumDigitNum.Text.Trim())

            '获取流水号位数是否补0
            Dim lngPrefixZero As Long
            If chkSNumPreZero.Checked Then
                lngPrefixZero = 1
            Else
                lngPrefixZero = 0
            End If

            '获取不吉利数字
            Dim strSkipNum As String = txtNumToSkip.Text.Trim()

            CTableColACoding.AddSeriesNum(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), lngDigitNum, lngPrefixZero, lngResetTime, lngResetTag, strSkipNum)
        Catch ex As Exception
            PromptMsg("添加流水号失败！", ex, True)
        End Try

        ShowDataGridData() '完整Load的列表和数据
    End Sub

    Private Sub btnSNumEditSNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumEditSNum.Click
        Try
            If VLng("PAGE_RECID") <= 0 Then
                PromptMsg("页面处理逻辑错误")
                Return
            End If

            '获取重置时间
            Dim lngResetTime As Long = GetResetTime()
            If lngResetTime = -1 Then
                PromptMsg("请选择正确的日期表达式")
                Return
            End If
            Dim lngResetTag As Long = GetResetTag()

            '获取流水号位数
            If Not IsNumeric(txtSNumDigitNum.Text.Trim()) Then
                PromptMsg("请选择正确的流水号位数")
                Return
            End If
            Dim lngDigitNum As Long = CLng(txtSNumDigitNum.Text.Trim())

            '获取流水号位数是否补0
            Dim lngPrefixZero As Long
            If chkSNumPreZero.Checked Then
                lngPrefixZero = 1
            Else
                lngPrefixZero = 0
            End If

            '获取不吉利数字
            Dim strSkipNum As String = txtNumToSkip.Text.Trim()

            CTableColACoding.EditSeriesNum(CmsPass, VLng("PAGE_RECID"), lngDigitNum, lngPrefixZero, lngResetTime, lngResetTag, strSkipNum, CLng(txtSNumValue.Text.Trim()))

            btnSNumEditSNum.Enabled = False

            ShowDataGridData() '完整Load的列表和数据
        Catch ex As Exception
            PromptMsg("编辑流水号失败！", ex, True)
        End Try
    End Sub

    '--------------------------------------------------
    '获取自动编码中日期设置
    '--------------------------------------------------
    Private Function GetACodingDate() As String
        Dim strConstant As String
        Try
            If rdoSNumYear1.Checked Then
                strConstant = "YYYY"
            ElseIf rdoSNumYear2.Checked Then
                strConstant = "YY"
            ElseIf rdoSNumMonth.Checked Then
                strConstant = "MM"
            ElseIf rdoSNumDate.Checked Then
                strConstant = "DD"
            Else
                strConstant = ""
            End If
        Catch ex As Exception
            strConstant = ""
        End Try

        Return strConstant
    End Function

    '--------------------------------------------------
    '获取界面上重置时间设置
    '--------------------------------------------------
    Private Function GetResetTime() As Long
        Dim lngResetTime As Long = -1
        If rdoSNumNoReset.Checked Then
            lngResetTime = ACodingResetTime.NoReset
        ElseIf rdoSNumYearReset.Checked Then
            lngResetTime = ACodingResetTime.YearReset
        ElseIf rdoSNumMonthReset.Checked Then
            lngResetTime = ACodingResetTime.MonthReset
        ElseIf rdoSNumDateReset.Checked Then
            lngResetTime = ACodingResetTime.DateReset
        Else
            lngResetTime = -1
        End If
        Return lngResetTime
    End Function

    '--------------------------------------------------
    '获取界面上重置时间设置
    '--------------------------------------------------
    Private Function GetResetTag() As Long
        Dim lngResetTag As Long = 0
        If rdoSNumNoReset.Checked Then
            lngResetTag = 0
        ElseIf rdoSNumYearReset.Checked Then
            lngResetTag = Today.Year
        ElseIf rdoSNumMonthReset.Checked Then
            lngResetTag = Today.Month
        ElseIf rdoSNumDateReset.Checked Then
            lngResetTag = Today.Day
        Else
            lngResetTag = 0
        End If
        Return lngResetTag
    End Function

    '----------------------------------------------------------
    '显示主表数据
    '----------------------------------------------------------
    Private Sub ShowDataGridData()
        Try
            Dim pst As CmsPassport = CmsPass()
            DataGrid1.DataSource = CTableColACoding.GetDatasetForAdmin(pst, VLng("PAGE_RESID"), VStr("PAGE_COLNAME")).Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("获取自动编码信息失败！", ex)
        End Try
    End Sub

    '------------------------------------------------------------------
    '创建资源数据表的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumns()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "内部字段ID"
        col.DataField = "CDC_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "编码类型"
        col.DataField = "CDC_TYPE_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "编码值"
        col.DataField = "CDC_VALUE"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        intWidth += 250

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '或者用图标，如："<img src='/cmsweb/images/common/delete.gif' border=0>"
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(80)
        colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        DataGrid1.Columns.Add(colDel)
        intWidth += 80

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '编辑模式下Load原先的值
    '---------------------------------------------------------------
    Private Sub LoadValueForEdit(ByVal lngAIID As Long)
        '用户点击了某行自动编码设置，Load该行编码设置信息
        Dim datACode As DataAutoCoding = CTableColACoding.GetOneACoding(CmsPass, lngAIID)
        If datACode Is Nothing Then Return

        If datACode.lngCDC_TYPE = ACodingType.IsConstant Then
            btnSNumEditConstant.Enabled = True
            'btnSNumEditDate.Enabled = False
            'btnSNumEditSNum.Enabled = False

            txtSNumConstant.Text = datACode.strCDC_VALUE
        ElseIf datACode.lngCDC_TYPE = ACodingType.IsDate Then
            'btnSNumEditConstant.Enabled = False
            btnSNumEditDate.Enabled = True
            'btnSNumEditSNum.Enabled = False

            If datACode.strCDC_VALUE = "YYYY" Then
                rdoSNumYear1.Checked = True
            ElseIf datACode.strCDC_VALUE = "YY" Then
                rdoSNumYear2.Checked = True
            ElseIf datACode.strCDC_VALUE = "MM" Then
                rdoSNumMonth.Checked = True
            ElseIf datACode.strCDC_VALUE = "DD" Then
                rdoSNumDate.Checked = True
            End If
        ElseIf datACode.lngCDC_TYPE = ACodingType.IsSeriesNum Then
            'btnSNumEditConstant.Enabled = False
            'btnSNumEditDate.Enabled = False
            btnSNumEditSNum.Enabled = True

            If datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.NoReset Then
                rdoSNumNoReset.Checked = True
            ElseIf datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.YearReset Then
                rdoSNumYearReset.Checked = True
            ElseIf datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.MonthReset Then
                rdoSNumMonthReset.Checked = True
            ElseIf datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.DateReset Then
                rdoSNumDateReset.Checked = True
            End If
            txtSNumDigitNum.Text = CStr(datACode.lngCDC_SNUM_LENGTH)
            If datACode.lngCDC_SNUM_PREZERO = 1 Then
                chkSNumPreZero.Checked = True
            Else
                chkSNumPreZero.Checked = False
            End If

            txtNumToSkip.Text = CStr(datACode.strCDC_SNUM_SKIP)
            If datACode.strCDC_VALUE = "" Then
                txtSNumValue.Text = "1"
            Else
                txtSNumValue.Text = datACode.strCDC_VALUE
            End If

            txtSNumValue.Text = datACode.strCDC_VALUE
        End If
    End Sub
End Class

End Namespace
