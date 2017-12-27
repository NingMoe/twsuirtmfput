Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D



''' <summary>
''' ssweb 的摘要说明。
''' </summary>
Public Class ImageProcessing

    ''' <summary>
    ''' 给图片上水印
    ''' </summary>
    ''' <param name="file">输入图片</param>
    ''' <param name="waterFile">水印图片</param>
    ''' <returns>输出图片</returns>
    Public Function MarkWaterII(ByVal file As Byte(), ByVal waterFile As String, ByVal Width As Integer, ByVal Height As Integer) As Byte()
        file = GetThumbnail(file, Width, Height, False)
        Dim lucencyPercent As Integer = 100
        Dim modifyImage As Image = Nothing
        Dim drawedImage As Image = Nothing
        Dim g As Graphics = Nothing
        Dim ms As New MemoryStream()
        Try
            '  建立图形对象
            ms = New MemoryStream()
            ms.Write(file, 0, file.Length)
            modifyImage = Image.FromStream(ms)
            ms.Close()
            drawedImage = Image.FromFile(waterFile, True)
            g = Graphics.FromImage(modifyImage)
            '  获取要绘制图形坐标
            Dim x As Integer = modifyImage.Width - drawedImage.Width
            Dim y As Integer = modifyImage.Height - drawedImage.Height
            '  设置颜色矩阵
            Dim matrixItems As Single()() = {New Single() {1, 0, 0, 0, 0}, New Single() {0, 1, 0, 0, 0}, New Single() {0, 0, 1, 0, 0}, New Single() {0, 0, 0, CSng(lucencyPercent) / 100.0F, 0}, New Single() {0, 0, 0, 0, 1}}
            Dim colorMatrix As New ColorMatrix(matrixItems)
            Dim imgAttr As New ImageAttributes()
            imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
            g.DrawImage(drawedImage, New Rectangle(x, y, drawedImage.Width, drawedImage.Height), 0, 0, drawedImage.Width, drawedImage.Height, _
             GraphicsUnit.Pixel, imgAttr)
            Dim bmp As New Bitmap(modifyImage)
            ms = New MemoryStream()
            bmp.Save(ms, ImageFormat.Jpeg)
        Finally
            modifyImage.Dispose()
            drawedImage.Dispose()
            g.Dispose()
        End Try
        Return ms.ToArray()
    End Function



    ''' <summary>
    ''' 固定图片大小后增加水印
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="waterFile"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <returns></returns>
    Public Function MarkWater(ByVal file As Byte(), ByVal waterFile As String, ByVal Width As Integer, ByVal Height As Integer) As Byte()
        file = GetThumbnail(file, Width, Height, False)
        Dim lucencyPercent As Integer = 100
        Dim modifyImage As Image = Nothing
        Dim drawedImage As Image = Nothing
        Dim g As Graphics = Nothing
        Dim ms As New MemoryStream()
        Try
            '  建立图形对象
            ms = New MemoryStream()
            ms.Write(file, 0, file.Length)
            modifyImage = Image.FromStream(ms)
            ms.Close()
            drawedImage = Image.FromFile(waterFile, True)
            g = Graphics.FromImage(modifyImage)
            '  获取要绘制图形坐标
            Dim x As Integer = modifyImage.Width - drawedImage.Width
            Dim y As Integer = modifyImage.Height - drawedImage.Height



            x = modifyImage.Width - drawedImage.Width
            y = modifyImage.Height - drawedImage.Height

            '  设置颜色矩阵
            Dim matrixItems As Single()() = {New Single() {1, 0, 0, 0, 0}, New Single() {0, 1, 0, 0, 0}, New Single() {0, 0, 1, 0, 0}, New Single() {0, 0, 0, CSng(lucencyPercent) / 100.0F, 0}, New Single() {0, 0, 0, 0, 1}}
            Dim colorMatrix As New ColorMatrix(matrixItems)
            Dim imgAttr As New ImageAttributes()
            imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
            g.DrawImage(drawedImage, New Rectangle(x, y, drawedImage.Width, drawedImage.Height), 0, 0, drawedImage.Width, drawedImage.Height, _
             GraphicsUnit.Pixel, imgAttr)
            Dim bmp As New Bitmap(modifyImage)
            ms = New MemoryStream()
            bmp.Save(ms, ImageFormat.Jpeg)
        Finally
            modifyImage.Dispose()
            drawedImage.Dispose()
            g.Dispose()
        End Try
        file = GetThumbnail(ms.ToArray(), Width, Height, True)
        'return ms.ToArray();
        Return file
    End Function


    ''' <summary>
    ''' 固定图片大小
    ''' </summary>
    ''' <param name="sourceFile"></param>
    ''' <param name="destWidth"></param>
    ''' <param name="destHeight"></param>
    ''' <param name="IsFixed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThumbnail(ByVal sourceFile As Byte(), ByVal destWidth As Integer, ByVal destHeight As Integer, ByVal IsFixed As Boolean) As Byte()
        Dim ms As New MemoryStream()



        ms.Write(sourceFile, 0, sourceFile.Length)


        Dim imgSource As Image = Image.FromStream(ms)

        Dim thisFormat As ImageFormat = imgSource.RawFormat
        Dim sW As Integer = 0, sH As Integer = 0
        Dim sWidth As Integer = imgSource.Width
        Dim sHeight As Integer = imgSource.Height

        'If destWidth = 0 And destHeight = 0 Then

        '    destWidth = sWidth
        '    destHeight = sHeight
        'ElseIf destWidth = 0 Then
        '    destWidth = sWidth / (sHeight / destHeight)
        'ElseIf destHeight = 0 Then
        '    destHeight = sHeight / (sWidth / destWidth)

        'End If

        If sWidth >= sHeight Then
            destHeight = 0
            If sWidth < destWidth Then
                destWidth = sWidth
                destHeight = sHeight
            End If
        Else
            destWidth = 0
            If sHeight < destHeight Then
                destWidth = sWidth
                destHeight = sHeight
            End If
        End If
        If destWidth = 0 And destHeight = 0 Then
            destWidth = sWidth
            destHeight = sHeight
        ElseIf destWidth = 0 Then
            destWidth = sWidth / (sHeight / destHeight)
        ElseIf destHeight = 0 Then
            destHeight = sHeight / (sWidth / destWidth)
        End If

        Dim outBmp As New Bitmap(destWidth, destHeight)
        'If Not IsFixed Then
        '    outBmp = New Bitmap(sW, sH)
        'End If
        Dim g As Graphics = Graphics.FromImage(outBmp)
        g.Clear(Color.White)
        g.CompositingQuality = CompositingQuality.HighQuality
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        'If Not IsFixed Then
        '    g.DrawImage(imgSource, New Rectangle(0, 0, sW, sH), 0, 0, imgSource.Width, imgSource.Height, _
        '     GraphicsUnit.Pixel)
        'Else
        '    g.DrawImage(imgSource, New Rectangle((destWidth - sW) \ 2, (destHeight - sH) \ 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, _
        '     GraphicsUnit.Pixel)
        'End If
        g.DrawImage(imgSource, New Rectangle(0, 0, destWidth, destHeight), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel)

        g.Dispose()
        Dim encoderParams As New EncoderParameters()
        Dim quality As Long() = New Long(0) {}
        quality(0) = 100

        Dim encoderParam As New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality)
        encoderParams.Param(0) = encoderParam
        Try
            Dim arrayICI As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()
            Dim jpegICI As ImageCodecInfo = Nothing
            For x As Integer = 0 To arrayICI.Length - 1
                If arrayICI(x).FormatDescription.Equals("gif") Then
                    jpegICI = arrayICI(x)
                    Exit For
                End If
            Next
            ms = New MemoryStream()
            outBmp.Save(ms, imgSource.RawFormat)
            Return ms.ToArray()
        Catch
            Return ms.ToArray()
        Finally
            imgSource.Dispose()
            outBmp.Dispose()
        End Try
    End Function
End Class

