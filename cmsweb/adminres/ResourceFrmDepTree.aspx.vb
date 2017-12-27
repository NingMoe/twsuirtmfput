Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceFrmDepTree
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbtnDailyWork As System.Web.UI.WebControls.LinkButton
    Protected WithEvents panelTree As System.Web.UI.WebControls.Panel

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        lblDepTitle.Text = "部门列表"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '----------------------------------------------------------------------------------------
    'Load自定义的树结构
    '----------------------------------------------------------------------------------------
    Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        Dim strDepID As String = AspPage.RStr("depid", Request)
        If strDepID = "" AndAlso CLng(pst.Employee.LastNodeID) > 0 Then strDepID = CStr(ResFactory.ResService.GetResDepartment(pst, CLng(pst.Employee.LastNodeID)))

            WebTreeDepartment.LoadResTreeView(pst, Request, Response, "ResourceFrmContent.aspx", "content", strDepID, pst.Employee.ID, , False)
    End Sub
End Class

End Namespace
