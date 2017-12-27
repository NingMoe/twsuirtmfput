Option Strict On
Option Explicit On

Imports System.Data.OleDb

Imports NetReusables
Imports Unionsoft.Platform
Partial Class cmsrights_RightSetFrm
    Inherits CmsPage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '显示当前选中用户的权限信息
        If IsPostBack Then Return
        ' Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, RLng("resid"), OrgFactory.EmpDriver.GetEmpID(CmsPass, RLng("id")))
        GainerID = Request("gainerid").ToString
        If Request.QueryString("resid") IsNot Nothing Then ResourceID = Convert.ToInt64(Request.QueryString("resid"))
        If Request.QueryString("type") IsNot Nothing Then GainerType = Convert.ToInt32(Request.QueryString("type"))
        If GainerType = RightsGainerType.IsEmployee Then
            GainerID = OrgFactory.EmpDriver.GetEmpID(CmsPass, Convert.ToInt64(GainerID)).Trim
        End If


        Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, ResourceID, GainerID)
        Dim lngRightsValue As Long = RightsValue.lngQX_VALUE

        ' Dim lngRightsValue As Long = Rights.GetRightsValue(ResourceID, GainerID, CType(GainerType, RightsGainerType))
        'Dim dtRights As DataTable = SDbStatement.Query("select * from dbo.CMS_RIGHTS where QX_OBJECT_ID='" + RLng("resid").ToString + "' and QX_GAINER_ID='" + OrgFactory.EmpDriver.GetEmpID(CmsPass, RLng("id")) + "' and QX_NAME=0 and QX_GAINER_TYPE=" + RStr("type").Trim).Tables(0)
        'Dim lngRightsValue As Long = 0
        'If dtRights.Rows.Count > 0 Then
        '    lngRightsValue = DbField.GetLng(dtRights.Rows(0), "QX_VALUE")
        'End If
        ShowUserRights(lngRightsValue)
    End Sub 


    Private Sub ShowUserRights(ByVal lngRightsValue As Long)
        If (lngRightsValue And CmsRightsDefine.RecView) = CmsRightsDefine.RecView Then
            chkRecView.Checked = True
        Else
            chkRecView.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecAdd) = CmsRightsDefine.RecAdd Then
            chkRecAdd.Checked = True
        Else
            chkRecAdd.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecEdit) = CmsRightsDefine.RecEdit Then
            chkRecEdit.Checked = True
        Else
            chkRecEdit.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecDel) = CmsRightsDefine.RecDel Then
            chkRecDel.Checked = True
        Else
            chkRecDel.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecPrint) = CmsRightsDefine.RecPrint Then
            chkRecPrint.Checked = True
        Else
            chkRecPrint.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecPrintList) = CmsRightsDefine.RecPrintList Then
            chkRecPrintList.Checked = True
        Else
            chkRecPrintList.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.DocCheckIn) = CmsRightsDefine.DocCheckIn Then
            chkDocCheckin.Checked = True
        Else
            chkDocCheckin.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.DocCheckoutCancel) = CmsRightsDefine.DocCheckoutCancel Then
            chkDocCheckoutCancel.Checked = True
        Else
            chkDocCheckoutCancel.Checked = False
        End If

        'If (lngRightsValue And CmsRightsDefine.DocCheckOut) = CmsRightsDefine.DocCheckOut Then
        '    chkDocCheckout.Checked = True
        'Else
        '    chkDocCheckout.Checked = False
        'End If
        If (lngRightsValue And CmsRightsDefine.DocGet) = CmsRightsDefine.DocGet Then
            chkDocGet.Checked = True
        Else
            chkDocGet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocViewOnline) = CmsRightsDefine.DocViewOnline Then
            chkDocViewOnline.Checked = True
        Else
            chkDocViewOnline.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocPrint) = CmsRightsDefine.DocPrint Then
            chkDocPrint.Checked = True
        Else
            chkDocPrint.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocViewHistory) = CmsRightsDefine.DocViewHistory Then
            chkDocViewHistory.Checked = True
        Else
            chkDocViewHistory.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.DocShare) = CmsRightsDefine.DocShare Then
            chkDocShare.Checked = True
        Else
            chkDocShare.Checked = False
        End If
        'If (lngRightsValue And CmsRightsDefine.DocMove) = CmsRightsDefine.DocMove Then
        '    chkDocMove.Checked = True
        'Else
        '    chkDocMove.Checked = False
        'End If

        If (lngRightsValue And CmsRightsDefine.MgrRightsSet) = CmsRightsDefine.MgrRightsSet Then
            chkMgrRightsSet.Checked = True
        Else
            chkMgrRightsSet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrColumnSet) = CmsRightsDefine.MgrColumnSet Then
            chkMgrColumnSet.Checked = True
        Else
            chkMgrColumnSet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrColumnShowSet) = CmsRightsDefine.MgrColumnShowSet Then
            chkMgrColumnShowSet.Checked = True
        Else
            chkMgrColumnShowSet.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrInputFormDesign) = CmsRightsDefine.MgrInputFormDesign Then
            chkMgrInputFormDesign.Checked = True
        Else
            chkMgrInputFormDesign.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrPrintFormDesign) = CmsRightsDefine.MgrPrintFormDesign Then
            chkMgrPrintFormDesign.Checked = True
        Else
            chkMgrPrintFormDesign.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrRelatedTable) = CmsRightsDefine.MgrRelatedTable Then
            chkMgrRelatedTable.Checked = True
        Else
            chkMgrRelatedTable.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrRowColor) = CmsRightsDefine.MgrRowColor Then
            chkMgrRowColor.Checked = True
        Else
            chkMgrRowColor.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrFormula) = CmsRightsDefine.MgrFormula Then
            chkFormula.Checked = True
        Else
            chkFormula.Checked = False
        End If
        'If (lngRightsValue And CmsRightsDefine.MgrEmailSmsSet) = CmsRightsDefine.MgrEmailSmsSet Then
        '    chkMgrEmailSmsSet.Checked = True
        'Else
        '    chkMgrEmailSmsSet.Checked = False
        'End If
        If (lngRightsValue And CmsRightsDefine.MgrResExport) = CmsRightsDefine.MgrResExport Then
            chkResExport.Checked = True
        Else
            chkResExport.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrResImport) = CmsRightsDefine.MgrResImport Then
            chkResImport.Checked = True
        Else
            chkResImport.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecSendEmailSms) = CmsRightsDefine.RecSendEmailSms Then
            chkResEmailSmsNotify.Checked = True
        Else
            chkResEmailSmsNotify.Checked = False
        End If
        'If (lngRightsValue And CmsRightsDefine.ResEmailSmsBatchSend) = CmsRightsDefine.ResEmailSmsBatchSend Then
        '    chkResEmailSmsBatchSend.Checked = True
        'Else
        '    chkResEmailSmsBatchSend.Checked = False
        'End If
        If (lngRightsValue And CmsRightsDefine.RecBatchUpdateField) = CmsRightsDefine.RecBatchUpdateField Then
            chkResBatchUpdateField.Checked = True
        Else
            chkResBatchUpdateField.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.RecBatchUpdateRecords) = CmsRightsDefine.RecBatchUpdateRecords Then
            chkRecBatchUpdateRecords.Checked = True
        Else
            chkRecBatchUpdateRecords.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.MgrResAdd) = CmsRightsDefine.MgrResAdd Then
            chkResAdd.Checked = True
        Else
            chkResAdd.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrResEdit) = CmsRightsDefine.MgrResEdit Then
            chkResEdit.Checked = True
        Else
            chkResEdit.Checked = False
        End If
        If (lngRightsValue And CmsRightsDefine.MgrResDel) = CmsRightsDefine.MgrResDel Then
            chkResDel.Checked = True
        Else
            chkResDel.Checked = False
        End If

        If (lngRightsValue And CmsRightsDefine.RecSearchMultitableList) = CmsRightsDefine.RecSearchMultitableList Then
            chkRecSearchMultitableList.Checked = True
        Else
            chkRecSearchMultitableList.Checked = False
        End If



        AdjustFunctionsSwitch() '调整功能开关决定的功能支持与否
    End Sub
    '----------------------------------------------------------------------
    '调整功能开关决定的功能支持与否
    '----------------------------------------------------------------------
    Private Sub AdjustFunctionsSwitch()
        '判断当前资源有无项目权限定义
        lbtnMenu.Enabled = CmsMenu.HasProjRightsOfResource(CmsPass, VLng("PAGE_RESID"))

        If CmsFunc.IsEnable("FUNC_DOCHISTORY") = False Then
            chkDocViewHistory.Enabled = False
            chkDocViewHistory.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_RELATION_TABLE") = False Then
            chkMgrRelatedTable.Enabled = False
            chkMgrRelatedTable.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_COLUMN_SET") = False Then
            chkMgrColumnSet.Enabled = False
            chkMgrColumnSet.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_COLUMNSHOW_SET") = False Then
            chkMgrColumnShowSet.Enabled = False
            chkMgrColumnShowSet.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_RES_ROWCOLOR") = False Then
            chkMgrRowColor.Enabled = False
            chkMgrRowColor.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_FORMULA") = False Then
            chkFormula.Enabled = False
            chkFormula.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_COMM_EMAILPHONE") = False Then
            'chkMgrEmailSmsSet.Enabled = False
            'chkMgrEmailSmsSet.Checked = False
            chkResEmailSmsNotify.Enabled = False
            chkResEmailSmsNotify.Checked = False
        End If

        'If CmsFunc.IsEnable("FUNC_BATCHSEND") = False Then
        '    chkResEmailSmsBatchSend.Enabled = False
        '    chkResEmailSmsBatchSend.Checked = False
        'End If

        If CmsFunc.IsEnable("FUNC_RES_IMP_OTHERTABLE") = False Then
            chkResImport.Enabled = False
            chkResImport.Checked = False
        End If

        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") = False Then
            chkResExport.Enabled = False
            chkResExport.Checked = False
        End If

        '在线浏览文档
        Dim bln As Boolean = CmsFunc.IsEnable("FUNC_ONLINEVIEW")
        If bln = False Then
            chkDocViewOnline.Enabled = False
            chkDocViewOnline.Checked = False
        End If

        '在线打印文档
        bln = CmsFunc.IsEnable("FUNC_ONLINEPRINT")
        If bln = False Then
            chkDocPrint.Enabled = False
            chkDocPrint.Checked = False
        End If

        '行过滤条件
        bln = CmsFunc.IsEnable("FUNC_ROWWHERE")
        If bln = False Then lbtnRowFilter.Visible = False

        '权限管理中支持增加/修改/删除资源
        bln = CmsFunc.IsEnable("FUNC_RESEDIT_RIGHTS")
        If bln = False Then
            chkResAdd.Enabled = False
            chkResAdd.Checked = False
            chkResEdit.Enabled = False
            chkResEdit.Checked = False
            chkResDel.Enabled = False
            chkResDel.Checked = False
        End If

        '是否支持文档表
        bln = CmsFunc.IsEnable("FUNC_TABLETYPE_DOC")
        If bln = False Then
            chkDocCheckin.Enabled = False
            chkDocCheckoutCancel.Enabled = False
            chkDocGet.Enabled = False
            chkDocShare.Enabled = False
            chkDocViewHistory.Enabled = False
            chkDocViewOnline.Enabled = False
            chkDocPrint.Enabled = False

            chkDocCheckin.Checked = False
            chkDocCheckoutCancel.Checked = False
            chkDocGet.Checked = False
            chkDocShare.Checked = False
            chkDocViewHistory.Checked = False
            chkDocViewOnline.Checked = False
            chkDocPrint.Checked = False
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lngRightsValue As Long = GetUserRightsFromUI()
        SaveRights(lngRightsValue)
    End Sub


    '----------------------------------------------------------------
    '获取界面上的权限设置信息
    '----------------------------------------------------------------
    Private Function GetUserRightsFromUI() As Long
        'AdjustFunctionsSwitch() '调整功能开关决定的功能支持与否

        Dim lngRightsValue As Long = 0
        If chkRecView.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecView
        If chkRecAdd.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecAdd
        If chkRecEdit.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecEdit
        If chkRecDel.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecDel
        If chkRecPrint.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecPrint
        If chkRecPrintList.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecPrintList

        If chkDocCheckin.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocCheckIn
        If chkDocCheckoutCancel.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocCheckoutCancel
        'If chkDocCheckout.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocCheckOut
        If chkDocGet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocGet
        If chkDocViewOnline.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocViewOnline
        If chkDocPrint.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocPrint
        If chkDocViewHistory.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocViewHistory
        If chkDocShare.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocShare
        'If chkDocMove.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.DocMove

        If chkMgrRightsSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrRightsSet
        If chkMgrColumnSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrColumnSet
        If chkMgrColumnShowSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrColumnShowSet
        If chkMgrInputFormDesign.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrInputFormDesign
        If chkMgrPrintFormDesign.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrPrintFormDesign
        If chkMgrRelatedTable.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrRelatedTable
        If chkMgrRowColor.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrRowColor
        If chkFormula.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrFormula
        'If chkMgrEmailSmsSet.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrEmailSmsSet

        If chkResExport.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResExport
        If chkResImport.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResImport
        If chkResEmailSmsNotify.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecSendEmailSms
        'If chkResEmailSmsBatchSend.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.ResEmailSmsBatchSend
        If chkResBatchUpdateField.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecBatchUpdateField
        If chkRecBatchUpdateRecords.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecBatchUpdateRecords

        If chkResAdd.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResAdd
        If chkResEdit.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResEdit
        If chkResDel.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.MgrResDel

        If chkRecSearchMultitableList.Checked Then lngRightsValue = lngRightsValue Or CmsRightsDefine.RecSearchMultitableList
        Return lngRightsValue
    End Function


    '------------------------------------------------------------------
    '保存权限
    '------------------------------------------------------------------
    Private Sub SaveRights(ByVal lngDefaultRightsValue As Long)
        '获取当前选中的权限获得者ID
        If GainerID = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        End If
        Dim RightsValue As DataCmsRights = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, ResourceID, GainerID)

        Dim alistGainerIDs As ArrayList = StringDeal.Split(GainerID, ",")



        If Not CmsRights.IsSetRights(CmsPass, ResourceID, GainerID) Then
            CmsRights.AddRightsGainer(CmsPass, ResourceID, GainerID, RightsGainerType.IsDepartment, lngDefaultRightsValue)
        Else
            CmsRights.EditRights(CmsPass, ResourceID, GainerID, RightsName.Resource, lngDefaultRightsValue)
        End If
        If GainerID.IndexOf(",") >= 0 Then
            '是多选情况，则必须清除选中的权限获得者和权限信息
            ShowUserRights(0) '清空对权限获得者的选中
            GainerID = Nothing '清空当前选中的权限获得者
        Else
            '是单选情况，显示正确的权限信息 

            Dim lngRightsValue As Long = lngDefaultRightsValue ' RightsValue.lngQX_VALUE
            ShowUserRights(lngRightsValue)
        End If

        '        GridDataBind() '绑定权限信息至DataGrid中
    End Sub



    ''------------------------------------------------------------------
    ''保存权限
    ''------------------------------------------------------------------
    'Private Sub SaveRights(ByVal lngDefaultRightsValue As Long)
    '    '获取当前选中的权限获得者ID
    '    If VStr("QXSET_GAINERID") = "" Then
    '        PromptMsg("请先选择权限获得者！")
    '        Return
    '    End If

    '    Dim alistGainerIDs As ArrayList = StringDeal.Split(VStr("QXSET_GAINERID"), ",")
    '    Dim strOneGainerID As String
    '    For Each strOneGainerID In alistGainerIDs
    '        '保存权限信息，暂时只支持表单操作权限
    '        CmsRights.EditRights(CmsPass, VLng("PAGE_RESID"), strOneGainerID, RightsName.Resource, lngDefaultRightsValue)
    '        'CmsRights.UpdateResourcePermissionForm(VLng("PAGE_RESID"), strOneGainerID, RightsName.Resource, Me.drpResourceForms.SelectedValue)
    '    Next

    '    If VStr("QXSET_GAINERID").IndexOf(",") >= 0 Then
    '        '是多选情况，则必须清除选中的权限获得者和权限信息
    '        ShowUserRights(0) '清空对权限获得者的选中
    '        ViewState("QXSET_GAINERID") = Nothing '清空当前选中的权限获得者
    '    Else
    '        '是单选情况，显示正确的权限信息
    '        Dim strRightsGainerID As String = VStr("QXSET_GAINERID")
    '        Dim lngRightsValue As Long = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, VLng("PAGE_RESID"), strRightsGainerID).lngQX_VALUE
    '        ShowUserRights(lngRightsValue)
    '    End If

    '    GridDataBind() '绑定权限信息至DataGrid中
    'End Sub




    Private Sub lbtnRowFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRowFilter.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = GainerID
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置行过滤条件！")
            Return
        End If
 
        Session("CMSBP_MTableSearch") = "/cmsweb/cmsrights/RightSetFrm.aspx?gainerid=" & Request("gainerid").ToString & "&resid=" + ResourceID.ToString + "&type=" & GainerType.ToString
        Response.Redirect("/cmsweb/adminres/MTableSearch.aspx?mnuresid=" & ResourceID.ToString & "&mtstype=" & MTSearchType.PersonalRowWhere & "&mnuempid=" & GainerID.Trim & "&mnufromadmin=rights" + IIf(GainerType = RightsGainerType.IsEmployee, "&mnuinherit=1", "").ToString, False)


        
    End Sub



    Private Sub lbtnRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnRow.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = GainerID.Trim
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置行记录权限！")
            Return
        End If
 
        Session("CMSBP_MTableSearchColDef") = "/cmsweb/cmsrights/RightSetFrm.aspx?gainerid=" & Request("gainerid").ToString & "&resid=" + ResourceID.ToString + "&type=" & GainerType.ToString
        Response.Redirect("/cmsweb/adminres/MTableSearchColDef.aspx?mnuresid=" & ResourceID.ToString & "&mtstype=" & MTSearchType.RowRights & "&mnuempid=" & GainerID.Trim + IIf(GainerType = RightsGainerType.IsEmployee, "&mnuinherit=1", "").ToString, False)
 
    End Sub

    Private Sub lbtnColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnColumn.Click
        '获取当前选中的权限获得者ID
        Dim strRightsGainerIDs As String = GainerID.Trim
        If strRightsGainerIDs = "" Then
            PromptMsg("请先选择权限获得者！")
            Return
        ElseIf strRightsGainerIDs.IndexOf(",") > 0 Then
            PromptMsg("不能同时对多个人员设置列字段权限！")
            Return
        End If
 
        Session("CMSBP_RightsSetColumn") = "/cmsweb/cmsrights/RightSetFrm.aspx?gainerid=" & Request("gainerid").ToString & "&resid=" + ResourceID.ToString + "&type=" & GainerType.ToString
        Response.Redirect("/cmsweb/cmsrights/RightsSetColumn.aspx?mnuresid=" & ResourceID.ToString & "&gainerid=" & GainerID.Trim + IIf(GainerType = RightsGainerType.IsEmployee, "&mnuinherit=1", "").ToString, False)
 
    End Sub



    Public Property GainerID() As String
        Get
            If ViewState("gainerid") Is Nothing Then ViewState("gainerid") = ""
            Return ViewState("gainerid").ToString
        End Get
        Set(ByVal value As String)
            ViewState("gainerid") = value
        End Set
    End Property



    Public Property GainerType() As Integer
        Get
            If ViewState("gainertype") Is Nothing Then ViewState("gainertype") = 0
            Return Convert.ToInt32(ViewState("gainertype"))
        End Get
        Set(ByVal value As Integer)
            ViewState("gainertype") = value
        End Set
    End Property

    Public Property ResourceID() As Long
        Get
            If ViewState("ResourceID") Is Nothing Then ViewState("ResourceID") = 0
            Return Convert.ToInt64(ViewState("ResourceID"))
        End Get
        Set(ByVal value As Long)
            ViewState("ResourceID") = value
        End Set
    End Property


    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Write("<script>window.parent.location.href='" + Session("CMSBP_DepEmpList").ToString + "';</script>")
    End Sub
End Class
