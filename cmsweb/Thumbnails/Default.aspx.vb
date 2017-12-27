Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement
Imports System.IO


Partial Class Thumbnails_Default
    Inherits CmsPage

    Protected ResourceId As Long = 0
    Protected HostResourceId As Long = 0
    Protected HostRecID As Long = 0

    Protected Sub bt_upload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSave.Click
        '检查上传文件的格式是否有效 

        If CmsPass.GetDataRes(ResourceId).ResTableType.ToUpper.Trim <> "DOC" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "javascript:alert('只有文档资源才可使用该功能！');history.back();", True)
            ' Response.Redirect(VStr("PAGE_BACKPAGE"), False)
            Return
        Else
            If Me.FileUpload1.PostedFile.ContentType.ToLower().IndexOf("image") < 0 Then
                Response.Write("上传图片格式无效！")
                Return
            End If

            '生成原图 
            Dim strType As String = Me.FileUpload1.PostedFile.FileName.Substring(Me.FileUpload1.PostedFile.FileName.LastIndexOf("."))
            Dim oFileByte As [Byte]() = New Byte(Me.FileUpload1.PostedFile.ContentLength - 1) {}
            Dim oStream As System.IO.Stream = Me.FileUpload1.PostedFile.InputStream
            Dim oImage As System.Drawing.Image = System.Drawing.Image.FromStream(oStream)

            Dim oWidth As Integer = oImage.Width
            '原图宽度 
            Dim oHeight As Integer = oImage.Height
            '原图高度 
            Dim tWidth As Integer = Convert.ToDouble(txtWidth.Text.Trim)
            '设置缩略图初始宽度 
            Dim tHeight As Integer = Convert.ToDouble(txtHeight.Text.Trim)
            '设置缩略图初始高度 
            '按比例计算出缩略图的宽度和高度 
            If tWidth > oWidth And tHeight > oHeight Then
                tWidth = oWidth
                tHeight = oHeight
            Else
                If oWidth >= oHeight Then
                    tHeight = CInt(Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(tWidth) / Convert.ToDouble(oWidth))))
                Else
                    tWidth = CInt(Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(tHeight) / Convert.ToDouble(oHeight))))
                End If
            End If


            '生成缩略原图 
            Dim tImage As New Bitmap(tWidth, tHeight)
            Dim g As Graphics = Graphics.FromImage(tImage)
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
            '设置高质量插值法 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            '设置高质量,低速度呈现平滑程度 
            g.Clear(Color.Transparent)
            '清空画布并以透明背景色填充 
            g.DrawImage(oImage, New Rectangle(0, 0, tWidth, tHeight), New Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel)

            Dim strUrl As String = Server.MapPath(".") & "\temp"
            Dim strName As String = NetReusables.TimeId.CurrentMilliseconds(30).ToString & strType
            If Not System.IO.Directory.Exists(strUrl) Then
                System.IO.Directory.CreateDirectory(strUrl)
            End If

            Dim oFullName As String = strUrl & "\" & "o" & strName
            '保存原图的物理路径 
            Dim tFullName As String = strUrl & "\" & "t" & strName
            '保存缩略图的物理路径 
            Try
                '以JPG格式保存图片 
                oImage.Save(oFullName, GetImageFormat(Me.FileUpload1.PostedFile.ContentType.Trim))
                tImage.Save(tFullName, GetImageFormat(Me.FileUpload1.PostedFile.ContentType.Trim))


                img1.ImageUrl = CmsConfig.WebApplicationPath + "Thumbnails/temp/t" & strName.Trim

                ' ResFactory.TableService("DOC").AddRecord(CmsPass, ResourceId, 0, True)
                Dim strFileName As String = Me.FileUpload1.PostedFile.FileName.Substring(Me.FileUpload1.PostedFile.FileName.LastIndexOf("\") + 1)

                Update(ResourceId, strFileName, tFullName)
                lnkSave.Enabled = False
                Me.MsgBox("保存成功")
            Catch ex As Exception
                Me.MsgBox("保存失败")
            Finally
                '释放资源 
                oImage.Dispose()
                g.Dispose()

                tImage.Dispose()
            End Try
        End If
    End Sub

    Private Sub Update(ByVal ResourceId As Long, ByVal strFileName As String, ByVal FileUrl As String)

        Dim hstRelation As Hashtable = CmsTableRelation.GetRelFieldValForRelRes(CmsPass, HostResourceId, HostRecID, ResourceId, True)
        Dim datRes As DataResource = CmsPass.GetDataRes(ResourceId)

        Dim ResTableName As String = datRes.ResTable

        If datRes.IsView Then
            ResTableName = CmsPass.GetDataRes(datRes.IndepParentResID).ResTable
            ResourceId = datRes.IndepParentResID
        End If
        Dim dbs As DbStatement = New DbStatement(SDbConnectionPool.GetDbConfig())
        Try

            dbs.TransactionBegin()

            Dim docID As Long = UploadFile(dbs, ResourceId, strFileName.Substring(0, strFileName.LastIndexOf(".")), FileUrl)
            Dim hst As New Hashtable
            hst.Add("ID", TimeId.CurrentMilliseconds().ToString)
            hst.Add("DOCID", docID)
            hst.Add("RESID", ResourceId)
            hst.Add("RELID", 0)

            Dim en As IDictionaryEnumerator = hstRelation.GetEnumerator()
            Dim str As String = "tab" + ResourceId.ToString
            Do While en.MoveNext
                If en.Key.ToString.ToLower.Contains(str) Then
                    Dim strCol As String = en.Key.ToString.Substring(str.Length + 3)
                    Dim strValue As String = en.Value.ToString

                    hst.Add(strCol, strValue)
                End If
            Loop
            dbs.InsertRow(hst, ResTableName)
            dbs.TransactionCommit()
        Catch ex As Exception
            dbs.TransactionRollback() '失败，回滚事务
            CmsError.ThrowEx("添加记录异常失败！", ex, True)
        Finally
            '  binFile = Nothing '文档所占内存太大，必须清空
        End Try


    End Sub

    Private Function UploadFile(ByVal dbs As DbStatement, ByVal lngResID As Long, ByVal strFileName As String, ByVal FileUrl As String) As Long
        Dim strFilePath As String = ""
        Dim lngDocID As Long = TimeId.CurrentMilliseconds()
        Dim strFileExt As String = FileUrl.Substring(FileUrl.LastIndexOf(".") + 1)
        Dim binFile As Byte()
        Try

            Dim fs As FileStream = New FileStream(FileUrl, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)
            Dim DOC2_SIZE As Long = fs.Length
            binFile = br.ReadBytes(CInt(DOC2_SIZE))
            br.Close()
            fs.Close()

            If CmsDatabase.FileUploadMode = FileUploadMode.Database Then
                '保存记录
                Dim strSql As String = "SELECT * FROM " & CmsDatabase.DocDatabase & " WHERE DOC2_ID=1" '获取空数据集
                Dim ds As DataSet = dbs.Query(strSql)
                Dim drv As DataRowView = ds.Tables(0).DefaultView.AddNew()
                drv.BeginEdit()

                drv("DOC2_ID") = lngDocID '唯一的记录ID
                drv("DOC2_RESID1") = lngResID '隶属的资源ID
                drv("DOC2_CRTID") = CmsPass.Employee.ID '创建人员ID

                drv("DOC2_CRTTIME") = Date.Now  '创建时间
                drv("DOC2_EDTID") = CmsPass.Employee.ID '最后修改人员ID
                drv("DOC2_EDTTIME") = Date.Now  '最后修改时间 
                drv("DOC2_FILE_BIN") = binFile
                drv("DOC2_NAME") = strFileName
                drv("DOC2_EXT") = strFileExt

                drv("DOC2_SIZE") = DOC2_SIZE

                drv("DOC2_COMPRESSED_SIZE") = 0 '文档SIZE，压缩后大小（默认不显示状态）
                drv("DOC2_COMPRESSED_RATE") = 0 '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%

                drv("DOC2_COMMENTS") = ""

                'If HashField.ContainsKey(hashFieldValToSave, "DOC2_COMMENTS") Then
                '    drv("DOC2_COMMENTS") = strComments
                '    'hashFieldValToSave.Remove("DOC2_COMMENTS")
                'End If
                drv("DOC2_KEYWORDS") = ""
                'If HashField.ContainsKey(hashFieldValToSave, "DOC2_KEYWORDS") Then
                '    drv("DOC2_KEYWORDS") = strKeywords
                '    'hashFieldValToSave.Remove("DOC2_KEYWORDS") tq 2010-07-22 
                'End If
                drv("DOC2_STATUS") = ""
                drv("DOC2_CONVERTED") = 0 '初始值必为0
                drv("DOC2_SHARES") = 1 '初始值必为1
                drv("DOC2_LCYC_ENABLE") = CLng(IIf(False, 1, 0))  '启动文档生命周期管理。0或其它值：不启动（系统默认值）1：启动注：签出状态的文档既便已过生命周期，也不会被删除。
                drv("DOC2_LCYC_LASTDO_TIME") = Date.Now
                drv.EndEdit()
                dbs.QueryUpdate(ds)
                ds.Clear()
                ds = Nothing
            Else '上传到文件夹
                strFilePath = "UploadFile\" + lngResID.ToString '+ "\" + lngDocID.ToString + "." + strFileExt.Trim 'strDocFolder & strFileName
                If Not System.IO.Directory.Exists(CmsConfig.ProjectRootFolder + strFilePath.Trim) Then System.IO.Directory.CreateDirectory(CmsConfig.ProjectRootFolder + strFilePath.Trim)
                strFilePath += "\" + lngDocID.ToString + "." + strFileExt
                'Dim fs As FileStream = New FileStream(CmsConfig.ProjectRootFolder + strFilePath, FileMode.Create, FileAccess.Write)
                'fs.Write(binFile, 0, binFile.Length)
                'fs.Flush()
                'fs.Close()

                System.IO.File.Copy(FileUrl, CmsConfig.ProjectRootFolder + strFilePath)
            End If



            '保存附件主机数据库
            Dim hst As Hashtable = New Hashtable
            If CmsDatabase.FileUploadMode = FileUploadMode.Database Then
                hst.Add("DocHostName", CmsDatabase.DocDatabase)
                hst.Add("IsUpDirectory", 0)
            Else
                hst.Add("DocHostName", "/" + strFilePath.Replace("\", "/"))
                hst.Add("IsUpDirectory", 1)
            End If
            hst("DOC2_ID") = lngDocID '唯一的记录ID
            hst("DOC2_RESID1") = lngResID '隶属的资源ID
            hst("DOC2_CRTID") = CmsPass.Employee.ID '创建人员ID
            hst("DOC2_CRTTIME") = Date.Now '创建时间
            hst("DOC2_EDTID") = CmsPass.Employee.ID '最后修改人员ID
            hst("DOC2_EDTTIME") = Date.Now '最后修改时间
            hst("DOC2_NAME") = strFileName
            hst("DOC2_EXT") = strFileExt

            hst("DOC2_SIZE") = DOC2_SIZE '文档SIZE，压缩前大小（默认不显示状态）

            hst("DOC2_COMPRESSED_SIZE") = 0 '文档SIZE，压缩后大小（默认不显示状态）
            hst("DOC2_COMPRESSED_RATE") = 0 '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%
            hst("DOC2_COMMENTS") = ""
            'Dim strComments As String = ""
            'If HashField.ContainsKey(hashFieldValToSave, "DOC2_COMMENTS") Then
            '    'strComments = HashField.GetStr(hashFieldValToSave, "DOC2_COMMENTS")
            '    hst("DOC2_COMMENTS") = strComments
            '    hashFieldValToSave.Remove("DOC2_COMMENTS")
            'End If
            hst("DOC2_KEYWORDS") = ""
            'Dim strKeywords As String = ""
            'If HashField.ContainsKey(hashFieldValToSave, "DOC2_KEYWORDS") Then
            '    strKeywords = HashField.GetStr(hashFieldValToSave, "DOC2_KEYWORDS")
            '    hst("DOC2_KEYWORDS") = strKeywords
            '    hashFieldValToSave.Remove("DOC2_KEYWORDS")
            'End If
            hst("DOC2_STATUS") = ""
            hst("DOC2_CONVERTED") = 0 '初始值必为0
            hst("DOC2_SHARES") = 1 '初始值必为1
            hst("DOC2_LCYC_ENABLE") = CLng(IIf(False, 1, 0))  '启动文档生命周期管理。0或其它值：不启动（系统默认值）1：启动注：签出状态的文档既便已过生命周期，也不会被删除。
            hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
            dbs.InsertRow(hst, "Cms_DocumentCenter")
        Catch ex As Exception
        Finally
            binFile = Nothing '文档所占内存太大，必须清空
        End Try
        Return lngDocID
    End Function


    Public Function GetImageFormat(ByVal strType As String) As System.Drawing.Imaging.ImageFormat
        Select Case strType.ToLower
            Case "image/pjpeg"
                Return System.Drawing.Imaging.ImageFormat.Jpeg
            Case "image/gif"
                Return System.Drawing.Imaging.ImageFormat.Gif
            Case "image/x-png"
                Return System.Drawing.Imaging.ImageFormat.Png
            Case "image/x-icon"
                Return System.Drawing.Imaging.ImageFormat.Icon
            Case Else
                Return System.Drawing.Imaging.ImageFormat.Jpeg
        End Select
    End Function


    Protected Sub lnkExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExit.Click
        Response.Redirect("../cmshost/CmsFrmBridge.aspx?noderesid=" + AspPage.RStr("noderesid", Request) + "&schdocid=" + AspPage.RStr("schdocid", Request) + "&depid=" + AspPage.RStr("depid", Request) + "&timeid=" + TimeId.CurrentMilliseconds(1).ToString + "&type=" + AspPage.RStr("type", Request) + "&RecId=" + AspPage.RStr("RecId", Request))
    End Sub

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("mnuresid") IsNot Nothing Then ResourceId = Convert.ToInt64(Request("mnuresid").Trim)
        If Request("mnuhostresid") IsNot Nothing Then HostResourceId = Convert.ToInt64(Request("mnuhostresid"))
        If Request("mnuhostrecid") IsNot Nothing Then HostRecID = Convert.ToInt64(Request("mnuhostrecid"))
    End Sub
End Class
