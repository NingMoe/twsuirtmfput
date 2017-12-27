Imports NetReusables

Imports Unionsoft.Platform

Partial Class cmsdocument_OfficeEditor
    Inherits Page
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
    Public mTemplate
    Public mFileType
    Public mEditType
    Public mUserName
    '   Public DBAobj As iDBManage2000

    Public mWord
    Public mExcel

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '   DBAobj = Session("DBDemo")

        mHTMLPath = ""
        mDisabled = ""
        '自动获取OfficeServer和OCX文件完整URL路径
        mScriptName = "OfficeEditor.aspx"
        mServerName = "OfficeServer.aspx"
        mHttpUrl = "http://" + Request.ServerVariables("HTTP_HOST") + Request.ServerVariables("SCRIPT_NAME")
        mHttpUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length)
        mServerUrl = mHttpUrl + mServerName                                   '取得OfficeServer文件的完整URL


        mRecordID = Request.QueryString("DOCID")
        mTemplate = ""

        Dim dtDocument As DataTable = SDbStatement.Query("select * from Cms_DocumentCenter where DOC2_ID=" + mRecordID.ToString).Tables(0)
        mEditType = "0"
        If dtDocument.Rows.Count > 0 Then
            mFileType = "." + DbField.GetStr(dtDocument.Rows(0), "DOC2_EXT")
            mFileName = DbField.GetStr(dtDocument.Rows(0), "DOC2_NAME")
            'mUserName = OrgFactory.EmpDriver.GetEmpName(CmsPass, DbField.GetStr(dtDocument.Rows(0), "DOC2_CRTID"))
        End If

        '取得编号
        If (mRecordID = "") Then

            mRecordID = "" '编号为空
        End If
        '取得模式
        If (mEditType = "") Then

            mEditType = "1" '  0 阅读  1 修改[无痕迹] 2 修改[有痕迹]  3 核稿
        End If
        '取得类型
        If (mFileType = "") Then

            mFileType = ".doc" ' 默认为.doc文档
        End If
        '取得用户名
        If (mUserName = "") Then

            mUserName = "金格科技"
        End If

        '取得模板
        If (mTemplate = "") Then

            mTemplate = "" ' 默认没有模板
        End If

        '打开数据库
        'Dim mTemplateList As String
        'Dim mCommand As SqlCommand
        'Dim mReader As SqlDataReader
        'Dim strSelectCmd As String

        'strSelectCmd = "Select * From Document Where RecordID='" + mRecordID + "'"
        'mCommand = New SqlCommand(strSelectCmd, DBAobj.Connection)
        'mReader = mCommand.ExecuteReader()

        'If (mReader.Read()) Then

        '    mRecordID = mReader("RecordID").ToString()
        '    mTemplate = mReader("Template").ToString()
        '    mSubject = mReader("Subject").ToString()
        '    mAuthor = mReader("Author").ToString()
        '    mFileDate = mReader("FileDate").ToString()
        '    mStatus = mReader("Status").ToString()
        '    mFileType = mReader("FileType").ToString()
        '    mHTMLPath = mReader("HTMLPath").ToString()
        'Else

        '    mRecordID = DateTime.Now.ToString("yyyyMMddhhmmss")                         '取得唯一值(mRecordID)
        '    mTemplate = mTemplate
        '    mSubject = "请输入主题"
        '    mAuthor = mUserName
        '    mFileDate = DBAobj.GetDateTime()
        '    mStatus = "DERF"
        '    mFileType = mFileType
        '    mHTMLPath = ""
        'End If
        'mReader.Close()

        'If (mStatus = "EDIT") Then mEditType = "0" '	0 显示
        'If (mStatus = "READ") Then mEditType = mEditType ' 	
        'If (mStatus = "DERF") Then mEditType = "1" '	1 起草

        If (mEditType.CompareTo("0") = 0) Then

            mDisabled = "disabled"
        Else
            mDisabled = ""
        End If

        mFileName = mRecordID + mFileType

        If (mFileType.ToString.ToLower = ".doc" Or mFileType.ToString.ToLower = ".docx") Then
            mWord = ""
            mExcel = "disabled"
        Else
            mWord = "disabled"
            mExcel = ""
        End If

    End Sub
End Class
