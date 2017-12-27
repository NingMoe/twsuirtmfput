Option Strict On
Option Explicit On 

Imports System.Text

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmContent
    Inherits Cms.Web.CmsFrmContentBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents panelColumnSet As System.Web.UI.WebControls.Panel
    Protected WithEvents panelColshowSet As System.Web.UI.WebControls.Panel
    Protected WithEvents panelInputformDesign As System.Web.UI.WebControls.Panel
    Protected WithEvents lbtnProjRightsForDep As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnProjRightsForEmp As System.Web.UI.WebControls.LinkButton

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

    Private Const BACK_PAGE As String = "/cmsweb/adminsys/EmployeeFrmContent.aspx"

        '当前选择的部门ID
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' If IsPostBack Then Return


        End Sub

        Protected Overrides Sub CmsPageSaveParametersToViewState()
            MyBase.CmsPageSaveParametersToViewState()
        End Sub

        Protected Overrides Sub CmsPageInitialize()
            If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then '只有系统管理员和部门管理员能进入人员管理
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If

            lnkDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除人员吗？');")

            RegisterScriptOfRowClick() '在页面上注册：行记录点击事件的JavaScript
        End Sub

        Protected Overrides Sub CmsPageDealFirstRequest()
            If CmsPass.EmpIsSysAdmin = True Or OrgFactory.DepDriver.GetDepAdmin(CmsPass, RLng("depid"), True).Trim = CmsPass.Employee.ID.Trim Then

                Cmspager1.CurrentPage = 0
                Session("CMS_EMPTABLE_PAGING") = 0
                Try
                    If RStr("cmsaction") = "seldep" Then '选择部门后更改人员所属部门
                        Try
                            Dim lngNewDep As Long = RLng("tmpdepid")
                            If lngNewDep <> 0 Then
                                'OrgFactory.EmpDriver.ChangeDepartment(CmsPass, SLng("CMSEMPMGR_EMPID"), lngNewDep)
                                'Session("CMSEMPMGR_EMPID") = Nothing
                            Else
                                PromptMsg("不能移动人员至企业之下！")
                            End If
                        Catch ex As Exception
                            PromptMsg("更换部门失败！", ex, True)
                        End Try
                    End If

                    Session("CMS_EMPSEARCH_COLTYPE") = WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsResID.Employee, ddlColumns, True, True, True)      '初始化DropDownList中字段列表项
                    WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions, False, 80)  '初始化DropDownList中查找条件项

                    '---------------------------------------------------------------
                    '初始化分页控件
                    Cmspager1.EnableViewState = True
                    Cmspager1.PageRows = CmsConfig.GetInt("SYS_CONFIG", "TABLEROWS_EMP")
                    Cmspager1.Language = CmsPass.Employee.Language
                    Cmspager1.BgColor = "#e7ebef"
                    Cmspager1.TableAlign = "left"
                    Cmspager1.ButtonAlign = "left"
                    Cmspager1.WordAlign = "left"
                    Cmspager1.TableHeight = "25px"
                    Cmspager1.TotalWidth = "100%"
                    Cmspager1.ButtonsWidth = "130px"
                    Cmspager1.CurrentPage = SInt("CMS_EMPTABLE_PAGING")
                    '---------------------------------------------------------------

                    InitialFuncShow() '界面功能的初始化

                    ShowTableOfEmployee(RLng("depid")) '填入表格数据
                Catch ex As Exception
                    PromptMsg("", ex, True)
                End Try
            Else
                Cmspager1.Visible = False
                lnkAdd.Enabled = False
                lnkEdit.Enabled = False
                lnkDelete.Enabled = False
                lbtnChangeDep.Enabled = False

                lbtnSearch.Enabled = False
                lbtnCancelSearch.Enabled = False

                lnkSetPassword.Enabled = False
                lbtnClearPassword.Enabled = False
                ' lbtnImport.Enabled = False

                'lbtnProjRightsForEmp.Enabled = False
                'lbtnProjRightsForDep.Enabled = False
            End If

            If CmsPass.EmpIsDepAdmin Then

                lbtnColumnSet.Enabled = False
                lbtnColshowSet.Enabled = False
                lbtnInputformDesign.Enabled = False
            End If
        End Sub

        Protected Overrides Sub CmsPageDealPostBack()
            InitialFuncShow() '界面功能的初始化
        End Sub

        Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgEmployee.Init
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If

            Try
                CmsPageSaveParametersToViewState()

                '初始化DataGrid
                WebUtilities.InitialDataGrid(dgEmployee, CmsConfig.GetLong("SYS_CONFIG", "TABLEROWS_EMP"), True, True, False, True)      '设置DataGrid显示属性

                '创建DataGrid的自定义列。必须在DataGrid1_Init()中创建列，否则之后不能进入PageIndexChanged、SortCommand等的事件入口
                Dim dv As DataView = ResFactory.TableService("EMP").GetTableColumns(CmsPass, CmsResID.Employee)
                WebUtilities.LoadResTableColumns(CmsPass, CmsResID.Employee, dgEmployee, dv)
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEmployee.ItemCreated
            '设置每行的唯一ID和OnClick方法，用于传回服务器做相应操作，如修改、删除等，下面代码与Html页面中
            '2部分代码联合使用：1）Javascript方法。2）添加一个hidden变量：<input type="hidden" name="RECID">
            If dgEmployee.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                'ItemCreated事件会被触发2次，第一次是视图状态恢复时，没有意义的一次，不必处理；第二次是真正输出HTML页面前，所以应该处理。
                If m_intItemCreatedCounter < 1 Then
                    m_intItemCreatedCounter += 1
                    Return
                End If

                Dim i As Integer = 0
                For i = 0 To dgEmployee.Items.Count - 1
                    Dim index As Integer = 0
                    Dim row As DataGridItem = dgEmployee.Items(i) 

                    Dim strAiid As String = row.Cells(0).Text.Trim() '第1列必须是记录ID
                    row.Attributes.Add("RECID", strAiid) '在客户端保存记录ID
                    row.Attributes.Add("USERCODE", dgEmployee.DataKeys(i).ToString()) '在客户端保存记录ID
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '在客户端生成：点击记录的响应方法OnClick()
                Next
            End If
        End Sub

        Private Sub dgEmployee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEmployee.SortCommand
            '每次点击替换排序次序
            ViewState("CMSPAGE_EMP_ORDERBY_TYPE") = CStr(IIf(VStr("CMSPAGE_EMP_ORDERBY_TYPE") = "ASC", "DESC", "ASC"))
            ViewState("CMSPAGE_EMP_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("CMSPAGE_EMP_ORDERBY_TYPE")
            ShowTableOfEmployee(RLng("depid"))
        End Sub

        Private Sub Cmspager1_Click(ByVal sender As Object, ByVal eventArgument As String) Handles Cmspager1.Click
            ShowTableOfEmployee(RLng("depid"), eventArgument)
        End Sub

        Private Sub lbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSearch.Click
            '组织Where语句
            Dim hashColType As Hashtable = CType(Session("CMS_EMPSEARCH_COLTYPE"), Hashtable)
            Dim lngColType As Long = CLng(hashColType(ddlColumns.SelectedItem.Value))
            Dim strCondition As String = ddlConditions.SelectedItem.Value.Trim()
            Dim strWhere As String = CTableStructure.GenerateFieldWhere(ddlColumns.SelectedItem.Value, txtSearchValue.Text.Trim(), lngColType, strCondition, SDbConnectionPool.GetDbConfig().DatabaseType)
            ViewState("CMSPAGE_EMP_WHERE") = strWhere

            Cmspager1.CurrentPage = 0
            Session("CMS_EMPTABLE_PAGING") = 0

            '显示列表、
            Dim depid As Long = RLng("depid")
            If depid = 0 Then depid = -1
            ShowTableOfEmployee(depid)

            If strWhere = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
        End Sub

        Private Sub lbtnCancelSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCancelSearch.Click
            txtSearchValue.Text = ""
            ShowTableOfEmployee(RLng("depid"))
        End Sub

        Private Sub lnkAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAdd.Click
            Session("CMSBP_RecordEdit") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))

            Response.Redirect("/cmsweb/cmshost/RecordEdit.aspx?mnuhostresid=" & CmsResID.Employee & "&mnuhostrecid=&mnuresid=" & CmsResID.Employee & "&mnurecid=&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & InputMode.AddInHostTable & "&depid=" & RLng("depid"), False)
        End Sub

        Private Sub lnkEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("请先选择需要操作的人员记录")
                Return
            End If

            Session("CMSBP_RecordEdit") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/cmshost/RecordEdit.aspx?mnuhostresid=" & CmsResID.Employee & "&mnuhostrecid=&mnuresid=" & CmsResID.Employee & "&mnurecid=" & lngRecID & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & InputMode.EditInHostTable & "&depid=" & RLng("depid"), False)
        End Sub

        Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("请先选择需要操作的人员记录")
            Else
                ResFactory.TableService("EMP").DelRecords(CmsPass, 0, CStr(lngRecID))
                ShowTableOfEmployee(RLng("depid"))
            End If
        End Sub

        Private Sub lbtnChangeDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnChangeDep.Click
            Dim lngRecID As String = Request.Form("selectRecId1") ' RLng("RECID")
            If lngRecID.Trim = "" Then lngRecID = RLng("RECID").ToString
            If lngRecID.Length <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("请先选择需要操作的人员记录")
                Return
            End If

            Session("CMSEMPMGR_EMPID") = lngRecID '切换页面前先保留当前人员ID
            Session("CMSBP_DepartmentSelect") = CmsUrl.AppendParam(BACK_PAGE, "employeeid=" & lngRecID & "&depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/adminsys/DepartmentSelect.aspx?employeeid=" & lngRecID & "&depid=" & RLng("depid"), False)
        End Sub

        Private Sub lnkSetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSetPassword.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("请先选择需要操作的人员记录")
                Return
            End If

            Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, lngRecID)
            Dim strEmpName As String = OrgFactory.EmpDriver.GetEmpName(CmsPass, strEmpID)
            Session("EMPPASS_BACKPAGE") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/adminsys/EmployeeSetPass.aspx?empid=" & strEmpID & "&empname=" & strEmpName, False)
        End Sub

        Private Sub lbtnClearPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearPassword.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("请先选择需要操作的人员记录")
                Return
            End If

            Try
                'OrgFactory.EmpDriver.UpdatePassword(CmsPass.Employee.ID, CmsPass.Employee.Password, "")   '清空密码
                OrgFactory.EmpDriver.SetPass(CmsPass, OrgFactory.EmpDriver.GetEmpID(CmsPass, lngRecID), "")

                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("密码清空成功！")
            Catch ex As Exception
                PromptMsg("密码清空失败，无法连接数据库！")
            End Try
        End Sub

        Private Sub lbtnColumnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnColumnSet.Click
            Session("CMSBP_ResourceColumnSet") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & CmsResID.Employee, False)
        End Sub

        Private Sub lbtnColshowSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnColshowSet.Click
            Session("CMSBP_ResourceColumnShowSet") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/adminres/ResourceColumnShowSet.aspx?mnuresid=" & CmsResID.Employee, False)
        End Sub

        Private Sub lbtnInputformDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnInputformDesign.Click
            Session("CMSBP_FormDesign") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/cmsform/FormDesign.aspx?mnuresid=" & CmsResID.Employee & "&mnuformtype=" & FormType.InputForm & "&mnuformname=", False)
        End Sub

        Private Sub lbtnProjRightsForEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnProjRightsForEmp.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("请先选择需要操作的人员记录")
                Return
            End If

            Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, lngRecID)
            Session("CMSBP_RightsSetProject") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid") & "&mnufrom=projrights&gainerid=" & strEmpID)
            Response.Redirect("/cmsweb/cmsrights/RightsSetProject.aspx?gainerid=" & strEmpID & "&gainertype=" & RightsGainerType.IsEmployee, False)
        End Sub

        Private Sub lbtnProjRightsForDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnProjRightsForDep.Click
            Session("CMSBP_RightsSetProject") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid") & "&mnufrom=projrights&gainerid=" & RLng("depid"))
            Response.Redirect("/cmsweb/cmsrights/RightsSetProject.aspx?gainerid=" & RLng("depid") & "&gainertype=" & RightsGainerType.IsDepartment, False)
        End Sub

        '-----------------------------------------------------------
        '界面功能的初始化
        '-----------------------------------------------------------
        Private Sub InitialFuncShow()
            '----------------------------------------------------------
            '检查是否支持人员关联表的“字段设置”和“显示设置”
            Dim bln As Boolean = CmsFunc.IsEnable("FUNC_EMP_COLMGR")
            lbtnColumnSet.Enabled = bln
            lbtnColshowSet.Enabled = bln
            lbtnInputformDesign.Enabled = bln
            '----------------------------------------------------------

            '----------------------------------------------------------
            '当前选中部门如果是企业，则所有人员管理功能禁止使用
            If RLng("depid") = 0 Then
                'lnkAdd.Enabled = False
                'lnkEdit.Enabled = False
                'lnkDelete.Enabled = False
                'lbtnChangeDep.Enabled = False

                ''lbtnSearch.Enabled = True
                '' lbtnCancelSearch.Enabled = False

                'lnkSetPassword.Enabled = False
                'lbtnClearPassword.Enabled = False
                '' lbtnImport.Enabled = False

                ''lbtnProjRightsForEmp.Enabled = False
                ''lbtnProjRightsForDep.Enabled = False
            End If
            '----------------------------------------------------------
        End Sub

        '----------------------------------------------------------
        '创建DataGrid的列字段和填充内容
        '----------------------------------------------------------
        Private Sub ShowTableOfEmployee(ByVal lngDepID As Long, Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
            Try
                Dim strWhere As String = VStr("CMSPAGE_EMP_WHERE") '必须在此保存查询条件
                Dim strOrderBy As String = VStr("CMSPAGE_EMP_ORDERBY")

                '--------------------------------------------------------------------------------------------
                '绑定数据前的分页控件的处理
                Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '设置页数
                dgEmployee.CurrentPageIndex = 0 'Cmspager1.CurrentPage

                Session("CMS_EMPTABLE_PAGING") = Cmspager1.CurrentPage
                '--------------------------------------------------------------------------------------------

                Dim intTotalRecNum As Integer = 0
                Dim ds As DataSet = ResFactory.TableService("EMP").GetHostTableData(CmsPass, lngDepID, strWhere, strOrderBy, , Cmspager1.RecStartOnCurPage, Cmspager1.PageRows, intTotalRecNum)
                Cmspager1.TotalRecordNumber = intTotalRecNum

                Dim dv As DataView = ds.Tables(0).DefaultView
                dgEmployee.VirtualItemCount = dv.Count
                dgEmployee.DataSource = dv
                dgEmployee.DataBind()
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

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
            strbScript.Append("        self.document.forms(0).RECID.value = src.RECID;showControls(src);")
            strbScript.Append("     }")


            strbScript.Append("function IsShowCheckbox(id)")
            strbScript.Append("{")
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
            strbScript.Append("}")

            strbScript.Append("</script>")
            Response.Write(strbScript.ToString())

            RegisterHiddenField("RECID", "")
        End Sub


        Protected Sub dgEmployee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEmployee.ItemDataBound

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                For i As Integer = 0 To e.Item.Controls.Count - 1
                    For j As Integer = 0 To e.Item.Controls(i).Controls.Count - 1
                        Dim cid As String = e.Item.Controls(i).Controls(j).ClientID
                        If cid.IndexOf("cbxStatus") > 0 Then
                            ' Dim IsChecked As Boolean = Convert.ToBoolean(IIf(DbField.GetInt(drv, "Status") > 0, True, False))
                            CType(e.Item.FindControl("cbxStatus"), CheckBox).Checked = DbField.GetBool(drv, "Status")
                        End If
                    Next
                Next
            End If
        End Sub
    End Class
End Namespace
