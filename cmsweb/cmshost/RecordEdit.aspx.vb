'===========================================================================
' 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
' 类名已更改，且类已修改为从文件“App_Code\Migrated\cmshost\Stub_recordedit_aspx_vb.vb”的抽象基类 
' 继承。
' 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
' 代码隐藏页。
' 关联的内容页“cmshost\recordedit.aspx”也已修改，以引用新的类名。
' 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
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
                '必须在此处处理定制窗体信息 CHENYU 2010/6/1
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
    '初始化页面
    '--------------------------------------------------------------------------
    Private Sub PageInitialize()
        '初始化Panel
        Panel1 = New Panel
        Panel1.ID = "Panel1"
        Panel1.EnableViewState = False
            Dim strStyle As String = "Z-INDEX: 101;POSITION: absolute; LEFT: 4px; POSITION: absolute; TOP: 28px"
        Panel1.BorderColor = Color.FromName("#D7E7FF")
        Panel1.Attributes.Add("style", strStyle)
        Panel1.BorderWidth = Unit.Pixel(1)
            Me.Form1.Controls.Add(Panel1)
            
        '多语言控制
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
        If lngMode = InputMode.AddInHostTable Then Session("CMS_HOSTTABLE_RECID") = "" '清空当前选中记录ID

        '输入窗体提交时需要检查时间、数字项是否有效
        lnkSave.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")

        '显示标题，表示当前状态（添加、修改、查阅、...）
        Dim lngCurResID As Long = VLng("PAGE_RESID")
        Dim strResName As String = ResFactory.ResService.GetResName(CmsPass, lngCurResID)
        lblHeader.Text = strResName

        '只有最初进来的是添加状态，才显示“新建”
        If VLng("PAGE_INMODE_BAK") = InputMode.AddInHostTable Or VLng("PAGE_INMODE_BAK") = InputMode.AddInRelTable Then
            lbtnNew.Enabled = True
        Else
            lbtnNew.Enabled = False
        End If

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
            ' Dim s As String = lnkSave.Attributes("onClick")
            lnkSave.Attributes.Add("OnClientClick", "if( CheckValue(self.document.forms(0))){ return true;} else{ return false;}")
        End Sub

        'Private Sub lbtnPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnPrint.Load
        '    'window.open('/cmsweb/cmshost/RecordPrintSimple.aspx?timeid=126473348&MenuSection=MENU_RECORD&MenuKey=MENU_RECPRINT&MNURESLOCATE=1&cmsaction=&mnuformresid=&mnuhostresid=" + VLng("PAGE_HOSTRESID").ToString + "&mnuhostrecid=" + VLng("PAGE_HOSTRECID").ToString + "&mnurecid=" + VLng("PAGE_RECID").ToString + "&mnuresid=" + VLng("PAGE_RESID").ToString + "&mnuformname=')
        '    lbtnPrint.Attributes.Add("OnClientClick", "return false;")
        'End Sub
        Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
            SaveRecordOnUI(CmsPass, Panel1, lblHeaderAction1, lnkSave, False, Nothing)  '保存记录 

            'Response.Write("<script>try{window.opener.OpenLoadbbb('" + CType(RLng("mnuinmode"), Integer).ToString + "','" + VLng("PAGE_RESID").ToString + "');}catch (e){}</script>") '仅适用于成都华润项目  tq  edit  2012-03-21
        End Sub

       









        Private Sub lbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnNew.Click
            Try
                Dim lngCurResID As Long = VLng("PAGE_RESID")
                Session("PAGE_EDITADV_DATAFORM") = Nothing

                '------------------------------------------------------------------------
                '调整当前状态
                lblHeaderAction1.Text = "－" & CmsMessage.GetUI(CmsPass, "TITLE_RECORD_STATUS_ADD")
                ViewState("PAGE_INMODE") = VLng("PAGE_INMODE_BAK") '恢复最初进来的编辑模式
                Dim lngMode As InputMode = CType(VLng("PAGE_INMODE"), InputMode)
                If lngMode = InputMode.EditInHostTable Then
                    lngMode = InputMode.AddInHostTable
                ElseIf lngMode = InputMode.EditInRelTable Then
                    lngMode = InputMode.AddInRelTable
                End If
                '------------------------------------------------------------------------

                '------------------------------------------------------------------------
                '判断是否在新增记录的界面上保留之前编辑记录的值
                Dim lngPrevRecID As Long = VLng("PAGE_RECID")
                ViewState("PAGE_RECID") = 0
                Dim blnKeepOldVal As Boolean = CTableForm.IsKeepOldValue(CmsPass, lngCurResID, VStr("PAGE_FORMNAME"))
                If blnKeepOldVal = False Then lngPrevRecID = 0

                Dim hashFieldRelVal As Hashtable = Nothing 'Key：控件名称；Value：主关联和输入关联字段的值
                If lngMode = InputMode.AddInRelTable Then
                    hashFieldRelVal = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), lngCurResID, True)
                End If
                If hashFieldRelVal Is Nothing Then hashFieldRelVal = New Hashtable

                '无论什么状态，在页面的第一个请求中都从Request中提取需要给字段赋的初始值
                GetFieldInitValueFromRequest(CmsPass, lngCurResID, hashFieldRelVal)
                '无论什么状态，在页面的第一个请求中都从菜单配置文件中提取需要给字段赋的初始值
                GetFieldInitValueFromMenuConfig(CmsPass, lngCurResID, hashFieldRelVal)
                '------------------------------------------------------------------------

                '------------------------------------------------------------------------
                'Load空表单
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, Panel1, Nothing, lngCurResID, VImd("PAGE_INMODE"), VStr("PAGE_FORMNAME"), hashFieldRelVal, lngPrevRecID, , , , , , True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
                Session("PAGE_EDITADV_DATAFORM") = datForm
                SetFocusOnTextbox(datForm.strFirstColName)
                RegisterHiddenFieldOfSubTableRecID(datForm) '注册子表在页面上的hidden变量
                RegisterHiddenFieldOfCurrentRes(lngCurResID, 0) '保存当前窗体的基本信息在界面hidden变量上
                RegisterCmsScripts(datForm) '需要在当前窗体中调用RegisterStartupScript注册的Javascript方法列表
                '------------------------------------------------------------------------

                'InitialPanel(Panel1) '设置3个Panel（标题、内容、脚注）的属性信息
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
