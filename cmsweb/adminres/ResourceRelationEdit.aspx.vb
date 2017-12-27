Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceRelationEdit
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

        If VLng("PAGE_RESID1") = 0 Then
            ViewState("PAGE_RESID1") = RLng("mnuresid1")
        End If
        If VLng("PAGE_RESID2") = 0 Then
            ViewState("PAGE_RESID2") = RLng("mnuresid2")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '��������Դ������
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID1")).ResName

        If RLng("selresid") <> 0 Then
            ViewState("PAGE_RESID2") = RLng("selresid") '�մ�ѡ����Դ�������
        End If

        LoadResColumns(VLng("PAGE_RESID1"), ListBox1)   'Load�����ֶ��б�
        LoadResColumns(VLng("PAGE_RESID2"), ListBox2)  'Load�ӱ��ֶ��б�

        'Load DataGrid
        WebUtilities.InitialDataGrid(DataGrid1) '��ʼ��DataGrid����
        CreateDataGridColumn()
        GridDataBind(VLng("PAGE_RESID1"), VLng("PAGE_RESID2"))  '������

        '-----------------------------------------------------------
        '�ж��Ƿ��ṩ�����������
        btnAddInputRelatedCol.Visible = CmsFunc.IsEnable("FUNC_RELTABLE_INPUT")

        '�ж��Ƿ��ṩ��ʾ��������
        btnAddShowRelation.Visible = CmsFunc.IsEnable("FUNC_RELTABLE_SHOW")

        '�ж��Ƿ��ṩ�����������
        btnAddCalcRelation.Visible = CmsFunc.IsEnable("FUNC_RELTABLE_CALC")
        '-----------------------------------------------------------
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAddMainRelatedCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMainRelatedCol.Click
        AddRelatedCol(TabRelationType.MainRelationCol)
    End Sub

    Private Sub btnAddInputRelatedCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddInputRelatedCol.Click
        AddRelatedCol(TabRelationType.InputRelationCol)
    End Sub

    Private Sub btnAddShowRelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddShowRelation.Click
        AddRelatedCol(TabRelationType.ShowRelationCol)
    End Sub

    Private Sub btnAddCalcRelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCalcRelation.Click
        AddRelatedCol(TabRelationType.CalcRelationCol)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnSelectRelRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelectRelRes.Click
        Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/ResourceRelationEdit.aspx?mnuresid1=" & VLng("PAGE_RESID1") '& "&mnuresid2=" & VLng("PAGE_RESID2")
        Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx?mnuresid=" & VLng("PAGE_RESID1"), False)
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            '��ȡ������Ϣ�ļ�¼ID
            Dim strTemp As String = e.Item.Cells(0).Text
            If IsNumeric(strTemp.Trim()) Then
                Dim lngAiid As Long = CLng(strTemp.Trim())
                CmsTableRelation.DelRelatedColumn(CmsPass, lngAiid) 'ɾ��������Ϣ
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind(VLng("PAGE_RESID1"), VLng("PAGE_RESID2"))  '������
    End Sub

    '--------------------------------------------------------------
    '��ӱ���ع�ϵ
    '--------------------------------------------------------------
    Private Sub AddRelatedCol(ByVal lngRelType As Long)
        Try
            CmsTableRelation.AddRelatedColumn(CmsPass, VLng("PAGE_RESID1"), ListBox1.SelectedItem.Value, VLng("PAGE_RESID2"), ListBox2.SelectedItem.Value, lngRelType)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind(VLng("PAGE_RESID1"), VLng("PAGE_RESID2"))  '������
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "RT_AIID" '�ؼ��ֶ�
        col.DataField = "RT_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "�����ֶ�"
        col.DataField = "RT_TAB1_COLDISPNAME"
        col.ItemStyle.Width = Unit.Pixel(170)
        DataGrid1.Columns.Add(col)
        intWidth += 170

        col = New BoundColumn
        col.HeaderText = "�ӱ��ֶ�"
        col.DataField = "RT_TAB2_COLDISPNAME"
        col.ItemStyle.Width = Unit.Pixel(170)
        DataGrid1.Columns.Add(col)
        intWidth += 170

        col = New BoundColumn
        col.HeaderText = "��������"
        col.DataField = "RT_TYPE2"
        col.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(col)
        intWidth += 100

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(50)
        DataGrid1.Columns.Add(colDel)
        intWidth += 50

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind(ByVal lngResID1 As Long, ByVal lngResID2 As Long)
        Try
            If lngResID1 > 0 And lngResID2 > 0 Then
                Dim ds As DataSet = CmsTableRelation.GetAllRelatedColumnsForDesign(CmsPass, lngResID1, lngResID2)
                DataGrid1.DataSource = ds.Tables(0).DefaultView
                DataGrid1.DataBind()
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

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
