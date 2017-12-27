Imports Unionsoft.Platform
Imports NetReusables
Imports Unionsoft.Cms.Web

Partial Class exdtc_config
    Inherits System.Web.UI.Page

    '----------------------------------------------------------------------------------------
    'Load自定义的树结构
    '----------------------------------------------------------------------------------------
    Protected Sub LoadDepartTree()
        LoadResTreeView("", "")
        Response.Write(vbCrLf & "document.write(deptree.toString());")
        Response.Write(vbCrLf & "-->")
        Response.Write(vbCrLf & "</script>")
    End Sub

    Public Sub LoadResTreeView(ByVal strDepUrl As String, ByVal strDepTarget As String)
        '生成树节点前的准备
        Dim strTreeName As String = "deptree"
        WebTreeview.TreePrepare(Response, strTreeName)

        '创建根节点：企业
        Dim dt As DataTable = SDbStatement.Query("SELECT * FROM CMS_DEPARTMENT WHERE PID=-1").Tables(0)
        Dim strDepName As String = dt.Rows(0)("Name")
        WebTreeview.AddOneNode(Request, Response, WebTreeType.DepartmentOnly, 0, -1, 0, False, strDepName, strDepUrl, strDepTarget, "", "", "", "", "ICON_ENTERPRISE", strTreeName) '设置根节点

        '创建根节点下所有部门节点
        Dim resCondDep As New DataResCondition
        resCondDep.ForceToShowAllDeps = True
        Dim dsDep As DataSet = SDbStatement.Query("SELECT * FROM CMS_DEPARTMENT WHERE PID>=0")
        WebTreeview.GenerateDepTree(Request, Response, dsDep.Tables(0).DefaultView, WebTreeType.DepAndRes, strDepUrl, strDepTarget, strTreeName)

        Dim ress As DataSet = SDbStatement.Query("SELECT * FROM CMS_RESOURCE WHERE PID>=0")
        WebTreeview.GenerateResourceTree(Request, Response, ress.Tables(0).DefaultView, WebTreeType.DepAndRes, "", "", strTreeName)
    End Sub

End Class
