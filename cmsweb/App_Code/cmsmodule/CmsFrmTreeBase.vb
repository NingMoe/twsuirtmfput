Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Public Class CmsFrmTreeBase
    Inherits CmsPage
    '------------------------------------------------------------------------------
    '处理资源内容管理界面上的所有菜单响应
    '返回：True:是菜单请求并已处理； False：不是菜单请求
    '------------------------------------------------------------------------------
    'Protected Function DealMenuAction(ByVal strAction As String) As Boolean
    '    Try
    '        Dim lngResID As Long = CmsPass.LastNodeID
    '        Select Case strAction
    '            'Case "MenuFavoriteAdd" '加入收藏夹
    '            '    DbDailyWork.AddFavoriteResource(CmsPass, lngResID, CmsPass.EmpID)

    '            'Case "MenuFavoriteDel" '移离收藏夹
    '            '    DbDailyWork.DelFavoriteResource(CmsPass, lngResID, CmsPass.EmpID)

    '            'Case "MenuFavoriteMoveUp" '收藏夹中资源向上移动
    '            '    DbDailyWork.MoveUp(CmsPass, lngResID, CmsPass.EmpID)

    '            'Case "MenuFavoriteMoveDown" '收藏夹中资源向下移动
    '            '    DbDailyWork.MoveDown(CmsPass, lngResID, CmsPass.EmpID)

    '            Case "MenuFavoriteRefresh" '刷新

    '            Case "MenuFavoriteSetMyWork" '设置我的工作
    '        End Select

    '        Return True
    '    Catch ex As Exception
    '        PromptMsg("", ex, True)
    '    End Try
    'End Function

    '------------------------------------------------------------------------
    'Load页面所有菜单
    '------------------------------------------------------------------------
    Protected Sub LoadCmsMenu(ByRef lbtnDailyWork As System.Web.UI.WebControls.LinkButton, ByVal strMenuSectionName As String)
        'Load主界面主表菜单：记录操作
        If lbtnDailyWork.Visible = True Then
            RegisterMenuScript(CmsPass, lbtnDailyWork, Nothing, CmsPass.HostResData.ResID, ResourceLocation.HostTable, strMenuSectionName, CmsMenuType.Standard, "ShowMenuOfResMgr")
        End If
    End Sub

    '-----------------------------------------------------------------------
    '初始化关联表的Tabstrip
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
