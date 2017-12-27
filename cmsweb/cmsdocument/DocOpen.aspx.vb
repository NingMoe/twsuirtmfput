Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class DocOpen
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
        '获取文档内容
        Dim lngResID As Long = RLng("mnuresid")
        If lngResID = 0 Then
            lngResID = RLng("resid")
        End If
        Dim lngRecID As Long = RLng("docrecid")
        If lngRecID = 0 Then
            lngRecID = RLng("mnurecid")
        End If
        Dim datDoc As DataDocument = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetDocument(CmsPass, lngResID, lngRecID, True)

        '显示文档内容
            Dim lngDocOpenStyle As Long = RLng("docopenstyle")
            If lngDocOpenStyle = 1 Then '提取文档
                FileTransfer.DownloadDoc(Response, datDoc)
            ElseIf lngDocOpenStyle = 2 Then '在线浏览文档
                FileTransfer.ShowDoc(Response, datDoc)
            Else '由配置文件信息决定如何打开文档
                If CmsConfig.GetInt("SYS_CONFIG", "DOCFILE_GET_STYLE") = 0 Then
                    FileTransfer.DownloadDoc(Response, datDoc)
                Else
                    FileTransfer.ShowDoc(Response, datDoc)
                End If
            End If

    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
