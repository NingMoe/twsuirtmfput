Option Strict On
Option Explicit On 

Imports System.IO
Imports Unionsoft.Platform
Imports NetReusables
Imports System.Text.RegularExpressions


Namespace Unionsoft.Cms.Web


Partial Class ResourceExport
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

        If VStr("PAGE_SEARCHRESULT") = "" Then
            ViewState("PAGE_SEARCHRESULT") = RStr("getsearchresult")
        End If
        If VLng("PAGE_MNURESLOCATE") = 0 Then
            ViewState("PAGE_MNURESLOCATE") = RLng("MNURESLOCATE")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
        '��ֹ��ȷ�ϡ��ύ��ť��������
        btnConfirm.Attributes.Add("onClick", "return ConfirmButtonClicked();")
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        '��ʼ��DropDownList�е����ļ�����
        LoadFieldAdvSettingOptions()

        '�ж�����Ȩ��
        Dim lngRightsValue As Long = CmsRights.GetRightsData(CmsPass, VLng("PAGE_RESID"), CmsPass.Employee.ID).lngQX_VALUE
        If (lngRightsValue And CmsRightsDefine.MgrResExport) = CmsRightsDefine.MgrResExport Then '��Ȩ��
        Else '��Ȩ��
            PromptMsg("��û�жԵ�ǰ��Դ(" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName & ")�ĵ���Ȩ�ޣ�")
            btnConfirm.Visible = False
            'If CmsPass.EmpIsSysAdmin Or CmsPass.EmpIsAspAdmin Or CmsPass.EmpIsAspClientAdmin Or CmsPass.EmpIsDepAdmin Then
            '    '����Ա�����Ե�����Դ
            'Else
            '    PromptMsg("��û�жԵ�ǰ��Դ(" & CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName & ")�ĵ���Ȩ�ޣ�")
            '    btnConfirm.Visible = False
            'End If
            Return
        End If
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

