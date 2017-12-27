Imports System.IO
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform

Namespace Unionsoft.Workflow.Web
    Partial Class cmsdocument_OfficeView
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

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            '   DBAobj = Session("DBDemo")

            If Not IsPostBack Then
                If Not System.IO.Directory.Exists(strUrl + "temp\\docedit") Then
                    System.IO.Directory.CreateDirectory(strUrl + "temp\\docedit")
                End If
            End If

            mHTMLPath = ""
            mDisabled = ""
            '自动获取OfficeServer和OCX文件完整URL路径
            mScriptName = "OfficeEditor.aspx"
            mServerName = "OfficeServer.aspx"
            mHttpUrl = "http://" + Request.ServerVariables("HTTP_HOST") + Request.ServerVariables("SCRIPT_NAME")
            mHttpUrl = mHttpUrl.Substring(0, mHttpUrl.Length - mScriptName.Length)
            mServerUrl = mHttpUrl + mServerName                                   '取得OfficeServer文件的完整URL


            mRecordID = Request.QueryString("RecordID")
            ResourceID = Request.QueryString("ResourceID")
            lngIsCheckOut = CLng(Request.QueryString("IsCheckOut"))
            mTemplate = ""

            datDoc = Unionsoft.Platform.ResFactory.TableService("DOC").GetDocument(Convert.ToInt64(ResourceID), Convert.ToInt64(mRecordID), False)
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

                mEditType = "2" '  0 阅读  1 修改[无痕迹] 2 修改[有痕迹]  3 核稿
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
End Namespace
