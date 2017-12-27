Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Public Enum WebTreeType As Integer
    DepartmentOnly = 1
    ResourceOnly = 2
    DepAndRes = 3
End Enum

Public Class WebTreeview
    '-----------------------------------------------------
    '���ɲ������ṹ
    '-----------------------------------------------------
    Public Shared Sub GenerateDepTree(ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByRef dvRes As DataView, ByVal treeType As WebTreeType, ByVal strDepUrl As String, ByVal strDepTarget As String, Optional ByVal strTreeName As String = "cmst")
            Dim dt As DataTable = dvRes.Table
            dt.PrimaryKey = New DataColumn() {dt.Columns("ID")}
        Dim drs() As DataRow = dt.Select("PID<>0")
        For i As Integer = 0 To drs.Length - 1
                Dim tempPid As String = DbField.GetStr(drs(i), ("PID"))

                If dt.Rows.Find(tempPid) Is Nothing Then
                    drs(i)("pid") = 0
                End If
            Next
        Dim drv As DataRowView
        For Each drv In dvRes
            Dim lngNodeID As Long = DbField.GetLng(drv, ("ID"))
            Dim lngNodePID As Long = DbField.GetLng(drv, ("PID"))
            Dim strDepName As String = DbField.GetStr(drv, ("NAME"))
            Dim strIconName As String = DbField.GetStr(drv, ("ICON_NAME"))
            WebTreeview.AddOneNode(Request, Response, WebTreeType.DepAndRes, lngNodeID, lngNodePID, lngNodeID, True, strDepName, "", "", strDepUrl, strDepTarget, "", "", strIconName, strTreeName)
        Next
    End Sub

    '-----------------------------------------------------
    '������Դ���ṹ
    '-----------------------------------------------------
    Public Shared Sub GenerateResourceTree(ByRef Request As System.Web.HttpRequest, ByRef Response As System.Web.HttpResponse, ByRef dvRes As DataView, ByVal treeType As WebTreeType, ByVal strResUrl As String, ByVal strResTarget As String, Optional ByVal strTreeName As String = "cmst", Optional ByVal lngForceNodePID As Long = 0, Optional ByVal blnIncludeDepIDinResUrl As Boolean = False)

        Dim drv As DataRowView
        For Each drv In dvRes
            Dim lngNodeID As Long = DbField.GetLng(drv, ("ID"))
            Dim lngNodePID As Long = DbField.GetLng(drv, ("PID"))
            If lngForceNodePID <> 0 Then lngNodePID = lngForceNodePID
            Dim lngDepID As Long = DbField.GetLng(drv, ("HOST_ID"))
            Dim strResName As String = DbField.GetStr(drv, ("NAME"))
            Dim strIconName As String = CStr(IIf(DbField.GetInt(drv, ("RES_ISFLOW")) = 1, "ICON_RES_WORKFLOW", DbField.GetStr(drv, ("RES_ICONNAME"))))
            WebTreeview.AddOneNode(Request, Response, treeType, lngNodeID, lngNodePID, lngDepID, False, strResName, "", "", "", "", strResUrl, strResTarget, strIconName, strTreeName, , blnIncludeDepIDinResUrl)
        Next
    End Sub

    '-----------------------------------------------------
    '�����ṹ������һ���ڵ�
    '-----------------------------------------------------
    Public Shared Sub AddOneNode( _
        ByRef Request As System.Web.HttpRequest, _
        ByRef Response As System.Web.HttpResponse, _
        ByVal treeType As WebTreeType, _
        ByVal lngNodeID As Long, _
        ByVal lngNodePID As Long, _
        ByVal lngDepID As Long, _
        ByVal blnIsDep As Boolean, _
        ByVal strNodeName As String, _
        ByVal strRootUrl As String, _
        ByVal strRootTarget As String, _
        ByVal strDepUrl As String, _
        ByVal strDepTarget As String, _
        ByVal strResUrl As String, _
        ByVal strResTarget As String, _
        ByVal strIconName As String, _
        Optional ByVal strTreeName As String = "cmst", _
        Optional ByVal strResParamPrefix As String = "?", _
        Optional ByVal blnIncludeDepIDinResUrl As Boolean = False _
        )
        '��ȡ�ڵ�����
        Dim strNodeText As String = CStr(IIf(CmsConfig.ShowIDForCms = True, strNodeName & "(" & lngNodeID & ")", strNodeName))

        '��ȡ������URL�ϵĲ���
        Dim strUrlParams As String = WebTreeview.GenerateUrlParams(Request)

        '��ȡ�ڵ�ID
        Dim strNodeID As String = ""
        If lngNodePID = -1 Then '���ڵ�
            strNodeID = "-1_1"
        ElseIf blnIsDep = True AndAlso lngNodePID = 0 Then '�����Žڵ�
            strNodeID = "1_" & lngNodeID
        ElseIf blnIsDep = True AndAlso lngNodePID <> 0 Then '�Ӳ��Žڵ�
            strNodeID = lngNodePID & "_" & lngNodeID
        ElseIf lngNodePID = 0 Then '����Դ
            If treeType = WebTreeType.ResourceOnly Then
                strNodeID = "1_" & lngNodeID
            Else 'If treeType = WebTreeType.DepAndRes Then
                strNodeID = lngDepID & "_" & lngNodeID
            End If
        Else '����Դ
            strNodeID = lngNodePID & "_" & lngNodeID
        End If

        '��ȡ�ڵ�URL��TARGET
        Dim strNodeUrl As String = "url:#;"
        Dim strNodeTarget As String = ""
        If lngNodePID = -1 Then '���ڵ�
            '��ȡ���ڵ�URL�еĽڵ�ID����
            Dim strTempParam As String = CStr(IIf(treeType = WebTreeType.ResourceOnly, "depid=" & lngDepID & "&noderesid=0", "depid=0"))

            If strRootUrl <> "" AndAlso strRootUrl <> "#" Then strNodeUrl = "url:" & CmsUrl.AppendParam(strRootUrl, strTempParam) & strUrlParams & ";"
            If strRootTarget <> "" Then strNodeTarget = "target:" & strRootTarget & ";"
        ElseIf blnIsDep = True Then '���Žڵ�
            If strDepUrl <> "" AndAlso strDepUrl <> "#" Then strNodeUrl = "url:" & CmsUrl.AppendParam(strDepUrl, "depid=" & lngNodeID) & strUrlParams & ";"
            If strDepTarget <> "" Then strNodeTarget = "target:" & strDepTarget & ";"
        Else '��Դ�ڵ�
            If strResUrl <> "" AndAlso strResUrl <> "#" Then strNodeUrl = "url:" & strResUrl & strResParamPrefix & CStr(IIf(blnIncludeDepIDinResUrl = True, "depid=" & lngDepID & "&", "")) & "noderesid=" & lngNodeID & strUrlParams & ";"
            If strResTarget <> "" Then strNodeTarget = "target:" & strResTarget & ";"
        End If

            '����ڵ���Ϣ
            Dim nodinfo As String = ""
            If (strNodeID.Substring(0, 2).Equals("1_")) Then
                nodinfo = vbCrLf & strTreeName & ".nodes[""" & strNodeID & """]=""" & "text:" & strNodeText & ";icon:ICON_RES_HOME;" & strNodeUrl & strNodeTarget & """"
            Else
                nodinfo = vbCrLf & strTreeName & ".nodes[""" & strNodeID & """]=""" & "text:" & strNodeText & ";icon:" & strIconName & ";" & strNodeUrl & strNodeTarget & """"
            End If

            Response.Write(nodinfo)
        End Sub

    '-----------------------------------------------------
    '�������ڵ�ǰ��׼��
    '-----------------------------------------------------
    Public Shared Sub TreePrepare(ByRef resp As System.Web.HttpResponse, Optional ByVal strTreeName As String = "cmst")
        Dim strScript As String = vbCrLf & "<SCRIPT LANGUAGE=""JavaScript"">"
        strScript &= vbCrLf & "<!--"
        strScript &= vbCrLf & "window." & strTreeName & " = new DFlowTree('" & strTreeName & "');"
        strScript &= vbCrLf & strTreeName & ".icons['ICON_ENTERPRISE'] = 'enterprise.gif';"
        strScript &= vbCrLf & strTreeName & ".icons['ICON_DEP_REAL'] = 'dep_real.gif';"
            strScript &= vbCrLf & strTreeName & ".icons['ICON_DEP_VIRTUAL'] = 'dep_virtual.gif';"
            strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_HOME'] = 'res_empty.png';"
            strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_EMPTY'] = 'res_empty.gif';"
            strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_TWOD'] = 'res_twod.png';"
        strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_TWOD_JC'] = 'res_twod_jc.gif';"
            strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_DOC'] = 'res_doc.png';"
            strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_VIEW'] = 'view.jpg';"
        strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_DOC_JC'] = 'res_doc_jc.gif';"
        strScript &= vbCrLf & strTreeName & ".icons['ICON_RES_WORKFLOW'] = 'res_workflow.gif';"
        strScript &= vbCrLf & strTreeName & ".icons['ICON_EMP'] = 'emp1.gif';"
        strScript &= vbCrLf & strTreeName & ".iconsExpand['ICON_FOLDER'] = 'folder.gif';"
        strScript &= vbCrLf & strTreeName & ".iconsExpand['ICON_FOLDER_OPEN'] = 'folderopen.gif';"
        strScript &= vbCrLf & strTreeName & ".setIconPath('/cmsweb/images/tree/');"
        strScript &= vbCrLf
        resp.Write(strScript)
    End Sub

    '-----------------------------------------------------
    '�������ڵ��λ��ָ���ڵ�
    '-----------------------------------------------------
    'Public Shared Sub TreeNodeFocus(ByRef Response As System.Web.HttpResponse, ByVal strClickDepID As String, ByVal strClickedResID As String, Optional ByVal strTreeName As String = "cmst")
    Public Shared Sub TreeNodeFocus(ByRef Response As System.Web.HttpResponse, ByVal strFocusNodeID As String, Optional ByVal strTreeName As String = "cmst")
       
        If strFocusNodeID = "" Then strFocusNodeID = "0"
        Dim strScript As String = vbCrLf & "document.write(" & strTreeName & ".toString());"
        strScript &= vbCrLf & "setTimeout(""" & strTreeName & ".focus('" & strFocusNodeID & "', true); " & strTreeName & ".expand(" & strTreeName & ".currentNode.id, true);"",10);"
        strScript &= vbCrLf & "//-->"
        strScript &= vbCrLf & "</SCRIPT>"
        strScript &= vbCrLf
        Response.Write(strScript)
    End Sub

    '-----------------------------------------------------
    'Load�Զ�������ṹ
    '-----------------------------------------------------
    Public Shared Function GenerateUrlParams(ByRef Request As System.Web.HttpRequest) As String
        Dim strMnuResID As String = AspPage.RStr("mnuresid", Request)
        Dim strMnuRecID As String = AspPage.RStr("mnurecid", Request)
        Dim strMnuDepID As String = AspPage.RStr("tmpdepid", Request)
        Return CStr(IIf(strMnuResID = "", "", "&mnuresid=" & strMnuResID)) & CStr(IIf(strMnuRecID = "", "", "&mnurecid=" & strMnuRecID)) & CStr(IIf(strMnuDepID = "", "", "&tmpdepid=" & strMnuDepID))
    End Function
End Class

End Namespace
