Option Strict On
Option Explicit On 

Imports System
Imports System.Data

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class RecordPrintSimple
    Inherits Cms.Web.PrintBase

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


    Protected dtWorkFlowTasks As DataTable = Nothing
    Protected Heigth2 As Int32


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Page.IsPostBack Then
            DealPostBack()
        End If
    End Sub

    Protected Overrides Sub CmsPageDealFirstRequest()

        Try
            '获取打印模式
            Dim intMode As InputMode
            If RInt("MNURESLOCATE") = ResourceLocation.HostTable Then
                intMode = InputMode.PrintInHostTable
            ElseIf RInt("MNURESLOCATE") = ResourceLocation.RelTable Then
                intMode = InputMode.PrintInRelTable
            Else
                    PromptMsg("不能识别的打印模式/cannot identify the print mode: " & intMode)
                Return
                End If



                ViewState("PAGE_FORMNAME") = RStr("mnuformname")
                If ViewState("PAGE_FORMNAME").ToString.Trim = "" Then ViewState("PAGE_FORMNAME") = FormManager.GetRouterFormName(CmsPass, RLng("mnuresid"), RLng("mnurecid"), FormType.PrintForm)

                If CTableForm.HasDesignedForms(CmsPass, RLng("mnuresid"), ViewState("PAGE_FORMNAME").ToString, FormType.PrintForm, intMode, False) = False Then
                    PromptMsg(CmsMessage.GetMsg(CmsPass, "RECORD_PRINT"))
                Else
                    ViewState("table") = GetRecList(intMode)
                    Dim ID As String = RLng("mnurecid").ToString()
                    If ID <> "0" Then
                        ViewState("PrintType") = "simple"
                        '打印单条记录
                        Dim al As ArrayList = CType(ViewState("table"), ArrayList)
                        While al.Count > 0
                            If CType(al(0), Long) <> RLng("mnurecid") Then
                                al.RemoveAt(0)
                            Else
                                Exit While
                            End If
                        End While
                        ViewState("table") = al
                        If al.Count <= 1 Then
                            btn_Next.Disabled = True
                            btn_PrintNext.Disabled = True
                        End If
                        SimplePrint(intMode, RLng("mnurecid"))
                    Else
                        ViewState("PrintType") = "multi"
                        Dim iCols As Integer = 1
                        Dim iRows As Integer = 0
                        iCols = GetRInt("cols", iCols)
                        iRows = GetRInt("rows", iRows)
                        BatchPrint(intMode, iCols, iRows)
                    End If
                End If

            '处理当前资源的打印辅助操作
            DealPrintingWork(CmsPass, RLng("mnuresid"), RLng("mnurecid"), RStr("MenuKey"))
        Catch ex As Exception
            SLog.Err("打印出错", ex, False)

            ' PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub BatchPrint(ByVal intMode As InputMode, ByVal iCols As Integer, ByVal iRows As Integer)
        '批量打印
        Dim i As Int32
        Dim al As ArrayList = CType(ViewState("table"), ArrayList)
        Dim Height As Integer = 0
        Dim Left As Integer = 5
        Dim iCol As Integer = 0
        Dim iRow As Integer = 0
        Dim count As Integer = iCols * iRows
        If iRows = 0 Or al.Count <= count Then
            '如果是批量打印或者记录数小于每页打印数,则下一页按钮disabled
            Me.btn_Next.Disabled = True
            Me.btn_PrintNext.Disabled = True
        End If
        While i < al.Count
            If iRows = 0 Or iRow < iRows Then
                Dim Panel As New System.Web.UI.WebControls.Panel
                Dim strStyle As String = "POSITION: absolute; top:" & Height & "px;left:" & Left & "px"
                Panel.Attributes.Add("style", strStyle)
                Me.PlaceHolder1.Controls.Add(Panel)
                    FormManager.LoadForm(CmsPass, Panel, Nothing, RLng("mnuresid"), intMode, ViewState("PAGE_FORMNAME").ToString, , Convert.ToInt64(al(i)), , , True, , , True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
                If iCol < iCols - 1 Then
                    Left = Left + CInt(Panel.Width.ToString().Remove(Panel.Width.ToString.Length - 2, 2))
                    iCol = iCol + 1
                Else
                    Height = Height + CInt(Panel.Height.ToString().Remove(Panel.Height.ToString.Length - 2, 2))
                    iRow = iRow + 1
                    Left = 5
                    iCol = 0
                End If
                i = i + 1
            Else
                Exit While
            End If
        End While
    End Sub
    Protected Sub SimplePrint(ByVal intMode As InputMode, ByVal recid As Long)
            FormManager.LoadForm(CmsPass, Panel1, Nothing, RLng("mnuresid"), intMode, ViewState("PAGE_FORMNAME").ToString, , recid, , , True, , , True, , VLng("PAGE_HOSTRESID"), VLng("PAGE_HOSTRECID"), False)
        Try
            Heigth2 = Convert.ToInt32(Panel1.Height.ToString().Remove(Panel1.Height.ToString.Length - 2, 2))
            Dim WorkFlowID As Long = Me.GetWorkFlowID(recid)
            dtWorkFlowTasks = GetWorkFlowTasks(WorkFlowID)

        Catch ex As Exception
            SLog.Err("生成流程流转历史信息发生错误.", ex)
        End Try
    End Sub
    Protected Sub DealPostBack()
        '获取打印模式
        Dim intMode As InputMode
        If RInt("MNURESLOCATE") = ResourceLocation.HostTable Then
            intMode = InputMode.PrintInHostTable
        ElseIf RInt("MNURESLOCATE") = ResourceLocation.RelTable Then
            intMode = InputMode.PrintInRelTable
        Else
                PromptMsg("不能识别的打印模式/cannot identify the print mode: " & intMode)
            Return
        End If
        If ViewState("PrintType").ToString() = "simple" Then
            '单页打印
            Dim al As ArrayList = CType(ViewState("table"), ArrayList)

            al.RemoveAt(0)
            ViewState("table") = al
            If al.Count <= 1 Then
                btn_Next.Disabled = True
                btn_PrintNext.Disabled = True
            End If
            SimplePrint(intMode, Convert.ToInt64(al(0)))
        Else
            Dim iCols As Integer = 1
            Dim iRows As Integer = 0
            iCols = GetRInt("cols", iCols)
            iRows = GetRInt("rows", iRows)

            Dim al As ArrayList = CType(ViewState("table"), ArrayList)

            Dim i As Integer = 0
            While i < iRows * iCols
                al.RemoveAt(0)
                i = i + 1
            End While
            ViewState("table") = al
            If al.Count <= iRows * iCols Then
                btn_Next.Disabled = True
                btn_PrintNext.Disabled = True
            End If
            BatchPrint(intMode, iCols, iRows)
        End If
    End Sub
    Public Shared Function GetWorkFlowTasks(ByVal WorkflowID As Long) As DataTable
        Dim rtnDt As New DataTable

        rtnDt.Columns.Add("NODEID", System.Type.GetType("System.Int64"))
        rtnDt.Columns.Add("NODENAME", System.Type.GetType("System.String"))
        rtnDt.Columns.Add("EMPCODE", System.Type.GetType("System.String"))
        rtnDt.Columns.Add("EMPNAME", System.Type.GetType("System.String"))
        rtnDt.Columns.Add("CreateTime", System.Type.GetType("System.DateTime"))
        rtnDt.Columns.Add("ViewTime", System.Type.GetType("System.DateTime"))
        rtnDt.Columns.Add("DealTime", System.Type.GetType("System.DateTime"))
        rtnDt.Columns.Add("ACTIONNAME", System.Type.GetType("System.String"))
        rtnDt.Columns.Add("Memo", System.Type.GetType("System.String"))
        'rtnDt.Columns.Add("IsPrint", System.Type.GetType("System.Int64"))
        'rtnDt.Columns.Add("IsDealed", System.Type.GetType("System.Int64"))

        Dim i As Integer, j As Integer
        Dim strSql As String = "SELECT * FROM WF_TASK WHERE WF_INSTANCE_ID=" & WorkflowID & " ORDER BY CREATETIME"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        For i = 0 To dt.Rows.Count - 1
            strSql = "SELECT * FROM WF_USERTASK WHERE [NodeID]=" & DbField.GetLng(dt.Rows(i), "NODEID") & " AND TaskID=" & DbField.GetLng(dt.Rows(i), "ID")
            Dim dt1 As DataTable = SDbStatement.Query(strSql).Tables(0)
            For j = 0 To dt1.Rows.Count - 1
                Dim tr As DataRow = rtnDt.NewRow()
                tr("NODEID") = DbField.GetLng(dt1.Rows(j), "NODEID")
                If DbField.GetInt(dt1.Rows(j), "ISCOPYFOR") = 1 Then
                    tr("NODENAME") = DbField.GetStr(dt.Rows(i), "NODENAME") & "(抄送)"
                Else
                    tr("NODENAME") = DbField.GetStr(dt.Rows(i), "NODENAME")
                End If
                tr("EMPCODE") = DbField.GetStr(dt1.Rows(j), "EMPCODE")
                tr("EMPNAME") = DbField.GetStr(dt1.Rows(j), "EMPNAME")
                tr("CreateTime") = DbField.GetDtm(dt1.Rows(j), "CreateTime")
                tr("ViewTime") = DbField.GetDtm(dt1.Rows(j), "ViewTime")
                tr("DealTime") = DbField.GetDtm(dt1.Rows(j), "DealTime")
                tr("ACTIONNAME") = DbField.GetStr(dt1.Rows(j), "ACTIONNAME")
                tr("Memo") = DbField.GetStr(dt1.Rows(j), "Memo")
                'tr("IsPrint") = DbField.GetLng(dt.Rows(i), "IsPrint")
                'tr("IsDealed") = IIf(DbField.GetLng(dt1.Rows(j), "TASKSTATUS") = TaskStatusConstants.Finished, 1, 0)
                rtnDt.Rows.Add(tr)
            Next
        Next

        Return rtnDt
    End Function
    Public Function GetWorkFlowID() As Long
        Dim Key As Long
        If Request.QueryString("mnurecid") = "" Then
            Key = CType(Request.QueryString("mnuresid"), Long)
        Else
            Key = CType(Request.QueryString("mnurecid"), Long)
        End If

        Dim strSql As String = "select [id] from WF_INSTANCE where [ID]=" + Key.ToString() + " or RecordID=" + Key.ToString()
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        Dim WorkFlowID As Long = 0
        If dt.Rows.Count <> 0 Then
            WorkFlowID = Convert.ToInt64(dt.Rows(0)(0))
        End If
        Return WorkFlowID
    End Function
    Public Function GetWorkFlowID(ByVal Key As Long) As Long

        Dim strSql As String = "select [id] from WF_INSTANCE where [ID]=" + Key.ToString() + " or RecordID=" + Key.ToString()
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        Dim WorkFlowID As Long = 0
        If dt.Rows.Count <> 0 Then
            WorkFlowID = Convert.ToInt64(dt.Rows(0)(0))
        End If
        Return WorkFlowID
    End Function

    Private Function GetRInt(ByVal RequestArgs As String, ByVal value As Integer) As Integer
        If Not Request(RequestArgs) Is Nothing Then
            Try
                value = CInt(Request(RequestArgs).ToString())
            Catch ex As Exception

            End Try
        End If
        Return value
    End Function

    Private Function GetRecList(ByVal intMode As InputMode) As ArrayList

        '直接返回选中记录
        If Not Request("selectedrecid") Is Nothing Then
            If Request("selectedrecid").ToString() <> "" Then
                Dim al As ArrayList = New ArrayList
                Dim strRec() As String = Request("selectedrecid").ToString().Split(CChar(","))
                For i As Integer = 0 To strRec.Length - 1
                    If strRec(i) <> "" Then
                        al.Add(strRec(i))
                    End If
                Next
                Return al
            End If
        End If
        '查询结果
        If intMode = InputMode.PrintInHostTable Then
            Return CType(Session("hosttable"), ArrayList)
        Else
            Return CType(Session("subtable"), ArrayList)
        End If
    End Function

End Class

End Namespace
