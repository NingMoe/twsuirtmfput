Namespace Unionsoft.Workflow.Web

Partial Class AdminSearch
    Inherits System.Web.UI.Page

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtWorkflowID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecieveDate As System.Web.UI.WebControls.TextBox

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
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
