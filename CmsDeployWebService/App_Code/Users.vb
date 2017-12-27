Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports NetReusables
Public Class Users
    Public Shared Function GetAllUsers() As List(Of UserInfo)
        Dim myList As List(Of UserInfo) = New List(Of UserInfo)
        Dim sql As String = "select E.*,D.Name AS DeptName from dbo.CMS_EMPLOYEE E " _
                        & " join dbo.CMS_DEPARTMENT D on E.Host_ID=D.ID;select id,pid,name from CMS_DEPARTMENT"
        Dim dt, dtCompany As DataTable
        With SDbStatement.Query(sql)
            dt = .Tables(0)
            dtCompany = .Tables(1)
        End With
        Dim UserInfo As UserInfo
        For Each dr As DataRow In dt.Rows
            UserInfo = New UserInfo
            UserInfo.ID = dr("EMP_ID").ToString.ToLower
            UserInfo.Name = dr("EMP_Name") & ""
            UserInfo.Status = dr("Status") & ""

            UserInfo.DepartmentID = dr("host_id")
            UserInfo.DepartmentName = dr("DeptName") & ""
            UserInfo.Password = dr("emp_pass") & ""
            
            Try
                UserInfo.HeadShip = dr("HeadShip") & ""
            Catch ex As Exception

            End Try
            myList.Add(UserInfo)
        Next
        Return myList
    End Function

    Public Shared Sub RemoveToken(ByVal token As String)
        Try
            SDbStatement.Execute("delete cms_LoginUsers where Guid='" + token + "'")
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function GetFirstDepartment(ByVal dtDept As DataTable, ByVal DeptID As String) As String
        Dim drs As DataRow() = dtDept.Select("id=" + DeptID)
        If drs.Length > 0 Then

            If drs(0)("pid") <> 0 Then
                Return GetFirstDepartment(dtDept, drs(0)("pid"))
            Else
                Return drs(0)("id").ToString + vbCrLf + drs(0)("name")
            End If
        Else
            Return "" + vbCrLf + ""
        End If

        

    End Function

    Public Shared Function GetAllLoginUsers() As DataTable
        Return SDbStatement.Query("select * from CMS_LoginUsers where dateadd(minute,isnull(Timeout,0),LoginDateTime)>getdate()").Tables(0)

    End Function

    ''' <summary>
    ''' ÃÌº”¡Ó≈∆
    ''' </summary>
    ''' <param name="token">¡Ó≈∆</param>
    ''' <param name="UserID">∆æ÷§</param>
    Public Shared Sub TokenInsert(ByVal token As String, ByVal UserID As String, Optional ByVal TimeOut As Integer = 30)

        ' Dim sql As String = "select * from CMS_LoginUsers where 1=0"
        Dim hst As Hashtable = New Hashtable
        hst.Add("Guid", token)
        hst.Add("UserID", UserID.ToLower)
        hst.Add("loginDatetime", Now)
        hst.Add("Timeout", TimeOut)
        SDbStatement.InsertRow(hst, "CMS_LoginUsers")



    End Sub

    Public Shared Function GetUserInfoByID(ByVal UserID As String) As UserInfo
        Dim DD As New UserInfo
        Dim myList As List(Of UserInfo) = GetAllUsers()
        For Each UserInfo As UserInfo In myList
            Dim LDAPUserID As String = UserInfo.LDAPUserID
            If LDAPUserID <> "" Then
                LDAPUserID = LDAPUserID.ToLower
            End If
            If UserInfo.ID.ToLower = UserID.ToLower Or LDAPUserID = UserID.ToLower Then
                Return UserInfo
            End If
        Next
        Return Nothing

    End Function

    Public Shared Function GetAllParentDepartmentIDByUserID(ByVal UserID As String) As String
        Dim strDepartmentID As String = ""
        Dim ds As DataSet = SDbStatement.Query("select * from CMS_DEPARTMENT;select * from   CMS_EMPLOYEE  where EMP_ID='" + UserID.Trim + "'")
        Dim dtDept As DataTable = ds.Tables(0)
        If ds.Tables(1).Rows.Count > 0 Then
            Dim ParentDeptID As String = DbField.GetStr(ds.Tables(1).Rows(0), "HOST_ID")

            GetParentDepartmentID(dtDept, ParentDeptID, strDepartmentID)
        End If
        If strDepartmentID.Trim <> "" Then strDepartmentID = strDepartmentID.Substring(1)
        Return strDepartmentID
    End Function

    Private Shared Function GetParentDepartmentID(ByVal dtDept As DataTable, ByVal DeptID As String, ByRef strDepartmentID As String) As String
        ' If Convert.ToInt64(DeptID) = 0 Then
        Dim dr() As DataRow = dtDept.Select("ID=" + DeptID)
        For i As Integer = 0 To dr.Length - 1
            strDepartmentID += "," + DbField.GetStr(dr(i), "ID")
            Dim PID As Long = DbField.GetLng(dr(i), "PID")
            If PID > -1 Then
                GetParentDepartmentID(dtDept, PID.ToString, strDepartmentID)
            End If
        Next
        Return strDepartmentID
    End Function
End Class
