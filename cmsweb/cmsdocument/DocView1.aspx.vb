Imports System.Text
Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform

    Partial Class cmsdocument_DocView
        Inherits System.Web.UI.Page



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim docid As String = Request.QueryString("docid")
            If docid IsNot Nothing Then

                Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(docid)

                If datDoc.lngDOCID > 0 Then
                    Dim strUrl As String = ""
                    Dim strFileExt As String = datDoc.strDOC2_EXT.Trim.ToUpper()
                    If strFileExt = "DOC" Or strFileExt = "XLS" Or strFileExt = "PPT" Or strFileExt = "CSV" Or strFileExt = "DOCX" Or strFileExt = "XLSX" Or strFileExt = "PPTX" Then
                        strUrl = "/cmsweb/cmsdocument/OfficeEditor.aspx?DOCID=" + datDoc.lngDOCID.ToString
                    ElseIf strFileExt = "PDF" Then
                        strUrl = "/cmsweb/cmsdocument/DocViewPdf.aspx?DOCID=" + datDoc.lngDOCID.ToString
                    ElseIf strFileExt = "DWG" Then
                        strUrl = "/cmsweb/cmsdocument/DocViewCAD.aspx?DOCID=" + datDoc.lngDOCID.ToString
                ElseIf strFileExt = "JPG" Or strFileExt = "JPEG" Or strFileExt = "GIF" Or strFileExt = "PNG" Or strFileExt = "BMP" Then
                    img1.Visible = True
                    img1.ImageUrl = CMSDocument.CreateDocumentOnServer(datDoc)

                    Return
                    ElseIf strFileExt = "TXT" Then
                        Dim FilePath As String = ""
                       FilePath = CMSDocument.CreateDocumentOnServer(datDoc)

                        FilePath = FilePath.Replace(CmsConfig.WebApplicationPath, CmsConfig.ProjectRootFolder).Replace("/", "\")
                        Dim sr As New StreamReader(FilePath, Encoding.GetEncoding("GB2312"))
                        lblTxt.Visible = True
                        Dim contents As String = sr.ReadToEnd()
                        lblTxt.Text = contents.Replace(vbCrLf, "<br>")
                        Return
                    Else
                    MsgBox("该文件类型不支持在线浏览")
                        Return
                    End If
                    Response.Redirect(strUrl, False)
                End If

            End If


        End Sub






    Public Function CreateDocumentOnServer(ByVal docid As String, ByRef strFileExt As String, ByRef hashOnlineViewFiles As Hashtable) As String
        Try
           
            Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(docid)

            strFileExt = datDoc.strDOC2_EXT
            Dim strExtUpcase As String = datDoc.strDOC2_EXT.ToUpper()
            'Dim strFileCanOnlineView As String = CmsConfig.GetString("SYS_CONFIG", "FILE_ONLINEVIEW").ToUpper()
            'Dim alistFileCanOnlineView As ArrayList = StringDeal.Split(strFileCanOnlineView, ",")
            If HashField.ContainsKey(hashOnlineViewFiles, strExtUpcase) Then '处理支持在线只读浏览的文件类型
                '获取临时文档目录
                Dim strTemp As String = CmsConfig.GetString("SYS_CONFIG", "DOC_ONLINEVIEW_FOLDER")
                strTemp = StringDeal.Trim(strTemp, "\", "\")
                Dim strDocFolder As String = CmsConfig.ProjectRootFolder & strTemp & "\" & datDoc.lngDOCID & "\"
                If Directory.Exists(strDocFolder) = False Then
                    Directory.CreateDirectory(strDocFolder)
                End If

                '获取临时文档全路径，并生产文档
                Dim strFileName As String
                Dim strFileExtToChange As String = "" 'CmsConfig.GetString("SYS_CONFIG", "FILE_TO_CHANGE_EXT").ToUpper()
                Dim alistFileExtToChange As ArrayList = StringDeal.Split(strFileExtToChange, ",")
                If Not alistFileExtToChange Is Nothing AndAlso alistFileExtToChange.Contains(strExtUpcase) Then
                    'Dim strDate As String = DateString.Replace("-", "")
                    'strFileName = "clientcache__" & strDate & "___" & datDoc.lngDOCID & "." & datDoc.strDOC2_EXT & "mnxyz.jpg" '故意加后缀mnxyz.jpg，为了支持任何类型的文件
                    strFileName = "cc__" & datDoc.lngDOCID & "." & datDoc.strDOC2_EXT & "mnxyz.jpg" '故意加后缀mnxyz.jpg，为了支持任何类型的文件
                Else
                    '''''''''''''''''''''2010-7-14 tq (原strFileName = datDoc.strDOC2_NAME.ToString() & "." & datDoc.strDOC2_EXT) '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    strFileName = datDoc.lngDOCID.ToString() & "." & datDoc.strDOC2_EXT
                End If
                Dim strFilePath As String = strDocFolder & strFileName
                Dim fs As FileStream = New FileStream(strFilePath, FileMode.Create, FileAccess.Write)
                Dim binFile As Byte() = CType(datDoc.bytDOC2_FILE_BIN, Byte())
                fs.Write(binFile, 0, binFile.Length)
                fs.Flush()
                fs.Close()

                '获取临时文档的URL全路径
                'Dim strWebHost As String = CmsConfig.GetString("SYS_DATABASE", "WEBHOST")
                'If strWebHost.ToUpper().StartsWith("HTTP") = False Then strWebHost = "http://" & strWebHost
                'If strWebHost.EndsWith("/") = False Then strWebHost &= "/"
                Dim strUrlPath As String = CmsConfig.GetString("SYS_CONFIG", "DOC_ONLINEVIEW_URLPATH")
                strUrlPath = StringDeal.Trim(strUrlPath, "/", "/")
                'Dim strFileUrlPath As String = strWebHost & strUrlPath & "/" & datDoc.lngDOCID & "/" & strFileName
                Dim strFileUrlPath As String = strUrlPath & "/" & datDoc.lngDOCID & "/" & strFileName

                Return strFileUrlPath
            Else '不支持的文档类型用浏览器默认方式打开
                'Dim datDoc2 As DataDocument = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetDocument(CmsPass, lngResID, lngRecID, True)
                'FileTransfer.DownloadDoc(Response, datDoc2)
               FileTransfer.ShowDoc(Response, datDoc)
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

        '----------------------------------------------------------
        '获取：可在线浏览的文档类型，以及对应的处理页面
        '----------------------------------------------------------
        Private Function GetOnlineViewFiles() As Hashtable
            Dim hashOnlineViewFiles As New Hashtable

            Dim datSvc As DataServiceSection = CmsConfig.GetConfig()
            Dim strKeys As ArrayList = datSvc.GetKeys("SYS_ONLINEVIEW_FILES")
            Dim strOneKey As String
            For Each strOneKey In strKeys
                Dim strOneFileType As String = datSvc.GetKeyAttr("SYS_ONLINEVIEW_FILES", strOneKey, "FILE_TYPE")
                Dim strUrl As String = datSvc.GetString("SYS_ONLINEVIEW_FILES", strOneKey)
                hashOnlineViewFiles.Add(strOneFileType, strUrl)
            Next
            Return hashOnlineViewFiles
        End Function
    End Class


