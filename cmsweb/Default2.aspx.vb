Imports NetReusables
Imports Unionsoft.Platform

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Code As String = GenerateAutoCode(375447125450, "C3_376571794074", True)
        Response.Write(Code)
    End Sub


    Public Shared Function GenerateAutoCode(ByVal lngResID As Long, ByVal strColName As String, Optional ByVal IsUpdate As Boolean = False) As String
        Dim strCode As String = "" '待返回的自动编码

        Try
            Dim ds As DataSet = SDbStatement.Query("select * from CMS_COL_AUTOCODING where CDC_RESID=" + lngResID.ToString + " and CDC_COLNAME='" + strColName.Trim + "' order by CDC_AIID ASC") ' CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), CmsTables.FieldAutoCoding, "CDC_RESID=" & pst.GetDataRes(lngResID).IndepParentResID & " AND CDC_COLNAME='" & strColName & "'", "CDC_AIID ASC")
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            For Each drv In dv
                Dim lngType As Long = DbField.GetLng(drv, "CDC_TYPE")
                Dim strValue As String = DbField.GetStr(drv, "CDC_VALUE")
                Select Case lngType
                    Case ACodingType.IsConstant
                        strCode &= strValue

                    Case ACodingType.IsDate
                        If strValue = "YYYY" Then
                            strCode &= Today.ToString("yyyy")
                        ElseIf strValue = "YY" Then
                            strCode &= Today.ToString("yy")
                        ElseIf strValue = "MM" Then
                            strCode &= Today.ToString("MM")
                        ElseIf strValue = "DD" Then
                            strCode &= Today.ToString("dd")
                        End If

                    Case ACodingType.IsSeriesNum
                        Dim strSeriesNum As String = GetSeriesNum(lngResID, strColName, strValue, drv)
                        strCode &= strSeriesNum
                        If IsUpdate Then EditSeriesNum(Convert.ToInt64(strSeriesNum) + 1, DbField.GetLng(drv, "CDC_AIID"))
                End Select
            Next

            Return strCode
        Catch ex As Exception
            CmsError.ThrowEx("生成自动编码值失败", ex, True)
        End Try
    End Function


    Private Shared Function EditSeriesNum(ByVal lngSNumValue As Long, ByVal lngAIID As Long) As Integer
        Dim strSql As String = "UPDATE CMS_COL_AUTOCODING SET CDC_VALUE='" + lngSNumValue.ToString + "' where CDC_AIID=" + lngAIID.ToString
        Return SDbStatement.Execute(strSql)
    End Function


    Private Shared Function GetSeriesNum(ByVal lngResID As Long, ByVal strColName As String, ByVal strValue As String, ByRef drv As DataRowView) As String
        Try
            '根据重置情况计算当前可使用的流水号
            Dim strSNum As String
            Dim lngSNum As Long
            Dim lngCurTag As Long = GetCurResetTag(DbField.GetLng(drv, "CDC_SNUM_RESET_TIME"))
            If lngCurTag <> 0 And lngCurTag <> DbField.GetLng(drv, "CDC_SNUM_RESET_TAG") Then '需要重置
                lngSNum = GenerateSeqnum(1, DbField.GetStr(drv, "CDC_SNUM_SKIP"))

                SDbStatement.Execute("UPDATE CMS_COL_AUTOCODING SET CDC_SNUM_RESET_TAG=" & lngCurTag & ", CDC_VALUE=" & lngSNum & " WHERE CDC_RESID=" & lngResID & " AND CDC_COLNAME='" & strColName & "' AND CDC_TYPE=" & ACodingType.IsSeriesNum)
            Else '不需要重置，获取当前可使用流水号
                lngSNum = GenerateSeqnum(CLng(strValue), DbField.GetStr(drv, "CDC_SNUM_SKIP"))
            End If

            '处理前缀0
            If DbField.GetLng(drv, "CDC_SNUM_PREZERO") = 1 Then '位数不够的前面用零补齐
                strSNum = CStr(lngSNum).PadLeft(DbField.GetInt(drv, "CDC_SNUM_LENGTH"), CChar("0"))
            Else
                strSNum = CStr(lngSNum)
            End If

            Return strSNum
        Catch ex As Exception
            CmsError.ThrowEx("获取流水号值异常失败", ex, True)
        End Try
    End Function


    '-------------------------------------------------------------
    '获取当前重置标志（年、月、日）
    '-------------------------------------------------------------
    Private Shared Function GetCurResetTag(ByVal lngResetTime As Long) As Long
        Dim lngCurTag As Long
        Select Case lngResetTime
            Case 0 '不重置
                lngCurTag = 0 '不重置
            Case 1 '每年
                lngCurTag = CLng(Today.Year)
            Case 2 '每月
                lngCurTag = CLng(Today.Month)
            Case 3 '每日
                lngCurTag = CLng(Today.Day)
            Case Else
                lngCurTag = 0 '不重置
        End Select

        Return lngCurTag
    End Function


    '-------------------------------------------------------------
    '生成流水号，跳过指定的（不吉利）的数字
    '-------------------------------------------------------------
    Private Shared Function GenerateSeqnum(ByVal lngSeqnum As Long, ByVal strSkipNums As String) As Long
        Dim strSkip1 As String = "x"
        Dim strSkip2 As String = "x"
        Dim strSkip3 As String = "x"

        strSkipNums = strSkipNums.Trim()
        If strSkipNums.Length = 0 Then
            Return lngSeqnum '返回原流水号即可
        ElseIf strSkipNums.Length = 1 Then
            strSkip1 = strSkipNums
        ElseIf strSkipNums.Length >= 3 And strSkipNums.Length <= 4 Then
            strSkip1 = strSkipNums.Substring(0, 1)
            strSkip2 = strSkipNums.Substring(2, 1)
        ElseIf strSkipNums.Length >= 5 And strSkipNums.Length <= 6 Then
            strSkip1 = strSkipNums.Substring(0, 1)
            strSkip2 = strSkipNums.Substring(2, 1)
            strSkip3 = strSkipNums.Substring(4, 1)
        Else
            Return lngSeqnum '格式不正确，返回原流水号即可
        End If

        '生成流水号
        Do While True
            Dim strTemp As String = CStr(lngSeqnum)
            If strTemp.IndexOf(strSkip1) > 0 OrElse strTemp.IndexOf(strSkip2) > 0 OrElse strTemp.IndexOf(strSkip3) > 0 Then
                lngSeqnum = lngSeqnum + 1
            Else
                Return lngSeqnum
            End If
        Loop
    End Function
End Class
