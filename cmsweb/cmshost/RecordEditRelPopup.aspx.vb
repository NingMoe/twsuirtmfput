'---------------------------------------------------------------------------
'本页面可能同时打开多个，所有不能有Session变量，除非此Session变量的名称带有本页面的唯一标志
'
'注意1：本窗体的窗体名称一定是：默认窗体。因为设计窗体时拖入父窗体中时没有名称选择
'---------------------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform
'Imports Unionsoft.WebControls.Uploader  'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Cms.Web


Partial Class RecordEditRelPopup
    Inherits Cms.Web.RecordEditBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Panel1 As Panel = Nothing

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            PageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改
            PageInitialize() '初始化页面

            Dim lngCurResID As Long = VLng("PAGE_RESID")
            If Not IsPostBack Then
                Dim datForm As DataInputForm = PageDealFirstRequest(CmsPass, Panel1, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, VLng("PAGE_RECID"), VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), Nothing, True, VStr("PAGE_NOCHECK_RECLOCK"), lblHeaderAction1, lnkSave, False)      '处理Postback之后的事务处理。
                Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
                'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
            Else
                PageDealPostBack(CmsPass, Request, ViewState, Panel1, lblHeaderAction1, CType(Session("PAGE_POPREL_DATAFORM" & lngCurResID), DataInputForm), RStr("isfrom"), VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, VLng("PAGE_RECID"), RLng("subtabresid"), VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), Nothing, VLng("PAGE_DEPID"), True, True, lblHeaderAction1, lnkSave, False, False)
                'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
            End If
        Catch ex As CmsExceptionCustForm
            Dim strUrl As String = ex.Message
            Server.Transfer(strUrl, True)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    '--------------------------------------------------------------------------
    '初始化页面
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        '初始化Panel
        Panel1 = New Panel
        Panel1.ID = "Panel1"
        Panel1.EnableViewState = False
        Dim strStyle As String = "Z-INDEX: 101; LEFT: 4px; POSITION: absolute; TOP: 28px"
        Panel1.BorderColor = Color.FromName("#D7E7FF")
        Panel1.Attributes.Add("style", strStyle)
        Panel1.BorderWidth = Unit.Pixel(1)
        Me.Form1.Controls.Add(Panel1)

        '多语言控制
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


        '输入窗体提交时需要检查时间、数字项是否有效
        lnkSave.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")
        '完成操作后关闭窗体
        lnkExit.Attributes.Add("onClick", "javascript:ClosePopupEditForm()")

        '显示标题，表示当前状态（添加、修改、查阅、...）
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Dim strResName As String = CmsPass.GetDataRes(lngCurResID).ResName
        lblHeader.Text = strResName

        'Load资源扩展功能菜单，一般是项目客户的定制开发
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
        '第一步：开始保存记录
        Dim blnSaveSuccess As Boolean = True '增加记录时的错误处理，必须保留原输入的值
        Dim hashUICtrlValue As New Hashtable '存放界面尚所有控件值，仅为了在保存出错时能将所有值重新显示在界面上。 Key：控件名称；Value：控件值
        Dim strPromptMsg As String = ""
        Dim lngRecIDAfterAdd As Long = 0 '返回当前编辑的记录ID
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Try
            Dim blnHasDocUploaded As Boolean = False '保存记录时返回当前请求中有无文档流保存
            Dim datForm As DataInputForm = CType(Session("PAGE_POPREL_DATAFORM" & lngCurResID), DataInputForm)
            lngRecIDAfterAdd = FormManager.SaveRecords(CmsPass, Request, ViewState, Panel1, VLng("PAGE_HOSTRESID"), lngCurResID, VLng("PAGE_RECID"), hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), VLng("PAGE_DEPID"))

            blnSaveSuccess = True
        Catch ex As CmsException
            blnSaveSuccess = False
            strPromptMsg = ex.Message
            SLog.Err("保存记录失败！" & ex.Message)
        Catch ex As Exception
            blnSaveSuccess = False
            strPromptMsg = "保存记录异常失败，请稍后再试！"
            SLog.Err("保存记录失败！", ex)
        End Try
        '-------------------------------------------------------------------------------

        '保存成功或失败后显示修改记录的窗体
        ShowFormOfEdit(blnSaveSuccess, lngRecIDAfterAdd, hashUICtrlValue, strPromptMsg)

        'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
    End Sub

    Private Sub lbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnNew.Click
        '显示新建记录的窗体
        ShowFormOfAddNew()

        'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
    End Sub

    Private Sub lbtnSaveNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSaveNew.Click
        '--------------------------------------------------------------------------
        '第一步：开始保存记录
        Dim blnSaveSuccess As Boolean = True '增加记录时的错误处理，必须保留原输入的值
        Dim hashUICtrlValue As New Hashtable '存放界面尚所有控件值，仅为了在保存出错时能将所有值重新显示在界面上。 Key：控件名称；Value：控件值
        Dim strPromptMsg As String = ""
        Dim lngRecIDAfterAdd As Long = 0 '返回当前编辑的记录ID
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Try
            Dim blnHasDocUploaded As Boolean = False '保存记录时返回当前请求中有无文档流保存
            Dim datForm As DataInputForm = CType(Session("PAGE_POPREL_DATAFORM" & lngCurResID), DataInputForm)
            lngRecIDAfterAdd = FormManager.SaveRecords(CmsPass, Request, ViewState, Panel1, VLng("PAGE_HOSTRESID"), lngCurResID, VLng("PAGE_RECID"), hashUICtrlValue, blnHasDocUploaded, datForm.hashUICtls, datForm.hashACodeValue, datForm.hashFieldRelVal, VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), VLng("PAGE_DEPID"))
            ViewState("PAGE_RECID") = lngRecIDAfterAdd

            blnSaveSuccess = True
        Catch ex As CmsException
            blnSaveSuccess = False
            strPromptMsg = ex.Message
            SLog.Err("保存记录失败！" & ex.Message)
        Catch ex As Exception
            blnSaveSuccess = False
            strPromptMsg = "保存记录异常失败，请稍后再试！"
            SLog.Err("保存记录失败！", ex)
        End Try
        '-------------------------------------------------------------------------------

        If blnSaveSuccess = True Then
            '成功保存记录后显示新建记录的窗体
            ShowFormOfAddNew()
        Else
            '保存记录失败后仍然显示修改记录的窗体
            ShowFormOfEdit(blnSaveSuccess, lngRecIDAfterAdd, hashUICtrlValue, strPromptMsg)
        End If

        'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
    End Sub

    '--------------------------------------------------------------------------
    '显示修改记录的窗体
    '--------------------------------------------------------------------------
    Private Function ShowFormOfEdit(ByVal blnSaveSuccess As Boolean, ByVal lngRecIDAfterAdd As Long, ByRef hashUICtrlValue As Hashtable, ByVal strPromptMsg As String) As Boolean
        Dim lngCurResID As Long = VLng("PAGE_RESID")

        '-------------------------------------------------------------------------------
        '第二步：保存成功或失败后的控制
        If blnSaveSuccess Then  '成功保存记录后需要修改当前编辑状态为：修改
            Try
                ViewState("PAGE_RECID") = lngRecIDAfterAdd
                lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_EDIT")
                If VImd("PAGE_INMODE") = InputMode.AddInHostTable Then
                    ViewState("PAGE_INMODE") = InputMode.EditInHostTable
                ElseIf VImd("PAGE_INMODE") = InputMode.AddInRelTable Then
                    ViewState("PAGE_INMODE") = InputMode.EditInRelTable
                End If

                '从数据库中重新提取当前保存成功的记录值并显示
                Dim lngMode As InputMode = VImd("PAGE_INMODE")
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, lngMode, VStr("PAGE_FORMNAME"), hashUICtrlValue, VLng("PAGE_RECID"), , , , , False, True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
                SetButtonStatus(CmsPass, lngMode, lblHeaderAction1, lnkSave)
                ViewState("PAGE_INMODE") = lngMode
                Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
                RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                RegisterHiddenFieldOfCurrentRes(lngCurResID, lngRecIDAfterAdd) '保存当前窗体的基本信息在界面hidden变量上
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
            PromptMsg(strPromptMsg)
            Return blnSaveSuccess
        Else '增加记录时的错误处理，必须保留原输入的值
            Try
                '-------------------------------------------------------------------------------------
                '若是关联表中添加记录，则必须获取主关联和输入字段的值，自动显示在输入窗体中。
                Dim hashFieldRelVal As Hashtable = Nothing 'Key：控件名称；Value：主关联和输入关联字段的值
                Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                If lngMode = InputMode.AddInRelTable Then
                    hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, True)
                End If
                If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

                '无论什么状态，在页面的第一个请求中都从Request中提取需要给字段赋的初始值
                GetFieldInitValueFromRequest(CmsPass, lngCurResID, hashFieldRelVal)
                '无论什么状态，在页面的第一个请求中都从菜单配置文件中提取需要给字段赋的初始值
                GetFieldInitValueFromMenuConfig(CmsPass, lngCurResID, hashFieldRelVal)

                '保存关联表信息
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
                RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                RegisterHiddenFieldOfCurrentRes(lngCurResID, lngRecIDAfterAdd) '保存当前窗体的基本信息在界面hidden变量上
                RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
                SetFocusOnTextbox(datForm.strFirstColName)
            Catch ex As Exception
                '已经提示错误信息，不必再提示
                SLog.Err("保存记录失败后重新Load窗体又失败！", ex)
            End Try

            PromptMsg(strPromptMsg)
            Return blnSaveSuccess
        End If
        '-------------------------------------------------------------------------------
    End Function

    '--------------------------------------------------------------------------
    '显示新建记录的窗体
    '--------------------------------------------------------------------------
    Private Function ShowFormOfAddNew() As Boolean
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Try
            '调整当前状态
            lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_ADD")
            ViewState("PAGE_INMODE") = InputMode.AddInRelTable

            '检查是否在新建记录时保留界面的信息
            Dim lngPrevRecID As Long = VLng("PAGE_RECID")
            ViewState("PAGE_RECID") = 0
            Dim blnKeepOldVal As Boolean = CTableForm.IsKeepOldValue(CmsPass, lngCurResID, VStr("PAGE_FORMNAME"))
            If blnKeepOldVal = False Then lngPrevRecID = 0

            '若是关联表中添加记录，则必须获取主关联和输入字段的值，自动显示在输入窗体中。
            Dim hashFieldRelVal As Hashtable = Nothing 'Key：控件名称；Value：主关联和输入关联字段的值
            Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
            If lngMode = InputMode.AddInRelTable Then
                hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, True)
            End If
            If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

            '无论什么状态，在页面的第一个请求中都从Request中提取需要给字段赋的初始值
            GetFieldInitValueFromRequest(CmsPass, lngCurResID, hashFieldRelVal)
            '无论什么状态，在页面的第一个请求中都从菜单配置文件中提取需要给字段赋的初始值
            GetFieldInitValueFromMenuConfig(CmsPass, lngCurResID, hashFieldRelVal)

            '检查当前窗体是否禁止记录锁定校验
            Dim blnCheckRecLock As Boolean = True
            If VStr("PAGE_NOCHECK_RECLOCK") = "1" Then blnCheckRecLock = False

            Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, lngMode, VStr("PAGE_FORMNAME"), hashFieldRelVal, lngPrevRecID, , , , , , True, blnCheckRecLock, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
            Session("PAGE_POPREL_DATAFORM" & lngCurResID) = datForm
            SetButtonStatus(CmsPass, lngMode, lblHeaderAction1, lnkSave)
            SetFocusOnTextbox(datForm.strFirstColName)
            RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
            RegisterHiddenFieldOfCurrentRes(lngCurResID, 0)  '保存当前窗体的基本信息在界面hidden变量上
            RegisterHiddenFieldForInputForm() '注册几个输入窗体所需的Hidden变量
            RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表

            Return True
        Catch ex As Exception
                PromptMsg(CmsMessage.GetMsg(CmsPass, "NEW_RECORD"), ex, True)
            Return False
        End Try
    End Function
End Class

End Namespace
