Imports Unionsoft.Platform
Imports NetReusables
Imports Unionsoft.Cms.Web

Partial Class cmsrights_ResourceTree
    Inherits CmsPage
    Protected strTree As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GainerID = Request("gainerid").ToString 
        If Request.QueryString("type") IsNot Nothing Then GainerType = Convert.ToInt32(Request.QueryString("type"))
        If GainerType = RightsGainerType.IsEmployee Then
            GainerID = OrgFactory.EmpDriver.GetEmpID(CmsPass, Convert.ToInt64(GainerID)).Trim
        End If

        Dim str As String = ""
        Dim strWhere As String = ""
        If CmsPass.EmpIsSysAdmin Then
            strWhere = "PID=0"
        ElseIf CmsPass.EmpIsDepAdmin Then
            Dim dt As DataTable = SDbStatement.Query("select ID from CMS_DEPARTMENT where DEP_ADMIN_ID='" + CmsPass.Employee.ID.Trim + "'").Tables(0)
            Dim ch() As Char = {","}
            For i As Integer = 0 To dt.Rows.Count - 1
                If strWhere.Trim <> "" Then strWhere += " or "
                strWhere += "HOST_ID='" + DbField.GetStr(dt.Rows(i), "ID").Trim + "'"
                Dim strChildDept() As String = OrgFactory.DepDriver.GetDepChildren(CmsPass, DbField.GetStr(dt.Rows(i), "ID")).Split(ch, StringSplitOptions.RemoveEmptyEntries)
                For j As Integer = 0 To strChildDept.Length - 1
                    strWhere += " or HOST_ID='" + strChildDept(j).Trim + "'"
                Next
            Next
            If strWhere.Trim <> "" Then
                strWhere = "(" + strWhere + ") and PID=0"
            Else
                strWhere = "HOST_ID='-1'"
            End If
        End If

        lblName.Text = str
        Dim icon As String = ""

        Dim dtResRights As DataTable = Unionsoft.Platform.CmsRights.GetAllResource_RightsValue(GainerID.Trim)
        Dim dr() As DataRow = dtResRights.Select(strWhere)

        strTree += "tree.nodes['0_-1'] = 'text:系统资源;url:#;target:left;icon:RESOURCE;';" + vbCrLf

        For i As Integer = 0 To dr.Length - 1
            If DbField.GetInt(dr(i), "RES_ISFLOW") Then
                icon = "Flow"
            Else
                icon = IIf(DbField.GetStr(dr(i), "RES_TABLETYPE").Trim = "", "Empty", DbField.GetStr(dr(i), "RES_TABLETYPE").Trim) + IIf(DbField.GetInt(dr(i), "RES_TYPE") = 0, "", "_jc")
            End If
            strTree += "tree.nodes['-1_" + DbField.GetStr(dr(i), "ID") + "'] = 'text:" + DbField.GetStr(dr(i), "NAME") + IIf(DbField.GetLng(dr(i), "RightsValue") > 0, " *", "") + ";url:RightSetFrm.aspx?resid=" + DbField.GetStr(dr(i), "ID") + "&gainerid=" + Request.QueryString("gainerid").Trim + "&type=" + Request.QueryString("type").Trim + ";target:List;icon:" + icon.Trim + ";';" + vbCrLf
            LoadTree(dtResRights, DbField.GetLng(dr(i), "ID"))
        Next
    End Sub

    Protected Sub LoadTree(ByVal dtDept As DataTable, ByVal PID As Long)
        Dim icon As String = ""

        Dim dr() As DataRow = dtDept.Select("PID=" + PID.ToString)
        For i As Integer = 0 To dr.Length - 1
            If DbField.GetInt(dr(i), "RES_ISFLOW") Then
                icon = "Flow"
            Else
                icon = IIf(DbField.GetStr(dr(i), "RES_TABLETYPE").Trim = "", "Empty", DbField.GetStr(dr(i), "RES_TABLETYPE").Trim) + IIf(DbField.GetInt(dr(i), "RES_TYPE") = 0, "", "_jc")
            End If
            strTree += "tree.nodes['" + IIf(PID = 0, "-1", PID.ToString) + "_" + DbField.GetStr(dr(i), "ID") + "'] = 'text:" + DbField.GetStr(dr(i), "NAME") + IIf(DbField.GetLng(dr(i), "RightsValue") > 0, " *", "") + ";url:RightSetFrm.aspx?resid=" + DbField.GetStr(dr(i), "ID") + "&gainerid=" + Request.QueryString("gainerid").Trim + "&type=" + Request.QueryString("type").Trim + ";target:List;icon:" + icon.Trim + ";';" + vbCrLf
            LoadTree(dtDept, DbField.GetLng(dr(i), "ID"))
        Next
    End Sub

    Public Property GainerID() As String
        Get
            If ViewState("gainerid") Is Nothing Then ViewState("gainerid") = ""
            Return ViewState("gainerid").ToString
        End Get
        Set(ByVal value As String)
            ViewState("gainerid") = value
        End Set
    End Property



    Public Property GainerType() As Integer
        Get
            If ViewState("gainertype") Is Nothing Then ViewState("gainertype") = 0
            Return Convert.ToInt32(ViewState("gainertype"))
        End Get
        Set(ByVal value As Integer)
            ViewState("gainertype") = value
        End Set
    End Property
End Class
