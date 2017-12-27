Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class CmsFrmLeftTree
    Inherits Cms.Web.CmsFrmTreeBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents lbtnDailyWork As System.Web.UI.WebControls.LinkButton

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
    'Load自定义的树结构
    '----------------------------------------------------------------------------------------
    Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        '----------------------------------------------------------------------------------------
        '准备树结构相关的参数信息
        Dim strResUrl As String = "/cmsweb/cmshost/CmsFrmBridge.aspx"
        Dim strResTarget As String = "content"

        Dim strRootUrl As String = ""
        Dim strRootTarget As String = ""
        Dim strDepUrl As String = ""
        Dim strDepTarget As String = ""
        If CmsConfig.GetBool("RESTREE_CONFIG", "SHOW_ONEDEP_RES_ONLY") = True Then
            '仅显示一个部门的资源信息，所以每次点击部门节点必须刷新页面
            strDepUrl = "/cmsweb/cmshost/CmsFrmLeftTree.aspx"
            strDepTarget = "_self"

            strRootUrl = "/cmsweb/cmshost/CmsFrmLeftTree.aspx"
            strRootTarget = "_self"
        Else '所有资源都一次性显示，所以每次点击部门节点无页面响应
        End If
        '----------------------------------------------------------------------------------------

        'Load树结构
            WebTreeDepResource.LoadResTreeView(pst, Request, Response, strRootUrl, strRootTarget, strDepUrl, strDepTarget, strResUrl, strResTarget, True, , False)
    End Sub
End Class

End Namespace
