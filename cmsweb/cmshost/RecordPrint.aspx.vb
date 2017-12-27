Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RecordPrint
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

    Private Panel1 As Panel = Nothing

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VStr("PAGE_INPUTMODE") = "" Then
            If RInt("MNURESLOCATE") = ResourceLocation.HostTable Then
                ViewState("PAGE_INPUTMODE") = InputMode.PrintInHostTable
            ElseIf RInt("MNURESLOCATE") = ResourceLocation.RelTable Then
                ViewState("PAGE_INPUTMODE") = InputMode.PrintInRelTable
            Else
                ViewState("PAGE_INPUTMODE") = InputMode.Unknown
            End If
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '初始化Panel
        Panel1 = New Panel
        Panel1.ID = "Panel1"
        Panel1.EnableViewState = False
        Dim strStyle As String = "Z-INDEX: 101; LEFT: 4px; POSITION: absolute; TOP: 28px"
        Panel1.BorderColor = Color.FromName("#D7E7FF")
        Panel1.Attributes.Add("style", strStyle)
        Panel1.BorderWidth = Unit.Pixel(1)
        Me.Form1.Controls.Add(Panel1)

        lnkExit.Attributes.Add("onClick", "return window.close();")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Try
            '显示输入框标题
            Dim strResName As String = ResFactory.ResService.GetResName(CmsPass, RLng("mnuresid"))
            lblHeader.Text = strResName & "－打印窗体"

            '装载输入框
            FormManager.LoadForm(CmsPass, Panel1, Nothing, RLng("mnuresid"), CType(VStr("PAGE_INPUTMODE"), InputMode), RStr("mnuformname"), , RLng("mnurecid"), , , True, , , True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)

            If CTableForm.HasDesignedForms(CmsPass, RLng("mnuresid"), RStr("mnuformname"), FormType.PrintForm, CType(VStr("PAGE_INPUTMODE"), InputMode), False) = False Then
                    PromptMsg(CmsMessage.GetMsg(CmsPass, "RECORD_PRINT"))
            End If

            '设置3个Panel（标题、内容、脚注）的属性信息
            'RecordEditBase.InitialPanel(Panel1)

            '判断是否显示Form的边框
            If CmsFunc.IsEnable("PRINTRECORD_FORMBORDER_ON") = False Then Panel1.BorderWidth = Unit.Pixel(0)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lnkExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkExit.Click
        Response.Redirect(RStr("mnubackpage"), False)
    End Sub
End Class

End Namespace
