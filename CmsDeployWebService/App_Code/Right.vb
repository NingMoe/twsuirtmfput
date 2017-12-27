Imports Microsoft.VisualBasic
Imports Unionsoft.Platform
Imports NetReusables
Imports System.Collections.Generic

Public Class Right

    Public Shared Function GetSingleFilter(ByVal lngResID As Long, ByVal UserID As String) As String  '��ȡ�����й���
        Dim pst As Unionsoft.Platform.CmsPassport = CmsPassport.GenerateCmsPassport(UserID)
        Return MultiTableSearch.AssemblePersonalWhereForSqlQuery(pst, lngResID, UserID)
    End Function


    Public Shared Function GetRights(ByVal UserID As String, ByVal ResourceID As String) As RightInfo()
        Dim CommonWhere As String = " AND QX_OBJECT_ID=" & ResourceID & " AND QX_NAME='0'"
        Dim strWhere As String = "(QX_GAINER_ID='" & UserID & "'" + CommonWhere + ")" '����Ȩ��
        strWhere = strWhere + " or (QX_OBJECT_ID='0'" + CommonWhere + ")"  '��ҵȨ��
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select HOST_ID from dbo.CMS_EMPLOYEE where emp_id='" + UserID + "'))"  '����Ȩ��
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select vdep_depid from dbo.CMS_DEP_VIRTUAL where vdep_empid='" + UserID + "'))"  '��ɫȨ��

        Dim datQx As New DataCmsRights
        Dim strNoAccessCols As String = ""
        Dim strRowWhere As String = ""
        Dim strFormName As String = ""

        Dim dv As DataView = New DataView
        dv.Table = SDbStatement.Query("select * from CMS_RIGHTS where " + strWhere).Tables(0) ' CmsDbBase.GetTableView(pst, CmsTables.Rights, strWhere)
        dv.Sort = " QX_GAINER_TYPE ASC, QX_GAINER_ID DESC " '���д�����Ա�����š���ҵ
        Dim drv As DataRowView
        For Each drv In dv
            '----------------------------------------------------------------------
            '����ģʽ�У���Ա�����š���ҵ��Ȩ��ֵ�ϲ�
            '����Ȩ����ȡ�����û������š����ⲿ�š���ҵ�����л���Ȩ�ޡ�����ϡ�
            datQx.lngQX_VALUE = datQx.lngQX_VALUE Or DbField.GetLng(drv, "QX_VALUE")

            '�˵�Ȩ����ȡ�����û������š����ⲿ�š���ҵ�����в˵�Ȩ�ޡ�����ϡ�
            datQx.lngQX_PROJVAL = datQx.lngQX_PROJVAL Or DbField.GetLng(drv, "QX_PROJVAL")

            '���ֶ�Ȩ����ȡ�����û������š����ⲿ�š���ҵ���������ֶ�Ȩ�ޡ�����ϡ�
            Dim strTemp As String = DbField.GetStr(drv, "QX_ACCESS_COLS")
            strTemp = StringDeal.Trim(strTemp, ",", ",")
            If strTemp <> "" Then strNoAccessCols &= "," & strTemp

        Next

        datQx.strQX_ACCESS_COLS = StringDeal.Trim(strNoAccessCols, ",", ",")
        Return GetRights(datQx)

    End Function

    Public Shared Function GetColumns(ByVal UserID As String, ByVal ResourceID As String) As String
        Dim CommonWhere As String = " AND QX_OBJECT_ID=" & ResourceID & " AND QX_NAME='0'"
        Dim strWhere As String = "(QX_GAINER_ID='" & UserID & "'" + CommonWhere + ")" '����Ȩ��
        strWhere = strWhere + " or (QX_OBJECT_ID='0'" + CommonWhere + ")"  '��ҵȨ��
        strWhere = strWhere + " or (QX_GAINER_TYPE='1' and QX_INHERIT='1'" + CommonWhere + " and QX_GAINER_ID in (select HOST_ID from dbo.CMS_EMPLOYEE where emp_id='" + UserID + "'))"  '����Ȩ��
        strWhere = strWhere + " or (QX_GAINER_TYPE='1'" + CommonWhere + " and QX_GAINER_ID in (select vdep_depid from dbo.CMS_DEP_VIRTUAL where vdep_empid='" + UserID + "'))"  '��ɫȨ��


        Dim strNoAccessCols As String = ""
        Dim strRowWhere As String = ""
        Dim strFormName As String = ""

        Dim dv As DataView = New DataView
        dv.Table = SDbStatement.Query("select * from CMS_RIGHTS where " + strWhere).Tables(0) ' CmsDbBase.GetTableView(pst, CmsTables.Rights, strWhere)
        dv.Sort = " QX_GAINER_TYPE ASC, QX_GAINER_ID DESC " '���д�����Ա�����š���ҵ
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
