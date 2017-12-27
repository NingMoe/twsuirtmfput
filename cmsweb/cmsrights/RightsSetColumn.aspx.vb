Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSetColumn
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

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

        If VStr("PAGE_GAINERID") = "" Then
            ViewState("PAGE_GAINERID") = RStr("gainerid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = CTableStructure.GetColumnShowsByDataset(CmsPass, VLng("PAGE_RESID"), True, True)
            PrepareDataSet(ds) '׼���ֶ���ʾ״̬��DataSet
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("��ȡ�Զ�����ֶ���ʾ��Ϣ����ʾʱ����", ex)
        End Try
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

        WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
        CreateDataGridColumn()
    End Sub

    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        GridDataBind()
    End Sub

    Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Try
            Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)

            '�����ж���ʱ��Ҫ�Ĳ���
            Dim hashColKeyValue As New Hashtable
            'Dim strColName As String = ""
            Dim i As Integer
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim ctl As System.Web.UI.Control
                Try
                    ctl = e.Item.Cells(i).Controls(0) '�����л��ڴ��в���Exception
                Catch ex As Exception
                    ctl = Nothing
                End Try
                If Not (ctl Is Nothing) Then
                    If TypeOf ctl Is TextBox Then
                        Dim ctlCell As TextBox = CType(ctl, TextBox)
                        hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), ctlCell.Text)
                    ElseIf TypeOf ctl Is CheckBox Then
                        Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                        'hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), ctlCell.Text)
                        If ctlCell.Checked = True Then
                            hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), "��ʾ")
                        Else
                            hashColKeyValue.Add(hashColumnNames(DataGrid1.Columns(i).HeaderText), "����ʾ")
                        End If
                    End If
                End If
            Next i

            Dim strColName As String = e.Item.Cells(0).Text
            If CStr(hashColKeyValue("CS_SHOW_COLUMN")) = "��ʾ" Then
                CmsRights.SetColumnHasRights(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID"), RightsName.Resource, strColName)
            Else
                CmsRights.SetColumnHasNoRights(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID"), RightsName.Resource, strColName)
            End If
        Catch ex As Exception
            SLog.Err("����Ȩ��������ʧ�ܣ�", ex)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
                Dim i As Integer
                For i = 0 To DataGrid1.Columns.Count - 1
                    Dim ctl As System.Web.UI.Control
                    Try
                        ctl = e.Item.Cells(i).Controls(0) '�����л��ڴ��в���Exception
                    Catch ex As Exception
                    End Try
                    If Not (ctl Is Nothing) Then
                        If TypeOf ctl Is TextBox Then
                            Dim ctlCell As TextBox = CType(ctl, TextBox)
                            ctlCell.Width = Unit.Percentage(100)
                        ElseIf TypeOf ctl Is CheckBox Then
                            Dim oneRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                            Dim strOldFieldValue As String = DbField.GetStr(oneRow, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                            Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                            If strOldFieldValue = "��ʾ" Then
                                ctlCell.Checked = True
                            Else
                                ctlCell.Checked = False
                            End If
                            ctlCell.Width = Unit.Percentage(100)
                        End If
                    End If
                Next
            End If
        Else
        End If
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
        Dim hashColumnNames As New Hashtable

        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "�ڲ��ֶ���"
        col.DataField = "CS_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ڲ��ֶ���", "CS_COLNAME")

        col = New BoundColumn
        col.HeaderText = "�ֶ�����"
        col.DataField = "CD_DISPNAME"
        col.ItemStyle.Width = Unit.Pixel(300)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 300
        hashColumnNames.Add("�ֶ�����", "CD_DISPNAME")

        Dim colTemplate As New TemplateColumn
        colTemplate.HeaderText = "��ʾ"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CS_SHOW_COLUMN", "��ʾ")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CS_SHOW_COLUMN", "��ʾ")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("��ʾ"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CS_SHOW_COLUMN", "��ʾ")
        colTemplate.ItemStyle.Width = Unit.Pixel(60)
        DataGrid1.Columns.Add(colTemplate)
        intWidth += 60
        hashColumnNames.Add("��ʾ", "CS_SHOW_COLUMN")

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "�༭"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "ȡ��"
        colEdit.EditText = "�༭"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colEdit)
        intWidth += 70

        DataGrid1.Width = Unit.Pixel(intWidth)

        '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
        ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
    End Sub

    '------------------------------------------------------------------
    '��ȡDataGrid����Ҫ��ʾ��CheckBox
    '------------------------------------------------------------------
    Private Function GetColumnTypeCheckbox(ByVal strCheckboxText As String) As CheckBox
        Dim objCtrl As New CheckBox
        objCtrl.Checked = False
        objCtrl.Text = strCheckboxText

        GetColumnTypeCheckbox = objCtrl
        objCtrl = Nothing
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------------
    '׼���ֶ���ʾ״̬��DataSet
    '-----------------------------------------------------------------
    Private Sub PrepareDataSet(ByRef ds As DataSet)
        '��Ȩ�ޱ��л�ȡȨ�޻���ߵ������ֶ���ʾ����
        Dim strColsOfNoRights As String = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID")).strQX_ACCESS_COLS
        If strColsOfNoRights <> "" Then strColsOfNoRights = "," & strColsOfNoRights & ","

        Dim strNewGainerColumns As String = ""
        '����1����ʾ�����ֶ�
        ds.Tables(0).Columns.Add("CS_SHOW_COLUMN")
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            If strColsOfNoRights = "" Then
                drv("CS_SHOW_COLUMN") = "��ʾ" '��Ӧ�ü̳���ʾ���ã�Ȩ�����ú���ʾ�����޹�
                ''֮ǰδ���ù��ֶ���ʾ���ƣ���̳���Դ���е���ʾ����
                'If DbField.GetLng(drv, "CS_SHOW_ENABLE") = 1 Then
                '    strNewGainerColumns &= strColName & ","
                '    drv("CS_SHOW_COLUMN") = "��ʾ"
                'Else
                '    drv("CS_SHOW_COLUMN") = "����ʾ"
                'End If
            Else
                Dim strColName As String = DbField.GetStr(drv, "CS_COLNAME")
                If strColsOfNoRights.IndexOf("," & strColName & ",") >= 0 Then
                    drv("CS_SHOW_COLUMN") = "����ʾ"
                Else
                    drv("CS_SHOW_COLUMN") = "��ʾ"
                End If
            End If
        Next

        'If strColsOfNoRights = "" Then '֮ǰδ���ù��ֶ���ʾ���ƣ���̳���Դ���е���ʾ����
        '    CmsRights.SetGainerColumns(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_GAINERID"), RightsName.Resource, strNewGainerColumns)
        'End If
    End Sub
End Class
End Namespace
