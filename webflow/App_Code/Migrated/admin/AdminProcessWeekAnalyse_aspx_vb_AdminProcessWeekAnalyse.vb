'====================================================================
' This file is generated as part of Web project conversion.
' The extra class 'AdminProcessWeekAnalyse' in the code behind file in 'admin\AdminProcessWeekAnalyse.aspx.vb' is moved to this file.
'====================================================================


Imports NetReusables


Namespace Unionsoft.Workflow.Web



 

Public Class AdminProcessWeekAnalyse
    Inherits System.Web.UI.Page

#Region " Web ������������ɵĴ��� "

    '�õ����� Web ���������������ġ�
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'ע��: ����ռλ�������� Web ���������������ġ�
    '��Ҫɾ�����ƶ�����
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: �˷��������� Web ����������������
        '��Ҫʹ�ô���༭���޸�����
        InitializeComponent()
    End Sub

#End Region

    Protected dtDepartent As DataTable
    Protected dtEmployee As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim strSql As String
        'strSql = "SELECT * FROM CMS_DEPARTMENT WHERE DEP_TYPE=0 AND PID<>-1"
        'Dim dtDepartent As DataTable = SDbStatement.Query(strSql).Tables(0)

        'strSql = "SELECT * FROM CMS_EMPLOYEE WHERE EMP_TYPE IS NULL"
        'Dim dtEmployee As DataTable = SDbStatement.Query(strSql).Tables(0)

        'For i As Integer = 0 To dtDepartent.Rows.Count - 1
        '    Dim dr() As DataRow = dtEmployee.Select("HOST_ID=" & CStr(dtDepartent.Rows(i)("ID")))
        '    Response.Write(dtDepartent.Rows(i)("Name"))
        '    Response.Write("<br>")
        '    For j As Integer = 0 To dr.Length - 1
        '        Response.Write("---" & CStr(dr(j)("EMP_NAME")))
        '        Response.Write("---" & GetTaskQty(CStr(dr(j)("EMP_ID"))))
        '        Response.Write("---" & GetUnViewTaskQty(CStr(dr(j)("EMP_ID"))))
        '        Response.Write("<br>")
        '    Next
        'Next
        Dim dtStatistics As New DataTable
        Dim dr As DataRow = dtStatistics.NewRow





    End Sub

   


End Class

End Namespace
 