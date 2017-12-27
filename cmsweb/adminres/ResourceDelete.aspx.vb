Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceDelete
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
        Dim lngResID As Long = VLng("PAGE_RESID")
        Try
            '删除数据库中节点信息
            Dim lngParentResID As Long = CmsPass.GetDataRes(lngResID).ResPID
            ResFactory.ResService.DeleteResource(CmsPass, lngResID)
            CmsPass.SetHostResID(lngParentResID) '必须设置被删除资源的父资源为当前资源，因为当前资源刚被删除

            '修改当前选中节点为被删除节点的父节点
            Dim strUrl As String = VStr("PAGE_BACKPAGE")
            Dim pos As Integer = strUrl.IndexOf("noderesid=")
            Dim len As Integer = 10
            If pos > 0 Then
                Dim strNodeID As String = ""
                Dim pos2 As Integer = strUrl.IndexOf("&", pos)
                If pos2 > 0 Then
                    strNodeID = strUrl.Substring(pos + len, pos2 - pos - len)
                Else
                    strNodeID = strUrl.Substring(pos + len)
                End If
                If strNodeID <> "" Then
                    strUrl = strUrl.Replace(strNodeID, CStr(lngParentResID))
                Else
                    strUrl = strUrl.Substring(0, pos + len) & lngParentResID & strUrl.Substring(pos2)
                End If
            Else
                strUrl = CmsUrl.AppendParam(strUrl, "noderesid=" & lngParentResID)
            End If
            Response.Redirect(strUrl, False)
        Catch ex As Exception
            lblNotes.Text = "删除资源 (" & CmsPass.GetDataRes(lngResID).ResName & ") 失败，错误信息：" & ex.Message
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
