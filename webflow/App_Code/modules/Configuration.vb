Imports NetReusables
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Public Class Configuration

    Public Shared CurrentConfig As DataServiceSection

    Public Shared ReadOnly PAGE_STYLE_CONFIG As String = "\conf\page_style_config.xml"

    '�������ʵĻ���.
    '��ÿ�η��ʵ�Office�ļ�,������IDд�������Hashtable��,��IDΪKey,��ʱ��ΪValue
    '��һ���˴򿪴��ļ���10������,�����˶����ܱ༭����ļ�.
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
