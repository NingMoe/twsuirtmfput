<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Base_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>青岛润德会计师事务所管理软件</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
  <%=this.GetScript1_4_3   %>
    <script src="../Project/js.js"></script>
</head>
<body>
 <script type="text/javascript" language="javascript">
     var TitleColName = "";
     var ColName = "";
     var UserName = "<%=UserName%>";
     var Time = "<%=Time%>";
     var _RecID = "<%=RecID%>";
        $(document).ready(function () {
            //如果主表记录ID和主表ResID不为空，并且 RecID 为空 （代表是添加记录），则执行下列方法
            //读取并自动完成主表的记录
           
            if ('<%=RecID %>' != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=<%=RecID %>",
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
                        for (var i = 0; i < jsonList.length; i++) {
                            for (var key in jsonList[i]) {
                                if ($("#<%=ResID %>_" + key).attr("type") == "checkbox") {
                                    if (jsonList[i][key] == "1" || jsonList[i][key] == "是" || jsonList[i][key] == "True" || jsonList[i][key] == "true") $("#<%=ResID %>_" + key).attr("checked", "true");
                                }
                                else $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                            }
                        }
                    }
                });
            }
            if ("<%=SearchType %>".indexOf("readonly") != -1) {
                var textInput = $("#div<%=ResID %>FormTable").find("input:text");
                var radioInput = $("#div<%=ResID %>FormTable").find("input:radio");
                var textarea = $("#div<%=ResID %>FormTable").find("textarea");
                var select = $("#div<%=ResID %>FormTable").find("select");
                var checkboxInput = $("#div<%=ResID %>FormTable").find("input:checkbox");
                checkboxInput.attr("disabled", "disabled");
                radioInput.attr("disabled", "disabled");
                textInput.attr("readonly", "readonly");
                textarea.attr("readonly", "readonly");
                select.attr("disabled", "disabled");
                $("#fnChildSave").hide();
            }
            SetBackgroundColor();
            LoadWriter("<%=ResID %>", "<%=RecID %>");
        });
       
     function fnChildSave() {//保存方法
         if (CheckValue("div<%=ResID %>FormTable")) {
             if ($.isFunction(window.ChildFunciton)) {
                 ChildFunciton();
             }
             if ($.isFunction(window.ChildCheckValue)) {
                 if (!ChildCheckValue())
                     return false;
             }
             $("#fnChildSave").attr("disabled", true);
             var jsonStr1 = "[{";
             jsonStr1 += GetFromJson("<%=ResID %>");
             if ($.isFunction(window.GetUserControlsJson)) {
                 jsonStr1 += GetUserControlsJson();
             }
             else {
                 jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
             }
             jsonStr1 += "}]";
             $.ajax({
                 type: "POST",
                 dataType: "json",
                 data: { "Json": "" + jsonStr1 + "" },
                 url: "<%=strUrl %>",
                 success: function (obj) {
                     if (obj.success || obj.success == "true") {
                         alert("保存成功！"); 
                         window.parent.ParentCloseWindow(); 
                         window.parent.RefreshGrid('<%=gridID %>');
                     } else {
                         alert("保存失败,请刷新页面！");
                     }
                 }
             });
         }
     }
    </script>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y: hidden; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnChildSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a></td>
                </tr>
            </table>
            <div title="<%=strTilte %>" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
                 <asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
        </div>
         <div id="ChooseTYWindow" style="overflow-x:hidden;overflow:scroll;position:relative;display:none;"></div>
         <input type="hidden" value=""  id="DictionaryConditon" />
        <asp:TextBox ID="txtResourceID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtRecID" runat="server" Visible="false"></asp:TextBox>   
        <div closed="true" class="easyui-window" id="divDictionary" style="overflow: hidden;" />
        <input type="hidden" value="" id="childForm" />
    </form>
</body>
</html>
<script type="text/javascript">
    $("#div<%=ResID %>FormTable").css("height", document.documentElement.clientHeight);

</script>