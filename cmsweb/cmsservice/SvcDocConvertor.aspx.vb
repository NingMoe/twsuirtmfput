'----------------------------------------------------------------------
'��WEB�ӿڷ�ʽ�����ṩ��ϵͳ������Ϣ
'----------------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SvcDocConvertor
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
            '------------------------------------------------------------------

            '------------------------------------------------------------------
            '��ʼ��װϵͳ��Ϣ
            Dim strResp As String = "<!--;;"

            '��ȡϵͳ������Ϣ
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
