<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditYHKHJL.aspx.cs" Inherits="Base_Sample_AddorEditYHKHJL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>银行卡号记录</title>
        <%=this.GetScript1_4_3   %>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            if ('<%=RecID %>' != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=<%=RecID %>",
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
                        var zhmc = "";
                        for (var i = 0; i < jsonList.length; i++) {
                            for (var key in jsonList[i]) {
                                $("#<%=ResID %>_" + key).textbox("setValue", jsonList[i][key]);
                            }
                        }
                        if ("<%=SearchType %>".indexOf("readonly") != -1) {
                            var textInput = $("#div<%=ResID %>FormTable").find("input:text");
                            textInput.textbox({ editable: false });
                            $("#btnParentSave").hide();
                            SetBackgroundColor();
                        }
                    }
                });
            }
            SetBackgroundColor();
        });

        $.extend($.fn.validatebox.defaults.rules, {
            intOrFloat: {// 验证整数或小数
                validator: function (value) {
                    return /^\d+(\.\d+)?$/i.test(value);
                },
                message: '请输入数字!'
            }
        });

        $.extend($.fn.validatebox.defaults.rules, {
            intOrFloat: {// 验证整数或小数
                validator: function (value) {
                    return /^\d+(\.\d+)?$/i.test(value);
                },
                message: '请输入数字!'
            }
        });
        //保存方法
        function fnParentSave() {
            var check = $('#form1').form('validate');
            if (!check) return;
            var jsonStr1 = "[{";
            jsonStr1 += GetFromJson("<%=ResID %>");
            jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
            jsonStr1 += "}]";
            $("#btnParentSave").attr("disabled", true);
            $.ajax({
                type: "POST",
                dataType: "json",
                data: { "Json": "" + jsonStr1 + "" },
                url: "../Common/CommonAjax_Request.aspx?typeValue=SaveInfo&ResID=<%=ResID %>&RecID=<%=RecID %>",
                success: function (obj) {
                    if (obj.success || obj.success == "true") {
                        $("#btnParentSave").attr("disabled", false);
                        alert("保存成功!");
                        window.parent.closeWindow1();
                    } else {
                        alert("保存失败,请刷新页面！");
                    }
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; height:460px; border: none">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="btnParentSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnParentSave(); return false;" /></a>
                        &nbsp;<a style="border: 0px;" href="#" onclick="window.parent.closeWindow1();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="银行卡号记录" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 3px; margin: 0px;width:98%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 130px;">姓名</th>   
                            <td colspan="3">
                               <input id="<%=ResID %>_姓名"  class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr>
                        <tr>
                            <th >银行卡号</th>
                            <td colspan="3">
                               <input id="<%=ResID %>_银行卡号"  class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr>
                        <tr>
                            <th >开户行</th>
                            <td  colspan="3"><input id="<%=ResID %>_开户行"  class="easyui-textbox" style="width: 98%;" /></td>                            
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
