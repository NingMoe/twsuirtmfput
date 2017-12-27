Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class CmsFrmLeftTree
    Inherits Cms.Web.CmsFrmTreeBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents lbtnDailyWork As System.Web.UI.WebControls.LinkButton

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    'Protected Overrides Sub CmsPageSaveParametersToViewState()
    '    MyBase.CmsPageSaveParametersToViewState()
    'End Sub

    'Protected Overrides Sub CmsPageInitialize()
    '    LoadCmsMenu(lbtnDailyWork, "MENU_RESMANAGER")
    'End Sub

    'Protected Overrides Sub CmsPageDealFirstRequest()
    '    'DealMenuAction(RStr("cmsaction"))
    'End Sub

    '----------------------------------------------------------------------------------------
    'Load�Զ�������ṹ
    '----------------------------------------------------------------------------------------
    Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        '----------------------------------------------------------------------------------------
        '׼�����ṹ��صĲ�����Ϣ
        Dim strResUrl As String = "/cmsweb/cmshost/CmsFrmBridge.aspx"
        Dim strResTarget As String = "content"

        Dim strRootUrl As String = ""
        Dim strRootTarget As String = ""
        Dim strDepUrl As String = ""
        Dim strDepTarget As String = ""
        If CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ONEDEP_RES_ONLY") = True Then
            '����ʾһ�����ŵ���Դ��Ϣ������ÿ�ε�����Žڵ����ˢ��ҳ��
            strDepUrl = "/cmsweb/cmshost/CmsFrmLeftTree.aspx"
            strDepTarget = "_self"

            strRootUrl = "/cmsweb/cmshost/CmsFrmLeftTree.aspx"
            strRootTarget = "_self"
        Else '������Դ��һ������ʾ������ÿ�ε�����Žڵ���ҳ����Ӧ
        End If
        '----------------------------------------------------------------------------------------

        'Load���ṹ
            WebTreeDepResource.LoadResTreeView(pst, Request, Response, strRootUrl, strRootTarget, strDepUrl, strDepTarget, strResUrl, strResTarget, True, , False)
    End Sub
End Class

End Namespace
