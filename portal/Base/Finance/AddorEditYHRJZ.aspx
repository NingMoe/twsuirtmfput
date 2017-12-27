<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditYHRJZ.aspx.cs" Inherits="Base_Finance_AddorEditYHRJZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>银行日记账</title>
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
            LoadPro("<%=ResID %>", "<%=RecID %>");
            SetBackgroundColor();
        });

        function LoadPro(ResID, RecID) {
            var SelectRecordData2 = {
                InitializationStr: "#" + ResID + "_请款编号",
                key: "#" + ResID + "_请款编号",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_请款编号").val(),
                keyWordValue: "LCXMBHCX",
                idField: "请款编号",
                textField: "请款编号",
                RecID: RecID,
                KeyWidth: 248,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 500,
                HasLastOperation: true,
                Condition:" and 归档='是'",
                QueryKeyField: "请款编号,请款金额,申请人,申请日期,请款事由",
                UserDefinedSql:"282994819609",
                Columns: "请款编号#请款编号,请款金额#请款金额,申请人#申请人,申请日期#申请日期,请款事由#请款事由",
                SetValueStr: "请款编号=请款编号",
                ROW_NUMBER_ORDER: "请款编号=流程编号"
            }
            InitializationComboGrid(SelectRecordData2);

            var SelectRecordData1 = {
                InitializationStr: "#" + ResID + "_报销编号",
                key: "#" + ResID + "_报销编号",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_报销编号").val(),
                keyWordValue: "LCXMBHCX",
                idField: "报销编号",
                textField: "报销编号",
                RecID: RecID,
                KeyWidth: 248,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 500,
                Condition:" and 归档='是'",
                HasLastOperation: true,
                QueryKeyField: "报销编号,报销人,日期,报销内容,报销金额",
                UserDefinedSql:"184270067595",
                Columns: "报销编号#报销编号,报销人#报销人,日期#日期,报销内容#报销内容,报销金额#报销金额",
                SetValueStr: "报销编号=报销编号",
                ROW_NUMBER_ORDER:  "报销编号=报销单编号"
            }
            InitializationComboGrid(SelectRecordData1);
        }

        function LastOperation(rowData, SelectRecordData) {
        }
        // 验证整数或小数
        $.extend($.fn.validatebox.defaults.rules, {
            intOrFloat: {
                validator: function (value) {
                    return /^\d+(\.\d+)?$/i.test(value);
                },
                message: '请输入数字!'
            },
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
            <div title="银行日记账" class="easyui-panel" collapsible="true" style="overflow: hidden; padding:1px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width:13%;">日期</th>
                            <td style="width:22%;">
                                <input id="<%=ResID %>_日期" type="text" class="easyui-datebox"  style="width: 95%" />
                            </td>
                            <th style="width:13%;">凭证单号</th>
                            <td style="width:22%;">
                            <input id="<%=ResID %>_凭证单号" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>摘要</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_摘要" type="text"  style="width: 98%; height: 60px" data-options="multiline:true"  class="easyui-textbox" style="width: 98%" /></td>
                        </tr>
                        <tr>
                            <th>收款</th>
                            <td><input id="<%=ResID %>_收款" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>支出</th>
                            <td><input id="<%=ResID %>_支出" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                         <tr>
                            <th>余额</th>
                            <td><input id="<%=ResID %>_余额" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>费用类型</th>
                            <td><select id="<%=ResID %>_费用类型"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="付款">付款</option>
                                <option value="项目收款">项目收款</option><option value="其他收款">其他收款</option>
                            </select></td>
                        </tr>
                        <tr>
                            <th>销售</th>
                            <td><input id="<%=ResID %>_销售"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                    data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=gsyyb' " />
                            </td>
                            <th>报销编号</th>
                            <td><input id="<%=ResID %>_报销编号" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr>
                            <tr>
                            <th>内部编号</th>
                            <td><input id="<%=ResID %>_内部编号" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                             <th>请款编号</th>
                            <td><input id="<%=ResID %>_请款编号" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr>
                        <tr> 
                            <th>回款时间</th>
                            <td><input id="<%=ResID %>_回款时间" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                          <th>银行类型</th>
                            <td rowspan="3">
                            <input id="<%=ResID %>_银行类型" value="<%=BackType %>" readonly="readonly" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
