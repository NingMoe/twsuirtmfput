Imports NetReusables


Public Class FavoriteManager

    Public Shared Sub AddFavorite(ByVal UserTaksID As Long, ByVal CreatorCode As String)
        Dim lngTaskID As Long
        Dim dt As DataTable
        Dim strSql As String
        'strSql = "SELECT TASKID FROM WF_USERTASK WHERE [ID]=" & UserTaksID
        'dt = SDbStatement.Query(strSql).Tables(0)
        'If dt.Rows.Count >= 0 Then
        'lngTaskID = DbField.GetLng(dt.Rows(0), "TASKID")
        lngTaskID = UserTaksID
        strSql = "SELECT * FROM WF_FAVORITE WHERE [TaskID]=" & lngTaskID & " AND CreatorCode='" & CreatorCode & "'"
        If SDbStatement.Query(strSql).Tables(0).Rows.Count <= 0 Then
            Dim hst As New Hashtable
            hst.Add("ID", TimeId.CurrentMilliseconds(30))
            hst.Add("TaskID", lngTaskID)
            hst.Add("CreatorCode", CreatorCode)
            SDbStatement.InsertRow(hst, "WF_FAVORITE")
        End If
        'End If
    End Sub

    Public Shared Sub DeleteFavorite(ByVal UserTaksID As Long, ByVal CreatorCode As String)
        Dim strSql As String = "DELETE WF_FAVORITE WHERE [TaskID]=" & UserTaksID & " AND CreatorCode='" & CreatorCode & "'"
        SDbStatement.Execute(strSql)
    End Sub

    Public Shared Function GetFavoriteFiles(ByVal UserCode As String) As DataTable
        Dim strSql As String
        strSql = "SELECT * FROM VIEW_WF_FAVORITE WHERE EMPCODE='" & UserCode & "'"
        Return SDbStatement.Query(strSql).Tables(0)
    End Function

End Class
