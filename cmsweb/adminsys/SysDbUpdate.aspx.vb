Option Strict On
Option Explicit On 

Imports System.IO

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class SysDbUpdate
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblLastUpdateDate As System.Web.UI.WebControls.Label

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '��ϵͳ����Ա����ʹ�õĹ�����Ҫ���ô˷���У��
        If CmsPass.EmpIsSysAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        btnUpdateFromFile.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫ�Զ��������ݿ�ϵͳ��');")
        btnUpdateDbFromSql.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫ�ֶ��������ݿ�ϵͳ��');")

        ShowDbInfo() '��ʾ���ݿ�汾������ʱ�����Ϣ
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If CmsDatabase.GetDbConfig.Database.ToLower() = "banks1" Then
            btnUpdateMdb.Visible = True '��Banks1���ݿ���Կ�������MDB�ļ�
        Else
            btnUpdateMdb.Visible = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnUpdateFromFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFromFile.Click
        Try
            Dim strNotes As String = ""
                Dim blnOK As Boolean = CmsDbUpdate.UpdateSqlDatabase(CmsPass, CmsConfig.ProjectRootFolder & "conf\dbupdate.sql", chkCheckLastUpdateTime.Checked, DbParameter.DB_UPDATETIME, strNotes)
            txtResult.Text = strNotes
            If blnOK = True Then
                PromptMsg("����ϵͳ���ݿ�ɹ���")
            Else
                PromptMsg("����ϵͳ���ݿⲿ��ʧ�ܣ�����ϸ�鿴���½����")
            End If

            ShowDbInfo() '��ʾ���ݿ�汾������ʱ�����Ϣ
        Catch ex As Exception
            PromptMsg("�Զ��������ݿ�ϵͳ�쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    'Private Sub btnUpdateMdb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMdb.Click
    '    Try
    '        Dim strNotes As String = ""
    '        Dim blnOK As Boolean = CmsDbUpdate.UpdateMdbDatabase(CmsPass, CmsConfig.ProjectRootFolder & "data\sql\dbupdate.sql", chkCheckLastUpdateTime.Checked, strNotes)
    '        txtResult.Text = strNotes
    '        If blnOK = True Then
    '            PromptMsg("����ϵͳ���ݿ�ɹ���")
    '        Else
    '            PromptMsg("����ϵͳ���ݿⲿ��ʧ�ܣ�����ϸ�鿴���½����")
    '        End If

    '        ShowDbInfo() '��ʾ���ݿ�汾������ʱ�����Ϣ
    '    Catch ex As Exception
    '        PromptMsg("�Զ��������ݿ�ϵͳ�쳣ʧ�ܣ�", ex, True)
    '    End Try
    'End Sub

    Private Sub btnUpdateDbFromSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateDbFromSql.Click
        Try
            If txtSql.Text.Trim() = "" Then
                PromptMsg("����������Ч��SQL��䣡")
                Return
            End If

            Dim sr As New StringReader(txtSql.Text.Trim())
            Dim strNotes As String
            Dim blnUpdated As Boolean = CmsDbUpdate.UpdateOneDatabase(CmsPass, Nothing, sr, "", strNotes, False, "", DbParameter.DB_UPDATETIME)
            txtResult.Text = strNotes
            sr.Close()

            ShowDbInfo() '��ʾ���ݿ�汾������ʱ�����Ϣ
        Catch ex As Exception
            PromptMsg("�ֶ�SQL�������ݿ��쳣ʧ�ܣ�", ex, True)
        End Try
    End Sub

    Private Sub btnGetManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetManual.Click
        Try
                Dim strFile As String = "conf\dbupdate.sql"
            Dim strDbUpdateFilePath As String = CmsConfig.ProjectRootFolder & strFile
            Dim sr As New StreamReader(strDbUpdateFilePath)
            Dim strManuals As String = ""
            While True
                Dim str As String = sr.ReadLine()
                If str Is Nothing Then Exit While
                If str.IndexOf("�ֶ�����") >= 0 Then
                    strManuals &= str & Environment.NewLine
                End If
            End While
            sr.Close()

            txtSql.Text = strManuals
        Catch ex As Exception
            PromptMsg("��ȡ�ļ��쳣����", ex, True)
            SLog.Err("��ȡ�ļ��쳣����", ex)
        End Try
    End Sub

    Private Sub btnReadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadAll.Click
        Try
                Dim strFile As String = "conf\dbupdate.sql"
            Dim strDbUpdateFilePath As String = CmsConfig.ProjectRootFolder & strFile
            Dim sr As New StreamReader(strDbUpdateFilePath)
            txtSql.Text = sr.ReadToEnd()
        Catch ex As Exception
            PromptMsg("��ȡ�ļ��쳣����", ex, True)
        End Try
    End Sub

    Private Sub lbtnClearSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearSql.Click
        txtSql.Text = ""
    End Sub

    Private Sub lbtnClearResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearResult.Click
        txtResult.Text = ""
    End Sub

    '------------------------------------------------------------------------
    '��ʾ���ݿ�汾������ʱ�����Ϣ
    '------------------------------------------------------------------------
    Private Sub ShowDbInfo()
        lblDbInfo.Text = "���ݿ��ַ��" & CmsConfig.GetString("SYS_DATABASE", "HOST") & "  �������ݿ⣺" & CmsConfig.GetString("SYS_DATABASE", "DATABASE") & "  �ĵ����ݿ⣺" & CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC")
        lblDbVersion.Text = "���ݿ�汾��" & CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODVER'")
        Dim dtmUpdateTime As String = DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_UPDATETIME)
        lblManualUpdateTime.Text = "���ݹ���ϵͳ���ݿ��������ʱ�䣺" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_UPDATETIME)
        'lblFlowUpdateTime.Text = "���������ݿ��������ʱ�䣺" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_FLOWUPDATETIME)
        'lblMDBUpdateTime.Text = "MDB���ݿ��������ʱ�䣺" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_MDBUPDATETIME)
        lblLastOperateTime.Text = "���ݿ�����������ʱ�䣺" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_UPDATE_ACTIONTIME)
    End Sub
End Class

End Namespace
