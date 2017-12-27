Imports NetReusables
Imports System.IO
Imports Unionsoft.Platform

Partial Class cmsdocument_FileDelete
    Inherits CmsPage

    Private DOCID As Long
    Private UploadId As String = "0"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DOCID = Convert.ToInt64(Request("id"))
        'Dim uploadpath As String = CmsConfig.ProjectRootFolder
        'Dim Dir As String = uploadpath & "\UploadFile\" + CmsPass().HostResData.ResID.ToString
        'If CmsDatabase.FileUploadMode = 0 Then
        '    CmsDbStatement.DelRows(SDbConnectionPool.GetDbConfig(), CmsDatabase.DocDatabase.Trim, "DOC2_ID=" & DOCID.ToString)
        'Else
        '    DeleteFolder(Dir, DOCID.ToString)
        'End If

        Dim dtFileLog As DataTable = SDbStatement.Query("select * from FileLOG where  DocID=" + DOCID.ToString).Tables(0)

        For i As Integer = 0 To dtFileLog.Rows.Count - 1
            If File.Exists(DbField.GetStr(dtFileLog.Rows(i), "filePath")) Then
                File.Delete(DbField.GetStr(dtFileLog.Rows(i), "filePath"))
            End If
        Next


        If Not Session("UPID") Is Nothing Then UploadId = CType(Session("UPID"), String)
        SDbStatement.Execute("delete from FileLOG where  DocID=" + DOCID.ToString)
    End Sub


    Public Sub DeleteFolder(ByVal dir As String, ByVal DOCID As String)
        If Directory.Exists(dir) Then
            For Each d As String In Directory.GetFileSystemEntries(dir)
                Dim FileName As String = d.Substring(d.LastIndexOf("\") + 1)
                If FileName.Substring(0, FileName.LastIndexOf(".")) = DOCID.Trim Then
                    File.Delete(d)
                End If
            Next
        End If
    End Sub
End Class
