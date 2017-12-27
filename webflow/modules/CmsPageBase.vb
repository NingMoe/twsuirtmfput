'------------------------------------------------------------------------------- 
' Description : 
' Author      : CHENYU  
'               Copyright (c) 2005-2006
' Version     : 0.1 
' Date        : 2006-4-3
'-------------------------------------------------------------------------------  

Option Explicit On 
Option Strict On



Imports NetReusables
Imports Unionsoft.Workflow.Platform

Public Class CmsPageBase
    Inherits UserPageBase


#Region "Banks add"

    '-----------------------------------------------------------------------------------------------------------------
    'Banks Added Start 05-11-29
    Protected Sub PromptMsg(ByVal strMsg As String, Optional ByVal blnLogErr As Boolean = False, Optional ByRef ex As Exception = Nothing)
        strMsg = strMsg.Trim()
        If strMsg.Length > 0 Then
            '设置：页面Load时会用Alert窗体显示指定的信息
            If Not IsStartupScriptRegistered("startup") Then
                Dim strScript As String = "<script language=""javascript"">window.onload = new function() {alert('" & strMsg & "');}</script>"
                RegisterStartupScript("startup", strScript)
            End If
        End If

        'Log信息
        If blnLogErr Then
            If ex Is Nothing Then
                SLog.Err(strMsg)
            Else
                SLog.Err(strMsg, ex)
            End If
        End If
    End Sub


    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Function RStr(ByVal strParamName As String) As String
        Return RStr(strParamName, Request)
    End Function


    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Shared Function RStr(ByVal strParamName As String, ByRef Request As System.Web.HttpRequest) As String
        Return GetParam(strParamName, Request)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Function RLng(ByVal strParamName As String) As Long
        Return RLng(strParamName, Request)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Shared Function RLng(ByVal strParamName As String, ByRef Request As System.Web.HttpRequest) As Long
        Dim strTemp As String
        Try
            strTemp = GetParam(strParamName, Request)
            If IsNumeric(strTemp) Then
                Return CLng(strTemp)
            Else
                Return 0
            End If
        Catch ex As Exception
            'CmsError.ThrowEx("Request参数[" & strParamName & "]的值[" & strTemp & "]转换为Long时异常出错", True, ex)
        End Try
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Function RInt(ByVal strParamName As String) As Integer
        Return RInt(strParamName, Request)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Shared Function RInt(ByVal strParamName As String, ByRef Request As System.Web.HttpRequest) As Integer
        Dim strTemp As String
        Try
            strTemp = GetParam(strParamName, Request)
            If IsNumeric(strTemp) Then
                Return CInt(strTemp)
            Else
                Return 0
            End If
        Catch ex As Exception
            'CmsError.ThrowEx("Request参数[" & strParamName & "]的值[" & strTemp & "]转换为Integer时异常出错", True, ex)
        End Try
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Function VStr(ByVal strParamName As String) As String
        Return VStr(strParamName, ViewState)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Shared Function VStr(ByVal strParamName As String, ByRef ViewState As System.Web.UI.StateBag) As String
        Return GetParamVState(ViewState, strParamName)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Function VLng(ByVal strParamName As String) As Long
        Return VLng(strParamName, ViewState)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Shared Function VLng(ByVal strParamName As String, ByRef ViewState As System.Web.UI.StateBag) As Long
        Dim strTemp As String
        Try
            strTemp = GetParamVState(ViewState, strParamName)
            If IsNumeric(strTemp) Then
                Return CLng(strTemp)
            Else
                Return 0
            End If
        Catch ex As Exception
            'CmsError.ThrowEx("ViewState参数[" & strParamName & "]的值[" & strTemp & "]转换为Long时异常出错", True, ex)
        End Try
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Function VInt(ByVal strParamName As String) As Integer
        Return VInt(strParamName, ViewState)
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Public Shared Function VInt(ByVal strParamName As String, ByRef ViewState As System.Web.UI.StateBag) As Integer
        Dim strTemp As String
        Try
            strTemp = GetParamVState(ViewState, strParamName)
            If IsNumeric(strTemp) Then
                Return CInt(strTemp)
            Else
                Return 0
            End If
        Catch ex As Exception
            'CmsError.ThrowEx("ViewState参数[" & strParamName & "]的值[" & strTemp & "]转换为Int时异常出错", True, ex)
        End Try
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Function SStr(ByVal strParamName As String) As String
        Return SStr(strParamName, Session)
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Shared Function SStr(ByVal strParamName As String, ByRef Session As System.Web.SessionState.HttpSessionState) As String
        Dim obj As Object = Session(strParamName)
        If obj Is Nothing Then
            Return ""
        Else
            Try
                Return CStr(obj).Trim()
            Catch ex As Exception
                Return ""
            End Try
        End If
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Function SLng(ByVal strParamName As String) As Long
        Return SLng(strParamName, Session)
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Shared Function SLng(ByVal strParamName As String, ByRef Session As System.Web.SessionState.HttpSessionState) As Long
        Dim obj As Object = Session(strParamName)
        If obj Is Nothing Then
            Return 0
        ElseIf IsNumeric(obj) Then
            Return CLng(obj)
        Else
            Return 0
        End If
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Function SInt(ByVal strParamName As String) As Integer
        Return SInt(strParamName, Session)
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Shared Function SInt(ByVal strParamName As String, ByRef Session As System.Web.SessionState.HttpSessionState) As Integer
        Dim obj As Object = Session(strParamName)
        If obj Is Nothing Then
            Return 0
        ElseIf IsNumeric(obj) Then
            Return CInt(obj)
        Else
            Return 0
        End If
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Function SDbl(ByVal strParamName As String) As Double
        Return SDbl(strParamName, Session)
    End Function

    '----------------------------------------------------------
    '获取Session中变量
    '----------------------------------------------------------
    Public Shared Function SDbl(ByVal strParamName As String, ByRef Session As System.Web.SessionState.HttpSessionState) As Double
        Dim obj As Object = Session(strParamName)
        If obj Is Nothing Then
            Return 0.0#
        ElseIf IsNumeric(obj) Then
            Return CDbl(obj)
        Else
            Return 0.0#
        End If
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Private Function GetParam(ByVal strParamName As String) As String
        Dim strParamValue As String
        Try
            strParamValue = Request.QueryString(strParamName)
            If strParamValue = "" Then
                strParamValue = Request.Params.Get(strParamName)
            End If
            If strParamValue Is Nothing Then
                GetParam = ""
            Else
                GetParam = strParamValue
            End If
        Catch ex As Exception
            SLog.Err("获取Request参数[" & strParamName & "]值时异常出错", ex)
            GetParam = ""
        End Try
    End Function

    '----------------------------------------------------------
    '获取当前请求GET或者POST中的指定参数名的值
    '----------------------------------------------------------
    Private Shared Function GetParam(ByVal strParamName As String, ByRef Request As System.Web.HttpRequest) As String
        Dim strParamValue As String
        Try
            strParamValue = Request.QueryString(strParamName)
            If strParamValue = "" Then
                strParamValue = Request.Params.Get(strParamName)
            End If
            If strParamValue Is Nothing Then
                GetParam = ""
            Else
                GetParam = strParamValue
            End If
        Catch ex As Exception
            SLog.Err("获取Request参数[" & strParamName & "]值时异常出错", ex)
            GetParam = ""
        End Try
    End Function


    '----------------------------------------------------------
    '获取当前请求ViewState中的值
    '----------------------------------------------------------
    Private Shared Function GetParamVState(ByRef ViewState As System.Web.UI.StateBag, ByVal strParamName As String) As String
        Dim objVal As Object
        Try
            objVal = ViewState(strParamName)
            If objVal Is Nothing Then
                Return ""
            Else
                Return CStr(objVal)
            End If
        Catch ex As Exception
            SLog.Err("获取ViewState参数[" & strParamName & "]值时异常出错", ex)
            Return ""
        End Try
    End Function


    '----------------------------------------------------------
    '设置光标Focus在指定控件（如Textbox）中
    '----------------------------------------------------------
    Public Sub SetFocusOnTextbox(ByVal strFirstCtrlName As String)
        '设置键盘光标默认选中的输入框
        If Not IsStartupScriptRegistered("TextboxFocus") And strFirstCtrlName <> "" Then
            '查阅状态下不必选中第一个控件
            'If lngInputMode <> InputMode.ViewInHostTable And lngInputMode <> InputMode.ViewInRelTable Then
            If strFirstCtrlName.IndexOf("-") < 0 Then '人员管理资源的ID为-1，但"-"在此处会引起Javascript错误。所以人员管理表单的第一个Textbox无法光标自动focus
                Dim strScript As String = "<script language=""javascript"">" & Environment.NewLine
                strScript = "try{" & Environment.NewLine
                strScript = "    self.document.forms(0)." & strFirstCtrlName & ".focus();" & Environment.NewLine
                strScript = "}catch(ex){" & Environment.NewLine
                strScript = "}" & Environment.NewLine
                strScript = "</script>" & Environment.NewLine
                RegisterStartupScript("TextboxFocus", strScript)
            End If
        End If
        'End If
    End Sub

    'Banks Added End 05-11-29
    '-----------------------------------------------------------------------------------------------------------------
#End Region



End Class
