Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform

Partial Class adminres_ResourceColumnUrlSet
    Inherits CmsPage

    Protected ColName As String = ""
    Protected ResID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ColName = Request("colname")
        ResID = Request("resid")
        If Not IsPostBack Then
            lblResName.Text = CmsPass.GetDataRes(ResID).ResName
            Dim dr() As DataRow = CTableStructure.GetColumnShowsByDataset(CmsPass, ResID, True, True).Tables(0).Select("CS_COLNAME='" + ColName + "'")
            If dr.Length > 0 Then
                Me.lblColName.Text = dr(0)("CD_DISPNAME").ToString
            End If
            BindData()
        End If
    End Sub


    Protected Sub BindData()
        dgColumnUrl.DataSource = SDbStatement.Query("select * from CMS_TABLE_COLURL ")
        dgColumnUrl.DataBind()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim dt As DataTable = SDbStatement.Query("select * from CMS_TABLE_COLURL").Tables(0)
        Dim dr As DataRow = dt.NewRow
        'CU_RESID, CU_COLNAME, CU_URL, CU_TARGET, CU_PARAM, ISENABLED
        dr("CU_ID") = 0
        dr("CU_RESID") = ResID
        dr("CU_COLNAME") = ColName
        dr("CU_URL") = ""
        dr("CU_TARGET") = ""
        dr("CU_PARAM") = ""
        dr("ISENABLED") = True
        dt.Rows.Add(dr)

        dgColumnUrl.EditItemIndex = dgColumnUrl.Items.Count
        dgColumnUrl.DataSource = dt
        dgColumnUrl.DataBind()
    End Sub

    Protected Sub dgColumnUrl_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgColumnUrl.CancelCommand
        dgColumnUrl.EditItemIndex = -1
        BindData()
    End Sub

    Protected Sub dgColumnUrl_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgColumnUrl.EditCommand
        dgColumnUrl.EditItemIndex = e.Item.ItemIndex
        BindData()
    End Sub

    Protected Sub dgColumnUrl_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgColumnUrl.UpdateCommand
        Dim hst As New Hashtable
        Dim CU_ID As Int64 = Convert.ToInt64(dgColumnUrl.DataKeys(e.Item.ItemIndex))
        hst.Add("CU_URL", CType(e.Item.FindControl("txtUrl"), TextBox).Text)
        hst.Add("CU_TARGET", CType(e.Item.FindControl("txtTarget"), TextBox).Text)
        hst.Add("CU_PARAM", CType(e.Item.FindControl("txtParam"), TextBox).Text)
        hst.Add("ISENABLED", CType(e.Item.FindControl("chk"), CheckBox).Checked)
        hst.Add("CU_RESID", ResID)
        hst.Add("CU_COLNAME", ColName)
        If CU_ID = 0 Then
            hst.Add("CU_ID", TimeId.CurrentMilliseconds(30))
            SDbStatement.InsertRow(hst, "CMS_TABLE_COLURL")
        Else
            SDbStatement.UpdateRows(hst, "CMS_TABLE_COLURL", "CU_ID=" + CU_ID.ToString)
        End If
        dgColumnUrl.EditItemIndex = -1
        BindData()
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgColumnUrl.ItemCreated
        If dgColumnUrl.Items.Count > 0 Then
            Dim row As DataGridItem
            For Each row In dgColumnUrl.Items
                Dim strCU_ID As String = Trim(row.Cells(0).Text) '第1列必须是字段内部名称
                row.Attributes.Add("RECID", strCU_ID)
                row.Attributes.Add("OnClick", "RowLeftClickNoPost(this)")
            Next
        End If
    End Sub
End Class
