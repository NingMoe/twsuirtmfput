Option Strict On
Option Explicit On 

Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Diagnostics
Imports System.Threading
Imports NetReusables
Imports Unionsoft.Platform
'Imports SMLibrary
Imports System.Text.RegularExpressions


Namespace Unionsoft.Cms.Web


Partial Class ResMailSend
    Inherits CmsPage

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

    Dim CommColEmail As String = ""
    Dim dtPerson As DataTable = New DataTable
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�ڴ˴����ó�ʼ��ҳ���û�����
        Dim res As DataResource = CmsPass.GetDataRes(RLng("mnuresid"))
        CommColEmail = res.CommColEmail
        dtPerson = BindData().Tables(0)
    End Sub

    Private Function BindData() As DataSet
        Dim strWhere As String = SStr("CMS_HOSTTABLE_WHERE")
        strWhere = CmsWhere.AppendAnd(strWhere, "")
        If Not Request("selectedrecid") Is Nothing Then
            If Request("selectedrecid").ToString() <> "" Then

                strWhere = "  Id in(" & Request("selectedrecid").ToString().Trim(CChar(",")) & ")"

            End If
        End If
        Dim strOrderBy As String = SStr("CMS_HOSTTABLE_ORDERBY")
        Dim strMoreTables As String = SStr("CMS_HOSTTABLE_MORETABLES")
        Dim ds As DataSet = ResFactory.TableService(CmsPass.HostResData.ResTableType).GetHostTableData(CmsPass, CmsPass.HostResData.ResID, strWhere, strOrderBy, , , , , strMoreTables)
        Return ds
    End Function

    Protected Overrides Sub CmsPageDealFirstRequest()
        Panel1.Visible = False
        Panel2.Visible = False
        '�����ռ��˵�ַ�ֶ��б�
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpRecipient, True, True, True, , True, True)

        '�������ռ��������ֶ��б�
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpRecipientName, True, True, True, , True, True)

        '���عؼ����滻
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey1, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey2, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey3, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey4, True, True, True, , True, True)
        WebUtilities.LoadResColumnsInDdlist(CmsPass, CmsPass.HostResData.ResID, drpKey5, True, True, True, , True, True)

        LoadConfig() '����������Ϣ

        'SMLibrary.LoadConfig.SetDb = CmsPass.Dbc '��������Դ
    End Sub

  
    Private Sub SendMail()
            Dim MailBody As String = ""
            Dim strSubject As String = txtSubject.Text                   '�ʼ�����
            If (chkIsKey.Checked = False) Then
                MailBody = txtBody.Text                 '�ʼ�����
            End If
            Dim strSmtpServer As String = CmsConfig.GetString("MailServer", "SmtpServer")
            Dim strUserName As String = CmsConfig.GetString("MailServer", "User")
            Dim strUserPass As String = CmsConfig.GetString("MailServer", "Password")
            Dim strEmailFrom As String = CmsConfig.GetString("MailServer", "SendFrom")

            '�ʼ�д�����
        Try
            For i As Int32 = 0 To dtPerson.Rows.Count - 1
                If (chkIsKey.Checked) Then '�ʼ�����
                    MailBody = GetMailBody(dtPerson.Rows(i))
                End If
                Dim strTo As String = DbField.GetStr(dtPerson.Rows(i), CommColEmail) '�ռ��˵�ַ
                Try
                    NetReusables.SimpleEmail.SendEmail(strEmailFrom, strTo, "", strSubject, MailBody, strSmtpServer, strUserName, strUserPass)
                Catch ex As Exception
                    SLog.Err("�ʼ�����ʧ�ܣ�ʧ�����䣺" & strTo)
                End Try
            Next
        Catch ex As Exception
            Throw ex.InnerException
        End Try
    End Sub
    '��ʼ�����ʼ�
    Private Sub bntSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSend.Click
        Dim workThread As Thread = New Thread(AddressOf SendMail)
        workThread.Start()
        Response.Write("�ʼ����ͳɹ�!")
    End Sub

    '��ȡ�ʼ�����
    Public Function GetMailBody(ByVal drv As DataRow) As String
        Try
            Dim tmpBody As String = txtBody.Text
            If (drpKey1.SelectedIndex <> 0 Or txtKey1.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey1.Text.Trim, DbField.GetStr(drv, drpKey1.SelectedValue))
            End If

            If (drpKey2.SelectedIndex <> 0 Or txtKey2.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey2.Text.Trim, DbField.GetStr(drv, drpKey2.SelectedValue))
            End If

            If (drpKey3.SelectedIndex <> 0 Or txtKey3.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey3.Text.Trim, DbField.GetStr(drv, drpKey3.SelectedValue))
            End If

            If (drpKey4.SelectedIndex <> 0 Or txtKey4.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey4.Text.Trim, DbField.GetStr(drv, drpKey4.SelectedValue))
            End If

            If (drpKey5.SelectedIndex <> 0 Or txtKey5.Text.Trim = "") Then
                tmpBody = tmpBody.Replace(txtKey5.Text.Trim, DbField.GetStr(drv, drpKey5.SelectedValue))
            End If
            Return tmpBody
        Catch ex As Exception
            Return txtBody.Text
        End Try
    End Function

