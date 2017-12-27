Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FormSetProperty
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

        If VLng("PAGE_FORMRECID") = 0 Then
            ViewState("PAGE_FORMRECID") = RLng("urlformrecid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        'btnExit.Attributes.Add("onClick", "return window.close();")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        txtResName.ReadOnly = True

        If VLng("PAGE_FORMRECID") = 0 Then
            PromptMsg("设置窗体属性前请先保存窗体设计！")
            btnConfirm.Enabled = False
        End If

        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCondCol, , , , , True, True)

        '提取数据库中原有的值
        chkRunFormula.Checked = CBool(IIf(CmsDbBase.GetFieldLng(CmsPass, CmsTables.ColInputList, "CILST_NOFORMULA", "CILST_ID=" & VLng("PAGE_FORMRECID")) = 0, True, False))
        chkKeepPrevRecord.Checked = CBool(IIf(CmsDbBase.GetFieldLng(CmsPass, CmsTables.ColInputList, "CILST_KEEP_OLDVAL", "CILST_ID=" & VLng("PAGE_FORMRECID")) = 0, False, True))
        txtReplaceSrc.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.ColInputList, "CILST_REPLACE_SRC", "CILST_ID=" & VLng("PAGE_FORMRECID"))
        txtReplaceDest.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.ColInputList, "CILST_REPLACE_DEST", "CILST_ID=" & VLng("PAGE_FORMRECID"))
        Dim strCondCol As String = CmsDbBase.GetFieldStr(CmsPass, CmsTables.ColInputList, "CILST_CONDCOL", "CILST_ID=" & VLng("PAGE_FORMRECID"))
        If strCondCol <> "" Then
            'ddlCondCol.SelectedItem.Value = strCondCol
            ddlCondCol.SelectedValue = strCondCol
            txtCondVal.Text = CmsDbBase.GetFieldStr(CmsPass, CmsTables.ColInputList, "CILST_CONDVAL", "CILST_ID=" & VLng("PAGE_FORMRECID"))
        End If
        chkNoCond.Checked = CBool(IIf(CmsDbBase.GetFieldInt(CmsPass, CmsTables.ColInputList, "CILST_NOCOND", "CILST_ID=" & VLng("PAGE_FORMRECID")) = 1, True, False))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            '保存窗体属性信息
            Dim hashFieldVal As New Hashtable
            hashFieldVal.Add("CILST_NOFORMULA", CLng(IIf(chkRunFormula.Checked = True, 0, 1)))
            hashFieldVal.Add("CILST_KEEP_OLDVAL", CLng(IIf(chkKeepPrevRecord.Checked = True, 1, 0)))
            hashFieldVal.Add("CILST_REPLACE_SRC", txtReplaceSrc.Text)
            hashFieldVal.Add("CILST_REPLACE_DEST", txtReplaceDest.Text)
            If ddlCondCol.SelectedValue <> "" Then
                hashFieldVal.Add("CILST_CONDCOL", ddlCondCol.SelectedValue)
                hashFieldVal.Add("CILST_CONDSIGN", "=") '暂时仅支持 =
                hashFieldVal.Add("CILST_CONDVAL", txtCondVal.Text.Trim())
            End If
            hashFieldVal.Add("CILST_NOCOND", CInt(IIf(chkNoCond.Checked = True, 1, 0)))
            CmsDbStatement.UpdateRows(SDbConnectionPool.GetDbConfig(), hashFieldVal, CmsTables.ColInputList, "CILST_ID=" & VLng("PAGE_FORMRECID"))
            CmsDbBase.ClearTableCache(CmsTables.ColInputList)

            '关闭窗体
            If Not IsStartupScriptRegistered("CmsCloseForm") Then
                Dim strScript As String = "<script language=""javascript"">" & Environment.NewLine
                strScript &= "    window.close();" & Environment.NewLine
                strScript &= "</script>" & Environment.NewLine
                RegisterStartupScript("CmsCloseForm", strScript)
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub
End Class

End Namespace
