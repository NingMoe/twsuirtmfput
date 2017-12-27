Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DepartmentManager
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnAddDep As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtDepNameToModify As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnModifyDepName As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtDepNameToDel As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDelDep As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnMoveDep As System.Web.UI.WebControls.LinkButton

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
            If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then  'ֻ��ϵͳ����Ա�ܽ��벿�Ź���
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim lngDepID As Long = RLng("depid")
        '���б������IsPostBack�ж�֮��
        If RStr("depempcmd") = "selected_depemp" Then '��ѡ������Աҳ�����
            '���ò��Ź���Ա
            Dim lngAiid As Long = RLng("empaiid")
            Dim strAdminID As String = OrgFactory.EmpDriver.GetEmpID(CmsPass, lngAiid)
            OrgFactory.DepDriver.SetDepAdmin(CmsPass, lngDepID, strAdminID)
        End If

        '��ʼѡ����ڵ㣨��ҵ��ƣ����ڡ��޸Ĳ��š��������Ӧ����ʾ��ҵ���
        txtDepNameToEdit.Text = OrgFactory.DepDriver.GetDepName(CmsPass, lngDepID)
            chkShowEnable.Checked = OrgFactory.DepDriver.IsShowEnable(CmsPass, lngDepID)

           

            ShowDepAttribute(lngDepID)
        End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnDelDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDelDep.Click
        Dim lngDepID As Long = RLng("depid")
        If lngDepID = 0 Then
            PromptMsg("����ɾ�����ڵ㣡")
            Return
        End If

        Try
            '��ȡ��ɾ���ڵ�ĸ��ڵ��Ա�ɾ��������ѡ��ɾ���ڵ�ĸ��ڵ�
            Dim lngParentDepID As Long = OrgFactory.DepDriver.GetParentDepID(CmsPass, lngDepID)

            '�������ݿ�
            OrgFactory.DepDriver.DelDepartment(CmsPass, lngDepID)

            '��GET���ص�ǰҳ��
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
                PromptMsg("����������Ҫ���ӵĲ�������")
                Return
            End If

            Dim lngDepID As Long = RLng("depid")
            If OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngDepID) Then
                PromptMsg("���ⲿ���²��ܽ����Ӳ��ţ�")
                Return
            End If

            '-------------------------------------------
            '��֯�������ŵ����ݽṹ�����������ݿ�
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

            '�ÿ�
            txtDepName.Text = ""
            chkVirtualDep.Checked = False
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnDepEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDepEdit.Click
        Try
            '�������ݿ�
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
            PromptMsg("�����ƶ���ҵ���ƣ�")
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
    '��ʾ����������Ϣ
    '-------------------------------------------------------
        Private Sub ShowDepAttribute(ByVal lngDepID As Long)
            Dim IsDepAdmin As Boolean = False

            If CmsPass.EmpIsDepAdmin = True And OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID, True).Trim = CmsPass.Employee.ID.Trim Then
                IsDepAdmin = True
            ElseIf CmsPass.EmpIsSysAdmin Then
                IsDepAdmin = True
            End If

            '��ʾ����Ա��Ϣ
            Dim strAdminID As String = OrgFactory.DepDriver.GetDepAdmin(CmsPass, lngDepID)
            If strAdminID = "" Then
                lblDepAdminID.Text = "(��)"
            Else
                lblDepAdminID.Text = OrgFactory.EmpDriver.GetEmpName(CmsPass, strAdminID) & " (" & strAdminID & ")"
            End If

            '��ʾ��������
            If OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngDepID) = True Then
                lblDepType.Text = "���ⲿ��"
            Else
                lblDepType.Text = "��ʵ����"
            End If

            If lngDepID = 0 Then '��ҵ�Ͻ�ֹ���ò��Ź���Ա
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
            '��λ��Ϣ
            txtDepName.Text = ""
            ' chkVirtualDep.Checked = False
        End Sub

    '-------------------------------------------------------
    '���ϻ������ƶ�����
    '-------------------------------------------------------
    Private Sub MoveDepUpDown(ByVal blnIsMoveup As Boolean)
        Dim lngDepID As Long = RLng("depid")
        If lngDepID = 0 Then
            PromptMsg("�����ƶ���ҵ���ƣ�")
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
                PromptMsg("���ǲ��ţ�")
                Return
            End If
            SDbStatement.Execute("update " & CmsTables.Department & " set AD_OU='" & Me.TextAD_OU.Value & "' where ID=" & lngDepID)
        End Sub
    End Class

End Namespace
