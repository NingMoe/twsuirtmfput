'---------------------------------------------------------------
'��ҳ��ֻ��ΪFrame�ܹ������Frame�Ĳ������ṹ������TreeView��AutoPostBack=False
'---------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmDepTree
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

            WebTreeDepartment.LoadResTreeView(pst, Request, Response, "/cmsweb/adminsys/EmployeeFrmBridge.aspx", "content", strDepID, pst.Employee.ID)
        End Sub
End Class
End Namespace
