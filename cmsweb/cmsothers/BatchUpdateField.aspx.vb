Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class BatchUpdateField
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

    Public datRes As DataResource
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        datRes = CmsPass.GetDataRes(VLng("PAGE_RESID"))

        lblResName.Text = datRes.ResName
    End Sub

    '--------------------------------------------------------------------------
    '������Ĳ�������ΪViewState������������ҳ������ȡ���޸ġ�
    '--------------------------------------------------------------------------
    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    '--------------------------------------------------------------------------
    '��ʼ��ҳ��
    '--------------------------------------------------------------------------
    Protected Overrides Sub CmsPageInitialize()
        btnConfirm.Attributes.Add("onClick", "return ConfirmUpdate('ȷ��Ҫ����������Դ�ֶ���', '����������Դ�ֶ�ֵ�Ĳ����ǲ��ɻָ��ģ�ȷ��Ҫִ����');")
    End Sub

    '--------------------------------------------------------------------------
    '�����һ��GET�����е�����
    '--------------------------------------------------------------------------
    Protected Overrides Sub CmsPageDealFirstRequest()

        ViewState("BATCOL_COLTYPE1") = WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlColumns1, True, True, True)
        ViewState("BATCOL_COLTYPE2") = WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlColumns2, True, True, True)
        ViewState("BATCOL_COLTYPE3") = WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlColumns3, True, True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_RESID"), ddlDestColumn, True, True, True)
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions1, True)   '��ʼ��DropDownList�в���������
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions2, True)   '��ʼ��DropDownList�в���������
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions3, True)   '��ʼ��DropDownList�в���������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strColName As String = ddlDestColumn.SelectedValue
            Dim strColValue As String = txtColValue.Text
            If strColName.Trim() = "" Then
                PromptMsg("��ѡ������µ��ֶΣ�")
                Return
            End If

            Dim strWhere As String '= GetRowWhere(VLng("PAGE_RESID"))
            If Me.CheckBox1.Checked Then
                strWhere = " id in(" & RStr("selectedrecid").Trim(CChar(",")) & ")"
            Else
                strWhere = GetRowWhere(VLng("PAGE_RESID"))
            End If
            Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
            ResFactory.TableService(datRes.ResTableType).BatchUpdateFieldWithoutFormula(CmsPass, VLng("PAGE_RESID"), strColName, strColValue, strWhere)

            PromptMsg("���������ֶ�ֵ�ɹ���")
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------
    '��ȡ��ǰѡȡ������
    '-----------------------------------------------------------
    Private Function GetRowWhere(ByVal lngResID As Long) As String
        'Where1
        Dim strColName1 As String = ddlColumns1.SelectedValue
        Dim strCondition1 As String = ddlConditions1.SelectedValue
        Dim hashColType1 As Hashtable = CType(ViewState("BATCOL_COLTYPE1"), Hashtable)
        Dim lngColType1 As Long = CLng(hashColType1(strColName1))
        Dim strWhere1 As String = CTableStructure.GenerateFieldWhere(strColName1, txtSearchValue1.Text.Trim(), lngColType1, strCondition1, SDbConnectionPool.GetDbConfig().DatabaseType)
        If strWhere1.Trim() = "11=22" Then strWhere1 = ""

        'Where2
        Dim strColName2 As String = ddlColumns2.SelectedValue
        Dim strCondition2 As String = ddlConditions2.SelectedValue
        Dim hashColType2 As Hashtable = CType(ViewState("BATCOL_COLTYPE2"), Hashtable)
        Dim lngColType2 As Long = CLng(hashColType2(strColName2))
        Dim strWhere2 As String = CTableStructure.GenerateFieldWhere(strColName2, txtSearchValue2.Text.Trim(), lngColType2, strCondition2, SDbConnectionPool.GetDbConfig().DatabaseType)
        If strWhere2.Trim() = "11=22" Then strWhere2 = ""

        'Where3
        Dim strColName3 As String = ddlColumns3.SelectedValue
        Dim strCondition3 As String = ddlConditions3.SelectedValue
        Dim hashColType3 As Hashtable = CType(ViewState("BATCOL_COLTYPE3"), Hashtable)
        Dim lngColType3 As Long = CLng(hashColType3(strColName3))
        Dim strWhere3 As String = CTableStructure.GenerateFieldWhere(strColName3, txtSearchValue3.Text.Trim(), lngColType3, strCondition3, SDbConnectionPool.GetDbConfig().DatabaseType)
        If strWhere3.Trim() = "11=22" Then strWhere3 = ""

        Dim strWhere As String = ""
        If strWhere1 <> "" Then strWhere = strWhere1
        If strWhere2 <> "" Then
            If strWhere = "" Then
                strWhere = strWhere2
            Else
                strWhere &= " AND " & strWhere2
            End If
        End If
        If strWhere3 <> "" Then
            If strWhere = "" Then
                strWhere = strWhere3
            Else
                strWhere &= " AND " & strWhere3
            End If
        End If

        '�����ԴID����
        If strWhere.Trim = "" Then
            strWhere = " RESID=" & lngResID & " "
        Else
            strWhere &= " AND RESID=" & lngResID & " "
        End If

        If strWhere.Trim() <> "" Then strWhere = "(" & strWhere & ")"

        Return strWhere
    End Function
End Class

End Namespace
