Imports Unionsoft.Platform
Imports NetReusables
Imports Unionsoft.Cms.Web

Partial Class cmsrights_DepartmentFrmTree
    Inherits CmsPage

    'Protected strTree As String = ""

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Dim dtDept As DataTable = SDbStatement.Query("select * from CMS_DEPARTMENT").Tables(0)
    '    Dim dr() As DataRow = dtDept.Select("PID=-1")
    '    For i As Integer = 0 To dr.Length - 1
    '        Dim PID As Long = IIf(DbField.GetLng(dr(i), "ID") = 0, -1, DbField.GetLng(dr(i), "ID"))
    '        strTree += "tree.nodes['0_" + PID.ToString + "'] = 'text:" + DbField.GetStr(dr(i), "NAME") + ";url:#;target:left;icon:enterprise;';" + vbCrLf
    '        LoadTree(dtDept, DbField.GetLng(dr(i), "ID"))
    '    Next
    'End Sub

    'Protected Sub LoadTree(ByVal dtDept As DataTable, ByVal PID As Long)
    '    Dim dr() As DataRow = dtDept.Select("PID=" + PID.ToString)
    '    For i As Integer = 0 To dr.Length - 1
    '        strTree += "tree.nodes['" + IIf(PID = 0, "-1", PID.ToString) + "_" + DbField.GetStr(dr(i), "ID") + "'] = 'text:" + DbField.GetStr(dr(i), "NAME") + ";url:RightSetFrmContent.aspx?gainerid=" + DbField.GetStr(dr(i), "ID") + "&type=" + CType(RightsGainerType.IsDepartment, Integer).ToString + ";target:content;icon:" + IIf(DbField.GetInt(dr(i), "DEP_TYPE") = 0, "Dept", "virtual").ToString + ";';" + vbCrLf
    '        LoadTree(dtDept, DbField.GetLng(dr(i), "ID"))
    '    Next
    'End Sub



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()

    End Sub

    '----------------------------------------------------------------------------------------
    'Load自定义的树结构
    '----------------------------------------------------------------------------------------
    Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse)
        WebTreeDepartment.LoadResTreeView(pst, Request, Response, "/cmsweb/cmsrights/RightSetFrmContent.aspx", "content", AspPage.RStr("depid", Request), pst.Employee.ID)
    End Sub

End Class
