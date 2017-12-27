Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceRowColor
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
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields11)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields12)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields13)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields21)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields22)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields23)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields31)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields32)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlFields33)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition11, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition12, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition13, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition21, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition22, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition23, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition31, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition32, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlCondition33, True)
        LoadColors(ddlColor1)
        LoadColors(ddlColor2)
        LoadColors(ddlColor3)

        LoadOriginalSetting()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        '��1������
        Dim RowColor1 As String = ddlColor1.SelectedItem.Value.Trim()
        Dim RowColorWhere1 As String = ""
        If RowColor1 <> "" Then
            If ddlFields11.SelectedItem.Text.Trim() <> "" And ddlCondition11.SelectedItem.Text.Trim() <> "" Then
                '����ȷ�ĵ�һ������ɫ����
                RowColorWhere1 = ddlFields11.SelectedItem.Value & " {[" & ddlCondition11.SelectedItem.Value & "]} " & txtCondVal11.Text.Trim()
                If ddlFields12.SelectedItem.Text.Trim() <> "" And ddlCondition12.SelectedItem.Text.Trim() <> "" Then
                    '����ȷ�ĵڶ�������ɫ����
                    RowColorWhere1 &= " {{AND}} " & ddlFields12.SelectedItem.Value & " {[" & ddlCondition12.SelectedItem.Value & "]} " & txtCondVal12.Text.Trim()

                    If ddlFields13.SelectedItem.Text.Trim() <> "" And ddlCondition13.SelectedItem.Text.Trim() <> "" Then
                        '����ȷ�ĵ���������ɫ����
                        RowColorWhere1 &= " {{AND}} " & ddlFields13.SelectedItem.Value & " {[" & ddlCondition13.SelectedItem.Value & "]} " & txtCondVal13.Text.Trim()
                    End If
                End If
            End If
        End If

        '��2������
        Dim RowColor2 As String = ddlColor2.SelectedItem.Value.Trim()
        Dim RowColorWhere2 As String = ""
        If RowColor2 <> "" Then
            If ddlFields21.SelectedItem.Text.Trim() <> "" And ddlCondition21.SelectedItem.Text.Trim() <> "" Then
                '����ȷ�ĵ�һ������ɫ����
                RowColorWhere2 = ddlFields21.SelectedItem.Value & " {[" & ddlCondition21.SelectedItem.Value & "]} " & txtCondVal21.Text.Trim()
                If ddlFields22.SelectedItem.Text.Trim() <> "" And ddlCondition22.SelectedItem.Text.Trim() <> "" Then
                    '����ȷ�ĵڶ�������ɫ����
                    RowColorWhere2 &= " {{AND}} " & ddlFields22.SelectedItem.Value & " {[" & ddlCondition22.SelectedItem.Value & "]} " & txtCondVal22.Text.Trim()

                    If ddlFields23.SelectedItem.Text.Trim() <> "" And ddlCondition23.SelectedItem.Text.Trim() <> "" Then
                        '����ȷ�ĵ���������ɫ����
                        RowColorWhere2 &= " {{AND}} " & ddlFields23.SelectedItem.Value & " {[" & ddlCondition23.SelectedItem.Value & "]} " & txtCondVal23.Text.Trim()
                    End If
                End If
            End If
        End If

        '��3������
        Dim RowColor3 As String = ddlColor3.SelectedItem.Value.Trim()
        Dim RowColorWhere3 As String = ""
        If RowColor3 <> "" Then
            If ddlFields31.SelectedItem.Text.Trim() <> "" And ddlCondition31.SelectedItem.Text.Trim() <> "" Then
                '����ȷ�ĵ�һ������ɫ����
                RowColorWhere3 = ddlFields31.SelectedItem.Value & " {[" & ddlCondition31.SelectedItem.Value & "]} " & txtCondVal31.Text.Trim()
                If ddlFields32.SelectedItem.Text.Trim() <> "" And ddlCondition32.SelectedItem.Text.Trim() <> "" Then
                    '����ȷ�ĵڶ�������ɫ����
                    RowColorWhere3 &= " {{AND}} " & ddlFields32.SelectedItem.Value & " {[" & ddlCondition32.SelectedItem.Value & "]} " & txtCondVal32.Text.Trim()

                    If ddlFields33.SelectedItem.Text.Trim() <> "" And ddlCondition33.SelectedItem.Text.Trim() <> "" Then
                        '����ȷ�ĵ���������ɫ����
                        RowColorWhere3 &= " {{AND}} " & ddlFields33.SelectedItem.Value & " {[" & ddlCondition33.SelectedItem.Value & "]} " & txtCondVal33.Text.Trim()
                    End If
                End If
            End If
        End If

        ResFactory.ResService.SaveResRowColor(CmsPass, VLng("PAGE_RESID"), RowColor1, RowColorWhere1, RowColor2, RowColorWhere2, RowColor3, RowColorWhere3)
        LoadOriginalSetting()
    End Sub

    Private Sub btnClearSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSettings.Click
        ResFactory.ResService.SaveResRowColor(CmsPass, VLng("PAGE_RESID"), "", "")
        LoadOriginalSetting()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------
    '��ʼ��DropDownList���ֶ��б���
    '-----------------------------------------------------------
    Private Shared Sub LoadColors(ByVal ddlColor As DropDownList)
        ddlColor.Items.Clear()

        ddlColor.Items.Add(New ListItem("", ""))
        ddlColor.Items.Add(New ListItem("�Ƶ�ɫ", "yellow"))
        ddlColor.Items.Add(New ListItem("���ɫ", "#F69CA9"))
        ddlColor.Items.Add(New ListItem("�̵�ɫ", "#97F98A"))
        ddlColor.Items.Add(New ListItem("����ɫ", "#86DDE7"))
        ddlColor.Items.Add(New ListItem("�ҵ�ɫ", "#DCD8E0"))
        ddlColor.Items.Add(New ListItem("�׵�ɫ", "white"))
        'ddlColor.Items.Add(New ListItem("���ɫ", "red"))
        'ddlColor.Items.Add(New ListItem("���Ƶ�ɫ", "#FFFF80"))
        'ddlColor.Items.Add(New ListItem("����ɫ", "blue"))
        'ddlColor.Items.Add(New ListItem("�̵�ɫ", "green"))
        'ddlColor.Items.Add(New ListItem("���ҵ�ɫ", "Silver"))
        'ddlColor.Items.Add(New ListItem("ǳ�ϵ�ɫ", "lilac"))
    End Sub

    '-----------------------------------------------------------
    '��ʼ��DropDownList���ֶ��б���
    '-----------------------------------------------------------
    Private Sub LoadOriginalSetting()
        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))

        Try
            ddlColor1.SelectedValue = datRes.RowColor1
        Catch ex As Exception
        End Try
        Try
            ddlFields11.SelectedValue = datRes.RowColorCol1(0)
            ddlCondition11.SelectedValue = datRes.RowColorCond1(0)
            txtCondVal11.Text = datRes.RowColorCondVal1(0)
        Catch ex As Exception
        End Try
        Try
            ddlFields12.SelectedValue = datRes.RowColorCol1(1)
            ddlCondition12.SelectedValue = datRes.RowColorCond1(1)
            txtCondVal12.Text = datRes.RowColorCondVal1(1)
        Catch ex As Exception
        End Try
        Try
            ddlFields13.SelectedValue = datRes.RowColorCol1(2)
            ddlCondition13.SelectedValue = datRes.RowColorCond1(2)
            txtCondVal13.Text = datRes.RowColorCondVal1(2)
        Catch ex As Exception
        End Try

        Try
            ddlColor2.SelectedValue = datRes.RowColor2
        Catch ex As Exception
        End Try
        Try
            ddlFields21.SelectedValue = datRes.RowColorCol2(0)
            ddlCondition21.SelectedValue = datRes.RowColorCond2(0)
            txtCondVal21.Text = datRes.RowColorCondVal2(0)
        Catch ex As Exception
        End Try
        Try
            ddlFields22.SelectedValue = datRes.RowColorCol2(1)
            ddlCondition22.SelectedValue = datRes.RowColorCond2(1)
            txtCondVal22.Text = datRes.RowColorCondVal2(1)
        Catch ex As Exception
        End Try
        Try
            ddlFields33.SelectedValue = datRes.RowColorCol2(2)
            ddlCondition33.SelectedValue = datRes.RowColorCond2(2)
            txtCondVal33.Text = datRes.RowColorCondVal2(2)
        Catch ex As Exception
        End Try

        Try
            ddlColor3.SelectedValue = datRes.RowColor3
        Catch ex As Exception
        End Try
        Try
            ddlFields31.SelectedValue = datRes.RowColorCol3(0)
            ddlCondition31.SelectedValue = datRes.RowColorCond3(0)
            txtCondVal31.Text = datRes.RowColorCondVal3(0)
        Catch ex As Exception
        End Try
        Try
            ddlFields32.SelectedValue = datRes.RowColorCol3(1)
            ddlCondition32.SelectedValue = datRes.RowColorCond3(1)
            txtCondVal32.Text = datRes.RowColorCondVal3(1)
        Catch ex As Exception
        End Try
        Try
            ddlFields33.SelectedValue = datRes.RowColorCol3(2)
            ddlCondition33.SelectedValue = datRes.RowColorCond3(2)
            txtCondVal33.Text = datRes.RowColorCondVal3(2)
        Catch ex As Exception
        End Try
    End Sub
End Class

End Namespace
