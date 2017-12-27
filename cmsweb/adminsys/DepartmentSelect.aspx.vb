Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DepartmentSelect
    Inherits CmsPage

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
    End Sub

    Private Sub btnSelectDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDep.Click
        Dim lngSelDepID As Long = RLng("depid")
        If lngSelDepID = 0 Then
            PromptMsg("请选择合适的部门！")
            Return
        End If

        Try
            If OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngSelDepID) = True Then
                PromptMsg("不能移动人员至虚拟部门中！")
                Return
            End If

            'Dim employee As DataCmsEmployee = OrgFactory.EmpDriver.GetEmployee(CLng(Request.QueryString("employeeid")))
            'employee.DepartmentId = lngSelDepID
            'OrgFactory.EmpDriver.UpdateEmployee(employee)

                OrgFactory.EmpDriver.ChangeDepartment(CmsPass, Request.QueryString("employeeid"), lngSelDepID)

            Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "cmsaction=seldep&tmpdepid=" & lngSelDepID), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
