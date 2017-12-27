'----------------------------------------------------------------------
'以WEB接口方式对外提供本系统基本信息
'----------------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SvcBatchSend
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
            Dim lngResID As Long = RLng("mnuresid")
            Dim strEmailOrSms As String = RStr("action")
            If strEmailOrSms <> "email" And strEmailOrSms <> "sms" Then
                Return '校验失败
            End If
            '------------------------------------------------------------------

            '------------------------------------------------------------------
            '开始组装系统信息
            Dim strResp As String = "<!--;;"

            '获取系统基本信息
            Dim datBatSend As DataBatchSend = Nothing
            If strEmailOrSms = "email" Then
                datBatSend = BatchSendStore.GetEmailData()
            ElseIf strEmailOrSms = "sms" Then
                datBatSend = BatchSendStore.GetSmsData()
            End If

            strResp &= "[empid]=" & datBatSend.EmpID & ";;"

            strResp &= "[mnuresid]=" & datBatSend.ResID & ";;"
            strResp &= "[restable]=" & datBatSend.ResTable & ";;"
            strResp &= "[col_email]=" & datBatSend.ColNameOfEmail & ";;"
            strResp &= "[col_phone]=" & datBatSend.ColNameOfPhone & ";;"

            strResp &= "[status_resid]=" & datBatSend.StatusResID & ";;"
            strResp &= "[status_restable]=" & datBatSend.StatusResTable & ";;"
            strResp &= "[status_col_email]=" & datBatSend.StatusColNameOfEmail & ";;"
            strResp &= "[status_col_phone]=" & datBatSend.StatusColNameOfSms & ";;"

            strResp &= "[msg_success]=" & datBatSend.MsgOfSentSuccess & ";;"
            strResp &= "[msg_fail]=" & datBatSend.MsgOfSentFail & ";;"

            strResp &= "[subject]=" & datBatSend.Subject & ";;"
            strResp &= "[body]=" & datBatSend.Body & ";;"

            '为跳高检索效率，将记录ID列表放在最后
            strResp &= "[recids]=" & datBatSend.RecIDs.ToString() & ";;"

            strResp &= "-->"

            'SLog.Info("test resp数据：" & strResp)
            '------------------------------------------------------------------

            Response.Write(strResp)
        Catch ex As Exception
            SLog.Err(ex.Message, ex)
        End Try
    End Sub
End Class

End Namespace
