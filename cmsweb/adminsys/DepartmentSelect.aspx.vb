Option Strict On
Option Explicit On 

Imports Unionsoft.Platform


Namespace Unionsoft.Cms.Web


Partial Class DepartmentSelect
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
    End Sub

    Private Sub btnSelectDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDep.Click
        Dim lngSelDepID As Long = RLng("depid")
        If lngSelDepID = 0 Then
            PromptMsg("��ѡ����ʵĲ��ţ�")
            Return
        End If

        Try
            If OrgFactory.DepDriver.IsVirtualDep(CmsPass, lngSelDepID) = True Then
                PromptMsg("�����ƶ���Ա�����ⲿ���У�")
                Return
            End If

            'Dim employee As DataCmsEmployee = OrgFactory.EmpDriver.GetEmployee(CLng(Request.QueryString("employeeid")))
            'employee.DepartmentId = lngSelDepID
            'OrgFactory.EmpDriver.UpdateEmployee(employee)

                OrgFactory.EmpDriver.ChangeDepartment(CmsPass, Request.QueryString("employeeid"), lngSelDepID)

            Response.Redirect(CmsUrl.AppendParam(VStr("PAGE_BACKPAGE"), "cmsaction=seldep&tmpdepid=" & lngSelDepID), False)
        Catch ex As Exception
            PromptMsg("", ex, True)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub
End Class

End Namespace
