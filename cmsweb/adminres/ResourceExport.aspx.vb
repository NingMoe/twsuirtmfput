Option Strict On
Option Explicit On 

Imports System.IO
Imports Unionsoft.Platform
Imports NetReusables
Imports System.Text.RegularExpressions


Namespace Unionsoft.Cms.Web


Partial Class ResourceExport
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

        If VStr("PAGE_SEARCHRESULT") = "" Then
            ViewState("PAGE_SEARCHRESULT") = RStr("getsearchresult")
        End If
        If VLng("PAGE_MNURESLOCATE") = 0 Then
            ViewState("PAGE_MNURESLOCATE") = RLng("MNURESLOCATE")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '防止“确认”提交按钮被点击多次
        btnConfirm.Attributes.Add("onClick", "return ConfirmButtonClicked();")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '初始化DropDownList中导出文件类型
        LoadFieldAdvSettingOptions()

        '判断有无权限
        Dim lngRightsValue As Long = CmsRights.GetRightsData(CmsPass, VLng("PAGE_RESID"), CmsPass.Employee.ID).lngQX_VALUE
        If (lngRightsValue And CmsRightsDefine.MgrResExport) = CmsRightsDefine.MgrResExport Then '有权限
        Else '无权限
            PromptMsg("您没有对当前资源(" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName & ")的导出权限！")
            btnConfirm.Visible = False
            'If CmsPass.EmpIsSysAdmin Or CmsPass.EmpIsAspAdmin Or CmsPass.EmpIsAspClientAdmin Or CmsPass.EmpIsDepAdmin Then
            '    '管理员都可以导出资源
            'Else
            '    PromptMsg("您没有对当前资源(" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName & ")的导出权限！")
            '    btnConfirm.Visible = False
            'End If
            Return
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

