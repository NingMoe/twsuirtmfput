Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmployeeSetPass
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label

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

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblEmpID.Text = RStr("empid") 'SStr("EMPPASS_EMPID")
        lblEmpName.Text = RStr("empname")
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strOldPass As String = txtOldPass.Text.Trim()
            Dim strNewPass1 As String = txtNewPass1.Text.Trim()
            Dim strNewPass2 As String = txtNewPass2.Text.Trim()
            If strNewPass1 <> strNewPass2 Then
                PromptMsg("������ȷ�ϴ������������룡")
                Return
            End If
            If strNewPass1.Trim().Length > 20 Then
                Throw New CmsException("�û����볤�Ȳ��ܳ���20λ��")
            End If

            'If OrgFactory.EmpDriver.ValidatePass(SDbConnectionPool.GetDbConfig(), lblEmpID.Text.Trim(), strOldPass) Then
            '    OrgFactory.EmpDriver.SetPass(CmsPass, lblEmpID.Text.Trim(), strNewPass1)
            '    PromptMsg("�����޸ĳɹ���") '�ɹ���Ϣ�����Ǵ���
            '    Return
            'Else
            '    PromptMsg("������У��ʧ�ܣ����������룡")
            '    Return
            'End If
            'OrgFactory.EmpDriver.UpdatePassword(CmsPass.Employee.ID, strOldPass, strNewPass1)

            OrgFactory.EmpDriver.SetPass(CmsPass, lblEmpID.Text.Trim(), strNewPass1)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(SStr("EMPPASS_BACKPAGE"), False)
    End Sub
End Class

End Namespace
