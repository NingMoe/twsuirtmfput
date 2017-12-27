Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls
Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class PrintBase
    Inherits CmsPage
    '----------------------------------------------------------------
    '����ǰ��Դ�Ĵ�ӡ��������
    '----------------------------------------------------------------
    Protected Sub DealPrintingWork(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long, ByVal strPrintSection As String)
        '��ȡ�ձ���ļ�¼��������Ϣ
        If lngResID = 0 OrElse lngRecID = 0 Then Return
        Dim datRes As DataResource = pst.GetDataRes(lngResID)

        '����ӡ�������������Ƿ����
        Dim strOneMenuSection As String = "MENU_PRINT_" & CmsPass.GetDataRes(lngResID).IndepParentResID & "_" & strPrintSection
        Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(pst.Employee.Language, CmsMenuType.Extension)
        If datSvc.GetKeyNum(strOneMenuSection) <= 0 Then
            Return
        End If

        Dim hashFieldVal As Hashtable = ResFactory.TableService(datRes.ResTableType).GetRecordDataByHashtable(pst, lngResID, lngRecID)
        Try
            Dim blnSetFieldOfCurRes As Boolean = False '�ж��������õ�ǰ��Դ���ֶ�

            'Dim strOneMenuSection As String = RStr("MenuKey")
            'If strOneMenuSection = "" Then Return
            '��ȡ�˵����ֵ��б�
            Dim hashMenuDict As Hashtable = CmsFrmContentFlow.GetMenuDiction(CmsPass, datSvc, lngResID, True)
            Dim alistFlowAct As ArrayList = CmsFrmContentFlow.GetColSet(CmsPass, datSvc, strOneMenuSection, hashMenuDict, hashFieldVal)
            Dim datColSet As DataColSet
            For Each datColSet In alistFlowAct
                If datColSet.strRESTYPE = "0" Then '0����ǰ��Դ
                    HashField.SetStr(hashFieldVal, datColSet.strCOLNAME, CmsFrmContentFlow.FilterFieldValue(pst, datColSet.strColValue))
                    blnSetFieldOfCurRes = True
                ElseIf datColSet.strRESTYPE = "1" Then '1���ǵ�ǰ��Դ�ĸ���Դ
                ElseIf datColSet.strRESTYPE = "2" Then '2���ǵ�ǰ��Դ������Դ
                    CmsFrmContentFlow.SetColumnOfSubResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                ElseIf datColSet.strRESTYPE = "3" Then '3����������Դ
                    CmsFrmContentFlow.SetColumnOfOtherResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                End If
            Next

            If blnSetFieldOfCurRes = True Then
                '���ù���ǰ��Դ���ֶΣ������¼ֵ
                ResFactory.TableService(datRes.ResTableType).EditRecord(pst, lngResID, lngRecID, hashFieldVal, , , , , , False)
            End If
        Catch ex As Exception
            SLog.Err("��ӡ����ĸ��������쳣ʧ�ܣ�lngResID=" & lngResID & " lngRecID=" & lngRecID, ex)
        End Try
    End Sub
End Class

End Namespace
