'--------------------------------------------------------------
'�Դ���DataGrid�е�ģ��ؼ���
'�༭ģʽ�£��ؼ������ɹ��캯���д��롣
'��ʾģʽ�£�ͨ����ģ��������ݰ��¼������������ȡ����
'--------------------------------------------------------------

Option Strict On
Option Explicit On 

Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports NetReusables


Namespace Unionsoft.Cms.Web


Public Class DataGridTemplate
    Implements ITemplate

    Private mstrFieldName As String '�������ֶ�����
    Private mstrHeaderName As String '�ֶ���ʾ����
        Private mstrFooterName As String '�ֶ���ʾ����
        Private mstrShowName As String

    Private mobjTemplateType As ListItemType
    Private mctlCell As System.Web.UI.Control 'DropDownList

    '---------------------------------------------------------------
    'һ���û�HeaderStyle
    '---------------------------------------------------------------
        Sub New(ByVal type As ListItemType, ByVal strFieldName As String, ByVal strHeaderName As String, Optional ByVal strFooterName As String = "", Optional ByVal strShowName As String = "")
            mobjTemplateType = type

            mstrFieldName = strFieldName
            mstrHeaderName = strHeaderName
            mstrFooterName = strFooterName
            mstrShowName = strShowName
        End Sub

    '---------------------------------------------------------------
    'һ�����ڱ༭ģʽ
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
                    '��ģ��������ݰ��õ��¼�������� 

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
        '��ģ��������ݰ��õ��¼��������
        '-------------------------------------------------------
        Private Sub TemplateControl_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
            '��ȡ��ģ��������á�������������������ã�Ȼ�󽫴����Ŀؼ��� NamingContainer ���Ի�ȡ��ֵ���������
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

                'ʹ�ø���������ȡ���������ģ�ģ����ģ�DataItem ���ԡ��� DataItem ������ȡ��������Ԫ�أ����磬�����У���ʹ������������Ҫ�󶨵Ŀؼ�������

            Catch ex As Exception
                SLog.Err("DataGrid�༭ģʽ�°����ݳ����ֶΣ�" & mstrFieldName, ex)
            End Try
        End Sub
End Class

End Namespace
