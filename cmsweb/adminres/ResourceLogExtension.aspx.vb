Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceLogExtension
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        txtResName.ReadOnly = True

        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol1, True, True, True, datRes.LogColumn1) '��ʼ��DropDownList���ֶ��б���
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol2, True, True, True, datRes.LogColumn2) '��ʼ��DropDownList���ֶ��б���
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol3, True, True, True, datRes.LogColumn3) '��ʼ��DropDownList���ֶ��б���
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol4, True, True, True, datRes.LogColumn4) '��ʼ��DropDownList���ֶ��б���
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol5, True, True, True, datRes.LogColumn5) '��ʼ��DropDownList���ֶ��б���
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlCol6, True, True, True, datRes.LogColumn6) '��ʼ��DropDownList���ֶ��б���
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
        If CmsFunc.IsEnable("FUNC_LOG_EXTCOLUMN") = False Then
            btnConfirm.Enabled = False
            PromptMsg("��ǰϵͳ��֧����־��չ����")
        End If
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            ResFactory.ResService().SaveLogColumn(CmsPass, VLng("PAGE_RESID"), ddlCol1.SelectedValue, ddlCol2.SelectedValue, ddlCol3.SelectedValue, ddlCol4.SelectedValue, ddlCol5.SelectedValue, ddlCol6.SelectedValue)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            ddlCol1.SelectedValue = ""
            ddlCol2.SelectedValue = ""
            ddlCol3.SelectedValue = ""
            ddlCol4.SelectedValue = ""
            ddlCol5.SelectedValue = ""
            ddlCol6.SelectedValue = ""
            ResFactory.ResService().SaveLogColumn(CmsPass, VLng("PAGE_RESID"), ddlCol1.SelectedValue, ddlCol2.SelectedValue, ddlCol3.SelectedValue, ddlCol4.SelectedValue, ddlCol5.SelectedValue, ddlCol6.SelectedValue)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
