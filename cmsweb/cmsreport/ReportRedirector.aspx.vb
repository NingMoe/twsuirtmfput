Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ReportRedirector
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
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim blnCloseForm As Boolean = False
        Try
            Dim strRedirectUrl As String = ""
            Dim strFlowMsg As String = ""
            Dim blnRefreshTableData As Boolean = False
            Dim bln As Boolean = CmsFrmContentFlow.FlowEntry(CmsPass, "MenuFlowControllerForReport", strRedirectUrl, strFlowMsg, blnRefreshTableData, Request, Session, Response, Server)
            If bln Then
                If strRedirectUrl <> "" Then
                    '校验和处理成功后重定向至指定报表URL
                    Dim strUrl As String = CmsUrl.AppendParam(strRedirectUrl, "mnurecid=" & RStr("mnurecid") & "&timeid=" & TimeId.CurrentMilliseconds())
                    Response.Redirect(strUrl, False)
                    Return
                Else
                    PromptMsg("未定义有效的重定向URL！")
                    blnCloseForm = True
                End If
            Else
                PromptMsg(strFlowMsg)
                blnCloseForm = True
            End If
        Catch ex As Exception
            SLog.Err("显示报表异常出错！", ex)
            blnCloseForm = True
        End Try

        '失败后关闭窗体
        If blnCloseForm = True AndAlso Not IsStartupScriptRegistered("CmsCloseForm") Then
            Dim strScript As String = "<script language=""javascript"">" & Environment.NewLine
            strScript &= "    window.close();" & Environment.NewLine
            strScript &= "</script>" & Environment.NewLine
            RegisterStartupScript("CmsCloseForm", strScript)
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
