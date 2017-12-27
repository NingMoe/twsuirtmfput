Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform

Namespace Unionsoft.Workflow.Web
    Partial Class cmsdocument_OfficeEditor
        Inherits UserPageBase

        Public mSubject
        Public mStatus
        Public mAuthor
        Public mFileName
        Public mFileDate
        Public mHTMLPath

        Public mDisabled

        Public mHttpUrl
        Public mScriptName
        Public mServerName
        Public mServerUrl



        Public mRecordID
        Public DocumentID
        Public mTemplate
        Public mFileType
        Public mEditType
        Public mUserName
        '   Public DBAobj As iDBManage2000

        Public mWord
        Public mExcel
        Protected strUrl As String = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.Replace("\", "\\")
        Dim ResourceID As String
        Dim datDoc As New DataDocument
        Protected lngIsCheckOut As Long = 0
        Protected IsEnabled As Boolean = False
        Protected EditType As String = "0"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            '   DBAobj = Session("DBDemo")

            If Not IsPostBack Then
                If Not System.IO.Directory.Exists(strUrl + "temp\\docedit") Then
                    System.IO.Directory.CreateDirectory(strUrl + "temp\\docedit")
                End If
            End If

            mRecordID = Request.QueryString("RecordID")
            ResourceID = Request.QueryString("ResourceID")
            lngIsCheckOut = CLng(Request.QueryString("IsCheckOut"))
            datDoc = Unionsoft.Platform.ResFactory.TableService("DOC").GetDocument(Convert.ToInt64(ResourceID), Convert.ToInt64(mRecordID), False)

            Me.btnUpdateFile.Enabled = True
            If lngIsCheckOut <> 3 Then
                If Configuration.DocumentAccessCache.Contains(CStr(datDoc.lngDOCID)) Then
                    If CType(Unionsoft.Workflow.Web.Configuration.DocumentAccessCache(CStr(datDoc.lngDOCID)), DocumentAccessLog).UserCode <> Me.CurrentUser.Code Then
                        If DateDiff(DateInterval.Minute, CType(Unionsoft.Workflow.Web.Configuration.DocumentAccessCache(CStr(datDoc.lngDOCID)), DocumentAccessLog).AccessTime, Now) <= 10 Then
                            Me.btnUpdateFile.Enabled = False
                            Me.btnUpdateFile.Text = "此文件正在被[" & OrganizationFactory.Implementation.GetEmployee(CType(Unionsoft.Workflow.Web.Configuration.DocumentAccessCache(CStr(datDoc.lngDOCID)), DocumentAccessLog).UserCode).Name & "]编辑"
                            IsEnabled = True
                        Else
                            Unionsoft.Workflow.Web.Configuration.DocumentAccessCache(CStr(datDoc.lngDOCID)) = New DocumentAccessLog(MyBase.CurrentUser.Code)
                        End If
                    End If
                Else
                    Unionsoft.Workflow.Web.Configuration.DocumentAccessCache.Add(CStr(datDoc.lngDOCID), New DocumentAccessLog(MyBase.CurrentUser.Code))
                End If
                EditType = "3"
            Else
                Me.btnUpdateFile.Enabled = False
                EditType = "0"
            End If


            mHTMLPath = ""
            mDisabled = ""
            '自动获取OfficeServer和OCX文件完整URL路径
            mScriptName = "OfficeEditor.aspx"
            mServerName = "OfficeServer.aspx?resid=" + ResourceID.Trim & "&recid=" & mRecordID.ToString
            mHttpUrl = "http://" + Request.ServerVariables("HTTP_HOST") + Request.ServerVariables("SCRIPT_NAME")
            mHttpUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length)
            mServerUrl = mHttpUrl + mServerName                                   '取得OfficeServer文件的完整URL


         
            mTemplate = ""


            DocumentID = datDoc.lngDOCID
            ' Dim dtDocument As DataTable = SDbStatement.Query("select * from Cms_DocumentCenter where DOC2_ID=" + datDoc.lngDOCID.ToString).Tables(0)
            mEditType = "0"

            mFileType = "." + datDoc.strDOC2_EXT
            mFileName = datDoc.strDOC2_NAME.Trim
            'mUserName = OrgFactory.EmpDriver.GetEmpName(CmsPass, DbField.GetStr(dtDocument.Rows(0), "DOC2_CRTID"))


            '取得编号
            If (mRecordID = "") Then

                mRecordID = "" '编号为空
            End If
            '取得模式
            If (mEditType = "") Then

                mEditType = 3 '  0 阅读  1 修改[无痕迹] 2 修改[有痕迹]  3 核稿
            End If
            '取得类型
            If (mFileType = "") Then

                mFileType = ".doc" ' 默认为.doc文档
            End If
            '取得用户名
            If (mUserName = "") Then

                mUserName = Me.CurrentUser.Name
            End If

            '取得模板
            If (mTemplate = "") Then

                mTemplate = "" ' 默认没有模板
            End If

            If (mEditType.CompareTo("0") = 0) Then

                mDisabled = "disabled"
            Else
                mDisabled = ""
            End If

            '   mFileName = mRecordID + mFileType

            If (mFileType.ToString.ToLower = ".doc" Or mFileType.ToString.ToLower = ".docx") Then
                mWord = ""
                mExcel = "disabled"
            Else
                mWord = "disabled"
                mExcel = ""
            End If


        End Sub

        Protected Sub btnUpdateFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateFile.Click

            'Try 
            '    Dim fs As FileStream = New FileStream(txtFileName.Text.Trim, FileMode.Open, FileAccess.Read) 
            '    ResFactory.TableService("DOC").Checkin(CmsPassport.GenerateCmsPassportForInnerUse(Me.CurrentUser.Code), ResourceID, mRecordID, fs, strUrl + "\\temp\\" + txtFileName.Text.Trim.Substring(txtFileName.Text.Trim.LastIndexOf("\")), , , "update")
            '    fs.Close()
            'Catch ex As Exception
            '    SLog.Err(ex.Message)
            'End Try
        End Sub

        'Public Sub Checkin(ByVal docID As Long, ByVal bytDOC2_FILE_BIN As Byte())
        '    Dim datRes As DataResource = pst.GetDataRes(lngResID)
        '    Dim strSql As String
        '    Dim DocDatabaseName As String = CmsDatabase.DocDatabase
        '    Try
        '        '检验文档输入流
        '        If sm Is Nothing Then Throw New CmsException("没有文档内容上传")
        '        If sm.Length <= 0 Then Throw New CmsException("没有文档内容上传")

        '        '-------------------------------------------------------------------
        '        strSql = "SELECT " & CmsDatabase.DocDatabaseLink & ".* FROM " & datRes.ResTable & ", " & CmsDatabase.DocDatabaseLink & " WHERE RESID=" & lngResID & " AND DOCID=DOC2_ID AND ID=" & lngRecID
        '        If strOperation.Trim = "" Then
        '            '判断是否需要签出状态校验
        '            If blnForceNoVerifyOfCheckout = False Then
        '                '检验是否处于签出状态
        '                strSql &= " AND (DOC2_STATUS='" & DocVersion.StatusCheckout & "' OR DOC2_SIZE=0)"
        '            End If
        '        End If
        '        Dim ds As DataSet = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
        '        Dim dv As DataView = ds.Tables(0).DefaultView
        '        If dv.Count <= 0 Then
        '            Throw New CmsException("签入失败，没有处于签出状态的指定文档！")
        '        Else
        '            strSql = "SELECT " & CmsDatabase.DocDatabaseLink & ".* FROM " & CmsDatabase.DocDatabaseLink & " where DOC2_ID=" & DbField.GetStr(dv(0), "DOC2_ID")
        '            ds = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
        '            dv = ds.Tables(0).DefaultView
        '        End If
        '        Dim drv As DataRowView = dv(0)

        '        If strOperation.Trim = "" Then
        '            '判断是否需要签出人校验
        '            If blnForceNoVerifyOfCheckoutUser = False Then
        '                '只允许签出人签入文档
        '                If DbField.GetStr(drv, "DOC2_EDTID") <> pst.Employee.ID Then
        '                    Throw New CmsException("签入失败，只有签出者才可以签入文档！")
        '                End If
        '            End If
        '        End If
        '        '-------------------------------------------------------------------

        '        '-------------------------------------------------------------------
        '        Dim strFileName As String = Path.GetFileNameWithoutExtension(strClientFilePath)
        '        SLog.Err(strFileName)
        '        Dim strFileExt As String = Path.GetExtension(strClientFilePath)
        '        If strFileExt.StartsWith(".") Then strFileExt = strFileExt.Substring(1) '去掉"."
        '        Dim blnCompressed As Boolean
        '        Dim binFile() As Byte

        '        If DbField.GetStr(drv, "DocHostName") <> "" Then DocDatabaseName = DbField.GetStr(drv, "DocHostName")
        '        SLog.Err(DocDatabaseName)

        '        '校验：Checkin的文件名称和数据中的必须相同
        '        Dim strFileNameInDb As String = DbField.GetStr(drv, "DOC2_NAME")
        '        Dim strFileExtInDb As String = DbField.GetStr(drv, "DOC2_EXT")
        '        Dim IsUpDirectory As Boolean = DbField.GetBool(drv, "IsUpDirectory")
        '        'chenyu 2010/4/10 cancel
        '        'If strFileName <> strFileNameInDb OrElse strFileExt <> strFileExtInDb Then Throw New CmsException("签入文档失败，签入文档的名称必须与数据库内的文档名称完全相同。")

        '        '判断是否需要压缩
        '        Dim blnNozip As Boolean = False
        '        If sm.Length <= CmsConfig.GetSizeOfNozipFile() OrElse CmsConfig.IsNozipFileExt(strFileExt) Then
        '            blnNozip = True
        '        End If

        '        ' If IsUpDirectory Then blnNozip = False
        '        SLog.Err(sm.Length.ToString())
        '        If Not IsUpDirectory And blnNozip = False Then

        '            '压缩文档流
        '            binFile = CompressFile(sm)
        '            blnCompressed = True
        '            '压缩文件
        '        Else
        '            Dim br As BinaryReader = New BinaryReader(sm)
        '            binFile = br.ReadBytes(CInt(sm.Length))
        '            blnCompressed = False
        '        End If
        '        SLog.Err("压缩文件:" & blnCompressed)
        '        '-------------------------------------------------------------------

        '        '-------------------------------------------------------------------
        '        '更新文档信息表
        '        drv.BeginEdit()
        '        Dim lngDocID As Long = DbField.GetLng(drv, "DOC2_ID")
        '        Dim strComments As String = DbField.GetStr(drv, "DOC2_COMMENTS")
        '        Dim strKeywords As String = DbField.GetStr(drv, "DOC2_KEYWORDS")
        '        drv("DOC2_EDTID") = pst.Employee.ID
        '        drv("DOC2_EDTTIME") = Date.Now
        '        'drv("DOC2_FILE_BIN") = binFile
        '        drv("DOC2_NAME") = strFileName
        '        drv("DOC2_EXT") = strFileExt
        '        If IsUpDirectory Then drv("DocHostName") = DocDatabaseName.Substring(0, DocDatabaseName.LastIndexOf(".")) + "." + strFileExt
        '        drv("DOC2_SIZE") = sm.Length
        '        If blnCompressed Then '文档压缩过
        '            drv("DOC2_COMPRESSED_SIZE") = binFile.Length '文档SIZE，压缩后大小（默认不显示状态）
        '            Dim intRate As Integer = CInt((sm.Length - binFile.Length) * 100 / sm.Length)
        '            If intRate < 0 Then intRate = 0 '压缩后Size反而变大
        '            drv("DOC2_COMPRESSED_RATE") = intRate '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%
        '        Else '文档没有压缩
        '            drv("DOC2_COMPRESSED_SIZE") = 0 '文档SIZE，压缩后大小（默认不显示状态）
        '            drv("DOC2_COMPRESSED_RATE") = 0 '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%
        '        End If
        '        drv("DOC2_STATUS") = DocVersion.StatusCheckin
        '        drv("DOC2_CONVERTED") = 0
        '        drv("DOC2_EDTTIME") = Date.Now
        '        drv("DOC2_LCYC_LASTDO_TIME") = Date.Now
        '        drv.EndEdit()
        '        CmsDbStatement.QueryUpdate(SDbConnectionPool.GetDbConfig(), ds, strSql)
        '        ds.Clear()
        '        ds = Nothing

        '        '保存文件
        '        If datRes.DocumentServerUrl.Trim <> "" Then  '分布式上传文档 tq add

        '        Else

        '            If IsUpDirectory Then '上传到文件夹
        '                Dim strFilePath As String = CmsConfig.ProjectRootFolder + DocDatabaseName.Replace("/", "\")
        '                If File.Exists(strFilePath) Then
        '                    File.Delete(strFilePath.Trim)
        '                End If

        '                strFilePath = CmsConfig.ProjectRootFolder + DocDatabaseName.Replace("/", "\")
        '                strFilePath = strFilePath.Substring(0, strFilePath.LastIndexOf("\") + 1) + lngDocID.ToString + "." + strFileExt
        '                Dim strDirectory As String = strFilePath.Substring(0, strFilePath.LastIndexOf("\"))
        '                If Not System.IO.Directory.Exists(strDirectory.Trim) Then System.IO.Directory.CreateDirectory(strDirectory.Trim)
        '                Dim fs As FileStream = New FileStream(strFilePath, FileMode.Create, FileAccess.Write)
        '                fs.Write(binFile, 0, binFile.Length)
        '                fs.Flush()
        '                fs.Close()
        '            Else
        '                Dim hst As New Hashtable
        '                hst("DOC2_EDTID") = pst.Employee.ID
        '                hst("DOC2_EDTTIME") = Date.Now
        '                hst("DOC2_FILE_BIN") = binFile
        '                hst("DOC2_NAME") = strFileName
        '                hst("DOC2_EXT") = strFileExt
        '                hst("DOC2_SIZE") = sm.Length
        '                If blnCompressed Then '文档压缩过
        '                    hst("DOC2_COMPRESSED_SIZE") = binFile.Length '文档SIZE，压缩后大小（默认不显示状态）
        '                    Dim intRate As Integer = CInt((sm.Length - binFile.Length) * 100 / sm.Length)
        '                    If intRate < 0 Then intRate = 0 '压缩后Size反而变大
        '                    hst("DOC2_COMPRESSED_RATE") = intRate '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%
        '                Else '文档没有压缩
        '                    hst("DOC2_COMPRESSED_SIZE") = 0 '文档SIZE，压缩后大小（默认不显示状态）
        '                    hst("DOC2_COMPRESSED_RATE") = 0 '压缩率：(压缩前Size-压缩后Size)/压缩前Size*100%
        '                End If
        '                hst("DOC2_STATUS") = DocVersion.StatusCheckin
        '                hst("DOC2_CONVERTED") = 0
        '                hst("DOC2_EDTTIME") = Date.Now
        '                hst("DOC2_LCYC_LASTDO_TIME") = Date.Now
        '                CmsDbStatement.UpdateRows(SDbConnectionPool.GetDbConfig(), hst, DocDatabaseName, "DOC2_ID=" & lngDocID)
        '            End If
        '        End If
        '        '-------------------------------------------------------------------

        '        '-----------------------------------------------------------
        '        '保存文档为临时文件，以便全文检索的文档内容转换用
        '        If IsFTSearchWork(pst, lngResID, strFileExt) Then
        '            If blnCompressed Then '压缩过
        '                Dim br As BinaryReader = New BinaryReader(sm)
        '                sm.Seek(0, SeekOrigin.Begin)
        '                Dim binFileTemp() As Byte = br.ReadBytes(CInt(sm.Length))
        '                SaveFileForFulltextSearch(pst, binFileTemp, lngDocID, strFileExt, pst.ClientID)
        '                binFileTemp = Nothing
        '            Else '未压缩过
        '                SaveFileForFulltextSearch(pst, binFile, lngDocID, strFileExt, pst.ClientID)
        '            End If
        '        End If
        '        '-----------------------------------------------------------

        '        Dim hashLogColValue As Hashtable = ResourceBase.GetLogColValueForDocTable(pst, datRes.ResID, datRes, Nothing, strFileName, strFileExt, strComments, strKeywords)
        '        CmsLog.Save(pst, datRes, lngRecID, hashLogColValue, LogTitle.DocCheckin, strFileName & "." & strFileExt)

        '        binFile = Nothing '文档所占内存太大，必须清空
        '    Catch ex As Exception
        '        CmsError.ThrowEx("签入文档异常失败！", ex, True)
        '    End Try
        'End Sub


    End Class
End Namespace
