Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocHistoryList
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

        If VLng("PAGE_RECID") = 0 Then
            ViewState("PAGE_RECID") = RLng("mnurecid")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("docaction") = "getdoc" Then '提取文档
            Try
                Dim strDocPath As String = RStr("file")
                    Dim datDoc As DataDocument = DocVersion.GetOneDocInfo(strDocPath)

                  FileTransfer.DownloadDoc(Response, datDoc)
            Catch ex As Exception
                '线程正被中止，不做任何操作
                'CmsError.ThrowEx("下载文档失败", ex, True)
            End Try
        ElseIf RStr("docaction") = "showdoc" Then '浏览文档
            Try
                Dim strDocPath As String = RStr("file")
                    Dim datDoc As DataDocument = DocVersion.GetOneDocInfo(strDocPath)
                    FileTransfer.ShowDoc(Response, datDoc)

                Catch ex As Exception
                    '线程正被中止，不做任何操作
                    'CmsError.ThrowEx("显示文档失败", ex, True)
                End Try
        End If

        '创建DataGrid的列字段和填充内容
        ShowGridData(VLng("PAGE_DOCID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

        If VLng("PAGE_RECID") <> 0 And VLng("PAGE_RESID") <> 0 Then
            '根据记录ID获取DOCID
            ViewState("PAGE_DOCID") = ResFactory.TableService(CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTableType).GetDocID(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_RECID"))
        End If

        WebUtilities.InitialDataGrid(DataGrid1) '设置DataGrid显示属性
        CreateColumns(DataGrid1)
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Dim strFilePath As String
        Try
            strFilePath = e.Item.Cells(1).Text '文档全路径是第二列
            DocVersion.DeleteOneDoc(strFilePath)
        Catch ex As Exception
                PromptMsg(CmsMessage.GetMsg(CmsPass, "DELETE_DOCUMENT") & strFilePath, ex, True)
        End Try

        DataGrid1.EditItemIndex = CInt(e.Item.ItemIndex)
        DataGrid1.EditItemIndex = -1
        ShowGridData(VLng("PAGE_DOCID"))
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------------------
    '创建自定义表的列
    '------------------------------------------------------------------
    Private Sub CreateColumns(ByRef DataGrid1 As System.Web.UI.WebControls.DataGrid)
            Dim intWidth As Integer = 0

            Dim strLanguage As String = CmsMessage.GetBriefLanguage(CmsPass.Employee.Language)

            If strLanguage.Trim = "en" Then
                lblTitle.Text = "History Document"
                btnExit.Text = "Exit"
            End If

        '第一列是唯一ID，但不显示，用于修改、删除时作为唯一ID用
        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "DOC_LINEID"
        col.DataField = "DOC_LINEID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
            col.HeaderText = "文档目录" 
        col.DataField = "DOC_PATH"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
            col.HeaderText = "文档名"
            If strLanguage.Trim = "en" Then
                col.HeaderText = "Name"
            End If
        col.DataField = "DOC_NAME"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 250

        col = New BoundColumn
            col.HeaderText = "日期"
            If strLanguage.Trim = "en" Then
                col.HeaderText = "Date"
            End If
        col.DataField = "DOC_DATE"
        col.ItemStyle.Width = Unit.Pixel(150)
        col.ItemStyle.HorizontalAlign = HorizontalAlign.Right
        col.HeaderStyle.HorizontalAlign = HorizontalAlign.Right
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 150

        col = New BoundColumn
            col.HeaderText = "大小"
            If strLanguage.Trim = "en" Then
                col.HeaderText = "Size"
            End If
        col.DataField = "DOC_SIZE"
        col.ItemStyle.Width = Unit.Pixel(80)
        col.ItemStyle.HorizontalAlign = HorizontalAlign.Right
        col.HeaderStyle.HorizontalAlign = HorizontalAlign.Right
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 80

        Dim colHyperlink As New HyperLinkColumn
            colHyperlink.HeaderText = "提取文档"
            If strLanguage.Trim = "en" Then
                colHyperlink.HeaderText = ""
            End If
        'colHyperlink.NavigateUrl = "/cmsweb/cmsdocument/DocHistoryList.aspx?docaction=getdoc" '"javascript:GetDoc(this);"
        'colHyperlink.Target = "new"
        colHyperlink.DataTextField = "DOC_PATH"
            colHyperlink.DataTextFormatString = "<a href=""javascript:GetDoc('{0}');"">提取</a>"
            If strLanguage.Trim = "en" Then
                colHyperlink.DataTextFormatString = "<a href=""javascript:GetDoc('{0}');"">Download</a>"
            End If
        colHyperlink.ItemStyle.Width = Unit.Pixel(100)
        colHyperlink.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        colHyperlink.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        DataGrid1.Columns.Add(colHyperlink)
        colHyperlink = Nothing
        intWidth += 100

        If CmsFunc.IsEnable("FUNC_ONLINEVIEW") Then
            colHyperlink = New HyperLinkColumn
                colHyperlink.HeaderText = "在线浏览文档"
                If strLanguage.Trim = "en" Then
                    colHyperlink.HeaderText = ""
                End If
            colHyperlink.DataTextField = "DOC_PATH"
                colHyperlink.DataTextFormatString = "<a href=""javascript:ShowDoc('{0}');"">浏览</a>"
                If strLanguage.Trim = "en" Then
                    colHyperlink.DataTextFormatString = "<a href=""javascript:ShowDoc('{0}');"">view</a>"
                End If
            colHyperlink.ItemStyle.Width = Unit.Pixel(100)
            colHyperlink.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            colHyperlink.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            DataGrid1.Columns.Add(colHyperlink)
            colHyperlink = Nothing
            intWidth += 100
        End If

        '只有当前资源所在的部门的部门管理员有删除文档历史版本的权限
        Dim lngCurrentDepID As Long = ResFactory.ResService.GetResDepartment(CmsPass, VLng("PAGE_RESID"))
        If CmsPass.EmpIsDepAdmin And CmsPass.Employee.DepartmentId = lngCurrentDepID Then
            Dim colDel As ButtonColumn = New ButtonColumn
            colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
            colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '或者用图标，如："<img src='/cmsweb/images/common/delete.gif' border=0>"
            colDel.CommandName = "Delete"
            colDel.ButtonType = ButtonColumnType.LinkButton
            colDel.ItemStyle.Width = Unit.Pixel(50)
            colDel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            colDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            DataGrid1.Columns.Add(colDel)
            colDel = Nothing
            intWidth += 50
        End If

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '----------------------------------------------------------
    '创建DataGrid的列字段和填充内容
    '----------------------------------------------------------
    Public Sub ShowGridData(ByVal lngDocID As Long)
        Dim ds As DataSet = DocVersion.GetDocHistoryList(lngDocID)
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub
End Class

End Namespace
