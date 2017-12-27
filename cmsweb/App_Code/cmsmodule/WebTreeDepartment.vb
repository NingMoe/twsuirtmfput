Option Strict On
Option Explicit On 

Imports System.Web

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeDepartment
    '-----------------------------------------------------
    'Load自定义的树结构
    '-----------------------------------------------------
        Public Shared Sub LoadResTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByVal strDepUrl As String, ByVal strDepTarget As String, Optional ByVal strFocusNodeID As String = "", Optional ByVal strDepAdminID As String = "", Optional ByVal blnDepShowEnableOnly As Boolean = False, Optional ByVal IsShowVirtualDep As Boolean = True)
            '生成树节点前的准备
            Dim strTreeName As String = "deptree"
            WebTreeview.TreePrepare(Response, strTreeName)

            '创建根节点：企业
            Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, 0)  '获得企业名称
            WebTreeview.AddOneNode(Request, Response, WebTreeType.DepartmentOnly, 0, -1, 0, False, strDepName, strDepUrl, strDepTarget, "", "", "", "", "ICON_ENTERPRISE", strTreeName) '设置根节点

            '创建根节点下所有部门节点
            Dim resCondDep As New DataResCondition
            resCondDep.ForceToShowAllDeps = True
            Dim dsDep As DataSet = ResFactory.ResService.GetDepByDatasetWithUserRights(pst, resCondDep, strDepAdminID, blnDepShowEnableOnly, IsShowVirtualDep)
            WebTreeview.GenerateDepTree(Request, Response, dsDep.Tables(0).DefaultView, WebTreeType.DepartmentOnly, strDepUrl, strDepTarget, strTreeName)

            '生成树节点后定位到指定节点
            If strFocusNodeID = "" Then strFocusNodeID = AspPage.RStr("depid", Request)
            WebTreeview.TreeNodeFocus(Response, strFocusNodeID, strTreeName)
        End Sub
End Class

End Namespace
