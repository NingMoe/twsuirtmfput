Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceAddMain
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

        If VLng("PAGE_DEPID") = 0 Then
            If RLng("depid") <> 0 Then
                ViewState("PAGE_DEPID") = RLng("depid")
            Else
                ViewState("PAGE_DEPID") = ResFactory.ResService.GetResDepartment(CmsPass, VLng("PAGE_RESID"))
            End If
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_RES_TEMPLATE")
        lblTemplate.Visible = bln
        ddlTemplate.Visible = bln

        bln = CmsFunc.IsEnable("FUNC_SELFDEFINE_TABLENAME")
        txtTableName.Visible = bln
        lblTableName.Visible = bln
        lblComments.Visible = bln
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If CmsFunc.IsEnable("FUNC_RES_TEMPLATE") Then
            LoadResTemplates(CmsPass, ddlTemplate)
        End If

        If CmsConfig.GetBool("CLIENT_CONFIG", "RESCREATE_CHECK_INHERIT") = True Then
            chkInherit.Checked = True
        Else
            chkInherit.Checked = False
        End If
        chkInherit.ToolTip = "与父资源共用相同的表单结构"
        If VLng("PAGE_RESID") = 0 Then '正在创建根资源
            chkInherit.Visible = False '创建根资源时不需要显示“继承选项”
        End If

        LoadResTypeOptions() '初始化资源类型的Listbox

        '设置键盘光标默认选中的输入框
        If Not IsStartupScriptRegistered("MouseFocus") Then
            Dim strScript As String = "<script language=""javascript"">self.document.forms(0).txtResName.focus();</script>"
            RegisterStartupScript("MouseFocus", strScript)
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '----------------------------------------------------------
    '增加/修改资源的实际响应入口
    '----------------------------------------------------------
    Private Sub btnSaveResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveResource.Click
        Try
            If txtResName.Text.Trim().Length = 0 Then '检验资源名称
                PromptMsg("请输入合理的资源名称！")
                Return
            End If

            If ddlResType.SelectedValue = "工作流表单" Then
                CmsDocFlow.CreateFlowResource(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_DEPID"), txtResName.Text.Trim())
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                Return
            End If

            '---------------------------------------------------------------------------------
            '创建资源
            Dim lngResID As Long = 0
            If ddlTemplate.Visible = True And ddlTemplate.SelectedValue <> "" Then
                '创建来自于资源模板的新资源
                lngResID = ResourceTemplate.CreateResourceByTemplate(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_DEPID"), ddlTemplate.SelectedValue, txtResName.Text.Trim())
            Else
                '创建普通的新资源
                Dim lngResType As ResInheritType
                If chkInherit.Visible = True AndAlso chkInherit.Checked = True Then
                        lngResType = ResInheritType.IsInherit
                    ElseIf ddlResType.SelectedItem.Text.Trim = "视图管理" Then 
                        lngResType = ResInheritType.IsInherit
                    Else
                        lngResType = ResInheritType.IsIndependent
                End If
                lngResID = ResFactory.ResService.AddResource(CmsPass, txtResName.Text.Trim(), lngResType, VLng("PAGE_RESID"), VLng("PAGE_DEPID"))
            End If
            '---------------------------------------------------------------------------------

            '---------------------------------------------------------------------------------
            '创建表单
            If ddlResType.SelectedValue <> "" Then
                Dim iRes As IResource = ResFactory.ResService
                Dim strHostTableType As String = iRes.ConvTableTypeDispName2InnerName(CmsPass, ddlResType.SelectedValue)

                Dim strTableName As String = ""
                If CmsFunc.IsEnable("FUNC_SELFDEFINE_TABLENAME") = True Then
                    strTableName = txtTableName.Text.Trim()
                Else
                    strTableName = ""
                End If
                    iRes.CreateResTable(CmsPass, lngResID, VLng("PAGE_RESID"), strHostTableType, strTableName)
            End If
            '---------------------------------------------------------------------------------

            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------
    '初始化DropDownList中字段列表项
    '-----------------------------------------------------------
    Private Shared Sub LoadResTemplates(ByRef pst As CmsPassport, ByVal ddlFields As DropDownList)
        ddlFields.Items.Clear()
        ddlFields.Items.Add(New ListItem("", ""))
        Dim alistTemplateNames As ArrayList = ResourceTemplate.GetTemplateNames()
        Dim strName As String
        For Each strName In alistTemplateNames
            ddlFields.Items.Add(New ListItem(strName, strName))
        Next
    End Sub

    '------------------------------------------------------
    '初始化资源类型的Listbox
    '------------------------------------------------------
    Private Sub LoadResTypeOptions()
        If ddlResType.Items.Count <= 0 Then
            ddlResType.Items.Add(New ListItem("", ""))

            Dim iRes As IResource = ResFactory.ResService
                Dim alistResourceType As ArrayList = iRes.GetTableTypeDispNameList(CmsPass)
                If VLng("PAGE_RESID") = 0 Then
                    alistResourceType.Remove("视图管理")
                End If
                Dim en As IEnumerator = alistResourceType.GetEnumerator
               
            Do While en.MoveNext
                ddlResType.Items.Add(CStr(en.Current))
                Loop
               
                ddlResType.Items.Add(New ListItem("工作流表单", "工作流表单"))
            End If
    End Sub
End Class

End Namespace
