Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvDictionary
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResName2.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblFieldName.Text = VStr("PAGE_COLDISPNAME")
        lblFieldType.Text = CTableStructure.GetColTypeDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Try
            '-----------------------------------------------------------------
            If RLng("selresid") <> 0 Then '�մ�ѡ����Դ�������
                ViewState("PAGE_RESID2") = RLng("selresid")
            Else
                ViewState("PAGE_RESID2") = CTableColAdvDictionary.GetDictResID(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            End If
            lblDictResName.Text = ResFactory.ResService.GetResName(CmsPass, VLng("PAGE_RESID2"))
            LoadResColumns(VLng("PAGE_RESID"), ListBox1)    'Load�����ֶ��б�
            LoadResColumns(VLng("PAGE_RESID2"), ListBox2)  'Load�ӱ��ֶ��б�
            '-----------------------------------------------------------------

            chkAddDictRes.Checked = CTableColAdvDictionary.IsAddDictResEnabled(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            chkEditDictRes.Checked = CTableColAdvDictionary.IsEditDictResEnabled(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnSelDictRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelDictRes.Click
        Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME")
        Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CTableColAdvDictionary.SaveAdvDictSettings(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), chkAddDictRes.Checked, chkEditDictRes.Checked)
    End Sub

    Private Sub btnAddDicField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDicField.Click
        If ListBox1.SelectedItem Is Nothing Or ListBox2.SelectedItem Is Nothing Then
            PromptMsg("��ѡ���ֵ������ƥ���ֶΣ�")
            Return
        End If
        If ListBox1.SelectedItem.Text Is Nothing Or ListBox2.SelectedItem.Text Is Nothing Then
            PromptMsg("��ѡ���ֵ������ƥ���ֶΣ�")
            Return
        End If

        Try
            CTableColAdvDictionary.SaveDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), ListBox1.SelectedItem.Value, VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, ADVDICT_COLTYPE.Matching, chkAddDictRes.Checked, chkEditDictRes.Checked)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
    End Sub

    Private Sub btnAddDictRefCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDictRefCol.Click
        If ListBox2.SelectedItem Is Nothing Then
            PromptMsg("��ѡ���ֵ�ο��ֶΣ�")
            Return
        End If
        If ListBox2.SelectedItem.Text Is Nothing Then
            PromptMsg("��ѡ���ֵ�ο��ֶΣ�")
            Return
        End If

        Try
            CTableColAdvDictionary.SaveDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), "", VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, ADVDICT_COLTYPE.Reference, chkAddDictRes.Checked, chkEditDictRes.Checked)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
    End Sub

    Private Sub btnAddFilterCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFilterCol.Click
        If ListBox2.SelectedItem Is Nothing Then
            PromptMsg("��ѡ���ֵ�ο��ֶΣ�")
            Return
        End If
        If ListBox2.SelectedItem.Text Is Nothing Then
            PromptMsg("��ѡ���ֵ�ο��ֶΣ�")
            Return
        End If

        Try
            CTableColAdvDictionary.SaveDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), ListBox1.SelectedItem.Value, VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, ADVDICT_COLTYPE.Filter, chkAddDictRes.Checked, chkEditDictRes.Checked)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
    End Sub

    Private Sub btnDictWhere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDictWhere.Click
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & VStr("PAGE_COLNAME")
            Dim strUrl As String = "/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & VLng("PAGE_RESID2") & "&advdict_hostresid=" & VLng("PAGE_RESID") & "&mtstype=" & MTSearchType.AdvDictFilter & "&mnuempid=" + CmsPass.Employee.ID.Trim + "&mnucolname=" & VStr("PAGE_COLNAME")
        Response.Redirect(strUrl, True)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ���в��ִ�������ʹ��
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '���ÿͻ��˵ļ�¼ID��Javascript��������1���Ǹ߼��ֶζ������ԴID
                Dim strRecID As String = row.Cells(0).Text.Trim()
                If IsNumeric(strRecID) Then
                    row.Attributes.Add("RECID", strRecID) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()

                    If lngRecIDClicked > 0 And lngRecIDClicked = CLng(strRecID) Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '�޸ı������¼�ı�����ɫ
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            '��ȡ������Ϣ�ļ�¼ID
            Dim strTemp As String = e.Item.Cells(0).Text
            If IsNumeric(strTemp.Trim()) Then
                Dim lngAiid As Long = CLng(strTemp.Trim())
                CTableColAdvDictionary.DelDictionary(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), lngAiid)    'ɾ��������Ϣ
                CTableColAdvDictionary.SaveAdvDictSettings(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), chkAddDictRes.Checked, chkEditDictRes.Checked)
            Else
                PromptMsg("ɾ��ʧ�ܣ����ݿ����ݲ�һ�£�", Nothing, True)
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
    End Sub

    '---------------------------------------------------------------
    '��ʾ��Դ�ֶ��б�
    '---------------------------------------------------------------
    Private Sub LoadResColumns(ByVal lngResID As Long, ByRef lstCols As ListBox)
        If lngResID = 0 Then Return

        If lstCols.Items.Count <= 0 Then
            lstCols.Items.Clear() '����б�

            Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(CmsPass, lngResID, True, True)
            Dim datCol As DataTableColumn
            For Each datCol In alistColumns
                lstCols.Items.Add(New ListItem(datCol.ColDispName, datCol.ColName))
            Next
            alistColumns.Clear()
            alistColumns = Nothing
        End If
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "CDZ2_AIID" '�ؼ��ֶ�
        col.DataField = "CDZ2_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "�����ֶ�"
        col.DataField = "CDZ2_COL1_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "�ֵ��ֶ�"
        col.DataField = "CDZ2_COL2_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "�ֶι�ϵ"
        col.DataField = "CDZ2_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 80

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(colDel)
        col = Nothing
        intWidth += 100

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID1 As Long, ByVal strMainColName As String, ByVal lngResID2 As Long)
        If lngResID1 = 0 Or lngResID2 = 0 Then Return

        Try
            Dim ds As DataSet = CTableColAdvDictionary.GetDictionaryByDictRes(CmsPass, lngResID1, strMainColName, lngResID2)
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            ds.Tables(0).Columns.Add("CDZ2_COL1_NAME")
            ds.Tables(0).Columns.Add("CDZ2_COL2_NAME")
            ds.Tables(0).Columns.Add("CDZ2_TYPE2")
            For Each drv In dv
                '��ȡ�ֶ�1��ʾ����
                If DbField.GetStr(drv, "CDZ2_COL1") = "" Then
                    drv("CDZ2_COL1_NAME") = ""
                Else
                    drv("CDZ2_COL1_NAME") = CTableStructure.GetColDispName(CmsPass, lngResID1, DbField.GetStr(drv, "CDZ2_COL1"))
                End If

                '��ȡ�ֶ�2��ʾ����
                drv("CDZ2_COL2_NAME") = CTableStructure.GetColDispName(CmsPass, lngResID2, DbField.GetStr(drv, "CDZ2_COL2"))

                '��ȡ�ֶι�ϵ
                Dim intType As ADVDICT_COLTYPE = CType(DbField.GetInt(drv, "CDZ2_TYPE"), ADVDICT_COLTYPE)
                If intType = ADVDICT_COLTYPE.Matching Then
                    drv("CDZ2_TYPE2") = "ƥ���ֶ�"
                ElseIf intType = ADVDICT_COLTYPE.Reference Then
                    drv("CDZ2_TYPE2") = "�ο��ֶ�"
                ElseIf intType = ADVDICT_COLTYPE.Filter Then
                    drv("CDZ2_TYPE2") = "�����ֶ�"
                End If
            Next

            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("��ѡ����Ҫ�����ĸ߼��ֶζ��壡")
                Return
            End If
            CTableColAdvDictionary.MoveUp(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("��ѡ����Ҫ�����ĸ߼��ֶζ��壡")
                Return
            End If
            CTableColAdvDictionary.MoveToFirst(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("��ѡ����Ҫ�����ĸ߼��ֶζ��壡")
                Return
            End If
            CTableColAdvDictionary.MoveDown(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Try
            Dim lngAiid As Long = GetRecIDOfGrid()
            If lngAiid = 0 Then
                PromptMsg("��ѡ����Ҫ�����ĸ߼��ֶζ��壡")
                Return
            End If
            CTableColAdvDictionary.MoveToLast(CmsPass, VLng("PAGE_RESID"), lngAiid, VStr("PAGE_COLNAME"))

            GridDataBind(VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), VLng("PAGE_RESID2"))    '������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
