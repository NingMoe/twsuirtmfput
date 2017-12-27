Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceCreateTable
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
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_SELFDEFINE_TABLENAME")
        txtTableName.Visible = bln
        lblTableName.Visible = bln
        lblComments.Visible = bln
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        LoadResTypeOptions() '初始化资源类型的Listbox
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnCreateTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateTable.Click
        Try
            Dim iRes As IResource = ResFactory.ResService
            Dim strHostTableType As String = iRes.ConvTableTypeDispName2InnerName(CmsPass, ddlResType.SelectedValue)

            Dim strTableName As String = txtTableName.Text.Trim()
                iRes.CreateResTable(CmsPass, SLng("RESADDTAB_RESID"), 0, strHostTableType, strTableName)

            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        Catch ex As Exception
            PromptMsg("创建资源主表失败，无法连接数据库！")
        End Try
    End Sub

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------
    '初始化资源类型的Listbox
    '------------------------------------------------------
    Private Sub LoadResTypeOptions()
        If ddlResType.Items.Count <= 0 Then
            Dim iRes As IResource = ResFactory.ResService
            Dim alistResourceType As ArrayList = iRes.GetTableTypeDispNameList(CmsPass)
            Dim en As IEnumerator = alistResourceType.GetEnumerator
            Do While en.MoveNext
                ddlResType.Items.Add(CStr(en.Current))
            Loop
        End If
    End Sub
End Class

End Namespace
