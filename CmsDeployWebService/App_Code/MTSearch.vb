Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports NetReusables

Public Class MTSearch
    Public Shared Function GetMTSearch(ByVal ResourceID As String, ByVal GainerObjectID As String) As DataSet
        Dim strSql As String = "select * from CMS_MTSEARCH where MTS_RESID=" + ResourceID + " AND MTS_TYPE=6 AND MTS_EMPID='" + GainerObjectID + "';"
        strSql &= "select M.MtsCol_ID,R.Name,M.MTSCOL_COLDISPNAME,M.MTSCOL_COLCOND,M.MTSCOL_COLVALUE,M.MTSCOL_LOGIC  from  CMS_MTSEARCH_COLDEF M join cms_resource R on M.Mtscol_ResID=R.ID "
        strSql &= " where MTSCOL_HOSTID in (select MTS_ID from CMS_MTSEARCH where MTS_RESID=" + ResourceID + " AND MTS_TYPE=6 AND MTS_EMPID='" + GainerObjectID + "') order by MTSCOL_ID;"
        Return SDbStatement.Query(strSql)
    End Function


    Public Shared Function GetMTSCol_ColCond() As List(Of FieldInfo)
        Dim oList As New List(Of FieldInfo)

        oList.Add(Common.FillFieldInfo("", "", ""))
        oList.Add(Common.FillFieldInfo("当前用户帐号", "[CUR_USERID]", ""))

        oList.Add(Common.FillFieldInfo("当前用户名称", "[CUR_USERNAME]", ""))
        oList.Add(Common.FillFieldInfo("当前用户部门", "[CUR_USERDEPNAME]", ""))
        oList.Add(Common.FillFieldInfo("上年年份", "[PREV_YEAR]", ""))
        oList.Add(Common.FillFieldInfo("当前年份", "[CUR_YEAR]", ""))
        oList.Add(Common.FillFieldInfo("下年年份", "[NEXT_YEAR]", ""))
        oList.Add(Common.FillFieldInfo("上月月份", "[PREV_MONTH]", ""))
        oList.Add(Common.FillFieldInfo("当前月份", "[CUR_MONTH]", ""))
        oList.Add(Common.FillFieldInfo("下月月份", "[NEXT_MONTH]", ""))
        oList.Add(Common.FillFieldInfo("当前日期", "[CUR_DATE]", ""))
        oList.Add(Common.FillFieldInfo("当前月日", "[CUR_MONTHDATE]", ""))
        oList.Add(Common.FillFieldInfo("当前日期(生日比较)", "[BIRTHDAY]", ""))
        oList.Add(Common.FillFieldInfo("明天日期", "[TOMORROW]", ""))
        oList.Add(Common.FillFieldInfo("当前日期几", "[CUR_DAYOFWEEK]", ""))
        oList.Add(Common.FillFieldInfo("当前小时", "[CUR_HOUR]", ""))
        oList.Add(Common.FillFieldInfo("上月第一天", "[PREV_MONTH_FSTDAY]", ""))
        oList.Add(Common.FillFieldInfo("本月第一天", "[CUR_MONTH_FSTDAY]", ""))

        oList.Add(Common.FillFieldInfo("下月第一天", "[NEXT_MONTH_FSTDAY]", ""))
        oList.Add(Common.FillFieldInfo("下下月第一天", "[NNEXT_MONTH_FSTDAY]", ""))

        oList.Add(Common.FillFieldInfo("上周星期一", "[PREVWK_MON]", ""))
        oList.Add(Common.FillFieldInfo("上周星期六", "[PREVWK_SAT]", ""))
        oList.Add(Common.FillFieldInfo("本周星期一", "[THISWK_MON]", ""))
        oList.Add(Common.FillFieldInfo("上周星期六", "[THISWK_SAT]", ""))
        oList.Add(Common.FillFieldInfo("下周星期一", "[NEXTWK_MON]", ""))
        oList.Add(Common.FillFieldInfo("下周星期六", "[NEXTWK_SAT]", ""))
        oList.Add(Common.FillFieldInfo("下下周星期一", "[NNEXTWK_MON]", ""))

        oList.Add(Common.FillFieldInfo("上季第一天", "[PREV_QTR_FSTDAY]", ""))
        oList.Add(Common.FillFieldInfo("本季第一天", "[CUR_QTR_FSTDAY]", ""))
        oList.Add(Common.FillFieldInfo("下季第一天", "[NEXT_QTR_FSTDAY]", ""))
        Return oList
    End Function
     
    Public Shared Function UpdateMTSearch(ByVal Mts_ID As Long, ByVal ResourceID As String, ByVal Mts_EmpID As String, ByVal ColName As String, ByVal ColDispName As String, ByVal ColCond As String, ByVal ColCond_EN As String, ByVal ColValue As String, ByVal ColValue_EN As String, ByVal Loglc As String, ByVal CreatorAccount As String) As Boolean
        Try
            Dim Res_Table As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim strSql As String = "select * from CMS_MTSEARCH where mts_id=" & Mts_ID & ";"
            strSql &= "select * from CMS_MTSEARCH_COLDEF where MTSCOL_HOSTID =" & Mts_ID & ";"
            Dim ds As DataSet = SDbStatement.Query(strSql)
            Dim dt1 As DataTable = ds.Tables(0)
            dt1.PrimaryKey = New DataColumn() {dt1.Columns("mts_id")}

            Dim dt2 As DataTable = ds.Tables(1)
            dt2.PrimaryKey = New DataColumn() {dt2.Columns("MtsCol_ID")}

            ' strSql = "select Max(MTS_SHOWORDER) ShowOrder from CMS_MTSEARCH;select Max(MTSCOL_SHOWORDER) ShowOrder from CMS_MTSEARCH_COLDEF;"

            If Mts_ID = 0 Then
                Mts_ID = TimeId.GetCurrentMilliseconds()
                Dim dr As DataRow = dt1.NewRow()
                dr("Mts_ID") = Mts_ID
                dr("MTS_RESID") = ResourceID
                dr("MTS_DICTRESID") = "0"
                dr("MTS_EDTID") = CreatorAccount
                dr("MTS_EDTTIME") = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
                dr("MTS_SHOWORDER") = "0"
                dr("MTS_TYPE") = "6"
                dr("Mts_EmpID") = Mts_EmpID
                dr("MTS_TABLELOGIC") = "AND"
                dr("MTS_COLURLID") = "0"
                dt1.Rows.Add(dr)
            End If

            Dim drChild As DataRow = dt2.NewRow
            Dim strWhere As String = Res_Table & "." & ColName & " " & ColCond_EN

            If ColCond_EN.ToLower.Trim = "like" Or ColCond_EN.ToLower.Trim = "not like" Then
                strWhere &= "'%" & ColValue_EN & "%'"
            Else
                strWhere &= "'" & ColValue_EN & "'"
            End If

            If ColCond_EN.ToLower.Trim = "not like" Or ColCond_EN.Trim = "!=" Then
                strWhere &= " OR " & Res_Table & "." & ColName & " IS NULL"
            End If

            drChild("MTSCOL_ID") = TimeId.GetCurrentMilliseconds()
            drChild("MTSCOL_RESID") = ResourceID
            drChild("MTSCOL_EDTID") = CreatorAccount
            drChild("MTSCOL_EDTTIME") = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
            drChild("MTSCOL_SHOWORDER") = dt2.Rows.Count + 1
            drChild("MTSCOL_HOSTID") = Mts_ID
            drChild("MTSCOL_TYPE") = "2"
            drChild("MTSCOL_COLNAME") = ColName
            drChild("MTSCOL_COLDISPNAME") = ColDispName
            drChild("MTSCOL_COLCOND") = ColCond
            drChild("MTSCOL_COLVALUE") = ColValue
            drChild("MTSCOL_WHERE") = "(" & strWhere & ")"
            drChild("MTSCOL_LOGIC") = Loglc

            dt2.Rows.Add(drChild)

            Return Transaction.CommitUpdate(strSql, ds)

        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function DeleteMTSearch(ByVal MTS_ID As String) As Boolean
        Dim strSql As String = "delete from CMS_MTSEARCH_COLDEF where MTSCOL_HOSTID in (select MTS_ID from CMS_MTSEARCH where MTS_ID=" + MTS_ID + ");"
        strSql += "delete from CMS_MTSEARCH where MTS_ID=" + MTS_ID + ";"
        Try
            If SDbStatement.Execute(strSql) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try 
    End Function 

End Class
