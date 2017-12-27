Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


Partial Class ResourceImportStep1
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rdoAutoImport As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rdoManualImport As System.Web.UI.WebControls.RadioButton
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
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Session("CMS_RESIMP_DBC") = Nothing

        lblResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try

                ' Dim m_upload As Uploader = New Uploader
                Dim m_file As HttpPostedFile = File1.PostedFile
                Dim fileName As String = m_file.FileName.Substring(m_file.FileName.LastIndexOf("\") + 1)
            Dim ext As String = m_file.FileName.Substring(m_file.FileName.LastIndexOf(".") + 1)
            Dim strImportFile As String = ""

                If ext.ToLower() = "xls" Or ext.ToLower.Trim = "xlsx" Or ext.ToLower() = "mdb" Then
                    Dim strTempFolder As String = CmsConfig.TempFolder & "expimp\"
                    If Directory.Exists(strTempFolder) = False Then Directory.CreateDirectory(strTempFolder)
                    Dim strUnique As String = CStr(TimeId.CurrentMilliseconds())
                    strImportFile = strTempFolder & fileName
                    m_file.SaveAs(strImportFile)
                End If

            '��ȡ��������Դ�ļ���������Ϊ��ʱ�ļ�
            'Dim strImportFile As String = FileTransfer.GetImportFile(Request, "*.mdb;*.xls")
            If strImportFile = "" Then
                PromptMsg("��������Ч�����ݿ��ļ�ȫ·����")
                Return
            End If

            Dim strFileExt As String = Path.GetExtension(strImportFile)
            strFileExt = StringDeal.Trim(strFileExt, ".", ".").ToLower()
            Dim dbc As DbConfig

                If strFileExt = "xls" Or ext.ToLower.Trim = "xlsx" Then
                    dbc = AdoxDbManager.GenerateDbConfigForExcel(strImportFile)
                ElseIf strFileExt = "mdb" Then
                    dbc = AdoxDbManager.GenerateDbConfigForMdb(strImportFile)
                End If

            Dim blnConn As Boolean = AdoxDbManager.TestConnection(dbc)

            If blnConn = False Then
                PromptMsg("���ݿ�����ʧ�ܣ���������ȷ�����ݿ���Ϣ��")
                Return
            End If

            Session("CMS_RESIMP_DBC") = dbc
            Session("CMSBP_ResourceImportStep2") = "/cmsweb/adminres/ResourceImportStep1.aspx?mnuresid=" & VLng("PAGE_RESID")
            Response.Redirect("/cmsweb/adminres/ResourceImportStep2.aspx?mnuresid=" & VLng("PAGE_RESID"), False)

        Catch ex As Exception

            PromptMsg("", ex, True)

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Private Sub btnImpSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImpSQL.Click
        Try
            'У��������Ϣ
            If txtSqlIP.Text.Trim() = "" Then
                PromptMsg("��������Ч��SQL���ݿ��ַ��")
                Return
            End If
            If IsNumeric(txtSqlPort.Text.Trim()) = False Then
                PromptMsg("��������Ч��SQL���ݿ�˿ڣ�")
                Return
            End If
            If txtSqlDbName.Text.Trim() = "" Then
                PromptMsg("��������Ч��SQL���ݿ�DB���ƣ�")
                Return
            End If
            If txtSqlUser.Text.Trim() = "" Then
                PromptMsg("��������Ч��SQL���ݿ��¼�û�����")
                Return
            End If

            Dim dbcSql As New DbConfig
            dbcSql.DatabaseType = DbConfig.DbType.MsSql
            dbcSql.Host = txtSqlIP.Text.Trim()
            dbcSql.Port = txtSqlPort.Text.Trim()
            dbcSql.Database = txtSqlDbName.Text.Trim()
            dbcSql.User = txtSqlUser.Text.Trim()
            dbcSql.Pass = txtSqlUserPass.Text.Trim()
            Dim blnConn As Boolean = AdoxDbManager.TestConnection(dbcSql)
            If blnConn = False Then
                PromptMsg("���ݿ�����ʧ�ܣ���������ȷ�����ݿ���Ϣ��")
                Return
            End If
            Session("CMS_RESIMP_DBC") = dbcSql
            Session("CMSBP_ResourceImportStep2") = "/cmsweb/adminres/ResourceImportStep1.aspx?mnuresid=" & VLng("PAGE_RESID")
            Response.Redirect("/cmsweb/adminres/ResourceImportStep2.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit2.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
