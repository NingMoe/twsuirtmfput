Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceAdd
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
            ViewState("PAGE_DEPID") = RLng("depid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_RES_TEMPLATE")
        lblTemplate.Visible = bln
        ddlTemplate.Visible = bln
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If CmsFunc.IsEnable("FUNC_RES_TEMPLATE") Then
            LoadResTemplates(CmsPass, ddlTemplate)
        End If

        chkInherit.Checked = False
        If VLng("PAGE_RESID") = 0 Then '正在创建根资源
            chkInherit.Visible = False '创建根资源时不需要显示“继承选项”
        Else
            chkInherit.ToolTip = "与父资源共用表单（主表/子表）"
        End If

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

            If ddlTemplate.Visible = True And ddlTemplate.SelectedValue <> "" Then
                '创建来自于资源模板的新资源
                ResourceTemplate.CreateResourceByTemplate(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_DEPID"), ddlTemplate.SelectedValue, txtResName.Text.Trim())
            Else
                '创建普通的新资源
                Dim lngResType As ResInheritType
                If chkInherit.Visible = True AndAlso chkInherit.Checked = True Then
                    lngResType = ResInheritType.IsInherit
                Else
                    lngResType = ResInheritType.IsIndependent
                End If
                ResFactory.ResService.AddResource(CmsPass, txtResName.Text.Trim(), lngResType, VLng("PAGE_RESID"), VLng("PAGE_DEPID"))
            End If

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
End Class

End Namespace
