Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports Unionsoft.Implement
Imports Unionsoft.Platform
Imports NetReusables
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Passport
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetAllUsers() As list(Of UserInfo)
        Return Users.GetAllUsers


    End Function
    <WebMethod()> _
       Public Function Edit(ByVal UserInfo As UserInfo) As Boolean
        Dim hst As Hashtable = New Hashtable
        hst.Add("emp_id", UserInfo.ID)
        hst.Add("emp_Name", UserInfo.Name)
        'hst.Add("emp_Pass", UserInfo.Password)
        hst.Add("Domain_TelephoneNumber", UserInfo.TelephoneNumber)
        hst.Add("Domain_Title", UserInfo.Title)
        hst.Add("Domain_Mobile", UserInfo.Mobile)
        hst.Add("Domain_Mail", UserInfo.Email)
        hst.Add("Emp_email", UserInfo.Email)
        Try
            SDbStatement.UpdateRows(hst, "CMS_EMPLOYEE", "emp_id='" + UserInfo.ID + "'")
            Return True
        Catch ex As Exception
            Return False
        End Try
        

    End Function
    <WebMethod()> _
      Public Function ChangePassword(ByVal UserID As String, ByVal NewPassword As String) As Boolean
        Dim hst As Hashtable = New Hashtable
        hst.Add("emp_id", UserID)

        hst.Add("emp_Pass", NetReusables.Encrypt.Encrypt(NewPassword))
        Try
            SDbStatement.UpdateRows(hst, "CMS_EMPLOYEE", "emp_id='" + UserID + "'")
            Return True
        Catch ex As Exception
            Return False
        End Try
       

    End Function

    <WebMethod()> _
      Public Function Add(ByVal UserInfo As UserInfo) As Boolean
        Dim hst As Hashtable = New Hashtable
        hst.Add("id", TimeId.CurrentMilliseconds(30))
        hst.Add("emp_id", UserInfo.ID)
        hst.Add("emp_Name", UserInfo.Name)
        'hst.Add("emp_Pass", UserInfo.Password)
        hst.Add("Domain_TelephoneNumber", UserInfo.TelephoneNumber)
        hst.Add("Domain_Title", UserInfo.Title)
        hst.Add("Domain_Mobile", UserInfo.Mobile)
        hst.Add("Domain_Mail", UserInfo.Email)
        hst.Add("Emp_email", UserInfo.Email)
        Try
            SDbStatement.InsertRow(hst, "CMS_EMPLOYEE")
            Return True
        Catch ex As Exception
            Return False
        End Try
       

    End Function
    <WebMethod()> _
      Public Function Delete(ByVal UserID As String) As Boolean
        Dim hst As Hashtable = New Hashtable
        hst.Add("emp_id", UserID)

        hst.Add("Status", 1)

        Try
            SDbStatement.UpdateRows(hst, "CMS_EMPLOYEE", "emp_id='" + UserID + "'")
            Return True
        Catch ex As Exception
            Return False
        End Try
       
       

    End Function

    <WebMethod()> _
     Public Function Login(ByVal UserID As String, ByVal Password As String) As Boolean
        If UserID.IndexOf("\") > 0 Then

            Dim DomainName As String = UserID.Split("\")(0)
            Dim UserName As String = UserID.Split("\")(1)
            Dim Domaincls As Domain = New Domain
            If Domaincls.ValidUser(UserName, DomainName, Password) = True Then

                Return True

            Else
                Return False
            End If
        Else

            Dim pst As CmsPassport = CmsPassport.GenerateCmsPassport(UserID, Password)
            If pst Is Nothing Then
                Return False
            Else
                Return True
            End If
        End If

    End Function
End Class
