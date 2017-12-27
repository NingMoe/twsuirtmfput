Namespace Unionsoft.Workflow.Web

Partial Class AdminSearch
    Inherits System.Web.UI.Page

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtWorkflowID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecieveDate As System.Web.UI.WebControls.TextBox

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Dim strWorkflowID As String = Request.QueryString("WorkflowID")
        Dim strWorkflowMainFieldValue As String = Me.txtWorkflowTitle.Text
        Dim strCreateDate As String = Me.txtCreateDate.Text
        Dim strCreateDate1 As String = Me.txtCreateDate1.Text

        Response.Redirect("AdminWorkflowInstances.aspx?WorkflowID=" & strWorkflowID & _
                          "&WorkflowMainFieldValue=" & Server.UrlEncode(strWorkflowMainFieldValue) & _
                          "&CreateDate=" & strCreateDate & _
                          "&CreateDate1=" & strCreateDate1 _
                         )

    End Sub


End Class

End Namespace
