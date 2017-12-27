Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class login1
    Inherits PageBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '在此处放置初始化页的用户代码
        'Response.Write(NetReusables.CmsEncrypt.Decrypt("R1gn27haQnUi0Txi9Q/DjQ=="))
        'Response.End()
    End Sub


        Protected Sub Login(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim oEmployee As Employee
            Dim pst As CmsPassport
            If Me.txtUserName.Text <> "" Then
                Try
                    pst = CmsPassport.GenerateCmsPassport(Me.txtUserName.Text, Me.txtPassWord.Text, Request.UserHostAddress)
                    Session.Item("CMS_PASSPORT") = pst

                    If Not pst Is Nothing Then
                        oEmployee = OrganizationFactory.Implementation.GetEmployee(Me.txtUserName.Text)
                        oEmployee.Password = Me.txtPassWord.Text
                        Session("User") = oEmployee
                        If oEmployee.Code <> "" Then
                            If oEmployee.Code <> "admin" Then
                                Response.Redirect(Request.Path.Substring(0, Request.Path.LastIndexOf("/")))
                            Else
                                Response.Redirect("../admin/AdminIndex.aspx")
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Me.MessageBox("用户登录帐号或密码错误，请重新输入！")
                    SLog.Err("用户登录帐号或密码错误，请重新输入！", ex)
                End Try
            End If
            ' OrganizationFactory.Implementation.GetEmployee("tq")
        End Sub



End Class

End Namespace
