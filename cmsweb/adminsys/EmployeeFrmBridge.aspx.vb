Option Strict On
Option Explicit On 

Imports Unionsoft.Platform
Imports NetReusables


Namespace Unionsoft.Cms.Web


Partial Class EmployeeFrmBridge
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
        '��ȡ����ID
        Dim lngDepID As Long = RLng("depid")
        Dim blnIsVirtualDep As Boolean = OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngDepID)
            If blnIsVirtualDep Then
                'Dim blnHasEmp As Boolean = OrgFactory.EmpDriver.HasEmployeeUnderDep(CmsPass, lngDepID)
                'If blnHasEmp Then
                '    '���ⲿ��������Ա�ʺţ�Ϊ����ԭϵͳ�汾����
                '    Response.Redirect("/cmsweb/adminsys/EmployeeFrmContent.aspx?depid=" & lngDepID, False)
                'Else
                '    '���ⲿ����û����Ա�ʺţ�ֱ�ӽ�����ϵͳ�����ⲿ����Ա�������
                '    Response.Redirect("/cmsweb/adminsys/EmployeeFrmVirtualDep.aspx?depid=" & lngDepID, False)
                'End If
                Response.Redirect("/cmsweb/adminsys/EmployeeFrmVirtualDep.aspx?depid=" & lngDepID, False)
            Else
                Response.Redirect("/cmsweb/adminsys/EmployeeFrmContent.aspx?depid=" & lngDepID, False)
            End If
    End Sub
End Class

End Namespace
