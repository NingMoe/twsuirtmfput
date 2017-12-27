<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditCKJL.aspx.cs" Inherits="Base_Finance_AddorEditCKJL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>收款记录维护界面</title>
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
            } else {
                if ("<%=FPSQDH %>"!="") {
                    $("#<%=ResID %>_发票申请单号").textbox("setValue", "<%=FPSQDH %>");
                    $("#<%=ResID %>_发票申请单号").textbox('textbox').attr("readonly", true);
                }
            }
            SetBackgroundColor();
        });
       
        // 验证整数或小数
        $.extend($.fn.validatebox.defaults.rules, {
            intOrFloat: {
                validator: function (value) {
                    return /^\d+(\.\d+)?$/i.test(value);
                },
                message: '请输入数字!'
            }
        });

        //保存方法
        function fnParentSave() {
            //验证非空
            var check = $('#form1').form('validate');
            if (!check) { return; }
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
        <div class="con" id="div<%=ResID %>FormTable" style="overflow: hidden; position: relative; height: 100%; border: none">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="btnParentSave"
                        src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnParentSave(); return false;" /></a>
                        &nbsp;<a style="border: 0px;" href="#" onclick="window.parent.closeWindow1();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="收款记录" class="easyui-panel" collapsible="true" style="overflow: hidden; padding:1px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table1">
                         <tr>
                            <th style="width:15%">催款日期</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_催款日期" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                            <th style="width:15%">催款人员</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_催款人员" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >预计回款日期</th>
                            <td >
                                <input id="<%=ResID %>_预计回款日期" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                            <th >客户方联系人</th>
                            <td >
                                <input id="<%=ResID %>_客户方联系人" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <th>发票申请单号</th>
                            <td >
                                <input id="<%=ResID %>_发票申请单号" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                            <th>催款发票申请单号</th>
                            <td >
                                <input id="<%=ResID %>_催款发票申请单号" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr>                
                        <tr>
                            <th>备注</th>
                            <td colspan="3">
                                <input class="easyui-textbox" id="<%=ResID %>_备注" style="width: 98%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
