Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CmsFrmBridge
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
        If Not CmsPass.HostResData Is Nothing AndAlso CmsPass.HostResData.ResID <> RLng("noderesid") Then
            Session("CMS_HOSTTABLE_RECID") = "" '清空当前选中记录ID
            Session("CMS_HOSTTABLE_MORETABLES") = "" '多个表单查询中其它表单列表
            Session("CMS_HOSTTABLE_WHERE") = "" '清空查询条件
            Session("CMS_HOSTTABLE_ORDERBY") = "" '必须将排序条件置空

            Session("CMS_SUBTABLE_RECID") = "" '清空当前选中记录ID
            Session("CMS_SUBTABLE_WHERE") = "" '清空查询条件
            Session("CMS_SUBTABLE_ORDERBY") = "" '必须将排序条件置空

            '点击资源节点进入的，且当前节点已改变，页面恢复为第一页
            Session("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID) = 0
            Session("CMS_RELTABLE_PAGE" & CmsPass.RelResData.ResID) = 0
        End If
    End Sub
End Class

End Namespace
