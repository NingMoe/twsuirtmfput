Option Strict On
Option Explicit On
Imports System.Text
Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Partial Class DocumentView
    Inherits System.Web.UI.Page
    Public FlashFile As String
    Public strFileUrlPath As String
    Public FilePath As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
 
        Dim docid As String = "195045299393"

        If Request("DOCID") IsNot Nothing Then docid = Request("DOCID")


        CreateDocumentOnServer(docid)

    End Sub
 

    Public Function CreateDocumentOnServer(ByVal docid As String) As String
        Dim datDoc As New DataDocument
        Try
            datDoc = ResFactory.TableService("DOC").GetDocument(docid)
            
        Catch ex As Exception
            SLog.Err(ex.Message)
            Response.Write("<script>alert('文档可能已损坏，请与管理员联系！');</script>")
        End Try


       


        Try
            FilePath = CMSDocument.CreateDocumentOnServer(datDoc, False)

            If datDoc.strDOC2_EXT.ToLower = "pdf" Then
                Response.Redirect(FilePath)
            Else
                Dim SwfPath As String = FilePath.Replace("." + datDoc.strDOC2_EXT, ".swf").ToUpper.Replace("/UPLOADFILE", "")
                SwfPath = "/" + datDoc.lngDOCID.ToString + "/" + datDoc.lngDOCID.ToString + ".swf"
                Dim PhysicalFlashPath As String = Server.MapPath("uploadFlash") + SwfPath.Replace("/", "\")

                If File.Exists(PhysicalFlashPath) Then
                    FlashFile = "uploadFlash" + SwfPath
                Else
                    Dim flashServer As Print2Flash3.Server2 = New Print2Flash3.Server2
                    Dim i As Integer = PhysicalFlashPath.LastIndexOf("\")
                    If Directory.Exists(PhysicalFlashPath.Substring(0, i)) = False Then
                        Directory.CreateDirectory(PhysicalFlashPath.Substring(0, i))
                    End If
                    If File.Exists(FilePath) Then
                        flashServer.ConvertFile(FilePath, PhysicalFlashPath)
                        FlashFile = "uploadFlash" + SwfPath
                    Else
                        SLog.Err(FilePath & "文件不存在")
                        Response.Write("<script>alert('文档可能已损坏，请与管理员联系！');</script>")
                    End If
                End If
            End If

           
        Catch ex As Exception
            SLog.Err(ex.Message)
            If ex.Message.Contains("系统找不到指定的路径") Then
                Response.Write("<script>alert('文档可能已损坏，请与管理员联系！');</script>")
            Else
                Response.Write("<script>alert('没有找到相应组件，请与管理员联系！');</script>")
            End If
        End Try
    End Function

    ''----------------------------------------------------------
    ''获取：可在线浏览的文档类型，以及对应的处理页面
    ''----------------------------------------------------------
    'Private Function GetOnlineViewFiles() As Hashtable
    '    Dim hashOnlineViewFiles As New Hashtable

    '    Dim datSvc As DataServiceSection = CmsConfig.GetConfig()
    '    Dim strKeys As ArrayList = datSvc.GetKeys("SYS_ONLINEVIEW_FILES")
    '    Dim strOneKey As String
    '    For Each strOneKey In strKeys
    '        Dim strOneFileType As String = datSvc.GetKeyAttr("SYS_ONLINEVIEW_FILES", strOneKey, "FILE_TYPE")
    '        Dim strUrl As String = datSvc.GetString("SYS_ONLINEVIEW_FILES", strOneKey)
    '        hashOnlineViewFiles.Add(strOneFileType, strUrl)
    '    Next
    '    Return hashOnlineViewFiles
    'End Function
End Class


