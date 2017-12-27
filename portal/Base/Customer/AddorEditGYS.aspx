<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditGYS.aspx.cs" Inherits="Base_Customer_AddorEditGYS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>供应商</title>
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
            <div title="供应商" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 1px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                        <tr>
                            <th style="width: 14%">供应商编号</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_供应商编号" class="easyui-textbox" style="width: 95%;" readonly="readonly" /></td>
                            <th style="width: 14%">联系人</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_联系人" class="easyui-textbox" data-options="required:true" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th style="width: 14%">供应商名称</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_供应商名称" class="easyui-textbox" style="width: 98%;" data-options="required:true" /></td>
                        </tr>
                        <tr>
                            <th>主营产品</th>
                            <td colspan="5">
                                <input class="easyui-textbox" id="<%=ResID %>_主营产品" style="width: 99%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div title="供应商联系人信息" class="easyui-panel" data-options="collapsible:true" style="overflow: hidden; padding: 1px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table1">
                        <tr>
                            <th style="width: 14%">联系电话</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_联系电话" class="easyui-textbox" style="width: 95%;" data-options="required:true,validType:'telNum'" /></td>
                            <th style="width: 14%">手机</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_手机" name="phoneNum" class="easyui-textbox" style="width: 95%;" 
                                    data-options="required:true,validType:'phoneNum'" />
                            </td>
                        </tr>
                        <tr>
                            <th>地址</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_地址" type="text" class="easyui-textbox" style="width: 98%;" /></td>
                        </tr>
                        <tr>
                            <th>邮件</th>
                            <td>
                                <input id="<%=ResID %>_邮件" type="text" class="easyui-textbox" style="width: 95%;" 
                                    data-options="required:true,validType:'email'" /></td>
                            <th>传真</th>
                            <td>
                                <input id="<%=ResID %>_传真" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>网址</th>
                            <td>
                                <input id="<%=ResID %>_网址" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>其他联系人</th>
                            <td>
                                <input id="<%=ResID %>_其他联系人" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>部门</th>
                            <td>
                                <input id="<%=ResID %>_部门" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>职位</th>
                            <td>
                                <input id="<%=ResID %>_职位" type="text" class="easyui-textbox" style="width: 95%;" /></td>

                        </tr>
                        <tr>
                            <th>登记人</th>
                            <td>
                                <input id="<%=ResID %>_登记人" type="text" class="easyui-textbox" style="width: 95%" 
                                    value="<%=UserName %>" data-options="required:true" /></td>
                            <th>登记日期</th>
                            <td>
                                <input id="<%=ResID %>_登记日期" type="text" class="easyui-datebox" style="width: 95%" 
                                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" value="<%=Time%>" /></td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="5">
                                <input class="easyui-textbox" id="<%=ResID %>_备注" style="width: 99%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
