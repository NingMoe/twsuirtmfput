Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceColumnShowSet
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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

        GetColName()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If ResFactory.ResService.IsIndependentResource(CmsPass, VLng("PAGE_RESID")) = False Then
            btnReset.Visible = True '�Ǽ̳�������Դ
        Else
            btnReset.Visible = False '�Ƕ���������Դ
        End If
        btnReset.ToolTip = "�ָ�Ϊ����Դ����ʾ����"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        GridDataBind() '������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = GetColShowDataSet()
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
            Dim hashColKeyValue As New Hashtable '�����Ҫ����/�޸ĵ�����Ϣ
            Dim i As Integer
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim ctl As System.Web.UI.Control
                Try
                    ctl = e.Item.Cells(i).Controls(0) '�����л��ڴ��в���Exception
                Catch ex As Exception
                    ctl = Nothing
                End Try
                If Not (ctl Is Nothing) Then
                    Dim strColName As String
                    If HashField.ContainsKey(hashColumnNames, DataGrid1.Columns(i).HeaderText) Then
                        strColName = CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText))
                        If strColName.EndsWith("22") Then
                            strColName = strColName.Substring(0, strColName.Length - 2)
                        End If
                    End If

                    If TypeOf ctl Is TextBox Then
                        Dim ctlCell As TextBox = CType(ctl, TextBox)
                        hashColKeyValue.Add(strColName, ctlCell.Text)
                    ElseIf TypeOf ctl Is DropDownList Then
                        Dim ctlCell As DropDownList = CType(ctl, DropDownList)
                        hashColKeyValue.Add(strColName, ctlCell.SelectedItem.Value)
                    ElseIf TypeOf ctl Is CheckBox Then
                        Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                        If ctlCell.Checked = True Then
                            hashColKeyValue.Add(strColName, 1)
                        Else
                            hashColKeyValue.Add(strColName, 0)
                        End If
                    End If
                End If
            Next i

            Dim strFieldName As String = e.Item.Cells(0).Text
            hashColKeyValue.Add("CS_COLNAME", strFieldName)

            '��ý�����ֶβ�������Ϊ����ʾ��
            Dim intColType As Integer = CTableStructure.GetColType(CmsPass, VLng("PAGE_RESID"), strFieldName)
            If intColType = FieldDataType.LongBinary Then
                Dim intShowEnable As Integer = CInt(hashColKeyValue("CS_SHOW_ENABLE"))
                If intShowEnable = 1 Then
                    DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
                    DataGrid1.EditItemIndex = -1
                    GridDataBind()

                    PromptMsg("��ý�����ֶβ�������Դ������ʾ��")
                    Return
                End If
            End If
            CTableStructure.EditShowSettings(CmsPass, VLng("PAGE_RESID"), hashColKeyValue)
        Catch ex As CmsException
            PromptMsg(ex.Message)
        Catch ex As Exception
            SLog.Err("�����ֶ���ʾ����ʱ�쳣ʧ�ܣ�", ex)
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
                            ElseIf TypeOf ctl Is DropDownList Then
                                Dim ctlCell As DropDownList = CType(ctl, DropDownList)
                                ctlCell.Width = Unit.Percentage(100)

                                '������ȡ��DropDownList��ѡ�У�������δ����ǰ��������еġ��༭��ʱ�����
                                ctlCell.SelectedIndex = -1

                                '---------------------------------------------------------------
                                '��ȡDropDownList��Cell��ԭ����ֵ
                                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                                Dim strAlignType As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                                Dim lngAlignType As Long = 0
                                If strAlignType = "�����" Then
                                    lngAlignType = 0
                                ElseIf strAlignType = "�ж���" Then
                                    lngAlignType = 1
                                ElseIf strAlignType = "�Ҷ���" Then
                                    lngAlignType = 2
                                End If
                                ctlCell.SelectedValue = CStr(lngAlignType) 'ΪDropDownList����ԭ����ֵ
                                '---------------------------------------------------------------
                            ElseIf TypeOf ctl Is CheckBox Then
                                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                                Dim lngOldFieldValue As Long = DbField.GetLng(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                                Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                                If lngOldFieldValue = 0 Then
                                    ctlCell.Checked = False
                                Else
                                    ctlCell.Checked = True
                                End If
                                ctlCell.Width = Unit.Percentage(100)
                            End If
                        End If
                    Next
                End If
            Else
            End If
        End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
            If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
                Dim row As DataGridItem
                Dim strColNameClicked As String = GetColName()
                For Each row In DataGrid1.Items
                    Dim strColName As String = Trim(row.Cells(0).Text) '��1�б������ֶ��ڲ�����
                    row.Attributes.Add("RECID", strColName)
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")

                    If strColName <> "" And strColNameClicked = strColName Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '�û������ĳ����¼���޸ı������¼�ı�����ɫ
                    End If
                Next
            End If
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
        Dim hashColumnNames As New Hashtable

            DataGrid1.AutoGenerateColumns = False
            DataGrid1.DataKeyField = ""

        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "�ڲ��ֶ���"
        col.DataField = "CS_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ڲ��ֶ���", "CS_COLNAME")
        col = Nothing

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "�༭"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "ȡ��"
        colEdit.EditText = "�༭"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(65)
        DataGrid1.Columns.Add(colEdit)
        colEdit = Nothing
        intWidth += 65

       

        col = New BoundColumn
        col.HeaderText = "�ֶ�����"
        col.DataField = "CD_DISPNAME"
        col.ItemStyle.Width = Unit.Pixel(180)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ֶ�����", "CD_DISPNAME")
        col = Nothing
        intWidth += 180

            Dim colTemplate As New TemplateColumn
            'colTemplate = New TemplateColumn
        colTemplate.HeaderText = "��ʾ"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CS_SHOW_ENABLE", "��ʾ")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CS_SHOW_ENABLE", "��ʾ")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("��ʾ"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CS_SHOW_ENABLE", "��ʾ")
        colTemplate.ItemStyle.Width = Unit.Pixel(60)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("��ʾ", "CS_SHOW_ENABLE")
        colTemplate = Nothing
        intWidth += 60

        col = New BoundColumn
        col.HeaderText = "��ʾ���"
        col.DataField = "CS_SHOW_WIDTH"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("��ʾ���", "CS_SHOW_WIDTH")
        col = Nothing
        intWidth += 70

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "���뷽ʽ"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CS_ALIGN22", "���뷽ʽ")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CS_ALIGN22", "���뷽ʽ")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeDDList())
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CS_ALIGN22", "���뷽ʽ")
        colTemplate.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("���뷽ʽ", "CS_ALIGN22")
        colTemplate = Nothing
        intWidth += 80

        col = New BoundColumn
        col.HeaderText = "�ֶ�ǰ׺"
        col.DataField = "CS_PREFIX"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ֶ�ǰ׺", "CS_PREFIX")
        col = Nothing
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "�ֶκ�׺"
        col.DataField = "CS_SUFFIX"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ֶκ�׺", "CS_SUFFIX")
        col = Nothing
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "��ʾ��ʽ������"
        col.DataField = "CS_FORMAT"
        col.ItemStyle.Width = Unit.Pixel(135)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("��ʾ��ʽ������", "CS_FORMAT")
        col = Nothing
            intWidth += 135




            Dim colTemplate1 As New TemplateColumn
            colTemplate1.HeaderText = ""
            colTemplate1.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "", "��ʾ")
            colTemplate1.ItemTemplate = New DataGridTemplate(ListItemType.Item, "", "", "", "<a href='#' onclick='SetColumnUrl(this);'>���õ�ַ</a>")
            colTemplate1.ItemStyle.Width = Unit.Pixel(60)
            DataGrid1.Columns.Add(colTemplate1)
            '  hashColumnNames.Add("��ʾ", "CS_SHOW_ENABLE")
            colTemplate1 = Nothing
            intWidth += 60

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

        Private Function GetColumnTypeLiteral(ByVal strText As String) As Literal
            Dim objCtrl As New Literal
            ' objCtrl.Checked = False
            objCtrl.Text = strText
            objCtrl.ID = "ltlSetUrl"

            GetColumnTypeLiteral = objCtrl
            objCtrl = Nothing
        End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        CTableStructure.MoveUp(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveUp5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        CTableStructure.MoveUp5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        CTableStructure.MoveToFirst(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        CTableStructure.MoveDown(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveDown5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        CTableStructure.MoveDown5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        CTableStructure.MoveToLast(CmsPass, VLng("PAGE_RESID"), strColName)
        GridDataBind() '������
    End Sub

    Private Sub btnColSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColSet.Click
        Response.Redirect("/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&backpage=" & VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnInputFormSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInputFormSet.Click
        Session("CMSBP_FormDesign") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsform/FormDesign.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnuformtype=" & FormType.InputForm, False)
    End Sub

    Private Sub btnRightsSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRightsSet.Click
        Session("CMSBP_RightsSet") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

    Private Sub btnShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAll.Click
        ShowAllColumns(True)
        GridDataBind() '������
    End Sub

    Private Sub btnShowNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowNone.Click
        ShowAllColumns(False)
        GridDataBind() '������
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        CTableStructure.ResetShowSettingsFromParentResource(CmsPass, VLng("PAGE_RESID"))
        GridDataBind() '������
    End Sub

    '------------------------------------------------------------------
    '���֧�ֵ��������͸�DropDownList
    '------------------------------------------------------------------
    Private Function GetColumnTypeDDList() As DropDownList
        Dim objCtrl As New DropDownList

        Dim li As New ListItem
        li.Text = "�����"
        li.Value = "0"
        objCtrl.Items.Add(li)
        li = Nothing

        li = New ListItem
        li.Text = "�ж���"
        li.Value = "1"
        objCtrl.Items.Add(li)
        li = Nothing

        li = New ListItem
        li.Text = "�Ҷ���"
        li.Value = "2"
        objCtrl.Items.Add(li)

        Return objCtrl
    End Function

    '------------------------------------------------------------------
    '��ȡ�ֶζ�����Ϣ�����ݼ�DataSet�����ָ����Դû�ж������ֶζ�����򷵻�Nothing
    '------------------------------------------------------------------
    Private Function GetColShowDataSet() As DataSet
        Dim ds As DataSet = CTableStructure.GetColumnShowsByDataset(CmsPass, VLng("PAGE_RESID"), True, True)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        ds.Tables(0).Columns.Add("CS_ALIGN22")
        For Each drv In dv
            If DbField.GetLng(drv, "CS_ALIGN") = 0 Then
                drv("CS_ALIGN22") = "�����"
            ElseIf DbField.GetLng(drv, "CS_ALIGN") = 1 Then
                drv("CS_ALIGN22") = "�ж���"
            ElseIf DbField.GetLng(drv, "CS_ALIGN") = 2 Then
                drv("CS_ALIGN22") = "�Ҷ���"
            End If
        Next

        Return ds
    End Function

    '-------------------------------------------------------------
    '��ȡ��ǰѡ�е��ֶ�����
    '-------------------------------------------------------------
    Private Function GetColName() As String
        Dim strRecID As String = RStr("RECID")
        If strRecID <> "" Then
            ViewState("PAGE_RECID") = strRecID
        Else
            If VStr("PAGE_RECID") = "" Then
                ViewState("PAGE_RECID") = RStr("columnid")
            End If
        End If
        Return VStr("PAGE_RECID")
    End Function

    '------------------------------------------------------------------
    '���õ�ǰ�����ֶ��Ƿ���ʾ
    '------------------------------------------------------------------
    Private Sub ShowAllColumns(ByVal blnShow As Boolean)
        Dim intShow As Integer = CInt(IIf(blnShow = True, 1, 0))
        Dim lngResID As Long = VLng("PAGE_RESID")
        Dim ds As DataSet = CTableStructure.GetColumnShowsByDataset(CmsPass, lngResID, True, True)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            Dim hashColKeyValue As New Hashtable '�����Ҫ����/�޸ĵ�����Ϣ
            hashColKeyValue.Add("CS_COLNAME", DbField.GetStr(drv, "CS_COLNAME"))
            hashColKeyValue.Add("CS_SHOW_ENABLE", intShow)
            CTableStructure.EditShowSettings(CmsPass, lngResID, hashColKeyValue)
        Next
    End Sub
End Class
End Namespace
