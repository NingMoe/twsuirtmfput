Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class UpdatePassword
    Inherits CmsPage

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
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        If txtNewPass1.Text <> txtNewPass2.Text Then
                PromptMsg("��������ͬ��������/Please enter the same new password!")
            Return
        End If

        Try
            'OrgFactory.EmpDriver.UpdatePassword(CmsPass.Employee.ID, txtOldPass.Text, txtNewPass2.Text)
            OrgFactory.EmpDriver.SetPass(CmsPass, CmsPass.Employee.ID, txtNewPass2.Text)
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

    End Sub
End Class

End Namespace
