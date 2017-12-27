Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmailSmsConfig
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
        ddlRelHostRes.AutoPostBack = True
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        LoadHostAndSelfResources(VLng("PAGE_RESID"))
        LoadOriginalData(VLng("PAGE_RESID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If IsNumeric(ddlRelHostRes.SelectedValue) = False Then
                PromptMsg("��ѡ����Դ�����������")
                Return
            End If

            If ddlEmail.SelectedValue = "" And ddlHandphone.SelectedValue = "" Then
                PromptMsg("������ѡ������ʼ����ֻ�����֮һ��")
                Return
            End If

            Dim lngResID1 As Long = 0
            If IsNumeric(ddlRelHostRes.SelectedValue) Then lngResID1 = CLng(ddlRelHostRes.SelectedValue)

            ResFactory.ResService.SetResourceCommProp(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).ResID, lngResID1, ddlEmail.SelectedValue, ddlHandphone.SelectedValue, ddlRefColumn.SelectedValue)

            PromptMsg("�����ʼ��ֻ�������Ϣ�ɹ���")
        Catch ex As Exception
            PromptMsg("������ԴͨѶ������Ϣʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnClearSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSetting.Click
        Try
            ResFactory.ResService.SetResourceCommProp(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).ResID, 0, "", "", "")

            ddlRelHostRes.SelectedValue = ""
            LoadResColumns1()
        Catch ex As Exception
            PromptMsg("��������쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub ddlRelHostRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRelHostRes.SelectedIndexChanged
        LoadResColumns1()
    End Sub

    '----------------------------------------------------------
    '��ʾ��Դ��������й�������
    '----------------------------------------------------------
    Private Sub LoadHostAndSelfResources(ByVal lngResID As Long)
        If ddlRelHostRes.Items.Count <= 0 Then
            '��ӿ���Ŀ
            ddlRelHostRes.Items.Add(New ListItem("", ""))

            '��ʾ��Դ����
            ddlRelHostRes.Items.Add(New ListItem(CmsPass.GetDataRes(lngResID).ResName & "(����Դ)", CStr(CmsPass.GetDataRes(lngResID).ResID)))

            '��ʾ���й�������
            Dim hashRelHostRes As Hashtable = CmsTableRelation.GetHostRelatedResources(CmsPass, CmsPass.GetDataRes(lngResID).ResID)
            Dim en As IDictionaryEnumerator = hashRelHostRes.GetEnumerator()
            Do While en.MoveNext
                Dim datRes As DataResource = CType(en.Value, DataResource)
                ddlRelHostRes.Items.Add(New ListItem(datRes.ResName, CStr(datRes.ResID)))
            Loop
        End If
    End Sub

    '----------------------------------------------------------
    '��ʾѡ�й�������������ֶ�
    '----------------------------------------------------------
    Private Sub LoadResColumns1()
        If IsNumeric(ddlRelHostRes.SelectedValue) Then
            Dim lngResID As Long = CLng(ddlRelHostRes.SelectedValue)
            If lngResID <> 0 Then
                WebUtilities.LoadResColumnsInDdlist(CmsPass, lngResID, ddlEmail, True, True, True)
                WebUtilities.LoadResColumnsInDdlist(CmsPass, lngResID, ddlHandphone, True, True, True)
                WebUtilities.LoadResColumnsInDdlist(CmsPass, lngResID, ddlRefColumn, True, True, True)
            End If
        Else
            ddlEmail.Items.Clear()
            ddlHandphone.Items.Clear()
            ddlRefColumn.Items.Clear()
        End If
    End Sub

    '----------------------------------------------------------
    '��ʾѡ�й�������������ֶ�
    '----------------------------------------------------------
    Private Sub LoadOriginalData(ByVal lngResID As Long)
        If CmsPass.GetDataRes(lngResID).CommResID <> 0 Then
            '��ȡ��ԴID
            ddlRelHostRes.SelectedValue = CStr(CmsPass.GetDataRes(lngResID).CommResID)
            LoadResColumns1()

            If CmsPass.GetDataRes(lngResID).CommColEmail <> "" Then ddlEmail.SelectedValue = CmsPass.GetDataRes(lngResID).CommColEmail
            If CmsPass.GetDataRes(lngResID).CommColHandphone <> "" Then ddlHandphone.SelectedValue = CmsPass.GetDataRes(lngResID).CommColHandphone
            If CmsPass.GetDataRes(lngResID).CommColRef <> "" Then ddlRefColumn.SelectedValue = CmsPass.GetDataRes(lngResID).CommColRef
        End If
    End Sub
End Class

End Namespace
