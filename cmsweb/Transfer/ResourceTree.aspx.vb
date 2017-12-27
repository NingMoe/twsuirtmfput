Imports Unionsoft.Platform
Imports NetReusables

Partial Class Transfer_ResourceTree
    Inherits CmsPage
    Protected strTree As String = ""
    Protected strResID As String = ""
    Protected strRecID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strResID = Request("resid")
        strRecID = Request("recid")

        Dim icon As String = ""
        Dim ResourceIDs As String = CmsConfig.GetString("SYS_CONFIG", "ACHIVESET")
        Dim dtResRights As DataTable = SDbStatement.Query("select * from CMS_RESOURCE ").Tables(0)
        strTree += "tree.nodes['0_-1'] = 'text:系统资源;url:#;target:;icon:RESOURCE;';" + vbCrLf
        Dim dr() As DataRow = dtResRights.Select("ID in (" + ResourceIDs + ")")
        For i As Integer = 0 To dr.Length - 1
            If DbField.GetInt(dr(i), "RES_ISFLOW") Then
                icon = "Flow"
            Else
                icon = IIf(DbField.GetStr(dr(i), "RES_TABLETYPE").Trim = "", "Empty", DbField.GetStr(dr(i), "RES_TABLETYPE").Trim) + IIf(DbField.GetInt(dr(i), "RES_TYPE") = 0, "", "_jc")
            End If
            strTree += "tree.nodes['-1_" + DbField.GetStr(dr(i), "ID") + "'] = 'text:" + DbField.GetStr(dr(i), "NAME") + ";url:ResourceList.aspx?id=" + DbField.GetStr(dr(i), "ID") + "&fromresid=" + strResID.Trim + "&fromrecid=" + strRecID.Trim + ";target:List;icon:" + icon.Trim + ";';" + vbCrLf
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
            strTree += "tree.nodes['" + IIf(PID = 0, "-1", PID.ToString) + "_" + DbField.GetStr(dr(i), "ID") + "'] = 'text:" + DbField.GetStr(dr(i), "NAME") + ";url:ResourceList.aspx?id=" + DbField.GetStr(dr(i), "ID") + "&fromresid=" + strResID.Trim + "&fromrecid=" + strRecID.Trim + ";target:List;icon:" + icon.Trim + ";';" + vbCrLf
            LoadTree(dtDept, DbField.GetLng(dr(i), "ID"))
        Next
    End Sub

    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
    '    Dim strResID As String = Me.InputValue.Value.Substring(Me.InputValue.Value.LastIndexOf("_") + 1)
    '    Response.Write(Me.InputValue.Value + "-------------" + strResID.Trim)

    'End Sub
End Class
