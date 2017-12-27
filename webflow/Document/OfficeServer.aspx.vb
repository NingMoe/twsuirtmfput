Imports System.Data
Imports System.Data.SqlClient
Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement
Imports System.IO

Namespace Unionsoft.Workflow.Web
    Public Class cmsdocument_OfficeServer
        Inherits UserPageBase

        Public mFileSize
        Public mFileBody
        Public mFileName
        Public mFileType
        Public mFileDate
        Public mFileID
        Public mRecordID
        Public mTemplate
        Public mDateTime
        Public mOption
        Public mMarkName
        Public mPassword
        Public mMarkList
        Public mBookmark
        Public mDescript
        Public mHostName
        Public mMarkGuid

        Public mHtmlName
        Public mDirectory
        Public mFilePath

        Public mUserName
        Public mCommand
        Public mContent

        Private mLabelName
        Private mImageName
        Private mTableContent
        Private mColumns
        Private mCells
        Private mLocalFile
        Private mRemoteFile

        '打印控制
        Private mOfficePrints
        Private mCopies

        '自定义信息传递
        Private mInfo

        Public mError

        Public MsgObj As DBstep.MsgServer2000Class
        ' Public DBAobj As iDBManage2000



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            '在此处放置初始化页的用户代码
            '  DBAobj = Session("DBDemo")
            MsgObj = New DBstep.MsgServer2000Class()

            MsgObj.Create()
            MsgObj.MsgVariant = Request.BinaryRead(Request.ContentLength)

            mTableContent = ""
            mColumns = 3
            mCells = 8
            mFilePath = Server.MapPath(".")

            If (MsgObj.GetMsgByName("DBSTEP") = "DBSTEP") Then                      '如果是合法的信息包
                mOption = MsgObj.GetMsgByName("OPTION")                               '取得操作信息
                mUserName = MsgObj.GetMsgByName("USERNAME")                           '取得操作用户的名称
                If (mOption = "LOADFILE") Then                                        '下面的代码为打开服务器数据库里的文件
                    mRecordID = MsgObj.GetMsgByName("RECORDID")                         '取得文档编号
                    mFileName = MsgObj.GetMsgByName("FILENAME")                         '取得文档名称
                    mFileType = MsgObj.GetMsgByName("FILETYPE")                         '取得文档类型

                    MsgObj.MsgTextClear()                                               '清空文本信息




                    Try
                        If IsUpDirectory() Then
                            MsgObj.MsgFileLoad(mFilePath)
                        Else
                            MsgObj.MsgFileBody = (mFileBody)
                        End If

                        MsgObj.SetMsgByName("STATUS", "打开成功!.")                       '设置状态信息
                        MsgObj.MsgError = ("")
                    Catch ex As Exception
                        MsgObj.MsgError = ("打开失败!")
                    End Try
                ElseIf (mOption = "SAVEFILE") Then

                    ' Dim fs As FileStream = New FileStream(mFilePath, FileMode.OpenOrCreate)
                    mFileBody = MsgObj.MsgFileBody()
                    'fs.Write(mFileBod
                    Dim fs As New MemoryStream(mFileBody, True)
                    mRecordID = MsgObj.GetMsgByName("RECORDID")
                    mFileName = MsgObj.GetMsgByName("FILENAME")                         '取得文档名称
                    mFileType = MsgObj.GetMsgByName("FILETYPE")

                    ResFactory.TableService("DOC").Checkin(CmsPassport.GenerateCmsPassportForInnerUse(Me.CurrentUser.Code), Convert.ToInt64(Request("resid")), Convert.ToInt64(Request("recid")), fs, mFilePath + "\\temp\\" + mFileName + mFileType, , , "update")

                Else
                    MsgObj.MsgError = ("Error:packet message")
                    MsgObj.MsgTextClear()
                    MsgObj.MsgFileClear()
                End If


            Else
                MsgObj.MsgError = ("Error:packet message")
                MsgObj.MsgTextClear()
                MsgObj.MsgFileClear()
            End If

            Response.BinaryWrite(MsgObj.MsgVariant())
            MsgObj.Free()
            MsgObj = Nothing
        End Sub



        Private Function IsUpDirectory() As Boolean
            Dim datDoc As DataDocument = ResFactory.TableService("DOC").GetDocument(Convert.ToInt64(mRecordID))

            mFileBody = datDoc.bytDOC2_FILE_BIN
            Return False
        End Function


    End Class
End Namespace
