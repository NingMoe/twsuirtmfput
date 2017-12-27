Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports Microsoft.Office.Interop

Public Class WordOperation
    ''' <summary>   
    ''' WordOperation 的摘要说明。   
    ''' 原理：在WORD模板中插入书签，然后用程序查找到书签，再插入数据   
    ''' 要引用office程序集   
    ''' </summary>   
    Private word As Word.ApplicationClass = Nothing
    Private doc As Word.Document = Nothing
    Private obj As Object = System.Reflection.Missing.Value

    ''' <summary>   
    ''' 打开WORD模板，可以是word2003、word2007   
    ''' </summary>   
    ''' <param name="wordTemplateName">模板路径</param>   
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
    ''' 查找到书签然后插入文本数据   
    ''' </summary>   
    ''' <param name="index">书签名</param>   
    ''' <param name="markValue">要插入的值</param>   
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
    ''' 查找到书签然后插入图片数据   
    ''' </summary>   
    ''' <param name="index">书签名</param>   
    ''' <param name="picPath">图片路径</param>   
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
    ''' 保存WORD文档（只能保存为word2003格式）   
    ''' </summary>   
    ''' <param name="fileName">保存路径</param>   
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
    ''' 关闭文档   
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
