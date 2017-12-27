Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CommAddrbook
        Inherits CmsPage

    Private Structure DataComm
        Public strEmail As String
        Public strPhone As String
        Public strReference As String
    End Structure

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
        ViewState("PAGE_COMM_RESID") = RLng("noderesid")
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lbtnSearch.ToolTip = "ͬʱģ����ѯ�ο��ֶΡ������ʼ����ֻ�����"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        ViewState("PAGE_COMM_WHERE") = ""
        ViewState("PAGE_COMM_ORDERBY") = ""

        WebUtilities.InitialDataGrid(DataGrid1, 17)  '����DataGrid��ʾ����
        Dim lngResID As Long = AspPage.RLng("noderesid", Request)
        Dim datComm As DataComm = GetDataComm(lngResID)
        ShowAddrbook(lngResID)
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnConfirm.Click
        Dim strEmailList As String = GetAllEmailPhones(lbxEmail)
        Dim strPhoneList As String = GetAllEmailPhones(lbxSms)
        If SStr("CMSCOMM_EMAILS") = "" Then
            Session("CMSCOMM_EMAILS") = strEmailList
        Else
            Session("CMSCOMM_EMAILS") = SStr("CMSCOMM_EMAILS") & ", " & strEmailList
        End If
        If SStr("CMSCOMM_PHONES") = "" Then
            Session("CMSCOMM_PHONES") = strPhoneList
        Else
            Session("CMSCOMM_PHONES") = SStr("CMSCOMM_PHONES") & ", " & strPhoneList
        End If

        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "commfrom=addrbook"), False)
    End Sub

    Private Sub lbtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCancel.Click
        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "commfrom=addrbook"), False)
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

        '----------------------------------------------------------
        '��ǰҳ��Loadʱ���Ƚ�����ڣ�������ԴID�Ļ�ȡ��������
        'Dim lngResID As Long = VLng("PAGE_COMM_RESID")
        '----------------------------------------------------------

        WebUtilities.InitialDataGrid(DataGrid1, 17) '����DataGrid��ʾ����
        Dim lngResID As Long = AspPage.RLng("noderesid", Request)
        Dim datComm As DataComm = GetDataComm(lngResID)
        CreateDataGridColumn(datComm)
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '---------------------------------------------------------------
                '����DataGridÿ�е�ΨһID��OnClick���������ڴ��ط�������Ϣ����Ӧ���������޸ġ�ɾ����
                Dim strRecID As String = row.Cells(0).Text.Trim()
                row.Attributes.Add("RECID", strRecID)
                row.Attributes.Add("REC_EMAIL", row.Cells(2).Text.Trim())
                row.Attributes.Add("REC_HANDPHONE", row.Cells(3).Text.Trim())
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")
                '---------------------------------------------------------------

                '---------------------------------------------------------------
                'ʼ��ѡ���û�������ļ�¼
                If strRecID <> "" And RStr("RECID") = strRecID Then
                    row.Attributes.Add("bgColor", "#C4D9F9")
                End If
                '---------------------------------------------------------------
            Next
        End If
    End Sub

    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        DataGrid1.CurrentPageIndex = e.NewPageIndex

        ShowAddrbook(VLng("PAGE_COMM_RESID"), VStr("PAGE_COMM_WHERE"), VStr("PAGE_COMM_ORDERBY"))
    End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        'ÿ�ε���滻�������
        If VStr("PAGE_COMM_ORDERBY_TYPE") = "ASC" Then
            ViewState("PAGE_COMM_ORDERBY_TYPE") = "DESC"
        Else
            ViewState("PAGE_COMM_ORDERBY_TYPE") = "ASC"
        End If

        '������ʾ����
        ViewState("PAGE_COMM_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("PAGE_COMM_ORDERBY_TYPE")

        ShowAddrbook(VLng("PAGE_COMM_RESID"), VStr("PAGE_COMM_WHERE"), VStr("PAGE_COMM_ORDERBY"))
    End Sub

    Private Sub lbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSearch.Click
        Dim lngResID As Long = AspPage.RLng("noderesid", Request)
        If lngResID = 0 Then Return

        Dim strColRef As String = CType(DataGrid1.Columns(1), BoundColumn).DataField
        Dim strColEmail As String = CType(DataGrid1.Columns(2), BoundColumn).DataField
        Dim strColPhone As String = CType(DataGrid1.Columns(3), BoundColumn).DataField

        Dim strSearchVal As String = txtSearchValue.Text
        Dim strWhere As String = strColRef & " LIKE '%" & strSearchVal & "%' OR " & strColEmail & " LIKE '%" & strSearchVal & "%' OR " & strColPhone & " LIKE '%" & strSearchVal & "%'"

        ShowAddrbook(lngResID, strWhere, VStr("PAGE_COMM_ORDERBY"))
    End Sub

    Private Sub lbtnCancelSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCancelSearch.Click
        Try
            Dim lngResID As Long = AspPage.RLng("noderesid", Request)
            If lngResID = 0 Then Return

            ViewState("PAGE_COMM_WHERE") = ""
            ViewState("PAGE_COMM_ORDERBY") = ""
            ShowAddrbook(lngResID)

            txtSearchValue.Text = ""
        Catch ex As Exception
            PromptMsg("��Ϣ��ѯ�쳣ʧ�ܣ��޷��������ݿ⣡", ex, True)
        End Try
    End Sub

    Private Sub lbtnAddReceiver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddReceiver.Click
        Dim strEmail As String = RStr("REC_EMAIL").Trim()
        If strEmail <> "" And strEmail.ToLower() <> "&nbsp;" Then lbxEmail.Items.Add(strEmail)

        Dim strHandphone As String = RStr("REC_HANDPHONE").Trim()
        If strHandphone <> "" And strHandphone.ToLower() <> "&nbsp;" Then lbxSms.Items.Add(strHandphone)
    End Sub

    Private Sub lbtnAddEmailReceiver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddEmailReceiver.Click
        Dim strEmail As String = RStr("REC_EMAIL")
        If strEmail.Trim <> "" And strEmail.Trim.ToLower <> "&nbsp;" Then lbxEmail.Items.Add(strEmail)
    End Sub

    Private Sub lbtnAddSmsReceiver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddSmsReceiver.Click
        Dim strHandphone As String = RStr("REC_HANDPHONE")
        If strHandphone.Trim() <> "" And strHandphone.Trim.ToLower <> "&nbsp;" Then lbxSms.Items.Add(strHandphone)
    End Sub

    Private Sub lbtnRemoveEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRemoveEmail.Click
        If lbxEmail.SelectedIndex >= 0 Then lbxEmail.Items.RemoveAt(lbxEmail.SelectedIndex)
    End Sub

    Private Sub lbtnRemovePhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRemovePhone.Click
        If lbxSms.SelectedIndex >= 0 Then lbxSms.Items.RemoveAt(lbxSms.SelectedIndex)
    End Sub

    '------------------------------------------------------------------
    '��ȡָ����Դ�б���ֶ�����
    '------------------------------------------------------------------
    Private Function GetDataComm(ByVal lngResID As Long) As DataComm
        Dim datRes As DataResource = ResFactory.ResService.GetOneResource(CmsPass, lngResID)
        Dim datComm As DataComm
        datComm.strEmail = datRes.CommColEmail
        datComm.strPhone = datRes.CommColHandphone
        datComm.strReference = datRes.CommColRef

        Return datComm
    End Function

    '------------------------------------------------------------------
    '������ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn(ByVal datComm As DataComm)
        Dim intWidth As Integer = 0

        DataGrid1.Columns.Clear()

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "ID"
        col.DataField = "ID"
        col.SortExpression = col.DataField
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "�ο��ֶ�"
        col.DataField = datComm.strReference
        col.SortExpression = col.DataField
        col.ItemStyle.Width = Unit.Pixel(120)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 120

        col = New BoundColumn
        col.HeaderText = "�����ʼ�"
        col.DataField = datComm.strEmail
        col.SortExpression = col.DataField
        col.ItemStyle.Width = Unit.Pixel(160)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 160

        col = New BoundColumn
        col.HeaderText = "�ֻ�����"
        col.DataField = datComm.strPhone
        col.SortExpression = col.DataField
        col.ItemStyle.Width = Unit.Pixel(80)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 80

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '----------------------------------------------------------
    '��DataGrid����
    '----------------------------------------------------------
    Private Sub ShowAddrbook(ByVal lngResID As Long, Optional ByVal strWhere As String = "", Optional ByVal strOrderBy As String = "", Optional ByVal blnFromSearch As Boolean = False)
        If lngResID = 0 Then Return

        Dim datRes As DataResource = ResFactory.ResService.GetOneResource(CmsPass, lngResID)
        If Not (datRes Is Nothing) Then
            If datRes.ResTableType <> "" Then
                Dim ds As DataSet = ResFactory.TableService(datRes.ResTableType).GetHostTableData(CmsPass, lngResID, strWhere, strOrderBy)
                If Not (ds Is Nothing) Then
                    Dim blnError As Boolean = False
                    Try
                        DataGrid1.VirtualItemCount = ds.Tables(0).DefaultView.Count '��ҳ��
                        DataGrid1.DataSource = ds.Tables(0).DefaultView
                        DataGrid1.DataBind()
                    Catch ex As Exception
                        blnError = True
                    End Try
                    If blnError = True Then
                        DataGrid1.CurrentPageIndex = 0
                        DataGrid1.VirtualItemCount = ds.Tables(0).DefaultView.Count '��ҳ��
                        DataGrid1.DataSource = ds.Tables(0).DefaultView
                        DataGrid1.DataBind()
                    End If
                End If
            End If
        End If
    End Sub

    '----------------------------------------------------------
    '��ȡѡ�е������ʼ����ֻ�
    '----------------------------------------------------------
    Public Function GetAllEmailPhones(ByRef lbx As ListBox) As String
        Dim strRtn As String = ""

        Dim li As ListItem
        For Each li In lbx.Items
            strRtn &= li.Text & ", "
        Next

        If strRtn.Length >= 2 Then strRtn = strRtn.Substring(0, strRtn.Length - 2) 'ȥ������", "

        Return strRtn
    End Function

    '----------------------------------------------------------------------------------------
    'Load�Զ�������ṹ
    '----------------------------------------------------------------------------------------
    Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        '----------------------------------------------------------------------------------------
        '׼�����ṹ��صĲ�����Ϣ
        Dim strResUrl As String = "/cmsweb/cmsothers/CommAddrbook.aspx"
        Dim strResTarget As String = "content"

        Dim strRootUrl As String = ""
        Dim strRootTarget As String = ""
        Dim strDepUrl As String = ""
        Dim strDepTarget As String = ""
        If CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ONEDEP_RES_ONLY") = True Then
            '����ʾһ�����ŵ���Դ��Ϣ������ÿ�ε�����Žڵ����ˢ��ҳ��
            strDepUrl = "/cmsweb/cmsothers/CommAddrbook.aspx"
            strDepTarget = "_self"

            strRootUrl = "/cmsweb/cmsothers/CommAddrbook.aspx"
            strRootTarget = "_self"
        Else '������Դ��һ������ʾ������ÿ�ε�����Žڵ���ҳ����Ӧ
        End If
        '----------------------------------------------------------------------------------------

        'Load���ṹ
        WebTreeDepResource.LoadResTreeView(pst, Request, Response, strRootUrl, strRootTarget, strDepUrl, strDepTarget, strResUrl, strResTarget, True)
    End Sub
End Class

End Namespace
