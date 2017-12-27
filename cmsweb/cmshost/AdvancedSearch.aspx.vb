Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class AdvancedSearch
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
        '在此处放置初始化页的用户代码
    End Sub
    Protected Overrides Sub CmsPageDealFirstRequest()
        Try
            Dim intMode As Integer = 1

            If HasSearchField() = False Then
                PromptMsg("不存在高级查询字段，请先为本资源设置高级查询字段！")
                ButtonSearch.Visible = False
                ButtonExit.Visible = False
            Else
                Dim datForm As DataInputForm
                datForm = FormManager.LoadSearchForm(Panel1, Nothing, CmsPass, RLng("mnuresid"), , RLng("mnurecid"), , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"))

                Session("PAGE_EDITADV_DATAFORM") = datForm

                ButtonSearch.Visible = True
                ButtonExit.Visible = True
                Dim Heigth As Integer = Convert.ToInt32(Panel1.Height.ToString().Remove(Panel1.Height.ToString.Length - 2, 2)) - 30
                Dim Left As Integer = Convert.ToInt32(Convert.ToInt32(Panel1.Width.ToString().Remove(Panel1.Width.ToString.Length - 2, 2)) / 2 + 120)
                Dim strStyle As String = "POSITION: absolute;top:" & Heigth & "px;left:" & Left & "px"
                Dim strStyle2 As String = "POSITION: absolute;top:" & Heigth & "px;left:" & Left + 45 & "px"
                ButtonSearch.Attributes.Add("style", strStyle)
                ButtonExit.Attributes.Add("style", strStyle2)
                ButtonSearch.Attributes.Add("onClick", "return CheckValue(Form1);")
            End If
        Catch ex As Exception
            PromptMsg(ex.Message, , True)
        End Try
    End Sub
    Function HasSearchField() As Boolean
        Dim ds As DataSet = CTableStructure.GetColumnsByDataset(CmsPass, RLng("mnuresid"), True, True)
        If ds.Tables(0).Rows.Count <= 0 Then
            Return False
        Else
            If SearchFileCount(ds, "CD_IS_SEARCH") > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Function SearchFileCount(ByVal ds As DataSet, ByVal ColName As String) As Integer
        Dim dv As DataView = ds.Tables(0).DefaultView
        Dim drv As DataRowView
        Dim intCount As Integer = 0
        For Each drv In dv
            If DbField.GetLng(drv, "CD_IS_SEARCH") = 1 Then
                intCount = intCount + 1
            Else
                intCount = intCount + 0
            End If
        Next
        Return intCount
    End Function

    Private Sub ButtonSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click

        Dim BlnIsEnd As Boolean = True

        Dim strWhere As String = ""

        Dim hashUICtrlValue As New Hashtable '存放界面尚所有控件值，仅为了在保存出错时能将所有值重新显示在界面上。 Key：控件名称；Value：控件值

        Dim datForm As DataInputForm = CType(Session("PAGE_EDITADV_DATAFORM"), DataInputForm)
        Dim hashColValuesOfResources As Hashtable = FormManager.GetColValuesOfResourcesFromUI(CmsPass, Request, datForm.hashUICtls, RLng("mnuresid"), hashUICtrlValue, True)


        Dim alistColumns As New ArrayList
        CTableStructure.GetColumnsByCollection(CmsPass, RLng("mnuresid"), alistColumns, Nothing, False, True, " CD_IS_SEARCH=1", False, False)

        Dim hashFields As New Hashtable
        hashFields = CType(hashColValuesOfResources(CStr(RLng("mnuresid"))), Hashtable)
        Dim en As IDictionaryEnumerator = hashFields.GetEnumerator()
        Do While en.MoveNext = True
            Dim strColName As String = CStr(en.Key)
            Dim strColValue As String = CStr(en.Value)
            If strColName.IndexOf("DDL") < 0 And strColName.IndexOf("___") < 0 Then
                If strColName.EndsWith("STR") And strColName.IndexOf("__") >= 0 Then
                    Dim ColName As String = strColName.Substring(0, strColName.Length - 5)
                    Dim strEndColName As String = strColName.Substring(0, strColName.Length - 5) & "__END"
                    Dim strEndColValue As String = CStr(hashFields.Item(strEndColName))
                    If strColValue = "" AndAlso strEndColValue <> "" Then
                        PromptMsg("请输入开始时间！")
                        BlnIsEnd = False
                    ElseIf strColValue <> "" AndAlso strEndColValue = "" Then
                        PromptMsg("请输入结束时间！")
                        BlnIsEnd = False
                    ElseIf strColValue <> "" AndAlso strEndColValue <> "" Then

                        If IsDate(strColValue) = True And IsDate(strEndColValue) = True Then
                            If Date.Parse(strColValue) > Date.Parse(strEndColValue) Then
                                PromptMsg("开始日期不能大于结束日期")
                                BlnIsEnd = False
                            Else
                                strWhere &= " and (" & ColName & " between '" & Date.Parse(strColValue) & "' and '" & Date.Parse(strEndColValue) & "')"
                            End If
                        End If
                    End If
                ElseIf strColName.EndsWith("END") And strColName.IndexOf("__") > 0 Then
                    Dim ColName As String = strColName.Substring(0, strColName.Length - 5)
                    Dim strStartColName As String = strColName.Substring(0, strColName.Length - 5) & "__STR"
                    Dim strStartColValue As String = CStr(hashFields.Item(strStartColName))
                    If strColValue = "" AndAlso strStartColValue <> "" Then
                        PromptMsg("请输入结束时间！")
                        BlnIsEnd = False
                    ElseIf strColValue <> "" AndAlso strStartColValue = "" Then
                        PromptMsg("请输入开始时间！")
                        BlnIsEnd = False
                    ElseIf strColValue <> "" AndAlso strStartColValue <> "" Then
                        If IsDate(strColValue) = True And IsDate(strStartColValue) = True Then
                            If Date.Parse(strColValue) < Date.Parse(strStartColValue) Then
                                PromptMsg("开始日期不能大于结束日期")
                                BlnIsEnd = False
                            Else
                                strWhere &= " and (" & ColName & " between '" & Date.Parse(strStartColValue) & "' and '" & Date.Parse(strColValue) & "')"
                            End If
                        End If
                    End If
                Else
                    If strColValue <> "" Then
                        Dim strConditions As String = CStr(hashFields.Item("DDL___" & strColName))
                        Dim datCol As DataTableColumn
                        'Dim blnWidthSetForBinFile As Boolean = False
                        Dim intHeight As Integer = 0
                        For Each datCol In alistColumns
                            If datCol.ColName = strColName Then
                                strWhere &= " and " & CTableStructure.GenerateFieldWhere(strColName, strColValue, datCol.ColType, strConditions, 0)
                            End If
                        Next
                    End If
                End If
            End If
        Loop
        If strWhere.IndexOf("11=22") >= 0 Then
                PromptMsg(CmsMessage.GetMsg(CmsPass, "SEARCH_MSG"))
            BlnIsEnd = False
        End If
        If BlnIsEnd Then
            If strWhere.StartsWith(" and") Then
                strWhere = strWhere.Substring(5)
            End If
            If RStr("MenuKey") = "HostAdvancedSearch" Then
                Session("CMS_HOSTTABLE_WHERE") = strWhere
            ElseIf RStr("MenuKey") = "SubAdvancedSearch" Then
                Session("CMS_SUBTABLE_WHERE") = strWhere
            End If

            'Response.Write(strWhere)
            Page.RegisterStartupScript("message", "<script language='javascript'>var a=self.opener.location.href;a=a.substr(0,a.length-1);window.close();window.top.opener.document.location.replace(a); </script>")
        Else
            FormManager.LoadSearchForm(Panel1, Nothing, CmsPass, RLng("mnuresid"), hashUICtrlValue, RLng("mnurecid"), , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"))
        End If

    End Sub

    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Page.RegisterStartupScript("message", "<script language='javascript'>var a=self.opener.location.href;a=a.substr(0,a.length-1);window.close();window.top.opener.document.location.replace(a); </script>")
    End Sub
End Class

End Namespace
