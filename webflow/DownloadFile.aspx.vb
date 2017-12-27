Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web



Partial Class DownloadFile
    Inherits PageBase


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

        Dim lngResourceID As Long = CLng(Request.QueryString("ResourceID"))
        Dim lngDocumentID As Long = CLng(Request.QueryString("DocumentID"))
        Try
            Dim datDoc As DataDocument
            datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
            'datDoc = CmsDocFlow.GetOneAttachment((New CmsResource(MyBase.CurrentUser.Code, MyBase.CurrentUser.Password)).GetPst, lngResourceID, lngDocumentID, True)
            'Dim datDoc As DataDocument = ResFactory.TableService(New CmsResource(MyBase.CurrentUser.Code, MyBase.CurrentUser.Password)).GetPst.GetDataRes(lngResourceID).ResTableType).GetDocument(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
            FileTransfer.DownloadDoc(Response, datDoc)
        Catch ex As Exception
            SLog.Err("下载附件发生异常.", ex)
        End Try

    End Sub

End Class

End Namespace
