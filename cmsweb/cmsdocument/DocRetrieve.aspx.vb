Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocRetrieve
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
        Dim lngResID As Long = RLng("mnuresid")
        Dim lngRecID As Long = RLng("mnurecid")
        If lngRecID = 0 Then Throw New CmsException("请选择需要操作的文档！")

        Dim strCmd As String = RStr("cmsaction")
        Select Case strCmd
            Case "MenuDocCheckout"
                DocCheckout(CmsPass, lngResID, lngRecID)

            Case "MenuDocGet"
                DocGet(CmsPass, lngResID, lngRecID)
        End Select
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '----------------------------------------------------------
    '文档签出
    '----------------------------------------------------------
    Protected Sub DocCheckout(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long)
        Dim strStatus As String = ResFactory.TableService(pst.GetDataRes(lngResID).ResTableType).GetStatus(pst, lngResID, lngRecID)
        If strStatus = DocVersion.StatusCheckout Then Throw New CmsException("文件处签出状态，不能重复签出！") '已经是Checkout状态

        Try
            Dim datDoc As DataDocument = ResFactory.TableService(pst.GetDataRes(lngResID).ResTableType).Checkout(pst, lngResID, lngRecID)
                FileTransfer.DownloadDoc(Response, datDoc)
        Catch ex As Exception
            '线程正被中止，不做任何操作
            'CmsError.ThrowEx("签出文档失败", ex, True)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    '----------------------------------------------------------
    '在线下载文档
    '----------------------------------------------------------
    Protected Sub DocGet(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long)
        Try
            Dim datDoc As DataDocument = ResFactory.TableService(pst.GetDataRes(lngResID).ResTableType).GetDocument(pst, lngResID, lngRecID, True)
                FileTransfer.DownloadDoc(Response, datDoc)
        Catch ex As Exception
            '线程正被中止，不做任何操作
            'CmsError.ThrowEx("下载文档失败", ex, True)
        End Try
    End Sub
End Class

End Namespace
