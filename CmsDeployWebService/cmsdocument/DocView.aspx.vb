Imports System.Text
Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform

    Partial Class cmsdocument_DocView
        Inherits System.Web.UI.Page

    Public FlashFile As String

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim AllExt As String = "DOC,XLS,PPT,CSV,DOCX,XLSX,PPTX,PDF,JPG,JPEG,GIF,PNG,TXT"
        Dim FilePath As String = ""
            Dim docid As String = Request.QueryString("docid")
            If docid IsNot Nothing Then

            Dim datDoc As DocmentInfo = Document.GetDocumentByID(docid)

            ' ResFactory.TableService("DOC").GetDocument(docid)
            If datDoc Is Nothing Then
                Response.Write("文档不存在")
                Response.End()
            End If

            Dim strUrl As String = ""
            Dim strFileExt As String = datDoc.Ext.Trim.ToUpper()
            If strFileExt = "" Then
                MsgBox("未知名的文件类型")
                Return
            End If
            If AllExt.Contains(strFileExt) Then

                FilePath = Common.GetServerPath + datDoc.DownLoadPath
                FilePath = Server.MapPath("../uploadfile") + datDoc.DownLoadPath.Replace("/", "\").ToUpper.Replace("\UPLOADFILE", "")
                Dim SwfPath As String = datDoc.DownLoadPath.Replace("." + datDoc.Ext, ".swf").ToUpper.Replace("/UPLOADFILE", "")
                Dim PhysicalFlashPath As String = Server.MapPath("../uploadFlash") + SwfPath.Replace("/", "\")


                If File.Exists(PhysicalFlashPath) Then
                    FlashFile = "../uploadFlash" + SwfPath
                Else
                    Dim flashServer As Print2Flash3.Server2 = New Print2Flash3.Server2

                    Dim i As Integer = PhysicalFlashPath.LastIndexOf("\")


                    If Directory.Exists(PhysicalFlashPath.Substring(0, i)) = False Then
                        Directory.CreateDirectory(PhysicalFlashPath.Substring(0, i))
                    End If
                    Try
                        flashServer.ConvertFile(FilePath, PhysicalFlashPath)
                        FlashFile = "../uploadFlash" + SwfPath
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    End Try

                End If



            End If

        End If




        End Sub

   




    'Public Function CreateDocumentOnServer(ByVal docid As String, ByRef strFileExt As String, ByRef hashOnlineViewFiles As Hashtable) As String
    '    Try

    '        Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(docid)

    '        strFileExt = datDoc.strDOC2_EXT
    '        Dim strExtUpcase As String = datDoc.strDOC2_EXT.ToUpper()
    '        'Dim strFileCanOnlineView As String = CmsConfig.GetString("SYS_CONFIG", "FILE_ONLINEVIEW").ToUpper()
    '        'Dim alistFileCanOnlineView As ArrayList = StringDeal.Split(strFileCanOnlineView, ",")
    '        If HashField.ContainsKey(hashOnlineViewFiles, strExtUpcase) Then '处理支持在线只读浏览的文件类型
    '            '获取临时文档目录
    '            Dim strTemp As String = CmsConfig.GetString("SYS_CONFIG", "DOC_ONLINEVIEW_FOLDER")
    '            strTemp = StringDeal.Trim(strTemp, "\", "\")
    '            Dim strDocFolder As String = CmsConfig.ProjectRootFolder & strTemp & "\" & datDoc.lngDOCID & "\"
    '            If Directory.Exists(strDocFolder) = False Then
    '                Directory.CreateDirectory(strDocFolder)
    '            End If

    '            '获取临时文档全路径，并生产文档
    '            Dim strFileName As String
    '            Dim strFileExtToChange As String = "" 'CmsConfig.GetString("SYS_CONFIG", "FILE_TO_CHANGE_EXT").ToUpper()
    '            Dim alistFileExtToChange As ArrayList = StringDeal.Split(strFileExtToChange, ",")
    '            If Not alistFileExtToChange Is Nothing AndAlso alistFileExtToChange.Contains(strExtUpcase) Then
    '                'Dim strDate As String = DateString.Replace("-", "")
    '                'strFileName = "clientcache__" & strDate & "___" & datDoc.lngDOCID & "." & datDoc.strDOC2_EXT & "mnxyz.jpg" '故意加后缀mnxyz.jpg，为了支持任何类型的文件
    '                strFileName = "cc__" & datDoc.lngDOCID & "." & datDoc.strDOC2_EXT & "mnxyz.jpg" '故意加后缀mnxyz.jpg，为了支持任何类型的文件
    '            Else
    '                '''''''''''''''''''''2010-7-14 tq (原strFileName = datDoc.strDOC2_NAME.ToString() & "." & datDoc.strDOC2_EXT) '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '                strFileName = datDoc.lngDOCID.ToString() & "." & datDoc.strDOC2_EXT
    '            End If
    '            Dim strFilePath As String = strDocFolder & strFileName
    '            Dim fs As FileStream = New FileStream(strFilePath, FileMode.Create, FileAccess.Write)
    '            Dim binFile As Byte() = CType(datDoc.bytDOC2_FILE_BIN, Byte())
    '            fs.Write(binFile, 0, binFile.Length)
    '            fs.Flush()
    '            fs.Close()

    '            '获取临时文档的URL全路径
    '            'Dim strWebHost As String = CmsConfig.GetString("SYS_DATABASE", "WEBHOST")
    '            'If strWebHost.ToUpper().StartsWith("HTTP") = False Then strWebHost = "http://" & strWebHost
    '            'If strWebHost.EndsWith("/") = False Then strWebHost &= "/"
    '            Dim strUrlPath As String = CmsConfig.GetString("SYS_CONFIG", "DOC_ONLINEVIEW_URLPATH")
    '            strUrlPath = StringDeal.Trim(strUrlPath, "/", "/")
    '            'Dim strFileUrlPath As String = strWebHost & strUrlPath & "/" & datDoc.lngDOCID & "/" & strFileName
    '            Dim strFileUrlPath As String = strUrlPath & "/" & datDoc.lngDOCID & "/" & strFileName

    '            Return strFileUrlPath
    '        Else '不支持的文档类型用浏览器默认方式打开
    '            'Dim datDoc2 As DataDocument = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetDocument(CmsPass, lngResID, lngRecID, True)
    '            'FileTransfer.DownloadDoc(Response, datDoc2)
    '            If datDoc.IsUpDirectory Then
    '                Response.Redirect(CmsConfig.WebApplicationPath + datDoc.UpFilePath)
    '            Else
    '                FileTransfer.ShowDoc(Response, datDoc)
    '            End If
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function

    '    '----------------------------------------------------------
    '    '获取：可在线浏览的文档类型，以及对应的处理页面
    '    '----------------------------------------------------------
    '    Private Function GetOnlineViewFiles() As Hashtable
    '        Dim hashOnlineViewFiles As New Hashtable

    '        Dim datSvc As DataServiceSection = CmsConfig.GetConfig()
    '        Dim strKeys As ArrayList = datSvc.GetKeys("SYS_ONLINEVIEW_FILES")
    '        Dim strOneKey As String
    '        For Each strOneKey In strKeys
    '            Dim strOneFileType As String = datSvc.GetKeyAttr("SYS_ONLINEVIEW_FILES", strOneKey, "FILE_TYPE")
    '            Dim strUrl As String = datSvc.GetString("SYS_ONLINEVIEW_FILES", strOneKey)
    '            hashOnlineViewFiles.Add(strOneFileType, strUrl)
    '        Next
    '        Return hashOnlineViewFiles
    '    End Function
    End Class


