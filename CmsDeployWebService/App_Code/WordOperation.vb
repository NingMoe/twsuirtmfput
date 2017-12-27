Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports Microsoft.Office.Interop

Public Class WordOperation
    ''' <summary>   
    ''' WordOperation ��ժҪ˵����   
    ''' ԭ����WORDģ���в�����ǩ��Ȼ���ó�����ҵ���ǩ���ٲ�������   
    ''' Ҫ����office����   
    ''' </summary>   
    Private word As Word.ApplicationClass = Nothing
    Private doc As Word.Document = Nothing
    Private obj As Object = System.Reflection.Missing.Value

    ''' <summary>   
    ''' ��WORDģ�壬������word2003��word2007   
    ''' </summary>   
    ''' <param name="wordTemplateName">ģ��·��</param>   
    ''' <returns>bool</returns>   
    Public Function OpenTemplate(ByVal wordTemplateName As Object) As Boolean
        Try
            word = New Word.ApplicationClass()
            doc = word.Documents.Open(wordTemplateName, obj, obj, obj, obj, obj, _
             obj, obj, obj, obj, obj, obj)

            Return True
        Catch ex As Exception
            Dim msg As String = ex.ToString()
            CloseWord()
            Return False
        End Try
    End Function

    ''' <summary>   
    ''' ���ҵ���ǩȻ������ı�����   
    ''' </summary>   
    ''' <param name="index">��ǩ��</param>   
    ''' <param name="markValue">Ҫ�����ֵ</param>   
    ''' <returns>bool</returns>   
    Public Function SetValue(ByVal index As Object, ByVal markValue As String) As Boolean
        Try
            doc.Bookmarks.Item(index).Range.Text = markValue
            Return True
        Catch ex As Exception
            Dim msg As String = ex.ToString()
            '  CloseWord();   
            Return False
        End Try
    End Function

    ''' <summary>   
    ''' ���ҵ���ǩȻ�����ͼƬ����   
    ''' </summary>   
    ''' <param name="index">��ǩ��</param>   
    ''' <param name="picPath">ͼƬ·��</param>   
    ''' <returns></returns>   
    Public Function FillMapInWord(ByVal index As Object, ByVal picPath As String) As Boolean
        Try
            doc.Bookmarks.Item(index).Range.InlineShapes.AddPicture(picPath, obj, obj, obj)
            Return True
        Catch
            CloseWord()
            Return False
        End Try
    End Function

    ''' <summary>   
    ''' ����WORD�ĵ���ֻ�ܱ���Ϊword2003��ʽ��   
    ''' </summary>   
    ''' <param name="fileName">����·��</param>   
    ''' <returns>bool</returns>   
    Public Function SaveAs(ByVal fileName As Object) As Boolean
        Try
            doc.SaveAs(fileName, obj, obj, obj, obj, obj, _
             obj, obj, obj, obj, obj)
            Return True
        Catch
            CloseWord()
            Return False
        End Try
    End Function

    ''' <summary>   
    ''' �ر��ĵ�   
    ''' </summary>   
    Public Sub CloseWord()
        If doc IsNot Nothing Then
            doc.Close(obj, obj, obj)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(doc)
            doc = Nothing
        End If

        If word IsNot Nothing Then
            word.Quit(obj, obj, obj)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(word)
            word = Nothing
        End If
    End Sub
End Class
