Option Strict On
Option Explicit On 

Imports System.IO
Imports System.Text

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class FieldGetAdvDictionary
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

    '这些变量只在同一个请求中有效
    Private m_dvCols As DataView = Nothing '高级字典定义的字段列表
    Private m_lngMainResID As Long = 0
    Private m_lngDictResID As Long = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        End Sub




    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If Not Request.QueryString("mode") Is Nothing AndAlso Request.QueryString("mode").ToString() <> "" Then
            ViewState("lngMode") = RStr("mode")
            If Request.QueryString("mode").ToString() = "undefined" Then
                ViewState("lngMode") = "2"
            End If
        Else
            ViewState("lngMode") = "2"
        End If


        If VStr("PAGE_COLNAME") = "" Then
            ViewState("PAGE_COLNAME") = RStr("colname")
            ViewState("PAGE_COLDISPNAME") = CTableStructure.GetColDispName(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        End If
        If VStr("PAGE_COLVALUE") = "" Then
            'ViewState("PAGE_COLVALUE") = RStr("colval")
            Dim strTemp As String = RStr("colval")
            ViewState("PAGE_COLVALUE") = strTemp.Replace("x__plus", "+")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        LoadRowClickScript() '设置客户端Javascript

        lbtnCancelSearch.ToolTip = "取消查询和排序"
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        ViewState("CMS_ADVDICT_SEARCH_COLTYPE") = WebUtilities.LoadResColumnsInDdlist(CmsPass, VLng("PAGE_DICTRESID"), ddlColumns, True, False, False) '初始化DropDownList中字段列表项
        ddlColumns.SelectedValue = RStr("ddlColumns")
        WebUtilities.LoadConditionsInDdlist(CmsPass, ddlConditions, True)   '初始化DropDownList中查找条件项

        '查看有无高级字典的过滤字段定义
        Dim strFilterWhere As String = ""
        Dim strFilterCol As String = RStr("filtercol1")
        Dim strFilterColVal As String = RStr("filtercolval1")
        If strFilterCol <> "" AndAlso strFilterColVal <> "" Then strFilterWhere = strFilterCol & "='" & strFilterColVal & "'"
        strFilterCol = RStr("filtercol2")
        strFilterColVal = RStr("filtercolval2")
        If strFilterCol <> "" AndAlso strFilterColVal <> "" Then strFilterWhere = CmsWhere.AppendAnd(strFilterWhere, strFilterCol & "='" & strFilterColVal & "'")
        strFilterCol = RStr("filtercol3")
        strFilterColVal = RStr("filtercolval3")
        If strFilterCol <> "" AndAlso strFilterColVal <> "" Then strFilterWhere = CmsWhere.AppendAnd(strFilterWhere, strFilterCol & "='" & strFilterColVal & "'")

        ViewState("PAGE_FILTER_WHERE") = strFilterWhere

        '自动先定位（检索）到字典字段中指定的值
        Dim strWhere As String = ""
        Dim strDictColName As String = CTableColAdvDictionary.GetMainColumnInDictRes(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
        If VStr("PAGE_COLVALUE") <> "" Then
            ddlColumns.SelectedValue = strDictColName
            ddlConditions.SelectedValue = "LIKE"
            txtSearchValue.Text = VStr("PAGE_COLVALUE")
            strWhere = strDictColName & " LIKE '%" & VStr("PAGE_COLVALUE") & "%'"
            End If

            lbtnSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_START")
            lbtnCancelSearch.Text = CmsMessage.GetUI(CmsPass, "CONT_MNU_SEARCH_CANCEL")
            lnkOK.Text = CmsMessage.GetUI(CmsPass, "TITLE_CONFIRM")
            lnkCancel.Text = CmsMessage.GetUI(CmsPass, "TITLE_CANCEL")

        '---------------------------------------------------------------
        '初始化分页控件
        Cmspager1.PageRows = CmsConfig.GetInt("SYS_CONFIG", "TABLEROWS_DICT")
        Cmspager1.Language = CmsPass.Employee.Language
        Cmspager1.BgColor = "#e7ebef"
        Cmspager1.TableAlign = "left"
        Cmspager1.ButtonAlign = "left"
        Cmspager1.WordAlign = "left"
        Cmspager1.TableHeight = "25px"
        Cmspager1.TotalWidth = "100%"
        Cmspager1.ButtonsWidth = "130px"
        '---------------------------------------------------------------

        ViewState("PAGE_WHERE") = strWhere
        GridDataBind() '填入表格数据
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub DataGrid1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.Init
        Try
            If Session("CMS_PASSPORT") Is Nothing Then '用户尚未登录或者Session过期，转至登录页面
                Response.Redirect("/cmsweb/Logout.aspx", True)
                Return
            End If
            CmsPageSaveParametersToViewState() '将传入的参数保留为ViewState变量，便于在页面中提取和修改

            WebUtilities.InitialDataGrid(DataGrid1, CmsConfig.GetInt("SYS_CONFIG", "TABLEROWS_DICT"), True)  '设置DataGrid显示属性
            CreateDataGridColumn() '创建列表
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub DataGrid1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemCreated
        If DataGrid1.Items.Count > 0 And e.Item.ItemIndex = -1 Then
            Dim i As Integer = 0

            '-----------------------------------------------------------
            '找到行颜色设置的几个行的序号
            Dim datRes As DataResource = ResFactory.ResService.GetOneResource(CmsPass, VLng("PAGE_DICTRESID"))
            Dim intCol1() As Integer = {-1, -1, -1}
            Dim intCol1Type() As Integer = {0, 0, 0}
            Dim intCol2() As Integer = {-1, -1, -1}
            Dim intCol2Type() As Integer = {0, 0, 0}
            Dim intCol3() As Integer = {-1, -1, -1}
            Dim intCol3Type() As Integer = {0, 0, 0}
            For i = 0 To DataGrid1.Columns.Count - 1
                Dim bc As BoundColumn = CType(DataGrid1.Columns(i), BoundColumn)
                Dim j As Integer
                For j = 0 To 2
                    If datRes.RowColorCol1(j) <> "" And bc.DataField = datRes.RowColorCol1(j) Then
                        intCol1(j) = i
                        If IsNumeric(bc.FooterText) Then intCol1Type(j) = CInt(bc.FooterText)
                    End If
                Next
                For j = 0 To 2
                    If datRes.RowColorCol2(j) <> "" And bc.DataField = datRes.RowColorCol2(j) Then
                        intCol2(j) = i
                        If IsNumeric(bc.FooterText) Then intCol2Type(j) = CInt(bc.FooterText)
                    End If
                Next
                For j = 0 To 2
                    If datRes.RowColorCol3(j) <> "" And bc.DataField = datRes.RowColorCol3(j) Then
                        intCol3(j) = i
                        If IsNumeric(bc.FooterText) Then intCol3Type(j) = CInt(bc.FooterText)
                    End If
                Next
            Next
            '-----------------------------------------------------------

            For i = 0 To DataGrid1.Items.Count - 1
                    Dim row As DataGridItem = DataGrid1.Items(i)


                '-----------------------------------------------------------
                '将所有字段值写入行属性中，便于“确定”后提取
                Dim j As Integer = 0
                Dim drv As DataRowView
                For Each drv In m_dvCols
                    If j < DataGrid1.Columns.Count Then
                        Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                        Dim strMainCtrl As String = ""
                        If strMainCol <> "" Then strMainCtrl = TextboxName.GetCtrlName(strMainCol, m_lngMainResID)
                        Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                        Dim strDictCtrl As String = ""
                        If strDictCol <> "" Then strDictCtrl = TextboxName.GetCtrlName(strDictCol, m_lngDictResID)

                        Dim strTemp As String = row.Cells(j).Text.Trim()
                        If strTemp = "&nbsp;" OrElse strTemp = "&amp;nbsp;" Then
                            strTemp = ""
                        End If
                        If strMainCtrl <> "" Then row.Attributes.Add(strMainCtrl, strTemp) '在客户端保存记录ID
                            If strDictCol <> "" Then row.Attributes.Add(strDictCtrl, strTemp) '在客户端保存记录ID
                            'row.Attributes.Add("RecID", DbField.GetStr(drv, "ID"))
                        j += 1
                    Else
                        Exit For
                    End If
                Next
                '-----------------------------------------------------------

                '-----------------------------------------------------------
                '设置满足条件的行颜色
                Dim blnSet As Boolean = False
                blnSet = ResTableRowColor.SetRowColor(datRes, row, intCol1, intCol1Type, 1)
                If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol2, intCol2Type, 2)
                If blnSet = False Then ResTableRowColor.SetRowColor(datRes, row, intCol3, intCol3Type, 3)
                '-----------------------------------------------------------



            Next
        End If
    End Sub

    'Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    DataGrid1.CurrentPageIndex = e.NewPageIndex

    '    GridDataBind()  '填入表格数据
    'End Sub

    Private Sub DataGrid1_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid1.SortCommand
        '每次点击替换排序次序
        If VStr("PAGE_ORDERBY_TYPE") = "ASC" Then
            ViewState("PAGE_ORDERBY_TYPE") = "DESC"
        Else
            ViewState("PAGE_ORDERBY_TYPE") = "ASC"
        End If
        '排序显示数据
        ViewState("PAGE_ORDERBY") = "ORDER BY " & e.SortExpression & " " & VStr("PAGE_ORDERBY_TYPE")

        GridDataBind()
    End Sub

    Private Sub Cmspager1_Click(ByVal sender As System.Object, ByVal eventArgument As String) Handles Cmspager1.Click
        GridDataBind(eventArgument) '填入表格数据
    End Sub

    Private Sub lbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSearch.Click
        DataGrid1.CurrentPageIndex = 0 '查询前必须先置为首页

        '组织Where语句
        Dim hashColType As Hashtable = CType(ViewState("CMS_ADVDICT_SEARCH_COLTYPE"), Hashtable)
        Dim strColumns As String = ddlColumns.SelectedValue
        Dim lngColType As Long = CLng(hashColType(strColumns))
        Dim strWhere As String = CTableStructure.GenerateFieldWhere(strColumns, txtSearchValue.Text.Trim(), lngColType, ddlConditions.SelectedValue, SDbConnectionPool.GetDbConfig().DatabaseType)
        ViewState("PAGE_WHERE") = strWhere

        GridDataBind()  '填入表格数据
    End Sub

    Private Sub lbtnCancelSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnCancelSearch.Click
        ViewState("PAGE_WHERE") = ""
        ViewState("PAGE_ORDERBY") = ""
        ddlColumns.SelectedValue = ""
        txtSearchValue.Text = ""
        ddlConditions.SelectedValue = ""
        GridDataBind()  '填入表格数据
    End Sub

    '------------------------------------------------------------------
    '创建修改表结构的DataGrid的列
    '------------------------------------------------------------------
    Private Sub CreateDataGridColumn()
        Try
            DataGrid1.AutoGenerateColumns = False
            DataGrid1.Columns.Clear()

            m_lngMainResID = VLng("PAGE_RESID")

            Dim col As BoundColumn
            Dim ds As DataSet = CTableColAdvDictionary.GetDictionaryByMatchAndReference(CmsPass, VLng("PAGE_RESID"), VStr("PAGE_COLNAME"))
            m_dvCols = ds.Tables(0).DefaultView
                If m_dvCols.Count > 0 Then
                    m_lngDictResID = DbField.GetLng(m_dvCols(0), "CDZ2_RESID2") '获取字典关联资源ID
                    If DbField.GetBool(m_dvCols(0), "CDZ2_ADD_DICTREC") Or DbField.GetBool(m_dvCols(0), "CDZ2_EDIT_DICTREC") Then
                        imgEdit.Visible = True
                        If DbField.GetBool(m_dvCols(0), "CDZ2_ADD_DICTREC") Then lbtnAdd.Visible = True
                        If DbField.GetBool(m_dvCols(0), "CDZ2_EDIT_DICTREC") Then lbtnEdit.Visible = True
                    End If
                End If
            ViewState("PAGE_DICTRESID") = m_lngDictResID

            '获取指定用户的列字段权限信息
            Dim strColsOfNoRights As String = CmsRights.GetRightsData(CmsPass, m_lngDictResID, CmsPass.Employee.ID).strQX_ACCESS_COLS
            If strColsOfNoRights <> "" Then strColsOfNoRights = "," & strColsOfNoRights & ","

            Dim strResTableType As String = CmsPass.GetDataRes(m_lngMainResID).ResTableType
            Dim strBriefLanguage As String = CmsMessage.GetBriefLanguage(CmsPass.Employee.Language)

            Dim dvDict As DataView = ResFactory.TableService(CmsPass.GetDataRes(m_lngDictResID).ResTableType).GetTableColumns(CmsPass, m_lngDictResID)
            'Dim dvDict As DataView = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), "select * from CMS_TABLE_SHOW inner join cms_table_define on(cd_resid=cs_resid and cd_colName=cs_colname) where CS_RESID=" & m_lngDictResID & " AND CD_RESID=" & m_lngDictResID & "  order by CS_SHOW_ORDER ASC").Tables(0).DefaultView()
            Dim strRowFilter As String = dvDict.RowFilter.Replace("AND CS_SHOW_ENABLE=1", "")
            Dim intWidth As Integer = 0
            Dim drv As DataRowView
            For Each drv In m_dvCols
                Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                'Dim intDictType As ADVDICT_COLTYPE = CType(DbField.GetInt(drv, "CDZ2_TYPE"), ADVDICT_COLTYPE)

                Dim blnShowCol As Boolean = True
                If strColsOfNoRights <> "" Then
                    If strColsOfNoRights.IndexOf("," & strDictCol & ",") >= 0 Then blnShowCol = False '无权限，不创建列字段
                End If
                If blnShowCol = True Then
                    If strRowFilter = "" Then
                        dvDict.RowFilter = "CS_COLNAME='" & strDictCol & "'"
                    Else
                        dvDict.RowFilter = strRowFilter & " and CS_COLNAME='" & strDictCol & "'"
                    End If

                    'If dvDict.Count > 0 AndAlso (intDictType = ADVDICT_COLTYPE.Matching OrElse intDictType = ADVDICT_COLTYPE.Reference) Then
                    If dvDict.Count > 0 Then
                        If DbField.GetStr(dvDict(0), "CS_SHOW_ENABLE") = "1" Then
                            intWidth += WebUtilities.CreateOneColumn(DataGrid1, dvDict(0), "", True, False, "", strBriefLanguage, strResTableType)  '只显示在字典中出现的字段
                        Else
                            WebUtilities.CreateOneColumn(DataGrid1, dvDict(0), "", True, False, "", strBriefLanguage, strResTableType, False) '只显示在字典中出现的字段
                        End If
                    End If
                End If
            Next
            DataGrid1.Width = Unit.Pixel(intWidth)
        Catch ex As Exception
            SLog.Err("在高级字典中创建列字段异常失败！", ex)
            Throw New CmsException("高级字典定义有错误，请检查！")
        End Try
    End Sub

    '--------------------------------------------------------------------------------------------
    '创建DataGrid的列字段和填充内容
    '--------------------------------------------------------------------------------------------
    Public Sub GridDataBind(Optional ByVal strHostPageCommand As String = "MoveCurrentPage")
        If VLng("PAGE_DICTRESID") = 0 Then Return
        Try
            '--------------------------------------------------------------------------------------------
            '绑定数据前的分页控件的处理
            Cmspager1.DealPageCommandBeforeBind(strHostPageCommand)   '设置页数
            DataGrid1.CurrentPageIndex = 0
            '--------------------------------------------------------------------------------------------

                '组装条件语句
                Dim ResID As Long = VLng("PAGE_RESID")
                Dim datRes As DataResource = CmsPass.GetDataRes(ResID)

                If datRes.ResType = ResInheritType.IsInherit Then
                    ResID = datRes.IndepParentResID
                End If

                Dim strWhere As String = MultiTableSearch.AssembleAdvDictWhereForSqlQuery(CmsPass, ResID, VStr("PAGE_COLNAME"))
                strWhere = CmsWhere.FilterWhere(CmsPass, strWhere)
                strWhere = CmsWhere.AppendAnd(strWhere, VStr("PAGE_WHERE"))
                strWhere = CmsWhere.AppendAnd(strWhere, VStr("PAGE_FILTER_WHERE"))  

                '绑定数据
                Dim strResTableType As String = ResFactory.ResService.GetResTableType(CmsPass, VLng("PAGE_DICTRESID"))
                Dim intTotalRecNum As Integer = 0
                Dim ds As DataSet = ResFactory.TableService(strResTableType).GetDict2TableData(CmsPass, VLng("PAGE_DICTRESID"), strWhere, VStr("PAGE_ORDERBY"), Cmspager1.RecStartOnCurPage, Cmspager1.PageRows, intTotalRecNum)
                Cmspager1.TotalRecordNumber = intTotalRecNum

                '绑定数据集
                Dim dv As DataView = ds.Tables(0).DefaultView
                DataGrid1.VirtualItemCount = dv.Count '分页用
                DataGrid1.DataSource = dv
                DataGrid1.DataBind()
            Catch ex As Exception
                PromptMsg("", ex, True)
            End Try
    End Sub

    Private Sub LoadRowClickScript()
        If Not IsStartupScriptRegistered("RowClick") Then
            Dim strScript As String = ""
            strScript &= "<script language=""javascript"">" & Environment.NewLine
            strScript &= "<!--" & Environment.NewLine
                strScript &= "function RowLeftClickNoPost(src){" & Environment.NewLine
                strScript &= "var o = document.getElementById(src);    " & Environment.NewLine
                strScript &= "var trs = document.getElementById('DataGrid1').rows; " & Environment.NewLine
                strScript &= "for(var i=1;i<trs.length;i++)  " & Environment.NewLine
                strScript &= "{  " & Environment.NewLine
                strScript &= "trs[i].style.backgroundColor=""white"";  " & Environment.NewLine
                strScript &= "}  " & Environment.NewLine
                strScript &= "o.style.backgroundColor= '#C4D9F9';" & Environment.NewLine
                strScript &= "document.getElementById('txtRecID').value=src;" & Environment.NewLine
                Dim drv As DataRowView
                For Each drv In m_dvCols
                    Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                    Dim strMainCtrl As String = ""
                    If strMainCol <> "" Then strMainCtrl = TextboxName.GetCtrlName(strMainCol, m_lngMainResID)
                    Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                    Dim strDictCtrl As String = ""
                    If strDictCol <> "" Then strDictCtrl = TextboxName.GetCtrlName(strDictCol, m_lngDictResID)
                    ' Dim strRecID As String = DbField.GetStr(drv, "ID")
                    'event.srcElement.getAttribute("link") 
                    If strMainCtrl <> "" Then
                        strScript &= "    self.document.forms(0)." & strMainCtrl & ".value = o.attributes.getNamedItem('" & strMainCtrl & "').nodeValue ;" & Environment.NewLine
                        RegisterHiddenField(strMainCtrl, "") '注册客户端Form隐含变量
                    End If
                    If strDictCtrl <> "" Then
                        strScript &= "    self.document.forms(0)." & strDictCtrl & ".value = o.attributes.getNamedItem('" & strDictCtrl & "').nodeValue ;" & Environment.NewLine
                        RegisterHiddenField(strDictCtrl, "") '注册客户端Form隐含变量
                    End If
                Next
                 

                strScript &= "}" & Environment.NewLine
                strScript &= "" & Environment.NewLine
                strScript &= "function ReturnSelectedValue(){" & Environment.NewLine
                strScript &= "    var ctlName;" & Environment.NewLine

                If CType(ViewState("lngMode"), Long) = InputMode.ViewInHostTable OrElse CType(ViewState("lngMode"), Long) = InputMode.ViewInRelTable Then
                    strScript &= " " & Environment.NewLine
                Else
                    For Each drv In m_dvCols
                        Dim intDictType As ADVDICT_COLTYPE = CType(DbField.GetInt(drv, "CDZ2_TYPE"), ADVDICT_COLTYPE)
                        Dim strMainCol As String = DbField.GetStr(drv, "CDZ2_COL1")
                        Dim strMainCtrl As String = ""
                        If strMainCol <> "" Then strMainCtrl = TextboxName.GetCtrlName(strMainCol, m_lngMainResID)
                        Dim strDictCol As String = DbField.GetStr(drv, "CDZ2_COL2")
                        Dim strDictCtrl As String = ""
                        If strDictCol <> "" Then strDictCtrl = TextboxName.GetCtrlName(strDictCol, m_lngDictResID)

                        '本表字段的赋值：只需对匹配字段赋值
                        If intDictType = ADVDICT_COLTYPE.Matching Then
                            If strMainCtrl <> "" Then
                                strScript &= "    try{" & Environment.NewLine
                                strScript &= "        if (self.document.forms(0)." & strMainCtrl & ".value != ""&nbsp;""){" & Environment.NewLine '&nbsp;
                                strScript &= "            ctlName=eval(""window.opener.Form1." & strMainCtrl & """)" & Environment.NewLine
                                strScript &= "            ctlName.value = self.document.forms(0)." & strMainCtrl & ".value" & Environment.NewLine
                                strScript &= "        }" & Environment.NewLine
                                strScript &= "    }catch(ex){" & Environment.NewLine
                                strScript &= "    }" & Environment.NewLine
                            End If
                        End If

                        '关联父表字段的赋值：所有匹配字段和参考字段赋值
                        If strDictCtrl <> "" Then
                            strScript &= "    try{" & Environment.NewLine
                            strScript &= "        if (self.document.forms(0)." & strDictCtrl & ".value != ""&nbsp;""){" & Environment.NewLine '&nbsp;
                            strScript &= "            ctlName=eval(""window.opener.Form1." & strDictCtrl & """)" & Environment.NewLine
                            strScript &= "            ctlName.value = self.document.forms(0)." & strDictCtrl & ".value" & Environment.NewLine
                            strScript &= "        }" & Environment.NewLine
                            strScript &= "    }catch(ex){" & Environment.NewLine
                            strScript &= "    }" & Environment.NewLine
                        End If
                    Next
                End If

                strScript &= "    window.close();" & Environment.NewLine
                strScript &= "}" & Environment.NewLine
                strScript &= "-->" & Environment.NewLine
                strScript &= "</script>" & Environment.NewLine
                strScript &= "" & Environment.NewLine

                RegisterStartupScript("RowClick", strScript)
            End If
    End Sub

        Protected Sub lbtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnAdd.Click
            Dim strUrl As String = "../cmshost/RecordEdit.aspx?mnuinmode=2&timeid=2648484618&MenuSection=&MenuKey=&MNURESLOCATE=1&cmsaction=&mnuformresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostrecid=&mnurecid=&mnuresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuformname="
            Response.Write("<script>window.location.href='" + strUrl + "&backpage='+window.location.href.replace(/\&/g,'[and]');</script>")
        End Sub

        Protected Sub lbtnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnEdit.Click
            If txtRecID.Text.Trim() <> "" Then
                Dim strUrl As String = "../cmshost/RecordEdit.aspx?mnuinmode=4&timeid=567072032&MenuSection=&MenuKey=&MNURESLOCATE=1&cmsaction=&mnuformresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuhostrecid=&mnurecid=" + txtRecID.Text.Trim() + "&mnuresid=" + VLng("PAGE_DICTRESID").ToString + "&mnuformname="
                Response.Write("<script>window.location.href='" + strUrl + "&backpage='+window.location.href.replace(/\&/g,'[and]');</script>")
            Else
                Response.Write("<script>alert('请选择有效记录！');</script>")
            End If
        End Sub

        Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                e.Item.Attributes.Add("ID", DbField.GetStr(drv, "ID"))
                e.Item.Attributes.Add("OnClick", "RowLeftClickNoPost('" & DbField.GetStr(drv, "ID") & "')") '在客户端生成：点击记录的响应方法OnClick()
            End If
        End Sub
    End Class

End Namespace
