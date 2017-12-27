Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SysConfigUpdate
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

    Protected Overrides Sub CmsPageInitialize()
        '仅系统管理员可以使用的功能需要调用此方法校验
        If CmsPass.EmpIsSysAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        ddlFileType.Items.Clear()
        ddlFileType.Items.Add("")
        ddlFileType.Items.Add("系统配置文件")
        ddlFileType.Items.Add("客户配置文件")
        ddlFileType.Items.Add("sql")
        ddlFileType.Items.Add("bin")
        ddlFileType.Items.Add("css")
        ddlFileType.Items.Add("script")
        ddlFileType.Items.Add("images")
        ddlFileType.Items.Add("help")
    End Sub


    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            '获取上传文件的目录
            Dim strDestFileFolder As String = CmsConfig.ProjectRootFolder
            If ddlFileType.SelectedValue <> "" Then
                Select Case ddlFileType.SelectedValue
                    Case "系统配置文件"
                        strDestFileFolder &= "conf\"
                    Case "客户配置文件"
                        strDestFileFolder &= "conf\client\" & CmsConfig.GetClientCode() & "\"
                    Case "sql"
                        strDestFileFolder &= "sql\"
                    Case "script"
                        strDestFileFolder &= "script\"
                    Case "css"
                        strDestFileFolder &= "css\"
                    Case "bin"
                        strDestFileFolder &= "bin\"
                    Case "images"
                        strDestFileFolder &= "images\"
                    Case "help"
                        strDestFileFolder &= "help\"
                End Select
            ElseIf txtFolder.Text.Trim() <> "" Then
                Dim strTemp As String = txtFolder.Text.Trim()
                strTemp = strTemp.Replace("/", "\")
                strTemp = StringDeal.Trim(strTemp, "\", "\")
                strTemp = StringDeal.Trim(strTemp, "\", "\")
                strDestFileFolder &= strTemp & "\"
            Else
                PromptMsg("请选择需要上传的文件类型")
                Return
            End If
            Dim strSrcFileName As String = Path.GetFileName(Request.Files(0).FileName)
            Dim strDestFilePath As String = strDestFileFolder & strSrcFileName

            '获取上传的文件内容
            If Request.Files Is Nothing Then
                PromptMsg("请上传正确的文件！")
                Return
            End If
            If Request.Files.Count <= 0 Then
                PromptMsg("请上传正确的文件！")
                Return
            End If
            Dim sm As System.IO.Stream = Request.Files(0).InputStream
            If sm Is Nothing Then
                PromptMsg("请上传正确的文件！")
                Return
            End If
            If sm.Length <= 0 Then
                PromptMsg("请上传正确的文件！")
                Return
            End If

            '写入目标文件
            Dim br As BinaryReader = New BinaryReader(sm)
            Dim binFile() As Byte = br.ReadBytes(CInt(sm.Length))
            Dim fs As FileStream = New FileStream(strDestFilePath, FileMode.Create, FileAccess.Write)
            fs.Write(binFile, 0, binFile.Length)
            fs.Flush()
            fs.Close()

            CmsConfig.ReloadAll()
            PromptMsg("系统配置信息上传成功！")
        Catch ex As Exception
            PromptMsg("文件使用中，上传失败！", ex, True)
        End Try
    End Sub
End Class

End Namespace
