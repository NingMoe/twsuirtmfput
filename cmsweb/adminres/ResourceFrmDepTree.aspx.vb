Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceFrmDepTree
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbtnDailyWork As System.Web.UI.WebControls.LinkButton
    Protected WithEvents panelTree As System.Web.UI.WebControls.Panel

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

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblDepTitle.Text = "�����б�"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '----------------------------------------------------------------------------------------
    'Load�Զ�������ṹ
    '----------------------------------------------------------------------------------------
    Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        Dim strDepID As String = AspPage.RStr("depid", Request)
        If strDepID = "" AndAlso CLng(pst.Employee.LastNodeID) > 0 Then strDepID = CStr(ResFactory.ResService.GetResDepartment(pst, CLng(pst.Employee.LastNodeID)))

            WebTreeDepartment.LoadResTreeView(pst, Request, Response, "ResourceFrmContent.aspx", "content", strDepID, pst.Employee.ID, , False)
    End Sub
End Class

End Namespace
