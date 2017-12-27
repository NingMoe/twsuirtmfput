Imports Microsoft.VisualBasic

Public Class PrintToFlash


    Public Shared Function ConvertFile(ByVal FullFileName As String, ByVal OutFileName As String) As Boolean
        Try


            Dim server As Print2Flash3.Server2 = New Print2Flash3.Server2

            'MyPrint.ApplyChanges()
            server.ConvertFile(FullFileName, OutFileName, Nothing, Nothing, Nothing)

            If System.IO.File.Exists(FullFileName) Then


                Dim strFilePath As String = OutFileName.Substring(0, OutFileName.LastIndexOf("\"))
                If Not System.IO.Directory.Exists(strFilePath) Then
                    System.IO.Directory.CreateDirectory(strFilePath)

                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try






    End Function
End Class
