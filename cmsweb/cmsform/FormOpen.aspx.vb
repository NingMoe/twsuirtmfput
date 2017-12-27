Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FormOpen
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

        If VLng("PAGE_FORMTYPE") = 0 Then
            ViewState("PAGE_FORMTYPE") = RLng("mnuformtype")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '��Listbox����ʾ��������ƵĴ�������
        LoadDesignedForms()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    '--------------------------------------------------------------------
    '��Listbox����ʾ��������ƵĴ�������
    '--------------------------------------------------------------------
    Private Sub LoadDesignedForms()
        ListBox1.Items.Clear() '����б�

        Dim alistDForms As ArrayList = CTableForm.GetDesignedFormNames(CmsPass, VLng("PAGE_RESID"), CType(VInt("PAGE_FORMTYPE"), FormType))
        Dim strDFormName As String
        For Each strDFormName In alistDForms
            Dim li As New ListItem
            li.Text = strDFormName '�ֶ���ʾ����
            li.Value = strDFormName '�ֶ��ڲ�����
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
                PromptMsg("��ѡ����Ҫɾ���Ĵ�����ƣ�")
            End If
        Catch ex As Exception
            PromptMsg("ɾ���������ʧ�ܣ��������ӳ���")
        End Try
    End Sub
End Class

End Namespace
