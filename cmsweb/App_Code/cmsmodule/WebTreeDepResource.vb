Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeDepResource
    '-----------------------------------------------------
    'Load�Զ�������ṹ
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

            '�������ڵ�ǰ��׼��
            WebTreeview.TreePrepare(Response)

            '��ȡ��ǰ��Դ���ڵĲ���ID
            Dim iRes As IResource = ResFactory.ResService()
            Dim lngDepIDToShowRes As Long = -1
            If CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ONEDEP_RES_ONLY") = True Then
                '�������ʱˢ���ҽ���ʾ�ò��ŵ���Դ����Դ��������30000ʱ���á�
                If IsNumeric(strClickDepID) = True Then
                    lngDepIDToShowRes = CLng(strClickDepID)
                Else
                    lngDepIDToShowRes = iRes.GetResDepartment(pst, lngCurResID)
                End If
            End If

            '�������ڵ㣺��ҵ 
            Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, 0)
            WebTreeview.AddOneNode(Request, Response, WebTreeType.DepAndRes, 0, -1, 0, False, strDepName, strRootUrl, strRootTarget, strDepUrl, strDepTarget, strResUrl, strResTarget, "ICON_ENTERPRISE")  '���ø��ڵ�

            '�������ڵ������в��Žڵ�
            Dim resCondDep As New DataResCondition
            resCondDep.ForceToShowAllDeps = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ALL_DEPS")
            resCondDep.ResShowEnableOnly = blnResShowEnableOnly '���������������ݹ�������ʾ����Դ
            resCondDep.ResRightsValue = CmsRightsDefine.RecView '���������㵱ǰȨ����������Դ
            resCondDep.NoRightsCheckForAdmin = False '���Ȩ�������򿪣�����ǰ�û���ϵͳ����Ա�����Ź���Աʱ��Ȩ��������Ч
            resCondDep.HostDepID = -1 '�����ش˲���ID�µ���Դ
            resCondDep.ResourceType = "" '������ָ����Դ���͵���Դ�����ͣ�DOC TWOD
            resCondDep.DepColumns = "ID,PID,NAME,ICON_NAME" '�����صĲ����б��ֶ�
            resCondDep.FirstRes = True
            Dim dsDep As DataSet = iRes.GetDepByDatasetWithUserRights(pst, resCondDep, , blnDepShowEnableOnly, IsShowVirtualDep)
            WebTreeview.GenerateDepTree(Request, Response, dsDep.Tables(0).DefaultView, WebTreeType.DepAndRes, strDepUrl, strDepTarget)

            Dim dsRes As DataSet = iRes.GetResourceByDatasetWithUserRights(pst, resCondDep)
            WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget)


            '�������ڵ���������Դ�ڵ�
            Dim resCondRes As New DataResCondition
            resCondRes.ResShowEnableOnly = blnResShowEnableOnly '���������������ݹ�������ʾ����Դ
            Dim blnShowResWithoutCheckRights As Boolean = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_RES_WITHOUT_RIGHTS_CHECK") '��ʾ��Դʱ����Ȩ���жϡ��ڳ���2000����ԴʱΪ����ٶȶ�Ӧ�ô򿪴����á�
            resCondRes.ResRightsValue = CType(IIf(blnShowResWithoutCheckRights = True, CmsRightsDefine.NoRights, CmsRightsDefine.RecView), CmsRightsDefine) '���������㵱ǰȨ����������Դ
            resCondRes.NoRightsCheckForAdmin = False '���Ȩ�������򿪣�����ǰ�û���ϵͳ����Ա�����Ź���Աʱ��Ȩ��������Ч
            resCondRes.HostDepID = lngDepIDToShowRes '�����ش˲���ID�µ���Դ
            resCondRes.ResourceType = "" '������ָ����Դ���͵���Դ�����ͣ�DOC TWOD
            resCondRes.ResColumns = "ID,PID,HOST_ID,NAME,RES_ICONNAME,RES_ISFLOW,RES_TABLE,RES_TABLETYPE" '�����ص���Դ�б��ֶ�
            dsRes = iRes.GetResourceByDatasetWithUserRights(pst, resCondRes)
            WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.DepAndRes, strResUrl, strResTarget)

            '�������ڵ��λ��ָ���ڵ�
            Dim strFocusNodeID As String = CStr(IIf(IsNumeric(strClickDepID) = True, strClickDepID, lngCurResID))
            WebTreeview.TreeNodeFocus(Response, strFocusNodeID)

            '���ñ�־�������ǵ�һ�ε��
            'If pst.IsFirstRequestToResTree = True Then pst.IsFirstRequestToResTree = False
        End Sub
    End Class

End Namespace
