<%@ Page Language="VB" AutoEventWireup="false" CodeFile="userSelect.aspx.vb" Inherits="userSelect" %>

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
<body onload ="SelectCheck()">
    <form id="form1" runat="server">
        <div style="width: 480px; text-align: left;">
          &nbsp;  <input name="chkall" type="checkbox" id="chkall" value="全选/反选" onclick="selectall(this.form)" />全选/反选
        </div>
        <asp:DataList ID="DataMan" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
            CellPadding="4" ForeColor="#333333">
            <ItemTemplate>
                <div style="width: 150px; line-height: 25px;">
                    <asp:CheckBox ID="ckbUser" runat="server" Text='<%#Eval("EMP_NAME") %>' name="ckbUser" onclick="NoselectAll(this)" /></td>
                </div>
            </ItemTemplate>
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
        </asp:DataList>
        <br />
        <div style="width: 480px; text-align: center;">
            <asp:Button ID="butSub" runat="server" Text="确认" CssClass="btn" Height="24px" Width="75px" />&nbsp;&nbsp;<asp:Button
                ID="Button1" runat="server" CssClass="btn" OnClientClick="return confirm('您确认删除此条信息已授权用户么？')"
                Text="无权限" Height="24px" Width="75px" />
        </div>

        <script type ="text/javascript">
       function selectall(whosform){
            for(var i=0;i<whosform.elements.length;i++){
                var box = whosform.elements[i];
               if (box.name != 'chkall')
                  box.checked = whosform.chkall.checked;
              }        
        }
        
        function NoselectAll(obj) {        
            if (obj.checked == false) {
                document.getElementById("chkall").checked = false;
            }
        }
      function SelectCheck() {
                var count = 0;
                var m = 0;
                var inputs = document.getElementsByTagName("input"); //获取所有的input标签对象
                var arrayList =[];
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].id.indexOf("ckbUser") != -1) {
                        arrayList[m]= inputs[i];
                        m++;                    }
                }
                for (var j = 0; j < arrayList.length; j++) {                  
                        if (arrayList[j].checked == true) {
                            count++;
                        }                  
                }
                if (count == arrayList.length) {
                    document.getElementById("chkall").checked = true;

                } else {
                    document.getElementById("chkall").checked = false;
                }
            }
        </script>

    </form>
</body>
</html>
