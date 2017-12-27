<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SignPicture.aspx.vb" Inherits="cmshost_SignPicture" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="NetReusables"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script language="vb" runat="server">
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strUserCode As String = Request.QueryString("UserCode")
        Dim strSql As String = "SELECT SIGNPICTURE FROM CMS_EMPLOYEE WHERE EMP_ID='" & strUserCode & "'"
        Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim b() As Byte = CType(DbField.GetObj(dt.Rows(0), "SIGNPICTURE"), Byte())
            If Not b Is Nothing Then
                Response.ContentType = "image/gif"
                Response.BinaryWrite(b)
            End If
        End If
    End Sub
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
