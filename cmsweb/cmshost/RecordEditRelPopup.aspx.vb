'---------------------------------------------------------------------------
'��ҳ�����ͬʱ�򿪶�������в�����Session���������Ǵ�Session���������ƴ��б�ҳ���Ψһ��־
'
'ע��1��������Ĵ�������һ���ǣ�Ĭ�ϴ��塣��Ϊ��ƴ���ʱ���븸������ʱû������ѡ��
'---------------------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


Partial Class RecordEditRelPopup
    Inherits Cms.Web.RecordEditBase

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
                Dim datForm As DataInputForm = PageDealFirstRequest(CmsPass, Panel1, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, VLng("PAGE_RECID"), VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), Nothing, True, VStr("PAGE_NOCHECK_RECLOCK"), lblHeaderAction1, lnkSave, False)      '����Postback֮���������
                Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
                'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
            Else
                PageDealPostBack(CmsPass, Request, ViewState, Panel1, lblHeaderAction1, CType(Session("PAGE_POPREL_DATAFORM" & lngCurResID), DataInputForm), RStr("isfrom"), VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, VLng("PAGE_RECID"), RLng("subtabresid"), VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), Nothing, VLng("PAGE_DEPID"), True, True, lblHeaderAction1, lnkSave, False, False)
                'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
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
        Dim strStyle As String = "Z-INDEX: 101; LEFT: 4px; POSITION: absolute; TOP: 28px"
        Panel1.BorderColor = Color.FromName("#D7E7FF")
        Panel1.Attributes.Add("style", strStyle)
        Panel1.BorderWidth = Unit.Pixel(1)
        Me.Form1.Controls.Add(Panel1)

        '�����Կ���
        lnkSave.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_SAVE")
        lbtnNew.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_NEW")
        lnkExit.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_EXIT")
        lbtnExtension.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_EXT")
            lbtnSaveNew.Text = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_SAVENEW")

            Dim printTitle As String = CmsMessage.GetUI(CmsPass, "TITLE_RECORD_PRINT")
            If CmsRights.HasSpecifiedRights(CmsPass, VLng("PAGE_RESID"), CType(16, Long)) = False Then
                lblPrint.Text = printTitle
                lblPrint.Enabled = False
            Else
                lblPrint.Enabled = True
                lblPrint.Text = "<a onclick='openPrint();' style='cursor:pointer;'>" + printTitle + "</a>"
                ' lblPrint.Text = "<a href='/cmsweb/cmshost/RecordPrintSimple.aspx?timeid=&MenuSection=MENU_RECORD&MenuKey=MENU_RECPRINT&MNURESLOCATE=1&cmsaction=&mnuformresid=&mnuhostresid=" + VLng("PAGE_HOSTRESID").ToString + "&mnuhostrecid=" + VLng("PAGE_HOSTRECID").ToString + "&mnurecid=" + VLng("PAGE_RECID").ToString + "&mnuresid=" + VLng("PAGE_RESID").ToString + "&mnuformname=' target='_blank'>" + printTitle + "</a>"
            End If


        '���봰���ύʱ��Ҫ���ʱ�䡢�������Ƿ���Ч
        lnkSave.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")
        '��ɲ�����رմ���
        lnkExit.Attributes.Add("onClick", "javascript:ClosePopupEditForm()")

        '��ʾ���⣬��ʾ��ǰ״̬����ӡ��޸ġ����ġ�...��
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Dim strResName As String = CmsPass.GetDataRes(lngCurResID).ResName
        lblHeader.Text = strResName

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
            'Dim s As String = lnkSave.Attributes("onClick")
            lnkSave.Attributes.Add("OnClientClick", "if( CheckValue(self.document.forms(0))){return true;} else{ return false;}")
    End Sub
    Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        '--------------------------------------------------------------------------
        '��һ������ʼ�����¼
        Dim blnSaveSuccess As Boolean = True '���Ӽ�¼ʱ�Ĵ��������뱣��ԭ�����ֵ
        Dim hashUICtrlValue As New Hashtable '��Ž��������пؼ�ֵ����Ϊ���ڱ������ʱ�ܽ�����ֵ������ʾ�ڽ����ϡ� Key���ؼ����ƣ�Value���ؼ�ֵ
        Dim strPromptMsg As String = ""
        Dim lngRecIDAfterAdd As Long = 0 '���ص�ǰ�༭�ļ�¼ID
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Try
            Dim blnHasDocUploaded As Boolean = False '�����¼ʱ���ص�ǰ�����������ĵ�������
            Dim datForm As DataInputForm = CType(Session("PAGE_POPREL_DATAFORM" & lngCurResID), DataInputForm)
            lngRecIDAfterAdd = FormManager.SaveRecords(CmsPass, Request, ViewState, Panel1, VLng("PAGE_HOSTRESID"), lngCurResID, VLng("PAGE_RECID"), hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), VLng("PAGE_DEPID"))

            blnSaveSuccess = True
        Catch ex As CmsException
            blnSaveSuccess = False
            strPromptMsg = ex.Message
            SLog.Err("�����¼ʧ�ܣ�" & ex.Message)
        Catch ex As Exception
            blnSaveSuccess = False
            strPromptMsg = "�����¼�쳣ʧ�ܣ����Ժ����ԣ�"
            SLog.Err("�����¼ʧ�ܣ�", ex)
        End Try
        '-------------------------------------------------------------------------------

        '����ɹ���ʧ�ܺ���ʾ�޸ļ�¼�Ĵ���
        ShowFormOfEdit(blnSaveSuccess, lngRecIDAfterAdd, hashUICtrlValue, strPromptMsg)

        'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
    End Sub

    Private Sub lbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnNew.Click
        '��ʾ�½���¼�Ĵ���
        ShowFormOfAddNew()

        'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
    End Sub

    Private Sub lbtnSaveNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSaveNew.Click
        '--------------------------------------------------------------------------
        '��һ������ʼ�����¼
        Dim blnSaveSuccess As Boolean = True '���Ӽ�¼ʱ�Ĵ��������뱣��ԭ�����ֵ
        Dim hashUICtrlValue As New Hashtable '��Ž��������пؼ�ֵ����Ϊ���ڱ������ʱ�ܽ�����ֵ������ʾ�ڽ����ϡ� Key���ؼ����ƣ�Value���ؼ�ֵ
        Dim strPromptMsg As String = ""
        Dim lngRecIDAfterAdd As Long = 0 '���ص�ǰ�༭�ļ�¼ID
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Try
            Dim blnHasDocUploaded As Boolean = False '�����¼ʱ���ص�ǰ�����������ĵ�������
            Dim datForm As DataInputForm = CType(Session("PAGE_POPREL_DATAFORM" & lngCurResID), DataInputForm)
            lngRecIDAfterAdd = FormManager.SaveRecords(CmsPass, Request, ViewState, Panel1, VLng("PAGE_HOSTRESID"), lngCurResID, VLng("PAGE_RECID"), hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), VLng("PAGE_DEPID"))
            ViewState("PAGE_RECID") = lngRecIDAfterAdd

            blnSaveSuccess = True
        Catch ex As CmsException
            blnSaveSuccess = False
            strPromptMsg = ex.Message
            SLog.Err("�����¼ʧ�ܣ�" & ex.Message)
        Catch ex As Exception
            blnSaveSuccess = False
            strPromptMsg = "�����¼�쳣ʧ�ܣ����Ժ����ԣ�"
            SLog.Err("�����¼ʧ�ܣ�", ex)
        End Try
        '-------------------------------------------------------------------------------

        If blnSaveSuccess = True Then
            '�ɹ������¼����ʾ�½���¼�Ĵ���
            ShowFormOfAddNew()
        Else
            '�����¼ʧ�ܺ���Ȼ��ʾ�޸ļ�¼�Ĵ���
            ShowFormOfEdit(blnSaveSuccess, lngRecIDAfterAdd, hashUICtrlValue, strPromptMsg)
        End If

        'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
    End Sub

    '--------------------------------------------------------------------------
    '��ʾ�޸ļ�¼�Ĵ���
    '--------------------------------------------------------------------------
    Private Function ShowFormOfEdit(ByVal blnSaveSuccess As Boolean, ByVal lngRecIDAfterAdd As Long, ByRef hashUICtrlValue As Hashtable, ByVal strPromptMsg As String) As Boolean
        Dim lngCurResID As Long = VLng("PAGE_RESID")

        '-------------------------------------------------------------------------------
        '�ڶ���������ɹ���ʧ�ܺ�Ŀ���
        If blnSaveSuccess Then  '�ɹ������¼����Ҫ�޸ĵ�ǰ�༭״̬Ϊ���޸�
            Try
                ViewState("PAGE_RECID") = lngRecIDAfterAdd
                lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                If VImd("PAGE_INMODE") = InputMode.AddInHostTable Then
                    ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                ElseIf VImd("PAGE_INMODE") = InputMode.AddInRelTable Then
                    ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                End If

                '�����ݿ���������ȡ��ǰ����ɹ��ļ�¼ֵ����ʾ
                Dim lngMode As InputMode = VImd("PAGE_INMODE")
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , , False, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
                SetButtonStatus(CmsPass, lngMode, lblHeaderAction1, lnkSave)
                ViewState("PAGE_INMODE") = lngMode
                Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
                RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                RegisterHiddenFieldOfCurrentRes(lngCurResID, lngRecIDAfterAdd) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
                RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
                SetFocusOnTextbox(datForm.strFirstColName)
            Catch ex As CmsException
                '��Ҫ��ʾ������������Ϊֻ�� Ҳ�ᵼ����ʾʧ��
                'strPromptMsg = "�����¼�ɹ�������ʾ����ʧ�ܣ�" & ex.Message
                SLog.Err("�����¼�ɹ�������ʾ����ʧ�ܣ�" & ex.Message)
            Catch ex As Exception
                '��Ҫ��ʾ������������Ϊֻ�� Ҳ�ᵼ����ʾʧ��
                'strPromptMsg = "�����¼�ɹ�������ʾ����ʧ�ܣ�"
                SLog.Err("�����¼�ɹ�������ʾ����ʧ�ܣ�", ex)
            End Try
            PromptMsg(strPromptMsg)
            Return blnSaveSuccess
        Else '���Ӽ�¼ʱ�Ĵ��������뱣��ԭ�����ֵ
            Try
                '-------------------------------------------------------------------------------------
                '���ǹ���������Ӽ�¼��������ȡ�������������ֶε�ֵ���Զ���ʾ�����봰���С�
                Dim hashFieldRelVal As Hashtable = Nothing 'Key���ؼ����ƣ�Value������������������ֶε�ֵ
                Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                If lngMode = InputMode.AddInRelTable Then
                    hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, True)
                End If
                If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

                '����ʲô״̬����ҳ��ĵ�һ�������ж���Request����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
                GetFieldInitValueFromRequest(CmsPass, lngCurResID, hashFieldRelVal)
                '����ʲô״̬����ҳ��ĵ�һ�������ж��Ӳ˵������ļ�����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
                GetFieldInitValueFromMenuConfig(CmsPass, lngCurResID, hashFieldRelVal)

                '�����������Ϣ
                Dim en As IDictionaryEnumerator = hashFieldRelVal.GetEnumerator()
                While en.MoveNext
                    Dim strCtrlName As String = CStr(en.Key)
                    If hashUICtrlValue.ContainsKey(strCtrlName) = False Then
                        HashField.SetStr(hashUICtrlValue, strCtrlName, CStr(en.Value))
                    End If
                End While
                '-------------------------------------------------------------------------------------

                'Dim lngMode As InputMode = VImd("PAGE_INMODE")
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , , True, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
                SetButtonStatus(CmsPass, lngMode, lblHeaderAction1, lnkSave)
                Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
                RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                RegisterHiddenFieldOfCurrentRes(lngCurResID, lngRecIDAfterAdd) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
                RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
                SetFocusOnTextbox(datForm.strFirstColName)
            Catch ex As Exception
                '�Ѿ���ʾ������Ϣ����������ʾ
                SLog.Err("�����¼ʧ�ܺ�����Load������ʧ�ܣ�", ex)
            End Try

            PromptMsg(strPromptMsg)
            Return blnSaveSuccess
        End If
        '-------------------------------------------------------------------------------
    End Function

    '--------------------------------------------------------------------------
    '��ʾ�½���¼�Ĵ���
    '--------------------------------------------------------------------------
    Private Function ShowFormOfAddNew() As Boolean
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Try
            '������ǰ״̬
            lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_ADD")
            ViewState("PAGE_INMODE") = InputMode.AddInRelTable

            '����Ƿ����½���¼ʱ�����������Ϣ
            Dim lngPrevRecID As Long = VLng("PAGE_RECID")
            ViewState("PAGE_RECID") = 0
            Dim blnKeepOldVal As Boolean = CTableForm.IsKeepOldValue(CmsPass, lngCurResID, VStr("PAGE_FORMNAME"))
            If blnKeepOldVal = False Then lngPrevRecID = 0

            '���ǹ���������Ӽ�¼��������ȡ�������������ֶε�ֵ���Զ���ʾ�����봰���С�
            Dim hashFieldRelVal As Hashtable = Nothing 'Key���ؼ����ƣ�Value������������������ֶε�ֵ
            Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
            If lngMode = InputMode.AddInRelTable Then
                hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, True)
            End If
            If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

            '����ʲô״̬����ҳ��ĵ�һ�������ж���Request����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
            GetFieldInitValueFromRequest(CmsPass, lngCurResID, hashFieldRelVal)
            '����ʲô״̬����ҳ��ĵ�һ�������ж��Ӳ˵������ļ�����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
            GetFieldInitValueFromMenuConfig(CmsPass, lngCurResID, hashFieldRelVal)

            '��鵱ǰ�����Ƿ��ֹ��¼����У��
            Dim blnCheckRecLock As Boolean = True
            If VStr("PAGE_NOCHECK_RECLOCK") = "1" Then blnCheckRecLock = False

            Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, lngMode, VStr("PAGE_FORMNAME"), hashFieldRelVal, lngPrevRecID, , , , , , True, blnCheckRecLock, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
            Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
            SetButtonStatus(CmsPass, lngMode, lblHeaderAction1, lnkSave)
            SetFocusOnTextbox(datForm.strFirstColName)
            RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
            RegisterHiddenFieldOfCurrentRes(lngCurResID, 0)  '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
            RegisterHiddenFieldForInputForm() 'ע�Ἰ�����봰�������Hidden����
            RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�

            Return True
        Catch ex As Exception
                PromptMsg(CmsMessage.GetMsg(CmsPass, "NEW_RECORD"), ex, True)
            Return False
        End Try
    End Function
End Class

End Namespace
