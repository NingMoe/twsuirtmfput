<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RowRights.aspx.cs" Inherits="Base_SystemRights_RowRights" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=this.GetScript1_4_3 %>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1">
        <tr>
            <td colspan="8" valign="middle"> 
               &nbsp; <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                    <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                        onclick="return false;" /></a>
            </td>
        </tr>
    </table>
   <table border="0" cellspacing="5" cellpadding="0">
       <tr>
           <td style="height:40px;">
               <asp:TextBox ID="txtResourceName" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:TextBox ID="txtResourceID" runat="server" style="display:none;"></asp:TextBox>
                <asp:TextBox ID="txtMTS_ID" runat="server" style="display:none;"></asp:TextBox>

               
           </td>
           <td>
               <select id="drpLogic" runat="server">
                   <option value="AND">并且</option>
                   <option value="OR">或者</option>
               </select>
           </td>
           <td>
               <asp:DropDownList ID="drpColName" runat="server"></asp:DropDownList>
              <%-- <select id="drpColName" runat="server">
                    <asp:Repeater ID="repColName" runat="server">
                        <ItemTemplate>
                            <option value="<%# Eval("Name") %>"><%# Eval("Description") %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>--%>
           </td>
           <td>
               <select id="drpOperator" runat="server">
                    <option value=""></option>
                    <option value="like">包含</option>
                    <option value="=">等于</option>
                    <option value=">">大于</option>
                    <option value="<">小于</option>
                    <option value=">=">大于等于</option>
                    <option value="<=">小于等于</option>
                    <option value="!=">不等于</option>
                    <option value="not like">不包含</option>
                </select>
           </td>
           <td>
               <asp:DropDownList ID="drpColValue" runat="server"></asp:DropDownList>
              <%-- <select id="" runat="server">
                    <asp:Repeater ID="repColValue" runat="server">
                        <ItemTemplate>
                            <option value="<%# Eval("FieldName") %>"><%# Eval("FieldDescription") %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>--%>
           </td>
           <td><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
           <td> 
               <asp:Button ID="btnAdd" runat="server" Text="添加条件" OnClick="btnAdd_Click" />
               <asp:Button ID="btnDel" runat="server" Text="删除条件" OnClientClick="return DelConfirm();" OnClick="btnDel_Click"/>
           </td>
       </tr>
   </table>
       <div title="行记录权限" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
            <table border="0" class="tableChildList" cellspacing="0" cellpadding="0" id="tableFilesList" style="width:100%; overflow-x:hidden;margin-bottom:20px;">
                <tr class="tdHead">
                    <td style="width:20%;">资源名称</td>
                    <td style="width:20%;">字段名称</td>
                    <td style="width:20%;">字段条件</td>
                    <td style="width:20%;">字段条件值</td>
                    <td style="width:15%;">逻辑</td>
                    <td style="width:5%;">&nbsp;</td> 
                </tr>
                <asp:Repeater ID="repList" runat ="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("MTSCOL_COLDISPNAME") %></td>
                            <td><%#Eval("MTSCOL_COLCOND") %></td>
                            <td><%#Eval("MTSCOL_COLVALUE") %></td>
                            <td><%#Eval("MTSCOL_LOGIC") %></td>
                            <td style="text-align:center;"><label id="lblMts_ID" runat="server" style="display:none;"><%#Eval("MTSCOL_ID") %></label><asp:LinkButton ID="lbtnDel" runat="server" CommandArgument="<%#Container.ItemIndex %>" OnClientClick="return DelConfirm();" OnClick="lbtnDel_Click"  >删除</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table> 
       </div>
    </form>

    <script type="text/javascript">
        function DelConfirm() {
            if (window.confirm("确认要删除？"))
                return true;
            else return false;
        }
    </script>
</body>
</html>

 