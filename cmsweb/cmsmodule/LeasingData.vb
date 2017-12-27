Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform

Public Class LeasingData
    Private m_dsMaterial As DataSet = Nothing '存放物品信息

    '------------------------------------------------------------------
    '初始化
    '------------------------------------------------------------------
    Public Sub New(ByRef pst As CmsPassport, ByVal strSection As String)
        '提取物品信息
        Dim lngResIDofMaterial As Long = GetConfig(pst).GetSecAttrByLong(strSection, "RESID")
        Dim strTableName As String = pst.GetDataRes(lngResIDofMaterial).ResTable
        Dim strSql As String = "SELECT * FROM " & strTableName
        m_dsMaterial = CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql)
    End Sub

    '------------------------------------------------------------------
    '显示图像
    '------------------------------------------------------------------
    Public Shared Sub ShowMaterialImage(ByRef pnl As Panel, ByRef datSvc As DataServiceSection, ByVal strSection As String, ByVal lngResIDofMaterial As Long, ByRef dr As DataRow, ByVal lngLeft As Long, ByVal lngTop As Long, ByVal lngWidth As Long, ByVal lngHeight As Long, ByVal strTooltip As String, ByVal strImageUrl As String)
        Dim img As New System.Web.UI.WebControls.Image
        img.ImageUrl = strImageUrl
        img.EnableViewState = True
        img.ToolTip = strTooltip '购买佛像施主的基本信息

        Dim strCol_MaterialStatus As String = datSvc.GetString(strSection, "MATERIAL_STATUS")
        Dim strMaterialStatus As String = DbField.GetStr(dr, strCol_MaterialStatus)
        Dim strMaterialStatusIdle As String = datSvc.GetKeyAttr(strSection, "MATERIAL_STATUS", "IDLE")
        If strMaterialStatus = strMaterialStatusIdle Then
            Dim strScript As String = "MaterialSelected("
            Dim i As Integer = 0
            For i = 1 To 10
                '获取当前待传回的字段值
                Dim strChildCol As String = datSvc.GetKeyAttr(strSection, "PARENTWIN_COL" & i, "MATERIAL_COL")
                Dim strChildColValue As String = DbField.GetStr(dr, strChildCol)
                strScript &= "'" & strChildColValue & "', "
            Next
            strScript = StringDeal.Trim(strScript, "", ",")
            strScript &= ");"
            img.Attributes.Add("onClick", strScript)

            Dim strStyle As String = "cursor:hand; POSITION: absolute; HEIGHT: " & lngHeight & "px; WIDTH: " & lngWidth & "px;top:" & lngTop & "px;left:" & lngLeft & "px; TEXT-ALIGN: center;"
            img.Attributes.Add("style", strStyle)
        Else
            Dim strStyle As String = "POSITION: absolute; HEIGHT: " & lngHeight & "px; WIDTH: " & lngWidth & "px;top:" & lngTop & "px;left:" & lngLeft & "px; TEXT-ALIGN: center;"
            img.Attributes.Add("style", strStyle)
        End If

        pnl.Controls.Add(img)
    End Sub

    '------------------------------------------------------------------
    '显示Label
    '------------------------------------------------------------------
    Public Shared Sub GenerateLabel(ByRef pnl As Panel, ByVal strValue As String, ByVal lngLeft As Long, ByVal lngTop As Long, ByVal lngWidth As Long, ByVal lngHeight As Long)
        Dim lbl As New Label
        lbl.Text = strValue
        Dim strStyle As String = "POSITION: absolute; HEIGHT: " & lngHeight & "px; WIDTH: " & lngWidth & "px;top:" & lngTop & "px;left:" & lngLeft & "px; TEXT-ALIGN: center;"
        lbl.Attributes.Add("style", strStyle)

        pnl.Controls.Add(lbl)
    End Sub

    '------------------------------------------------------------------
    '获取物品列表，用于显示
    '------------------------------------------------------------------
    Public Function GetMaterialData(ByRef pst As CmsPassport, ByVal strSection As String, Optional ByVal lngRow As Long = -1) As DataRow()
        '---------------------------------------------------------------------------------------
        Dim datSvc As DataServiceSection = LeasingData.GetConfig(pst)
        Dim strCol_Row As String = datSvc.GetString(strSection, "MATERIAL_ROW")
        Dim strOrderBy1 As String = datSvc.GetString(strSection, "MATERIAL_ORDER1").Trim()
        Dim strOrderBy2 As String = datSvc.GetString(strSection, "MATERIAL_ORDER2").Trim()
        Dim strOrderBy3 As String = datSvc.GetString(strSection, "MATERIAL_ORDER3").Trim()

        Dim strWhere As String = ""
        If lngRow = -1 Then
            strWhere = ""
        Else
            strWhere = strCol_Row & "=" & lngRow
        End If
        Dim strOrderBy As String = strOrderBy1 & ", " & strOrderBy2 & ", " & strOrderBy3
        strOrderBy = StringDeal.Trim(strOrderBy, ",", ",")
        strOrderBy = StringDeal.Trim(strOrderBy, ",", ",")
        '---------------------------------------------------------------------------------------

        Return m_dsMaterial.Tables(0).Select(strWhere, strOrderBy)
    End Function

    '------------------------------------------------------------------
    '获取物品列表，用于显示
    '------------------------------------------------------------------
    Public Function GetMaterialRows(ByRef pst As CmsPassport, ByVal strSection As String) As Integer
        Dim datSvc As DataServiceSection = LeasingData.GetConfig(pst)
        Dim strCol_Row As String = datSvc.GetString(strSection, "MATERIAL_ROW")
        Dim lngResIDofMaterial As Long = CLng(GetConfig(pst).GetSecAttr(strSection, "RESID"))

        Dim strTableName As String = pst.GetDataRes(lngResIDofMaterial).ResTable
        Dim strSql As String = "SELECT DISTINCT " & strCol_Row & " FROM " & strTableName
        Return CmsDbStatement.Query(SDbConnectionPool.GetDbConfig(), strSql).Tables(0).DefaultView.Count
    End Function

    '------------------------------------------------------------------
    '初始化
    '------------------------------------------------------------------
    Public Shared Function GetConfig(ByRef pst As CmsPassport) As DataServiceSection
        Dim strBriefLanguage As String = CmsMessage.GetBriefLanguage(pst.Employee.Language)

        Dim strClientCode As String = CmsConfig.GetClientCode()
        Dim strWzfFilePath As String = CmsConfig.ProjectRootFolder & "conf\client\" & strClientCode & "\client_" & strClientCode & "_leasing_" & strBriefLanguage & ".xml"
        Return New DataServiceSection(strWzfFilePath)
    End Function

    '------------------------------------------------------------------
    '初始化
    '------------------------------------------------------------------
    Public Shared Function GenerateScript(ByRef pst As CmsPassport, ByVal lngResIDofParent As Long, ByVal strSection As String) As String
        Dim strScript As String = "<script language=""javascript"">" & Environment.NewLine
        strScript &= "<!--" & Environment.NewLine
        strScript &= "function MaterialSelected(strVal1, strVal2, strVal3, strVal4, strVal5, strVal6, strVal7, strVal8, strVal9, strVal10){" & Environment.NewLine
        strScript &= "    try{" & Environment.NewLine
        strScript &= "        var ctlName;" & Environment.NewLine

        Dim datSvc As DataServiceSection = LeasingData.GetConfig(pst)
        Dim strCtrlPrefix As String = "TAB" & lngResIDofParent & "___"

        Dim strParentCol As String = datSvc.GetString(strSection, "PARENTWIN_COL1")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal1;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL2")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal2;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL3")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal3;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL4")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal4;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL5")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal5;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL6")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal6;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL7")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal7;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL8")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal8;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL9")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal9;" & Environment.NewLine
        End If

        strParentCol = datSvc.GetString(strSection, "PARENTWIN_COL10")
        If strParentCol <> "" Then
            strScript &= "        ctlName=eval('window.opener.Form1." & strCtrlPrefix & strParentCol & "');" & Environment.NewLine
            strScript &= "        ctlName.value=strVal10;" & Environment.NewLine
        End If

        strScript &= "    }catch(e){" & Environment.NewLine
        strScript &= "    }" & Environment.NewLine
        strScript &= "    window.close();" & Environment.NewLine
        strScript &= "}" & Environment.NewLine
        strScript &= "-->" & Environment.NewLine
        strScript &= "</script>" & Environment.NewLine

        Return strScript
    End Function
End Class
