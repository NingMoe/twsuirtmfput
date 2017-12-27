'----------------------------------------------------------------------
'��WEB�ӿڷ�ʽ�����ṩ��ϵͳ������Ϣ
'----------------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SvcBatchSend
    Inherits AspPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IsPostBack Then Return

            '------------------------------------------------------------------
            '�����Ƿ��Ǳ�ϵͳ�û���¼
            Dim strPass As String = RStr("websvcpass")
            If strPass <> "svcpass1234" Then
                Return 'У��ʧ��
            End If
            Dim lngResID As Long = RLng("mnuresid")
            Dim strEmailOrSms As String = RStr("action")
            If strEmailOrSms <> "email" And strEmailOrSms <> "sms" Then
                Return 'У��ʧ��
            End If
            '------------------------------------------------------------------

            '------------------------------------------------------------------
            '��ʼ��װϵͳ��Ϣ
            Dim strResp As String = "<!--;;"

            '��ȡϵͳ������Ϣ
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

            'Ϊ���߼���Ч�ʣ�����¼ID�б�������
            strResp &= "[recids]=" & datBatSend.RecIDs.ToString() & ";;"

            strResp &= "-->"

            'SLog.Info("test resp���ݣ�" & strResp)
            '------------------------------------------------------------------

            Response.Write(strResp)
        Catch ex As Exception
            SLog.Err(ex.Message, ex)
        End Try
    End Sub
End Class

End Namespace
