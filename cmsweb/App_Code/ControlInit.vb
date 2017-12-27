Imports Microsoft.VisualBasic

'Public Enum HorizontalAlign
'    Left = 0
'    Center = 1
'    Right = 2
'End Enum

Public Class ControlInit
    Public Shared Sub CreateOneColumn1(ByRef DataGrid1 As DataGrid, ByRef strColumnHeader() As String, ByVal strColumnValue() As String, ByVal intColumnWidth() As Integer, Optional ByVal oSortExpression() As String = Nothing, Optional ByVal oDataFormatString() As String = Nothing)
        Dim DataGridWidth As Integer = 0
        Dim IsBe As Boolean = True
        If strColumnHeader.Length <> strColumnValue.Length Or strColumnHeader.Length <> intColumnWidth.Length Then
            IsBe = False
            Throw New Exception("参数有误，请确认！")
            'ElseIf oHorizontalAlign IsNot Nothing Then
            '    If strColumnHeader.Length <> oHorizontalAlign.Length Then
            '        IsBe = False
            '        Throw New Exception("参数有误，请确认！")
            '    End If
        ElseIf oDataFormatString IsNot Nothing Then
            If strColumnHeader.Length <> oDataFormatString.Length Then
                IsBe = False
                Throw New Exception("参数有误，请确认！")
            End If
        ElseIf oSortExpression IsNot Nothing Then
            If strColumnHeader.Length <> oSortExpression.Length Then
                IsBe = False
                Throw New Exception("参数有误，请确认！")
            End If
        End If

        If IsBe Then
            For i As Integer = 0 To strColumnHeader.Length - 1
                Dim col As New BoundColumn
                col.HeaderText = strColumnHeader(i).Trim()
                col.DataField = strColumnValue(i).Trim()
                col.HeaderStyle.Width = intColumnWidth(i)
                col.ItemStyle.Width = intColumnWidth(i)
                col.SortExpression = strColumnValue(i).Trim()

                DataGridWidth += intColumnWidth(i)

                'If oHorizontalAlign IsNot Nothing Then
                '    If oHorizontalAlign(i) = HorizontalAlign.Left Then
                '        col.ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Left
                '    ElseIf oHorizontalAlign(i) = HorizontalAlign.Center Then
                '        col.ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Center
                '    ElseIf oHorizontalAlign(i) = HorizontalAlign.Right Then
                '        col.ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Right
                '    End If
                'Else
                '    col.ItemStyle.HorizontalAlign = WebControls.HorizontalAlign.Left
                'End If

                'If oSortExpression IsNot Nothing Then
                '    If oSortExpression(i).Trim <> "" Then col.SortExpression = oSortExpression(i).Trim
                'End If

                If oDataFormatString IsNot Nothing Then
                    If oDataFormatString(i).Trim() <> "" Then col.DataFormatString = oDataFormatString(i).Trim
                End If
                DataGrid1.Columns.Add(col)
            Next
        End If
        DataGrid1.Width = DataGridWidth
    End Sub
End Class
