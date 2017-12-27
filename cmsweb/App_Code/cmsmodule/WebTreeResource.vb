Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeResource
    '-----------------------------------------------------
    'Load自定义的树结构
    '-----------------------------------------------------
        Public Shared Sub LoadResTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByVal strResUrl As String, ByVal strResTarget As String, ByVal strEmpUrl As String, ByVal strEmpTarget As String, Optional ByVal strFocusNodeID As String = "", Optional ByVal strDepID As String = "", Optional ByVal blnShowRootNodeOnly As Boolean = False, Optional ByVal blnIncludeDepIDinResUrl As Boolean = False, Optional ByVal blnResShowEnableOnly As Boolean = True)
            '生成树节点前的准备
            Dim strTreeName As String = "restree"
            WebTreeview.TreePrepare(Response, strTreeName)

            '创建根节点：部门
            If strDepID = "" Then strDepID = AspPage.RStr("depid", Request)
            If strDepID = "" Then strDepID = "0"
            Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, CLng(strDepID))
            WebTreeview.AddOneNode(Request, Response, WebTreeType.ResourceOnly, 0, -1, CLng(strDepID), False, strDepName, strResUrl, strResTarget, "", "", "", "", "ICON_DEP_REAL", strTreeName, , blnIncludeDepIDinResUrl)  '设置根节点
            If blnShowRootNodeOnly = True Then
                '仅显示根节点
                WebTreeview.TreeNodeFocus(Response, "", strTreeName)
                Return
            End If

            '只有在企业节点下才显示“人员管理”资源
            'If strDepID = "0" Then
            '    WebTreeview.AddOneNode(Request, Response, WebTreeType.ResourceOnly, CmsResID.Employee, 1, 0, False, CmsMessage.GetUI(pst, "RES_USERMGR"), "", "", "", "", strEmpUrl, strEmpTarget, "ICON_EMP", strTreeName, , blnIncludeDepIDinResUrl)  '设置根节点
            'End If

            '创建根节点下所有资源节点
            Dim resCondRes As New DataResCondition
            resCondRes.ResShowEnableOnly = blnResShowEnableOnly '仅返回允许在内容管理中显示的资源
            Dim blnShowResWithoutCheckRights As Boolean = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_RES_WITHOUT_RIGHTS_CHECK") '显示资源时不作权限判断。在超过2000个资源时为提高速度而应该打开此设置。
            resCondRes.ResRightsValue = CType(IIf(blnShowResWithoutCheckRights = True, CmsRightsDefine.NoRights, CmsRightsDefine.NoRights), CmsRightsDefine)  '仅返回满足当前权限条件的资源
            resCondRes.NoRightsCheckForAdmin = False '如果权限条件打开，但当前用户是系统管理员、部门管理员时，权限条件无效
            resCondRes.HostDepID = CLng(strDepID) '仅返回此部门ID下的资源
            resCondRes.ResourceType = "" '仅返回指定资源类型的资源，类型：DOC TWOD
            resCondRes.ResColumns = "ID,PID,HOST_ID,NAME,RES_ICONNAME,RES_ISFLOW,RES_TABLE,RES_TABLETYPE" '待返回的资源列表字段
            Dim dsRes As DataSet = ResFactory.ResService.GetResourceByDatasetWithUserRights(pst, resCondRes)
            WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget, strTreeName, , blnIncludeDepIDinResUrl)

            '生成树节点后定位到指定节点
            If strFocusNodeID = "" Then strFocusNodeID = AspPage.RStr("noderesid", Request)
            WebTreeview.TreeNodeFocus(Response, strFocusNodeID, strTreeName)

            '设置标志：不再是第一次点击
            'If pst.IsFirstRequestToResTree = True Then pst.IsFirstRequestToResTree = False
        End Sub
End Class

End Namespace
