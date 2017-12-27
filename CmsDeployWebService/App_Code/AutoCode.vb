Imports Microsoft.VisualBasic
Imports NetReusables
Public Class AutoCode

    Public Shared Function GetAutoCode(ByVal ResourceDescription As String, ByVal ColumnDescription As String) As String
       
        Dim sql As String = "select cms_resource.id,CMS_TABLE_DEFINE.CD_COLNAME from cms_resource " _
& " join dbo.CMS_TABLE_DEFINE on cms_resource.id=CMS_TABLE_DEFINE.cd_resid " _
& " where res_comments='" + ResourceDescription + "' and cd_DISPNAME='" + ColumnDescription + "'"
        Dim ds As DataSet = SDbStatement.Query(sql)
        If ds.Tables.Count > 0 Then
            If SDbStatement.Query(sql).Tables(0).Rows.Count > 0 Then
                Return GetAutoCode(ds.Tables(0).Rows(0)("id"), ds.Tables(0).Rows(0)("CD_COLNAME"), True)
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function


    Public Shared Function GetAutoCode(ByVal ResourceID As Long, ByVal ColumnDescription As String, ByVal IsUpdate As Boolean) As String

        Dim sql As String = "select cms_resource.id,CMS_TABLE_DEFINE.CD_COLNAME from cms_resource " _
& " join dbo.CMS_TABLE_DEFINE on cms_resource.id=CMS_TABLE_DEFINE.cd_resid " _
& " where cms_resource.ID=" + ResourceID.ToString + " and cd_DISPNAME='" + ColumnDescription + "'"
        Dim ds As DataSet = SDbStatement.Query(sql)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return GetAutoCode(ds.Tables(0).Rows(0)("id").ToString, ds.Tables(0).Rows(0)("CD_COLNAME"), IsUpdate)
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function

    Public Shared Function GetAutoCode(ByVal ResourceID As String, ByVal ColumnName As String, ByVal IsUpdate As Boolean) As String
        Dim strCode As String = "" '�����ص��Զ�����

        Try
            Dim ds As DataSet = SDbStatement.Query("select * from CMS_COL_AUTOCODING where CDC_RESID=" + ResourceID + " and CDC_COLNAME='" + ColumnName + "' order by CDC_AIID ASC") ' CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), CmsTables.FieldAutoCoding, "CDC_RESID=" & pst.GetDataRes(lngResID).IndepParentResID & " AND CDC_COLNAME='" & strColName & "'", "CDC_AIID ASC")
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            For Each drv In dv
                Dim lngType As Long = DbField.GetLng(drv, "CDC_TYPE")
                Dim strValue As String = DbField.GetStr(drv, "CDC_VALUE")
                Select Case lngType
                    Case 1
                        strCode &= strValue

                    Case 2
                        If strValue = "YYYY" Then
                            strCode &= Today.ToString("yyyy")
                        ElseIf strValue = "YY" Then
                            strCode &= Today.ToString("yy")
                        ElseIf strValue = "MM" Then
                            strCode &= Today.ToString("MM")
                        ElseIf strValue = "DD" Then
                            strCode &= Today.ToString("dd")
                        End If

                    Case 3
                        Dim strSeriesNum As String = GetSerialNo(ResourceID, ColumnName, strValue, drv)
                        strCode &= strSeriesNum
                        If IsUpdate Then EditSerialNo(DbField.GetLng(drv, "CDC_AIID"))
                End Select
            Next

            Return strCode
        Catch ex As Exception
            Return ""
            'CmsError.ThrowEx("�����Զ�����ֵʧ��", ex, True)
        End Try
    End Function


    Private Shared Function EditSerialNo(ByVal ID As String) As Integer
        Dim strSql As String = "UPDATE CMS_COL_AUTOCODING SET CDC_VALUE=Cast(IsNull(CDC_Value,0) as bigint)+1 where CDC_AIID=" + ID
        Return SDbStatement.Execute(strSql)
    End Function


    Private Shared Function GetSerialNo(ByVal ResourceID As Long, ByVal ColumnName As String, ByVal strValue As String, ByRef drv As DataRowView) As String
        Try
            '��������������㵱ǰ��ʹ�õ���ˮ��
            Dim strSNum As String
            Dim lngSNum As Long
            Dim lngCurTag As Long = GetCurResetTag(DbField.GetLng(drv, "CDC_SNUM_RESET_TIME"))
            If lngCurTag <> 0 And lngCurTag <> DbField.GetLng(drv, "CDC_SNUM_RESET_TAG") Then '��Ҫ����
                lngSNum = GetSequcenum(1, DbField.GetStr(drv, "CDC_SNUM_SKIP"))

                SDbStatement.Execute("UPDATE CMS_COL_AUTOCODING SET CDC_SNUM_RESET_TAG=" & lngCurTag & ", CDC_VALUE=" & lngSNum & " WHERE CDC_RESID=" & ResourceID & " AND CDC_COLNAME='" & ColumnName & "' AND CDC_TYPE=3")
            Else '����Ҫ���ã���ȡ��ǰ��ʹ����ˮ��
                lngSNum = GetSequcenum(CLng(strValue), DbField.GetStr(drv, "CDC_SNUM_SKIP"))
            End If

            '����ǰ׺0
            If DbField.GetLng(drv, "CDC_SNUM_PREZERO") = 1 Then 'λ��������ǰ�����㲹��
                strSNum = CStr(lngSNum).PadLeft(DbField.GetInt(drv, "CDC_SNUM_LENGTH"), CChar("0"))
            Else
                strSNum = CStr(lngSNum)
            End If

            Return strSNum
        Catch ex As Exception
            ' CmsError.ThrowEx("��ȡ��ˮ��ֵ�쳣ʧ��", ex, True)
            Return ""
        End Try
    End Function


    '-------------------------------------------------------------
    '��ȡ��ǰ���ñ�־���ꡢ�¡��գ�
    '-------------------------------------------------------------
    Private Shared Function GetCurResetTag(ByVal lngResetTime As Long) As Long
        Dim lngCurTag As Long
        Select Case lngResetTime
            Case 0 '������
                lngCurTag = 0 '������
            Case 1 'ÿ��
                lngCurTag = CLng(Today.Year)
            Case 2 'ÿ��
                lngCurTag = CLng(Today.Month)
            Case 3 'ÿ��
                lngCurTag = CLng(Today.Day)
            Case Else
                lngCurTag = 0 '������
        End Select

        Return lngCurTag
    End Function


    '-------------------------------------------------------------
    '������ˮ�ţ�����ָ���ģ���������������
    '-------------------------------------------------------------
    Private Shared Function GetSequcenum(ByVal lngSeqnum As Long, ByVal strSkipNums As String) As Long
        Dim strSkip1 As String = "x"
        Dim strSkip2 As String = "x"
        Dim strSkip3 As String = "x"

        strSkipNums = strSkipNums.Trim()
        If strSkipNums.Length = 0 Then
            Return lngSeqnum '����ԭ��ˮ�ż���
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
            Return lngSeqnum '��ʽ����ȷ������ԭ��ˮ�ż���
        End If

        '������ˮ��
        Do While True
            Dim strTemp As String = CStr(lngSeqnum)
            If strTemp.IndexOf(strSkip1) > 0 OrElse strTemp.IndexOf(strSkip2) > 0 OrElse strTemp.IndexOf(strSkip3) > 0 Then
                lngSeqnum = lngSeqnum + 1
            Else
                Return lngSeqnum
            End If
        Loop
        Return lngSeqnum
    End Function
End Class
