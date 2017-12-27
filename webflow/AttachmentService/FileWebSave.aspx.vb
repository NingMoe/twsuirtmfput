Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform
'Imports Unionsoft.WebControls.Uploader 'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Workflow.Web


Partial Class upload
    Inherits System.Web.UI.Page

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

    Private lngResourceID As Long
    Private lngDocumentID As Long

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            lngResourceID = CType(Request.QueryString("ResourceID"), Long)
            lngDocumentID = CType(Request.QueryString("DocumentID"), Long)

            If Request.Files.Count = 1 Then
                Dim CmsRes As New CmsResource
                CmsRes.CheckInFile(lngResourceID, lngDocumentID, Request.Files(0).InputStream, Request.Files(0).FileName)
                Response.Write("OK!")
                SLog.Info("OK!")
                SLog.Info(Request.Files(0).FileName)
            Else
                Response.Write("failed!")
                SLog.Info("failed!")
            End If
        Catch ex As Exception
            SLog.Err(ex.ToString())
            Response.Write(ex.ToString())
            Response.Write("<br>")
            Response.Write(Request.Files(0).FileName)
        End Try

    End Sub

End Class

End Namespace
