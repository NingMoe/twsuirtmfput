Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvRadio
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
        lboxOptValues.EnableViewState = True
        lboxOptValues.Attributes.Add("ondblclick", "ListboxOptionClicked();")
        txtResName.ReadOnly = True
        txtFieldName.ReadOnly = True
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        txtFieldName.Text = VStr("PAGE_COLDISPNAME")

        LoadFieldOptions() 'Load�ֶ�����ѡ����
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim strInput As String = txtOptValue.Text.Trim()
        If strInput = "" Then
            PromptMsg("������ӿ�ֵ��ѡ��")
            Return
        End If
        lboxOptValues.Items.Add(strInput)

        txtOptValue.Text = ""
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If lboxOptValues.SelectedIndex < 0 Then
            PromptMsg("��ѡ����Ч��ѡ��������޸ģ�")
            Return
        End If

        Dim strInput As String = txtOptValue.Text.Trim()
        If strInput = "" Then strInput = "" '"(��)"
        lboxOptValues.SelectedItem.Text = strInput
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim intSel As Integer = lboxOptValues.SelectedIndex
        If intSel < 0 Then
            PromptMsg("��ѡ��������Ǹ�ѡ����֮ǰ��")
            Return
        End If
        Dim strInput As String = txtOptValue.Text.Trim()
        If strInput = "" Then strInput = "" '"(��)"

        '����ʱ��������ѡ����
        Dim alistTemp As New ArrayList
        Dim i As Integer
        For i = 0 To lboxOptValues.Items.Count - 1
            alistTemp.Add(lboxOptValues.Items(i).Text.Trim())
        Next

        '�������ѡ����
        lboxOptValues.Items.Clear()

        '�ٲ���ѡ����
        i = 0
        Dim str As String
        For Each str In alistTemp
            If intSel = i Then
                lboxOptValues.Items.Add(strInput)
            End If
            lboxOptValues.Items.Add(str)
            i += 1
        Next

        txtOptValue.Text = ""
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim intSel As Integer = lboxOptValues.SelectedIndex
        If intSel < 0 Then
            PromptMsg("��ѡ����Ҫɾ����ѡ���")
            Return
        End If

        If lboxOptValues.Items.Count > 0 Then
            lboxOptValues.Items.RemoveAt(intSel)
            If intSel >= lboxOptValues.Items.Count Then
                lboxOptValues.SelectedIndex = lboxOptValues.Items.Count - 1
            End If
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim intSel As Integer = lboxOptValues.SelectedIndex
        If intSel = 0 Then Return '��һ��

        Dim str1 As String = lboxOptValues.Items(intSel - 1).Text
        Dim str2 As String = lboxOptValues.Items(intSel).Text
        lboxOptValues.Items(intSel - 1).Text = str2
        lboxOptValues.Items(intSel).Text = str1
        lboxOptValues.SelectedIndex = intSel - 1
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim intSel As Integer = lboxOptValues.SelectedIndex
        If intSel >= lboxOptValues.Items.Count - 1 Then Return '���һ��


        Dim str1 As String = lboxOptValues.Items(intSel).Text
        Dim str2 As String = lboxOptValues.Items(intSel + 1).Text
        lboxOptValues.Items(intSel).Text = str2
        lboxOptValues.Items(intSel + 1).Text = str1
        lboxOptValues.SelectedIndex = intSel + 1
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim fldOpts As New ArrayList
        Dim li As ListItem
        For Each li In lboxOptValues.Items
            If li.Text.Trim() = "(��)" Or li.Text.Trim() = "" Then
                fldOpts.Add("")
            Else
                fldOpts.Add(li.Text.Trim())
            End If
        Next

        Try
            Dim intDescLines As Integer = CInt(txtDescRowNum.Text.Trim())
            Dim intTemp As Integer
            If rdoRow.Checked = True Then
                intTemp = 0
            Else
                intTemp = 1
            End If

            Dim intSaveStyle As Integer = 0
            If rdoSaveOptionValue.Checked = True Then
                intSaveStyle = 0
            ElseIf rdoSaveNumber.Checked = True Then
                intSaveStyle = 1
            ElseIf rdoSaveABC.Checked = True Then
                intSaveStyle = 2
            End If

            CTableColRadio.Save(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"), txtOptionDesc.Text.Trim(), intDescLines, intTemp, intSaveStyle, fldOpts)
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '--------------------------------------------------------------
    'Load�ֶ�����ѡ����
    '--------------------------------------------------------------
    Private Sub LoadFieldOptions()
        lboxOptValues.Items.Clear()

        txtOptionDesc.Text = CTableStructure.GetColValue(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        Dim intTemp As Integer = CTableStructure.GetFieldInt(CmsPass, VLng("PAGE_RESID"), "CD_INT1", VStr("PAGE_COLNAME"))
        If intTemp = 0 Then
            txtDescRowNum.Text = "1"
        Else
            txtDescRowNum.Text = CStr(intTemp)
        End If
        intTemp = CTableStructure.GetFieldInt(CmsPass, VLng("PAGE_RESID"), "CD_INT2", VStr("PAGE_COLNAME"))
        If intTemp = 0 Then
            rdoRow.Checked = True
            rdoCol.Checked = False
        Else
            rdoRow.Checked = False
            rdoCol.Checked = True
        End If

        Dim intSaveStyle As Integer = CTableStructure.GetFieldInt(CmsPass, VLng("PAGE_RESID"), "CD_STR3", VStr("PAGE_COLNAME"))
        If intSaveStyle = 0 Then
            rdoSaveOptionValue.Checked = True
            rdoSaveNumber.Checked = False
            rdoSaveABC.Checked = False
        ElseIf intSaveStyle = 1 Then
            rdoSaveOptionValue.Checked = False
            rdoSaveNumber.Checked = True
            rdoSaveABC.Checked = False
        ElseIf intSaveStyle = 2 Then
            rdoSaveOptionValue.Checked = False
            rdoSaveNumber.Checked = False
            rdoSaveABC.Checked = True
        End If

        Dim fldOpts As ArrayList = CTableColRadio.Get(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        Dim strFieldVal As String
        For Each strFieldVal In fldOpts
            If strFieldVal = "" Then
                lboxOptValues.Items.Add("") '"(��)"
            Else
                lboxOptValues.Items.Add(strFieldVal)
            End If
        Next
    End Sub
End Class

End Namespace
