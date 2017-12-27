Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ReportGetSql
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
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Try
            '格式：SELECT JBYS_GDCODE AS [定单号], JBYS_KHCODE AS [客户 编号] FROM CT186449308402
            txtSql.ReadOnly = True
            txtSql.Text = ""
            Dim lngResID As Long = VLng("PAGE_RESID")
            Dim ds As DataSet = CTableStructure.GetColumnsByDataset(CmsPass, lngResID, False, True)
            If ds Is Nothing OrElse ds.Tables(0).DefaultView.Count <= 0 Then Return
            Dim strTable As String = CmsPass.GetDataRes(lngResID).ResTable
            If strTable = "" Then Return

            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            Dim strSql As String = " SELECT "
            For Each drv In dv
                Dim strColName As String = DbField.GetStr(drv, "CD_COLNAME")
                Dim strColDispName As String = DbField.GetStr(drv, "CD_DISPNAME")
                strColDispName = strColDispName.Replace(" ", "_") '更换所有空格为_，方便Reporting Service中使用
                strSql &= strColName & " AS [" & strColDispName & "], "
            Next
            strSql = StringDeal.Trim(strSql, "", ",")
            strSql &= " FROM " & strTable
            txtSql.Text = strSql
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
