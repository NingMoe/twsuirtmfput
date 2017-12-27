Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculationSettings
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
        If VLng("PAGE_AIID") = 0 Then
            ViewState("PAGE_AIID") = RLng("urlfmlaiid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        lblFieldName.Text = VStr("PAGE_COLDISPNAME")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '��ȡָ���ֶ���Դ���ֶεĹ�ʽ��Ϣ
        Dim datFml As DataFormula = CTableColCalculation.GetFormulaByAiid(CmsPass, VLng("PAGE_AIID"))
        'If datFml.intCDJ_CALTIME = 1 Then
        '    rdoRunBeforeSave.Checked = False
        '    rdoRunAfterSave.Checked = True
        'Else
        '    rdoRunBeforeSave.Checked = True
        '    rdoRunAfterSave.Checked = False
        'End If

        If (datFml.intCDJ_CALOCCASION And FormulaOccasion.RecordAdd) = FormulaOccasion.RecordAdd Then
            chkRunRecordAdd.Checked = True
        Else
            chkRunRecordAdd.Checked = False
        End If
        If (datFml.intCDJ_CALOCCASION And FormulaOccasion.RecordEdit) = FormulaOccasion.RecordEdit Then
            chkRunRecordEdit.Checked = True
        Else
            chkRunRecordEdit.Checked = False
        End If
        'If (datFml.intCDJ_CALOCCASION And FormulaOccasion.RecordDel) = FormulaOccasion.RecordDel Then
        '    chkRunRecordDel.Checked = True
        'Else
        '    chkRunRecordDel.Checked = False
        'End If
        If (datFml.intCDJ_CALOCCASION And FormulaOccasion.OuterResRecordAdd) = FormulaOccasion.OuterResRecordAdd Then
            chkRunRecordAdd2.Checked = True
        Else
            chkRunRecordAdd2.Checked = False
        End If
        If (datFml.intCDJ_CALOCCASION And FormulaOccasion.OuterResRecordEdit) = FormulaOccasion.OuterResRecordEdit Then
            chkRunRecordEdit2.Checked = True
        Else
            chkRunRecordEdit2.Checked = False
        End If
        If (datFml.intCDJ_CALOCCASION And FormulaOccasion.OuterResRecordDel) = FormulaOccasion.OuterResRecordDel Then
            chkRunRecordDel2.Checked = True
        Else
            chkRunRecordDel2.Checked = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim intCalOccasion As Integer = 0
            If chkRunRecordAdd.Checked Then intCalOccasion = intCalOccasion Or FormulaOccasion.RecordAdd
            If chkRunRecordEdit.Checked Then intCalOccasion = intCalOccasion Or FormulaOccasion.RecordEdit
            'If chkRunRecordDel.Checked Then intCalOccasion = intCalOccasion Or FormulaOccasion.RecordDel
            If chkRunRecordAdd2.Checked Then intCalOccasion = intCalOccasion Or FormulaOccasion.OuterResRecordAdd
            If chkRunRecordEdit2.Checked Then intCalOccasion = intCalOccasion Or FormulaOccasion.OuterResRecordEdit
            If chkRunRecordDel2.Checked Then intCalOccasion = intCalOccasion Or FormulaOccasion.OuterResRecordDel

            CTableColCalculation.SaveFormulaSettings(CmsPass, VLng("PAGE_AIID"), intCalOccasion) ', rdoRunAfterSave.Checked)

            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