#Region "���÷���"
    '�ʼ�������������֤
    Public Function CheckMailInfo() As Boolean
        If Not IsEmail(txtFrom.Text.Trim) Then
            Response.Write("�����˵�ַ����ȷ!")
            Return False
        End If
        If Not IsEmail(txtReplyTo.Text.Trim) Then
            Response.Write("�ظ���ַ������ȷ!")
            Return False
        End If
        If txtSmtpServer.Text.Trim.Length < 5 Then
            Response.Write("�ʼ�����������ȷ!")
            Return False
        End If
        If txtUser.Text.Trim = "" Or txtPass.Text.Trim = "" Or txtFromName.Text.Trim = "" Then
            Response.Write("�����˺�,����,�����������Ƿ���ȷ!")
            Return False
        End If
        Return True
    End Function

    '�Ƿ�����ʼ�
    Public Function IsEmail(ByVal Value As String) As Boolean
        Dim RegEmail As New Regex("\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        Dim m As Match = RegEmail.Match(Value)
        Return m.Success
    End Function


    '�����ʼ���Ϣ
    Public Sub LoadConfig()
        'txtSmtpServer.Text = SMLibrary.LoadConfig.CONF_MAIL_SMTP
        'txtUser.Text = SMLibrary.LoadConfig.CONF_MAIL_USER
        'txtPass.Text = SMLibrary.LoadConfig.CONF_MAIL_PASS
        'txtReplyTo.Text = SMLibrary.LoadConfig.CONF_MAIL_REPLYTO
        'txtFrom.Text = SMLibrary.LoadConfig.CONF_MAIL_FROM
        'txtFromName.Text = SMLibrary.LoadConfig.CONF_MAIL_FROMNAME
        'txtLogTable.Text = SMLibrary.LoadConfig.CONF_LOGTABLE
        'txtInterval.Text = SMLibrary.LoadConfig.CONF_THREAD_GROPUINTERVAL.ToString
        'txtNumber.Text = SMLibrary.LoadConfig.CONF_THREAD_GROPUNUMBER.ToString
    End Sub


    '�����ʼ���Ϣ
    Public Sub SetConfig()
        'SMLibrary.LoadConfig.CONF_MAIL_SMTP = txtSmtpServer.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_USER = txtUser.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_PASS = txtPass.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_REPLYTO = txtReplyTo.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_FROM = txtFrom.Text.Trim
        'SMLibrary.LoadConfig.CONF_MAIL_FROMNAME = txtFromName.Text.Trim
        'SMLibrary.LoadConfig.CONF_LOGTABLE = txtLogTable.Text.Trim
        'SMLibrary.LoadConfig.CONF_THREAD_GROPUINTERVAL = CInt(txtInterval.Text.Trim)
        'SMLibrary.LoadConfig.CONF_THREAD_GROPUNUMBER = CInt(txtNumber.Text.Trim)
        'SMLibrary.LoadConfig.SaveXmlConfig() 'jmail
    End Sub

#End Region

    '����������Ϣ
    Private Sub bntSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntSave.Click
        If (CheckMailInfo()) And IsNumeric(txtNumber.Text.Trim) And IsNumeric(txtInterval.Text.Trim) Then
            SetConfig()
        End If
    End Sub

    '�Ƿ���ʾ�ʼ�����������
    Private Sub chkShowConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowConfig.CheckedChanged
        If (chkShowConfig.Checked) Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    '�Ƿ����ùؼ����滻
    Private Sub chkIsKey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsKey.CheckedChanged
        If (chkIsKey.Checked) Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub
End Class

End Namespace
