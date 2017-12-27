Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeFavorite
    '-----------------------------------------------------
    'Load收藏夹的树结构
    '-----------------------------------------------------
    Public Shared Sub LoadFavoriteTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        Dim strResUrl As String = "/cmsweb/cmshost/CmsFrmBridge.aspx"
        Dim strResTarget As String = "content"

        Dim lngCurResID As Long = AspPage.RLng("noderesid", Request)
        If lngCurResID = 0 Then lngCurResID = CLng(pst.Employee.LastNodeID)

        '移离收藏夹后定位到根节点
        If AspPage.RStr("cmsaction", Request) = "MenuFavoriteDel" Then lngCurResID = 0

        '生成树节点前的准备
        WebTreeview.TreePrepare(Response)

        '创建根节点：企业
        Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, 0)  '获得企业名称
        WebTreeview.AddOneNode(Request, Response, WebTreeType.ResourceOnly, 0, -1, 0, False, strDepName, "", "", "", "", "", "", "ICON_ENTERPRISE") '设置根节点

        '创建所有收藏夹资源的根资源节点
        'Dim dsFav As DataSet = DbDailyWork.GetDailyworkByDataSet(pst, pst.EmpID)
        'WebTreeview.GenerateResourceTree(Request, Response, dsFav.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget, , 1)

        '创建根节点下所有资源节点
        'Dim dvFav As DataView = dsFav.Tables(0).DefaultView
        'Dim drvFav As DataRowView
        'For Each drvFav In dvFav
        '    Dim lngResPID As Long = DbField.GetLng(drvFav, ("ID"))
        '    Dim resCondRes As New DataResCondition
        '    resCondRes.ResShowEnableOnly = True '仅返回允许在内容管理中显示的资源
        '    Dim blnShowResWithoutCheckRights As Boolean = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_RES_WITHOUT_RIGHTS_CHECK") '显示资源时不作权限判断。在超过2000个资源时为提高速度而应该打开此设置。
        '    resCondRes.ResRightsValue = CType(IIf(blnShowResWithoutCheckRights = True, CmsRightsDefine.NoRights, CmsRightsDefine.RecView), CmsRightsDefine) '仅返回满足当前权限条件的资源
        '    resCondRes.NoRightsCheckForAdmin = False '如果权限条件打开，但当前用户是系统管理员、部门管理员时，权限条件无效
        '    resCondRes.HostDepID = -1 '仅返回此部门ID下的资源
        '    resCondRes.ParentResID = lngResPID '仅返回此资源下属的资源
        '    resCondRes.ResourceType = "" '仅返回指定资源类型的资源，类型：DOC TWOD
        '    resCondRes.ResColumns = "ID,PID,HOST_ID,NAME,RES_ICONNAME,RES_ISFLOW,RES_TABLE,RES_TABLETYPE" '待返回的资源列表字段
        '    Dim dsRes As DataSet = ResFactory.ResService.GetResourceByDatasetWithUserRights(pst, resCondRes)
        '    WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget)
        'Next

        '生成树节点后定位到指定节点
        WebTreeview.TreeNodeFocus(Response, CStr(lngCurResID))

        '设置标志：不再是第一次点击
        'If pst.IsFirstRequestToResTree = True Then pst.IsFirstRequestToResTree = False
    End Sub
End Class

End Namespace
