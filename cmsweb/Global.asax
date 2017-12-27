<%@ Application Language="VB" %>

<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.IO" %> 


<%@ Import Namespace="NetReusables" %>
<%@ Import Namespace="Unionsoft.Implement" %>
<%@ Import Namespace="Unionsoft.Platform" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
          
        ''系统初始化
        'system.Web.HttpContext.Current.Request.ApplicationPath 
        CmsEnvironment.Init(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, GetServerPath(), "cms.log", "dbsql.log", "\log\", True)
        PrepareEnvironment() '创建和删除一些目录
        OrgFactory.ConfigFile = CmsConfig.ProjectRootFolder & "conf\medi_organization.xml"
        ''配置<资源管理>实现模块
        ResFactory.ConfigFile = CmsConfig.ProjectRootFolder & "conf\medi_resource.xml"
        OrgFactory.DepDriver = New CmsDepartment
        OrgFactory.EmpDriver = New Unionsoft.Implement.CmsEmployee
        ResFactory.ResService = New CmsResource
        ResFactory.TableService("DOC") = New CTableDataDoc
        ResFactory.TableService("TWOD") = New CTableData2D
        ResFactory.TableService("EMP") = New CTableDataEmp

        '配置<定制计算公式>实现模块
        FormulaFactory.DllRootFolder = CmsConfig.ProjectRootFolder

        ' 在应用程序启动时运行的代码
    End Sub
    
    
    Function GetServerPath() As String        Dim FilePath As String        Dim server As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Name")        Dim port As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Port")        If port = "80" Then            FilePath = "http://" + server + System.Web.HttpContext.Current.Request.ApplicationPath        Else            FilePath = "http://" + server + ":" + port + System.Web.HttpContext.Current.Request.ApplicationPath        End If        Return FilePath    End Function
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' 在应用程序关闭时运行的代码
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' 在出现未处理的错误时运行的代码
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
    
    
     
     
    

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
       

        '退至登录页面
        ' Response.Redirect("/cmsweb/Logout.aspx", True)
    End Sub
       
    Private Sub PrepareEnvironment()
        '--------------------------------------------------
        '创建本系统用的临时目录
        If Directory.Exists(CmsConfig.TempFolder) = True Then
            '--------------------------------------------------------------
            '测试目录文件访问权限
            Dim strTempFolder As String = CmsConfig.TempFolder & "test" & TimeId.CurrentMilliseconds() & "\"
            Try
                Directory.CreateDirectory(strTempFolder)
            Catch ex As Exception
                Dim strErr As String = "当前系统没有文件创建权限！"
                CmsConfig.AddSystemError(strErr)
                SLog.Fatal(strErr, ex)
            End Try
            Try
                If Directory.Exists(strTempFolder) Then
                    Directory.Delete(strTempFolder, True) '删除文件上传下载用临时目录
                End If
            Catch ex As Exception
                Dim strErr As String = "当前系统没有文件删除权限！"
                CmsConfig.AddSystemError(strErr)
                SLog.Fatal(strErr, ex)
            End Try
            '--------------------------------------------------------------

            Try
                '--------------------------------------------------------------
                '删除临时目录下的所有子目录
                Dim di As New DirectoryInfo(CmsConfig.TempFolder)
                Dim dirs As DirectoryInfo() = di.GetDirectories("*")
                If Not dirs Is Nothing AndAlso dirs.Length > 0 Then
                    Dim oneDir As DirectoryInfo
                    For Each oneDir In dirs
                        Directory.Delete(oneDir.FullName, True)
                    Next
                End If
                '--------------------------------------------------------------

                '--------------------------------------------------------------
                '删除临时目录下的所有文件
                Dim fis As FileInfo() = di.GetFiles("*")
                Dim oneFile As FileInfo
                For Each oneFile In fis
                    File.Delete(oneFile.FullName)
                Next
                '--------------------------------------------------------------
            Catch ex As Exception
                SLog.Fatal("删除临时目录的一些子目录时异常失败！", ex)
            End Try
        Else
        End If
        '--------------------------------------------------
    End Sub
</script>