Option Strict On
Option Explicit On 

Imports System.IO
Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldGetAdvDictionary
    Inherits CmsPage

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

    '��Щ����ֻ��ͬһ����������Ч
    Private m_dvCols As DataView = Nothing '�߼��ֵ䶨����ֶ��б�
    Private m_lngMainResID As Long = 0
    Private m_lngDictResID As Long = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        End Sub




    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If Not Request.QueryString("mode") Is Nothing AndAlso Request.QueryString("mode").ToString() <> "" Then
            ViewState("lngMode") = RStr("mode")
            If Request.QueryString("mode").ToString() = "undefined" Then
                ViewState("lngMode") = "2"
            End If
        Else
            ViewState("lngMode") = "2"
        End If


        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
        If VStr("PAGE_COLVALUE") = "" Then
            'ViewState("PAGE_COLVALUE") = RStr("colval")
            Dim strTemp As String = RStr("colval")
            ViewState("PAGE_COLVALUE") = strTemp.Replace("x__plus", "+")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        LoadRowClickScript() '���ÿͻ���Javascript

        lbtnCancelSearch.ToolTip = "ȡ����ѯ������"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        ViewState("CMS_ADVDICT_SEARCH_COLTYPE") = WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_DICTRESID"), ddlColumns, True, False, False) '��ʼ��DropDownList���ֶ��б���
        ddlColumns.SelectedValue = RStr("ddlColumns")
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions, True)   '��ʼ��DropDownList�в���������

        '�鿴���޸߼��ֵ�Ĺ����ֶζ���
        Dim strFilterWhere As String = ""
        Dim strFilterCol As String = RStr("filtercol1")
        Dim strFilterColVal As String = RStr("filtercolval1")
        If strFilterCol <> "" AndAlso strFilterColVal <> "" Then strFilterWhere = strFilterCol & "='" & strFilterColVal & "'"
        strFilterCol = RStr("filtercol2")
        strFilterColVal = RStr("filtercolval2")
        If strFilterCol <> "" AndAlso strFilterColVal <> "" Then strFilterWhere = CmsWhere.AppendAnd(strFilterWhere, strFilterCol & "='" & strFilterColVal & "'")
        strFilterCol = RStr("filtercol3")
        strFilterColVal = RStr("filtercolval3")
        If strFilterCol <> "" AndAlso strFilterColVal <> "" Then strFilterWhere = CmsWhere.AppendAnd(strFilterWhere, strFilterCol & "='" & strFilterColVal & "'")

        ViewState("PAGE_FILTER_WHERE") = strFilterWhere

        '�Զ��ȶ�λ�����������ֵ��ֶ���ָ����ֵ
        Dim strWhere As String = ""
        Dim strDictColName As String = CTableColAdvDictionary.GetMainColumnInDictRes(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        If VStr("PAGE_COLVALUE") <> "" Then
            ddlColumns.SelectedValue = strDictColName
            ddlConditions.SelectedValue = "LIKE"
            txtSearchValue.Text = VStr("PAGE_COLVALUE")
            strWhere = strDictColName & " LIKE '%" & VStr("PAGE_COLVALUE") & "%'"
            End If

            lbtnSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_START")
            lbtnCancelSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_CANCEL")
            lnkOK.Text = CmsMessage.GetUI(CmsPass, "TITLE_CONFIRM")
            lnkCancel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CANCEL")

        '---------------------------------------------------------------
        '��ʼ����ҳ�ؼ�
        Cmspager1.PageRows = CmsConfig.GetInt("SYS_CONFIG", "TABLEROWS_DICT")
        Cmspager1.Language = CmsPass.Employee.Language
        Cmspager1.BgColor = "#e7ebef"
        Cmspager1.TableAlign = "left"
        Cmspager1.ButtonAlign = "left"
        Cmspager1.WordAlign = "left"
        Cmspager1.TableHeight = "25px"
        Cmspager1.TotalWidth = "100%"
        Cmspager1.ButtonsWidth = "130px"
        '---------------------------------------------------------------

        ViewState("PAGE_WHERE") = strWhere
        GridDataBind() '����������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            WebUtilities.InitialDataGrid(DataGrid1, CmsConfig.GetInt("SYS_CONFIG", "TABLEROWS_DICT"), True)  '����DataGrid��ʾ����
            CreateDataGridColumn() '�����б�
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim i As Integer = 0

            '-----------------------------------------------------------
            '�ҵ�����ɫ���õļ����е����
            Dim datRes As DataResource = ResFactory.ResService.GetOneResource(CmsPass, VLng("PAGE_DICTRESID"))
            Dim intCol1() As Integer = {-1, -1, -1}
            Dim intCol1Type() As Integer = {0, 0, 0}
            Dim intCol2() As Integer = {-1, -1, -1}
            Dim intCol2Type() As Integer = {0, 0, 0}
            Dim intCol3() As Integer = {-1, -1, -1}
            Dim intCol3Type() As Integer = {0, 0, 0}
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim bc As BoundColumn = CType(DataGrid1.Columns(i), BoundColumn)
                Dim j As Integer
                For j = 0 To 2
                    If datRes.RowColorCol1(j) <> "" And bc.DataField = datRes.RowColorCol1(j) Then
                        intCol1(j) = i
                        If IsNumeric(bc.FooterText) Then intCol1Type(j) = CInt(bc.FooterText)
                    End If
                Next
                For j = 0 To 2
                    If datRes.RowColorCol2(j) <> "" And bc.DataField = datRes.RowColorCol2(j) Then
                        intCol2(j) = i
                        If IsNumeric(bc.FooterText) Then intCol2Type(j) = CInt(bc.FooterText)
                    End If
                Next
                For j = 0 To 2
                    If datRes.RowColorCol3(j) <> "" And bc.DataField = datRes.RowColorCol3(j) Then
                        intCol3(j) = i
                        If IsNumeric(bc.FooterText) Then intCol3Type(j) = CInt(bc.FooterText)
                    End If
                Next
            Next
            '-----------------------------------------------------------

            For i = 0 To DataGrid1.Items.Count - 1
                    Dim row As DataGridItem = DataGrid1.Items(i)


                '-----------------------------------------------------------
                '�������ֶ�ֵд���������У����ڡ�ȷ��������ȡ
                Dim j As Integer = 0
                Dim drv As DataRowView
                For Each drv In m_dvCols
                    If j < DataGrid1.Columns.Count Then
                        Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                        Dim strMainCtrl As String = ""
                        If strMainCol <> "" Then strMainCtrl = TextboxName.GetCtrlName(strMainCol, m_lngMainResID)
                        Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                        Dim strDictCtrl As String = ""
                        If strDictCol <> "" Then strDictCtrl = TextboxName.GetCtrlName(strDictCol, m_lngDictResID)

                        Dim strTemp As String = row.Cells(j).Text.Trim()
                        If strTemp = "&nbsp;" OrElse strTemp = "&amp;nbsp;" Then
                            strTemp = ""
                        End If
                        If strMainCtrl <> "" Then row.Attributes.Add(strMainCtrl, strTemp) '�ڿͻ��˱����¼ID
                            If strDictCol <> "" Then row.Attributes.Add(strDictCtrl, strTemp) '�ڿͻ��˱����¼ID
                            'row.Attributes.Add("RecID", DbField.GetStr(drv, "ID"))
                        j += 1
                    Else
                        Exit For
                    End If
                Next
                '-----------------------------------------------------------

                '-----------------------------------------------------------
                '������������������ɫ
                Dim blnSet As Boolean = False
                blnSet = ResTableRowColor.SetRowColor(datRes, row, intCol1, intCol1Type, 1)
                If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol2, intCol2Type, 2)
                If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol3, intCol3Type, 3)
                '-----------------------------------------------------------



            Next
        End If
    End Sub

    'Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    DataGrid1.CurrentPageIndex = e.NewPageIndex

    '    GridDataBind()  '����������
    'End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        'ÿ�ε���滻�������
        If VStr("PAGE_ORDERBY_TYPE") = "ASC" Then
            ViewState("PAGE_ORDERBY_TYPE") = "DESC"
        Else
            ViewState("PAGE_ORDERBY_TYPE") = "ASC"
        End If
        '������ʾ����
        ViewState("PAGE_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("PAGE_ORDERBY_TYPE")

        GridDataBind()
    End Sub

    Private Sub Cmspager1_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager1.Click
        GridDataBind(eventArgument) '����������
    End Sub

    Private Sub lbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSearch.Click
        DataGrid1.CurrentPageIndex = 0 '��ѯǰ��������Ϊ��ҳ

        '��֯Where���
        Dim hashColType As Hashtable = CType(ViewState("CMS_ADVDICT_SEARCH_COLTYPE"), Hashtable)
        Dim strColumns As String = ddlColumns.SelectedValue
        Dim lngColType As Long = CLng(hashColType(strColumns))
        Dim strWhere As String = CTableStructure.GenerateFieldWhere(strColumns, txtSearchValue.Text.Trim(), lngColType, ddlConditions.SelectedValue, SDbConnectionPool.GetDbConfig().DatabaseType)
        ViewState("PAGE_WHERE") = strWhere

        GridDataBind()  '����������
    End Sub

    Private Sub lbtnCancelSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCancelSearch.Click
        ViewState("PAGE_WHERE") = ""
        ViewState("PAGE_ORDERBY") = ""
        ddlColumns.SelectedValue = ""
        txtSearchValue.Text = ""
        ddlConditions.SelectedValue = ""
        GridDataBind()  '����������
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Try
            DataGrid1.AutoGenerateColumns = False
            DataGrid1.Columns.Clear()

            m_lngMainResID = VLng("PAGE_RESID")

            Dim col As BoundColumn
            Dim ds As DataSet = CTableColAdvDictionary.GetDictionaryByMatchAndReference(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            m_dvCols = ds.Tables(0).DefaultView
                If m_dvCols.Count > 0 Then
                    m_lngDictResID = DbField.GetLng(m_dvCols(0), "CDZ2_RESID2") '��ȡ�ֵ������ԴID
                    If DbField.GetBool(m_dvCols(0), "CDZ2_ADD_DICTREC") Or DbField.GetBool(m_dvCols(0), "CDZ2_EDIT_DICTREC") Then
                        imgEdit.Visible = True
                        If DbField.GetBool(m_dvCols(0), "CDZ2_ADD_DICTREC") Then lbtnAdd.Visible = True
                        If DbField.GetBool(m_dvCols(0), "CDZ2_EDIT_DICTREC") Then lbtnEdit.Visible = True
                    End If
                End If
            ViewState("PAGE_DICTRESID") = m_lngDictResID

            '��ȡָ���û������ֶ�Ȩ����Ϣ
            Dim strColsOfNoRights As String = CmsRights.GetRightsData(CmsPass, m_lngDictResID, CmsPass.Employee.ID).strQX_ACCESS_COLS
            If strColsOfNoRights <> "" Then strColsOfNoRights = "," & strColsOfNoRights & ","

            Dim strResTableType As String = CmsPass.GetDataRes(m_lngMainResID).ResTableType
            Dim strBriefLanguage As String = CmsMessage.GetBriefLanguage(CmsPass.Employee.Language)

            Dim dvDict As DataView = ResFactory.TableService(CmsPass.GetDataRes(m_lngDictResID).ResTableType).GetTableColumns(CmsPass, m_lngDictResID)
            'Dim dvDict As DataView = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "select * from CMS_TABLE_SHOW inner join cms_table_define on(cd_resid=cs_resid and cd_colName=cs_colname) where CS_RESID=" & m_lngDictResID & " AND CD_RESID=" & m_lngDictResID & "  order by CS_SHOW_ORDER ASC").Tables(0).DefaultView()
            Dim strRowFilter As String = dvDict.RowFilter.Replace("AND CS_SHOW_ENABLE=1", "")
            Dim intWidth As Integer = 0
            Dim drv As DataRowView
            For Each drv In m_dvCols
                Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                'Dim intDictType As ADVDICT_COLTYPE = CType(DbField.GetInt(drv, "CDZ2_TYPE"), ADVDICT_COLTYPE)

                Dim blnShowCol As Boolean = True
                If strColsOfNoRights <> "" Then
                    If strColsOfNoRights.IndexOf("," & strDictCol & ",") >= 0 Then blnShowCol = False '��Ȩ�ޣ����������ֶ�
                End If
                If blnShowCol = True Then
                    If strRowFilter = "" Then
                        dvDict.RowFilter = "CS_COLNAME='" & strDictCol & "'"
                    Else
                        dvDict.RowFilter = strRowFilter & " and CS_COLNAME='" & strDictCol & "'"
                    End If

                    'If dvDict.Count > 0 AndAlso (intDictType = ADVDICT_COLTYPE.Matching OrElse intDictType = ADVDICT_COLTYPE.Reference) Then
                    If dvDict.Count > 0 Then
                        If DbField.GetStr(dvDict(0), "CS_SHOW_ENABLE") = "1" Then
                            intWidth += WebUtilities.CreateOneColumn(DataGrid1, dvDict(0), "", True, False, "", strBriefLanguage, strResTableType)  'ֻ��ʾ���ֵ��г��ֵ��ֶ�
                        Else
                            WebUtilities.CreateOneColumn(DataGrid1, dvDict(0), "", True, False, "", strBriefLanguage, strResTableType, False) 'ֻ��ʾ���ֵ��г��ֵ��ֶ�
                        End If
                    End If
                End If
            Next
            DataGrid1.Width = Unit.Pixel(intWidth)
        Catch ex As Exception
            SLog.Err("�ڸ߼��ֵ��д������ֶ��쳣ʧ�ܣ�", ex)
            Throw New CmsException("�߼��ֵ䶨���д������飡")
        End Try
    End Sub

    '--------------------------------------------------------------------------------------------
    '����DataGrid�����ֶκ��������
    '--------------------------------------------------------------------------------------------
    Public Sub GridDataBind(Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
        If VLng("PAGE_DICTRESID") = 0 Then Return
        Try
            '--------------------------------------------------------------------------------------------
            '������ǰ�ķ�ҳ�ؼ��Ĵ���
            Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '����ҳ��
            DataGrid1.CurrentPageIndex = 0
            '--------------------------------------------------------------------------------------------

                '��װ�������
                Dim ResID As Long = VLng("PAGE_RESID")
                Dim datRes As DataResource = CmsPass.GetDataRes(ResID)

                If datRes.ResType = ResInheritType.IsInherit Then
                    ResID = datRes.IndepParentResID
                End If

                Dim strWhere As String = MultiTableSearch.AssembleAdvDictWhereForSqlQuery(CmsPass, ResID, VStr("PAGE_COLNAME"))
                strWhere = CmsWhere.FilterWhere(CmsPass, strWhere)
                strWhere = CmsWhere.AppendAnd(strWhere, VStr("PAGE_WHERE"))
                strWhere = CmsWhere.AppendAnd(strWhere, VStr("PAGE_FILTER_WHERE"))  

                '������
                Dim strResTableType As String = ResFactory.ResService.GetResTableType(CmsPass, VLng("PAGE_DICTRESID"))
                Dim intTotalRecNum As Integer = 0
                Dim ds As DataSet = ResFactory.TableService(strResTableType).GetDict2TableData(CmsPass, VLng("PAGE_DICTRESID"), strWhere, VStr("PAGE_ORDERBY"), Cmspager1.RecStartOnCurPage, Cmspager1.PageRows, intTotalRecNum)
                Cmspager1.TotalRecordNumber = intTotalRecNum

                '�����ݼ�
                Dim dv As DataView = ds.Tables(0).DefaultView
                DataGrid1.VirtualItemCount = dv.Count '��ҳ��
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
    End Sub

    Private Sub LoadRowClickScript()
        If Not IsStartupScriptRegistered("RowClick") Then
            Dim strScript As String = ""
            strScript &= "<script language=""javascript"">" & Environment.NewLine
            strScript &= "<!--" & Environment.NewLine
                strScript &= "function RowLeftClickNoPost(src){" & Environment.NewLine
                strScript &= "var o = document.getElementById(src);    " & Environment.NewLine
                strScript &= "var trs = document.getElementById('DataGrid1').rows; " & Environment.NewLine
                strScript &= "for(var i=1;i<trs.length;i++)  " & Environment.NewLine
                strScript &= "{  " & Environment.NewLine
                strScript &= "trs[i].style.backgroundColor=""white"";  " & Environment.NewLine
                strScript &= "}  " & Environment.NewLine
                strScript &= "o.style.backgroundColor= '#C4D9F9';" & Environment.NewLine
                strScript &= "document.getElementById('txtRecID').value=src;" & Environment.NewLine
                Dim drv As DataRowView
                For Each drv In m_dvCols
                    Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                    Dim strMainCtrl As String = ""
                    If strMainCol <> "" Then strMainCtrl = TextboxName.GetCtrlName(strMainCol, m_lngMainResID)
                    Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                    Dim strDictCtrl As String = ""
                    If strDictCol <> "" Then strDictCtrl = TextboxName.GetCtrlName(strDictCol, m_lngDictResID)
                    ' Dim strRecID As String = DbField.GetStr(drv, "ID")
                    'event.srcElement.getAttribute("link") 
                    If strMainCtrl <> "" Then
                        strScript &= "    self.document.forms(0)." & strMainCtrl & ".value = o.attributes.getNamedItem('" & strMainCtrl & "').nodeValue ;" & Environment.NewLine
                        RegisterHiddenField(strMainCtrl, "") 'ע��ͻ���Form��������
                    End If
                    If strDictCtrl <> "" Then
                        strScript &= "    self.document.forms(0)." & strDictCtrl & ".value = o.attributes.getNamedItem('" & strDictCtrl & "').nodeValue ;" & Environment.NewLine
                        RegisterHiddenField(strDictCtrl, "") 'ע��ͻ���Form��������
                    End If
                Next
                 

                strScript &= "}" & Environment.NewLine
                strScript &= "" & Environment.NewLine
                strScript &= "function ReturnSelectedValue(){" & Environment.NewLine
                strScript &= "    var ctlName;" & Environment.NewLine

                If CType(ViewState("lngMode"), Long) = InputMode.ViewInHostTable OrElse CType(ViewState("lngMode"), Long) = InputMode.ViewInRelTable Then
                    strScript &= " " & Environment.NewLine
                Else
                    For Each drv In m_dvCols
                        Dim intDictType As ADVDICT_COLTYPE = CType(DbField.GetInt(drv, "CDZ2_TYPE"), ADVDICT_COLTYPE)
                        Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                        Dim strMainCtrl As String = ""
                        If strMainCol <> "" Then strMainCtrl = TextboxName.GetCtrlName(strMainCol, m_lngMainResID)
                        Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                        Dim strDictCtrl As String = ""
                        If strDictCol <> "" Then strDictCtrl = TextboxName.GetCtrlName(strDictCol, m_lngDictResID)

                        '�����ֶεĸ�ֵ��ֻ���ƥ���ֶθ�ֵ
                        If intDictType = ADVDICT_COLTYPE.Matching Then
                            If strMainCtrl <> "" Then
                                strScript &= "    try{" & Environment.NewLine
                                strScript &= "        if (self.document.forms(0)." & strMainCtrl & ".value != ""&nbsp;""){" & Environment.NewLine '&nbsp;
                                strScript &= "            ctlName=eval(""window.opener.Form1." & strMainCtrl & """)" & Environment.NewLine
                                strScript &= "            ctlName.value = self.document.forms(0)." & strMainCtrl & ".value" & Environment.NewLine
                                strScript &= "        }" & Environment.NewLine
                                strScript &= "    }catch(ex){" & Environment.NewLine
                                strScript &= "    }" & Environment.NewLine
                            End If
                        End If

                        '���������ֶεĸ�ֵ������ƥ���ֶκͲο��ֶθ�ֵ
                        If strDictCtrl <> "" Then
                            strScript &= "    try{" & Environment.NewLine
                            strScript &= "        if (self.document.forms(0)." & strDictCtrl & ".value != ""&nbsp;""){" & Environment.NewLine '&nbsp;
                            strScript &= "            ctlName=eval(""window.opener.Form1." & strDictCtrl & """)" & Environment.NewLine
                            strScript &= "            ctlName.value = self.document.forms(0)." & strDictCtrl & ".value" & Environment.NewLine
                            strScript &= "        }" & Environment.NewLine
                            strScript &= "    }catch(ex){" & Environment.NewLine
                            strScript &= "    }" & Environment.NewLine
                        End If
                    Next
                End If

                strScript &= "    window.close();" & Environment.NewLine
                strScript &= "}" & Environment.NewLine
                strScript &= "-->" & Environment.NewLine
                strScript &= "</script>" & Environment.NewLine
                strScript &= "" & Environment.NewLine

                RegisterStartupScript("RowClick", strScript)
            End If
    End Sub

        Protected Sub lbtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnAdd.Click
            Dim strUrl As String = "../cmshost/RecordEdit.aspx?mnuinmode=2&timeid=2648484618&MenuSection=&MenuKey=&MNURESLOCATE=1&cmsaction=&mnuformresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostrecid=&mnurecid=&mnuresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuformname="
            Response.Write("<script>window.location.href='" + strUrl + "&backpage='+window.location.href.replace(/\&/g,'[and]');</script>")
        End Sub

        Protected Sub lbtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnEdit.Click
            If txtRecID.Text.Trim() <> "" Then
                Dim strUrl As String = "../cmshost/RecordEdit.aspx?mnuinmode=4&timeid=567072032&MenuSection=&MenuKey=&MNURESLOCATE=1&cmsaction=&mnuformresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostrecid=&mnurecid=" + txtRecID.Text.Trim() + "&mnuresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuformname="
                Response.Write("<script>window.location.href='" + strUrl + "&backpage='+window.location.href.replace(/\&/g,'[and]');</script>")
            Else
                Response.Write("<script>alert('��ѡ����Ч��¼��');</script>")
            End If
        End Sub

        Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                e.Item.Attributes.Add("ID", DbField.GetStr(drv, "ID"))
                e.Item.Attributes.Add("OnClick", "RowLeftClickNoPost('" & DbField.GetStr(drv, "ID") & "')") '�ڿͻ������ɣ������¼����Ӧ����OnClick()
            End If
        End Sub
    End Class

End Namespace
