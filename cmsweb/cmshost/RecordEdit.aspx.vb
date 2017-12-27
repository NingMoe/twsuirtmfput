'===========================================================================
' ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
' �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\cmshost\Stub_recordedit_aspx_vb.vb���ĳ������ 
' �̳С�
' ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
' ��������ҳ��
' ����������ҳ��cmshost\recordedit.aspx��Ҳ���޸ģ��������µ�������
' �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


'Partial Class RecordEdit
Partial Class Migrated_RecordEdit

Inherits RecordEdit

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

    Private Panel1 As Panel = Nothing

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            PageSaveParametersToViewState() '������Ĳ�������ΪViewState������������ҳ������ȡ���޸�
            PageInitialize() '��ʼ��ҳ��

            Dim lngCurResID As Long = VLng("PAGE_RESID")
            If Not IsPostBack Then
                '�����ڴ˴������ƴ�����Ϣ CHENYU 2010/6/1
                ViewState("PAGE_FORMNAME") = FormManager.GetRouterFormName(CmsPass, lngCurResID, VLng("PAGE_RECID")) 'CHENYU 20100601 ADD
                Dim datForm As DataInputForm = PageDealFirstRequest(CmsPass, Panel1, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, VLng("PAGE_RECID"), VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), Nothing, True, VStr("PAGE_NOCHECK_RECLOCK"), lblHeaderAction1, lnkSave, False)
                Session("PAGE_EDITADV_DATAFORM") = datForm
            Else
                PageDealPostBack(CmsPass, Request, ViewState, Panel1, lblHeaderAction1, CType(Session("PAGE_EDITADV_DATAFORM"), DataInputForm), RStr("isfrom"), VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, VLng("PAGE_RECID"), RLng("subtabresid"), VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), Nothing, VLng("PAGE_DEPID"), True, True, lblHeaderAction1, lnkSave, False, False)
                If Request.Form("CommandName") = "save" Then

                    SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, Nothing)
                    Session("PAGE_EDITADV_DATAFORM") = Nothing
                    Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                Else
                    If Request.Form("CommandName").ToString() = "exit" Then
                        Session("PAGE_EDITADV_DATAFORM") = Nothing
                        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                    End If
                End If
            End If
        Catch ex As CmsExceptionCustForm
            Dim strUrl As String = ex.Message
            Server.Transfer(strUrl, True)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------
    '��ʼ��ҳ��
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        '��ʼ��Panel
        Panel1 = New Panel
        Panel1.ID = "Panel1"
        Panel1.EnableViewState = False
            Dim strStyle As String = "Z-INDEX: 101;POSITION: absolute; LEFT: 4px; POSITION: absolute; TOP: 28px"
        Panel1.BorderColor = Color.FromName("#D7E7FF")
        Panel1.Attributes.Add("style", strStyle)
        Panel1.BorderWidth = Unit.Pixel(1)
            Me.Form1.Controls.Add(Panel1)
            
        '�����Կ���
        lnkSave.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_SAVE")
        lbtnNew.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_NEW")
            hyExit.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_EXIT")
            lbtnExtension.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_EXT")

            'if cmspass.r
            Dim printTitle As String = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_PRINT")
            If CmsRights.HasSpecifiedRights(CmsPass, VLng("PAGE_RESID"), CType(16, Long)) = False Then
                lblPrint.Text = printTitle
                lblPrint.Enabled = False
            Else
                lblPrint.Enabled = True
                lblPrint.Text = "<a onclick='openPrint();' style='cursor:pointer;'>" + printTitle + "</a>"
                ' lblPrint.Text = "<a href='/cmsweb/cmshost/RecordPrintSimple.aspx?timeid=&MenuSection=MENU_RECORD&MenuKey=MENU_RECPRINT&MNURESLOCATE=1&cmsaction=&mnuformresid=&mnuhostresid=" + VLng("PAGE_HOSTRESID").ToString + "&mnuhostrecid=" + VLng("PAGE_HOSTRECID").ToString + "&mnurecid=" + VLng("PAGE_RECID").ToString + "&mnuresid=" + VLng("PAGE_RESID").ToString + "&mnuformname=' target='_blank'>" + printTitle + "</a>"
            End If
 


        Dim lngMode As InputMode = VImd("PAGE_INMODE")
        If lngMode = InputMode.AddInHostTable Then Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID

        '���봰���ύʱ��Ҫ���ʱ�䡢�������Ƿ���Ч
        lnkSave.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")

        '��ʾ���⣬��ʾ��ǰ״̬����ӡ��޸ġ����ġ�...��
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Dim strResName As String = ResFactory.ResService.GetResName(CmsPass, lngCurResID)
        lblHeader.Text = strResName

        'ֻ����������������״̬������ʾ���½���
        If VLng("PAGE_INMODE_BAK") = InputMode.AddInHostTable Or VLng("PAGE_INMODE_BAK") = InputMode.AddInRelTable Then
            lbtnNew.Enabled = True
        Else
            lbtnNew.Enabled = False
        End If

        'Load��Դ��չ���ܲ˵���һ������Ŀ�ͻ��Ķ��ƿ���
        Dim intResLoc As ResourceLocation = ResourceLocation.HostTable
        Dim strMenuJSFunc As String = "ShowExtMenuInHost"
        If IsFromHost(CType(VInt("PAGE_INMODE"), InputMode)) = ResourceLocation.HostTable Then
            intResLoc = ResourceLocation.HostTable
            strMenuJSFunc = "ShowExtMenuInHost"
        Else
            intResLoc = ResourceLocation.RelTable
            strMenuJSFunc = "ShowExtMenuInSub"
        End If
        Dim strScript As String = CmsMenu.CreateScriptOfOneMainMenu(CmsPass, lbtnExtension, imgExtension, lngCurResID, intResLoc, "MENU_RES_" & CmsPass.GetDataRes(lngCurResID).IndepParentResID, CmsMenuType.Extension, strMenuJSFunc, CmsMenuFrom.EditForm)
        If strScript <> "" And (Not IsStartupScriptRegistered(strMenuJSFunc)) Then
            RegisterStartupScript(strMenuJSFunc, strScript)
        End If
    End Sub

    Private Sub lnkSave_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Load
        Dim m_button As LinkButton = CType(sender, LinkButton)
            'Dim m_upload As Uploader = New Uploader
            'm_upload.RegisterProgressBar(m_button)
            ' Dim s As String = lnkSave.Attributes("onClick")
            lnkSave.Attributes.Add("OnClientClick", "if( CheckValue(self.document.forms(0))){ return true;} else{ return false;}")
        End Sub

        'Private Sub lbtnPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnPrint.Load
        '    'window.open('/cmsweb/cmshost/RecordPrintSimple.aspx?timeid=126473348&MenuSection=MENU_RECORD&MenuKey=MENU_RECPRINT&MNURESLOCATE=1&cmsaction=&mnuformresid=&mnuhostresid=" + VLng("PAGE_HOSTRESID").ToString + "&mnuhostrecid=" + VLng("PAGE_HOSTRECID").ToString + "&mnurecid=" + VLng("PAGE_RECID").ToString + "&mnuresid=" + VLng("PAGE_RESID").ToString + "&mnuformname=')
        '    lbtnPrint.Attributes.Add("OnClientClick", "return false;")
        'End Sub
        Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
            SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, Nothing)  '�����¼ 

            'Response.Write("<script>try{window.opener.OpenLoadbbb('" + CType(RLng("mnuinmode"), Integer).ToString + "','" + VLng("PAGE_RESID").ToString + "');}catch (e){}</script>") '�������ڳɶ�������Ŀ  tq  edit  2012-03-21
        End Sub

       









        Private Sub lbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnNew.Click
            Try
                Dim lngCurResID As Long = VLng("PAGE_RESID")
                Session("PAGE_EDITADV_DATAFORM") = Nothing

                '------------------------------------------------------------------------
                '������ǰ״̬
                lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_ADD")
                ViewState("PAGE_INMODE") = VLng("PAGE_INMODE_BAK") '�ָ���������ı༭ģʽ
                Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                If lngMode = InputMode.EditInHostTable Then
                    lngMode = InputMode.AddInHostTable
                ElseIf lngMode = InputMode.EditInRelTable Then
                    lngMode = InputMode.AddInRelTable
                End If
                '------------------------------------------------------------------------

                '------------------------------------------------------------------------
                '�ж��Ƿ���������¼�Ľ����ϱ���֮ǰ�༭��¼��ֵ
                Dim lngPrevRecID As Long = VLng("PAGE_RECID")
                ViewState("PAGE_RECID") = 0
                Dim blnKeepOldVal As Boolean = CTableForm.IsKeepOldValue(CmsPass, lngCurResID, VStr("PAGE_FORMNAME"))
                If blnKeepOldVal = False Then lngPrevRecID = 0

                Dim hashFieldRelVal As Hashtable = Nothing 'Key���ؼ����ƣ�Value������������������ֶε�ֵ
                If lngMode = InputMode.AddInRelTable Then
                    hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, True)
                End If
                If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

                '����ʲô״̬����ҳ��ĵ�һ�������ж���Request����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
                GetFieldInitValueFromRequest(CmsPass, lngCurResID, hashFieldRelVal)
                '����ʲô״̬����ҳ��ĵ�һ�������ж��Ӳ˵������ļ�����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
                GetFieldInitValueFromMenuConfig(CmsPass, lngCurResID, hashFieldRelVal)
                '------------------------------------------------------------------------

                '------------------------------------------------------------------------
                'Load�ձ�
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), hashFieldRelVal, lngPrevRecID, , , , , , True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
                Session("PAGE_EDITADV_DATAFORM") = datForm
                SetFocusOnTextbox(datForm.strFirstColName)
                RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                RegisterHiddenFieldOfCurrentRes(lngCurResID, 0) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
                RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
                '------------------------------------------------------------------------

                'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
            Catch ex As Exception
                PromptMsg(CmsMessage.GetMsg(CmsPass, "NEW_RECORD"), ex, True)
            End Try
        End Sub

        Private Sub lnkExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkExit.Click
            Session("PAGE_EDITADV_DATAFORM") = Nothing
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        End Sub

    End Class
End Namespace
