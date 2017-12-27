<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadFile.aspx.vb" Inherits="cmsdocument_UploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border=0 cellpadding=0 cellspacing=0 width=400px>
    <tr>
    <td>
    <asp:FileUpload  runat="server" ID="FileUpload1" Width="411px"/>
    </td>
    </tr>
    <tr>
    <td align="center">
     <asp:Button ID="BtnUpload" runat="server" Text="上传" Width="100px" /></td>
    </tr>
    </table>
    <br />
        <br />
       &nbsp;</div>
    </form>
</body>
</html>
