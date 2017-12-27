Imports Microsoft.VisualBasic
Imports Unionsoft.Implement
Imports Unionsoft.Platform
Imports NetReusables


Public Enum MTSearchType As Integer
    Unknown = 0 '0或NULL：无意义；
    MultiTableView = 1 '1：列表统计；
    MultiTableViewForTemp = 2 '2：临时列表统计；
    GeneralRowWhere = 3 '通用行过滤设置
    PersonalRowWhere = 4 '个人行过滤设置
    BatchSend = 5 '邮件短信群发设置
    RowRights = 6 '行权限设置
    AdvDictFilter = 7 '高级字典用的行过滤设置
    ViewFilter = 8 ' 视图条件
    ColumnUrlFilter = 9 ' 字段链接条件
End Enum

Public Class ResourceCondition


    Public Const CMSWHERE_AND As String = "{[AND]}"
    Public Const CMSWHERE_OR As String = "{[OR]}"
    Public Shared Function GetRowRightsWhere(ByVal lngResID As Long, ByVal UserID As String) As String
        Dim hostId As String = ResourceCondition.GetHostID(lngResID, UserID)
        Dim RowRightsWhere As String = ResourceCondition.GetResourceCondition(lngResID, MTSearchType.RowRights, UserID, hostId)
        Return RowRightsWhere
    End Function
    Public Shared Function GetHostID(ByVal lngResID As Long, ByVal UserID As String) As String

        If GetRowRightsCount(lngResID, UserID) = 0 Then
            Dim userinfo As UserInfo = Users.GetUserInfoByID(UserID)
            If GetRowRightsCount(lngResID, userinfo.DepartmentID) = 0 Then
                Dim hostId As String = userinfo.DepartmentID
                For index As Integer = 0 To 10
                    Dim id As String = GetDepartmentID(hostId)
                    If id = "0" Then
                        Return ""
                    Else
                        hostId = id
                        If GetRowRightsCount(lngResID, hostId) > 0 Then
                            Return hostId
                        End If
                    End If
                Next
            Else
                Return userinfo.DepartmentID
            End If
        Else
            Return UserID
        End If
        Return ""
    End Function

    '得到上一级ID
    Private Shared Function GetDepartmentID(ByVal id As String) As String
        Dim sql As String = "select pid from CMS_DEPARTMENT WHERE ID='" + id + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count = 0 Then
            Return "0"
        Else
            Return dt.Rows(0)(0).ToString
        End If
    End Function
    '通过HostID得到行记录权限条数
    Private Shared Function GetRowRightsCount(ByVal lngResID As String, ByVal hostId As String) As Integer
        Dim strWhere As String = "MTS_RESID=" & lngResID & " AND MTS_TYPE=6 AND MTS_EMPID='" + hostId + "'"
        Dim sql As String = "select count(*) from CMS_MTSEARCH where " + strWhere
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Return Convert.ToInt32(dt.Rows(0)(0))
    End Function
    Public Shared Function GetResourceCondition(ByVal lngResID As Long, ByVal SearchType As MTSearchType, ByVal UserID As String, ByVal hostId As String) As String
        Try
            Dim strWhere As String = "MTS_RESID=" & lngResID & " AND MTS_TYPE=" & SearchType

            If SearchType <> MTSearchType.ViewFilter Then strWhere += " AND MTS_EMPID='" + hostId + "'"
            Dim strDepWhere As String = ""
            strWhere = AppendAnd(strWhere, strDepWhere)

            Dim strRtnWhere As String = ""
            Dim dv As DataView = SDbStatement.Query("select * from CMS_MTSEARCH where " + strWhere).Tables(0).DefaultView 'chenyu add 2010/6/10
            Dim drv As DataRowView
            For Each drv In dv '启用的个人行过滤可能有多个：个人的、所在部门的、所在虚拟组的、企业的
                Dim strOneWhere As String = AssembleWhereForOneTable(DbField.GetLng(drv, "MTS_ID"), UserID)
                strRtnWhere = CmsWhere.AppendAnd(strRtnWhere, strOneWhere)
            Next
            Return strRtnWhere
        Catch ex As Exception
            SLog.Err("获取个人(" & UserID & ")的资源(" & lngResID & ")的已启用的个人行过滤定义异常失败！", ex)
            Return ""
        End Try
    End Function
  

    '----------------------------------------------------------
    '根据MTS主记录ID获取查询条件，所有条件必须是一个资源表单的，不能涉及到表间条件
    '----------------------------------------------------------
    Public Shared Function GetResourceCondition(ByVal lngResID As Long, ByVal SearchType As MTSearchType, ByVal UserID As String) As String
        Try

            Dim strWhere As String = "MTS_RESID=" & lngResID & " AND MTS_TYPE=" & SearchType
            'If SearchType <> MTSearchType.ViewFilter Then strWhere += " AND MTS_START=1 AND MTS_EMPID='" + UserID + "'"
            If SearchType <> MTSearchType.ViewFilter Then strWhere += " AND MTS_EMPID='" + UserID + "'"

            Dim strDepWhere As String = "" '"MTS_EMPID IN " & OrgFactory.EmpDriver.GetGroupList(pst, strEmpID)
            strWhere = AppendAnd(strWhere, strDepWhere)

            Dim strRtnWhere As String = ""
            'Dim dv As DataView = CmsDbBase.GetTableView(pst, CmsTables.MTableHost, strWhere)
            Dim dv As DataView = SDbStatement.Query("select * from CMS_MTSEARCH where " + strWhere).Tables(0).DefaultView 'chenyu add 2010/6/10

            If dv.Count = 0 Then

                For index As Integer = 0 To 8

                Next

            End If

            Dim drv As DataRowView
            For Each drv In dv '启用的个人行过滤可能有多个：个人的、所在部门的、所在虚拟组的、企业的
                Dim strOneWhere As String = AssembleWhereForOneTable(DbField.GetLng(drv, "MTS_ID"), UserID)
                strRtnWhere = CmsWhere.AppendAnd(strRtnWhere, strOneWhere)
            Next
            Return strRtnWhere
        Catch ex As Exception
            SLog.Err("获取个人(" & UserID & ")的资源(" & lngResID & ")的已启用的个人行过滤定义异常失败！", ex)
            Return ""
        End Try
    End Function



    '----------------------------------------------------------
    '根据MTS主记录ID获取查询条件，所有条件必须是一个资源表单的，不能涉及到表间条件
    '----------------------------------------------------------
    Private Shared Function AssembleWhereForOneTable(ByVal lngMTSHostID As Long, ByVal UserID As String) As String
        Try
            Dim strWhere As String = ""
            'Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), CmsTables.MTableColDef, "MTSCOL_HOSTID=" & lngMTSHostID)
            'Dim dv As DataView = CmsDbBase.GetTableView(pst, CmsTables.MTableColDef, "MTSCOL_HOSTID=" & lngMTSHostID)
            Dim dv As New DataView
            dv.Table = SDbStatement.Query("select * from  CMS_MTSEARCH_COLDEF where MTSCOL_HOSTID=" & lngMTSHostID & " order by MTSCOL_ID").Tables(0)
            Dim drv As DataRowView
            For Each drv In dv
                If DbField.GetStr(drv, "MTSCOL_LOGIC").ToUpper.Trim = "OR" Then
                    strWhere = CmsWhere.AppendOr(strWhere, FilterWhere(DbField.GetStr(drv, "MTSCOL_WHERE"), UserID))
                Else
                    strWhere = CmsWhere.AppendAnd(strWhere, FilterWhere(DbField.GetStr(drv, "MTSCOL_WHERE"), UserID))
                End If
            Next
            Return strWhere
        Catch ex As Exception
            SLog.Err("判断多表统计和行过滤设置权限信息时异常失败！", ex)
        End Try
    End Function



    Private Shared Function FilterWhere(ByVal strWhere As String, ByVal UserID As String) As String
        If strWhere.Trim() = "" Then Return ""

        Dim oUserInfo As UserInfo = Users.GetUserInfoByID(UserID)

        strWhere = strWhere.Replace(CMSWHERE_AND, "AND")
        strWhere = strWhere.Replace(CMSWHERE_OR, "OR")
        strWhere = strWhere.Replace("[CUR_USERID]", oUserInfo.ID)
        strWhere = strWhere.Replace("[CUR_USERNAME]", oUserInfo.Name)
        If strWhere.IndexOf("[CUR_USERDEPNAME]") >= 0 Then
            Dim strDepName As String = oUserInfo.DepartmentName
            strWhere = strWhere.Replace("[CUR_USERDEPNAME]", strDepName)
        End If
        If strWhere.IndexOf("[CUR_MONTH_FSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[CUR_MONTH_FSTDAY]", Today.Year & "-" & Today.Month.ToString("00") & "-01")
        End If
        If strWhere.IndexOf("[CUR_MONTH_LSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[CUR_MONTH_LSTDAY]", Today.Year & "-" & Today.Month.ToString("00") & "-" & Date.DaysInMonth(Today.Year, Today.Month) & " 23:59:59")
        End If
        If strWhere.IndexOf("[PREV_MONTH_FSTDAY]") >= 0 Then
            Dim strValue As String = ""
            If Today.Month = 1 Then
                strValue = (Today.Year - 1) & "-12-01"
            Else
                strValue = Today.Year & "-" & (Today.Month - 1).ToString("00") & "-01"
            End If
            strWhere = strWhere.Replace("[PREV_MONTH_FSTDAY]", strValue)
        End If
        If strWhere.IndexOf("[NEXT_MONTH_FSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[NEXT_MONTH_FSTDAY]", CmsFormulaTime.DealFunc_NextMonthFirstDay())
        End If
        If strWhere.IndexOf("[NNEXT_MONTH_FSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[NNEXT_MONTH_FSTDAY]", CmsFormulaTime.DealFunc_NNextMonthFirstDay())
        End If

        If strWhere.IndexOf("[PREVWK_MON]") >= 0 Then
            strWhere = strWhere.Replace("[PREVWK_MON]", CmsFormulaTime.DealFunc_PrevWeekMon())
        End If
        If strWhere.IndexOf("[PREVWK_SAT]") >= 0 Then
            strWhere = strWhere.Replace("[PREVWK_SAT]", CmsFormulaTime.DealFunc_PrevWeekSat())
        End If
        If strWhere.IndexOf("[THISWK_MON]") >= 0 Then
            strWhere = strWhere.Replace("[THISWK_MON]", CmsFormulaTime.DealFunc_ThisWeekMon())
        End If
        If strWhere.IndexOf("[THISWK_SAT]") >= 0 Then
            strWhere = strWhere.Replace("[THISWK_SAT]", CmsFormulaTime.DealFunc_ThisWeekSat())
        End If
        If strWhere.IndexOf("[NEXTWK_MON]") >= 0 Then
            strWhere = strWhere.Replace("[NEXTWK_MON]", CmsFormulaTime.DealFunc_NextWeekMon())
        End If
        If strWhere.IndexOf("[NEXTWK_SAT]") >= 0 Then
            strWhere = strWhere.Replace("[NEXTWK_SAT]", CmsFormulaTime.DealFunc_NextWeekSat())
        End If
        If strWhere.IndexOf("[NNEXTWK_MON]") >= 0 Then
            strWhere = strWhere.Replace("[NNEXTWK_MON]", CmsFormulaTime.DealFunc_NNextWeekMon())
        End If

        If strWhere.IndexOf("[PREV_QTR_FSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[PREV_QTR_FSTDAY]", CmsFormulaTime.DealFunc_PrevQrtFirstDay())
        End If
        If strWhere.IndexOf("[CUR_QTR_FSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[CUR_QTR_FSTDAY]", CmsFormulaTime.DealFunc_CurQrtFirstDay())
        End If
        If strWhere.IndexOf("[NEXT_QTR_FSTDAY]") >= 0 Then
            strWhere = strWhere.Replace("[NEXT_QTR_FSTDAY]", CmsFormulaTime.DealFunc_NextQrtFirstDay())
        End If

        If strWhere.IndexOf("[PREV_YEAR]") >= 0 Then
            strWhere = strWhere.Replace("[PREV_YEAR]", CStr(Today.Year - 1))
        End If
        If strWhere.IndexOf("[CUR_YEAR]") >= 0 Then
            strWhere = strWhere.Replace("[CUR_YEAR]", CStr(Today.Year))
        End If
        If strWhere.IndexOf("[NEXT_YEAR]") >= 0 Then
            strWhere = strWhere.Replace("[NEXT_YEAR]", CStr(Today.Year + 1))
        End If
        If strWhere.IndexOf("[PREV_MONTH]") >= 0 Then
            Dim strMon As String = ""
            If Today.Month = 1 Then
                strMon = "12"
            Else
                strMon = CStr(Today.Month - 1)
            End If
            strWhere = strWhere.Replace("[PREV_MONTH]", strMon)
        End If
        If strWhere.IndexOf("[CUR_MONTH]") >= 0 Then
            strWhere = strWhere.Replace("[CUR_MONTH]", CStr(Today.Month))
        End If
        If strWhere.IndexOf("[NEXT_MONTH]") >= 0 Then
            Dim strMon As String = ""
            If Today.Month = 12 Then
                strMon = "1"
            Else
                strMon = CStr(Today.Month + 1)
            End If
            strWhere = strWhere.Replace("[NEXT_MONTH]", strMon)
        End If
        If strWhere.IndexOf("[TOMORROW]") >= 0 Then
            strWhere = strWhere.Replace("[TOMORROW]", CStr(Today.AddDays(1).ToString()))
        End If
        If strWhere.IndexOf("[CUR_DAYOFWEEK]") >= 0 Then
            Dim intDw As Integer = CInt(Today.DayOfWeek)
            If intDw = 0 Then intDw = 7
            strWhere = strWhere.Replace("[CUR_DAYOFWEEK]", CStr(intDw))
        End If
        If strWhere.IndexOf("[CUR_HOUR]") >= 0 Then
            strWhere = strWhere.Replace("[CUR_HOUR]", CStr(Date.Now.Hour))
        End If

        strWhere = strWhere.Replace("[CUR_TIME]", DateString & " " & TimeString)
        Dim pos As Integer = strWhere.IndexOf("[CUR_DATE]")
        If pos >= 0 Then
            '含日期计算，格式只支持：[CUR_DATE]+5  [CUR_DATE] - 3
            Dim pos2 As Integer = strWhere.IndexOf("+", pos + "[CUR_DATE]".Length)
            If pos2 >= 0 Then
                Dim posEnd As Integer = strWhere.IndexOf("'", pos + "[CUR_DATE]".Length)
                Dim strDays As String = strWhere.Substring(pos2 + 1, posEnd - pos2 - 1).Trim()
                If IsNumeric(strDays) = True Then
                    Dim intDays As Integer = CInt(strDays)
                    Dim strRtnDate As String = Date.Now.AddDays(intDays).ToString("yyyy-MM-dd")
                    strWhere = strWhere.Substring(0, pos).Trim() & strRtnDate & strWhere.Substring(posEnd).Trim()
                    SLog.Info("日期条件：" & strWhere)
                Else
                    SLog.Warn("行过滤条件中包含错误的日期格式：" & strWhere & " 忽略此条件！")
                    Return ""
                End If
            Else
                pos2 = strWhere.IndexOf("-", pos + "[CUR_DATE]".Length)
                If pos2 >= 0 Then
                    Dim posEnd As Integer = strWhere.IndexOf("'", pos + "[CUR_DATE]".Length)
                    Dim strDays As String = strWhere.Substring(pos2 + 1, posEnd - pos2 - 1).Trim()
                    If IsNumeric(strDays) = True Then
                        Dim intDays As Integer = CInt(strDays) * (-1)
                        Dim strRtnDate As String = Date.Now.AddDays(intDays).ToString("yyyy-MM-dd")
                        strWhere = strWhere.Substring(0, pos).Trim() & strRtnDate & strWhere.Substring(posEnd).Trim()
                        SLog.Info("日期条件：" & strWhere)
                    Else
                        SLog.Warn("行过滤条件中包含错误的日期格式：" & strWhere & " 忽略此条件！")
                        Return ""
                    End If
                Else '只有[CUR_DATE]，没有表达式
                    strWhere = strWhere.Replace("[CUR_DATE]", DateString)
                End If
            End If
        End If

        Return strWhere
    End Function



    Private Shared Function AppendAnd(ByVal strWhere1 As String, ByVal strWhere2 As String) As String
        If strWhere1 Is Nothing AndAlso strWhere2 Is Nothing Then Return ""
        If strWhere1 Is Nothing Then Return strWhere2.Trim()
        If strWhere2 Is Nothing Then Return strWhere1.Trim()
        strWhere1 = strWhere1.Trim()
        strWhere2 = strWhere2.Trim()

        If strWhere1 = "" Then
            If strWhere2 = "" Then
                Return ""
            Else
                Return "(" & strWhere2 & ")"
            End If
        Else
            If strWhere2 = "" Then
                Return "(" & strWhere1 & ")"
            Else
                Return "((" & strWhere1 & ") AND (" & strWhere2 & "))"
            End If
        End If
    End Function

    Private Shared Function AppendOr(ByVal strWhere1 As String, ByVal strWhere2 As String) As String
        If strWhere1 Is Nothing And strWhere2 Is Nothing Then Return ""
        If strWhere1 Is Nothing Then Return strWhere2.Trim()
        If strWhere2 Is Nothing Then Return strWhere1.Trim()
        strWhere1 = strWhere1.Trim()
        strWhere2 = strWhere2.Trim()

        If strWhere1 = "" Then
            If strWhere2 = "" Then
                Return ""
            Else
                Return "(" & strWhere2 & ")"
            End If
        Else
            If strWhere2 = "" Then
                Return "(" & strWhere1 & ")"
            Else
                Return "((" & strWhere1 & ") OR (" & strWhere2 & "))"
            End If
        End If
    End Function

End Class


