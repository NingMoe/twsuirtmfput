Option Strict On
Option Explicit On 

Imports System.IO
Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SetupDatabase
    Inherits AspPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCms1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCms2 As System.Web.UI.WebControls.TextBox

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'CmsEnvironment.Init(Request.PhysicalApplicationPath, "cms.log", "dbsql.log", "..\cmsdoc\log\", True)
        'Response.Write("<br>----------------------------------------<br>")
        'Response.Write(CmsConfig.ProjectRootFolder)
        'Response.Write("<br>----------------------------------------<br>")
        'Response.Write(CmsConfig.TempFolder)
        'Response.End()
        Try
            If IsPostBack Then Return

            txtDb1.Text = CmsConfig.GetString("SYS_DATABASE", "DATABASE")
            txtDb2.Text = CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC")

            'Dim intAttachDb As Integer = CmsConfig.GetInt("SYS_DATABASE", "SETUP_DBATTACH")
            'If intAttachDb = 0 Then
            '    '�½����ݿ⣬�ж����ݿ�ϵͳ�Ƿ��Ѿ�����
            '    If CmsDatabase.IsDbInitialized(CmsDatabase.GetDbConfig()) = True Then
            '        PromptMsg("��ͬ���Ƶ����ݿ��Ѿ����ڣ���Ҫ�½����ݿ������޸��������ݿ���ĵ����ݿ����ƺ󵥻�""ȷ��""�����ʹ���Ѵ��ڵ����ݿ⣬���뵥��""��һ��""��")
            '        Return
            '    End If
            'Else
            '    '�������ݿ�ʱ�������޸ĸ������ݿ�����
            '    lblDb1Comments.Text = "���������ݿ�ģ�壩"
            '    lblDb2Comments.Text = "���������ݿ�ģ�壩"
            '    txtDb1.Enabled = False
            '    txtDb2.Enabled = False
            'End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect("/cmsweb/Default.htm", False)
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            '�½����ݿ⣬�ж����ݿ�ϵͳ�Ƿ��Ѿ�����
            If CmsDatabase.IsDbInitialized(CmsDatabase.GetDbConfig()) = True Then
                PromptMsg("��ͬ���Ƶ����ݿ��Ѿ����ڣ���Ҫ�½����ݿ������޸��������ݿ���ĵ����ݿ����ƣ�")
                Return
            End If

            '�޸����ݿ������������ļ���
            CmsConfig.SetString("SYS_DATABASE", "DATABASE", txtDb1.Text.Trim())
            CmsConfig.SetString("SYS_DATABASE", "DATABASEDOC", txtDb2.Text.Trim())
            CmsDatabase.InitialCmsDatabase()

            '�½����ݿ�
            Dim bln As Boolean = CreateNewDatabase()
            If bln = True Then
                Response.Redirect("/cmsweb/Default.htm", False)
            Else
                PromptMsg("�������ݿ�ʧ�ܣ�")
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '----------------------------------------------------------------
    '��ʼ�����ݿ�
    '----------------------------------------------------------------
    Private Function CreateNewDatabase() As Boolean
        Dim dbInit As New DbDatabase
        dbInit.DbHost = txtHost.Text.Trim()
        dbInit.DbPort = txtPort.Text.Trim()
        If dbInit.DbHost = "" Or dbInit.DbPort = "" Then
            Throw New CmsException("�����������ݿ��ַ�Ͷ˿���Ϣ��")
        End If

        Dim strConfFolder As String = CmsConfig.ProjectRootFolder & "data\sql\"

        '--------------------------------------------------------------------
        '��ȡ�ͼ������
        Dim strDbName1 As String = txtDb1.Text.Trim()
        Dim strDbName2 As String = txtDb2.Text.Trim()
        If strDbName1 = "" Or strDbName2 = "" Then
            Throw New CmsException("���������������ݿ���ĵ����ݿ����ƣ�")
        End If

        '�������ݿ�����Ŀ¼
        Dim strDbFilePath As String = CmsConfig.ProjectRootFolder & "..\..\db\"
        If Directory.Exists(strDbFilePath) = False Then
            Directory.CreateDirectory(strDbFilePath)
        End If
        '--------------------------------------------------------------------

        '----------------------------------------------------------------
        '�������ݿ�
        Dim strSetupInfo As String = ""
        Try
            dbInit.LoginID = "sa"
            dbInit.LoginPass = txtSaPass.Text.Trim()
            dbInit.CreateDatabase(strDbName1, strDbFilePath, strDbFilePath)
            strSetupInfo &= "�������ݿ�" & strDbName1 & "�ɹ���" & Environment.NewLine
            dbInit.CreateDatabase(strDbName2, strDbFilePath, strDbFilePath)
            strSetupInfo &= "�������ݿ�" & strDbName2 & "�ɹ���" & Environment.NewLine
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "�������ݿ�ʧ�ܣ�������Ϣ��" & ex.Message & Environment.NewLine
            txtSetupInfo.Text = strSetupInfo
            Return False
        End Try
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '�����û�
        Try
            dbInit.AddLogin(CmsDatabase.DbUser, CmsDatabase.DbUserPass, strDbName1)
            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName1)
            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName2)
            strSetupInfo &= Environment.NewLine & "���ݿ��û������ɹ���" & Environment.NewLine
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "���ݿ��û�cmsuser����ʧ�ܣ�������û��Ѿ����ڲ���������ȷ�����룬������Դ˴�����Ϣ��" & Environment.NewLine
        End Try
        '----------------------------------------------------------------

        '--------------------------------------------------------------------
        '��������ϵͳ��
        Try
            dbInit.LoginID = CmsDatabase.DbUser
            dbInit.LoginPass = CmsDatabase.DbUserPass

            '������1�����б�
            Dim strScriptFile As String = strConfFolder & "cms1_tables.sql"
            Dim fs As FileStream = File.OpenRead(strScriptFile)
            Dim sr As StreamReader = New StreamReader(fs, Encoding.UTF8)
            fs.Seek(0, SeekOrigin.Begin)
            Dim strScripts As String = sr.ReadToEnd()
            sr.Close()
            If strScripts <> "" Then dbInit.Execute(strScripts, strDbName1)

            '������2�����б�
            strScriptFile = strConfFolder & "cms2_tables.sql"
            fs = File.OpenRead(strScriptFile)
            sr = New StreamReader(fs, Encoding.UTF8)
            fs.Seek(0, SeekOrigin.Begin)
            strScripts = sr.ReadToEnd()
            sr.Close()
            If strScripts <> "" Then dbInit.Execute(strScripts, strDbName2)

            strSetupInfo &= Environment.NewLine & "�������ݿ���ɹ���" & Environment.NewLine
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "�������ݿ��ʧ�ܣ�������Ϣ��" & ex.Message & Environment.NewLine
            txtSetupInfo.Text = strSetupInfo
            Return False
        End Try
        '--------------------------------------------------------------------

        Try
            '--------------------------------------------------------------------
            '��ʼ������ϵͳ���ĳ�ʼ��¼
            dbInit.LoginID = CmsDatabase.DbUser
            dbInit.LoginPass = CmsDatabase.DbUserPass

            Dim strScriptFile As String = strConfFolder & "cms1_initvalue.sql"
            Dim fs As FileStream = File.OpenRead(strScriptFile)
            Dim sr As StreamReader = New StreamReader(fs, Encoding.UTF8)
            fs.Seek(0, SeekOrigin.Begin)
            Dim strScripts As String = sr.ReadToEnd()
            sr.Close()
            If strScripts <> "" Then dbInit.Execute(strScripts, strDbName1)

            strSetupInfo &= Environment.NewLine & "��ʼ��ϵͳ����¼�ɹ���" & Environment.NewLine
            '--------------------------------------------------------------------
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "��ʼ��ϵͳ����¼ʧ�ܣ�������Ϣ��" & ex.Message & Environment.NewLine
            txtSetupInfo.Text = strSetupInfo
            Return False
        End Try


        strSetupInfo &= Environment.NewLine & "ϵͳ���ݿ��ʼ���ɹ���" & Environment.NewLine
        txtSetupInfo.Text = strSetupInfo

        Return True
    End Function

    'Private Sub btnAttachDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachDb.Click
    '    Try
    '        '�������ݿ�
    '        Dim strDbSource1 As String = CmsConfig.GetString("SYS_DATABASE", "DATABASE")
    '        Dim strDbSource2 As String = CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC")
    '        Dim bln As Boolean = AttachDatabase(strDbSource1, strDbSource2)
    '        If bln = True Then
    '            '���ݿ��ʼ���ɹ�����½�����ʾ
    '            CmsConfig.SetString("SYS_DATABASE", "DATABASE", txtDb1.Text.Trim())
    '            CmsConfig.SetString("SYS_DATABASE", "DATABASEDOC", txtDb2.Text.Trim())
    '            CmsDatabase.InitialCmsDatabase()
    '            'Response.Redirect("/cmsweb/cmssetup/SetupEnd.aspx", False)
    '            Response.Redirect("/cmsweb/Default.aspx", False)
    '        Else
    '            PromptMsg("�������ݿ�ʧ�ܣ�")
    '        End If
    '    Catch ex As Exception
    '        PromptMsg("", ex, True)
    '    End Try
    'End Sub

    'Private Sub btnChangeDbInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeDbInfo.Click
    '    Try
    '        CmsConfig.SetString("SYS_DATABASE", "HOST", txtHost.Text.Trim())
    '        CmsConfig.SetString("SYS_DATABASE", "PORT", txtPort.Text.Trim())
    '        CmsConfig.SetString("SYS_DATABASE", "DATABASE", txtDb1.Text.Trim())
    '        CmsConfig.SetString("SYS_DATABASE", "DATABASEDOC", txtDb2.Text.Trim())
    '        CmsDatabase.InitialCmsDatabase()
    '        Response.Redirect("/cmsweb/Default.htm", False)
    '    Catch ex As Exception
    '        PromptMsg("�������ݿ�ʧ�ܣ�")
    '    End Try
    'End Sub

    ''----------------------------------------------------------------
    ''�������ݿ�
    ''----------------------------------------------------------------
    'Private Function AttachDatabase(ByVal strDbSource1 As String, ByVal strDbSource2 As String) As Boolean
    '    Dim strSetupInfo As String = ""

    '    Try
    '        '----------------------------------------------------------------
    '        '��װ�������ݿ�Ļ�����Ϣ
    '        Dim strDbFolder As String = CmsConfig.ProjectRootFolder & "..\..\db\"
    '        Dim strDbName1 As String = txtDb1.Text.Trim()
    '        Dim strDbName2 As String = txtDb2.Text.Trim()
    '        If strDbName1 = "" Or strDbName2 = "" Then
    '            Throw New CmsException("�����������ݺ��ĵ����ݿ����ƣ�")
    '        End If

    '        Dim strDb1DataFile As String = strDbFolder & strDbSource1 & "_Data.mdf"
    '        Dim strDb1LogFile As String = strDbFolder & strDbSource1 & "_Log.ldf"
    '        Dim strDb2DataFile As String = strDbFolder & strDbSource2 & "_Data.mdf"
    '        Dim strDb2LogFile As String = strDbFolder & strDbSource2 & "_Log.ldf"
    '        '----------------------------------------------------------------

    '        '----------------------------------------------------------------
    '        '�������ݿ�
    '        Dim dbInit As New DbDatabase
    '        dbInit.DbHost = txtHost.Text.Trim()
    '        dbInit.DbPort = txtPort.Text.Trim()
    '        If dbInit.DbHost = "" Or dbInit.DbPort = "" Then
    '            Throw New CmsException("�����������ݿ��ַ�Ͷ˿���Ϣ��")
    '        End If
    '        dbInit.LoginID = "sa"
    '        dbInit.LoginPass = txtSaPass.Text.Trim()

    '        dbInit.AttachDatabase(strDbName1, strDb1DataFile, strDb1LogFile)
    '        strSetupInfo &= "�������ݿ�(" & strDbName1 & ")�ɹ���" & Environment.NewLine
    '        dbInit.AttachDatabase(strDbName2, strDb2DataFile, strDb2LogFile)
    '        strSetupInfo &= "�������ݿ�(" & strDbName2 & ")�ɹ���" & Environment.NewLine
    '        '----------------------------------------------------------------

    '        '----------------------------------------------------------------
    '        '�����û�
    '        Try
    '            dbInit.AddLogin(CmsDatabase.DbUser, CmsDatabase.DbUserPass, strDbName1)
    '            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName1)
    '            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName2)
    '            strSetupInfo &= Environment.NewLine & "���ݿ��û������ɹ���" & Environment.NewLine
    '        Catch ex As Exception
    '            strSetupInfo &= Environment.NewLine & "���ݿ��û�cmsuser����ʧ�ܣ�������û��Ѿ����ڲ���������ȷ�����룬������Դ˴�����Ϣ��" & Environment.NewLine
    '        End Try
    '        '----------------------------------------------------------------

    '        strSetupInfo &= Environment.NewLine & "ϵͳ���ݿ��ʼ���ɹ���" & Environment.NewLine
    '        txtSetupInfo.Text = strSetupInfo
    '        Return True
    '    Catch ex As Exception
    '        strSetupInfo &= Environment.NewLine
    '        strSetupInfo &= "ϵͳ���ݿ��ʼ��ʧ�ܣ�" & Environment.NewLine
    '        txtSetupInfo.Text = strSetupInfo

    '        CmsError.ThrowEx("�������ݿ�ģ��ʧ�ܣ�" & ex.Message, ex, True)
    '    End Try
    'End Function
End Class

End Namespace
