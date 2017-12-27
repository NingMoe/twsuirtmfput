Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform
Imports AjaxPro


Namespace Unionsoft.Cms.Web


Partial Class FormDesign
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ImageButton2 As System.Web.UI.WebControls.ImageButton
    'Protected WithEvents lbtnColumnSet As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnColShowSet As System.Web.UI.WebControls.LinkButton

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Protected _ResourceId As Long = 0
    Protected _FormName As String = CTableForm.DEF_DESIGN_FORM
    Protected _FormType As Integer = 0
    Protected _FormId As Long = 0
    Protected _Command As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Request.QueryString("mnuresid") <> "" Then _ResourceId = Convert.ToInt64(Request.QueryString("mnuresid"))
        If Request.Form("formname") <> "" Then _FormName = Request.Form("formname")
        If Request.Form("formid") <> "" Then _FormId = CLng(Request.Form("formid"))
        If Request.QueryString("mnuformtype") <> "" Then _FormType = CInt(Request.QueryString("mnuformtype")) 
            If Not Me.IsPostBack Then
                InitializeFormValues()
            Else 
                UpdateFormDesigner(_Command)
            End If 
        If GetDesignType(_FormType) = InputMode.DesignInputForm Then
            lblPageTitle.Text = CmsPass.GetDataRes(_ResourceId).ResName & "&nbsp;&nbsp;�����봰�����ƣ�" & _FormName & "��"
        Else
            lblPageTitle.Text = CmsPass.GetDataRes(_ResourceId).ResName & "&nbsp;&nbsp;����ӡ�������ƣ�" & _FormName & "��"
        End If
    End Sub

    '--------------------------------------------------------------------
    '���洰�������Ϣ
    '--------------------------------------------------------------------
    Protected Sub InitializeFormValues()
        ddlFontName.Items.Clear()
        ddlFontName.Items.Add(New ListItem("����", "����"))
        ddlFontName.Items.Add(New ListItem("����", "����"))
        ddlFontName.Items.Add(New ListItem("����", "����"))
        ddlFontName.Items.Add(New ListItem("Arial", "Arial"))
        ddlFontName.Items.Add(New ListItem("Times NR", "Times New Roman"))
        ddlFontName.SelectedValue = "����"

        ddlFontSize.Items.Clear()
        ddlFontSize.Items.Add(New ListItem("8", "8"))
        ddlFontSize.Items.Add(New ListItem("9", "9"))
        ddlFontSize.Items.Add(New ListItem("10", "10"))
        ddlFontSize.Items.Add(New ListItem("11", "11"))
        ddlFontSize.Items.Add(New ListItem("12", "12"))
        ddlFontSize.Items.Add(New ListItem("13", "13"))
        ddlFontSize.Items.Add(New ListItem("14", "14"))
        ddlFontSize.Items.Add(New ListItem("15", "15"))
        ddlFontSize.Items.Add(New ListItem("16", "16"))
        ddlFontSize.Items.Add(New ListItem("17", "17"))
        ddlFontSize.Items.Add(New ListItem("18", "18"))
        ddlFontSize.Items.Add(New ListItem("19", "19"))
        ddlFontSize.Items.Add(New ListItem("20", "20"))
        ddlFontSize.Items.Add(New ListItem("22", "22"))
        ddlFontSize.Items.Add(New ListItem("24", "24"))
        ddlFontSize.Items.Add(New ListItem("26", "26"))
        ddlFontSize.Items.Add(New ListItem("28", "28"))
        ddlFontSize.Items.Add(New ListItem("30", "30"))
        ddlFontSize.Items.Add(New ListItem("32", "32"))
        ddlFontSize.Items.Add(New ListItem("36", "36"))
        ddlFontSize.Items.Add(New ListItem("40", "40"))
        ddlFontSize.Items.Add(New ListItem("44", "44"))
        ddlFontSize.Items.Add(New ListItem("48", "48"))
        ddlFontSize.Items.Add(New ListItem("52", "52"))
        ddlFontSize.Items.Add(New ListItem("56", "56"))
        ddlFontSize.Items.Add(New ListItem("60", "60"))
        ddlFontSize.Items.Add(New ListItem("70", "70"))
        ddlFontSize.Items.Add(New ListItem("80", "80"))
        ddlFontSize.Items.Add(New ListItem("90", "90"))
        ddlFontSize.Items.Add(New ListItem("100", "100"))
        ddlFontSize.SelectedValue = "12"

        ddlFontColor.Items.Clear()
        ddlFontColor.Items.Add(New ListItem("", ""))
        ddlFontColor.Items.Add(New ListItem("��ɫ", "black"))
        ddlFontColor.Items.Add(New ListItem("��ɫ", "red"))
        ddlFontColor.Items.Add(New ListItem("��ɫ", "green"))
        ddlFontColor.Items.Add(New ListItem("��ɫ", "blue"))
        ddlFontColor.Items.Add(New ListItem("��ɫ", "gray"))
        ddlFontColor.SelectedValue = "black"

        ddlFontBold.Items.Clear()
        ddlFontBold.Items.Add(New ListItem("����", "normal"))
        ddlFontBold.Items.Add(New ListItem("����", "bold"))
        ddlFontBold.SelectedValue = "normal"

        ddlFontItalic.Items.Clear()
        ddlFontItalic.Items.Add(New ListItem("����", "normal"))
        ddlFontItalic.Items.Add(New ListItem("б��", "italic"))
        ddlFontItalic.SelectedValue = "normal"

        ddlFontLine.Items.Clear()
        ddlFontLine.Items.Add(New ListItem("", ""))
        ddlFontLine.Items.Add(New ListItem("�ϻ���", "overline"))
        ddlFontLine.Items.Add(New ListItem("�л���", "line-through"))
        ddlFontLine.Items.Add(New ListItem("�»���", "underline"))
        ddlFontLine.SelectedValue = ""


        ddlBorderStyle.Items.Clear()
        'ddlBorderStyle.Items.Add(New ListItem("����ʽ", "NotSet"))
        ddlBorderStyle.Items.Add(New ListItem("�ޱ߿�", "none"))

        ddlBorderStyle.Items.Add(New ListItem("����ʽ�߿�", "dotted"))
        ddlBorderStyle.Items.Add(New ListItem("����ʽ�±߿�", "underdotted"))

        ddlBorderStyle.Items.Add(New ListItem("������ʽ�߿�", "dashed"))
        ddlBorderStyle.Items.Add(New ListItem("������ʽ�±߿�", "underdashed"))

        ddlBorderStyle.Items.Add(New ListItem("ֱ��ʽ�߿�", "solid"))
        ddlBorderStyle.Items.Add(New ListItem("ֱ��ʽ�±߿�", "undersolid"))

        ddlBorderStyle.Items.Add(New ListItem("˫��ʽ�߿�", "double"))
        ddlBorderStyle.Items.Add(New ListItem("˫��ʽ�±߿�", "underdouble"))

        ddlBorderStyle.Items.Add(New ListItem("����ʽ�߿�", "groove"))
        ddlBorderStyle.Items.Add(New ListItem("����ʽ�߿�", "ridge"))
        ddlBorderStyle.Items.Add(New ListItem("��ǶЧ���߿�", "inset"))
        ddlBorderStyle.Items.Add(New ListItem("ͻ��Ч���߿�", "outset"))
        ddlBorderStyle.SelectedValue = "none"

        LoadTableFields(_ResourceId) '��Listbox����ʾ�����ֶ�
        ShowSubRelTables() '��ʾ��ǰ��Դ�Ĺ�������Դ�б�

        '�ڴ�������ʾԭ�ȶ�����ֶβ�����Ϣ
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    Protected Sub UpdateFormDesigner(ByVal command As String)
        Select Case command
            Case "save"

            Case "saveas"

        End Select


        Dim strCmd As String = RStr("postcmd")
            If strCmd = "save" Then '���洰��Ԫ�ز�����Ϣ
                ActionSave("���洰�������Ϣ�ɹ���")
                Return

            ElseIf strCmd = "saveas" Then '���洰��Ԫ�ز�����Ϣ
                Dim strFormName As String = RStr("saveasname")
                If CTableForm.IsResDesignedForm(CmsPass, _ResourceId, strFormName, _FormType) Then
                    'ViewState("PAGE_FORMNAME") = strFormName
                    FormDesignSave(strFormName, "���洰�������Ϣ�ɹ���")

                    '�ڴ�������ʾԭ�ȶ�����ֶβ�����Ϣ

                Else
                    PromptMsg("�ô��������Ѵ���")

                End If
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
                ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
                Return
            ElseIf strCmd = "saveasprint" Then '���洰��Ԫ�ز�����Ϣ

                Dim strFormName As String = RStr("saveasname")
                FormDesignSave(strFormName, "���洰�������Ϣ�ɹ���", , True)

                '�ڴ�������ʾԭ�ȶ�����ֶβ�����Ϣ
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
                ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
                Return
            ElseIf strCmd = "exitsave" Then '���洰��Ԫ�ز�����Ϣ��Ȼ���˳�

                Dim strFormName As String = _FormName
                If strFormName = "" Then
                    PromptMsg("��½�Ѿ���ʱ�����ܱ��洰����ƣ�") 'Session��ʱ����ʾ����
                    Return
                End If
                FormDesignSave(strFormName, "���洰�������Ϣ�ɹ���")

                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                Return
            ElseIf strCmd = "exit" Then '�����洰��Ԫ�ز�����Ϣ���˳�

                'ViewState("PAGE_FORMDESIGN_RESIDS") = Nothing
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
            ElseIf strCmd = "HostResChange" Then '�����洰��Ԫ�ز�����Ϣ���˳�

                If _ResourceId <> 0 Then
                    SetColResType(_ResourceId, ResRelation.HostRes)
                    ActionSave() 'ActionSave("�л���Դʱ��ǰ���������Ϣ���Զ����棡") '���洰��Ԫ�ز�����Ϣ
                    LoadTableFields(_ResourceId)
                Else
                    ActionSave() 'δѡ���κ���������Դ����ֻ���������
                End If
                Return
            ElseIf strCmd = "SubResChange" Then '�����洰��Ԫ�ز�����Ϣ���˳�

                Dim lngCurResID As Long = GetCurrentResIDInSub()
                If lngCurResID <> 0 Then
                    SetColResType(lngCurResID, ResRelation.SubRes)
                    ActionSave() 'ActionSave("�л���Դʱ��ǰ���������Ϣ���Զ����棡") '���洰��Ԫ�ز�����Ϣ
                    LoadTableFields(lngCurResID)
                Else
                    ActionSave() 'δѡ���κ��ӹ�����Դ����ֻ���������
                End If
                Return
            ElseIf strCmd = "DeleteForm" Then 'ɾ������

                CTableForm.DelDesignedForms(CmsPass, _ResourceId, _FormName, CType(_FormType, InputMode))

                '�ڴ�������ʾԭ�ȶ�����ֶβ�����Ϣ
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
                'ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
                Me._FormId = datForm.lngFormRecID

                PromptMsg("��ǰ��ƴ���ɾ���ɹ���")
                Return
            ElseIf strCmd = "open" Then '��ָ���Ĵ������

                'FormDesignSave(_FormName, , True)  '�ȱ���ԭ���

                'ViewState("PAGE_FORMNAME") = RStr("formname")

                FormDesignOpen(_FormName)

                LoadTableFields(_ResourceId) '��Listbox����ʾ�����ֶ�
                'ShowHostRelTables() '��ʾ��ǰ��Դ����������Դ�б�
                ShowSubRelTables() '��ʾ��ǰ��Դ�Ĺ�������Դ�б�
                Return
            ElseIf strCmd = "new" Then '�½��������

                FormDesignSave(_FormName, , True)   '�ȱ���ԭ���

                ViewState("PAGE_FORMNAME") = RStr("formname")
                FormDesignNew(_FormName)

                LoadTableFields(_ResourceId) '��Listbox����ʾ�����ֶ�
                'ShowHostRelTables() '��ʾ��ǰ��Դ����������Դ�б�
                ShowSubRelTables() '��ʾ��ǰ��Դ�Ĺ�������Դ�б�
                Return

            End If
    End Sub

    Private Sub FormDesignOpen(ByVal strFormName As String)
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), strFormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    '--------------------------------------------------------------------
    '���洰�������Ϣ
    '--------------------------------------------------------------------
    Private Sub FormDesignNew(ByVal strFormName As String)
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), strFormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    '--------------------------------------------------------------------
    '���洰�������Ϣ
    '--------------------------------------------------------------------
    Private Function FormDesignSave(ByVal strFormName As String, Optional ByVal strMsg As String = "", Optional ByVal blnEditOnly As Boolean = False, Optional ByVal blnSaveToPrintForm As Boolean = False) As Long
        Try
            Dim strCtlLayout As String = RStr("dfrminfo")
            If strCtlLayout <> "" Then
                Dim lngResID As Long = _ResourceId
                Dim lngFormType As Long = CType(_FormType, InputMode)
                Dim alistFieldsLayout As ArrayList = FilterLayoutInfo(strCtlLayout) '������д���Ԫ�ز�����Ϣ
                If strFormName.StartsWith("CHANGERES__") = True Then '���Ϊ������Դ��������ͬ��ṹ����Դ���Ĵ���
                    '����ʽ��  CHANGERES__12345689012__Ĭ�ϴ���
                    Dim intPos1 As Integer = "CHANGERES__".Length
                    Dim intPos2 As Integer = strFormName.IndexOf("__", intPos1)
                    If intPos2 > 0 Then
                        lngResID = CLng(strFormName.Substring(intPos1, intPos2 - intPos1))
                        strFormName = strFormName.Substring(intPos2 + 2)
                    End If
                End If
                If blnSaveToPrintForm = True Then
                    lngFormType = FormType.PrintForm
                End If
                    Dim lngFormRecID As Long = CTableForm.SaveLayout1(CmsPass, lngResID, strFormName, lngFormType, alistFieldsLayout, blnEditOnly, blnSaveToPrintForm)
                If strMsg <> "" Then PromptMsg(strMsg)

                ViewState("PAGE_FORMRECID") = lngFormRecID
                Return lngFormRecID
            Else
                PromptMsg("û�д��������Ϣ��Ҫ���棡")
                Return 0
            End If
        Catch ex As Exception
            PromptMsg("���洰�������Ϣʧ�ܣ�", ex, True)
            Return 0
        End Try
    End Function

    '--------------------------------------------------------------------
    '��Listbox����ʾ�����ֶ�
    '--------------------------------------------------------------------
    Private Sub LoadTableFields(ByVal lngResID As Long)
        If lngResID = 0 Then Return

        ListBox1.Items.Clear() '����б�

        Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(CmsPass, lngResID, False, True)
        Dim datCol As DataTableColumn
        For Each datCol In alistColumns
            Dim li As New ListItem
            li.Text = datCol.ColDispName '�ֶ���ʾ����
            '��ʽ��[n]xxx   ע�ͣ�n���ֶε�ֵ���ͣ�xxx���ֶ��ڲ����ƣ����ʽ��Javascript�б�����
            If CType(_FormType, InputMode) = FormType.InputForm Then '�����봰�����
                If datCol.ColType = FieldDataType.LongBinary Then
                    li.Value = "[" & 101 & "]" & datCol.ColName
                Else
                    If datCol.ColType = FieldDataType.HTML Then '@guoja
                        li.Value = "[" & 201 & "]" & datCol.ColName
                    Else
                        li.Value = "[" & datCol.ColValType & "]" & datCol.ColName '�ֶ��ڲ�����
                    End If
                End If
            ElseIf CType(_FormType, InputMode) = FormType.PrintForm Then '�ǲ��Ĵ������
                If datCol.ColType = FieldDataType.LongBinary Then
                    li.Value = "[" & 102 & "]" & datCol.ColName
                Else
                    If datCol.ColType = FieldDataType.HTML Then
                        li.Value = "[" & 202 & "]" & datCol.ColName
                    Else
                        li.Value = "[" & 100 & "]" & datCol.ColName
                    End If

                End If
            End If
            ListBox1.Items.Add(li)
            li = Nothing
        Next
    End Sub

    '--------------------------------------------------------------------
    '��ȡ���д���Ԫ�صĲ�����Ϣ
    '������
    '   strCtlLayout����ʽ��";;DOC2_COMMENTS:51px,139px,90px,20px,;;lblDOC2_COMMENTS:[��ע��]:51px,139px,90px,20px,;;"
    '--------------------------------------------------------------------
    Private Function FilterLayoutInfo(ByVal strCtlLayout As String) As ArrayList
        Dim alistFieldsLayout As New ArrayList '������д���Ԫ�ز�����Ϣ
        Try
            Dim alistCtrls As ArrayList = StringDeal.Split(strCtlLayout, ";;")
            Dim strSection As String '�����ֶεĲ�����Ϣ��
            For Each strSection In alistCtrls
                Dim datCol As DataTableColumn = GetOneFieldLayout(strSection)
                If Not (datCol Is Nothing) Then alistFieldsLayout.Add(datCol)
            Next
            'Dim lngPos1 As Integer = 0
            'Dim lngPos2 As Integer = 0
            'Dim strSection As String '�����ֶεĲ�����Ϣ��
            'While True
            '    lngPos1 = strCtlLayout.IndexOf(";;", lngPos1)
            '    If lngPos1 < 0 Then Exit While
            '    lngPos2 = strCtlLayout.IndexOf(";;", lngPos1 + 2)
            '    If lngPos2 < 0 Then Exit While

            '    strSection = strCtlLayout.Substring(lngPos1 + 2, lngPos2 - lngPos1 - 2)
            '    lngPos1 = lngPos2
            '    If lngPos1 >= (strCtlLayout.Length - 1) Then Exit While

            '    Dim datCol As DataTableColumn = GetOneFieldLayout(strSection)
            '    If Not (datCol Is Nothing) Then
            '        alistFieldsLayout.Add(datCol)
            '    End If
            'End While

            Return alistFieldsLayout
        Catch ex As Exception
            SLog.Err("�������岼����Ϣʱ����strCtlLayout=" & strCtlLayout, ex)
            Return alistFieldsLayout
        End Try
    End Function

    '--------------------------------------------------------------------
    '��ȡ�����ֶεĲ�����Ϣ
    '������
    '   strSection����ʽ1��"DOC2_COMMENTS||1||[��ע]||51px||139px||90px||20px||;;"
    '               ��ʽ2��"lblDOC2_COMMENTS||0||��ע��||51px||139px||90px||20px||;;"
    '--------------------------------------------------------------------
    Private Function GetOneFieldLayout(ByVal strSection As String) As DataTableColumn
        Try
            Dim datCol As New DataTableColumn '��������Ԫ�ز�����Ϣ

            Dim alistUnits As ArrayList = StringDeal.Split(strSection, "||", False) '����ȡ�ո�
            Dim i As Integer
            Dim intNum As Integer = alistUnits.Count
            For i = 0 To (intNum - 1)
                Dim strUnit As String = CStr(alistUnits(i))  '�����ֶεĲ�����Ϣ��
                If i = 0 Then '����Ԫ������
                    '�ֶ���ʾ���͡�0��NULL��δ֪Ԫ�أ�1�����屾��2��Label��3������ؼ�����TextBox��4��Image��5��File�ؼ����ļ��ϴ��ؼ�����6��DropDownList��7��Line��8��ResTable ��Դ����
                    If IsNumeric(strUnit) Then
                        datCol.FrmFieldFormType = CType(CInt(strUnit), FieldFormType)
                        If datCol.FrmFieldFormType = FieldFormType.ResTable Then
                            datCol.FrmColResID = _ResourceId
                        End If
                    Else
                        SLog.Err("�ͻ��˴������Ĳ�����Ϣ��ʽ")
                        Exit For '�����ʽ
                    End If
                ElseIf i = 1 Then '�ֶ���ʾ����
                    If IsFieldToSave(strUnit) = False Then
                        Return Nothing
                    Else
                        If datCol.FrmFieldFormType = FieldFormType.ImageForInputform OrElse datCol.FrmFieldFormType = FieldFormType.ImageForDirFile OrElse datCol.FrmFieldFormType = FieldFormType.ImageForUrlCol Then
                            Dim strPrefix As String = ""
                            If datCol.FrmFieldFormType = FieldFormType.ImageForInputform Then
                                strPrefix = "bincolimage_"
                            ElseIf datCol.FrmFieldFormType = FieldFormType.ImageForDirFile Then
                                strPrefix = "dirfileimage_"
                            ElseIf datCol.FrmFieldFormType = FieldFormType.ImageForUrlCol Then
                                strPrefix = "urlcolimage_"
                            End If

                            Dim strCtrlName As String = strUnit.Substring(strPrefix.Length)
                            datCol.ColName = TextboxName.GetColName(strCtrlName)
                            datCol.FrmColName = strUnit
                            datCol.FrmColResID = TextboxName.GetResID(strCtrlName)
                            If datCol.FrmColResID = 0 Then datCol.FrmColResID = _ResourceId
                            datCol.FrmColResType = GetColResType(datCol.FrmColResID)
                        Else
                            If GetDesignType(_FormType) = InputMode.DesignPrintForm Then '��ӡ����
                                If datCol.FrmFieldFormType = FieldFormType.Textbox OrElse datCol.FrmFieldFormType = FieldFormType.DropDownList OrElse datCol.FrmFieldFormType = FieldFormType.Image OrElse datCol.FrmFieldFormType = FieldFormType.TextboxInPrint OrElse datCol.FrmFieldFormType = FieldFormType.Checkbox OrElse datCol.FrmFieldFormType = FieldFormType.RadioGroup OrElse datCol.FrmFieldFormType = FieldFormType.HtmlEdit Then
                                    '���뱣��Ϊʵ���ֶ����Ƶ�Ԫ��
                                    datCol.ColName = TextboxName.GetColName(strUnit)
                                Else
                                    If strUnit.StartsWith("lbl") = True Then '�����Label
                                        datCol.ColName = strUnit
                                    Else '��TextBox�ȿؼ�
                                        datCol.ColName = TextboxName.GetColName(strUnit)
                                    End If
                                End If
                            Else '���봰��
                                If datCol.FrmFieldFormType = FieldFormType.Textbox OrElse datCol.FrmFieldFormType = FieldFormType.DropDownList OrElse datCol.FrmFieldFormType = FieldFormType.Image OrElse datCol.FrmFieldFormType = FieldFormType.TextboxInPrint OrElse datCol.FrmFieldFormType = FieldFormType.Checkbox OrElse datCol.FrmFieldFormType = FieldFormType.RadioGroup OrElse datCol.FrmFieldFormType = FieldFormType.FileForDirFile OrElse datCol.FrmFieldFormType = FieldFormType.HtmlEdit Then
                                    datCol.ColName = TextboxName.GetColName(strUnit)
                                Else
                                    datCol.ColName = strUnit
                                End If
                            End If
                            datCol.FrmColName = datCol.ColName
                            datCol.FrmColResID = TextboxName.GetResID(strUnit)
                            If datCol.FrmColResID = 0 Then datCol.FrmColResID = _ResourceId
                            datCol.FrmColResType = GetColResType(datCol.FrmColResID)
                        End If
                    End If
                ElseIf i = 2 Then '1���˿ؼ��ڵ�ǰ������Ϊֻ����0����ֻ����
                    If IsNumeric(strUnit) Then
                        If CLng(strUnit) <> 0 Then
                            datCol.FrmReadonly = FieldFunction.Enable
                        Else
                            datCol.FrmReadonly = FieldFunction.Disable
                        End If
                    Else
                        datCol.FrmReadonly = FieldFunction.Disable
                    End If
                ElseIf i = 3 Then '����Ԫ�ص����ݣ������ڱ�ǩLabel
                    If datCol.FrmFieldFormType = FieldFormType.ImageForPageUrl Then
                        Dim pos As Integer = strUnit.IndexOf("/cmsweb")
                        If pos > 0 Then
                            datCol.FrmText = strUnit.Substring(pos)
                        Else
                            datCol.FrmText = strUnit
                        End If
                    Else
                        datCol.FrmText = strUnit
                    End If
                ElseIf i = 4 Then 'X����
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmLeft = CLng(strUnit)
                ElseIf i = 5 Then 'Y����
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmTop = CLng(strUnit)
                ElseIf i = 6 Then '���
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmWidth = CLng(strUnit)
                ElseIf i = 7 Then '�߶�
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmHeight = CLng(strUnit)
                ElseIf i = 8 Then '������Ϣ
                    strUnit = strUnit.ToLower()
                    If strUnit = "right" Then
                        datCol.FrmAlign = 1
                    ElseIf strUnit = "center" Then
                        datCol.FrmAlign = 2
                    Else 'If strUnit = "left" Then
                        datCol.FrmAlign = 0
                    End If
                ElseIf i = 9 Then 'Button, LinkButton�ȵ�Javascript����
                    If strUnit.Trim() <> "" Then

                        datCol.FrmCtrlProperty = strUnit.Trim()
                    End If
                ElseIf i = 10 Then '��������
                    If strUnit.Trim() <> "" Then

                        datCol.FrmFontName = strUnit.Trim()
                    End If
                ElseIf i = 11 Then '�����С
                    If strUnit.Trim() <> "" Then
                        Dim strTemp As String = strUnit.Trim().ToLower
                        If strTemp.EndsWith("px") Then strTemp = strTemp.Substring(0, strTemp.Length - 2).Trim()
                        If IsNumeric(strTemp) Then
                            datCol.FrmFontSize = CLng(strTemp)
                        Else
                            datCol.FrmFontSize = 12
                        End If
                    End If

                ElseIf i = 12 Then '������ɫ
                    If strUnit.Trim() <> "" Then
                        datCol.FrmForeColor = strUnit.Trim()
                    End If

                ElseIf i = 13 Then '�������
                    If strUnit.Trim() <> "" Then
                        If strUnit.Trim().ToLower = "bold" Then
                            datCol.FrmFontBold = 1
                        Else
                            datCol.FrmFontBold = 0
                        End If
                    End If

                ElseIf i = 14 Then '����б��
                    If strUnit.Trim() <> "" Then
                        If strUnit.Trim().ToLower = "italic" Then
                            datCol.FrmFontItalic = 1
                        Else
                            datCol.FrmFontItalic = 0
                        End If
                    End If

                ElseIf i = 15 Then '�ϡ��С��»�������
                    If strUnit.Trim() <> "" Then
                        datCol.FrmFontLine = strUnit.Trim()
                    End If

                ElseIf i = 16 Then '������
                    If IsNumeric(strUnit) Then
                        If CLng(strUnit) <> 0 Then
                            datCol.FrmIsNoNull = FieldFunction.Enable
                        Else
                            datCol.FrmIsNoNull = FieldFunction.Disable
                        End If
                    Else
                        datCol.FrmIsNoNull = FieldFunction.Disable
                    End If
                ElseIf i = 17 Then
                    If strUnit.Trim() <> "" Then
                        Dim alistStyle As ArrayList = StringDeal.Split(strUnit, ",", False)
                        If CStr(alistStyle(0)) = "" AndAlso CStr(alistStyle(1)) = "" Then
                            datCol.FrmBorderStyle = ""
                        ElseIf CStr(alistStyle(0)) <> "" AndAlso CStr(alistStyle(1)) <> "" AndAlso CStr(alistStyle(0)) = CStr(alistStyle(1)) Then
                            If CStr(alistStyle(0)) = "1px" Then
                                datCol.FrmBorderStyle = "none"
                            Else
                                datCol.FrmBorderStyle = CStr(alistStyle(0)).Substring(4)
                            End If
                        ElseIf CStr(alistStyle(0)) = "" AndAlso CStr(alistStyle(1)) <> "" Then
                            If CStr(alistStyle(0)) = "1px" Then
                                datCol.FrmBorderStyle = "none"
                            Else
                                datCol.FrmBorderStyle = "under" & CStr(alistStyle(1)).Substring(4)
                            End If
                        End If
                    Else
                        datCol.FrmBorderStyle = ""
                    End If
                ElseIf i = 18 Then
                    If strUnit.Trim() <> "" Then
                        datCol.FrmBackColor = strUnit.Trim()
                    End If
                Else
                    Exit For
                End If
            Next

            Return datCol
        Catch ex As Exception
            SLog.Err("�������岼����Ϣʱ����strSection=" & strSection, ex)
            Return New DataTableColumn
        End Try
    End Function

    Private Function IsFieldToSave(ByVal strColName As String) As Boolean
        If strColName = "left_top" OrElse strColName = "left_mid" OrElse strColName = "left_bot" OrElse strColName = "middle_top" OrElse strColName = "middle_bot" OrElse strColName = "right_top" OrElse strColName = "right_mid" OrElse strColName = "right_bot" Then
            Return False
        Else
            Return True
        End If
    End Function

    '-----------------------------------------------------------------
    '��ʾ��ǰ��Դ�Ĺ�������Դ�б�
    '-----------------------------------------------------------------
    'Private Sub ShowHostRelTables()
    '    ddlHostTables.Items.Clear()
    '    ddlHostTables.Items.Add(New ListItem("", ""))
    '    ddlHostTables.Items.Add(New ListItem(CmsPass.GetDataRes(VLng("PAGE_RESID")).ResName, CStr(VLng("PAGE_RESID"))))

    '    Dim hashHostRes As Hashtable = CmsTableRelation.GetHostRelatedResources(CmsPass, VLng("PAGE_RESID"))
    '    Dim en As IDictionaryEnumerator = hashHostRes.GetEnumerator()
    '    While en.MoveNext
    '        Dim datRes As DataResource = CType(en.Value, DataResource)
    '        ddlHostTables.Items.Add(New ListItem(datRes.ResName, CStr(datRes.ResID)))
    '    End While
    '    hashHostRes.Clear()

    '    ddlHostTables.SelectedIndex = 1
    '    ddlHostTables.Attributes.Add("onchange", "HostRelResChanged()")
    'End Sub

    '-----------------------------------------------------------------
    '��ʾ��ǰ��Դ�Ĺ�������Դ�б�
    '-----------------------------------------------------------------
    Private Sub ShowSubRelTables()
        ddlSubTables.Items.Clear()
        ddlSubTables.Items.Add(New ListItem("", ""))

        Dim alistHostRes As ArrayList = CmsTableRelation.GetSubRelatedResources(CmsPass, _ResourceId, False, True)
        Dim datRes As DataResource
        For Each datRes In alistHostRes
            ddlSubTables.Items.Add(New ListItem(datRes.ResName, CStr(datRes.ResID)))
        Next
        alistHostRes.Clear()

        'ddlSubTables.Attributes.Add("onchange", "SubRelResChanged()")
    End Sub

    '-----------------------------------------------------------------
    '���洰��Ԫ�ز�����Ϣ
    '-----------------------------------------------------------------
    Private Sub ActionSave(Optional ByVal strMsg As String = "")
        FormDesignSave(_FormName, strMsg)
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    '-----------------------------------------------------------------
    '��ȡ��������ԴID
    '-----------------------------------------------------------------
    'Private Function GetCurrentResIDInHost() As Long
    '    Dim lngResID As Long
    '    If IsNumeric(ddlHostTables.SelectedValue) Then
    '        lngResID = CLng(ddlHostTables.SelectedValue)
    '    Else
    '        lngResID = 0
    '    End If

    '    Return lngResID
    'End Function

    '-----------------------------------------------------------------
    '��ȡ��������ԴID
    '-----------------------------------------------------------------
    Private Function GetCurrentResIDInSub() As Long
        Dim lngResID As Long
        If IsNumeric(ddlSubTables.SelectedValue) Then
            lngResID = CLng(ddlSubTables.SelectedValue)
        Else
            lngResID = 0
        End If

        Return lngResID
    End Function

    '-----------------------------------------------------------------
    '���õ�ǰѡ�е���Դ�Ĺ�����ϵ����
    '-----------------------------------------------------------------
    Private Sub SetColResType(ByVal lngResID As Long, ByVal intType As Integer)
        If lngResID = _ResourceId Then intType = ResRelation.Self '����Ǳ���Դ������������Դ����

        Dim hashColResType As Hashtable = Nothing
        If ViewState("PAGE_FORMDESIGN_RESIDS") Is Nothing Then
            hashColResType = New Hashtable
            ViewState("PAGE_FORMDESIGN_RESIDS") = hashColResType
        Else
            hashColResType = CType(ViewState("PAGE_FORMDESIGN_RESIDS"), Hashtable)
        End If

        If HashField.ContainsKey(hashColResType, CStr(lngResID)) = False Then
            hashColResType.Add(CStr(lngResID), intType)
        Else
            hashColResType(CStr(lngResID)) = intType '�������¸�ֵ��Ϊ����ʷ�汾����
        End If
    End Sub

    '-----------------------------------------------------------------
    '��ȡ��ǰѡ�е���Դ�Ĺ�����ϵ����
    '-----------------------------------------------------------------
    Private Function GetColResType(ByVal lngResID As Long) As ResRelation
        If ViewState("PAGE_FORMDESIGN_RESIDS") Is Nothing Then
            Return ResRelation.Self
        Else
            Dim hashColResType As Hashtable = CType(ViewState("PAGE_FORMDESIGN_RESIDS"), Hashtable)
            If HashField.ContainsKey(hashColResType, CStr(lngResID)) Then
                Return CType(hashColResType(CStr(lngResID)), ResRelation)
            Else
                Return ResRelation.Self
            End If
        End If
    End Function

    '-----------------------------------------------------------------
    '��ȡ���ģʽ
    '-----------------------------------------------------------------
    Private Shared Function GetDesignType(ByVal lngFormType As Long) As InputMode
        If lngFormType = FormType.InputForm Then
            Return InputMode.DesignInputForm
        ElseIf lngFormType = FormType.PrintForm Then
            Return InputMode.DesignPrintForm
        Else
            Return InputMode.DesignInputForm
        End If
    End Function

End Class

End Namespace
