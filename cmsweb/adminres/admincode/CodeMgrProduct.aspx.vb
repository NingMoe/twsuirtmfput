Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CodeMgrProduct
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
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

        '判断有无正确的产品码
        Dim strProdCode As String = CmsCodeProduct.GetProductCode(CmsPass)
        If strProdCode <> "" Then '有产品码
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
