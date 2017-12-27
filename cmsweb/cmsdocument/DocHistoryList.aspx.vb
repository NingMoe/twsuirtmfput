Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocHistoryList
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
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
        If RStr("docaction") = "getdoc" Then '��ȡ�ĵ�
            Try
                Dim strDocPath As String = RStr("file")
                    Dim datDoc As DataDocument = DocVersion.GetOneDocInfo(strDocPath)

                  FileTransfer.DownloadDoc(Response, datDoc)
            Catch ex As Exception
                '�߳�������ֹ�������κβ���
                'CmsError.ThrowEx("�����ĵ�ʧ��", ex, True)
            End Try
        ElseIf RStr("docaction") = "showdoc" Then '����ĵ�
            Try
                Dim strDocPath As String = RStr("file")
                    Dim datDoc As DataDocument = DocVersion.GetOneDocInfo(strDocPath)
                    FileTransfer.ShowDoc(Response, datDoc)

                Catch ex As Exception
                    '�߳�������ֹ�������κβ���
                    'CmsError.ThrowEx("��ʾ�ĵ�ʧ��", ex, True)
                End Try
        End If

        '����DataGrid�����ֶκ��������
        ShowGridData(VLng("PAGE_DOCID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If
        CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

        If VLng("PAGE_RECID") <> 0 And VLng("PAGE_RESID") <> 0 Then
            '���ݼ�¼ID��ȡDOCID
            ViewState("PAGE_DOCID") = ResFactory.TableService(CmsPass.GetDataRes(VLng("PAGE_RESID")).ResTableType).GetDocID(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_RECID"))
        End If

        WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
        CreateColumns(DataGrid1)
    End Sub

    Private Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Dim strFilePath As String
        Try
            strFilePath = e.Item.Cells(1).Text '�ĵ�ȫ·���ǵڶ���
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
    '�����Զ�������
    '------------------------------------------------------------------
    Private Sub CreateColumns(ByRef DataGrid1 As System.Web.UI.WebControls.DataGrid)
            Dim intWidth As Integer = 0

            Dim strLanguage As String = CmsMessage.GetBriefLanguage(CmsPass.Employee.Language)

            If strLanguage.Trim = "en" Then
                lblTitle.Text = "History Document"
                btnExit.Text = "Exit"
            End If

        '��һ����ΨһID��������ʾ�������޸ġ�ɾ��ʱ��ΪΨһID��
        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "DOC_LINEID"
        col.DataField = "DOC_LINEID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
            col.HeaderText = "�ĵ�Ŀ¼" 
        col.DataField = "DOC_PATH"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)
        col = Nothing

        col = New BoundColumn
            col.HeaderText = "�ĵ���"
            If strLanguage.Trim = "en" Then
                col.HeaderText = "Name"
            End If
        col.DataField = "DOC_NAME"
        col.ItemStyle.Width = Unit.Pixel(250)
        DataGrid1.Columns.Add(col)
        col = Nothing
        intWidth += 250

        col = New BoundColumn
            col.HeaderText = "����"
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
            col.HeaderText = "��С"
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
            colHyperlink.HeaderText = "��ȡ�ĵ�"
            If strLanguage.Trim = "en" Then
                colHyperlink.HeaderText = ""
            End If
        'colHyperlink.NavigateUrl = "/cmsweb/cmsdocument/DocHistoryList.aspx?docaction=getdoc" '"javascript:GetDoc(this);"
        'colHyperlink.Target = "new"
        colHyperlink.DataTextField = "DOC_PATH"
            colHyperlink.DataTextFormatString = "<a href=""javascript:GetDoc('{0}');"">��ȡ</a>"
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
                colHyperlink.HeaderText = "��������ĵ�"
                If strLanguage.Trim = "en" Then
                    colHyperlink.HeaderText = ""
                End If
            colHyperlink.DataTextField = "DOC_PATH"
                colHyperlink.DataTextFormatString = "<a href=""javascript:ShowDoc('{0}');"">���</a>"
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

        'ֻ�е�ǰ��Դ���ڵĲ��ŵĲ��Ź���Ա��ɾ���ĵ���ʷ�汾��Ȩ��
        Dim lngCurrentDepID As Long = ResFactory.ResService.GetResDepartment(CmsPass, VLng("PAGE_RESID"))
        If CmsPass.EmpIsDepAdmin And CmsPass.Employee.DepartmentId = lngCurrentDepID Then
            Dim colDel As ButtonColumn = New ButtonColumn
            colDel.HeaderText = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL")
            colDel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMD_DEL") '������ͼ�꣬�磺"<img src='/cmsweb/images/common/delete.gif' border=0>"
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
    '����DataGrid�����ֶκ��������
    '----------------------------------------------------------
    Public Sub ShowGridData(ByVal lngDocID As Long)
        Dim ds As DataSet = DocVersion.GetDocHistoryList(lngDocID)
        DataGrid1.DataSource = ds.Tables(0).DefaultView
        DataGrid1.DataBind()
    End Sub
End Class

End Namespace
