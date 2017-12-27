Imports NetReusables
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Web

Partial Class ExtensionForms_common_AjaxRequest
    Inherits UserPageBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim typeValue As String = ""
        Dim returnJson As String = ""
        If Request("typeValue") <> "" Then
            typeValue = Request("typeValue")
        End If

        If typeValue = "GetWBXMBH" Then
            returnJson = GetWBXMBH()
        End If
        If typeValue = "GetBXDBH" Then
            returnJson = GetBXDBH()
        End If
        If typeValue = "GetCGMX" Then
            returnJson = GetCGMX()
        End If
        If typeValue = "SaveQKSQ" Then
            returnJson = SaveQKSQ()
        End If

        Response.Write(returnJson)
    End Sub

    Protected Function GetWBXMBH() As String
        Dim PageSize As Integer = 10
        If Request("rows") Is Nothing Then
        Else
            PageSize = Convert.ToInt32(Request("rows"))
        End If
        Dim PageNumber As Integer = 10
        If Request("page") Is Nothing Then
        Else
            PageNumber = Convert.ToInt32(Request("page"))
        End If
        Dim SortField As String = "填表日期"
        If Request("sort") Is Nothing Then
        Else
            SortField = Convert.ToInt32(Request("sort"))
        End If
        Dim SortBy As String = "DESC"
        If Request("order") Is Nothing Then
        Else
            SortBy = Convert.ToInt32(Request("order"))
        End If
        Dim SQL As String = "select ID,C3_382788121781 项目编号,C3_382788132843 外包项目编号,C3_382788155750 代理名称 ,Convert(varchar,C3_382788143312,112) 填表日期,C3_382788197562 样本单价,C3_382788213390 总费用,C3_383066866953 督导 from CT382788102093 WHERE C3_382788226156='未结算'"

        Dim OrderBy As String = " order by " + SortField + " " + SortBy
        Dim sqldata As String = " select top " + PageSize.ToString()
        sqldata += " * from (" + SQL + ") T where id not in (select top " + ((PageNumber - 1) * PageSize).ToString()
        sqldata += " id from (" + SQL + ") W  " + OrderBy + ")  " + OrderBy
        Dim bxkmDt As DataTable = SDbStatement.Query(sqldata).Tables(0)
        Dim CoustDt As DataTable = SDbStatement.Query("select count(1) from CT382788102093 WHERE C3_382788226156='未结算' ").Tables(0)
        Dim RowCount As Integer = CoustDt.Rows(0)(0).ToString()
        Dim timeConverter As Newtonsoft.Json.Converters.IsoDateTimeConverter = New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd"
        Dim Str As String = Newtonsoft.Json.JsonConvert.SerializeObject(bxkmDt)

        Return "{""total"":" + RowCount.ToString() + ",""rows"":" + Str + "}"
    End Function


    Protected Function GetBXDBH() As String
        Dim PageSize As Integer = 10
        If Request("rows") Is Nothing Then
        Else
            PageSize = Convert.ToInt32(Request("rows"))
        End If
        Dim PageNumber As Integer = 10
        If Request("page") Is Nothing Then
        Else
            PageNumber = Convert.ToInt32(Request("page"))
        End If
        Dim SortField As String = "日期"
        If Request("sort") Is Nothing Then
        Else
            SortField = Convert.ToInt32(Request("sort"))
        End If
        Dim SortBy As String = "DESC"
        If Request("order") Is Nothing Then
        Else
            SortBy = Convert.ToInt32(Request("order"))
        End If
        Dim SQL As String = "select ID,C3_284057533109 报销单编号,C2 报销人,C4 报销金额 ,C1 日期,C7 备注  from CT184270067595 WHERE FLOW_FILE='是'"

        Dim OrderBy As String = " order by " + SortField + " " + SortBy
        Dim sqldata As String = " select top " + PageSize.ToString()
        sqldata += " * from (" + SQL + ") T where id not in (select top " + ((PageNumber - 1) * PageSize).ToString()
        sqldata += " id from (" + SQL + ") W  " + OrderBy + ")  " + OrderBy
        Dim bxkmDt As DataTable = SDbStatement.Query(sqldata).Tables(0)
        Dim CoustDt As DataTable = SDbStatement.Query("select count(1) from CT184270067595 WHERE FLOW_FILE='是' ").Tables(0)
        Dim RowCount As Integer = CoustDt.Rows(0)(0).ToString()
        Dim timeConverter As Newtonsoft.Json.Converters.IsoDateTimeConverter = New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd"
        Dim Str As String = Newtonsoft.Json.JsonConvert.SerializeObject(bxkmDt)

        Return "{""total"":" + RowCount.ToString() + ",""rows"":" + Str + "}"
    End Function


    Protected Function GetCGMX() As String
        Dim qkbh As String = ""
        If Request("QKBH") IsNot Nothing Then
            qkbh = Request("QKBH")
        End If
        Dim sqldata As String = "select ID,C3_282995004359 请款编号,C3_282995029781 物品名称,C3_282995087546 数量,C3_282995098390 单价,C3_282995108593 用途,C3_282995628468 金额  from CT282994832750 WHERE C3_282995004359='" + qkbh + "'"
        Dim bxkmDt As DataTable = SDbStatement.Query(sqldata).Tables(0)
        Dim CoustDt As DataTable = SDbStatement.Query("select count(1) from CT282994832750 WHERE C3_282995004359='" + qkbh + "'").Tables(0)
        Dim RowCount As Integer = CoustDt.Rows(0)(0).ToString()
        Dim timeConverter As Newtonsoft.Json.Converters.IsoDateTimeConverter = New Newtonsoft.Json.Converters.IsoDateTimeConverter()
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd"
        Dim Str As String = Newtonsoft.Json.JsonConvert.SerializeObject(bxkmDt)

        Return "{""total"":" + RowCount.ToString() + ",""rows"":" + Str + "}"
    End Function

    Protected Function SaveQKSQ() As String
        Dim QKSQID As String = Request("QKSQID")
        Dim Code As String = Request("Code")
        Dim dataJson1 As String = Request("Json1")
        Dim dataJson2 As String = Request("Json2")
        Dim WorkflowId As String = Request("WorkflowId")
        Dim ActionKey As String = Request("ActionKey")
        Try
            Dim dt1 As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(dataJson1)





            Dim FLOWCODE As String
            Dim MaxFLOWCODEDt As DataTable = SDbStatement.Query("select max(Convert(float,FLOWCODE))+1 from CT282994819609  ").Tables(0)
            FLOWCODE = Convert.ToInt32(MaxFLOWCODEDt.Rows(0)(0)).ToString()

            Dim hst As New Hashtable
            hst.Add("ID", QKSQID)
            hst.Add("RESID", 282994819609)
            hst.Add("RELID", 0)
            hst.Add("CRTID", Code)
            hst.Add("CRTTIME", DateTime.Now)
            hst.Add("EDTID", Code)
            hst.Add("EDTTIME", DateTime.Now)
            hst.Add("FLOWCODE", FLOWCODE) '编号 流程上的
            hst.Add("C3_282994956906", "QK-" + FLOWCODE) '(流程编号)
            hst.Add("C3_282994853625", dt1(0)("C3_282994853625").ToString()) '申请日期   
            hst.Add("C3_282994864593", dt1(0)("C3_282994864593").ToString()) '申请人   
            hst.Add("C3_282994932921", dt1(0)("C3_282994932921").ToString()) '请款金额   
            hst.Add("C3_282994883125", dt1(0)("C3_282994883125").ToString()) '采购金额合计
            hst.Add("C3_282994921125", dt1(0)("C3_282994921125").ToString()) '请款事由 
            hst.Add("C3_384347781708", dt1(0)("C3_384347781708").ToString()) '报销科目名称 
            hst.Add("C3_386163735265", dt1(0)("C3_386163735265").ToString()) '外包项目编号 
            hst.Add("C3_284725263953", dt1(0)("C3_284725263953").ToString()) '是否已汇款   
            hst.Add("C3_284725355500", dt1(0)("C3_284725355500").ToString()) '是否已转为报销
            hst.Add("C3_284725426953", dt1(0)("C3_284725426953").ToString()) '关联报销单编号 
            Dim isHave As Integer = SDbStatement.InsertRow(hst, "CT282994819609")
            If isHave = 0 Then
                Return "0"
            End If

            Dim QKBH As String = "QK-" + FLOWCODE '请款编号   


            If dataJson2 <> "[]" Then
                Dim dt2 As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(dataJson2)
                For index = 0 To dt2.Count - 1
                    Dim hst1 As New Hashtable
                    hst1.Add("ID", TimeId.CurrentMilliseconds(30))
                    hst1.Add("RESID", 282994832750)
                    hst1.Add("RELID", 0)
                    hst1.Add("CRTID", Code)
                    hst1.Add("CRTTIME", DateTime.Now)
                    hst1.Add("EDTID", Code)
                    hst1.Add("EDTTIME", DateTime.Now)
                    hst1.Add("C3_282995004359", QKBH) '请款编号   
                    hst1.Add("C3_282995029781", dt2(index)("C3_282995029781").ToString()) '物品名称   
                    hst1.Add("C3_282995087546", dt2(index)("C3_282995087546").ToString()) '数量   
                    hst1.Add("C3_282995098390", dt2(index)("C3_282995098390").ToString()) '单价   
                    hst1.Add("C3_282995108593", dt2(index)("C3_282995108593").ToString()) '用途   
                    hst1.Add("C3_282995628468", dt2(index)("C3_282995628468").ToString()) '金额 
                    isHave = SDbStatement.InsertRow(hst1, "CT282994832750")
                    If isHave = 0 Then
                        Return "0"
                    End If
                Next
            End If

            Return "1"
        Catch ex As Exception
            Return "0"
        End Try
    End Function

End Class
