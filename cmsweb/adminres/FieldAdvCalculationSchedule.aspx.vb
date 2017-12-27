Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldAdvCalculationSchedule
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
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
        '初始化界面
        ddlTimeUnit.Items.Clear()
        ddlTimeUnit.Items.Add(New ListItem("分钟", "minute"))
        ddlTimeUnit.Items.Add(New ListItem("小时", "hour"))

        '提取已设计的定时设置信息
        Dim datFml As DataFormula = CTableColCalculation.GetFormulaByAiid(CmsPass, VLng("PAGE_AIID"))
        If datFml Is Nothing Then Return
        If datFml.datSchedule.Started = False Then
            '停用状态
            rdoStopSchedule.Checked = True
            rdoStartSchedule.Checked = False
        Else
            '启用状态
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
                    PromptMsg("请输入有效的整数类型的定时间隔时间！")
                    SetFocusOnTextbox("txtIntervalTime")
                    Return
                End If
                datSchedule.TimerAmount = CInt(txtIntervalTime.Text.Trim())
                datSchedule.TimerUnit = ddlTimeUnit.SelectedValue

                txtOnceTime.Text = ""
            ElseIf datSchedule.Type = FormulaScheduleType.IsFixTime Then
                If txtOnceTime.Text.Trim() = "" Then
                    PromptMsg("请输入有效的定时运算时间！")
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
