Option Strict On
Option Explicit On 

Imports System.Text

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmContent
    Inherits Cms.Web.CmsFrmContentBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents panelColumnSet As System.Web.UI.WebControls.Panel
    Protected WithEvents panelColshowSet As System.Web.UI.WebControls.Panel
    Protected WithEvents panelInputformDesign As System.Web.UI.WebControls.Panel
    Protected WithEvents lbtnProjRightsForDep As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnProjRightsForEmp As System.Web.UI.WebControls.LinkButton

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    'ItemCreated�¼��ᱻ����2�Σ���һ������ͼ״̬�ָ�ʱ��û�������һ�Σ����ش����ڶ������������HTMLҳ��ǰ������Ӧ�ô���
    Private m_intItemCreatedCounter As Integer = 0

    Private Const BACK_PAGE As String = "/cmsweb/adminsys/EmployeeFrmContent.aspx"

        '��ǰѡ��Ĳ���ID
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' If IsPostBack Then Return


        End Sub

        Protected Overrides Sub CmsPageSaveParametersToViewState()
            MyBase.CmsPageSaveParametersToViewState()
        End Sub

        Protected Overrides Sub CmsPageInitialize()
            If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then 'ֻ��ϵͳ����Ա�Ͳ��Ź���Ա�ܽ�����Ա����
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If

            lnkDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����Ա��');")

            RegisterScriptOfRowClick() '��ҳ����ע�᣺�м�¼����¼���JavaScript
        End Sub

        Protected Overrides Sub CmsPageDealFirstRequest()
            If CmsPass.EmpIsSysAdmin = True Or OrgFactory.DepDriver.GetDepAdmin(CmsPass, RLng("depid"), True).Trim = CmsPass.Employee.ID.Trim Then

                Cmspager1.CurrentPage = 0
                Session("CMS_EMPTABLE_PAGING") = 0
                Try
                    If RStr("cmsaction") = "seldep" Then 'ѡ���ź������Ա��������
                        Try
                            Dim lngNewDep As Long = RLng("tmpdepid")
                            If lngNewDep <> 0 Then
                                'OrgFactory.EmpDriver.ChangeDepartment(CmsPass, SLng("CMSEMPMGR_EMPID"), lngNewDep)
                                'Session("CMSEMPMGR_EMPID") = Nothing
                            Else
                                PromptMsg("�����ƶ���Ա����ҵ֮�£�")
                            End If
                        Catch ex As Exception
                            PromptMsg("��������ʧ�ܣ�", ex, True)
                        End Try
                    End If

                    Session("CMS_EMPSEARCH_COLTYPE") = WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsResID.Employee, ddlColumns, True, True, True)      '��ʼ��DropDownList���ֶ��б���
                    WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions, False, 80)  '��ʼ��DropDownList�в���������

                    '---------------------------------------------------------------
                    '��ʼ����ҳ�ؼ�
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

                    InitialFuncShow() '���湦�ܵĳ�ʼ��

                    ShowTableOfEmployee(RLng("depid")) '����������
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
            InitialFuncShow() '���湦�ܵĳ�ʼ��
        End Sub

        Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgEmployee.Init
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If

            Try
                CmsPageSaveParametersToViewState()

                '��ʼ��DataGrid
                WebUtilities.InitialDataGrid(dgEmployee, CmsConfig.GetLong("SYS_CONFIG", "TABLEROWS_EMP"), True, True, False, True)      '����DataGrid��ʾ����

                '����DataGrid���Զ����С�������DataGrid1_Init()�д����У�����֮���ܽ���PageIndexChanged��SortCommand�ȵ��¼����
                Dim dv As DataView = ResFactory.TableService("EMP").GetTableColumns(CmsPass, CmsResID.Employee)
                WebUtilities.LoadResTableColumns(CmsPass, CmsResID.Employee, dgEmployee, dv)
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEmployee.ItemCreated
            '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ����
            '2���ִ�������ʹ�ã�1��Javascript������2�����һ��hidden������<input type="hidden" name="RECID">
            If dgEmployee.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                'ItemCreated�¼��ᱻ����2�Σ���һ������ͼ״̬�ָ�ʱ��û�������һ�Σ����ش����ڶ������������HTMLҳ��ǰ������Ӧ�ô���
                If m_intItemCreatedCounter < 1 Then
                    m_intItemCreatedCounter += 1
                    Return
                End If

                Dim i As Integer = 0
                For i = 0 To dgEmployee.Items.Count - 1
                    Dim index As Integer = 0
                    Dim row As DataGridItem = dgEmployee.Items(i) 

                    Dim strAiid As String = row.Cells(0).Text.Trim() '��1�б����Ǽ�¼ID
                    row.Attributes.Add("RECID", strAiid) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("USERCODE", dgEmployee.DataKeys(i).ToString()) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()
                Next
            End If
        End Sub

        Private Sub dgEmployee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEmployee.SortCommand
            'ÿ�ε���滻�������
            ViewState("CMSPAGE_EMP_ORDERBY_TYPE") = CStr(IIf(VStr("CMSPAGE_EMP_ORDERBY_TYPE") = "ASC", "DESC", "ASC"))
            ViewState("CMSPAGE_EMP_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("CMSPAGE_EMP_ORDERBY_TYPE")
            ShowTableOfEmployee(RLng("depid"))
        End Sub

        Private Sub Cmspager1_Click(ByVal sender As Object, ByVal eventArgument As String) Handles Cmspager1.Click
            ShowTableOfEmployee(RLng("depid"), eventArgument)
        End Sub

        Private Sub lbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSearch.Click
            '��֯Where���
            Dim hashColType As Hashtable = CType(Session("CMS_EMPSEARCH_COLTYPE"), Hashtable)
            Dim lngColType As Long = CLng(hashColType(ddlColumns.SelectedItem.Value))
            Dim strCondition As String = ddlConditions.SelectedItem.Value.Trim()
            Dim strWhere As String = CTableStructure.GenerateFieldWhere(ddlColumns.SelectedItem.Value, txtSearchValue.Text.Trim(), lngColType, strCondition, SDbConnectionPool.GetDbConfig().DatabaseType)
            ViewState("CMSPAGE_EMP_WHERE") = strWhere

            Cmspager1.CurrentPage = 0
            Session("CMS_EMPTABLE_PAGING") = 0

            '��ʾ�б�
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
                PromptMsg("����ѡ����Ҫ��������Ա��¼")
                Return
            End If

            Session("CMSBP_RecordEdit") = CmsUrl.AppendParam(BACK_PAGE, "depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/cmshost/RecordEdit.aspx?mnuhostresid=" & CmsResID.Employee & "&mnuhostrecid=&mnuresid=" & CmsResID.Employee & "&mnurecid=" & lngRecID & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & InputMode.EditInHostTable & "&depid=" & RLng("depid"), False)
        End Sub

        Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("����ѡ����Ҫ��������Ա��¼")
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
                PromptMsg("����ѡ����Ҫ��������Ա��¼")
                Return
            End If

            Session("CMSEMPMGR_EMPID") = lngRecID '�л�ҳ��ǰ�ȱ�����ǰ��ԱID
            Session("CMSBP_DepartmentSelect") = CmsUrl.AppendParam(BACK_PAGE, "employeeid=" & lngRecID & "&depid=" & RLng("depid"))
            Response.Redirect("/cmsweb/adminsys/DepartmentSelect.aspx?employeeid=" & lngRecID & "&depid=" & RLng("depid"), False)
        End Sub

        Private Sub lnkSetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSetPassword.Click
            Dim lngRecID As Long = RLng("RECID")
            If lngRecID <= 0 Then
                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("����ѡ����Ҫ��������Ա��¼")
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
                PromptMsg("����ѡ����Ҫ��������Ա��¼")
                Return
            End If

            Try
                'OrgFactory.EmpDriver.UpdatePassword(CmsPass.Employee.ID, CmsPass.Employee.Password, "")   '�������
                OrgFactory.EmpDriver.SetPass(CmsPass, OrgFactory.EmpDriver.GetEmpID(CmsPass, lngRecID), "")

                ShowTableOfEmployee(RLng("depid"))
                PromptMsg("������ճɹ���")
            Catch ex As Exception
                PromptMsg("�������ʧ�ܣ��޷��������ݿ⣡")
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
                PromptMsg("����ѡ����Ҫ��������Ա��¼")
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
        '���湦�ܵĳ�ʼ��
        '-----------------------------------------------------------
        Private Sub InitialFuncShow()
            '----------------------------------------------------------
            '����Ƿ�֧����Ա������ġ��ֶ����á��͡���ʾ���á�
            Dim bln As Boolean = CmsFunc.IsEnable("FUNC_EMP_COLMGR")
            lbtnColumnSet.Enabled = bln
            lbtnColshowSet.Enabled = bln
            lbtnInputformDesign.Enabled = bln
            '----------------------------------------------------------

            '----------------------------------------------------------
            '��ǰѡ�в����������ҵ����������Ա�����ܽ�ֹʹ��
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
        '����DataGrid�����ֶκ��������
        '----------------------------------------------------------
        Private Sub ShowTableOfEmployee(ByVal lngDepID As Long, Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
            Try
                Dim strWhere As String = VStr("CMSPAGE_EMP_WHERE") '�����ڴ˱����ѯ����
                Dim strOrderBy As String = VStr("CMSPAGE_EMP_ORDERBY")

                '--------------------------------------------------------------------------------------------
                '������ǰ�ķ�ҳ�ؼ��Ĵ���
                Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '����ҳ��
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
    '��ҳ����ע�᣺�м�¼����¼���JavaScript
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
