Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class AdminEditPermissoin
    Inherits AdminPageBase

    Private _WorkflowId As Long

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
        _WorkflowId = CLng(Request.QueryString("WorkflowId"))

        If Me.Page.IsPostBack Then Exit Sub
        BindData(_WorkflowId)
    End Sub

    Private Sub BindData(ByVal WorkflowId As Long)
        Dim table As New DataTable
        Dim cln As New DataColumn
        table.Columns.Add("OrgName", System.Type.GetType("System.String"))
        table.Columns.Add("OrgCode", System.Type.GetType("System.String"))
        table.Columns.Add("OrgType", System.Type.GetType("System.String"))

        Dim dt As DataTable = PermissionUtility.GetWorkflowItemPermissions(WorkflowId)
        For i As Integer = 0 To dt.Rows.Count - 1
            Select Case CType(dt.Rows(i)("ORGTYPE"), OrganizationTypeConstants)
                Case OrganizationTypeConstants.Department
                    Dim Dept As Department = OrganizationFactory.Implementation.GetDepartment(CType(dt.Rows(i)("ORGCODE"), String))
                    If Not Dept Is Nothing Then
                        Dim tr As DataRow = table.NewRow()
                        tr("OrgName") = Dept.Name
                        tr("OrgCode") = Dept.Code
                        tr("OrgType") = CStr(OrganizationTypeConstants.Department)
                        table.Rows.Add(tr)
                    End If
                Case OrganizationTypeConstants.Person
                    Dim Emp As Employee = OrganizationFactory.Implementation.GetEmployee(CType(dt.Rows(i)("ORGCODE"), String))
                    If Not Emp Is Nothing Then
                        Dim tr As DataRow = table.NewRow()
                        tr("OrgName") = Emp.Name
                        tr("OrgCode") = Emp.Code
                        tr("OrgType") = CStr(OrganizationTypeConstants.Person)
                        table.Rows.Add(tr)
                    End If
            End Select
        Next
        Me.PermissionList.DataSource = table
        Me.PermissionList.DataBind()
    End Sub

    '增加权限设置
    Private Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdd.Click
        Response.Redirect("AdminEmployeeSelect.aspx?WorkflowId=" & _WorkflowId.ToString())
        Response.End()
    End Sub

    Private Sub PermissionList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles PermissionList.ItemCommand
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim permission As New PermissionTransact
            Dim txt As TextBox
            Dim strOrgCode As String, strOrgType As String
            'Dim flowid As Long = CLng(Request.QueryString("FlowID"))
            txt = CType(e.Item.FindControl("OrgCode"), TextBox)
            strOrgCode = txt.Text
            txt = CType(e.Item.FindControl("OrgType"), TextBox)
            strOrgType = txt.Text
            If strOrgType <> "" Then
                PermissionUtility.Delete(_WorkflowId, strOrgCode, CType(strOrgType, OrganizationTypeConstants))
            End If

            BindData(_WorkflowId)
        End If
    End Sub

    Private Sub PermissionList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles PermissionList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim txt As TextBox
            Dim drView As DataRowView = CType(e.Item.DataItem, DataRowView)
            txt = CType(e.Item.FindControl("OrgCode"), TextBox)
            txt.Text = DbField.GetStr(drView, "OrgCode")
            txt = CType(e.Item.FindControl("OrgType"), TextBox)
            txt.Text = DbField.GetStr(drView, "OrgType")

            Dim lnkbtn As LinkButton = CType(e.Item.FindControl("Delete"), LinkButton)
            If Not lnkbtn Is Nothing Then
                lnkbtn.Attributes.Add("onclick", "javascript:return window.confirm('确实要删除吗?');")
            End If
        End If
    End Sub

End Class

End Namespace
