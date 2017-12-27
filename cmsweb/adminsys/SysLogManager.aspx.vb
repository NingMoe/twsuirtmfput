Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class SysLogManager
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DataGrid2 As System.Web.UI.WebControls.DataGrid

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Const DATAGRID_PAGESIZE As Integer = 16 'ÿҳ��ʾ������

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsSysSecurity = False Then 'ֻ��ϵͳ����Ա��ϵͳ��ȫԱ�ܽ�����־����
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        lbtnDelete.ToolTip = "ֻ��ϵͳ��ȫԱ��ɾ����־Ȩ��"
        If CmsPass.EmpIsSysSecurity Then
            lbtnDelete.Enabled = True
            lbtnDelete.Attributes.Add("onClick", "return window.confirm('��־ɾ�������ٻָ���ȷ��Ҫɾ��ϵͳ��־��');")
        Else
            lbtnDelete.Enabled = False
        End If

        DataGrid1.AllowSorting = True
        DataGrid1.PageSize = DATAGRID_PAGESIZE
        DataGrid1.AllowPaging = True
        DataGrid1.AllowCustomPaging = False
        DataGrid1.EnableViewState = True
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '---------------------------------------------------------------
        '��ʼ����ҳ�ؼ�
        Cmspager1.PageRows = DATAGRID_PAGESIZE
        Cmspager1.Language = CmsPass.Employee.Language
        Cmspager1.TableAlign = "left"
        Cmspager1.ButtonAlign = "left"
        Cmspager1.WordAlign = "right"
        Cmspager1.TableHeight = "25px"
        Cmspager1.TotalWidth = "280px"
        Cmspager1.ButtonsWidth = "130px"
        '---------------------------------------------------------------

        BindLogData()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lnkQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkQuery.Click
        DataGrid1.CurrentPageIndex = 0
        BindLogData()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")
            Next
        End If
    End Sub

    Private Sub lnkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        txtLogTitle.Text = ""
        txtLogContent.Text = ""
        txtUser.Text = ""
        txtIP.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        txtExt1.Text = ""
        txtExt2.Text = ""
        txtExt3.Text = ""
        txtExt4.Text = ""
        txtExt5.Text = ""
        txtExt6.Text = ""
        BindLogData("MoveFirstPage")
    End Sub

    Private Sub lbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDelete.Click
        CmsLog.DelLogs(CmsPass, txtLogTitle.Text.Trim(), txtResName.Text.Trim(), txtLogContent.Text.Trim(), txtUser.Text.Trim(), txtIP.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtResID.Text.Trim(), txtRecID.Text.Trim(), txtExt1.Text.Trim(), txtExt2.Text.Trim(), txtExt3.Text.Trim(), txtExt4.Text.Trim(), txtExt5.Text.Trim(), txtExt6.Text.Trim())

        BindLogData("MoveCurrentPage")
    End Sub

    Private Sub Cmspager1_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager1.Click
        BindLogData(eventArgument)
    End Sub

    Private Sub BindLogData(Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
        Try
            '--------------------------------------------------------------------------------------------
            '������ǰ�ķ�ҳ�ؼ��Ĵ���
            Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '����ҳ��
            DataGrid1.CurrentPageIndex = Cmspager1.CurrentPage
            '--------------------------------------------------------------------------------------------
            Dim intRowsCount As Integer = 500
            If txtRowsCount.Text <> "" Then
                Try
                    intRowsCount = CInt(txtRowsCount.Text.Trim())
                Catch ex As Exception

                End Try
            End If
            Dim intTotalRecNum As Integer = 0
            Dim ds As DataSet = CmsLog.GetLogs(CmsPass, intRowsCount, Cmspager1.RecStartOnCurPage, Cmspager1.PageRows, intTotalRecNum, txtLogTitle.Text.Trim(), txtResName.Text.Trim(), txtLogContent.Text.Trim(), txtUser.Text.Trim(), txtIP.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtResID.Text.Trim(), txtRecID.Text.Trim(), txtExt1.Text.Trim(), txtExt2.Text.Trim(), txtExt3.Text.Trim(), txtExt4.Text.Trim(), txtExt5.Text.Trim(), txtExt6.Text.Trim())
            Cmspager1.TotalRecordNumber = intTotalRecNum

            '�����ݼ�
            Dim dv As DataView = ds.Tables(0).DefaultView
            DataGrid1.VirtualItemCount = dv.Count
            DataGrid1.DataSource = dv
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg("�����쳣ʧ�ܣ������ѯ�����Ƿ���ȷ��", ex, True)
        End Try
    End Sub
End Class

End Namespace
