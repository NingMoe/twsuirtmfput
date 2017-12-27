Imports NetReusables
Imports Unionsoft.Platform
Imports Unionsoft.Implement


Namespace Unionsoft.Cms.Web


'2010/5/28 CHENYU 

Partial Class ResourceFormRouter
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

        Protected _ResourceId As Long = 0
        Protected _FormType As Integer = CType(FormType.InputForm, Integer)

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            _ResourceId = CLng(Request.QueryString("ResourceId"))
            If Request.QueryString("formtype") IsNot Nothing Then _FormType = CInt(Request.QueryString("formtype"))



            If Not Me.IsPostBack Then
                BindData()


                Dim alistForms As ArrayList = CTableForm.GetDesignedFormNames(CmsPass, _ResourceId, CType(_FormType, FormType))
                Me.radFormName.DataSource = alistForms
                Me.radFormName.DataBind()
            End If
        End Sub

        Private Sub BindData()
            Me.dgForm.DataSource = FormManager.GetRouterForm(_ResourceId, CType(_FormType, FormType))
            Me.dgForm.DataBind()
        End Sub
 

        Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            Dim strFormName As String = ""
            Dim strFormUrl As String = ""
            If radFormCategory.SelectedValue.Trim = "0" Then
                strFormName = radFormName.SelectedValue
            Else
                strFormName = NetReusables.TimeId.CurrentMilliseconds(30).ToString
                strFormUrl = Me.txtUrl.Text.Trim
            End If
            FormManager.UpdateRouterValue(_ResourceId, strFormName, txtRouterValue.Text, strFormUrl, CType(Convert.ToInt32(radFormCategory.SelectedValue), FormCategory), CType(_FormType, FormType))
            BindData()
        End Sub

        Protected Sub lbtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                FormManager.DelRouterForm(CType(CType(sender, LinkButton).CommandArgument.Trim, Int64))
                BindData()
            Catch ex As Exception
                Response.Write("<script>alert('ɾ��ʧ��');</script>")
            End Try
        End Sub


        Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
            Response.Redirect(VStr("PAGE_BACKPAGE"), False)
        End Sub
    End Class

End Namespace
