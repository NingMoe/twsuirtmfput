Option Strict On
Option Explicit On 

Imports System.Text
Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DocViewAgent
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '��ȡ��������������ĵ����ͣ��Լ���Ӧ�Ĵ���ҳ��
            Dim hashOnlineViewFiles As Hashtable = GetOnlineViewFiles()

            Dim lngResID As Long = RLng("mnuresid")
            If lngResID = 0 Then lngResID = RLng("resid")
            Dim lngRecID As Long = RLng("docrecid")
            If lngRecID = 0 Then lngRecID = RLng("mnurecid")



            Dim datDoc As DataDocument = ResFactory.TableService(CmsPass.GetDataRes(lngResID).ResTableType).GetDocument(CmsPass, lngResID, lngRecID, False)

            If datDoc.lngDOCID > 0 Then
                Dim strUrl As String = ""
                Dim strFileExt As String = datDoc.strDOC2_EXT.Trim.ToUpper()
                If HashField.ContainsKey(hashOnlineViewFiles, strFileExt) Then '
                    Dim en As New System.Collections.DictionaryEntry
                    For Each en In hashOnlineViewFiles
                        If en.Key.ToString().ToUpper.Trim = strFileExt.ToUpper.Trim Then
                            strUrl = en.Value.ToString + "?DOCID=" + datDoc.lngDOCID.ToString
                            Exit For
                        End If
                    Next
                    'If strFileExt = "DWG" Then
                    '    strUrl = "/cmsweb/cmsdocument/DocViewCAD.aspx?DOCID=" + datDoc.lngDOCID.ToString
                    'Else
                    '    strUrl = "/cmsweb/cmsdocument/DocumentView.aspx?DOCID=" + datDoc.lngDOCID.ToString
                    'End If
                Else
                    MsgBox(CmsMessage.GetMsg(CmsPass, "VIEW_DOCUMENT"))
                    Return
                End If
                Response.Redirect(strUrl, False)
            End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Protected Overrides Sub CmsPageDealAllRequestsBeforeExit()
    End Sub
  
        '    '----------------------------------------------------------
        '    '��ȡ��������������ĵ����ͣ��Լ���Ӧ�Ĵ���ҳ��
        '    '----------------------------------------------------------
        'Private Function GetOnlineViewFiles() As Hashtable
        '    Dim hashOnlineViewFiles As New Hashtable

        '    Dim datSvc As DataServiceSection = CmsConfig.GetConfig()
        '        Dim strKeys As String = datSvc.GetString("SYS_ONLINEVIEW_FILES", "FILE_ONLINEVIEW") 'datSvc.GetKeys("SYS_ONLINEVIEW_FILES")

        '        Dim ch() As Char = {CType(",", Char)}

        '        Dim strValues() As String = strKeys.Split(ch, StringSplitOptions.RemoveEmptyEntries)

        '        For i As Integer = 0 To strValues.Length - 1
        '            hashOnlineViewFiles.Add(strValues(i).ToUpper.Trim, strValues(i).ToUpper.Trim)
        '        Next

        '        'Dim strOneKey As String
        '        'For Each strOneKey In strKeys
        '        '    Dim strOneFileType As String = datSvc.GetKeyAttr("SYS_ONLINEVIEW_FILES", strOneKey, "FILE_TYPE")
        '        '    Dim strUrl As String = datSvc.GetString("SYS_ONLINEVIEW_FILES", strOneKey)
        '        '    hashOnlineViewFiles.Add(strOneFileType, strUrl)
        '        'Next
        '    Return hashOnlineViewFiles
        'End Function



        '----------------------------------------------------------
        '��ȡ��������������ĵ����ͣ��Լ���Ӧ�Ĵ���ҳ��
        '----------------------------------------------------------
        Private Function GetOnlineViewFiles() As Hashtable
            Dim hashOnlineViewFiles As New Hashtable

            Dim datSvc As DataServiceSection = CmsConfig.GetConfig()
            Dim strKeys As ArrayList = datSvc.GetKeys("SYS_ONLINEVIEW_FILES")
            Dim strOneKey As String
            For Each strOneKey In strKeys
                Dim strOneFileType As String = datSvc.GetKeyAttr("SYS_ONLINEVIEW_FILES", strOneKey, "FILE_TYPE")
                Dim strUrl As String = datSvc.GetString("SYS_ONLINEVIEW_FILES", strOneKey)
                hashOnlineViewFiles.Add(strOneFileType, strUrl)
            Next
            Return hashOnlineViewFiles
        End Function
End Class

End Namespace
