Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Collections.Generic
Imports ICSharpCode.SharpZipLib.Zip
<Serializable()> _
Public Class ZipFile

    Const ChunkSize As Long = 524288

    Public Shared Function UnCompressFile(ByRef bytTemp() As Byte) As Byte()
        'SLog.Info("###获取文档，解压前size=" & bytTemp.Length)
        Dim imsm As MemoryStream = New MemoryStream(bytTemp)
        Dim omsm As MemoryStream = NetReusables.SimpleZip.UnCompressStream(CType(imsm, Stream))
        Dim intLen As Integer = CInt(omsm.Length)
        'SLog.Info("###获取文档，解压后size=" & intLen)
        Dim binFile(intLen - 1) As Byte
        omsm.Seek(0, SeekOrigin.Begin)
        If omsm.CanRead Then omsm.Read(binFile, 0, intLen)
        imsm.Close()
        omsm.Close()
        Return binFile
    End Function

    'Public Sub Compreassmtfile(ByVal HttpResponse As HttpResponse, ByVal AllPath As ArrayList, ByVal ZipFileName As String)
    '    Dim ms As MemoryStream = New MemoryStream()
    '    Dim strFilePath As String = ZipClass(AllPath, ZipFileName)

    '    Dim bytDOC2_FILE_BIN As Byte()
    '    Dim fs As FileStream = New FileStream(strFilePath, FileMode.Open, FileAccess.Read)
    '    Dim br As BinaryReader = New BinaryReader(fs)
    '    bytDOC2_FILE_BIN = br.ReadBytes(CInt(fs.Length))
    '    br.Close()
    '    fs.Close()

    '    HttpResponse.AddHeader("Content-Disposition", "inline;filename=" & HttpUtility.UrlEncode(ZipFileName & ".rar"))
    '    HttpResponse.ContentType = "application/octet-stream"
    '    HttpResponse.BinaryWrite(bytDOC2_FILE_BIN)
    '    HttpResponse.Flush()
    '    bytDOC2_FILE_BIN = Nothing
    '    HttpResponse.[End]()
    'End Sub



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="AllPath"></param>
    ''' <param name="ZipFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ZipClass(ByVal AllPath As ArrayList, ByVal ZipFileName As String) As String
        Dim ZipFile As String = ""
        Dim strTemp As String = "temp"

        Dim MapPath As String = System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath)


        '  Dim classpath As New ArrayList()
        '记录按班级压缩后的文件路径和名称
        Dim i As Integer = 0, flag As Integer = 1
        'flag用来做标志位，当班级名称不一致时，改变其值
        Dim czos As ZipOutputStream = Nothing
        While i < AllPath.Count
            Dim entrypath As String = AllPath(i) '.FilePath + "\" + AllPath(i).FileName.Trim
            ' Dim entryname As String = AllPath(i).classname
            Dim strName As String = System.IO.Path.GetFileName(entrypath)




            '根据路径取文件名
            Dim fs As FileStream = File.OpenRead(entrypath)
            'FileStream fs = new FileStream(entrypath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);                           

            'If (i > 0) AndAlso (AllPath(i - 1).classname <> entryname) Then
            '    '出现新的班级时，关闭并完成上一个压缩
            '    flag = 0

            '    czos.Finish()
            '    czos.Close()
            'End If
            If i = 0 OrElse flag = 0 Then
                '向压缩流中添加第一个文件时或者出现新的班级时，new一个新的压缩流
                If Not System.IO.Directory.Exists(MapPath + "\" + strTemp) Then
                    System.IO.Directory.CreateDirectory(MapPath + "\" + strTemp)
                End If
                ZipFile = MapPath + "\" + strTemp + "\" + ZipFileName & ".rar" 'entrypath.Replace(strName, ZipFileName & ".rar")
                '将文件名替换为班级名称
                czos = New ZipOutputStream(File.Create(ZipFile))

                '  classpath.Add(zipcname)
                '记录按班级打包的压缩包的路径和名称
                '重置标志位=1
                flag = 1
            End If

            Dim entry As New ZipEntry(strName)
            czos.PutNextEntry(entry)
            '同一个班级时，向压缩流中添加压缩实体
            Dim buffer As Byte() = New Byte(ChunkSize - 1) {}
            Dim fslenght As Long = fs.Length
            While fslenght > 0
                Dim lRead As Integer = fs.Read(buffer, 0, Convert.ToInt32(ChunkSize))
                '读取的大小
                czos.Write(buffer, 0, lRead)
                fslenght = fslenght - lRead
            End While

            fs.Close()
            i += 1
        End While
        czos.Finish()
        czos.Close()
        Dim FilePath As String = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath
        Dim server As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Name")
        Dim port As String = System.Web.HttpContext.Current.Request.ServerVariables("Server_Port")
        If port = "80" Then
            FilePath = "http://" + server + System.Web.HttpContext.Current.Request.ApplicationPath & ""
        Else
            FilePath = "http://" + server + ":" + port + System.Web.HttpContext.Current.Request.ApplicationPath & ""
        End If
        FilePath = FilePath + "/" + strTemp + "/" + ZipFileName & ".rar"
        Return FilePath
        'ZipEntry(classpath)
    End Function

End Class
