<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSearch.aspx.cs" Inherits="Base_SystemConfig_AddSearch" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加查询字段</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
</head>
<body>
 <script type="text/javascript" language="javascript">
     var jsonStr = "";
     $(document).ready(function () {
         SetBackgroundColor();     

     });


        function fnSave() {
            if (CheckValue("div<%=ResID %>FormTable")) {
                $("#fnChildSave").attr("disabled", true);
                if (GetColName()) { 
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        data: { "Json": "" + jsonStr + "" },
                        url: "Ajax_Request.aspx?typeValue=SaveSearchInfo&ResID=<%=ResID %>&keyWordValue=<%=RelatedValue %>",
                        success: function (obj) {
                            if (obj.success || obj.success == "true") {
                                alert("保存成功！");
                                window.parent.ParentCloseWindow();
                                window.parent.RefreshGrid("Grid<%=PResID %>_<%=ResID %>");
                            } else {
                                alert("保存失败,请刷新页面！");
                            }
                        }
                    });
                }
            }
        }

     function SetColName(strTitle, strValue) {
         var title = strTitle.split(",");
         var value = strValue.split(",");
         // $("input[name='ChkColName']").attr("checked", 'false');
         $("input[name='ChkColName']").each(function () {
             for (var i = 0 ; i < value.length; i++) {
                 if (value[i] == this.value) {
                     $(this).attr("checked", 'true');
                     $("input[id='" + this.id.replace("Chk_", "txt_") + "']").val(title[i]);
                 }
             }
         })
     }

     function ValidateColName(obj) {
         $("input[id='" + obj.id + "']:checked").each(function () { 
             var result = $("input[id='" + obj.id.replace("Chk_", "txt_") + "']").val()
             if (result == "") {
                 alert("字段“" + obj.value + "”的显示名称不能为空");
                 obj.checked = false;
                 return false;
             } 
         })
     }

     function GetColName() {
         jsonStr = "";
         $("input[name='ChkColName']:checked").each(function () {
             var result = $("input[id='" + this.id.replace("Chk_", "txt_") + "']").val();//.attr("value");

             if (result == "") {
                 alert("字段“" + this.value + "”的显示名称不能为空");
                 return false;
             }
             else { 
                 var jsonStr1 = "[{";
                 jsonStr1 += "'参数关键字':'<%=RelatedValue%>','显示字段':'" + result + "','查询字段':'" + this.value + "','数据类型':'" + $("input[id='" + this.id.replace("Chk_", "txtDataType_") + "']").val() + "'";
                 jsonStr1 += "}]";
                 jsonStr += "," + jsonStr1;
             }
         })
         if (jsonStr != "") jsonStr = jsonStr.substr(1, jsonStr.length - 1); 
         return true;
     }
    </script>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y:auto; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="列表查询配置" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;"> 
             <asp:DataList ID="dlCol" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                <ItemTemplate>
                    <table border="0" style="margin-right:4px; width:99%;" cellspacing="0" class="table2" cellpadding="0">
                        <tr>
                            <td style="width:180px; height:30px;"><input type="checkbox" name="ChkColName" onclick="ValidateColName(this)" id='Chk_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("CD_DISPNAME") %>" <%# (Convert.ToInt32(Eval("IsBe"))==0?"":"checked") %> /><%#Eval("CD_DISPNAME") %></td>
                            <td style="width:150px;"><input type="text" id='txt_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("CD_ShowName") %>" class="box3" style="width:99%;" /></td> 
                            <td style=" text-align:left;"><input type="text" id='txtDataType_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("DataType") %>" readonly="readonly"  class="box3" style="width:60px;" /></td> 
                        </tr> 
                    </table> 
                </ItemTemplate>
            </asp:DataList>
           </div>
        </div>
         <div id="ChooseTYWindow" style="overflow-x:hidden;overflow:scroll;position:relative;display:none;"></div>
         <input type="hidden" value=""  id="DictionaryConditon" />
    </form>
</body>
</html>
<script type="text/javascript">
    $("#div<%=ResID %>FormTable").css("height", document.documentElement.clientHeight);

</script>
