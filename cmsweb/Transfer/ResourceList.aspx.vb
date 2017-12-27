Imports Unionsoft.Platform
Imports Unionsoft.Cms.Web
Imports NetReusables

Partial Class Transfer_ResourceList
    Inherits CmsFrmContentBase

    Protected ResID As String = ""
    Dim FromResID As String = ""
    Dim FromRecID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ResID = Request("id")
        FromResID = Request.QueryString("mnuresid")
        Dim dt As DataTable = SDbStatement.Query("select distinct Toresid from dbo.CMS_DTC_RELATION where fromresid=" + FromResID).Tables(0)
        If dt.Rows.Count > 0 Then
            ResID = dt.Rows(0)(0)
        End If
        FromRecID = Request.QueryString("mnurecid")
        If Not IsPostBack Then
            BindData()
        End If


    End Sub

    Public Sub BindData()
        Dim tablename As String = "WORKFLOW_FORM_Tentative"

        Dim sql As String = "select id from  cms_resource where res_table='WORKFLOW_FORM_Tentative' and res_type=0"

        'ResFactory.ResService.GetResTable(CmsPassport.GenerateCmsPassportForInnerUse(""), ResID)
        '  Dim IsIndependent As Boolean = ResFactory.ResService.IsIndependentResource(CmsPassport.GenerateCmsPassportForInnerUse(""), ResID)

        Dim lngResID As Long = SDbStatement.Query(sql).Tables(0).Rows(0)(0)

        Dim dtColumn As DataTable = SDbStatement.Query("select CD_COLNAME,CD_DISPNAME from CMS_TABLE_DEFINE join CMS_TABLE_SHOW on CMS_TABLE_SHOW.CS_COLNAME=CMS_TABLE_DEFINE.CD_COLNAME and CMS_TABLE_SHOW.CS_RESID=CMS_TABLE_DEFINE.CD_RESID where CD_RESID=" + lngResID.ToString + " and CS_SHOW_ENABLE=1 order by CS_SHOW_ORDER").Tables(0)
        Dim strColumn As String = ""
        For i As Integer = 0 To dtColumn.Rows.Count - 1
            strColumn += "," + DbField.GetStr(dtColumn.Rows(i), "CD_COLNAME") + " as " + DbField.GetStr(dtColumn.Rows(i), "CD_DISPNAME")
        Next

        Dim strSql As String = "select ID, RESID, RELID, CRTID, CRTTIME, EDTID, EDTTIME " + strColumn.Trim + " from " + tablename.Trim
        Me.dgResource.DataSource = SDbStatement.Query(strSql).Tables(0)
        Me.dgResource.DataBind()
    End Sub

    'Public Function GetResID(ByVal ResID As Long) As Long
    '    If Not ResFactory.ResService.IsIndependentResource(CmsPass, ResID) Then
    '        ResID = ResFactory.ResService.GetResParentID(CmsPass, ResID)
    '        ResID = GetResID(ResID)
    '    End If
    '    Return ResID
    'End Function

    Protected Sub dgResource_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgResource.ItemDataBound
        For i As Integer = 0 To 6
            e.Item.Cells(i).Style.Add("display", "none")
        Next
        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Attributes.Add("Recid", DbField.GetStr(drv, "ID"))
            e.Item.Attributes.Add("Resid", DbField.GetStr(drv, "RESID"))
            e.Item.Attributes.Add("FileNum", DbField.GetStr(drv, "档号"))
            e.Item.Attributes.Add("onclick", "selectedRow(this);")
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Response.Redirect("Transfer.aspx?Fromresid=" + FromResID.Trim + "&FromRecID=" + FromRecID + "&ToFileNum=" + Me.TextToFileNum.Text)
        'Response.Write("RecID:" + txtRecID.Text + "----ResID:" + txtResID.Text + "----FromResID:" + FromResID.Trim + "----FromRecID:" + FromRecID.Trim)
    End Sub
End Class
 
