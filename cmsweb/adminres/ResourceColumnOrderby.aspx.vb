Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceColumnOrderby
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
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
        txtResName.ReadOnly = True

        InitialDdlColumns(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn1, ddlOrderbyType1)
        InitialDdlColumns(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn2, ddlOrderbyType2)
        InitialDdlColumns(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn3, ddlOrderbyType3)
        InitialDdlColumns(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn4, ddlOrderbyType4)
        InitialDdlColumns(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn5, ddlOrderbyType5)
        InitialDdlColumns(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn6, ddlOrderbyType6)

        LoadOrderbyFromDb() 'Load���ݿ��е���������
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            ResFactory.ResService.SaveOrderby(CmsPass, VLng("PAGE_RESID"), ddlOrderColumn1.SelectedValue, ddlOrderbyType1.SelectedValue, ddlOrderColumn2.SelectedValue, ddlOrderbyType2.SelectedValue, ddlOrderColumn3.SelectedValue, ddlOrderbyType3.SelectedValue, ddlOrderColumn4.SelectedValue, ddlOrderbyType4.SelectedValue, ddlOrderColumn5.SelectedValue, ddlOrderbyType5.SelectedValue, ddlOrderColumn6.SelectedValue, ddlOrderbyType6.SelectedValue)
            LoadOrderbyFromDb() 'Load���ݿ��е���������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            ResFactory.ResService.SaveOrderby(CmsPass, VLng("PAGE_RESID"), "", "", "", "", "", "", "", "", "", "", "", "")
            LoadOrderbyFromDb() 'Load���ݿ��е���������
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '-----------------------------------------------------------
    '��ʼ��DropDownList���ֶ��б���
    '-----------------------------------------------------------
    Private Shared Sub InitialDdlColumns(ByRef pst As CmsPassport, ByRef lngResID As Long, ByRef ddlOrderColumn As DropDownList, ByRef ddlOrderbyType As DropDownList)
        If lngResID = 0 Then 'û����Դ�����
            ddlOrderColumn.Items.Clear()
            ddlOrderbyType.Items.Clear()
            Return
        End If

        ddlOrderColumn.Items.Clear()
        ddlOrderColumn.Items.Add(New ListItem("", ""))

        Dim datRes As DataResource = pst.GetDataRes(lngResID)
        If datRes.ResTable <> "" And datRes.ResTableType <> "" Then
            Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(pst, lngResID, True, True, True, True)
            Dim datCol As DataTableColumn
            For Each datCol In alistColumns
                ddlOrderColumn.Items.Add(New ListItem(datCol.ColDispName, datCol.ColName))
            Next
            alistColumns.Clear()
            alistColumns = Nothing

            ddlOrderbyType.Items.Clear()
            ddlOrderbyType.Items.Add(New ListItem("", ""))
            ddlOrderbyType.Items.Add(New ListItem("����", "DESC"))
            ddlOrderbyType.Items.Add(New ListItem("����", "ASC"))
        End If
    End Sub

    '-----------------------------------------------------------
    'Load���ݿ��е���������
    '-----------------------------------------------------------
    Private Sub LoadOrderbyFromDb()
        '�����ֶ�
        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
        Try
            ddlOrderColumn1.SelectedValue = datRes.Orderby1Column
        Catch ex As Exception
        End Try
        Try
            ddlOrderColumn2.SelectedValue = datRes.Orderby2Column
        Catch ex As Exception
        End Try
        Try
            ddlOrderColumn3.SelectedValue = datRes.Orderby3Column
        Catch ex As Exception
        End Try
        Try
            ddlOrderColumn4.SelectedValue = datRes.Orderby4Column
        Catch ex As Exception
        End Try
        Try
            ddlOrderColumn5.SelectedValue = datRes.Orderby5Column
        Catch ex As Exception
        End Try
        Try
            ddlOrderColumn6.SelectedValue = datRes.Orderby6Column
        Catch ex As Exception
        End Try

        '��������
        Try
            ddlOrderbyType1.SelectedValue = datRes.Orderby1Type
        Catch ex As Exception
        End Try
        Try
            ddlOrderbyType2.SelectedValue = datRes.Orderby2Type
        Catch ex As Exception
        End Try
        Try
            ddlOrderbyType3.SelectedValue = datRes.Orderby3Type
        Catch ex As Exception
        End Try
        Try
            ddlOrderbyType4.SelectedValue = datRes.Orderby4Type
        Catch ex As Exception
        End Try
        Try
            ddlOrderbyType5.SelectedValue = datRes.Orderby5Type
        Catch ex As Exception
        End Try
        Try
            ddlOrderbyType6.SelectedValue = datRes.Orderby6Type
        Catch ex As Exception
        End Try
    End Sub
End Class

End Namespace
