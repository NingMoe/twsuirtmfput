Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class SetupProductCode
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
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�
            lblProdCodeNotes.Text = CmsMessage.GetNotes(pstTemp, "NOTES_PRODCODE_INTRODUCE")

            If IsPostBack Then Return

            '�����Ʒ���Ƿ��Ѿ�����
            Dim strProdCode As String = CmsCodeProduct.GetProductCode(pstTemp)
            If strProdCode <> "" Then '��Ʒ����֤�ɹ�
                txtCode1.Text = strProdCode.Substring(0, 4) & "-" & strProdCode.Substring(4, 4) & "-" & strProdCode.Substring(8, 4) & "-" & strProdCode.Substring(12, 4) & "-" & strProdCode.Substring(16, 4)
                btnConfirm.Enabled = False
                txtCode1.ReadOnly = True
            Else
                btnConfirm.Enabled = True
                txtCode1.ReadOnly = False
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim strCode As String = txtCode1.Text.Trim()
            strCode = strCode.Replace("-", "")
            strCode = strCode.Replace(" ", "")
            If strCode.Length <> 20 Then
                PromptMsg("��������ȷ�Ĳ�Ʒ�룡")
                Return
            End If

            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�
            Dim strProductSymbol As String = CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODID'")
            Dim strVer As String = CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODVER'")
            If CmsCodeProduct.ValidateProductCode(strCode, strVer, strProductSymbol) = False Then
                PromptMsg("��������ȷ�Ĳ�Ʒ�룡")
                Return
            Else '��Ʒ����֤�ɹ�
                '������֤��
                CmsCodeProduct.SaveProductCodeToDb(pstTemp, strCode)
                PromptMsg("�����ϵͳ�ѱ������ӭʹ�ã�")
                txtCode1.ReadOnly = True
                btnConfirm.Enabled = False

                'Response.Redirect("/cmsweb/Default.htm", False)
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'Response.Redirect("/cmsweb/Default.htm", False)
        Response.Write("<script type='text/javascript'>top.location.href='/cmsweb/Default.htm' </script>")
    End Sub
End Class

End Namespace
