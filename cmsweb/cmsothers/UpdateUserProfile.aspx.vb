Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class UpdateUserProfile
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

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        btnConfirm.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")
        'txtEmpSender.Attributes.Add("fType", "email")
        'txtEmpSender.Attributes.Add("errmsg", "[" & CmsPass.EmpEmailSender & "]")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'Load�û�Profileԭ��Ϣ
        txtEmpID.Text = CmsPass.Employee.ID
        txtEmpName.Text = CmsPass.Employee.Name
        txtEmpHandphone.Text = CmsPass.Employee.Handphone
        txtEmpSender.Text = CmsPass.Employee.Email
        txtSmtpServer.Text = CmsPass.Employee.EmailSmtp
        txtSMTPAccount.Text = CmsPass.Employee.EmailAccount
        txtSMTPPass.Text = CmsPass.Employee.EmailPassword

        ddlLanguage.Items.Clear()
        'ddlLanguage.Items.Add(New ListItem("", ""))
        ddlLanguage.Items.Add(New ListItem("����", "����"))
        If CmsFunc.MultiLanguageEnable() = True Then '֧�ֶ�̬������
            ddlLanguage.Items.Add(New ListItem("English", "English"))
        End If
        Try
            ddlLanguage.SelectedValue = CmsPass.Employee.Language
        Catch ex As Exception
        End Try

        ddlLoginPage.Items.Clear()
        'ddlLoginPage.Items.Add(New ListItem("", ""))
        ddlLoginPage.Items.Add(New ListItem("���ݹ���CMS", "CMS"))
        If CmsFunc.IsEnable("FUNC_WORKFLOW") = True Then  '֧�ֹ�����
            ddlLoginPage.Items.Add(New ListItem("������WORKFLOW", "FLOW"))
        End If
        Try
            ddlLoginPage.SelectedValue = CmsPass.Employee.Mainpage
        Catch ex As Exception
        End Try

        If CmsFunc.MultiLanguageEnable() = False Then '��֧�ֶ�����
            ddlLanguage.Visible = False
            lblLanguage.Visible = False
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            'У�鷢�ͷ������ʼ�
            Dim strTemp As String = txtEmpSender.Text.Trim()
            If strTemp <> "" AndAlso (strTemp.StartsWith("<") = True Or strTemp.IndexOf("<") < 0 Or strTemp.IndexOf(">") < 0 Or strTemp.IndexOf("@") < 0 Or strTemp.IndexOf(".") < 0) Then
                PromptMsg("��������Ч�ķ��ͷ������ʼ�")
                Return
            End If

            '�������ݿ�
            OrgFactory.EmpDriver.SetUserProfile(CmsPass, CmsPass.Employee.ID, txtEmpName.Text.Trim(), txtEmpHandphone.Text.Trim(), txtEmpSender.Text.Trim(), txtSmtpServer.Text.Trim(), txtSMTPAccount.Text.Trim(), txtSMTPPass.Text.Trim(), ddlLanguage.SelectedValue, ddlLoginPage.SelectedValue)

            '���»���
            CmsPassport.RetrieveUserInfoWithEncryptPass(CmsPass, SDbConnectionPool.GetDbConfig(), CmsPass.Employee.ID, CmsPass.Employee.Password, Request.UserHostAddress())

            'CmsPass.Employee.Name = txtEmpName.Text.Trim()
            'CmsPass.Employee.Handphone = txtEmpHandphone.Text.Trim()
            'CmsPass.Employee.Email = txtEmpSender.Text.Trim()
            'CmsPass.Employee.EmailSmtp = txtSmtpServer.Text.Trim()
            'CmsPass.Employee.EmailAccount = txtSMTPAccount.Text.Trim()
            'CmsPass.Employee.EmailPassword = txtSMTPPass.Text.Trim()
            'CmsPass.Employee.Language = ddlLanguage.SelectedValue
            'CmsPass.Employee.Mainpage = ddlLoginPage.SelectedValue

            'OrgFactory.EmpDriver.UpdateEmployee(CmsPass.Employee)

            PromptMsg("�û�������Ϣ�޸ĳɹ���")
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub
End Class
End Namespace
