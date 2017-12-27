
Imports NetReusables
Imports Unionsoft.Platform

Partial Class adminres_ViewRelationList
    Inherits CmsPage

    Protected HostResid As Long = 0
    Protected lngResID As Long = 0
    Protected Deptid As Long = 0
    Protected Selectid As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("mnuresid") Then HostResid = Convert.ToInt64(Request("mnuresid"))
        If Request("depid") Then Deptid = Convert.ToInt64(Request("depid"))

        If IsPostBack Then Return
        BindData()

    End Sub

    Protected Sub BindData()
        'Dim strSql As String = "select  distinct  RESOURCEID from " _
        '                    & " (select RESOURCEID1 RESOURCEID from CMS_VIEW_RELATION where CR_RESID=0 " _
        '                    & " union " _
        '                    & " select RESOURCEID2 RESOURCEID from CMS_VIEW_RELATION where CR_RESID=0)t ;"
        Dim strSql As String = "SELECT *,RESOURCENAME1='',RESOURCENAME2='' FROM CMS_VIEW_RELATION_TABLE  where CR_RESID=0; select R.*,RESOURCENAME1='',RESOURCENAME2='',COLUMNDISPNAME1='',COLUMNDISPNAME2='' from CMS_VIEW_RELATION R JOIN CMS_VIEW_RELATION_TABLE T ON R.CR_ID=T.ID ;select *,ResourceName='',COLUMNDISPNAME='' from CMS_VIEW_SHOW where CS_RESID=0"

        Dim ds As DataSet = SDbStatement.Query(strSql)

        dtRelationTable = ds.Tables(0)
        dtRelation = ds.Tables(1)
        dtShow = ds.Tables(2)
    End Sub

    Protected Sub BindData_Resoures()
        Dim strWhere As String = Me.txtResourcesID.Text.Trim
        If strWhere.Trim.Length > 0 Then strWhere = strWhere.Substring(1)
        Dim dv As DataView = ResFactory.ResService.GetResourcesView(CmsPass, False, "ID in (" + strWhere + ")")
        repResource.DataSource = dv
        repResource.DataBind()
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        BindData_Resoures()
    End Sub

    Protected Sub repResource_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repResource.ItemDataBound
        Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim dt As DataTable = SDbStatement.Query("select * from " & CmsTables.ColDefine & " where CD_RESID=" & dr("ID").ToString).Tables(0)
        Dim dg As DataGrid = CType(e.Item.FindControl("dgResourceColumn"), DataGrid)
        dg.DataSource = dt
        dg.DataBind()
    End Sub

    Protected Sub btnRelation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRelation.Click
        Dim dt As DataTable = dtRelation
        Dim dt1 As DataTable = dtRelationTable

        Dim index As Integer = 0
        Dim dr As DataRow
        Dim dr1 As DataRow
        For i As Integer = 0 To repResource.Items.Count - 1
            Dim dg As DataGrid = CType(repResource.Items(i).FindControl("dgResourceColumn"), DataGrid)
            For j As Integer = 0 To dg.Items.Count - 1
                If CType(dg.Items(j).FindControl("chkSelected"), CheckBox).Checked Then

                    If index = 0 Then
                        dr = dt.NewRow
                        dr1 = dt1.NewRow
                        dr1("ID") = TimeId.CurrentMilliseconds(30)
                        dr1("RESOURCEID1") = CType(repResource.Items(i).FindControl("txtResID"), TextBox).Text.Trim
                        dr1("RESOURCENAME1") = CType(repResource.Items(i).FindControl("txtResName"), TextBox).Text.Trim


                        dr("ID") = TimeId.CurrentMilliseconds(30)
                        dr("CR_ID") = dr1("ID").ToString
                        dr("RESOURCEID1") = CType(repResource.Items(i).FindControl("txtResID"), TextBox).Text.Trim
                        dr("RESOURCENAME1") = CType(repResource.Items(i).FindControl("txtResName"), TextBox).Text.Trim
                        dr("COLUMNNAME1") = dg.DataKeys(j).ToString
                        dr("COLUMNDISPNAME1") = CType(dg.Items(j).FindControl("txtColName"), TextBox).Text.Trim
                       
                        index += 1

                    Else
                        dr("RESOURCEID2") = CType(repResource.Items(i).FindControl("txtResID"), TextBox).Text.Trim
                        dr("RESOURCENAME2") = CType(repResource.Items(i).FindControl("txtResName"), TextBox).Text.Trim
                        dr("COLUMNNAME2") = dg.DataKeys(j).ToString
                        dr("COLUMNDISPNAME2") = CType(dg.Items(j).FindControl("txtColName"), TextBox).Text.Trim

                        dr1("RESOURCEID2") = CType(repResource.Items(i).FindControl("txtResID"), TextBox).Text.Trim
                        dr1("RESOURCENAME2") = CType(repResource.Items(i).FindControl("txtResName"), TextBox).Text.Trim
                        Exit For
                    End If
                End If
            Next
        Next
        dt.Rows.Add(dr)
        If dt1.Select("(RESOURCEID1=" + dr1("RESOURCEID1").ToString + " and RESOURCEID2=" + dr1("RESOURCEID2").ToString + ") or (RESOURCEID2=" + dr1("RESOURCEID1").ToString + " and RESOURCEID1=" + dr1("RESOURCEID2").ToString + ")").Length = 0 Then dt1.Rows.Add(dr1)
        dtRelationTable = dt1
        dtRelation = dt
        BindData_Resoures()
        Me.repRelation.DataSource = dt1
        Me.repRelation.DataBind()
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Dim dt As DataTable = dtShow

        For i As Integer = 0 To repResource.Items.Count - 1
            Dim dg As DataGrid = CType(repResource.Items(i).FindControl("dgResourceColumn"), DataGrid)
            For j As Integer = 0 To dg.Items.Count - 1
                If CType(dg.Items(j).FindControl("chkSelected"), CheckBox).Checked Then
                    Dim dr As DataRow = dt.NewRow
                    dr = dt.NewRow
                    dr("ID") = TimeId.CurrentMilliseconds(30)
                    dr("CS_RESID") = 0
                    dr("RESOURCEID") = CType(repResource.Items(i).FindControl("txtResID"), TextBox).Text.Trim
                    dr("RESOURCENAME") = CType(repResource.Items(i).FindControl("txtResName"), TextBox).Text.Trim
                    dr("COLUMNNAME") = dg.DataKeys(j).ToString
                    dr("COLUMNDISPNAME") = CType(dg.Items(j).FindControl("txtColName"), TextBox).Text.Trim
                    dt.Rows.Add(dr)
                End If
            Next
        Next
        dtShow = dt
        BindData_Resoures()
        Me.dgShow.DataSource = dt
        Me.dgShow.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        lngResID = ResFactory.ResService.AddResource(CmsPass, txtName.Text.Trim, ResInheritType.IsIndependent, HostResid, Deptid)


        Dim dbs As DbStatement = New DbStatement(SDbConnectionPool.GetDbConfig())
        Dim tableShow As DataTable = dtShow

        For i As Integer = 0 To tableShow.Rows.Count - 1
            tableShow.Rows(i)("CS_RESID") = lngResID
        Next

        Dim tableRelationTable As DataTable = dtRelationTable
        For i As Integer = 0 To tableRelationTable.Rows.Count - 1
            tableRelationTable.Rows(i)("CR_RESID") = lngResID
        Next

        Dim ds As New DataSet
        tableShow.TableName = "CMS_TABLE_SHOW"
        ds.Tables.Add(tableShow)
        tableRelationTable.TableName = "CMS_VIEW_RELATION_TABLE"
        ds.Tables.Add(tableRelationTable)

        Dim tableRelation As DataTable = dtRelation
        tableRelation.TableName = "CMS_VIEW_RELATION"
        ds.Tables.Add(tableRelation)

        dbs.QueryUpdate(ds)

    End Sub

    Protected Sub repRelation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repRelation.ItemDataBound
       
        Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
        Dim dt As DataTable = dtRelation
        Dim dg As DataGrid = CType(e.Item.FindControl("dgRelation"), DataGrid)
        Dim dr() As DataRow = dt.Select("CR_ID=" + drv("ID").ToString)
        Dim dt1 As DataTable = dt.Clone

        For j As Integer = 0 To dr.Length - 1
            Dim dr1 As DataRow = dt1.NewRow
            For i As Integer = 0 To dt1.Columns.Count - 1

                dr1(dt1.Columns(i).ColumnName) = dr(j)(dt1.Columns(i).ColumnName)
            Next
            dt1.Rows.Add(dr1)
        Next 
        dg.DataSource = dt1
        dg.DataBind()
    End Sub

    Protected Property dtRelationTable() As DataTable
        Get
            If ViewState("dtRelationTable") Is Nothing Then Return Nothing
            Return CType(ViewState("dtRelationTable"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtRelationTable") = value
        End Set
    End Property

    Protected Property dtRelation() As DataTable
        Get
            If ViewState("dtRelation") Is Nothing Then Return Nothing
            Return CType(ViewState("dtRelation"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtRelation") = value
        End Set
    End Property

    Protected Property dtShow() As DataTable
        Get
            If ViewState("dtShow") Is Nothing Then Return Nothing
            Return CType(ViewState("dtShow"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtShow") = value
        End Set
    End Property


End Class
