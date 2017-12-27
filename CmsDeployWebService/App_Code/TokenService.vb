Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports NetReusables
Public Class Authentication
    Inherits SoapHeader
    Public UserID As String
    'Public Password As String
    ' Public Domain As String
End Class
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class TokenService
     Inherits System.Web.Services.WebService

    Public tokenAuthentication As Authentication

    <WebMethod(Description:="身份验证方法")> _
    <SoapHeader("tokenAuthentication", Direction:=SoapHeaderDirection.In)> _
     Public Function GetToken() As String
        If Verification(tokenAuthentication.UserID) = True Then
            Dim token As String = Guid.NewGuid.ToString
            Users.TokenInsert(token, tokenAuthentication.UserID)
            Return token
        Else
            Return ("")
        End If
        
    End Function

    <WebMethod(Description:="身份验证方法")> _
   <SoapHeader("tokenAuthentication", Direction:=SoapHeaderDirection.In)> _
    Public Function GetTokenByTimeOut(ByVal TimeOut As Integer) As String
        If Verification(tokenAuthentication.UserID) = True Then
            Dim token As String = Guid.NewGuid.ToString
            Users.TokenInsert(token, tokenAuthentication.UserID, TimeOut)
            Return token
        Else
            Return ("")
        End If

    End Function


    Public Function Verification(ByVal UserID As String) As Boolean

        Dim UserInfo As UserInfo = Users.GetUserInfoByID(UserID.ToLower)
        If UserInfo Is Nothing Then
            Return False
        Else
            Return True
        End If
        'If UserInfo.Password = Password Then
        '    Return True
        'Else
        '    Return False
        'End If

    End Function

    ''' <summary>
    ''' 根据令牌获取用户凭证
    ''' </summary>
    ''' <param name="token">令牌</param>
    ''' <returns></returns>
    <WebMethod()> _
    Public Function GetUserInfoByToken(ByVal token As String) As UserInfo
        Dim UserInfo As UserInfo = Nothing
        Try

            Dim dt As DataTable = Users.GetAllLoginUsers()
            dt.PrimaryKey = New DataColumn() {dt.Columns("Guid")}
            If dt IsNot Nothing Then
                Dim dr As DataRow = dt.Rows.Find(token.ToLower)
                If dr IsNot Nothing Then
                    UserInfo = Users.GetUserInfoByID(dr("UserID"))

                End If
            End If
            ' Users.RemoveToken(token)
            Return UserInfo

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' 添加令牌
    ''' </summary>
    ''' <param name="token">令牌</param>
    ''' <param name="UserID">凭证</param>
    Public Shared Sub TokenInsert(ByVal token As String, ByVal UserID As String)


        Dim has As Hashtable = New Hashtable
        has.Add("Guid", token)
        has.Add("UserID", UserID.ToLower)
        has.Add("loginDatetime", Now)
        SDbStatement.InsertRow(has, "Cms_LoginUsers")
      

    End Sub
End Class
