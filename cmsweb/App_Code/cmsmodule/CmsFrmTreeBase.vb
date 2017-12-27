Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Public Class CmsFrmTreeBase
    Inherits CmsPage
    '------------------------------------------------------------------------------
    '������Դ���ݹ�������ϵ����в˵���Ӧ
    '���أ�True:�ǲ˵������Ѵ��� False�����ǲ˵�����
    '------------------------------------------------------------------------------
    'Protected Function DealMenuAction(ByVal strAction As String) As Boolean
    '    Try
    '        Dim lngResID As Long = CmsPass.LastNodeID
    '        Select Case strAction
    '            'Case "MenuFavoriteAdd" '�����ղؼ�
    '            '    DbDailyWork.AddFavoriteResource(CmsPass, lngResID, CmsPass.EmpID)

    '            'Case "MenuFavoriteDel" '�����ղؼ�
    '            '    DbDailyWork.DelFavoriteResource(CmsPass, lngResID, CmsPass.EmpID)

    '            'Case "MenuFavoriteMoveUp" '�ղؼ�����Դ�����ƶ�
    '            '    DbDailyWork.MoveUp(CmsPass, lngResID, CmsPass.EmpID)

    '            'Case "MenuFavoriteMoveDown" '�ղؼ�����Դ�����ƶ�
    '            '    DbDailyWork.MoveDown(CmsPass, lngResID, CmsPass.EmpID)

    '            Case "MenuFavoriteRefresh" 'ˢ��

    '            Case "MenuFavoriteSetMyWork" '�����ҵĹ���
    '        End Select

    '        Return True
    '    Catch ex As Exception
    '        PromptMsg("", ex, True)
    '    End Try
    'End Function

    '------------------------------------------------------------------------
    'Loadҳ�����в˵�
    '------------------------------------------------------------------------
    Protected Sub LoadCmsMenu(ByRef lbtnDailyWork As System.Web.UI.WebControls.LinkButton, ByVal strMenuSectionName As String)
        'Load����������˵�����¼����
        If lbtnDailyWork.Visible = True Then
            RegisterMenuScript(CmsPass, lbtnDailyWork, Nothing, CmsPass.HostResData.ResID, ResourceLocation.HostTable, strMenuSectionName, CmsMenuType.Standard, "ShowMenuOfResMgr")
        End If
    End Sub

    '-----------------------------------------------------------------------
    '��ʼ���������Tabstrip
    '-----------------------------------------------------------------------
    Private Sub RegisterMenuScript( _
        ByRef pst As CmsPassport, _
        ByRef lbtnMenu As System.Web.UI.WebControls.LinkButton, _
        ByRef imgMenu As System.Web.UI.WebControls.Image, _
        ByVal lngResID As Long, _
        ByVal intResLoc As ResourceLocation, _
        ByVal strMenuSection As String, _
        ByVal intMenuType As CmsMenuType, _
        ByVal strMenuJSFunc As String _
        )
        Dim strScript As String = CmsMenu.CreateScriptOfOneMainMenu(pst, lbtnMenu, imgMenu, lngResID, intResLoc, strMenuSection, intMenuType, strMenuJSFunc, CmsMenuFrom.ResourceTree)
        If strScript <> "" And (Not IsStartupScriptRegistered(strMenuJSFunc)) Then
            RegisterStartupScript(strMenuJSFunc, strScript)
        End If
    End Sub
End Class

End Namespace
