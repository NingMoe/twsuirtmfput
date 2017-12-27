Imports NetReusables
Imports System.Data
Partial Class ShowDocument
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim docid As String = Request.QueryString("docid")
        Dim tableName As String = Request.QueryString("tableName")
        If docid IsNot Nothing And tableName IsNot Nothing Then
            Transfer(docid, tableName)
        End If


    End Sub


    Private Sub Transfer(ByVal docid As String, ByVal tableName As String)


        Dim sql As String = "select doc2_name,doc2_ext,doc2_file_bin,DOC2_COMPRESSED_SIZE from " + tableName + " where doc2_id=" + docid
        Dim dt As DataTable = SDbStatement.Query(sql).Tables(0)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim FileName As String = dr("doc2_name") + "." + dr("doc2_ext")
            Dim filebyte As Byte() = dr("doc2_file_bin")
            If DbField.GetLng(dr, "DOC2_COMPRESSED_SIZE") > 0 Then '未压缩
                filebyte = ZipFile.UnCompressFile(filebyte)
            End If
            Response.ContentType = checktype(dr("doc2_ext"))
            FileName = HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)

            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)


            Response.BinaryWrite(filebyte)
            Response.End()

        End If

    End Sub
    Private Function checktype(ByVal extName As String) As String
        Dim ContentType As String
        Select Case extName.ToLower()
            Case "asf"
                ContentType = "video/x-ms-asf "
                Exit Select
            Case "avi"
                ContentType = "video/avi "
                Exit Select
            Case "doc"
                ContentType = "application/msword "
                Exit Select
            Case "zip"
                ContentType = "application/zip "
                Exit Select
            Case "xls"
                ContentType = "application/vnd.ms-excel "
                Exit Select
            Case "gif"
                ContentType = "image/gif "
                Exit Select
            Case "jpg"
                ContentType = "image/jpeg "
                Exit Select
            Case "jpeg"
                ContentType = "image/jpeg "
                Exit Select
            Case "wav"
                ContentType = "audio/wav "
                Exit Select
            Case "mp3 "
                ContentType = "audio/mpeg3 "
                Exit Select
            Case "mpg"
                ContentType = "video/mpeg "
                Exit Select
            Case "mepg"
                ContentType = "video/mpeg "
                Exit Select
            Case "rtf"
                ContentType = "application/rtf "
                Exit Select
            Case "html"
                ContentType = "text/html "
                Exit Select
            Case "htm"
                ContentType = "text/html "
                Exit Select
            Case "txt"
                ContentType = "text/plain "
                Exit Select
            Case "pdf"
                ContentType = "application/pdf"
            Case Else
                ContentType = "application/octet-stream "
                Exit Select
        End Select
        Return ContentType
    End Function

End Class
