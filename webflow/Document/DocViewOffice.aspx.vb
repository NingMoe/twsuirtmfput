
Option Strict On
Option Explicit On

Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Platform

 


Namespace Unionsoft.Workflow.Web


    Partial Class DocViewOffice
        Inherits UserPageBase

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            '��ȡ�ĵ�����
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
                '��ʾ�������������ĵ���ĸ�����
                If lngIsCheckOut = 2 Then
                    'datDoc = ResFactory.TableService(CmsPassport.GenerateCmsPassportForInnerUse("").GetDataRes(lngResourceID).ResTableType).GetDocument(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                    datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                    FileTransfer.DownloadDoc(Response, datDoc)
                Else
                    datDoc = CmsDocFlow.GetOneAttachment(CmsPassport.GenerateCmsPassportForInnerUse(""), lngResourceID, lngDocumentID, True)
                    If datDoc.strDOC2_EXT.ToLower() = "xls" Or datDoc.strDOC2_EXT.ToLower() = "doc" Or datDoc.strDOC2_EXT.ToLower() = "ppt" Or datDoc.strDOC2_EXT.ToLower() = "xlsx" Or datDoc.strDOC2_EXT.ToLower() = "docx" Or datDoc.strDOC2_EXT.ToLower() = "pptx" Then
                        If lngIsCheckOut <> 3 Then
                            '���ĵ��ķ��ʼ�¼���뻺��
                            If Configuration.DocumentAccessCache.Contains(CStr(lngDocumentID)) Then
                                If CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode = Me.CurrentUser.Code Then
                                    Configuration.DocumentAccessCache(CStr(lngDocumentID)) = New DocumentAccessLog(CType(Configuration.DocumentAccessCache(CStr(lngDocumentID)), DocumentAccessLog).UserCode)
                                End If
                            Else
                                Configuration.DocumentAccessCache.Add(CStr(lngDocumentID), New DocumentAccessLog(MyBase.CurrentUser.Code))
                            End If
                        End If
                        Response.Redirect("OfficeEditor.aspx?ResourceID=" & lngResourceID & "&DOCID=" & lngDocumentID & "&IsCheckOut=" & lngIsCheckOut)
                    Else
                        'FileTransfer.DownloadDoc(Response, datDoc)

                        Dim strFileName As String = datDoc.strDOC2_NAME & "." & datDoc.strDOC2_EXT
                        strFileName = System.Web.HttpUtility.UrlEncode(strFileName, Encoding.GetEncoding("UTF-8"))
                        Response.AddHeader("Content-Disposition", "inline;filename=" & strFileName)
                        Response.ContentType = "application/octet-stream"

                        '��ʼ��ͻ���д�ļ���
                        Response.BinaryWrite(datDoc.bytDOC2_FILE_BIN)
                        Response.Flush()

                        datDoc.bytDOC2_FILE_BIN = Nothing '�ֶ�����ĵ���ռ�ڴ�
                        datDoc = Nothing
                        Response.End()
                    End If
                End If
            Catch ex As Exception
                SLog.Err("���ظ��������쳣.", ex)
                Me.MessageBox(ex.Message)
                Me.Close()
            End Try
        End Sub
    End Class

End Namespace
