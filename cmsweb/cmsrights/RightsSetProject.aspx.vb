Option Strict On
Option Explicit On 

Imports System.IO

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RightsSetProject
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
        InitializeComponent()
    End Sub

#End Region

    Private Const PROJQX_SECPRIFIX As String = "MENU_RES_"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Overrides Sub CmsPageSaveParametersToViewState()
        MyBase.CmsPageSaveParametersToViewState()

        If VStr("PAGE_GAINERID") = "" Then
            ViewState("PAGE_GAINERID") = RStr("gainerid")
        End If
        If VLng("PAGE_GAINERTYPE") = 0 Then
            ViewState("PAGE_GAINERTYPE") = RLng("gainertype")
        End If
    End Sub

    Protected Overrides Sub CmsPageInitialize()
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()
        LoadProjQxData()
    End Sub

    Protected Overrides Sub CmsPageDealPostBack()
    End Sub

    Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
        Try
            '-----------------------------------------------------------
            '提取所有项目权限设置信息
            Dim hashResProjQxVals As New Hashtable
            'Dim alistChecks As ArrayList = CType(ViewState("PAGE_CHECKS"), ArrayList)
            Dim hashChecks As Hashtable = CType(ViewState("PAGE_CHECKS"), Hashtable)
            Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(CmsPass.Employee.Language, CmsMenuType.Extension)
            If datSvc Is Nothing Then
                PromptMsg("没有项目权限定义，不能保存！")
                Return
            End If
            Dim en As IDictionaryEnumerator = hashChecks.GetEnumerator()
            While en.MoveNext
                Dim lngResID As Long = CLng(en.Value)
                Dim strCtrl As String = CStr(en.Key)
                Dim strVal As String = RStr(strCtrl)
                Dim alistItems As ArrayList = StringDeal.Split(strCtrl, "___")
                'Dim lngResID As Long = CLng(CStr(alistItems(0)).Substring(PROJQX_SECPRIFIX.Length))
                If strVal.ToLower() = "on" Then
                    Dim lngQxVal As Long = datSvc.GetKeyAttrByLong(CStr(alistItems(1)), CStr(alistItems(2)), "MNURIGHTS")
                    If HashField.ContainsKey(hashResProjQxVals, CStr(lngResID)) Then
                        lngQxVal = lngQxVal Or CLng(hashResProjQxVals(CStr(lngResID)))
                    End If
                    HashField.SetLng(hashResProjQxVals, CStr(lngResID), lngQxVal)
                Else
                    If HashField.ContainsKey(hashResProjQxVals, CStr(lngResID)) = False Then
                        HashField.SetLng(hashResProjQxVals, CStr(lngResID), 0)
                    End If
                End If
            End While
            '-----------------------------------------------------------

            '-----------------------------------------------------------
            '保存所有项目权限设置信息
            Dim lngQxResID As Long
            en = hashResProjQxVals.GetEnumerator()
            While en.MoveNext
                CmsRights.AddProjRights(CmsPass, CLng(en.Key), VStr("PAGE_GAINERID"), CType(VLng("PAGE_GAINERTYPE"), RightsGainerType), CLng(en.Value))
            End While
            '-----------------------------------------------------------

            '-----------------------------------------------------------
            '重新Load所有功能设置信息
            LoadProjQxData()
            '-----------------------------------------------------------

            'PromptMsg("保存项目权限信息成功！")
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub lnkExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkExit.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    '--------------------------------------------------------------------------
    '提取所有项目权限信息显示在界面上
    '--------------------------------------------------------------------------
    Protected Sub LoadProjQxData()
        'Dim alistChecks As New ArrayList
        Dim hashChecks As New Hashtable

        Dim i As Integer = 0
        Dim datSvc As DataServiceSection = CmsMenu.MenuConfig(CmsPass.Employee.Language, CmsMenuType.Extension)
        If datSvc Is Nothing Then
            lnkSave.Enabled = False '没有项目权限
            Return
        End If

        'Dim alistSections As ArrayList = Nothing
        Dim hashSections As Hashtable = Nothing
        Dim alistResID As New ArrayList
        If VLng("PAGE_RESID") = 0 Then '显示所有资源的项目权限定义
            'alistSections = datSvc.GetSections()
            hashSections = New Hashtable
        Else '显示指定一个资源的项目权限定义
            'alistSections = New ArrayList
            hashSections = New Hashtable
            Dim lngResID As Long = VLng("PAGE_RESID")
            Dim lngIndpResID As Long = CmsPass.GetDataRes(lngResID).IndepParentResID
            hashSections.Add(CStr(lngResID), PROJQX_SECPRIFIX & lngIndpResID)
        End If
        'Dim strMenuSection As String
        Dim en As IDictionaryEnumerator = hashSections.GetEnumerator()
        While en.MoveNext
            'For Each strMenuSection In alistSections
            Dim strMenuSection As String = CStr(en.Value)
            Dim lngResID As Long = CLng(en.Key)
            Dim strShareMenuSection As String = datSvc.GetSecAttr(strMenuSection, "SHAREMENU")
            If strShareMenuSection = "" Then strShareMenuSection = strMenuSection

            '获取资源ID
            'Dim strResID As String = strMenuSection.Substring(PROJQX_SECPRIFIX.Length)
            'If IsNumeric(strResID) = True Then
            If lngResID <> 0 Then
                'Dim lngResID As Long = CLng(strResID)

                Dim strEnable As String = datSvc.GetSecAttr(strMenuSection, "SHOWENABLE")
                If strEnable <> "0" Then '只显示需要显示的
                    Dim alistFuncs As ArrayList = datSvc.GetKeys(strShareMenuSection)

                    '显示Section说明
                    Dim lblSection As New System.Web.UI.WebControls.Label
                    lblSection.ID = "lblSection" & strMenuSection
                    lblSection.ForeColor = Color.FromName("red")
                    lblSection.Text = datSvc.GetSecAttr(strMenuSection, "DESC")
                    lblSection.EnableViewState = True
                    Dim strStyle As String = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 10 & "px"
                    lblSection.Attributes.Add("style", strStyle)
                    panelForm.Controls.Add(lblSection)
                    i += 1

                    Dim strKey As String
                    For Each strKey In alistFuncs
                        If strKey = "MENU_SEP" Then
                            'i += 1
                        Else
                            Dim strDesc As String = datSvc.GetKeyAttr(strShareMenuSection, strKey, "MNUNAME")
                            Dim lngDefQxValue As Long = datSvc.GetKeyAttrByLong(strShareMenuSection, strKey, "MNURIGHTS")
                            strEnable = datSvc.GetKeyAttr(strShareMenuSection, strKey, "SHOWENABLE")
                            If strEnable <> "0" Then '只显示已支持的功能
                                '生成CheckBox
                                Dim intEnable As Integer
                                Dim lngUserProjQxVal As Long = CmsRights.GetRightsDataForUserSelfOnly(CmsPass, lngResID, VStr("PAGE_GAINERID")).lngQX_PROJVAL
                                If (lngUserProjQxVal And lngDefQxValue) > 0 Then
                                    intEnable = 1
                                Else
                                    intEnable = 0
                                End If
                                Dim chk As New System.Web.UI.WebControls.CheckBox
                                '格式：本身菜单Section ___ Share菜单Section ___ Share菜单Key
                                chk.ID = strMenuSection & "___" & strShareMenuSection & "___" & strKey
                                chk.Text = strDesc
                                chk.EnableViewState = True
                                strStyle = "POSITION: absolute; top:" & (24 * i + 10) & "px;left:" & 30 & "px"
                                chk.Attributes.Add("style", strStyle)
                                If intEnable = 1 Then '默认已打开
                                    chk.Checked = True
                                Else '默认关闭
                                    chk.Checked = False
                                End If
                                panelForm.Controls.Add(chk)
                                'alistChecks.Add(chk.ID)
                                hashChecks.Add(chk.ID, CStr(lngResID))

                                i += 1
                            End If
                        End If
                    Next

                    i += 1
                End If
            End If
            'Next
        End While

        panelForm.Width = Unit.Pixel(600)
        panelForm.Height = Unit.Pixel(i * 24 + 10)

        ViewState("PAGE_CHECKS") = hashChecks 'alistChecks
    End Sub
End Class

End Namespace
