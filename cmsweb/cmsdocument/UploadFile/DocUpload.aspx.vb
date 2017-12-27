Imports NetReusables
Imports System.IO
Imports Unionsoft.Platform


Partial Class UploadFile_DocUpload
    Inherits CmsPage

    Protected ResID As String = ""
    Protected pst As New CmsPassport

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        pst = CmsPass

        If Request("mnuresid") IsNot Nothing Then ResID = Request("mnuresid")
        If IsPostBack Then Return
        Session("UPID") = TimeId.CurrentMilliseconds(30)
    End Sub


    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim IsUpDirectory As Boolean = False
        Dim DocHostName As String = CmsDatabase.DocDatabase
        If Not Session("UPID") Is Nothing Then
            If CmsDatabase.FileUploadMode = 1 Then
                IsUpDirectory = True
            End If
            Dim UploadID As String = CType(Session("UPID"), String)
            Dim strSql As String = "select * from FileLOG where UploadID=" + UploadID.Trim
            Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
            strSql = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim DOCID As String = DbField.GetStr(dt.Rows(i), "DocID")
                Dim hst As New Hashtable 
                If CmsDatabase.FileUploadMode = 0 Then
                    Dim Buffer As Byte() = GetFile(DbField.GetStr(dt.Rows(i), "filePath"))
                    Document(DOCID, DbField.GetStr(dt.Rows(i), "CONTENT"), DbField.GetStr(dt.Rows(i), "FILEEXT"), DbField.GetInt(dt.Rows(i), "FileSize"), Buffer, DocHostName)
                Else
                    Dim strToFilePath As String = CmsConfig.ProjectRootFolder + "UploadFile\" + ResID
                    If Not System.IO.Directory.Exists(strToFilePath) Then
                        System.IO.Directory.CreateDirectory(strToFilePath)
                    End If 
                    strToFilePath += "\" + DOCID.Trim + "." + DbField.GetStr(dt.Rows(i), "FILEEXT") 
                    DocHostName = "/UploadFile/" + ResID + "/" + DOCID.Trim + "." + DbField.GetStr(dt.Rows(i), "FILEEXT")
                    File.Copy(DbField.GetStr(dt.Rows(i), "filePath"), strToFilePath)
                End If
                DocumentCenter(DOCID, DbField.GetStr(dt.Rows(i), "CONTENT"), DbField.GetStr(dt.Rows(i), "FILEEXT"), DbField.GetInt(dt.Rows(i), "FileSize"), DocHostName)
                InsertRecord(DOCID)
            Next
            SDbStatement.Execute(strSql + "delete FileLOG where UploadID=" + UploadID.Trim + ";")
        End If
        Response.Write("<script>window.parent.location.href=window.parent.location.href;</script>")
    End Sub


    Private Function GetFile(ByVal strFilePath As String) As Byte() 
        Dim bytDOC2_FILE_BIN As Byte()
        Dim fs As FileStream = New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
        Dim br As BinaryReader = New BinaryReader(fs)
        bytDOC2_FILE_BIN = br.ReadBytes(CInt(fs.Length))
        br.Close()
        fs.Close()
        Return bytDOC2_FILE_BIN
    End Function

    Private Sub Document(ByVal DocID As String, ByVal strFileName As String, ByVal strFileExt As String, ByVal intFileSize As Integer, ByVal Buffer As Byte(), ByVal DocHostName As String)
        Dim hst As Hashtable = New Hashtable
        hst.Add("DOC2_ID", DocID.ToString)
        hst.Add("DOC2_CRTID", pst.Employee.ID.Trim)
        hst.Add("DOC2_EDTID", pst.Employee.ID.Trim)
        hst.Add("DOC2_CRTTIME", DateTime.Now)
        hst.Add("DOC2_EDTTIME", DateTime.Now)
        hst.Add("DOC2_FILE_BIN", Buffer)
        hst.Add("DOC2_NAME", strFileName)
        hst.Add("DOC2_EXT", strFileExt)
        hst.Add("DOC2_SIZE", intFileSize)
        hst.Add("DOC2_RESID1", ResID)
        SDbStatement.InsertRow(hst, DocHostName)
    End Sub

    Private Sub DocumentCenter(ByVal DocID As String, ByVal strFileName As String, ByVal strFileExt As String, ByVal intFileSize As Integer, ByVal DocHostName As String)
        Dim hst As Hashtable = New Hashtable
        hst.Add("DocHostName", CmsDatabase.DocDatabase)
        hst.Add("IsUpDirectory", CType(CmsDatabase.FileUploadMode, Integer))
        hst("DOC2_ID") = DocID '唯一的记录ID
        hst("DOC2_RESID1") = ResID '隶属的资源ID
        hst("DOC2_CRTID") = pst.Employee.ID '创建人员ID
        hst("DOC2_CRTTIME") = Date.Now '创建时间
        hst("DOC2_EDTID") = pst.Employee.ID '最后修改人员ID
        hst("DOC2_EDTTIME") = DateTime.Now   '最后修改时间
        hst("DOC2_NAME") = strFileName
        hst("DOC2_EXT") = strFileExt
        hst("DOC2_SIZE") = intFileSize
        hst("DOC2_COMPRESSED_SIZE") = 0 '文档SIZE，压缩后大小（默认不显示状态）
        hst("DOC2_COMPRESSED_RATE") = 0 '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%
        hst("DOC2_COMMENTS") = Me.txtCOMMENTS.Text.Trim
        hst("DOC2_KEYWORDS") = Me.txtKEYWORDS.Text.Trim
        hst("DOC2_STATUS") = ""
        hst("DOC2_CONVERTED") = 0 '初始值必为0
        hst("DOC2_SHARES") = 1 '初始值必为1
        hst("DOC2_LCYC_ENABLE") = 0  '启动文档生命周期管理。0或其它值：不启动（系统默认值）1：启动注：签出状态的文档既便已过生命周期，也不会被删除。
        hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        SDbStatement.InsertRow(hst, "Cms_DocumentCenter")
    End Sub

    Private Sub InsertRecord(ByVal DOCID As String)
        Dim hst As New Hashtable
        hst.Add("ID", NetReusables.TimeId.CurrentMilliseconds(30))
        hst.Add("DOCID", DOCID)
        hst.Add("ResID", ResID)
        hst.Add("RELID", 0)
        SDbStatement.InsertRow(hst, ResName)
    End Sub


    Public Property ResName() As String
        Get
            If ViewState("resname") Is Nothing Then ViewState("resname") = Unionsoft.Platform.ResFactory.ResService.GetResTable(pst, Convert.ToInt64(ResID)) 'ViewState("resname") = pst.HostResData.ResTable ' GetResName()
            Return CType(ViewState("resname"), String)
        End Get
        Set(ByVal Value As String)
            ViewState("resname") = Value
        End Set
    End Property

End Class
