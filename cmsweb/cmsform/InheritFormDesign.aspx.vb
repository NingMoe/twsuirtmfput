Imports NetReusables
Imports Unionsoft.Platform
Imports AjaxPro

Partial Class InheritFormDesign
    Inherits CmsPage

    Dim lngResID As Long = 0
    Protected _FormType As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("mnuresid") IsNot Nothing Then lngResID = Convert.ToInt64(Request("mnuresid"))
        If Request.QueryString("mnuformtype") <> "" Then _FormType = CInt(Request.QueryString("mnuformtype"))

        If IsPostBack Then Return
        Dim datRes As DataResource = CmsPass.GetDataRes(lngResID)
        Dim ParentResID As Long = datRes.IndepParentResID
        lblResName.Text = datRes.ResName.Trim()
        If CType(_FormType, FormType) = FormType.InputForm Then
            lblTitle.Text = "输入窗体设置"
        Else
            lblTitle.Text = "打印窗体设置"
        End If
        Dim alistForms As ArrayList = CTableForm.GetDesignedFormNames(CmsPass, ParentResID, _FormType)

        Me.RadioButtonList1.DataSource = alistForms
        Me.RadioButtonList1.DataBind()


        Dim FormName As String = FormManager.GetRouterFormName(CmsPass, lngResID, 0) 'CHENYU 20100601 ADD

        For i As Integer = 0 To Me.RadioButtonList1.Items.Count - 1
            If Me.RadioButtonList1.Items(i).Text.Trim = FormName.Trim Then
                Me.RadioButtonList1.Items(i).Selected = True
            End If
        Next

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        ' Response.Write(VStr("PAGE_BACKPAGE"))
        Response.Redirect(VStr("PAGE_BACKPAGE"), False)
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        FormManager.UpdateRouterValue(lngResID, RadioButtonList1.SelectedValue, "", CType(_FormType, FormType))
    End Sub

End Class
