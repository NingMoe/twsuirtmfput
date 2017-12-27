Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


    Partial Class MTableSearchColDef_1
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

            If VLng("PAGE_MTSHOSTID") = 0 Then
                ViewState("PAGE_MTSHOSTID") = RLng("mtshostid")
            Else

            End If
            If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.Unknown Then
                ViewState("PAGE_MTSLIST_TYPE") = RInt("mtstype")
            End If

            If VStr("PAGE_EMPID") = "" Then '���ڣ��м�¼Ȩ��
                ViewState("PAGE_EMPID") = RStr("mnuempid")
                If VStr("PAGE_EMPID") = "" Then
                    ViewState("PAGE_EMPID") = CmsPass.Employee.ID
                End If
            End If

            If VStr("PAGE_COLNAME") = "" Then '���ڣ��߼��ֵ��й�������
                ViewState("PAGE_COLNAME") = RStr("mnucolname")
            End If
            If VLng("PAGE_ADVDICT_HOSTRES") = 0 Then '���ڣ��߼��ֵ��й�������
                ViewState("PAGE_ADVDICT_HOSTRES") = RStr("advdict_hostresid")
            End If

            If VStr("PAGE_COLURLRECID") = "" Then
                ViewState("PAGE_COLURLRECID") = RStr("colurlid")
            End If
        End Sub

        Protected Overrides Sub CmsPageInitialize()
            ddlResList.AutoPostBack = True
            btnDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ����ǰ������');")

            Select Case VInt("PAGE_MTSLIST_TYPE")
                Case MTSearchType.ColumnUrlFilter
                    lblTitle.Text = "�ֶ�������������"
                    btnAddEmail.Visible = False
                    btnAddMobile.Visible = False

                    EnableSaveTitle(False)
                    lbtnSelHostRes.Enabled = False
                    btnStartSearch.Visible = False
                    btnAddShow.Visible = False
                    btnAddOrderASC.Visible = False
                    btnAddOrderDesc.Visible = False


                    Dim lngMTSHostID As Long = CmsDbBase.GetFieldLng(CmsPass, CmsTables.MTableHost, "MTS_ID", "MTS_TYPE=" & MTSearchType.ColumnUrlFilter & " AND MTS_RESID=" + VLng("PAGE_RESID").ToString + " and MTS_COLURLID=" & VLng("PAGE_COLURLRECID") & " AND MTS_EMPID='" & VStr("PAGE_EMPID") & "'")
                    If lngMTSHostID <> 0 Then
                        ViewState("PAGE_MTSHOSTID") = lngMTSHostID
                    End If

            End Select
        End Sub

        Protected Overrides Sub CmsPageDealFirstRequest()
            Dim hashFieldVal As Hashtable = Nothing
            If VLng("PAGE_MTSHOSTID") <> 0 Then '������Ƶ�����
                hashFieldVal = CmsDbBase.GetRecordByWhere(CmsPass, CmsTables.MTableHost, "MTS_ID=" & VLng("PAGE_MTSHOSTID"))
                ViewState("PAGE_RESID") = HashField.GetLng(hashFieldVal, "MTS_RESID")
            Else
                '����Ƿ�մ�ѡ����Դ�������
                If RLng("selresid") <> 0 Then
                    ViewState("PAGE_RESID") = RLng("selresid")
                End If
            End If

            txtMTSearchTitle.Text = HashField.GetStr(hashFieldVal, "MTS_TITLE")
            ShowAfterHostResSelected(VLng("PAGE_RESID")) '������Դѡ�к���ʾ��ص���Ϣ

            '��ʾĬ��ֵ�б�
            dllDefaultVal.Items.Clear()
            dllDefaultVal.Items.Add(New ListItem("", ""))
            dllDefaultVal.Items.Add(New ListItem("��ǰ�û��ʺ�", "[CUR_USERID]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ�û�����", "[CUR_USERNAME]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ�û�����", "[CUR_USERDEPNAME]"))
            dllDefaultVal.Items.Add(New ListItem("�������", "[PREV_YEAR]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ���", "[CUR_YEAR]"))
            dllDefaultVal.Items.Add(New ListItem("�������", "[NEXT_YEAR]"))
            dllDefaultVal.Items.Add(New ListItem("�����·�", "[PREV_MONTH]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ�·�", "[CUR_MONTH]"))
            dllDefaultVal.Items.Add(New ListItem("�����·�", "[NEXT_MONTH]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ����", "[CUR_DATE]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ����", "[CUR_MONTHDATE]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ����(���ձȽ�)", "[BIRTHDAY]"))
            dllDefaultVal.Items.Add(New ListItem("��������", "[TOMORROW]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰ���ڼ�", "[CUR_DAYOFWEEK]"))
            dllDefaultVal.Items.Add(New ListItem("��ǰСʱ", "[CUR_HOUR]"))
            dllDefaultVal.Items.Add(New ListItem("���µ�һ��", "[PREV_MONTH_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("���µ�һ��", "[CUR_MONTH_FSTDAY]"))
            'dllDefaultVal.Items.Add(New ListItem("�������һ��", "[CUR_MONTH_LSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("���µ�һ��", "[NEXT_MONTH_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("�����µ�һ��", "[NNEXT_MONTH_FSTDAY]"))

            dllDefaultVal.Items.Add(New ListItem("��������һ", "[PREVWK_MON]"))
            dllDefaultVal.Items.Add(New ListItem("����������", "[PREVWK_SAT]"))
            dllDefaultVal.Items.Add(New ListItem("��������һ", "[THISWK_MON]"))
            dllDefaultVal.Items.Add(New ListItem("����������", "[THISWK_SAT]"))
            dllDefaultVal.Items.Add(New ListItem("��������һ", "[NEXTWK_MON]"))
            dllDefaultVal.Items.Add(New ListItem("����������", "[NEXTWK_SAT]"))
            dllDefaultVal.Items.Add(New ListItem("����������һ", "[NNEXTWK_MON]"))

            dllDefaultVal.Items.Add(New ListItem("�ϼ���һ��", "[PREV_QTR_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("������һ��", "[CUR_QTR_FSTDAY]"))
            dllDefaultVal.Items.Add(New ListItem("�¼���һ��", "[NEXT_QTR_FSTDAY]"))

            GridDataBind() '������
        End Sub

        Protected Overrides Sub CmsPageDealPostBack()
        End Sub

        Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
            If CmsPass.EmpIsSysAdmin = False Then  'And CmsPass.EmpIsDepAdmin = False Then
                If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableView Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.MultiTableViewForTemp Then
                    '��ϵͳ����Ա����Ҫ�ж�Ȩ��
                    If MultiTableSearch.HasRightsOnMTSResources(CmsPass, VLng("PAGE_MTSHOSTID")) = False Then
                        Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "checkrights_fail=yes"), False)
                        Return
                    End If
                End If
            End If

            If VLng("PAGE_MTSHOSTID") <> 0 Or VLng("PAGE_RESID") <> 0 Then '�Ǳ༭״̬
                lbtnSelHostRes.Enabled = False
            End If
        End Sub

        Private Sub btnAddShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddShow.Click
            AddColumnForHostRes(MTSearchColumnType.Show)
            GridDataBind() '������
        End Sub

        Private Sub btnAddCond_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCond.Click
            AddColumnForHostRes(MTSearchColumnType.Condition)

            '��װ��������������
            'Dim strWhere As String = MultiTableSearchColumn.GetWhereOfColCondition(CmsPass, VLng("PAGE_MTSHOSTID"))
            'Dim hashFieldVal As New Hashtable
            'hashFieldVal.Add("MTS_WHERE", strWhere)
            'CmsDbBase.EditRecord(CmsPass, CmsTables.MTableHost, VLng("PAGE_MTSHOSTID"), hashFieldVal)

            GridDataBind() '������
        End Sub

        Private Sub btnAddOrderASC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrderASC.Click
            AddColumnForHostRes(MTSearchColumnType.Order, True)
            GridDataBind() '������
        End Sub

        Private Sub btnAddOrderDesc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrderDesc.Click
            AddColumnForHostRes(MTSearchColumnType.Order, False)
            GridDataBind() '������
        End Sub

        Private Sub btnAddEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEmail.Click
            AddColumnForHostRes(MTSearchColumnType.Email)
            GridDataBind() '������
        End Sub

        Private Sub btnAddMobile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMobile.Click
            AddColumnForHostRes(MTSearchColumnType.MobilePhone)
            GridDataBind() '������
        End Sub

        Private Sub ddlResList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlResList.SelectedIndexChanged
            '��ʾ��������Դ���ֶ���Ϣ
            Dim strCurResID As String = ddlResList.SelectedValue
            If IsNumeric(strCurResID) = False Then Return
            ViewState("PAGE_COLTYPE_" & CLng(strCurResID)) = WebUtilities.LoadResColumnsInDdlist(CmsPass, CLng(strCurResID), ddlHostResCol, , , False, , , )
            WebUtilities.LoadResColumnsInDdlist(CmsPass, CLng(strCurResID), ddlValCol, , , False, , , )
            GridDataBind() '������
        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            If txtMTSearchTitle.Text.Trim() = "" Then
                PromptMsg("��������Ч�ı��⣡")
                Return
            End If
            If VLng("PAGE_MTSHOSTID") = 0 Then '��û����ӹ����ͳ�ƺ��й��������ֶζ���
                PromptMsg("���������Ч����ʾ�ֶΡ�")
                Return
            Else
                Dim hashFieldVal As New Hashtable
                hashFieldVal.Add("MTS_TITLE", txtMTSearchTitle.Text.Trim())
                hashFieldVal.Add("MTS_TYPE", VInt("PAGE_MTSLIST_TYPE"))
                hashFieldVal.Add("MTS_EMPID", VStr("PAGE_EMPID"))
                'Dim strWhere As String = MultiTableSearchColumn.GetWhereOfColCondition(CmsPass, VLng("PAGE_MTSHOSTID"))
                'hashFieldVal.Add("MTS_WHERE", strWhere)
                'hashFieldVal.Add("MTS_TABLELOGIC", IIf(rdoTableAnd.Checked = True, "AND", "OR"))
                hashFieldVal.Add("MTS_TABLELOGIC", "AND")
                CmsDbBase.EditRecordByWhere(CmsPass, CmsTables.MTableHost, hashFieldVal, "MTS_ID=" & VLng("PAGE_MTSHOSTID"), "MTS_EDTID", "MTS_EDTTIME")
                If Not Request("mtspid") Is Nothing Then
                    SDbStatement.Execute("update " & CmsTables.MTableHost & " set MTS_PID=" & RLng("mtspid") & " where MTS_ID=" & VLng("PAGE_MTSHOSTID"))
                End If
            End If
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Try
                Dim lngRecID As Long = VLng("PAGE_MTSHOSTID")
                MultiTableSearch.DelMTSRecordByID(CmsPass, lngRecID)
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                'ViewState("PAGE_MTSHOSTID") = 0
                'GridDataBind() '������
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
        End Sub

        Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        End Sub

        Private Sub lbtnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveUp.Click
            Dim lngRecID As Long = Me.GetRecIDOfGrid()
            If lngRecID = 0 Then
                PromptMsg("����ѡ����Ч���ֶ����ü�¼��")
                Return
            End If
            MultiTableSearchColumn.MoveUp(CmsPass, CmsTables.MTableColDef, VLng("PAGE_MTSHOSTID"), lngRecID)
            GridDataBind() '������
        End Sub

        Private Sub lbtnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveDown.Click
            Dim lngRecID As Long = Me.GetRecIDOfGrid()
            If lngRecID = 0 Then
                PromptMsg("����ѡ����Ч���ֶ����ü�¼��")
                Return
            End If
            MultiTableSearchColumn.MoveDown(CmsPass, CmsTables.MTableColDef, VLng("PAGE_MTSHOSTID"), lngRecID)
            GridDataBind() '������
        End Sub

        Private Sub lbtnSelHostRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelHostRes.Click
            Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/MTableSearchColDef.aspx?mtshostid=" & VLng("PAGE_MTSHOSTID") & "&mtstype=" & VInt("PAGE_MTSLIST_TYPE") & "&mnuempid=" & VStr("PAGE_EMPID")
            Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx", False)
        End Sub

        Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
            Try
                If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                    Response.Redirect("/cmsweb/Logout.aspx", True)
                    Return
                End If
                CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

                WebUtilities.InitialDataGrid(DataGrid1) '��ʼ��DataGrid����
                CreateDataGridColumn()
            Catch ex As Exception
                SLog.Err("����ѯ�б���ʾ�쳣����", ex)
            End Try
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
                Dim lngRecID As Long = CLng(e.Item.Cells(0).Text)
                CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.MTableColDef, "MTSCOL_ID=" & lngRecID)
                CmsDbBase.ClearTableCache(CmsTables.MTableColDef)
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
                Dim lngRecID As Long = CLng(e.Item.Cells(0).Text)  '��Cell����ʾ����Ϊ�ж����������޸ļ�¼
                CmsDbBase.EditRecordByWhere(CmsPass, CmsTables.MTableColDef, hashColKeyValue, "MTSCOL_ID=" & lngRecID, "MTSCOL_EDTID", "MTSCOL_EDTTIME")
                '----------------------------------------------------------------
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            DataGrid1.EditItemIndex = -1
            GridDataBind()
        End Sub

        Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
            Try
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
            Catch ex As Exception
                SLog.Err("����ѯ�б���ʾ�쳣����", ex)
            End Try
        End Sub

        Private Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            Try
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
            Catch ex As Exception
                SLog.Err("����ѯ�б���ʾ�쳣����", ex)
            End Try
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
            col.HeaderText = "MTSCOL_ID"
            col.DataField = "MTSCOL_ID"
            col.ItemStyle.Width = Unit.Pixel(1)
            col.Visible = False
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("MTSCOL_ID", "MTSCOL_ID")

            Dim colEdit As EditCommandColumn = New EditCommandColumn
            colEdit.HeaderText = "�༭"
            colEdit.EditText = "�༭"
            colEdit.UpdateText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_SAVE")
            colEdit.CancelText = "ȡ��"
            colEdit.ButtonType = ButtonColumnType.LinkButton
            colEdit.ItemStyle.Width = Unit.Pixel(65)
            DataGrid1.Columns.Add(colEdit)
            intWidth += 65

            col = New BoundColumn
            col.HeaderText = "����"
            col.DataField = "MTSCOL_TYPE2"
            col.ItemStyle.Width = Unit.Pixel(70)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("����", "MTSCOL_TYPE2")
            intWidth += 70

            col = New BoundColumn
            col.HeaderText = "��Դ����"
            col.DataField = "MTSCOL_RESID22"
            col.ItemStyle.Width = Unit.Pixel(100)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("��Դ����", "MTSCOL_RESID22")
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "�ֶ�����"
            col.DataField = "MTSCOL_COLDISPNAME"
            col.ItemStyle.Width = Unit.Pixel(100)
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("�ֶ�����", "MTSCOL_COLDISPNAME")
            intWidth += 100

            col = New BoundColumn
            col.HeaderText = "�ֶ�����"
            col.DataField = "MTSCOL_COLCOND"
            col.ItemStyle.Width = Unit.Pixel(65)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("�ֶ�����", "MTSCOL_COLCOND")
            intWidth += 65

            col = New BoundColumn
            col.HeaderText = "�ֶ�����ֵ"
            col.DataField = "MTSCOL_COLVALUE"
            col.ItemStyle.Width = Unit.Pixel(90)
            col.ReadOnly = True
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("�ֶ�����ֵ", "MTSCOL_COLVALUE")
            intWidth += 90

            col = New BoundColumn
            col.HeaderText = "�߼�"
            col.DataField = "MTSCOL_LOGIC"
            col.ItemStyle.Width = Unit.Pixel(40)
            DataGrid1.Columns.Add(col)
            hashColumnNames.Add("�߼�", "MTSCOL_LOGIC")
            intWidth += 40

            If VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.GeneralRowWhere And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.PersonalRowWhere And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.RowRights And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.AdvDictFilter And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.BatchSend Then
                col = New BoundColumn
                col.HeaderText = "��ʾ��ʽ"
                col.DataField = "MTSCOL_SHOWFORMAT"
                col.ItemStyle.Width = Unit.Pixel(90)
                DataGrid1.Columns.Add(col)
                hashColumnNames.Add("��ʾ��ʽ", "MTSCOL_SHOWFORMAT")
                intWidth += 90

                col = New BoundColumn
                col.HeaderText = "��ʾ���"
                col.DataField = "MTSCOL_SHOWWIDTH"
                col.ItemStyle.Width = Unit.Pixel(65)
                DataGrid1.Columns.Add(col)
                hashColumnNames.Add("��ʾ���", "MTSCOL_SHOWWIDTH")
                intWidth += 65
            End If

            If VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.RowRights And VInt("PAGE_MTSLIST_TYPE") <> MTSearchType.AdvDictFilter Then
                col = New BoundColumn
                col.HeaderText = "����"
                col.DataField = "MTSCOL_ORDERBY2"
                col.ItemStyle.Width = Unit.Pixel(40)
                col.ReadOnly = True
                DataGrid1.Columns.Add(col)
                hashColumnNames.Add("����", "MTSCOL_ORDERBY2")
                intWidth += 40
            End If

            Dim colDel As ButtonColumn = New ButtonColumn
            colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
            colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '������ͼ�꣬�磺"<img src='/cmsweb/images/common/delete.gif' border=0>"
            colDel.CommandName = "Delete"
            colDel.ButtonType = ButtonColumnType.LinkButton
            colDel.ItemStyle.Width = Unit.Pixel(40)
            colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            DataGrid1.Columns.Add(colDel)
            intWidth += 40

            DataGrid1.Width = Unit.Pixel(intWidth)

            '���ڱ����ֶ����ƺ���ʾ���ƶԣ��Ա�����������ʱʹ��
            ViewState("COLMGR_COLUMN_NAMES") = hashColumnNames
        End Sub

        '---------------------------------------------------------------
        '������
        '---------------------------------------------------------------
        Private Sub GridDataBind()
            Try
                Dim ds As DataSet = MultiTableSearchColumn.GetMTSearchColdefByDataSet(CmsPass, VLng("PAGE_MTSHOSTID"))
                DataGrid1.DataSource = ds.Tables(0).DefaultView
                DataGrid1.DataBind()
            Catch ex As Exception
                SLog.Err("�󶨶��ͳ�ƺ��й���������Ϣʱ����", ex)
            End Try
        End Sub

        '------------------------------------------------------------------------------
        'Ϊ���ͳ�Ƽ�¼�������������¼
        '------------------------------------------------------------------------------
        Private Function AddMTSearchRecordForTemp(ByVal lngCurResID As Long, ByVal intMTSType As Integer) As Long
            Dim hashFieldVal As New Hashtable
            hashFieldVal.Add("MTS_RESID", lngCurResID)
            hashFieldVal.Add("MTS_TITLE", txtMTSearchTitle.Text.Trim())
            If intMTSType <> MTSearchType.MultiTableView Then
                hashFieldVal.Add("MTS_TYPE", intMTSType)
            Else
                hashFieldVal.Add("MTS_TYPE", MTSearchType.MultiTableViewForTemp)
            End If
            hashFieldVal.Add("MTS_EMPID", VStr("PAGE_EMPID"))
            hashFieldVal.Add("MTS_COLNAME", VStr("PAGE_COLNAME"))
            'hashFieldVal.Add("MTS_TABLELOGIC", IIf(rdoTableAnd.Checked = True, "AND", "OR"))
            hashFieldVal.Add("MTS_TABLELOGIC", "AND")
            hashFieldVal.Add("MTS_DICTRESID", VLng("PAGE_ADVDICT_HOSTRES"))
            hashFieldVal.Add("MTS_COLURLID", VLng("PAGE_COLURLRECID"))
            CmsDbBase.AddOrEditRecordByWhere(CmsPass, CmsTables.MTableHost, hashFieldVal, "", "MTS_ID", "MTS_SHOWORDER", "MTS_EDTID", "MTS_EDTTIME")
            Return HashField.GetLng(hashFieldVal, "MTS_ID")
        End Function

        '------------------------------------------------------------------------------
        '�������Դ�ֶζ���
        '------------------------------------------------------------------------------
        Private Sub AddColumnForHostRes(ByVal intMTSColType As MTSearchColumnType, Optional ByVal blnIsAscOrder As Boolean = True)
            Dim hashFieldVal As New Hashtable

            If VLng("PAGE_RESID") = 0 Then
                PromptMsg("��ѡ����ȷ������Դ")
                Return
            End If
            If IsNumeric(ddlResList.SelectedValue) = False Then
                PromptMsg("��ѡ����ȷ����Դ")
                Return
            End If
            Dim lngCurResID As Long = CLng(ddlResList.SelectedValue)
            Dim strCurResTable As String = CmsPass.GetDataRes(lngCurResID).ResTable

            hashFieldVal.Add("MTSCOL_RESID", lngCurResID)

            Dim lngHostID As Long = VLng("PAGE_MTSHOSTID")
            Dim MTSCOL_EDTID As String = Request.QueryString("mnuempid")
            If lngHostID = 0 Then  '������
                'Ϊ���ͳ�Ƽ�¼�������������¼
               lngHostID = AddMTSearchRecordForTemp(lngCurResID, VInt("PAGE_MTSLIST_TYPE"))

                ViewState("PAGE_MTSHOSTID") = lngHostID
            End If

            hashFieldVal.Add("MTSCOL_HOSTID", lngHostID)
            hashFieldVal.Add("MTSCOL_TYPE", intMTSColType)
            If (MTSCOL_EDTID.Trim() <> "") Then
                hashFieldVal.Add("MTSCOL_EDTID", MTSCOL_EDTID)
                hashFieldVal.Add("MTSCOL_EDTTIME", DateTime.Now)
            End If

            If ddlHostResCol.SelectedValue = "" Then
                PromptMsg("��ѡ����ȷ����Դ�ֶ�")
                Return
            End If
            hashFieldVal.Add("MTSCOL_COLNAME", ddlHostResCol.SelectedValue)

            hashFieldVal.Add("MTSCOL_COLDISPNAME", ddlHostResCol.SelectedItem.Text)

            If intMTSColType = MTSearchColumnType.Show Then
                'CmsPass.HostResData.ResTableType δȡ��ֵ??
                Dim dv As DataView = ResFactory.TableService("TWOD").GetTableColumns(CmsPass, lngCurResID)
                dv.RowFilter = "CD_COLNAME='" & ddlHostResCol.SelectedValue & "'"
                If dv.Count > 0 Then
                    hashFieldVal.Add("MTSCOL_SHOWFORMAT", DbField.GetStr(dv(0), "cs_format")) '��ʼ�����ʾ��ʽ
                Else
                    hashFieldVal.Add("MTSCOL_SHOWFORMAT", "") '��ʼ���ʱ��������ʾ��ʽ
                End If

                hashFieldVal.Add("MTSCOL_SHOWWIDTH", "70") '��ʼ���ʱ��ʾ���Ϊ70
            ElseIf intMTSColType = MTSearchColumnType.Order Then
                hashFieldVal.Add("MTSCOL_COLVALUE", CLng(IIf(blnIsAscOrder = True, 0, 1)))
            ElseIf intMTSColType = MTSearchColumnType.Condition Then
                If ddlHostConditions.SelectedValue = "" Then
                    PromptMsg("��ѡ����ȷ������Դ�ֶε��ж�����")
                    Return
                End If
                hashFieldVal.Add("MTSCOL_COLCOND", ddlHostConditions.SelectedItem.Text)

                '��ȡ����ֵ
                Dim strOneWhere As String = ""
                Dim intColValType As CondValType = CondValType.IsText
                If dllDefaultVal.SelectedValue <> "" Then
                    intColValType = CondValType.IsDefaultValue
                    Dim strValue As String = ""
                    If txtColValue.Text.Trim() <> "" Then
                        hashFieldVal.Add("MTSCOL_COLVALUE", dllDefaultVal.SelectedItem.Text & txtColValue.Text.Trim())
                        strValue = dllDefaultVal.SelectedValue & txtColValue.Text.Trim()
                    Else
                        hashFieldVal.Add("MTSCOL_COLVALUE", dllDefaultVal.SelectedItem.Text)
                        strValue = dllDefaultVal.SelectedValue
                    End If
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, strValue, CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, False)
                ElseIf txtColValue.Text.Trim() <> "" Then
                    intColValType = CondValType.IsText
                    hashFieldVal.Add("MTSCOL_COLVALUE", txtColValue.Text.Trim())
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, txtColValue.Text.Trim(), CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, False)
                ElseIf ddlValCol.SelectedValue <> "" Then
                    intColValType = CondValType.IsColumnName
                    hashFieldVal.Add("MTSCOL_COLVALUE", ddlValCol.SelectedItem.Text)
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, strCurResTable & "." & ddlValCol.SelectedValue, CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, True)
                Else '��ֵ
                    intColValType = CondValType.IsText
                    hashFieldVal.Add("MTSCOL_COLVALUE", "")
                    strOneWhere = CmsWhere.AssembleOneWhereByValue(CmsPass, ddlHostResCol.SelectedValue, ddlHostConditions.SelectedValue, "", CType(ViewState("PAGE_COLTYPE_" & lngCurResID), Hashtable), strCurResTable, False)
                End If

                hashFieldVal.Add("MTSCOL_WHERE", strOneWhere)
                hashFieldVal.Add("MTSCOL_LOGIC", "AND") 'Ĭ��Ϊ���롱��ϵ
            ElseIf intMTSColType = MTSearchColumnType.Email Then
            ElseIf intMTSColType = MTSearchColumnType.MobilePhone Then
            End If

            CmsDbBase.AddOrEditRecordByWhere(CmsPass, CmsTables.MTableColDef, hashFieldVal, "", "MTSCOL_ID", "MTSCOL_SHOWORDER", "MTS_EDTID", "MTS_EDTTIME")
        End Sub

        '------------------------------------------------------------------------------
        '������Դѡ�к���ʾ��ص���Ϣ
        '------------------------------------------------------------------------------
        Private Sub ShowAfterHostResSelected(ByVal lngHostResID As Long)
            If lngHostResID <> 0 Then
                WebUtilities.LoadConditionsInDdlist(CmsPass, ddlHostConditions, True) '��ʼ��DropDownList�в���������

                Dim blnShowSubResList As Boolean = True
                If VInt("PAGE_MTSLIST_TYPE") = MTSearchType.GeneralRowWhere Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.PersonalRowWhere Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.RowRights Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.AdvDictFilter Or VInt("PAGE_MTSLIST_TYPE") = MTSearchType.ColumnUrlFilter Then
                    blnShowSubResList = False '���й������ù����Ĳ���ʾ����Դ�б�
                End If
                WebUtilities.LoadSubResourcesInDdlist(CmsPass, lngHostResID, ddlResList, False, , True, blnShowSubResList)

                '��ʾ����Դ����������Դ�б�
                ViewState("PAGE_COLTYPE_" & lngHostResID) = WebUtilities.LoadResColumnsInDdlist(CmsPass, lngHostResID, ddlHostResCol, , , False, , , )
                WebUtilities.LoadResColumnsInDdlist(CmsPass, CLng(lngHostResID), ddlValCol, , , False, , , )
            End If
        End Sub

        '------------------------------------------------------------------------------
        '��ʾ����������Ԫ��
        '------------------------------------------------------------------------------
        Private Sub EnableSaveTitle(ByVal blnEnable As Boolean)
            If blnEnable Then
                lblSearchTitle.Visible = True
                txtMTSearchTitle.Visible = True
                btnSave.Visible = True
            Else
                lblSearchTitle.Visible = False
                txtMTSearchTitle.Visible = False
                btnSave.Visible = False
            End If
        End Sub
    End Class

End Namespace
