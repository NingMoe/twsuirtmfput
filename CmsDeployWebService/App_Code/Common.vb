Imports Microsoft.VisualBasic
Imports Unionsoft.Implement
Imports Unionsoft.Platform
Imports NetReusables
Imports System.Data
Imports System.Collections.Generic
Imports System.IO
Imports System.Xml
Public Class Common
    Public Shared Function Add(ByVal ResourceID As String, ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Boolean
        Try
            Dim hst As New Hashtable
            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim dtResourceColumns As DataTable = GetDefineColumnsByTableName(TableName)
            For Each fi As Field In Common.GetFieldListAll(ResourceID)
                If fi.ValueType = Field.FieldValueType.AutoCoding Then
                    hst.Add(fi.Name, AutoCode.GetAutoCode(ResourceID, fi.Name, True))

                Else

                    For Each li As FieldInfo In FieldValueList
                        If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                            If li.FieldValue = "" Then
                                hst.Add(fi.Name, DBNull.Value)
                                '  row(fi.Name) = DBNull.Value
                            Else
                                hst.Add(fi.Name, li.FieldValue)

                            End If
                        End If
                    Next

                End If
            Next
            hst.Add("ID", NetReusables.TimeId.CurrentMilliseconds())
            hst.Add("ResID", ResourceID)
            hst.Add("CRTID", UserID)
            hst.Add("CRTTIME", Date.Now)
            hst.Add("RELID", 0)
            If ResourceID = 1300 Then
                hst.Add("HOST_ID", "565440274712")  '默认添加到统计员部门
                hst.Add("EMP_PASS", "FC306886DC5B253E")
                hst.Add("Status", "0")
		'hst.Add("SHOW_ORDER", "100")
            End If
            SDbStatement.InsertRow(hst, TableName)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function




    Public Shared Function GetCurrentMilliseconds() As String
        Return NetReusables.TimeId.CurrentMilliseconds()
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="IsRowRights">是否使用行记录权限中的条件过虑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFieldNameSum(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal IsRowRights As Boolean, ByVal fieldName As String) As String
        Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID.ToString)
        If ResourceInfo IsNot Nothing Then
            Dim sql As String
            If ResourceInfo.TableType = "TWOD" Then
                sql = Resource.GetSqlByResourceInfo(IsRowRights, ResourceInfo, UserID, Condition, False)
            Else
                sql = Document.GetDocumentSqlByResourceID(ResourceID.ToString, Condition)
            End If
            sql = "select sum(T." + fieldName + ") from (" + sql + ") T"
            Return SDbStatement.Query(sql).Tables(0).Rows(0)(0).ToString()
        Else
            Return ""
        End If
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="IsRowRights">是否使用行记录权限中的条件过虑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFieldNameSumList(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal IsRowRights As Boolean, ByVal fieldName As String) As DataTable

        Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID.ToString)
        If ResourceInfo IsNot Nothing Then
            Dim sql As String
            If ResourceInfo.TableType = "TWOD" Then
                sql = Resource.GetSqlByResourceInfo(IsRowRights, ResourceInfo, UserID, Condition, False)
            Else
                sql = Document.GetDocumentSqlByResourceID(ResourceID.ToString, Condition)
            End If
            Dim fieldStr As String() = fieldName.Split(",")
            Dim sumStr As String = "select "
            For index = 1 To fieldStr.Length - 1
                If index = fieldStr.Length - 1 Then
                    sumStr += " isnull(sum(T." + fieldStr(index) + "),0) as " + fieldStr(index)
                Else
                    sumStr += " isnull(sum(T." + fieldStr(index) + "),0) as " + fieldStr(index) + ", "
                End If
            Next
            sumStr += " from (" + sql + ") T  "


            Return SDbStatement.Query(sumStr).Tables(0)
        Else
            Return Nothing
        End If
    End Function


    Public Shared Function GetUserInfoIDMax(ByVal ResourceName As String, ByVal IsCheckTableName As Boolean) As String
        ' Dim Sql As String = "SELECT  max ([EmpCode])  FROM [OA1].[dbo].[HR_Employee] where ISNUMERIC([EmpCode])>0"
        Dim Sql As String
        Dim TableName As String
        If IsCheckTableName Then
            TableName = Resource.GetTableNameByResourceID(ResourceName)
        Else
            TableName = ResourceName
        End If

        If TableName = "" Then
            Sql = " SELECT max( convert(bigint,[EmpCode]))  FROM [HR_Employee]   where ascii([EmpCode]) between 48 and 57 "
        Else
            Sql = " SELECT count(*)  FROM  [HR_Employee]  where [EmpCode] = '" + TableName + "'"
        End If

        Dim dt As DataTable
        With SDbStatement.Query(Sql)
            dt = .Tables(0)
        End With
        If dt.Rows.Count = 1 Then
            Return dt.Rows(0)(0)
        End If


        Return ""


    End Function


    Public Shared Function GetDataListByTableName(ByVal TableName As String, ByVal Condition As String) As DataSet

        Dim Sql As String
        Dim ds As DataSet = New DataSet
        Dim Field As String = ""
        'If IsCheckTableName Then
        '    TableName = Resource.GetTableNameByResourceID(ResourceName)
        'Else
        '    TableName = ResourceName
        'End If

        Dim f() As Field = GetFieldList(GetResourceidByTableName(TableName))

        If TableName <> "" Then
            For i As Integer = 0 To f.Length - 1
                Field += "," + f(i).Name + " " + f(i).Description
            Next
            Field = "ID,ResID" & Field
            Sql = "SELECT * FROM (SELECT " + Field + " FROM  " + TableName + ")T "

            If Condition <> "" Then Sql = Sql + " where 1=1 " + Condition
            ds = SDbStatement.Query(Sql)

        End If

        Return ds


    End Function




    Public Shared Function AddReturnID(ByVal ResourceID As String, ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Long
        Try
            Dim hst As New Hashtable
            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim dtResourceColumns As DataTable = GetDefineColumnsByTableName(TableName)

            For Each fi As Field In Common.GetFieldListAll(ResourceID)
                If fi.ValueType = Field.FieldValueType.AutoCoding Then
                    hst.Add(fi.Name, AutoCode.GetAutoCode(ResourceID, fi.Name, True))

                Else

                    For Each li As FieldInfo In FieldValueList
                        If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                            If li.FieldValue = "" Then
                                hst.Add(fi.Name, DBNull.Value)
                                '  row(fi.Name) = DBNull.Value
                            Else
                                hst.Add(fi.Name, li.FieldValue)

                            End If
                        End If
                    Next

                End If
            Next


            Dim id As Long = NetReusables.TimeId.CurrentMilliseconds()
            hst.Add("ID", id)
            hst.Add("ResID", ResourceID)
            hst.Add("RELID", 0)
            hst.Add("CRTID", UserID)
            'hst.Add("EDTID", UserID)
            hst.Add("CRTTIME", Date.Now)
            'hst.Add("EDTTIME", Date.Now)
            SDbStatement.InsertRow(hst, TableName)
            Return id
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return 0
        End Try
    End Function

    Public Shared Function Add(ByVal ParentResourceID As String, ByVal ParentRecordID As String, ByVal ResourceID As String, ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Boolean
        Try
            Dim hst As New Hashtable
            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim dtResourceColumns As DataTable = GetDefineColumnsByTableName(TableName)

            For Each fi As Field In Common.GetFieldListAll(ResourceID)
                For Each li As FieldInfo In FieldValueList
                    If fi.Name = li.FieldName Or fi.Description = li.FieldDescription Then
                        If li.FieldValue = "" Then
                            hst.Add(fi.Name, DBNull.Value)
                        Else
                            hst.Add(fi.Name, li.FieldValue)
                        End If
                    End If
                Next

                If fi.ValueType = Field.FieldValueType.AutoCoding Then
                    If hst.Contains(fi.Name) Then hst.Remove(fi.Name)
                    hst.Add(fi.Name, AutoCode.GetAutoCode(ResourceID, fi.Name, True))
                End If
            Next

            'For Each dr As DataRow In dtResourceColumns.Rows 
            '    For Each li As FieldInfo In FieldValueList
            '        If DbField.GetStr(dr, "CD_COLNAME") = li.FieldName Or DbField.GetStr(dr, "CD_DISPNAME") = li.FieldDescription Then
            '            If li.FieldValue = "" Then
            '                hst.Add(DbField.GetStr(dr, "CD_COLNAME"), DBNull.Value)
            '            Else
            '                hst.Add(DbField.GetStr(dr, "CD_COLNAME"), li.FieldValue)
            '            End If
            '        End If
            '    Next
            'Next
            Dim dt As DataTable = Resource.GetRelation(ParentResourceID, True).Tables(0)

            Dim ParentTableName As String
            Dim ParentRelationField As String
            Dim ChildRelationField As String
            Dim ParentTable As DataTable
            If dt.Rows.Count > 0 Then
                ParentTableName = dt.Rows(0)("ParentTableName")
                ParentTable = SDbStatement.Query("select * from " + ParentTableName + " where id=" + ParentRecordID).Tables(0)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("ChildResourceID") = ResourceID And ParentTable.Rows.Count > 0 Then
                        ParentRelationField = dt.Rows(i)("ParentColName")
                        ChildRelationField = dt.Rows(i)("ChildColName")

                        If hst.Contains(ChildRelationField) Then hst.Remove(ChildRelationField)
                        hst.Add(ChildRelationField, ParentTable.Rows(0)(ParentRelationField))

                        'Dim sql As String = "select " + ParentRelationField + " from " + ParentTableName + " where id=" + ParentRecordID
                        'With SDbStatement.Query(sql).Tables(0)
                        '    If .Rows.Count > 0 Then
                        '        hst.Add(ChildRelationField, .Rows(0)(0))
                        '    End If
                        'End With
                        ' Exit For
                    End If
                Next
            End If
            hst.Add("ID", NetReusables.TimeId.CurrentMilliseconds())
            hst.Add("ResID", ResourceID)
            hst.Add("RELID", 0)
            hst.Add("CRTID", UserID)
            hst.Add("EDTID", UserID)
            hst.Add("CRTTIME", Date.Now)
            hst.Add("EDTTIME", Date.Now)

            SDbStatement.InsertRow(hst, TableName)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function EditUser(ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Boolean
        Try
            Dim hst As New Hashtable
            Dim TableName As String = Resource.GetTableNameByResourceID("1300")
            Dim dtResourceColumns As DataTable = GetDefineColumnsByResouceID("1300")
            For Each dr As DataRow In dtResourceColumns.Rows
                For Each li As FieldInfo In FieldValueList
                    If DbField.GetStr(dr, "CD_COLNAME") = li.FieldName Or DbField.GetStr(dr, "CD_DISPNAME") = li.FieldDescription Then
                        If li.FieldValue = "" Then
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), DBNull.Value)
                        Else
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), li.FieldValue)
                        End If
                    End If
                Next
            Next


            hst.Add("EDTID", UserID)
            hst.Add("EDTTIME", Date.Now)

            SDbStatement.UpdateRows(hst, TableName, "emp_id='" + UserID + "'")
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function Edit(ByVal ResourceID As String, ByVal ID As String, ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Boolean
        Try
            Dim hst As New Hashtable
            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim dtResourceColumns As DataTable = GetDefineColumnsByTableName(TableName)
            For Each dr As DataRow In dtResourceColumns.Rows
                For Each li As FieldInfo In FieldValueList
                    If DbField.GetStr(dr, "CD_COLNAME") = li.FieldName Or DbField.GetStr(dr, "CD_DISPNAME") = li.FieldDescription Then
                        If li.FieldValue = "" Then
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), DBNull.Value)
                        Else
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), li.FieldValue)
                        End If
                        ' 
                    End If
                Next
            Next
            hst.Add("EDTID", UserID)
            hst.Add("EDTTIME", Date.Now)

            SDbStatement.UpdateRows(hst, TableName, "id=" + ID)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function


    Public Shared Function EditByCondition(ByVal ResourceID As String, ByVal Condition As String, ByVal UserID As String, ByVal FieldValueList As FieldInfo()) As Boolean
        Try
            Dim hst As New Hashtable
            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim dtResourceColumns As DataTable = GetDefineColumnsByTableName(TableName)
            For Each dr As DataRow In dtResourceColumns.Rows
                For Each li As FieldInfo In FieldValueList
                    If DbField.GetStr(dr, "CD_COLNAME") = li.FieldName Or DbField.GetStr(dr, "CD_DISPNAME") = li.FieldDescription Then
                        If li.FieldValue = "" Then
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), DBNull.Value)
                        Else
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), li.FieldValue)
                        End If
                        ' 
                    End If
                Next
            Next
            hst.Add("EDTID", UserID)
            hst.Add("EDTTIME", Date.Now)
            If Condition <> "" Then
                Condition = " 1=1 " + Condition
            End If
            SDbStatement.UpdateRows(hst, TableName, Condition)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function Delete(ByVal ResourceID As String, ByVal ID As String, ByVal UserID As String) As Boolean
        Try
            Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
            If ResInfo.TableType.ToLower = "doc" Then
                Return DeleteDOC(ResourceID, ID, UserID)
            Else
                Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
                SDbStatement.DelRows(TableName, "id=" + ID)
                Return True
            End If
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function


    Public Shared Function Delete(ByVal ResourceID As String, ByVal ID As String, ByVal UserID As String, ByVal IsDeleteChildData As Boolean) As Boolean
        Try
            If IsDeleteChildData Then
                Return Delete(ResourceID, ID)
            Else
                Return Delete(ResourceID, ID, UserID)
            End If
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function

    Private Shared Function Delete(ByVal ResourceID As String, ByVal ID As String) As Boolean
        Try
            Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
            Dim dt As DataTable = Resource.GetRelation(ResourceID, False).Tables(0)
            Dim strSql As String = ""
            Dim TableName As String = ""
            For Each dr As DataRow In dt.Rows
                TableName = dr("ParentTableName").ToString()
                strSql &= "delete from " & dr("ChildTableName").ToString() & " where " & dr("ChildColName").ToString() & " in (select " & dr("ParentColName").ToString() & " from " & dr("ParentTableName").ToString() & " where ID=" & ID.Trim() & ");"
                ' SDbStatement.Execute(strSql)
            Next

            If TableName.Trim = "" Then Resource.GetTableNameByResourceID(ResourceID)

            strSql &= "delete from " & TableName & " where ID=" & ID.Trim() & ";"

            If SDbStatement.Execute(strSql) > 0 Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function DeleteByTableName(ByVal TableName As String, ByVal Condition As String, ByVal UserID As String) As Boolean
        Try
            Dim index As Integer = Condition.ToLower().Trim().IndexOf("and ")
            If (index = 0) Then
                Condition = Condition.ToLower().Trim().Substring(4)
            End If

            SDbStatement.DelRows(TableName, Condition)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function


    Public Shared Function DeleteDOC(ByVal ResourceID As String, ByVal ID As String, ByVal UserID As String) As Boolean
        Try
            Dim strSql As String = ""
            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            Dim dtDoc As DataTable = SDbStatement.Query("select DOC2_ID, DocHostName,IsUpDirectory from Cms_DocumentCenter where DOC2_ID in (select DocID from " + TableName + " where ID=" + ID + ")").Tables(0)

            If (dtDoc.Rows.Count > 0) Then
                Dim DocHostName As String = DbField.GetStr(dtDoc.Rows(0), "DocHostName")
                Dim DocID As String = DbField.GetStr(dtDoc.Rows(0), "DOC2_ID")
                If (DbField.GetBool(dtDoc.Rows(0), "IsUpDirectory")) Then
                    Dim FilePathUrl As String = HttpContext.Current.Server.MapPath("~")
                    FilePathUrl = FilePathUrl.Substring(0, FilePathUrl.LastIndexOf("\")) + DocHostName.Replace("/", "\\")
                    If File.Exists(FilePathUrl) Then File.Delete(FilePathUrl)
                Else
                    strSql += "delete from " + DocHostName + " where DOC2_ID=" + DocID + ";"
                End If
            End If
            strSql += "delete from Cms_DocumentCenter where DOC2_ID in (select DocID from " + TableName + " where ID=" + ID + ");"
            strSql += "delete from " + TableName + " where ID=" + ID
            SDbStatement.Execute(strSql)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function



    Public Shared Function GetCmsWebSite() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("cmswebsite").ConnectionString

    End Function

    Public Shared Function GetFieldList(ByVal ResourceID As String) As Field()
        '   Dim VersionSql As String = "SELECT SERVERPROPERTY('productversion') 版本号, SERVERPROPERTY ('productlevel') 级别, SERVERPROPERTY ('edition') 类型"
        '  Dim dtVersion As DataTable = SDbStatement.Query(VersionSql).Tables(0)
        '  Dim TableName As String = Resource.GetTableNameByResourceID(id)
        Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        If ResInfo.TableName <> "" Then
            Dim sql As String
            Dim Condtion As String = ""

            Dim strTableName As String = ResInfo.TableName

            If ResInfo.IsView Then
                Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
                dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
                Dim ParentID As Long = Resource.GetTopParentID(ResInfo.ParentID, dtResource)
                strTableName = Resource.GetTableNameByResourceID(ParentID)
            End If


            If ResInfo.IsUseParentShow Then
                Condtion = " and S.CS_RESID in (select id from cms_resource where RES_TABLE='" + strTableName + "' and res_type=0)"
            Else
                Condtion = " and S.CS_RESID =" + ResourceID
            End If

            sql = "SELECT E.CD_COLNAME Name,E.CD_TYPE as DataType,E.CD_SIZE as DataLength,ISNULL(E.CD_IS_NONULL,0) AS CD_IS_NONULL," _
                    & " E.CD_DISPNAME as Description,S.CS_SHOW_WIDTH AS WIDTH," _
                    & " isnull(E.cd_valtype,0) cd_valtype,S.CS_ALIGN," _
                    & " S.CS_FORMAT,S.CS_SHOW_ORDER  FROM cms_table_define E" _
                    & " join cms_table_show S on E.CD_COLNAME=S.CS_COLNAME  AND CS_SHOW_ENABLE=1 " _
                    & " join CMS_RESOURCE ON CMS_RESOURCE.ID=E.CD_RESID " _
                    & " where   CMS_RESOURCE.id in (select id from CMS_RESOURCE where  RES_TABLE='" + strTableName + "' and res_type=0)"

            sql = sql + Condtion

            sql = sql + " order by CS_SHOW_ORDER"
            'End If

            Dim mylist As Field()
            Dim Field As Field
            Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
            ReDim mylist(dt.Rows.Count - 1)
            For i As Integer = 0 To dt.Rows.Count - 1

                Field = New Field
                Field.Name = dt.Rows(i)("Name")
                Field.Description = dt.Rows(i)("Description")
                Field.IsRequired = dt.Rows(i)("CD_IS_NONULL")
                Select Case dt.Rows(i)("DataType")
                    Case 1
                        Field.DataType = "nvarchar"
                    Case 2
                        Field.DataType = "float"
                    Case 3
                        Field.DataType = "int"
                    Case 4
                        Field.DataType = "datetime"
                    Case 5
                        Field.DataType = "ntext"
                    Case 6
                        Field.DataType = "image"
                    Case 7
                        Field.DataType = "money"
                    Case 8
                        Field.DataType = "datetime"
                    Case 9
                        Field.DataType = "bit"
                    Case 10
                        Field.DataType = "ntext"
                End Select
                Field.DataLength = dt.Rows(i)("DataLength")
                Field.ValueType = dt.Rows(i)("cd_valtype")
                Field.Width = dt.Rows(i)("WIDTH")
                Field.Format = dt.Rows(i)("CS_FORMAT") & ""
                Select Case dt.Rows(i)("CS_ALIGN")
                    Case 0
                        Field.Align = HorizontalAlign.Left
                    Case 1
                        Field.Align = HorizontalAlign.Center
                    Case 2
                        Field.Align = HorizontalAlign.Right
                    Case Else
                        Field.Align = HorizontalAlign.NotSet
                End Select

                mylist(i) = Field
            Next

            Return mylist
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetFieldListAll(ByVal ResourceID As String, ByVal ResourceType As String) As Field()
        Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        If ResInfo.TableName <> "" Then
            Dim sql As String
            Dim Condtion As String = ""

            Dim strTableName As String = ResInfo.TableName

            If ResInfo.IsView Then
                Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
                dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
                Dim ParentID As Long = Resource.GetTopParentID(ResInfo.ParentID, dtResource)
                strTableName = Resource.GetTableNameByResourceID(ParentID.ToString)
            End If


            If ResInfo.IsUseParentShow Then
                Condtion = " and S.CS_RESID in (select id from cms_resource where RES_TABLE='" + strTableName + "' and res_type=0)"
            Else
                Condtion = " and S.CS_RESID =" + ResourceID
            End If

            sql = "SELECT E.CD_COLNAME Name,E.CD_TYPE as DataType,E.CD_SIZE as DataLength,ISNULL(E.CD_IS_NONULL,0) AS CD_IS_NONULL," _
                    & " E.CD_DISPNAME as Description,S.CS_SHOW_WIDTH AS WIDTH," _
                    & " isnull(E.cd_valtype,0) cd_valtype,S.CS_ALIGN," _
                    & " S.CS_FORMAT,S.CS_SHOW_ORDER  FROM cms_table_define E" _
                    & " join cms_table_show S on E.CD_COLNAME=S.CS_COLNAME   " _
                    & " join CMS_RESOURCE ON CMS_RESOURCE.ID=E.CD_RESID " _
                    & " where   CMS_RESOURCE.id in (select id from CMS_RESOURCE where  RES_TABLE='" + strTableName + "' and res_type=0)"

            If (ResourceType.ToUpper.Trim = "DOC") Then sql = sql + " and E.CD_COLNAME not in ('DOC2_COMMENTS','DOC2_COMPRESSED_RATE','DOC2_EDTID','DOC2_EDTTIME','DOC2_EXT','DOC2_KEYWORDS','DOC2_NAME','DOC2_SIZE','DOC2_STATUS')"

            sql = sql + Condtion

            sql = sql + " order by CS_SHOW_ORDER"
            'End If

            Dim mylist As Field()
            Dim Field As Field
            Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
            ReDim mylist(dt.Rows.Count - 1)
            For i As Integer = 0 To dt.Rows.Count - 1

                Field = New Field
                Field.Name = dt.Rows(i)("Name")
                Field.Description = dt.Rows(i)("Description")
                Field.IsRequired = dt.Rows(i)("CD_IS_NONULL")
                Select Case dt.Rows(i)("DataType")
                    Case 1
                        Field.DataType = "nvarchar"
                    Case 2
                        Field.DataType = "float"
                    Case 3
                        Field.DataType = "int"
                    Case 4
                        Field.DataType = "datetime"
                    Case 5
                        Field.DataType = "ntext"
                    Case 6
                        Field.DataType = "image"
                    Case 7
                        Field.DataType = "money"
                    Case 8
                        Field.DataType = "datetime"
                    Case 9
                        Field.DataType = "bit"
                    Case 10
                        Field.DataType = "ntext"
                End Select
                Field.DataLength = dt.Rows(i)("DataLength")
                Field.ValueType = dt.Rows(i)("cd_valtype")
                Field.Width = dt.Rows(i)("WIDTH")
                Field.Format = dt.Rows(i)("CS_FORMAT") & ""
                Select Case dt.Rows(i)("CS_ALIGN")
                    Case 0
                        Field.Align = HorizontalAlign.Left
                    Case 1
                        Field.Align = HorizontalAlign.Center
                    Case 2
                        Field.Align = HorizontalAlign.Right
                    Case Else
                        Field.Align = HorizontalAlign.NotSet
                End Select

                mylist(i) = Field
            Next

            Return mylist
        Else
            Return Nothing
        End If
    End Function


    Public Shared Function GetFieldListAll(ByVal ResourceID As String) As Field()
        Dim ResInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        If ResInfo.TableName <> "" Then
            Dim sql As String
            Dim Condtion As String = ""

            Dim strTableName As String = ResInfo.TableName

            If ResInfo.IsView Then
                Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
                dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
                Dim ParentID As Long = Resource.GetTopParentID(ResInfo.ParentID, dtResource)
                strTableName = Resource.GetTableNameByResourceID(ParentID.ToString)
            End If


            If ResInfo.IsUseParentShow Then
                Condtion = " and S.CS_RESID in (select id from cms_resource where RES_TABLE='" + strTableName + "' and res_type=0)"
            Else
                Condtion = " and S.CS_RESID =" + ResourceID
            End If

            sql = "SELECT E.CD_COLNAME Name,E.CD_TYPE as DataType,E.CD_SIZE as DataLength,ISNULL(E.CD_IS_NONULL,0) AS CD_IS_NONULL," _
                    & " E.CD_DISPNAME as Description,S.CS_SHOW_WIDTH AS WIDTH," _
                    & " isnull(E.cd_valtype,0) cd_valtype,S.CS_ALIGN," _
                    & " S.CS_FORMAT,S.CS_SHOW_ORDER  FROM cms_table_define E" _
                    & " join cms_table_show S on E.CD_COLNAME=S.CS_COLNAME   " _
                    & " join CMS_RESOURCE ON CMS_RESOURCE.ID=E.CD_RESID " _
                    & " where   CMS_RESOURCE.id in (select id from CMS_RESOURCE where  RES_TABLE='" + strTableName + "' and res_type=0)"

            sql = sql + Condtion

            sql = sql + " order by CS_SHOW_ORDER"
            'End If

            Dim mylist As Field()
            Dim Field As Field
            Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
            ReDim mylist(dt.Rows.Count - 1)
            For i As Integer = 0 To dt.Rows.Count - 1

                Field = New Field
                Field.Name = dt.Rows(i)("Name")
                Field.Description = dt.Rows(i)("Description")
                Field.IsRequired = dt.Rows(i)("CD_IS_NONULL")
                Select Case dt.Rows(i)("DataType")
                    Case 1
                        Field.DataType = "nvarchar"
                    Case 2
                        Field.DataType = "float"
                    Case 3
                        Field.DataType = "int"
                    Case 4
                        Field.DataType = "datetime"
                    Case 5
                        Field.DataType = "ntext"
                    Case 6
                        Field.DataType = "image"
                    Case 7
                        Field.DataType = "money"
                    Case 8
                        Field.DataType = "datetime"
                    Case 9
                        Field.DataType = "bit"
                    Case 10
                        Field.DataType = "ntext"
                End Select
                Field.DataLength = dt.Rows(i)("DataLength")
                Field.ValueType = dt.Rows(i)("cd_valtype")
                Field.Width = dt.Rows(i)("WIDTH")
                Field.Format = dt.Rows(i)("CS_FORMAT") & ""
                Select Case DbField.GetInt(dt.Rows(i), "CS_ALIGN")
                    Case 0
                        Field.Align = HorizontalAlign.Left
                    Case 1
                        Field.Align = HorizontalAlign.Center
                    Case 2
                        Field.Align = HorizontalAlign.Right
                    Case Else
                        Field.Align = HorizontalAlign.NotSet
                End Select

                mylist(i) = Field
            Next

            Return mylist
        Else
            Return Nothing
        End If
    End Function


    Public Shared Function Remove(ByVal TableName As String, ByVal ID As String) As Boolean
        Try


            SDbStatement.Execute("delete from " + TableName + " where ID=" + ID)
            Return True
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ResourcePath">资源路径以/分隔</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableByResourcePath(ByVal ResourcePath As String) As String
        Dim sql As String = "select Child.id,Child.res_table from cms_resource Child join cms_resource parent" _
            & " on Child.pid=parent.id where parent.name+'/'+Child.name='" + ResourcePath + "'"
        If SDbStatement.Query(sql).Tables(0).Rows.Count > 0 Then
            Return SDbStatement.Query(sql).Tables(0).Rows(0)("res_table")
        Else
            Return ""
        End If
    End Function
    'Public Shared Function GetDataList(ByVal ResourcePath As String, ByVal UserID As String, Optional ByVal _PageParameter As PageParameter = Nothing) As DataTable
    '    Dim TableName As String = GetTableByResourcePath(ResourcePath)


    '    If TableName <> "" Then
    '        Return Show(TableName, UserID, "")
    '    Else
    '        Return Nothing
    '    End If

    'End Function
    'Public Shared Function GetDataList(ByVal NodeID As Int64, ByVal UserID As String, Optional ByVal Condition As String = "") As DataTable
    '    Dim TableName As String = GetTableByResourcePath(NodeID)
    '    If TableName <> "" Then
    '        Return Show(TableName, UserID, Condition)
    '    Else
    '        Return Nothing
    '    End If

    'End Function

    Public Shared Function Show(ByVal TableName As String, ByVal UserID As String, Optional ByVal Condition As String = "") As DataTable
        Dim strSql As String = ""
        Dim strColumns As String = ""
        Dim RecourceID As Int64 = GetResourceidByTableName(TableName)

        Dim dtColumns As DataTable = Resource.GetColumnsByResouceID(RecourceID)
        For Each dr As DataRow In dtColumns.Rows
            strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
        Next

        strColumns = "ID,ResID,CRTID,CRTTIME,CRTID as 创建人ID,CRTTIME as 创建时间,(select EMP_NAME from CMS_EMPLOYEE where EMP_ID=CT.CRTID) as 创建人姓名" + strColumns
        strSql = "select " + strColumns + " from " + TableName + " CT where 1=1 " + Condition
        Return SDbStatement.Query(strSql.Trim).Tables(0)
    End Function


    Public Shared Function ShowChildTables(ByVal ResourceID As String, Optional ByVal Condition As String = "") As DataSet
        Dim Sql As String = Resource.GetSqlByResourceID(ResourceID, Condition, False)
        Dim strSql As String = Resource.GetSqlByResourceID(ResourceID, Condition)
        Dim FullSql As String = strSql + ";"
        Dim ChildSql As String = ""
        Dim ChildCondition As String = ""
        Dim drRelation As DataTable = Resource.GetRelationTable(ResourceID)
        For Each row As DataRow In drRelation.Rows
            ChildCondition = " and " + row("ChildColDispName") + " in (select [" + row("ParentColDispName") + "] from (" + Sql + ") T )"
            If row("ChildTableType").ToString.ToUpper = "DOC" Then
                ChildSql = Document.GetDocumentSqlByResourceID(row("ChildResourceID".ToString), ChildCondition)

            Else
                ChildSql = Resource.GetSqlByResourceID(row("ChildResourceID".ToString), ChildCondition)

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
    ''' 通过表名获取数据
    ''' </summary>
    ''' <param name="TableName">表名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Show(ByVal TableName As String) As DataTable
        Dim strSql As String = ""
        Dim strColumns As String = ""
        Dim RecourceID As Int64 = GetResourceidByTableName(TableName)
        Dim dtColumns As DataTable = Resource.GetColumnsByResouceID(RecourceID)
        strSql = GetSql(TableName, dtColumns)
        Return SDbStatement.Query(strSql.Trim).Tables(0)
    End Function

    Public Shared Function GetSql(ByVal tableName As String, ByVal dtColumns As DataTable) As String
        Dim strColumns As String = ""
        For Each dr As DataRow In dtColumns.Rows
            strColumns += "," + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
        Next
        strColumns = "ID,ResID,CRTID,CRTTIME,CRTID as 创建人ID,CRTTIME as 创建时间,(select EMP_NAME from CMS_EMPLOYEE where EMP_ID=CT.CRTID) as 创建人姓名" + strColumns
        Return "select " + strColumns + " from " + tableName + " CT where 1=1 "

    End Function


    Public Shared Function GetAllDirectoryTreeByResourceName(ByVal ResourceDescription As String) As DataTable
        Dim sql As String = "select id,pid as ParentID,Name from Cms_resource where Res_pid1=(select top 1 id from cms_resource where RES_COMMENTS='" + ResourceDescription + "')"
        Return SDbStatement.Query(sql).Tables(0)

    End Function

    Public Shared Function GetNextDirectoryTreeByResourceID(ByVal ResourceID As String) As List(Of ResourceInfo)
        Dim sql As String = "select * from Cms_resource order by show_order"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim drs() As DataRow = dt.Select("show_enable=1 and pid=" + ResourceID)
        Dim MyList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        Dim info As ResourceInfo
        For Each dr As DataRow In drs
            info = New ResourceInfo
            Resource.SetRowToResourceInfo(dr, info, dt)
            MyList.Add(info)
        Next
        Return MyList

    End Function

    Public Shared Function GetNextAllDirectoryTreeByResourceID(ByVal ResourceID As String, ByVal IsEnable As Boolean, Optional ByVal IsShowDefault As Boolean = True) As DataTable
        Dim sql As String = "select * from Cms_resource order by show_order"
        Dim dtResource As DataTable = SDbStatement.Query(sql).Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        Dim strChildResouceID As String = ResourceID + Resource.GetAllChildResourceID(ResourceID, dtResource)

        Dim strWhere As String = ""
        If strChildResouceID.Trim() <> "" Then strWhere = " and ID in (" + strChildResouceID + ")"
        If IsShowDefault = False Then strWhere += " and IsNull(RES_IsDefaultMenu,0)=0"

        If IsEnable Then strWhere += " and IsNull(show_enable,0) = 1"


        Dim dt As DataTable = SDbStatement.Query("select * from Cms_resource where PID!=-1 " + strWhere + " order by show_order").Tables(0)
        dt.PrimaryKey = New DataColumn() {dt.Columns("ID")}
        Dim dtNew As New DataTable
        dtNew = dt.Copy
        If IsShowDefault = False And ResourceID.Trim <> "" Then
            For Each dr As DataRow In dt.Rows
                If DbField.GetLng(dr, "ID") <> ResourceID Then
                    Dim PID As Int64 = DbField.GetLng(dr, "PID")
                    LoadParentNode(dtResource, dtNew, PID, ResourceID, True)
                End If
            Next
        End If

        Return dtNew
    End Function

    ''' <summary>
    ''' 返回此资源下此用户可见的资源列表
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="ResourceID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNextDirectoryTreeByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
        Dim sql As String '= "select * from Cms_resource where show_enable=1 and pid=" + ResourceID + " order by show_order"

        Dim CommonWhere As String = " and QX_OBJECT_ID=Cms_resource.ID and QX_NAME='0'"
        Dim strWhere As String = "(QX_GAINER_ID='" & UserID & "'" + CommonWhere + ")" '个人权限
        strWhere = strWhere + " or (QX_OBJECT_ID='0'" + CommonWhere + ")"  '企业权限
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select HOST_ID from dbo.CMS_EMPLOYEE where emp_id='" + UserID + "'))"  '部门权限
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select vdep_depid from dbo.CMS_DEP_VIRTUAL where vdep_empid='" + UserID + "'))"  '角色权限

        strWhere = " and exists (select 1 from CMS_RIGHTS where " + strWhere + ")"
        sql = "select * from Cms_resource where show_enable=1 and pid=" + ResourceID + strWhere + " order by show_order"

        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim MyList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        Dim info As ResourceInfo
        For Each dr As DataRow In dt.Rows
            info = New ResourceInfo
            Resource.SetRowToResourceInfo(dr, info, dt)
            MyList.Add(info)
        Next
        Return MyList

    End Function

    Public Shared Function GetNextPortalTreeRootByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
        'Dim strWhere As String = ""
        'Dim strDeptID As String = Users.GetAllParentDepartmentIDByUserID(UserID)
        '' Dim CommonWhere As String = " and Resourceid=Cms_resource.ID  and IsNull(IsEnabled,0)=1"
        'strWhere = "GainerObjectID in ('" + UserID + "','" + strDeptID.Replace(",", "','") + "') and IsNull(IsEnabled,0)=1  and Resourceid=Cms_resource.ID "
        'strWhere = " and (exists (select 1 from SystemRights where " + strWhere + ") or IsNull(RES_IsDefaultMenu,0)=1 )"
        'Dim strSql As String = "select * from Cms_resource where show_enable=1  and PID=" + ResourceID + strWhere + " order by show_order;"
        'Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)

        Dim dt As DataTable = GetNextPortalAllTreeByResourceIDAndUserID(UserID, ResourceID, False)
        Dim drs() As DataRow = dt.Select("PID=" + ResourceID)
        Dim MyList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        Dim info As ResourceInfo
        For Each dr As DataRow In drs
            info = New ResourceInfo
            Resource.SetRowToResourceInfo(dr, info, dt)
            MyList.Add(info)
        Next
        Return MyList
    End Function


    Public Shared Function GetNextPortalTreeByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String) As List(Of ResourceInfo)
        Dim strWhere As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        Dim strChildResourceID As String = Resource.GetAllChildResourceID(ResourceID, dtResource)
        Dim strDeptID As String = Users.GetAllParentDepartmentIDByUserID(UserID)
        ' Dim CommonWhere As String = " and Resourceid=Cms_resource.ID  and IsNull(IsEnabled,0)=1"
        strWhere = "GainerObjectID in ('" + UserID + "','" + strDeptID.Replace(",", "','") + "') and IsNull(IsEnabled,0)=1  and Resourceid=Cms_resource.ID "
        strWhere = " and (exists (select 1 from SystemRights where " + strWhere + ") or IsNull(RES_IsDefaultMenu,0)=1 )"

        Dim strSql As String = "select * from Cms_resource where (show_enable=1  and ID in (select ID from cms_resource where PID=" + ResourceID + ")" + strWhere + ") order by show_order;"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)

        Dim MyList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        Dim info As ResourceInfo
        For Each dr As DataRow In dt.Rows
            info = New ResourceInfo
            Resource.SetRowToResourceInfo(dr, info, dt)
            MyList.Add(info)
        Next
        Return MyList
    End Function

    Public Shared Function GetNextPortalAllTreeByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String, Optional ByVal IsClearUrl As Boolean = True) As DataTable
        Dim strWhere As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        Dim strChildResourceID As String = ResourceID + Resource.GetAllChildResourceID(ResourceID, dtResource)

        Dim strDeptID As String = Users.GetAllParentDepartmentIDByUserID(UserID)
        ' Dim CommonWhere As String = " and Resourceid=Cms_resource.ID  and IsNull(IsEnabled,0)=1"
        strWhere = "GainerObjectID in ('" + UserID + "','" + strDeptID.Replace(",", "','") + "') and IsNull(IsEnabled,0)=1  and Resourceid=Cms_resource.ID "
        strWhere = " and (exists (select 1 from (select * from Sys_GetRights union select * from  Sys_GetChildRights)Tab where " + strWhere + ") or IsNull(RES_IsDefaultMenu,0)=1 )"

        Dim strSql As String = "select * from Cms_resource where (show_enable=1  and ID in(" + strChildResourceID + ")" + strWhere + ") order by show_order;"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)

        dt.PrimaryKey = New DataColumn() {dt.Columns("ID")}
        Dim MyList As List(Of ResourceInfo) = New List(Of ResourceInfo)
        'Dim info As ResourceInfo
        Dim dtNew As New DataTable
        dtNew = dt.Copy

        For Each dr As DataRow In dt.Rows
            If DbField.GetLng(dr, "ID") <> ResourceID Then
                Dim PID As Int64 = DbField.GetLng(dr, "PID")
                LoadParentNode(dtResource, dtNew, PID, ResourceID, IsClearUrl)
            End If
        Next
        Dim dataView As DataView = dtNew.DefaultView
        dataView.Sort = "show_order asc"
        Return dataView.ToTable
    End Function


    Private Shared Function LoadParentNode(ByVal dtResource As DataTable, ByRef dt As DataTable, ByVal ResourceID As String, ByVal ParentResourceID As String, ByVal IsClearUrl As Boolean) As DataTable
        If Convert.ToInt64(ResourceID) = 0 Then Return dt
        Dim dr As DataRow = dt.Rows.Find(ResourceID)
        If dr Is Nothing Then
            Dim drNew As DataRow = dtResource.Rows.Find(ResourceID)
            If DbField.GetBool(drNew, "show_enable") Then
                If IsClearUrl Then
                    drNew("RES_EMPTYRESOURCEURL") = ""
                    drNew("RES_EMPTYRESOURCETARGET") = ""
                End If
                dt.Rows.Add(drNew.ItemArray)
                If Convert.ToInt64(ResourceID) = Convert.ToInt64(ParentResourceID) Then Return dt
                LoadParentNode(dtResource, dt, DbField.GetStr(drNew, "PID"), ParentResourceID, IsClearUrl)
            End If
        Else
            LoadParentNode(dtResource, dt, DbField.GetStr(dr, "PID"), ParentResourceID, IsClearUrl)
        End If
        Return dt
    End Function


    Public Shared Function GetAllPortalOperationByResourceIDAndUserID(ByVal UserID As String, ByVal ResourceID As String, Optional ByVal IsGetChildResource As Boolean = False) As DataTable
        Dim strWhere As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        Dim strChildResourceID As String = ""
        If IsGetChildResource Then strChildResourceID = Resource.GetAllChildResourceID(ResourceID, dtResource)
        Dim strDeptID As String = Users.GetAllParentDepartmentIDByUserID(UserID)


        Dim strSql As String = " select  R.ID,R.name, IsNull(S.ChildModuleCode,'') ChildModuleCode,  S.RightsValue,IsNull(S.IsEnabled,0) IsEnabled  from "
        strSql += " (select * from Cms_resource where show_enable=1  and ID in (" + ResourceID + strChildResourceID.Trim + ") ) R "
        strSql += " join ( select  * from SystemRights where GainerObjectID in ('" + UserID + "','" + strDeptID.Replace(",", "','") + "') and IsNull(IsEnabled,0)=1 and RightsValue not in (select OpNum RightsValue from Sys_ColOperateSettings where (OperateType='操作' or OperateType='流程操作') and IsNull(IsEnabled,0)=0  union  select OpNum RightsValue from UserDefinedToolBar where   IsNull(IsEnabled,0)=0) ) S on S.ResourceID=R.ID"
        strSql += "  union "


        strSql += " select Table1.ID,Table1.Name,Table1.ChildModuleCode,T1.RightsValue,1 IsEnabled from( select * from ( select ID,PID,Name,IsNull(Res_Comments,'') KeyWord,'' parentKeyWord,'' ChildModuleCode,RES_IsDefaultMenu  from Cms_resource"
        strSql += " union select R.ID,R.PID,R.Name,IsNull(M.ChildKeyWord,'') KeyWord,IsNull(R.Res_Comments,'') parentKeyWord,IsNull(M.ChildNum,'') ChildModuleCode,RES_IsDefaultMenu  from Cms_resource R join MasterTableAssociation M on R.Res_Comments=M.MasterKeyWord)T where ID=" + ResourceID + " and  IsNull(RES_IsDefaultMenu,0)=1) Table1"
        strSql += " join (select LinkCol ToolName, ENKeyWord KeyWord,OpNum RightsValue from Sys_ColOperateSettings where (OperateType='操作' or OperateType='流程操作') and IsNull(IsEnabled,0)=1 "
        strSql += " union select ToolName,KeyWordValue KeyWord,OpNum RightsValue from UserDefinedToolBar where   IsNull(IsEnabled,0)=1"
        strSql += " union select '预览' ToolName, Res_Comments ENKeyWord,'1' RightsValue from CMS_RESOURCE   where  IsNull(RES_IsDefaultMenu,0)=1 "
        strSql += " union select '预览' ToolName, M.ChildKeyWord ENKeyWord,'1' RightsValue from CMS_RESOURCE  R join MasterTableAssociation M on R.Res_Comments=M.MasterKeyWord where  IsNull(RES_IsDefaultMenu,0)=1 "
        strSql += " union select '添加' ToolName,  ENKeyWord,'2' RightsValue from SysSettings where IsNull(IsAdd,0)=1 and IsNull(AddUrl,'')<>''"
        strSql += " union select '修改' ToolName,  ENKeyWord,'3' RightsValue from SysSettings where IsNull(IsUpdate,0)=1 and IsNull(EidtUrl,'')<>''"
        strSql += " union select '删除' ToolName,  ENKeyWord,'4' RightsValue from SysSettings where IsNull(IsDelete,0)=1"
        strSql += " union select '导出' ToolName,  ENKeyWord,'6' RightsValue from SysSettings where IsNull(IsExp,0)=1) T1 on Table1.KeyWord=T1.KeyWord "


        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)

        Return dt
    End Function

    Public Shared Function GetPortalChildResourceList(ByVal UserID As String, ByVal KeyWord As String) As DataTable
        Dim strDeptID As String = Users.GetAllParentDepartmentIDByUserID(UserID)
        Dim strSql As String = "  select ChildNum 编号, ShowTitle as [显示Title],MasterKeyWord [主表关键字],ChildKeyWord as [子表关键字], RSResID [主表资源ID],ChildResId as [子表资源ID],LedgerConditions [台帐主子表关联字段],LedgerChildKey [台帐子表引用字段],InitialQueryStr [初始筛选条件] ,IsNull(ChildOrderNo,0)ChildOrderNo from MasterTableAssociation M  " _
          & " join (select ChildModuleCode,Min(RightsValue) RightsValue  from SystemRights where  GainerObjectID in ('" + UserID + "','" + strDeptID.Replace(",", "','") + "') and IsNull(IsEnabled,0)=1  group by ChildModuleCode) R on M.ChildNum=R.ChildModuleCode " _
          & "where MasterKeyWord='" + KeyWord + "' and IsNull(IsEnabled,0)=1 order by ChildOrderNo"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        Return dt
    End Function


    Public Shared Function GetAllPortalChildResourceList(ByVal KeyWord As String) As DataTable
        Dim strSql As String = "select ChildNum 编号, ShowTitle as [显示Title],MasterKeyWord [主表关键字],ChildKeyWord as [子表关键字], RSResID [主表资源ID],ChildResId as [子表资源ID],LedgerConditions [台帐主子表关联字段],LedgerChildKey [台帐子表引用字段],InitialQueryStr [初始筛选条件] ,IsNull(ChildOrderNo,0)ChildOrderNo from MasterTableAssociation M  where MasterKeyWord='" + KeyWord + "' and IsNull(IsEnabled,0)=1 order by ChildOrderNo"
        Return SDbStatement.Query(strSql).Tables(0)
    End Function



    Public Shared Function ShowDirectoryTree(ByVal TableName As String) As DataTable
        Dim RecourceID As Int64 = GetResourceidByTableName(TableName)
        Dim sql As String = "select id,pid as ParentID,Name from Cms_resource where Res_pid1=" + RecourceID.ToString + " and show_enable=1"
        Return SDbStatement.Query(sql).Tables(0)
    End Function

    Public Shared Function GetResourceidByTableName(ByVal TableName As String) As Int64

        Dim sql As String = "select id from cms_resource where res_table='" + TableName + "' and res_type=0"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return -1
        End If

    End Function

    Public Shared Function GetDefineColumnsByResouceID(ByVal Resourceid As String) As DataTable

        Dim sql As String = "select  D.CD_COLNAME,D.CD_DISPNAME from CMS_TABLE_DEFINE D" _
              & " where CD_RESID=" + Resourceid
        Return SDbStatement.Query(sql).Tables(0)

    End Function


    Public Shared Function GetDataList(ByVal _Description As String, ByVal IsGetChildren As Boolean, Optional ByVal Condition As String = "") As DataSet

        Dim ResourceID As String = GetResourceIDByDescription(_Description)
        If ResourceID = "" Then
            Return Nothing
        End If
        Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        If info.TableType.ToLower = "doc" Then
            Return SDbStatement.Query(Document.GetDocumentSqlByResourceID(ResourceID, Condition))

        Else

            If IsGetChildren = False Then
                Return SDbStatement.Query(Resource.GetSqlByResourceID(ResourceID, Condition))

            Else

                Dim TableName As String = info.TableName
                Dim ParentResourceID As String = Resource.GetParentByResourceID(TableName)
                If info.Type = 1 Then
                    Dim AllChildList As String = Resource.GetAllChildList(ResourceID)
                    Return ShowChildTables(ParentResourceID, Condition + " and resid in (" + AllChildList + ")")
                Else
                    Return ShowChildTables(ParentResourceID, Condition)
                End If

            End If
        End If
    End Function


    Public Shared Function GetDataListByParentRecID(ByVal ParentResID As String, ByVal ResID As String, ByVal ParentRecID As String) As DataTable
        '  Dim strSql As String ="select * from  MasterTableAssociation where ";

        Dim dt As DataTable = Resource.GetRelationTable(ParentResID)
        Dim dr() As DataRow = dt.Select("ChildResourceID=" + ResID)
        If dr.Length > 0 Then
            Dim strWhere As String = " and " + dr(0)("ChildColDispName").ToString & " in (select " + dr(0)("ParentColName").ToString + " from " + dr(0)("ParentTableName").ToString + " where id=" + ParentRecID + ")"
            Return SDbStatement.Query(Resource.GetSqlByResourceID(ResID, strWhere)).Tables(0)
        End If
        Return Nothing
    End Function

    Public Shared Function GetDataListByResourceID(ByVal ResourceID As String, ByVal IsGetChildren As Boolean, Optional ByVal Condition As String = "", Optional ByVal UserID As String = "") As DataSet
        Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
        If info.TableType.ToLower = "doc" Then
            Return SDbStatement.Query(Document.GetDocumentSqlByResourceInfo(info, UserID, Condition))
        Else
            If IsGetChildren = False Then
                If UserID = "" Then
                    Return SDbStatement.Query(Resource.GetSqlByResourceID(ResourceID, Condition))
                Else
                    Return SDbStatement.Query(Resource.GetSqlByResourceID(ResourceID, UserID, Condition))
                End If
            Else
                Dim TableName As String = info.TableName
                If info.IsView Then
                    Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
                    dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
                    Dim ParentID As Long = Resource.GetTopParentID(info.ParentID, dtResource)
                    TableName = Resource.GetTableNameByResourceID(ParentID.ToString)
                    Return ShowChildTables(ResourceID, Condition)
                End If
                Dim ParentResourceID As String = Resource.GetParentByResourceID(TableName)
                If info.Type = 1 Then '
                    Dim AllChildList As String = Resource.GetAllChildList(ResourceID)
                    Return ShowChildTables(ParentResourceID, Condition + " and resid in (" + AllChildList + ")")
                Else
                    Return ShowChildTables(ResourceID, Condition)
                End If
            End If
        End If
    End Function


    Public Shared Function GetDataListByResID(ByVal ResourceID As String, ByVal OrderBy As String, Optional ByVal Condition As String = "", Optional ByVal UserID As String = "") As DataSet
        Dim sql As String = ""
        Try
            Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)
            If info.TableType.ToLower = "doc" Then
                Return SDbStatement.Query(Document.GetDocumentSqlByResourceID(ResourceID, Condition))
            Else

                If UserID = "" Then
                    sql = Resource.GetSqlByResourceID(ResourceID, Condition)
                Else
                    sql = Resource.GetSqlByResourceID(ResourceID, UserID, Condition)
                End If

                If OrderBy <> "" Then
                    sql += " order by " + OrderBy
                End If

                Return SDbStatement.Query(sql)
            End If
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return Nothing
        End Try

    End Function

    Public Shared Function GetDataColumnListByResID(ByVal ResourceID As String, ByVal ColumnStr As String, ByVal OrderBy As String, Optional ByVal Condition As String = "", Optional ByVal UserID As String = "") As DataSet
        Dim sql As String = ""
        Try
            sql = Resource.GetSqlColumnByResourceID(ResourceID, ColumnStr, Condition)

            If OrderBy <> "" Then
                sql += " order by " + OrderBy
            End If

            Return SDbStatement.Query(sql)
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return Nothing
        End Try

    End Function

    Public Shared Function GetDataListRecordCount(ByVal Sql As String) As Int64
        If Sql <> "" Then

            Sql = "select count(*) from (" + Sql + ") T"
            Return SDbStatement.Query(Sql).Tables(0).Rows(0)(0)

        Else
            Return 0
        End If

    End Function

    Public Shared Function GetDataListRecordCount(ByVal Description As String, ByVal Condition As String) As Int64

        Dim ResourceID As String = GetResourceIDByDescription(Description)
        If ResourceID <> "" Then

            Dim sql As String = Resource.GetSqlByResourceID(ResourceID, Condition, False)
            sql = "select count(*) from (" + sql + ") T"
            Return SDbStatement.Query(sql).Tables(0).Rows(0)(0)

        Else
            Return 0
        End If

    End Function

    Public Shared Function GetDataListRecordCount(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String) As Int64

        Dim sql As String
        Try
            Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID.ToString)
            If ResourceInfo IsNot Nothing Then

                If ResourceInfo.TableType = "TWOD" Or ResourceInfo.TableType = "EMP" Then
                    sql = Resource.GetSqlByResourceInfo(ResourceInfo, UserID, Condition, False)
                Else
                    'sql = Document.GetDocumentSqlByResourceID(ResourceID.ToString, Condition)
                    sql = Document.GetDocumentSqlByResourceInfo(ResourceInfo, UserID, Condition, False)
                End If


                sql = "select count(*) from (" + sql + ") T"

                Return SDbStatement.Query(sql).Tables(0).Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
            SLog.Err(ex.Message & sql)
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="IsRowRights">是否使用行记录权限中的条件过虑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataListRecordCount(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal IsRowRights As Boolean) As Int64
        Dim sql As String = ""
        Try
            Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID.ToString)
            If ResourceInfo IsNot Nothing Then


                If ResourceInfo.TableType = "TWOD" Then
                    Sql = Resource.GetSqlByResourceInfo(IsRowRights, ResourceInfo, UserID, Condition, False)
                Else
                    Sql = Document.GetDocumentSqlByResourceID(ResourceID.ToString, Condition)
                End If


                Sql = "select count(*) from (" + Sql + ") T"

                Return SDbStatement.Query(Sql).Tables(0).Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return 0
        End Try

    End Function


    Public Shared Function GetDataListRecordCount(ByVal Description As String, ByVal UserID As String, ByVal Condition As String) As Int64
        Dim sql As String = ""
        Try
            Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByDescription(Description)
            If ResourceInfo IsNot Nothing Then
                sql = Resource.GetSqlByResourceInfo(ResourceInfo, UserID, Condition, False)
                sql = "select count(*) from (" + sql + ") T"

                Return SDbStatement.Query(sql).Tables(0).Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
            SLog.Err(ex.Message & sql)
        End Try
    End Function

    ''' <summary>
    ''' sql语句和分页对象获取数据列表
    ''' </summary>
    ''' <param name="Sql">Sql语句</param>
    ''' <param name="PageParameter">分页参数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataListPage(ByVal Sql As String, ByVal PageParameter As PageParameter) As DataSet

        If Sql <> "" Then
            Dim sqlStr As String
            Dim OrderBy As String = PageParameter.SortField + " " + PageParameter.SortBy
            sqlStr = " select top " + PageParameter.PageSize.ToString _
                & " * from (" + Sql + ") T where id not in (select top " + (PageParameter.PageIndex * PageParameter.PageSize).ToString _
                & " id from (" + Sql + ") W order by " + OrderBy + ") order by " + OrderBy

            Return SDbStatement.Query(sqlStr)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function GetDataListPage(ByVal Description As String, ByVal Condition As String, ByVal PageParameter As PageParameter) As DataSet
        Dim sql As String = ""
        Try
            Dim ResourceID As String = GetResourceIDByDescription(Description)
            If ResourceID <> "" Then
                sql = Resource.GetSqlByResourceID(ResourceID, Condition)
                If PageParameter Is Nothing Then
                    Return SDbStatement.Query(Resource.GetSqlByResourceID(ResourceID, Condition))

                Else
                    Dim OrderBy As String = PageParameter.SortField + " " + PageParameter.SortBy
                    sql = " select top " + PageParameter.PageSize.ToString _
                    & " * from (" + sql + ") T where id not in (select top " + (PageParameter.PageIndex * PageParameter.PageSize).ToString _
                    & " id from (" + sql + ") W order by " + OrderBy + ") order by " + OrderBy

                    Return SDbStatement.Query(sql)
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return Nothing
        End Try





    End Function
    ''' <summary>
    ''' 根据用户与资源信息获取分页,此方法用于继承关系
    ''' </summary>
    ''' <param name="Description"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="PageParameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataListPage(ByVal Description As String, ByVal UserID As String, ByVal Condition As String, ByVal PageParameter As PageParameter) As DataSet
        Dim sql As String = ""
        Try
            Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByDescription(Description)

            If ResourceInfo IsNot Nothing Then
                sql = Resource.GetSqlByResourceInfo(ResourceInfo, UserID, Condition, False)
                If PageParameter Is Nothing Then
                    Return SDbStatement.Query(sql)

                Else
                    Dim OrderBy As String = PageParameter.SortField + " " + PageParameter.SortBy
                    sql = " select top " + PageParameter.PageSize.ToString _
                    & " * from (" + sql + ") T where id not in (select top " + (PageParameter.PageIndex * PageParameter.PageSize).ToString _
                    & " id from (" + sql + ") W order by " + OrderBy + ") order by " + OrderBy

                    Return SDbStatement.Query(sql)
                End If


            Else
                Return Nothing
            End If
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' 通过资源ID返回所有继承型的列表
    ''' </summary>
    ''' <param name="ResourceID">资源ID</param>
    ''' <param name="UserID">用户ID</param>
    ''' <param name="Condition">条件</param>
    ''' <param name="PageParameter">分页参数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function GetDataListPage(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal PageParameter As PageParameter) As DataSet
        Dim sql As String = ""
        Try
            Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID.ToString)
            If ResourceInfo IsNot Nothing Then
                If ResourceInfo.TableType = "TWOD" Or ResourceInfo.TableType = "EMP" Then
                    sql = Resource.GetSqlByResourceInfo(ResourceInfo, UserID, Condition, False)
                Else
                    'sql = Document.GetDocumentSqlByResourceID(ResourceID.ToString, Condition)
                    sql = Document.GetDocumentSqlByResourceInfo(ResourceInfo, UserID, Condition, False)
                End If

                If PageParameter Is Nothing Then
                    Return SDbStatement.Query(sql)

                Else
                    Dim OrderBy As String = PageParameter.SortField + " " + PageParameter.SortBy
                    If ResourceInfo.ResOrder.Trim <> "" Then
                        OrderBy = ResourceInfo.ResOrder.Trim
                    End If
                    sql = " select top " + PageParameter.PageSize.ToString _
                    & " * from (" + sql + ") T where id not in (select top " + (PageParameter.PageIndex * PageParameter.PageSize).ToString _
                    & " id from (" + sql + ") W order by " + OrderBy + ") order by " + OrderBy

                    Return SDbStatement.Query(sql)
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' 分页查询（可使用行权限中的过虑条件）
    ''' </summary>
    ''' <param name="ResourceID"></param>
    ''' <param name="IsRowRights">是否使用行权限中过虑条件</param>
    ''' <param name="UserID"></param>
    ''' <param name="Condition"></param>
    ''' <param name="PageParameter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataListPage(ByVal ResourceID As Int64, ByVal IsRowRights As Boolean, ByVal UserID As String, ByVal Condition As String, ByVal PageParameter As PageParameter) As DataSet
        Dim sql As String = ""
        Try
            Dim ResourceInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID.ToString)

            If ResourceInfo IsNot Nothing Then

                If ResourceInfo.TableType = "TWOD" Then
                    sql = Resource.GetSqlByResourceInfo(IsRowRights, ResourceInfo, UserID, Condition, False)
                Else
                    sql = Document.GetDocumentSqlByResourceID(ResourceID.ToString, Condition)
                End If

                If PageParameter Is Nothing Then
                    Return SDbStatement.Query(sql)

                Else
                    Dim OrderBy As String = PageParameter.SortField + " " + PageParameter.SortBy
                    If ResourceInfo.ResOrder.Trim <> "" Then
                        OrderBy = ResourceInfo.ResOrder.Trim
                    End If


                    sql = " select top " + PageParameter.PageSize.ToString _
                    & " * from (" + sql + ") T where id not in (select top " + (PageParameter.PageIndex * PageParameter.PageSize).ToString _
                    & " id from (" + sql + ") W order by " + OrderBy + ") order by " + OrderBy

                    Return SDbStatement.Query(sql)
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            SLog.Err(ex.Message & sql)
            Return Nothing
        End Try

    End Function


    Public Shared Function GetDataListPage(ByVal ResourceID As Int64, ByVal UserID As String, ByVal Condition As String, ByVal PageParameter As PageParameter, ByVal IsShowChild As Boolean) As DataSet
        If Not IsShowChild Then
            Return GetDataListPage(ResourceID, UserID, Condition, PageParameter)
        Else
            Dim ds As DataSet = GetDataListPage(ResourceID, UserID, Condition, PageParameter)

            '  Dim Sql As String = Resource.GetSqlByResourceID(ResourceID, UserID, Condition)
            Dim FullSql As String = ""
            Dim ChildSql As String = ""
            Dim ChildCondition As String = ""
            Dim drRelation As DataTable = Resource.GetRelationTable(ResourceID)
            For Each row As DataRow In drRelation.Rows

                Dim Keys As String = ""
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Keys = Keys + ",'" + ds.Tables(0).Rows(i)(row("ParentColDispName")).ToString + "'"
                Next
                If Keys <> "" Then
                    Keys = Keys.Remove(0, 1)
                    ChildCondition = " and " + row("ChildColDispName") + " in (" + Keys + ")"
                Else
                    ChildCondition = " and 1=0"
                End If
                '  ChildCondition = " and " + row("ChildColDispName") + " in (select [" + row("ParentColDispName") + "] from (" + Sql + ") T )"
                If row("ChildTableType").ToString.ToUpper = "DOC" Then
                    ChildSql = Document.GetDocumentSqlByResourceID(row("ChildResourceID".ToString), ChildCondition)

                Else
                    ChildSql = Resource.GetSqlByResourceID(row("ChildResourceID".ToString), UserID, ChildCondition, False)

                End If

                FullSql = FullSql + ChildSql + ";"

            Next
            Dim dsTemp As DataSet = New DataSet
            dsTemp = SDbStatement.Query(FullSql)
            For j As Integer = 0 To dsTemp.Tables.Count - 1
                dsTemp.Tables(j).TableName = drRelation.Rows(j)("ChildTableDispName")
                ds.Tables.Add(dsTemp.Tables(j).Copy)
            Next

            For i As Integer = 0 To drRelation.Rows.Count - 1
                ds.Relations.Add(i.ToString, ds.Tables(0).Columns(drRelation.Rows(i)("ParentColDispName")), ds.Tables(i + 1).Columns(drRelation.Rows(i)("ChildColDispName")))
                'ds.Tables(i + 1).TableName = drRelation.Rows(i)("ChildTableDispName")

            Next
            Return ds
        End If
    End Function




    Public Shared Function GetTableNameByDescription(ByVal _Description As String) As String
        Dim sql As String = "select res_table from cms_resource where res_comments='" + _Description + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return ""
        End If
    End Function
    Public Shared Function GetResourceIDByDescription(ByVal Description As String) As String
        Dim sql As String = "select ID from cms_resource where res_comments='" + Description + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0).ToString
        Else
            Return ""
        End If
    End Function


    'Public Shared Function GetResourceIDByTableName(ByVal TableName As String) As String
    '    Dim sql As String = "select ID from cms_resource where RES_TABLE='" + TableName + "'"
    '    Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
    '    If dt.Rows.Count > 0 Then
    '        Return dt.Rows(0)(0).ToString
    '    Else
    '        Return ""
    '    End If
    'End Function



    Public Shared Function GetTableRow(ByVal TableName As String, ByVal userid As String, ByVal id As String) As DataRow
        Dim dt As DataTable = Show(TableName, userid, " and id=" + id)
        If dt.Rows.Count > 0 Then

            Return dt.Rows(0)

        Else

            Return Nothing
        End If
    End Function
    

    Public Shared Function GetServerPath() As String

        Dim FilePath As String
        Dim server As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Name")
        Dim port As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Port")
        If port = "80" Then
            FilePath = "http://" + server + System.Web.HttpContext.Current.Request.ApplicationPath
        Else
            FilePath = "http://" + server + ":" + port + System.Web.HttpContext.Current.Request.ApplicationPath
        End If
        Return FilePath
    End Function
    Public Shared Function GetServerHeader() As String
        Dim FilePath As String
        Dim server As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Name")
        Dim port As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Port")
        If port = "80" Then
            FilePath = server
        Else
            FilePath = server + ":" + port
        End If
        Return FilePath
    End Function



    Public Shared Function GetAllRoles() As List(Of RoleInfo)
        Dim sql As String = "select * from dbo.CMS_DEPARTMENT"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim myList As List(Of RoleInfo) = New List(Of RoleInfo)
        Dim RoleInfo As RoleInfo
        Dim FullName As String = ""
        For Each row As DataRow In dt.Select("dep_type=1")
            RoleInfo = New RoleInfo
            RoleInfo.id = row("id").ToString
            RoleInfo.Name = row("Name")
            FullName = Resource.GetDeptName(row("pid"), row("Name"), dt)

            RoleInfo.FullName = FullName
            myList.Add(RoleInfo)
        Next
        Return myList

    End Function

    Public Shared Function GetRolesByUserID(ByVal UserID As String) As List(Of RoleInfo)
        Dim sql As String = "select distinct vdep_depid as deptID from CMS_DEP_VIRTUAL where VDEP_EMPID='" + UserID + "'"
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        dt.PrimaryKey = New DataColumn() {dt.Columns("deptID")}
        Dim AllRolesList As List(Of RoleInfo) = GetAllRoles()
        Dim myList As List(Of RoleInfo) = New List(Of RoleInfo)
        Dim FullName As String = ""
        For Each RoleInfo As RoleInfo In AllRolesList
            If dt.Rows.Find(RoleInfo.id) IsNot Nothing Then
                myList.Add(RoleInfo)
            End If

        Next
        Return myList

    End Function





    Public Shared Function UploadFile(ByVal ResourceID As String, ByVal UserID As String, ByVal FileName As String, ByVal binFile As Byte(), ByVal FieldInfoList As FieldInfo()) As Boolean

        Dim Ext As String = ""
        Dim i As Integer = FileName.LastIndexOf(".")
        If i > 0 Then
            Ext = FileName.Substring(i + 1, FileName.Length - i - 1)
        End If

        Dim id As String = NetReusables.TimeId.CurrentMilliseconds(30).ToString
        Dim strFilePath As String = "\UploadFile\" + ResourceID '+ "\" + lngDocID.ToString + "." + strFileExt.Trim 'strDocFolder & strFileName
        Dim CmsPhysicalPath As String = System.Configuration.ConfigurationManager.ConnectionStrings("cmsPath").ConnectionString

        If Not System.IO.Directory.Exists((CmsPhysicalPath + strFilePath.Trim + "\").ToString) Then System.IO.Directory.CreateDirectory(CmsPhysicalPath + strFilePath.Trim)
        strFilePath += "\" + id + "." + Ext

        Try
            Dim fs As FileStream = New FileStream(CmsPhysicalPath + strFilePath, FileMode.Create, FileAccess.Write)
            fs.Write(binFile, 0, binFile.Length)
            fs.Flush()
            fs.Close()

            InsertFileToDB(strFilePath, UserID, id, ResourceID, FileName, binFile.Length, FieldInfoList)
            Return True
        Catch ex As Exception

            Return False

        End Try
    End Function

    Public Shared Function UploadFile(ByVal ResourceID As String, ByVal UserID As String, ByVal FileName As String, ByVal binFile As Byte(), ByVal xmlFieldAndValueList As String) As String

        Dim Ext As String = ""
        Dim i As Integer = FileName.LastIndexOf(".")
        If i > 0 Then
            Ext = FileName.Substring(i + 1, FileName.Length - i - 1)
        End If



        Dim id As String = NetReusables.TimeId.CurrentMilliseconds(30).ToString
        Dim strFilePath As String = "\UploadFile\" + ResourceID '+ "\" + lngDocID.ToString + "." + strFileExt.Trim 'strDocFolder & strFileName
        Dim CmsPhysicalPath As String = System.Configuration.ConfigurationManager.ConnectionStrings("cmsPath").ConnectionString

        If Not System.IO.Directory.Exists((CmsPhysicalPath + strFilePath.Trim + "\").ToString) Then System.IO.Directory.CreateDirectory(CmsPhysicalPath + strFilePath.Trim)
        strFilePath += "\" + id + "." + Ext


        Dim FieldInfoList As FieldInfo() = XmlToFieldInfoList(xmlFieldAndValueList)


        Try
            Dim fs As FileStream = New FileStream(CmsPhysicalPath + strFilePath, FileMode.Create, FileAccess.Write)
            fs.Write(binFile, 0, binFile.Length)
            fs.Flush()
            fs.Close()

            Return InsertDocToDB(strFilePath, UserID, id, ResourceID, FileName, binFile.Length, FieldInfoList)
        Catch ex As Exception

            Return ""

        End Try
    End Function



    '只向Cms_DocumentCenter插入记录，不做上传操作,并将生成好的记录和表关联
    Public Shared Sub InsertFileToDB(ByVal Cms_DocumentCenterId As String, ByVal KeyWords As String, ByVal ResourceID As String, ByVal strFilePath As String, ByVal UserID As String, ByVal FileName As String, ByVal Size As Long, ByVal FieldValueList As FieldInfo())
        Dim Ext As String = ""
        Dim i As Integer = FileName.LastIndexOf(".")
        If i > 0 Then
            Ext = FileName.Substring(i + 1, FileName.Length - i - 1)
            FileName = FileName.Substring(0, i)
        End If
        Dim hst As Hashtable = New Hashtable
        hst("DOC2_ID") = Cms_DocumentCenterId '唯一的记录ID
        hst("DOC2_CRTID") = UserID '创建人员ID
        hst("DOC2_CRTTIME") = Date.Now '创建时间
        hst("DOC2_EDTID") = UserID '修改人员ID
        hst("DOC2_EDTTIME") = Date.Now '修改时间
        hst("DOC2_NAME") = FileName
        hst("DOC2_EXT") = Ext
        hst("DOC2_SIZE") = Size '文档SIZE，压缩前大小（默认不显示状态）
        hst.Add("DocHostName", strFilePath.Replace("\", "/"))
        hst("DOC2_COMMENTS") = ""
        hst("DOC2_STATUS") = ""
        hst("DOC2_CONVERTED") = 0 '初始值必为0
        hst("DOC2_SHARES") = 1 '初始值必为1
        hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        hst("isupDirectory") = 1
        hst("DOC2_RESID1") = ResourceID '隶属的资源ID 
        hst.Add("DOC2_KEYWORDS", KeyWords)
        SDbStatement.InsertRow(hst, "Cms_DocumentCenter")
        hst.Clear()
        Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
        hst.Add("id", NetReusables.TimeId.CurrentMilliseconds())
        hst.Add("DOCID", Cms_DocumentCenterId)
        hst.Add("RESID", ResourceID)
        For Each li As FieldInfo In FieldValueList
            hst.Add(li.FieldName, li.FieldValue)
        Next
        SDbStatement.InsertRow(hst, TableName)
    End Sub

    '只向Cms_DocumentCenter插入记录，不做上传操作
    Public Shared Function UploadFile(ByVal Cms_DocumentCenterId As String, ByVal KeyWords As String, ByVal ResourceID As String, ByVal strFilePath As String, ByVal UserID As String, ByVal FileName As String, ByVal Size As Long, ByVal FieldValueList As FieldInfo()) As Boolean
        Try
            InsertFileToDB(Cms_DocumentCenterId, KeyWords, ResourceID, strFilePath, UserID, FileName, Size, FieldValueList)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function XmlToFieldInfoList(ByVal xmlFieldAndValueList As String) As FieldInfo()
        Dim ds As DataSet = New DataSet
        ds = XmlToDataSet(xmlFieldAndValueList)
        Dim FieldInfoList() As FieldInfo
        ReDim FieldInfoList(ds.Tables(0).Rows.Count - 1)
        Dim fieldinfo As FieldInfo
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            fieldinfo = New FieldInfo
            fieldinfo.FieldName = ds.Tables(0).Rows(i)("FieldName")
            fieldinfo.FieldValue = ds.Tables(0).Rows(i)("FieldValue")
            FieldInfoList(i) = fieldinfo
        Next
        Return FieldInfoList
    End Function

    Public Shared Function XmlToDataSet(ByVal xmlStr As String) As DataSet

        If Not String.IsNullOrEmpty(xmlStr) Then
            Dim Xmlrdr As XmlTextReader = Nothing
            Dim StrStream As StringReader = Nothing
            Try
                Dim ds As DataSet = New DataSet
                If Not xmlStr.Contains("<?xml version=""1.0""") Then
                    xmlStr = "<?xml version=""1.0"" encoding=""GB2312"" ?>" + xmlStr
                End If
                StrStream = New StringReader(xmlStr)
                Xmlrdr = New XmlTextReader(StrStream)
                ds.ReadXml(Xmlrdr)
                Return ds
            Catch e As Exception

                Return Nothing

            Finally

                If Not Xmlrdr Is Nothing Then

                    Xmlrdr.Close()
                    StrStream.Close()
                    StrStream.Dispose()

                End If

            End Try
        Else

            Return Nothing

        End If
    End Function
    Public Shared Sub InsertFileToDB(ByVal strFilePath As String, ByVal UserID As String, ByVal ID As String, ByVal ResourceID As String, ByVal FileName As String, ByVal Size As Integer, ByVal FieldInfoList As FieldInfo())

        Dim Ext As String = ""
        Dim CRTIDstr As String = ""
        Dim i As Integer = FileName.LastIndexOf(".")
        If i > 0 Then
            Ext = FileName.Substring(i + 1, FileName.Length - i - 1)
            FileName = FileName.Substring(0, i)
        End If
        Dim updataID As String = ""
        Dim DOC2Time As String = ""
        Dim DOC2_EDTID As String = ""
        Dim FileSize As String = ""
        Dim FName As String = ""
        Dim TbaleName As String = ""
        Dim COMMENTS As String = ""
        Dim KEYWORDS As String = ""


        updataID = ""




        Dim hst As Hashtable = New Hashtable
        hst("DOC2_ID") = ID '唯一的记录ID

        hst("DOC2_CRTID") = UserID '创建人员ID
        hst("DOC2_CRTTIME") = Date.Now '创建时间
        hst("DOC2_EDTID") = UserID '修改人员ID
        hst("DOC2_EDTTIME") = Date.Now '修改时间
        hst("DOC2_NAME") = FileName
        hst("DOC2_EXT") = Ext

        hst("DOC2_SIZE") = Size '文档SIZE，压缩前大小（默认不显示状态）

        hst.Add("DocHostName", "/" + strFilePath.Replace("\", "/"))




        hst("DOC2_COMMENTS") = ""

        hst("DOC2_KEYWORDS") = ""
        For Each fieldinfo As FieldInfo In FieldInfoList
            Dim fieldName As String = fieldinfo.FieldName
            If fieldName <> "" Then
                fieldName = fieldName.ToUpper
            End If
            If fieldName = "DOC2_COMMENTS" Or fieldinfo.FieldDescription = "备注" Then
                hst("DOC2_COMMENTS") = fieldinfo.FieldValue
            ElseIf fieldName = "DOC2_KEYWORDS" Or fieldinfo.FieldDescription = "关键字" Then
                hst("DOC2_KEYWORDS") = fieldinfo.FieldValue
            ElseIf fieldinfo.FieldDescription = "RecID" Then
                hst("[DOC2_RESID2]") = fieldinfo.FieldValue
                updataID = fieldinfo.FieldValue
            ElseIf fieldinfo.FieldDescription = "上传文件表" Then
                TbaleName = fieldinfo.FieldValue
            End If



        Next


        If ResourceID = 432746741276 Then
            ResourceID = TbaleName
        End If

        hst("DOC2_RESID1") = ResourceID '隶属的资源ID

        hst("DOC2_STATUS") = ""
        hst("DOC2_CONVERTED") = 0 '初始值必为0
        hst("DOC2_SHARES") = 1 '初始值必为1
        hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        '  hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        hst("isupDirectory") = 1
        SDbStatement.InsertRow(hst, "Cms_DocumentCenter")

        hst = New Hashtable
        hst("ID") = TimeId.CurrentMilliseconds(30)
        'If updataID = "" Then
        '    hst("ID") = TimeId.CurrentMilliseconds(30)
        'Else
        '    hst("ID") = updataID
        'End If

        hst("DOCID") = ID

        hst("RESID") = ResourceID '隶属的资源ID


        Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
        Dim dtResourceColumns As DataTable = GetDefineColumnsByResouceID(ResourceID)
        If FieldInfoList IsNot Nothing Then
            For Each dr As DataRow In dtResourceColumns.Rows
                For Each li As FieldInfo In FieldInfoList
                    If DbField.GetStr(dr, "CD_COLNAME") = li.FieldName Or DbField.GetStr(dr, "CD_DISPNAME") = li.FieldDescription Then
                        If li.FieldValue = "" Then
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), DBNull.Value)
                        Else
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), li.FieldValue)
                        End If
                    End If
                Next
            Next
        End If


        If hst.Contains("UpFileTable") = False And TbaleName <> "" Then
            hst("UpFileTable") = TbaleName
        End If


        SDbStatement.InsertRow(hst, TableName)

    End Sub


    Public Shared Function InsertDocToDB(ByVal strFilePath As String, ByVal UserID As String, ByVal ID As String, ByVal ResourceID As String, ByVal FileName As String, ByVal Size As Integer, ByVal FieldInfoList As FieldInfo()) As String

        Dim Ext As String = ""
        Dim i As Integer = FileName.LastIndexOf(".")
        If i > 0 Then
            Ext = FileName.Substring(i + 1, FileName.Length - i - 1)
            FileName = FileName.Substring(0, i)
        End If
        Dim hst As Hashtable = New Hashtable
        hst("DOC2_ID") = ID '唯一的记录ID
        hst("DOC2_RESID1") = ResourceID '隶属的资源ID
        hst("DOC2_CRTID") = UserID '创建人员ID
        hst("DOC2_CRTTIME") = Date.Now '创建时间
        hst("DOC2_EDTID") = UserID '修改人员ID
        hst("DOC2_EDTTIME") = Date.Now '修改时间
        hst("DOC2_NAME") = FileName
        hst("DOC2_EXT") = Ext

        hst("DOC2_SIZE") = Size '文档SIZE，压缩前大小（默认不显示状态）

        hst.Add("DocHostName", "/" + strFilePath.Replace("\", "/"))
        hst("DOC2_COMMENTS") = ""
        hst("DOC2_KEYWORDS") = ""
        For Each fieldinfo As FieldInfo In FieldInfoList
            Dim fieldName As String = fieldinfo.FieldName
            If fieldName <> "" Then
                fieldName = fieldName.ToUpper
            End If
            If fieldName = "DOC2_COMMENTS" Or fieldinfo.FieldDescription = "备注" Then
                hst("DOC2_COMMENTS") = fieldinfo.FieldValue
            ElseIf fieldName = "DOC2_KEYWORDS" Or fieldinfo.FieldDescription = "关键字" Then
                hst("DOC2_KEYWORDS") = fieldinfo.FieldValue
            End If
        Next



        hst("DOC2_STATUS") = ""
        hst("DOC2_CONVERTED") = 0 '初始值必为0
        hst("DOC2_SHARES") = 1 '初始值必为1
        hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        '  hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        hst("isupDirectory") = 1
        SDbStatement.InsertRow(hst, "Cms_DocumentCenter")

        hst = New Hashtable
        Dim KeyID As String = TimeId.CurrentMilliseconds(30)
        hst("ID") = KeyID
        hst("docID") = ID
        hst("RESID") = ResourceID


        Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
        Dim dtResourceColumns As DataTable = GetDefineColumnsByResouceID(ResourceID)
        If FieldInfoList IsNot Nothing Then
            For Each dr As DataRow In dtResourceColumns.Rows
                For Each li As FieldInfo In FieldInfoList
                    If DbField.GetStr(dr, "CD_COLNAME") = li.FieldName Or DbField.GetStr(dr, "CD_DISPNAME") = li.FieldDescription Then
                        If li.FieldValue = "" Then
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), DBNull.Value)
                        Else
                            hst.Add(DbField.GetStr(dr, "CD_COLNAME"), li.FieldValue)
                        End If
                    End If
                Next
            Next
        End If
        SDbStatement.InsertRow(hst, TableName)
        Return KeyID
    End Function



    Public Shared Function translation(ByVal sql As String) As String
        Dim TableCollection As Collection = GetCollection("[", "]", sql)
        Dim ColumnCollection As Collection = GetCollection("<%", "%>", sql)
        Dim TableNameList As String = ""
        For Each value As String In TableCollection

            TableNameList = TableNameList + ",'" + value + "'"

        Next

        If TableNameList <> "" Then
            TableNameList = TableNameList.Remove(0, 1)
        Else
            Return sql
        End If
        Dim dtTables As DataTable = GetTableNamesByNames(TableNameList)

        Dim dtColumns As DataTable = GetColumnsByNames(TableNameList)
        For Each TableRow As DataRow In dtTables.Rows
            Dim ColumnRow As DataRow = dtColumns.NewRow
            ColumnRow("resourceName") = TableRow("name")
            ColumnRow("fullcolname") = TableRow("res_table") + ".id"
            ColumnRow("fulldispname") = TableRow("name") + ".id"
            ColumnRow("dispname") = "id"
            ColumnRow("colname") = "id"
            dtColumns.Rows.Add(ColumnRow)
        Next
        dtColumns.PrimaryKey = New DataColumn() {dtColumns.Columns("FullDISPNAME")}

        dtTables.PrimaryKey = New DataColumn() {dtTables.Columns("NAME")}
        Dim dr As DataRow
        For Each value As String In ColumnCollection
            If value.IndexOf(".*") > 0 Then
                Dim allColumns As String = ""
                For Each dr In dtColumns.Select("ResourceName='" + value.Split(".")(0) + "'")
                    allColumns = allColumns + "," + dr("FullCOLNAME") + " as " + dr("DispName")
                Next
                If allColumns <> "" Then
                    allColumns = allColumns.Remove(0, 1)
                    sql = sql.Replace("<%" + value + "%>", allColumns)
                End If
            Else
                dr = dtColumns.Rows.Find(value)
                sql = sql.Replace("<%" + value + "%>", dr("FullColName"))
            End If
        Next
        If dtTables.Rows.Count > 0 Then
            For Each value As String In TableCollection
                sql = sql.Replace("[" + value + "]", dtTables.Rows.Find(value)("res_table"))
            Next
        End If

        Return sql
    End Function



    Public Shared Function GetCollection(ByVal StartString As String, ByVal EndString As String, ByVal Str As String) As Collection
        Dim Collection As Collection = New Collection
        Dim Value As String
        Dim i As Integer = 0
        While Str.IndexOf(StartString, i) > 0
            i = Str.IndexOf(StartString, i) + StartString.Length
            Dim j As Integer = Str.IndexOf(EndString, i)
            If j > 0 Then
                Value = Str.Substring(i, j - i)
                If Not Collection.Contains(Value) Then
                    Collection.Add(Value)
                End If
            End If

        End While
        Return Collection
    End Function
    Public Shared Function GetColumnsByNames(ByVal ResourceNames As String) As DataTable

        Dim sql As String = "select R.Name as ResourceName, R.res_table+'.'+D.CD_COLNAME as FullCOLNAME,R.Name+'.'+D.CD_DISPNAME as FullDISPNAME," _
              & " D.CD_DISPNAME as DispName,D.CD_COLNAME as ColName from CMS_TABLE_DEFINE D " _
              & " join CMS_RESOURCE R ON R.ID=D.CD_RESID " _
              & " where R.Name in(" + ResourceNames + ")"

        Return SDbStatement.Query(sql).Tables(0)

    End Function
    Public Shared Function GetDefineColumnsByTableName(ByVal TableName As String) As DataTable
        Dim sql As String = "select  D.CD_COLNAME,D.CD_DISPNAME from CMS_TABLE_DEFINE D" _
                    & " where CD_RESID in (select id from cms_resource where res_table='" + TableName + "' and res_type=0)"
        Return SDbStatement.Query(sql).Tables(0)
    End Function

    Public Shared Function GetTableNamesByNames(ByVal Names As String) As DataTable

        Dim sql As String = "select res_table,Name from CMS_RESOURCE" _
              & " where Name in(" + Names + ")"
        Return SDbStatement.Query(sql).Tables(0)

    End Function

    Public Shared Function FillFieldInfo(ByVal FieldDescription As String, ByVal FieldName As String, ByVal FieldValue As Object) As FieldInfo
        Dim fi As New FieldInfo()
        fi.FieldDescription = FieldDescription
        fi.FieldName = FieldName
        fi.FieldValue = FieldValue
        Return fi
    End Function

    Public Shared Function IsTrue(ByVal timeTicks As String, ByVal randomNum As String, ByVal keyPass As String) As Boolean
        Dim defaultStr As String = "2017kurun0831"
        Dim strlist As New List(Of String)
        strlist.Add(timeTicks)
        strlist.Add(randomNum)
        strlist.Add(defaultStr)
        strlist.Sort()
        Dim str As String = strlist(0) + strlist(1) + strlist(2)
        Dim pwdstr As String = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1")
        If System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwdstr, "MD5") = keyPass Then
            Return True
        Else
            Return False
        End If
    End Function

    ''-------------------------------------------------------------------------
    ''获取有独立表单的最近的父资源ID
    ''-------------------------------------------------------------------------
    'Public Shared Function GetIndepParentResID(ByVal dtRes As DataTable, ByVal lngResID As Long) As Long
    '    Dim dr() As DataRow = dtRes.Select("ID=" + lngResID.ToString)
    '    If dr.Length > 0 Then
    '        If dr(0)("Res_type") = 0 Then
    '            Return dr(0)("ID")
    '        Else
    '            If dr(0)("PID") = 0 Then
    '                Return dr(0)("ID")
    '            Else
    '                Return GetIndepParentResID(dtRes, dr(0)("PID"))
    '            End If
    '        End If
    '    End If


    'End Function
End Class


