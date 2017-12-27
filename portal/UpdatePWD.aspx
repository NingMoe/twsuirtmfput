<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePWD.aspx.cs" Inherits="UpdatePWD" %>

<html xmlns ="http://www.w3.org/1999/xhtml" >
<head>
    <title>登陆界面</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
   <%-- <link href="css/cmsstyle.css" type="text/css" rel="stylesheet"/>--%>
</head>
<body>
<form  runat ="server"  id="Form1">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
            <asp:Repeater ID="GridView1" runat="server">
            <ItemTemplate>
              <tr>
                <td width="29%" height="20">&nbsp;<%#Eval("EMP_id")%></td>
                <td width="38%">&nbsp;<%#Eval("EMP_PASS")%></td>
              </tr>
            </ItemTemplate>
            </asp:Repeater>
        </table>
    <%=pwd %>
</form>
</body>
</html>