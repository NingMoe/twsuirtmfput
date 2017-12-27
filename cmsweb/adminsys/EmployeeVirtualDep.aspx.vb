Option Strict On
Option Explicit On

Imports Unionsoft.Platform
Imports NetReusables

Partial Class adminsys_EmployeeVirtualDep
    Inherits CmsPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()

        'lnkDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除虚拟部门下的人员吗？');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'If RStr("depempcmd") = "selected_depemp" Then '从选择部门人员页面回来
        '    Dim lngAiid As String = RStr("empaiid")
        '    Dim ch() As Char = {CType(",", Char)}
        '    Dim EmpID() As String = lngAiid.Split(ch, StringSplitOptions.RemoveEmptyEntries)
        '    For i As Integer = 0 To EmpID.Length - 1
        '        Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, Convert.ToInt64(EmpID(i)))
        '        If strEmpID <> "" Then
        '            '先判断选择的人员是否已经存在与虚拟部门下
        '            Dim lngNum As Long = CmsDbBase.CountRows(CmsPass, CmsTables.DepartmentVirtual, "VDEP_DEPID=" & RLng("depid") & " AND VDEP_EMPID='" & strEmpID & "'")
        '            If lngNum > 0 Then
        '                '不必提示
        '                'PromptMsg("人员帐号(" & strEmpID & ")已经存在与当前虚拟部门中！")
        '            Else
        '                '添加人员至虚拟部门中
        '                DbVirtualDep.AddEmployee(CmsPass, RLng("depid"), strEmpID)
        '            End If
        '        End If
        '    Next
        'End If


        GridDataBind()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
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
        col.HeaderText = "VDEP_DEPID" '关键字段
        col.DataField = "VDEP_DEPID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "部门名称"
        col.DataField = "Name"
        col.ItemStyle.Width = Unit.Pixel(450)
        DataGrid1.Columns.Add(col)
        intWidth += 450

        'col = New BoundColumn
        'col.HeaderText = "人员姓名"
        'col.DataField = "EMP_NAME" '"VDEP_EMPID2"
        'col.ItemStyle.Width = Unit.Pixel(300)
        'DataGrid1.Columns.Add(col)
        'intWidth += 300

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '绑定数据
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = DbVirtualDep.GetDeptListByDataSet(RStr("uid"))
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub
End Class
