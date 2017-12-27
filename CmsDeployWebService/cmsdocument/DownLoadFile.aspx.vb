Imports NetReusables
Imports Unionsoft.Platform
Imports System.IO

Partial Class cmsdocument_DownLoadFile
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FilePath As String = ""
        Dim docid As String = Request.QueryString("docid")
        If docid IsNot Nothing Then

            Dim datDoc As DocmentInfo = Document.GetDocumentByID(docid)

            If datDoc IsNot Nothing Then
                Dim strUrl As String = ""
                Dim strFileExt As String = datDoc.Ext.ToUpper
                Dim FileName As String = datDoc.Name + "." + strFileExt
                If strFileExt = "" Then
                    MsgBox("未知名的文件类型")
                    Return
                End If
                FilePath = datDoc.DownLoadPath
                FilePath = Server.MapPath("../uploadfile/") + FilePath.Replace("/", "\").ToUpper.Replace("\UPLOADFILE", "")
                Dim fs As FileStream = New IO.FileStream(FilePath, FileMode.Open, FileAccess.Read)
                Dim br As BinaryReader = New BinaryReader(fs)
                Dim filebyte As Byte() = br.ReadBytes(CInt(fs.Length))
                br.Close()
                fs.Close()
                FileName = HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)
                Response.BinaryWrite(filebyte)
                Response.End()
            End If

        End If
    End Sub
End Class
