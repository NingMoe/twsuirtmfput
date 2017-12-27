Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ReportRedirector
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        Dim blnCloseForm As Boolean = False
        Try
            Dim strRedirectUrl As String = ""
            Dim strFlowMsg As String = ""
            Dim blnRefreshTableData As Boolean = False
            Dim bln As Boolean = CmsFrmContentFlow.FlowEntry(CmsPass, "MenuFlowControllerForReport", strRedirectUrl, strFlowMsg, blnRefreshTableData, Request, Session, Response, Server)
            If bln Then
                If strRedirectUrl <> "" Then
                    'У��ʹ���ɹ����ض�����ָ������URL
                    Dim strUrl As String = CmsUrl.AppendParam(strRedirectUrl, "mnurecid=" & RStr("mnurecid") & "&timeid=" & TimeId.CurrentMilliseconds())
                    Response.Redirect(strUrl, False)
                    Return
                Else
                    PromptMsg("δ������Ч���ض���URL��")
                    blnCloseForm = True
                End If
            Else
                PromptMsg(strFlowMsg)
                blnCloseForm = True
            End If
        Catch ex As Exception
            SLog.Err("��ʾ�����쳣����", ex)
            blnCloseForm = True
        End Try

        'ʧ�ܺ�رմ���
        If blnCloseForm = True AndAlso Not IsStartupScriptRegistered("CmsCloseForm") Then
            Dim strScript As String = "<script language=""javascript"">" & Environment.NewLine
            strScript &= "    window.close();" & Environment.NewLine
            strScript &= "</script>" & Environment.NewLine
            RegisterStartupScript("CmsCloseForm", strScript)
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub
End Class

End Namespace
