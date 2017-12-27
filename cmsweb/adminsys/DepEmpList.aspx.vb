Option Strict On
Option Explicit On 

Imports System.Text
Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class DepEmpList
        Inherits CmsPage

        Dim templateType As ListItemType
        Dim columnName As String



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

    'ItemCreated事件会被触发2次，第一次是视图状态恢复时，没有意义的一次，不必处理；第二次是真正输出HTML页面前，所以应该处理。
    Private m_intItemCreatedCounter As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        RegisterScriptOfRowClick() '在页面上注册：行记录点击事件的JavaScript
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("nodep") = "yes" Then
            btnChooseDep.Visible = False
            btnChooseEnterprise.Visible = False
        End If
        If RStr("noemp") = "yes" Then btnChooseEmp.Visible = False

        '---------------------------------------------------------------
        '初始化分页控件
        Cmspager1.PageRows = CmsConfig.GetInt("SYS_CONFIG", "TABLEROWS_EMP")
        Cmspager1.Language = CmsPass.Employee.Language
        Cmspager1.BgColor = "#e7ebef"
        Cmspager1.TableAlign = "left"
        Cmspager1.ButtonAlign = "left"
        Cmspager1.WordAlign = "left"
        Cmspager1.TableHeight = "25px"
        Cmspager1.TotalWidth = "100%"
        Cmspager1.ButtonsWidth = "130px"
        '---------------------------------------------------------------

        ShowTableOfEmployee(RLng("depid")) '填入表格数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub Cmspager1_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager1.Click
        ShowTableOfEmployee(RLng("depid"), eventArgument) '填入表格数据
    End Sub

    Private Sub btnChooseDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseDep.Click
        Dim lngDepID As Long = RLng("depid")
        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "depid=" & lngDepID & "&depempcmd=selected_depemp"), False)
    End Sub

    Private Sub btnChooseEnterprise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseEnterprise.Click
        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "depid=0&depempcmd=selected_depemp"), False)
    End Sub

    Private Sub btnChooseEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChooseEmp.Click
            Dim lngRecID As String = RStr("RECID")
            If lngRecID.Length = 0 Then lngRecID = Request.Form("selectRecId1")
            If lngRecID.Length > 0 Then
                Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "empaiid=" & lngRecID & "&depempcmd=selected_depemp"), False)
            Else
                Dim lngDepID As Long = RLng("depid")
                ShowTableOfEmployee(lngDepID)  '为了显示人员表结构

                PromptMsg("请选择人员")
            End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        Try
            CmsPageSaveParametersToViewState()

            WebUtilities.InitialDataGrid(DataGrid1, CmsConfig.GetLong("SYS_CONFIG", "TABLEROWS_EMP"), True, True, False, True) '设置DataGrid显示属性
            'Dim dv As DataView = ResFactory.TableService("EMP").GetTableColumns(CmsPass, CmsResID.Employee)
            'WebUtilities.LoadResTableColumns(CmsPass, CmsResID.Employee, DataGrid1, dv)
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中
        '2部分代码联合使用：1）Javascript方法。2）添加一个hidden变量：<input type="hidden" name="RECID">
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            'ItemCreated事件会被触发2次，第一次是视图状态恢复时，没有意义的一次，不必处理；第二次是真正输出HTML页面前，所以应该处理。
            If m_intItemCreatedCounter < 1 Then
                m_intItemCreatedCounter += 1
                Return
            End If

            Dim i As Integer = 0
            For i = 0 To DataGrid1.Items.Count - 1
                Dim row As DataGridItem = DataGrid1.Items(i)
                    Dim strAiid As String = row.Cells(0).Text.Trim() '第1列必须是记录ID
                    row.Attributes.Add("RECID", strAiid) '在客户端保存记录ID
                    row.Attributes.Add("USERCODE", row.Cells(2).Text.Trim.ToLower()) '在客户端保存记录ID
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '在客户端生成：点击记录的响应方法OnClick()
            Next
        End If
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        ShowTableOfEmployee(RLng("depid"))
    End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        '每次点击替换排序次序
        ViewState("CMSPAGE_EMP_ORDERBY_TYPE") = CStr(IIf(VStr("CMSPAGE_EMP_ORDERBY_TYPE") = "ASC", "DESC", "ASC"))
        ViewState("CMSPAGE_EMP_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("CMSPAGE_EMP_ORDERBY_TYPE")
        ShowTableOfEmployee(RLng("depid"))
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
            Dim intWidth As Integer = 0


            Dim col As BoundColumn = New BoundColumn
            col.HeaderText = "ID" '关键字段
            col.DataField = "ID"
            col.ItemStyle.Width = Unit.Pixel(1)
            col.Visible = False
            DataGrid1.Columns.Add(col)

            If Request("type") IsNot Nothing And Request("type") = "Virtual" Then
                Dim colTemplate As New TemplateColumn
                colTemplate.HeaderTemplate = New DataGridTemplate1(ListItemType.Header, "")
                colTemplate.ItemTemplate = New DataGridTemplate1(ListItemType.Item, "")
                colTemplate.ItemStyle.Width = Unit.Pixel(60)
                DataGrid1.Columns.Add(colTemplate)
                colTemplate = Nothing
                intWidth += 60
            End If




            col = New BoundColumn
            col.HeaderText = "人员帐号"
            col.DataField = "EMP_ID"
            col.ItemStyle.Width = Unit.Pixel(100)
            DataGrid1.Columns.Add(col)
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "人员姓名"
            col.DataField = "EMP_NAME"
            col.ItemStyle.Width = Unit.Pixel(120)
            DataGrid1.Columns.Add(col)
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "手机"
            col.DataField = "EMP_HANDPHONE"
            col.ItemStyle.Width = Unit.Pixel(100)
            DataGrid1.Columns.Add(col)
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "电子邮件"
            col.DataField = "EMP_EMAIL"
            col.ItemStyle.Width = Unit.Pixel(160)
            DataGrid1.Columns.Add(col)
            intWidth += 120

            DataGrid1.Width = Unit.Pixel(intWidth)
        End Sub

    '----------------------------------------------------------
    '创建DataGrid的列字段和填充内容
    '----------------------------------------------------------
        Private Sub ShowTableOfEmployee(ByVal lngDepID As Long, Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
            Try
                ' If CmsPass.EmpIsSysAdmin Or OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID, True).Trim = CmsPass.Employee.ID.Trim Then
                '--------------------------------------------------------------------------------------------
                '绑定数据前的分页控件的处理
                Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '设置页数
                DataGrid1.CurrentPageIndex = 0
                '--------------------------------------------------------------------------------------------

                Dim intTotalRecNum As Integer = 0
                Dim strOrderBy As String = VStr("CMSPAGE_EMP_ORDERBY")
                Dim ds As DataSet = GetDataset(lngDepID, strHostPageCommand, Condition)



                '绑定数据集
                Dim dv As DataView = ds.Tables(0).DefaultView
                DataGrid1.VirtualItemCount = dv.Count '分页用
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
                ' End If
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

    '----------------------------------------------------------
    '创建DataGrid的列字段和填充内容
    '----------------------------------------------------------
        Private Function GetDataset(ByVal lngDepID As Long, ByVal strHostPageCommand As String, ByVal Condition As String) As DataSet
            Dim intTotalRecNum As Integer = 0
            Dim strOrderBy As String = VStr("CMSPAGE_EMP_ORDERBY")
            Dim ds As DataSet = Nothing
            If OrgFactory.DepDriver().IsVirtualDep(CmsPass, lngDepID) = True Then
                '是虚拟部门
                Dim strWhere As String = "EMP_ID IN (SELECT VDEP_EMPID FROM " & CmsTables.DepartmentVirtual & " WHERE VDEP_DEPID=" & lngDepID & ")"
                Dim strSql As String = "SELECT * FROM " & CmsTables.Employee & " WHERE " & strWhere + IIf(Condition.Trim = "", "", " and " + Condition.Trim).ToString
                'ds = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql, CmsTables.Employee)
                ds = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql, Cmspager1.RecStartOnCurPage, Cmspager1.PageRows, CmsTables.Employee)
                Dim strSqlCount As String = "SELECT COUNT(*) FROM " & CmsTables.Employee & " WHERE " & strWhere + IIf(Condition.Trim = "", "", " and " + Condition.Trim).ToString
                intTotalRecNum = CInt(CmsDbStatement.CountSql(SDbConnectionPool.GetDbConfig(), strSqlCount))
            Else
                ds = ResFactory.TableService("EMP").GetHostTableData(CmsPass, lngDepID, Condition, strOrderBy, , Cmspager1.RecStartOnCurPage, Cmspager1.PageRows, intTotalRecNum)
            End If
            Cmspager1.TotalRecordNumber = intTotalRecNum
            Return ds
        End Function

    '----------------------------------------------------------
    '在页面上注册：行记录点击事件的JavaScript
    '----------------------------------------------------------
    Private Sub RegisterScriptOfRowClick()
        Dim strbScript As New StringBuilder(2048)
        strbScript.Append("<script language=""javascript"">")
        strbScript.Append("    function RowLeftClickNoPost(src){")
        strbScript.Append("        var o=src.parentNode;")
        strbScript.Append("        for (var k=1;k<o.children.length;k++){")
        strbScript.Append("            o.children[k].bgColor = ""white"";")
        strbScript.Append("        }")
        strbScript.Append("        src.bgColor = ""#C4D9F9"";")
        strbScript.Append("        self.document.forms(0).RECID.value = src.RECID;")
            strbScript.Append("    }")

            strbScript.Append("function IsShowCheckbox(id)")
            strbScript.Append("{")
            strbScript.Append("if(document.getElementById(id)!=null){")
            strbScript.Append("    var trList=document.getElementById(id).getElementsByTagName('tr');")
            strbScript.Append("    for(var j=0;j<trList.length;j++)")
            strbScript.Append("    {")
            strbScript.Append("     if(trList[j].USERCODE=='admin' || trList[j].USERCODE=='security' || trList[j].USERCODE=='sysuser')")
            strbScript.Append("     {")
            strbScript.Append("        var inputList=trList[j].getElementsByTagName('input');")
            strbScript.Append("        for(var i=0;i<inputList.length;i++)")
            strbScript.Append("        {")
            strbScript.Append("            if(inputList[i].id.indexOf('cbx')>=0 && inputList[i].type=='checkbox')")
            strbScript.Append("            {")
            strbScript.Append("               inputList[i].style.display='none';")
            strbScript.Append("            }")
            strbScript.Append("        }")
            strbScript.Append("        }")
            strbScript.Append("    }  ")
            strbScript.Append("    }  ")
            strbScript.Append("}")
        strbScript.Append("</script>")
        Response.Write(strbScript.ToString())

        RegisterHiddenField("RECID", "")
        End Sub

        Private Class DataGridTemplate1
            Implements ITemplate
            Dim templateType As ListItemType
            Dim columnName As String

            Sub New(ByVal type As ListItemType, ByVal ColName As String)
                templateType = type
                columnName = ColName
            End Sub

            Sub InstantiateIn(ByVal container As Control) _
               Implements ITemplate.InstantiateIn
                Dim lc As New Literal
                Select Case templateType
                    Case ListItemType.Header
                        Dim cb As New CheckBox
                        cb.ID = "cbx"
                        cb.Attributes.Add("onclick", "checkAllCheckBox(this);")
                        container.Controls.Add(cb)
                    Case ListItemType.Item
                        Dim cb As New CheckBox
                        cb.ID = "cbx"
                        cb.Attributes.Add("onclick", "clickCheckBox(this);")
                        container.Controls.Add(cb)
                    Case ListItemType.EditItem
                        Dim tb As New TextBox
                        tb.Text = ""
                        tb.ID = "txt"
                        container.Controls.Add(tb)
                    Case ListItemType.Footer
                        lc.Text = "<I>Footer</I>"
                        container.Controls.Add(lc)
                End Select
            End Sub
        End Class


        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim lngDepID As Long = RLng("depid")
            If lngDepID = 0 Then lngDepID = -1
            Condition = " (EMP_ID = '" + txtSearch.Text.Trim + "' or EMP_NAME like '%" + txtSearch.Text.Trim + "%') "
            ShowTableOfEmployee(lngDepID)
        End Sub


        Protected Property Condition() As String
            Get
                If ViewState("Condition") Is Nothing Then ViewState("Condition") = ""
                Return ViewState("Condition").ToString
            End Get
            Set(ByVal value As String)
                ViewState("Condition") = value
            End Set
        End Property


    End Class



End Namespace
