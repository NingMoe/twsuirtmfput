<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditBX.aspx.cs" Inherits="Base_Finance_AddorEditBX" %>

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
                        }
                        SetBackgroundColor();
                    }
                });
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
                            <th style="width:120px;">日期</th>
                            <td style="width:270px;">
                                <input id="<%=ResID %>_日期" type="text" class="easyui-datebox"   style="width: 95%" /></td>
                            <th style="width:120px">报销人</th>
                            <td style="width:270px;">
                                <input id="<%=ResID %>_报销人" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >报销内容</th>
                            <td colspan="3" >
                                <input id="<%=ResID %>_报销内容" type="text" class="easyui-textbox"  style="width: 98%" /></td>
                        </tr><tr>
                            <th >报销说明</th>
                            <td colspan="3" >
                                <input id="<%=ResID %>_报销说明" type="text" class="easyui-textbox"  style="width: 98%" /></td>
                        </tr><tr>
                            <th >报销金额</th>
                            <td >
                                <input id="<%=ResID %>_报销金额" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th >凭证张数</th>
                            <td >
                                <input id="<%=ResID %>_凭证张数" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <th>报销科目</th>
                            <td>
                                <input id="<%=ResID %>_报销科目" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>是否已有请款</th>
                            <td>
                              <input id="<%=ResID %>_是否已有请款" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>请款编号</th>
                            <td><input id="<%=ResID %>_请款编号"  type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>请款金额</th>
                            <td><input id="<%=ResID %>_请款金额" type="text" class="easyui-textbox"  style="width: 95%;" /></td>                            
                        </tr>
                        <tr><th>备注</th>
                            <td colspan="3">
                                <input class="easyui-textbox" id="<%=ResID %>_备注" style="width: 98%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                        <tr><th>外包项目编号</th>
                            <td colspan="3">
                                <input class="easyui-textbox" id="<%=ResID %>_外包项目编号" style="width: 98%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                        <tr><th>是否已汇款</th>
                             <td><input id="<%=ResID %>_是否已汇款"  type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>凭证是否已到财务</th>
                            <td><input id="<%=ResID %>_凭证是否已到财务" type="text" class="easyui-textbox"  style="width: 95%;" /></td>                            
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
