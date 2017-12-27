﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditKPXX.aspx.cs" Inherits="Base_Customer_AddorEditKPXX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>开票信息</title>
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
                if ("<%=BH %>" != "") {
                    $("#<%=ResID %>_编号").textbox("setValue", "<%=BH %>");
                    $("#<%=ResID %>_客户全称").textbox("setValue", "<%=KHQC %>");
                    $("#<%=ResID %>_编号").textbox("textbox").attr("readonly", true);
                    $("#<%=ResID %>_客户全称").textbox("textbox").attr("readonly", true);
                }
            }
            SetBackgroundColor();
        });
        //验证手机号 
        $.extend($.fn.validatebox.defaults.rules, {
            phoneNum: {
                validator: function (value, param) {
                    return /^1[3-8]+\d{9}$/.test(value);
                },
                message: '请输入正确的手机号码。'
            },

            telNum: { //既验证手机号，又验证座机号
                validator: function (value, param) {
                    return /(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\d3)|(\d{3}\-))?(1[358]\d{9})$)/.test(value);
                },
                message: '请输入正确的电话号码。'
            }
        });
        function fnParentSave() {
            //验证非空
            var check = $('#form1').form('validate');
            if (!check) { return; }

            var jsonStr1 = "[{";
            jsonStr1 += GetFromJson("<%=ResID %>");
            jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
            jsonStr1 += "}]";
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
                            $("#btnParentSave").attr("disabled", false);
                        }
                    }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow: hidden; height: 100%; position: relative; border: none">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="btnParentSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnParentSave(); return false;" /></a>
                        &nbsp;<a style="border: 0px;" href="#" onclick=" window.parent.closeWindow1();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="开票信息" class="easyui-panel"   data-options="collapsible:true" style="overflow: hidden; width:100%; padding: 1px; margin: 0px;">     
                   <div class="easyui-panel" border="true" style="border-bottom: none;  width:100%;">         
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                        <tr><th style="width:100px;">编号</th>
                            <td style="width:400px;">
                            <input id="<%=ResID %>_编号" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr> 
                        <tr><th >客户全称</th>
                            <td ><input id="<%=ResID %>_客户全称" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                        <th>客户开票信息</th>
                        <td >
                            <input id="<%=ResID %>_客户开票信息" class="easyui-textbox" style="width: 95%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
