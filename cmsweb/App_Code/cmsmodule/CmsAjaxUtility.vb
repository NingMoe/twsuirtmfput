Imports System.Web
Imports System.Web.SessionState
Imports AjaxPro

Imports NetReusables
Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


'<AjaxPro.AjaxNamespace("Unionsoft.Cms.Web")> _

Public Class CmsAjaxUtility

    '-----------------------------------------------------------------------
    '��ȡ�������Ϣ
    '-----------------------------------------------------------------------
    <AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)> _
    Public Function GetCmsResource(ByVal ResourceId As Long) As Array
        Return Nothing
    End Function


    '-----------------------------------------------------------------------
    '��ȡ���������Ϣ
    '-----------------------------------------------------------------------
    <AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)> _
    Public Function GetCmsResourceColumns(ByVal ResourceId As Long) As DataTable
        Dim Session As HttpSessionState = System.Web.HttpContext.Current.Session
        Dim dt As DataTable = CTableStructure.GetColumnShowsByDataset(CType(Session("CMS_PASSPORT"), CmsPassport), ResourceId).Tables(0)
        Return dt
    End Function

End Class

End Namespace
