Option Strict On
Option Explicit On 

Imports NetReusables
Imports Unionsoft.Platform
Imports AjaxPro


Namespace Unionsoft.Cms.Web


Partial Class FormDesign
    Inherits CmsPage

#Region " Web 窗体设计器生成的代码 "

    '该调用是 Web 窗体设计器所必需的。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ImageButton1 As System.Web.UI.WebControls.ImageButton
    Protected WithEvents ImageButton2 As System.Web.UI.WebControls.ImageButton
    'Protected WithEvents lbtnColumnSet As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents lbtnColShowSet As System.Web.UI.WebControls.LinkButton

    '注意: 以下占位符声明是 Web 窗体设计器所必需的。
    '不要删除或移动它。

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: 此方法调用是 Web 窗体设计器所必需的
        '不要使用代码编辑器修改它。
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
            lblPageTitle.Text = CmsPass.GetDataRes(_ResourceId).ResName & "&nbsp;&nbsp;（输入窗体名称：" & _FormName & "）"
        Else
            lblPageTitle.Text = CmsPass.GetDataRes(_ResourceId).ResName & "&nbsp;&nbsp;（打印窗体名称：" & _FormName & "）"
        End If
    End Sub

    '--------------------------------------------------------------------
    '保存窗体设计信息
    '--------------------------------------------------------------------
    Protected Sub InitializeFormValues()
        ddlFontName.Items.Clear()
        ddlFontName.Items.Add(New ListItem("宋体", "宋体"))
        ddlFontName.Items.Add(New ListItem("黑体", "黑体"))
        ddlFontName.Items.Add(New ListItem("隶书", "隶书"))
        ddlFontName.Items.Add(New ListItem("Arial", "Arial"))
        ddlFontName.Items.Add(New ListItem("Times NR", "Times New Roman"))
        ddlFontName.SelectedValue = "宋体"

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
        ddlFontColor.Items.Add(New ListItem("黑色", "black"))
        ddlFontColor.Items.Add(New ListItem("红色", "red"))
        ddlFontColor.Items.Add(New ListItem("绿色", "green"))
        ddlFontColor.Items.Add(New ListItem("蓝色", "blue"))
        ddlFontColor.Items.Add(New ListItem("灰色", "gray"))
        ddlFontColor.SelectedValue = "black"

        ddlFontBold.Items.Clear()
        ddlFontBold.Items.Add(New ListItem("正常", "normal"))
        ddlFontBold.Items.Add(New ListItem("粗体", "bold"))
        ddlFontBold.SelectedValue = "normal"

        ddlFontItalic.Items.Clear()
        ddlFontItalic.Items.Add(New ListItem("正常", "normal"))
        ddlFontItalic.Items.Add(New ListItem("斜体", "italic"))
        ddlFontItalic.SelectedValue = "normal"

        ddlFontLine.Items.Clear()
        ddlFontLine.Items.Add(New ListItem("", ""))
        ddlFontLine.Items.Add(New ListItem("上划线", "overline"))
        ddlFontLine.Items.Add(New ListItem("中划线", "line-through"))
        ddlFontLine.Items.Add(New ListItem("下划线", "underline"))
        ddlFontLine.SelectedValue = ""


        ddlBorderStyle.Items.Clear()
        'ddlBorderStyle.Items.Add(New ListItem("无样式", "NotSet"))
        ddlBorderStyle.Items.Add(New ListItem("无边框", "none"))

        ddlBorderStyle.Items.Add(New ListItem("点线式边框", "dotted"))
        ddlBorderStyle.Items.Add(New ListItem("点线式下边框", "underdotted"))

        ddlBorderStyle.Items.Add(New ListItem("破折线式边框", "dashed"))
        ddlBorderStyle.Items.Add(New ListItem("破折线式下边框", "underdashed"))

        ddlBorderStyle.Items.Add(New ListItem("直线式边框", "solid"))
        ddlBorderStyle.Items.Add(New ListItem("直线式下边框", "undersolid"))

        ddlBorderStyle.Items.Add(New ListItem("双线式边框", "double"))
        ddlBorderStyle.Items.Add(New ListItem("双线式下边框", "underdouble"))

        ddlBorderStyle.Items.Add(New ListItem("槽线式边框", "groove"))
        ddlBorderStyle.Items.Add(New ListItem("脊线式边框", "ridge"))
        ddlBorderStyle.Items.Add(New ListItem("内嵌效果边框", "inset"))
        ddlBorderStyle.Items.Add(New ListItem("突起效果边框", "outset"))
        ddlBorderStyle.SelectedValue = "none"

        LoadTableFields(_ResourceId) '在Listbox中显示所有字段
        ShowSubRelTables() '显示当前资源的关联子资源列表

        '在窗体中显示原先定义的字段布局信息
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    Protected Sub UpdateFormDesigner(ByVal command As String)
        Select Case command
            Case "save"

            Case "saveas"

        End Select


        Dim strCmd As String = RStr("postcmd")
            If strCmd = "save" Then '保存窗体元素布局信息
                ActionSave("保存窗体设计信息成功！")
                Return

            ElseIf strCmd = "saveas" Then '保存窗体元素布局信息
                Dim strFormName As String = RStr("saveasname")
                If CTableForm.IsResDesignedForm(CmsPass, _ResourceId, strFormName, _FormType) Then
                    'ViewState("PAGE_FORMNAME") = strFormName
                    FormDesignSave(strFormName, "保存窗体设计信息成功！")

                    '在窗体中显示原先定义的字段布局信息

                Else
                    PromptMsg("该窗体名称已存在")

                End If
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
                ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
                Return
            ElseIf strCmd = "saveasprint" Then '保存窗体元素布局信息

                Dim strFormName As String = RStr("saveasname")
                FormDesignSave(strFormName, "保存窗体设计信息成功！", , True)

                '在窗体中显示原先定义的字段布局信息
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
                ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
                Return
            ElseIf strCmd = "exitsave" Then '保存窗体元素布局信息，然后退出

                Dim strFormName As String = _FormName
                If strFormName = "" Then
                    PromptMsg("登陆已经超时，不能保存窗体设计！") 'Session超时，提示错误
                    Return
                End If
                FormDesignSave(strFormName, "保存窗体设计信息成功！")

                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
                Return
            ElseIf strCmd = "exit" Then '不保存窗体元素布局信息，退出

                'ViewState("PAGE_FORMDESIGN_RESIDS") = Nothing
                Response.Redirect(VStr("PAGE_BACKPAGE"), False)
            ElseIf strCmd = "HostResChange" Then '不保存窗体元素布局信息，退出

                If _ResourceId <> 0 Then
                    SetColResType(_ResourceId, ResRelation.HostRes)
                    ActionSave() 'ActionSave("切换资源时当前窗体设计信息被自动保存！") '保存窗体元素布局信息
                    LoadTableFields(_ResourceId)
                Else
                    ActionSave() '未选中任何主关联资源，则只做保存操作
                End If
                Return
            ElseIf strCmd = "SubResChange" Then '不保存窗体元素布局信息，退出

                Dim lngCurResID As Long = GetCurrentResIDInSub()
                If lngCurResID <> 0 Then
                    SetColResType(lngCurResID, ResRelation.SubRes)
                    ActionSave() 'ActionSave("切换资源时当前窗体设计信息被自动保存！") '保存窗体元素布局信息
                    LoadTableFields(lngCurResID)
                Else
                    ActionSave() '未选中任何子关联资源，则只做保存操作
                End If
                Return
            ElseIf strCmd = "DeleteForm" Then '删除窗体

                CTableForm.DelDesignedForms(CmsPass, _ResourceId, _FormName, CType(_FormType, InputMode))

                '在窗体中显示原先定义的字段布局信息
                Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
                'ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
                Me._FormId = datForm.lngFormRecID

                PromptMsg("当前设计窗体删除成功！")
                Return
            ElseIf strCmd = "open" Then '打开指定的窗体设计

                'FormDesignSave(_FormName, , True)  '先保存原设计

                'ViewState("PAGE_FORMNAME") = RStr("formname")

                FormDesignOpen(_FormName)

                LoadTableFields(_ResourceId) '在Listbox中显示所有字段
                'ShowHostRelTables() '显示当前资源的主关联资源列表
                ShowSubRelTables() '显示当前资源的关联子资源列表
                Return
            ElseIf strCmd = "new" Then '新建窗体设计

                FormDesignSave(_FormName, , True)   '先保存原设计

                ViewState("PAGE_FORMNAME") = RStr("formname")
                FormDesignNew(_FormName)

                LoadTableFields(_ResourceId) '在Listbox中显示所有字段
                'ShowHostRelTables() '显示当前资源的主关联资源列表
                ShowSubRelTables() '显示当前资源的关联子资源列表
                Return

            End If
    End Sub

    Private Sub FormDesignOpen(ByVal strFormName As String)
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), strFormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    '--------------------------------------------------------------------
    '保存窗体设计信息
    '--------------------------------------------------------------------
    Private Sub FormDesignNew(ByVal strFormName As String)
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), strFormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    '--------------------------------------------------------------------
    '保存窗体设计信息
    '--------------------------------------------------------------------
    Private Function FormDesignSave(ByVal strFormName As String, Optional ByVal strMsg As String = "", Optional ByVal blnEditOnly As Boolean = False, Optional ByVal blnSaveToPrintForm As Boolean = False) As Long
        Try
            Dim strCtlLayout As String = RStr("dfrminfo")
            If strCtlLayout <> "" Then
                Dim lngResID As Long = _ResourceId
                Dim lngFormType As Long = CType(_FormType, InputMode)
                Dim alistFieldsLayout As ArrayList = FilterLayoutInfo(strCtlLayout) '存放所有窗体元素布局信息
                If strFormName.StartsWith("CHANGERES__") = True Then '另存为其它资源（共用相同表结构的资源）的窗体
                    '另存格式：  CHANGERES__12345689012__默认窗体
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
                PromptMsg("没有窗体设计信息需要保存！")
                Return 0
            End If
        Catch ex As Exception
            PromptMsg("保存窗体设计信息失败！", ex, True)
            Return 0
        End Try
    End Function

    '--------------------------------------------------------------------
    '在Listbox中显示所有字段
    '--------------------------------------------------------------------
    Private Sub LoadTableFields(ByVal lngResID As Long)
        If lngResID = 0 Then Return

        ListBox1.Items.Clear() '清空列表

        Dim alistColumns As ArrayList = CTableStructure.GetColumnsByArraylist(CmsPass, lngResID, False, True)
        Dim datCol As DataTableColumn
        For Each datCol In alistColumns
            Dim li As New ListItem
            li.Text = datCol.ColDispName '字段显示名称
            '格式：[n]xxx   注释：n是字段的值类型，xxx是字段内部名称，这格式在Javascript中被解析
            If CType(_FormType, InputMode) = FormType.InputForm Then '是输入窗体设计
                If datCol.ColType = FieldDataType.LongBinary Then
                    li.Value = "[" & 101 & "]" & datCol.ColName
                Else
                    If datCol.ColType = FieldDataType.HTML Then '@guoja
                        li.Value = "[" & 201 & "]" & datCol.ColName
                    Else
                        li.Value = "[" & datCol.ColValType & "]" & datCol.ColName '字段内部名称
                    End If
                End If
            ElseIf CType(_FormType, InputMode) = FormType.PrintForm Then '是查阅窗体设计
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
    '获取所有窗体元素的布局信息
    '参数：
    '   strCtlLayout：格式：";;DOC2_COMMENTS:51px,139px,90px,20px,;;lblDOC2_COMMENTS:[备注：]:51px,139px,90px,20px,;;"
    '--------------------------------------------------------------------
    Private Function FilterLayoutInfo(ByVal strCtlLayout As String) As ArrayList
        Dim alistFieldsLayout As New ArrayList '存放所有窗体元素布局信息
        Try
            Dim alistCtrls As ArrayList = StringDeal.Split(strCtlLayout, ";;")
            Dim strSection As String '单个字段的布局信息块
            For Each strSection In alistCtrls
                Dim datCol As DataTableColumn = GetOneFieldLayout(strSection)
                If Not (datCol Is Nothing) Then alistFieldsLayout.Add(datCol)
            Next
            'Dim lngPos1 As Integer = 0
            'Dim lngPos2 As Integer = 0
            'Dim strSection As String '单个字段的布局信息块
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
            SLog.Err("分析窗体布局信息时出错！strCtlLayout=" & strCtlLayout, ex)
            Return alistFieldsLayout
        End Try
    End Function

    '--------------------------------------------------------------------
    '获取单个字段的布局信息
    '参数：
    '   strSection：格式1："DOC2_COMMENTS||1||[备注]||51px||139px||90px||20px||;;"
    '               格式2："lblDOC2_COMMENTS||0||备注：||51px||139px||90px||20px||;;"
    '--------------------------------------------------------------------
    Private Function GetOneFieldLayout(ByVal strSection As String) As DataTableColumn
        Try
            Dim datCol As New DataTableColumn '单个窗体元素布局信息

            Dim alistUnits As ArrayList = StringDeal.Split(strSection, "||", False) '必须取空格
            Dim i As Integer
            Dim intNum As Integer = alistUnits.Count
            For i = 0 To (intNum - 1)
                Dim strUnit As String = CStr(alistUnits(i))  '单个字段的布局信息块
                If i = 0 Then '窗体元素名称
                    '字段显示类型。0或NULL：未知元素；1：窗体本身；2：Label；3：输入控件，如TextBox；4：Image；5：File控件（文件上传控件）；6：DropDownList；7：Line；8：ResTable 资源表单；
                    If IsNumeric(strUnit) Then
                        datCol.FrmFieldFormType = CType(CInt(strUnit), FieldFormType)
                        If datCol.FrmFieldFormType = FieldFormType.ResTable Then
                            datCol.FrmColResID = _ResourceId
                        End If
                    Else
                        SLog.Err("客户端传入错误的布局信息格式")
                        Exit For '错误格式
                    End If
                ElseIf i = 1 Then '字段显示类型
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
                            If GetDesignType(_FormType) = InputMode.DesignPrintForm Then '打印窗体
                                If datCol.FrmFieldFormType = FieldFormType.Textbox OrElse datCol.FrmFieldFormType = FieldFormType.DropDownList OrElse datCol.FrmFieldFormType = FieldFormType.Image OrElse datCol.FrmFieldFormType = FieldFormType.TextboxInPrint OrElse datCol.FrmFieldFormType = FieldFormType.Checkbox OrElse datCol.FrmFieldFormType = FieldFormType.RadioGroup OrElse datCol.FrmFieldFormType = FieldFormType.HtmlEdit Then
                                    '必须保存为实际字段名称的元素
                                    datCol.ColName = TextboxName.GetColName(strUnit)
                                Else
                                    If strUnit.StartsWith("lbl") = True Then '是真的Label
                                        datCol.ColName = strUnit
                                    Else '是TextBox等控件
                                        datCol.ColName = TextboxName.GetColName(strUnit)
                                    End If
                                End If
                            Else '输入窗体
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
                ElseIf i = 2 Then '1：此控件在当前窗体中为只读；0：非只读。
                    If IsNumeric(strUnit) Then
                        If CLng(strUnit) <> 0 Then
                            datCol.FrmReadonly = FieldFunction.Enable
                        Else
                            datCol.FrmReadonly = FieldFunction.Disable
                        End If
                    Else
                        datCol.FrmReadonly = FieldFunction.Disable
                    End If
                ElseIf i = 3 Then '窗体元素的内容，仅用于标签Label
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
                ElseIf i = 4 Then 'X坐标
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmLeft = CLng(strUnit)
                ElseIf i = 5 Then 'Y坐标
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmTop = CLng(strUnit)
                ElseIf i = 6 Then '宽度
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmWidth = CLng(strUnit)
                ElseIf i = 7 Then '高度
                    strUnit = strUnit.Replace("px", "").Trim()
                    If IsNumeric(strUnit) Then datCol.FrmHeight = CLng(strUnit)
                ElseIf i = 8 Then '对齐信息
                    strUnit = strUnit.ToLower()
                    If strUnit = "right" Then
                        datCol.FrmAlign = 1
                    ElseIf strUnit = "center" Then
                        datCol.FrmAlign = 2
                    Else 'If strUnit = "left" Then
                        datCol.FrmAlign = 0
                    End If
                ElseIf i = 9 Then 'Button, LinkButton等的Javascript方法
                    If strUnit.Trim() <> "" Then

                        datCol.FrmCtrlProperty = strUnit.Trim()
                    End If
                ElseIf i = 10 Then '字体名称
                    If strUnit.Trim() <> "" Then

                        datCol.FrmFontName = strUnit.Trim()
                    End If
                ElseIf i = 11 Then '字体大小
                    If strUnit.Trim() <> "" Then
                        Dim strTemp As String = strUnit.Trim().ToLower
                        If strTemp.EndsWith("px") Then strTemp = strTemp.Substring(0, strTemp.Length - 2).Trim()
                        If IsNumeric(strTemp) Then
                            datCol.FrmFontSize = CLng(strTemp)
                        Else
                            datCol.FrmFontSize = 12
                        End If
                    End If

                ElseIf i = 12 Then '字体颜色
                    If strUnit.Trim() <> "" Then
                        datCol.FrmForeColor = strUnit.Trim()
                    End If

                ElseIf i = 13 Then '字体粗体
                    If strUnit.Trim() <> "" Then
                        If strUnit.Trim().ToLower = "bold" Then
                            datCol.FrmFontBold = 1
                        Else
                            datCol.FrmFontBold = 0
                        End If
                    End If

                ElseIf i = 14 Then '字体斜体
                    If strUnit.Trim() <> "" Then
                        If strUnit.Trim().ToLower = "italic" Then
                            datCol.FrmFontItalic = 1
                        Else
                            datCol.FrmFontItalic = 0
                        End If
                    End If

                ElseIf i = 15 Then '上、中、下划线名称
                    If strUnit.Trim() <> "" Then
                        datCol.FrmFontLine = strUnit.Trim()
                    End If

                ElseIf i = 16 Then '必填项
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
            SLog.Err("分析窗体布局信息时出错！strSection=" & strSection, ex)
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
    '显示当前资源的关联主资源列表
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
    '显示当前资源的关联子资源列表
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
    '保存窗体元素布局信息
    '-----------------------------------------------------------------
    Private Sub ActionSave(Optional ByVal strMsg As String = "")
        FormDesignSave(_FormName, strMsg)
        Dim datForm As DataInputForm = FormManager.LoadForm(CmsPass, panelForm, Nothing, _ResourceId, GetDesignType(_FormType), _FormName)
        ViewState("PAGE_FORMRECID") = datForm.lngFormRecID
    End Sub

    '-----------------------------------------------------------------
    '获取关联主资源ID
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
    '获取关联子资源ID
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
    '设置当前选中的资源的关联关系类型
    '-----------------------------------------------------------------
    Private Sub SetColResType(ByVal lngResID As Long, ByVal intType As Integer)
        If lngResID = _ResourceId Then intType = ResRelation.Self '如果是本资源，重新设置资源类型

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
            hashColResType(CStr(lngResID)) = intType '必须重新赋值，为与历史版本兼容
        End If
    End Sub

    '-----------------------------------------------------------------
    '获取当前选中的资源的关联关系类型
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
    '获取设计模式
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
