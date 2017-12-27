'===========================================================================
' 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
' 类名已更改，且类已修改为从文件“App_Code\Migrated\cmsdocument\Stub_doccheckin_aspx_vb.vb”的抽象基类 
' 继承。
' 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
' 代码隐藏页。
' 关联的内容页“cmsdocument\doccheckin.aspx”也已修改，以引用新的类名。
' 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Option Strict On
Option Explicit On 

Imports System.IO
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


'Partial Class DocCheckin
Partial Class Migrated_DocCheckin

Inherits DocCheckin

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

        Private strOperation As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_RECID") = 0 Then
            ViewState("PAGE_RECID") = RLng("mnurecid")
            End If

            If Request("operation") IsNot Nothing Then strOperation = Request("operation")

            If strOperation.Trim <> "" Then
                btnCheckin.Text = "上传"
                lblTitle.Text = "上传文档"
            End If
        End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnCheckin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckin.Load
        Dim m_button As Button = CType(sender, Button)
            'Dim m_upload As Uploader = New Uploader
            'm_upload.RegisterProgressBar(m_button)
    End Sub
        Private Sub btnCheckin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckin.Click
            Try
                'If File1.PostedFile Is Nothing Then
                '    '没有文件上传，Checkin失败
                'Else
                '    If File1.PostedFile.InputStream.Length > 0 Then
                '        ResFactory.TableService("DOC").Checkin(CmsPass(), VLng("PAGE_RESID"), VLng("PAGE_RECID"), File1.PostedFile.InputStream, File1.PostedFile.FileName)
                '    Else
                '        '"错误信息：没有文件上传！"
                '    End If
                'End If
                ' Dim lngResID As Long = TextboxName.GetResID(strFileCtrlName)
                'Dim m_upload As Uploader = New Uploader
                Dim m_file As HttpPostedFile = File1.PostedFile

                Dim s As Stream = m_file.InputStream
                ResFactory.TableService("DOC").Checkin(CmsPass(), VLng("PAGE_RESID"), VLng("PAGE_RECID"), s, m_file.FileName.Substring(m_file.FileName.LastIndexOf("\") + 1), , , strOperation)
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try
        End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
