Imports NetReusables
Imports Unionsoft.Platform
Imports System.IO
Partial Class cmsdocument_UploadFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim AllExt As String = "DOC,XLS,PPT,CSV,DOCX,XLSX,PPTX,PDF,JPG,JPEG,GIF,PNG,TXT"
        Dim FilePath As String = ""
        Dim docid As String = Request.QueryString("docid")
        If docid IsNot Nothing Then

            Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(docid)

            If datDoc.lngDOCID > 0 Then
                Dim strUrl As String = ""
                Dim strFileExt As String = datDoc.strDOC2_EXT.Trim.ToUpper()
                Dim FileName As String = datDoc.strDOC2_NAME + "." + strFileExt
                If strFileExt = "" Then
                    MsgBox("未知名的文件类型")
                    Return
                End If
                If AllExt.Contains(strFileExt) Then

                    FilePath = Common.GetServerPath + datDoc.UpFilePath
                    FilePath = Server.MapPath("../uploadfile") + datDoc.UpFilePath.Replace("/", "\").ToUpper.Replace("\UPLOADFILE", "")

                    Dim fs As FileStream = New IO.FileStream(FilePath, FileMode.Open)

                    'Dim binFile As Byte() = CType(datDoc.bytDOC2_FILE_BIN, Byte())
                    'fs.Write(binFile, 0, binFile.Length)
                    'fs.Flush()
                    'fs.Close()
                    Response.Write(fs.ReadByte)

                End If

            End If

        End If
    End Sub
End Class
