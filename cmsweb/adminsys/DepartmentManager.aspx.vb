Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DepartmentManager
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnAddDep As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtDepNameToModify As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnModifyDepName As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtDepNameToDel As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDelDep As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnMoveDep As System.Web.UI.WebControls.LinkButton

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

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
            If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then  '只有系统管理员能进入部门管理
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim lngDepID As Long = RLng("depid")
        '下行必须放在IsPostBack判断之后
        If RStr("depempcmd") = "selected_depemp" Then '从选择部门人员页面回来
            '设置部门管理员
            Dim lngAiid As Long = RLng("empaiid")
            Dim strAdminID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, lngAiid)
            OrgFactory.DepDriver.SetDepAdmin(CmsPass, lngDepID, strAdminID)
        End If

        '初始选择根节点（企业简称），在“修改部门”输入框中应该显示企业简称
        txtDepNameToEdit.Text = OrgFactory.DepDriver.GetDepName(CmsPass, lngDepID)
            chkShowEnable.Checked = OrgFactory.DepDriver.IsShowEnable(CmsPass, lngDepID)

           

            ShowDepAttribute(lngDepID)
        End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnDelDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDelDep.Click
        Dim lngDepID As Long = RLng("depid")
        If lngDepID = 0 Then
            PromptMsg("不能删除根节点！")
            Return
        End If

        Try
            '获取将删除节点的父节点以便删除操作后选中删除节点的父节点
            Dim lngParentDepID As Long = OrgFactory.DepDriver.GetParentDepID(CmsPass, lngDepID)

            '更新数据库
            OrgFactory.DepDriver.DelDepartment(CmsPass, lngDepID)

            '用GET返回当前页面
            Response.Redirect("/cmsweb/adminsys/DepartmentManager.aspx?depid=" & lngParentDepID, True)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnDepMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDepMoveup.Click
        MoveDepUpDown(True)
    End Sub

    Private Sub lbtnDepMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDepMovedown.Click
        MoveDepUpDown(False)
    End Sub

    Private Sub btnDepAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDepAdd.Click
        Try
            If txtDepName.Text.Trim() = "" Then
                PromptMsg("请先输入需要增加的部门名称")
                Return
            End If

            Dim lngDepID As Long = RLng("depid")
            If OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngDepID) Then
                PromptMsg("虚拟部门下不能建立子部门！")
                Return
            End If

            '-------------------------------------------
            '组织单个部门的数据结构，保存如数据库
            Dim datDep As DataDepartment
            datDep.lngPID = lngDepID
            datDep.strNAME = txtDepName.Text.Trim()
            If chkVirtualDep.Checked Then
                datDep.lngDEP_TYPE = 1
                datDep.strICON_NAME = "ICON_DEP_VIRTUAL"
            Else
                datDep.lngDEP_TYPE = 0
                datDep.strICON_NAME = "ICON_DEP_REAL"
            End If
            datDep.strDEP_COMMENTS = ""
            datDep.strDEP_DB_URL = ""
            datDep.strDEP_ADMIN_ID = ""
            datDep.strDEP_DB_URL = ""
            OrgFactory.DepDriver.AddDepartment(CmsPass, datDep)
            '-------------------------------------------

            '置空
            txtDepName.Text = ""
            chkVirtualDep.Checked = False
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnDepEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDepEdit.Click
        Try
            '更新数据库
            Dim lngDepID As Long = RLng("depid")
            OrgFactory.DepDriver.EditDepProperties(CmsPass, lngDepID, txtDepNameToEdit.Text.Trim(), Not chkShowEnable.Checked)

            ShowDepAttribute(lngDepID)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnDepMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDepMove.Click
        Dim lngDepID As Long = RLng("depid")
        If lngDepID = 0 Then
            PromptMsg("不能移动企业名称！")
            Return
        End If

        Session("CMSBP_DepartmentMove") = "/cmsweb/adminsys/DepartmentManager.aspx?depid=" & lngDepID
        Response.Redirect("/cmsweb/adminsys/DepartmentMove.aspx?tmpdepid=" & lngDepID, False)
    End Sub

    Private Sub btnSetDepAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDepAdmin.Click
        Dim lngDepID As Long = RLng("depid")
        Session("CMSBP_DepEmpList") = "/cmsweb/adminsys/DepartmentManager.aspx?depid=" & lngDepID
        Response.Redirect("/cmsweb/adminsys/DepEmpList.aspx?nodep=yes&depid=" & lngDepID, False)
    End Sub

    Private Sub btnDelDepAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelDepAdmin.Click
        Try
            Dim lngDepID As Long = RLng("depid")
            OrgFactory.DepDriver.DelDepAdmin(CmsPass, lngDepID)
            ShowDepAttribute(lngDepID)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '-------------------------------------------------------
    '显示部门属性信息
    '-------------------------------------------------------
        Private Sub ShowDepAttribute(ByVal lngDepID As Long)
            Dim IsDepAdmin As Boolean = False

            If CmsPass.EmpIsDepAdmin = True And OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID, True).Trim = CmsPass.Employee.ID.Trim Then
                IsDepAdmin = True
            ElseIf CmsPass.EmpIsSysAdmin Then
                IsDepAdmin = True
            End If

            '显示管理员信息
            Dim strAdminID As String = OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID)
            If strAdminID = "" Then
                lblDepAdminID.Text = "(无)"
            Else
                lblDepAdminID.Text = OrgFactory.EmpDriver.GetEmpName(CmsPass, strAdminID) & " (" & strAdminID & ")"
            End If

            '显示部门类型
            If OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngDepID) = True Then
                lblDepType.Text = "虚拟部门"
            Else
                lblDepType.Text = "真实部门"
            End If

            If lngDepID = 0 Then '企业上禁止设置部门管理员
                btnSetDepAdmin.Enabled = False
                btnDelDepAdmin.Enabled = False

            Else
                btnSetDepAdmin.Enabled = True
                btnDelDepAdmin.Enabled = True
            End If

            If CmsPass.EmpIsDepAdmin Then
                lbtnDepMovedown.Enabled = False
                lbtnDepMoveup.Enabled = False 
 
                If IsDepAdmin = False Or OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID).Trim = CmsPass.Employee.ID.Trim Then
                    btnDepEdit.Enabled = False
                    btnSetDepAdmin.Enabled = False
                    btnDelDepAdmin.Enabled = False

                    btnSetDepAdmin.Enabled = False
                    btnDelDepAdmin.Enabled = False

                    lbtnDelDep.Enabled = False
                    If IsDepAdmin = False Then btnDepAdd.Enabled = False
                End If
            End If


            If CmsConfig.GetString("DomainServer", "Authentication").ToLower = "true" Then
                Me.ADSetup.Visible = True
                Me.TextAD_OU.Value = SDbStatement.Query("select AD_OU from CMS_DEPARTMENT where id=" + lngDepID.ToString).Tables(0).Rows(0)(0).ToString & ""
            Else
                Me.ADSetup.Visible = False

            End If
            '复位信息
            txtDepName.Text = ""
            ' chkVirtualDep.Checked = False
        End Sub

    '-------------------------------------------------------
    '向上或向下移动部门
    '-------------------------------------------------------
    Private Sub MoveDepUpDown(ByVal blnIsMoveup As Boolean)
        Dim lngDepID As Long = RLng("depid")
        If lngDepID = 0 Then
            PromptMsg("不能移动企业名称！")
            Return
        End If

        Try
            If blnIsMoveup Then
                OrgFactory.DepDriver.MoveUp(CmsPass, lngDepID)
            Else
                OrgFactory.DepDriver.MoveDown(CmsPass, lngDepID)
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

        Protected Sub BtnSaveAD_OU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSaveAD_OU.Click
            Dim lngDepID As Long = RLng("depid")
            If lngDepID = 0 Then
                PromptMsg("不是部门！")
                Return
            End If
            SDbStatement.Execute("update " & CmsTables.Department & " set AD_OU='" & Me.TextAD_OU.Value & "' where ID=" & lngDepID)
        End Sub
    End Class

End Namespace
