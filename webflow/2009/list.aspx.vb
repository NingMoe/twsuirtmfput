Imports Unionsoft.Workflow
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class list
    Inherits UserPageBase

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Me.IsPostBack Then
            BindData()
        End If

        txtkeyvalue.Attributes.Add("onkeydown", "return execSearch()")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSearch.Click
        BindData()
    End Sub

    Private Sub BindData()
        Dim strCondition As String = ""
        Dim strValue As String = "FlowName LIKE '%{0}%' OR MAINFIELDVALUE LIKE '%{0}%' OR CREATORNAME LIKE '%{0}%' OR CreateTime LIKE '%{0}%' "
        If Me.txtkeyvalue.Text.Trim() <> "" Then
            strCondition = String.Format(strValue, Me.txtkeyvalue.Text.Trim())
        End If
            Dim dt As DataTable = Worklist.GetWorklistItems(MyBase.CurrentUser.Code, strCondition)
        Me.Repeater1.DataSource = dt
        Me.Repeater1.DataBind()

        lblInfo.Text = "��" & dt.Rows.Count & "����������"


    End Sub
End Class

End Namespace
