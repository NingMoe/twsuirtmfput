Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeResource
    '-----------------------------------------------------
    'Load�Զ�������ṹ
    '-----------------------------------------------------
        Public Shared Sub LoadResTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByVal strResUrl As String, ByVal strResTarget As String, ByVal strEmpUrl As String, ByVal strEmpTarget As String, Optional ByVal strFocusNodeID As String = "", Optional ByVal strDepID As String = "", Optional ByVal blnShowRootNodeOnly As Boolean = False, Optional ByVal blnIncludeDepIDinResUrl As Boolean = False, Optional ByVal blnResShowEnableOnly As Boolean = True)
            '�������ڵ�ǰ��׼��
            Dim strTreeName As String = "restree"
            WebTreeview.TreePrepare(Response, strTreeName)

            '�������ڵ㣺����
            If strDepID = "" Then strDepID = AspPage.RStr("depid", Request)
            If strDepID = "" Then strDepID = "0"
            Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, CLng(strDepID))
            WebTreeview.AddOneNode(Request, Response, WebTreeType.ResourceOnly, 0, -1, CLng(strDepID), False, strDepName, strResUrl, strResTarget, "", "", "", "", "ICON_DEP_REAL", strTreeName, , blnIncludeDepIDinResUrl)  '���ø��ڵ�
            If blnShowRootNodeOnly = True Then
                '����ʾ���ڵ�
                WebTreeview.TreeNodeFocus(Response, "", strTreeName)
                Return
            End If

            'ֻ������ҵ�ڵ��²���ʾ����Ա������Դ
            'If strDepID = "0" Then
            '    WebTreeview.AddOneNode(Request, Response, WebTreeType.ResourceOnly, CmsResID.Employee, 1, 0, False, CmsMessage.GetUI(pst, "RES_USERMGR"), "", "", "", "", strEmpUrl, strEmpTarget, "ICON_EMP", strTreeName, , blnIncludeDepIDinResUrl)  '���ø��ڵ�
            'End If

            '�������ڵ���������Դ�ڵ�
            Dim resCondRes As New DataResCondition
            resCondRes.ResShowEnableOnly = blnResShowEnableOnly '���������������ݹ�������ʾ����Դ
            Dim blnShowResWithoutCheckRights As Boolean = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_RES_WITHOUT_RIGHTS_CHECK") '��ʾ��Դʱ����Ȩ���жϡ��ڳ���2000����ԴʱΪ����ٶȶ�Ӧ�ô򿪴����á�
            resCondRes.ResRightsValue = CType(IIf(blnShowResWithoutCheckRights = True, CmsRightsDefine.NoRights, CmsRightsDefine.NoRights), CmsRightsDefine)  '���������㵱ǰȨ����������Դ
            resCondRes.NoRightsCheckForAdmin = False '���Ȩ�������򿪣�����ǰ�û���ϵͳ����Ա�����Ź���Աʱ��Ȩ��������Ч
            resCondRes.HostDepID = CLng(strDepID) '�����ش˲���ID�µ���Դ
            resCondRes.ResourceType = "" '������ָ����Դ���͵���Դ�����ͣ�DOC TWOD
            resCondRes.ResColumns = "ID,PID,HOST_ID,NAME,RES_ICONNAME,RES_ISFLOW,RES_TABLE,RES_TABLETYPE" '�����ص���Դ�б��ֶ�
            Dim dsRes As DataSet = ResFactory.ResService.GetResourceByDatasetWithUserRights(pst, resCondRes)
            WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget, strTreeName, , blnIncludeDepIDinResUrl)

            '�������ڵ��λ��ָ���ڵ�
            If strFocusNodeID = "" Then strFocusNodeID = AspPage.RStr("noderesid", Request)
            WebTreeview.TreeNodeFocus(Response, strFocusNodeID, strTreeName)

            '���ñ�־�������ǵ�һ�ε��
            'If pst.IsFirstRequestToResTree = True Then pst.IsFirstRequestToResTree = False
        End Sub
End Class

End Namespace
