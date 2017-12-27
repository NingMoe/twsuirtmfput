<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Base_SystemRights_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%=GetScript1_4_3 %>

    <style type="text/css">
      body{ font-size:12px;margin:0px; padding:0px; background-color:#ffffff;font-family:微软雅黑,Calibri, Arial, Helvetica, sans-serif; color:#282828;} 
        .tableList{ background-color: #f5f5f5;margin:0;padding:0;border:0; border-bottom:1px solid #d6d6d6;} 
        .tableList td{  }
        .tableList td a{
	        font-size: 12px;
	        color: #0033CC;
	        text-decoration: none;
        }
        .tableList td a:hover{color: #CC0000;
	        text-decoration: none;}
    </style> 
    <script type="text/javascript">
        var centerHeight = $('#westModel_id',window.parent.document).height();
        var centerWidth = $('#centerModel_id', window.parent.document).width();

        $(document).ready(function () {
            $("#div<%=RightsResID %>").css({ height: centerHeight-100 });
        })
         
        function fnSave() {
            var jsonStr = GetRights();
            if (jsonStr != "") {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: { "Json": "" + jsonStr + "" },
                    url: "../SystemConfig/Ajax_Request.aspx?typeValue=SaveInfo_Rights&ResID=<%=ResID %>",
                    success: function (obj) {
                        if (obj.success || obj.success == "true") {
                            alert("保存成功！");
                            window.location.reload();
                        } else {
                            alert("保存失败,请刷新页面！");
                        }
                    }
                });
            }
        }

        function GetRights() {
            var jsonStr="";
            $("input[name='ChkParentRights']").each(function () {
                var strValue = $(this).val().split("_");
                var jsonStr1 = "[{'ID':'" + strValue[0] + "','权限对象编号':'<%=ObjectID %>','权限对象类型':'<%=GainerType %>','资源ID':'<%=RightsResID %>','权限值':'" + strValue[1] + "'";
                
                if (this.checked)
                    jsonStr1 += ",'是否启用':'1'}]"
                else 
                    jsonStr1 += ",'是否启用':'0'}]"
                jsonStr += "," + jsonStr1;
            })
            //if ($("#tableChild").length > 0) {
                $("input[name='ChkChildRights']").each(function () {
                    var strValue = $(this).val().split("_");
                    var jsonStr1 = "[{'ID':'" + strValue[0] + "','权限对象编号':'<%=ObjectID %>','权限对象类型':'<%=GainerType %>','资源ID':'<%=RightsResID %>'";
                    jsonStr1 += ",'子模块编号':'" + strValue[1] + "','权限值':'" + strValue[2] + "'"
                    if (this.checked)
                        jsonStr1 += ",'是否启用':'1'}]"
                    else
                        jsonStr1 += ",'是否启用':'0'}]"
                    jsonStr += "," + jsonStr1;
                })
            //}
            if (jsonStr != "") jsonStr = jsonStr.substr(1, jsonStr.length - 1);
            return jsonStr;
        }

        function selectCheckBox(obj) { 
            var chkName = obj.name;
            var chkID = obj.id; 
            var objID = chkID.substr(chkID.indexOf("chk_") + 4, chkID.length - 4);
            var objName = chkName.substr(chkName.indexOf("chk_") + 4, chkName.length - 4);
             
            var chkList = $("#" + objID + " input[name='" + objName + "']");
            var isSelected = obj.checked;
            for (var i = 0; i < chkList.length; i++) {
                $(chkList[i]).checked = isSelected;
                chkList[i].checked = isSelected;
            } 
        }

        function ClearAllCheckBox() { 
            var chkList = $("#div<%=ResID %> input[type='checkbox']");
            for (var i = 0; i < chkList.length; i++) {
                $(chkList[i]).checked = true
                chkList[i].checked = true;
            }
            fnSave();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div title="权限设置" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;"> 
         <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnSave(); return false;" /></a> 
                        &nbsp;<a style="border: 0px;" href="#" onclick="ClearAllCheckBox();">清除</a>
                    </td>
                </tr>
            </table>
       <div id="div<%=ResID %>" title="<input id='chk_tableParent' name='chk_ChkParentRights' type='checkbox' onclick='selectCheckBox(this)'/>【<%=RightsResName %>】<span style='float:right;<%=(IsRowRihgt?"":"display:none;") %>'><a href='#' onclick=window.parent.fnParentFormListDialog('Base/SystemRights/RowRights.aspx?MenuResID=<%=RightsResID %>&ObjectID=<%=ObjectID %>',0,0,'设置行过滤');>行过滤设置</a>&nbsp;</span>" class="easyui-panel" style="overflow-x: hidden;overflow-y:auto;padding: 5px;margin: 0px;">
       <table id="tableParent" style="width:100%;border-top:1px solid #ffffff;" class="tableList" cellspacing="0" cellpadding="0"><tr><td>
        <asp:DataList ID="dlRights" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" HeaderStyle-Height="30px" ItemStyle-Height="30px" ItemStyle-BorderWidth="0" >
        <ItemTemplate>
            <table border="0" style="margin-right:4px; width:150px;" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="height:30px; border-bottom:0px;"><input type="checkbox" name="ChkParentRights" id='Chk_<%#Eval("ResourceID") %>_<%#Eval("RightsValue") %>' <%# (Convert.ToInt32(Eval("IsEnabled"))==0?"":"checked") %> value="<%#Eval("ID") %>_<%#Eval("RightsValue") %>" /><%#Eval("RightsName") %></td>
                </tr> 
            </table> 
        </ItemTemplate> <ItemStyle BorderWidth="0" />
        </asp:DataList>
        <asp:DataList ID="dlRights1" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" HeaderStyle-Height="30px" ItemStyle-Height="30px" ItemStyle-BorderWidth="0" >
        <ItemTemplate>
            <table border="0" style="margin-right:4px; width:150px;" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="height:30px; border-bottom:0px;"><input type="checkbox" name="ChkParentRights" id='Chk_<%#Eval("ResourceID") %>_<%#Eval("RightsValue") %>' <%# (Convert.ToInt32(Eval("IsEnabled"))==0?"":"checked") %> value="<%#Eval("ID") %>_<%#Eval("RightsValue") %>" /><%#Eval("RightsName") %></td>
                </tr> 
            </table> 
        </ItemTemplate><ItemStyle BorderWidth="0" />  
        </asp:DataList>
        <asp:DataList ID="dlRights2" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" HeaderStyle-Height="30px" ItemStyle-Height="30px" ItemStyle-BorderWidth="0" >
        <ItemTemplate>
            <table border="0" style="margin-right:4px; width:150px;" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="height:30px; border-bottom:0px;"><input type="checkbox" name="ChkParentRights" id='Chk_<%#Eval("ResourceID") %>_<%#Eval("RightsValue") %>' <%# (Convert.ToInt32(Eval("IsEnabled"))==0?"":"checked") %> value="<%#Eval("ID") %>_<%#Eval("RightsValue") %>" /><%#Eval("RightsName") %></td>
                </tr> 
            </table> 
        </ItemTemplate><ItemStyle BorderWidth="0" />  
        </asp:DataList>       </td></tr></table>
         <asp:Repeater ID="repChildRes" runat="server" OnItemDataBound="repChildRes_ItemDataBound"  >
            <ItemTemplate> 
               <table id="tableChild_<%#Eval("子表配置号") %>" style="width:100%;border-top:1px solid #ffffff;" class="tableList" cellspacing="0" cellpadding="0">
                   <tr><td style="height:30px; border-bottom:1px solid #99BBE8; background:#ddf0fe;"><input id='chk_tableChild_<%#Eval("子表配置号") %>' name='chk_ChkChildRights' type='checkbox' onclick='selectCheckBox(this)'/>子资源【<%#Eval("显示标题") %>】</td></tr>
                   <tr>
                        <td>
                            <asp:DataList ID="dlRights_ChildRes" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                                <ItemTemplate>
                                    <table border="0" style="margin-right:4px; width:150px;" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="height:30px; border-bottom:0px;"><input type="checkbox" name="ChkChildRights" id='Chk_<%#Eval("ResourceID") %>_<%#Eval("ChildCode") %>_<%#Eval("RightsValue") %>' <%# (Convert.ToInt32(Eval("IsEnabled"))==0?"":"checked") %> value="<%#Eval("ID") %>_<%#Eval("ChildCode") %>_<%#Eval("RightsValue") %>" /><%#Eval("RightsName") %></td>
                                        </tr> 
                                    </table> 
                                </ItemTemplate>
                                <ItemStyle BorderWidth="0" />
                            </asp:DataList> 
                            <asp:DataList ID="dlRights_ChildRes1" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                                <ItemTemplate>
                                    <table border="0" style="margin-right:4px; width:150px;" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="height:30px; border-bottom:0px;"><input type="checkbox" name="ChkChildRights" id='Chk_<%#Eval("ResourceID") %>_<%#Eval("ChildCode") %>_<%#Eval("RightsValue") %>' <%# (Convert.ToInt32(Eval("IsEnabled"))==0?"":"checked") %> value="<%#Eval("ID") %>_<%#Eval("ChildCode") %>_<%#Eval("RightsValue") %>" /><%#Eval("RightsName") %></td>
                                        </tr> 
                                    </table> 
                                </ItemTemplate>
                                <ItemStyle BorderWidth="0" />
                            </asp:DataList> 
                            <asp:DataList ID="dlRights_ChildRes2" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                                <ItemTemplate>
                                    <table border="0" style="margin-right:4px; width:150px;" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="height:30px; border-bottom:0px;"><input type="checkbox" name="ChkChildRights" id='Chk_<%#Eval("ResourceID") %>_<%#Eval("ChildCode") %>_<%#Eval("RightsValue") %>' <%# (Convert.ToInt32(Eval("IsEnabled"))==0?"":"checked") %> value="<%#Eval("ID") %>_<%#Eval("ChildCode") %>_<%#Eval("RightsValue") %>" /><%#Eval("RightsName") %></td>
                                        </tr> 
                                    </table> 
                                </ItemTemplate>
                                <ItemStyle BorderWidth="0" />
                            </asp:DataList> 
                        </td>
                   </tr> 
               </table>
            </ItemTemplate>
        </asp:Repeater> 
       </div> 
    </div>
    </form>
</body>
</html>
