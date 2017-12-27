Imports NetReusables
Imports Unionsoft.Platform
Imports System.DirectoryServices
Imports System.Data.sqlclient
Imports System.DATA
Partial Class adminsys_ImportDomainUsers
    Inherits Page
   
    Property dt() As DataTable
        Get
            Return ViewState("CMS_EMPLOYEE")
        End Get
        Set(ByVal value As DataTable)
            ViewState("CMS_EMPLOYEE") = value
        End Set
    End Property
    Property dtDept() As DataTable
        Get
            Return ViewState("CMS_DEPARTMENT")
        End Get
        Set(ByVal value As DataTable)
            ViewState("CMS_DEPARTMENT") = value
        End Set
    End Property
    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If CmsPass.EmpIsSysAdmin = False Then  '只有系统管理员能进入
        '    Response.Redirect("/cmsweb/ErrorMessage.aspx?code=5", False)
        '    Return
        'End If
        If Not IsPostBack Then
            Me.GridView1.DataSource = ExportDomain()
            Me.GridView1.DataBind()
        End If
    End Sub
    Function ExportDomain() As DataTable

        Dim sql As String = "SELECT CD_COLNAME,CD_DISPNAME FROM  CMS_TABLE_DEFINE WHERE CD_RESID IN (select ID from dbo.CMS_RESOURCE where res_table='CMS_EMPLOYEE') AND CD_COLNAME LIKE 'Domain_%' order by CD_ID"
        dt = New DataTable
        Dim dtCol As DataTable = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), sql).Tables(0)
        Me.GridView1.Columns.Clear()
        Dim dcf As BoundField
        For i As Integer = 0 To dtCol.Rows.Count - 1

            dcf = New BoundField
            dcf.HeaderText = dtCol.Rows(i)("CD_DISPNAME")
            dcf.DataField = dtCol.Rows(i)("CD_COLNAME")
            dt.Columns.Add(dtCol.Rows(i)("CD_COLNAME"))
            GridView1.Columns.Add(dcf)
        Next

       




        '= CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "select * from dbo.CMS_EMPLOYEE where 1=0").Tables(0)
        Dim dr As DataRow
        Dim sLDAP As String
        'sLDAP = "LDAP://ou=公司员工,DC=SHCRLAND,DC=com"
        sLDAP = CmsConfig.GetString("DomainServer", "LDAP")
        Dim adRoot As DirectoryEntry = New DirectoryEntry(sLDAP)
        Dim mySearcher As DirectorySearcher = New DirectorySearcher(adRoot)
        mySearcher.Filter = "(&(objectCategory=person)(objectClass=user))"
        For Each resEnt As SearchResult In mySearcher.FindAll()
            dr = dt.NewRow

            Dim user As DirectoryEntry = resEnt.GetDirectoryEntry()
            dr("Domain_adsPath") = user.Path

            For Each pvc As PropertyValueCollection In user.Properties
                If dt.Columns.Contains("Domain_" + pvc.PropertyName) Then
                    dr("Domain_" + pvc.PropertyName) = pvc.Value.ToString
                End If
            Next

            dt.Rows.Add(dr)
        Next
        Return dt
      
    End Function
  
    Public Sub UpdateDataTable()
        Try

      
            dtDept = GetDeptOU()
            dt.PrimaryKey = New DataColumn() {dt.Columns("Domain_sAMAccountName")}
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("select * from dbo.CMS_EMPLOYEE", GetConnectionString)
            Dim sqlCmdBuilder = New SqlClient.SqlCommandBuilder(da)
            Dim dttemp As DataTable = New DataTable
            da.Fill(dttemp)
            dttemp.PrimaryKey = New DataColumn() {dttemp.Columns("emp_id")}
            Dim dr As DataRow
            For i As Integer = 0 To dt.Rows.Count - 1
                dr = dt.Rows(i)
                Dim sn As String = dr("Domain_sn") & ""
                Dim givenName As String = dr("Domain_givenName") & ""
                Dim drtemp As DataRow = dttemp.Rows.Find(dr("Domain_sAMAccountName"))
                If drtemp Is Nothing Then

                    drtemp = dttemp.NewRow
                    drtemp("Emp_id") = dr("Domain_sAMAccountName")
                    drtemp("host_id") = GetDeptID(dr("Domain_adspath"))
                    drtemp("id") = TimeId.CurrentMilliseconds(30)
                    drtemp("emp_name") = sn + givenName
                    For j As Integer = 0 To dt.Columns.Count - 1
                        drtemp(dt.Columns(j).ColumnName) = dr(j)
                    Next
                    dttemp.Rows.Add(drtemp)
                Else
                    For j As Integer = 0 To dt.Columns.Count - 1
                        drtemp(dt.Columns(j).ColumnName) = dr(j)
                    Next
                    drtemp("emp_name") = sn + givenName
                    Dim host_id As Int64 = GetDeptID(dr("Domain_adspath"))
                    If drtemp("host_id") = 0 Then
                        drtemp("host_id") = host_id
                    End If
                End If

            Next
            da.Update(dttemp)
            Response.Write("<script>alert('同步完成')</script>")
        Catch ex As Exception

       

            ' dttemp.AcceptChanges()



      
            Response.Write(ex.Message)
        End Try
    End Sub
    Function GetDeptID(ByVal AdsPath As String) As Int64
        For Each dr As DataRow In dtDept.Rows
            If AdsPath.IndexOf(dr("AD_OU").ToString.ToUpper) >= 0 Then
                Return dr("id")
           
            End If
        Next
        Return 0
    End Function
    Function GetDeptOU() As DataTable
        Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("select ID,AD_OU from dbo.CMS_DEPARTMENT WHERE ISNULL(AD_OU,'')<>''", GetConnectionString)
        Dim dt As DataTable = New DataTable
        da.Fill(dt)
        Return dt
    End Function
    Function GetConnectionString() As String
        Dim DBName As String = CmsConfig.GetString("SYS_DATABASE", "DATABASE")
        Dim ServerName As String = CmsConfig.GetString("SYS_DATABASE", "HOST")
        Dim m_strDbUser As String = NetReusables.Encrypt.Decrypt(CmsConfig.GetString("SYS_DATABASE", "UserID"))
        Dim m_strDbUserPass As String = NetReusables.Encrypt.Decrypt(CmsConfig.GetString("SYS_DATABASE", "Password"))
        Return "Data Source=" + ServerName + "; Initial Catalog=" + DBName + ";User ID=" + m_strDbUser + ";Password=" + m_strDbUserPass
    End Function
    Protected Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        UpdateDataTable()
    End Sub
End Class
