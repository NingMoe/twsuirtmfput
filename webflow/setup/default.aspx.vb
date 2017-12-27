
'--------------------------------------------------------------------------------
'每次数据库的更改都需要在此文件中进行处理，以便于程序更新而不至于忘记数据库的更新。
'2009-5-14  CHENYU
'--------------------------------------------------------------------------------

Imports NetReusables
Imports System.IO
Imports System.Text


Namespace Unionsoft.Workflow.Web



Partial Class _default1
    Inherits System.Web.UI.Page

    '安装数据库更新
    Protected Sub DatabaseUpdateInstall(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SQLColomnExists("CMS_EMPLOYEE", "Headship") = False Then SQLUpdateInstallSQLText("ALTER TABLE [CMS_EMPLOYEE] ADD [Headship] [varchar] (50)", "2009-9-2", "CMS_EMPLOYEE", 0)

        '2009-5-14  CHENYU---------------------------------------------------------------------------------
        If SQLObjectExists("DatabaseUpdate") = False Then SQLUpdateInstall("DatabaseUpdate.SQL")
        If SQLObjectExists("VIEW_WF_ASSOCIATE") = False Then SQLUpdateInstall("VIEW_WF_ASSOCIATE.SQL")
        If SQLObjectExists("VIEW_WF_RECEIVEFILES") = False Then SQLUpdateInstall("VIEW_WF_RECEIVEFILES.SQL")
        If SQLObjectExists("VIEW_WF_START") = False Then SQLUpdateInstall("VIEW_WF_START.SQL")
        If SQLObjectExists("VIEW_WF_FAVORITE") = False Then SQLUpdateInstall("VIEW_WF_FAVORITE.SQL")
        If SQLObjectExists("VIEW_WF_DRAFT") = False Then SQLUpdateInstall("VIEW_WF_DRAFT.SQL")
        If SQLObjectExists("WF_COMMON_SHORTCUTS") = False Then SQLUpdateInstall("WF_COMMON_SHORTCUTS.SQL")
        If SQLObjectExists("WF_ALARMSERVICE") = False Then SQLUpdateInstall("WF_ALARMSERVICE.SQL")
        If SQLObjectExists("WF_MESSAGE_CENTER") = False Then SQLUpdateInstall("WF_MESSAGE_CENTER.SQL")

        '2009-9-2  CHENYU---------------------------------------------------------------------------------
        If SQLColomnExists("WF_POXYSETTING", "ProxyName") = False Then SQLUpdateInstallSQLText("ALTER TABLE [WF_POXYSETTING] ADD [ProxyName] [varchar] (50)", "2009-9-2", "WF_POXYSETTING", 0)
        If SQLCheckUpdateScript("2009-9-2", "WF_POXYSETTING", 1) = False Then SQLUpdateInstallSQLText("ALTER TABLE [WF_POXYSETTING] DROP PK_WF_POXYSETTING", "2009-9-2", "WF_POXYSETTING", 1)
        If SQLCheckUpdateScript("2009-9-2", "WF_POXYSETTING", 2) = False Then SQLUpdateInstallSQLText("ALTER TABLE [WF_POXYSETTING] ADD CONSTRAINT [PK_WF_POXYSETTING] PRIMARY KEY  CLUSTERED ([UserCode],[ProxyCode])  ON [PRIMARY] ", "2009-9-2", "WF_POXYSETTING", 2)

        '2009/11/6  CHENYU---------------------------------------------------------------------------------
        If SQLColomnExists("FLW_NODE", "COPYFORORGANIZATION") Then SQLUpdateInstallSQLText("SP_RENAME 'FLW_NODE.COPYFORORGANIZATION','CCORGANIZATION','COLUMN'", "2009-11-6", "FLW_NODE", 3)
        If SQLColomnExists("FLW_NODE", "ISCopyForORGSELECT") Then SQLUpdateInstallSQLText("SP_RENAME 'FLW_NODE.ISCopyForORGSELECT','ISCCORGSELECT','COLUMN'", "2009-11-6", "FLW_NODE", 3)
        If SQLColomnExists("FLW_NODE", "MessageEmail") = False Then SQLUpdateInstallSQLText("ALTER TABLE [FLW_NODE] ADD [MessageEmail] [smallint]", "2009-11-6", "FLW_NODE", 3)
        If SQLColomnExists("FLW_NODE", "MessageSms") = False Then SQLUpdateInstallSQLText("ALTER TABLE [FLW_NODE] ADD [MessageSms] [smallint]", "2009-11-6", "FLW_NODE", 3)
        If SQLColomnExists("FLW_NODE", "NODEKEY") = False Then SQLUpdateInstallSQLText("ALTER TABLE [FLW_NODE] ADD [NODEKEY] [varchar] (50)", "2009-11-6", "FLW_NODE", 3)
        If SQLColomnExists("FLW_LINK", "LINKKEY") = False Then SQLUpdateInstallSQLText("ALTER TABLE [FLW_LINK] ADD [LINKKEY] [varchar] (50)", "2009-11-6", "FLW_LINK", 3)
        If SQLColomnLength("FLW_NODE", "DEFFROM") < 400 Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ALTER COLUMN DEFFROM varchar(400)", "2009-11-6", "FLW_NODE", 3)
        If SQLObjectExists("FLW_NODE_COPYFORORGANIZATION") Then SQLUpdateInstallSQLText("SP_RENAME 'FLW_NODE_COPYFORORGANIZATION','FLW_NODE_CCORGANIZATION'", "2009-11-6", "FLW_NODE_COPYFORORGANIZATION", 3)

        '2009/11/23  CHENYU---------------------------------------------------------------------------------
        If SQLColomnExists("WF_USERTASK", "IsAutoProcess") = False Then SQLUpdateInstallSQLText("ALTER TABLE [WF_USERTASK] ADD [IsAutoProcess] [smallint]", "2009-11-23", "WF_USERTASK", 3)

        '2009/11/25  CHENYU---------------------------------------------------------------------------------
        If SQLColomnExists("FLW_NODE_TIMEALARM", "ISUSESMS") = False Then SQLUpdateInstallSQLText("ALTER TABLE [FLW_NODE_TIMEALARM] ADD [ISUSESMS] [smallint]", "2009-11-25", "FLW_NODE_TIMEALARM", 3)

        '2009/4/19 表:WF_LOGS已经不再需要.
        If SQLObjectExists("WF_LOGS") = True Then SQLObjectDelete("WF_LOGS")
        If SQLObjectExists("WF_WATCH_LOG") = True Then SQLObjectDelete("WF_WATCH_LOG")

        '2010/6/4 表:FLW_FORM需要增加字段IsCustmizeForm
        If SQLColomnExists("FLW_FORM", "IsCustmizeForm") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_FORM ADD IsCustmizeForm smallint", "2010-6-4", "FLW_FORM", 1)

        '2010/9/15 表node增加字段 IsOrgSql,OrgSql
        If SQLColomnExists("FLW_NODE", "IsOrgSql") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ADD IsOrgSql smallint", "2010-9-15", "FLW_NODE", 1)
        If SQLColomnExists("FLW_NODE", "OrgSql") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ADD OrgSql varchar(1500)", "2010-9-15", "FLW_NODE", 1)

        '2010/10/8 表node增加字段 Code
        If SQLColomnExists("FLW_NODE", "Code") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ADD Code varchar(20)", "2010-10-8", "FLW_NODE", 1)

        '2010/10/28 数据库更新
        If SQLColomnExists("FLW_NODE", "CommandRequired") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ADD CommandRequired bit", "2010-10-28", "FLW_NODE", 1)
        If SQLColomnExists("FLW_NODE", "CommandType") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ADD CommandType int", "2010-10-28", "FLW_NODE", 1)
        If SQLColomnExists("FLW_NODE", "CommandText") = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ADD CommandText nvarchar(500)", "2010-10-28", "FLW_NODE", 1)

        '2010/11/1 数据库更新
        If SQLColomnExists("FLW_NODE", "Isprint") = True Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE DROP COLUMN Isprint", "2010-11-1", "FLW_NODE", 1)
        If SQLColomnExists("WF_USERTASK", "SAVESTATUS") = True Then SQLUpdateInstallSQLText("ALTER TABLE WF_USERTASK DROP COLUMN SAVESTATUS", "2010-11-1", "WF_TASK", 1)
        If SQLColomnExists("WF_TASK", "Isprint") = True Then SQLUpdateInstallSQLText("ALTER TABLE WF_TASK DROP COLUMN Isprint", "2010-11-1", "WF_TASK", 1)
        If SQLColomnExists("WF_TASK", "printtimes") = True Then SQLUpdateInstallSQLText("ALTER TABLE WF_TASK DROP COLUMN printtimes", "2010-11-1", "WF_TASK", 1)
        If SQLColomnExists("WF_TASK", "isexigence") = True Then SQLUpdateInstallSQLText("ALTER TABLE WF_TASK DROP COLUMN isexigence", "2010-11-1", "WF_TASK", 1)
        If SQLColomnExists("WF_INSTANCE", "watchdog") = True Then SQLUpdateInstallSQLText("ALTER TABLE WF_INSTANCE DROP COLUMN watchdog", "2010-11-1", "WF_INSTANCE", 1)
        If SQLColomnExists("WF_INSTANCE", "isuseless") = True Then SQLUpdateInstallSQLText("ALTER TABLE WF_INSTANCE DROP COLUMN isuseless", "2010-11-1", "WF_INSTANCE", 1)

        '更新FLW_NODE表的ISORGSELECT、ISCCORGSELECT、ISSELECTONE字段为允许空.(2010/11/1)
        If SQLCheckUpdateScript("2010-11-1", "FLW_NODE", 1) = False Then SQLUpdateInstallSQLText("ALTER TABLE FLW_NODE ALTER COLUMN ISORGSELECT BIT NULL;ALTER TABLE FLW_NODE ALTER COLUMN ISCCORGSELECT BIT NULL;ALTER TABLE FLW_NODE ALTER COLUMN ISSELECTONE BIT NULL;", "2010-11-1", "FLW_NODE", 1)

        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_START", 5) = False Then
            SQLObjectDelete("VIEW_WF_START")
            SQLUpdateInstall("VIEW_WF_START.SQL", "2010-11-1", "VIEW_WF_START", 5)
        End If
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_RECEIVEFILES", 5) = False Then
            SQLObjectDelete("VIEW_WF_RECEIVEFILES")
            SQLUpdateInstall("VIEW_WF_RECEIVEFILES.SQL", "2010-11-1", "VIEW_WF_RECEIVEFILES", 5)
        End If
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_ASSOCIATE", 5) = False Then
            SQLObjectDelete("VIEW_WF_ASSOCIATE")
            SQLUpdateInstall("VIEW_WF_ASSOCIATE.SQL", "2010-11-1", "VIEW_WF_ASSOCIATE", 5)
        End If
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_DRAFT", 5) = False Then
            SQLObjectDelete("VIEW_WF_DRAFT")
            SQLUpdateInstall("VIEW_WF_DRAFT.SQL", "2010-11-1", "VIEW_WF_DRAFT", 5)
        End If
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_FAVORITE", 5) = False Then
            SQLObjectDelete("VIEW_WF_FAVORITE")
            SQLUpdateInstall("VIEW_WF_FAVORITE.SQL", "2010-11-1", "VIEW_WF_FAVORITE", 5)
        End If

        '2010/11/11 数据库更新 - 光棍节 
        If SQLColomnExists("WF_USERTASK", "ProxyEmpCode") = False Then SQLUpdateInstallSQLText("ALTER TABLE WF_USERTASK ADD ProxyEmpCode nvarchar(50)", "2010-11-11", "WF_USERTASK", 1)
        If SQLColomnExists("WF_USERTASK", "ProxyEmpName") = False Then SQLUpdateInstallSQLText("ALTER TABLE WF_USERTASK ADD ProxyEmpName nvarchar(50)", "2010-11-11", "WF_USERTASK", 1)

        If SQLColomnExists("WF_TASK", "NodeCode") = False Then SQLUpdateInstallSQLText("ALTER TABLE WF_TASK ADD NodeCode nvarchar(50)", "2010-11-11", "WF_TASK", 1)
        If SQLColomnExists("WF_TASK", "NodeUrl") = False Then SQLUpdateInstallSQLText("ALTER TABLE WF_TASK ADD NodeUrl nvarchar(250)", "2010-11-11", "WF_TASK", 1)

        If SQLObjectExists("SP_WorkflowInstanceDelete") = False Then SQLUpdateInstall("SP_WorkflowInstanceDelete.SQL")
        If SQLObjectExists("SP_WorkflowItemDelete") = False Then SQLUpdateInstall("SP_WorkflowItemDelete.SQL")


        Response.Redirect("default.aspx")
    End Sub

    '检查数据库是否需要更新，并提示
    Protected Function DatabaseUpdateCheck() As Boolean
        Dim str As String = ""
        '2009-5-14  CHENYU---------------------------------------------------------------------------------
        If SQLObjectExists("DatabaseUpdate") = False Then
            str = str & "缺少表:DatabaseUpdate." & "<br>"
            Response.Write(str)
            Exit Function
        End If

        If SQLColomnExists("CMS_EMPLOYEE", "Headship") = False Then str = str & "表:CMS_EMPLOYEE缺少字段Headship [varchar] (50).(2009-9-2)" & "<br>"

        If SQLObjectExists("VIEW_WF_ASSOCIATE") = False Then str = str & "缺少视图:VIEW_WF_ASSOCIATE." & "<br>"
        If SQLObjectExists("VIEW_WF_FAVORITE") = False Then str = str & "缺少视图:VIEW_WF_FAVORITE." & "<br>"
        If SQLObjectExists("VIEW_WF_RECEIVEFILES") = False Then str = str & "缺少视图:VIEW_WF_RECEIVEFILES." & "<br>"
        If SQLObjectExists("VIEW_WF_START") = False Then str = str & "缺少视图:VIEW_WF_START." & "<br>"
        If SQLObjectExists("WF_COMMON_SHORTCUTS") = False Then str = str & "缺少表:WF_COMMON_SHORTCUTS." & "<br>"
        If SQLObjectExists("VIEW_WF_DRAFT") = False Then str = str & "缺少视图:VIEW_WF_DRAFT.(2009-8-31)" & "<br>"
        If SQLObjectExists("WF_ALARMSERVICE") = False Then str = str & "表WF_ALARMSERVICE不存在.(2009/11/16 新增)" & "<br>"
        If SQLObjectExists("WF_MESSAGE_CENTER") = False Then str = str & "缺少表:WF_MESSAGE_CENTER.(2009/12/31 新增)" & "<br>"

        '2009-9-2  CHENYU---------------------------------------------------------------------------------
        If SQLColomnExists("WF_POXYSETTING", "ProxyName") = False Then str = str & "表:WF_POXYSETTING缺少字段ProxyName [varchar] (50).(2009-9-2)" & "<br>"
        If SQLCheckUpdateScript("2009-9-2", "WF_POXYSETTING", 1) = False Then str = str & "WF_POXYSETTING V1 2009-9-2 更新." & "<br>"
        If SQLCheckUpdateScript("2009-9-2", "WF_POXYSETTING", 2) = False Then str = str & "WF_POXYSETTING V2 2009-9-2 更新." & "<br>"

        '2009/11/6
        If SQLColomnExists("FLW_NODE", "COPYFORORGANIZATION") Then str = str & "表FLW_NODE字段COPYFORORGANIZATION应更名为CCORGANIZATION.(2009/11/6 更新)" & "<br>"
        If SQLColomnExists("FLW_NODE", "ISCopyForORGSELECT") Then str = str & "表FLW_NODE字段ISCopyForORGSELECT应更名为ISCCORGSELECT.(2009/11/6 更新)" & "<br>"
        If SQLColomnExists("FLW_NODE", "MessageEmail") = False Then str = str & "表FLW_NODE缺少字段MessageEmail [smallint].(2009/11/6 更新)" & "<br>"
        If SQLColomnExists("FLW_NODE", "MessageSms") = False Then str = str & "表FLW_NODE缺少字段MessageSms [smallint].(2009/11/6 更新)" & "<br>"
        If SQLColomnExists("FLW_NODE", "NODEKEY") = False Then str = str & "表FLW_NODE缺少字段NODEKEY VARCHAR(50).(2009/11/6 更新)" & "<br>"
        If SQLColomnExists("FLW_LINK", "LINKKEY") = False Then str = str & "表FLW_LINK缺少字段LINKKEY VARCHAR(50).(2009/11/6 更新)" & "<br>"
        If SQLColomnLength("FLW_NODE", "DEFFROM") < 400 Then str = str & "表FLW_NODE字段DEFFROM 的长度应是400.(2009/11/6 更新)" & "<br>"
        If SQLObjectExists("FLW_NODE_COPYFORORGANIZATION") = True Then str = str & "表:FLW_NODE_COPYFORORGANIZATION应更名为FLW_NODE_CCORGANIZATION.(2009/11/6 更新)" & "<br>"

        '2009/11/23
        If SQLColomnExists("WF_USERTASK", "IsAutoProcess") = False Then str = str & "表WF_USERTASK新增字段IsAutoProcess.(2009/11/23 新增)" & "<br>"

        '2009/11/25
        If SQLColomnExists("FLW_NODE_TIMEALARM", "ISUSESMS") = False Then str = str & "表FLW_NODE_TIMEALARM新增字段ISUSESMS.(2009/11/25 新增)" & "<br>"

        '2009/4/19
        If SQLObjectExists("WF_LOGS") = True Then str = str & "表:WF_LOGS已经不再需要.(2009/4/19)" & "<br>"
        If SQLObjectExists("WF_WATCH_LOG") = True Then str = str & "表:WF_WATCH_LOG已经不再需要.(2009/4/19)" & "<br>"

        '2010/6/4
        If SQLColomnExists("FLW_FORM", "IsCustmizeForm") = False Then str = str & "表:FLW_FORM需要增加字段IsCustmizeForm.(2009/6/4)" & "<br>"

        '2010/9/15 表node增加字段 IsOrgSql,OrgSql
        If SQLColomnExists("FLW_NODE", "IsOrgSql") = False Then str = str & "表:FLW_NODE需要增加字段IsOrgSql.(2010/9/15)" & "<br>"
        If SQLColomnExists("FLW_NODE", "OrgSql") = False Then str = str & "表:FLW_NODE需要增加字段OrgSql.(2010/9/15)" & "<br>"

        '2010/10/8 表node增加字段 Code
        If SQLColomnExists("FLW_NODE", "Code") = False Then str = str & "表:FLW_NODE需要增加字段Code.(2010/10/8)" & "<br>"

        '2010/10/28 数据库更新
        If SQLColomnExists("FLW_NODE", "CommandRequired") = False Then str = str & "表:FLW_NODE需要增加字段CommandRequired.(2010/10/28)" & "<br>"
        If SQLColomnExists("FLW_NODE", "CommandType") = False Then str = str & "表:FLW_NODE需要增加字段CommandType.(2010/10/28)" & "<br>"
        If SQLColomnExists("FLW_NODE", "CommandText") = False Then str = str & "表:FLW_NODE需要增加字段CommandText.(2010/10/28)" & "<br>"

        '2010/11/1 数据库更新
        If SQLColomnExists("FLW_NODE", "Isprint") = True Then str = str & "表:FLW_NODE需要删除无用字段Isprint.(2010/11/1)" & "<br>"
        If SQLColomnExists("WF_USERTASK", "SAVESTATUS") = True Then str = str & "表:WF_USERTASK需要删除无用字段SAVESTATUS.(2010/11/1)" & "<br>"
        If SQLColomnExists("WF_TASK", "Isprint") = True Then str = str & "表:WF_TASK需要删除无用字段Isprint.(2010/11/1)" & "<br>"
        If SQLColomnExists("WF_TASK", "printtimes") = True Then str = str & "表:WF_TASK需要删除无用字段printtimes.(2010/11/1)" & "<br>"
        If SQLColomnExists("WF_TASK", "isexigence") = True Then str = str & "表:WF_TASK需要删除无用字段isexigence.(2010/11/1)" & "<br>"
        If SQLColomnExists("WF_INSTANCE", "watchdog") = True Then str = str & "表:WF_INSTANCE需要删除无用字段watchdog.(2010/11/1)" & "<br>"
        If SQLColomnExists("WF_INSTANCE", "isuseless") = True Then str = str & "表:WF_INSTANCE需要删除无用字段isuseless.(2010/11/1)" & "<br>"

        If SQLCheckUpdateScript("2010-11-1", "FLW_NODE", 1) = False Then str = str & "更新FLW_NODE表的ISORGSELECT、ISCCORGSELECT、ISSELECTONE字段为允许空.(2010/11/1)" & "<br>"

        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_START", 5) = False Then str = str & "视图VIEW_WF_START 2010-11-1 更新." & "<br>"
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_RECEIVEFILES", 5) = False Then str = str & "视图VIEW_WF_RECEIVEFILES 2010-11-1 更新." & "<br>"
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_ASSOCIATE", 5) = False Then str = str & "视图VIEW_WF_ASSOCIATE 2010-11-1 更新." & "<br>"
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_DRAFT", 5) = False Then str = str & "视图VIEW_WF_DRAFT 2010-11-1 更新." & "<br>"
        If SQLCheckUpdateScript("2010-11-1", "VIEW_WF_FAVORITE", 5) = False Then str = str & "视图VIEW_WF_FAVORITE 2010-11-1 更新." & "<br>"

        '2010/11/11 数据库更新 - 光棍节 
        If SQLColomnExists("WF_USERTASK", "ProxyEmpCode") = False Then str = str & "表:WF_USERTASK需要增加字段ProxyEmpCode.(2010/11/11)" & "<br>"
        If SQLColomnExists("WF_USERTASK", "ProxyEmpName") = False Then str = str & "表:WF_USERTASK需要增加字段ProxyEmpName.(2010/11/11)" & "<br>"

        If SQLColomnExists("WF_TASK", "NodeCode") = False Then str = str & "表:WF_TASK需要增加字段NodeCode.(2010/11/11)" & "<br>"
        If SQLColomnExists("WF_TASK", "NodeUrl") = False Then str = str & "表:WF_TASK需要增加字段NodeUrl.(2010/11/11)" & "<br>"

        If SQLObjectExists("SP_WorkflowInstanceDelete") = False Then str = str & "缺少存储过程:SP_WorkflowInstanceDelete." & "<br>"
        If SQLObjectExists("SP_WorkflowItemDelete") = False Then str = str & "缺少存储过程:SP_WorkflowItemDelete." & "<br>"

        If (str = "") Then str = "不需要更新数据库."
        Response.Write(str)
    End Function

#Region "辅助函数"


    '验证SQL对象是否存在， 如SQL对象包括 表、视图、存储过程等
    Private Function SQLObjectExists(ByVal name As String) As Boolean
        Dim strSql As String = "select * from sysobjects where name='" & name & "'"
        If SDbStatement.Query(strSql).Tables(0).Rows.Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    '删除SQL对象
    Private Sub SQLObjectDelete(ByVal ObjectName As String)
        Dim strSql As String = "select * from sysobjects where name='" & ObjectName & "'"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        If dt.Rows.Count = 1 Then
            Select Case DbField.GetStr(dt.Rows(0), "type").ToUpper()
                Case "U"
                    SDbStatement.Execute("DROP TABLE " & ObjectName)
                Case "V"
                    SDbStatement.Execute("DROP VIEW " & ObjectName)
                Case "P"
                    SDbStatement.Execute("DROP PROCEDURE " & ObjectName)
            End Select
        End If
    End Sub

    '验证SQL表的列是否存在
    Private Function SQLColomnExists(ByVal table As String, ByVal name As String) As Boolean
        Dim strSql As String = "select * from syscolumns where id=object_id('" & table & "') and name='" + name + "'"
        If SDbStatement.Query(strSql).Tables(0).Rows.Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    '获取字段长度
    Private Function SQLColomnLength(ByVal table As String, ByVal name As String) As Integer
        Dim strSql As String = "select length from syscolumns where id=object_id('" & table & "') and name='" + name + "'"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        If dt.Rows.Count = 1 Then
            Return DbField.GetInt(dt.Rows(0), "length")
        Else
            Return 0
        End If
    End Function

    '安装SQL的更新
    Private Sub SQLUpdateInstall(ByVal filename As String)
        Dim buff As Byte()
        Dim sr As StreamReader = New StreamReader(Request.PhysicalApplicationPath & "setup\" & filename)
        Dim str As String = sr.ReadToEnd()
        sr.Close()
        SDbStatement.Execute(str)
    End Sub

    '从文件安装SQL更新
    Private Sub SQLUpdateInstall(ByVal filename As String, ByVal [date] As String, ByVal name As String, ByVal version As Integer)
        Dim buff As Byte()
        Dim sr As StreamReader = New StreamReader(Request.PhysicalApplicationPath & "setup\" & filename)
        Dim str As String = sr.ReadToEnd()
        sr.Close()
        SDbStatement.Execute(str)

        Dim sql As String = ""
        sql += "if not exists(select * from DatabaseUpdate where [date]='" + [date] + "' and name='" + name + "' and version=" + version.ToString() + ")"
        sql += "insert into DatabaseUpdate ([date],[name],version) values ('" + [date] + "','" + name + "'," + version.ToString() + ")"
        SDbStatement.Execute(sql)
    End Sub

    '从SQL语句安装SQL更新
    Private Sub SQLUpdateInstallSQLText(ByVal text As String, ByVal [date] As String, ByVal name As String, ByVal version As Integer)
        SDbStatement.Execute(text)

        Dim sql As String = ""
        sql += "if not exists(select * from DatabaseUpdate where [date]='" + [date] + "' and name='" + name + "' and version=" + version.ToString() + ")"
        sql += "insert into DatabaseUpdate ([date],[name],version) values ('" + [date] + "','" + name + "'," + version.ToString() + ")"
        SDbStatement.Execute(sql)
    End Sub

    '检查更新脚本
    Private Function SQLCheckUpdateScript(ByVal [date] As String, ByVal name As String, ByVal version As Integer) As Boolean
        If SQLObjectExists("DatabaseUpdate") = False Then Return False

        Dim strSql As String = "select * from DatabaseUpdate where [date]='" & [date] & "' and name='" & name & "' and version=" & version
        If SDbStatement.Query(strSql).Tables(0).Rows.Count = 1 Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region

End Class

End Namespace
