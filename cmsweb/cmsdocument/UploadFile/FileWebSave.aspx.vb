Imports NetReusables
Imports System.IO
Imports Unionsoft.Platform

Partial Class cmsdocument_FileWebSave
    Inherits CmsPage
    Private _action As String
    Private _Id As String = "0"
    Private _RecId As String = "0"
    Private ResID As Long = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ResID = Convert.ToInt64(Request("resid"))
        doFormUploadDisk1()
    End Sub



    Public Sub doFormUploadDisk1()
        Dim hst As New Hashtable
        Dim UPID As String = ""
        If Not Session("UPID") Is Nothing Then UPID = CType(Session("UPID"), String)

        Dim uploadpath As String
        uploadpath = CmsConfig.ProjectRootFolder
        Dim uploadFiles As System.Web.HttpFileCollection
        Dim theFile As System.Web.HttpPostedFile
        uploadFiles = Request.Files
        Dim strSql As String = ""
        Dim i As Integer

        For i = 0 To uploadFiles.Count - 1

            Dim DOCID As Long = TimeId.CurrentMilliseconds(30)
            theFile = uploadFiles(i)
            Dim filename As String = theFile.FileName.Substring(theFile.FileName.LastIndexOf("\") + 1)
            Dim filetype As String = filename.Substring(filename.LastIndexOf(".") + 1)


            '  strSql += "INSERT INTO [LOG](UploadID,DOCID,CONTENT,FILEEXT,FileSize)VALUES(" + UPID.Trim + "," + DOCID.ToString + ",'" + filename.Substring(0, filename.LastIndexOf(".")) + "','" + filetype.Trim + "'," + theFile.ContentLength.ToString + ");"

            'If CmsDatabase.FileUploadMode = 0 Then
            '    Dim buffer(CType(theFile.InputStream.Length, Integer)) As Byte
            '    Dim br As BinaryReader = New BinaryReader(theFile.InputStream)
            '    br.Read(buffer, 0, buffer.Length)
            '    br.Close()
            '    hst.Clear()
            '    hst.Add("DOC2_ID", DOCID.ToString)
            '    hst.Add("DOC2_CRTID", CmsPass().Employee.ID.Trim)
            '    hst.Add("DOC2_EDTID", CmsPass().Employee.ID.Trim)
            '    hst.Add("DOC2_CRTTIME", DateTime.Now)
            '    hst.Add("DOC2_EDTTIME", DateTime.Now)
            '    hst.Add("DOC2_FILE_BIN", buffer)
            '    hst.Add("DOC2_NAME", filename.Substring(0, filename.LastIndexOf(".")))
            '    hst.Add("DOC2_EXT", filetype)
            '    hst.Add("DOC2_SIZE", theFile.ContentLength)
            '    hst.Add("DOC2_RESID1", ResID)
            '    SDbStatement.InsertRow(hst, CmsDatabase.DocDatabase)
            'Else
            '    If Not Directory.Exists(uploadpath & "\UploadFile\" + ResID.ToString) Then Directory.CreateDirectory(uploadpath & "\UploadFile\" + ResID.ToString)

            '    If File.Exists(uploadpath & "\UploadFile\" & ResID.ToString & "\" & DOCID.ToString & "." & filetype.Trim) Then File.Delete(uploadpath & "\UploadFile\" & ResID.ToString & "\" & DOCID.ToString & "." & filetype.Trim)
            '    theFile.SaveAs(uploadpath & "\UploadFile\" & ResID.ToString & "\" & DOCID.ToString & "." & filetype.Trim)
            'End If

            If Not Directory.Exists(uploadpath & "temp\" + ResID.ToString) Then Directory.CreateDirectory(uploadpath & "\temp\" + ResID.ToString)

            Dim filePath As String = uploadpath & "temp\" & ResID.ToString & "\" & DOCID.ToString & "." & filetype.Trim

            If File.Exists(filePath) Then File.Delete(filePath)

            theFile.SaveAs(filePath)

            strSql += "INSERT INTO [FileLOG](UploadID,DOCID,CONTENT,FILEEXT,FileSize,filePath)VALUES(" + UPID.Trim + "," + DOCID.ToString + ",'" + filename.Substring(0, filename.LastIndexOf(".")) + "','" + filetype.Trim + "'," + theFile.ContentLength.ToString + ",'" + filePath + "');"

            Response.Write(DOCID)
        Next
        If strSql.Trim <> "" Then SDbStatement.Execute(strSql)
    End Sub


End Class
