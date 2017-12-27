<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserListDictionary.aspx.cs" Inherits="Base_CommonDictionary_UserListDictionary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=this.GetScript1_4_3  %>

    <script type="text/javascript">
        function MultiselectRow() { 
            var ChechboxList = $('#tableUserList input[type=checkbox]:checked');
            var UserID = "";
            var UserAccount = "";
            var UserName = "";
            for (var i = 0; i < ChechboxList.length; i++) {
                var strID = ChechboxList[i].id.replace("chk_", "lbl_");
                UserID += "," + $("#" + strID + "_ID")[0].innerHTML;
                UserAccount += "," + $("#" + strID + "_员工编号")[0].innerHTML;
                UserName += "," + $("#" + strID + "_员工名称")[0].innerHTML;
            }
            var strParame = ("," + "<%=strParame %>").split(",");
            for (var i = 0; i < strParame.length; i++) {
                if (strParame[i] != "") {
                    var str = strParame[i].split("=");
                    if (str[0] == "UserAccount") $("#" + str[1], window.parent.document).val(UserAccount);
                    else if (str[0] == "UserID") $("#" + str[1], window.parent.document).val(UserID);
                    else if (str[0] == "UserName") $("#" + str[1], window.parent.document).val(UserName);
                }
            }
            window.parent.CloseDictionary();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" >
        <tr>
            <td style="width:400px;"> 
                <asp:DropDownList ID="drpSearch" runat="server">
                    <asp:ListItem>员工编号</asp:ListItem>
                    <asp:ListItem>员工名称</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
            </td>
            <td valign="middle">
                &nbsp;<a style="border: 0px;" href="#">
                <input type="image" id="fnChildSave" src="../../images/bar_choice.gif" style="padding: 3px 0px 0px 0px; border: 0px; " onclick="MultiselectRow()" /></a>
                <a style="border: 0px;" href="#" onclick="window.parent.CloseDictionary();">
                    <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;" onclick="return false;" /></a>
            </td>
        </tr>
    </table>
    <div id="divList" style="overflow-y:auto;">
     <table border="0" class="tableChildList" cellspacing="0" cellpadding="0" id="tableUserList" style="width: 100%;"> 
         <tr class="tdHead">
             <td><input type="checkbox" /></td>
             <td>员工编号</td>
             <td>员工名称</td>
         </tr>
         <asp:Repeater ID="repList" runat="server">
             <ItemTemplate>
                 <tr>
                     <td><input type="checkbox" id="chk_<%#Container.ItemIndex %>" <%# (Account.Contains(","+ Eval("员工编号").ToString().Trim())?"checked":"") %> /></td>                     
                     <td><label id="lbl_<%#Container.ItemIndex %>_员工编号"><%# Eval("员工编号") %></label><label id="lbl_<%#Container.ItemIndex %>_ID" style="display:none;"><%# Eval("ID") %></label></td>
                     <td><label id="lbl_<%#Container.ItemIndex %>_员工名称"><%# Eval("员工名称") %></label></td>
                 </tr>
             </ItemTemplate>
         </asp:Repeater>
    </table></div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript"> 
    var Height = document.documentElement.clientHeight - 35;
    $("#divList").css("height", Height);
</script>