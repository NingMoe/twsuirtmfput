Option Strict On
Option Explicit On 

Imports System.IO
Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SetupDatabase
    Inherits AspPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCms1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCms2 As System.Web.UI.WebControls.TextBox

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
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
            '    '新建数据库，判断数据库系统是否已经创建
            '    If CmsDatabase.IsDbInitialized(CmsDatabase.GetDbConfig()) = True Then
            '        PromptMsg("相同名称的数据库已经存在，需要新建数据库则请修改内容数据库和文档数据库名称后单击""确认""。如果使用已存在的数据库，则请单击""下一步""！")
            '        Return
            '    End If
            'Else
            '    '附加数据库时不允许修改附加数据库名称
            '    lblDb1Comments.Text = "（附加数据库模板）"
            '    lblDb2Comments.Text = "（附加数据库模板）"
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
            '新建数据库，判断数据库系统是否已经创建
            If CmsDatabase.IsDbInitialized(CmsDatabase.GetDbConfig()) = True Then
                PromptMsg("相同名称的数据库已经存在，需要新建数据库则请修改内容数据库和文档数据库名称！")
                Return
            End If

            '修改数据库名称入配置文件中
            CmsConfig.SetString("SYS_DATABASE", "DATABASE", txtDb1.Text.Trim())
            CmsConfig.SetString("SYS_DATABASE", "DATABASEDOC", txtDb2.Text.Trim())
            CmsDatabase.InitialCmsDatabase()

            '新建数据库
            Dim bln As Boolean = CreateNewDatabase()
            If bln = True Then
                Response.Redirect("/cmsweb/Default.htm", False)
            Else
                PromptMsg("创建数据库失败！")
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '----------------------------------------------------------------
    '初始化数据库
    '----------------------------------------------------------------
    Private Function CreateNewDatabase() As Boolean
        Dim dbInit As New DbDatabase
        dbInit.DbHost = txtHost.Text.Trim()
        dbInit.DbPort = txtPort.Text.Trim()
        If dbInit.DbHost = "" Or dbInit.DbPort = "" Then
            Throw New CmsException("必须输入数据库地址和端口信息！")
        End If

        Dim strConfFolder As String = CmsConfig.ProjectRootFolder & "data\sql\"

        '--------------------------------------------------------------------
        '获取和检验参数
        Dim strDbName1 As String = txtDb1.Text.Trim()
        Dim strDbName2 As String = txtDb2.Text.Trim()
        If strDbName1 = "" Or strDbName2 = "" Then
            Throw New CmsException("必须输入内容数据库和文档数据库名称！")
        End If

        '创建数据库所在目录
        Dim strDbFilePath As String = CmsConfig.ProjectRootFolder & "..\..\db\"
        If Directory.Exists(strDbFilePath) = False Then
            Directory.CreateDirectory(strDbFilePath)
        End If
        '--------------------------------------------------------------------

        '----------------------------------------------------------------
        '创建数据库
        Dim strSetupInfo As String = ""
        Try
            dbInit.LoginID = "sa"
            dbInit.LoginPass = txtSaPass.Text.Trim()
            dbInit.CreateDatabase(strDbName1, strDbFilePath, strDbFilePath)
            strSetupInfo &= "创建数据库" & strDbName1 & "成功！" & Environment.NewLine
            dbInit.CreateDatabase(strDbName2, strDbFilePath, strDbFilePath)
            strSetupInfo &= "创建数据库" & strDbName2 & "成功！" & Environment.NewLine
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "创建数据库失败，错误信息：" & ex.Message & Environment.NewLine
            txtSetupInfo.Text = strSetupInfo
            Return False
        End Try
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '创建用户
        Try
            dbInit.AddLogin(CmsDatabase.DbUser, CmsDatabase.DbUserPass, strDbName1)
            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName1)
            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName2)
            strSetupInfo &= Environment.NewLine & "数据库用户创建成功！" & Environment.NewLine
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "数据库用户cmsuser创建失败，如果此用户已经存在并设置了正确的密码，则请忽略此错误信息！" & Environment.NewLine
        End Try
        '----------------------------------------------------------------

        '--------------------------------------------------------------------
        '创建所有系统表单
        Try
            dbInit.LoginID = CmsDatabase.DbUser
            dbInit.LoginPass = CmsDatabase.DbUserPass

            '创建表1的所有表单
            Dim strScriptFile As String = strConfFolder & "cms1_tables.sql"
            Dim fs As FileStream = File.OpenRead(strScriptFile)
            Dim sr As StreamReader = New StreamReader(fs, Encoding.UTF8)
            fs.Seek(0, SeekOrigin.Begin)
            Dim strScripts As String = sr.ReadToEnd()
            sr.Close()
            If strScripts <> "" Then dbInit.Execute(strScripts, strDbName1)

            '创建表2的所有表单
            strScriptFile = strConfFolder & "cms2_tables.sql"
            fs = File.OpenRead(strScriptFile)
            sr = New StreamReader(fs, Encoding.UTF8)
            fs.Seek(0, SeekOrigin.Begin)
            strScripts = sr.ReadToEnd()
            sr.Close()
            If strScripts <> "" Then dbInit.Execute(strScripts, strDbName2)

            strSetupInfo &= Environment.NewLine & "创建数据库表单成功！" & Environment.NewLine
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "创建数据库表单失败，错误信息：" & ex.Message & Environment.NewLine
            txtSetupInfo.Text = strSetupInfo
            Return False
        End Try
        '--------------------------------------------------------------------

        Try
            '--------------------------------------------------------------------
            '初始化部分系统表单的初始记录
            dbInit.LoginID = CmsDatabase.DbUser
            dbInit.LoginPass = CmsDatabase.DbUserPass

            Dim strScriptFile As String = strConfFolder & "cms1_initvalue.sql"
            Dim fs As FileStream = File.OpenRead(strScriptFile)
            Dim sr As StreamReader = New StreamReader(fs, Encoding.UTF8)
            fs.Seek(0, SeekOrigin.Begin)
            Dim strScripts As String = sr.ReadToEnd()
            sr.Close()
            If strScripts <> "" Then dbInit.Execute(strScripts, strDbName1)

            strSetupInfo &= Environment.NewLine & "初始化系统表单记录成功！" & Environment.NewLine
            '--------------------------------------------------------------------
        Catch ex As Exception
            strSetupInfo &= Environment.NewLine & "初始化系统表单记录失败，错误信息：" & ex.Message & Environment.NewLine
            txtSetupInfo.Text = strSetupInfo
            Return False
        End Try


        strSetupInfo &= Environment.NewLine & "系统数据库初始化成功！" & Environment.NewLine
        txtSetupInfo.Text = strSetupInfo

        Return True
    End Function

    'Private Sub btnAttachDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachDb.Click
    '    Try
    '        '附加数据库
    '        Dim strDbSource1 As String = CmsConfig.GetString("SYS_DATABASE", "DATABASE")
    '        Dim strDbSource2 As String = CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC")
    '        Dim bln As Boolean = AttachDatabase(strDbSource1, strDbSource2)
    '        If bln = True Then
    '            '数据库初始化成功后更新界面显示
    '            CmsConfig.SetString("SYS_DATABASE", "DATABASE", txtDb1.Text.Trim())
    '            CmsConfig.SetString("SYS_DATABASE", "DATABASEDOC", txtDb2.Text.Trim())
    '            CmsDatabase.InitialCmsDatabase()
    '            'Response.Redirect("/cmsweb/cmssetup/SetupEnd.aspx", False)
    '            Response.Redirect("/cmsweb/Default.aspx", False)
    '        Else
    '            PromptMsg("附加数据库失败！")
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
    '        PromptMsg("附加数据库失败！")
    '    End Try
    'End Sub

    ''----------------------------------------------------------------
    ''附加数据库
    ''----------------------------------------------------------------
    'Private Function AttachDatabase(ByVal strDbSource1 As String, ByVal strDbSource2 As String) As Boolean
    '    Dim strSetupInfo As String = ""

    '    Try
    '        '----------------------------------------------------------------
    '        '组装附加数据库的基本信息
    '        Dim strDbFolder As String = CmsConfig.ProjectRootFolder & "..\..\db\"
    '        Dim strDbName1 As String = txtDb1.Text.Trim()
    '        Dim strDbName2 As String = txtDb2.Text.Trim()
    '        If strDbName1 = "" Or strDbName2 = "" Then
    '            Throw New CmsException("必须输入内容和文档数据库名称！")
    '        End If

    '        Dim strDb1DataFile As String = strDbFolder & strDbSource1 & "_Data.mdf"
    '        Dim strDb1LogFile As String = strDbFolder & strDbSource1 & "_Log.ldf"
    '        Dim strDb2DataFile As String = strDbFolder & strDbSource2 & "_Data.mdf"
    '        Dim strDb2LogFile As String = strDbFolder & strDbSource2 & "_Log.ldf"
    '        '----------------------------------------------------------------

    '        '----------------------------------------------------------------
    '        '附加数据库
    '        Dim dbInit As New DbDatabase
    '        dbInit.DbHost = txtHost.Text.Trim()
    '        dbInit.DbPort = txtPort.Text.Trim()
    '        If dbInit.DbHost = "" Or dbInit.DbPort = "" Then
    '            Throw New CmsException("必须输入数据库地址和端口信息！")
    '        End If
    '        dbInit.LoginID = "sa"
    '        dbInit.LoginPass = txtSaPass.Text.Trim()

    '        dbInit.AttachDatabase(strDbName1, strDb1DataFile, strDb1LogFile)
    '        strSetupInfo &= "附加数据库(" & strDbName1 & ")成功！" & Environment.NewLine
    '        dbInit.AttachDatabase(strDbName2, strDb2DataFile, strDb2LogFile)
    '        strSetupInfo &= "附加数据库(" & strDbName2 & ")成功！" & Environment.NewLine
    '        '----------------------------------------------------------------

    '        '----------------------------------------------------------------
    '        '创建用户
    '        Try
    '            dbInit.AddLogin(CmsDatabase.DbUser, CmsDatabase.DbUserPass, strDbName1)
    '            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName1)
    '            dbInit.ChangeDBOwner(CmsDatabase.DbUser, strDbName2)
    '            strSetupInfo &= Environment.NewLine & "数据库用户创建成功！" & Environment.NewLine
    '        Catch ex As Exception
    '            strSetupInfo &= Environment.NewLine & "数据库用户cmsuser创建失败，如果此用户已经存在并设置了正确的密码，则请忽略此错误信息！" & Environment.NewLine
    '        End Try
    '        '----------------------------------------------------------------

    '        strSetupInfo &= Environment.NewLine & "系统数据库初始化成功！" & Environment.NewLine
    '        txtSetupInfo.Text = strSetupInfo
    '        Return True
    '    Catch ex As Exception
    '        strSetupInfo &= Environment.NewLine
    '        strSetupInfo &= "系统数据库初始化失败！" & Environment.NewLine
    '        txtSetupInfo.Text = strSetupInfo

    '        CmsError.ThrowEx("附加数据库模板失败！" & ex.Message, ex, True)
    '    End Try
    'End Function
End Class

End Namespace
