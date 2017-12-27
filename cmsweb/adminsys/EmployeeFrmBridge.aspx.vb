Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmBridge
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
        '获取部门ID
        Dim lngDepID As Long = RLng("depid")
        Dim blnIsVirtualDep As Boolean = OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngDepID)
            If blnIsVirtualDep Then
                'Dim blnHasEmp As Boolean = OrgFactory.EmpDriver.HasEmployeeUnderDep(CmsPass, lngDepID)
                'If blnHasEmp Then
                '    '虚拟部门下有人员帐号，为了与原系统版本兼容
                '    Response.Redirect("/cmsweb/adminsys/EmployeeFrmContent.aspx?depid=" & lngDepID, False)
                'Else
                '    '虚拟部门下没有人员帐号，直接进入新系统的虚拟部门人员管理界面
                '    Response.Redirect("/cmsweb/adminsys/EmployeeFrmVirtualDep.aspx?depid=" & lngDepID, False)
                'End If
                Response.Redirect("/cmsweb/adminsys/EmployeeFrmVirtualDep.aspx?depid=" & lngDepID, False)
            Else
                Response.Redirect("/cmsweb/adminsys/EmployeeFrmContent.aspx?depid=" & lngDepID, False)
            End If
    End Sub
End Class

End Namespace
