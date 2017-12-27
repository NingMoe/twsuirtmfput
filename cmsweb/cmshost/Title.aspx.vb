Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class Title
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSep2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSep3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSep4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSep5 As System.Web.UI.WebControls.Label
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblSep6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSep1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnBusiness As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lblSep7 As System.Web.UI.WebControls.Label
    'Protected WithEvents lnkFavorite As System.Web.UI.WebControls.HyperLink
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

    Protected Overrides Sub CmsPageInitialize()
        If Session("CMS_PASSPORT") Is Nothing Then
            Response.Redirect("/cmsweb/Logout.aspx", True)
            Return
        End If

        '显示登录用户
            lblEmpName.Text = CmsPass.Employee.ID & "&nbsp;/&nbsp;" & CmsPass.Employee.Name
            lblEmpName1.Text = CmsPass.Employee.ID & "&nbsp;/&nbsp;" & CmsPass.Employee.Name

        '内容管理
            'lnkContent.Text = CmsMessage.GetUI(CmsPass, "TITLE_CMS")
            'If CmsPass.Employee.Type = EmployeeType.SysAdmin AndAlso CmsPass.EmpIsSysSecurity = False Then
            '    lnkContent.Visible = True
            '    lnkContent.NavigateUrl = "/cmsweb/cmshost/CmsFrmBody.aspx?timeid=" & TimeId.CurrentMilliseconds(1)
            '    lnkContent.Target = "cmsbody"
            'Else
            '    lnkContent.Visible = False
            'End If
 
            If CmsConfig.GetBool("SYS_CONFIG", "EMPLOYYESYSTEM") Then
                Me.tdSystem.Visible = False
                Me.tdEmployye.Visible = True

                lnkDomain.Text = CmsMessage.GetUI(CmsPass, "TITLE_MENU_DOMAIN")
                lnkDept.Text = CmsMessage.GetUI(CmsPass, "TITLE_MENU_DEP")
                lnkEmp.Text = CmsMessage.GetUI(CmsPass, "TITLE_MENU_EMP")
            Else
                Me.tdSystem.Visible = True
                Me.tdEmployye.Visible = False
                lnkWorkflow.Visible = False
                lnkWorkflow.Text = CmsMessage.GetUI(CmsPass, "TITLE_WORKFLOW")
                If CmsPass.Employee.Type <> EmployeeType.SysAdmin AndAlso CmsPass.EmpIsSysSecurity = False Then
                    If CmsFunc.IsEnable("FUNC_WORKFLOW") Then
                        lnkWorkflow.Visible = True
                        lnkWorkflow.NavigateUrl = "../workflow_directservice.aspx"
                    End If
                End If

                '系统功能
                lnkSysFunc.Text = CmsMessage.GetUI(CmsPass, "TITLE_SYSTEM_FUNC")
                If CmsPass.EmpIsSysSecurity = False Then
                    lnkSysFunc.Visible = True
                    lnkSysFunc.NavigateUrl = "#"
                    lnkSysFunc.Attributes.Add("onClick", "showSysFuncMenu(this)")
                Else
                    lnkSysFunc.Visible = False
                End If
                lnkHome.Text = CmsMessage.GetUI(CmsPass, "TITLE_HOME")


                '系统管理
                lnkSystem.Text = CmsMessage.GetUI(CmsPass, "TITLE_SYSTEM")
                If CmsPass.Employee.Type = EmployeeType.SysAdmin OrElse CmsPass.EmpIsSysSecurity OrElse CmsPass.EmpIsDepAdmin Then
                    lnkSystem.Visible = True
                    'lblSep2.Visible = True
                    lnkSystem.NavigateUrl = "#"
                    lnkSystem.Attributes.Add("onClick", "showSysMenu(this)")
                Else
                    lnkSystem.Visible = False
                    'lblSep2.Visible = False
                End If

                '工具
                lnkTools.Text = CmsMessage.GetUI(CmsPass, "TITLE_TOOLS")
                lnkTools.NavigateUrl = "#"
                lnkTools.Attributes.Add("onClick", "showToolsMenu(this)")
                'lblSep3.Visible = True

                '帮助
                lnkHelp.Text = CmsMessage.GetUI(CmsPass, "TITLE_HELP")
                lnkHelp.NavigateUrl = "#"
                lnkHelp.Attributes.Add("onClick", "showHelpMenu(this)")
                'lblSep4.Visible = True
            End If

            '退出登录
            lnkExit.Text = CmsMessage.GetUI(CmsPass, "TITLE_EXIT")
            lnkExit.Visible = True
            lnkExit1.Text = CmsMessage.GetUI(CmsPass, "TITLE_EXIT")
            'lnkExit.NavigateUrl = "/cmsweb/Logout.aspx"
            ' lnkExit.Target = "_top"
            'lblSep5.Visible = True
        End Sub


    Private Sub btn_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Exit.Click
        OnlineUser.DeleteOnlineUser(CmsPass.Employee.ID)
        Response.Redirect("/cmsweb/Logout.aspx")

    End Sub
End Class

End Namespace
