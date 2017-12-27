Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


'---------------------------------------------------------
'对当前活动的流程进行操作
'---------------------------------------------------------
Partial Class AdminWorkflowInstances
    Inherits AdminPageBase

    Private _action As String

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lnkDelete As System.Web.UI.WebControls.LinkButton

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _action = Request.QueryString("action")
        If _action = "finish" Then Me.ActiveFlows.Columns(Me.ActiveFlows.Columns.Count - 1).Visible = False
        If Not Me.IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub ActiveFlows_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles ActiveFlows.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.SelectedItem Then
            'e.Item.Cells(5).Attributes.Add("onClick", "return window.confirm('确实要挂起吗?')")
            Dim chk As CheckBox = CType(e.Item.FindControl("chkItemID"), CheckBox)
            If Not chk Is Nothing Then
                chk.Attributes.Item("ItemID") = CStr(ActiveFlows.DataKeys.Item(e.Item.ItemIndex))
            End If
        End If
    End Sub

    Private Sub Pager1_Click(ByVal sender As Object, ByVal eventArgument As String) Handles Pager1.Click
        Select Case eventArgument
            Case "MoveFirstPage"
                ActiveFlows.CurrentPageIndex = 0
            Case "MovePreviousPage"
                If ActiveFlows.CurrentPageIndex > 0 Then ActiveFlows.CurrentPageIndex = ActiveFlows.CurrentPageIndex - 1
            Case "MoveNextPage"
                If ActiveFlows.CurrentPageIndex < ActiveFlows.PageCount - 1 Then ActiveFlows.CurrentPageIndex = ActiveFlows.CurrentPageIndex + 1
            Case "MoveLastPage"
                ActiveFlows.CurrentPageIndex = ActiveFlows.PageCount - 1
        End Select
        BindData()
    End Sub

    Private Sub BindData()
        Dim lngWorkflowID As Long = CLng(IIf(IsNumeric(Request.QueryString("WorkflowID")) = True, Request.QueryString("WorkflowID"), 0))
        Dim strWorkflowMainFieldValue As String = Request.QueryString("WorkflowMainFieldValue")
        Dim strCreateDate As String = Request.QueryString("CreateDate")
        Dim strCreateDate1 As String = Request.QueryString("CreateDate1")

        Dim strSql As String = "SELECT ID,FlOWNAME,CreateTime,MainFieldValue,CreatorName FROM WF_INSTANCE "

        'Select Case _action
        '    Case "active"
        '        strSql = strSql & " WHERE TaskStatus=" & TaskStatusConstants.Actived
        '    Case "pause"
        '        strSql = strSql & " WHERE TaskStatus=" & TaskStatusConstants.Paused
        '    Case "finish"
        '        strSql = strSql & " WHERE TaskStatus=" & TaskStatusConstants.Finished
        'End Select
        strSql = strSql & " WHERE 1=1 "

        If lngWorkflowID <> 0 Then
            strSql = strSql & " AND FLOWID=" & lngWorkflowID
        End If
        If strWorkflowMainFieldValue <> "" Then
            strSql = strSql & " AND MainFieldValue LIKE '%" & strWorkflowMainFieldValue & "%'"
        End If
        If IsDate(strCreateDate) Then
            strSql = strSql & " AND CreateTime >='" & strCreateDate & " 00:00:00'"
        End If
        If IsDate(strCreateDate1) Then
            strSql = strSql & " AND CreateTime <='" & strCreateDate1 & " 23:59:59'"
        End If

        If _action = "active" Then
            strSql = strSql & " AND TaskStatus=" & WorkflowStatus.Active
        ElseIf _action = "finish" Then
            strSql = strSql & " AND TaskStatus=" & WorkflowStatus.Finish
        ElseIf _action = "pause" Then
            strSql = strSql & " AND TaskStatus=" & WorkflowStatus.Paused
        End If

        strSql = strSql & " ORDER BY CreateTime DESC"

        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)

        Me.ActiveFlows.DataSource = dt
        Me.ActiveFlows.DataBind()
        Pager1.CurrentPage = ActiveFlows.CurrentPageIndex
        Pager1.PageCount = ActiveFlows.PageCount
        Pager1.RecordCount = dt.Rows.Count
    End Sub

    Private Sub chSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chSelectAll.CheckedChanged
        Dim i As Integer
        For i = 0 To Me.ActiveFlows.Items.Count - 1
            Dim chk As CheckBox = CType(Me.ActiveFlows.Items(i).FindControl("chkItemID"), CheckBox)
            If Not chk Is Nothing Then
                chk.Checked = Me.chSelectAll.Checked
            End If
        Next
    End Sub

    Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim i As Integer
        For i = 0 To Me.ActiveFlows.Items.Count - 1
            Dim chk As CheckBox = CType(Me.ActiveFlows.Items(i).FindControl("chkItemID"), CheckBox)
            If Not chk Is Nothing Then
                If chk.Checked = True Then
                    WorkflowManager.DeleteWorkflowInstance(chk.Attributes.Item("ItemID"))
                End If
            End If
        Next
        BindData()
        Me.chSelectAll.Checked = False
    End Sub

    Private Sub lnkSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        Response.Redirect("AdminSearch.aspx?WorkflowID=" & Request.QueryString("WorkflowID"))
    End Sub

End Class

End Namespace
