Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvAutoCoding
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents pnlAutoCoding As System.Web.UI.WebControls.Panel
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelOneACoding As System.Web.UI.WebControls.Button
    Protected WithEvents btnEditOneACoding As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label

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

        If RStr("acodeaction") = "rowclick" Then
            ViewState("PAGE_RECID") = RLng("RECID")
        Else
            If VLng("PAGE_RECID") = 0 Then
                ViewState("PAGE_RECID") = RLng("RECID")
            End If
        End If
        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        'Ĭ�ϲ���ʾ���С��޸ġ�Button
        btnSNumEditConstant.Enabled = False
        btnSNumEditDate.Enabled = False
        btnSNumEditSNum.Enabled = False

        txtSNumDigitNum.Attributes.Add("style", "TEXT-ALIGN: right;")
        txtNumToSkip.Attributes.Add("style", "TEXT-ALIGN: right;")
        txtSNumValue.Attributes.Add("style", "TEXT-ALIGN: right;")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblFieldName.Text = VStr("PAGE_COLDISPNAME")

        ShowDataGridData() '����Load���б������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        If RStr("acodeaction") = "rowclick" Then
            Try
                If VLng("PAGE_RECID") > 0 Then LoadValueForEdit(VLng("PAGE_RECID")) '�༭ģʽ��Loadԭ�ȵ�ֵ

                ShowDataGridData() '����Load���б������
            Catch ex As Exception
                PromptMsg("�༭�Զ������쳣ʧ�ܣ�", ex, True)
            End Try
        End If
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

        WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
        CreateDataGridColumns() '���뽫����Column����DataGrid1_Init()�¼�����У�������ʹ��DataGrid�Դ����С��༭������ɾ�����ȹ���
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ���в��ִ�������ʹ��
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim row As DataGridItem
            Dim lngRecIDClicked As Long = VLng("PAGE_RECID")
            For Each row In DataGrid1.Items
                '��1�б����Ǽ�¼ID
                Dim strRecID As String = row.Cells(0).Text.Trim()
                If IsNumeric(strRecID) Then
                    row.Attributes.Add("RECID", strRecID) '�ڿͻ��˱����¼ID
                    row.Attributes.Add("OnClick", "RowLeftClickPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()

                    If RStr("acodeaction") = "rowclick" And lngRecIDClicked > 0 Then
                        If lngRecIDClicked = CLng(strRecID) Then
                            row.Attributes.Add("bgColor", "#C1D2EE") '�޸ı������¼�ı�����ɫ
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Try
            CTableColACoding.Delete(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), CLng(e.Item.Cells(0).Text))
            ViewState("PAGE_RECID") = 0

            DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
            DataGrid1.EditItemIndex = -1
            ShowDataGridData() '����Load���б������

            '���ɾ����ʱ��ͬ��ѡ�и��У����ԡ��޸�...�������Զ�Enable
            btnSNumEditConstant.Enabled = False
            btnSNumEditDate.Enabled = False
            btnSNumEditSNum.Enabled = False
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnSNumAddConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumAddConstant.Click
        Try
            Dim strConstant As String = txtSNumConstant.Text.Trim()
            If strConstant.Length = 0 Then
                PromptMsg("��������ȷ�ĳ���ֵ")
            Else
                Dim pst As CmsPassport = CmsPass()
                CTableColACoding.AddConstant(pst, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), strConstant)
            End If
        Catch ex As Exception
            PromptMsg("��ӳ���ʧ�ܣ�", ex, True)
        End Try

        ShowDataGridData() '����Load���б������
    End Sub

    Private Sub btnSNumEditConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumEditConstant.Click
        Try
            If VLng("PAGE_RECID") <= 0 Then
                PromptMsg("ҳ�洦���߼�����", Nothing, True)
                Return
            End If

            Dim strConstant As String = txtSNumConstant.Text.Trim()
            If strConstant.Length = 0 Then
                PromptMsg("��������ȷ�ĳ���ֵ")
            Else
                CTableColACoding.EditConstant(CmsPass, VLng("PAGE_RECID"), strConstant)
            End If

            btnSNumEditConstant.Enabled = False

            ShowDataGridData() '����Load���б������
        Catch ex As Exception
            PromptMsg("�༭����ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnSNumAddDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumAddDate.Click
        Try
            Dim strConstant As String = GetACodingDate()
            If strConstant.Length = 0 Then
                PromptMsg("��ѡ����ȷ�����ڱ��ʽ")
            Else
                CTableColACoding.AddDate(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), strConstant)
            End If

            ShowDataGridData() '����Load���б������
        Catch ex As Exception
            PromptMsg("�������ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnSNumEditDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumEditDate.Click
        Try
            If VLng("PAGE_RECID") <= 0 Then
                PromptMsg("ҳ�洦���߼�����")
                Return
            End If

            Dim strConstant As String = GetACodingDate()
            If strConstant.Length = 0 Then
                PromptMsg("��ѡ����ȷ�����ڱ��ʽ")
            Else
                CTableColACoding.EditDate(CmsPass, VLng("PAGE_RECID"), strConstant)
            End If

            btnSNumEditDate.Enabled = False

            ShowDataGridData() '����Load���б������
        Catch ex As Exception
            PromptMsg("�������ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnSNumAddSNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumAddSNum.Click
        Try
            '��ȡ����ʱ��
            Dim lngResetTime As Long = GetResetTime()
            If lngResetTime = -1 Then
                PromptMsg("��ѡ����ȷ�����ڱ��ʽ")
                Return
            End If
            Dim lngResetTag As Long = GetResetTag()

            '��ȡ��ˮ��λ��
            If Not IsNumeric(txtSNumDigitNum.Text.Trim()) Then
                PromptMsg("��ѡ����ȷ����ˮ��λ��")
                Return
            End If
            Dim lngDigitNum As Long = CLng(txtSNumDigitNum.Text.Trim())

            '��ȡ��ˮ��λ���Ƿ�0
            Dim lngPrefixZero As Long
            If chkSNumPreZero.Checked Then
                lngPrefixZero = 1
            Else
                lngPrefixZero = 0
            End If

            '��ȡ����������
            Dim strSkipNum As String = txtNumToSkip.Text.Trim()

            CTableColACoding.AddSeriesNum(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), lngDigitNum, lngPrefixZero, lngResetTime, lngResetTag, strSkipNum)
        Catch ex As Exception
            PromptMsg("�����ˮ��ʧ�ܣ�", ex, True)
        End Try

        ShowDataGridData() '����Load���б������
    End Sub

    Private Sub btnSNumEditSNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNumEditSNum.Click
        Try
            If VLng("PAGE_RECID") <= 0 Then
                PromptMsg("ҳ�洦���߼�����")
                Return
            End If

            '��ȡ����ʱ��
            Dim lngResetTime As Long = GetResetTime()
            If lngResetTime = -1 Then
                PromptMsg("��ѡ����ȷ�����ڱ��ʽ")
                Return
            End If
            Dim lngResetTag As Long = GetResetTag()

            '��ȡ��ˮ��λ��
            If Not IsNumeric(txtSNumDigitNum.Text.Trim()) Then
                PromptMsg("��ѡ����ȷ����ˮ��λ��")
                Return
            End If
            Dim lngDigitNum As Long = CLng(txtSNumDigitNum.Text.Trim())

            '��ȡ��ˮ��λ���Ƿ�0
            Dim lngPrefixZero As Long
            If chkSNumPreZero.Checked Then
                lngPrefixZero = 1
            Else
                lngPrefixZero = 0
            End If

            '��ȡ����������
            Dim strSkipNum As String = txtNumToSkip.Text.Trim()

            CTableColACoding.EditSeriesNum(CmsPass, VLng("PAGE_RECID"), lngDigitNum, lngPrefixZero, lngResetTime, lngResetTag, strSkipNum, CLng(txtSNumValue.Text.Trim()))

            btnSNumEditSNum.Enabled = False

            ShowDataGridData() '����Load���б������
        Catch ex As Exception
            PromptMsg("�༭��ˮ��ʧ�ܣ�", ex, True)
        End Try
    End Sub

    '--------------------------------------------------
    '��ȡ�Զ���������������
    '--------------------------------------------------
    Private Function GetACodingDate() As String
        Dim strConstant As String
        Try
            If rdoSNumYear1.Checked Then
                strConstant = "YYYY"
            ElseIf rdoSNumYear2.Checked Then
                strConstant = "YY"
            ElseIf rdoSNumMonth.Checked Then
                strConstant = "MM"
            ElseIf rdoSNumDate.Checked Then
                strConstant = "DD"
            Else
                strConstant = ""
            End If
        Catch ex As Exception
            strConstant = ""
        End Try

        Return strConstant
    End Function

    '--------------------------------------------------
    '��ȡ����������ʱ������
    '--------------------------------------------------
    Private Function GetResetTime() As Long
        Dim lngResetTime As Long = -1
        If rdoSNumNoReset.Checked Then
            lngResetTime = ACodingResetTime.NoReset
        ElseIf rdoSNumYearReset.Checked Then
            lngResetTime = ACodingResetTime.YearReset
        ElseIf rdoSNumMonthReset.Checked Then
            lngResetTime = ACodingResetTime.MonthReset
        ElseIf rdoSNumDateReset.Checked Then
            lngResetTime = ACodingResetTime.DateReset
        Else
            lngResetTime = -1
        End If
        Return lngResetTime
    End Function

    '--------------------------------------------------
    '��ȡ����������ʱ������
    '--------------------------------------------------
    Private Function GetResetTag() As Long
        Dim lngResetTag As Long = 0
        If rdoSNumNoReset.Checked Then
            lngResetTag = 0
        ElseIf rdoSNumYearReset.Checked Then
            lngResetTag = Today.Year
        ElseIf rdoSNumMonthReset.Checked Then
            lngResetTag = Today.Month
        ElseIf rdoSNumDateReset.Checked Then
            lngResetTag = Today.Day
        Else
            lngResetTag = 0
        End If
        Return lngResetTag
    End Function

    '----------------------------------------------------------
    '��ʾ��������
    '----------------------------------------------------------
    Private Sub ShowDataGridData()
        Try
            Dim pst As CmsPassport = CmsPass()
            DataGrid1.DataSource = CTableColACoding.GetDatasetForAdmin(pst, VLng("PAGE_RESID"), VStr("PAGE_COLNAME")).Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            SLog.Err("��ȡ�Զ�������Ϣʧ�ܣ�", ex)
        End Try
    End Sub

    '------------------------------------------------------------------
    '������Դ���ݱ����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumns()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "�ڲ��ֶ�ID"
        col.DataField = "CDC_AIID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        col.ReadOnly = True
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "��������"
        col.DataField = "CDC_TYPE_NAME"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "����ֵ"
        col.DataField = "CDC_VALUE"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        intWidth += 250

        Dim colDel As ButtonColumn = New ButtonColumn
        colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
        colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '������ͼ�꣬�磺"<img src='/cmsweb/images/common/delete.gif' border=0>"
        colDel.CommandName = "Delete"
        colDel.ButtonType = ButtonColumnType.LinkButton
        colDel.ItemStyle.Width = Unit.Pixel(80)
        colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        DataGrid1.Columns.Add(colDel)
        intWidth += 80

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '�༭ģʽ��Loadԭ�ȵ�ֵ
    '---------------------------------------------------------------
    Private Sub LoadValueForEdit(ByVal lngAIID As Long)
        '�û������ĳ���Զ��������ã�Load���б���������Ϣ
        Dim datACode As DataAutoCoding = CTableColACoding.GetOneACoding(CmsPass, lngAIID)
        If datACode Is Nothing Then Return

        If datACode.lngCDC_TYPE = ACodingType.IsConstant Then
            btnSNumEditConstant.Enabled = True
            'btnSNumEditDate.Enabled = False
            'btnSNumEditSNum.Enabled = False

            txtSNumConstant.Text = datACode.strCDC_VALUE
        ElseIf datACode.lngCDC_TYPE = ACodingType.IsDate Then
            'btnSNumEditConstant.Enabled = False
            btnSNumEditDate.Enabled = True
            'btnSNumEditSNum.Enabled = False

            If datACode.strCDC_VALUE = "YYYY" Then
                rdoSNumYear1.Checked = True
            ElseIf datACode.strCDC_VALUE = "YY" Then
                rdoSNumYear2.Checked = True
            ElseIf datACode.strCDC_VALUE = "MM" Then
                rdoSNumMonth.Checked = True
            ElseIf datACode.strCDC_VALUE = "DD" Then
                rdoSNumDate.Checked = True
            End If
        ElseIf datACode.lngCDC_TYPE = ACodingType.IsSeriesNum Then
            'btnSNumEditConstant.Enabled = False
            'btnSNumEditDate.Enabled = False
            btnSNumEditSNum.Enabled = True

            If datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.NoReset Then
                rdoSNumNoReset.Checked = True
            ElseIf datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.YearReset Then
                rdoSNumYearReset.Checked = True
            ElseIf datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.MonthReset Then
                rdoSNumMonthReset.Checked = True
            ElseIf datACode.lngCDC_SNUM_RESET_TIME = ACodingResetTime.DateReset Then
                rdoSNumDateReset.Checked = True
            End If
            txtSNumDigitNum.Text = CStr(datACode.lngCDC_SNUM_LENGTH)
            If datACode.lngCDC_SNUM_PREZERO = 1 Then
                chkSNumPreZero.Checked = True
            Else
                chkSNumPreZero.Checked = False
            End If

            txtNumToSkip.Text = CStr(datACode.strCDC_SNUM_SKIP)
            If datACode.strCDC_VALUE = "" Then
                txtSNumValue.Text = "1"
            Else
                txtSNumValue.Text = datACode.strCDC_VALUE
            End If

            txtSNumValue.Text = datACode.strCDC_VALUE
        End If
    End Sub
End Class

End Namespace
