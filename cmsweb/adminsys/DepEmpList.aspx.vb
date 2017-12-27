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



#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        RegisterScriptOfRowClick() '��ҳ����ע�᣺�м�¼����¼���JavaScript
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("nodep") = "yes" Then
            btnChooseDep.Visible = False
            btnChooseEnterprise.Visible = False
        End If
        If RStr("noemp") = "yes" Then btnChooseEmp.Visible = False

        '---------------------------------------------------------------
        '��ʼ����ҳ�ؼ�
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

        ShowTableOfEmployee(RLng("depid")) '����������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub Cmspager1_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager1.Click
        ShowTableOfEmployee(RLng("depid"), eventArgument) '����������
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
                ShowTableOfEmployee(lngDepID)  'Ϊ����ʾ��Ա��ṹ

                PromptMsg("��ѡ����Ա")
            End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        Try
            CmsPageSaveParametersToViewState()

            WebUtilities.InitialDataGrid(DataGrid1, CmsConfig.GetLong("SYS_CONFIG", "TABLEROWS_EMP"), True, True, False, True) '����DataGrid��ʾ����
            'Dim dv As DataView = ResFactory.TableService("EMP").GetTableColumns(CmsPass, CmsResID.Employee)
            'WebUtilities.LoadResTableColumns(CmsPass, CmsResID.Employee, DataGrid1, dv)
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ����
        '2���ִ�������ʹ�ã�1��Javascript������2�����һ��hidden������<input type="hidden" name="RECID">
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            'ItemCreated�¼��ᱻ����2�Σ���һ������ͼ״̬�ָ�ʱ��û�������һ�Σ����ش����ڶ������������HTMLҳ��ǰ������Ӧ�ô���
            If m_intItemCreatedCounter < 1 Then
                m_intItemCreatedCounter += 1
                Return
            End If

            Dim i As Integer = 0
            For i = 0 To DataGrid1.Items.Count - 1
                Dim row As DataGridItem = DataGrid1.Items(i)
                    Dim strAiid As String = row.Cells(0).Text.Trim() '��1�б����Ǽ�¼ID
                    row.Attributes.Add("RECID", strAiid) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("USERCODE", row.Cells(2).Text.Trim.ToLower()) '�ڿͻ��˱����¼ID
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()
            Next
        End If
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        ShowTableOfEmployee(RLng("depid"))
    End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        'ÿ�ε���滻�������
        ViewState("CMSPAGE_EMP_ORDERBY_TYPE") = CStr(IIf(VStr("CMSPAGE_EMP_ORDERBY_TYPE") = "ASC", "DESC", "ASC"))
        ViewState("CMSPAGE_EMP_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("CMSPAGE_EMP_ORDERBY_TYPE")
        ShowTableOfEmployee(RLng("depid"))
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
            Dim intWidth As Integer = 0


            Dim col As BoundColumn = New BoundColumn
            col.HeaderText = "ID" '�ؼ��ֶ�
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
            col.HeaderText = "��Ա�ʺ�"
            col.DataField = "EMP_ID"
            col.ItemStyle.Width = Unit.Pixel(100)
            DataGrid1.Columns.Add(col)
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "��Ա����"
            col.DataField = "EMP_NAME"
            col.ItemStyle.Width = Unit.Pixel(120)
            DataGrid1.Columns.Add(col)
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "�ֻ�"
            col.DataField = "EMP_HANDPHONE"
            col.ItemStyle.Width = Unit.Pixel(100)
            DataGrid1.Columns.Add(col)
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "�����ʼ�"
            col.DataField = "EMP_EMAIL"
            col.ItemStyle.Width = Unit.Pixel(160)
            DataGrid1.Columns.Add(col)
            intWidth += 120

            DataGrid1.Width = Unit.Pixel(intWidth)
        End Sub

    '----------------------------------------------------------
    '����DataGrid�����ֶκ��������
    '----------------------------------------------------------
        Private Sub ShowTableOfEmployee(ByVal lngDepID As Long, Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
            Try
                ' If CmsPass.EmpIsSysAdmin Or OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID, True).Trim = CmsPass.Employee.ID.Trim Then
                '--------------------------------------------------------------------------------------------
                '������ǰ�ķ�ҳ�ؼ��Ĵ���
                Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '����ҳ��
                DataGrid1.CurrentPageIndex = 0
                '--------------------------------------------------------------------------------------------

                Dim intTotalRecNum As Integer = 0
                Dim strOrderBy As String = VStr("CMSPAGE_EMP_ORDERBY")
                Dim ds As DataSet = GetDataset(lngDepID, strHostPageCommand, Condition)



                '�����ݼ�
                Dim dv As DataView = ds.Tables(0).DefaultView
                DataGrid1.VirtualItemCount = dv.Count '��ҳ��
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
                ' End If
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

    '----------------------------------------------------------
    '����DataGrid�����ֶκ��������
    '----------------------------------------------------------
        Private Function GetDataset(ByVal lngDepID As Long, ByVal strHostPageCommand As String, ByVal Condition As String) As DataSet
            Dim intTotalRecNum As Integer = 0
            Dim strOrderBy As String = VStr("CMSPAGE_EMP_ORDERBY")
            Dim ds As DataSet = Nothing
            If OrgFactory.DepDriver().IsVirtualDep(CmsPass, lngDepID) = True Then
                '�����ⲿ��
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
