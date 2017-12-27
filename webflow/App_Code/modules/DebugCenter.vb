Imports System.IO
Imports System.Text


Namespace Unionsoft.Workflow.Web



#If DEBUG Then

Public Class DebugCenter


    Public Shared strCurrentDirectory As String = ""
    Private Shared strFilePath As String = ""

    Public Shared Sub Write(ByVal value As String)
        CreateLogFile()
        Try
            Dim sm As StreamWriter = File.AppendText(strFilePath)
            sm.Write(value & "    " & Now)
            sm.Write(vbCrLf)
            sm.Write(vbCrLf)
            sm.Flush()
            sm.Close()
            sm = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub WriteLine(ByVal value As String)
        CreateLogFile()
        Try
            Dim sm As StreamWriter = File.AppendText(strFilePath)
            sm.Write(value)
            sm.Write(vbCrLf)
            sm.Flush()
            sm.Close()
            sm = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub CreateLogFile()
        Try
            strFilePath = strCurrentDirectory & "\Log\DebugLog.txt"
            If Not File.Exists(strFilePath) Then
                File.CreateText(strFilePath)
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class

End Namespace

#End If

