Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class CmsFrmBridge
    Inherits CmsPage

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not CmsPass.HostResData Is Nothing AndAlso CmsPass.HostResData.ResID <> RLng("noderesid") Then
            Session("CMS_HOSTTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
            Session("CMS_HOSTTABLE_MORETABLES") = "" '�������ѯ���������б�
            Session("CMS_HOSTTABLE_WHERE") = "" '��ղ�ѯ����
            Session("CMS_HOSTTABLE_ORDERBY") = "" '���뽫���������ÿ�

            Session("CMS_SUBTABLE_RECID") = "" '��յ�ǰѡ�м�¼ID
            Session("CMS_SUBTABLE_WHERE") = "" '��ղ�ѯ����
            Session("CMS_SUBTABLE_ORDERBY") = "" '���뽫���������ÿ�

            '�����Դ�ڵ����ģ��ҵ�ǰ�ڵ��Ѹı䣬ҳ��ָ�Ϊ��һҳ
            Session("CMS_HOSTTABLE_PAGE" & CmsPass.HostResData.ResID) = 0
            Session("CMS_RELTABLE_PAGE" & CmsPass.RelResData.ResID) = 0
        End If
    End Sub
End Class

End Namespace
