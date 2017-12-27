Option Strict On
Option Explicit On 

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculationList
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

        GetSelectedFormulaResID()
        GetColName()
        GetSelectedFormulaType()
        GetSelectedFormulaAiid()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResDispName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        'ɾ����ʽǰ����
        btnDelFormula.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ�����㹫ʽ��');")

        btnAddVerifyFormula.Visible = CmsFunc.IsEnable("FUNC_FORMULA_VERIFY")
        btnFmlOption.Visible = CmsFunc.IsEnable("FUNC_FORMULA_OPTIONS")
        btnSchedule.Visible = CmsFunc.IsEnable("FUNC_FORMULA_SCHEDULE")

        If VLng("PAGE_RESID") = 0 Then
            '��ʾ��ϵͳ���м��㹫ʽʱ���¹��ܲ�����
            btnAddVerifyFormula.Enabled = False
        End If

        If RStr("isrightres") = "1" Then
            '��ʾ�����㹫ʽʱ���¹��ܲ�����
            lbtnMoveup.Enabled = False
            lbtnMoveToFirst.Enabled = False
            lbtnMovedown.Enabled = False
            lbtnMoveToLast.Enabled = False
            btnAddVerifyFormula.Enabled = False
        End If
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
            Dim ds As DataSet = GetFormulaDataset()
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
                    Dim strColName As String = ""
                    If HashField.ContainsKey(hashColumnNames, DataGrid1.Columns(i).HeaderText) Then
                        strColName = CStr(hashColumnNames(DataGrid1.Columns(i).HeaderText))
                        strColName = StringDeal.Trim(strColName, "", "22")
                    End If
                    If strColName <> "" Then
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
                End If
            Next i

            '��ʼ�޸ļ��㹫ʽ
            Dim lngAiid As Long = CLng(e.Item.Cells(0).Text) '��Cell����ʾ����Ϊ�ж����������޸ļ�¼
            CTableColCalculation.EditFormula(CmsPass, lngAiid, hashColKeyValue)
        Catch ex As Exception
            SLog.Err("�����ֶ������쳣ʧ��", ex)
            PromptMsg(ex.Message)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        GridDataBind()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                Dim strColName As String = row.Cells(2).Text.Trim() '��2�б������ֶ��ڲ�����
                row.Attributes.Add("fmlaiid", row.Cells(0).Text.Trim())
                row.Attributes.Add("fmltype", row.Cells(1).Text.Trim())
                row.Attributes.Add("fmlcolname", strColName)
                row.Attributes.Add("fmlresid", row.Cells(3).Text.Trim())
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")

                Dim strColNameClicked As String = GetColName()
                If strColNameClicked <> "" And strColNameClicked = strColName Then
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

        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "CDJ_AIID"
        col.DataField = "CDJ_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        col = New BoundColumn
        col.HeaderText = "CDJ_TYPE"
        col.DataField = "CDJ_TYPE"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        col = New BoundColumn
        col.HeaderText = "CDJ_COLNAME"
        col.DataField = "CDJ_COLNAME"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

        col = New BoundColumn
        col.HeaderText = "��ԴID"
        col.DataField = "CDJ_RESID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 1

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

        col = New BoundColumn
        col.HeaderText = "��Դ����"
        col.DataField = "CDJ_RESID22"
        col.ItemStyle.Width = Unit.Pixel(110)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 110

        col = New BoundColumn
        col.HeaderText = "�ֶ�����"
        col.DataField = "CDJ_COLNAME22"
        col.ItemStyle.Width = Unit.Pixel(140)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 140

        col = New BoundColumn
        col.HeaderText = "��ʽ����"
        col.DataField = "CDJ_TYPE22"
        col.ItemStyle.Width = Unit.Pixel(70)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 70

        col = New BoundColumn
        col.HeaderText = "���㹫ʽ�ⲿ���ʽ"
        col.DataField = "CDJ_FORMULA_DESC"
        col.ItemStyle.Width = Unit.Pixel(800)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("���㹫ʽ�ⲿ���ʽ", "CDJ_FORMULA_DESC")
        intWidth += 800

        'If CmsConfig.DebugingMode() Then
        'End If
        '�ڲ���ʽ����Debugģʽ����ʾ
        col = New BoundColumn
        col.HeaderText = "���㹫ʽ�ڲ����ʽ"
        col.DataField = "CDJ_FMLRIGHT_EXPR"
        col.ItemStyle.Width = Unit.Pixel(1500)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("���㹫ʽ�ڲ����ʽ", "CDJ_FMLRIGHT_EXPR")
        intWidth += 1500

        col = New BoundColumn
        col.HeaderText = "У����ʾ��Ϣ"
        col.DataField = "CDJ_VERIFY_TIP"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        hashColumnNames.Add("У����ʾ��Ϣ", "CDJ_VERIFY_TIP")
        intWidth += 250

        'col = New BoundColumn
        'col.HeaderText = "����ִ��ʱ��"
        'col.DataField = "CDJ_CALTIME22"
        'col.ItemStyle.Width = Unit.Pixel(100)
        'col.ReadOnly = True
        'DataGrid1.Columns.Add(col)
        'intWidth += 100

        col = New BoundColumn
        col.HeaderText = "����������"
        col.DataField = "CDJ_NO_ARITHMETIC22"
        col.ItemStyle.Width = Unit.Pixel(90)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 90

        col = New BoundColumn
        col.HeaderText = "��Ӽ�¼ʱ����"
        col.DataField = "CDJ_CALOCCASION_ADD"
        col.ItemStyle.Width = Unit.Pixel(100)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "�޸ļ�¼ʱ����"
        col.DataField = "CDJ_CALOCCASION_EDIT"
        col.ItemStyle.Width = Unit.Pixel(100)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 100

        col = New BoundColumn
        col.HeaderText = "ɾ����¼ʱ����"
        col.DataField = "CDJ_CALOCCASION_DEL"
        col.ItemStyle.Width = Unit.Pixel(100)
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)
        intWidth += 100

        DataGrid1.Width = Unit.Pixel(intWidth)

        '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
        ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If

        'CTableColCalculation.MoveUp(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveUp(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveToFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToFirst.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If

        'CTableColCalculation.MoveToFirst(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveToFirst(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If

        'CTableColCalculation.MoveDown(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveDown(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '������
    End Sub

    Private Sub lbtnMoveToLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveToLast.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If

        'CTableColCalculation.MoveToLast(CmsPass, VLng("PAGE_RESID"), strColName)
        CTableColCalculation.MoveToLast(CmsPass, GetSelectedFormulaResID(), strColName)
        GridDataBind() '������
    End Sub

    '------------------------------------------------------------------
    '��ȡ�ֶζ�����Ϣ�����ݼ�DataSet�����ָ����Դû�ж������ֶζ�����򷵻�Nothing
    '------------------------------------------------------------------
    Private Function GetFormulaDataset() As DataSet
        Dim ds As DataSet = Nothing
        If RStr("isrightres") = "1" Then
            ds = CTableColCalculation.GetAllFormulaDatasetByFmlRightResID(CmsPass, VLng("PAGE_RESID"))
        Else
            ds = CTableColCalculation.GetAllFormulaDatasetByFmlLeftResID(CmsPass, VLng("PAGE_RESID"))
        End If

        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        ds.Tables(0).Columns.Add("CDJ_RESID22")
        ds.Tables(0).Columns.Add("CDJ_COLNAME22")
        ds.Tables(0).Columns.Add("CDJ_TYPE22")
        'ds.Tables(0).Columns.Add("CDJ_FORMULA_DESC22")
        'ds.Tables(0).Columns.Add("CDJ_CALTIME22")
        ds.Tables(0).Columns.Add("CDJ_NO_ARITHMETIC22")
        ds.Tables(0).Columns.Add("CDJ_CALOCCASION_ADD")
        ds.Tables(0).Columns.Add("CDJ_CALOCCASION_EDIT")
        ds.Tables(0).Columns.Add("CDJ_CALOCCASION_DEL")
        For Each drv In dv
            Dim lngFmlResID As Long = DbField.GetLng(drv, "CDJ_RESID")
            drv("CDJ_RESID22") = CmsPass.GetDataRes(lngFmlResID).ResName

            Dim intType As Integer = DbField.GetInt(drv, "CDJ_TYPE")
            If intType = FormulaType.IsCalculation Then
                drv("CDJ_COLNAME22") = CTableStructure.GetColDispName(CmsPass, lngFmlResID, DbField.GetStr(drv, "CDJ_COLNAME"))
                If DbField.GetStr(drv, "CDJ_SCHEDULE") <> "" Then '�Ƕ�ʱ����
                    drv("CDJ_TYPE22") = "��ʱ����"
                Else '�Ǽ��㹫ʽ
                    Dim strTemp As String = DbField.GetStr(drv, "CDJ_FORMULA_DESC")
                    If strTemp.IndexOf("::") > 0 Then
                        drv("CDJ_TYPE22") = "������" '��ʽ�����к���::�ļ��Ǳ����㣬��Ϊ������Դ���ֶ����Ƶķָ���
                    Else
                        drv("CDJ_TYPE22") = "���ڼ���"
                    End If
                End If

                'Dim strDesc As String = DbField.GetStr(drv, "CDJ_FORMULA_DESC")
                'drv("CDJ_FORMULA_DESC22") = strDesc.Substring(strDesc.IndexOf("=") + 1)
            ElseIf intType = FormulaType.IsVerify Then
                drv("CDJ_COLNAME22") = DbField.GetStr(drv, "CDJ_COLNAME")
                drv("CDJ_TYPE22") = "У�鹫ʽ"
                'ElseIf intType = FormulaType.IsSchedule Then
                '    drv("CDJ_COLNAME22") = CTableStructure.GetColDispName(CmsPass, lngFmlResID, DbField.GetStr(drv, "CDJ_COLNAME"))
                '    drv("CDJ_TYPE22") = "��ʱ����"

                'drv("CDJ_FORMULA_DESC22") = DbField.GetStr(drv, "CDJ_FORMULA_DESC")
            End If

            'If DbField.GetInt(drv, "CDJ_CALTIME") = 1 Then
            '    drv("CDJ_CALTIME22") = "��������"
            'Else
            '    drv("CDJ_CALTIME22") = "����ǰ����"
            'End If

            If DbField.GetInt(drv, "CDJ_NO_ARITHMETIC") = 0 Then
                drv("CDJ_NO_ARITHMETIC22") = "��"
            Else
                drv("CDJ_NO_ARITHMETIC22") = "��"
            End If

            Dim intTemp As Integer = DbField.GetInt(drv, "CDJ_CALOCCASION")
            If (intTemp And FormulaOccasion.RecordAdd) = FormulaOccasion.RecordAdd Then
                drv("CDJ_CALOCCASION_ADD") = "��"
            Else
                drv("CDJ_CALOCCASION_ADD") = "��"
            End If
            If (intTemp And FormulaOccasion.RecordEdit) = FormulaOccasion.RecordEdit Then
                drv("CDJ_CALOCCASION_EDIT") = "��"
            Else
                drv("CDJ_CALOCCASION_EDIT") = "��"
            End If
            If (intTemp And FormulaOccasion.RecordDel) = FormulaOccasion.RecordDel Then
                drv("CDJ_CALOCCASION_DEL") = "��"
            Else
                drv("CDJ_CALOCCASION_DEL") = "��"
            End If
        Next
        Return ds
    End Function

    Private Sub btnSetFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFormula.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If

        Dim lngResID As Long = GetSelectedFormulaResID()
        Session("CMSBP_FieldAdvCalculation") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlfmlcolname=" & strColName & "&urlfmlresid=" & GetSelectedFormulaResID() & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & lngResID & "&colname=" & strColName & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid(), False)
    End Sub

    Private Sub btnSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedule.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If
        If GetSelectedFormulaType() = FormulaType.IsVerify Then
            PromptMsg("У�鹫ʽ��֧�ֶ�ʱ���㣡")
            Return
        End If

        Dim lngResID As Long = GetSelectedFormulaResID()
        Session("CMSBP_FieldAdvCalculationSchedule") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlfmlcolname=" & strColName & "&urlfmlresid=" & GetSelectedFormulaResID() & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculationSchedule.aspx?mnuresid=" & lngResID & "&colname=" & strColName & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid(), False)
    End Sub

    Private Sub btnDelFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFormula.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If
        Dim lngResID As Long = GetSelectedFormulaResID()

        CTableColCalculation.DelFormula(CmsPass, lngResID, strColName)
        GridDataBind() '������
    End Sub

    Private Sub btnFmlOption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFmlOption.Click
        Dim strColName As String = GetColName()
        If strColName = "" Then
            PromptMsg("����ѡ����Ҫ�����ļ��㹫ʽ")
            Return
        End If

        Dim lngResID As Long = GetSelectedFormulaResID()
        Session("CMSBP_FieldAdvCalculationSettings") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlfmlcolname=" & strColName & "&urlfmlresid=" & GetSelectedFormulaResID() & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculationSettings.aspx?mnuresid=" & lngResID & "&colname=" & strColName & "&urlfmltype=" & GetSelectedFormulaType() & "&urlfmlaiid=" & GetSelectedFormulaAiid(), False)
    End Sub

    Private Sub btnAddVerifyFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddVerifyFormula.Click
        Session("CMSBP_FieldAdvCalculation") = "/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=" & RStr("isrightres") & "&mnuresid=" & VStr("PAGE_RESID") & "&urlcolname=" & GetColName() & "&urlresid=" & GetSelectedFormulaResID()
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculation.aspx?mnuresid=" & VStr("PAGE_RESID") & "&urlfmltype=" & FormulaType.IsVerify, False)
    End Sub

    '-------------------------------------------------------------
    '��ȡ��ǰѡ�е��ֶ�����
    '-------------------------------------------------------------
    Private Function GetSelectedFormulaResID() As Long
        Dim lngResID As Long = RLng("fmlresid")
        If lngResID <> 0 Then
            ViewState("PAGE_FMLRESID") = lngResID
        Else
            If VLng("PAGE_FMLRESID") = 0 Then
                ViewState("PAGE_FMLRESID") = RLng("urlfmlresid")
            End If
        End If

        Return VLng("PAGE_FMLRESID")
    End Function

    '-------------------------------------------------------------
    '��ȡ��ǰѡ�е��ֶ�����
    '-------------------------------------------------------------
    Private Function GetColName() As String
        Dim strRecID As String = RStr("fmlcolname")
        If strRecID <> "" Then
            ViewState("PAGE_FMLCOLNAME") = strRecID
        Else
            If VStr("PAGE_FMLCOLNAME") = "" Then
                ViewState("PAGE_FMLCOLNAME") = RStr("urlfmlcolname")
            End If
        End If
        Return VStr("PAGE_FMLCOLNAME")
    End Function

    '-------------------------------------------------------------
    '��ȡ��ǰѡ�еĹ�ʽ����
    '-------------------------------------------------------------
    Private Function GetSelectedFormulaType() As Long
        Dim strType As String = RStr("fmltype")
        If IsNumeric(strType) Then
            ViewState("PAGE_FMLTYPE") = strType
        Else
            If VStr("PAGE_FMLTYPE") = "" Then
                ViewState("PAGE_FMLTYPE") = RLng("urlfmltype")
            End If
        End If

        Return VLng("PAGE_FMLTYPE")
    End Function

    '-------------------------------------------------------------
    '��ȡ��ǰѡ�еĹ�ʽ��AIID
    '-------------------------------------------------------------
    Private Function GetSelectedFormulaAiid() As Long
        Dim lngAiid As Long = RLng("fmlaiid")
        If lngAiid <> 0 Then
            ViewState("PAGE_FMLAIID") = lngAiid
        Else
            If VLng("PAGE_FMLAIID") = 0 Then
                ViewState("PAGE_FMLAIID") = RLng("urlfmlaiid")
            End If
        End If

        Return VLng("PAGE_FMLAIID")
    End Function
End Class

End Namespace
