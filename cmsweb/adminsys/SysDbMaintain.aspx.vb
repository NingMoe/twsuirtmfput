Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SysDbMaintain
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
        lbtnCheckColumn.Attributes.Add("onClick", "return CmsPrmoptConfirm('��ǰ������Ҫ�ϳ�ʱ�䣬ȷ��Ҫִ����');")
        lbtnCheckDoc.Attributes.Add("onClick", "return CmsPrmoptConfirm('��ǰ������Ҫ�ϳ�ʱ�䣬ȷ��Ҫִ����');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub lbtnCheckColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCheckColumn.Click
        Try
            '��ɾ���ķ�һ�����ֶ�
            Dim strWhere As String = ""
            Dim blnError As Boolean = False

            Dim lngOneNum1 As Long = 0
            Dim lngOneNum2 As Long = 0
            Dim lngOneNum3 As Long = 0
            Dim lngOneNum4 As Long = 0
            Dim lngOneNum5 As Long = 0
            Dim lngOneNum6 As Long = 0
            Dim lngOneNum7 As Long = 0
            Dim lngOneNum8 As Long = 0
            Dim lngOneNum9 As Long = 0
            Dim lngOneNum10 As Long = 0
            Dim lngOneNum11 As Long = 0
            Dim lngOneNum12 As Long = 0

            '����������Դ
            Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), CmsTables.Resource, "")
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            For Each drv In dv
                Try
                    Dim lngResID As Long = DbField.GetLng(drv, "ID")
                    Dim lngIndepResPID As Long = CmsPass.GetDataRes(lngResID).IndepParentResID

                    'У����ʾ����
                    strWhere = "CS_RESID=" & lngResID & " AND (CS_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum1 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ColShow, strWhere)

                    '���Բ���ɾ����������е��κ���Ϣ����Ϊ�����г��ֶζ����⣬����������Label�ȵĶ���
                    ''У�鴰�����
                    'strWhere = "CI_RESID=" & lngResID & " AND CI_COLNAME<> '' AND (NOT (CI_COLNAME IS NULL)) AND (CI_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngDelNum += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ColInput, strWhere)

                    'У���Զ�����
                    strWhere = "CDC_RESID=" & lngResID & " AND (CDC_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum2 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldAutoCoding, strWhere)

                    'У����㹫ʽ������ɾ��У�鹫ʽ
                    strWhere = "CDJ_RESID=" & lngResID & " AND (CDJ_TYPE=0 OR CDJ_TYPE IS NULL) AND (CDJ_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum3 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldCalculation, strWhere)

                    'У�鶨�Ʊ���
                    strWhere = "CDD_RESID=" & lngResID & " AND (CDD_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum4 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldCustomizeCoding, strWhere)

                    'У��߼��ֵ�
                    strWhere = "CDZ2_RESID1=" & lngResID & " AND CDZ2_MAINCOL<> '' AND (NOT (CDZ2_MAINCOL IS NULL)) AND (CDZ2_MAINCOL NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum5 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldDictionary, strWhere)
                    strWhere = "CDZ2_RESID1=" & lngResID & " AND CDZ2_COL1<> '' AND (NOT (CDZ2_COL1 IS NULL)) AND (CDZ2_COL1 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum5 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldDictionary, strWhere)
                    strWhere = "CDZ2_RESID2=" & lngResID & " AND CDZ2_COL2<> '' AND (NOT (CDZ2_COL2 IS NULL)) AND (CDZ2_COL2 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum5 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldDictionary, strWhere)

                    'У��������ѡ����
                    strWhere = "CDX_RESID=" & lngResID & " AND (CDX_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum6 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldOption, strWhere)

                    'У���ѡһѡ����
                    strWhere = "CDR_RESID=" & lngResID & " AND (CDR_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum7 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.FieldRadio, strWhere)

                    'У�������
                    strWhere = "RT_TAB1_RESID=" & lngResID & " AND RT_TAB1_COLNAME<> '' AND (NOT (RT_TAB1_COLNAME IS NULL)) AND (RT_TAB1_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum8 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RelatedTable, strWhere)
                    strWhere = "RT_TAB2_RESID=" & lngResID & " AND RT_TAB2_COLNAME<> '' AND (NOT (RT_TAB2_COLNAME IS NULL)) AND (RT_TAB2_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum8 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RelatedTable, strWhere)

                    'У���ֵ�ģ��
                    'strWhere = "DICTT_RESID=" & lngResID & " AND DICTT_COLNAME<> '' AND (NOT (DICTT_COLNAME IS NULL)) AND (DICTT_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngOneNum9 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.DictTemplate, strWhere)

                    'У������ģ��
                    'strWhere = "DTMPL_HOSTRESID=" & lngResID & " AND DTMPL_COL1<> '' AND (NOT (DTMPL_COL1 IS NULL)) AND (DTMPL_COL1 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngOneNum10 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RecTemplate, strWhere)
                    'strWhere = "DTMPL_DATARESID=" & lngResID & " AND DTMPL_COL2<> '' AND (NOT (DTMPL_COL2 IS NULL)) AND (DTMPL_COL2 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    'lngOneNum10 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.RecTemplate, strWhere)

                    'У���Ӧ�ñ�
                    strWhere = "MTS_RESID=" & lngResID & " AND MTS_COLNAME<> '' AND (NOT (MTS_COLNAME IS NULL)) AND (MTS_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum11 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.MTableHost, strWhere)
                    strWhere = "MTSCOL_RESID=" & lngResID & " AND MTSCOL_COLNAME<> '' AND (NOT (MTSCOL_COLNAME IS NULL)) AND (MTSCOL_COLNAME NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum11 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.MTableColDef, strWhere)

                    'У������ͬ��
                    strWhere = "SYNCOL_HOSTRESID=" & lngResID & " AND SYNCOL_COLNAME1<> '' AND (NOT (SYNCOL_COLNAME1 IS NULL)) AND (SYNCOL_COLNAME1 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum12 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ResSyncCol, strWhere)
                    strWhere = "SYNCOL_SYNCRESID=" & lngResID & " AND SYNCOL_COLNAME2<> '' AND (NOT (SYNCOL_COLNAME2 IS NULL)) AND (SYNCOL_COLNAME2 NOT IN (SELECT CD_COLNAME FROM " & CmsTables.ColDefine & " WHERE CD_RESID=" & lngIndepResPID & "))"
                    lngOneNum12 += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsTables.ResSyncCol, strWhere)
                Catch ex As Exception
                    blnError = True
                    SLog.Err("ɾ����һ�����ֶζ����쳣ʧ�ܣ�������������", ex)
                End Try
            Next
            CmsConfig.ReloadAll()

            Dim strNumMsg As String = ""
            strNumMsg &= " ��ʾ����(" & lngOneNum1 & ") "
            strNumMsg &= " �Զ�����(" & lngOneNum2 & ") "
            strNumMsg &= " ���㹫ʽ(" & lngOneNum3 & ") "
            strNumMsg &= " ���Ʊ���(" & lngOneNum4 & ") "
            strNumMsg &= " �߼��ֵ�(" & lngOneNum5 & ") "
            strNumMsg &= " ������ѡ����(" & lngOneNum6 & ") "
            strNumMsg &= " ��ѡһѡ����(" & lngOneNum7 & ") "
            strNumMsg &= " ������(" & lngOneNum8 & ") "
            strNumMsg &= " �ֵ�ģ��(" & lngOneNum9 & ") "
            strNumMsg &= " ����ģ��(" & lngOneNum10 & ") "
            strNumMsg &= " ��Ӧ�ñ�(" & lngOneNum11 & ") "
            strNumMsg &= " ����ͬ��(" & lngOneNum12 & ") "
            Dim lngDelNum As Long = lngOneNum1 + lngOneNum2 + lngOneNum3 + lngOneNum4 + lngOneNum5 + lngOneNum6 + lngOneNum7 + lngOneNum8 + lngOneNum9 + lngOneNum10 + lngOneNum11 + lngOneNum12

            If blnError = True Then
                PromptMsg("У�鲿��ʧ�ܣ�ɾ��" & lngDelNum & "����һ�����ֶζ��塣��ϸ��Ϣ��" & strNumMsg)
            Else
                PromptMsg("У����ɣ�ɾ��" & lngDelNum & "����һ�����ֶζ��塣��ϸ��Ϣ��" & strNumMsg)
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnCheckDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCheckDoc.Click
        Dim strSql As String = "SELECT DOC2_ID, DOC2_RESID1, DOC2_RESID2, DOC2_RESID3, DOC2_RESID4, DOC2_RESID5 FROM " & CmsDatabase.DocDatabaseLink
        Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        Dim lngDelNum As Long = 0
        For Each drv In dv
            Try
                Dim strWhere As String = ""
                Dim lngResID1 As Long = DbField.GetLng(drv, "DOC2_RESID1")
                If lngResID1 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID1)
                Dim lngResID2 As Long = DbField.GetLng(drv, "DOC2_RESID2")
                If lngResID2 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID2)
                Dim lngResID3 As Long = DbField.GetLng(drv, "DOC2_RESID3")
                If lngResID3 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID3)
                Dim lngResID4 As Long = DbField.GetLng(drv, "DOC2_RESID4")
                If lngResID4 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID4)
                Dim lngResID5 As Long = DbField.GetLng(drv, "DOC2_RESID5")
                If lngResID5 > 0 Then strWhere = CmsWhere.AppendOr(strWhere, "ID=" & lngResID5)
                Dim lngNum As Long = CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), CmsTables.Resource, strWhere)
                If lngNum <= 0 Then
                    Dim lngDocID As Long = DbField.GetLng(drv, "DOC2_ID")
                    lngDelNum += CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsDatabase.DocDatabaseLink, "DOC2_ID=" & lngDocID)
                End If
            Catch ex As Exception
                SLog.Err("ɾ����������Դ���ĵ�ʱ�쳣����������������", ex)
            End Try
        Next

        PromptMsg("�ĵ�У����ɣ�ɾ��" & lngDelNum & "���ĵ�")
    End Sub
End Class

End Namespace
