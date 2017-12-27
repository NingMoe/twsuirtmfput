
Partial Class ErrorMessage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("Code") IsNot Nothing Then
            Select Case Request.QueryString("Code")
                Case 0

                Case 1
                    Me.spanMessage.InnerText = "请配置域信息"
                Case 2
                    Me.spanMessage.InnerText = "域验证失败,请检查用户名及密码!"
                Case 3
                    If Request.QueryString("UserID") Is Nothing Then
                        Me.spanMessage.InnerText = "该域用户未同步到数据库,请联系管理员!"
                    Else
                        Me.spanMessage.InnerText = "域用户" + Request.QueryString("UserID") + "未同步到数据库,请联系管理员!"
                    End If
                    Me.spanMessage.InnerText = "该域用户未同步到数据库,请联系管理员!"
                Case 4
                    Me.spanMessage.InnerText = "请检查数据库连接!"
                Case 5
                    Me.spanMessage.InnerText = "你无权限此操作!"
            End Select
        End If
    End Sub
End Class
