Imports Microsoft.VisualBasic

Imports Unionsoft.Platform
Imports NetReusables
Imports System.IO
Public Class CMSDocument

    Public Shared Function CreateDocumentOnServer(ByVal datDoc As DataDocument, Optional ByVal IsWebApplicationPath As Boolean = True) As String
        Try
            '��ȡ��ʱ�ĵ�Ŀ¼
            Dim strTemp As String = CmsConfig.GetString("SYS_CONFIG", "DOC_ONLINEVIEW_FOLDER")
            strTemp = StringDeal.Trim(strTemp, "\", "\")
            Dim strDocFolder As String = CmsConfig.ProjectRootFolder & strTemp & "\" & datDoc.lngDOCID & "\"
            If Directory.Exists(strDocFolder) = False Then
                Directory.CreateDirectory(strDocFolder)
            End If

            '��ȡ��ʱ�ĵ�ȫ·�����������ĵ�
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

            Dim strUrlPath As String = CmsConfig.WebApplicationPath & strTemp & "/" & datDoc.lngDOCID & "/"
            strUrlPath = StringDeal.Trim(strUrlPath, "/", "/").Replace("\", "/")
            Dim strFileUrlPath As String = strUrlPath & "/" & strFileName
            Return strFileUrlPath
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
