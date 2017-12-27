'--------------------------------------------------------------
'自创建DataGrid中的模板控件，
'编辑模式下：控件类型由构造函数中传入。
'显示模式下：通过向模板添加数据绑定事件处理程序来获取数据
'--------------------------------------------------------------

Option Strict On
Option Explicit On 

Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports NetReusables


Namespace Unionsoft.Cms.Web


Public Class DataGridTemplate
    Implements ITemplate

    Private mstrFieldName As String '数据中字段名称
    Private mstrHeaderName As String '字段显示名称
        Private mstrFooterName As String '字段显示名称
        Private mstrShowName As String

    Private mobjTemplateType As ListItemType
    Private mctlCell As System.Web.UI.Control 'DropDownList

    '---------------------------------------------------------------
    '一般用户HeaderStyle
    '---------------------------------------------------------------
        Sub New(ByVal type As ListItemType, ByVal strFieldName As String, ByVal strHeaderName As String, Optional ByVal strFooterName As String = "", Optional ByVal strShowName As String = "")
            mobjTemplateType = type

            mstrFieldName = strFieldName
            mstrHeaderName = strHeaderName
            mstrFooterName = strFooterName
            mstrShowName = strShowName
        End Sub

    '---------------------------------------------------------------
    '一般用于编辑模式
    '---------------------------------------------------------------
    Sub New(ByVal type As ListItemType, ByVal ddList As System.Web.UI.Control)
        mobjTemplateType = type
        mctlCell = ddList
    End Sub

    Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
        Dim lc As New Literal
        Select Case mobjTemplateType
            Case ListItemType.Header
                lc.Text = mstrHeaderName '"<B>" & mstrHeaderName & "</B>"
                container.Controls.Add(lc)

            Case ListItemType.Item
                    '向模板添加数据绑定用的事件处理程序 

                    If mstrHeaderName.Trim = "" Then
                        lc.Text = mstrShowName
                        container.Controls.Add(lc)
                    Else
                        AddHandler lc.DataBinding, AddressOf TemplateControl_DataBinding
                        container.Controls.Add(lc)
                    End If

                Case ListItemType.EditItem
                    container.Controls.Add(mctlCell)

                Case ListItemType.Footer
                    lc.Text = mstrFooterName '"<I>Footer</I>"
                    container.Controls.Add(lc)
            End Select
    End Sub

        '-------------------------------------------------------
        '向模板添加数据绑定用的事件处理程序
        '-------------------------------------------------------
        Private Sub TemplateControl_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
            '获取对模板项的引用。创建变量来保存该引用，然后将从您的控件的 NamingContainer 属性获取的值分配给它。
            Try

                Dim lc As Literal
                lc = CType(sender, Literal)
               
                'Dim container As RepeaterItem
                'container = CType(lc.NamingContainer, RepeaterItem)
                Dim container As DataGridItem
                container = CType(lc.NamingContainer, DataGridItem)
                'If mstrFieldName.Trim = "" And mstrShowName.Trim <> "" Then
                '    ' If lc.ID.Trim = "ltlSetUrl" Then
                '    ' lc.ID = "ltlSetUrl" + container.DataSetIndex.ToString
                '    lc.Text = mstrShowName ' lc.Text + lc.ID
                '    ' End If
                'Else
                Dim obj As Object = DataBinder.Eval(container.DataItem, mstrFieldName)

                If obj Is System.Convert.DBNull Then
                    lc.Text = ""
                Else
                    lc.Text = ObjField.GetStr(obj)
                End If


                'End If

                '使用该引用来获取命名容器的（模板项的）DataItem 属性。从 DataItem 对象提取单个数据元素（例如，数据列）并使用它来设置您要绑定的控件的属性

            Catch ex As Exception
                SLog.Err("DataGrid编辑模式下绑定数据出错。字段：" & mstrFieldName, ex)
            End Try
        End Sub
End Class

End Namespace
