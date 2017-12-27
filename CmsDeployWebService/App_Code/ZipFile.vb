Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Collections.Generic
Imports ICSharpCode.SharpZipLib.Zip
<Serializable()> _
Public Class ZipFile

    Const ChunkSize As Long = 524288

    Public Shared Function UnCompressFile(ByRef bytTemp() As Byte) As Byte()
        'SLog.Info("###��ȡ�ĵ�����ѹǰsize=" & bytTemp.Length)
        Dim imsm As MemoryStream = New MemoryStream(bytTemp)
        Dim omsm As MemoryStream = NetReusables.SimpleZip.UnCompressStream(CType(imsm, Stream))
        Dim intLen As Integer = CInt(omsm.Length)
        'SLog.Info("###��ȡ�ĵ�����ѹ��size=" & intLen)
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
        '��¼���༶ѹ������ļ�·��������
        Dim i As Integer = 0, flag As Integer = 1
        'flag��������־λ�����༶���Ʋ�һ��ʱ���ı���ֵ
        Dim czos As ZipOutputStream = Nothing
        While i < AllPath.Count
            Dim entrypath As String = AllPath(i) '.FilePath + "\" + AllPath(i).FileName.Trim
            ' Dim entryname As String = AllPath(i).classname
            Dim strName As String = System.IO.Path.GetFileName(entrypath)




            '����·��ȡ�ļ���
            Dim fs As FileStream = File.OpenRead(entrypath)
            'FileStream fs = new FileStream(entrypath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);                           

            'If (i > 0) AndAlso (AllPath(i - 1).classname <> entryname) Then
            '    '�����µİ༶ʱ���رղ������һ��ѹ��
            '    flag = 0

            '    czos.Finish()
            '    czos.Close()
            'End If
            If i = 0 OrElse flag = 0 Then
                '��ѹ��������ӵ�һ���ļ�ʱ���߳����µİ༶ʱ��newһ���µ�ѹ����
                If Not System.IO.Directory.Exists(MapPath + "\" + strTemp) Then
                    System.IO.Directory.CreateDirectory(MapPath + "\" + strTemp)
                End If
                ZipFile = MapPath + "\" + strTemp + "\" + ZipFileName & ".rar" 'entrypath.Replace(strName, ZipFileName & ".rar")
                '���ļ����滻Ϊ�༶����
                czos = New ZipOutputStream(File.Create(ZipFile))

                '  classpath.Add(zipcname)
                '��¼���༶�����ѹ������·��������
                '���ñ�־λ=1
                flag = 1
            End If

            Dim entry As New ZipEntry(strName)
            czos.PutNextEntry(entry)
            'ͬһ���༶ʱ����ѹ���������ѹ��ʵ��
            Dim buffer As Byte() = New Byte(ChunkSize - 1) {}
            Dim fslenght As Long = fs.Length
            While fslenght > 0
                Dim lRead As Integer = fs.Read(buffer, 0, Convert.ToInt32(ChunkSize))
                '��ȡ�Ĵ�С
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
