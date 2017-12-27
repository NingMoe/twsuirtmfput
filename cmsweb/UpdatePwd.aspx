<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdatePwd.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size:12px; font-family:Arial ;">
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" Text="批量更新" OnClientClick="return window.confirm('您确定要更新？');" /><br /><span style="color:Red;">(注：批量更新时，密码为“解码错误”用户系统将自动将密码修改为空)  </span>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="EMP_NAME" HeaderText="姓名" HeaderStyle-Width="100" />
                        <asp:BoundField DataField="EMP_ID" HeaderText="帐号" HeaderStyle-Width ="100" />
                        <asp:BoundField DataField="EMP_PASS" HeaderText="密码" HeaderStyle-Width ="100"/>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                帐号：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                新密码：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="加密" style="display:none;"/>
                <span style="display:none;">加密后密码：</span><asp:TextBox ID="TextBox3" runat="server" style="display:none;"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="修改" OnClientClick="return window.confirm('您确定要修改？');" />      
                
                <br />
                <span style="display:none;"><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><asp:Button ID="Button4" runat="server" Text="Button" /><asp:TextBox ID="TextBox5" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="Button5" runat="server" Text="Button" /><asp:TextBox ID="TextBox6" runat="server" Width="200px"></asp:TextBox>
                    </span>    
            </td>
        </tr>
        
    </table> 
    </form>   
</body>
</html>
