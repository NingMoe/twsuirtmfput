Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform

'-------------------------------------------------------------------------------   
' Description : 对文件类型进行判断,如果时Office文件调用WebOffice控件打开,其他则进行下载.
' Interface   :  
' Call        :  
' Author      : CHENYU  
' Date        : 2006-4-5
'-------------------------------------------------------------------------------
Imports System.IO
Imports System.Text


Namespace Unionsoft.Workflow.Web


Partial Class FileWebAdapter
    Inherits UserPageBase

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '获取文档内容
        Dim lngResourceID As Long = CLng(Request.QueryString("ResourceID"))
        Dim lngDocumentID As Long = CLng(Request.QueryString("DocumentID"))
        Dim lngIsCheckOut As Long = 2
        If IsNumeric(Request.QueryString("IsCheckOut")) = False Then
            lngIsCheckOut = 0
        Else
            lngIsCheckOut = CLng(Request.QueryString("IsCheckOut"))
        End If

        Dim datDoc As DataDocument
        Try
            '表示不是来自流程文档表的附件。
            If lngIsCheckOut = 2 Then
                'datDoc = ResFactory.TableService(CmsPassport.GenerateCmsPassportForInnerUse("").GetDataRes(lngResourceID).ResTableType).GetDocument(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                    FileTransfer.DownloadDoc(Response, datDoc)
                    'ElseIf lngIsCheckOut = 3 Then
                    '    datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                    '    Response.Redirect("OfficeView.aspx?ResourceID=" & datDoc.lngRESID.ToString & "&RecordID=" & datDoc.lngID & "&IsCheckOut=" & lngIsCheckOut)
                Else
                    datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                    If datDoc.strDOC2_EXT.ToLower() = "xls" Or datDoc.strDOC2_EXT.ToLower() = "doc" Or datDoc.strDOC2_EXT.ToLower() = "ppt" Or datDoc.strDOC2_EXT.ToLower() = "xlsx" Or datDoc.strDOC2_EXT.ToLower() = "docx" Or datDoc.strDOC2_EXT.ToLower() = "pptx" Then
                        If lngIsCheckOut <> 3 Then
                            '将文档的访问记录放入缓存
                            If Configuration.DocumentAccessCache.Contains(CStr(lngDocumentID)) Then
                                If CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode = Me.CurrentUser.Code Then
                                    Configuration.DocumentAccessCache(CStr(lngDocumentID)) = New DocumentAccessLog(CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode)
                                End If
                            Else
                                Configuration.DocumentAccessCache.Add(CStr(lngDocumentID), New DocumentAccessLog(MyBase.CurrentUser.Code))
                            End If
                        End If
                        Response.Redirect("OfficeEditor.aspx?ResourceID=" & datDoc.lngRESID.ToString & "&RecordID=" & datDoc.lngID & "&IsCheckOut=" & lngIsCheckOut, False)
                    ElseIf (datDoc.strDOC2_EXT.ToUpper() = "PDF") Then
                        Response.Redirect("DocViewPdf.aspx?DOCID=" & datDoc.lngDOCID, False)
                    Else
                        'FileTransfer.DownloadDoc(Response, datDoc)

                        Dim strFileName As String = datDoc.strDOC2_NAME & "." & datDoc.strDOC2_EXT
                        strFileName = System.Web.HttpUtility.UrlEncode(strFileName, Encoding.GetEncoding("UTF-8"))
                        Response.AddHeader("Content-Disposition", "inline;filename=" & strFileName)
                        Response.ContentType = "application/octet-stream"

                        '开始向客户端写文件流
                        Response.BinaryWrite(datDoc.bytDOC2_FILE_BIN)
                        Response.Flush()

                        datDoc.bytDOC2_FILE_BIN = Nothing '手动清除文档所占内存
                        datDoc = Nothing
                        Response.End()
                    End If
            End If
        Catch ex As Exception
            SLog.Err("下载附件发生异常.", ex)
            Me.MessageBox(ex.Message)
            Me.Close()
        End Try

    End Sub

End Class

End Namespace