#Region "��������"
    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Session("CMSBP_ResourceExportResult") = VStr("PAGE_BACKPAGE")

        Dim strPageSuccess As String = "/cmsweb/adminres/ResourceExportResult.aspx?result=1"
        Dim strPageFailure As String = "/cmsweb/adminres/ResourceExportResult.aspx?result=0"
        '-------------------------------------------------------------------
        '��ȡ��Դ�����Լ�����ӵ�С���ȫ���ơ�Ȩ�޵�����Դ�����ָ�ʽ��1���ַ�����ʽ���б�23,456,11)��2��ArrayList��3��Hashstable
        '���в�����Դ�������ڵ�����Դ������û�С���ȫ���ơ�Ȩ�ޣ���Ϊ����ʾӵ�С���ȫ���ơ�������Դ��������ʾ
        Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
        Dim iRes As IResource = ResFactory.ResService()
        Dim hashChildRes2 As New Hashtable
        Dim strResIDList2 As String = ""
        Dim blnExpChildRes As Boolean = chkExpChild.Checked
        If blnExpChildRes Then
            Dim hashResOfHasRights As Hashtable = iRes.GetResourceByHashtableWithUserRights(CmsPass, CmsRightsDefine.RecView, True, True, , VLng("PAGE_RESID"))
            Dim strAllParentResIDs As String = iRes.GetParentResIDsByString(CmsPass, VLng("PAGE_RESID"))
            strAllParentResIDs = "," & strAllParentResIDs & ","

            Dim en As IDictionaryEnumerator = hashResOfHasRights.GetEnumerator()
            While en.MoveNext
                Dim lngResIDTemp As Long = CLng(en.Key)
                If strAllParentResIDs.IndexOf("," & lngResIDTemp & ",") < 0 Then 'ȥ��ѡ����Դ�ĸ���Դ
                    Dim strResTable As String = iRes.GetResTable(CmsPass, lngResIDTemp)
                    hashChildRes2.Add(CStr(lngResIDTemp), strResTable)
                    strResIDList2 &= CStr(lngResIDTemp) & ","
                End If
            End While
        End If
        If hashChildRes2.Count <= 0 OrElse hashChildRes2.ContainsKey(VStr("PAGE_RESID")) = False Then '�����Դ����
            hashChildRes2.Add(CStr(VLng("PAGE_RESID")), datRes.ResTable)
            strResIDList2 &= CStr(VLng("PAGE_RESID")) & ","
        End If
        strResIDList2 = StringDeal.Trim(strResIDList2, ",", ",")

        Dim mark As Boolean = False
        Dim lngResID As String
        Dim strRst As String = ""
        Dim reg As Regex = New Regex("[\W]")
        For Each lngResID In strResIDList2.Split(CChar(","))
            Dim ds As DataSet = CTableStructure.GetColumnsByDataset(CmsPass, CLng(lngResID), True, True)
            Dim dv As DataView = ds.Tables(0).DefaultView
            Dim drv As DataRowView
            Dim strResName As String = CmsPass().GetDataRes(CLng(lngResID)).ResName
            Dim rstCol As String = ""
            For Each drv In dv
                Dim strColDispName As String = DbField.GetStr(drv, "CD_DISPNAME")
                If reg.IsMatch(strColDispName) Then
                    rstCol += "," & strColDispName
                    mark = True
                End If
            Next
            If mark Then
                strRst += strResName & "�����ֶ�:" & rstCol.Substring(1, rstCol.Length - 1) & ";"
            End If
        Next
        If mark Then
            Session("CMS_IMPRES_MESSAGE") = strRst & "���������ַ������ݲ��ܽ��е������������ֶ�����"
            Response.Redirect(strPageFailure, True)
        End If

        '-------------------------------------------------------------------
        Try
            '-------------------------------------------------------------------
            'SQL���ݿ�������������ݽṹ
                Dim dbcSrc As DbConfig = SDbConnectionPool.GetDbConfig()
            '-------------------------------------------------------------------

            '-------------------------------------------------------------------
            '������MDB�ļ������ݿ�������������ݽṹ
            Dim dbcDest As New DbConfig
            Dim strUnique As String = CStr(TimeId.CurrentMilliseconds())
            'Dim datRes As DataResource = CmsPass.GetDataRes(VLng("PAGE_RESID"))
            Dim strTempFolder As String = CmsConfig.TempFolder & "expimp\"
            If Directory.Exists(strTempFolder) = False Then Directory.CreateDirectory(strTempFolder)
            If ddlExportFileType.SelectedValue = "mdb" Then
                dbcDest.DatabaseType = DbConfig.DbType.MsAccess
                dbcDest.DbFilePath = strTempFolder & datRes.ResName & "__" & strUnique & "." & ddlExportFileType.SelectedValue
                dbcDest.User = "Admin"
                dbcDest.Pass = ""
                File.Copy(CmsConfig.ProjectRootFolder & "data\sql\empty.mdb", dbcDest.DbFilePath)
            ElseIf ddlExportFileType.SelectedValue = "xls" Then
                dbcDest.DatabaseType = DbConfig.DbType.MsExcel
                dbcDest.DbFilePath = strTempFolder & datRes.ResName & "__" & strUnique & "." & ddlExportFileType.SelectedValue
                dbcDest.User = ""
                dbcDest.Pass = ""
            ElseIf ddlExportFileType.SelectedValue = "txt" Then
                dbcDest.DatabaseType = DbConfig.DbType.Text
                'dbcDest.DbFilePath = strTempFolder & datRes.ResName & "__" & strUnique & "." & ddlExportFileType.SelectedValue
                dbcDest.TextExtProperties = "'text;HDR=No;FMT=TabDelimited'"
                dbcDest.DbFilePath = "c:\test300.txt"
                dbcDest.User = ""
                dbcDest.Pass = ""
                File.Copy(CmsConfig.ProjectRootFolder & "data\sql\empty.txt", dbcDest.DbFilePath)
            End If
            Session("CMSTRANS_EXPORTFILE") = dbcDest.DbFilePath
            '-------------------------------------------------------------------

            Dim blnRtn As Boolean = False
            Dim strResult As String = ""

            '-------------------------------------------------------------------
            '������Դ������
            Dim strWhere As String = ""
            If VStr("PAGE_SEARCHRESULT") = "1" Then '������ѯ���
                Dim lngResLocate As ResourceLocation = CType(VLng("PAGE_MNURESLOCATE"), ResourceLocation)
                If lngResLocate = ResourceLocation.HostTable Then
                    strWhere = SStr("CMS_HOSTTABLE_WHERE")
                ElseIf lngResLocate = ResourceLocation.RelTable Then
                    strWhere = SStr("CMS_SUBTABLE_WHERE")
                    If strWhere = "" Then
                        '��������ӱ�Ĳ�ѯ����Ϊ�գ��������ȡ��������ֵ
                        strWhere = CmsWhere.AssemblyWhereOfTableRelation(CmsPass, RLng("mnuhostresid"), VLng("PAGE_RESID"), RLng("mnuhostrecid"))
                    Else
                        '��������ӱ�Ĳ�ѯ������Ϊ�գ����������ֵ��Ȼ�Ѿ���������
                    End If
                End If
            Else '����������Դ������
                strWhere = ""
            End If
            Dim strRedirectUrl As String = ""
            blnRtn = TransResTable.ExportResTableData(CmsPass, hashChildRes2, dbcDest, chkContinueOnError.Checked, strWhere)
            If blnRtn Then
                strResult &= "������Դ�����ݳɹ���" & "<BR><BR>"
                strRedirectUrl = strPageSuccess
            Else
                strResult &= "������Դ������ʧ�ܣ�" & "<BR><BR>"
                strRedirectUrl = strPageFailure
            End If
            '-------------------------------------------------------------------

            Session("CMS_IMPRES_MESSAGE") = strResult
            Response.Redirect(strRedirectUrl, False)
        Catch ex As Exception
            PromptMsg("������Դ�쳣ʧ�ܣ�û���ļ�����Ȩ�ޣ�", ex, True)
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
#End Region
    '-----------------------------------------------------------
    '��ʼ��DropDownList�е����ļ�����
    '-----------------------------------------------------------
    Private Sub LoadFieldAdvSettingOptions()
        ddlExportFileType.Items.Clear()
        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") And CmsFunc.IsEnable("FUNC_RES_EXP_EXCEL") Then
            ddlExportFileType.Items.Add(New ListItem("MS EXCEL", "xls"))
        End If
        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") And CmsFunc.IsEnable("FUNC_RES_EXP_ACCESS") Then
            ddlExportFileType.Items.Add(New ListItem("MS ACCESS", "mdb"))
        End If
        If CmsFunc.IsEnable("FUNC_RES_EXP_OTHERTABLE") And CmsFunc.IsEnable("FUNC_RES_EXP_TEXT") Then
            ddlExportFileType.Items.Add(New ListItem("Text", "txt"))
        End If
    End Sub
End Class

End Namespace
