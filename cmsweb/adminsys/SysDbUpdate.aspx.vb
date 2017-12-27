Option Strict On
Option Explicit On 

Imports System.IO

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class SysDbUpdate
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblLastUpdateDate As System.Web.UI.WebControls.Label

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

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '仅系统管理员可以使用的功能需要调用此方法校验
        If CmsPass.EmpIsSysAdmin = False Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        btnUpdateFromFile.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要自动更新数据库系统吗？');")
        btnUpdateDbFromSql.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要手动更新数据库系统吗？');")

        ShowDbInfo() '显示数据库版本、更新时间等信息
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If CmsDatabase.GetDbConfig.Database.ToLower() = "banks1" Then
            btnUpdateMdb.Visible = True '仅Banks1数据库可以看到更新MDB文件
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
                PromptMsg("更新系统数据库成功！")
            Else
                PromptMsg("更新系统数据库部分失败，请仔细查看更新结果！")
            End If

            ShowDbInfo() '显示数据库版本、更新时间等信息
        Catch ex As Exception
            PromptMsg("自动更新数据库系统异常失败！", ex, True)
        End Try
    End Sub

    'Private Sub btnUpdateMdb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateMdb.Click
    '    Try
    '        Dim strNotes As String = ""
    '        Dim blnOK As Boolean = CmsDbUpdate.UpdateMdbDatabase(CmsPass, CmsConfig.ProjectRootFolder & "data\sql\dbupdate.sql", chkCheckLastUpdateTime.Checked, strNotes)
    '        txtResult.Text = strNotes
    '        If blnOK = True Then
    '            PromptMsg("更新系统数据库成功！")
    '        Else
    '            PromptMsg("更新系统数据库部分失败，请仔细查看更新结果！")
    '        End If

    '        ShowDbInfo() '显示数据库版本、更新时间等信息
    '    Catch ex As Exception
    '        PromptMsg("自动更新数据库系统异常失败！", ex, True)
    '    End Try
    'End Sub

    Private Sub btnUpdateDbFromSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateDbFromSql.Click
        Try
            If txtSql.Text.Trim() = "" Then
                PromptMsg("请先输入有效的SQL语句！")
                Return
            End If

            Dim sr As New StringReader(txtSql.Text.Trim())
            Dim strNotes As String
            Dim blnUpdated As Boolean = CmsDbUpdate.UpdateOneDatabase(CmsPass, Nothing, sr, "", strNotes, False, "", DbParameter.DB_UPDATETIME)
            txtResult.Text = strNotes
            sr.Close()

            ShowDbInfo() '显示数据库版本、更新时间等信息
        Catch ex As Exception
            PromptMsg("手动SQL更新数据库异常失败！", ex, True)
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
                If str.IndexOf("手动设置") >= 0 Then
                    strManuals &= str & Environment.NewLine
                End If
            End While
            sr.Close()

            txtSql.Text = strManuals
        Catch ex As Exception
            PromptMsg("读取文件异常出错！", ex, True)
            SLog.Err("读取文件异常出错！", ex)
        End Try
    End Sub

    Private Sub btnReadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadAll.Click
        Try
                Dim strFile As String = "conf\dbupdate.sql"
            Dim strDbUpdateFilePath As String = CmsConfig.ProjectRootFolder & strFile
            Dim sr As New StreamReader(strDbUpdateFilePath)
            txtSql.Text = sr.ReadToEnd()
        Catch ex As Exception
            PromptMsg("读取文件异常出错！", ex, True)
        End Try
    End Sub

    Private Sub lbtnClearSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearSql.Click
        txtSql.Text = ""
    End Sub

    Private Sub lbtnClearResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearResult.Click
        txtResult.Text = ""
    End Sub

    '------------------------------------------------------------------------
    '显示数据库版本、更新时间等信息
    '------------------------------------------------------------------------
    Private Sub ShowDbInfo()
        lblDbInfo.Text = "数据库地址：" & CmsConfig.GetString("SYS_DATABASE", "HOST") & "  内容数据库：" & CmsConfig.GetString("SYS_DATABASE", "DATABASE") & "  文档数据库：" & CmsConfig.GetString("SYS_DATABASE", "DATABASEDOC")
        lblDbVersion.Text = "数据库版本：" & CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODVER'")
        Dim dtmUpdateTime As String = DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_UPDATETIME)
        lblManualUpdateTime.Text = "内容管理系统数据库最近更新时间：" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_UPDATETIME)
        'lblFlowUpdateTime.Text = "工作流数据库最近更新时间：" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_FLOWUPDATETIME)
        'lblMDBUpdateTime.Text = "MDB数据库最近更新时间：" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_MDBUPDATETIME)
        lblLastOperateTime.Text = "数据库更新最近操作时间：" & DbParameter.GetDateTime(SDbConnectionPool.GetDbConfig(), DbParameter.DB_UPDATE_ACTIONTIME)
    End Sub
End Class

End Namespace
