Option Strict On
Option Explicit On 

Imports System.IO

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class DevMain
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
            If SStr("DEV_MANAGER") <> "1" Then
                Response.Redirect("../cmsdev/DevLogin.aspx", False)
                Return
            End If

            txtSysError.Text = CmsConfig.GetSystemError()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnSysConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSysConfig.Click
        Response.Redirect("../cmsdev/DevAppConfigAdvance.aspx", False)
    End Sub

    Private Sub lbtnSwitch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSwitch.Click
        Dim strConfFile As String = "app_function.xml"
        Response.Redirect("../cmsdev/DevAppConfig.aspx?isfrom=sysuser&conffile=" & strConfFile & "&backpage=../cmsdev/DevMain.aspx", False)
        'Response.Redirect("DevSwitch.aspx", False)
    End Sub

    Private Sub lbtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnExit.Click
        Session.Abandon() '�ͷ���Դ,����session�е����ж���
        Response.Redirect("../Default.htm", False)
    End Sub

    Private Sub lbtnAppConfig2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAppConfig2.Click
        Dim strConfFile As String = "app_config.xml"
        Response.Redirect("../cmsdev/DevAppConfig.aspx?isfrom=sysuser&conffile=" & strConfFile & "&backpage=../cmsdev/DevMain.aspx", False)
    End Sub

    Private Sub lbtnGetLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetLog.Click
        Try
            FileTransfer.DownloadLogFile(Response)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnUpdateDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnUpdateDb.Click
        Try
            Dim pst As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("")
            Dim strNotes As String = ""
            Dim blnOK As Boolean = CmsDbUpdate.UpdateSqlDatabase(pst, CmsConfig.ProjectRootFolder & "conf\dbupdate.sql", chkCheckLastUpdateTime.Checked, DbParameter.DB_UPDATETIME, strNotes)
            txtSysError.Text = strNotes
            If blnOK = True Then
                PromptMsg("�������ݿ�ɹ���")
            Else
                PromptMsg("�������ݿⲿ��ʧ�ܣ�����ϸ�鿴���½����")
            End If
        Catch ex As Exception
            PromptMsg("�������ݿ�ϵͳ�쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    'Private Sub lbtnUpdateMdb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnUpdateMdb.Click
    '    Try
    '        Dim pst As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("")
    '        Dim strNotes As String = ""
    '        Dim blnOK As Boolean = CmsDbUpdate.UpdateMdbDatabase(pst, CmsConfig.ProjectRootFolder & "data\sql\dbupdate.sql", chkCheckLastUpdateTime.Checked, strNotes)
    '        txtSysError.Text = strNotes
    '        If blnOK = True Then
    '            PromptMsg("����MDB���ݿ�ɹ���")
    '        Else
    '            PromptMsg("����MDB���ݿⲿ��ʧ�ܣ�����ϸ�鿴���½����")
    '        End If
    '    Catch ex As Exception
    '        PromptMsg("����MDB���ݿ�ϵͳ�쳣ʧ�ܣ�", ex, True)
    '    End Try
    'End Sub

    Private Sub lbtnGetUpdateTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetUpdateTime.Click
        Dim dtmUpdateTime As String = DbParameter.GetDateTime(CmsDatabase.GetDbConfig(), DbParameter.DB_UPDATETIME)
        PromptMsg("���¸���ʱ�䣺" & dtmUpdateTime)
    End Sub

    Private Sub lbtnClearNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearNotes.Click
        txtSysError.Text = ""
    End Sub

    Private Sub lbtnDebug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDebug.Click
        Session("CMSBP_SysDebug") = "../cmsdev/DevMain.aspx"
        Response.Redirect("../adminsys/SysDebug.aspx", False)
    End Sub

    Protected Sub ExchangeDocumentCenter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim DatabaseName As String = CmsConfig.GetConfig().GetString("SYS_DATABASE", "DATABASEDOC")
        If DatabaseName = "" Then
            txtSysError.Text = "δָ��ԭ�ĵ��Ĵ�����ݿ���������app_config.xml������SYS_DATABASE\DATABASEDOC��ֵ."
            Return
        End If

        If SQLObjectExists("Cms_DocumentCenter") = False Then
            txtSysError.Text = "��Cms_DocumentCenter������,����ִ�����ݿ����."
            Return
        End If

        ExchangeDocumentCenter(DatabaseName)
    End Sub

    Private Sub ExchangeDocumentCenter(ByVal DatabaseName As String)
        Dim strSql As String = ""

        strSql &= "INSERT INTO Cms_DocumentCenter (DocHostName,DOC2_ID,DOC2_RESIDS,DOC2_CRTID,DOC2_CRTTIME,DOC2_EDTID,DOC2_EDTTIME,DOC2_FILE_TEXT,DOC2_NAME,DOC2_EXT,DOC2_SIZE,DOC2_COMPRESSED_SIZE,DOC2_COMPRESSED_RATE,DOC2_COMMENTS,DOC2_STATUS,DOC2_CONVERTED,DOC2_SHARES,DOC2_LCYC_ENABLE,DOC2_LCYC_EXPIRE_DATE,DOC2_LCYC_EXPIRE_DAYS,DOC2_LCYC_LASTDO_TIME,DOC2_KEYWORDS,DOC2_RESID1,DOC2_RESID2,DOC2_RESID3,DOC2_RESID4,DOC2_RESID5)"
        strSql &= "(SELECT '" & DatabaseName & ".dbo.CMS_DOC',DOC2_ID,DOC2_RESIDS,DOC2_CRTID,DOC2_CRTTIME,DOC2_EDTID,DOC2_EDTTIME,DOC2_FILE_TEXT,DOC2_NAME,DOC2_EXT,DOC2_SIZE,DOC2_COMPRESSED_SIZE,DOC2_COMPRESSED_RATE,DOC2_COMMENTS,DOC2_STATUS,DOC2_CONVERTED,DOC2_SHARES,DOC2_LCYC_ENABLE,DOC2_LCYC_EXPIRE_DATE,DOC2_LCYC_EXPIRE_DAYS,DOC2_LCYC_LASTDO_TIME,DOC2_KEYWORDS,DOC2_RESID1,DOC2_RESID2,DOC2_RESID3,DOC2_RESID4,DOC2_RESID5 "
        strSql &= "FROM " & DatabaseName & ".dbo.CMS_DOC "
        strSql &= "WHERE NOT DOC2_ID IN (SELECT DOC2_ID FROM Cms_DocumentCenter))"

        Try
            SDbStatement.Execute(strSql)
        Catch ex As Exception
            txtSysError.Text = "�������ݷ�������:" & ex.Message & vbCrLf
            txtSysError.Text &= ex.ToString()
            Return
        End Try
        txtSysError.Text = "���³ɹ�!"
    End Sub

    '��֤SQL�����Ƿ���ڣ� ��SQL������� ����ͼ���洢���̵�
    Private Function SQLObjectExists(ByVal name As String) As Boolean
        Dim strSql As String = "select * from sysobjects where name='" & name & "'"
        If SDbStatement.Query(strSql).Tables(0).Rows.Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class

End Namespace
