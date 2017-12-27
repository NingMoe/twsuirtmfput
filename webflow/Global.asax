<%@ Application Language="VB" %>

<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.IO" %> 


<%@ Import Namespace="NetReusables" %>
<%@ Import Namespace="Unionsoft.Implement" %>
<%@ Import Namespace="Unionsoft.Platform" %>
<%@ Import Namespace="Unionsoft.Workflow.Engine" %>
<%@ Import Namespace=" Unionsoft.Workflow.Web" %>

<%@ Import Namespace="Unionsoft.Workflow.Platform" %>


<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
       InitSystemConfig
        ' 在应用程序启动时运行的代码
    End Sub
 
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' 在应用程序关闭时运行的代码
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' 在出现未处理的错误时运行的代码
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        InitSystemConfig()
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
       

        '退至登录页面
        ' Response.Redirect("/cmsweb/Logout.aspx", True)
    End Sub
       
    Private Sub InitSystemConfig()

        NetReusables.TimeId.SleepMilliseconds = 30

        '当前的工作路径
        Dim strCurrentDirectory As String = System.Web.HttpContext.Current.Request.PhysicalApplicationPath
        Try
            SLog.LogLevel = SLog.LOG_LEVEL.Info
            SLog.LogFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "log\workflow.log"

            '系统初始化,金波的
            CmsEnvironment.Init( strCurrentDirectory,GetServerPath())

            '配置<部门结构和人员管理>实现模块
            OrgFactory.ConfigFile = CmsConfig.ProjectRootFolder & "medi_organization.xml"
            OrgFactory.DepDriver = New CmsDepartment
            OrgFactory.EmpDriver = New CmsEmployee

            '配置<资源管理>实现模块
            ResFactory.ConfigFile = CmsConfig.ProjectRootFolder & "medi_resource.xml"
            ResFactory.ResService = New Unionsoft.Implement.CmsResource
            ResFactory.TableService("DOC") = New CTableDataDoc
            ResFactory.TableService("TWOD") = New CTableData2D
            ResFactory.DllRootFolder=CmsConfig.ProjectRootFolder

            '配置<定制计算公式>实现模块
            FormulaFactory.DllRootFolder = CmsConfig.ProjectRootFolder
        Catch ex As Exception
            SLog.Err("系统初始化(CmsEnvironment Error)失败.", ex)
        End Try

        '因为 CmsEnvironment.Init(strCurrentDirectory) 中更改了log文件名，所以这里重新指定名称！
        SLog.LogLevel = SLog.LOG_LEVEL.Info
        SLog.LogFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "log\workflow.log"

        '获取流程引擎的配置
        Dim AppConfig As New DataServiceSection
        Try
            AppConfig.LoadSource(strCurrentDirectory & "conf\workflow_config.xml")
            Unionsoft.Workflow.Web.Configuration.CurrentConfig = AppConfig
        Catch ex As Exception
            SLog.Err("获取流程配置文件失败.", ex)
        End Try

        Unionsoft.Workflow.Platform.MediImplement.WebRootFolder = strCurrentDirectory
        '设置流程引擎中的人员管理的模块
        Try
            Unionsoft.Workflow.Platform.MediImplement.WebRootFolder = strCurrentDirectory
            OrganizationFactory.ConfigFile = strCurrentDirectory & "conf\workflow_config.xml"
            OrganizationFactory.Implementation = OrganizationFactory.LoadDriver("MEDI_ORGANIZATION")
            
            MessageSendFactory.MailConfigFile = strCurrentDirectory & "conf\workflow_config.xml"
            MessageSendFactory.MailNodeName = "MEDI_MAILMESSAGE"
            
            
            MessageSendFactory.SmsConfigFile = strCurrentDirectory & "conf\workflow_config.xml"
            MessageSendFactory.SmsNodeName = "MEDI_MOBILEMESSAGE"
            ' MailFactory.Implementation = MailFactory.LoadDriver()
            
            ' Unionsoft.Workflow.Platform.mai()
        Catch ex As Exception
            SLog.Err("初始化流程引擎中的人员管理的模块失败.程序无法启动.", ex)
        End Try

    End Sub
    
       Function GetServerPath() As String        Dim FilePath As String        Dim server As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Name")        Dim port As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Port")        If port = "80" Then            FilePath = "http://" + server + System.Web.HttpContext.Current.Request.ApplicationPath        Else            FilePath = "http://" + server + ":" + port + System.Web.HttpContext.Current.Request.ApplicationPath        End If        Return FilePath    End Function
</script>