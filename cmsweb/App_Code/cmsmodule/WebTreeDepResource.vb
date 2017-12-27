Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeDepResource
    '-----------------------------------------------------
    'Load自定义的树结构
    '-----------------------------------------------------
        Public Shared Sub LoadResTreeView( _
            ByRef pst As CmsPassport, _
            ByRef Request As System.Web.HttpRequest, _
            ByRef Response As System.Web.HttpResponse, _
            ByVal strRootUrl As String, _
            ByVal strRootTarget As String, _
            ByVal strDepUrl As String, _
            ByVal strDepTarget As String, _
            ByVal strResUrl As String, _
            ByVal strResTarget As String, _
            Optional ByVal blnDepShowEnableOnly As Boolean = True, _
            Optional ByVal blnResShowEnableOnly As Boolean = True, _
             Optional ByVal IsShowVirtualDep As Boolean = True _
            )
            Dim strClickDepID As String = AspPage.RStr("depid", Request)
            Dim lngCurResID As Long = AspPage.RLng("noderesid", Request)
            If lngCurResID = 0 AndAlso strClickDepID = "" Then lngCurResID = CLng(pst.Employee.LastNodeID)

            '生成树节点前的准备
            WebTreeview.TreePrepare(Response)

            '获取当前资源所在的部门ID
            Dim iRes As IResource = ResFactory.ResService()
            Dim lngDepIDToShowRes As Long = -1
            If CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ONEDEP_RES_ONLY") = True Then
                '点击部门时刷新且仅显示该部门的资源。资源数量超过30000时适用。
                If IsNumeric(strClickDepID) = True Then
                    lngDepIDToShowRes = CLng(strClickDepID)
                Else
                    lngDepIDToShowRes = iRes.GetResDepartment(pst, lngCurResID)
                End If
            End If

            '创建根节点：企业 
            Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, 0)
            WebTreeview.AddOneNode(Request, Response, WebTreeType.DepAndRes, 0, -1, 0, False, strDepName, strRootUrl, strRootTarget, strDepUrl, strDepTarget, strResUrl, strResTarget, "ICON_ENTERPRISE")  '设置根节点

            '创建根节点下所有部门节点
            Dim resCondDep As New DataResCondition
            resCondDep.ForceToShowAllDeps = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ALL_DEPS")
            resCondDep.ResShowEnableOnly = blnResShowEnableOnly '仅返回允许在内容管理中显示的资源
            resCondDep.ResRightsValue = CmsRightsDefine.RecView '仅返回满足当前权限条件的资源
            resCondDep.NoRightsCheckForAdmin = False '如果权限条件打开，但当前用户是系统管理员、部门管理员时，权限条件无效
            resCondDep.HostDepID = -1 '仅返回此部门ID下的资源
            resCondDep.ResourceType = "" '仅返回指定资源类型的资源，类型：DOC TWOD
            resCondDep.DepColumns = "ID,PID,NAME,ICON_NAME" '待返回的部门列表字段
            resCondDep.FirstRes = True
            Dim dsDep As DataSet = iRes.GetDepByDatasetWithUserRights(pst, resCondDep, , blnDepShowEnableOnly, IsShowVirtualDep)
            WebTreeview.GenerateDepTree(Request, Response, dsDep.Tables(0).DefaultView, WebTreeType.DepAndRes, strDepUrl, strDepTarget)

            Dim dsRes As DataSet = iRes.GetResourceByDatasetWithUserRights(pst, resCondDep)
            WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget)


            '创建根节点下所有资源节点
            Dim resCondRes As New DataResCondition
            resCondRes.ResShowEnableOnly = blnResShowEnableOnly '仅返回允许在内容管理中显示的资源
            Dim blnShowResWithoutCheckRights As Boolean = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_RES_WITHOUT_RIGHTS_CHECK") '显示资源时不作权限判断。在超过2000个资源时为提高速度而应该打开此设置。
            resCondRes.ResRightsValue = CType(IIf(blnShowResWithoutCheckRights = True, CmsRightsDefine.NoRights, CmsRightsDefine.RecView), CmsRightsDefine) '仅返回满足当前权限条件的资源
            resCondRes.NoRightsCheckForAdmin = False '如果权限条件打开，但当前用户是系统管理员、部门管理员时，权限条件无效
            resCondRes.HostDepID = lngDepIDToShowRes '仅返回此部门ID下的资源
            resCondRes.ResourceType = "" '仅返回指定资源类型的资源，类型：DOC TWOD
            resCondRes.ResColumns = "ID,PID,HOST_ID,NAME,RES_ICONNAME,RES_ISFLOW,RES_TABLE,RES_TABLETYPE" '待返回的资源列表字段
            dsRes = iRes.GetResourceByDatasetWithUserRights(pst, resCondRes)
            WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.DepAndRes, strResUrl, strResTarget)

            '生成树节点后定位到指定节点
            Dim strFocusNodeID As String = CStr(IIf(IsNumeric(strClickDepID) = True, strClickDepID, lngCurResID))
            WebTreeview.TreeNodeFocus(Response, strFocusNodeID)

            '设置标志：不再是第一次点击
            'If pst.IsFirstRequestToResTree = True Then pst.IsFirstRequestToResTree = False
        End Sub
    End Class

End Namespace
