Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmVirtualDep
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lnkExit As System.Web.UI.WebControls.LinkButton

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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        'lnkDelete.Attributes.Add("onClick", "return CmsPrmoptConfirm('ȷ��Ҫɾ�����ⲿ���µ���Ա��');")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        If RStr("depempcmd") = "selected_depemp" Then '��ѡ������Աҳ�����
                Dim lngAiid As String = RStr("empaiid")
                Dim ch() As Char = {CType(",", Char)}
                Dim EmpID() As String = lngAiid.Split(ch, StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To EmpID.Length - 1
                    Dim strEmpID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, Convert.ToInt64(EmpID(i)))
                    If strEmpID <> "" Then
                        '���ж�ѡ�����Ա�Ƿ��Ѿ����������ⲿ����
                        Dim lngNum As Long = CmsDbBase.CountRows(CmsPass, CmsTables.DepartmentVirtual, "VDEP_DEPID=" & RLng("depid") & " AND VDEP_EMPID='" & strEmpID & "'")
                        If lngNum > 0 Then
                            '������ʾ
                            'PromptMsg("��Ա�ʺ�(" & strEmpID & ")�Ѿ������뵱ǰ���ⲿ���У�")
                        Else
                            '�����Ա�����ⲿ����
                            DbVirtualDep.AddEmployee(CmsPass, RLng("depid"), strEmpID)
                        End If
                    End If
                Next
        End If


        GridDataBind()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
        GridDataBind()
    End Sub

    Private Sub lnkAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAdd.Click
        Session("CMSBP_DepEmpList") = "/cmsweb/adminsys/EmployeeFrmVirtualDep.aspx?depid=" & RLng("depid")
            Response.Redirect("/cmsweb/adminsys/DepEmpList.aspx?nodep=yes&type=Virtual", False)
    End Sub

    Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lngRecID As Long = Me.GetRecIDOfGrid()
        If lngRecID = 0 Then
            PromptMsg("����ѡ����Ч����Ա��")
            Return
        End If

        DbVirtualDep.DelEmployee(CmsPass, lngRecID)
        GridDataBind()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '�û���δ��¼����Session���ڣ�ת����¼ҳ��
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�

            WebUtilities.InitialDataGrid(DataGrid1) '����DataGrid��ʾ����
            CreateDataGridColumn()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        '����ÿ�е�ΨһID��OnClick���������ڴ��ط���������Ӧ���������޸ġ�ɾ���ȣ����������Htmlҳ���в��ִ�������ʹ��
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim lngRecIDClicked As Long = GetRecIDOfGrid()
            Dim row As DataGridItem
            For Each row In DataGrid1.Items
                '���ÿͻ��˵ļ�¼ID��Javascript�������������ǹ��������ԴID
                    Dim strRecID As String = row.Cells(0).Text.Trim()
                    Dim strUserID As String = row.Cells(1).Text.Trim()
                If IsNumeric(strRecID) Then
                        row.Attributes.Add("RECID", strRecID) '�ڿͻ��˱����¼ID
                        row.Attributes.Add("USERID", strUserID)
                    row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)") '�ڿͻ������ɣ������¼����Ӧ����OnClick()

                    If lngRecIDClicked > 0 And lngRecIDClicked = CLng(strRecID) Then
                        row.Attributes.Add("bgColor", "#C4D9F9") '�޸ı������¼�ı�����ɫ
                    End If
                End If
            Next
        End If
    End Sub

    '------------------------------------------------------------------
    '�����޸ı�ṹ��DataGrid����
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Dim intWidth As Integer = 0

        Dim col As BoundColumn = New BoundColumn
        col.HeaderText = "VDEP_ID" '�ؼ��ֶ�
        col.DataField = "VDEP_ID"
        col.ItemStyle.Width = Unit.Pixel(1)
        col.Visible = False
        DataGrid1.Columns.Add(col)

        col = New BoundColumn
        col.HeaderText = "��Ա�ʺ�"
        col.DataField = "VDEP_EMPID"
        col.ItemStyle.Width = Unit.Pixel(150)
        DataGrid1.Columns.Add(col)
        intWidth += 150

        col = New BoundColumn
        col.HeaderText = "��Ա����"
        col.DataField = "EMP_NAME" '"VDEP_EMPID2"
        col.ItemStyle.Width = Unit.Pixel(300)
        DataGrid1.Columns.Add(col)
        intWidth += 300

        DataGrid1.Width = Unit.Pixel(intWidth)
    End Sub

    '---------------------------------------------------------------
    '������
    '---------------------------------------------------------------
    Private Sub GridDataBind()
        Try
            Dim ds As DataSet = DbVirtualDep.GetEmpListByDataSet(CmsPass, RLng("depid"))
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try
    End Sub

        'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
        '        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
        '        e.Item.Attributes.Add("uid", DbField.GetStr(drv, "VDEP_EMPID"))
        '        e.Item.Attributes.Add("onclick", "selectRows(this);")
        '    End If
        'End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim ds As DataSet = DbVirtualDep.GetEmpListByDataSet(CmsPass, RLng("depid"))
                Dim dv As DataView = ds.Tables(0).DefaultView
                dv.RowFilter = " VDEP_EMPID='" + Me.txtSearch.Text.Trim + "' or EMP_NAME like '%" + Me.txtSearch.Text.Trim + "%' "
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch ex As Exception
                PromptMsg(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
