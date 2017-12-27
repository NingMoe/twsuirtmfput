Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CodeMgrProduct
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
        lblProdCodeNotes.Text = CmsMessage.GetNotes(CmsPass, "NOTES_PRODCODE_INTRODUCE")
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        txtCode1.ReadOnly = True
        txtProdSymbol.ReadOnly = True
        txtProdVer.ReadOnly = True
        txtProdSNum.ReadOnly = True

        '�ж�������ȷ�Ĳ�Ʒ��
        Dim strProdCode As String = CmsCodeProduct.GetProductCode(CmsPass)
        If strProdCode <> "" Then '�в�Ʒ��
            txtCode1.Text = strProdCode.Substring(0, 4) & "-" & strProdCode.Substring(4, 4) & "-" & strProdCode.Substring(8, 4) & "-" & strProdCode.Substring(12, 4) & "-" & strProdCode.Substring(16, 4)
            txtProdSymbol.Text = CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODID'")
            txtProdVer.Text = CmsDbStatement.GetFieldStr(SDbConnectionPool.GetDbConfig(), "PDINF_VALUE", CmsTables.ProductInfo, "PDINF_NAME='CMSCD_PRODVER'")
            txtProdSNum.Text = CStr(CmsCodeProduct.GetProductSerialNum(CmsPass))
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
