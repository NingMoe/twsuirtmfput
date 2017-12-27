Option Strict On
Option Explicit On 

Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DevLogin
    Inherits AspPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnExit As System.Web.UI.WebControls.Button

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
            PageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�
            PageInitialize() '��ʼ��ҳ��

            If Not IsPostBack Then
                PageDealFirstRequest() '�����һ��GET�����е�����
            Else
                PageDealPostBack() '����POST�е�����
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------
    '������Ĳ�������ΪViewState������������ҳ������ȡ���޸ġ�
    '--------------------------------------------------------------------------
    Private Sub PageSaveParametersToViewState()
    End Sub

    '--------------------------------------------------------------------------
    '��ʼ��ҳ��
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        Session("DEV_MANAGER") = "0"
    End Sub

    '--------------------------------------------------------------------------
    '�����һ��GET�����е�����
    '--------------------------------------------------------------------------
    Private Sub PageDealFirstRequest()
    End Sub

    '--------------------------------------------------------------------------
    '����POST�е�������أ�True���˳����ӿں�ֱ���˳����壻False���˳����ӿں����֮��Ĵ���
    '--------------------------------------------------------------------------
    Private Function PageDealPostBack() As Boolean
        Response.Write("<br>")
        Response.Write(CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE"))
        Response.Write("<br>")
            Response.Write(Encrypt.Encrypt(txtCode.Text.Trim()))
        Response.Write("<br>")

        'If CmsEncrypt.Encrypt(txtCode.Text.Trim()) = CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE") Then
        '    Session("DEV_MANAGER") = "1"
        '    Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
        'Else
        '    Session("DEV_MANAGER") = "0"
        '    PromptMsg("У�������")
        'End If
        If OrgFactory.EmpDriver.EncryptPass(txtCode.Text.Trim()) = CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE") Then
            Session("DEV_MANAGER") = "1"
            Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
        Else
            Session("DEV_MANAGER") = "0"
            PromptMsg("У�������")
        End If
    End Function
End Class

End Namespace
