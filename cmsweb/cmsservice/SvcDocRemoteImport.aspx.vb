Option Strict On
Option Explicit On 

Imports System.IO
Imports System.Web

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SvcDocRemoteImport
    Inherits AspPage

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

    Private Const MSG_SIGN1 As String = "<!--[[CMS_RESPONSE_MSG=" '"[[CMS_RESPONSE_MSG="
    Private Const MSG_SIGN2 As String = "]]-->" '"]]"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IsPostBack Then Return

            ''----------------------------------------------------------
            ''检验是否有文档上传，若没有则退出
            'Dim lngFileNum As Long = Request.Files.Count()
            'If lngFileNum <= 0 Then
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("没有文档传入"))
            '    Return
            'ElseIf lngFileNum > 1 Then
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("不能同时导入多个文档"))
            '    Return
            'End If
            ''----------------------------------------------------------

            ''----------------------------------------------------------
            ''检验用户登陆帐号和密码
            'Dim pst As New CmsPassport
            'pst.EmpID = RStr("UFILE_USERID")
            'pst.EmpPass = RStr("UFILE_USERPASS")
            ''----------------------------------------------------------

            ''----------------------------------------------------------
            ''获取文档上传的资源ID和表名
            'Dim strCmd As String = RStr("UFILE_CMD")
            'Dim lngResID As Long = RLng("UFILE_RESID")
            'If lngResID <= 0 Then Return
            'Dim strResHostTable As String = ResFactory.ResService.GetResTable(lngResID)
            ''----------------------------------------------------------

            ''----------------------------------------------------------
            ''放置空的文档说明信息
            'Dim hashFieldVal As New Hashtable
            'hashFieldVal.Add("DOC2_COMMENTS", "")
            'hashFieldVal.Add("DOC2_KEYWORDS", "")
            ''----------------------------------------------------------

            ''添加文档至数据库中
            'If strCmd = "checkin" Then '是签入
            '    ResFactory.TableService("DOC").CheckinByName(pst, lngResID, Request.Files(0).InputStream, Request.Files(0).FileName)
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("签入文档成功"))
            'ElseIf strCmd = "addnew" Then '导入、新增
            '    ResFactory.TableService("DOC").AddRecord(pst, lngResID, 0, hashFieldVal, Nothing, Request.Files(0).InputStream, Request.Files(0).FileName)
            '    Response.Write(TransportMessage.DocuploadGenerateRespMsg("导入文档成功"))
            'End If
        Catch ex As Exception
            Response.Write(DocuploadGenerateRespMsg(ex.Message))
            SLog.Err("上传文档失败！", ex)
        End Try
    End Sub

    Public Shared Function DocuploadGenerateRespMsg(ByVal strRespMsg As String) As String
        Return MSG_SIGN1 & strRespMsg & MSG_SIGN2
    End Function

    Public Shared Function DocuploadRetrieveRespMsg(ByVal strRespMsg As String) As String
        Dim pos1 As Integer = strRespMsg.IndexOf(MSG_SIGN1)
        Dim pos2 As Integer = strRespMsg.IndexOf(MSG_SIGN2, pos1)
        Return strRespMsg.Substring(pos1 + MSG_SIGN1.Length, pos2 - pos1 - MSG_SIGN1.Length)
    End Function
End Class

End Namespace
