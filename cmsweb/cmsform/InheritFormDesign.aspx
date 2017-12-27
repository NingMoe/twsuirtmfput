<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InheritFormDesign.aspx.vb" Inherits="InheritFormDesign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" style="font-size:12px; border:1px solid #000000;" cellpadding="0" cellspacing="0">
			<tr>
				<td height="22" style="border-bottom:1px solid #000000; font-weight:bold;"><asp:Label ID="lblTitle" runat="server"></asp:Label>：&nbsp;&nbsp;&nbsp;(资源名称：<asp:Label ID="lblResName" runat="server"></asp:Label>)</td>
			</tr>
			<tr>
				<td width="150" valign="top"><asp:RadioButtonList id="RadioButtonList1" runat="server" AutoPostBack="True"></asp:RadioButtonList></td> 
			</tr>
		</table>
		<asp:Button ID="btnUpdate" Runat="server" Text="保存设置"></asp:Button>
        <asp:Button ID="btnExit" runat="server" Text="退出" />
    </div>
    </form>
</body>
</html>
