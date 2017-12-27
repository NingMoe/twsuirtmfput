Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class ChartDefine
        Inherits Unionsoft.Platform.CmsPage

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
        If Not Page.IsPostBack Then

            ViewState("EditFlag") = "1" '新增
            ViewState("Id") = TimeId.CurrentMilliseconds()
            InitControls()
            If RStr("chartid") <> "" Then
                ViewState("EditFlag") = "0" '修改
                ViewState("Id") = RStr("chartid")
                BindData(ViewState("Id").ToString())
            End If
        End If
    End Sub
    Private Sub InitControls()
        
        Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(Me.CmsPass, RLng("mnuresid"), False, True, False, False)
        For i As Integer = 0 To alistColumns.Count - 1
            Dim datCol As DataTableColumn = CType(alistColumns(i), DataTableColumn)
            Me.ddlst_ConditionColumn1.Items.Add(New ListItem(datCol.ColDispName, datCol.ColID.ToString()))

            ' If datCol.ColType = FieldDataType.Date Or datCol.ColType = FieldDataType.Time Then
            Me.ddlst_ConditionColumn2.Items.Add(New ListItem(datCol.ColDispName, datCol.ColID.ToString()))

            'End If

            Me.ddlst_ValueColumn.Items.Add(New ListItem(datCol.ColDispName, datCol.ColID.ToString()))
        Next
        Me.ddlst_ConditionColumn2.Items.Insert(0, New ListItem("", "0"))
    End Sub

    '绑定数据
    Private Sub BindData(ByVal Id As String)
        Dim dt As DataTable = ChartBasicCode.GetChart(Id)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Me.txt_ChartName.Text = DbField.GetStr(dr, "CharTNAME")
            Me.txt_Content.Text = DbField.GetStr(dr, "MEMO")

            Try
                Me.ddlst_ConditionColumn1.SelectedValue = DbField.GetStr(dr, "ConditionColumnID1")
            Catch ex As Exception

            End Try
            Try
                Me.ddlst_ConditionColumn2.SelectedValue = DbField.GetStr(dr, "ConditionColumnID2")
            Catch ex As Exception

            End Try
            Try
                Me.ddlst_ValueColumn.SelectedValue = DbField.GetStr(dr, "VALUECOLUMNID")
            Catch ex As Exception

            End Try
            Try
                Me.rblst_ChartType.SelectedValue = DbField.GetStr(dr, "ChartType")
            Catch ex As Exception

            End Try
            Dim strKind As String = DbField.GetStr(dr, "ChartKind")
            For i As Integer = 0 To Me.rblst_ChartKind.Items.Count - 1
                If strKind.IndexOf(Me.rblst_ChartKind.Items(i).Value) > -1 Then
                    Me.rblst_ChartKind.Items(i).Selected = True
                End If
            Next
        End If
    End Sub

    '保存
    Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        
        Dim ht As New Hashtable
        ht.Add("resid", RStr("mnuresid"))
        ht.Add("CHARTNAME", Me.txt_ChartName.Text)
        ht.Add("MEMO", Me.txt_Content.Text)
        ht.Add("ConditionColumnID1", Me.ddlst_ConditionColumn1.SelectedValue)
        ht.Add("ConditionColumnNAME1", Me.ddlst_ConditionColumn1.SelectedItem.Text)
        ht.Add("ConditionColumnID2", Me.ddlst_ConditionColumn2.SelectedValue)
        ht.Add("ConditionColumnNAME2", Me.ddlst_ConditionColumn2.SelectedItem.Text)
        ht.Add("VALUECOLUMNID", Me.ddlst_ValueColumn.SelectedValue)
        ht.Add("VALUECOLUMNNAME", Me.ddlst_ValueColumn.SelectedItem.Text)
        ht.Add("ACTIVEFLAG", 1)
        ht.Add("DELETEDFLAG", 0)
        ht.Add("ChartKind", rblst_ChartKind.SelectedValue)
        ht.Add("ChartType", rblst_ChartType.SelectedValue)
        ht.Add("SqlString", GenerateSql())
        If ViewState("EditFlag").ToString() = "0" Then
            SDbStatement.UpdateRows(ht, ChartBasicCode.ChartDefineTable, "id=" + ViewState("Id").ToString())
            ht.Add("Modifier", Me.CmsPass.Employee.ID)
            ht.Add("Modifytime", DateTime.Now)
        Else
            ht.Add("id", ViewState("Id").ToString())
            ht.Add("Creator", Me.CmsPass.Employee.ID)
            ht.Add("CreateTime", DateTime.Now)
            SDbStatement.InsertRow(ht, ChartBasicCode.ChartDefineTable)
            ViewState("EditFlag") = "0"
        End If
        Response.Redirect("ChartList.aspx?mnuresid=" & RStr("mnuresid"), False)
    End Sub

    Private Function GenerateSql() As String
        Dim datRes As DataResource = CmsPass.GetDataRes(RLng("mnuresid"))
        Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(Me.CmsPass, RLng("mnuresid"), False, True, False, False)
        Dim strTableName As String = datRes.ResTable
        Dim strSql As String = "select " & GetColName(alistColumns, ddlst_ConditionColumn1.SelectedValue) & ","
        If ddlst_ConditionColumn2.SelectedValue <> "0" Then
            strSql += GetColName(alistColumns, ddlst_ConditionColumn2.SelectedValue) & ","
        End If
        If Me.rblst_ChartType.SelectedValue = "1" Then
            strSql += "count(" & GetColName(alistColumns, ddlst_ValueColumn.SelectedValue) & ") "
        Else
            strSql += "sum(isnull(" & GetColName(alistColumns, ddlst_ValueColumn.SelectedValue) & ",0)) "
        End If
        strSql += " from " & strTableName
        strSql += " group by " & GetColName(alistColumns, ddlst_ConditionColumn1.SelectedValue)

        If ddlst_ConditionColumn2.SelectedValue <> "0" Then
            strSql += "," & GetColName(alistColumns, ddlst_ConditionColumn2.SelectedValue)
        End If
        strSql += " order by " & GetColName(alistColumns, ddlst_ConditionColumn1.SelectedValue)
        If ddlst_ConditionColumn2.SelectedValue <> "0" Then
            strSql += "," & GetColName(alistColumns, ddlst_ConditionColumn2.SelectedValue)
        End If
        Return strSql
    End Function
    Private Function GetColName(ByVal al As ArrayList, ByVal strId As String) As String
        For i As Integer = 0 To al.Count - 1
            Dim datCol As DataTableColumn = CType(al(i), DataTableColumn)
            If datCol.ColID.ToString() = strId Then
                Return datCol.ColName
            End If
        Next
    End Function
    Private Sub btn_Back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Back.Click
        Response.Redirect("ChartList.aspx?mnuresid=" & RStr("mnuresid"), False)
    End Sub

    Private Sub rblst_ChartType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblst_ChartType.SelectedIndexChanged
        Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(Me.CmsPass, RLng("mnuresid"), False, True, False, False)
        Me.ddlst_ValueColumn.Items.Clear()
        If Me.rblst_ChartType.SelectedValue = "1" Then
            For i As Integer = 0 To alistColumns.Count - 1
                Dim datCol As DataTableColumn = CType(alistColumns(i), DataTableColumn)
                Me.ddlst_ValueColumn.Items.Add(New ListItem(datCol.ColDispName, datCol.ColID.ToString()))
            Next
        Else
            For i As Integer = 0 To alistColumns.Count - 1
                Dim datCol As DataTableColumn = CType(alistColumns(i), DataTableColumn)
                If datCol.ColType = FieldDataType.Float Or datCol.ColType = FieldDataType.Int32 Or datCol.ColType = FieldDataType.Money Then
                    Me.ddlst_ValueColumn.Items.Add(New ListItem(datCol.ColDispName, datCol.ColID.ToString()))
                End If
            Next
        End If
    End Sub
End Class

End Namespace
