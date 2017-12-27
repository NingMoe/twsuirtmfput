Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables
Imports Unionsoft.Workflow.Platform

Public Class LoginFail
    Public intTimes As Integer = 0
    Public lngTime As Long = 0
End Class

Public Class DefaultBase
    Inherits AspPage

    '------------------------------------------------------------------
    '����ϵͳ��ǰ����ʲô״̬��������Ӧ�Ĵ������ʾ
    '------------------------------------------------------------------
    Protected Sub SystemValidate( _
        ByRef txtUserName As System.Web.UI.WebControls.TextBox, _
        ByRef txtPassword As System.Web.UI.WebControls.TextBox, _
        Optional ByRef panelDemo As System.Web.UI.WebControls.Panel = Nothing, _
        Optional ByRef lblCopyright As System.Web.UI.WebControls.Label = Nothing, _
        Optional ByRef lbtnCreateDb As System.Web.UI.WebControls.LinkButton = Nothing, _
        Optional ByVal lbtnRegister As System.Web.UI.WebControls.LinkButton = Nothing _
        )
        Try

            'ByRef drpPlatForm As System.Web.UI.WebControls.DropDownList, _
            'Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '��������ʱ�������ݿ�

            '----------------------------------------------------------------------
            '��δ��������ڴ˺�����λ
            If Not lbtnCreateDb Is Nothing Then lbtnCreateDb.Visible = False
            If Not lbtnRegister Is Nothing Then lbtnRegister.Visible = False

            If Not lblCopyright Is Nothing Then lblCopyright.Text = "COPYRIGHT&copy;2005 UNIONSOFT"
            If Not panelDemo Is Nothing Then
                If CmsConfig.NoCorpInfoShowed() Then
                    panelDemo.Visible = True
                    If Not lblCopyright Is Nothing Then lblCopyright.Visible = False
                Else
                    panelDemo.Visible = False
                    If Not lblCopyright Is Nothing Then lblCopyright.Visible = True
                End If
            End If
            '----------------------------------------------------------------------

            If IsLoginDisabled() Then
                'ָ��ʱ���ڣ�5���ӣ��Ѿ����ε�¼ʧ�ܣ����Ծܾ���¼
                txtUserName.Enabled = False
                txtPassword.Enabled = False
                Return
            End If

            '-------------------------------------------------------------
            '����ϵͳ���ݿ��Ƿ��Ѿ�����
            If CmsDatabase.IsDbInitialized(CmsDatabase.GetDbConfig()) = False Then
                If Not lbtnCreateDb Is Nothing Then
                    'Ϊ����sysuser�ܵ�¼
                    'txtUserName.Enabled = False
                    'txtPassword.Enabled = False

                    lbtnCreateDb.Text = "�봴�����ݿ�"
                    lbtnCreateDb.Visible = True

                    Dim strUserName As String = txtUserName.Text.Trim()
                    Dim strUserPass As String = txtPassword.Text.Trim()
                    If strUserName = "sysuser" Then '��ϵͳ�ڲ��û�
                        Dim strEnUserPass As String = CmsConfig.GetString("SYS_CONFIG", "IMPLEMENTOR_CODE")

                        Dim strDeUserPass As String =OrgFactory.EmpDriver.DecryptPass(strEnUserPass)
                        If strDeUserPass = strUserPass Then
                            Session("DEV_MANAGER") = "1"
                            Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
                            Return
                        Else
                            Return
                        End If
                    Else
                        Return
                    End If
                Else
                    txtUserName.Enabled = True
                    txtPassword.Enabled = True
                    'lbtnCreateDb.Visible = False
                End If
            End If
            '-------------------------------------------------------------

            If RStr("fromsrc") = "login" Then '�ǵ�¼����
                Login(txtUserName.Text.Trim(), txtPassword.Text.Trim())
                'Else
                '    Session.Abandon()
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '------------------------------------------------------------------
    'ָ��ʱ���ڣ�5���ӣ��Ѿ����ε�¼ʧ�ܣ����Ծܾ���¼
    '------------------------------------------------------------------
    Protected Function IsLoginDisabled() As Boolean
        Try
            '���顰��¼ʧ�ܴ������͡���¼ʧ��ʱ�䡱
            If Session("CMS_LOGINFAIL_RECORD") Is Nothing Then Return False
            Dim logf As LoginFail = CType(Session("CMS_LOGINFAIL_RECORD"), LoginFail)
            If logf.intTimes >= CmsConfig.GetInt("SYS_CONFIG", "FAILLOGIN_LOCK_TIMES") And (TimeId.CurrentMilliseconds(1) - logf.lngTime) < CmsConfig.GetInt("SYS_CONFIG", "FAILLOGIN_LOCK_TIME") * 1000 Then
                logf.lngTime = TimeId.CurrentMilliseconds(1)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            SLog.Err("�����¼ʧ�ܴ����쳣ʧ�ܣ�", ex)
            Return False
        End Try
    End Function

    '------------------------------------------------------------------
    '��ʼ��¼
    '------------------------------------------------------------------
    Private Sub Login(ByVal strUserName As String, ByVal strUserPass As String)
        Dim blnLoginSuccess As Boolean = False

        Try
            Session("CMS_PASSPORT") = Nothing
            If Not OnlineUser.IsAvailableUser(strUserName, Request.UserHostAddress) Then
                PromptMsg("���û�����������½�������½����ϵ����Ա��")
                Return
            End If
            Dim pst As CmsPassport = CmsPassport.GenerateCmsPassport(strUserName, strUserPass, Request.UserHostAddress)
            If Not (pst Is Nothing) Then
                '��¼�ɹ��������ʧ�ܴ���
                Session("CMS_LOGINFAIL_RECORD") = Nothing
                'Session.Abandon()

                Session("CMS_PASSPORT") = pst
                '����ǰ�û����������û��б�
                OnlineUser.AddOnlineUser(strUserName, Request.UserHostAddress)
                CmsLog.Save(pst, LogTitle.Login, "")

                blnLoginSuccess = True
                If pst.Employee.ID.ToLower() = "sysuser" Then '��ϵͳ�ڲ��û�
                    Session("DEV_MANAGER") = "1"
                    Response.Redirect("/cmsweb/cmsdev/DevMain.aspx", False)
                    Return
                ElseIf pst.Employee.ID.ToLower() = "admin" Then '��ϵͳ����Ա
                    Response.Redirect("/cmsweb/adminres/ResourceFrame.aspx?timeid=" & TimeId.CurrentMilliseconds(1), False)
                    'Response.Redirect("/cmsweb/cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminres/ResourceFrameBody.aspx&timeid=" & TimeId.CurrentMilliseconds(1), False)
                    Return
                ElseIf pst.EmpIsSysSecurity Then  '��ϵͳ��ȫԱ
                    'Response.Redirect("/cmsweb/adminsys/SysLogFrame.aspx?timeid=" & TimeId.CurrentMilliseconds(1), False)
                    Response.Redirect("/cmsweb/cmshost/CmsFrame.aspx?cmsbodypage=/cmsweb/adminsys/SysLogManager.aspx&timeid=" & TimeId.CurrentMilliseconds(1), False)
                    Return
                Else '����ͨ��Ա
                    Dim strFlowEntryUrl As String = CmsConfig.GetString("SYS_CONFIG", "LOGIONPAGE")
                    If strFlowEntryUrl <> "" Then
                        If strFlowEntryUrl.ToLower() = "pageweb" Then
                            Response.Redirect("/pageweb/SessionAgent.aspx?UserName=" & strUserName & "&UserPass=" & strUserPass & "&Host=" & Request.UserHostAddress & "")
                            Return
                        Else
                            Dim Emp As New Employee
                            Emp.Code = strUserName
                            Emp.Name = pst.Employee.Name
                            Emp.Password = pst.Employee.Password
                            Emp.LocalDepartCode = CStr(pst.Employee.DepartmentId)
                            Session.Item("CurrentEmployee") = Emp
                            Response.Redirect(strFlowEntryUrl & "?timeid=" & TimeId.CurrentMilliseconds(1), False)
                            Return
                        End If
                    Else
                        Response.Redirect("/cmsweb/cmshost/CmsFrame.aspx?timeid=" & TimeId.CurrentMilliseconds(1), False)
                    End If
                    Return
                    'End If
                End If
            End If
        Catch ex As Exception
            PromptMsg(ex.Message)
        End Try

        '��¼����¼ʧ�ܴ������͡���¼ʧ��ʱ�䡱
        If blnLoginSuccess = False Then
            If Session("CMS_LOGINFAIL_RECORD") Is Nothing Then
                Dim logf As New LoginFail
                logf.intTimes = 1
                logf.lngTime = TimeId.CurrentMilliseconds(1)
                Session("CMS_LOGINFAIL_RECORD") = logf
            Else
                Dim logf As LoginFail = CType(Session("CMS_LOGINFAIL_RECORD"), LoginFail)
                logf.intTimes += 1
                logf.lngTime = TimeId.CurrentMilliseconds(1)
                Session("CMS_LOGINFAIL_RECORD") = logf
            End If
        End If
    End Sub
End Class
