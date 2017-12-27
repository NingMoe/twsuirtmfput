Option Strict On
Option Explicit On 

Imports NetReusables
Imports System.Data.OleDb
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceColumnSet
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtColDispName As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtColSize As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnAddOneColumn As System.Web.UI.WebControls.Button
    Protected WithEvents ddlColType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtColWidth As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtColComments As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkColShow As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkColIndex As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkColReadonly As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents btnFormulaList As System.Web.UI.WebControls.Button

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private m_blnAddDelAttribute As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        GetColName()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        btnDelCol.Attributes.Add("onClick", "return CmsPrmoptConfirm('ɾ���ֶκ���ֶ��е��������ݽ���ɾ����ȷ��Ҫɾ���ֶ���');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        GridDataBind() '������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = ColdefGetColumnsForColMgr()
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("���ֶ���Ϣ����ʾʱ����", ex)
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

    Private Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        GridDataBind()
    End Sub

    Private Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            Dim strColName As String = e.Item.Cells(2).Text '�ֶ��ڲ������ǵ�3��
            CTableStructure.DeleteColumn(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Try
            Dim hashColumnNames As Hashtable = CType(ViewState("COLMGR_COLUMN_NAMES"), Hashtable)
            Dim hashColKeyValue As New Hashtable '�����Ҫ����/�޸ĵ�����Ϣ
            Dim strColNameField As String = ""
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

            '----------------------------------------------------------------
            '��������Ϣ�����ݿ�
            Dim strCD_ID As String = e.Item.Cells(0).Text '��Cell����ʾ����Ϊ�ж����������޸ļ�¼
            If strCD_ID = "0" Then '������¼
                '����Ĭ��ֵ����Ϊ�������� 0
                If HashField.ContainsKey(hashColKeyValue, "CD_VALTYPE") Then
                    hashColKeyValue("CD_VALTYPE") = FieldValueType.Input
                Else
                    hashColKeyValue.Add("CD_VALTYPE", FieldValueType.Input)
                End If

                Dim blnLimit255 As Boolean = CmsConfig.GetBool("SYS_CONFIG", "TEXTCOLUMN_LIMIT_SIZE255")
                CTableStructure.AddColumn(CmsPass, VLng("PAGE_RESID"), hashColKeyValue, blnLimit255)
            Else '�޸ļ�¼
                If HashField.ContainsKey(hashColKeyValue, "CD_VALTYPE") Then
                    hashColKeyValue.Remove("CD_VALTYPE") '�߼����ã�ֵ���ͣ������޸�
                End If

                Dim strColName As String
                If CmsConfig.ShowColumnName = True Then
                    '�ֶ��ڲ���������ʾ�����Ѿ���Hashtable��
                    strColName = CStr(hashColKeyValue("CD_COLNAME"))
                Else
                    'ֻ�����ֶ��ڲ����Ʋ���ʾ������£��ſ���������ã������ÿ�ֵ��
                    strColName = e.Item.Cells(2).Text '�ֶ��ڲ����Ʊ����ǵ�3��
                End If
                CTableStructure.EditColumn(CmsPass, VLng("PAGE_RESID"), strColName, hashColKeyValue, CmsConfig.GetBool("SYS_CONFIG", "TEXTCOLUMN_LIMIT_SIZE255"))
            End If
            '----------------------------------------------------------------
        Catch ex As Exception
            SLog.Err("�����ֶ������쳣ʧ��", ex)
            PromptMsg(ex.Message)
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
                            Dim strOldFieldValue As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))

                            'ΪDropDownList����ԭ����ֵ
                            Dim item As ListItem = ctlCell.Items.FindByText(strOldFieldValue)
                            If Not item Is Nothing Then item.Selected = True
                            '---------------------------------------------------------------
                        ElseIf TypeOf ctl Is CheckBox Then
                            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                            Dim strOldFieldValue As String = DbField.GetStr(drv, CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText)))
                            Dim ctlCell As CheckBox = CType(ctl, CheckBox)
                            If strOldFieldValue = "��" Then
                                ctlCell.Checked = True
                            ElseIf strOldFieldValue = "��" Then
                                ctlCell.Checked = False
                            End If
                            ctlCell.Width = Unit.Percentage(100)
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnAdvSetting2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvSetting2.Click
        btnAdvSetting_Click(sender, e)
    End Sub

    Private Sub btnAddCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCol.Click
        Dim ds As DataSet = ColdefGetColumnsForColMgr()
        Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew()
        drv.BeginEdit()
        drv("CD_ID") = 0 '�����ǣ��Ա��ڸ���ʱ֪����������1����¼���������޸ļ�¼������Ϣ�����¼�����ݿ���
        drv("CD_COLNAME") = ""
        drv("CD_DISPNAME") = ""
        drv("CD_TYPE") = 0
        drv("CD_TYPE22") = ""
        drv("CD_SIZE") = 8
        drv.EndEdit()

        DataGrid1.EditItemIndex = ds.Tables(0).DefaultView.Count - 1
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub

    Private Sub btnAddCol2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCol2.Click
        btnAddCol_Click(sender, e)
    End Sub

    Private Sub btnAdvSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdvSetting.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        '��ȡ�ֶε�ֵ���ͣ�����Ƿ������ͣ���ֱ��������Ӧ��ֵ���͹������
        Dim lngColValType As Long = CTableStructure.GetColValType(CmsPass, VLng("PAGE_RESID"), strColName)
        Select Case lngColValType
            Case FieldValueType.Input
                Session("CMSBP_FieldAdvanceSetting") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvanceSetting.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.OptionValue
                Session("CMSBP_FieldAdvOption") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvOption.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.RadioGroup
                Session("CMSBP_FieldAdvRadio") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvRadio.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.Checkbox
                Session("CMSBP_FieldAdvCheckbox") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvCheckbox.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.AutoCoding
                Session("CMSBP_FieldAdvAutoCoding") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvAutoCoding.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.Calculation
                Session("CMSBP_FieldAdvCalculation") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName & "&urlfmltype=" & FormulaType.IsCalculation, False)

            Case FieldValueType.AdvDictionary
                Session("CMSBP_FieldAdvDictionary") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvDictionary.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.Constant
                Session("CMSBP_FieldAdvConstant") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvConstant.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.DefaultValue
                Session("CMSBP_FieldAdvDefaultValue") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvDefaultValue.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.CustomizeCoding
                Session("CMSBP_FieldAdvCustomizeCoding") = "/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&columnid=" & strColName
                Response.Redirect("/cmsweb/adminres/FieldAdvCustomizeCoding.aspx?mnuresid=" & VLng("PAGE_RESID") & "&colname=" & strColName, False)

            Case FieldValueType.IncrementalCoding
                PromptMsg("������������û�����ý��棡")

            Case FieldValueType.DirectoryFile
                PromptMsg("Ŀ¼�ļ�����û�����ý��棡")

            Case Else
                PromptMsg("����ʶ����ֶ����ͣ����ݿ����ݲ�һ�£�")
        End Select
    End Sub

    Private Sub btnCopyColDef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyColDef.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            Dim hashColKeyValue As New Hashtable
            CTableStructure.CopyColumn(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try

        GridDataBind() '������
    End Sub

    Private Sub btnCopyColDef2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyColDef2.Click
        btnCopyColDef_Click(sender, e)
    End Sub

    Private Sub btnDelCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelCol.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.DeleteColumn(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind() '������
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnExit2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit2.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
        Dim hashColumnNames As New Hashtable

        DataGrid1.AutoGenerateColumns = False
        Dim intWidth As Integer = 0

        '��1�б����Ǽ�¼ID
        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "�ֶ�ID"
        col.DataField = "CD_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ֶ�ID", "CD_ID")
        col = Nothing

        Dim colEdit As EditCommandColumn = New EditCommandColumn
        colEdit.HeaderText = "�༭"
        colEdit.EditText = "�༭"
        colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
        colEdit.CancelText = "ȡ��"
        colEdit.ButtonType = ButtonColumnType.LinkButton
        colEdit.ItemStyle.Width = Unit.Pixel(65)
        DataGrid1.Columns.Add(colEdit)
        intWidth += 65
        colEdit = Nothing

        '��2�б������ֶ��ڲ�����
        col = New BoundColumn
        col.HeaderText = "�ڲ��ֶ���"
        col.DataField = "CD_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(115)
        If CmsConfig.ShowColumnName = True Then
            col.Visible = True
            col.ReadOnly = False
            intWidth += 115
        Else
            col.Visible = False
            col.ReadOnly = True
        End If
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ڲ��ֶ���", "CD_COLNAME")
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "�ֶ�����"
        col.DataField = "CD_DISPNAME"
        col.ItemStyle.Width = Unit.Pixel(180)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ֶ�����", "CD_DISPNAME")
        intWidth += 180
        col = Nothing

        Dim colTemplate As New TemplateColumn
        colTemplate.HeaderText = "�ֶ�����"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_TYPE22", "�ֶ�����")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_TYPE22", "�ֶ�����")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeDDList())
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_TYPE22", "�ֶ�����")
        colTemplate.ItemStyle.Width = Unit.Pixel(100)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("�ֶ�����", "CD_TYPE22")
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "����"
        col.DataField = "CD_SIZE"
        col.ItemStyle.Width = Unit.Pixel(40)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("����", "CD_SIZE")
        intWidth += 40
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "�߼�����"
        col.DataField = "CD_VALTYPE22"
        col.ItemStyle.Width = Unit.Pixel(90)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�߼�����", "CD_VALTYPE22")
        intWidth += 90
        col = Nothing

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "Ψһֵ"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNIQUE22", "Ψһֵ")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNIQUE22", "Ψһֵ")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("Ψһֵ"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNIQUE22", "Ψһֵ")
        colTemplate.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("Ψһֵ", "CD_IS_UNIQUE22")
        intWidth += 70

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "����Ψһ"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNITE_UNIQUE22", "����Ψһ")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNITE_UNIQUE22", "����Ψһ")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("����Ψһ"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNITE_UNIQUE22", "����Ψһ")
        colTemplate.ItemStyle.Width = Unit.Pixel(80)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("����Ψһ", "CD_IS_UNITE_UNIQUE22")
        intWidth += 80

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "��ѯ�ֶ�"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_SEARCH22", "��ѯ�ֶ�")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_SEARCH22", "��ѯ�ֶ�")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("��ѯ�ֶ�"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_SEARCH22", "��ѯ�ֶ�")
        colTemplate.ItemStyle.Width = Unit.Pixel(85)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("��ѯ�ֶ�", "CD_IS_SEARCH22")
        intWidth += 85

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "ֻ��"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_READONLY22", "ֻ��")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_READONLY22", "ֻ��")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("ֻ��"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_READONLY22", "ֻ��")
        colTemplate.ItemStyle.Width = Unit.Pixel(55)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("ֻ��", "CD_IS_READONLY22")
        intWidth += 55

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "������"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_NONULL22", "������")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_NONULL22", "������")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("������"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_NONULL22", "������")
        colTemplate.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("������", "CD_IS_NONULL22")
        intWidth += 70

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "�����޸�"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNEDITABLE22", "�����޸�")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNEDITABLE22", "�����޸�")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("�����޸�"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNEDITABLE22", "�����޸�")
        colTemplate.ItemStyle.Width = Unit.Pixel(85)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("�����޸�", "CD_IS_UNEDITABLE22")
        intWidth += 85

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "��ֵ�󲻿��޸�"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_IS_UNMODIFIED22", "��ֵ�󲻿��޸�")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_IS_UNMODIFIED22", "��ֵ�󲻿��޸�")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("��ֵ�󲻿��޸�"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_IS_UNMODIFIED22", "��ֵ�󲻿��޸�")
        colTemplate.ItemStyle.Width = Unit.Pixel(130)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("��ֵ�󲻿��޸�", "CD_IS_UNMODIFIED22")
        intWidth += 130

        colTemplate = New TemplateColumn
        colTemplate.HeaderText = "������"
        colTemplate.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "CD_INDEX_NAME22", "������")
        colTemplate.ItemTemplate = New DataGridTemplate(ListItemType.Item, "CD_INDEX_NAME22", "������")
        colTemplate.EditItemTemplate = New DataGridTemplate(ListItemType.EditItem, GetColumnTypeCheckbox("������"))
        colTemplate.FooterTemplate = New DataGridTemplate(ListItemType.Footer, "CD_INDEX_NAME22", "������")
        colTemplate.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(colTemplate)
        hashColumnNames.Add("������", "CD_INDEX_NAME22")
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "С������"
        col.DataField = "CD_FLOAT_PRECISION"
        col.ItemStyle.Width = Unit.Pixel(70)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("С������", "CD_FLOAT_PRECISION")
        intWidth += 70
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "��¼����ֵ"
        col.DataField = "CD_LOCKVALUE"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("��¼����ֵ", "CD_LOCKVALUE")
        intWidth += 150
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "����1"
        col.DataField = "CD_INT1"
        col.ItemStyle.Width = Unit.Pixel(70)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("����1", "CD_INT1")
        intWidth += 70
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "����2"
        col.DataField = "CD_INT2"
        col.ItemStyle.Width = Unit.Pixel(70)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("����2", "CD_INT2")
        intWidth += 70
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "����3"
        col.DataField = "CD_STR3"
        col.ItemStyle.Width = Unit.Pixel(250)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("����3", "CD_STR3")
        intWidth += 250
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "����4"
        col.DataField = "CD_STR4"
        col.ItemStyle.Width = Unit.Pixel(250)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("����4", "CD_STR4")
        intWidth += 250
        col = Nothing

        col = New BoundColumn
        col.HeaderText = "�ֶ�ע��"
        col.DataField = "CD_NOTES"
        col.ItemStyle.Width = Unit.Pixel(400)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("�ֶ�ע��", "CD_NOTES")
        intWidth += 400
        col = Nothing

        'Dim colDel As ButtonColumn = New ButtonColumn
        'colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        'colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        'colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '������ͼ�꣬�磺"<img src='/cmsweb/images/common/delete.gif' border=0>"
        'colDel.CommandName = "Delete"
        'colDel.ButtonType = ButtonColumnType.LinkButton
        'colDel.ItemStyle.Width = Unit.Pixel(40)
        'colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        'DataGrid1.Columns.Add(colDel)
        'intWidth += 40

        DataGrid1.Width = Unit.Pixel(intWidth)

        '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
        ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
    End Sub

    '------------------------------------------------------------------
    '���֧�ֵ��������͸�DropDownList
    '------------------------------------------------------------------
    Private Function GetColumnTypeDDList() As DropDownList
        Dim objCtrl As New DropDownList
        Dim alistColTypes As ArrayList = CTableStructure.GetColumnTypes()

        Dim fldTypePair As FieldTypePair
        For Each fldTypePair In alistColTypes
            Dim li As ListItem = New ListItem(fldTypePair.strFieldTypeDispName, CStr(fldTypePair.lngFieldType))
            objCtrl.Items.Add(li)
            li = Nothing
        Next

        alistColTypes.Clear()
        alistColTypes = Nothing

        Return objCtrl
    End Function

    '------------------------------------------------------------------
    '���֧�ֵ��������͸�DropDownList
    '------------------------------------------------------------------
    Private Function GetDefaultValueDDList() As DropDownList
        Dim objCtrl As New DropDownList
        Dim alistDefVal As ArrayList = CTableStructure.GetDefaultValNames()

        Dim en As IEnumerator = alistDefVal.GetEnumerator()
        Do While en.MoveNext
            objCtrl.Items.Add(CType(en.Current, String))
        Loop
        en = Nothing
        alistDefVal.Clear()
        alistDefVal = Nothing

        Return objCtrl
    End Function

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

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ����
        '2���ִ�������ʹ�ã�1��Javascript����RowLeftClickNoPost()��2�����һ��hidden������<input type="hidden" name="RECID">
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim i As Integer
            Dim strColNameClicked As String = GetColName()
            For i = 0 To DataGrid1.Items.Count - 1
                Dim row As DataGridItem = DataGrid1.Items(i)
                'Ϊ�ڶ��е�ɾ����ť���ȷ�ϲ���
                Dim strColName As String = Trim(row.Cells(2).Text) '�ֶ��ڲ����Ʊ����ǵ�3��
                row.Attributes.Add("RECID", strColName)
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")

                If strColName <> "" And strColNameClicked = strColName Then
                    row.Attributes.Add("bgColor", "#C4D9F9") '�û������ĳ����¼���޸ı������¼�ı�����ɫ
                End If
            Next
        End If
    End Sub

    Private Sub btnDelAdvSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelAdvSetting.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            '��ȡԭ�ȵĸ߼��������ͣ��Ա�ɾ�����и߼�����
            Dim lngAdvType As Long = CTableColInput.GetInputType(CmsPass, VLng("PAGE_RESID"), strColName)
            Select Case lngAdvType
                Case FieldValueType.AutoCoding
                    CTableColACoding.DeleteAll(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.Calculation
                    CTableColCalculation.DelFormula(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.Constant
                    CTableColConstant.DelConstant(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.CustomizeCoding
                    CTableColCustomizeCoding.DelCustCoding(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.IncrementalCoding
                    CTableColIncrementalCoding.DelCoding(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.DirectoryFile
                    CTableColDirectoryFile.Delete(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.DefaultValue
                    CTableColDefaultValue.DelDefaultVal(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.AdvDictionary
                    CTableColAdvDictionary.DelDictionary(CmsPass, VLng("PAGE_RESID"), strColName)

                Case FieldValueType.Input
                    CTableStructure.SetColumnDefinition(CmsPass, VLng("PAGE_RESID"), strColName, "", FieldValueType.Input)

                Case FieldValueType.OptionValue
                    CTableColOption.Delete(CmsPass, VLng("PAGE_RESID"), strColName)

                Case Else
                    CTableStructure.SetColumnDefinition(CmsPass, VLng("PAGE_RESID"), strColName, "", FieldValueType.Input)

            End Select
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        GridDataBind() '������
    End Sub

    Private Sub btnDelAdvSetting2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelAdvSetting2.Click
        btnDelAdvSetting_Click(sender, e)
    End Sub

    '------------------------------------------------------------------
    '��ȡ�ֶζ�����Ϣ�����ݼ�DataSet�����ָ����Դû�ж������ֶζ�����򷵻�Nothing
    '------------------------------------------------------------------
    Public Function ColdefGetColumnsForColMgr() As DataSet
        Dim ds As DataSet = CTableStructure.GetColumnsByDataset(CmsPass, VLng("PAGE_RESID"), True, True)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        ds.Tables(0).Columns.Add("CD_IS_UNIQUE22")
        ds.Tables(0).Columns.Add("CD_IS_UNITE_UNIQUE22")

        ds.Tables(0).Columns.Add("CD_IS_SEARCH22")

        ds.Tables(0).Columns.Add("CD_IS_READONLY22")
        ds.Tables(0).Columns.Add("CD_IS_NONULL22")
        ds.Tables(0).Columns.Add("CD_IS_UNEDITABLE22")
        ds.Tables(0).Columns.Add("CD_IS_UNMODIFIED22")
        ds.Tables(0).Columns.Add("CD_INDEX_NAME22")
        ds.Tables(0).Columns.Add("CD_VALTYPE22")
        ds.Tables(0).Columns.Add("CD_TYPE22")
        For Each drv In dv
            If DbField.GetLng(drv, "CD_IS_UNIQUE") = 1 Then
                drv("CD_IS_UNIQUE22") = "��"
            Else
                drv("CD_IS_UNIQUE22") = "��"
            End If

            If DbField.GetLng(drv, "CD_IS_UNITE_UNIQUE") = 1 Then
                drv("CD_IS_UNITE_UNIQUE22") = "��"
            Else
                drv("CD_IS_UNITE_UNIQUE22") = "��"
            End If

            If DbField.GetLng(drv, "CD_IS_SEARCH") = 1 Then
                drv("CD_IS_SEARCH22") = "��"
            Else
                drv("CD_IS_SEARCH22") = "��"
            End If

            If DbField.GetLng(drv, "CD_IS_READONLY") = 1 Then
                drv("CD_IS_READONLY22") = "��"
            Else
                drv("CD_IS_READONLY22") = "��"
            End If

            If DbField.GetLng(drv, "CD_IS_NONULL") = 1 Then
                drv("CD_IS_NONULL22") = "��"
            Else
                drv("CD_IS_NONULL22") = "��"
            End If

            If DbField.GetLng(drv, "CD_IS_UNEDITABLE") = 1 Then
                drv("CD_IS_UNEDITABLE22") = "��"
            Else
                drv("CD_IS_UNEDITABLE22") = "��"
            End If

            If DbField.GetLng(drv, "CD_IS_UNMODIFIED") = 1 Then
                drv("CD_IS_UNMODIFIED22") = "��"
            Else
                drv("CD_IS_UNMODIFIED22") = "��"
            End If

            If DbField.GetStr(drv, "CD_INDEX_NAME") <> "" Then
                drv("CD_INDEX_NAME22") = "��"
            Else
                drv("CD_INDEX_NAME22") = "��"
            End If

            drv("CD_VALTYPE22") = CTableStructure.GetValTypeDispName(DbField.GetLng(drv, "CD_VALTYPE"))

            drv("CD_TYPE22") = CTableStructure.ConvColTypeToDispName(DbField.GetLng(drv, "CD_TYPE"))
        Next

        Return ds
    End Function

    Private Sub btnColShowSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColShowSet.Click
        Response.Redirect("/cmsweb/adminres/ResourceColumnShowSet.aspx?mnuresid=" & VLng("PAGE_RESID") & "&backpage=" & VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnInputFormSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInputFormSet.Click
        Session("CMSBP_FormDesign") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsform/FormDesign.aspx?mnuresid=" & VLng("PAGE_RESID") & "&mnuformtype=" & FormType.InputForm, False)
    End Sub

    Private Sub btnRightsSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRightsSet.Click
        Session("CMSBP_RightsSet") = VStr("PAGE_BACKPAGE")
        Response.Redirect("/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

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

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.MoveUp(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveUp5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.MoveUp5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.MoveToFirst(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '������
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.MoveDown(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveDown5Step_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown5Step.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.MoveDown5Step(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�������ֶ�")
            Return
        End If

        Try
            CTableStructure.MoveToLast(CmsPass, VLng("PAGE_RESID"), strColName)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
        GridDataBind() '������
    End Sub
End Class
End Namespace
