
Partial Class Default3
    Inherits System.Web.UI.Page
    Protected dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Return
        BindData()
    End Sub

    Protected Sub BindData()
        dt = NetReusables.SDbStatement.Query("select * from CMS_EMPLOYEE order by Emp_Name").Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim pass As String = ""
            Try
                pass = NetReusables.CmsEncrypt.DecryptPassword(NetReusables.DbField.GetStr(dt.Rows(i), "EMP_PASS"))
            Catch ex As Exception
                Try
                    pass = NetReusables.Encrypt.Decrypt(NetReusables.DbField.GetStr(dt.Rows(i), "EMP_PASS")) + "(已更新)"
                Catch ex1 As Exception
                    pass = "解码错误"
                End Try
            End Try
            dt.Rows(i)("EMP_PASS") = pass
        Next
        Me.GridView1.DataSource = dt
        Me.GridView1.DataBind()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.TextBox3.Text = NetReusables.Encrypt.Encrypt(Me.TextBox2.Text.Trim)
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            NetReusables.SDbStatement.Execute("Update CMS_EMPLOYEE set EMP_PASS='" + NetReusables.Encrypt.Encrypt(Me.TextBox2.Text) + "' where Emp_ID='" + Me.TextBox1.Text + "'")
            Response.Write("<script>alert('密码修改成功');</script>")
        Catch ex As Exception
            NetReusables.SLog.Err("密码修改失败：" + ex.Message)
        End Try

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            dt = NetReusables.SDbStatement.Query("select * from CMS_EMPLOYEE order by Emp_Name").Tables(0)
            Dim strSql As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim pass As String = ""
                Try
                    pass = NetReusables.CmsEncrypt.DecryptPassword(NetReusables.DbField.GetStr(dt.Rows(i), "EMP_PASS"))
                Catch ex As Exception
                    Try
                        pass = NetReusables.Encrypt.Decrypt(NetReusables.DbField.GetStr(dt.Rows(i), "EMP_PASS"))
                        pass = "已更新"
                    Catch ex1 As Exception

                    End Try

                End Try
                If pass.Trim <> "已更新" Then strSql += "Update CMS_EMPLOYEE set EMP_PASS='" + NetReusables.Encrypt.Encrypt(pass) + "' where Emp_ID='" + NetReusables.DbField.GetStr(dt.Rows(i), "Emp_ID") + "';"
            Next
            Dim RowsCount As Integer = 0
            If strSql <> "" Then RowsCount = NetReusables.SDbStatement.Execute(strSql)
            Response.Write("<script>alert('成功更新" + RowsCount.ToString + "条数据');</script>")
        Catch ex As Exception
            NetReusables.SLog.Err("更新密码加密方式失败：" + ex.Message)
        End Try
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.TextBox5.Text = NetReusables.CmsEncrypt.EncryptPassword(Me.TextBox4.Text.Trim)
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.TextBox6.Text = NetReusables.CmsEncrypt.DecryptPassword(Me.TextBox5.Text.Trim)
    End Sub
End Class
