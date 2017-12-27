Imports System.Data
Imports UNIONSOFT.Platform
Imports System.Text
Imports System.Collections
Imports System.DirectoryServices
Imports NetReusables
Partial Class DomainLogin
    Inherits System.Web.UI.Page
    Property LDAP() As String
        Get
            Return ViewState("LDAP")
        End Get
        Set(ByVal value As String)
            ViewState("LDAP") = value
        End Set
    End Property
    Property DomainServer() As String
        Get
            Return ViewState("DomainServer")
        End Get
        Set(ByVal value As String)
            ViewState("DomainServer") = value
        End Set
    End Property
    Protected Sub BtnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLogin.Click
        Dim adPath As String = ""
        Try
            Login(Me.txtUsername.Value, Me.txtPassword.Value)
        Catch ex As Exception
         
        End Try
 
    End Sub
    '------------------------------------------------------------------
    '开始登录
    '------------------------------------------------------------------
    Private Sub Login(ByVal strUserName As String, ByVal strUserPass As String)
        Session("CMS_PASSPORT") = Nothing
        If Authenticated(strUserName, strUserPass) Then
            RedirectPage(strUserName)
        Else
            Response.Redirect("ErrorMessage.aspx?Code=2", True)
        End If
    End Sub
    Private Sub RedirectPage(ByVal strUserName)
        Try

            Dim pst As CmsPassport = CmsPassport.GenerateCmsPassport(strUserName)
            If Not (pst Is Nothing) Then
                Session("CMS_PASSPORT") = pst
                If pst.Employee.ID.ToLower() = "sysuser" Then '是系统内部用户
                    Response.Redirect("cmsdev/DevMain.aspx", False)
                    Return
                ElseIf pst.Employee.ID.ToLower() = "admin" Then '是系统管理员
                    pst.EmpIsSysAdmin = True
                    Session("CMS_PASSPORT") = pst
                    Response.Redirect("cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminres/ResourceFrameBody.aspx&timeid=" & TimeId.CurrentMilliseconds(1), False)
                    Return
                ElseIf pst.EmpIsSysSecurity Then  '是系统安全员
                    Response.Redirect("cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminsys/SysLogManager.aspx", False)
                    Return
                Else '是普通人员
                    Dim strFlowEntryUrl As String = CmsConfig.GetString("SYS_CONFIG", "LOGIONPAGE")
                    If strFlowEntryUrl.Trim() = "" Then
                        Response.Redirect("cmshost/CmsFrame.aspx", False)
                        Return
                    Else
                        Response.Redirect(strFlowEntryUrl & "?user= " & pst.Employee.ID.Trim() & " &ucode=" & pst.Employee.Password.Trim(), False)
                    End If
                    Return
                End If
            Else

                Response.Redirect("ErrorMessage.aspx?Code=3&UserID=" + strUserName, False)
            End If
            Return
        Catch ex As Exception

            Response.Redirect("ErrorMessage.aspx?Code=4", False)
            Return
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
           
            DomainServer = CmsConfig.GetString("DomainServer", "ServerName")
            LDAP = CmsConfig.GetString("DomainServer", "LDAP")
            If DomainServer = "" Or LDAP = "" Then
                Response.Redirect("ErrorMessage.aspx?Code=1", False)
                Return
            End If

            Dim ADUserID As String = Request.ServerVariables("LOGON_USER")
            Dim Array As Array = ADUserID.Split("\")
            Dim UserID As String = Array(Array.Length - 1)
            Dim DomainName As String = Array(0)
            If DomainName.ToUpper = DomainServer.ToUpper Then
                RedirectPage(UserID)
            End If
           
        End If
    End Sub
    Private Function Authenticated(ByVal username As String, ByVal pwd As String) As Boolean
        Dim domainAndUsername As String = DomainServer & "\" & username
        Dim entry As New DirectoryEntry(LDAP, domainAndUsername, pwd)
        Try
            Dim obj As Object = entry.NativeObject
            Dim search As New DirectorySearcher(entry)

            search.Filter = "(&(objectClass=user)(objectCategory=person)(sAMAccountName=" & username & "))"
            Dim result As SearchResultCollection = search.FindAll
            Return result IsNot Nothing
        Catch ex As Exception
            Return False

        End Try


    End Function

  
End Class
