Option Strict On
Option Explicit On 

Imports System.Web

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeDepartment
    '-----------------------------------------------------
    'Load�Զ�������ṹ
    '-----------------------------------------------------
        Public Shared Sub LoadResTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByVal strDepUrl As String, ByVal strDepTarget As String, Optional ByVal strFocusNodeID As String = "", Optional ByVal strDepAdminID As String = "", Optional ByVal blnDepShowEnableOnly As Boolean = False, Optional ByVal IsShowVirtualDep As Boolean = True)
            '�������ڵ�ǰ��׼��
            Dim strTreeName As String = "deptree"
            WebTreeview.TreePrepare(Response, strTreeName)

            '�������ڵ㣺��ҵ
            Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, 0)  '�����ҵ����
            WebTreeview.AddOneNode(Request, Response, WebTreeType.DepartmentOnly, 0, -1, 0, False, strDepName, strDepUrl, strDepTarget, "", "", "", "", "ICON_ENTERPRISE", strTreeName) '���ø��ڵ�

            '�������ڵ������в��Žڵ�
            Dim resCondDep As New DataResCondition
            resCondDep.ForceToShowAllDeps = True
            Dim dsDep As DataSet = ResFactory.ResService.GetDepByDatasetWithUserRights(pst, resCondDep, strDepAdminID, blnDepShowEnableOnly, IsShowVirtualDep)
            WebTreeview.GenerateDepTree(Request, Response, dsDep.Tables(0).DefaultView, WebTreeType.DepartmentOnly, strDepUrl, strDepTarget, strTreeName)

            '�������ڵ��λ��ָ���ڵ�
            If strFocusNodeID = "" Then strFocusNodeID = AspPage.RStr("depid", Request)
            WebTreeview.TreeNodeFocus(Response, strFocusNodeID, strTreeName)
        End Sub
End Class

End Namespace
