Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web



Partial Class BatchSendList
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
        If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        'btnBSendEmail.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要开始群发电子邮件吗？');")
        'btnBSendSms.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要开始群发手机短信吗？');")
        btnDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前选中的群发设置吗？');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind() '绑定数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnBSendEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBSendEmail.Click
        RunBatchSend(BATCHSEND_TYPE.Email)
    End Sub

    Private Sub btnBSendSms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBSendSms.Click
        RunBatchSend(BATCHSEND_TYPE.SMS)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mtstype=" & MTSearchType.BatchSend & "&URLRECID=" & Me.GetRecIDOfGrid()
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.BatchSend, False)

        'Session("CMSBP_BatchSendSetting") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & Me.GetRecIDOfGrid()
        'Response.Redirect("/cmsweb/BatchSendSetting.aspx", False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?mnuhostresid=" & VLng("PAGE_HOST_RESID") & "&mnuresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.BatchSend
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mtshostid=" & lngRecID & "&mtstype=" & MTSearchType.BatchSend, False)

        'Session("CMSBP_BatchSendSetting") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        'Response.Redirect("/cmsweb/BatchSendSetting.aspx?mtshostid=" & lngRecID, False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        MultiTableSearch.DelMTSRecordByID(CmsPass, lngRecID)
        GridDataBind() '绑定数据
    End Sub

    Private Sub btnEmailContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailContent.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        Session("CMSBP_BatchSendContent") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendContent.aspx?mtshostid=" & lngRecID & "&bsend_type=" & BATCHSEND_TYPE.Email, False)
    End Sub

    Private Sub btnSmsContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmsContent.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        Session("CMSBP_BatchSendContent") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendContent.aspx?mtshostid=" & lngRecID & "&bsend_type=" & BATCHSEND_TYPE.SMS, False)
    End Sub

    Private Sub btnEmailServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmailServer.Click
        Session("CMSBP_BatchSendEmailSetting") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & Me.GetRecIDOfGrid()
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendEmailSetting.aspx?emailset_type=" & DbParameter.BSEND_SMTP, False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        'CmsDbBase.MoveUpByRecID(CmsPass, CmsTables.MTableHost, lngRecID, "MTS_TYPE=" & MTSearchType.BatchSend)
        CmsDbBase.MoveUpByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & MTSearchType.BatchSend, "MTS_SHOWORDER")
        GridDataBind() '绑定数据
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        'CmsDbBase.MoveDownByRecID(CmsPass, CmsTables.MTableHost, lngRecID, "MTS_TYPE=" & MTSearchType.BatchSend)
        CmsDbBase.MoveDownByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & lngRecID, "MTS_RESID=" & VLng("PAGE_RESID") & " AND MTS_TYPE=" & MTSearchType.BatchSend, "MTS_SHOWORDER")
        GridDataBind() '绑定数据
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

            WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
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
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "MTS_ID" '关键字段
        col.DataField = "MTS_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "群发标题"
        col.DataField = "MTS_TITLE"
        col.ItemStyle.Width = Unit.Pixel(400)
        DataGrid1.Columns.Add(col)
        intWidth += 400

        col = New BoundColumn
        col.HeaderText = "主资源"
        col.DataField = "RESID1_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "设置人员"
        col.DataField = "MTS_EMPID2"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = MultiTableSearch.GetMTSearchByDataSet(CmsPass, "", MTSearchType.BatchSend, 0, CmsPass.Employee.ID)
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    '---------------------------------------------------------------
    '开始运行群发操作
    '---------------------------------------------------------------
    Private Sub RunBatchSend(ByVal intBSendType As BATCHSEND_TYPE)
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的记录！")
            Return
        End If
        If lngRecID <> BatchSendThread.MtsHostID() Then
            If BatchSendThread.GetStatus = ThreadStatus.Pause Or BatchSendThread.GetStatus = ThreadStatus.Running Then
                Dim strTitle As String = CmsDbBase.GetFieldStr(CmsPass, CmsTables.MTableHost, "MTS_TITLE", "MTS_ID=" & BatchSendThread.MtsHostID())
                PromptMsg("当前正在运行其它群发操作（标题：" & strTitle & "），不能同时运行2个或2个以上的群发操作！")
                Return
            Else
                BatchSendThread.ThreadMessage = ""
                BatchSendThread.TotalNum = 0
                BatchSendThread.SentNum = 0
            End If
        End If

        Session("CMSBP_BatchSendWorking") = "/cmsweb/cmsbatchsend/BatchSendList.aspx?URLRECID=" & lngRecID
        Response.Redirect("/cmsweb/cmsbatchsend/BatchSendWorking.aspx?mtshostid=" & lngRecID & "&bsend_type=" & intBSendType, False)
    End Sub
End Class

End Namespace
