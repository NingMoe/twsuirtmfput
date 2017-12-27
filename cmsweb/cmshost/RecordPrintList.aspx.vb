Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class RecordPrintList
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
        Try
            WebUtilities.InitialDataGrid(DataGrid1)    '设置DataGrid显示属性
            Dim lngResID As Long = VLng("PAGE_RESID")
            Dim dv As DataView = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetTableColumns(CmsPass, lngResID)
            WebUtilities.LoadResTableColumns(CmsPass, lngResID, DataGrid1, dv)

            '获取主表或者关联子表的查询条件
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

            '显示主表数据
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
