Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class RecordEditBase
    Inherits CmsPage
    '--------------------------------------------------------------------------
    '������Ĳ�������ΪViewState������������ҳ������ȡ���޸ġ�
    '--------------------------------------------------------------------------
    Protected Sub PageSaveParametersToViewState()
        If VStr("PAGE_BACKPAGE") = "" Then
            Dim strBP As String = RStr("backpage").Trim()
            Dim strFileName As String = Path.GetFileNameWithoutExtension(Request.PhysicalPath())
            If strBP <> "" Then
                strBP = strBP.Replace("[and]", "&")
                ViewState("PAGE_BACKPAGE") = strBP
                Session("CMSBP_" & strFileName) = strBP
            Else
                ViewState("PAGE_BACKPAGE") = SStr("CMSBP_" & strFileName)
                Dim yy As String = Convert.ToString(ViewState("PAGE_BACKPAGE"))

            End If
        End If
        If VLng("PAGE_RESID") = 0 Then
            ViewState("PAGE_RESID") = RLng("mnuresid")
            Dim aa As String = Convert.ToString(ViewState("PAGE_RESID"))
        End If
        If VLng("PAGE_RECID") = 0 Then
            ViewState("PAGE_RECID") = RLng("mnurecid")
            Dim bb As String = Convert.ToString(ViewState("PAGE_RECID"))
        End If
        If VStr("PAGE_FORMNAME") = "" Then
            ViewState("PAGE_FORMNAME") = RStr("mnuformname") '��������
            If VStr("PAGE_FORMNAME") = "" Then ViewState("PAGE_FORMNAME") = CTableForm.DEF_DESIGN_FORM
            Dim cc As String = Convert.ToString(ViewState("PAGE_FORMNAME"))
        End If
        If VLng("PAGE_INMODE") = 0 Then
            ViewState("PAGE_INMODE") = RLng("mnuinmode")
            Dim dd As String = Convert.ToString(ViewState("PAGE_INMODE"))
        End If
        If VLng("PAGE_INMODE_BAK") = 0 Then
            ViewState("PAGE_INMODE_BAK") = RLng("mnuinmode")
            Dim ee As String = Convert.ToString(ViewState("PAGE_INMODE_BAK"))
        End If

        If VLng("PAGE_HOSTRESID") = 0 Then
            ViewState("PAGE_HOSTRESID") = RLng("mnuhostresid")
            Dim ff As String = Convert.ToString(ViewState("PAGE_HOSTRESID"))
        End If
        If VLng("PAGE_HOSTRECID") = 0 Then
            ViewState("PAGE_HOSTRECID") = RLng("mnuhostrecid")
            Dim gg As String = Convert.ToString(ViewState("PAGE_HOSTRECID"))
        End If
        If VLng("PAGE_DEPID") = 0 Then
            ViewState("PAGE_DEPID") = RLng("depid")
            Dim hh As String = Convert.ToString(ViewState("PAGE_DEPID"))
        End If

        Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
        If lngMode = InputMode.AddInHostTable OrElse lngMode = InputMode.AddInRelTable Then 'OrElse lngMode = InputMode.MultiAddInHostTable OrElse lngMode = InputMode.MultiAddInRelTable Then
            '���״̬�µ�ǰ��¼ID������Ϊ0
            ViewState("PAGE_RECID") = 0 '���������

        End If

        If VStr("PAGE_NOCHECK_RECLOCK") = "" Then
            ViewState("PAGE_NOCHECK_RECLOCK") = RStr("nochecklock")
            Dim vv As String = Convert.ToString(ViewState("PAGE_NOCHECK_RECLOCK"))
        End If

        'If VLng("PAGE_SUBTAB_RESID") = 0 Then
        '    ViewState("PAGE_SUBTAB_RESID") = RLng("subtabresid")
        'End If
        'If VStr("PAGE_ISFROM") = "" Then
        '    ViewState("PAGE_ISFROM") = RStr("isfrom")
        'End If
    End Sub

    '--------------------------------------------------------------------------
    '�����һ��GET�����е�����
    '--------------------------------------------------------------------------
        Protected Function PageDealFirstRequest( _
            ByRef pst As CmsPassport, _
            ByRef Panel1 As System.Web.UI.WebControls.Panel, _
            ByVal lngHostResID As Long, _
            ByVal lngHostRecID As Long, _
            ByVal lngResID As Long, _
            ByRef lngRecID As Long, _
            ByRef lngMode As InputMode, _
            ByRef strFormName As String, _
            ByRef forceRights As ForceRightsInForm, _
            Optional ByVal blnCheckColRights As Boolean = False, _
            Optional ByVal strNoCheckRecLock As String = "0", _
            Optional ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label = Nothing, _
            Optional ByVal lnkSave As System.Web.UI.WebControls.LinkButton = Nothing, _
            Optional ByVal blnShowSaveButton As Boolean = True _
            ) As DataInputForm
            '���ǹ���������Ӽ�¼��������ȡ�������������ֶε�ֵ���Զ���ʾ�����봰���С�
            Dim hashFieldRelVal As Hashtable = Nothing 'Key���ؼ����ƣ�Value������������������ֶε�ֵ
            If lngMode = InputMode.AddInRelTable Then
                hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(pst, lngHostResID, lngHostRecID, lngResID, True)
            End If
            If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

            '����ʲô״̬����ҳ��ĵ�һ�������ж���Request����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
            GetFieldInitValueFromRequest(pst, lngResID, hashFieldRelVal)
            '����ʲô״̬����ҳ��ĵ�һ�������ж��Ӳ˵������ļ�����ȡ��Ҫ���ֶθ��ĳ�ʼֵ
            GetFieldInitValueFromMenuConfig(pst, lngResID, hashFieldRelVal)

            '��鵱ǰ�����Ƿ��ֹ��¼����У��
            Dim blnCheckRecLock As Boolean = True
            If strNoCheckRecLock = "1" Then blnCheckRecLock = False

            Dim datForm As DataInputForm = FormManager.LoadForm(pst, Panel1, Nothing, lngResID, lngMode, strFormName, hashFieldRelVal, lngRecID, , , , forceRights, True, blnCheckColRights, blnCheckRecLock, lngHostResID, lngHostRecID, blnShowSaveButton, True)
            SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
            SetFocusOnTextbox(datForm.strFirstColName)
            RegisterHiddenFieldOfRecID(datForm, lngRecID)
            RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
            RegisterHiddenFieldOfCurrentRes(lngResID, lngRecID) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
            RegisterHiddenFieldForInputForm() 'ע�Ἰ�����봰�������Hidden����
            RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�

            Return datForm
        End Function

        '--------------------------------------------------------------------------
        '����POST�е�������أ�True���˳����ӿں�ֱ���˳����壻False���˳����ӿں����֮��Ĵ���
        '--------------------------------------------------------------------------
        Protected Function PageDealPostBack( _
            ByRef pst As CmsPassport, _
            ByRef Request As System.Web.HttpRequest, _
            ByRef ViewState As System.Web.UI.StateBag, _
            ByRef Panel1 As System.Web.UI.WebControls.Panel, _
            ByRef lblStatus As System.Web.UI.WebControls.Label, _
            ByRef datForm As DataInputForm, _
            ByVal strCommand As String, _
            ByVal lngHostResID As Long, _
            ByVal lngHostRecID As Long, _
            ByVal lngResID As Long, _
            ByRef lngRecID As Long, _
            ByVal lngSubResID As Long, _
            ByRef lngMode As InputMode, _
            ByVal strFormName As String, _
            ByRef forceRights As ForceRightsInForm, _
            Optional ByVal lngDepID As Long = -1, _
            Optional ByVal blnPromptNoDocUpload As Boolean = False, _
            Optional ByVal blnCheckColRights As Boolean = False, _
            Optional ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label = Nothing, _
            Optional ByVal lnkSave As System.Web.UI.WebControls.LinkButton = Nothing, _
            Optional ByVal blnShowSaveButton As Boolean = True, _
            Optional ByVal blnThrowExceptionWhileError As Boolean = True _
            ) As Boolean
            Dim blnRtn As Boolean = False
            Select Case strCommand
                Case "savehostrec"
                    '��Ϊ�˹������е�Save��ť���ű������´���
                    'SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, SaveToDb, Nothing) '�����¼
                    'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
                    CommandSaveRecord1(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)     '�������Դ���е�"����"����������������¼��Ϣ
                    blnRtn = True

                Case "delrelrec"
                    FormManager.DeleteRecordsInRelTable(pst, lngSubResID, RStr("RECID3_" & lngSubResID, Request), strFormName)
                    'CommandDelSubRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngSubResID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)    'ɾ������Դ�б��ĳ����¼
                    blnRtn = True

                Case "popupfresh"
                    'CommandRefreshByPopupForm(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, , blnThrowExceptionWhileError)      '�������Ӵ��屣���˳�ʱ��ˢ�±��������������
                    blnRtn = True

                Case "savehostrec2"
                    'CommandSaveRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)     '�������Դ���е�"����"����������������¼��Ϣ
                    blnRtn = True

                Case Else
                    blnRtn = False
            End Select

            RegisterHiddenFieldForInputForm() 'ע�Ἰ�����봰�������Hidden����
            Return blnRtn
        End Function


        Protected Function PageDealPostBack1( _
            ByRef pst As CmsPassport, _
            ByRef Request As System.Web.HttpRequest, _
            ByRef ViewState As System.Web.UI.StateBag, _
            ByRef Panel1 As System.Web.UI.WebControls.Panel, _
            ByRef lblStatus As System.Web.UI.WebControls.Label, _
            ByRef datForm As DataInputForm, _
            ByVal strCommand As String, _
            ByVal lngHostResID As Long, _
            ByVal lngHostRecID As Long, _
            ByVal lngResID As Long, _
            ByRef lngRecID As Long, _
            ByVal lngSubResID As Long, _
            ByRef lngMode As InputMode, _
            ByVal strFormName As String, _
            ByRef forceRights As ForceRightsInForm, _
            Optional ByVal lngDepID As Long = -1, _
            Optional ByVal blnPromptNoDocUpload As Boolean = False, _
            Optional ByVal blnCheckColRights As Boolean = False, _
            Optional ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label = Nothing, _
            Optional ByVal lnkSave As System.Web.UI.WebControls.LinkButton = Nothing, _
            Optional ByVal blnShowSaveButton As Boolean = True, _
            Optional ByVal blnThrowExceptionWhileError As Boolean = True _
            ) As Boolean
            Dim blnRtn As Boolean = False
            Select Case strCommand
                Case "savehostrec"
                    '��Ϊ�˹������е�Save��ť���ű������´���
                    'SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, SaveToDb, Nothing) '�����¼
                    'InitialPanel(Panel1) '����3��Panel�����⡢���ݡ���ע����������Ϣ
                    Dim savehostas As Boolean = CommandSaveRecord1(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)    '�������Դ���е�"����"����������������¼��Ϣ
                    If savehostas Then
                        blnRtn = True
                    Else
                        blnRtn = False
                    End If


                Case "delrelrec"
                    FormManager.DeleteRecordsInRelTable(pst, lngSubResID, RStr("RECID3_" & lngSubResID, Request), strFormName)
                    'CommandDelSubRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngSubResID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)    'ɾ������Դ�б��ĳ����¼
                    blnRtn = True

                Case "popupfresh"
                    'CommandRefreshByPopupForm(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, , blnThrowExceptionWhileError)      '�������Ӵ��屣���˳�ʱ��ˢ�±��������������
                    blnRtn = True

                Case "savehostrec2"
                    'CommandSaveRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)     '�������Դ���е�"����"����������������¼��Ϣ
                    blnRtn = True

                Case Else
                    blnRtn = False
            End Select

            RegisterHiddenFieldForInputForm() 'ע�Ἰ�����봰�������Hidden����
            Return blnRtn
        End Function
        '--------------------------------------------------------------------------
        '�������Դ���е�"����"����������������¼��Ϣ
        '--------------------------------------------------------------------------
        Private Sub CommandSaveRecord( _
            ByRef pst As CmsPassport, _
            ByRef Request As System.Web.HttpRequest, _
            ByRef ViewState As System.Web.UI.StateBag, _
            ByRef Panel1 As System.Web.UI.WebControls.Panel, _
            ByRef lblStatus As System.Web.UI.WebControls.Label, _
            ByRef datForm As DataInputForm, _
            ByVal strCommand As String, _
            ByVal lngHostResID As Long, _
            ByVal lngHostRecID As Long, _
            ByVal lngResID As Long, _
            ByRef lngRecID As Long, _
            ByRef lngMode As InputMode, _
            ByVal strFormName As String, _
            ByRef forceRights As ForceRightsInForm, _
            Optional ByVal lngDepID As Long = -1, _
            Optional ByVal blnPromptNoDocUpload As Boolean = False, _
            Optional ByVal blnCheckColRights As Boolean = False, _
            Optional ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label = Nothing, _
            Optional ByVal lnkSave As System.Web.UI.WebControls.LinkButton = Nothing, _
            Optional ByVal blnShowSaveButton As Boolean = True, _
            Optional ByVal blnThrowExceptionWhileError As Boolean = True _
            )
            Dim strErrMsg As String = ""
            Dim hashUICtrlValue As New Hashtable '��Ž��������пؼ�ֵ����Ϊ���ڱ������ʱ�ܽ�����ֵ������ʾ�ڽ����ϡ� Key���ؼ����ƣ�Value���ؼ�ֵ
            Dim blnSuccess As Boolean = False

            Try
                Dim blnHasDocUploaded As Boolean = False
                lngRecID = FormManager.SaveRecords1(pst, Request, ViewState, Panel1, lngHostResID, lngResID, lngRecID, hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, lngMode, strFormName, lngDepID)
                If lngMode = InputMode.AddInHostTable Then
                    '�����״̬���ҵ�һ�ε�������桱������ɹ�����Ҫ�޸�״̬Ϊ���޸ġ�
                    If Not lblStatus Is Nothing Then lblStatus.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                    ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                    lngMode = InputMode.EditInHostTable
                    ViewState("PAGE_RECID") = lngRecID
                ElseIf lngMode = InputMode.AddInRelTable Then
                    '�����״̬���ҵ�һ�ε�������桱������ɹ�����Ҫ�޸�״̬Ϊ���޸ġ�
                    If Not lblStatus Is Nothing Then lblStatus.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                    ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                    lngMode = InputMode.EditInRelTable
                    ViewState("PAGE_RECID") = lngRecID
                End If

                If pst.GetDataRes(lngResID).ResTableType = "DOC" And blnHasDocUploaded = False Then
                    '������ĵ�������Ӽ�¼ʱû���ϴ��ĵ�������ʾ��֮������ĵ��ķ���
                    If blnPromptNoDocUpload Then strErrMsg = "��û���ϴ��ĵ����Ժ������ĵ������˵��е�ǩ���ĵ���Ϊ������¼����ĵ���"
                End If

                blnSuccess = True
            Catch ex As Exception
                blnSuccess = False
                SLog.Err("��������¼ʧ��", ex)
                strErrMsg = ex.Message

                If blnThrowExceptionWhileError Then
                    Throw New CmsException("��������¼ʧ�ܣ�" & ex.Message)
                End If
            End Try

            Try
                '���۳ɹ���ʧ�ܣ�����Ҫˢ�½�����ʾ
                datForm = FormManager.LoadForm(pst, Panel1, Nothing, lngResID, lngMode, strFormName, hashUICtrlValue, lngRecID, , , , forceRights, (Not blnSuccess), blnCheckColRights, , lngHostResID, lngHostRecID, blnShowSaveButton)
                SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                SetFocusOnTextbox(datForm.strFirstColName)
                RegisterHiddenFieldOfRecID(datForm, lngRecID)
                RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                RegisterHiddenFieldOfCurrentRes(lngResID, lngRecID) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
                RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
            Catch ex As Exception
                SLog.Err("��������¼�ɹ���ˢ��ҳ��ʧ��", ex)
                If strErrMsg = "" Then strErrMsg = ex.Message

                If blnThrowExceptionWhileError Then
                    Throw New CmsException("��������¼�ɹ���ˢ��ҳ��ʧ�ܣ�" & ex.Message)
                End If
            End Try

            If strErrMsg <> "" Then PromptMsg(strErrMsg, Nothing, True)
        End Sub

        Private Function CommandSaveRecord1( _
               ByRef pst As CmsPassport, _
               ByRef Request As System.Web.HttpRequest, _
               ByRef ViewState As System.Web.UI.StateBag, _
               ByRef Panel1 As System.Web.UI.WebControls.Panel, _
               ByRef lblStatus As System.Web.UI.WebControls.Label, _
               ByRef datForm As DataInputForm, _
               ByVal strCommand As String, _
               ByVal lngHostResID As Long, _
               ByVal lngHostRecID As Long, _
               ByVal lngResID As Long, _
               ByRef lngRecID As Long, _
               ByRef lngMode As InputMode, _
               ByVal strFormName As String, _
               ByRef forceRights As ForceRightsInForm, _
               Optional ByVal lngDepID As Long = -1, _
               Optional ByVal blnPromptNoDocUpload As Boolean = False, _
               Optional ByVal blnCheckColRights As Boolean = False, _
               Optional ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label = Nothing, _
               Optional ByVal lnkSave As System.Web.UI.WebControls.LinkButton = Nothing, _
               Optional ByVal blnShowSaveButton As Boolean = True, _
               Optional ByVal blnThrowExceptionWhileError As Boolean = True _
               ) As Boolean

            Dim strErrMsg As String = ""
            Dim hashUICtrlValue As New Hashtable '��Ž��������пؼ�ֵ����Ϊ���ڱ������ʱ�ܽ�����ֵ������ʾ�ڽ����ϡ� Key���ؼ����ƣ�Value���ؼ�ֵ
            Dim blnSuccess As Boolean = False


            Dim blnHasDocUploaded As Boolean = False
            lngRecID = FormManager.SaveRecords1(pst, Request, ViewState, Panel1, lngHostResID, lngResID, lngRecID, hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, lngMode, strFormName, lngDepID)
            If lngRecID = 0 Then
                Return False
            Else

                Try

                    If lngMode = InputMode.AddInHostTable Then
                        '�����״̬���ҵ�һ�ε�������桱������ɹ�����Ҫ�޸�״̬Ϊ���޸ġ�
                        If Not lblStatus Is Nothing Then lblStatus.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                        ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                        lngMode = InputMode.EditInHostTable
                        ViewState("PAGE_RECID") = lngRecID
                    ElseIf lngMode = InputMode.AddInRelTable Then
                        '�����״̬���ҵ�һ�ε�������桱������ɹ�����Ҫ�޸�״̬Ϊ���޸ġ�
                        If Not lblStatus Is Nothing Then lblStatus.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                        ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                        lngMode = InputMode.EditInRelTable
                        ViewState("PAGE_RECID") = lngRecID
                    End If

                    If pst.GetDataRes(lngResID).ResTableType = "DOC" And blnHasDocUploaded = False Then
                        '������ĵ�������Ӽ�¼ʱû���ϴ��ĵ�������ʾ��֮������ĵ��ķ���
                        If blnPromptNoDocUpload Then strErrMsg = "��û���ϴ��ĵ����Ժ������ĵ������˵��е�ǩ���ĵ���Ϊ������¼����ĵ���"
                    End If

                    blnSuccess = True
                Catch ex As Exception
                    blnSuccess = False
                    SLog.Err("��������¼ʧ��", ex)
                    strErrMsg = ex.Message

                    If blnThrowExceptionWhileError Then
                        Throw New CmsException("��������¼ʧ�ܣ�" & ex.Message)
                    End If
                End Try

                Try
                    '���۳ɹ���ʧ�ܣ�����Ҫˢ�½�����ʾ
                    datForm = FormManager.LoadForm(pst, Panel1, Nothing, lngResID, lngMode, strFormName, hashUICtrlValue, lngRecID, , , , forceRights, (Not blnSuccess), blnCheckColRights, , lngHostResID, lngHostRecID, blnShowSaveButton)
                    SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                    SetFocusOnTextbox(datForm.strFirstColName)
                    RegisterHiddenFieldOfRecID(datForm, lngRecID)
                    RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                    RegisterHiddenFieldOfCurrentRes(lngResID, lngRecID) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
                    RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
                Catch ex As Exception
                    SLog.Err("��������¼�ɹ���ˢ��ҳ��ʧ��", ex)
                    If strErrMsg = "" Then strErrMsg = ex.Message

                    If blnThrowExceptionWhileError Then
                        Throw New CmsException("��������¼�ɹ���ˢ��ҳ��ʧ�ܣ�" & ex.Message)
                    End If
                End Try

                If strErrMsg <> "" Then PromptMsg(strErrMsg, Nothing, True)
                Return True
            End If

        End Function


        '--------------------------------------------------------------------------
        'ע�ᵱǰ��Դ�ļ�¼ID��ҳ���ϵ�hidden����
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldOfRecID(ByRef datForm As DataInputForm, ByVal lngRecID As Long)
            RegisterHiddenField("RECID", CStr(lngRecID))
        End Sub

        '--------------------------------------------------------------------------
        'ע���ӱ���ҳ���ϵ�hidden����
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldOfSubTableRecID(ByRef datForm As DataInputForm)
            If Not datForm Is Nothing Then
                If Not datForm.alistSubResIDs Is Nothing Then
                    Dim lngSubResID As Long
                    For Each lngSubResID In datForm.alistSubResIDs
                        RegisterHiddenField("RECID3_" & lngSubResID, "")
                    Next
                End If
            Else
            End If
        End Sub

        '--------------------------------------------------------------------------
        '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldOfCurrentRes(ByVal lngResID As Long, ByVal lngRecID As Long)
            RegisterHiddenField("FORMDATA_RESID", CStr(lngResID))
            RegisterHiddenField("FORMDATA_RECID", CStr(lngRecID))
        End Sub

        '--------------------------------------------------------------------------
        'ע�Ἰ�����봰�������Hidden����
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldForInputForm()
            RegisterHiddenField("isfrom", "")
            RegisterHiddenField("subtabresid", "")
        End Sub

        '--------------------------------------------------------------------------
        '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
        '--------------------------------------------------------------------------
        Protected Sub RegisterCmsScripts(ByRef datForm As DataInputForm)
            If Not datForm Is Nothing Then
                If Not datForm.alistRegScripts Is Nothing Then
                    If datForm.alistRegScripts.Count > 0 Then
                        Dim strScript As String
                        For Each strScript In datForm.alistRegScripts
                            If strScript <> "" Then
                                Dim strTemp As String = "<script language=""javascript"">" & Environment.NewLine
                                strTemp &= strScript & Environment.NewLine
                                strTemp &= "</script>" & Environment.NewLine
                                RegisterStartupScript(CStr(TimeId.CurrentMilliseconds()), strTemp)
                            End If
                        Next
                    End If
                End If
            Else
            End If
        End Sub

        '--------------------------------------------------------------------------
        '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
        '--------------------------------------------------------------------------
        Protected Sub GetFieldInitValueFromRequest(ByRef pst As CmsPassport, ByVal lngResID As Long, ByRef hashFieldRelVal As Hashtable)
            Dim i As Long = 1
            For i = 1 To 100
                Dim strColName As String = RStr("colname___" & i)
                If strColName = "" Then Exit For
                Dim strColValue As String = RStr("colval___" & i)
                HashField.SetStr(hashFieldRelVal, TextboxName.GetCtrlName(strColName, lngResID), strColValue)
            Next
        End Sub

        '--------------------------------------------------------------------------
        '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
        '--------------------------------------------------------------------------
        Protected Sub GetFieldInitValueFromMenuConfig(ByRef pst As CmsPassport, ByVal lngResID As Long, ByRef hashFieldRelVal As Hashtable)
            Dim strMenuSection As String = AspPage.RStr("MenuKey", Request)
            If strMenuSection = "" Then Return
            Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(pst.Employee.Language, CmsMenuType.Extension)
            If datSvc Is Nothing Then Return
            Dim hashMenuDict As Hashtable = CmsFrmContentFlow.GetMenuDiction(pst, datSvc, lngResID)
            Dim alistFlowUrl As ArrayList = CmsFrmContentFlow.GetColSet(pst, datSvc, strMenuSection, hashMenuDict, hashFieldRelVal)
            Dim datColSet As DataColSet
            Dim i As Long = 1
            For Each datColSet In alistFlowUrl
                If datColSet.strCOLNAME <> "" Then
                    HashField.SetStr(hashFieldRelVal, TextboxName.GetCtrlName(datColSet.strCOLNAME, lngResID), CmsFrmContentFlow.FilterFieldValue(pst, datColSet.strColValue))
                End If
            Next
        End Sub

        '----------------------------------------------------------------
        '���á����桱�Ȱ�ť��״̬
        '----------------------------------------------------------------
        Protected Sub SetButtonStatus( _
            ByRef pst As CmsPassport, _
            ByVal lngMode As InputMode, _
            ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label, _
            ByVal lnkSave As System.Web.UI.WebControls.LinkButton _
            )
            If lngMode = InputMode.AddInHostTable OrElse lngMode = InputMode.AddInRelTable Then
                If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_ADD")
                If Not lnkSave Is Nothing Then lnkSave.Enabled = True
            ElseIf lngMode = InputMode.EditInHostTable OrElse lngMode = InputMode.EditInRelTable Then
                If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                If Not lnkSave Is Nothing Then lnkSave.Enabled = True
            ElseIf lngMode = InputMode.ViewInHostTable OrElse lngMode = InputMode.ViewInRelTable Then
                If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_VIEW")
                If Not lnkSave Is Nothing Then lnkSave.Enabled = False '����״̬��Disable�����桱��ť
            End If
        End Sub

        '----------------------------------------------------------------
        '�ж���Դ�����������ӱ�
        '----------------------------------------------------------------
        Protected Function IsFromHost(ByVal lngMode As InputMode) As Integer
            If lngMode = InputMode.AddInHostTable OrElse lngMode = InputMode.EditInHostTable OrElse lngMode = InputMode.PrintInHostTable OrElse lngMode = InputMode.ViewInHostTable Then
                Return ResourceLocation.HostTable
            Else
                Return ResourceLocation.RelTable
            End If
        End Function

        '--------------------------------------------------------------------------
        '�����¼
        '--------------------------------------------------------------------------
        Protected Function SaveRecordOnUI(ByRef pst As CmsPassport, ByRef Panel1 As System.Web.UI.WebControls.Panel, ByRef lblHeaderAction1 As System.Web.UI.WebControls.Label, ByRef lnkSave As System.Web.UI.WebControls.LinkButton, ByVal blnShowSaveButton As Boolean, ByRef forceRights As ForceRightsInForm) As Long
            Dim blnSaveSuccess As Boolean = True '���Ӽ�¼ʱ�Ĵ��������뱣��ԭ�����ֵ
            Dim hashUICtrlValue As New Hashtable '��Ž��������пؼ�ֵ����Ϊ���ڱ������ʱ�ܽ�����ֵ������ʾ�ڽ����ϡ� Key���ؼ����ƣ�Value���ؼ�ֵ
            Dim strPromptMsg As String = ""
            Dim lngRecIDAfterAdd As Long = 0 '���ص�ǰ�༭�ļ�¼ID

            '--------------------------------------------------------------------------
            '��һ������ʼ�����¼
            Try
                '--------------------------------------------------------------------------
                '���浱ǰ��Դ�ĵ�ǰ��¼
                Dim blnHasDocUploaded As Boolean = False '�����¼ʱ���ص�ǰ�����������ĵ�������
                Dim datForm As DataInputForm = CType(Session("PAGE_EDITADV_DATAFORM"), DataInputForm)
                lngRecIDAfterAdd = FormManager.SaveRecords(pst, Request, ViewState, Panel1, VLng("PAGE_HOSTRESID"), VLng("PAGE_RESID"), VLng("PAGE_RECID"), hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, CType(VLng("PAGE_INMODE"), InputMode), VStr("PAGE_FORMNAME"), VLng("PAGE_DEPID"))
                '--------------------------------------------------------------------------

                '������ĵ�������Ӽ�¼ʱû���ϴ��ĵ�������ʾ��֮������ĵ��ķ���
                If pst.GetDataRes(VLng("PAGE_RESID")).ResTableType = "DOC" And blnHasDocUploaded = False And (CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInHostTable OrElse CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInRelTable) Then
                    strPromptMsg = CmsMessage.GetMsg(CmsPass, "TIP_ADDDOC_LATER")
                End If

                blnSaveSuccess = True
            Catch ex As CmsException
                blnSaveSuccess = False
                strPromptMsg = ex.Message
                SLog.Err("�����¼ʧ�ܣ�" & ex.Message)
            Catch ex As Exception
                blnSaveSuccess = False
                strPromptMsg = "�����¼�쳣ʧ�ܣ����Ժ����ԣ�"
                SLog.Err(strPromptMsg, ex)
            End Try
            '-------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------
            '�ڶ�������¼����ʱ��Ҫͬʱ����˵������ļ��ж����������Դ����Ϣ
            If blnSaveSuccess Then
                Try
                    SaveMenuSectionResources(pst, VLng("PAGE_RESID"), lngRecIDAfterAdd)
                Catch ex As Exception
                    strPromptMsg = "��¼����ɹ�������ǰ�˵������������Դд�����ʧ�ܣ�"
                    SLog.Err(strPromptMsg, ex)
                End Try
            End If
            '-------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------
            '������������ɹ���ʧ�ܺ�Ŀ���
            If blnSaveSuccess Then  '�ɹ������¼����Ҫ�޸ĵ�ǰ�༭״̬Ϊ���޸�
                Try
                    ViewState("PAGE_RECID") = lngRecIDAfterAdd
                    If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "��" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                    If CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInHostTable Then
                        ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                    ElseIf CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInRelTable Then
                        ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                    End If

                    '�����ݿ���������ȡ��ǰ����ɹ��ļ�¼ֵ����ʾ
                    Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                    Dim datForm As DataInputForm = FormManager.LoadForm(pst, Panel1, Nothing, VLng("PAGE_RESID"), lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , forceRights, False, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), blnShowSaveButton)
                    SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                    ViewState("PAGE_INMODE") = lngMode
                    Session("PAGE_EDITADV_DATAFORM") = datForm
                    RegisterHiddenFieldOfRecID(datForm, VLng("PAGE_RECID"))
                    RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                    RegisterHiddenFieldOfCurrentRes(VLng("PAGE_RESID"), lngRecIDAfterAdd) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
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
                If strPromptMsg.Trim = "" Then
                    PromptMsg("����ɹ���")
                Else
                    PromptMsg(strPromptMsg)
                End If

                Return lngRecIDAfterAdd
            Else '���Ӽ�¼ʱ�Ĵ��������뱣��ԭ�����ֵ
                Try
                    Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                    Dim datForm As DataInputForm = FormManager.LoadForm(pst, Panel1, Nothing, VLng("PAGE_RESID"), lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , forceRights, True, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), blnShowSaveButton)
                    SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                    Session("PAGE_EDITADV_DATAFORM") = datForm
                    RegisterHiddenFieldOfRecID(datForm, VLng("PAGE_RECID"))
                    RegisterHiddenFieldOfSubTableRecID(datForm) 'ע���ӱ���ҳ���ϵ�hidden����
                    RegisterHiddenFieldOfCurrentRes(VLng("PAGE_RESID"), lngRecIDAfterAdd) '���浱ǰ����Ļ�����Ϣ�ڽ���hidden������
                    RegisterCmsScripts(datForm) '��Ҫ�ڵ�ǰ�����е���RegisterStartupScriptע���Javascript�����б�
                    SetFocusOnTextbox(datForm.strFirstColName)
                Catch ex As Exception
                    '�Ѿ���ʾ������Ϣ����������ʾ
                    SLog.Err("�����¼ʧ�ܺ�����Load������ʧ�ܣ�", ex)
                End Try

                PromptMsg(strPromptMsg)
                Return lngRecIDAfterAdd
            End If

            If strPromptMsg.Trim() = "" Then
                PromptMsg("����ɹ���")
            End If
            Return lngRecIDAfterAdd
            '-------------------------------------------------------------------------------
        End Function

        '----------------------------------------------------------------
        '��¼����ʱ��Ҫͬʱ����˵������ļ��ж����������Դ����Ϣ
        '----------------------------------------------------------------
        Private Sub SaveMenuSectionResources(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long)
            '��ȡ�ձ���ļ�¼��������Ϣ
            If lngResID = 0 OrElse lngRecID = 0 Then Return
            Dim datRes As DataResource = pst.GetDataRes(lngResID)

            Dim hashFieldVal As Hashtable = ResFactory.TableService(datRes.ResTableType).GetRecordDataByHashtable(pst, lngResID, lngRecID)
            Try
                Dim strOneMenuSection As String = RStr("MenuKey")
                If strOneMenuSection <> "" Then
                    Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(pst.Employee.Language, CmsMenuType.Extension)
                    '��ȡ�˵����ֵ��б�
                    Dim hashMenuDict As Hashtable = CmsFrmContentFlow.GetMenuDiction(pst, datSvc, lngResID)
                    Dim alistFlowAct As ArrayList = CmsFrmContentFlow.GetColSet(pst, datSvc, strOneMenuSection, hashMenuDict, hashFieldVal)
                    Dim datColSet As DataColSet
                    For Each datColSet In alistFlowAct
                        If datColSet.strRESTYPE = "0" Then '0����ǰ��Դ
                        ElseIf datColSet.strRESTYPE = "1" Then '1���ǵ�ǰ��Դ�ĸ���Դ
                        ElseIf datColSet.strRESTYPE = "2" Then '2���ǵ�ǰ��Դ������Դ
                            CmsFrmContentFlow.SetColumnOfSubResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                        ElseIf datColSet.strRESTYPE = "3" Then '3����������Դ
                            CmsFrmContentFlow.SetColumnOfOtherResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                        End If
                    Next

                    'If datSvc.GetSecAttr(strOneMenuSection, "ROLLBACK_ALLOT") = "1" Then '�ع����ʲ���
                    '    BizAllot.AllotRollback(pst, VLng("PAGE_RESID"), hashFieldVal)
                    'End If
                End If
            Catch ex As Exception
                SLog.Err("�༭���봰��ʱִ�в˵�ָ��ʱ�쳣����", ex)
            End Try
        End Sub

        '----------------------------------------------------------
        '��ȡ��ǰ����GET����POST�е�ָ����������ֵ
        '----------------------------------------------------------
        Public Function VImd(ByVal strParamName As String) As InputMode
            Return CType(VLng(strParamName, ViewState), InputMode)
        End Function
    End Class

End Namespace
