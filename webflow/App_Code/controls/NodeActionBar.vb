Option Explicit On 
Option Strict On


Imports System
Imports NetReusables
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine


Namespace Unionsoft.Workflow.Web



Public Class NodeActionBar
    Inherits Control
    Implements IPostBackEventHandler

    '环节上的所有动作,其子对象是Action
    Private _Actions As ActionCollection = New ActionCollection
    Private _ButtonCssName As String = ""
    Private _CssName As String = ""
    Private _ShowBackButton As Boolean = True
    Private _ShowButtons As Boolean = True
    Private _ShowViewHistory As Boolean = True
    Private strViewDiagramHref As String = ""
    Private _ShowBackTask As Boolean = False
    Private _ShowUntreadTask As Boolean = False
    Private blnEndFlow As Boolean = False           '结束流程的按纽
    Private _Width As Long

    '点击NodeActionBar上的控件时,引发的事件
    Public Event ItemCommand(ByVal sender As Object, ByVal e As String)

    '---------------------------------------------------------------
    '传如环节上的所有动作,用于成相应的按钮
    '---------------------------------------------------------------
    Public Property Actions() As ActionCollection
        Get
            Return _Actions
        End Get
        Set(ByVal Value As ActionCollection)
            _Actions = Value
        End Set
    End Property

    '表格的样式
    Public Property ButtonCssName() As String
        Get
            Return _ButtonCssName
        End Get
        Set(ByVal Value As String)
            _ButtonCssName = Value
        End Set
    End Property

    '表格本身的样式
    Public Property CssName() As String
        Get
            Return _CssName
        End Get
        Set(ByVal Value As String)
            _CssName = Value
        End Set
    End Property

    '是否显示按钮
    Public Property ShowActoinButtons() As Boolean
        Get
            Return _ShowButtons
        End Get
        Set(ByVal Value As Boolean)
            _ShowButtons = Value
        End Set
    End Property

    '退回
    Public Property ShowBackTask() As Boolean
        Get
            Return _ShowBackTask
        End Get
        Set(ByVal Value As Boolean)
            _ShowBackTask = Value
        End Set
    End Property

    '回收
    Public Property ShowUntreadTask() As Boolean
        Get
            Return _ShowUntreadTask
        End Get
        Set(ByVal Value As Boolean)
            _ShowUntreadTask = Value
        End Set
    End Property


    '能否查看流转历史
    Public Property ShowViewHistory() As Boolean
        Get
            Return _ShowViewHistory
        End Get
        Set(ByVal Value As Boolean)
            _ShowViewHistory = Value
        End Set
    End Property

    Public Property ViewDiagramHref() As String
        Get
            Return strViewDiagramHref
        End Get
        Set(ByVal Value As String)
            strViewDiagramHref = Value
        End Set
    End Property

    '能否查看流转历史
    Private _PrintWorkflow As Boolean = False
    Public Property PrintWorkflow() As Boolean
        Get
            Return _PrintWorkflow
        End Get
        Set(ByVal Value As Boolean)
            _PrintWorkflow = Value
        End Set
    End Property

    '能否查看流转历史
    Private _PrintWorkflowPageUrl As String = ""
    Public Property PrintWorkflowPageUrl() As String
        Get
            Return _PrintWorkflowPageUrl
        End Get
        Set(ByVal Value As String)
            _PrintWorkflowPageUrl = Value
        End Set
    End Property

    Private _SetIsExigence As Boolean
    Public Property SetIsExigence() As Boolean
        Get
            _SetIsExigence = CType(viewstate.Item("SetIsExigence"), Boolean)
            Return _SetIsExigence
        End Get
        Set(ByVal Value As Boolean)
            _SetIsExigence = Value
            viewstate.Item("SetIsExigence") = _SetIsExigence
        End Set
    End Property

    'Private _IsExigence As Boolean = False
    Public Property IsExigence() As Boolean
        Get
            Return CType(viewstate.Item("IsExigence"), Boolean)
        End Get
        Set(ByVal Value As Boolean)
            viewstate.Item("IsExigence") = Value
        End Set
    End Property

    Public Property Width() As Long
        Get
            Return _Width
        End Get
        Set(ByVal Value As Long)
            _Width = Value
        End Set
    End Property

    Private _SubmitScript As String = ""
    Public Property SubmitScript() As String
        Get
            Return _SubmitScript
        End Get
        Set(ByVal Value As String)
            _SubmitScript = Value
        End Set
    End Property

    Public Sub RaisePostBackEvent(ByVal eventArgument As String) Implements System.Web.UI.IPostBackEventHandler.RaisePostBackEvent
        RaiseEvent ItemCommand(Me, eventArgument)
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        If Me.ShowViewHistory = False And _ShowButtons = False Then Return
        Dim em As IEnumerator
        Dim Action As ActionItem
        writer.WriteLine("<table border=""0"" class=""" & _CssName & """ width=""" & _Width.ToString() & """ cellspacing=1 cellpadding=0>")
        writer.WriteLine("<tr>")
        If _ShowButtons Then
            em = _Actions.GetEnumerator
            
            While em.MoveNext
                Action = CType(em.Current, ActionItem)
                writer.WriteLine("<td width=""90"" valign=middle nowrap class=""" & _ButtonCssName & """><input type=""button"" class=WorkflowNavigateButton onClick=""javascript: " & _SubmitScript & " if (!CheckValue(self.document.forms(0))) return false; " & Page.GetPostBackEventReference(Me, Action.Key) & ";"" value=""" & Action.Name & """></td>")
            End While

            If _ShowBackTask Then
                writer.WriteLine("<td width=""90"" valign=""middle"" nowrap class=""" & _ButtonCssName & """><input type=""button"" class=WorkflowNavigateButton onClick=""javascript:" & Page.GetPostBackEventReference(Me, "BackTask") & """ value=""退回""></td>")
            End If

            If _ShowUntreadTask Then
                writer.WriteLine("<td width=""90"" valign=""middle"" nowrap class=""" & _ButtonCssName & """><input type=""button"" class=WorkflowNavigateButton onClick=""javascript:" & Page.GetPostBackEventReference(Me, "UntreadTask") & """ value=""重新办理""></td>")
            End If
            '结束流程的按纽
            If blnEndFlow Then
                writer.WriteLine("<td width=""90"" valign=""middle"" nowrap class=""" & _ButtonCssName & """><input type=""button"" class=WorkflowNavigateButton onClick=""javascript:" & Page.GetPostBackEventReference(Me, "EndFlow") & """ value=""终止流转""></td>")
            End If
        End If

        If Configuration.CurrentConfig.GetLong("ISEXIGENCESETTINGS", "ISEXIGENCESETTINGS") = 1 Then
            If Me.SetIsExigence Then
                writer.WriteLine("<td width=""90"" valign=""middle"" nowrap class=""" & _ButtonCssName & """><input type=""checkbox"" name=""chkSetIsExigence"" value=""1"">紧急流程</td>")
            End If
        End If

        writer.WriteLine("<td>&nbsp;</td>")
        If Me.PrintWorkflow Then
            writer.WriteLine("<td width=""90"" valign=""middle"" nowrap class=""" & _ButtonCssName & """><input type=""button"" class=WorkflowNavigateButton onClick=""javascript:window.open('" & _PrintWorkflowPageUrl & "');"" value=""打印流程""></td>")
        End If

        If Me.ShowViewHistory Then
            writer.WriteLine("<td width=""90"" valign=""middle"" nowrap class=""" & _ButtonCssName & """><input type=""button"" class=WorkflowNavigateButton onClick=""javascript:OpenModalWin('" & strViewDiagramHref & "');"" value=""查看流程信息""></td>")
        End If
        writer.WriteLine("</tr>")
        writer.WriteLine("</table>")
    End Sub

End Class

End Namespace
