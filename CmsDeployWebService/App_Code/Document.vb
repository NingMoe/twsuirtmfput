Imports Microsoft.VisualBasic
Imports System.IO
Imports Unionsoft.Platform
Imports System.Collections.Generic
Imports NetReusables
Public Class Document


    Public Shared Function DownloadDoc(ByVal lngResID As Long, ByVal lngRecID As Long) As String

        Dim ds As DataSet = Resource.ShowChildTablesByID(lngRecID.ToString, lngResID.ToString)
        Dim strFilePath As String = ""
        Dim al As New ArrayList
        If ds.Tables.Count > 1 Then
            For i As Integer = 1 To ds.Tables.Count - 1
                Dim dt As DataTable = ds.Tables(i)
                If dt.Rows.Count > 0 Then

                    If Resource.GetResourceInfoByResourceID(dt.Rows(0)("ResID")).TableType.ToLower.Trim = "doc" Then
                        For j As Integer = 0 To dt.Rows.Count - 1
                            Dim strPath As String = CreateDocumentOnServer(dt.Rows(j)("ResID"), dt.Rows(j)("DOCID"))
                            If strPath.Trim <> "" Then al.Add(strPath)
                        Next
                    End If

                End If
            Next
        End If
        If al.Count > 0 Then
            strFilePath = ZipFile.ZipClass(al, lngResID.ToString)
        End If
        Return strFilePath
    End Function
    Public Shared Function CreateDocumentOnServer(ByVal lngResourceID As Long, ByVal lngDocumentID As Long) As String
        Try
            Dim ProjectRootFolder As String = System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath)
            Dim datDoc As Unionsoft.Platform.DataDocument = ResFactory.TableService("DOC").GetDocument(lngDocumentID)
            Dim strFileExt As String = datDoc.strDOC2_EXT
            '获取临时文档目录
            Dim strTemp As String = "Temp"
            Dim strDocFolder As String = Unionsoft.Platform.CmsConfig.ProjectRootFolder & strTemp & "\" & datDoc.lngDOCID & "\"
            If Directory.Exists(strDocFolder) = False Then
                Directory.CreateDirectory(strDocFolder)
            End If
            '获取临时文档全路径，并生产文档
            Dim strFileName As String
            strFileName = datDoc.lngDOCID.ToString() & "." & datDoc.strDOC2_EXT
            Dim strFilePath As String = strDocFolder & strFileName
            Dim fs As FileStream = New FileStream(strFilePath, FileMode.Create, FileAccess.Write)
            Dim binFile As Byte() = CType(datDoc.bytDOC2_FILE_BIN, Byte())
            fs.Write(binFile, 0, binFile.Length)
            fs.Flush()
            fs.Close()
            Dim strFileUrlPath As String = strFilePath
            Return strFileUrlPath
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function GetDocuments(ByVal lngResID As Long, ByVal lngRecID As Long) As List(Of DocmentInfo)
        Dim dtDocument As New DataTable
        Dim docIDs As String = ""
        Dim myList As List(Of DocmentInfo) = New List(Of DocmentInfo)
        Dim ds As DataSet = Resource.ShowChildTablesByID(lngRecID.ToString, lngResID.ToString)
        If ds.Tables.Count > 1 Then
            For i As Integer = 1 To ds.Tables.Count - 1
                Dim dt As DataTable = ds.Tables(i)
                If dt.Rows.Count > 0 Then

                    If Resource.GetResourceInfoByResourceID(dt.Rows(0)("ResID").ToString).TableType.ToLower.Trim = "doc" Then
                        For j As Integer = 0 To dt.Rows.Count - 1
                            myList.Add(GetDocumentByID(dt.Rows(j)("DOCID").ToString))
                        Next
                    End If
                End If
            Next
        End If

        Return myList
    End Function
    Public Shared Function GetDocument(ByVal DocResourceID As Long, ByVal ID As Long) As DataSet
          
        Dim sql As String = GetDocumentSqlByResourceID(DocResourceID, "and id=" + ID.ToString)
        Return SDbStatement.Query(sql)


    End Function
    Public Shared Function GetBytesByDocumentID(ByVal DocumentID As String) As Byte()

        Dim sql As String = "select * from Cms_DocumentCenter where Doc2_id=" + DocumentID
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)

            Return GetDocumentToBinary(dr("DOCHOSTNAME"))
           
        Else
            Return Nothing
        End If

    End Function

    Public Shared Function GetDocumentByID(ByVal DocumentID As String) As DocmentInfo
        Dim sql As String = "select * from Cms_DocumentCenter where Doc2_id=" + DocumentID
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)
        Dim myList As DocmentInfo = New DocmentInfo
        Dim StoreType As Boolean = False
        Dim DocmentInfo As DocmentInfo
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            DocmentInfo = New DocmentInfo
            DocmentInfo.Name = dr("DOC2_NAME")
            DocmentInfo.ID = dr("DOC2_ID").ToString
            If Not IsDBNull(dr("isUpDirectory")) Then
                StoreType = dr("isUpDirectory")
            End If

            If StoreType = True Then

                DocmentInfo.Path = Common.GetServerPath + "\cmsdocument\DocView.aspx?docid=" + DocumentID
            Else
                ' Common.GetServerPath(+"/ShowDocument.aspx?docid='+convert(varchar(255),DOCID)+'&TableName='+docHostName")
                DocmentInfo.Path = Common.GetServerPath + "/ShowDocument.aspx?docid=" + dr("DOC2_ID").ToString + "&TableName=" + dr("DocHostName")
            End If
            DocmentInfo.DownLoadPath = dr("DOCHOSTNAME")
            DocmentInfo.DownLoadFullPath = Common.GetServerPath + dr("DOCHOSTNAME")
            DocmentInfo.Ext = dr("DOC2_EXT")
            DocmentInfo.Size = dr("DOC2_SIZE")
            DocmentInfo.StoreType = IIf(IsDBNull(dr("isUpDirectory")), False, dr("isUpDirectory"))
            DocmentInfo.DocResid = dr("doc2_resid1").ToString

            Return DocmentInfo
        Else
            Return Nothing
        End If


    End Function

    'Public Shared Function GetDocumentSqlByResourceID(ByVal ResourceID As String, ByVal UserID As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String
    '    Dim strSql As String = ""
    '    Dim strColumns As String = ""

    '    Dim info As ResourceInfo = Resource.GetResourceInfoByResourceID(ResourceID)

    '    Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
    '    dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}

    '    Dim ParentResourceID As String = Resource.GetTopParentID(ResourceID, dtResource)

    '    Dim TableName As String = info.TableName

    '    Dim dtColumns As DataTable = Resource.GetColumnsByResouceID(ParentResourceID)
    '    For Each dr As DataRow In dtColumns.Rows
    '        If dr("CD_IS_SYSTIELD") = 1 Then
    '            strColumns += ",Doc." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
    '        Else
    '            strColumns += ",Data." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
    '        End If
    '    Next

    '    If Not info.IsView Then

    '    End If

    '    strColumns = "ID,ResID,DOCID,case when isnull(isUpDirectory,0)=0 then '" + Common.GetServerPath + "/ShowDocument.aspx?docid='+convert(varchar(255),DOCID)+'&TableName='+docHostName else '" + System.Configuration.ConfigurationManager.ConnectionStrings("cmswebsite").ConnectionString + "'+docHostName  end as [路径]" + strColumns
    '    strSql = "select * from (select " + strColumns + " from " + TableName + " Data join Cms_DocumentCenter Doc on Data.docid=Doc.doc2_id ) T where 1=1 " + Condition
    '    Return strSql
    'End Function

    Public Shared Function GetDocumentSqlByResourceID(ByVal ResourceID As String, Optional ByVal Condition As String = "") As String
        Dim strSql As String = ""
        Dim strColumns As String = ""

        Dim TableName As String = Resource.GetTableNameByResourceID(ResourceID)
        Dim dtColumns As DataTable = Resource.GetColumnsByResouceID(ResourceID)
        For Each dr As DataRow In dtColumns.Rows
            If dr("CD_IS_SYSTIELD") = 1 Then
                If dr("CD_COLNAME") = "DOC2_SIZE" Then
                    strColumns += ",convert(varchar(255),CAST(Doc." + dr("CD_COLNAME") + "/1024 as decimal(38, 2)))+'KB' as [" + dr("CD_DISPNAME") + "]"

                Else
                    strColumns += ",Doc." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
                End If

            Else
                strColumns += ",Data." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
            End If

        Next
        strColumns = "ID,ResID,DOCID,RELID,case when isnull(isUpDirectory,0)=0 then '" + Common.GetServerPath + "/ShowDocument.aspx?docid='+convert(varchar(255),DOCID)+'&TableName='+docHostName else docHostName  end as [路径]" + strColumns
        strSql = "select * from (select " + strColumns + " from " + TableName + " Data join Cms_DocumentCenter Doc on Data.docid=Doc.doc2_id ) T where 1=1 " + Condition
        Return strSql
    End Function





    Public Shared Function GetDocumentSqlByResourceInfo(ByVal oResourceInfo As ResourceInfo, ByVal UserID As String, ByVal Condition As String, Optional ByVal IsOrderBy As Boolean = True) As String
        Dim ParentResourceID As String = "-9999"
        Dim strColumns As String = ""
        Dim SearchCondition As String = ""
        Dim dtResource As DataTable = SDbStatement.Query("select * from cms_resource").Tables(0)
        dtResource.PrimaryKey = New DataColumn() {dtResource.Columns("id")}
        ParentResourceID = Resource.GetTopParentID(oResourceInfo.ID, dtResource)

        If oResourceInfo.IsView Then
            SearchCondition = ResourceCondition.GetResourceCondition(oResourceInfo.ID, MTSearchType.ViewFilter, UserID)
        Else
            Dim ResourceInfoList As List(Of ResourceInfo) = Resource.GetAllResourceInfoByTableName(oResourceInfo.TableName)

            If oResourceInfo.ShowChild = 1 Then
                Dim ReturnStr As String = Resource.GetAllChildResourceID(oResourceInfo.ID, ResourceInfoList)
                If ReturnStr <> "" Then
                    ReturnStr = oResourceInfo.ID + ReturnStr
                Else
                    ReturnStr = oResourceInfo.ID
                End If
                Condition = Condition + " and resid IN (" + ReturnStr + ")" ''此处写一个递归
            Else
                Condition = Condition + " and resid=" + oResourceInfo.ID
            End If
        End If

        Dim dtColumns As DataTable = Resource.GetColumnsByResouceID(ParentResourceID)
        For Each dr As DataRow In dtColumns.Rows
            If dr("CD_IS_SYSTIELD") = 1 And dr("CD_COLNAME").ToString.Trim.ToUpper <> "FLOWCODE" Then
                If dr("CD_COLNAME") = "DOC2_SIZE" Then
                    strColumns += ",convert(varchar(255),CAST(Doc." + dr("CD_COLNAME") + "/1024 as decimal(38, 2)))+'KB' as [" + dr("CD_DISPNAME") + "]"

                Else
                    strColumns += ",Doc." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
                End If

            Else
                strColumns += ",Data." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
            End If

        Next
        strColumns = "ID,ResID,DOCID,RELID,case when isnull(isUpDirectory,0)=0 then '" + Common.GetServerPath + "/ShowDocument.aspx?docid='+convert(varchar(255),DOCID)+'&TableName='+docHostName else '" + System.Configuration.ConfigurationManager.ConnectionStrings("cmswebsite").ConnectionString + "'+docHostName  end as [路径]" + strColumns
        Dim strSql As String = "select * from (select " + strColumns + " from (select * from " + oResourceInfo.TableName + IIf(SearchCondition.Trim = "", "", " where " + SearchCondition.Trim) + ") Data join Cms_DocumentCenter Doc on Data.docid=Doc.doc2_id ) T where 1=1 " + Condition
        Return strSql
    End Function


    Public Shared Function GetDocumentToBinary(ByVal FullFileName As String) As Byte()
        Dim Path As String = System.Configuration.ConfigurationManager.ConnectionStrings("cmsPath").ConnectionString
        Dim FileBin As Byte()
        Dim fs As FileStream = New FileStream(Path + Replace(FullFileName, "/", "\"), FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        FileBin = br.ReadBytes(CInt(fs.Length))
        br.Close()
        fs.Close()
        Return FileBin



    End Function


    Public Shared Function GetPageOfDocuments(ByVal ResourceDescription As String, ByVal DocmentName As String, ByVal PageParameter As PageParameter, ByVal Condition As String, ByVal UserID As String) As DataSet
        Dim dt As DataTable = Resource.GetRelationTable(ResourceDescription, DocmentName)
        Dim strColumns As String = ""
        Dim strSql As String = ""
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("ChildTableType") = "DOC" Then


                Dim dtColumns As DataTable = Resource.GetColumnsByResouceID(dt.Rows(0)("ChildResourceID"))
                For Each dr As DataRow In dtColumns.Rows
                    If dr("CD_IS_SYSTIELD") = 1 Then
                        strColumns += ",Doc." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
                    Else
                        strColumns += ",Data." + dr("CD_COLNAME") + " as [" + dr("CD_DISPNAME") + "]"
                    End If

                Next
                strColumns = "ID,ResID,DOCID,case when isnull(isUpDirectory,0)=0 then '" + Common.GetServerPath + "/ShowDocument.aspx?docid='+convert(varchar(255),DOCID)+'&TableName='+docHostName else '" + System.Configuration.ConfigurationManager.ConnectionStrings("cmswebsite").ConnectionString + "'+docHostName  end as [路径]" + strColumns
                strSql = "select * from (select " + strColumns + " from " + dt.Rows(0)("ChildTableName") + " Data join Cms_DocumentCenter Doc on Data.docid=Doc.doc2_id ) T where ResID=" + dt.Rows(0)("ChildResourceID").ToString + Condition

                Dim OrderBy As String = PageParameter.SortField + " " + PageParameter.SortBy
                strSql = " select top " + PageParameter.PageSize.ToString _
                & " * from (" + strSql + ") T where id not in (select top " + (PageParameter.PageIndex * PageParameter.PageSize).ToString _
                & " id from (" + strSql + ") W order by " + OrderBy + ") order by " + OrderBy

                Return SDbStatement.Query(strSql)
            Else
                Return Nothing
            End If
        Else
            Return Nothing

        End If


    End Function
End Class
