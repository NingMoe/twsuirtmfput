Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FormOpen
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

        If VLng("PAGE_FORMTYPE") = 0 Then
            ViewState("PAGE_FORMTYPE") = RLng("mnuformtype")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '在Listbox中显示所有已设计的窗体名称
        LoadDesignedForms()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '--------------------------------------------------------------------
    '在Listbox中显示所有已设计的窗体名称
    '--------------------------------------------------------------------
    Private Sub LoadDesignedForms()
        ListBox1.Items.Clear() '清空列表

        Dim alistDForms As ArrayList = CTableForm.GetDesignedFormNames(CmsPass, VLng("PAGE_RESID"), CType(VInt("PAGE_FORMTYPE"), FormType))
        Dim strDFormName As String
        For Each strDFormName In alistDForms
            Dim li As New ListItem
            li.Text = strDFormName '字段显示名称
            li.Value = strDFormName '字段内部名称
            ListBox1.Items.Add(li)
            li = Nothing
        Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strDFormName As String = ListBox1.SelectedValue()
            If strDFormName <> "" Then
                CTableForm.DelDesignedForms(CmsPass, VLng("PAGE_RESID"), strDFormName, VLng("PAGE_FORMTYPE"))
            Else
                PromptMsg("请选择需要删除的窗体设计！")
            End If
        Catch ex As Exception
            PromptMsg("删除窗体设计失败，网络连接出错！")
        End Try
    End Sub
End Class

End Namespace
