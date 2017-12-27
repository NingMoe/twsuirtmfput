Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class WebTreeFavorite
    '-----------------------------------------------------
    'Load�ղؼе����ṹ
    '-----------------------------------------------------
    Public Shared Sub LoadFavoriteTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        Dim strResUrl As String = "/cmsweb/cmshost/CmsFrmBridge.aspx"
        Dim strResTarget As String = "content"

        Dim lngCurResID As Long = AspPage.RLng("noderesid", Request)
        If lngCurResID = 0 Then lngCurResID = CLng(pst.Employee.LastNodeID)

        '�����ղؼк�λ�����ڵ�
        If AspPage.RStr("cmsaction", Request) = "MenuFavoriteDel" Then lngCurResID = 0

        '�������ڵ�ǰ��׼��
        WebTreeview.TreePrepare(Response)

        '�������ڵ㣺��ҵ
        Dim strDepName As String = OrgFactory.DepDriver.GetDepName(pst, 0)  '�����ҵ����
        WebTreeview.AddOneNode(Request, Response, WebTreeType.ResourceOnly, 0, -1, 0, False, strDepName, "", "", "", "", "", "", "ICON_ENTERPRISE") '���ø��ڵ�

        '���������ղؼ���Դ�ĸ���Դ�ڵ�
        'Dim dsFav As DataSet = DbDailyWork.GetDailyworkByDataSet(pst, pst.EmpID)
        'WebTreeview.GenerateResourceTree(Request, Response, dsFav.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget, , 1)

        '�������ڵ���������Դ�ڵ�
        'Dim dvFav As DataView = dsFav.Tables(0).DefaultView
        'Dim drvFav As DataRowView
        'For Each drvFav In dvFav
        '    Dim lngResPID As Long = DbField.GetLng(drvFav, ("ID"))
        '    Dim resCondRes As New DataResCondition
        '    resCondRes.ResShowEnableOnly = True '���������������ݹ�������ʾ����Դ
        '    Dim blnShowResWithoutCheckRights As Boolean = CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_RES_WITHOUT_RIGHTS_CHECK") '��ʾ��Դʱ����Ȩ���жϡ��ڳ���2000����ԴʱΪ����ٶȶ�Ӧ�ô򿪴����á�
        '    resCondRes.ResRightsValue = CType(IIf(blnShowResWithoutCheckRights = True, CmsRightsDefine.NoRights, CmsRightsDefine.RecView), CmsRightsDefine) '���������㵱ǰȨ����������Դ
        '    resCondRes.NoRightsCheckForAdmin = False '���Ȩ�������򿪣�����ǰ�û���ϵͳ����Ա�����Ź���Աʱ��Ȩ��������Ч
        '    resCondRes.HostDepID = -1 '�����ش˲���ID�µ���Դ
        '    resCondRes.ParentResID = lngResPID '�����ش���Դ��������Դ
        '    resCondRes.ResourceType = "" '������ָ����Դ���͵���Դ�����ͣ�DOC TWOD
        '    resCondRes.ResColumns = "ID,PID,HOST_ID,NAME,RES_ICONNAME,RES_ISFLOW,RES_TABLE,RES_TABLETYPE" '�����ص���Դ�б��ֶ�
        '    Dim dsRes As DataSet = ResFactory.ResService.GetResourceByDatasetWithUserRights(pst, resCondRes)
        '    WebTreeview.GenerateResourceTree(Request, Response, dsRes.Tables(0).DefaultView, WebTreeType.ResourceOnly, strResUrl, strResTarget)
        'Next

        '�������ڵ��λ��ָ���ڵ�
        WebTreeview.TreeNodeFocus(Response, CStr(lngCurResID))

        '���ñ�־�������ǵ�һ�ε��
        'If pst.IsFirstRequestToResTree = True Then pst.IsFirstRequestToResTree = False
    End Sub
End Class

End Namespace
