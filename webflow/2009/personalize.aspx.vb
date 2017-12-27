Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web


Partial Class personalize
    Inherits UserPageBase

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

    Private dtWorkflowCategories As DataTable = WorkflowManager.GetWorkflowCategories()
    Private oWorkflowShortcuts As WorkflowShortcuts

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oWorkflowShortcuts = New WorkflowShortcuts(Me.CurrentUser.Code)
    End Sub

    Protected Sub GenerateWorkflowCategories(ByVal CategoryId As Long)
        Dim rows() As DataRow = dtWorkflowCategories.Select("PId=" & CategoryId)
        For i As Integer = 0 To rows.Length - 1
            CategoryId = CLng(rows(i)("Id"))
            Dim dtWorkflowItems As DataTable = PermissionUtility.GetPermissionWorkflowItems(Me.CurrentUser.Code, CategoryId)
            If dtWorkflowItems.Rows.Count > 0 Then
                Response.Write("<tr height='20'><td class='biaoti1' colspan='3'>" & CStr(rows(i)("Descr")) & "</td></tr>" & vbCrLf)
                GenerateWorkflowItems(dtWorkflowItems, CategoryId)
            End If
            GenerateWorkflowCategories(CategoryId)
        Next
    End Sub

    Private Sub GenerateWorkflowItems(ByVal dtWorkflowItems As DataTable, ByVal CategoryId As Long)
        'Dim dtWorkflowItems As DataTable = WorkflowManager.GetWorkflowItems(CategoryId)
        'Dim dtWorkflowItems As DataTable = PermissionUtility.GetPermissionWorkflowItems(Me.CurrentUser.Code, CategoryId)
        For i As Integer = 0 To dtWorkflowItems.Rows.Count - 1
            Response.Write("<tr  height='20'>" & vbCrLf)
            For j As Integer = 1 To 3
                If i < dtWorkflowItems.Rows.Count Then
                    Response.Write("<td class='text1'> &nbsp;&nbsp;&nbsp;<img src='images/org_arrow.gif' align='absmiddle'>&nbsp;&nbsp;<input type='checkbox' value='" & CStr(dtWorkflowItems.Rows(i)("Id")) & "' name='chkWorkflowItemKey' " & CStr(IIf(oWorkflowShortcuts.ExitsShortcut(CStr(dtWorkflowItems.Rows(i)("Id"))), "checked", "")) & "><a href='../process/director.aspx?action=create&WorkflowId=" & CStr(dtWorkflowItems.Rows(i)("Id")) & "&url=2009/message.aspx'>" & CStr(dtWorkflowItems.Rows(i)("Name")) & "</td>" & vbCrLf)
                    If j < 3 Then i += 1
                Else
                    Response.Write("<td class='text1'>&nbsp;</td>" & vbCrLf)
                End If
            Next
            Response.Write("</tr>" & vbCrLf)
        Next
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strWorkflowKeys As String = Request.Form("chkWorkflowItemKey")
        strWorkflowKeys = strWorkflowKeys & ","
        Dim aryWorkflowKeys() As String = strWorkflowKeys.Split(Char.Parse(","))
        For i As Integer = 0 To aryWorkflowKeys.Length - 1
            If CStr(aryWorkflowKeys(i)) <> "" Then oWorkflowShortcuts.AddShortcut(CStr(aryWorkflowKeys(i)))
        Next
        oWorkflowShortcuts.Update()
        Response.Redirect("personalize.aspx")
    End Sub

End Class

End Namespace
