Option Strict On
Option Explicit On 

Imports System.IO
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceExportResult
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
        lblResult.Text = SStr("CMS_IMPRES_MESSAGE")

        If RLng("result") <> 1 Then
            btnDownload.Enabled = False
        Else
            btnDownload.Enabled = True
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DownloadDoc()
    End Sub

    Public Sub DownloadDoc()
        Try
            '必须Encode文件名称，否则乱码
            Dim strFilePath As String = SStr("CMSTRANS_EXPORTFILE")
            Dim strFileName As String = Path.GetFileNameWithoutExtension(strFilePath) ' & "." & Path.GetExtension(strFilePath)
            strFileName = strFileName.Substring(0, strFileName.IndexOf("__"))
            strFileName = strFileName & Path.GetExtension(strFilePath)
            strFileName = HttpUtility.UrlEncode(strFileName)
            Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
            Response.ContentType = "application/octet-stream"

            '读取文件
            Dim fs As FileStream = New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)

            '开始向客户端写文件流
            Response.BinaryWrite(br.ReadBytes(CInt(fs.Length)))
            Response.Flush()

            '务必再Response.End()之前调用
            br.Close()
            fs.Close()

            Response.End()
        Catch ex As Exception
            '线程正被中止，不做任何操作
            '调用Response.End()后，必然到达这里
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
