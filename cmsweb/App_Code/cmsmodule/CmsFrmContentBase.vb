'---------------------------------------------------------------
'��Ϊϵͳ������ĸ��࣬��Ҫ�ṩһЩ����ģ��Ľӿڣ�ͬʱ�����������
'---------------------------------------------------------------

Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls
Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class CmsFrmContentBase
    Inherits CmsPage

    Private Const BACK_PAGE As String = "/cmsweb/cmshost/CmsFrmContent.aspx"
        Private RecordFormUrl As String = "RecordForm.aspx"
    '------------------------------------------------------------------------------
    '������Դ���ݹ�������ϵ����в˵���Ӧ
    '���أ�True:�ǲ˵������Ѵ��� False�����ǲ˵�����
    '------------------------------------------------------------------------------
    Protected Function DealMenuAction( _
            ByVal strAction As String, _
            ByRef dgridHostTable As DataGrid, _
            ByRef dgridSubTable As DataGrid, _
            ByRef ctrlPager1 As Cms.Web.CmsPager, _
            ByRef ctrlPager2 As Cms.Web.CmsPager, _
            ByVal strColumns As String, _
            ByVal strConditions As String, _
            ByRef txtSearchValue As System.Web.UI.WebControls.TextBox, _
            ByVal strColumnsSub As String, _
            ByVal strConditionsSub As String, _
            ByRef txtSearchValueSub As System.Web.UI.WebControls.TextBox, _
            ByRef panelHostTableHeader As Panel, _
            ByRef panelSubTable As Panel, _
            ByRef Panel1 As Panel, _
            ByRef Panel2 As Panel _
        ) As Boolean
        Try
            Dim lngResLocate As ResourceLocation = CType(RLng("MNURESLOCATE"), ResourceLocation)
            Select Case strAction
                Case "MenuFlowController"
                    Dim strFlowMsg As String = ""
                    Dim blnRefreshTableData As Boolean = False
                    CmsFrmContentFlow.FlowEntry(CmsPass, strAction, "", strFlowMsg, blnRefreshTableData, Request, Session, Response, Server)
                    If blnRefreshTableData Then
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '������������
                        PromptMsg(strFlowMsg)
                    End If

                Case "MenuFlowControllerByEditForm"
                    Dim strFlowMsg As String = ""
                    Dim blnRefreshTableData As Boolean = False
                    CmsFrmContentFlow.FlowEntry(CmsPass, strAction, "", strFlowMsg, blnRefreshTableData, Request, Session, Response, Server)
                    If blnRefreshTableData Then
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '������������
                        PromptMsg(strFlowMsg)
                    End If

                Case "MenuFlowEditResRecords"
                    Dim strFlowMsg As String = ""
                    Dim blnRefreshTableData As Boolean = False
                    CmsFrmContentFlow.FlowEntry(CmsPass, strAction, "", strFlowMsg, blnRefreshTableData, Request, Session, Response, Server)
                    If blnRefreshTableData Then
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '������������
                        PromptMsg(strFlowMsg)
                    End If

                Case "MenuRecordAdd"
                    If lngResLocate = ResourceLocation.HostTable Then
                        RecordAddInHostTable(CmsPass)
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        RecordAddInRelTable(CmsPass)
                    End If

                Case "MenuRecordEdit"
                    If lngResLocate = ResourceLocation.HostTable Then
                        RecordEdit(CmsPass, CmsPass.HostResData.ResID, CmsPass.HostResData.ResID, InputMode.EditInHostTable) ', intCurPageNO)
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        GetHostRecIDForRelTableAction(True)
                        RecordEditInRelTable(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, InputMode.EditInRelTable) ', intCurPageNO1, intCurPageNO2)
                    End If

                Case "MenuRecordView"
                    If lngResLocate = ResourceLocation.HostTable Then
                        RecordView(CmsPass, CmsPass.HostResData.ResID, CmsPass.HostResData.ResID, InputMode.ViewInHostTable) ', intCurPageNO)
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        GetHostRecIDForRelTableAction(True)
                        RecordView(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, InputMode.ViewInRelTable) ', intCurPageNO1, intCurPageNO2)
                    End If

                Case "MenuRecordDelete"
                    If lngResLocate = ResourceLocation.HostTable Then

                        RecordDelete(CmsPass, RLng("mnuresid"), RStr("mnuformname"))
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                        Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        RecordDeleteInRelTable(CmsPass, RLng("mnuresid"), RStr("mnuformname"))
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                        GetHostRecIDForRelTableAction(True)
                        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    End If

                    '-------------------------------------------------------------------------------------------------------------------------------------------
                    '2007-08-20
                    '-------------------------------------------------------------------------------------------------------------------------------------------
                Case "BatchMenuRecordDelete" '����ɾ��
                    If lngResLocate = ResourceLocation.HostTable Then

                        BatchDelete(CmsPass, RLng("mnuresid"), RStr("mnuformname"))
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                        'Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        RecordDeleteInRelTable(CmsPass, RLng("mnuresid"), RStr("mnuformname"))
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                        GetHostRecIDForRelTableAction(True)
                        'Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    End If

                Case "MenuDocCheckout"
                    Dim bln As Boolean = DocCheckout(CmsPass, RLng("mnuresid"))
                    If bln = False Then ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '������������

                Case "MenuDocCheckin"
                    Dim bln As Boolean = DocCheckin(CmsPass, RLng("mnuresid"))
                    If bln = False Then ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '������������

                Case "MenuCancelDocCheckout"
                    Dim bln As Boolean = DocCheckoutCancel(CmsPass, RLng("mnuresid"))
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '������������

                Case "MenuDocView"
                        ' DocView(CmsPass, RLng("mnuresid"))

                    '����������͹�����Ĳ�ѯ����
                Case "MenuSearch"
                    'If lngResLocate = ResLocation.HostTable Then
                    '    Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID

                    '    '��֯Where���
                    '    Dim hashColType As Hashtable = CType(ViewState("CMS_HOSTSEARCH_COLTYPE"), Hashtable)
                    '    Dim lngColType As Long = CLng(hashColType(strColumns))
                    '    Session("CMS_HOSTTABLE_WHERE") = CTableStructure.GenerateFieldWhere(strColumns, txtSearchValue.Text.Trim(), lngColType, strConditions, SDbConnectionPool.GetDbConfig().DatabaseType)

                    '    ShowHostTableData(True)
                    '    If SStr("CMS_HOSTTABLE_WHERE") = "11=22" Then PromptMsg("����������Ч����������������������")
                    'ElseIf lngResLocate = ResLocation.RelTable Then
                    '    Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

                    '    '��֯Where���
                    '    Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    '    Dim hashColType As Hashtable = CType(ViewState("CMS_SUBSEARCH_COLTYPE"), Hashtable)
                    '    Dim lngColType As Long = CLng(hashColType(strColumnsSub))
                    '    Session("CMS_SUBTABLE_WHERE") = CTableStructure.GenerateFieldWhere(strColumnsSub, txtSearchValueSub.Text.Trim(), lngColType, strConditionsSub, SDbConnectionPool.GetDbConfig().DatabaseType)

                    '    '��ȡ������Ĺ�����������������������ʾ������
                    '    'Dim strRelWhere As String = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, lngHostRecID)
                    '    'Session("CMS_SUBTABLE_WHERE") = CmsWhere.AppendAnd(strWhere, strRelWhere)

                    '    ShowHostTableData(True)
                    '    If SStr("CMS_SUBTABLE_WHERE") = "11=22" Then PromptMsg("����������Ч����������������������")
                    'End If

                Case "MenuSearchAgain"
                    'If lngResLocate = ResLocation.HostTable Then
                    '    Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID

                    '    '��֯Where���
                    '    Dim hashColType As Hashtable = CType(ViewState("CMS_HOSTSEARCH_COLTYPE"), Hashtable)
                    '    Dim lngColType As Long = CLng(hashColType(strColumns))
                    '    Dim strWhere As String = CTableStructure.GenerateFieldWhere(strColumns, txtSearchValue.Text.Trim(), lngColType, strConditions, SDbConnectionPool.GetDbConfig().DatabaseType)
                    '    Session("CMS_HOSTTABLE_WHERE") = CmsWhere.AppendAnd(strWhere, SStr("CMS_HOSTTABLE_WHERE"))

                    '    ShowHostTableData(True) '��ʾ������
                    '    If strWhere = "11=22" Then PromptMsg("����������Ч����������������������")
                    'ElseIf lngResLocate = ResLocation.RelTable Then
                    '    Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

                    '    '��֯Where���
                    '    Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    '    Dim hashColType As Hashtable = CType(ViewState("CMS_SUBSEARCH_COLTYPE"), Hashtable)
                    '    Dim lngColType As Long = CLng(hashColType(strColumnsSub))
                    '    Dim strWhere As String = CTableStructure.GenerateFieldWhere(strColumnsSub, txtSearchValueSub.Text.Trim(), lngColType, strConditionsSub, SDbConnectionPool.GetDbConfig().DatabaseType)
                    '    Session("CMS_SUBTABLE_WHERE") = CmsWhere.AppendAnd(strWhere, SStr("CMS_SUBTABLE_WHERE"))

                    '    ShowHostTableData(True) '��ʾ������
                    '    If strWhere = "11=22" Then PromptMsg("����������Ч����������������������")
                    'End If

                Case "MenuSearchFullTable" 'ȫ���ѯ
                    'If lngResLocate = ResLocation.HostTable Then
                    '    Session("CMS_HOSTTABLE_RECID") = "" '������յ�ǰѡ�м�¼ID
                    '    Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
                    '    Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
                    '    Session("CMS_HOSTTABLE_ORDERBY") = "" '�����������
                    '    '��֯Where���
                    '    Session("CMS_HOSTTABLE_WHERE") = CTableStructure.GenerateFullTableFieldWhere(CmsPass, lngResLocate, CmsPass.HostResData.ResID, txtSearchValue.Text.Trim(), strConditions, SDbConnectionPool.GetDbConfig().DatabaseType)

                    '    ShowHostTableData(True)    '��ʾ������
                    '    If SStr("CMS_HOSTTABLE_WHERE") = "11=22" Then PromptMsg("����������Ч����������������������")
                    'ElseIf lngResLocate = ResLocation.RelTable Then
                    '    Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

                    '    Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    '    Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                    '    Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

                    '    '��֯Where���
                    '    Session("CMS_SUBTABLE_WHERE") = CTableStructure.GenerateFullTableFieldWhere(CmsPass, lngResLocate, CmsPass.RelResData.ResID, txtSearchValueSub.Text.Trim(), strConditionsSub, SDbConnectionPool.GetDbConfig().DatabaseType)

                    '    ShowHostTableData(True)    '��ʾ������
                    '    If SStr("CMS_SUBTABLE_WHERE") = "11=22" Then PromptMsg("����������Ч����������������������")
                    'End If

                Case "MenuRecordRefresh"
                    'If lngResLocate = ResLocation.HostTable Then
                    '    Session("CMS_HOSTTABLE_RECID") = "" '������յ�ǰѡ�м�¼ID
                    '    Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
                    '    Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
                    '    Session("CMS_HOSTTABLE_ORDERBY") = "" '�����������
                    'ElseIf lngResLocate = ResLocation.RelTable Then
                    'End If

                    ''���������ǹ�����ˢ�£���Ӧ��ˢ�¹�����Session����
                    'Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    'Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                    'Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2, False) '������������

                Case "MenuSearchFullText"
                    If lngResLocate = ResourceLocation.HostTable Then
                        Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                        Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
                        Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
                        Session("CMS_HOSTTABLE_ORDERBY") = "" '���뽫���������ÿ�
                        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                        Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                        Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

                        Dim strHostRecIDs As String = FilterRecIDs(SStr("CMS_HOSTTABLE_RECID"))
                        ShowFulltextSearchData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2, RLng("mnuresid"), lngResLocate, strHostRecIDs, txtSearchValue.Text.Trim())
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)

                        Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                        Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                        Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

                        '��ȡ����������Ĺ�����������������������ʾ������

                        ShowFulltextSearchData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2, RLng("mnuresid"), lngResLocate, CStr(lngHostRecID), txtSearchValueSub.Text.Trim())
                    End If
                    '------------------------------------------------------------

                    '------------------------------------------------------------
                    '������ͳ�ƹ���
                Case "MenuStatisticSum"
                    If lngResLocate = ResourceLocation.HostTable Then
                        Dim strColName As String = strColumns
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")
                        txtSearchValue.Text = CStr(StatColumnSum(CmsPass, CmsPass.HostResData.ResID, strColName, SStr("CMS_HOSTTABLE_WHERE")))
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        Dim strColName As String = strColumnsSub
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")

                        '��ȡ������Ĺ�����������������������ʾ������
                        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
                        Dim strRelWhere As String = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, lngHostRecID)
                        Dim strWhere As String = CmsWhere.AppendAnd(SStr("CMS_SUBTABLE_WHERE"), strRelWhere)
                        txtSearchValueSub.Text = CStr(StatColumnSum(CmsPass, CmsPass.RelResData.ResID, strColName, strWhere))
                    End If
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)  '��ʾ������

                Case "MenuStatisticAvg"
                    If lngResLocate = ResourceLocation.HostTable Then
                        Dim strColName As String = strColumns
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")
                        txtSearchValue.Text = CStr(StatColumnAvg(CmsPass, CmsPass.HostResData.ResID, strColName, SStr("CMS_HOSTTABLE_WHERE")))
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        Dim strColName As String = strColumnsSub
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")

                        '��ȡ������Ĺ�����������������������ʾ������
                        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
                        Dim strRelWhere As String = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, lngHostRecID)
                        Dim strWhere As String = CmsWhere.AppendAnd(SStr("CMS_SUBTABLE_WHERE"), strRelWhere)
                        txtSearchValueSub.Text = CStr(StatColumnAvg(CmsPass, CmsPass.RelResData.ResID, strColName, strWhere))
                    End If
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)  '��ʾ������

                Case "MenuStatisticMax"
                    If lngResLocate = ResourceLocation.HostTable Then
                        Dim strColName As String = strColumns
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")
                        txtSearchValue.Text = CStr(StatColumnMax(CmsPass, CmsPass.HostResData.ResID, strColName, SStr("CMS_HOSTTABLE_WHERE")))
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        Dim strColName As String = strColumnsSub
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")

                        '��ȡ������Ĺ�����������������������ʾ������
                        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
                        Dim strRelWhere As String = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, lngHostRecID)
                        Dim strWhere As String = CmsWhere.AppendAnd(SStr("CMS_SUBTABLE_WHERE"), strRelWhere)
                        txtSearchValueSub.Text = CStr(StatColumnMax(CmsPass, CmsPass.RelResData.ResID, strColName, strWhere))
                    End If
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)  '��ʾ������

                Case "MenuStatisticMin"
                    If lngResLocate = ResourceLocation.HostTable Then
                        Dim strColName As String = strColumns
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")
                        txtSearchValue.Text = CStr(StatColumnMin(CmsPass, CmsPass.HostResData.ResID, strColName, SStr("CMS_HOSTTABLE_WHERE")))
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        Dim strColName As String = strColumnsSub
                        If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")

                        '��ȡ������Ĺ�����������������������ʾ������
                        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
                        Dim strRelWhere As String = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, lngHostRecID)
                        Dim strWhere As String = CmsWhere.AppendAnd(SStr("CMS_SUBTABLE_WHERE"), strRelWhere)
                        txtSearchValueSub.Text = CStr(StatColumnMin(CmsPass, CmsPass.RelResData.ResID, strColName, strWhere))
                    End If
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)  '��ʾ������

                Case "MenuStatisticUnique" '��ʱ��֧���ӱ�˵�����Ϊ�Ƚϸ���
                    Dim strColName As String = strColumns
                    If strColName = "" Then Throw New CmsException("��ѡ����Ч�����ֶΣ�")
                    Dim datRes As DataResource = CmsPass.GetDataRes(RLng("mnuresid"))
                    Dim strWhere As String = CmsWhere.AppendAnd("RESID=" & datRes.ResID, SStr("CMS_HOSTTABLE_WHERE"))
                    Dim strSql As String = "SELECT DISTINCT " & strColName & " FROM " & datRes.ResTable & " WHERE " & strWhere
                    Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
                    txtSearchValue.Text = CStr(ds.Tables(0).DefaultView.Count)
                    ds.Clear()
                    ds = Nothing
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)  '��ʾ������

                Case "MenuListSameColumnValue" '�г�ָ���ֶ�����ֵͬ�ļ�¼�б�
                    If lngResLocate = ResourceLocation.HostTable Then
                        Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                        Dim strRecIDs As String = GetRecIDsOfSameColValue(CmsPass, RLng("mnuresid"), strColumns, VStr("CMS_HOSTTABLE_WHERE"))
                        If strRecIDs = "" Then
                            Session("CMS_HOSTTABLE_WHERE") = "11=22" '���ظ���¼
                        Else
                            Session("CMS_HOSTTABLE_WHERE") = "ID IN (" & strRecIDs & ")"
                        End If
                        Session("CMS_HOSTTABLE_ORDERBY") = strColumns & " ASC" '��������Ϊ��ǰ�ֶε����򣬷���������ظ���¼����Ϣ�����ײ鿴
                        ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2, True)
                    ElseIf lngResLocate = ResourceLocation.RelTable Then
                        PromptMsg("�����ӱ��в�֧�ִ˹��ܡ�")
                    End If

                Case "MenuUnmatchColInSubTable"
                    Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    Dim strWhere As String = GetWhereOfUnmatchColInSubTable(CmsPass, RLng("mnuresid"), CmsPass.RelResData.ResID, strColumns)
                    strWhere = CmsWhere.AppendAnd(strWhere, SStr("CMS_HOSTTABLE_WHERE"))
                    Session("CMS_HOSTTABLE_WHERE") = strWhere
                    Session("CMS_HOSTTABLE_MORETABLES") = CmsPass.RelResData.ResTable
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2, True)

                Case "MenuRecordCopy"
                    RecordCopy(CmsPass, RLng("mnuresid"))
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)
                Case "MenuRecordCut"
                    RecordCut(CmsPass, RLng("mnuresid"))
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                Case "MenuRecordPaste"
                    RecordPaste(CmsPass, RLng("mnuresid"))
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                Case "RefreshTableList"
                    '�ڲ˵��в�֪�����ֹ������ֻ�ܷ���Fresh������ˢ�±��б�
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                Case "hostrowclick"
                    Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
                    Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)   '�����������ر�����

                Case "tabclick"
                    '����������
                    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2) '�����������ر�����

                    'Case "MenuAllotShouKuan" '�տ����
                    '    Dim strHostRecIDs As String = FilterRecIDs(SStr("CMS_HOSTTABLE_RECID"))
                    '    Dim strSubRecIDs As String = FilterRecIDs(RStr("RECID2"))
                    '    Dim strPromptMsg As String = BizAllot.DealShouFuKuan(CmsPass, RLng("mnuresid"), lngResLocate, strHostRecIDs, strSubRecIDs, AllotType.ShouKuan)
                    '    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)
                    '    If strPromptMsg <> "" Then PromptMsg(strPromptMsg)

                    'Case "MenuAllotFuKuan" '�������
                    '    Dim strHostRecIDs As String = FilterRecIDs(SStr("CMS_HOSTTABLE_RECID"))
                    '    Dim strSubRecIDs As String = FilterRecIDs(RStr("RECID2"))
                    '    Dim strPromptMsg As String = BizAllot.DealShouFuKuan(CmsPass, RLng("mnuresid"), lngResLocate, strHostRecIDs, strSubRecIDs, AllotType.FuKuan)
                    '    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)
                    '    If strPromptMsg <> "" Then PromptMsg(strPromptMsg)

                    'Case "MenuRecordCopyDataTemplate"
                    '    RecordTemplate.CopyDataTemplate(CmsPass, RLng("mnuresid"), RStr("mnurecid"))
                    '    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                    'Case "MenuRecordDeleteDataTemplate"
                    '    RecordTemplate.DeleteDataTemplate(CmsPass, RLng("mnuresid"), RStr("mnurecid"))
                    '    ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)

                Case Else
                    '��Ŀ�ͻ����ƴ�����¼����
                    If strAction.StartsWith("CLIENT_") Then
                        Dim strPromptMsg As String = ""
                        Dim strHostRecIDs As String = FilterRecIDs(SStr("CMS_HOSTTABLE_RECID"))
                        Dim strSubRecIDs As String = FilterRecIDs(RStr("RECID2"))
                        Dim iMenu As ICmsMenu = MenuFactory.Service()
                        If Not iMenu Is Nothing Then
                            strPromptMsg = iMenu.EventEntry(CmsPass, strAction, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, strHostRecIDs, strSubRecIDs, strColumns, strConditions, txtSearchValue.Text.Trim(), strColumnsSub, strConditionsSub, txtSearchValueSub.Text.Trim())
                            ShowHostTableData(dgridHostTable, dgridSubTable, ctrlPager1, ctrlPager2)
                            If strPromptMsg <> "" Then
                                PromptMsg(strPromptMsg)
                            End If
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
            End Select

            Return True
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Function

    '---------------------------------------------------------------
    '��Ӽ�¼
    '---------------------------------------------------------------
    Protected Sub RecordAddInHostTable(ByRef pst As CmsPassport)
        Session("CMSBP_RecordEdit") = BACK_PAGE
            Response.Redirect("/cmsweb/cmshost/" + RecordFormUrl + "?mnuresid=" & RLng("mnuresid") & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & InputMode.AddInHostTable & "&MenuSection=" & RStr("MenuSection") & "&MenuKey=" & RStr("MenuKey"), False)
    End Sub

    '---------------------------------------------------------------
    '��Ӽ�¼
    '---------------------------------------------------------------
    Protected Sub RecordAddInRelTable(ByRef pst As CmsPassport)
        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
        If lngHostRecID = 0 Then Throw New CmsException("����ӱ�/��ر��¼ǰ����ѡ����Ч�������¼��")

        'Dim blnIsLockAdd As Boolean = DbLockRelation.IsLockAdd(pst, pst.HostResData.ResID, pst.RelResData.ResID)
        'If blnIsLockAdd = True Then '��Ӽ�¼�й�����������
        '    Dim blnHostRecLocked As Boolean = ResFactory.TableService(pst.HostResData.ResTableType).IsRecordLocked(pst, pst.HostResData.ResID, lngHostRecID)
        '    If blnHostRecLocked Then
        '        Throw New CmsException("�����¼�Ѿ��������������ӱ�����Ӽ�¼��")
        '    End If
        'End If
            'Dim ResID As String = pst.RelResData.ResID.ToString
            'If pst.RelResData.ResIconName = "ICON_RES_VIEW" Then
            '    ResID = pst.RelResData.IndepParentResID.ToString
            'End If
        Session("CMSBP_RecordEdit") = BACK_PAGE
            Response.Redirect("/cmsweb/cmshost/" + RecordFormUrl + "?mnuhostresid=" & pst.HostResData.ResID & "&mnuhostrecid=" & lngHostRecID & "&mnuresid=" & pst.RelResData.ResID & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & InputMode.AddInRelTable & "&MenuSection=" & RStr("MenuSection") & "&MenuKey=" & RStr("MenuKey"), False)
    End Sub

    '----------------------------------------------------------
    '�༭��¼
    '----------------------------------------------------------
    Protected Sub RecordEdit(ByRef pst As CmsPassport, ByVal lngHostResID As Long, ByVal lngCurResID As Long, ByVal lngMode As InputMode) ', Optional ByVal intCurPageNO1 As Integer = 0, Optional ByVal intCurPageNO2 As Integer = 0)
        Session("CMSBP_RecordEdit") = BACK_PAGE 'CmsUrl.AppendParam(BACK_PAGE, "pageno=" & intCurPageNO1 & "&pageno2=" & intCurPageNO2)
            Response.Redirect("/cmsweb/cmshost/" + RecordFormUrl + "?mnuhostresid=" & lngHostResID & "&mnuhostrecid=&mnuresid=" & lngCurResID & "&mnurecid=" & CLng(GetSelectedRecID(True, False)) & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & lngMode & "&MenuSection=" & RStr("MenuSection") & "&MenuKey=" & RStr("MenuKey"), False)
    End Sub

    '----------------------------------------------------------
    '���ӱ��б༭��¼
    '----------------------------------------------------------
    Protected Sub RecordEditInRelTable(ByRef pst As CmsPassport, ByVal lngHostResID As Long, ByVal lngCurResID As Long, ByVal lngMode As InputMode) ', Optional ByVal intCurPageNO1 As Integer = 0, Optional ByVal intCurPageNO2 As Integer = 0)
        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
        If lngHostRecID = 0 Then Throw New CmsException("����ѡ����Ч�������¼��")

        'Dim blnIsLockEdit As Boolean = DbLockRelation.IsLockEdit(pst, pst.HostResData.ResID, pst.RelResData.ResID)
        'If blnIsLockEdit = True Then '��Ӽ�¼�й�����������
        '    Dim blnHostRecLocked As Boolean = ResFactory.TableService(pst.HostResData.ResTableType).IsRecordLocked(pst, pst.HostResData.ResID, lngHostRecID)
        '    If blnHostRecLocked Then
        '        Throw New CmsException("�����¼�Ѿ��������������ӱ����޸ļ�¼��")
        '    End If
        'End If

        Session("CMSBP_RecordEdit") = BACK_PAGE 'CmsUrl.AppendParam(BACK_PAGE, "pageno=" & intCurPageNO1 & "&pageno2=" & intCurPageNO2)
            Response.Redirect("/cmsweb/cmshost/" + RecordFormUrl + "?mnuhostresid=" & lngHostResID & "&mnuhostrecid=&mnuresid=" & lngCurResID & "&mnurecid=" & CLng(GetSelectedRecID(True, False)) & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & lngMode & "&MenuSection=" & RStr("MenuSection") & "&MenuKey=" & RStr("MenuKey"), False)
    End Sub

    '----------------------------------------------------------
    '���ļ�¼
    '----------------------------------------------------------
    Protected Sub RecordView(ByRef pst As CmsPassport, ByVal lngHostResID As Long, ByVal lngCurResID As Long, ByVal lngMode As InputMode) ', Optional ByVal intCurPageNO1 As Integer = 0, Optional ByVal intCurPageNO2 As Integer = 0)
        Session("CMSBP_RecordEdit") = BACK_PAGE 'CmsUrl.AppendParam(BACK_PAGE, "pageno=" & intCurPageNO1 & "&pageno2=" & intCurPageNO2)
            Response.Redirect("/cmsweb/cmshost/" + RecordFormUrl + "?mnuhostresid=" & lngHostResID & "&mnuhostrecid=&mnuresid=" & lngCurResID & "&mnurecid=" & CLng(GetSelectedRecID(True, False)) & "&mnuformname=" & Server.UrlEncode(RStr("mnuformname")) & "&mnuinmode=" & lngMode, False)
    End Sub

    '----------------------------------------------------------
    'ɾ����¼
    '----------------------------------------------------------
    Protected Sub RecordDelete(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strFormName As String)
        Dim strRecIDs As String = GetSelectedRecID(True, True)
        ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).DelRecords(pst, ResourceId, strRecIDs, strFormName)
    End Sub
    '-------------------------------------------------------------------------------------------------------------------------------------------
    '2007-08-20
    '-------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub BatchDelete(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strFormName As String) '����ɾ����¼
        Dim strBatchRecIDs As String = GetSelectedBacthRecID(True, True)
        ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).DelRecords(pst, ResourceId, strBatchRecIDs, strFormName)
    End Sub

    '----------------------------------------------------------
    '���ӱ���ɾ����¼
    '----------------------------------------------------------
    Protected Sub RecordDeleteInRelTable(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strFormName As String)
        Dim lngHostRecID As Long = GetHostRecIDForRelTableAction(True)
        If lngHostRecID = 0 Then Throw New CmsException("����ѡ����Ч�������¼��")

        'Dim blnIsLockDel As Boolean = DbLockRelation.IsLockDel(pst, pst.HostResData.ResID, pst.RelResData.ResID)
        'If blnIsLockDel = True Then '��Ӽ�¼�й�����������
        '    Dim blnHostRecLocked As Boolean = ResFactory.TableService(pst.HostResData.ResTableType).IsRecordLocked(pst, pst.HostResData.ResID, lngHostRecID)
        '    If blnHostRecLocked Then
        '        Throw New CmsException("�����¼�Ѿ��������������ӱ���ɾ����¼��")
        '    End If
        'End If

        Dim strRecIDs As String = GetSelectedRecID(True, True)
        ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).DelRecords(pst, ResourceId, strRecIDs, strFormName)
    End Sub

    '----------------------------------------------------------
    '���Ƽ�¼
    '----------------------------------------------------------
    Protected Sub RecordCopy(ByRef pst As CmsPassport, ByVal ResourceId As Long)
        Dim strRecID As String = GetSelectedRecID(True, True)
        Dim lngRecID As Long = 0
        Try
            If strRecID.IndexOf(",") > -1 Then
                strRecID = strRecID.Substring(0, strRecID.IndexOf(","))
            End If
            lngRecID = CLng(strRecID)
        Catch ex As Exception
            Throw New Exception("δѡ�м�¼��")
        End Try

        ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).CopyRecord(pst, ResourceId, lngRecID)
    End Sub
    '----------------------------------------------------------
    '���м�¼
    '----------------------------------------------------------
    Protected Sub RecordCut(ByRef pst As CmsPassport, ByVal ResourceId As Long)
        Dim strRecIDs As String = GetSelectedRecID(True, True)
        CmsClipBoard.Cut(pst, ResourceId, strRecIDs)
    End Sub

    '----------------------------------------------------------
    'ճ����¼
    '----------------------------------------------------------
    Protected Sub RecordPaste(ByRef pst As CmsPassport, ByVal ResourceId As Long)
        CmsClipBoard.Paste(pst, ResourceId)
    End Sub

    '----------------------------------------------------------
    '��������ĵ�
    '----------------------------------------------------------
        'Protected Sub DocView(ByRef pst As CmsPassport, ByVal ResourceId As Long)
        '    Try
        '        Dim lngRecID As Long = CLng(GetSelectedRecID(True, False))
        '        Dim datDoc As DataDocument = ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).GetDocument(pst, ResourceId, lngRecID, True)
        '        FileTransfer.ShowDoc(Response, datDoc)
        '    Catch ex As Exception
        '        '�߳�������ֹ�������κβ���
        '        'CmsError.ThrowEx("����ĵ�ʧ��", ex, True)
        '    End Try
        'End Sub

    '----------------------------------------------------------
    'ǩ���ļ�
    '----------------------------------------------------------
    Protected Function DocCheckout(ByVal pst As CmsPassport, ByVal ResourceId As Long) As Boolean
        Try
            Dim lngRecID As Long = CLng(GetSelectedRecID(True, False))

            '-------------------------------------------------------------------
            '�����Ƿ���ǩ��Ȩ��
            If CmsRights.HasRightsDocCheckoutCancel(pst, ResourceId) = False Then
                Throw New Exception("��ǰ�û�û��ȡ��ǩ��״̬��Ȩ�ޡ�")
            End If

            'ȡ��ǩ��״̬
            ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).Checkout(pst, ResourceId, lngRecID)

            Return True
        Catch ex As Exception
            SLog.Err("ǩ���ļ������쳣.", ex)
            Return False
        End Try
    End Function

    '----------------------------------------------------------
    'ǩ���ĵ�
    '----------------------------------------------------------
    Protected Function DocCheckin(ByRef pst As CmsPassport, ByVal ResourceId As Long) As Boolean
        Try
            Dim lngRecID As Long = CLng(GetSelectedRecID(True, False))

            '-------------------------------------------------------------------
            '�����Ƿ���ǩ��״̬
            Dim strTable As String = pst.GetDataRes(ResourceId).ResTable
            Dim strSql As String = "SELECT " & CmsDatabase.DocDatabaseLink & ".* FROM " & strTable & ", " & CmsDatabase.DocDatabaseLink & " WHERE ID=" & lngRecID & " AND (DOC2_STATUS='" & DocVersion.StatusCheckout & "' OR DOC2_SIZE=0) AND " & CmsDatabase.DocDatabaseLink & ".DOC2_ID = " & strTable & ".DOCID"
            Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)

            ds = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
            Dim dv As DataView = ds.Tables(0).DefaultView
            If dv.Count = 0 Then
                Throw New CmsException("ǩ��ʧ�ܣ�û�д���ǩ��״̬��ָ���ĵ���")
            End If

            'ֻ����ǩ����ǩ���ĵ�
            If DbField.GetStr(dv(0), "DOC2_EDTID") <> pst.Employee.ID Then
                Throw New CmsException("ǩ��ʧ�ܣ�ֻ��ǩ���߲ſ���ǩ���ĵ���")
            End If
            '-------------------------------------------------------------------

            Session("CMSBP_DocCheckin") = BACK_PAGE
            Response.Redirect("/cmsweb/cmsdocument/DocCheckin.aspx?mnuresid=" & ResourceId & "&mnurecid=" & lngRecID, False)
            Return True
        Catch ex As Exception
            PromptMsg(ex.Message)
            Return False
        End Try
    End Function

    '----------------------------------------------------------
    'ȡ��ǩ��״̬
    '----------------------------------------------------------
    Protected Function DocCheckoutCancel(ByRef pst As CmsPassport, ByVal ResourceId As Long) As Boolean
        Try
            Dim lngRecID As Long = CLng(GetSelectedRecID(True, False))

            '-------------------------------------------------------------------
            '�����Ƿ���ǩ��Ȩ��
            If CmsRights.HasRightsDocCheckoutCancel(pst, ResourceId) = False Then
                Throw New CmsException("��ǰ�û�û��ȡ��ǩ��״̬��Ȩ�ޡ�")
            End If

            'ȡ��ǩ��״̬
            ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).CheckoutCancel(pst, ResourceId, lngRecID)

            Return True
        Catch ex As Exception
            PromptMsg(ex.Message)
            Return False
        End Try
    End Function

    '----------------------------------------------------------
    '��ȡָ���ֶεĺϼ�ֵ
    '----------------------------------------------------------
    Private Function StatColumnSum(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strColName As String, ByVal strWhere As String) As Double
        Return ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).Sum(pst, ResourceId, strColName, strWhere)
    End Function

    '----------------------------------------------------------
    '��ȡָ���ֶε�ƽ��ֵ
    '----------------------------------------------------------
    Private Function StatColumnAvg(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strColName As String, ByVal strWhere As String) As Double
        Return ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).Avg(pst, ResourceId, strColName, strWhere)
    End Function

    '----------------------------------------------------------
    '��ȡָ���ֶε����ֵ
    '----------------------------------------------------------
    Private Function StatColumnMax(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strColName As String, ByVal strWhere As String) As Object
        Return ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).Max(pst, ResourceId, strColName, strWhere)
    End Function

    '----------------------------------------------------------
    '��ȡָ���ֶε���Сֵ
    '----------------------------------------------------------
    Private Function StatColumnMin(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strColName As String, ByVal strWhere As String) As Object
        Return ResFactory.TableService(pst.GetDataRes(ResourceId).ResTableType).Min(pst, ResourceId, strColName, strWhere)
    End Function

    '----------------------------------------------------------
    '��ȡ��ǰ��ѡ�еļ�¼ID
    '----------------------------------------------------------
    Public Shared Function GetHostResIDInSession(ByRef Session As System.Web.SessionState.HttpSessionState, ByRef Response As System.Web.HttpResponse) As Long
        Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"), CmsPassport)
        If pst Is Nothing Then
            '����û���δ��¼����ת����¼ҳ�� 
            Response.Redirect("/cmsweb/Logout.aspx", True)
        Else
            Return pst.HostResData.ResID
        End If
    End Function

    '----------------------------------------------------------
    '��ȡ��ǰ��ѡ�еļ�¼ID
    '----------------------------------------------------------
    Public Shared Function GetHostRecIDInSession(ByRef Request As System.Web.HttpRequest, ByRef Session As System.Web.SessionState.HttpSessionState, ByRef Response As System.Web.HttpResponse) As String
        Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"), CmsPassport)
        If pst Is Nothing Then
            '����û���δ��¼����ת����¼ҳ�� 
            Response.Redirect("/cmsweb/Logout.aspx", True)
        Else
            Return FilterRecIDs(SStr("CMS_HOSTTABLE_RECID", Session))
        End If
    End Function

    '----------------------------------------------------------
    '��ȡ��ǰ��ѡ�еļ�¼ID
    '----------------------------------------------------------
    Private Function GetSelectedRecID(ByVal blnMustSelectRec As Boolean, ByVal blnAllowMultiSel As Boolean) As String
        'mnurecid�л�ȡ�ļ�¼���ܻ��ظ�����ΪJavascript��д������
        Dim strRecIDs As String = FilterRecIDs(RStr("mnurecid"))
        Dim alistTemp As ArrayList = StringDeal.Split(strRecIDs, ",")
        strRecIDs = StringDeal.Merge(alistTemp, ",")

        If strRecIDs = "" And blnMustSelectRec Then
            Throw New CmsException("����ѡ����Ч�ļ�¼��")
        End If
        If blnAllowMultiSel = False And strRecIDs.IndexOf(",") >= 0 Then
            Throw New CmsException("��ǰ������֧�ֶ�ѡ��¼����ѡ��һ����¼��")
        End If

        Return strRecIDs
    End Function
    '-------------------------------------------------------------------------------------------------------------------------------------------
    '2007-08-20
    '-------------------------------------------------------------------------------------------------------------------------------------------
    Private Function GetSelectedBacthRecID(ByVal blnMustSelectRec As Boolean, ByVal blnAllowMultiSel As Boolean) As String '��ȡ��¼ID
        Dim al As ArrayList = CType(Session("hosttable"), ArrayList)
        Dim strRecID As String = ""
        For i As Integer = 0 To al.Count - 1
            strRecID &= Convert.ToString(al(i)) & ","
        Next
        'Dim alistTemp As ArrayList = StringDeal.Split(strRecID, ",")
        'strRecID = StringDeal.Merge(alistTemp, ",")
        strRecID = strRecID.Trim(CChar(","))
        Return strRecID
    End Function

    '----------------------------------------------------------
    '��ȡ��ǰ��ѡ�еļ�¼ID
    '----------------------------------------------------------
    Protected Function GetHostRecIDForRelTableAction(ByVal blnMustSelectRec As Boolean) As Long
        Dim strRecIDs As String = FilterRecIDs(RStr("RECID"))
        If strRecIDs = "" Then
            strRecIDs = FilterRecIDs(SStr("CMS_HOSTTABLE_RECID"))
        Else
            Dim strTemp As String = FilterRecIDs(SStr("CMS_HOSTTABLE_RECID"))
            If strTemp <> "" Then strRecIDs &= "," & strTemp
        End If
        If strRecIDs.IndexOf(",") >= 0 Then Throw New CmsException("���й��������ʱ�����������ֻ����һ����¼��ѡ�У�")
        If IsNumeric(strRecIDs) = False And blnMustSelectRec Then
            Throw New CmsException("����ѡ����Ч�������¼��")
        End If
        Return CLng(strRecIDs)
    End Function

    '---------------------------------------------------------------
    '���˵�ǰѡ�еļ�¼ID�б�
    '---------------------------------------------------------------
    Private Shared Function FilterRecIDs(ByVal strRecIDs As String) As String
        strRecIDs = strRecIDs.Trim()
        If strRecIDs = "" Then Return ""

        strRecIDs = strRecIDs.Replace(";", ",")
        strRecIDs = strRecIDs.Replace(",,", ",")
        strRecIDs = StringDeal.Trim(strRecIDs, ",", ",")
        Return strRecIDs
    End Function

    '------------------------------------------------------------------------
    '������ԴID�����Ƿ���ʾ�����ӱ�
    '------------------------------------------------------------------------
    Protected Sub PrepareUILayout(ByRef dgridSubTable As DataGrid, ByRef panelHostTableHeader As Panel, ByRef panelSubTable As Panel, ByRef Panel1 As Panel, ByRef Panel2 As Panel, ByRef panelPager1 As Panel, ByRef panelPager2 As Panel)
        Dim lngCurClickedDepID As Long = AspPage.RLng("depid", Request)
        If lngCurClickedDepID > 0 Then
            panelHostTableHeader.Visible = False
            panelSubTable.Visible = False
            Panel1.Visible = False
            Panel2.Visible = False
            If Not panelPager1 Is Nothing Then panelPager1.Visible = False
            If Not panelPager2 Is Nothing Then panelPager2.Visible = False
        ElseIf CmsPass.HostResData.ResID = 0 OrElse CmsPass.HostResData.ResTable = "" Then
            panelHostTableHeader.Visible = False
            panelSubTable.Visible = False
            Panel1.Visible = False
            Panel2.Visible = False
            If Not panelPager1 Is Nothing Then panelPager1.Visible = False
            If Not panelPager2 Is Nothing Then panelPager2.Visible = False
        ElseIf CmsRights.HasRightsRecView(CmsPass, CmsPass.HostResData.ResID) = False Then
            panelHostTableHeader.Visible = False
            panelSubTable.Visible = False
            Panel1.Visible = False
            Panel2.Visible = False
            If Not panelPager1 Is Nothing Then panelPager1.Visible = False
            If Not panelPager2 Is Nothing Then panelPager2.Visible = False
        Else
            Panel1.Visible = True
            If Not panelPager1 Is Nothing Then panelPager1.Visible = True
            panelHostTableHeader.Visible = True
            If CmsPass.RelResList.Count > 0 Then
                panelSubTable.Visible = True
                Panel2.Visible = True
                If Not panelPager2 Is Nothing Then panelPager2.Visible = True
                dgridSubTable.Visible = True
            Else
                panelSubTable.Visible = False
                Panel2.Visible = False
                If Not panelPager2 Is Nothing Then panelPager2.Visible = False
                dgridSubTable.Visible = False
            End If
        End If
    End Sub

    '----------------------------------------------------------------------------------
    '��Ϊ�˸�����/��������ӹ�����
    '----------------------------------------------------------------------------------
    Protected Sub InitControlsSize(ByRef pst As CmsPassport, ByRef panelHostTableHeader As Panel, ByRef panelSubTable As Panel, ByRef Panel1 As System.Web.UI.WebControls.Panel, ByRef Panel2 As System.Web.UI.WebControls.Panel)
            'If Not Panel1 Is Nothing Then Panel1.Width = Unit.Pixel(pst.WindowHostWidth) 'Unit.Pixel(intWidth)
        'If Not Panel1 Is Nothing Then Panel1.Height = Unit.Pixel(pst.WindowHostHeight)
        'If Not Panel2 Is Nothing Then Panel2.Width = Unit.Pixel(pst.WindowRelWidth) 'Unit.Pixel(intWidth)
        'If Not Panel2 Is Nothing Then Panel2.Height = Unit.Pixel(pst.WindowRelHeight)
        'If Not panelHostTableHeader Is Nothing Then panelHostTableHeader.Width = Unit.Pixel(pst.WindowHostWidth)
        'If Not panelSubTable Is Nothing Then panelSubTable.Width = Unit.Pixel(pst.WindowHostWidth)
    End Sub

    '----------------------------------------------------------------------------------
    '�г�ָ���ֶ�����ֵͬ�ļ�¼�б�
    '----------------------------------------------------------------------------------
    Private Function GetRecIDsOfSameColValue(ByRef pst As CmsPassport, ByVal ResourceId As Long, ByVal strFieldName As String, ByVal strWhere As String) As String
        If strFieldName = "" Then Throw New CmsException("��ָ����Ч���ֶΡ�")

        Dim strbRecIDs As New StringBuilder(2048)

        Dim strOrderby As String = " ORDER BY " & strFieldName & " ASC"
        Dim strSql As String = "SELECT ID, " & strFieldName & " FROM " & pst.GetDataRes(ResourceId).ResTable
        If strWhere <> "" Then strSql &= " WHERE " & strWhere
        strSql &= " " & strOrderby
        Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        Dim strLastColVal As String = ""
        Dim strLastRecID As String = ""
        For Each drv In dv
            Dim strColVal As String = DbField.GetStr(drv, strFieldName)
            If strColVal = strLastColVal Then
                '��ǰһ���ֶ���ֵͬ�Ķ���¼����
                strbRecIDs.Append(DbField.GetStr(drv, "ID") & ",")
                If strLastRecID <> "" Then strbRecIDs.Append(strLastRecID & ",")
            End If
            strLastRecID = DbField.GetStr(drv, "ID")
            strLastColVal = strColVal
        Next

        Dim strRtn As String = strbRecIDs.ToString()
        Return StringDeal.Trim(strRtn, "", ",")
    End Function

    '----------------------------------------------------------------------------------
    '��ȡ������ָ�������ֶε�ֵ���ӱ���������ֶ�ֵ��ƥ��������¼
    '----------------------------------------------------------------------------------
    Private Function GetWhereOfUnmatchColInSubTable(ByRef pst As CmsPassport, ByVal lngHostResID As Long, ByVal lngSubResID As Long, ByVal strHostFieldName As String) As String
        If lngSubResID = 0 Then Throw New CmsException("��ǰ����(" & pst.GetDataRes(lngHostResID).ResName & ")û�й����ӱ��޷�������")
        If strHostFieldName = "" Then Throw New CmsException("��ѡ����Ч�ĵ�ǰ����(" & pst.GetDataRes(lngHostResID).ResName & ")���ֶΡ�")

        Dim strWhere As String = ""
        Dim blnFoundRelTableCol As Boolean = False
        Dim alistRelColumns As ArrayList = CmsTableRelation.GetRelatedColumns(pst, lngHostResID, lngSubResID, False, True, True, False, False)
        Dim datRelCols As DataRelatedColumn
        For Each datRelCols In alistRelColumns
            If datRelCols.intRelType = TabRelationType.MainRelationCol Then
                strWhere = CmsWhere.AppendAnd(strWhere, datRelCols.strHostColumn & "=" & datRelCols.strSubColumn)
            ElseIf datRelCols.intRelType = TabRelationType.InputRelationCol AndAlso strHostFieldName = datRelCols.strHostColumn Then
                blnFoundRelTableCol = True
                strWhere = CmsWhere.AppendAnd(strWhere, datRelCols.strHostColumn & "<>" & datRelCols.strSubColumn)
            End If
        Next

        If blnFoundRelTableCol = False Then Throw New CmsException("��ǰ����(" & pst.GetDataRes(lngHostResID).ResName & ")��ѡ���ֶ�û�ж�Ӧ���ӱ�(" & pst.GetDataRes(lngSubResID).ResName & ")����������ֶΡ�")
        Return strWhere
    End Function

    '----------------------------------------------------------
    '��DataGrid����
    '----------------------------------------------------------
    Public Sub ShowHostTableData(ByRef dgridHostTable As DataGrid, ByRef dgridSubTable As DataGrid, ByRef ctrlPager1 As Cms.Web.CmsPager, ByRef ctrlPager2 As Cms.Web.CmsPager, Optional ByVal blnResetToFirstPage As Boolean = False, Optional ByVal strHostPageCommand As String = "MoveCurrentPage", Optional ByVal strRelPageCommand As String = "MoveCurrentPage", Optional ByVal strOuterWhere As String = "")
        If CmsPass.HostResData.ResID = 0 Or CmsPass.HostResData.ResTable = "" Then Return 'û�к������ԴID
        If CmsRights.HasRightsRecView(CmsPass, CmsPass.HostResData.ResID) = False Then Return '�����������Ȩ��

        Try
            '--------------------------------------------------------------------------------------------
            '������ǰ�ķ�ҳ�ؼ��Ĵ���
            If blnResetToFirstPage = True Then strHostPageCommand = "MoveFirstPage"
            ctrlPager1.DealPageCommandBeforeBind(strHostPageCommand)   '����ҳ��
            dgridHostTable.CurrentPageIndex = 0
            Session("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID) = ctrlPager1.CurrentPage
            '--------------------------------------------------------------------------------------------

            '--------------------------------------------------------------------------------------------
            '��ȡ���ݼ�
            Dim strWhere As String = SStr("CMS_HOSTTABLE_WHERE")
            strWhere = CmsWhere.AppendAnd(strWhere, strOuterWhere)
            Dim strOrderBy As String = SStr("CMS_HOSTTABLE_ORDERBY")
            Dim strMoreTables As String = SStr("CMS_HOSTTABLE_MORETABLES")
            Dim intTotalRecNum As Integer = 0
                Dim dt As DataTable = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostTableData(CmsPass, CmsPass.HostResData.ResID, strWhere, strOrderBy, , ctrlPager1.RecStartOnCurPage, intTotalRecNum, intTotalRecNum, strMoreTables).Tables(0)
            '--------------------------------------------------------------------------------------------------
            '2007-08-13���
            '--------------------------------------------------------------------------------------------------
            Dim al As New ArrayList
            For ii As Integer = 0 To dt.Rows.Count - 1
                al.Add(DbField.GetStr(dt.Rows(ii), "Id"))
            Next
            Session("hosttable") = al

            '--------------------------------------------------------------------------------------------------

                Dim ds As DataSet = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostTableData(CmsPass, CmsPass.HostResData.ResID, strWhere, strOrderBy, , ctrlPager1.RecStartOnCurPage, ctrlPager1.PageRows, intTotalRecNum, strMoreTables)
            ctrlPager1.TotalRecordNumber = intTotalRecNum
            'ctrlPager1.AdjustCurrentPage()
            'Session("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID) = ctrlPager1.CurrentPage
            '--------------------------------------------------------------------------------------------

            ''��ȡDataset���ݺ�����ҳ���ؼ�
            ''Dim intDatasetCount As Integer = ds.Tables(0).DefaultView.Count
            'ctrlPager1.SetRecordNumber(intTotalRecNum, intDatasetCount)
            'dgridHostTable.VirtualItemCount = intDatasetCount '��ҳ��
            ''--------------------------------------------------------------------------------------------


            '�����ݼ�
                Dim dv As DataView = ds.Tables(0).DefaultView
            'dgridHostTable.VirtualItemCount = dv.Count
            dgridHostTable.PageSize = ctrlPager1.PageRows
            dgridHostTable.DataSource = dv
            dgridHostTable.DataBind()

            '��ʾ����������
            ShowRelationTableData(dgridSubTable, ctrlPager2, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, SLng("CMS_HOSTTABLE_RECID"), "", False, False, strRelPageCommand)
        Catch ex As Exception
            SLog.Err(ex.Message, ex)
        End Try
    End Sub

    Public Sub ShowFulltextSearchData(ByRef dgridHostTable As DataGrid, ByRef dgridSubTable As DataGrid, ByRef ctrlPager1 As Cms.Web.CmsPager, ByRef ctrlPager2 As Cms.Web.CmsPager, ByVal ResourceId As Long, ByVal lngResLocate As ResourceLocation, ByVal strHostRecIDs As String, ByVal strFTSearchContent As String, Optional ByVal strHostPageCommand As String = "MoveCurrentPage", Optional ByVal strRelPageCommand As String = "MoveCurrentPage")
        If CmsPass.HostResData.ResID = 0 Or CmsPass.HostResData.ResTable = "" Then Return 'û�к������ԴID
        If CmsRights.HasRightsRecView(CmsPass, ResourceId) = False Then Return '�����������Ȩ��

        Try
            '--------------------------------------------------------------------------------------------
            '������ǰ�ķ�ҳ�ؼ��Ĵ���
            ctrlPager1.DealPageCommandBeforeBind(strHostPageCommand)   '����ҳ��
            dgridHostTable.CurrentPageIndex = 0 'ctrlPager1.CurrentPage
            Session("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID) = ctrlPager1.CurrentPage
            '--------------------------------------------------------------------------------------------

            '��ʾ��������
            Dim strMoreTables As String = SStr("CMS_HOSTTABLE_MORETABLES")
            Dim ds As DataSet = Nothing
            Dim intTotalRecNum As Integer = 0
            If lngResLocate = ResourceLocation.HostTable Then
                ds = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostFTSearchTableData(CmsPass, CmsPass.HostResData.ResID, strFTSearchContent, , ctrlPager1.RecStartOnCurPage, ctrlPager1.PageRows, intTotalRecNum, strMoreTables)
            ElseIf lngResLocate = ResourceLocation.RelTable Then
                Dim strWhere As String = SStr("CMS_HOSTTABLE_WHERE")
                Dim strOrderBy As String = SStr("CMS_HOSTTABLE_ORDERBY")
                ds = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostTableData(CmsPass, CmsPass.HostResData.ResID, strWhere, strOrderBy, , ctrlPager1.RecStartOnCurPage, ctrlPager1.PageRows, intTotalRecNum, strMoreTables)
            End If
            ctrlPager1.TotalRecordNumber = intTotalRecNum
            'ctrlPager1.AdjustCurrentPage()
            'Session("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID) = ctrlPager1.CurrentPage

            '�����ݼ�
            Dim dv As DataView = ds.Tables(0).DefaultView
            'dgridHostTable.VirtualItemCount = dv.Count
            dgridHostTable.DataSource = dv
            dgridHostTable.DataBind()

            If lngResLocate = ResourceLocation.RelTable Then '��ʾ��ر�����
                ShowRelationTableData(dgridSubTable, ctrlPager2, CmsPass.HostResData.ResID, CmsPass.RelResData.ResID, CLng(strHostRecIDs), strFTSearchContent, True, True, strRelPageCommand)
            End If
        Catch ex As Exception
            SLog.Err(ex.Message, ex)
        End Try
    End Sub

    '----------------------------------------------------------
    '����ǰ��¼�����¼������
    '----------------------------------------------------------
    Public Sub ShowRelationTableData(ByRef dgridSubTable As DataGrid, ByRef ctrlPager2 As Cms.Web.CmsPager, ByVal lngHostResID As Long, ByVal lngRelResID As Long, ByVal lngHostRecID As Long, ByVal strFTSearchContent As String, ByVal blnIsFTSearch As Boolean, ByVal blnResetToFirstPage As Boolean, Optional ByVal strRelPageCommand As String = "MoveCurrentPage")
        Try
            If CmsPass.RelResList.Count <= 0 Or lngHostResID = 0 Or lngRelResID = 0 Or CmsPass.RelResData.ResTable = "" Then Return

            '--------------------------------------------------------------------------------------------
            '������ǰ�ķ�ҳ�ؼ��Ĵ���
            If blnResetToFirstPage = True Then strRelPageCommand = "MoveFirstPage"
            ctrlPager2.DealPageCommandBeforeBind(strRelPageCommand)   '����ҳ��
            dgridSubTable.CurrentPageIndex = 0
            Session("CMS_RELTABLE_PAGE" & CmsPass.RelResData.ResID) = ctrlPager2.CurrentPage
            '--------------------------------------------------------------------------------------------

            '--------------------------------------------------------------------------------------------
            Dim intTotalRecNum As Integer = 0
            Dim datRes As DataResource = CmsPass.GetDataRes(lngRelResID)
            Dim ds As DataSet = Nothing
            If blnIsFTSearch Then
                ds = ResFactory.TableService(datRes.ResTableType).GetRelFTSearchTableData(CmsPass, lngHostResID, lngRelResID, lngHostRecID, strFTSearchContent, SStr("CMS_SUBTABLE_ORDERBY"), ctrlPager2.RecStartOnCurPage, ctrlPager2.PageRows, intTotalRecNum)
            Else
                ds = ResFactory.TableService(datRes.ResTableType).GetRelTableData(CmsPass, lngHostResID, lngRelResID, lngHostRecID, SStr("CMS_SUBTABLE_WHERE"), SStr("CMS_SUBTABLE_ORDERBY"), ctrlPager2.RecStartOnCurPage, ctrlPager2.PageRows, intTotalRecNum)
            End If
            ctrlPager2.TotalRecordNumber = intTotalRecNum
            'ctrlPager2.AdjustCurrentPage()
            'Session("CMS_RELTABLE_PAGE" & CmsPass.RelResData.ResID) = ctrlPager2.CurrentPage
            Dim al As New ArrayList
            For ii As Integer = 0 To ds.Tables(0).Rows.Count - 1
                al.Add(DbField.GetStr(ds.Tables(0).Rows(ii), "Id"))
            Next
            Session("subtable") = al

            '�����ݼ�
            Dim dv As DataView = ds.Tables(0).DefaultView
            'dgridSubTable.VirtualItemCount = dv.Count
            dgridSubTable.PageSize = ctrlPager2.PageRows
            dgridSubTable.DataSource = dv
            dgridSubTable.DataBind()
        Catch ex As Exception
            SLog.Err("�������¼��Ϣ��ʾʧ�ܣ�", ex)
        End Try
    End Sub

    '------------------------------------------------------------------
    '��ȡ��DataGrid�ĵ�ǰѡ�еļ�¼ID
    '------------------------------------------------------------------
    Public Shared Function GetRecID2OfGrid(ByRef Request As System.Web.HttpRequest, ByRef ViewState As System.Web.UI.StateBag, ByVal IsPostBack As Boolean) As String
        If Not IsPostBack Then
            Dim strRecID As String = AspPage.RStr("URLRECID2", Request)
            ViewState("PAGE_GRIDRECID2") = strRecID
            Return strRecID
        Else
            Dim strRecID As String = AspPage.RStr("RECID2", Request)
            If strRecID <> "" Then
                ViewState("PAGE_GRIDRECID2") = strRecID
                Return strRecID
            Else
                Return AspPage.VStr("PAGE_GRIDRECID2", ViewState)
            End If
        End If
    End Function
End Class

End Namespace
