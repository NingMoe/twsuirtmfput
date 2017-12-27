'-------------------------------------------------------------------------------   
' Description : 将文件保存到数据库
' Interface   :  
' Call        :  
' Author      : CHENYU  
' Date        : 2006-4-5
'-------------------------------------------------------------------------------   
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
'Imports Unionsoft.WebControls.Uploader 'Webb.WAVE.Controls.Upload


Namespace Unionsoft.Workflow.Web



Partial Class FileWebSave
    Inherits UserPageBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            doFormUploadDisk()
        Catch ex As Exception

            Throw New WorkflowException("OFFICE CONTROL 在线保存文件 发生错误!", ex)
        End Try
    End Sub

    Public Sub doFormUploadDisk()
        Dim uploadpath As String
        uploadpath = Request.PhysicalApplicationPath 'Server.MapPath(".").ToString()
        Dim uploadFiles As System.Web.HttpFileCollection
        Dim theFile As System.Web.HttpPostedFile
        uploadFiles = Request.Files

        Dim i As Integer
        For i = 0 To uploadFiles.Count - 1
            theFile = uploadFiles(i)
            If uploadFiles.GetKey(i).ToUpper() = "EDITFILE" Then
                Dim filename As String = theFile.FileName.Substring(theFile.FileName.LastIndexOf("\") + 1)
                Dim params() As String = Split(filename, "_")
                Dim lngResourceID As Long = CLng(params(0))
                Dim lngDocumentID As Long = CLng(params(1))

                '获取原文件的名称
                Dim datDoc As DataDocument = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                Dim strDocumentName As String = datDoc.strDOC2_NAME & "." & datDoc.strDOC2_EXT

                Dim CmsRes As New CmsResource(CurrentUser.Code, CurrentUser.Password)
                CmsRes.CheckInFile(lngResourceID, lngDocumentID, theFile.InputStream, "c:\" & strDocumentName)
            End If
        Next
    End Sub

    '直接从httpstream中取得文件 (有问题的，没做完) 2009/09/14
    Private Sub SaveUploadFile(ByVal sm As Stream)
        Dim iTotalByte, iTotalRead, iReadByte As Integer
        iTotalByte = Request.ContentLength
        iTotalRead = 0
        iReadByte = 0

        Dim sr As StreamReader = New StreamReader(sm)
        Dim buffer(CInt(sm.Length)) As Byte
        iReadByte = sm.Read(buffer, 0, 1024)
        While (iReadByte > 0)
            iReadByte = sm.Read(buffer, 0, 1024)
        End While
        sm.Seek(0, SeekOrigin.Begin)

        Dim strContentType As String = Request.ContentType
        Dim strBuffer As String = sr.ReadToEnd()
        Dim strBoundary As String = "--" + strContentType.Substring(strContentType.LastIndexOf("=") + 1) ', strContentType.Length()
        Dim strArray() As String = Regex.Split(strBuffer, strBoundary)
        Dim strSubString As String
        Dim iBegin As Integer = 0
        Dim iEnd As Integer = 0
        Dim strFieldName As String = ""
        Dim strFieldValue As String = ""
        Dim strFilePath As String = ""
        Dim strFileName As String = ""
        Dim strFileType As String = ""
        Dim bTrue As Boolean = False
        Dim iLocation As Integer = 0

        For iIndex As Integer = 0 To strArray.Length - 1
            strSubString = strArray(iIndex)
            iBegin = strSubString.IndexOf("name=""", 0)
            If (iBegin <> -1) Then
                strFieldName = ""
                strFieldValue = ""
                strFilePath = ""
                strFileName = ""
                strFileType = ""
                iEnd = strSubString.IndexOf("""", iBegin + 6)
                strFieldName = strSubString.Substring(iBegin + 6, iEnd)
                iBegin = strSubString.IndexOf("filename=""", 0)
                If (iBegin <> -1) Then
                    bTrue = True
                End If
                iEnd = strSubString.IndexOf("\r\n\r\n", 0)
                If (bTrue = True) Then
                    '//文件路径
                    strFilePath = strSubString.Substring(iBegin + 10, strSubString.IndexOf("""", iBegin + 10))
                    strFileName = strFilePath.Substring(strFilePath.LastIndexOf("\") + 1)
                    strFileType = strSubString.Substring(strSubString.IndexOf("Content-Type: ") + 14, strSubString.IndexOf("\r\n\r\n"))
                    '//文件数据
                    iBegin = strSubString.IndexOf("\r\n\r\n", iBegin)
                    strFieldValue = strSubString.Substring(iBegin + 4)
                    strFieldValue = strFieldValue.Substring(0, strFieldValue.LastIndexOf("\n") - 1)

                    Dim pFile() As Byte = Encoding.Default.GetBytes(strFieldValue)
                    Dim pFileExtend(pFile.Length) As Byte
                    iLocation = strBuffer.IndexOf("filename=""", iLocation)

                    For kIndex As Integer = iLocation To iTotalByte - 2
                        If (buffer(kIndex) = 13 And buffer(kIndex + 2) = 13) Then
                            iLocation = kIndex + 4
                            Exit For
                        End If
                    Next

                    For nIndex As Integer = 0 To pFile.Length
                        pFileExtend(nIndex) = buffer(iLocation + nIndex)
                    Next

                    Dim fs As FileStream = New FileStream("d:\" & strFileName, FileMode.OpenOrCreate)
                    fs.Write(pFileExtend, 0, pFileExtend.Length)
                End If
            End If
        Next
    End Sub

End Class

End Namespace
