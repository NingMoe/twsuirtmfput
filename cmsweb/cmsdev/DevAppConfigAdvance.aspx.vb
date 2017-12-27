Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DevAppConfigAdvance
    Inherits AspPage

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
        Try
            If SStr("DEV_MANAGER") <> "1" Then 'У���Ƿ���ȷ��¼
                Response.Redirect("/cmsweb/cmsdev/DevLogin.aspx", False)
                Return
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnGetOldValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetOldValue.Click
        Try
            txtValue.Text = CmsConfig.GetString(txtSection.Text.Trim(), txtKey.Text.Trim())
        Catch ex As Exception
            PromptMsg("��ȡ������Ϣ�쳣ʧ�ܣ�������Ϣ��" & ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            CmsConfig.SetString(txtSection.Text.Trim(), txtKey.Text.Trim(), txtValue.Text)

            CmsConfig.ReloadAll()

            'PromptMsg("������Ϣ�޸ĳɹ���")
        Catch ex As Exception
            PromptMsg("������Ϣ�޸��쳣ʧ�ܣ�������Ϣ��" & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
    End Sub
End Class

End Namespace
