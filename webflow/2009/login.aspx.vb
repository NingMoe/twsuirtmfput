Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Workflow.Items
Imports Unionsoft.Workflow.Engine
Imports Unionsoft.Workflow.Platform


Namespace Unionsoft.Workflow.Web


Partial Class login1
    Inherits PageBase

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
        '�ڴ˴����ó�ʼ��ҳ���û�����
        'Response.Write(NetReusables.CmsEncrypt.Decrypt("R1gn27haQnUi0Txi9Q/DjQ=="))
        'Response.End()
    End Sub


        Protected Sub Login(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim oEmployee As Employee
            Dim pst As CmsPassport
            If Me.txtUserName.Text <> "" Then
                Try
                    pst = CmsPassport.GenerateCmsPassport(Me.txtUserName.Text, Me.txtPassWord.Text, Request.UserHostAddress)
                    Session.Item("CMS_PASSPORT") = pst

                    If Not pst Is Nothing Then
                        oEmployee = OrganizationFactory.Implementation.GetEmployee(Me.txtUserName.Text)
                        oEmployee.Password = Me.txtPassWord.Text
                        Session("User") = oEmployee
                        If oEmployee.Code <> "" Then
                            If oEmployee.Code <> "admin" Then
                                Response.Redirect(Request.Path.Substring(0, Request.Path.LastIndexOf("/")))
                            Else
                                Response.Redirect("../admin/AdminIndex.aspx")
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Me.MessageBox("�û���¼�ʺŻ�����������������룡")
                    SLog.Err("�û���¼�ʺŻ�����������������룡", ex)
                End Try
            End If
            ' OrganizationFactory.Implementation.GetEmployee("tq")
        End Sub



End Class

End Namespace
