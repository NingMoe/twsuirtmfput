Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocSearchTransfer
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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
        Dim lngCurResID As Long = RLng("noderesid")
        Dim lngDocID As Long = RLng("schdocid")

        CmsPass.SetHostResID(lngCurResID) '必须在进入表单数据列表前改变当前资源
        Session("CMS_HOSTTABLE_RECID") = "" '清空当前选中记录ID
        Session("CMS_HOSTTABLE_MORETABLES") = "" '多个表单查询中其它表单列表
        Session("CMS_HOSTTABLE_WHERE") = "" '清空查询条件
        Session("CMS_HOSTTABLE_ORDERBY") = "" '必须将排序条件置空
        Session("CMS_SUBTABLE_RECID") = "" '清空当前选中记录ID
        Session("CMS_SUBTABLE_WHERE") = "" '清空查询条件
        Session("CMS_SUBTABLE_ORDERBY") = "" '必须将排序条件置空

        'If CmsConfig.MemorizeLastResNode() = True Then OrgFactory.EmpDriver.SetLastNodeID(CmsPass, CmsPass.Employee.ID, CStr(lngCurResID))
            Dim strUrl As String = "../cmshost/CmsFrmBody.aspx?noderesid=" & lngCurResID & "&schdocid=" & lngDocID
        Server.Transfer(strUrl, False)
    End Sub
End Class

End Namespace
