Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceCreateTable
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
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_SELFDEFINE_TABLENAME")
        txtTableName.Visible = bln
        lblTableName.Visible = bln
        lblComments.Visible = bln
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        LoadResTypeOptions() '��ʼ����Դ���͵�Listbox
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
            PromptMsg("������Դ����ʧ�ܣ��޷��������ݿ⣡")
        End Try
    End Sub

    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '------------------------------------------------------
    '��ʼ����Դ���͵�Listbox
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
