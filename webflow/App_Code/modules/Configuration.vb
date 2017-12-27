Imports NetReusables
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Public Class Configuration

    Public Shared CurrentConfig As DataServiceSection

    Public Shared ReadOnly PAGE_STYLE_CONFIG As String = "\conf\page_style_config.xml"

    '附件访问的缓存.
    '对每次访问的Office文件,都将其ID写入在这个Hashtable中,以ID为Key,打开时间为Value
    '在一个人打开此文件的10分钟内,其他人都不能编辑这个文件.
    Public Shared DocumentAccessCache As Hashtable = New Hashtable


End Class

Public Class DocumentAccessLog
    Public UserCode As String
    Public AccessTime As DateTime

    Public Sub New(ByVal strUserCode As String)
        UserCode = strUserCode
        AccessTime = Now
    End Sub

End Class

End Namespace
