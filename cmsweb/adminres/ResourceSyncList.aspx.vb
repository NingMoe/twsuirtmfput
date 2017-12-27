Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceSyncList
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblPageTitle.Text = "����ͬ��"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind(VLng("PAGE_RESID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("CMSBP_ResourceSyncListEdit") = "/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=0"
        Response.Redirect("/cmsweb/adminres/ResourceSyncListEdit.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=0", False)
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("��ѡ����Ҫ����������ͬ�����ã�")
            Return
        End If

        Session("CMSBP_ResourceSyncListEdit") = "/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & lngSyncListID
        Response.Redirect("/cmsweb/adminres/ResourceSyncListEdit.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & lngSyncListID, False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("��ѡ����Ҫ����������ͬ�����ã�")
            Return
        End If

        Try
            CmsResourceSync.DelSyncResource(CmsPass, lngSyncListID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnSyncCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyncCol.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("��ѡ����Ҫ����������ͬ�����ã�")
            Return
        End If

        Session("CMSBP_ResourceSyncColumn") = "/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&URLRECID=" & lngSyncListID
        Response.Redirect("/cmsweb/adminres/ResourceSyncColumn.aspx?mnuresid=" & VLng("PAGE_RESID") & "&sync_listid=" & lngSyncListID, False)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("��ѡ����Ҫ����������ͬ�����ã�")
            Return
        End If

        Try
            CmsResourceSync.MoveUpSyncList(CmsPass, lngSyncListID)
            GridDataBind(VLng("PAGE_RESID"))
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
        Dim lngSyncListID As Long = GetRecIDOfGrid()
        If lngSyncListID = 0 Then
            PromptMsg("��ѡ����Ҫ����������ͬ�����ã�")
            Return
        End If

        Try
            CmsResourceSync.MoveDownSyncList(CmsPass, lngSyncListID)
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

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        '��1�б����Ǽ�¼ID
        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "SYNLST_ID"
        col.DataField = "SYNLST_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "������Դ"
        col.DataField = "SYNLST_HOSTRESID22"
        col.ItemStyle.Width = Unit.Pixel(200)
        DataGrid1.Columns.Add(col)
        intWidth += 200

        col = New BoundColumn
        col.HeaderText = "ͬ����Դ"
        col.DataField = "SYNLST_SYNCRESID22"
        col.ItemStyle.Width = Unit.Pixel(200)
        DataGrid1.Columns.Add(col)
        intWidth += 200

        col = New BoundColumn
        col.HeaderText = "ͬ������"
        col.DataField = "SYNLST_ACTION22"
        col.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(col)
        intWidth += 80

        col = New BoundColumn
        col.HeaderText = "ͬ������"
        col.DataField = "SYNLST_SYNCTYPE22"
        col.ItemStyle.Width = Unit.Pixel(200)
        DataGrid1.Columns.Add(col)
        intWidth += 200

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID As Long)
        Dim ds As DataSet = GetDataSetForGrid(lngResID)
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub

    '------------------------------------------------------------------
    '��ȡ���й�������б�
    '------------------------------------------------------------------
    Private Function GetDataSetForGrid(ByVal lngResID As Long) As DataSet
        Dim ds As DataSet = CmsResourceSync.GetSyncList(CmsPass, lngResID)
        ds.Tables(0).Columns.Add("SYNLST_HOSTRESID22")
        ds.Tables(0).Columns.Add("SYNLST_SYNCRESID22")
        ds.Tables(0).Columns.Add("SYNLST_ACTION22")
        ds.Tables(0).Columns.Add("SYNLST_SYNCTYPE22")
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            drv.BeginEdit()
            drv("SYNLST_HOSTRESID22") = CmsPass.GetDataRes(lngResID).ResName
            drv("SYNLST_SYNCRESID22") = CmsPass.GetDataRes(DbField.GetLng(drv, "SYNLST_SYNCRESID")).ResName

            Dim intSyncAction As ResSyncAction = CType(DbField.GetInt(drv, "SYNLST_ACTION"), ResSyncAction)
            If intSyncAction = ResSyncAction.AddRecord Then
                drv("SYNLST_ACTION22") = "��Ӽ�¼"
            ElseIf intSyncAction = ResSyncAction.EditRecord Then
                drv("SYNLST_ACTION22") = "�޸ļ�¼"
            ElseIf intSyncAction = ResSyncAction.DelRecord Then
                drv("SYNLST_ACTION22") = "ɾ����¼"
            End If

            Dim intSyncType As ResSyncType = CType(DbField.GetInt(drv, "SYNLST_SYNCTYPE"), ResSyncType)
            If intSyncType = ResSyncType.AddOrEditRecord Then
                drv("SYNLST_SYNCTYPE22") = "��ͬ����Դ����ӻ��޸ļ�¼"
            ElseIf intSyncType = ResSyncType.AddRecordOnly Then
                drv("SYNLST_SYNCTYPE22") = "��ͬ����Դ��������Ӽ�¼"
            End If

            drv.EndEdit()
        Next
        Return ds
    End Function
End Class

End Namespace
