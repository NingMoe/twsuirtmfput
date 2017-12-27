Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DevAppConfig
    Inherits AspPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            PageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�
            PageInitialize() '��ʼ��ҳ��

            If Not IsPostBack Then
                PageDealFirstRequest() '�����һ��GET�����е�����
            Else
                PageDealPostBack() '����POST�е�������أ�True���˳����ӿں�ֱ���˳����壻False���˳����ӿں����֮��Ĵ���
            End If
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------
    '������Ĳ�������ΪViewState������������ҳ������ȡ���޸ġ�
    '--------------------------------------------------------------------------
    Private Sub PageSaveParametersToViewState()
        If VStr("PAGE_ISFROM") = "" Then
            ViewState("PAGE_ISFROM") = RStr("isfrom")
        End If

        If VStr("PAGE_CONFIGFILE") = "" Then
            ViewState("PAGE_CONFIGFILE") = CmsConfig.ProjectRootFolder & "conf\" & RStr("conffile")
        End If
    End Sub

    '--------------------------------------------------------------------------
    '��ʼ��ҳ��
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        If VStr("PAGE_ISFROM") = "admin" Then
            If Session("CMS_PASSPORT") Is Nothing Then
                Response.Redirect("/cmsweb/cmsdev/DevLogin.aspx", False)
                Return
            End If
        ElseIf VStr("PAGE_ISFROM") = "sysuser" Then  'У���Ƿ���ȷ��¼
            If SStr("DEV_MANAGER") <> "1" Then
                Response.Redirect("/cmsweb/cmsdev/DevLogin.aspx", False)
                Return
            End If
        End If
    End Sub

    '--------------------------------------------------------------------------
    '�����һ��GET�����е�����
    '--------------------------------------------------------------------------
    Private Sub PageDealFirstRequest()
        CmsConfig.ReloadAll()

        Dim alistTexts As New ArrayList
        Dim alistChecks As New ArrayList

        Dim i As Integer = 0

        ''----------------------------------------------------------------------------------------------
        ''��ʾSection˵��
        'Dim strTitle As String
        'Dim strConfigFile As String = VStr("PAGE_CONFIGFILE").ToLower()
        'If strConfigFile.IndexOf("app_function.xml") >= 0 Then
        '    strTitle = "��Ҫ˵�����������������к�ɫ�����ʾ��һ���Ʒ��Ӧ�ùرյĹ��ܣ���ɫ�����ʾ��Ҫ��������Ĺ���ģ�飡"
        'Else
        '    strTitle = "��Ҫ˵��������������Ϣ�к�ɫ�����ʾ��ϵͳ����ʱ������Ҫ�Ķ���������Ϣ��"
        'End If
        'Dim lblTitle As New System.Web.UI.WebControls.Label
        'lblTitle.ID = "lblTitle"
        'lblTitle.Font.Bold = True
        'lblTitle.ForeColor = Color.FromName("red")
        'lblTitle.Text = strTitle
        'lblTitle.EnableViewState = True
        'Dim strStyle As String = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 10 & "px"
        'lblTitle.Attributes.Add("style", strStyle)
        'Panel1.Controls.Add(lblTitle)
        'i += 1
        ''----------------------------------------------------------------------------------------------

        Dim datSvc As New DataServiceSection(VStr("PAGE_CONFIGFILE"))
        Dim alistSections As ArrayList = datSvc.GetSections()
        Dim strStyle As String = ""
        Dim strSec As String
        For Each strSec In alistSections
            Dim strEnable As String = datSvc.GetSecAttr(strSec, "SHOWENABLE")
            If strEnable <> "0" AndAlso VStr("PAGE_ISFROM") = "admin" Then
                strEnable = datSvc.GetSecAttr(strSec, "SHOWFORADMIN")
            End If
            If strEnable <> "0" Then 'ֻ��ʾ��֧�ֵĹ���
                Dim alistFuncs As ArrayList = datSvc.GetKeys(strSec)

                '��ʾSection˵��
                Dim lblSection As New System.Web.UI.WebControls.Label
                lblSection.ID = "lblSection" & strSec
                lblSection.Font.Bold = True
                lblSection.ForeColor = Color.FromName("blue")
                lblSection.Text = datSvc.GetSecAttr(strSec, "DESC")
                lblSection.EnableViewState = True
                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 10 & "px"
                lblSection.Attributes.Add("style", strStyle)
                Panel1.Controls.Add(lblSection)
                i += 1

                Dim strKey As String
                For Each strKey In alistFuncs
                    If strKey = "FUNC_SEP" Then
                        i += 1
                    Else
                        Dim strDesc As String = datSvc.GetKeyAttr(strSec, strKey, "DESC")
                        If strDesc = "" Then strDesc = "(δ֪)"
                        Dim strEnable2 As String = datSvc.GetKeyAttr(strSec, strKey, "SHOWENABLE")
                        If strEnable2 <> "0" AndAlso VStr("PAGE_ISFROM") = "admin" Then
                            strEnable2 = datSvc.GetKeyAttr(strSec, strKey, "SHOWFORADMIN")
                        End If
                        If strEnable2 <> "0" Then 'ֻ��ʾ��֧�ֵĹ���
                            If datSvc.GetKeyAttr(strSec, strKey, "ISSWITCH") = "1" Then
                                '����CheckBox
                                Dim intEnable As Integer = datSvc.GetInt(strSec, strKey)

                                Dim chk As New System.Web.UI.WebControls.CheckBox
                                chk.ID = strSec & "___" & strKey
                                chk.Text = strDesc
                                chk.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 30 & "px"
                                chk.Attributes.Add("style", strStyle)
                                If intEnable = 1 Then 'Ĭ���Ѵ�
                                    chk.Checked = True
                                Else 'Ĭ�Ϲر�
                                    chk.Checked = False
                                End If
                                Dim strColor As String = datSvc.GetKeyAttr(strSec, strKey, "DESCCOLOR")
                                If strColor <> "" Then chk.ForeColor = Color.FromName(strColor)
                                Panel1.Controls.Add(chk)
                                alistChecks.Add(chk.ID)
                            Else
                                '����TextBox��Label
                                Dim strText As String = datSvc.GetString(strSec, strKey)

                                Dim txt As New System.Web.UI.WebControls.TextBox
                                txt.ID = strSec & "___" & strKey
                                txt.Text = strText
                                txt.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 30 & "px; width: 350px"
                                txt.Attributes.Add("style", strStyle)
                                Panel1.Controls.Add(txt)
                                alistTexts.Add(txt.ID)

                                Dim lbl As New System.Web.UI.WebControls.Label
                                lbl.ID = "lbl" & strSec & "___" & strKey
                                lbl.Text = strDesc
                                lbl.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 386 & "px"
                                lbl.Attributes.Add("style", strStyle)
                                Dim strColor As String = datSvc.GetKeyAttr(strSec, strKey, "DESCCOLOR")
                                If strColor <> "" Then lbl.ForeColor = Color.FromName(strColor)
                                Panel1.Controls.Add(lbl)
                            End If

                            i += 1
                        End If
                    End If
                Next

                i += 1
            End If
        Next

        Panel1.Width = Unit.Pixel(900)
        Panel1.Height = Unit.Pixel(i * 24 + 10)

        ViewState("PAGE_TEXTS") = alistTexts
        ViewState("PAGE_CHECKS") = alistChecks
    End Sub

    '--------------------------------------------------------------------------
    '����POST�е�������أ�True���˳����ӿں�ֱ���˳����壻False���˳����ӿں����֮��Ĵ���
    '--------------------------------------------------------------------------
    Private Function PageDealPostBack() As Boolean
    End Function

    Private Sub lbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSave.Click
        Try
            Dim alistTexts As ArrayList = CType(ViewState("PAGE_TEXTS"), ArrayList)
            Dim alistChecks As ArrayList = CType(ViewState("PAGE_CHECKS"), ArrayList)

            Dim datSvc As New DataServiceSection(VStr("PAGE_CONFIGFILE"))

            '���湦��������Ϣ
            Dim strCtrl As String
            For Each strCtrl In alistChecks
                Dim strVal As String = RStr(strCtrl)
                Dim pos As Integer = strCtrl.IndexOf("___")
                Dim strSec As String = strCtrl.Substring(0, pos)
                Dim strKey As String = strCtrl.Substring(pos + 3)
                If strVal.ToLower.Trim() = "on" Then
                    datSvc.SetInt(strSec, strKey, 1)
                Else
                    datSvc.SetInt(strSec, strKey, 0)
                End If
            Next
            For Each strCtrl In alistTexts
                Dim strVal As String = RStr(strCtrl)
                Dim pos As Integer = strCtrl.IndexOf("___")
                Dim strSec As String = strCtrl.Substring(0, pos)
                Dim strKey As String = strCtrl.Substring(pos + 3)
                datSvc.SetString(strSec, strKey, strVal)
            Next

            '����Load���й���������Ϣ
            PageDealFirstRequest()
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lbtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnExit.Click
        Response.Redirect(RStr("backpage"), False)
    End Sub
End Class

End Namespace
