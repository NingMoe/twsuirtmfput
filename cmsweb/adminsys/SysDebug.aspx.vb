Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class SysDebug
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
        If CmsPass.EmpIsSysAdmin = False AndAlso CmsPass.EmpIsDepAdmin = False AndAlso CmsPass.Employee.ID <> "sysuser" Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        ddlColRes.AutoPostBack = True
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        'txtClientCode.ReadOnly = True
        'txtClientCode.Text = DbParameter.GetString1(SDbConnectionPool.GetDbConfig(), DbParameter.CLIENT_CODE)

        chkDebugMode.Checked = CmsConfig.DebugingMode
        chkDebugMode.AutoPostBack = True

        chkDbSqlLog.Checked = CmsConfig.EnableDbSqlLog
        chkDbSqlLog.AutoPostBack = True

        chkShowColName.Checked = CmsConfig.ShowColumnName
        chkShowColName.AutoPostBack = True

        chkShowIDForCms.Checked = CmsConfig.ShowIDForCms
        chkShowIDForCms.AutoPostBack = True

        If DbParameter.GetString1(SDbConnectionPool.GetDbConfig(), DbParameter.PRODUCT_FORDEMO) = "1" Then
            chkIsDemoVer.Checked = True
        Else
            chkIsDemoVer.Checked = False
        End If
        chkIsDemoVer.AutoPostBack = True
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub lbtnClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnClearCache.Click
        CmsConfig.ReloadAll()
        'PromptMsg("成功清空系统缓存!")
    End Sub

    Private Sub lbtnGetResID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetResID.Click
        If txtRes1.Text.Trim() <> "" Then
            Dim strResID As String = ""
            Dim dv As DataView = ResFactory.ResService.GetResourcesView(CmsPass, False, "NAME='" & txtRes1.Text.Trim() & "'")
            Dim drv As DataRowView
            For Each drv In dv
                strResID &= DbField.GetStr(drv, "ID") & ", "
            Next
            txtRes2.Text = StringDeal.Trim(strResID, ",", ",")
        Else
            PromptMsg("请输入有效的资源名称")
        End If
    End Sub

    Private Sub lbtnGetResName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetResName.Click
        If IsNumeric(txtRes1.Text.Trim()) Then
            txtRes2.Text = ResFactory.ResService.GetResName(CmsPass, CLng(txtRes1.Text.Trim()))
        Else
            PromptMsg("请输入有效的资源ID")
        End If
    End Sub

    Private Sub lbtnGetColumnInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetColumnInfo.Click
        Dim strColName As String = txtColName.Text.Trim()
        If strColName = "" Then
            PromptMsg("请输入有效的字段内部名称！")
            Return
        End If

        ddlColRes.Items.Clear()
        Dim hashCol As New Hashtable

        Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), CmsTables.ColDefine, "CD_COLNAME='" & strColName & "'")
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        For Each drv In dv
            Dim lngResID As Long = DbField.GetLng(drv, "CD_RESID")
            Dim strResName As String = CmsPass.GetDataRes(lngResID).ResName
            Dim strColDispName As String = DbField.GetStr(drv, "CD_DISPNAME")
            hashCol.Add(CStr(lngResID), strColDispName)
            ddlColRes.Items.Add(New ListItem(strResName, CStr(lngResID)))
        Next
        ViewState("PAGE_COLINFO_COLDISPNAME") = hashCol

        txtColDispName.Text = HashField.GetStr(CType(ViewState("PAGE_COLINFO_COLDISPNAME"), Hashtable), ddlColRes.SelectedValue)
    End Sub

    Private Sub ddlColRes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlColRes.SelectedIndexChanged
        txtColDispName.Text = HashField.GetStr(CType(ViewState("PAGE_COLINFO_COLDISPNAME"), Hashtable), ddlColRes.SelectedValue)
    End Sub

    Private Sub lbtnGetChongmingCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetChongmingCol.Click
        Dim lngTotalNum As Long = CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), CmsTables.ColDefine, "")
        txtCMNum1.Text = CStr(lngTotalNum)
        Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "SELECT DISTINCT CD_COLNAME FROM " & CmsTables.ColDefine)
        Dim lngNum As Long = ds.Tables(0).DefaultView.Count
        txtCMNum2.Text = CStr(lngNum)
        txtCMNum3.Text = CStr(lngTotalNum - lngNum)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub chkShowColName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowColName.CheckedChanged
        CmsConfig.ShowColumnName = chkShowColName.Checked
    End Sub

    Private Sub chkShowIDForCms_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIDForCms.CheckedChanged
        CmsConfig.ShowIDForCms = chkShowIDForCms.Checked
    End Sub

    Private Sub chkDbSqlLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDbSqlLog.CheckedChanged
        CmsConfig.EnableDbSqlLog = chkDbSqlLog.Checked
    End Sub

    Private Sub chkDebugMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDebugMode.CheckedChanged
        CmsConfig.DebugingMode = chkDebugMode.Checked
    End Sub

    Private Sub chkIsDemoVer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsDemoVer.CheckedChanged
        If chkIsDemoVer.Checked = True Then
            DbParameter.SetString1(SDbConnectionPool.GetDbConfig(), DbParameter.PRODUCT_FORDEMO, "1")
        Else
            DbParameter.SetString1(SDbConnectionPool.GetDbConfig(), DbParameter.PRODUCT_FORDEMO, "0")
        End If
    End Sub
End Class

End Namespace
