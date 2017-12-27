'-----------------------------------------------------------------------
'���������ҳ�档
'
'---˵����Ϣ1������
'����HostTable  ������RelTable (��SubTable)
'
'---˵����Ϣ2���������������й������󶼱�����POST��ʽ�����������
'-----------------------------------------------------------------------

Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CmsFrmContent
    Inherits Cms.Web.CmsFrmContentBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnRefresh As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnAddRecInSubTable As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblRecCountInHost As System.Web.UI.WebControls.Label
    Protected WithEvents lblRecCountInRel As System.Web.UI.WebControls.Label

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

    'ItemCreated�¼��ᱻ����2�Σ���һ������ͼ״̬�ָ�ʱ��û�������һ�Σ����ش����ڶ������������HTMLҳ��ǰ������Ӧ�ô���
    Private m_intItemCreatedCounterOfSubTable As Integer = 0

    '���ı������ѡ�м�¼ʱ�������ӱ�ķ�ҳӦ���л�����һҳ
        Private m_blnSetPaging2ToFirstPage As Boolean = False
        Private RecordFormUrl As String = "RecordForm.aspx"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        'If CmsPass.IsDepAdminByResID(VLng()) = True Then

        'End If
        If CmsPass.EmpIsSysAdmin Or CmsPass.EmpIsSysSecurity Then 'ϵͳ����Ա��ϵͳ��ȫԱԱ���ܽ������ݹ���
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
            End If

            If CmsPass.HostResData.ResTableType = "" Then
                If CmsPass.HostResData.EmptyResUrl.Trim <> "" Then
                    If CmsPass.HostResData.EmptyResTarget.Trim().ToLower = "_blank" Then
                        Response.Write("<script>window.open('" + CmsPass.HostResData.GetEmptyResUrl.Trim + "');</script>")
                    Else
                        Response.Write("<script>window.location.href='" + CmsPass.HostResData.GetEmptyResUrl.Trim + "';</script>")
                    End If
                End If
                Return
            End If

            Dim strCancel As String = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_CANCEL")
            lbtnHostCancel.Text = strCancel
            lbtnSubCancel.Text = strCancel

            lbtnHostAdd.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_ADD")
            lbtnHostEdit.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_EDIT")
            lbtnHostView.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_VIEW")
            lbtnHostDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
            lbtnHostRefresh.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_REFRESH")
            lbtnSubAdd.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_ADD")
            lbtnSubEdit.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_EDIT")
            lbtnSubView.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_VIEW")
            lbtnSubDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
            lbtnSubRefresh.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_REFRESH")

            lbtnHostSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_START")
            lbtnHostSearchAgain.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_AGAIN")
            lbtnHostFTableSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_FTABLE")

            lbtnHostAdvancedSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_ADVANCED")

            lbtnSubSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_START")
            lbtnSubSearchAgain.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_AGAIN")
            lbtnSubFTableSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_FTABLE")

            lbtnSubAdvancedSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_ADVANCED")

            LoadTabstrip(divTabs, RLng("TabID"))
            InitControlsSize(CmsPass, panelHostTableHeader, panelSubTable, Panel1, Panel2)   '��Ϊ�˸�����/��������ӹ�����

            LoadCmsMenu(CmsPass.HostResData.ResID) 'Loadҳ�����в˵�

            ''---------------------------------------------------------------
            ''���õ�ǰTab��Դ��ID��Ŀ�ģ��ظ������ǰѡ�е���ԴTabʱҳ�治�ύ
            'RegisterHiddenField("TabID_bak", RLng("TabID")) 'ע��ͻ���Form�������������������������ѡ�е��м�¼ID
            ''���õ�ǰ����ѡ�м�¼��ID��Ŀ�ģ��ظ������ǰѡ�е������¼ʱҳ�治�ύ
            'RegisterHiddenField("RECID_bak", VStr("PAGE_RECID")) 'ע��ͻ���Form�������������������������ѡ�е��м�¼ID
            ''---------------------------------------------------------------

            Dim strScript As String = "window.PopupDialog(document.all('txtSearchValue'),'/cmsweb/script/calendar.htm',getValueByID('txtSearchValue'));return false;"
            imgSearchDate1.Attributes.Add("onClick", strScript)
            imgSearchDate1.Attributes.Add("align", "absMiddle")
            strScript = "window.PopupDialog(document.all('txtSearchValueSub'),'/cmsweb/script/calendar.htm',getValueByID('txtSearchValueSub'));return false;"
            imgSearchDate2.Attributes.Add("onClick", strScript)
            imgSearchDate2.Attributes.Add("align", "absMiddle")

            DealBasicFunctions() '������������ݻ����������ܰ�ť

            'ȫ���ѯ������ʱ��֧�ֶ�ά��
            lbtnHostFTableSearch.Visible = CBool(IIf(CmsPass.HostResData.ResTableType = "TWOD", True, False))
            If CmsPass.RelResData Is Nothing OrElse CmsPass.RelResData.ResTableType <> "TWOD" Then
                lbtnSubFTableSearch.Visible = False
            End If
        End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '---------------------------------------------------------------
        If Request.QueryString("type") = Nothing Then
            Session("type") = "0"
        Else
            Session("type") = Request.QueryString("type")
        End If

        Dim lngCurClickedDepID As Long = AspPage.RLng("depid", Request)
        If lngCurClickedDepID > 0 Then  '����˲��Žڵ�
            PrepareUILayout(dgridSubTable, panelHostTableHeader, panelSubTable, Panel1, Panel2, panelPager1, panelPager2)   '������ԴID�����Ƿ���ʾ�����ӱ�
            Return
        End If
        '---------------------------------------------------------------

        '---------------------------------------------------------------
        If CmsFunc.IsEnable("FUNC_RELTABLE_SEARCH") Then
            panelPager2.Visible = True
            'lblNouse.Visible = False '�������ؼ���ʾʱ����ʾ�˸���Label
        Else
            panelPager2.Visible = False
            'lblNouse.Visible = True '�������ؼ�����ʾʱΪ����������Ϣ��Label��ȷ��ʾ���ұߣ�������ʾ�˸���Label
        End If
        '---------------------------------------------------------------

        '---------------------------------------------------------------
            '��ʾ���������ģ��    tq��Ҫ�޸�
        ViewState("CMS_HOSTSEARCH_COLTYPE") = WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, ddlColumns, True, True, False, , True, True) '��ʼ��DropDownList���ֶ��б���
        ddlColumns.SelectedValue = RStr("ddlColumns")
        ddlColumns.Width = Unit.Pixel(170)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions, False, 75)   '��ʼ��DropDownList�в���������
        '---------------------------------------------------------------

        '---------------------------------------------------------------
        '��ʼ����ҳ�ؼ�
        Cmspager1.EnableViewState = True
        If CmsPass.HostResData.PageRecCount = 0 Then
            Cmspager1.PageRows = 20 'CmsPass.TableHostRowNum
        Else
            Cmspager1.PageRows = CmsPass.HostResData.PageRecCount
        End If

        Cmspager1.Language = CmsPass.Employee.Language
        Cmspager1.TableAlign = "right"
        Cmspager1.ButtonAlign = "right"
        Cmspager1.WordAlign = "right"
        Cmspager1.TotalWidth = "250"
        Cmspager1.ButtonsWidth = "130"
        Cmspager1.CurrentPage = SInt("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID)

        Cmspager2.EnableViewState = True
        If CmsPass.RelResData.PageRecCount = 0 Then
            Cmspager2.PageRows = 20 'CmsPass.TableRelRowNum
        Else
            Cmspager2.PageRows = CmsPass.RelResData.PageRecCount
        End If

        Cmspager2.Language = CmsPass.Employee.Language
        Cmspager2.TableAlign = "right"
        Cmspager2.ButtonAlign = "right"
        Cmspager2.WordAlign = "right"
        Cmspager2.TotalWidth = "250"
        Cmspager2.ButtonsWidth = "130"
        Cmspager2.CurrentPage = SInt("CMS_RELTABLE_PAGE" & CmsPass.RelResData.ResID)
        '---------------------------------------------------------------

        If CmsFunc.IsEnable("FUNC_RELTABLE_SEARCH") Then
                '��ʾ�����������ģ��  tq��Ҫ�޸�
            Dim hashColType As Hashtable = WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.RelResData.ResID, ddlColumnsSub, True, True, False, , True, True)      '��ʼ��DropDownList���ֶ��б���
            ViewState("CMS_SUBSEARCH_COLTYPE") = hashColType
            ddlColumnsSub.SelectedValue = RStr("ddlColumnsSub")
            ddlColumnsSub.Width = Unit.Pixel(170)
            WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditionsSub, False, 75) '��ʼ��DropDownList�в���������
        End If

        '��ʾ����͹���������
        Dim lngSearchDocID As Long = RLng("schdocid")


        Dim daRes As New DataResource
        Dim l As String = daRes.ShowFiledOnly.ToString()



        If lngSearchDocID > 0 Then
            ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, , , , "DOC2_ID=" & lngSearchDocID)
        Else
            ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2)
        End If

        PrepareUILayout(dgridSubTable, panelHostTableHeader, panelSubTable, Panel1, Panel2, panelPager1, panelPager2) '������ԴID�����Ƿ���ʾ�����ӱ�
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
            '�ı���Դʱ��������Load������Ĳ�ѯ�ֶ��б�   tq��Ҫ�޸�
        If RStr("cmsaction") = "tabclick" AndAlso CmsFunc.IsEnable("FUNC_RELTABLE_SEARCH") Then
            Dim hashColType As Hashtable = WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.RelResData.ResID, ddlColumnsSub, True, True, True, , True, True)      '��ʼ��DropDownList���ֶ��б���
            ViewState("CMS_SUBSEARCH_COLTYPE") = hashColType
        End If

        '���Ʒ�ҳ
        If m_blnSetPaging2ToFirstPage = True Then Cmspager2.CurrentPage = 0

        Session("HostTableSelectRecId") = Request.Form("selectRecId1")
        Session("SubTableSelectRecId") = Request.Form("selectRecId2")
        DealMenuAction(RStr("cmsaction"), dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, RStr("ddlColumns").Trim(), RStr("ddlConditions").Trim(), txtSearchValue, RStr("ddlColumnsSub").Trim(), RStr("ddlConditionsSub").Trim(), txtSearchValueSub, panelHostTableHeader, panelSubTable, Panel1, Panel2)
    End Sub


    Private Sub dgridHostTable_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgridHostTable.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState()

        '���������洰���SIZE
        'CmsPass.InitWindowSize(RInt("content_width"), RInt("content_height"))

        Try
            If RLng("depid") > 0 Then Return '�����Դ���еĲ��Žڵ�

            '----------------------------------------------------------
            '��ǰҳ��Loadʱ���Ƚ�����ڣ�������ԴID�Ļ�ȡ��������
            'If CmsPass.IsFirstRequestToHostTable = True Then '��¼��ĵ�һ�������������
            '    CmsPass.IsFirstRequestToHostTable = False
            '    If CmsPass.Employee.LastNodeID <> 0 AndAlso ResFactory.ResService.IsResourceExist(CmsPass, CmsPass.Employee.LastNodeID) = False Then
            '        CmsPass.Employee.LastNodeID = 0
            '    End If
            '    CmsPass.SetHostResID(CmsPass.Employee.LastNodeID)
            'Else
            Dim lngResID As Long = RLng("noderesid")
            If lngResID <= 0 Then lngResID = CLng(CmsPass.Employee.LastNodeID)
            CmsPass.Employee.LastNodeID = CStr(lngResID)
            If ResFactory.ResService.IsResourceExist(CmsPass, lngResID) = False Then
                '�ϴ��˳�ʱ����Դ�����Ѿ���ɾ������������������ж���Դ�Ƿ���ڣ������û��޷�����ʹ��ϵͳ
                Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
                Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
                Session("CMS_HOSTTABLE_ORDERBY") = "" '���뽫���������ÿ�
                Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

                CmsPass.Employee.LastNodeID = "0"
                CmsPass.SetHostResID(CLng(CmsPass.Employee.LastNodeID))
            Else
                If lngResID <> CmsPass.HostResData.ResID Then '��Դ�ı��ˣ�������ȡ����Դ��������Ϣ
                    Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
                    Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
                    Session("CMS_HOSTTABLE_ORDERBY") = "" '���뽫���������ÿ�
                    Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                    Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�
                End If
                CmsPass.SetHostResID(lngResID)
            End If
            'End If
            '----------------------------------------------------------

            If CmsPass.HostResData.ResID = 0 Then Return '��ҵ�ڵ���Դ������ʾ�κ���Ϣ
            If CmsRights.HasRightsRecView(CmsPass, CmsPass.HostResData.ResID) = False Then
                'Dim strMsg As String = "<script language=""javascript"">window.onload = new function() {alert('���Ե�ǰ��Դû�з���Ȩ�ޣ�');}</script>"
                'Response.Write(strMsg)
                Return '�����������Ȩ��
            End If

            '----------------------------------------------------------
            '�����¼���ʱ��ȡ����ǰѡ�еļ�¼ID
            If CmsPass.RelResList.Count > 0 Then
                Dim strTemp As String = RStr("selectRecId1")
                If strtemp = "" Then
                    strTemp = RStr("RECID")
                End If

                If RStr("cmsaction") = "hostrowclick" Then
                    If SStr("CMS_HOSTTABLE_RECID") <> strTemp Then
                        m_blnSetPaging2ToFirstPage = True
                        Session("CMS_HOSTTABLE_RECID") = strTemp
                    End If
                Else
                    If strTemp <> "" Then
                        If strTemp.StartsWith(",") = False Then strTemp = "," & strTemp
                        If strTemp.EndsWith(",") = False Then strTemp = strTemp & ","
                        If strTemp.IndexOf("," & SStr("CMS_HOSTTABLE_RECID") & ",") >= 0 Then
                            Session("CMS_HOSTTABLE_RECID") = strTemp
                        Else
                            Session("CMS_HOSTTABLE_RECID") = SStr("CMS_HOSTTABLE_RECID") & "," & strTemp
                        End If
                    End If
                End If
            Else
                Dim strTemp As String = RStr("selectRecId1")
                If strtemp = "" Then
                    strTemp = RStr("RECID")
                End If
                If strTemp <> "" Then
                    'ֻ��ֵ��Ч�ļ�¼ID�����������ҳ�������ԭ��¼ID�����������ѡ�С�
                    If strTemp = "0" Then
                        Session("CMS_HOSTTABLE_RECID") = Nothing
                    Else
                        Session("CMS_HOSTTABLE_RECID") = strTemp
                    End If
                End If
            End If
            '----------------------------------------------------------

            '----------------------------------------------------------
            '����DataGrid��ʾ����
            'Dim intRowNum As Long = CmsPass.TableHostRowNum '��ȡÿҳ��ʾ����
            WebUtilities.InitialDataGrid(dgridHostTable, 20, True, True, False, True)        '��������EnableViewStateΪTrue������DataGrid�޷���ס��ǰҳ
            '----------------------------------------------------------

            '----------------------------------------------------------
            '������DataGrid1_Init()�д����У�����֮���ܽ���PageIndexChanged��SortCommand�ȵ��¼����
            If CmsPass.HostResData.ResTableType <> "" Then
                Dim dv As DataView = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetTableColumns(CmsPass, CmsPass.HostResData.ResID)
                WebUtilities.LoadResTableColumns(CmsPass, CmsPass.HostResData.ResID, dgridHostTable, dv)
            End If
            '----------------------------------------------------------
        Catch ex As Exception
            SLog.Err("����DataGrid1_Init�쳣����", ex)
        End Try
    End Sub

    Private Sub dgridHostTable_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgridHostTable.ItemCreated
        If dgridHostTable.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            'ItemCreated�¼��ᱻ����2�Σ���һ������ͼ״̬�ָ�ʱ��û�������һ�Σ����ش����ڶ������������HTMLҳ��ǰ������Ӧ�ô���
            If m_intItemCreatedCounter < 1 Then
                m_intItemCreatedCounter += 1
                Return
            End If

            Dim i As Integer
            Dim row As DataGridItem

            '-----------------------------------------------------------
            '�ҵ��ĵ�Size�ֶ������е����
            Dim intSizeColNO As Integer = -1
            For i = 0 To dgridHostTable.Columns.Count - 1
                Try
                    Dim bc As BoundColumn = CType(dgridHostTable.Columns(i), BoundColumn)
                    If bc.DataField = "DOC2_SIZE" Then
                        intSizeColNO = i
                        Exit For
                    End If
                Catch ex As Exception

                End Try
            Next
            '-----------------------------------------------------------

            '-----------------------------------------------------------
            '�ҵ�����ɫ���õļ����е����
            Dim datRes As DataResource = CmsPass.HostResData
            Dim intCol1() As Integer = {-1, -1, -1}
            Dim intCol1Type() As Integer = {0, 0, 0}
            Dim intCol2() As Integer = {-1, -1, -1}
            Dim intCol2Type() As Integer = {0, 0, 0}
            Dim intCol3() As Integer = {-1, -1, -1}
            Dim intCol3Type() As Integer = {0, 0, 0}
            For i = 0 To dgridHostTable.Columns.Count - 1
                Try
                    Dim bc As BoundColumn = CType(dgridHostTable.Columns(i), BoundColumn)
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
                Catch ex As Exception

                End Try

            Next
            '-----------------------------------------------------------

            Dim alistRecIDs As ArrayList = StringDeal.Split(SStr("CMS_HOSTTABLE_RECID"), ",")
            If alistRecIDs Is Nothing Then alistRecIDs = New ArrayList
            If alistRecIDs.Count = 0 Then
                Session("WorkFlowID") = "0"
            Else
                Session("WorkFlowID") = Convert.ToString(alistRecIDs(0))
            End If
            Dim strNoPost As String = RStr("mnunopost")
            For Each row In dgridHostTable.Items
                '-----------------------------------------------------------
                'ת���ĵ�Size����ʾ�� 13657  ->  14 KB
                If intSizeColNO >= 0 Then
                    Dim strTemp As String = WebUtilities.ConvertDocSize(row.Cells(intSizeColNO).Text)
                    row.Cells(intSizeColNO).Text = strTemp
                End If
                '-----------------------------------------------------------

                '���ÿͻ��˵ļ�¼ID��Javascript����
                If IsNumeric(row.Cells(0).Text.Trim()) Then
                    Dim lngCurrentRecID As Long = CLng(row.Cells(0).Text.Trim()) '��1�б����Ǽ�¼ID
                    row.Attributes.Add("RECID", CStr(lngCurrentRecID)) '�ڿͻ��˱����¼ID
                    If strNoPost <> "yes" And CmsPass.RelResList.Count > 0 Then '����ر�����������ΪPOST
                        row.Attributes.Add("OnClick", "RowLeftClickInHostTablePost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()
                    Else '����ر�
                        row.Attributes.Add("OnClick", "RowLeftClickInHostTableNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()
                    End If

                    '������������������ɫ
                    Dim blnSet As Boolean = False
                    blnSet = ResTableRowColor.SetRowColor(datRes, row, intCol1, intCol1Type, 1)
                    If blnSet = False Then blnSet = ResTableRowColor.SetRowColor(datRes, row, intCol2, intCol2Type, 2)
                    If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol3, intCol3Type, 3)

                    If lngCurrentRecID > 0 And alistRecIDs.Contains(CStr(lngCurrentRecID)) Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '�޸ı������¼�ı�����ɫ
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub dgridHostTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgridHostTable.SortCommand
            Session("CMS_HOSTTABLE_RECID") = "" '������յ�ǰѡ�м�¼ID


        'ÿ�ε���滻�������
        If SStr("HOSTTABLE_ORDERBY_TYPE") = "ASC" Then
            Session("HOSTTABLE_ORDERBY_TYPE") = "DESC"
        Else
            Session("HOSTTABLE_ORDERBY_TYPE") = "ASC"
        End If

        '������ʾ����
        Session("CMS_HOSTTABLE_ORDERBY") = e.SortExpression & " " & SStr("HOSTTABLE_ORDERBY_TYPE")
        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2)    '�����������ر�����
    End Sub

    Private Sub dgridSubTable_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgridSubTable.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState()

        Try
            '����������Ƿ����
            If CmsPass.RelResList Is Nothing OrElse CmsPass.RelResList.Count <= 0 Then Return 'û�й�����

            '----------------------------------------------------------
            '�������Tab������������Ȼ�ȡ�����Tab����ԴID
            If RStr("cmsaction") = "tabclick" Then
                Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�
                CmsPass.SetRelResID(RLng("TabID"))
            End If
            '----------------------------------------------------------

            '----------------------------------------------------------
            '������DataGrid1_Init()�д����У�����֮���ܽ���PageIndexChanged��SortCommand�ȵ��¼����
            If CmsPass.RelResData.ResTableType <> "" Then
                WebUtilities.InitialDataGrid(dgridSubTable, 20, True, True, False, True) '��������EnableViewStateΪTrue������DataGrid�޷���ס��ǰҳ
                Dim dv As DataView = ResFactory.TableService(CmsPass.RelResData.ResTableType).GetTableColumns(CmsPass, CmsPass.RelResData.ResID)
                WebUtilities.LoadResTableColumns(CmsPass, CmsPass.RelResData.ResID, dgridSubTable, dv)
            End If
            '----------------------------------------------------------
        Catch ex As Exception
            SLog.Err("������DataGrid1_Init�쳣����", ex)
        End Try
    End Sub

    Private Sub dgridSubTable_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgridSubTable.ItemCreated
        If dgridSubTable.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            'ItemCreated�¼��ᱻ����2�Σ���һ������ͼ״̬�ָ�ʱ��û�������һ�Σ����ش����ڶ������������HTMLҳ��ǰ������Ӧ�ô���
            If m_intItemCreatedCounterOfSubTable < 1 Then
                m_intItemCreatedCounterOfSubTable += 1
                Return
            End If

            '-----------------------------------------------------------
            '�ҵ��ĵ�Size�ֶ������е����
            Dim intSizeColNO As Integer = -1
            Dim i As Integer
            For i = 0 To dgridSubTable.Columns.Count - 1
                Try
                    Dim bc As BoundColumn = CType(dgridSubTable.Columns(i), BoundColumn)
                    If bc.DataField = "DOC2_SIZE" Then
                        intSizeColNO = i
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
            '-----------------------------------------------------------

            '-----------------------------------------------------------
            '�ҵ�����ɫ���õļ����е����
            Dim datRes As DataResource = CmsPass.RelResData
            Dim intCol1() As Integer = {-1, -1, -1}
            Dim intCol1Type() As Integer = {0, 0, 0}
            Dim intCol2() As Integer = {-1, -1, -1}
            Dim intCol2Type() As Integer = {0, 0, 0}
            Dim intCol3() As Integer = {-1, -1, -1}
            Dim intCol3Type() As Integer = {0, 0, 0}
            For i = 0 To dgridSubTable.Columns.Count - 1
                Try
                    Dim bc As BoundColumn = CType(dgridSubTable.Columns(i), BoundColumn)
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
                Catch ex As Exception

                End Try

            Next
            '-----------------------------------------------------------

            'Dim alistRecIDs As ArrayList = StringDeal.Split(RStr("RECID2"), ",")
            Dim alistRecIDs As ArrayList = StringDeal.Split(GetRecID2OfGrid(Request, ViewState, IsPostBack), ",")
            If alistRecIDs Is Nothing Then alistRecIDs = New ArrayList
            If alistRecIDs.Count = 0 Then
                Session("WorkFlowID") = "0"
            Else
                Session("WorkFlowID") = Convert.ToString(alistRecIDs(0))

            End If

            Dim row As DataGridItem
            For Each row In dgridSubTable.Items
                '-----------------------------------------------------------
                'ת���ĵ�Size����ʾ�� 13657  ->  14 KB
                If intSizeColNO >= 0 Then
                    Dim strTemp As String = WebUtilities.ConvertDocSize(row.Cells(intSizeColNO).Text)
                    row.Cells(intSizeColNO).Text = strTemp
                End If
                '-----------------------------------------------------------

                '���ÿͻ��˵ļ�¼ID��Javascript����
                If IsNumeric(row.Cells(0).Text.Trim()) Then
                    Dim lngCurrentRecID As Long = CLng(row.Cells(0).Text.Trim()) '��1�б����Ǽ�¼ID
                    row.Attributes.Add("RECID2", CStr(lngCurrentRecID)) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("OnClick", "RowLeftClickInSubTableNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()

                    '������������������ɫ
                    Dim blnSet As Boolean = False
                    blnSet = ResTableRowColor.SetRowColor(datRes, row, intCol1, intCol1Type, 1)
                    If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol2, intCol2Type, 2)
                    If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol3, intCol3Type, 3)

                    'If lngCurrentRecID > 0 And alistRecIDs.Contains(CStr(lngCurrentRecID)) Then
                    '    row.Attributes.Add("bgColor", "#C4D9F9") '�û������ĳ����¼���޸ı������¼�ı�����ɫ
                    'End If
                End If
            Next
        End If
    End Sub

    Private Sub dgridSubTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgridSubTable.SortCommand
        'ÿ�ε���滻�������
        If SStr("SUBTABLE_ORDERBY_TYPE") = "ASC" Then
            Session("SUBTABLE_ORDERBY_TYPE") = "DESC"
        Else
            Session("SUBTABLE_ORDERBY_TYPE") = "ASC"
        End If

        '������ʾ����
        Session("CMS_SUBTABLE_ORDERBY") = "ORDER BY " & e.SortExpression & " " & SStr("SUBTABLE_ORDERBY_TYPE")
        ShowRelationTableData(dgridSubTable, Cmspager2, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, SLng("CMS_HOSTTABLE_RECID"), "", False, False)
    End Sub

    Private Sub Cmspager1_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager1.Click
        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, False, eventArgument)
    End Sub

    Private Sub Cmspager2_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager2.Click
        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, False, , eventArgument)
    End Sub

    Private Sub lbtnHostSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnHostSearch.Click
        Try
            Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID

            '��֯Where���
            Dim strColumns As String = RStr("ddlColumns").Trim
            Dim strConditions As String = RStr("ddlConditions").Trim()
            Dim hashColType As Hashtable = CType(ViewState("CMS_HOSTSEARCH_COLTYPE"), Hashtable)
            Dim lngColType As Long = CLng(hashColType(strColumns))
            Session("CMS_HOSTTABLE_WHERE") = CTableStructure.GenerateFieldWhere(strColumns, txtSearchValue.Text.Trim(), lngColType, strConditions, SDbConnectionPool.GetDbConfig().DatabaseType)

            ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True)
                If SStr("CMS_HOSTTABLE_WHERE") = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnHostSearchAgain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnHostSearchAgain.Click
        Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID

        '��֯Where���
        Dim strColumns As String = RStr("ddlColumns").Trim
        Dim strConditions As String = RStr("ddlConditions").Trim()
        Dim hashColType As Hashtable = CType(ViewState("CMS_HOSTSEARCH_COLTYPE"), Hashtable)
        Dim lngColType As Long = CLng(hashColType(strColumns))
        Dim strWhere As String = CTableStructure.GenerateFieldWhere(strColumns, txtSearchValue.Text.Trim(), lngColType, strConditions, SDbConnectionPool.GetDbConfig().DatabaseType)
        Session("CMS_HOSTTABLE_WHERE") = CmsWhere.AppendAnd(strWhere, SStr("CMS_HOSTTABLE_WHERE"))

        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True) '��ʾ������
            If strWhere = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
    End Sub

    Private Sub lbtnHostFTableSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnHostFTableSearch.Click
        Session("CMS_HOSTTABLE_RECID") = "" '������յ�ǰѡ�м�¼ID
        Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
        Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
        Session("CMS_HOSTTABLE_ORDERBY") = "" '�����������
        '��֯Where���
        Dim strConditions As String = RStr("ddlConditions").Trim()
        Session("CMS_HOSTTABLE_WHERE") = CTableStructure.GenerateFullTableFieldWhere(CmsPass, ResourceLocation.HostTable, CmsPass.HostResData.ResID, txtSearchValue.Text.Trim(), strConditions, SDbConnectionPool.GetDbConfig().DatabaseType)

        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True)    '��ʾ������
            If SStr("CMS_HOSTTABLE_WHERE") = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
    End Sub

    Private Sub lbtnHostCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnHostCancel.Click
        Session("CMS_HOSTTABLE_RECID") = "" '������յ�ǰѡ�м�¼ID
        Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
        Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
        Session("CMS_HOSTTABLE_ORDERBY") = "" '�����������

        '���������ǹ�����ˢ�£���Ӧ��ˢ�¹�����Session����
        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
        Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
        Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

        ddlColumns.SelectedIndex = 0
        ddlConditions.SelectedIndex = 0
        txtSearchValue.Text = ""

        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True) '������������
    End Sub

    Private Sub lbtnSubSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSubSearch.Click
        Try
            Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

            '��֯Where���
            Dim strColumnsSub As String = RStr("ddlColumnsSub").Trim
            Dim strConditionsSub As String = RStr("ddlConditionsSub").Trim()
            Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
            Dim hashColType As Hashtable = CType(ViewState("CMS_SUBSEARCH_COLTYPE"), Hashtable)
            Dim lngColType As Long = CLng(hashColType(strColumnsSub))
            Session("CMS_SUBTABLE_WHERE") = CTableStructure.GenerateFieldWhere(strColumnsSub, txtSearchValueSub.Text.Trim(), lngColType, strConditionsSub, SDbConnectionPool.GetDbConfig().DatabaseType)

            '��ȡ������Ĺ�����������������������ʾ������
            'Dim strRelWhere As String = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, lngHostRecID)
            'Session("CMS_SUBTABLE_WHERE") = CmsWhere.AppendAnd(strWhere, strRelWhere)

            ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True)
                If SStr("CMS_SUBTABLE_WHERE") = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnSubSearchAgain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSubSearchAgain.Click
        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID

        '��֯Where���
        Dim strColumnsSub As String = RStr("ddlColumnsSub").Trim
        Dim strConditionsSub As String = RStr("ddlConditionsSub").Trim()
        Dim hashColType As Hashtable = CType(ViewState("CMS_SUBSEARCH_COLTYPE"), Hashtable)
        Dim lngColType As Long = CLng(hashColType(strColumnsSub))
        Dim strWhere As String = CTableStructure.GenerateFieldWhere(strColumnsSub, txtSearchValueSub.Text.Trim(), lngColType, strConditionsSub, SDbConnectionPool.GetDbConfig().DatabaseType)
        Session("CMS_SUBTABLE_WHERE") = CmsWhere.AppendAnd(strWhere, SStr("CMS_SUBTABLE_WHERE"))

        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True) '��ʾ������
            If strWhere = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
    End Sub

    Private Sub lbtnSubFTableSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSubFTableSearch.Click
        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
        Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
        Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

        '��֯Where���
        Dim strConditionsSub As String = RStr("ddlConditionsSub").Trim()
        Session("CMS_SUBTABLE_WHERE") = CTableStructure.GenerateFullTableFieldWhere(CmsPass, ResourceLocation.RelTable, CmsPass.RelResData.ResID, txtSearchValueSub.Text.Trim(), strConditionsSub, SDbConnectionPool.GetDbConfig().DatabaseType)

        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, True)    '��ʾ������
            If SStr("CMS_SUBTABLE_WHERE") = "11=22" Then PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
    End Sub

    Private Sub lbtnSubCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSubCancel.Click
        '���������ǹ�����ˢ�£���Ӧ��ˢ�¹�����Session����
        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
        Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
        Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

        ddlColumnsSub.SelectedIndex = 0
        ddlConditionsSub.SelectedIndex = 0
        txtSearchValueSub.Text = ""

        ShowHostTableData(dgridHostTable, dgridSubTable, Cmspager1, Cmspager2, False)   '������������
    End Sub

    '------------------------------------------------------------------------
    'Loadҳ�����в˵�
    '------------------------------------------------------------------------
    Private Sub LoadCmsMenu(ByVal lngResID As Long)
        'Load����������˵�����¼����
        RegisterMenuScript(CmsPass, lbtnHostRec, imgHostRec, CmsPass.HostResData.ResID, ResourceLocation.HostTable, "MENU_RECORD", CmsMenuType.Standard, "ShowRecordMenuInHost")

        'Load�����������˵�����¼����
        RegisterMenuScript(CmsPass, lbtnSubRec, imgSubRec, CmsPass.RelResData.ResID, ResourceLocation.RelTable, "MENU_RECORD", CmsMenuType.Standard, "ShowRecordMenuInRel")

        'Load����������˵����ĵ�����
        RegisterMenuScript(CmsPass, lbtnHostDoc, imgHostDoc, CmsPass.HostResData.ResID, ResourceLocation.HostTable, "MENU_DOC", CmsMenuType.Standard, "ShowDocMenuInHost")

        'Load�����������˵����ĵ�����
        RegisterMenuScript(CmsPass, lbtnSubDoc, imgSubDoc, CmsPass.RelResData.ResID, ResourceLocation.RelTable, "MENU_DOC", CmsMenuType.Standard, "ShowDocMenuInRel")
        Dim aa As Long = CmsPass.HostResData.ResID
        'Load����������˵�������
        RegisterMenuScript(CmsPass, lbtnHostTools, imgHostTools, CmsPass.HostResData.ResID, ResourceLocation.HostTable, "MENU_TOOLS", CmsMenuType.Standard, "ShowToolsMenuInHost")

        'Load�����������˵�������
        RegisterMenuScript(CmsPass, lbtnSubTools, imgSubTools, CmsPass.RelResData.ResID, ResourceLocation.RelTable, "MENU_TOOLS", CmsMenuType.Standard, "ShowToolsMenuInRel")

        'Load����������˵�����Դ����
        RegisterMenuScript(CmsPass, lbtnHostRes, imgHostRes, CmsPass.HostResData.ResID, ResourceLocation.HostTable, "MENU_RESACTION", CmsMenuType.Standard, "ShowResActionMenuInHost")

        'Load�����������˵�����Դ����
        RegisterMenuScript(CmsPass, lbtnSubRes, imgSubRes, CmsPass.RelResData.ResID, ResourceLocation.RelTable, "MENU_RESACTION", CmsMenuType.Standard, "ShowResActionMenuInRel")

        'Load��Դ��չ���ܲ˵���һ������Ŀ�ͻ��Ķ��ƿ���
        RegisterMenuScript(CmsPass, lbtnHostExt, imgHostExt, CmsPass.HostResData.ResID, ResourceLocation.HostTable, "MENU_RES_" & CmsPass.HostResData.IndepParentResID, CmsMenuType.Extension, "ShowExtMenuInHost")

        'Load��Դ��չ���ܲ˵���һ������Ŀ�ͻ��Ķ��ƿ���
        RegisterMenuScript(CmsPass, lbtnSubExt, imgSubExt, CmsPass.RelResData.ResID, ResourceLocation.RelTable, "MENU_RES_" & CmsPass.RelResData.IndepParentResID, CmsMenuType.Extension, "ShowExtMenuInSub")

        'Load����������˵�����Ϣ��ѯ
        RegisterMenuScript(CmsPass, lbtnHostStat, Nothing, CmsPass.HostResData.ResID, ResourceLocation.HostTable, "MENU_SEARCH", CmsMenuType.Standard, "ShowSearchMenuInHost")

        'Load����������˵�����Ϣ��ѯ
        RegisterMenuScript(CmsPass, lbtnSubStat, Nothing, CmsPass.RelResData.ResID, ResourceLocation.RelTable, "MENU_SEARCH", CmsMenuType.Standard, "ShowSearchMenuInSub")
    End Sub

    '-----------------------------------------------------------------------
    '��ʼ���������Tabstrip
    '-----------------------------------------------------------------------
    Private Sub RegisterMenuScript( _
        ByRef pst As CmsPassport, _
        ByRef lbtnMenu As System.Web.UI.WebControls.LinkButton, _
        ByRef imgMenu As System.Web.UI.WebControls.Image, _
        ByVal lngResID As Long, _
        ByVal intResLoc As ResourceLocation, _
        ByVal strMenuSection As String, _
        ByVal intMenuType As CmsMenuType, _
        ByVal strMenuJSFunc As String _
        )
        Dim strScript As String = CmsMenu.CreateScriptOfOneMainMenu(pst, lbtnMenu, imgMenu, lngResID, intResLoc, strMenuSection, intMenuType, strMenuJSFunc, CmsMenuFrom.ContentTable)
        If strScript <> "" And (Not IsStartupScriptRegistered(strMenuJSFunc)) Then
            RegisterStartupScript(strMenuJSFunc, strScript)
        End If
    End Sub

    '-----------------------------------------------------------------------
    '��ʼ���������Tabstrip
    '-----------------------------------------------------------------------
    Private Sub LoadTabstrip(ByRef divTabs As System.Web.UI.HtmlControls.HtmlGenericControl, ByVal lngTabID As Long)
        If divTabs Is Nothing Then Return

        Dim strHtml As String = ""
        strHtml &= "<table width='100%' height='25' border='0' cellpadding='0' cellspacing='0' style='font-size:12px;cursor:hand' onClick='ClickTabScript();'> "
        strHtml &= "<tr> "

        '����������ʾ����ԴTAB�����������������������ʾ��
        Dim intTabResNum As Integer = CmsConfig.GetInt("CLIENT_CONFIG", "RELTABLE_TABNUM")

        Dim i As Integer = 0
        Dim blnOptAdded As Boolean = False
        Dim lngRelResID As Long
        For Each lngRelResID In CmsPass.RelResList
            Dim datRes As DataResource = CmsPass.GetDataRes(lngRelResID)
            Try
                '�����û��Ƿ�ӵ�жԴ˹������ViewȨ��
                If CmsRights.HasRightsRecView(CmsPass, datRes.ResID) Then '��ViewȨ��
                    If i <= (intTabResNum - 1) Then
                        If lngTabID > 0 And lngTabID = datRes.ResID Then
                            strHtml &= "<td width='120' align='center' valign='bottom' background='/cmsweb/images/menu_r1_c70.gif' TabID='" & datRes.ResID & "'>" & datRes.ResName & "</td> "
                        ElseIf lngTabID = 0 And CmsPass.RelResData.ResID = datRes.ResID Then
                            strHtml &= "<td width='120' align='center' valign='bottom' background='/cmsweb/images/menu_r1_c70.gif' TabID='" & datRes.ResID & "'>" & datRes.ResName & "</td> "
                        Else
                            strHtml &= "<td width='120' align='center' valign='bottom' background='/cmsweb/images/menu_r1_c3.gif' TabID='" & datRes.ResID & "'>" & datRes.ResName & "</td> "
                        End If
                    Else
                        If blnOptAdded = False Then
                            strHtml &= "    <td width='120' align='left' valign='bottom'>" & Environment.NewLine
                            strHtml &= "        <SELECT id='optionResource' name='optionResource' onchange='javascript:ChangeResOption();' style='WIDTH: 120px;'>" & Environment.NewLine
                            strHtml &= "            <OPTION value=''></OPTION>" & Environment.NewLine
                        End If
                        'If lngTabID > 0 And lngTabID = datRes.ResID Then
                        If CmsPass.RelResData.ResID > 0 And CmsPass.RelResData.ResID = datRes.ResID Then
                            strHtml &= "            <OPTION value='" & datRes.ResID & "' selected>" & datRes.ResName & "</OPTION>" & Environment.NewLine
                        Else
                            strHtml &= "            <OPTION value='" & datRes.ResID & "'>" & datRes.ResName & "</OPTION>" & Environment.NewLine
                        End If
                        blnOptAdded = True
                    End If

                    i += 1
                End If
            Catch ex As Exception
                SLog.Err("������ر�Ȩ��ʱ�쳣����", ex)
            End Try
            datRes = Nothing
        Next

        If blnOptAdded = True Then
            strHtml &= "        </SELECT>" & Environment.NewLine
            strHtml &= "    </td>" & Environment.NewLine
        End If

        strHtml &= "<td style='cursor:default;'>&nbsp;</td>"
        strHtml &= "</tr>"
        strHtml &= "</table>"
        divTabs.InnerHtml = strHtml
    End Sub

    '------------------------------------------------------------------------------------------------------
    '������������ݻ����������ܰ�ť
    '------------------------------------------------------------------------------------------------------
    Private Sub DealBasicFunctions()
        '�������
        Dim aa As Long = CmsPass.HostResData.ResID
        Dim bb As Long = CmsPass.RelResData.ResID
        lbtnHostAdd.EnableViewState = False
        If CmsRights.HasRightsRecAdd(CmsPass, CmsPass.HostResData.ResID) = True Then
                lbtnHostAdd.Enabled = True
                'Dim ResID As String = CmsPass.HostResData.ResID.ToString
                'If CmsPass.HostResData.ResIconName = "ICON_RES_VIEW" Then
                '    ResID = CmsPass.HostResData.IndepParentResID.ToString
                'End If
                lbtnHostAdd.Attributes.Add("onClick", "return MenuItemEntryGet('', '', '', '/cmsweb/cmshost/" + RecordFormUrl + "?mnuinmode=" & InputMode.AddInHostTable & "', '" & CmsPass.HostResData.ResID & "', '0', '1', '0', '" & CmsPass.HostResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnHostAdd.Enabled = False
                lbtnHostAdd.Attributes.Add("onClick", "return false;")
            End If
            lbtnHostAdd.Attributes.Add("href", "#")

            lbtnHostEdit.EnableViewState = False
            If CmsRights.HasRightsRecEdit(CmsPass, CmsPass.HostResData.ResID) = True Then
                lbtnHostEdit.Enabled = True
                lbtnHostEdit.Attributes.Add("onClick", "return MenuItemEntryGet('', '', '', '/cmsweb/cmshost/" + RecordFormUrl + "?mnuinmode=" & InputMode.EditInHostTable & "', '" & CmsPass.HostResData.ResID & "', '0', '1', '1', '" & CmsPass.HostResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnHostEdit.Enabled = False
                lbtnHostEdit.Attributes.Add("onClick", "return false;")
            End If
            lbtnHostEdit.Attributes.Add("href", "#")

            lbtnHostView.EnableViewState = False
            If CmsRights.HasRightsRecView(CmsPass, CmsPass.HostResData.ResID) = True Then
                lbtnHostView.Enabled = True
                lbtnHostView.Attributes.Add("onClick", "return MenuItemEntryGet('', '', '', '/cmsweb/cmshost/" + RecordFormUrl + "?mnuinmode=" & InputMode.ViewInHostTable & "', '" & CmsPass.HostResData.ResID & "', '0', '1', '1', '" & CmsPass.HostResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnHostView.Enabled = False
                lbtnHostView.Attributes.Add("onClick", "return false;")
            End If
            lbtnHostView.Attributes.Add("href", "#")

            lbtnHostDel.EnableViewState = False
            If CmsRights.HasRightsRecDel(CmsPass, CmsPass.HostResData.ResID) = True Then
                lbtnHostDel.Enabled = True
                lbtnHostDel.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordDelete', '', '" & CmsPass.HostResData.ResID & "', '0', '1', '1', '" & CmsPass.HostResData.ResID & "', '', '" + CmsMessage.GetMsg(CmsPass, "RECORD_DELETE") + "', '_self', '');")
            Else
                lbtnHostDel.Enabled = False
                lbtnHostDel.Attributes.Add("onClick", "return false;")
            End If
            lbtnHostDel.Attributes.Add("href", "#")

            lbtnHostRefresh.EnableViewState = False
            If CmsRights.HasRightsRecView(CmsPass, CmsPass.HostResData.ResID) = True Then
                lbtnHostRefresh.Enabled = True
                lbtnHostRefresh.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordRefresh', '', '" & CmsPass.HostResData.ResID & "', '0', '1', '0', '" & CmsPass.HostResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnHostRefresh.Enabled = False
                lbtnHostRefresh.Attributes.Add("onClick", "return false;")
            End If
            lbtnHostRefresh.Attributes.Add("href", "#")

            lbtnHostAdvancedSearch.Attributes.Add("onClick", "return MenuItemEntryForPopup('POPUP','','HostAdvancedSearch','MNUSEARCHADVANCED','/cmsweb/cmshost/AdvancedSearch.aspx','" & CmsPass.HostResData.ResID & "','0','1', '0', '" & CmsPass.HostResData.ResID & "','', '', '_blank', '','300','150','670','460','no','no','no','yes','yes');")
            lbtnHostAdvancedSearch.Attributes.Add("href", "#")


            '�����ӱ����
            lbtnSubAdd.EnableViewState = False
            If CmsRights.HasRightsRecAdd(CmsPass, CmsPass.RelResData.ResID) = True Then
                lbtnSubAdd.Enabled = True
                'Dim ResID As String = CmsPass.RelResData.ResID.ToString
                'If CmsPass.RelResData.ResIconName = "ICON_RES_VIEW" Then
                '    ResID = CmsPass.RelResData.IndepParentResID.ToString
                'End If
                lbtnSubAdd.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordAdd', '', '" & CmsPass.RelResData.ResID & "', '0', '2', '0', '" & CmsPass.RelResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnSubAdd.Enabled = False
                lbtnSubAdd.Attributes.Add("onClick", "return false;")
            End If
            lbtnSubAdd.Attributes.Add("href", "#")

            lbtnSubEdit.EnableViewState = False
            If CmsRights.HasRightsRecEdit(CmsPass, CmsPass.RelResData.ResID) = True Then
                lbtnSubEdit.Enabled = True
                lbtnSubEdit.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordEdit', '', '" & CmsPass.RelResData.ResID & "', '0', '2', '1', '" & CmsPass.RelResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnSubEdit.Enabled = False
                lbtnSubEdit.Attributes.Add("onClick", "return false;")
            End If
            lbtnSubEdit.Attributes.Add("href", "#")

            lbtnSubView.EnableViewState = False
            If CmsRights.HasRightsRecView(CmsPass, CmsPass.RelResData.ResID) = True Then
                lbtnSubView.Enabled = True
                lbtnSubView.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordView', '', '" & CmsPass.RelResData.ResID & "', '0', '2', '1', '" & CmsPass.RelResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnSubView.Enabled = False
                lbtnSubView.Attributes.Add("onClick", "return false;")
            End If
            lbtnSubView.Attributes.Add("href", "#")

            lbtnSubDel.EnableViewState = False
            If CmsRights.HasRightsRecDel(CmsPass, CmsPass.RelResData.ResID) = True Then
                lbtnSubDel.Enabled = True
                lbtnSubDel.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordDelete', '', '" & CmsPass.RelResData.ResID & "', '0', '2', '1', '" & CmsPass.RelResData.ResID & "', '', '" + CmsMessage.GetMsg(CmsPass, "RECORD_DELETE") + "', '_self', '');")
            Else
                lbtnSubDel.Enabled = False
                lbtnSubDel.Attributes.Add("onClick", "return false;")
            End If
            lbtnSubDel.Attributes.Add("href", "#")

            lbtnSubRefresh.EnableViewState = False
            If CmsRights.HasRightsRecView(CmsPass, CmsPass.RelResData.ResID) = True Then
                lbtnSubRefresh.Enabled = True
                lbtnSubRefresh.Attributes.Add("onClick", "return MenuItemEntryPost('', '', 'MenuRecordRefresh', '', '" & CmsPass.RelResData.ResID & "', '0', '2', '0', '" & CmsPass.RelResData.ResID & "', '', '', '_self', '');")
            Else
                lbtnSubRefresh.Enabled = False
                lbtnSubRefresh.Attributes.Add("onClick", "return false;")
            End If
            lbtnSubRefresh.Attributes.Add("href", "#")

            lbtnSubAdvancedSearch.Attributes.Add("onClick", "return MenuItemEntryForPopup('POPUP','','SubAdvancedSearch','MNUSEARCHADVANCED','/cmsweb/cmshost/AdvancedSearch.aspx','" & CmsPass.RelResData.ResID & "','0','2', '0', '" & CmsPass.RelResData.ResID & "','', '', '_blank', '','300','150','670','460','no','no','no','yes','yes');")
            lbtnSubAdvancedSearch.Attributes.Add("href", "#")
        End Sub
End Class
End Namespace
