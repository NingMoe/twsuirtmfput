'----------------------------------------------------------------------
'以WEB接口方式对外提供本系统基本信息
'----------------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SvcDocConvertor
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IsPostBack Then Return

            '------------------------------------------------------------------
            '检验是否是本系统用户登录
            Dim strPass As String = RStr("websvcpass")
            If strPass <> "svcpass1234" Then
                Return '校验失败
            End If
            '------------------------------------------------------------------

            '------------------------------------------------------------------
            '开始组装系统信息
            Dim strResp As String = "<!--;;"

            '获取系统基本信息
            'Dim intSysDbType As Integer = CmsConfig.GetInt("SYS_CONFIG", "SYSDB_TYPE")
            strResp &= "[projfolder]=" & CmsConfig.ProjectRootFolder & ";;"
            'strResp &= "[sysdb_type]=" & intSysDbType & ";;"
            'If intSysDbType = 0 Then
            '    strResp &= "[docdb]=" & CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC") & ";;"
            '    strResp &= "[docdblink]=" & CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC") & ".DBO." & CmsTables.CmsDoc & ";;"
            '    strResp &= "[mdbfile]=" & "" & ";;"
            'ElseIf intSysDbType = 1 Then
            '    strResp &= "[docdb]=" & CmsTables.CmsDoc & ";;"
            '    strResp &= "[docdblink]=" & CmsTables.CmsDoc & ";;"
            '    strResp &= "[mdbfile]=" & CmsConfig.GetString("SYS_DATABASE", "DATABASEMDB") & ";;"
            'ElseIf intSysDbType = 2 Then
            '    strResp &= "[docdb]=" & CmsTables.CmsDoc & ";;"
            '    strResp &= "[docdblink]=" & CmsTables.CmsDoc & ";;"
            '    strResp &= "[mdbfile]=" & "" & ";;"
            'End If


            strResp &= "-->"
            '------------------------------------------------------------------

            Response.Write(strResp)
        Catch ex As Exception
            SLog.Err(ex.Message, ex)
        End Try
    End Sub
End Class

End Namespace
