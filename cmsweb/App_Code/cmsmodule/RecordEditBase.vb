Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Class RecordEditBase
    Inherits CmsPage
    '--------------------------------------------------------------------------
    '将传入的参数保留为ViewState变量，便于在页面中提取和修改。
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
            ViewState("PAGE_FORMNAME") = RStr("mnuformname") '窗体名称
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
            '添加状态下当前记录ID必须置为0
            ViewState("PAGE_RECID") = 0 '是主表操作

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
    '处理第一个GET请求中的事务
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
            '若是关联表中添加记录，则必须获取主关联和输入字段的值，自动显示在输入窗体中。
            Dim hashFieldRelVal As Hashtable = Nothing 'Key：控件名称；Value：主关联和输入关联字段的值
            If lngMode = InputMode.AddInRelTable Then
                hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(pst, lngHostResID, lngHostRecID, lngResID, True)
            End If
            If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

            '无论什么状态，在页面的第一个请求中都从Request中提取需要给字段赋的初始值
            GetFieldInitValueFromRequest(pst, lngResID, hashFieldRelVal)
            '无论什么状态，在页面的第一个请求中都从菜单配置文件中提取需要给字段赋的初始值
            GetFieldInitValueFromMenuConfig(pst, lngResID, hashFieldRelVal)

            '检查当前窗体是否禁止记录锁定校验
            Dim blnCheckRecLock As Boolean = True
            If strNoCheckRecLock = "1" Then blnCheckRecLock = False

            Dim datForm As DataInputForm = FormManager.LoadForm(pst, Panel1, Nothing, lngResID, lngMode, strFormName, hashFieldRelVal, lngRecID, , , , forceRights, True, blnCheckColRights, blnCheckRecLock, lngHostResID, lngHostRecID, blnShowSaveButton, True)
            SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
            SetFocusOnTextbox(datForm.strFirstColName)
            RegisterHiddenFieldOfRecID(datForm, lngRecID)
            RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
            RegisterHiddenFieldOfCurrentRes(lngResID, lngRecID) '保存当前窗体的基本信息在界面hidden变量上
            RegisterHiddenFieldForInputForm() '注册几个输入窗体所需的Hidden变量
            RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表

            Return datForm
        End Function

        '--------------------------------------------------------------------------
        '处理POST中的命令。返回：True：退出本接口后直接退出窗体；False：退出本接口后继续之后的处理
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
                    '仅为了工作流中的Save按钮，才保留以下代码
                    'SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, SaveToDb, Nothing) '保存记录
                    'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
                    CommandSaveRecord1(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)     '点击子资源表单中的"保存"，但保存的是主表记录信息
                    blnRtn = True

                Case "delrelrec"
                    FormManager.DeleteRecordsInRelTable(pst, lngSubResID, RStr("RECID3_" & lngSubResID, Request), strFormName)
                    'CommandDelSubRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngSubResID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)    '删除子资源列表的某条记录
                    blnRtn = True

                Case "popupfresh"
                    'CommandRefreshByPopupForm(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, , blnThrowExceptionWhileError)      '跳出的子窗体保存退出时的刷新本（父）窗体操作
                    blnRtn = True

                Case "savehostrec2"
                    'CommandSaveRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)     '点击子资源表单中的"保存"，但保存的是主表记录信息
                    blnRtn = True

                Case Else
                    blnRtn = False
            End Select

            RegisterHiddenFieldForInputForm() '注册几个输入窗体所需的Hidden变量
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
                    '仅为了工作流中的Save按钮，才保留以下代码
                    'SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, SaveToDb, Nothing) '保存记录
                    'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
                    Dim savehostas As Boolean = CommandSaveRecord1(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)    '点击子资源表单中的"保存"，但保存的是主表记录信息
                    If savehostas Then
                        blnRtn = True
                    Else
                        blnRtn = False
                    End If


                Case "delrelrec"
                    FormManager.DeleteRecordsInRelTable(pst, lngSubResID, RStr("RECID3_" & lngSubResID, Request), strFormName)
                    'CommandDelSubRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngSubResID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)    '删除子资源列表的某条记录
                    blnRtn = True

                Case "popupfresh"
                    'CommandRefreshByPopupForm(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, , blnThrowExceptionWhileError)      '跳出的子窗体保存退出时的刷新本（父）窗体操作
                    blnRtn = True

                Case "savehostrec2"
                    'CommandSaveRecord(pst, Request, ViewState, Panel1, lblStatus, datForm, strCommand, lngHostResID, lngHostRecID, lngResID, lngRecID, lngMode, strFormName, forceRights, lngDepID, blnPromptNoDocUpload, blnCheckColRights, lblHeaderAction1, lnkSave, blnShowSaveButton, blnThrowExceptionWhileError)     '点击子资源表单中的"保存"，但保存的是主表记录信息
                    blnRtn = True

                Case Else
                    blnRtn = False
            End Select

            RegisterHiddenFieldForInputForm() '注册几个输入窗体所需的Hidden变量
            Return blnRtn
        End Function
        '--------------------------------------------------------------------------
        '点击子资源表单中的"保存"，但保存的是主表记录信息
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
            Dim hashUICtrlValue As New Hashtable '存放界面尚所有控件值，仅为了在保存出错时能将所有值重新显示在界面上。 Key：控件名称；Value：控件值
            Dim blnSuccess As Boolean = False

            Try
                Dim blnHasDocUploaded As Boolean = False
                lngRecID = FormManager.SaveRecords1(pst, Request, ViewState, Panel1, lngHostResID, lngResID, lngRecID, hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, lngMode, strFormName, lngDepID)
                If lngMode = InputMode.AddInHostTable Then
                    '是添加状态，且第一次点击“保存”，保存成功后需要修改状态为“修改”
                    If Not lblStatus Is Nothing Then lblStatus.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                    ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                    lngMode = InputMode.EditInHostTable
                    ViewState("PAGE_RECID") = lngRecID
                ElseIf lngMode = InputMode.AddInRelTable Then
                    '是添加状态，且第一次点击“保存”，保存成功后需要修改状态为“修改”
                    If Not lblStatus Is Nothing Then lblStatus.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                    ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                    lngMode = InputMode.EditInRelTable
                    ViewState("PAGE_RECID") = lngRecID
                End If

                If pst.GetDataRes(lngResID).ResTableType = "DOC" And blnHasDocUploaded = False Then
                    '如果是文档表且添加记录时没有上传文档，则提示其之后添加文档的方法
                    If blnPromptNoDocUpload Then strErrMsg = "您没有上传文档，以后请用文档操作菜单中的签入文档来为此条记录添加文档！"
                End If

                blnSuccess = True
            Catch ex As Exception
                blnSuccess = False
                SLog.Err("保存主记录失败", ex)
                strErrMsg = ex.Message

                If blnThrowExceptionWhileError Then
                    Throw New CmsException("保存主记录失败！" & ex.Message)
                End If
            End Try

            Try
                '无论成功或失败，都需要刷新界面显示
                datForm = FormManager.LoadForm(pst, Panel1, Nothing, lngResID, lngMode, strFormName, hashUICtrlValue, lngRecID, , , , forceRights, (Not blnSuccess), blnCheckColRights, , lngHostResID, lngHostRecID, blnShowSaveButton)
                SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                SetFocusOnTextbox(datForm.strFirstColName)
                RegisterHiddenFieldOfRecID(datForm, lngRecID)
                RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                RegisterHiddenFieldOfCurrentRes(lngResID, lngRecID) '保存当前窗体的基本信息在界面hidden变量上
                RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
            Catch ex As Exception
                SLog.Err("保存主记录成功后刷新页面失败", ex)
                If strErrMsg = "" Then strErrMsg = ex.Message

                If blnThrowExceptionWhileError Then
                    Throw New CmsException("保存主记录成功后刷新页面失败！" & ex.Message)
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
            Dim hashUICtrlValue As New Hashtable '存放界面尚所有控件值，仅为了在保存出错时能将所有值重新显示在界面上。 Key：控件名称；Value：控件值
            Dim blnSuccess As Boolean = False


            Dim blnHasDocUploaded As Boolean = False
            lngRecID = FormManager.SaveRecords1(pst, Request, ViewState, Panel1, lngHostResID, lngResID, lngRecID, hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, lngMode, strFormName, lngDepID)
            If lngRecID = 0 Then
                Return False
            Else

                Try

                    If lngMode = InputMode.AddInHostTable Then
                        '是添加状态，且第一次点击“保存”，保存成功后需要修改状态为“修改”
                        If Not lblStatus Is Nothing Then lblStatus.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                        ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                        lngMode = InputMode.EditInHostTable
                        ViewState("PAGE_RECID") = lngRecID
                    ElseIf lngMode = InputMode.AddInRelTable Then
                        '是添加状态，且第一次点击“保存”，保存成功后需要修改状态为“修改”
                        If Not lblStatus Is Nothing Then lblStatus.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                        ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                        lngMode = InputMode.EditInRelTable
                        ViewState("PAGE_RECID") = lngRecID
                    End If

                    If pst.GetDataRes(lngResID).ResTableType = "DOC" And blnHasDocUploaded = False Then
                        '如果是文档表且添加记录时没有上传文档，则提示其之后添加文档的方法
                        If blnPromptNoDocUpload Then strErrMsg = "您没有上传文档，以后请用文档操作菜单中的签入文档来为此条记录添加文档！"
                    End If

                    blnSuccess = True
                Catch ex As Exception
                    blnSuccess = False
                    SLog.Err("保存主记录失败", ex)
                    strErrMsg = ex.Message

                    If blnThrowExceptionWhileError Then
                        Throw New CmsException("保存主记录失败！" & ex.Message)
                    End If
                End Try

                Try
                    '无论成功或失败，都需要刷新界面显示
                    datForm = FormManager.LoadForm(pst, Panel1, Nothing, lngResID, lngMode, strFormName, hashUICtrlValue, lngRecID, , , , forceRights, (Not blnSuccess), blnCheckColRights, , lngHostResID, lngHostRecID, blnShowSaveButton)
                    SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                    SetFocusOnTextbox(datForm.strFirstColName)
                    RegisterHiddenFieldOfRecID(datForm, lngRecID)
                    RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                    RegisterHiddenFieldOfCurrentRes(lngResID, lngRecID) '保存当前窗体的基本信息在界面hidden变量上
                    RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
                Catch ex As Exception
                    SLog.Err("保存主记录成功后刷新页面失败", ex)
                    If strErrMsg = "" Then strErrMsg = ex.Message

                    If blnThrowExceptionWhileError Then
                        Throw New CmsException("保存主记录成功后刷新页面失败！" & ex.Message)
                    End If
                End Try

                If strErrMsg <> "" Then PromptMsg(strErrMsg, Nothing, True)
                Return True
            End If

        End Function


        '--------------------------------------------------------------------------
        '注册当前资源的记录ID在页面上的hidden变量
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldOfRecID(ByRef datForm As DataInputForm, ByVal lngRecID As Long)
            RegisterHiddenField("RECID", CStr(lngRecID))
        End Sub

        '--------------------------------------------------------------------------
        '注册子表在页面上的hidden变量
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
        '保存当前窗体的基本信息在界面hidden变量上
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldOfCurrentRes(ByVal lngResID As Long, ByVal lngRecID As Long)
            RegisterHiddenField("FORMDATA_RESID", CStr(lngResID))
            RegisterHiddenField("FORMDATA_RECID", CStr(lngRecID))
        End Sub

        '--------------------------------------------------------------------------
        '注册几个输入窗体所需的Hidden变量
        '--------------------------------------------------------------------------
        Protected Sub RegisterHiddenFieldForInputForm()
            RegisterHiddenField("isfrom", "")
            RegisterHiddenField("subtabresid", "")
        End Sub

        '--------------------------------------------------------------------------
        '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
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
        '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
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
        '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
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
        '设置“保存”等按钮的状态
        '----------------------------------------------------------------
        Protected Sub SetButtonStatus( _
            ByRef pst As CmsPassport, _
            ByVal lngMode As InputMode, _
            ByVal lblHeaderAction1 As System.Web.UI.WebControls.Label, _
            ByVal lnkSave As System.Web.UI.WebControls.LinkButton _
            )
            If lngMode = InputMode.AddInHostTable OrElse lngMode = InputMode.AddInRelTable Then
                If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_ADD")
                If Not lnkSave Is Nothing Then lnkSave.Enabled = True
            ElseIf lngMode = InputMode.EditInHostTable OrElse lngMode = InputMode.EditInRelTable Then
                If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                If Not lnkSave Is Nothing Then lnkSave.Enabled = True
            ElseIf lngMode = InputMode.ViewInHostTable OrElse lngMode = InputMode.ViewInRelTable Then
                If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_VIEW")
                If Not lnkSave Is Nothing Then lnkSave.Enabled = False '查阅状态下Disable“保存”按钮
            End If
        End Sub

        '----------------------------------------------------------------
        '判断来源：主表、关联子表
        '----------------------------------------------------------------
        Protected Function IsFromHost(ByVal lngMode As InputMode) As Integer
            If lngMode = InputMode.AddInHostTable OrElse lngMode = InputMode.EditInHostTable OrElse lngMode = InputMode.PrintInHostTable OrElse lngMode = InputMode.ViewInHostTable Then
                Return ResourceLocation.HostTable
            Else
                Return ResourceLocation.RelTable
            End If
        End Function

        '--------------------------------------------------------------------------
        '保存记录
        '--------------------------------------------------------------------------
        Protected Function SaveRecordOnUI(ByRef pst As CmsPassport, ByRef Panel1 As System.Web.UI.WebControls.Panel, ByRef lblHeaderAction1 As System.Web.UI.WebControls.Label, ByRef lnkSave As System.Web.UI.WebControls.LinkButton, ByVal blnShowSaveButton As Boolean, ByRef forceRights As ForceRightsInForm) As Long
            Dim blnSaveSuccess As Boolean = True '增加记录时的错误处理，必须保留原输入的值
            Dim hashUICtrlValue As New Hashtable '存放界面尚所有控件值，仅为了在保存出错时能将所有值重新显示在界面上。 Key：控件名称；Value：控件值
            Dim strPromptMsg As String = ""
            Dim lngRecIDAfterAdd As Long = 0 '返回当前编辑的记录ID

            '--------------------------------------------------------------------------
            '第一步：开始保存记录
            Try
                '--------------------------------------------------------------------------
                '保存当前资源的当前记录
                Dim blnHasDocUploaded As Boolean = False '保存记录时返回当前请求中有无文档流保存
                Dim datForm As DataInputForm = CType(Session("PAGE_EDITADV_DATAFORM"), DataInputForm)
                lngRecIDAfterAdd = FormManager.SaveRecords(pst, Request, ViewState, Panel1, VLng("PAGE_HOSTRESID"), VLng("PAGE_RESID"), VLng("PAGE_RECID"), hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, CType(VLng("PAGE_INMODE"), InputMode), VStr("PAGE_FORMNAME"), VLng("PAGE_DEPID"))
                '--------------------------------------------------------------------------

                '如果是文档表且添加记录时没有上传文档，则提示其之后添加文档的方法
                If pst.GetDataRes(VLng("PAGE_RESID")).ResTableType = "DOC" And blnHasDocUploaded = False And (CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInHostTable OrElse CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInRelTable) Then
                    strPromptMsg = CmsMessage.GetMsg(CmsPass, "TIP_ADDDOC_LATER")
                End If

                blnSaveSuccess = True
            Catch ex As CmsException
                blnSaveSuccess = False
                strPromptMsg = ex.Message
                SLog.Err("保存记录失败！" & ex.Message)
            Catch ex As Exception
                blnSaveSuccess = False
                strPromptMsg = "保存记录异常失败，请稍后再试！"
                SLog.Err(strPromptMsg, ex)
            End Try
            '-------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------
            '第二步：记录保存时需要同时保存菜单配置文件中定义的其它资源的信息
            If blnSaveSuccess Then
                Try
                    SaveMenuSectionResources(pst, VLng("PAGE_RESID"), lngRecIDAfterAdd)
                Catch ex As Exception
                    strPromptMsg = "记录保存成功，但当前菜单定义的其它资源写入操作失败！"
                    SLog.Err(strPromptMsg, ex)
                End Try
            End If
            '-------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------
            '第三步：保存成功或失败后的控制
            If blnSaveSuccess Then  '成功保存记录后需要修改当前编辑状态为：修改
                Try
                    ViewState("PAGE_RECID") = lngRecIDAfterAdd
                    If Not lblHeaderAction1 Is Nothing Then lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                    If CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInHostTable Then
                        ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                    ElseIf CType(VLng("PAGE_INMODE"), InputMode) = InputMode.AddInRelTable Then
                        ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                    End If

                    '从数据库中重新提取当前保存成功的记录值并显示
                    Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                    Dim datForm As DataInputForm = FormManager.LoadForm(pst, Panel1, Nothing, VLng("PAGE_RESID"), lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , forceRights, False, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), blnShowSaveButton)
                    SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                    ViewState("PAGE_INMODE") = lngMode
                    Session("PAGE_EDITADV_DATAFORM") = datForm
                    RegisterHiddenFieldOfRecID(datForm, VLng("PAGE_RECID"))
                    RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                    RegisterHiddenFieldOfCurrentRes(VLng("PAGE_RESID"), lngRecIDAfterAdd) '保存当前窗体的基本信息在界面hidden变量上
                    RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
                    SetFocusOnTextbox(datForm.strFirstColName)
                Catch ex As CmsException
                    '不要显示错误，因输入框变为只读 也会导致显示失败
                    'strPromptMsg = "保存记录成功，但显示窗体失败！" & ex.Message
                    SLog.Err("保存记录成功，但显示窗体失败！" & ex.Message)
                Catch ex As Exception
                    '不要显示错误，因输入框变为只读 也会导致显示失败
                    'strPromptMsg = "保存记录成功，但显示窗体失败！"
                    SLog.Err("保存记录成功，但显示窗体失败！", ex)
                End Try
                If strPromptMsg.Trim = "" Then
                    PromptMsg("保存成功！")
                Else
                    PromptMsg(strPromptMsg)
                End If

                Return lngRecIDAfterAdd
            Else '增加记录时的错误处理，必须保留原输入的值
                Try
                    Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                    Dim datForm As DataInputForm = FormManager.LoadForm(pst, Panel1, Nothing, VLng("PAGE_RESID"), lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , forceRights, True, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), blnShowSaveButton)
                    SetButtonStatus(pst, lngMode, lblHeaderAction1, lnkSave)
                    Session("PAGE_EDITADV_DATAFORM") = datForm
                    RegisterHiddenFieldOfRecID(datForm, VLng("PAGE_RECID"))
                    RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                    RegisterHiddenFieldOfCurrentRes(VLng("PAGE_RESID"), lngRecIDAfterAdd) '保存当前窗体的基本信息在界面hidden变量上
                    RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
                    SetFocusOnTextbox(datForm.strFirstColName)
                Catch ex As Exception
                    '已经提示错误信息，不必再提示
                    SLog.Err("保存记录失败后重新Load窗体又失败！", ex)
                End Try

                PromptMsg(strPromptMsg)
                Return lngRecIDAfterAdd
            End If

            If strPromptMsg.Trim() = "" Then
                PromptMsg("保存成功！")
            End If
            Return lngRecIDAfterAdd
            '-------------------------------------------------------------------------------
        End Function

        '----------------------------------------------------------------
        '记录保存时需要同时保存菜单配置文件中定义的其它资源的信息
        '----------------------------------------------------------------
        Private Sub SaveMenuSectionResources(ByRef pst As CmsPassport, ByVal lngResID As Long, ByVal lngRecID As Long)
            '获取刚保存的记录的完整信息
            If lngResID = 0 OrElse lngRecID = 0 Then Return
            Dim datRes As DataResource = pst.GetDataRes(lngResID)

            Dim hashFieldVal As Hashtable = ResFactory.TableService(datRes.ResTableType).GetRecordDataByHashtable(pst, lngResID, lngRecID)
            Try
                Dim strOneMenuSection As String = RStr("MenuKey")
                If strOneMenuSection <> "" Then
                    Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(pst.Employee.Language, CmsMenuType.Extension)
                    '提取菜单的字典列表
                    Dim hashMenuDict As Hashtable = CmsFrmContentFlow.GetMenuDiction(pst, datSvc, lngResID)
                    Dim alistFlowAct As ArrayList = CmsFrmContentFlow.GetColSet(pst, datSvc, strOneMenuSection, hashMenuDict, hashFieldVal)
                    Dim datColSet As DataColSet
                    For Each datColSet In alistFlowAct
                        If datColSet.strRESTYPE = "0" Then '0：当前资源
                        ElseIf datColSet.strRESTYPE = "1" Then '1：是当前资源的父资源
                        ElseIf datColSet.strRESTYPE = "2" Then '2：是当前资源的子资源
                            CmsFrmContentFlow.SetColumnOfSubResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                        ElseIf datColSet.strRESTYPE = "3" Then '3：是其它资源
                            CmsFrmContentFlow.SetColumnOfOtherResource(pst, lngResID, hashFieldVal, False, datColSet, hashMenuDict)
                        End If
                    Next

                    'If datSvc.GetSecAttr(strOneMenuSection, "ROLLBACK_ALLOT") = "1" Then '回滚分帐操作
                    '    BizAllot.AllotRollback(pst, VLng("PAGE_RESID"), hashFieldVal)
                    'End If
                End If
            Catch ex As Exception
                SLog.Err("编辑输入窗体时执行菜单指令时异常出错", ex)
            End Try
        End Sub

        '----------------------------------------------------------
        '获取当前请求GET或者POST中的指定参数名的值
        '----------------------------------------------------------
        Public Function VImd(ByVal strParamName As String) As InputMode
            Return CType(VLng(strParamName, ViewState), InputMode)
        End Function
    End Class

End Namespace
