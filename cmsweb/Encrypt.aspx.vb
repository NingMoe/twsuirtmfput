
Partial Class Encrypt
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtValue.Text = NetReusables.Encrypt.Encrypt(txtValue.Text)
    End Sub
End Class
