<%@ Page Language="VB" AutoEventWireup="false" CodeFile="mnures.aspx.vb" Inherits="mnures" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style>
.btn {height:35;
BORDER-RIGHT: #7b9ebd 1px solid; PADDING-RIGHT: 2px; BORDER-TOP:
#7b9ebd 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 12px; FILTER:
progid:DXImageTransform.Microsoft.Gradient(GradientType=0,
StartColorStr=#ffffff, EndColorStr=#cecfde); BORDER-LEFT: #7b9ebd
1px solid; CURSOR: hand; COLOR: black; PADDING-TOP: 2px;
BORDER-BOTTOM: #7b9ebd 1px solid
}
</style>
</head>

<body>
    <form id="form1" runat="server">
        <div style="height: 30px; width: 450px;">
            <asp:Button ID="Button1" runat="server" Text="设置权限" BackColor="LightBlue" CssClass="btn" Height="27px" Width="85px" /></div>
        <div>
            <asp:DataList ID="DataMan" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" BackColor="#F4F4F4" BorderStyle="None">
                <ItemTemplate>
                    <div style="width: 150px; line-height: 30px; border-bottom :dashed 1px #ccc;">
                        &nbsp;&nbsp;<asp:CheckBox ID="ckbSelectName" runat="server" Checked="true" Text='<%#Eval("name")%>' />
                    </div>
                </ItemTemplate>
              
            </asp:DataList>
        </div>
    </form>
</body>
</html>