#Region "导出数据"
    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Session("CMSBP_ResourceExportResult") = VStr("PAGE_BACKPAGE")

        Dim strPageSuccess As String = "/cmsweb/adminres/ResourceExportResult.aspx?result=1"
        Dim strPageFailure As String = "/cmsweb/adminres/ResourceExportResult.aspx?result=0"
        '-------------------------------------------------------------------
        '获取资源本身以及所有拥有“完全控制”权限的子资源的三种格式：1）字符串形式的列表（23,456,11)；2）ArrayList；3）Hashstable
        '其中部分资源（非最后节点子资源）可能没有“完全控制”权限，但为了显示拥有“完全控制”的子资源而必须显示
        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
        Dim iRes As IResource = ResFactory.ResService()
        Dim hashChildRes2 As New Hashtable
        Dim strResIDList2 As String = ""
        Dim blnExpChildRes As Boolean = chkExpChild.Checked
        If blnExpChildRes Then
            Dim hashResOfHasRights As Hashtable = iRes.GetResourceByHashtableWithUserRights(CmsPass, CmsRightsDefine.RecView, True, True, , VLng("PAGE_RESID"))
            Dim strAllParentResIDs As String = iRes.GetParentResIDsByString(CmsPass, VLng("PAGE_RESID"))
            strAllParentResIDs = "," & strAllParentResIDs & ","

            Dim en As IDictionaryEnumerator = hashResOfHasRights.GetEnumerator()
            While en.MoveNext
                Dim lngResIDTemp As Long = CLng(en.Key)
                If strAllParentResIDs.IndexOf("," & lngResIDTemp & ",") < 0 Then '去除选中资源的父资源
                    Dim strResTable As String = iRes.GetResTable(CmsPass, lngResIDTemp)
                    hashChildRes2.Add(CStr(lngResIDTemp), strResTable)
                    strResIDList2 &= CStr(lngResIDTemp) & ","
                End If
            End While
        End If
        If hashChildRes2.Count <= 0 OrElse hashChildRes2.ContainsKey(VStr("PAGE_RESID")) = False Then '添加资源本身
            hashChildRes2.Add(CStr(VLng("PAGE_RESID")), datRes.ResTable)
            strResIDList2 &= CStr(VLng("PAGE_RESID")) & ","
        End If
        strResIDList2 = StringDeal.Trim(strResIDList2, ",", ",")

        Dim mark As Boolean = False
        Dim lngResID As String
        Dim strRst As String = ""
        Dim reg As Regex = New Regex("[\W]")
        For Each lngResID In strResIDList2.Split(CChar(","))
            Dim ds As DataSet = CTableStructure.GetColumnsByDataset(CmsPass, CLng(lngResID), True, True)
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            Dim strResName As String = CmsPass().GetDataRes(CLng(lngResID)).ResName
            Dim rstCol As String = ""
            For Each drv In dv
                Dim strColDispName As String = DbField.GetStr(drv, "CD_DISPNAME")
                If reg.IsMatch(strColDispName) Then
                    rstCol += "," & strColDispName
                    mark = True
                End If
            Next
            If mark Then
                strRst += strResName & "表中字段:" & rstCol.Substring(1, rstCol.Length - 1) & ";"
            End If
        Next
        If mark Then
            Session("CMS_IMPRES_MESSAGE") = strRst & "包含特殊字符，数据不能进行导出，请重新字段设置"
            Response.Redirect(strPageFailure, True)
        End If

        '-------------------------------------------------------------------
        Try
            '-------------------------------------------------------------------
            'SQL数据库连接所需的数据结构
                Dim dbcSrc As DbConfig = SDbConnectionPool.GetDbConfig()
            '-------------------------------------------------------------------

            '-------------------------------------------------------------------
            '导出用MDB文件的数据库连接所需的数据结构
            Dim dbcDest As New DbConfig
            Dim strUnique As String = CStr(TimeId.CurrentMilliseconds())
            'Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
            Dim strTempFolder As String = CmsConfig.TempFolder & "expimp\"
            If Directory.Exists(strTempFolder) = False Then Directory.CreateDirectory(strTempFolder)
            If ddlExportFileType.SelectedValue = "mdb" Then
                dbcDest.DatabaseType = DbConfig.DbType.MsAccess
                dbcDest.DbFilePath = strTempFolder & datRes.ResName & "__" & strUnique & "." & ddlExportFileType.SelectedValue
                dbcDest.User = "Admin"
                dbcDest.Pass = ""
                File.Copy(CmsConfig.ProjectRootFolder & "data\sql\empty.mdb", dbcDest.DbFilePath)
            ElseIf ddlExportFileType.SelectedValue = "xls" Then
                dbcDest.DatabaseType = DbConfig.DbType.MsExcel
                dbcDest.DbFilePath = strTempFolder & datRes.ResName & "__" & strUnique & "." & ddlExportFileType.SelectedValue
                dbcDest.User = ""
                dbcDest.Pass = ""
            ElseIf ddlExportFileType.SelectedValue = "txt" Then
                dbcDest.DatabaseType = DbConfig.DbType.Text
                'dbcDest.DbFilePath = strTempFolder & datRes.ResName & "__" & strUnique & "." & ddlExportFileType.SelectedValue
                dbcDest.TextExtProperties = "'text;HDR=No;FMT=TabDelimited'"
                dbcDest.DbFilePath = "c:\test300.txt"
                dbcDest.User = ""
                dbcDest.Pass = ""
                File.Copy(CmsConfig.ProjectRootFolder & "data\sql\empty.txt", dbcDest.DbFilePath)
            End If
            Session("CMSTRANS_EXPORTFILE") = dbcDest.DbFilePath
            '-------------------------------------------------------------------

            Dim blnRtn As Boolean = False
            Dim strResult As String = ""

            '-------------------------------------------------------------------
            '导出资源表单数据
            Dim strWhere As String = ""
            If VStr("PAGE_SEARCHRESULT") = "1" Then '导出查询结果
                Dim lngResLocate As ResourceLocation = CType(VLng("PAGE_MNURESLOCATE"), ResourceLocation)
                If lngResLocate = ResourceLocation.HostTable Then
                    strWhere = SStr("CMS_HOSTTABLE_WHERE")
                ElseIf lngResLocate = ResourceLocation.RelTable Then
                    strWhere = SStr("CMS_SUBTABLE_WHERE")
                    If strWhere = "" Then
                        '如果关联子表的查询条件为空，则必须提取关联条件值
                        strWhere = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, RLng("mnuhostresid"), VLng("PAGE_RESID"), RLng("mnuhostrecid"))
                    Else
                        '如果关联子表的查询条件不为空，则关联条件值必然已经包含在内
                    End If
                End If
            Else '导出整个资源的数据
                strWhere = ""
            End If
            Dim strRedirectUrl As String = ""
            blnRtn = TransResTable.ExportResTableData(CmsPass, hashChildRes2, dbcDest, chkContinueOnError.Checked, strWhere)
            If blnRtn Then
                strResult &= "导出资源表单数据成功！" & "<BR><BR>"
                strRedirectUrl = strPageSuccess
            Else
                strResult &= "导出资源表单数据失败！" & "<BR><BR>"
                strRedirectUrl = strPageFailure
            End If
            '-------------------------------------------------------------------

            Session("CMS_IMPRES_MESSAGE") = strResult
            Response.Redirect(strRedirectUrl, False)
        Catch ex As Exception
            PromptMsg("导出资源异常失败，没有文件访问权限！", ex, True)
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
#End Region
    '-----------------------------------------------------------
    '初始化DropDownList中导出文件类型
    '-----------------------------------------------------------
    Private Sub LoadFieldAdvSettingOptions()
        ddlExportFileType.Items.Clear()
        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") And CmsFunc.IsEnable("FUNC_RES_EXP_EXCEL") Then
            ddlExportFileType.Items.Add(New ListItem("MS EXCEL", "xls"))
        End If
        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") And CmsFunc.IsEnable("FUNC_RES_EXP_ACCESS") Then
            ddlExportFileType.Items.Add(New ListItem("MS ACCESS", "mdb"))
        End If
        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") And CmsFunc.IsEnable("FUNC_RES_EXP_TEXT") Then
            ddlExportFileType.Items.Add(New ListItem("Text", "txt"))
        End If
    End Sub
End Class

End Namespace
