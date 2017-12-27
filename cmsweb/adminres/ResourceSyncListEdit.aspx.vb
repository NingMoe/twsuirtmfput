Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ResourceSyncListEdit
    Inherits CmsPage

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VLng("PAGE_SYNC_LISTID") = 0 Then
            ViewState("PAGE_SYNC_LISTID") = RLng("URLRECID")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        rdoActionAdd.Checked = True
        rdoAddAndEdit.Checked = True

        txtResName.ReadOnly = True
        txtResName.Text = CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName

        If RLng("selresid") <> 0 Then '刚从选择资源窗体回来
            lbtnSelectRes.Enabled = False '已定义的数据同步资源不能再被更改

            ViewState("PAGE_SYNC_RESID") = RLng("selresid")
            txtSyncResName.Text = CmsPass.GetDataRes(VLng("PAGE_SYNC_RESID")).ResName
        Else
            Dim datSyncList As DataResSyncList = CmsResourceSync.GetOneSyncList(CmsPass, VLng("PAGE_SYNC_LISTID"))
            If Not datSyncList Is Nothing Then
                lbtnSelectRes.Enabled = False '已定义的数据同步资源不能再被更改

                'Load界面初始化信息
                txtSyncResName.Text = CmsPass.GetDataRes(datSyncList.lngSYNLST_SYNCRESID).ResName
                If datSyncList.intSYNLST_ACTION = ResSyncAction.AddRecord Then
                    rdoActionAdd.Checked = True
                    rdoActionEdit.Checked = False
                    rdoActionDel.Checked = False
                ElseIf datSyncList.intSYNLST_ACTION = ResSyncAction.EditRecord Then
                    rdoActionAdd.Checked = False
                    rdoActionEdit.Checked = True
                    rdoActionDel.Checked = False
                ElseIf datSyncList.intSYNLST_ACTION = ResSyncAction.DelRecord Then
                    rdoActionAdd.Checked = False
                    rdoActionEdit.Checked = False
                    rdoActionDel.Checked = True
                End If

                If datSyncList.intSYNLST_SYNCTYPE = ResSyncType.AddOrEditRecord Then
                    rdoAddAndEdit.Checked = True
                    rdoAddOnly.Checked = False
                ElseIf datSyncList.intSYNLST_SYNCTYPE = ResSyncType.AddRecordOnly Then
                    rdoAddAndEdit.Checked = False
                    rdoAddOnly.Checked = True
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub

    Private Sub lbtnSelectRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSelectRes.Click
        Session("CMSBP_ResourceSelect") = "/cmsweb/adminres/ResourceSyncListEdit.aspx?mnuresid=" & VLng("PAGE_RESID")
        Response.Redirect("/cmsweb/cmsothers/ResourceSelect.aspx?mnuresid=" & VLng("PAGE_RESID"), False)
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            '-------------------------------------------------------------------------------------------
            '提取界面设置信息
            Dim intSyncAction As ResSyncAction = ResSyncAction.AddRecord
            If rdoActionAdd.Checked = True Then
                intSyncAction = ResSyncAction.AddRecord
            ElseIf rdoActionEdit.Checked = True Then
                intSyncAction = ResSyncAction.EditRecord
            ElseIf rdoActionDel.Checked = True Then
                intSyncAction = ResSyncAction.DelRecord
            End If

            Dim intSyncType As ResSyncType = ResSyncType.AddOrEditRecord
            If rdoAddAndEdit.Checked = True Then
                intSyncType = ResSyncType.AddOrEditRecord
            ElseIf rdoAddOnly.Checked = True Then
                intSyncType = ResSyncType.AddRecordOnly
            End If
            '-------------------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------------------
            '保存设置
            Dim lngSyncListID As Long = VLng("PAGE_SYNC_LISTID")
            If lngSyncListID <> 0 Then '编辑设置
                CmsResourceSync.EditSyncList(CmsPass, lngSyncListID, intSyncAction, intSyncType)
                Response.Redirect("/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VStr("PAGE_RESID") & "&URLRECID=" & lngSyncListID, False)
            Else '添加新设置
                If VLng("PAGE_SYNC_RESID") = 0 Then
                    PromptMsg("请选择有效的数据同步资源。")
                Else
                    lngSyncListID = CmsResourceSync.AddOrEditSyncList(CmsPass, VLng("PAGE_RESID"), VLng("PAGE_SYNC_RESID"), intSyncAction, intSyncType)
                    Response.Redirect("/cmsweb/adminres/ResourceSyncList.aspx?mnuresid=" & VStr("PAGE_RESID") & "&URLRECID=" & lngSyncListID, False)
                End If
            End If
            '-------------------------------------------------------------------------------------------
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
