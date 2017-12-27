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
    '处理当前资源的打印辅助操作
    '----------------------------------------------------------------
    Protected Sub DealPrintingWork(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long, ByVal strPrintSection As String)
        '获取刚保存的记录的完整信息
        If lngResID = 0 OrElse lngRecID = 0 Then Return
        Dim datRes As DataResource = pst.GetDataRes(lngResID)

        '检查打印辅助操作定义是否存在
        Dim strOneMenuSection As String = "MENU_PRINT_" & CmsPass.GetDataRes(lngResID).IndepParentResID & "_" & strPrintSection
        Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(pst.Employee.Language, CmsMenuType.Extension)
        If datSvc.GetKeyNum(strOneMenuSection) <= 0 Then
            Return
        End If

        Dim hashFieldVal As Hashtable = ResFactory.TableService(datRes.ResTableType).GetRecordDataByHashtable(pst, lngResID, lngRecID)
        Try
            Dim blnSetFieldOfCurRes As Boolean = False '判断有无设置当前资源的字段

            'Dim strOneMenuSection As String = RStr("MenuKey")
            'If strOneMenuSection = "" Then Return
            '提取菜单的字典列表
            Dim hashMenuDict As Hashtable = CmsFrmContentFlow.GetMenuDiction(CmsPass, datSvc, lngResID, True)
            Dim alistFlowAct As ArrayList = CmsFrmContentFlow.GetColSet(CmsPass, datSvc, strOneMenuSection, hashMenuDict, hashFieldVal)
            Dim datColSet As DataColSet
            For Each datColSet In alistFlowAct
                If datColSet.strRESTYPE = "0" Then '0：当前资源
                    HashField.SetStr(hashFieldVal, datColSet.strCOLNAME, CmsFrmContentFlow.FilterFieldValue(pst, datColSet.strColValue))
                    blnSetFieldOfCurRes = True
                ElseIf datColSet.strRESTYPE = "1" Then '1：是当前资源的父资源
                ElseIf datColSet.strRESTYPE = "2" Then '2：是当前资源的子资源
                    CmsFrmContentFlow.SetColumnOfSubResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                ElseIf datColSet.strRESTYPE = "3" Then '3：是其它资源
                    CmsFrmContentFlow.SetColumnOfOtherResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                End If
            Next

            If blnSetFieldOfCurRes = True Then
                '设置过当前资源的字段，保存记录值
                ResFactory.TableService(datRes.ResTableType).EditRecord(pst, lngResID, lngRecID, hashFieldVal, , , , , , False)
            End If
        Catch ex As Exception
            SLog.Err("打印窗体的辅助操作异常失败，lngResID=" & lngResID & " lngRecID=" & lngRecID, ex)
        End Try
    End Sub
End Class

End Namespace
