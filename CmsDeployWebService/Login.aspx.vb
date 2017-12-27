Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports Unionsoft.Platform
Imports System.DirectoryServices
    ''' <summary>
    ''' 用户登录
    ''' 接收Get参数：BackURL
    ''' </summary>
Partial Public Class Login
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        '


        If Not IsPostBack Then
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click

        Dim UserID As String = Me.txtUserID.Text.Trim
        Dim Password As String = Me.txtPassword.Text.Trim
        Dim Domain As String = Me.DropDownListDomain.SelectedValue
        If Domain = "" Then
            Dim pst As CmsPassport = CmsPassport.GenerateCmsPassport(UserID, Password)
            If pst IsNot Nothing Then
                If Request.QueryString("ReturnUrl") IsNot Nothing Then
                    Dim tokenValue As String = Me.getGuidString()
                    TokenService.TokenInsert(tokenValue, UserID)
                    Dim url As String = Request.QueryString("ReturnUrl")

                    url = Regex.Replace(url, "(\?|&)Token=.*", "", RegexOptions.IgnoreCase)
                    If url.IndexOf("?") >= 0 Then
                        url = url + "&Token=" + tokenValue
                    Else
                        url = url + "?Token=" + tokenValue
                    End If
                    Response.Redirect(url)
                Else
                    Response.Redirect("Default.aspx")
                End If
            Else
                Response.Write("抱歉，帐号或密码有误！")
            End If
        Else

            Dim entry As DirectoryEntry = New DirectoryEntry(Domain, UserID, Password, AuthenticationTypes.Secure)
            Try

                Dim obj As Object = entry.NativeObject
                If Request.QueryString("ReturnUrl") IsNot Nothing Then
                    Response.Redirect(Request.QueryString("ReturnUrl"))
                Else
                    Response.Redirect("Default.aspx")
                End If
            Catch ex As Exception
                Response.Write("抱歉，域验证出错！")

            End Try
            'If adRoot IsNot Nothing Then
            '    If Request.QueryString("ReturnUrl") IsNot Nothing Then
            '        Response.Redirect(Request.QueryString("ReturnUrl"))
            '    Else
            '        Response.Redirect("Default.aspx")
            '    End If
            'End If





        End If

        'Dim tokenValue As String = Me.getGuidString()
        '' UsersManager.TokenInsert(tokenValue, UserID, Session.Timeout)
        ''跳转回分站
        'If Request.QueryString("ReturnUrl") IsNot Nothing Then
        '    Dim ReturnUrl As String = Request.QueryString("ReturnUrl")

        '    Dim url As String = Server.UrlDecode(SSOEncrypt.Decrypt(ReturnUrl))

        '    url = Regex.Replace(url, "(\?|&)Token=.*", "", RegexOptions.IgnoreCase)
        '    If url.IndexOf("?") >= 0 Then
        '        url = url + "&Token=" + tokenValue
        '    Else
        '        url = url + "?Token=" + tokenValue
        '    End If
        '    '  Response.Write(url)
        '    Response.Redirect(url)
        'Else
        '    ' Response.Redirect("LoginList.aspx")
        'End If
        'Else
        '    Response.Write("抱歉，帐号或密码有误！")
        'End If
        'Else
        'Response.Write("抱歉，帐号不能为空!")

    End Sub

    ''' <summary>
    ''' 产生绝对唯一字符串，用于令牌
    ''' </summary>
    ''' <returns></returns>
    Private Function getGuidString() As String
        Return Guid.NewGuid().ToString().ToUpper()
    End Function

End Class
