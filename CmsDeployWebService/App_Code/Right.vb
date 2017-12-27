Imports Microsoft.VisualBasic
Imports Unionsoft.Platform
Imports NetReusables
Imports System.Collections.Generic

Public Class Right

    Public Shared Function GetSingleFilter(ByVal lngResID As Long, ByVal UserID As String) As String  '获取个人行过滤
        Dim pst As Unionsoft.Platform.CmsPassport = CmsPassport.GenerateCmsPassport(UserID)
        Return MultiTableSearch.AssemblePersonalWhereForSqlQuery(pst, lngResID, UserID)
    End Function


    Public Shared Function GetRights(ByVal UserID As String, ByVal ResourceID As String) As RightInfo()
        Dim CommonWhere As String = " AND QX_OBJECT_ID=" & ResourceID & " AND QX_NAME='0'"
        Dim strWhere As String = "(QX_GAINER_ID='" & UserID & "'" + CommonWhere + ")" '个人权限
        strWhere = strWhere + " or (QX_OBJECT_ID='0'" + CommonWhere + ")"  '企业权限
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select HOST_ID from dbo.CMS_EMPLOYEE where emp_id='" + UserID + "'))"  '部门权限
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select vdep_depid from dbo.CMS_DEP_VIRTUAL where vdep_empid='" + UserID + "'))"  '角色权限

        Dim datQx As New DataCmsRights
        Dim strNoAccessCols As String = ""
        Dim strRowWhere As String = ""
        Dim strFormName As String = ""

        Dim dv As DataView = New DataView
        dv.Table = SDbStatement.Query("select * from CMS_RIGHTS where " + strWhere).Tables(0) ' CmsDbBase.GetTableView(pst, CmsTables.Rights, strWhere)
        dv.Sort = " QX_GAINER_TYPE ASC, QX_GAINER_ID DESC " '排列次序：人员、部门、企业
        Dim drv As DataRowView
        For Each drv In dv
            '----------------------------------------------------------------------
            '下面模式中，人员、部门、企业的权限值合并
            '基本权限提取：将用户、部门、虚拟部门、企业的所有基本权限“或组合”
            datQx.lngQX_VALUE = datQx.lngQX_VALUE Or DbField.GetLng(drv, "QX_VALUE")

            '菜单权限提取：将用户、部门、虚拟部门、企业的所有菜单权限“或组合”
            datQx.lngQX_PROJVAL = datQx.lngQX_PROJVAL Or DbField.GetLng(drv, "QX_PROJVAL")

            '列字段权限提取：将用户、部门、虚拟部门、企业的所有列字段权限“与组合”
            Dim strTemp As String = DbField.GetStr(drv, "QX_ACCESS_COLS")
            strTemp = StringDeal.Trim(strTemp, ",", ",")
            If strTemp <> "" Then strNoAccessCols &= "," & strTemp

        Next

        datQx.strQX_ACCESS_COLS = StringDeal.Trim(strNoAccessCols, ",", ",")
        Return GetRights(datQx)

    End Function

    Public Shared Function GetColumns(ByVal UserID As String, ByVal ResourceID As String) As String
        Dim CommonWhere As String = " AND QX_OBJECT_ID=" & ResourceID & " AND QX_NAME='0'"
        Dim strWhere As String = "(QX_GAINER_ID='" & UserID & "'" + CommonWhere + ")" '个人权限
        strWhere = strWhere + " or (QX_OBJECT_ID='0'" + CommonWhere + ")"  '企业权限
        strWhere = strWhere + " or (QX_GAINER_TYPE='1' and QX_INHERIT='1'" + CommonWhere + " and QX_GAINER_ID in (select HOST_ID from dbo.CMS_EMPLOYEE where emp_id='" + UserID + "'))"  '部门权限
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select vdep_depid from dbo.CMS_DEP_VIRTUAL where vdep_empid='" + UserID + "'))"  '角色权限


        Dim strNoAccessCols As String = ""
        Dim strRowWhere As String = ""
        Dim strFormName As String = ""

        Dim dv As DataView = New DataView
        dv.Table = SDbStatement.Query("select * from CMS_RIGHTS where " + strWhere).Tables(0) ' CmsDbBase.GetTableView(pst, CmsTables.Rights, strWhere)
        dv.Sort = " QX_GAINER_TYPE ASC, QX_GAINER_ID DESC " '排列次序：人员、部门、企业
        Dim drv As DataRowView
        For Each drv In dv
  

            strNoAccessCols = strNoAccessCols + "," + DbField.GetStr(drv, "QX_ACCESS_COLS")
           

        Next

        If strNoAccessCols <> "" Then
            strNoAccessCols = strNoAccessCols.Remove(0, 1)
        End If


        Return strNoAccessCols

    End Function

    Public Shared Function GetRights(ByVal datQx As DataCmsRights) As RightInfo()
        Dim lngRightsValue As Long = datQx.lngQX_VALUE
        Dim MyList As RightInfo()
        Dim RightInfo As RightInfo

        Dim CmsRightsDefineType As Type = GetType(CmsRightsDefine)
        Dim Arrays As Array = [Enum].GetValues(CmsRightsDefineType)
        ReDim MyList(Arrays.LongLength - 1)
        For i As Integer = 0 To Arrays.LongLength - 1

            RightInfo = New RightInfo
            RightInfo.Name = [Enum].GetName(CmsRightsDefineType, Arrays.GetValue(i))
            If (lngRightsValue And Convert.ToInt64(Arrays.GetValue(i))) = Convert.ToInt64(Arrays.GetValue(i)) Then

                RightInfo.Value = True
            Else
                RightInfo.Value = False
            End If
            MyList(i) = RightInfo
        Next
        Return MyList
    End Function
End Class
