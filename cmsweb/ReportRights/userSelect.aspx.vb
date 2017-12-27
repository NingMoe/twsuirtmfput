Imports NetReusables
Imports System.Data
Imports Unionsoft

Partial Class userSelect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim mnuresid As String = Request.QueryString("mnuresid")
            Dim mnurecid As String = Request.QueryString("mnurecid")

            Dim sqlStr As String = "select EMP_NAME,EMP_ID from CMS_EMPLOYEE where status<>1 and [HOST_ID]<>0 order by EMP_Name asc "
            Dim ds As DataSet = SDbStatement.Query(sqlStr)

            Me.DataMan.DataSource = ds
            Me.DataMan.DataBind()

            Dim sqlStrS As String = "select name=(select EMP_NAME from CMS_EMPLOYEE where EMP_ID=C.EmpCode) from RP_ReportRight C where Rresid='" + mnuresid + "' and Rrecid='" + mnurecid + "'"
            Dim dsS As DataSet = SDbStatement.Query(sqlStrS)
            For j As Integer = 0 To dsS.Tables(0).Rows.Count - 1

                For i As Integer = 0 To Me.DataMan.Items.Count - 1
                    Dim chb As CheckBox = Me.DataMan.Items(i).FindControl("ckbUser")
                    If dsS.Tables(0).Rows(j)("name") = chb.Text Then
                        chb.Checked = True

                    End If
                Next
            Next
        End If
    End Sub

    '确认
    Protected Sub butSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSub.Click
        Dim mnuresid As String = Request.QueryString("mnuresid")
        Dim mnurecid As String = Request.QueryString("mnurecid")

        Dim selCount As Integer = 0
        Dim result As Integer = SDbStatement.DelRows("RP_ReportRight", "   Rresid='" + mnuresid + "' and Rrecid='" + mnurecid + "' ")
        For i As Integer = 0 To Me.DataMan.Items.Count - 1
            Dim chb As CheckBox = Me.DataMan.Items(i).FindControl("ckbUser")
            If chb.Checked = True Then
                selCount = selCount + 1
                Dim ID As String = NetReusables.TimeId.CurrentMilliseconds(30)
                Dim sql As String = "select EMP_ID from CMS_EMPLOYEE where EMP_NAME='" + chb.Text + "'"
                Dim ds As DataSet = SDbStatement.Query(sql)

                Dim hs As Hashtable = New Hashtable()
                hs.Add("ID", ID)
                hs.Add("RESID", "434737930678")
                hs.Add("CRTTIME", Now.ToString("yyyy-MM-dd hh:MM:ss.ffff"))
                hs.Add("EDTTIME", Now.ToString("yyyy-MM-dd hh:MM:ss.ffff"))
                hs.Add("Rresid", mnuresid)
                hs.Add("Rrecid", mnurecid)
                hs.Add("EmpCode", ds.Tables(0).Rows(0)("EMP_ID"))

                SDbStatement.InsertRow(hs, "RP_ReportRight")
            End If
        Next

        If selCount > 0 Then
            Response.Write("<script>alert('操作成功!'); window.location.href='mnures.aspx?mnuresid=" + mnuresid + "&mnurecid=" + mnurecid + "';</script>")
        Else
            Response.Write("<script>alert('请选择您要授权的用户!')</script>")
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mnuresid As String = Request.QueryString("mnuresid")
        Dim mnurecid As String = Request.QueryString("mnurecid")
        Dim result As Integer = SDbStatement.DelRows("RP_ReportRight", "   Rresid='" + mnuresid + "' and Rrecid='" + mnurecid + "' ")
        Response.Write("<script>window.location.href='mnures.aspx?mnuresid=" + mnuresid + "&mnurecid=" + mnurecid + "';</script>")
    End Sub
End Class
