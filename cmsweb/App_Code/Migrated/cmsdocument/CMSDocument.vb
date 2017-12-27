Imports Microsoft.VisualBasic

Imports Unionsoft.Platform
Imports NetReusables
Imports System.IO
Public Class CMSDocument

    Public Shared Function CreateDocumentOnServer(ByVal datDoc As DataDocument, Optional ByVal IsWebApplicationPath As Boolean = True) As String
        Try
            '获取临时文档目录
            Dim strTemp As String = CmsConfig.GetString("SYS_CONFIG", "DOC_ONLINEVIEW_FOLDER")
            strTemp = StringDeal.Trim(strTemp, "\", "\")
            Dim strDocFolder As String = CmsConfig.ProjectRootFolder & strTemp & "\" & datDoc.lngDOCID & "\"
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

            If Not IsWebApplicationPath Then
                Return strFilePath
            End If

            Dim strUrlPath As String = GetServerPath() & "/" & strTemp & "/" & datDoc.lngDOCID & "/"
            strUrlPath = StringDeal.Trim(strUrlPath, "/", "/").Replace("\", "/")

            Dim strFileUrlPath As String = strUrlPath & "/" & strFileName
            Return strFileUrlPath
        Catch ex As Exception
            Return ""
        End Try
    End Function



    Private Shared Function GetServerPath() As String        Dim FilePath As String        Dim server As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Name")        Dim port As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Port")        If port = "80" Then            FilePath = "http://" + server + System.Web.HttpContext.Current.Request.ApplicationPath        Else            FilePath = "http://" + server + ":" + port + System.Web.HttpContext.Current.Request.ApplicationPath        End If        Return FilePath    End Function

End Class
