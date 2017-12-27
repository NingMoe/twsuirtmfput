Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmVirtualDep
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lnkExit As System.Web.UI.WebControls.LinkButton

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
        'lnkDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除虚拟部门下的人员吗？');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("depempcmd") = "selected_depemp" Then '从选择部门人员页面回来
                Dim lngAiid As String = RStr("empaiid")
                Dim ch() As Char = {CType(",", Char)}
                Dim EmpID() As String = lngAiid.Split(ch, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To EmpID.Length - 1
                    Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, Convert.ToInt64(EmpID(i)))
                    If strEmpID <> "" Then
                        '先判断选择的人员是否已经存在与虚拟部门下
                        Dim lngNum As Long = CmsDbBase.CountRows(CmsPass, CmsTables.DepartmentVirtual, "VDEP_DEPID=" & RLng("depid") & " AND VDEP_EMPID='" & strEmpID & "'")
                        If lngNum > 0 Then
                            '不必提示
                            'PromptMsg("人员帐号(" & strEmpID & ")已经存在与当前虚拟部门中！")
                        Else
                            '添加人员至虚拟部门中
                            DbVirtualDep.AddEmployee(CmsPass, RLng("depid"), strEmpID)
                        End If
                    End If
                Next
        End If


        GridDataBind()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        GridDataBind()
    End Sub

    Private Sub lnkAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAdd.Click
        Session("CMSBP_DepEmpList") = "/cmsweb/adminsys/EmployeeFrmVirtualDep.aspx?depid=" & RLng("depid")
            Response.Redirect("/cmsweb/adminsys/DepEmpList.aspx?nodep=yes&type=Virtual", False)
    End Sub

    Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("请先选择有效的人员！")
            Return
        End If

        DbVirtualDep.DelEmployee(CmsPass, lngRecID)
        GridDataBind()
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
                    Dim strUserID As String = row.Cells(1).Text.Trim()
                If IsNumeric(strRecID) Then
                        row.Attributes.Add("RECID", strRecID) '在客户端保存记录ID
                        row.Attributes.Add("USERID", strUserID)
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
        col.HeaderText = "VDEP_ID" '关键字段
        col.DataField = "VDEP_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "人员帐号"
        col.DataField = "VDEP_EMPID"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "人员姓名"
        col.DataField = "EMP_NAME" '"VDEP_EMPID2"
        col.ItemStyle.Width = Unit.Pixel(300)
        DataGrid1.Columns.Add(col)
        intWidth += 300

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = DbVirtualDep.GetEmpListByDataSet(CmsPass, RLng("depid"))
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

        'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
        '        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
        '        e.Item.Attributes.Add("uid", DbField.GetStr(drv, "VDEP_EMPID"))
        '        e.Item.Attributes.Add("onclick", "selectRows(this);")
        '    End If
        'End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim ds As DataSet = DbVirtualDep.GetEmpListByDataSet(CmsPass, RLng("depid"))
                Dim dv As DataView = ds.Tables(0).DefaultView
                dv.RowFilter = " VDEP_EMPID='" + Me.txtSearch.Text.Trim + "' or EMP_NAME like '%" + Me.txtSearch.Text.Trim + "%' "
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
