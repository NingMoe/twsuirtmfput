Imports Microsoft.VisualBasic
Imports NetReusables
Imports System.Data.SqlClient
Public Class Transaction
    Public Shared Function Add(ByVal UserID As String, ByVal ParentRecordInfo As RecordInfo, ByVal ParamArray ChildRecordInfoList As RecordInfo()()) As Long
        If ParentRecordInfo Is Nothing Then
            Return 0
        End If

        If ChildRecordInfoList.Length = 0 Then
            Dim oRecordInfo As RecordInfo() = {ParentRecordInfo}
            oRecordInfo = Add(UserID, oRecordInfo)
            If oRecordInfo.Length > 0 Then
                Return oRecordInfo(0).RecordID
            Else
                Return 0
            End If
        End If
        Dim ChildResourceList As String = ""
        Dim ParentResourceID As String = ParentRecordInfo.ResourceID
        If ParentRecordInfo.RecordID = "" Or ParentRecordInfo.RecordID = "0" Then
            ParentRecordInfo.RecordID = NetReusables.TimeId.CurrentMilliseconds()
        End If
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        Dim resInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ParentRecordInfo.ResourceID)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        ParentResourceID = Resource.GetTopParentID(ParentResourceID, dtResource)
        If resInfo.IsView Then
            ParentRecordInfo.ResourceID = ParentResourceID
        End If
        Dim TableName As String = Resource.GetTableNameByResourceID(ParentResourceID)

        Dim sql As String = "select * from " + TableName + " where id=" + ParentRecordInfo.RecordID + ";"
        For i As Integer = 0 To ChildRecordInfoList.Length - 1
            Dim RecordIDList As String = ""
            Dim ChildResourceID As String = ""
            For j As Integer = 0 To ChildRecordInfoList(i).Length - 1

                ChildResourceID = ChildRecordInfoList(i)(j).ResourceID
                resInfo = Resource.GetResourceInfoByResourceID(ChildResourceID)
                ChildResourceID = Resource.GetTopParentID(ChildResourceID, dtResource)
                If resInfo.IsView Then
                    ChildRecordInfoList(i)(j).ResourceID = ChildResourceID
                End If
                If ChildRecordInfoList(i)(j).RecordID = "" Or ChildRecordInfoList(i)(j).RecordID = "0" Then
                    ChildRecordInfoList(i)(j).RecordID = NetReusables.TimeId.CurrentMilliseconds()
                End If
                RecordIDList = RecordIDList + " or id=" + ChildRecordInfoList(i)(j).RecordID
            Next
            If RecordIDList <> "" Then
                RecordIDList = RecordIDList.Remove(0, 4)
            End If

            ' ChildResourceID = Resource.GetTopParentID(ChildResourceID, dtResource)

            sql = sql + "select * from " + Resource.GetTableNameByResourceID(ChildResourceID) + " where " + RecordIDList + ";"
            ChildResourceList = ChildResourceList + "," + ChildResourceID
            'If ChildFieldValueList(i).Length > 0 Then
            '    Dim ChildResourceID As String = ChildFieldValueList(i)(0)(0).ResourceID
            '    ChildResourceList = ChildResourceList + "," + ChildResourceID
            '    sql = sql + "select * from " + Resource.GetTableNameByResourceID(ChildResourceID) + " where 1=0;"
            'End If

        Next
        Dim ds As DataSet = New DataSet
        ds = SDbStatement.Query(sql)
        For i As Integer = 0 To ds.Tables.Count - 1
            ds.Tables(i).PrimaryKey = New DataColumn() {ds.Tables(i).Columns("id")}
        Next
        Dim dtParent As DataTable = ds.Tables(0)

        Dim row As DataRow = dtParent.Rows.Find(ParentRecordInfo.RecordID)
        Dim IsAdd As Boolean = False
        If row Is Nothing Then
            row = dtParent.NewRow
            IsAdd = True
        End If
        For Each fi As Field In Common.GetFieldListAll(ParentResourceID)
            For Each li As FieldInfo In ParentRecordInfo.FieldInfoList
                If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                    If li.FieldValue = "" Then
                        row(fi.Name) = DBNull.Value
                    Else
                        row(fi.Name) = li.FieldValue
                      
                    End If

                End If
            Next
            If IsAdd Then
                If fi.ValueType = Field.FieldValueType.AutoCoding Then
                    row(fi.Name) = AutoCode.GetAutoCode(ParentResourceID, fi.Name, True)
                End If
            End If
        Next
        row("id") = ParentRecordInfo.RecordID
        row("resid") = ParentRecordInfo.ResourceID
        If IsAdd Then
            row("CRTID") = UserID
            row("CRTTIME") = Date.Now
            row("RELID") = 0

            dtParent.Rows.Add(row)
        Else
            row("EDTID") = UserID
            row("EDTTIME") = Date.Now
            row("RELID") = 0
            ' dtParent.Rows.Add(row)
        End If
        Dim dtRelation As DataTable = Resource.GetRelation(ParentResourceID, True).Tables(0)
        ' Dim dtInputRelation As DataTable = Resource.GetInputRelation(ParentResourceID).Tables(0)
        ' dtRelation.PrimaryKey = New DataColumn() {dtRelation.Columns("ChildResourceID")}
        For i As Integer = 1 To ds.Tables.Count - 1
            For j As Integer = 0 To ChildRecordInfoList(i - 1).Length - 1
                IsAdd = False
                row = ds.Tables(i).Rows.Find(ChildRecordInfoList(i - 1)(j).RecordID)
                If row Is Nothing Then
                    row = ds.Tables(i).NewRow
                    IsAdd = True
                    '   row("id") = ChildRecordInfoList(i - 1)(j).RecordID
                End If
                Dim ChildResourceID As String = ChildResourceList.Split(",")(i)
                For Each fi As Field In Common.GetFieldListAll(ChildResourceID)
                    For Each li As FieldInfo In ChildRecordInfoList(i - 1)(j).FieldInfoList
                        If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                            If li.FieldValue = "" Then
                                row(fi.Name) = DBNull.Value
                            Else
                                row(fi.Name) = li.FieldValue

                            End If
                        End If
                    Next
                    If IsAdd Then
                        If fi.ValueType = Field.FieldValueType.AutoCoding Then
                            row(fi.Name) = AutoCode.GetAutoCode(ChildResourceID, fi.Name, True)
                        End If
                    
                    End If

                Next
                '主关联
                Dim RelationRows() As DataRow = dtRelation.Select("ChildResourceID=" + ChildResourceID)
                'If RelationRow IsNot Nothing Then
                '    row(RelationRow("ChildColName")) = dtParent.Rows(0)(RelationRow("ParentColName"))
                'End If
                For Each RelationRow As DataRow In RelationRows
                    row(RelationRow("ChildColName")) = dtParent.Rows(0)(RelationRow("ParentColName"))
                Next
                '输入关联
                'For Each rowInputRelation As DataRow In dtInputRelation.Select("ChildResourceID=" + ChildResourceID)
                '    row(rowInputRelation("ChildColName")) = dtParent.Rows(0)(rowInputRelation("ParentColName"))
                'Next
                ' dtInputRelation()


                row("id") = ChildRecordInfoList(i - 1)(j).RecordID
                row("resid") = ChildRecordInfoList(i - 1)(j).ResourceID
                If IsAdd Then
                    row("CRTID") = UserID
                    row("CRTTIME") = Date.Now
                    row("RELID") = 0
                    ds.Tables(i).Rows.Add(row)
                Else
                    row("EDTID") = UserID
                    row("EDTTIME") = Date.Now
                    row("RELID") = 0
                End If
            Next
        Next
        If CommitUpdate(sql, ds) Then
            Return ParentRecordInfo.RecordID
        Else
            Return 0
        End If
    End Function


    Public Shared Function AddofDoc(ByVal UserID As String, ByVal ParentRecordInfo As RecordInfo, ByVal ParamArray ChildRecordInfoList As RecordInfo()()) As Long
        If ParentRecordInfo Is Nothing Then
            Return 0
        End If

        If ChildRecordInfoList.Length = 0 Then
            Dim oRecordInfo As RecordInfo() = {ParentRecordInfo}
            oRecordInfo = Add(UserID, oRecordInfo)
            If oRecordInfo.Length > 0 Then
                Return oRecordInfo(0).RecordID
            Else
                Return 0
            End If 
        End If
        Try
            Dim IsDocResource As Boolean = False
            Dim ChildResourceList As String = ""
            Dim ParentResourceID As String = ParentRecordInfo.ResourceID
            If ParentRecordInfo.RecordID = "" Or ParentRecordInfo.RecordID = "0" Then
                ParentRecordInfo.RecordID = NetReusables.TimeId.CurrentMilliseconds()
            End If
            Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
            Dim resInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ParentRecordInfo.ResourceID)

            ParentRecordInfo.ResourceType = resInfo.TableType.ToUpper.Trim
            If resInfo.TableType.ToUpper.Trim = "DOC" Then IsDocResource = True

            dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
            ParentResourceID = Resource.GetTopParentID(ParentResourceID, dtResource)
            If resInfo.IsView Then
                ParentRecordInfo.ResourceID = ParentResourceID
            End If

            Dim TableName As String = Resource.GetTableNameByResourceID(ParentResourceID)

            Dim sql As String = "select * from " + TableName + " where id=" + ParentRecordInfo.RecordID + ";"
            For i As Integer = 0 To ChildRecordInfoList.Length - 1
                Dim RecordIDList As String = ""
                Dim ChildResourceID As String = ""
                For j As Integer = 0 To ChildRecordInfoList(i).Length - 1

                    ChildResourceID = ChildRecordInfoList(i)(j).ResourceID
                    resInfo = Resource.GetResourceInfoByResourceID(ChildResourceID)
                    If resInfo.IsView Then
                        Dim strParentResID As String = Resource.GetTopParentID(ParentResourceID, dtResource)
                        ChildResourceID = strParentResID
                        ChildRecordInfoList(i)(j).ResourceID = strParentResID
                    End If

                    ChildRecordInfoList(i)(j).ResourceType = resInfo.TableType.ToUpper.Trim
                    If resInfo.TableType.ToUpper.Trim = "DOC" Then IsDocResource = True
                    ChildResourceID = Resource.GetTopParentID(ChildResourceID, dtResource)
                    If resInfo.IsView Then
                        ChildRecordInfoList(i)(j).ResourceID = ChildResourceID
                    End If
                    If ChildRecordInfoList(i)(j).RecordID = "" Or ChildRecordInfoList(i)(j).RecordID = "0" Then
                        ChildRecordInfoList(i)(j).RecordID = NetReusables.TimeId.CurrentMilliseconds()
                    End If
                    RecordIDList = RecordIDList + " or id=" + ChildRecordInfoList(i)(j).RecordID
                Next
                If RecordIDList <> "" Then
                    RecordIDList = RecordIDList.Remove(0, 4)
                End If

                ' ChildResourceID = Resource.GetTopParentID(ChildResourceID, dtResource)

                sql = sql + "select * from " + Resource.GetTableNameByResourceID(ChildResourceID) + " where " + RecordIDList + ";"
                ChildResourceList = ChildResourceList + "," + ChildResourceID
                'If ChildFieldValueList(i).Length > 0 Then
                '    Dim ChildResourceID As String = ChildFieldValueList(i)(0)(0).ResourceID
                '    ChildResourceList = ChildResourceList + "," + ChildResourceID
                '    sql = sql + "select * from " + Resource.GetTableNameByResourceID(ChildResourceID) + " where 1=0;"
                'End If

            Next

            If IsDocResource Then sql += "select * from Cms_DocumentCenter where 1=2;"
            SLog.Err(sql)
            SLogDb.Err(sql)

            Dim ds As DataSet = New DataSet
            ds = SDbStatement.Query(sql)
            For i As Integer = 0 To ds.Tables.Count - 1
                If IsDocResource And i = ds.Tables.Count - 1 Then
                    ds.Tables(i).PrimaryKey = New DataColumn() {ds.Tables(i).Columns("DOC2_ID")}
                Else
                    ds.Tables(i).PrimaryKey = New DataColumn() {ds.Tables(i).Columns("id")}
                End If

            Next
            Dim dtParent As DataTable = ds.Tables(0)

            Dim row As DataRow = dtParent.Rows.Find(ParentRecordInfo.RecordID)
            Dim IsAdd As Boolean = False
            If row Is Nothing Then
                row = dtParent.NewRow
                IsAdd = True
            End If
            For Each fi As Field In Common.GetFieldListAll(ParentResourceID)
                For Each li As FieldInfo In ParentRecordInfo.FieldInfoList
                    If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                        If li.FieldValue = "" Then
                            row(fi.Name) = DBNull.Value
                        Else
                            row(fi.Name) = li.FieldValue

                        End If

                    End If
                Next
                If IsAdd Then
                    If fi.ValueType = Field.FieldValueType.AutoCoding Then
                        row(fi.Name) = AutoCode.GetAutoCode(ParentResourceID, fi.Name, True)
                    End If
                End If
            Next
            row("id") = ParentRecordInfo.RecordID
            row("resid") = ParentRecordInfo.ResourceID
            If IsAdd Then
                If ParentRecordInfo.ResourceType.Trim = "DOC" Then
                    Dim DOCID As String = NetReusables.TimeId.CurrentMilliseconds()
                    row("DOCID") = DOCID
                    ds = LoadDocumentCenter(DOCID, ParentRecordInfo, UserID, ds)
                Else
                    row("CRTID") = UserID
                    row("CRTTIME") = Date.Now
                End If

                row("RELID") = 0

                dtParent.Rows.Add(row)
            Else
                row("EDTID") = UserID
                row("EDTTIME") = Date.Now
                row("RELID") = 0
                ' dtParent.Rows.Add(row)
            End If
            Dim dtRelation As DataTable = Resource.GetRelation(ParentResourceID, True).Tables(0)
            'Dim dtInputRelation As DataTable = Resource.GetInputRelation(ParentResourceID).Tables(0)
            dtRelation.PrimaryKey = New DataColumn() {dtRelation.Columns("ChildResourceID")}
            Dim TableCount As Integer = ds.Tables.Count - 1
            If IsDocResource Then TableCount = ds.Tables.Count - 2
            For i As Integer = 1 To TableCount
                For j As Integer = 0 To ChildRecordInfoList(i - 1).Length - 1
                    IsAdd = False
                    row = ds.Tables(i).Rows.Find(ChildRecordInfoList(i - 1)(j).RecordID)
                    If row Is Nothing Then
                        row = ds.Tables(i).NewRow
                        IsAdd = True
                        '   row("id") = ChildRecordInfoList(i - 1)(j).RecordID
                    End If
                    Dim ChildResourceID As String = ChildResourceList.Split(",")(i)
                    For Each fi As Field In Common.GetFieldListAll(ChildResourceID, ChildRecordInfoList(i - 1)(j).ResourceType.Trim)

                        For Each li As FieldInfo In ChildRecordInfoList(i - 1)(j).FieldInfoList
                            If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                                If li.FieldValue = "" Then
                                    row(fi.Name) = DBNull.Value
                                Else
                                    row(fi.Name) = li.FieldValue

                                End If
                            End If
                        Next
                        If IsAdd Then
                            If fi.ValueType = Field.FieldValueType.AutoCoding Then
                                row(fi.Name) = AutoCode.GetAutoCode(ChildResourceID, fi.Name, True)
                            End If

                        End If

                    Next
                    '主关联
                    'Dim RelationRow As DataRow = dt.Rows.Find(ChildResourceID)
                    'If RelationRow IsNot Nothing Then
                    '    row(RelationRow("ChildColName")) = dtParent.Rows(0)(RelationRow("ParentColName"))
                    'End If

                    Dim RelationRows() As DataRow = dtRelation.Select("ChildResourceID=" + ChildResourceID)
                    For Each RelationRow As DataRow In RelationRows
                        row(RelationRow("ChildColName")) = dtParent.Rows(0)(RelationRow("ParentColName"))
                    Next

                    ''输入关联
                    'For Each rowInputRelation As DataRow In dtInputRelation.Select("ChildResourceID=" + ChildResourceID)
                    '    row(rowInputRelation("ChildColName")) = dtParent.Rows(0)(rowInputRelation("ParentColName"))
                    'Next
                    ' dtInputRelation()


                    row("id") = ChildRecordInfoList(i - 1)(j).RecordID
                    row("resid") = ChildRecordInfoList(i - 1)(j).ResourceID
                    If IsAdd Then
                        If ChildRecordInfoList(i - 1)(j).ResourceType.Trim = "DOC" Then
                            Dim DOCID As String = NetReusables.TimeId.CurrentMilliseconds()
                            row("DOCID") = DOCID
                            ds = LoadDocumentCenter(DOCID, ChildRecordInfoList(i - 1)(j), UserID, ds)
                        Else
                            row("CRTID") = UserID
                            row("CRTTIME") = Date.Now
                        End If
                        row("RELID") = 0
                        ds.Tables(i).Rows.Add(row)
                    Else
                        row("EDTID") = UserID
                        row("EDTTIME") = Date.Now
                        row("RELID") = 0
                    End If
                Next
            Next

            If CommitUpdate(sql, ds) Then
                Return ParentRecordInfo.RecordID
            Else
                Return 0
            End If
        Catch ex As Exception
            SLog.Err(ex.Message)
            Return 0
        End Try

    End Function

     

    Public Shared Function AddOfDoc(ByVal UserID As String, ByVal DocumentRecordInfo() As RecordInfo) As RecordInfo()

        If DocumentRecordInfo.Length = 0 Then
            Return Nothing
        End If
        Dim RecordIDList As String = ""
        Dim sql As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        For i As Integer = 0 To DocumentRecordInfo.Length - 1

            Dim ChildResourceID As String = ""
            Dim RecordID As String = DocumentRecordInfo(i).RecordID
            Dim ResourceID As String = DocumentRecordInfo(i).ResourceID
            Dim resInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)


            If RecordID = "" Or RecordID = "0" Then
                RecordID = NetReusables.TimeId.CurrentMilliseconds()
                DocumentRecordInfo(i).RecordID = RecordID
            End If

            DocumentRecordInfo(i).ResourceType = resInfo.TableType.ToUpper.Trim

            If resInfo.IsView Then
                ResourceID = Resource.GetTopParentID(ResourceID, dtResource)
                DocumentRecordInfo(i).ResourceID = ResourceID
            End If

            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            sql = sql + "select * from " + TableName + " where id=" + RecordID + ";"
        Next
        sql += "select * from Cms_DocumentCenter where 1=2;"

        Dim ds As DataSet = SDbStatement.Query(sql)

        For i As Integer = 0 To ds.Tables.Count - 1
            If i = ds.Tables.Count - 1 Then
                ds.Tables(i).PrimaryKey = New DataColumn() {ds.Tables(i).Columns("DOC2_ID")}
            Else
                ds.Tables(i).PrimaryKey = New DataColumn() {ds.Tables(i).Columns("id")}
            End If
        Next

        For i As Integer = 0 To DocumentRecordInfo.Length - 1
            Dim IsAdd As Boolean = False
            Dim row As DataRow = ds.Tables(i).Rows.Find(DocumentRecordInfo(i).RecordID)
            If row Is Nothing Then
                IsAdd = True
                row = ds.Tables(i).NewRow
            End If

            For Each fi As Field In Common.GetFieldListAll(DocumentRecordInfo(i).ResourceID, DocumentRecordInfo(i).ResourceType.Trim)

                '  For Each fi As Field In Common.GetFieldListAll(DocumentRecordInfo(i).ResourceID)
                For Each li As FieldInfo In DocumentRecordInfo(i).FieldInfoList
                    If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                        If li.FieldValue = "" Then
                            row(fi.Name) = DBNull.Value
                        Else
                            row(fi.Name) = li.FieldValue

                        End If
                    End If
                Next
                If IsAdd Then
                    If fi.ValueType = Field.FieldValueType.AutoCoding Then
                        row(fi.Name) = AutoCode.GetAutoCode(DocumentRecordInfo(i).ResourceID, fi.Name, True)
                    End If
                End If

            Next
            row("id") = DocumentRecordInfo(i).RecordID
            row("resid") = DocumentRecordInfo(i).ResourceID
            If IsAdd Then

                row("RELID") = 0

                Dim DocID As String = TimeId.CurrentMilliseconds()
                row("DOCID") = DocID
                ds = LoadDocumentCenter(DocID, DocumentRecordInfo(i), UserID, ds)
                ds.Tables(i).Rows.Add(row)
            Else
                row("RELID") = 0
            End If

        Next
        If CommitUpdate(sql, ds) Then
            Return DocumentRecordInfo
        Else
            Return Nothing
        End If
    End Function


    Private Shared Function LoadDocumentCenter(DocID As String, oRecordInfo As RecordInfo, UserID As String, ByRef ds As DataSet) As DataSet
        Dim dtColumnName As DataTable = SDbStatement.Query(" Select o.Name As TABLE_NAME , c.name As COLUMN_NAME , t.name As DATA_TYPE , 0 OrderNum,0 IsBe From SysObjects As o , SysColumns As c , SysTypes As t Where o.type in ('u','v') And o.id = c.id And c.xtype = t.xtype and  o.Name='Cms_DocumentCenter' and t.name<>'sysname' Order By o.name , c.name , t.name , c.Length ").Tables(0)
        Dim oFieldInfo As FieldInfo() = oRecordInfo.FieldInfoList
        Dim dtDoc As DataTable = ds.Tables(ds.Tables.Count - 1)
        Dim drNew As DataRow = dtDoc.NewRow

        For Each dr As DataRow In dtColumnName.Rows
            Dim strColumnName As String = DbField.GetStr(dr, "COLUMN_NAME").Trim
            For Each fi As FieldInfo In oFieldInfo
                If fi.FieldDescription = strColumnName Or fi.FieldName = strColumnName Then
                    drNew(strColumnName) = fi.FieldValue
                End If
            Next
        Next

        drNew("DOC2_ID") = DocID
        drNew("DOC2_RESID1") = oRecordInfo.ResourceID
        drNew("DOC2_CRTID") = UserID
        drNew("DOC2_EDTID") = UserID
        drNew("DOC2_COMPRESSED_SIZE") = "0"
        drNew("DOC2_COMPRESSED_RATE") = "0"
        drNew("DOC2_CONVERTED") = "0"
        drNew("DOC2_SHARES") = "1"
        drNew("DOC2_LCYC_ENABLE") = "0"
        drNew("DOC2_LCYC_LASTDO_TIME") = Date.Now
        drNew("DOC2_CRTTIME") = Date.Now
        drNew("DOC2_EDTTIME") = Date.Now

        dtDoc.Rows.Add(drNew)
        Return ds
    End Function

    Public Shared Function Add(ByVal UserID As String, ByVal RecordInfo() As RecordInfo) As RecordInfo()

        If RecordInfo.Length = 0 Then
            Return Nothing
        End If
        Dim RecordIDList As String = ""
        Dim sql As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("ID")}
        For i As Integer = 0 To RecordInfo.Length - 1

            Dim ChildResourceID As String = ""
            Dim RecordID As String = RecordInfo(i).RecordID
            Dim ResourceID As String = RecordInfo(i).ResourceID
            Dim resInfo As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)


            If RecordID = "" Or RecordID = "0" Then
                RecordID = NetReusables.TimeId.CurrentMilliseconds()
                RecordInfo(i).RecordID = RecordID
            End If



            If resInfo.IsView Then
                ResourceID = Resource.GetTopParentID(ResourceID, dtResource)
                RecordInfo(i).ResourceID = ResourceID
            End If

            Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
            sql = sql + "select * from " + TableName + " where id=" + RecordID + ";"
        Next

        Dim ds As DataSet = SDbStatement.Query(sql)

        For i As Integer = 0 To ds.Tables.Count - 1
            ds.Tables(i).PrimaryKey = New DataColumn() {ds.Tables(i).Columns("id")}
        Next

        For i As Integer = 0 To RecordInfo.Length - 1
            Dim IsAdd As Boolean = False
            Dim row As DataRow = ds.Tables(i).Rows.Find(RecordInfo(i).RecordID)
            If row Is Nothing Then
                IsAdd = True
                row = ds.Tables(i).NewRow
            End If
            For Each fi As Field In Common.GetFieldListAll(RecordInfo(i).ResourceID)
                For Each li As FieldInfo In RecordInfo(i).FieldInfoList
                    If fi.Description = li.FieldDescription Or fi.Name = li.FieldName Then
                        If li.FieldValue = "" Then
                            row(fi.Name) = DBNull.Value
                        Else
                            row(fi.Name) = li.FieldValue

                        End If
                    End If
                Next
                If IsAdd Then
                    If fi.ValueType = Field.FieldValueType.AutoCoding Then
                        row(fi.Name) = AutoCode.GetAutoCode(RecordInfo(i).ResourceID, fi.Name, True)
                    End If
                End If
            Next
            row("id") = RecordInfo(i).RecordID
            row("resid") = RecordInfo(i).ResourceID
            If IsAdd Then
                row("CRTID") = UserID
                row("CRTTIME") = Date.Now
                row("RELID") = 0
                ds.Tables(i).Rows.Add(row)
            Else
                row("EDTID") = UserID
                row("EDTTIME") = Date.Now
                row("RELID") = 0
            End If

        Next
        If CommitUpdate(sql, ds) Then
            Return RecordInfo
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' 事务提交
    ''' </summary>
    ''' <param name="selectSql"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CommitUpdate(ByVal selectSql As String, ByVal ds As DataSet) As Boolean
        Dim Conn As SqlConnection = New SqlConnection(GetConnectString())
        Conn.Open()
        Dim tran As SqlTransaction = Conn.BeginTransaction()
        For i As Integer = 0 To ds.Tables.Count - 1
            Dim da As SqlDataAdapter = New SqlClient.SqlDataAdapter(selectSql.Split(";")(i), Conn)
            Dim sqlCmdBuilder As SqlCommandBuilder = New SqlCommandBuilder(da)
            da.SelectCommand.Transaction = tran
            da.Update(ds.Tables(i))
        Next

        Try
            tran.Commit()
            Return True
        Catch ex As Exception 
            tran.Rollback()
            SLog.Err(ex.Message)
            Throw New Exception(ex.Message)
        Finally
            Conn.Close()
        End Try
    End Function


    Public Shared Function GetConnectString() As String
        Dim dbcSrc As DbConfig = SDbConnectionPool.GetDbConfig()
        Dim ConnectString As String = "Data Source=" + dbcSrc.Host + "; Initial Catalog=" + dbcSrc.Database + ";User ID=" + dbcSrc.User + ";Password=" + dbcSrc.Pass + ";Max Pool Size=150;Connect Timeout=500;"
        Return ConnectString
    End Function
End Class




