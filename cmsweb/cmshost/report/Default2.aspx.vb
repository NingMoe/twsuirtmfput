Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Xml
Imports System.Text
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft

Partial Class report_Default2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LoadBind()
            QJBind()
        End If
    End Sub

    Protected Sub LoadBind()
      
        Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"), CmsPassport)
        Dim Sql As String = " select EmpCode,EmpName,EntryDate,Department,Email from HR_Employee where EmpCode=" & pst.Employee.ID & ""


        Dim dt As DataTable = SDbStatement.Query(Sql).Tables(0)
        Repeater1.DataSource = dt
        Repeater1.DataBind()
    End Sub
    Protected Sub QJBind()
        Dim times As String = seYear.Value.ToString()
        If times = "" Then
            times = DateTime.Now.ToString("yyyy")
        End If
        Dim Months As String = seMonth.Value.ToString()
        If Months = "" Then
            Months = DateTime.Now.ToString("MM")
        End If
        Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"), CmsPassport)
        'Dim Sql1 As String = " select EmpCode,EmpName,Askdate,Leavetype,LeaveDays from HR_Leave where EmpCode=" & pst.Employee.ID & ""
        Dim Sql1 As String = " select EmpCode,EmpName,convert(varchar(4),Askdate,112) as AskYear,convert(varchar(2),Askdate,101) as AskMonth,Askdate,Leavetype,LeaveDays from HR_Leave where convert(varchar(4),Askdate,112)='" + times + "' and convert(varchar(2),Askdate,101)='" + Months + "' and EmpCode=" & pst.Employee.ID & ""


        Dim dt1 As DataTable = SDbStatement.Query(Sql1).Tables(0)
        Repeater2.DataSource = dt1
        Repeater2.DataBind()
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        QJBind()
        JBBind()
    End Sub
    Protected Sub JBBind()
        Dim times As String = seYear.Value.ToString()
        If times = "" Then
            times = DateTime.Now.ToString("yyyy")
        End If
        Dim Months As String = seMonth.Value.ToString()
        If Months = "" Then
            Months = DateTime.Now.ToString("MM")
        End If
        Dim pst As CmsPassport = CType(Session("CMS_PASSPORT"), CmsPassport)
        'Dim Sql1 As String = " select EmpCode,EmpName,Askdate,Leavetype,LeaveDays from HR_Leave where EmpCode=" & pst.Employee.ID & ""
        Dim Sql2 As String = " select EmpCode,EmpName,convert(varchar(4),StartDate,112) as AskYear,convert(varchar(2),StartDate,101) as AskMonth,StartDate,Overtime,Explain from HR_OverTime where convert(varchar(4),StartDate,112)='" + times + "'and convert(varchar(2),StartDate,101)='" + Months + "'  and EmpCode=" & pst.Employee.ID & ""


        Dim dt2 As DataTable = SDbStatement.Query(Sql2).Tables(0)
        Repeater3.DataSource = dt2
        Repeater3.DataBind()
    End Sub





End Class
