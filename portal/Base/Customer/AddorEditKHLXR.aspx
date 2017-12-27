<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditKHLXR.aspx.cs" Inherits="Base_Customer_AddorEditKHLXR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>客户联系人信息维护界面</title>
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
                if ("<%=KHDM %>" != "") {
                    $("#<%=ResID %>_客户代码").textbox("setValue", "<%=KHDM %>");
                    $("#<%=ResID %>_客户全称").textbox("setValue", "<%=KHQC %>");
                    $("#<%=ResID %>_客户代码").attr("readonly", true);
                    $("#<%=ResID %>_客户全称").textbox('textbox').attr("readonly", true);
                }
            }
            LoadCust("<%=ResID %>", "<%=RecID %>");
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

            function LoadCust(ResID, RecID) {
                var SelectRecordData1 = {
                    InitializationStr: "#" + ResID + "_客户代码",
                    key: "#" + ResID + "_客户代码",
                    ResID: ResID,
                    deafultValue: $("#" + ResID + "_客户代码").val(),
                    keyWordValue: "LCXMBHCX",
                    idField: "客户代码",
                    textField: "客户代码",
                    RecID: RecID,
                    KeyWidth: 233,
                    Keyheight: 25,
                    PercentageWidth: 70,
                    panelWidth: 500,
                    HasLastOperation: true,
                    QueryKeyField: "客户代码,客户全称,客户简称",
                    UserDefinedSql: "401290367151",
                    Columns: "客户代码#客户代码,客户全称#客户全称,客户简称#客户简称",
                    SetValueStr: "客户代码=客户代码,客户全称=客户全称",
                    ROW_NUMBER_ORDER: "客户代码=编号"
                } 
                InitializationComboGrid(SelectRecordData1);
            }
            function LastOperation(rowData, SelectRecordData) {
                var khdm = rowData.客户代码
                var khqc = rowData.客户全称
                if (khdm != "undefined" && khdm != "" && khdm != null) {
                    $("#<%=ResID %>_客户代码").textbox("setValue", khdm)
                }
                if (khqc != "undefined" && khqc != "" && khqc != null) {
                    $("#<%=ResID %>_客户全称").textbox("setValue", khqc)
                }
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
            <div title="客户信息" class="easyui-panel"   data-options="collapsible:true" style="overflow: hidden; width:100%; padding: 1px; margin: 0px;">     
                   <div class="easyui-panel" border="true" style="border-bottom: none;  width:100%;">         
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                        <tr><th style="width:13%">代码</th>
                            <td style ="width:22%">
                                <input id="<%=ResID %>_代码" class="easyui-textbox" readonly="readonly" style="width: 95%" /></td>
                            <th style="width:13%">客户代码</th>
                            <td style ="width:22%"><input id="<%=ResID %>_客户代码" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>客户全称</th>
                            <td >
                                <input id="<%=ResID %>_客户全称" class="easyui-textbox" data-options="required:true" style="width: 95%;" />
                            </td><th>销售</th>
                            <td >
                                <input id="<%=ResID %>_销售" value="<%=UserName %>" class="easyui-textbox" data-options="required:true" style="width: 95%;" />
                            </td>
                        </tr>
                         <tr>
                            <th>联系人</th>
                            <td><input id="<%=ResID %>_联系人" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>职位</th>
                            <td><input id="<%=ResID %>_职位" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                         <tr>
                            <th>固定电话</th>
                            <td><input id="<%=ResID %>_固定电话" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>手机</th>
                            <td><input id="<%=ResID %>_手机" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr> <tr>
                            <th>最近联系时间</th>
                            <td><input id="<%=ResID %>_最近联系时间" class="easyui-datebox"  style="width: 95%;"
                             onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" value="<%=Time%>"  /></td>
                            <th>其他联系方式</th>
                            <td><input id="<%=ResID %>_其他联系方式" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>email</th>
                            <td >
                                <input id="<%=ResID %>_email" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>办公地址</th>
                            <td >
                                <input id="<%=ResID %>_办公地址" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_备注" class="easyui-textbox" style="width: 98%;" /></td>
                        </tr>
                    </table>
                   </div>
            </div>
        </div>
    </form>
</body>
</html>
