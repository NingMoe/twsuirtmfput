Option Strict On
Option Explicit On 

Imports System.Drawing
Imports System.drawing.imaging
Imports System.drawing.drawing2d
Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement
Imports System.Xml
Imports System.Text


Namespace Unionsoft.Cms.Web


'2010/5/14 CHENYU 移除关联锁定功能

Partial Class ResourceFrmContent
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents panelResMain As System.Web.UI.WebControls.Panel
    Protected WithEvents chkAdvanceOptDistributed As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkAdvanceOpt As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtResourceName As System.Web.UI.WebControls.TextBox
    Protected WithEvents panelResAdv As System.Web.UI.WebControls.Panel
    Protected WithEvents chkVerctrlEnable As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlResourceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents panelResBtns As System.Web.UI.WebControls.Panel
    Protected WithEvents btnCancle As System.Web.UI.WebControls.Button
    Protected WithEvents btnReturn As System.Web.UI.WebControls.Button
    Protected WithEvents Panel7 As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSaveResource As System.Web.UI.WebControls.Button
    Protected WithEvents chkShowDisable As System.Web.UI.WebControls.CheckBox
    'Protected WithEvents lblResTableType As System.Web.UI.WebControls.Label
    'Protected WithEvents lblResType As System.Web.UI.WebControls.Label
    'Protected WithEvents lbtnImportExternalTable As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnRowFilter As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnDataTemplate As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnDataTemplateDelete As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnMTableSearch As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnDictTemplate As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnBackupRes As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnExportResData As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnRestoreRes As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnGetReportSql As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnAddDocFlowResource As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnCustomizeForm As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnLockRelation As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnFormualAmongTable As System.Web.UI.WebControls.LinkButton

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private m_strBackPage As String = ""
        Protected _ResourceId As Long 

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            btnSaveResAttr.Attributes.Add("onClick", "return CheckValue(self.document.forms(0));")
        End If
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        '提取当前资源ID和部门ID
            _ResourceId = CLng(IIf(AspPage.RLng("noderesid", Request) > 0, AspPage.RLng("noderesid", Request), 0))
        Dim strDepIDOfResID As String = CStr(ResFactory.ResService.GetResDepartment(CmsPass, _ResourceId))
        Dim strDepID As String = AspPage.RStr("depid", Request)
        If strDepID = "" Then
            strDepID = strDepIDOfResID
        ElseIf strDepID <> strDepIDOfResID Then
            _ResourceId = 0
        End If
        ViewState("PAGE_DEPID") = CLng(strDepID)
            ViewState("PAGE_RESID") = _ResourceId

            If ResFactory.ResService.GetResTableType(CmsPass, _ResourceId).Trim.ToUpper <> "DOC" Then
                Me.lblResServerUrl.Visible = False
                txtResServerUrl.Visible = False
            End If

            If ResFactory.ResService.GetResTableType(CmsPass, _ResourceId).Trim.ToUpper <> "" Then
                Me.lblEMPTYRESOURCETARGET.Visible = False
                Me.lblEMPTYRESOURCEURL.Visible = False
                Me.txtEMPTYRESOURCETARGET.Visible = False
                Me.txtEMPTYRESOURCEURL.Visible = False
                Me.chkEMPTYRESOURCETARGET.Visible = False
            End If

            ViewState("IsCreateRigth") = False
            If CmsPass.EmpIsSysAdmin = True Then
                ViewState("IsCreateRigth") = True
            Else
                If OrgFactory.DepDriver.GetDepAdmin(CmsPass, VLng("PAGE_DEPID"), True).Trim = CmsPass.Employee.ID.Trim Then ViewState("IsCreateRigth") = True
            End If
        End Sub

    Protected Overrides Sub CmsPageInitialize()
        'If CmsPass.EmpIsSysAdmin = False And CmsPass.EmpIsDepAdmin = False Then '只有系统管理员和部门管理员能进入资源管理
        '    Response.Redirect("/cmsweb/Logout.aspx", True)
        '    Return
        'End If

        VerifySystemLicense() '验证系统是否有效

        'm_strBackPage = "/cmsweb/adminres/ResourceFrmContent.aspx?depid=" & VLng("PAGE_DEPID") & "&noderesid=" & VLng("PAGE_RESID")
        m_strBackPage = "/cmsweb/adminres/ResourceFrmContent.aspx?noderesid=" & VLng("PAGE_RESID")

        '系统实施用的批量删除资源数据功能
        btnDelAllData.Visible = False
        btnDelDataKeep10Day.Visible = False
        btnDelDataKeep30Day.Visible = False
        If CmsPass.EmpIsSysAdmin = True AndAlso CmsConfig.GetConfig.GetInt("SYS_CONFIG", "BATCH_DEL_RESDATA") = 1 Then
            btnDelAllData.Visible = True
            btnDelDataKeep10Day.Visible = True
            btnDelDataKeep30Day.Visible = True
            btnDelAllData.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前资源的所有数据吗？非系统实施员请务必取消退出，否则将对系统造成重大数据破坏！');")
            btnDelDataKeep10Day.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前资源的10天前的所有数据吗？非系统实施员请务必取消退出，否则将对系统造成重大数据破坏！');")
            btnDelDataKeep30Day.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要删除当前资源的30天前的所有数据吗？非系统实施员请务必取消退出，否则将对系统造成重大数据破坏！');")
        End If

        lbtnDelResource.Attributes.Add("onClick", "return CmsPrmoptConfirm('确认要删除资源吗？这是不可恢复的操作。');") '删除资源前需要确认
        'lbtnChangeToDocFlowResource.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要将当前选中的资源转换为带流程文档表的文档流资源吗？');")
        'lbtnSetGuidangFilter.Attributes.Add("onClick", "return CmsPrmoptConfirm('归档过滤可能被重复设置多个，建议添加后进入通用行过滤设置查看。确定要添加文档流的归档过滤设置吗？');")

        txtRecNum.ReadOnly = True
        txtRecNum.Attributes.Add("style", "TEXT-ALIGN: right")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        LoadSecurityLevel()
        ShowResAttributes(VLng("PAGE_RESID"))
        PrepareFuncShow(VLng("PAGE_RESID"))
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lbtnAddResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddResource.Click
        Session("CMSBP_ResourceAddMain") = CmsUrl.AppendParam(m_strBackPage, "depid=" & VStr("PAGE_DEPID"))
        Response.Redirect("/cmsweb/adminres/ResourceAddMain.aspx?mnuresid=" & VLng("PAGE_RESID") & "&depid=" & VLng("PAGE_DEPID"), False)
    End Sub

    'Private Sub lbtnAddDocFlowResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnAddDocFlowResource.Click
    '    Session("CMSBP_ResourceAddDocFlow") = CmsUrl.AppendParam(m_strBackPage, "depid=" & VStr("PAGE_DEPID"))
    '    Response.Redirect("/cmsweb/adminres/ResourceAddDocFlow.aspx?mnuresid=" & VLng("PAGE_RESID") & "&depid=" & VLng("PAGE_DEPID"), False)
        'End Sub
    Private Sub lbtnChangeToDocFlowResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnChangeToDocFlowResource.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Try
            Dim lngDepID As Long = VLng("PAGE_DEPID")
            CmsDocFlow.ChangeToFlowResource(CmsPass, lngResID, lngDepID)

            PromptMsg("文档流转换成功！")
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    'Private Sub lbtnLockRelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnLockRelation.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return

    '    Session("CMSBP_ResourceLockRelation") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/ResourceLockRelation.aspx?mnuresid=" & lngResID, False)
        'End Sub
    Private Sub lbtnEditResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnEditResource.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_ResourceEditName") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceEditName.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnDelResource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDelResource.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        '删除后返回到父资源节点
        Dim lngResPID As Long = CmsPass.GetDataRes(lngResID).ResPID

        Try
            '删除数据库中节点信息
            ResFactory.ResService.DeleteResource(CmsPass, lngResID)
        Catch ex As Exception
            PromptMsg("", ex, True)
            Return
        End Try

        '删除后返回到父资源节点
        Dim strBackPage As String = "/cmsweb/adminres/ResourceFrmContent.aspx?depid=" & VStr("PAGE_DEPID") & "&noderesid=" & lngResPID
        Response.Redirect(strBackPage, False)
    End Sub

    Private Sub lbtnMoveRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveRes.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_ResourceMove") = m_strBackPage
        Response.Redirect("/cmsweb/cmsothers/ResourceMove.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnCreateHostTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCreateHostTable.Click
        Session("RESADDTAB_RESID") = VLng("PAGE_RESID")
        Session("CMSBP_ResourceCreateTable") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceCreateTable.aspx", False)
    End Sub

    Private Sub lbtnSetRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSetRights.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Session("CMSBP_RightsSet") = m_strBackPage
        Response.Redirect("/cmsweb/cmsrights/RightsSet.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnSetHostTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSetHostTable.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_ResourceColumnSet") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceColumnSet.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnSetHostTableShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSetHostTableShow.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        'Session("COLMGR_RESID") = lngResID
        'Session("COLMGR_BACKPAGE") = m_strBackPage
        Session("CMSBP_ResourceColumnShowSet") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceColumnShowSet.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnRelatedTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRelatedTable.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_ResourceRelationList") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceRelationList.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnCommunication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCommunication.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_EmailSmsConfig") = m_strBackPage
        Response.Redirect("/cmsweb/adminsys/EmailSmsConfig.aspx?mnuresid=" & lngResID, False)
    End Sub

    'Private Sub lbtnDataTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDataTemplate.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return
    '    If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
    '        If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
    '            PromptMsg("文档流的附件资源不支持当前操作！")
    '            Return
    '        End If
    '    End If

    '    Session("CMSBP_RecordTemplateAdmin") = m_strBackPage
    '    Response.Redirect("/cmsweb/cmshost/RecordTemplateAdmin.aspx?mnuresid=" & lngResID & "&datatmpltype=0", False)
    'End Sub

    'Private Sub lbtnDataTemplateDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDataTemplateDelete.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return
    '    If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
    '        If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
    '            PromptMsg("文档流的附件资源不支持当前操作！")
    '            Return
    '        End If
    '    End If

    '    Session("CMSBP_RecordTemplateAdmin") = m_strBackPage
    '    Response.Redirect("/cmsweb/cmshost/RecordTemplateAdmin.aspx?mnuresid=" & lngResID & "&datatmpltype=1", False)
    'End Sub

    'Private Sub lbtnDictTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnDictTemplate.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return
    '    If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
    '        If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
    '            PromptMsg("文档流的附件资源不支持当前操作！")
    '            Return
    '        End If
    '    End If

    '    Session("CMSBP_DictTemplateAdmin") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/DictTemplateAdmin.aspx?mnuresid=" & lngResID, False)
    'End Sub

    Private Sub lbtnMoveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMoveup.Click
        MoveResUpDown(True)
    End Sub

    Private Sub lbtnMovedown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMovedown.Click
        MoveResUpDown(False)
    End Sub

    Private Sub lbtnInputformDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnInputformDesign.Click
            Dim lngResID As Long = VerifyAndGetResID()
            If lngResID = 0 Then Return
            ' Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)

            If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
                If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                    PromptMsg("文档流的附件资源不支持当前操作！")
                    Return
                End If
            End If
            Dim strCommandName As String = CType(sender, LinkButton).CommandName.Trim
            Session("CMSBP_" + strCommandName) = m_strBackPage
            Response.Redirect("/cmsweb/cmsform/" + strCommandName.Trim + ".aspx?mnuresid=" & lngResID & "&mnuformtype=" & FormType.InputForm & "&mnuformname=", False)
    End Sub

    'Private Sub lbtnCustomizeForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCustomizeForm.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return

    '    Session("CMSBP_FormCustomize") = m_strBackPage
    '    Response.Redirect("/cmsweb/cmsform/FormCustomize.aspx?mnuresid=" & lngResID, False)
        'End Sub
    Private Sub lbtnPrintForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnPrintForm.Click
        Dim lngResID As Long = VerifyAndGetResID()
            If lngResID = 0 Then Return
            ' Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If
 
            Dim strCommandName As String = CType(sender, LinkButton).CommandName.Trim
            Session("CMSBP_" + strCommandName) = m_strBackPage
            Response.Redirect("/cmsweb/cmsform/" + strCommandName.Trim + ".aspx?mnuresid=" & lngResID & "&mnuformtype=" & FormType.PrintForm & "&mnuformname=", False)
        End Sub

        Private Sub lbtnFormRouter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnFormRouter.Click
            Session("CMSBP_ResourceFormRouter") = m_strBackPage
            Response.Redirect("ResourceFormRouter.aspx?ResourceId=" + _ResourceId.ToString)
        End Sub

    Private Sub lbtnRowColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRowColor.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_ResourceRowColor") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceRowColor.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnCopyRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCopyRes.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
            If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                PromptMsg("文档流的附件资源不支持当前操作！")
                Return
            End If
        End If

        Session("CMSBP_ResourceCopy") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceCopy.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnFormula.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        Session("CMSBP_FieldAdvCalculationList") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=0&mnuresid=" & lngResID, False)
    End Sub

    'Private Sub lbtnFormualAmongTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnFormualAmongTable.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return
    '    Session("CMSBP_FieldAdvCalculationList") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/FieldAdvCalculationList.aspx?isrightres=1&mnuresid=" & lngResID, False)
    'End Sub

    'Private Sub lbtnAllFormula_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Session("CMSBP_FieldAdvCalculationList") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/FieldAdvCalculationList.aspx?mnuresid=0", False)
    'End Sub

    'Private Sub lbtnRestoreRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRestoreRes.Click
    '    Session("RESIMP_BACKPAGE") = m_strBackPage
    '    Session("RESIMP_DEPID") = VLng("PAGE_DEPID")
    '    Dim lngResID As Long = VLng("PAGE_RESID")
    '    Session("RESIMP_PRESID") = lngResID
    '    Session("RESIMP_PRESNAME") = CmsPass.GetDataRes(lngResID).ResName

    '    Session("CMSBP_ResourceRestore") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/ResourceRestore.aspx", False)
    'End Sub

    'Private Sub lbtnBackupRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnBackupRes.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return

    '    Session("CMSBP_ResourceBackup") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/ResourceBackup.aspx?mnuresid=" & lngResID, False)
    'End Sub

    'Private Sub lbtnImportExternalTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnImportExternalTable.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return
    '    If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
    '        If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
    '            PromptMsg("文档流的附件资源不支持当前操作！")
    '            Return
    '        End If
    '    End If

    '    Session("CMSBP_ResourceImportStep1") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/ResourceImportStep1.aspx?mnuresid=" & lngResID, False)
    'End Sub

    'Private Sub lbtnGetReportSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGetReportSql.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return

    '    Session("CMSBP_ReportGetSql") = m_strBackPage
    '    Response.Redirect("/cmsweb/cmsreport/ReportGetSql.aspx?mnuresid=" & lngResID, False)
        'End Sub
    Private Sub lbtnOrderBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnOrderBy.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Session("CMSBP_ResourceColumnOrderby") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceColumnOrderby.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnLogExtension_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnLogExtension.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Session("CMSBP_ResourceLogExtension") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceLogExtension.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnResSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnResSync.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Session("CMSBP_ResourceSyncList") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & lngResID, False)
    End Sub

    Private Sub lbtnGeneralRowFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnGeneralRowFilter.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Session("CMSBP_MTableSearch") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/MTableSearch.aspx?mnuresid=" & lngResID & "&mtstype=" & MTSearchType.GeneralRowWhere & "&mnufromadmin=admin", False)
    End Sub

    Private Sub lbtnMTableStatistic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnMTableStatistic.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Session("CMSBP_MTableSearch") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/MTableSearch.aspx?mnuresid=" & lngResID & "&mtstype=" & MTSearchType.MultiTableView & "&mnufromadmin=admin", False)
    End Sub

    'Private Sub lbtnExportResData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnExportResData.Click
    '    Dim lngResID As Long = VerifyAndGetResID()
    '    If lngResID = 0 Then Return

    '    Session("CMSBP_ResourceExport") = m_strBackPage
    '    Response.Redirect("/cmsweb/adminres/ResourceExport.aspx?mnuresid=" & lngResID, False)
        'End Sub
    Private Sub btnSaveResAttr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveResAttr.Click
        Dim lngResID As Long = VerifyAndGetResID()
            If lngResID = 0 Then Return

            Dim datRes As New DataResource

            datRes.ResID = lngResID


        '资源锁定
        Dim lngShowOn As Long
        If chkShowResource.Checked Then 
                datRes.ShowEnable = 1
            Else
                datRes.ShowEnable = 0
            End If

        '关联表显示
        Dim lngShowInRel As Long
        If chkShowInRel.Checked Then
                datRes.IsShowInRelTables = 1
        Else
                datRes.IsShowInRelTables = 0
        End If

        '是否显示子关联资源
        Dim lngNotShowRelTables As Long
        If chkNotShowRelTables.Checked Then
                datRes.IsNotShowRelTables = 1
        Else
                datRes.IsNotShowRelTables = 0
        End If

        '显示子资源数据
        Dim lngShowChildResData As Long
        If chkShowChildResData.Checked Then
                datRes.IsShowChildResData = 1
        Else
                datRes.IsShowChildResData = 0
        End If


        '显示子资源数据
        Dim lngSaveHis As Long
        If chkSaveHis.Checked Then
            lngSaveHis = 1
        Else
            lngSaveHis = 0
        End If

        ''1：删除本资源中记录时同时删除该记录下所有子资源记录。0：仅删除本记录，不影响子资源中的子记录。
        'Dim lngDelSubRecords As Long
        'If chkDelSubRecords.Checked Then
        '    lngDelSubRecords = 1
        'Else
        '    lngDelSubRecords = 0
        'End If

        '在资源数据表单中显示记录最后创建人和创建时间
        Dim lngShowCRT As Long
        If chkShowCRT.Checked Then
                datRes.IsShowCRT = 1
        Else
                datRes.IsShowCRT = 0
        End If

        '在资源数据表单中显示记录最后修改人和修改时间
        Dim lngShowEDT As Long
        If chkShowEDT.Checked Then
                datRes.IsShowEDT = 1
        Else
                datRes.IsShowEDT = 0
        End If

        '在资源数据表单中显示记录最后修改人和修改时间
        Dim lngIsWorkflowRes As Long
        If chkIsWorkflowRes.Checked Then
                datRes.IsFlowResource = True
        Else
                datRes.IsFlowResource = False
        End If

        '是否使用独立型父资源的显示设置
        Dim lngUseParentShow As Long
        If chkUseParentShow.Checked Then
                datRes.IndepParentResID = 1
        Else
                datRes.IndepParentResID = 0
        End If

        '是否使用独立型父资源的显示设置
        Dim lngFlowShowFiledOnly As Long
        If chkFlowShowFiledOnly.Checked Then
                datRes.ShowFiledOnly = 1
        Else
                datRes.ShowFiledOnly = 0
        End If

        '是否使用独立型父资源的显示设置
        Dim lngRecursiveFormula As Long
        If chkRecursiveFormula.Checked Then
                datRes.RecursiveFormula = 1
        Else
                datRes.RecursiveFormula = 0
        End If

        Dim lngShowCheckBox As Long
        If chkShowCheckBox.Checked Then
                datRes.IsShowCheckBox = 1
        Else
                datRes.IsShowCheckBox = 0
        End If

        Dim iRecCount As Integer
        If txt_RecCount.Text = "" Then
                datRes.PageRecCount = 0
        Else
                datRes.PageRecCount = CInt(txt_RecCount.Text)
        End If
            ' Dim Sql As String = "UPDATE " & CmsTables.Resource & " SET RES_SHOWCHECKBOX=" & lngShowCheckBox & ",RES_RECCOUNT=" & iRecCount & " WHERE ID=" & lngResID
            ' CmsDbStatement.Execute(SDbConnectionPool.GetDbConfig(), Sql)
        'If CmsConfig.GetInt("SYS_CONFIG", "RECORD_SAVEHISTORY") = 1 Then
        '    Dim strSql As String = "UPDATE " & CmsTables.Resource & " SET Res_SaveHis=" & lngSaveHis & " WHERE ID=" & lngResID
        '    CmsDbStatement.Execute(SDbConnectionPool.GetDbConfig(), strSql)
        '    If lngSaveHis = 1 Then
        '        '生成历史记录表
        '        Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)
        '        Dim strTableName As String = datRes.ResTable
        '        strSql += "if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" & strTableName & "_History]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) "
        '        strSql += " begin "
        '        strSql += " select *  into [dbo]." & strTableName & "_History from " & strTableName & " where 1=2 "
        '        strSql += " alter table " & strTableName & "_History add PriId bigint default(0) constraint pk_" & strTableName & "_History  primary   key,  OpType varchar(20), OpUser varchar(40),OpTime datetime "
        '        strSql += " end "
        '        CmsDbStatement.Execute(SDbConnectionPool.GetDbConfig(), strSql)
            '    End If

            'End If

            datRes.Comments = txtResComments.Text.Trim()
            datRes.ServerUrl = Me.txtResServerUrl.Text.Trim()
            datRes.EmptyResUrl = Me.txtEMPTYRESOURCEURL.Text.Trim
            If Me.chkEMPTYRESOURCETARGET.SelectedIndex = -1 Then
                If Me.txtEMPTYRESOURCETARGET.Text.Trim <> "" Then
                    datRes.EmptyResTarget = Me.txtEMPTYRESOURCETARGET.Text.Trim
                Else
                    datRes.EmptyResTarget = "_parent"
                End If
            Else
                datRes.EmptyResTarget = Me.chkEMPTYRESOURCETARGET.SelectedValue.Trim
            End If

            ResFactory.ResService.SetResourceProperty(CmsPass, datRes)
        End Sub

    Private Sub btnSaveDocResAttr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDocResAttr.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        '版本控制
        Dim lngVerctrlOn As Long
        If chkEnableVerCtrl.Checked Then
            lngVerctrlOn = 1
        Else
            lngVerctrlOn = 0
        End If

        '全文检索
        Dim lngFTSearchOn As Long
        If chkFTSearchOn.Checked Then
            lngFTSearchOn = 1
        Else
            lngFTSearchOn = 0
        End If

        ResFactory.ResService.SetResourcePropertyForDoc(CmsPass, lngResID, lngVerctrlOn, lngFTSearchOn)
    End Sub

    Private Sub btnSaveSecurity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSecurity.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Try
            If CLng(ddlSecurity.SelectedValue) < CmsPass.GetDataRes(lngResID).SecurityLevel Then
                '降低机密等级时需要检验系统安全员密码
                'If CmsAdmin.ValidateSecurity(txtSecurityPass.Text.Trim()) = False Then
                If OrgFactory.EmpDriver.ValidatePass(SDbConnectionPool.GetDbConfig(), "security", txtSecurityPass.Text.Trim()) = False Then
                    ddlSecurity.SelectedValue = CStr(CmsPass.GetDataRes(lngResID).SecurityLevel)
                    PromptMsg("降低资源机密性等级时必需输入系统安全员帐号密码！")
                    Return
                End If
            End If

            ResFactory.ResService.SetSecurityLevel(CmsPass, CmsPass.GetDataRes(lngResID).ResID, CLng(ddlSecurity.SelectedValue))
            CmsPass.GetDataRes(lngResID).SecurityLevel = CLng(ddlSecurity.SelectedValue)
        Catch ex As Exception
            PromptMsg("设置资源机密性等级时异常出错！", ex, True)
        End Try

        txtSecurityPass.Text = "" '每次操作完必须清空系统安全员密码
    End Sub

    '-------------------------------------------------------------------
    '设置各功能链接是否显示
    '-------------------------------------------------------------------
    Private Sub PrepareFuncShow(ByVal lngResID As Long)
        Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)

        Dim blnIsEnterpriseNode As Boolean = False
        If VLng("PAGE_DEPID") = 0 And datRes.ResID = 0 Then blnIsEnterpriseNode = True '部门树中当前选中的是企业节点
        Dim blnIsDepRootNode As Boolean = False
        If lngResID = 0 And datRes.ResID = 0 Then blnIsDepRootNode = True '部门节点
            Dim bln As Boolean = False


            

        '此功能尚未实现，所以暂时Disable
        'chkDelSubRecords.Enabled = False

        '创建资源
        'If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
        '    lbtnAddResource.Enabled = False
            'Else '部门树中当前选中的是部门节点

            If Convert.ToBoolean(ViewState("IsCreateRigth")) Then
                lbtnAddResource.Enabled = True
                lbtnImportRes.Enabled = True
                btnSaveSecurity.Enabled = True
                btnSaveDocResAttr.Enabled = True
                btnSaveResAttr.Enabled = True
            Else
                lbtnAddResource.Enabled = False
                lbtnImportRes.Enabled = False
                btnSaveSecurity.Enabled = False
                btnSaveDocResAttr.Enabled = False
                btnSaveResAttr.Enabled = False
            End If
            'End If

            '修改资源
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnEditResource.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnEditResource.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnEditResource.Enabled = True
                End If
            End If

            '删除资源
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnDelResource.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnDelResource.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnDelResource.Enabled = True
                End If
            End If

            '创建表单
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnCreateHostTable.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnCreateHostTable.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then
                        lbtnCreateHostTable.Enabled = True '表单尚未创建的才可以创建
                    Else
                        lbtnCreateHostTable.Enabled = False
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_CREATE_CTABLE")
            If bln = False Then
                lbtnCreateHostTable.Enabled = False
            End If

            '字段设置
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnSetHostTable.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnSetHostTable.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnSetHostTable.Enabled = False
                    Else
                        If lngResID = CmsPass.GetDataRes(lngResID).IndepParentResID Then '是独立型资源
                            lbtnSetHostTable.Enabled = True
                        Else '是继承型资源
                            lbtnSetHostTable.Enabled = False
                        End If
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_COLUMN_SET")
            If bln = False Then
                lbtnSetHostTable.Enabled = False
            End If

            '显示设置
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnSetHostTableShow.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnSetHostTableShow.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnSetHostTableShow.Enabled = False
                    Else
                        lbtnSetHostTableShow.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_COLUMNSHOW_SET")
            If bln = False Then
                lbtnSetHostTableShow.Enabled = False
            End If

            '权限设置
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnSetRights.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnSetRights.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnSetRights.Enabled = True
                End If
            End If

            '输入窗体设计/打印窗体设计
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnInputformDesign.Enabled = False
                lbtnPrintForm.Enabled = False
                lbtnFormRouter.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnInputformDesign.Enabled = False
                    lbtnPrintForm.Enabled = False
                    lbtnFormRouter.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnInputformDesign.Enabled = False
                        lbtnPrintForm.Enabled = False
                        lbtnFormRouter.Enabled = False
                    Else
                        lbtnInputformDesign.Enabled = True
                        lbtnPrintForm.Enabled = True
                        lbtnFormRouter.Enabled = True
                        If datRes.ResType = ResInheritType.IsIndependent Then
                            lbtnInputformDesign.CommandName = "FormDesign"
                            lbtnPrintForm.CommandName = "FormDesign"
                        Else
                            lbtnInputformDesign.CommandName = "InheritFormDesign"
                            lbtnPrintForm.CommandName = "InheritFormDesign"
                        End If
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_PRINTFORM")
            If bln = False Then
                lbtnPrintForm.Enabled = False
            End If
 

            '关联表设置
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnRelatedTable.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnRelatedTable.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnRelatedTable.Enabled = False
                    Else
                        lbtnRelatedTable.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_RELATION_TABLE")
            If bln = False Then
                lbtnRelatedTable.Enabled = False
            End If

            '行颜色设置
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnRowColor.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnRowColor.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnRowColor.Enabled = False
                    Else
                        lbtnRowColor.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_RES_ROWCOLOR")
            If bln = False Then
                lbtnRowColor.Enabled = False
            End If

            '复制资源
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnCopyRes.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnCopyRes.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnCopyRes.Enabled = False
                    Else
                        If datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
                            lbtnCopyRes.Enabled = False
                        Else
                            lbtnCopyRes.Enabled = True
                        End If
                    End If
                End If
            End If
 

            '资源计算公式
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnFormula.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnFormula.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnFormula.Enabled = False
                    ElseIf datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
                        lbtnFormula.Enabled = False
                    Else
                        lbtnFormula.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_FORMULA")
            If bln = False Then
                lbtnFormula.Enabled = False
            End If

            '资源表间计算公式
            'If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
            '    lbtnFormualAmongTable.Enabled = False
            'Else '部门树中当前选中的是部门节点
            '    If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
            '        lbtnFormualAmongTable.Enabled = False
            '    Else '资源树中当前选中的是资源节点
            '        If datRes.ResTable = "" Then '是空资源
            '            lbtnFormualAmongTable.Enabled = False
            '        ElseIf datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
            '            lbtnFormualAmongTable.Enabled = False
            '        Else
            '            lbtnFormualAmongTable.Enabled = True
            '        End If
            '    End If
            'End If
            'bln = CmsFunc.IsEnable("FUNC_FORMULA")
            'If bln = False Then
            '    lbtnFormualAmongTable.Enabled = False
            'End If

            ''系统计算公式
            'bln = CmsFunc.IsEnable("FUNC_FORMULA")
            'If bln = False Then
            '    lbtnAllFormula.Enabled = False
            'End If

            '排序设置
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnOrderBy.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnOrderBy.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnOrderBy.Enabled = False
                    Else
                        lbtnOrderBy.Enabled = True
                    End If
                End If
            End If

            '邮箱手机字段
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnCommunication.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnCommunication.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnCommunication.Enabled = False
                    ElseIf datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
                        lbtnCommunication.Enabled = False
                    Else
                        lbtnCommunication.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_COMM_EMAILPHONE")
            If bln = False Then
                lbtnCommunication.Enabled = False
            End If

            '移动资源
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnMoveRes.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnMoveRes.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnMoveRes.Enabled = True
                End If
            End If

            '向上移动
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnMoveup.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnMoveup.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnMoveup.Enabled = True
                End If
            End If
            If CmsConfig.ResTreeOrderbyResName() = True Then
                lbtnMoveup.Enabled = False
            End If

            '向下移动
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnMovedown.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnMovedown.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnMovedown.Enabled = True
                End If
            End If
            If CmsConfig.ResTreeOrderbyResName() = True Then
                lbtnMovedown.Enabled = False
            End If

            '日志扩展
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnLogExtension.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnLogExtension.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnLogExtension.Enabled = False
                    ElseIf datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
                        lbtnLogExtension.Enabled = False
                    Else
                        lbtnLogExtension.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_LOG_EXTCOLUMN")
            If bln = False Then
                lbtnLogExtension.Enabled = False
            End If

            '资源数据同步
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnResSync.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnResSync.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnResSync.Enabled = False
                    Else
                        lbtnResSync.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_RESSYNC")
            If bln = False Then
                lbtnResSync.Enabled = False
            End If

            '通用行过滤
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnGeneralRowFilter.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnGeneralRowFilter.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnGeneralRowFilter.Enabled = False
                    Else
                        lbtnGeneralRowFilter.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_ROWWHERE")
            If bln = False Then
                lbtnGeneralRowFilter.Enabled = False
            End If

            '列表统计
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnMTableStatistic.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnMTableStatistic.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTable = "" Then '是空资源
                        lbtnMTableStatistic.Enabled = False
                    Else
                        lbtnMTableStatistic.Enabled = True
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_MULTITABLE_SEARCH")
            If bln = False Then
                lbtnMTableStatistic.Enabled = False
            End If

            '创建文档流
            'If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
            '    lbtnAddDocFlowResource.Enabled = False
            'Else '部门树中当前选中的是部门节点
            '    If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
            '        lbtnAddDocFlowResource.Enabled = True
            '    ElseIf datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
            '        lbtnAddDocFlowResource.Enabled = False
            '    Else '资源树中当前选中的是资源节点
            '        lbtnAddDocFlowResource.Enabled = True
            '    End If
            'End If

            '转换文档流
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnChangeToDocFlowResource.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnChangeToDocFlowResource.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.IsFlowResource = True Then
                        lbtnChangeToDocFlowResource.Enabled = False
                    Else
                        If datRes.ResTable = "" Then '是空资源
                            lbtnChangeToDocFlowResource.Enabled = False
                        ElseIf datRes.IndepParentResID <> datRes.ResID Then '是继承型资源
                            lbtnChangeToDocFlowResource.Enabled = False
                        Else
                            lbtnChangeToDocFlowResource.Enabled = True '不是工作流资源才能转换为工作流
                        End If
                    End If
                End If
            End If
            ''''''''''''tq  2010-07-05'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If lbtnChangeToDocFlowResource.Enabled Then
                lbtnChangeToDocFlowResource.Attributes.Add("onClick", "return CmsPrmoptConfirm('确定要将当前选中的资源转换为带流程文档表的文档流资源吗？');")
            End If
            ''创建文档流归档
            'If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
            '    lbtnSetGuidangFilter.Enabled = False
            'Else '部门树中当前选中的是部门节点
            '    If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
            '        lbtnSetGuidangFilter.Enabled = False
            '    Else '资源树中当前选中的是资源节点
            '        If datRes.IsFlowResource = True Then
            '            lbtnSetGuidangFilter.Enabled = True '是工作流资源才能添加归档过滤
            '        Else
            '            lbtnSetGuidangFilter.Enabled = False
            '        End If
            '    End If
            'End If
            'bln = CmsFunc.IsEnable("FUNC_WORKFLOW") '判断功能模块开关是否打开
            'If bln = False Then
            '    lbtnAddDocFlowResource.Enabled = False
            '    lbtnChangeToDocFlowResource.Enabled = False
            '    lbtnSetGuidangFilter.Enabled = False
            'End If

            'checkbox: 仅显示归档数据
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                chkFlowShowFiledOnly.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    chkFlowShowFiledOnly.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.IsFlowResource = True Then
                        chkFlowShowFiledOnly.Enabled = True '是工作流资源才能添加归档过滤
                    Else
                        chkFlowShowFiledOnly.Enabled = False
                    End If
                End If
            End If
            bln = CmsFunc.IsEnable("FUNC_WORKFLOW") '判断功能模块开关是否打开
            If bln = False Then
                chkFlowShowFiledOnly.Enabled = False
            End If

            'checkbox: 递规计算关系
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                chkRecursiveFormula.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    chkRecursiveFormula.Enabled = False
                Else '资源树中当前选中的是资源节点
                    If datRes.ResTableType = "DOC" Then '是文档管理
                        chkRecursiveFormula.Enabled = False
                    Else
                        chkRecursiveFormula.Enabled = True
                    End If
                End If
            End If



            '导出资源
            If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
                lbtnExportRes.Enabled = False
            Else '部门树中当前选中的是部门节点
                If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
                    lbtnExportRes.Enabled = False
                Else '资源树中当前选中的是资源节点
                    lbtnExportRes.Enabled = True
                End If
            End If

            If datRes.IsView Then
                lbtnViewCondition.Enabled = True
            Else
                lbtnViewCondition.Enabled = False
            End If

            '关联锁定
            'If blnIsEnterpriseNode = True Then '部门树中当前选中的是企业节点
            '    lbtnLockRelation.Enabled = False
            'Else '部门树中当前选中的是部门节点
            '    If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
            '        lbtnLockRelation.Enabled = False
            '    Else '资源树中当前选中的是资源节点
            '        If blnIsDepRootNode = True Then '资源树中当前选中的是部门节点
            '            lbtnLockRelation.Enabled = False
            '        Else '资源树中当前选中的是资源节点
            '            If datRes.ResTable = "" Then '是空资源
            '                lbtnLockRelation.Enabled = False
            '            Else
            '                lbtnLockRelation.Enabled = True
            '            End If
            '        End If
            '    End If
            'End If
            'bln = CmsFunc.IsEnable("FUNC_LOCK_RELATION") '判断功能模块开关是否打开
            'If bln = False Then
            '    lbtnLockRelation.Enabled = False
            'End If
        End Sub

    '-------------------------------------------------------
    '向上或向下移动资源
    '-------------------------------------------------------
    Private Sub MoveResUpDown(ByVal blnIsMoveup As Boolean)
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return

        Try
            If blnIsMoveup Then
                ResFactory.ResService.MoveUp(CmsPass, lngResID)
            Else
                ResFactory.ResService.MoveDown(CmsPass, lngResID)
            End If
        Catch ex As Exception
            PromptMsg("移动资源失败，无法连接数据库", ex, True)
        End Try
    End Sub

    '-------------------------------------------------------
    '显示资源属性
    '-------------------------------------------------------
    Private Sub ShowResAttributes(ByVal lngResID As Long)
        Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)

        If datRes.ResID = 0 Then Return

        '显示资源
        If datRes.ShowEnable = 1 Then
            chkShowResource.Checked = True
        Else
            chkShowResource.Checked = False
        End If

        '关联表显示
        If datRes.IsShowInRelTables = 1 Then
            chkShowInRel.Checked = True
        Else
            chkShowInRel.Checked = False
        End If

        '是否显示子关联资源
        If datRes.IsNotShowRelTables = 1 Then
            chkNotShowRelTables.Checked = True
        Else
            chkNotShowRelTables.Checked = False
        End If

        '显示子资源数据
        If datRes.IsShowChildResData = 1 Then
            chkShowChildResData.Checked = True
        Else
            chkShowChildResData.Checked = False
        End If

        '1：在资源数据表单中显示记录创建人和创建时间；0：不显示
        If datRes.IsShowCRT = 1 Then
            chkShowCRT.Checked = True
        Else
            chkShowCRT.Checked = False
        End If

        '1：在资源数据表单中显示记录最后修改人和修改时间；0：不显示
        If datRes.IsShowEDT = 1 Then
            chkShowEDT.Checked = True
        Else
            chkShowEDT.Checked = False
        End If

        '是否工作流资源
        chkIsWorkflowRes.Checked = datRes.IsFlowResource

        '1:保存历史记录；0：不保存
        If datRes.IsSaveHis = 1 Then
            chkSaveHis.Checked = True
        Else
            chkSaveHis.Checked = False
        End If

        '1:显示checkbox；0：不显示
        If datRes.IsShowCheckBox = 1 Then
            chkShowCheckBox.Checked = True
        Else
            chkShowCheckBox.Checked = False
        End If
        txt_RecCount.Text = datRes.PageRecCount.ToString()

        '是否使用独立型父资源的显示设置
        If datRes.ResID = datRes.IndepParentResID Then
            '是独立型资源
            chkUseParentShow.Enabled = False
            chkUseParentShow.Checked = False
        Else
            '是继承型资源
            chkUseParentShow.Enabled = True
            If datRes.UseParentShow = 1 Then
                chkUseParentShow.Checked = True
            Else
                chkUseParentShow.Checked = False
            End If
        End If

        '版本控制
        If CmsFunc.IsEnable("FUNC_DOCHISTORY") = False Then
            chkEnableVerCtrl.Enabled = False
            chkEnableVerCtrl.Checked = False
        Else
            If datRes.ResTableType = "DOC" Then '是文档管理
                chkEnableVerCtrl.Enabled = True
                If datRes.IsVerctrlOn = 1 Then
                    chkEnableVerCtrl.Checked = True
                Else
                    chkEnableVerCtrl.Checked = False
                End If
            Else
                chkEnableVerCtrl.Enabled = False
                chkEnableVerCtrl.Checked = False
            End If
        End If

        '全文检索
        If CmsFunc.IsEnable("FUNC_FULLTEXT_SEARCH") = False Then
            chkFTSearchOn.Enabled = False
            chkFTSearchOn.Checked = False
        Else
            If datRes.ResTableType = "DOC" Then '是文档管理
                chkFTSearchOn.Enabled = True
                If datRes.IsFTSearchOn = True Then
                    chkFTSearchOn.Checked = True
                Else
                    chkFTSearchOn.Checked = False
                End If
            Else
                chkFTSearchOn.Enabled = False
                chkFTSearchOn.Checked = False
            End If
        End If

        '机密等级
        ddlSecurity.SelectedValue = CStr(datRes.SecurityLevel)

        'If datRes.ResID = 0 Then
        '    lblResType.Text = ""
        'ElseIf datRes.IndepParentResID = datRes.ResID Then
        '    lblResType.Text = "独立型资源（拥有独立数据表单结构）"
        'Else
        '    lblResType.Text = "继承型资源（与父资源共用数据表单）"
        'End If

        'If datRes.ResID = 0 Then
        '    lblResTableType.Text = ""
        'ElseIf datRes.ResTableType = "DOC" Or datRes.ResTableType = "TWOD" Then
        '    lblResTableType.Text = ResFactory.ResService().ConvTableTypeInnerName2DispName(CmsPass, datRes.ResTableType)
        'Else
        '    lblResTableType.Text = "（尚未创建表单）"
        'End If

            txtResComments.Text = datRes.Comments
            txtResServerUrl.Text = datRes.ServerUrl.Trim()

            Me.txtEMPTYRESOURCEURL.Text = datRes.EmptyResUrl

            If datRes.EmptyResTarget.Trim = "_blank" Or datRes.EmptyResTarget.Trim = "_parent" Or datRes.EmptyResTarget.Trim = "_search" Or datRes.EmptyResTarget.Trim = "_self" Or datRes.EmptyResTarget.Trim = "_top" Then
                For i As Integer = 0 To chkEMPTYRESOURCETARGET.Items.Count - 1
                    If chkEMPTYRESOURCETARGET.Items(i).Value = datRes.EmptyResTarget.Trim Then
                        chkEMPTYRESOURCETARGET.Items(i).Selected = True
                    End If
                Next
            Else
                Me.txtEMPTYRESOURCETARGET.Text = datRes.EmptyResTarget.Trim
            End If 
            chkFlowShowFiledOnly.Checked = CBool(IIf(datRes.ShowFiledOnly = 1, True, False))

            chkRecursiveFormula.Checked = CBool(IIf(datRes.RecursiveFormula = 1, True, False))

            '显示资源记录数量
            If datRes.ResTable <> "" Then
                txtRecNum.Text = CStr(CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), datRes.ResTable, ""))
            Else
                txtRecNum.Text = ""
            End If
        End Sub

    '------------------------------------------------------------------------
    'Load机密等级
    '------------------------------------------------------------------------
    Private Sub LoadSecurityLevel()
        ddlSecurity.Items.Clear()

        Dim li As ListItem
        li = New ListItem("普通", CStr(ResSecurityLevel.Normal))
        ddlSecurity.Items.Add(li)
        li = Nothing

        li = New ListItem("机密", CStr(ResSecurityLevel.Secret))
        ddlSecurity.Items.Add(li)
        li = Nothing
    End Sub

    Private Sub btnDelAllData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelAllData.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsPass.GetDataRes(lngResID).ResTableType = "DOC" Then
            PromptMsg("不支持文档表的批量删除资源数据！")
            Return
        End If
        Dim strSql As String = "DELETE " & CmsPass.GetDataRes(lngResID).ResTable & " WHERE RESID=" & lngResID
        Dim intNum As Integer = CmsDbStatement.Execute(SDbConnectionPool.GetDbConfig(), strSql)
        PromptMsg("共删除数据" & intNum & "条")
    End Sub

    Private Sub btnDelDataKeep10Day_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelDataKeep10Day.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsPass.GetDataRes(lngResID).ResTableType = "DOC" Then
            PromptMsg("不支持文档表的批量删除资源数据！")
            Return
        End If
        Dim str10DayBefore As String = Today.AddDays(-10).ToString("yyyy-MM-dd")
        Dim strSql As String = "DELETE " & CmsPass.GetDataRes(lngResID).ResTable & " WHERE RESID=" & lngResID & " AND CRTTIME<'" & str10DayBefore & "'"
        Dim intNum As Integer = CmsDbStatement.Execute(SDbConnectionPool.GetDbConfig(), strSql)
        PromptMsg("共删除数据" & intNum & "条")
    End Sub

    Private Sub btnDelDataKeep30Day_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelDataKeep30Day.Click
        Dim lngResID As Long = VerifyAndGetResID()
        If lngResID = 0 Then Return
        If CmsPass.GetDataRes(lngResID).ResTableType = "DOC" Then
            PromptMsg("不支持文档表的批量删除资源数据！")
            Return
        End If
        Dim str30DayBefore As String = Today.AddDays(-30).ToString("yyyy-MM-dd")
        Dim strSql As String = "DELETE " & CmsPass.GetDataRes(lngResID).ResTable & " WHERE RESID=" & lngResID & " AND CRTTIME<'" & str30DayBefore & "'"
        Dim intNum As Integer = CmsDbStatement.Execute(SDbConnectionPool.GetDbConfig(), strSql)
        PromptMsg("共删除数据" & intNum & "条")
    End Sub

    Private Function VerifyAndGetResID() As Long
        Dim lngResID As Long = VLng("PAGE_RESID")
        If lngResID = 0 Then
            PromptMsg("请选择有效的资源！")
        End If
        Return lngResID
    End Function

    '----------------------------------------------------------------------------------------
    'Load自定义的树结构
    '----------------------------------------------------------------------------------------
        Public Shared Sub LoadTreeView(ByRef pst As CmsPassport, ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByRef ViewState As System.Web.UI.StateBag)
            If Convert.ToBoolean(ViewState("IsCreateRigth")) = False Then Return
            Dim blnShowRootNodeOnly As Boolean = False
            If pst.EmpIsDepAdmin = True AndAlso pst.EmpIsSysAdmin = False Then
                '检查当前人员是否是默认资源所在部门的部门管理员
                Dim strEmpID As String = OrgFactory.DepDriver.GetDepAdmin(pst, AspPage.VLng("PAGE_DEPID", ViewState), True)
                If pst.Employee.ID.ToLower <> strEmpID.ToLower() Then
                    blnShowRootNodeOnly = True  '仅显示根节点
                End If
            End If

            WebTreeResource.LoadResTreeView(pst, Request, Response, "/cmsweb/adminres/ResourceFrmContent.aspx", "_self", "", "", AspPage.VStr("PAGE_RESID", ViewState), AspPage.VStr("PAGE_DEPID", ViewState), blnShowRootNodeOnly, False, False)
        End Sub

    '-------------------------------------------------------------------------------   
    '验证系统是否有效
    '-------------------------------------------------------------------------------   
    Private Function VerifySystemLicense() As Boolean
        Try
            Dim pstTemp As CmsPassport = CmsPassport.GenerateCmsPassportForInnerUse("") '仅用于临时访问数据库

            '每次系统启动的第一个请求中验证：产品码是否已经存在
            Dim strProdCode As String = CmsCodeProduct.GetProductCode(pstTemp)
            If strProdCode = "" Then
                '产品码验证失败则进入产品码输入界面
                Response.Redirect("/cmsweb/cmssetup/SetupProductCode.htm", True)
            Else
                '每次系统启动的第一个请求中验证：没有用户许可码的产品码是否已经过期，根据系统安装日期判断
                Dim intLicNum As Integer = CmsCodeLicense.GetTotalLicenseNumber(pstTemp)
                If intLicNum = 0 Then
                    If CmsCodeProduct.IsProdCodeExpired(pstTemp) = True Then
                        '产品码的默认用户数已过期
                            Response.Redirect("/cmsweb/adminres/admincode/CodeMgrExpired.aspx", True)
                    Else
                        intLicNum = 20 '产品码的默认用户数
                    End If
                End If

                '每次系统启动的第一个请求中验证：当前用户数量是否已经超过用户许可码规定的数量。没有用户许可码的产品码的默认用户数量为20个
                Dim lngCurEmpNum As Long = CmsDbStatement.Count(SDbConnectionPool.GetDbConfig(), CmsTables.Employee, "") - 3  '获取当前用户数: 减去3个系统管理员帐号
                If lngCurEmpNum > (intLicNum + 10) Then '10：当用户许可被破解后，这里再加一个限制
                        Response.Redirect("/cmsweb/adminres/admincode/CodeMgrLicense.aspx", True)
                ElseIf lngCurEmpNum > (intLicNum + 20) Then '20：当用户许可被破解后，这里再加一个限制
                        Response.Redirect("/cmsweb/adminres/admincode/CodeMgrLicense.aspx", True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    '导入资源
    Private Sub lbtnImportRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnImportRes.Click
        Dim lngResID As Long = VLng("PAGE_RESID") '获取选中资源
        Dim lngDepID As Long = VLng("PAGE_DEPID")

        Session("CMSBP_ResourceCopy") = m_strBackPage
        'Session("PAGE_BACKPAGE") = m_strBackPage
        Response.Redirect("/cmsweb/adminres/ResourceImport.aspx?depId=" & lngDepID & "&mnuresid=" & lngResID, False)

    End Sub

    '导出资源
    Private Sub lbtnExportRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnExportRes.Click
        Try
            Dim lngResID As Long = VerifyAndGetResID()
            If lngResID = 0 Then Return
            If CmsFunc.IsEnable("FUNC_WORKFLOW_DOCRESMGR") = False Then
                If ResFactory.ResService.IsWorkFlowAttachDocRes(CmsPass, lngResID) = True Then
                    PromptMsg("文档流的附件资源不支持当前操作！")
                    Return
                Else
                    DownloadDoc(XmlWrite(lngResID))
                End If
            Else
                DownloadDoc(XmlWrite(lngResID))
            End If

            Session("CMSBP_ResourceCopy") = m_strBackPage
            'Response.Write("<script language='javascript'>alert('导出资源成功！！！'); </script>")
        Catch ex As Exception
            'SLog.Err("导出资源出错" & ex.ToString())
            'Response.Write("<script language='javascript'>alert('导出资源失败！！！'); </script>")
        End Try

    End Sub

    Private Sub DownloadDoc(ByVal strFileName As String)
        Try
            '读取文件
            Dim fs As FileStream = New FileStream(strFileName, FileMode.Open, FileAccess.Read)
            '必须Encode文件名称，否则乱码
            'Dim strFilePath As String = SStr("CMSTRANS_EXPORTFILE")
            'Dim strFileName As String = Path.GetFileNameWithoutExtension(strFilePath) ' & "." & Path.GetExtension(strFilePath)
            'strFileName = strFileName.Substring(0, strFileName.IndexOf("__"))
            strFileName = strFileName.Substring(strFileName.LastIndexOf("\") + 1)
            strFileName = HttpUtility.UrlEncode(strFileName)
            Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
            Response.ContentType = "application/octet-stream"


            Dim br As BinaryReader = New BinaryReader(fs)

            '开始向客户端写文件流
            Response.BinaryWrite(br.ReadBytes(CInt(fs.Length)))
            Response.Flush()

            '务必再Response.End()之前调用
            br.Close()
            fs.Close()

            Response.End()
        Catch ex As Exception
            '线程正被中止，不做任何操作
            '调用Response.End()后，必然到达这里
        End Try
        End Sub

    Public Function XmlWrite(ByVal lngResID As Long) As String
        Try
            Dim cmsRes As New CmsResource
            Dim dataRes As DataResource = cmsRes.GetOneResource(CmsPass, lngResID)
            Dim FileName As String = dataRes.ResName & "_" & lngResID.ToString()
            Dim ResImp As New ResourceImportExport

            Dim xmlDoc As New XmlDocument
            Dim xmlDecl As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", String.Empty)
            xmlDoc.AppendChild(xmlDecl)
            Dim nodes As XmlNode = xmlDoc.CreateElement("ResourceImport")
            xmlDoc.AppendChild(nodes)
            ResImp.ResourceInfo(xmlDoc, nodes, lngResID)
            Dim path As String = Server.MapPath("\cmsweb\temp\")

            '''''''''''''''tq  2010-07-05'''''''''''''''''''''''''''''''''''''''''''''''''
            If System.IO.Directory.Exists(path) = False Then
                System.IO.Directory.CreateDirectory(path)
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            path += FileName & ".xml"

            xmlDoc.Save(path)
            Return path
        Catch ex As Exception
            CmsError.ThrowEx("导出资源(" & lngResID & ")失败", ex, True)
        End Try
    End Function

       
        Protected Sub lbtnViewCondition_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnViewCondition.Click
            Dim lngResID As Long = VerifyAndGetResID()
            If lngResID = 0 Then Return
            Session("CMSBP_MTableSearchColDef") = m_strBackPage
            'Session("CMSBP_MTableSearch") = m_strBackPage
            'Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & lngResID & "&mtstype=" & MTSearchType.ViewFilter & "&mnufromadmin=admin", False)
            Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & lngResID.ToString & "&mtstype=" & MTSearchType.ViewFilter & "&mnuempid=admin", False)
        End Sub

        Protected Sub lbtnCreateView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnCreateView.Click
            Dim lngResID As Long = 0 'VerifyAndGetResID()
            '  If lngResID = 0 Then Return
            Session("CMSBP_MTableSearchColDef") = m_strBackPage 
            Response.Redirect("/cmsweb/adminres/ViewRelationList.aspx?mnuresid=" & VLng("PAGE_RESID") & "&depid=" & VLng("PAGE_DEPID"), False)
        End Sub
    End Class
End Namespace
