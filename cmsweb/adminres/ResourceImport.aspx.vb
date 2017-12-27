
Imports NetReusables
Imports System.Text
Imports System.IO
Imports System.Xml

Imports Unionsoft.Implement
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


Partial Class ResourceImport
    Inherits CmsPage

    Private mstrDataSource As String
    Private lngResID As Long
    Private lngDepID As Long

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
        If Me.IsPostBack = False Then
            lngResID = CType(Request.QueryString("mnuresid"), Long)
            lngDepID = CType(Request.QueryString("depId"), Long)
        End If
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Me.SetFocusOnTextbox("txtResName") '设置键盘光标默认选中的输入框
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click

        'If File1.PostedFile.FileName <> Nothing Or File1.PostedFile.FileName <> "" Then
            ' Dim m_upload As Uploader = New Uploader
            Dim m_file As HttpPostedFile = File1.PostedFile
        Dim ext As String = m_file.FileName.Substring(m_file.FileName.LastIndexOf(".") + 1)

        If ext.ToLower() = "xml" Then
            Try
                ' mstrDataSource = File1.PostedFile.FileName
                mstrDataSource = Server.MapPath("../temp/xml")
                If Not Directory.Exists(mstrDataSource) Then
                    Directory.CreateDirectory(mstrDataSource)
                End If
                mstrDataSource = mstrDataSource + "\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"
                m_file.SaveAs(mstrDataSource)
                Dim ResIE As New ResourceImportExport
                ResIE.ImportResource(CmsPass, RLng("mnuresid"), RLng("depId"), mstrDataSource)
                Response.Write("<script language='javascript'>alert('导入资源成功！！！'); </script>")
            Catch ex As Exception
                SLog.Err("导入资源失败 " & ex.Message)
                Response.Write("<script language='javascript'>alert('导入资源失败！！！'); </script>")
            End Try
        Else
            Response.Write("<script language='javascript'>alert('请选择.xml格式的有效的文件！！！'); </script>")
        End If
        'Else
        'Response.Write("<script language='javascript'>alert('请选择有效的文件！！！'); </script>")
        'End If
    End Sub

    Public Function FileType(ByVal fileName As String) As Boolean
        Dim name As String() = fileName.Split(CType(".", Char))
        If name(name.Length - 1).ToString() = "xml" Or name(name.Length - 1).ToString() = "XML" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        'Dim aa As String = Session("PAGE_BACKPAGE").ToString()
        Response.Redirect(Session("CMSBP_ResourceCopy").ToString(), False)
    End Sub
End Class

End Namespace
