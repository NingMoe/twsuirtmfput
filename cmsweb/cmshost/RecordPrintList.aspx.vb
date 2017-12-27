Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class RecordPrintList
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
        Try
            WebUtilities.InitialDataGrid(DataGrid1)    '����DataGrid��ʾ����
            Dim lngResID As Long = VLng("PAGE_RESID")
            Dim dv As DataView = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetTableColumns(CmsPass, lngResID)
            WebUtilities.LoadResTableColumns(CmsPass, lngResID, DataGrid1, dv)

            '��ȡ������߹����ӱ�Ĳ�ѯ����
            Dim ds As DataSet = Nothing
            Dim strWhere As String = ""
            Dim strOrderby As String = ""
            Dim lngResLocate As ResourceLocation = CType(RLng("MNURESLOCATE"), ResourceLocation)
            If lngResLocate = ResourceLocation.HostTable Then
                strWhere = SStr("CMS_HOSTTABLE_WHERE")
                strOrderby = SStr("CMS_HOSTTABLE_ORDERBY")
                ds = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetHostTableData(CmsPass, lngResID, strWhere, strOrderby)
            ElseIf lngResLocate = ResourceLocation.RelTable Then
                strWhere = SStr("CMS_SUBTABLE_WHERE")
                strOrderby = SStr("CMS_SUBTABLE_ORDERBY")
                ds = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetRelTableData(CmsPass, RLng("mnuhostresid"), lngResID, RLng("mnuhostrecid"), strWhere, strOrderby)
            End If

            '��ʾ��������
            DataGrid1.DataSource = ds.Tables(0).DefaultView
            DataGrid1.DataBind()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
