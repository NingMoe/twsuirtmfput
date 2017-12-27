Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculationSchedule
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

        txtIntervalTime.Attributes.Add("style", "TEXT-ALIGN: right;")
        'txtOnceTime.Attributes.Add("style", "TEXT-ALIGN: right;")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '��ʼ������
        ddlTimeUnit.Items.Clear()
        ddlTimeUnit.Items.Add(New ListItem("����", "minute"))
        ddlTimeUnit.Items.Add(New ListItem("Сʱ", "hour"))

        '��ȡ����ƵĶ�ʱ������Ϣ
        Dim datFml As DataFormula = CTableColCalculation.GetFormulaByAiid(CmsPass, VLng("PAGE_AIID"))
        If datFml Is Nothing Then Return
        If datFml.datSchedule.Started = False Then
            'ͣ��״̬
            rdoStopSchedule.Checked = True
            rdoStartSchedule.Checked = False
        Else
            '����״̬
            rdoStartSchedule.Checked = True
            rdoStopSchedule.Checked = False
            If datFml.datSchedule.Type = FormulaScheduleType.IsTimer Then
                rdoOccurEvery.Checked = True
                txtIntervalTime.Text = CStr(datFml.datSchedule.TimerAmount)
                ddlTimeUnit.SelectedValue = datFml.datSchedule.TimerUnit
            ElseIf datFml.datSchedule.Type = FormulaScheduleType.IsFixTime Then
                rdoOccurOnce.Checked = True
                txtOnceTime.Text = datFml.datSchedule.FixTimeString
            End If

            If datFml.datSchedule.RunAtWebStart = True Then
                chkRunAtStart.Checked = True
            Else
                chkRunAtStart.Checked = False
            End If
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim datSchedule As New DataFmlSchedule
        datSchedule.Started = rdoStartSchedule.Checked
        If datSchedule.Started Then
            datSchedule.Type = CType(IIf(rdoOccurEvery.Checked, FormulaScheduleType.IsTimer, FormulaScheduleType.IsFixTime), FormulaScheduleType)
            If datSchedule.Type = FormulaScheduleType.IsTimer Then
                If IsNumeric(txtIntervalTime.Text.Trim()) = False Then
                    PromptMsg("��������Ч���������͵Ķ�ʱ���ʱ�䣡")
                    SetFocusOnTextbox("txtIntervalTime")
                    Return
                End If
                datSchedule.TimerAmount = CInt(txtIntervalTime.Text.Trim())
                datSchedule.TimerUnit = ddlTimeUnit.SelectedValue

                txtOnceTime.Text = ""
            ElseIf datSchedule.Type = FormulaScheduleType.IsFixTime Then
                If txtOnceTime.Text.Trim() = "" Then
                    PromptMsg("��������Ч�Ķ�ʱ����ʱ�䣡")
                    SetFocusOnTextbox("txtOnceTime")
                    Return
                End If
                datSchedule.FixTimeString = txtOnceTime.Text.Trim()

                txtIntervalTime.Text = ""
                ddlTimeUnit.SelectedValue = "minute"
            End If

            datSchedule.RunAtWebStart = chkRunAtStart.Checked
        End If
        CTableColCalculation.SaveFormulaSchedule(CmsPass, VLng("PAGE_AIID"), datSchedule)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
