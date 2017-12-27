Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceSyncColumn
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

        If VLng("PAGE_SYNC_LISTID") = 0 Then
            ViewState("PAGE_SYNC_LISTID") = RLng("sync_listid")
        End If
        If VLng("PAGE_SYNC_LISTID") <> 0 Then
            Dim datSyncList As DataResSyncList = CmsResourceSync.GetOneSyncList(CmsPass, VLng("PAGE_SYNC_LISTID"))
            ViewState("PAGE_SYNC_RESID") = datSyncList.lngSYNLST_SYNCRESID
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblHostResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblSyncResName.Text = CmsPass.GetDataRes(VLng("PAGE_SYNC_RESID")).ResName

        LoadResColumns(VLng("PAGE_RESID"), ListBox1)   'Load�����ֶ��б�
        LoadResColumns(VLng("PAGE_SYNC_RESID"), ListBox2)  'Load�ӱ��ֶ��б�

        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAddHostCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddHostCol.Click
        AddSyncCol(ResSyncColType.IsHostSyncCol)
        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    Private Sub btnAddSyncCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSyncCol.Click
        AddSyncCol(ResSyncColType.IsNormalSyncCol)
        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    Private Sub btnAddCondCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCondCol.Click
        AddSyncCol(ResSyncColType.IsCondSyncCol)
        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    Private Sub btnAddConstCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddConstCol.Click
        AddSyncCol(ResSyncColType.IsConstSyncCol)
        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngSyncColID As Long = GetRecIDOfGrid()
        If lngSyncColID = 0 Then
            PromptMsg("��ѡ����Ҫ������ͬ���ֶΡ�")
            Return
        End If

        Try
            CmsResourceSync.MoveUpSyncCol(CmsPass, lngSyncColID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngSyncColID As Long = GetRecIDOfGrid()
        If lngSyncColID = 0 Then
            PromptMsg("��ѡ����Ҫ������ͬ���ֶΡ�")
            Return
        End If

        Try
            CmsResourceSync.MoveDownSyncCol(CmsPass, lngSyncColID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

        WebUtilities.InitialDataGrid(DataGrid1) '��ʼ��DataGrid����
        CreateDataGridColumn()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ���в��ִ�������ʹ��
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '���ÿͻ��˵ļ�¼ID��Javascript�������������ǹ��������ԴID
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
                Dim lngSyncColID As Long = CLng(strTemp.Trim())
                CmsResourceSync.DelSyncResColumn(CmsPass, lngSyncColID)
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    '--------------------------------------------------------------
    '��ӱ���ع�ϵ
    '--------------------------------------------------------------
    Private Sub AddSyncCol(ByVal intSyncColType As ResSyncColType)
        Try
            Dim strCol1 As String = ""
            If Not ListBox1 Is Nothing AndAlso Not ListBox1.SelectedItem Is Nothing Then
                strCol1 = ListBox1.SelectedItem.Value
            End If
            Dim strCol2 As String = ""
            If Not ListBox2 Is Nothing AndAlso Not ListBox2.SelectedItem Is Nothing Then
                strCol2 = ListBox2.SelectedItem.Value
            End If
            CmsResourceSync.AddSyncColumn(CmsPass, VLng("PAGE_SYNC_LISTID"), strCol1, strCol2, intSyncColType, strCol1, "=", txtCondVal.Text.Trim())
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
        GridDataBind(VLng("PAGE_RESID")) '������
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "SYNCOL_ID" '�ؼ��ֶ�
        col.DataField = "SYNCOL_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "SYNCOL_LISTID"
        col.DataField = "SYNCOL_LISTID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "����Դ�ֶ�"
        col.DataField = "SYNCOL_COLNAME122"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "ͬ����Դ�ֶ�"
        col.DataField = "SYNCOL_COLNAME222"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "�ֶ�����"
        col.DataField = "SYNCOL_COLTYPE22"
        col.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(col)
        intWidth += 80

        col = New BoundColumn
        col.HeaderText = "��������"
        col.DataField = "SYNCOL_CONDSIGN"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "����ֵ"
        col.DataField = "SYNCOL_CONDVAL"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "����ֵ"
        col.DataField = "SYNCOL_CONSTVAL"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        intWidth += 70

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(40)
        DataGrid1.Columns.Add(colDel)
        intWidth += 40

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID As Long)
        Try
            Dim ds As DataSet = GetDataSetForGrid(lngResID)
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    '------------------------------------------------------------------
    '��ȡ���й�������б�
    '------------------------------------------------------------------
    Private Function GetDataSetForGrid(ByVal lngResID As Long) As DataSet
        Dim ds As DataSet = CmsResourceSync.GetSyncColumns(CmsPass, VLng("PAGE_SYNC_LISTID"))
        ds.Tables(0).Columns.Add("SYNCOL_COLNAME122")
        ds.Tables(0).Columns.Add("SYNCOL_COLNAME222")
        ds.Tables(0).Columns.Add("SYNCOL_COLTYPE22")
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            drv.BeginEdit()
            drv("SYNCOL_COLNAME122") = CTableStructure.GetColDispName(CmsPass, lngResID, DbField.GetStr(drv, "SYNCOL_COLNAME1"))
            drv("SYNCOL_COLNAME222") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_SYNC_RESID"), DbField.GetStr(drv, "SYNCOL_COLNAME2"))

            Dim intSyncColType As ResSyncColType = CType(DbField.GetInt(drv, "SYNCOL_COLTYPE"), ResSyncColType)
            If intSyncColType = ResSyncColType.IsHostSyncCol Then
                drv("SYNCOL_COLTYPE22") = "�������ֶ�"
            ElseIf intSyncColType = ResSyncColType.IsNormalSyncCol Then
                drv("SYNCOL_COLTYPE22") = "ͬ���ֶ�"
            ElseIf intSyncColType = ResSyncColType.IsCondSyncCol Then
                drv("SYNCOL_COLTYPE22") = "�����ֶ�"
            ElseIf intSyncColType = ResSyncColType.IsConstSyncCol Then
                drv("SYNCOL_COLTYPE22") = "�����ֶ�"
            End If
            drv.EndEdit()
        Next
        Return ds
    End Function

    '---------------------------------------------------------------
    '������
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
End Class

End Namespace
