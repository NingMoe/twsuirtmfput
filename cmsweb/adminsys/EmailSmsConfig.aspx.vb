Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class EmailSmsConfig
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
        ddlRelHostRes.AutoPostBack = True
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        LoadHostAndSelfResources(VLng("PAGE_RESID"))
        LoadOriginalData(VLng("PAGE_RESID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If IsNumeric(ddlRelHostRes.SelectedValue) = False Then
                PromptMsg("请选择资源或其关联主表！")
                Return
            End If

            If ddlEmail.SelectedValue = "" And ddlHandphone.SelectedValue = "" Then
                PromptMsg("请至少选择电子邮件和手机其中之一！")
                Return
            End If

            Dim lngResID1 As Long = 0
            If IsNumeric(ddlRelHostRes.SelectedValue) Then lngResID1 = CLng(ddlRelHostRes.SelectedValue)

            ResFactory.ResService.SetResourceCommProp(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).ResID, lngResID1, ddlEmail.SelectedValue, ddlHandphone.SelectedValue, ddlRefColumn.SelectedValue)

            PromptMsg("保存邮件手机配置信息成功！")
        Catch ex As Exception
            PromptMsg("保存资源通讯配置信息失败！", ex, True)
        End Try
    End Sub

    Private Sub btnClearSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSetting.Click
        Try
            ResFactory.ResService.SetResourceCommProp(CmsPass, CmsPass.GetDataRes(VLng("PAGE_RESID")).ResID, 0, "", "", "")

            ddlRelHostRes.SelectedValue = ""
            LoadResColumns1()
        Catch ex As Exception
            PromptMsg("清空设置异常失败！", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub ddlRelHostRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRelHostRes.SelectedIndexChanged
        LoadResColumns1()
    End Sub

    '----------------------------------------------------------
    '显示资源本身和所有关联主表
    '----------------------------------------------------------
    Private Sub LoadHostAndSelfResources(ByVal lngResID As Long)
        If ddlRelHostRes.Items.Count <= 0 Then
            '添加空条目
            ddlRelHostRes.Items.Add(New ListItem("", ""))

            '显示资源本身
            ddlRelHostRes.Items.Add(New ListItem(CmsPass.GetDataRes(lngResID).ResName & "(本资源)", CStr(CmsPass.GetDataRes(lngResID).ResID)))

            '显示所有关联主表
            Dim hashRelHostRes As Hashtable = CmsTableRelation.GetHostRelatedResources(CmsPass, CmsPass.GetDataRes(lngResID).ResID)
            Dim en As IDictionaryEnumerator = hashRelHostRes.GetEnumerator()
            Do While en.MoveNext
                Dim datRes As DataResource = CType(en.Value, DataResource)
                ddlRelHostRes.Items.Add(New ListItem(datRes.ResName, CStr(datRes.ResID)))
            Loop
        End If
    End Sub

    '----------------------------------------------------------
    '显示选中关联主表的所有字段
    '----------------------------------------------------------
    Private Sub LoadResColumns1()
        If IsNumeric(ddlRelHostRes.SelectedValue) Then
            Dim lngResID As Long = CLng(ddlRelHostRes.SelectedValue)
            If lngResID <> 0 Then
                WebUtilities.LoadResColumnsInDdlist(CmsPass, lngResID, ddlEmail, True, True, True)
                WebUtilities.LoadResColumnsInDdlist(CmsPass, lngResID, ddlHandphone, True, True, True)
                WebUtilities.LoadResColumnsInDdlist(CmsPass, lngResID, ddlRefColumn, True, True, True)
            End If
        Else
            ddlEmail.Items.Clear()
            ddlHandphone.Items.Clear()
            ddlRefColumn.Items.Clear()
        End If
    End Sub

    '----------------------------------------------------------
    '显示选中关联主表的所有字段
    '----------------------------------------------------------
    Private Sub LoadOriginalData(ByVal lngResID As Long)
        If CmsPass.GetDataRes(lngResID).CommResID <> 0 Then
            '提取资源ID
            ddlRelHostRes.SelectedValue = CStr(CmsPass.GetDataRes(lngResID).CommResID)
            LoadResColumns1()

            If CmsPass.GetDataRes(lngResID).CommColEmail <> "" Then ddlEmail.SelectedValue = CmsPass.GetDataRes(lngResID).CommColEmail
            If CmsPass.GetDataRes(lngResID).CommColHandphone <> "" Then ddlHandphone.SelectedValue = CmsPass.GetDataRes(lngResID).CommColHandphone
            If CmsPass.GetDataRes(lngResID).CommColRef <> "" Then ddlRefColumn.SelectedValue = CmsPass.GetDataRes(lngResID).CommColRef
        End If
    End Sub
End Class

End Namespace
