Imports Microsoft.VisualBasic
Imports NetReusables
Imports System.Collections.Generic
Public Class Resource
    Public Shared Sub SetRowToResourceInfo(ByVal Row As DataRow, ByVal info As ResourceInfo, ByVal dtResource As DataTable)
        info.Description = Row("RES_COMMENTS") & ""
        info.ID = Row("id").ToString
        info.Name = Row("Name") & ""
        info.ParentID = Row("pid").ToString
        info.TableName = Row("res_table").ToString
        info.TableType = Row("res_tabletype").ToString
        info.Type = Row("res_type").ToString
        info.ShowChild = DbField.GetInt(Row, "res_show_child")

        info.IsUseParentShow = DbField.GetInt(Row, "res_use_parentshow")
        info.RES_ROWCOLOR3_WHERE = Row("RES_ROWCOLOR3_WHERE") & ""

        If Row.Table.Columns.Contains("RES_IsView") Then
            info.IsView = IIf(IsDBNull(Row("RES_IsView")), False, Row("RES_IsView"))
        Else
            info.IsView = False
        End If
        If Row.Table.Columns.Contains("Res_ServerUrl") Then
            info.ServerUrl = Row("Res_ServerUrl") & ""
        End If

        If info.ServerUrl = "" Then
            info.ServerUrl = Common.GetServerHeader
        End If

        If Row.Table.Columns.Contains("res_emptyresourceurl") Then
            info.ResourceLinkUrl = Row("res_emptyresourceurl") & ""
        End If
        If Row.Table.Columns.Contains("res_emptyresourcetarget") Then
            info.ResourceLinkTarget = Row("res_emptyresourcetarget") & ""
        End If

        If Row("RES_ORDERBY_COL").ToString().Trim <> "" Then
            Dim ParentResourceID As Long = Convert.ToInt64(Row("id"))
            If info.Type Then
                dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
                ParentResourceID = GetTopParentID(ParentResourceID, dtResource)
            End If
            Dim strSql As String = "select CD_COLNAME,CD_DISPNAME,OrderType='" + Row("RES_ORDERBY_TYPE").ToString().Trim + "' from dbo.CMS_TABLE_DEFINE where CD_RESID=" + ParentResourceID.ToString + " and CD_COLNAME='" + Row("RES_ORDERBY_COL").ToString().Trim + "';"

            If Row("RES_ORDERBY2_COL").ToString().Trim <> "" Then
                strSql += "select CD_COLNAME,CD_DISPNAME,OrderType='" + Row("RES_ORDERBY2_TYPE").ToString().Trim + "' from dbo.CMS_TABLE_DEFINE where CD_RESID=" + ParentResourceID.ToString + " and CD_COLNAME='" + Row("RES_ORDERBY2_COL").ToString().Trim + "';"
            End If
            If Row("RES_ORDERBY3_COL").ToString().Trim <> "" Then
                strSql += "select CD_COLNAME,CD_DISPNAME,OrderType='" + Row("RES_ORDERBY3_TYPE").ToString().Trim + "' from dbo.CMS_TABLE_DEFINE where CD_RESID=" + ParentResourceID.ToString + " and CD_COLNAME='" + Row("RES_ORDERBY3_COL").ToString().Trim + "';"
            End If
            If Row("RES_ORDERBY4_COL").ToString().Trim <> "" Then
                strSql += "select CD_COLNAME,CD_DISPNAME,OrderType='" + Row("RES_ORDERBY4_TYPE").ToString().Trim + "' from dbo.CMS_TABLE_DEFINE where CD_RESID=" + ParentResourceID.ToString + " and CD_COLNAME='" + Row("RES_ORDERBY4_COL").ToString().Trim + "';"
            End If
            If Row("RES_ORDERBY5_COL").ToString().Trim <> "" Then
                strSql += "select CD_COLNAME,CD_DISPNAME,OrderType='" + Row("RES_ORDERBY5_TYPE").ToString().Trim + "' from dbo.CMS_TABLE_DEFINE where CD_RESID=" + ParentResourceID.ToString + " and CD_COLNAME='" + Row("RES_ORDERBY5_COL").ToString().Trim + "';"
            End If
            Dim ds As DataSet = SDbStatement.Query(strSql)
            For i As Integer = 0 To ds.Tables.Count - 1
                For j As Integer = 0 To ds.Tables(i).Rows.Count - 1
                    info.ResOrder += "," + ds.Tables(i).Rows(j)("CD_DISPNAME").ToString() + " " + ds.Tables(i).Rows(j)("OrderType").ToString()
                Next
            Next
            If info.ResOrder.Trim() <> "" Then
                info.ResOrder = info.ResOrder.Trim().Substring(1)
            End If
        End If
    End Sub

    Public Shared Function GetResourceInfoByResourceID(ByVal ResouceID As String) As ResourceInfo
        Dim info As ResourceInfo = Nothing
        Dim sql As String = "select * from cms_resource" ' where ID='" + ResouceID + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim dr() As DataRow = dt.Select("ID='" + ResouceID + "'")
        If dr.Length > 0 Then
            info = New ResourceInfo
            SetRowToResourceInfo(dr(0), info, dt)
            Return info
        End If
        Return info
    End Function

    Public Shared Function GetResourceInfoByDescription(ByVal Description As String) As ResourceInfo

        Dim info As ResourceInfo = Nothing
        Dim sql As String = "select * from cms_resource" ' where RES_COMMENTS='" + Description + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim dr() As DataRow = dt.Select("RES_COMMENTS='" + Description + "'")
        If dr.Length > 0 Then
            info = New ResourceInfo
            SetRowToResourceInfo(dr(0), info, dt)

            Return info

        End If
        Return info
    End Function

    Public Shared Function GetResuorsePath(ByVal lngResID As Long) As String
        Dim strResuorsePath As String = ""
        Dim dtRes As DataTable = SDbStatement.Query("select * from CMS_RESOURCE").Tables(0)
        ' Dim dtDept As DataTable = SDbStatement.Query("select * from CMS_DEPARTMENT ").Tables(0)
        GetResuorsePath(lngResID, strResuorsePath, dtRes)
        If strResuorsePath.EndsWith("\") Then
            strResuorsePath = strResuorsePath.Substring(0, strResuorsePath.Length - 1)

        End If
        Return strResuorsePath
    End Function

    Public Shared Function GetResuorsePath(ByVal lngResID As Long, ByRef strResuorsePath As String, ByVal dtRes As DataTable) As String

        Dim dr() As DataRow = dtRes.Select("ID=" + lngResID.ToString)
        If dr.Length > 0 Then
            strResuorsePath = DbField.GetStr(dr(0), "Name") + "\" + strResuorsePath
            If DbField.GetLng(dr(0), "PID") <> 0 Then
                GetResuorsePath(DbField.GetLng(dr(0), "PID"), strResuorsePath, dtRes)
            End If
        End If
        Return strResuorsePath
    End Function

    Public Shared Function GetAllChildList(ByVal ResourceID As String) As String
        Dim strChildResID As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select ID,PID from cms_resource where Res_PID1 in (select Res_PID1 from cms_resource where ID=" + ResourceID + ")").Tables(0)
        LoadTree(dtResource, strChildResID, ResourceID)
        strChildResID += ResourceID
        Return strChildResID
    End Function


    Public Shared Sub LoadTree(ByVal dtResource As DataTable, ByRef strChildResID As String, ByVal ResID As Long)
        Dim dr() As DataRow = dtResource.Select("PID=" + ResID.ToString)
        For i As Integer = 0 To dr.Length - 1
            strChildResID += DbField.GetStr(dr(i), "ID") + ","
            LoadTree(dtResource, strChildResID, DbField.GetLng(dr(i), "ID"))
        Next
    End Sub

    Public Shared Function GetDeptName(ByVal lngDeptID As Long, ByRef strDeptName As String, ByVal dtDept As DataTable) As String
        If lngDeptID <> -1 Then

            Dim dr() As DataRow = dtDept.Select("ID=" + lngDeptID.ToString)
            If dr.Length > 0 Then
                strDeptName = DbField.GetStr(dr(0), "Name") + "\" + strDeptName
                GetDeptName(DbField.GetLng(dr(0), "PID"), strDeptName, dtDept)
            End If
        End If
        Return strDeptName
    End Function

    Public Shared Function ShowChildTablesByID(ByVal ID As String, ByVal ResourceID As String) As DataSet
        Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        Dim TableName As String = info.TableName
        Dim ParentResourceID As String = GetParentByResourceID(TableName)
        Dim Sql As String = GetSqlByResourceID(ParentResourceID, " and ID=" + ID)
        Dim FullSql As String = Sql + ";"
        Dim ChildSql As String = ""
        Dim ChildCondition As String = ""
        Dim drRelation As DataTable = GetRelationTable(ParentResourceID)
        For Each row As DataRow In drRelation.Rows
            ChildCondition = " and " + row("ChildColDispName") + " in (select [" + row("ParentColDispName") + "] from (" + Sql + ") T )"
            If row("ChildTableType").ToString.ToUpper = "DOC" Then
                ChildSql = Document.GetDocumentSqlByResourceID(row("ChildResourceID".ToString), ChildCondition)

            Else
                ChildSql = GetSqlByResourceID(row("ChildResourceID".ToString), ChildCondition)

            End If

            FullSql = FullSql + ChildSql + ";"

        Next
        Dim ds As DataSet = New DataSet
        ds = SDbStatement.Query(FullSql)

        For i As Integer = 0 To drRelation.Rows.Count - 1
            ds.Relations.Add(i.ToString, ds.Tables(0).Columns(drRelation.Rows(i)("ParentColDispName")), ds.Tables(i + 1).Columns(drRelation.Rows(i)("ChildColDispName")))
            ds.Tables(i + 1).TableName = drRelation.Rows(i)("ChildTableDispName")

        Next

        Return ds
    End Function


    ''' <summary>
    ''' 查询指定列数据
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ColumnStr"></param>
    ''' <param name="Condition"></param>
    ''' <param name="IsOrderBy"></param>
    ''' <returns></returns>
    Public Shared Function GetSqlColumnByResourceID(ByVal ResourceID As String, ByVal ColumnStr As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String

        Dim strSql As String = ""
        Dim strColumns As String = ""
        Try
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
            Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
            Dim TableName As String = info.TableName
            Dim ParentResourceID As String = GetTopParentID(ResourceID, dtResource)

            Dim dtColumns As DataTable = GetColumnsByResouceID(ParentResourceID)
            For Each dr As DataRow In dtColumns.Rows
                strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
            Next
            If Not info.IsView Then
                Dim ResourceInfoList As List(Of ResourceInfo) = GetAllResourceInfoByTableName(info.TableName)

                If info.ShowChild = 1 Then
                    Dim ReturnStr As String = GetAllChildResourceID(info.ID, ResourceInfoList)
                    If ReturnStr <> "" Then
                        ReturnStr = info.ID + ReturnStr
                    Else
                        ReturnStr = info.ID
                    End If
                    Condition = Condition + " and resid IN (" + ReturnStr + ")" ''此处写一个递归
                Else
                    Condition = Condition + " and  resid=" + info.ID
                End If
            End If

            strColumns = "ID,ResID" + strColumns
            strSql = "select cast(ID as varchar)ID,cast(ResID as varchar)ResID," + ColumnStr + " from (select " + strColumns + " from " + TableName + ") T where 1=1 " + Condition + IIf(IsOrderBy, IIf(info.ResOrder.Trim <> "", " order by " + info.ResOrder, ""), "")
        Catch ex As Exception
            SLog.Err(ex.Message)
        End Try
        Return strSql
    End Function

    Public Shared Function GetSqlByResourceID(ByVal ResourceID As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String

        Dim strSql As String = ""
        Dim strColumns As String = ""
        Try
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
            Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
            Dim TableName As String = info.TableName
            Dim ParentResourceID As String = GetTopParentID(ResourceID, dtResource)

            Dim dtColumns As DataTable = GetColumnsByResouceID(ParentResourceID)
            For Each dr As DataRow In dtColumns.Rows
                strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
            Next
            If Not info.IsView Then
                Dim ResourceInfoList As List(Of ResourceInfo) = GetAllResourceInfoByTableName(info.TableName)

                If info.ShowChild = 1 Then
                    Dim ReturnStr As String = GetAllChildResourceID(info.ID, ResourceInfoList)
                    If ReturnStr <> "" Then
                        ReturnStr = info.ID + ReturnStr
                    Else
                        ReturnStr = info.ID
                    End If
                    Condition = Condition + " and resid IN (" + ReturnStr + ")" ''此处写一个递归
                Else
                    Condition = Condition + " and  resid=" + info.ID
                End If
            End If

            strColumns = "ID,ResID" + strColumns
            strSql = "select * from (select " + strColumns + " from " + TableName + ") T where 1=1 " + Condition + IIf(IsOrderBy, IIf(info.ResOrder.Trim <> "", " order by " + info.ResOrder, ""), "")
        Catch ex As Exception
            SLog.Err(ex.Message)
        End Try
        Return strSql
    End Function

    Public Shared Function GetSqlByResourceID(ByVal ResourceID As String, ByVal UserID As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String
        Dim strSql As String = ""
        Dim strColumns As String = ""
        Dim FilterString As String = ""
        ' ResourceCondition.GetResourceCondition(ResourceID, MTSearchType.GeneralRowWhere, UserID)

        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
        Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        Dim TableName As String = info.TableName
        Dim ParentResourceID As String = GetTopParentID(ResourceID, dtResource)

        Dim dtColumns As DataTable = GetColumnsByResouceID(ParentResourceID)
        For Each dr As DataRow In dtColumns.Rows
            strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
        Next
        If Not info.IsView Then
            Dim ResourceInfoList As List(Of ResourceInfo) = GetAllResourceInfoByTableName(info.TableName)

            If info.ShowChild = 1 Then
                Dim ReturnStr As String = GetAllChildResourceID(info.ID, ResourceInfoList)
                If ReturnStr <> "" Then
                    ReturnStr = info.ID + ReturnStr
                Else
                    ReturnStr = info.ID
                End If
                Condition = Condition + " and resid IN (" + ReturnStr + ")" ''此处写一个递归
            Else
                Condition = Condition + " and resid=" + info.ID
            End If

        Else
            FilterString = ResourceCondition.GetResourceCondition(ResourceID, MTSearchType.ViewFilter, UserID)
            If FilterString <> "" Then
                FilterString = " and " + FilterString
            End If
        End If

        strColumns = "ID,ResID" + strColumns
        strSql = "select * from (select " + strColumns + " from " + TableName + " where 1=1 " + FilterString + ") T where 1=1 " + Condition + IIf(IsOrderBy, IIf(info.ResOrder.Trim <> "", " order by " + info.ResOrder, ""), "")
        Return strSql
    End Function

    Public Shared Function GetSqlByResourceInfo(ByVal ResourceInfo As ResourceInfo, ByVal UserID As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String

        Try
            Dim strSql As String = ""
            Dim strColumns As String = ""
            Dim ParentResourceID As String = "-9999"
            Dim SearchCondition As String = ""
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
            ParentResourceID = GetTopParentID(ResourceInfo.ID, dtResource)

            If ResourceInfo.IsView Then
                SearchCondition = ResourceCondition.GetResourceCondition(ResourceInfo.ID, MTSearchType.ViewFilter, UserID)
            Else
                Dim ResourceInfoList As List(Of ResourceInfo) = GetAllResourceInfoByTableName(ResourceInfo.TableName)

                If ResourceInfo.ShowChild = 1 Then
                    Dim ReturnStr As String = GetAllChildResourceID(ResourceInfo.ID, ResourceInfoList)
                    If ReturnStr <> "" Then
                        ReturnStr = ResourceInfo.ID + ReturnStr
                    Else
                        ReturnStr = ResourceInfo.ID
                    End If
                    Condition = Condition + " and resid IN (" + ReturnStr + ")" ''此处写一个递归
                Else
                    Condition = Condition + " and resid=" + ResourceInfo.ID
                End If
            End If


            Dim dtColumns As DataTable = GetColumnsByResouceID(ParentResourceID)
            For Each dr As DataRow In dtColumns.Rows
                strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
            Next

            strColumns = "ID,ResID" + strColumns
            strSql = "select * from (select " + strColumns + " from " + ResourceInfo.TableName + IIf(SearchCondition.Trim = "", "", " where " + SearchCondition.Trim) + ") T where 1=1 " + Condition + IIf(IsOrderBy, IIf(ResourceInfo.ResOrder.Trim <> "", " order by " + ResourceInfo.ResOrder, ""), "")
            Return strSql
        Catch ex As Exception
            Return ""
        End Try


    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="IsRowRights">是否启用行权限中和过虑条件</param>
    ''' <param name="ResourceInfo"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="IsOrderBy"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSqlByResourceInfo(ByVal IsRowRights As Boolean, ByVal ResourceInfo As ResourceInfo, ByVal UserID As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String
        Dim strSql As String = ""
        Dim strColumns As String = ""
        Dim ParentResourceID As String = "-9999"
        Dim SearchCondition As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
        ParentResourceID = GetTopParentID(ResourceInfo.ID, dtResource)

        If ResourceInfo.IsView Then
            SearchCondition = ResourceCondition.GetResourceCondition(ResourceInfo.ID, MTSearchType.ViewFilter, UserID)
        Else
            Dim ResourceInfoList As List(Of ResourceInfo) = GetAllResourceInfoByTableName(ResourceInfo.TableName)

            If ResourceInfo.ShowChild = 1 Then
                Dim ReturnStr As String = GetAllChildResourceID(ResourceInfo.ID, ResourceInfoList)
                If ReturnStr <> "" Then
                    ReturnStr = ResourceInfo.ID + ReturnStr
                Else
                    ReturnStr = ResourceInfo.ID
                End If
                Condition = Condition + " and resid IN (" + ReturnStr + ")" ''此处写一个递归
            Else
                Condition = Condition + " and resid=" + ResourceInfo.ID
            End If
        End If


        If IsRowRights Then
            Dim condi As String = ResourceCondition.GetRowRightsWhere(ResourceInfo.ID, UserID)
            If condi <> "" Then
                If SearchCondition = "" Then
                    SearchCondition = condi
                Else
                    SearchCondition += " and " + condi
                End If
            End If



        End If

        Dim dtColumns As DataTable = GetColumnsByResouceID(ParentResourceID)
        For Each dr As DataRow In dtColumns.Rows
            strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
        Next

        strColumns = "ID,ResID" + strColumns
        strSql = "select * from (select " + strColumns + " from " + ResourceInfo.TableName + IIf(SearchCondition.Trim = "", "", " where " + SearchCondition.Trim) + ") T where 1=1 " + Condition + IIf(IsOrderBy, IIf(ResourceInfo.ResOrder.Trim <> "", " order by " + ResourceInfo.ResOrder, ""), "")
        Return strSql
    End Function

    Public Shared Function GetAllChildResourceID(ByVal ResourceID As String, ByVal ResourceList As List(Of ResourceInfo)) As String
        Dim ReturnStr As String = ""
        For Each info As ResourceInfo In ResourceList

            If info.ParentID = ResourceID Then
                ReturnStr = ReturnStr + "," + info.ID + GetAllChildResourceID(info.ID, ResourceList)
            End If
        Next
        Return ReturnStr

    End Function

    Public Shared Function GetAllChildResourceID(ByVal ResourceID As String, ByVal dtResource As DataTable) As String
        Dim ReturnStr As String = ""
        For Each dr As DataRow In dtResource.Rows

            If DbField.GetStr(dr, "PID").Trim() = ResourceID.Trim() Then
                Dim RecID As String = DbField.GetStr(dr, "ID").Trim()
                ReturnStr = ReturnStr & "," & RecID & GetAllChildResourceID(RecID, dtResource)
            End If
        Next
        Return ReturnStr

    End Function

    Public Shared Function GetAllResourceInfoByTableName(ByVal TableName As String) As List(Of ResourceInfo)
        Dim ResourceInfoList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        Dim sql As String = "select * from cms_resource" ' where res_table='" + TableName + "'"
        Dim info As ResourceInfo
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim dr() As DataRow = dt.Select("res_table='" + TableName + "'")
        For i As Integer = 0 To dr.Length - 1
            info = New ResourceInfo
            SetRowToResourceInfo(dr(i), info, dt)
            ResourceInfoList.Add(info)
        Next
        Return ResourceInfoList
    End Function

    Public Shared Function GetAllResourceInfoByCondition(ByVal Condition As String) As List(Of ResourceInfo)
        Dim ResourceInfoList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        Dim sql As String = "select * from cms_resource  where 1=1 " + Condition.Trim
        Dim info As ResourceInfo
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)

        For i As Integer = 0 To dt.Rows.Count - 1
            info = New ResourceInfo
            SetRowToResourceInfo(dt.Rows(i), info, dt)
            ResourceInfoList.Add(info)
        Next
        Return ResourceInfoList
    End Function

    Public Shared Function GetTableNameByResourceID(ByVal ResouceID As String) As String

        Dim sql As String = "select res_table from cms_resource where ID='" + ResouceID + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return ""
        End If

    End Function

    Public Shared Function GetTopParentID(ByVal ResourceID As String, ByVal dtResource As DataTable) As Long
        Dim info As ResourceInfo = New ResourceInfo
        Dim row As DataRow = dtResource.Rows.Find(ResourceID)
        If row Is Nothing Then
            Return Nothing
        Else
            If row("res_type") = 0 Then
                ' SetRowToResourceInfo(row, info)
                Return Convert.ToInt64(row("ID"))
            Else
                Return GetTopParentID(row("pid"), dtResource)
            End If
        End If
    End Function

    Public Shared Function GetRelationTable(ByVal ResourceID As String) As DataTable
        Dim dt As DataTable = New DataTable
        Dim ParentID As String = ""
        Dim Sql As String = "SELECT * FROM CMS_RESOURCE;"

        Dim dtResource As DataTable = SDbStatement.Query(Sql).Tables(0)

        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
        Dim ParentResourceID As Long = GetTopParentID(ResourceID, dtResource)
        Sql = "select ID  from dbo.CMS_RESOURCE where id=" + ResourceID + " and RES_NOSHOW_RELTABLES=0;"
        Sql = Sql + "select CD_RESID,CD_COLNAME,CD_DISPNAME from CMS_TABLE_DEFINE;"
        Sql = Sql & "if exists (select * from CMS_RELATED_TABLE RT where RT.RT_TYPE=0 AND (RT.RT_TAB1_RESID=" + ResourceID + "))" _
             & " select RT.RT_TAB1_RESID,RT.RT_TAB1_COLNAME,RT.RT_TAB2_RESID,RT.RT_TAB2_COLNAME,R.RES_SHOW_INREL from CMS_RESOURCE R  join  CMS_RELATED_TABLE RT on RT.RT_TAB2_RESID=R.ID  and R.RES_SHOW_INREL=1 where RT.RT_TYPE=0 AND (RT.RT_TAB1_RESID=" + ResourceID.Trim() + ") order by RT_SHOWORDER" _
             & " else " _
             & "select RT.RT_TAB1_RESID,RT.RT_TAB1_COLNAME,RT.RT_TAB2_RESID,RT.RT_TAB2_COLNAME,R.RES_SHOW_INREL from CMS_RESOURCE R  join  CMS_RELATED_TABLE RT on RT.RT_TAB2_RESID=R.ID  and R.RES_SHOW_INREL=1 where RT.RT_TYPE=0 AND (RT.RT_TAB1_RESID=" + ParentResourceID.ToString() + ") order by RT_SHOWORDER;"


        Dim ds As DataSet = SDbStatement.Query(Sql)

        If ds.Tables(0).Rows.Count = 0 Then
            Return dt
        End If

        Dim dtColumn As DataTable = ds.Tables(1)
        dtColumn.PrimaryKey = New DataColumn() {dtColumn.Columns("CD_RESID"), dtColumn.Columns("CD_COLNAME")}
        Dim dtRelation As DataTable = ds.Tables(2)
        If dtRelation.Rows.Count = 0 Then
            Return dt
        End If



        Dim ReturnDataSet As DataSet = New DataSet
        dt.Columns.Add("ParentResourceID")
        dt.Columns.Add("ParentColName")
        dt.Columns.Add("ParentColDispName")
        dt.Columns.Add("ParentTableDispName")
        dt.Columns.Add("ParentTableName")
        dt.Columns.Add("ChildColName")
        dt.Columns.Add("ChildResourceID")
        dt.Columns.Add("ChildTableDispName")
        dt.Columns.Add("ChildTableName")
        dt.Columns.Add("ChildTableType")
        dt.Columns.Add("ChildColDispName")
        For i As Integer = 0 To dtRelation.Rows.Count - 1
            Dim ChildResourceID As Long = GetTopParentID(dtRelation.Rows(i)("RT_TAB2_RESID"), dtResource)
            Dim Row As DataRow = dtColumn.Rows.Find(New Object() {ParentResourceID, dtRelation.Rows(i)("RT_TAB1_COLNAME")})
            Dim ParentColDispName As String = ""
            Dim ChildColDispName As String = ""
            If Row IsNot Nothing Then
                ParentColDispName = Row("CD_DISPNAME")
            Else
                Return Nothing
            End If
            Row = dtColumn.Rows.Find(New Object() {ChildResourceID, dtRelation.Rows(i)("RT_TAB2_COLNAME")})
            If Row IsNot Nothing Then
                ChildColDispName = Row("CD_DISPNAME")
            Else
                Return Nothing
            End If
            Dim ParentRow As DataRow = dtResource.Rows.Find(dtRelation.Rows(i)("RT_TAB1_RESID"))
            Dim ChildRow As DataRow = dtResource.Rows.Find(dtRelation.Rows(i)("RT_TAB2_RESID"))

            Row = dt.NewRow
            Row("ParentResourceID") = dtRelation.Rows(i)("RT_TAB1_RESID")
            Row("ChildResourceID") = dtRelation.Rows(i)("RT_TAB2_RESID")
            Row("ParentTableDispName") = ParentRow("name")
            Row("ParentTableName") = ParentRow("RES_TABLE")
            Row("ParentColName") = dtRelation.Rows(i)("RT_TAB1_COLNAME")
            Row("ChildColName") = dtRelation.Rows(i)("RT_TAB2_COLNAME")
            Row("ParentColDispName") = ParentColDispName
            Row("ChildTableDispName") = ChildRow("name")
            Row("ChildTableName") = ChildRow("RES_TABLE")
            Row("ChildTableType") = ChildRow("RES_TableType")
            Row("ChildColDispName") = ChildColDispName
            dt.Rows.Add(Row)
        Next
        ReturnDataSet.Tables.Add(dt)
        Return dt
    End Function


    Public Shared Function GetParentByResourceID(ByVal TableName As String) As String

        Dim sql As String = "select ID from cms_resource where res_table='" + TableName + "' and res_type=0"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return ""
        End If

    End Function

    Public Shared Function GetRelationTable(ByVal ParentResourceDescription As String, ByVal ChildResourceName As String) As DataTable


        Return GetRelation(ParentResourceDescription, ChildResourceName).Tables(0)

    End Function
    Public Shared Function GetRelation(ByVal ParentResourceDescription As String, ByVal ChildResourceName As String) As DataSet

        Dim sql As String = "select CR.NAME as ParentResourceName,RT_TAB1_COLNAME as ParentColName,RT_TAB2_COLNAME as ChildColName," _
            & "T.RT_TAB2_RESID as ChildResourceID,Parent.CD_DISPNAME as ParentColDispName,PR.RES_TABLE AS ChildTableName," _
            & "PR.Name as ChildTableDispName,PR.RES_TableType as ChildTableType," _
            & "Child.CD_DISPNAME as ChildColDispName FROM CMS_RELATED_TABLE T " _
            & " join dbo.CMS_TABLE_DEFINE Parent on  Parent.CD_RESID=T.RT_TAB1_RESID and T.RT_TAB1_COLNAME=Parent.CD_COLName " _
            & " join CMS_TABLE_DEFINE Child on  Child.CD_RESID=T.RT_TAB2_RESID and T.RT_TAB2_COLNAME=Child.CD_COLName " _
            & " join CMS_RESOURCE PR ON T.RT_TAB2_RESID=PR.ID " _
            & " join CMS_RESOURCE CR ON T.RT_TAB1_RESID=CR.ID " _
            & "  WHERE RT_TYPE=0 and CR.Res_comments ='" + ParentResourceDescription + "' and PR.Name='" + ChildResourceName + "'"

        Return SDbStatement.Query(sql.Trim)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ParentResourceID"></param>
    ''' <param name="IsGetInputRelated">是获取输入关联</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRelation(ByVal ParentResourceID As String, ByVal IsGetInputRelated As Boolean) As DataSet

        Dim sql As String = "select distinct Parent.NAME as ParentResourceName,Parent.RES_TABLE as ParentTableName," _
            & " T.RT_TAB2_RESID as ChildResourceID,Child.RES_TABLE AS ChildTableName," _
            & " Child.Name as ChildResourceName,RT_TAB1_COLNAME as ParentColName,RT_TAB2_COLNAME as ChildColName " _
            & " FROM CMS_RELATED_TABLE T  join dbo.CMS_RESOURCE Parent " _
            & " on  Parent.ID=T.RT_TAB1_RESID " _
            & " join CMS_RESOURCE Child on  Child.ID=T.RT_TAB2_RESID " _
            & " WHERE Parent.ID IN (SELECT ID FROM cms_resource WHERE RES_TABLE IN (select RES_TABLE from cms_resource where id=" + ParentResourceID + ") AND RES_TYPE=0)"
        If Not IsGetInputRelated Then sql &= " and RT_TYPE=0"
        Return SDbStatement.Query(sql.Trim)

    End Function


    Public Shared Function GetInputRelation(ByVal ParentResourceID As String) As DataSet

        Dim sql As String = "select distinct Parent.NAME as ParentResourceName,Parent.RES_TABLE as ParentTableName," _
& " T.RT_TAB2_RESID as ChildResourceID,Parent.RES_TABLE AS ChildTableName," _
& " Child.Name as ChildResourceName,RT_TAB1_COLNAME as ParentColName,RT_TAB2_COLNAME as ChildColName " _
& " FROM CMS_RELATED_TABLE T  join dbo.CMS_RESOURCE Parent " _
& " on  Parent.ID=T.RT_TAB1_RESID " _
& " join CMS_RESOURCE Child on  Child.ID=T.RT_TAB2_RESID " _
& " WHERE RT_TYPE=1  and Parent.ID IN (SELECT ID FROM cms_resource WHERE RES_TABLE IN (select RES_TABLE from cms_resource where id=" + ParentResourceID + ") AND RES_TYPE=0)"

        Return SDbStatement.Query(sql.Trim)

    End Function

    Public Shared Function GetColumnsByResouceID(ByVal Resourceid As String) As DataTable

        Dim sql As String = "select  D.CD_COLNAME,D.CD_DISPNAME," _
        & "isnull(D.CD_IS_SYSFIELD,0) as CD_IS_SYSTIELD from dbo.CMS_TABLE_SHOW S" _
        & " join dbo.CMS_TABLE_DEFINE D ON D.CD_COLNAME=S.CS_COLNAME and S.CS_RESID=D.CD_RESID" _
        & " where CS_RESID=" + Resourceid + "  ORDER BY CS_SHOW_ORDER "
        Return SDbStatement.Query(sql).Tables(0)

    End Function

    Public Shared Function GetColumnOptionList(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        Dim ParentID As String = Resource.GetTopParentID(ResourceID, dtResource)
        Dim sql As String = "select CMS_COL_OPTION.CDX_VALUE from cms_table_define" _
        & " join CMS_COL_OPTION on cms_table_define.CD_RESID=CMS_COL_OPTION.CDX_RESID and cms_table_define.cd_colname=CMS_COL_OPTION.CDX_COLNAME" _
        & " where cms_table_define.CD_DISPNAME='" + ColumnDescription + "' and cms_table_define.CD_RESID =" + ParentID + " ORDER BY CMS_COL_OPTION.CDX_AIID"
        Return SDbStatement.Query(sql)

    End Function
    Public Shared Function GetColumnRadioList(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        Dim ParentID As String = Resource.GetTopParentID(ResourceID, dtResource)

        Dim sql As String = "select CMS_COL_RADIO.CDR_VALUE from cms_table_define" _
        & " join CMS_COL_RADIO on cms_table_define.CD_RESID=CMS_COL_RADIO.CDR_RESID and cms_table_define.cd_colname=CMS_COL_RADIO.CDR_COLNAME" _
        & " where cms_table_define.CD_DISPNAME='" + ColumnDescription + "' and cms_table_define.CD_RESID=" + ParentID + " ORDER BY CMS_COL_RADIO.CDR_AIID"
        Return SDbStatement.Query(sql)

    End Function


    Public Shared Function GetDictionaryList(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet
        Dim sql As String = "select	Destination.CD_DISPNAME,Destination.cd_colname," & _
            "  cms_resource.RES_TABLE " & _
            " from CMS_COL_DICTIONARY DICTIONARY " & _
            " join cms_table_define Source" & _
            " on Source.CD_RESID=DICTIONARY.CDZ2_RESID1 " & _
            " and Source.cd_colname=DICTIONARY.CDZ2_MainCol" & _
            " join cms_table_define Destination " & _
            " on Destination.CD_RESID=DICTIONARY.CDZ2_RESID2 " & _
            " and Destination.cd_colname=DICTIONARY.CDZ2_COL2 " & _
            " join cms_resource on cms_resource.ID=DICTIONARY.CDZ2_RESID2" & _
            " where Source.CD_DISPNAME='" + ColumnDescription + "'" & _
            " and  Source.CD_RESID in (select id from cms_resource where res_type=0 and res_table in (select res_table from cms_resource where id=" + ResourceID + "))" & _
            " order by DICTIONARY.CDZ2_SHOWORDER"
 
        Dim dt As DataTable = SDbStatement.Query(Sql).Tables(0)
        If dt.Rows.Count <= 0 Then
            Return Nothing
        End If
        Dim strColumns As String = ""
        For Each dr As DataRow In dt.Rows
            strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
        Next
        strColumns = "ID" + strColumns
        Sql = "select * from (select " + strColumns + " from " + dt.Rows(0)("RES_TABLE") + ") T "
        Return SDbStatement.Query(Sql)

    End Function


    Public Shared Function GetDictionaryReturnColumn(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataTable
        Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)

        If ResInfo.IsView Then
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
            ResourceID = Resource.GetTopParentID(ResInfo.ParentID, dtResource).ToString

            '  strTableName = Resource.GetTableNameByResourceID(ParentID.ToString)
        End If
        ' GetParentByResourceID (

        Dim sql As String = "select	Destination.CD_RESID DicRESID,Destination.CD_DISPNAME,Destination.cd_colname," & _
           "  cms_resource.RES_TABLE,Destination.CD_Type DataType " & _
           " from CMS_COL_DICTIONARY DICTIONARY " & _
           " join cms_table_define Source" & _
           " on Source.CD_RESID=DICTIONARY.CDZ2_RESID1 " & _
           " and Source.cd_colname=DICTIONARY.CDZ2_MainCol" & _
           " join cms_table_define Destination " & _
           " on Destination.CD_RESID=DICTIONARY.CDZ2_RESID2 " & _
           " and Destination.cd_colname=DICTIONARY.CDZ2_COL2 " & _
           " join cms_resource on cms_resource.ID=DICTIONARY.CDZ2_RESID2" & _
           " where (Source.CD_DISPNAME='" + ColumnDescription + "' OR Source.CD_COLNAME='" + ColumnDescription + "')" & _
           " and  Source.CD_RESID in (select id from cms_resource where res_type=0 and res_table in (select res_table from cms_resource where id=" + ResourceID + "))" & _
           " order by DICTIONARY.CDZ2_SHOWORDER"

        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count <= 0 Then
            Return Nothing
        End If
        Return dt
    End Function

    Public Shared Function GetDictionaryReturnRelatedColumn(ByVal ResourceID As String, ByVal ColumnDescription As String) As DataSet

        Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)

        If ResInfo.IsView Then
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
            ResourceID = Resource.GetTopParentID(ResInfo.ParentID, dtResource).ToString

            '  strTableName = Resource.GetTableNameByResourceID(ParentID.ToString)
        End If

        Dim sql As String = " select	Destination2.CD_DISPNAME DicColCNName,Destination2.cd_colname DicColName,  cms_resource.RES_TABLE DicTableName,	Destination1.CD_DISPNAME ResColCNName,Destination1.cd_colname ResColName from CMS_COL_DICTIONARY DICTIONARY  " _
                & "  join cms_table_define Source on Source.CD_RESID=DICTIONARY.CDZ2_RESID1  and Source.cd_colname=DICTIONARY.CDZ2_MainCol " _
                & "   join cms_table_define Destination1 on Destination1.CD_RESID=DICTIONARY.CDZ2_RESID1  and Destination1.cd_colname=DICTIONARY.CDZ2_Col1 " _
                & "   join cms_table_define Destination2  on Destination2.CD_RESID=DICTIONARY.CDZ2_RESID2  and Destination2.cd_colname=DICTIONARY.CDZ2_COL2  " _
                & "  join cms_resource on cms_resource.ID=DICTIONARY.CDZ2_RESID2 where Source.CD_DISPNAME='" + ColumnDescription + "' " _
                & "  and  Source.CD_RESID in (select id from cms_resource where res_type=0 and res_table in (select res_table from cms_resource where id=" + ResourceID + ")) order by DICTIONARY.CDZ2_SHOWORDER"

        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)

        Return SDbStatement.Query(sql)
    End Function


    Public Shared Function GetDictionaryReturnResourceID(ByVal ResourceID As String, ByVal ColumnDescription As String) As String
        Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        If ResInfo.IsView Then
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
            ResourceID = Resource.GetTopParentID(ResInfo.ParentID, dtResource)
        End If

        Dim sql As String = "select top 1	cms_resource.ID DicResID, cms_resource.RES_TABLE DicTableName  from CMS_COL_DICTIONARY DICTIONARY " _
            & " join cms_table_define Source on Source.CD_RESID=DICTIONARY.CDZ2_RESID1  and Source.cd_colname=DICTIONARY.CDZ2_MainCol " _
            & " join cms_resource on cms_resource.ID=DICTIONARY.CDZ2_RESID2 where Source.CD_DISPNAME='" + ColumnDescription + "' " _
            & " and  Source.CD_RESID in (select id from cms_resource where res_type=0 and res_table in (select res_table from cms_resource where id=" + ResourceID + ")) order by DICTIONARY.CDZ2_SHOWORDER"


        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("DicResID").ToString
        End If
        Return ""
    End Function

    ''' <summary>
    ''' 返回字典数据中返回值的中文列名
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="ColumnDescription"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDictionaryReturnColumnName(ByVal ResourceID As String, ByVal ColumnDescription As String) As String
        Dim sql As String = "select	Destination.CD_DISPNAME,Destination.cd_colname," & _
            "  cms_resource.RES_TABLE " & _
            " from CMS_COL_DICTIONARY DICTIONARY " & _
            " join cms_table_define Source" & _
            " on Source.CD_RESID=DICTIONARY.CDZ2_RESID1 " & _
            " and Source.cd_colname=DICTIONARY.CDZ2_COL1" & _
            " join cms_table_define Destination " & _
            " on Destination.CD_RESID=DICTIONARY.CDZ2_RESID2 " & _
            " and Destination.cd_colname=DICTIONARY.CDZ2_COL2 " & _
            " join cms_resource on cms_resource.ID=DICTIONARY.CDZ2_RESID2" & _
            " where Source.CD_DISPNAME='" + ColumnDescription + "'" & _
            " and  Source.CD_RESID in (select id from cms_resource where res_type=0 and res_table in (select res_table from cms_resource where id=" + ResourceID + "))" & _
            " order by DICTIONARY.CDZ2_SHOWORDER"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count <= 0 Then
            Return ""
        Else
            Return SDbStatement.Query(sql).Tables(0).Rows(0)(0)
        End If


    End Function


    ''' <summary>
    ''' 返回所有字段下拉列表框
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnOptionListByResourceID(ByVal ResourceID As String) As DataSet
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        Dim ParentID As String = Resource.GetTopParentID(ResourceID, dtResource)
        Dim sql As String = "select cms_table_define.CD_DISPNAME,cms_table_define.CD_COLNAME,CMS_COL_OPTION.CDX_VALUE from cms_table_define" _
        & " join CMS_COL_OPTION on cms_table_define.CD_RESID=CMS_COL_OPTION.CDX_RESID and cms_table_define.cd_colname=CMS_COL_OPTION.CDX_COLNAME" _
        & " where  cms_table_define.CD_RESID =" + ParentID + " ORDER BY CMS_COL_OPTION.CDX_AIID"
        Return SDbStatement.Query(sql)


    End Function
End Class
