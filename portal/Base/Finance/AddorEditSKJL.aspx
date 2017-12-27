<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditSKJL.aspx.cs" Inherits="Base_Finance_AddorEditSKJL" %>

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
            if ("<%=XMBH %>" != "") {
                $("#<%=ResID %>_项目编号").textbox("setValue", "<%=XMBH %>");
                $("#<%=ResID %>_项目名称").textbox("setValue", "<%=XMMC %>");
                $("#<%=ResID %>_项目编号").attr("readonly", true);
                $("#<%=ResID %>_项目名称").textbox('textbox').attr("readonly", true);
            }
            if ("<%=FPSQDH %>" != "") {
                $("#<%=ResID %>_发票申请单号").textbox("setValue", "<%=FPSQDH %>");
                $("#<%=ResID %>_发票申请单号").textbox('textbox').attr("readonly", true);
            }

            if ("<%=SKBH %>" != "") {
                $("#<%=ResID %>_收款编号").textbox("setValue", "<%=SKBH %>");
                $("#<%=ResID %>_收款").textbox("setValue", "<%=SK %>");
                $("#<%=ResID %>_收款日期_库思中国银行").val("<%=SKRQ_KSZGYH %>");
                $("#<%=ResID %>_收款日期_库威中国银行").val("<%=SKRQ_KWZGYH %>");
                $("#<%=ResID %>_收款日期_库润民生银行").val("<%=SKRQ_KRMSYH %>");
                $("#<%=ResID %>_收款日期_支付宝kurun123126com").val("<%=SKRQ_ZFBKR126 %>");
                $("#<%=ResID %>_收款日期_支付宝qqsurvey1126com").val("<%=SKRQ_ZFBQQ1126 %>");
                $("#<%=ResID %>_收款日期_支付宝qqsurvey126com").val("<%=SKRQ_ZFBQQ126 %>");
                $("#<%=ResID %>_收款日期_库思农商银行").val("<%=SKRQ_KSNSYH %>");
                $("#<%=ResID %>_收款编号").textbox('textbox').attr("readonly", true);
                $("#<%=ResID %>_收款").textbox('textbox').attr("readonly", true);
                $("#<%=ResID %>_收款日期_库思中国银行").attr("readonly", true);
                $("#<%=ResID %>_收款日期_库威中国银行").attr("readonly", true);
                $("#<%=ResID %>_收款日期_库润民生银行").attr("readonly", true);
                $("#<%=ResID %>_收款日期_支付宝kurun123126com").attr("readonly", true);
                $("#<%=ResID %>_收款日期_支付宝qqsurvey1126com").attr("readonly", true);
                $("#<%=ResID %>_收款日期_支付宝qqsurvey126com").attr("readonly", true);
                $("#<%=ResID %>_收款日期_库思农商银行").attr("readonly", true);
                $("#<%=ResID %>_收款日期_现金").attr("readonly", true);
            }
        }
        var userTemp = ["收款日期_库思中国银行", "收款日期_库润民生银行", "收款日期_库威中国银行", "收款日期_现金", "收款日期_支付宝qqsurvey1126com", "收款日期_支付宝kurun123126com", "收款日期_支付宝qqsurvey126com", "收款日期_库思农商银行"];
        var resTemp = ["库思中国银行", "库润民生银行", "库威中国银行", "现金", "支付宝qqsurvey1126com", "支付宝库润123126com", "支付宝qqsurvey126com", "库思农商银行"];
        for (var i = 0; i < userTemp.length; i++) {
            LoadCust("<%=ResID %>", "<%=RecID %>", userTemp[i], resTemp[i]);
        }
        LoadPro("<%=ResID %>", "<%=RecID %>");
        SetBackgroundColor();
    });
        

       

    function LoadCust(ResID, RecID, userName,ResTemp) {
        var uSql = "";
        var uColumns = "";
        var uSetValueStr = "";
        var tableName = "";
        var ConditionStr = "";
        var ROW_NUMBER_ORDERStr="";
        if (ResTemp=="现金") {
            uSql = "283617186078";
            ROW_NUMBER_ORDERStr = userName + "=日期,凭证单号=凭证编号";
        } else {
            uSql = "423335232345";
            ConditionStr = " and 银行类型='" + ResTemp + "'";
            ROW_NUMBER_ORDERStr=userName + "=日期,收款编号=内部编号";
        }
        var UQueryKeyField = "凭证单号,销售,收款,摘要,收款编号," + userName;
        uColumns = "凭证单号#凭证单号,销售#销售,收款#收款,摘要#摘要,收款编号#收款编号,"+userName+"#"+userName;
        uSetValueStr  = "凭证单号=凭证单号,收款=收款,摘要=摘要,收款编号=收款编号,"+userName+"="+userName;
        var SelectRecordData1 = {
            InitializationStr: "#" + ResID + "_" + userName,
            key: "#" + ResID + "_" + userName,
            ResID: ResID,
            deafultValue: $("#" + ResID + "_" + userName).val(),
            keyWordValue: "LCXMBHCX",
            idField: userName,
            textField: userName,
            RecID: RecID,
            KeyWidth: 218,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 550,
            HasLastOperation: true,
            QueryKeyField: UQueryKeyField,
            Condition:ConditionStr,
            UserDefinedSql: uSql,
            Columns:uColumns,
            SetValueStr: uSetValueStr,
            ROW_NUMBER_ORDER: ROW_NUMBER_ORDERStr
        }
        InitializationComboGrid(SelectRecordData1);
    }

    function LoadPro(ResID, RecID) {
        var SelectRecordData1 = {
            InitializationStr: "#" + ResID + "_项目编号",
            key: "#" + ResID + "_项目编号",
            ResID: ResID,
            deafultValue: $("#" + ResID + "_项目编号").val(),
            keyWordValue: "LCXMBHCX",
            idField: "项目编号",
            textField: "项目编号",
            RecID: RecID,
            KeyWidth: 218,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 600,
            HasLastOperation: true,
            QueryKeyField: "项目编号,项目名称,销售,督导,客户简称",
            UserDefinedSql: "285435593984",
            Columns: "项目编号#项目编号,项目名称#项目名称,销售#销售,督导#督导,客户简称#客户简称,实际金额#实际金额,应收款#应收款,已收款#已收款",
            SetValueStr: "项目编号=项目编号,项目名称=项目名称",
            ROW_NUMBER_ORDER: "order by 项目编号 DESC "
        }
        InitializationComboGrid(SelectRecordData1);
    }

    function LastOperation(rowData, SelectRecordData) {
        var pzdh = rowData.凭证单号;
        if (pzdh != "undefined" && pzdh != "" && pzdh != null) {
            $("#<%=ResID %>_凭证单号").textbox("setValue", pzdh);
        }
        var xs = rowData.销售;
        if (xs != "undefined" && xs != "" && xs != null) {
            $("#<%=ResID %>_销售").textbox("setValue", xs);
        }
        var sk = rowData.收款;
        if (sk != "undefined" && sk != "" && sk != null) {
            $("#<%=ResID %>_收款").textbox("setValue", sk);
        }
        var zy = rowData.摘要
        if (zy != "undefined" && zy != "" && zy != null) {
            $("#<%=ResID %>_摘要").textbox("setValue", zy)
        }
        var sjbh = rowData.收款编号;
        if (sjbh != "undefined" && sjbh != "" && sjbh != null) {
            $("#<%=ResID %>_收款编号").textbox("setValue", sjbh);
        }
        var XMMC = rowData.项目名称;
        if (XMMC != "undefined" && XMMC != "" && XMMC != null) {
            $("#<%=ResID %>_项目名称").textbox("setValue", XMMC);
        }
    }
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
        changeSKRQ();
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
   
    function changeSKRQ(){
        var skrq="";
        if ($("#<%=ResID %>_收款日期_库思农商银行").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_库思农商银行").val();
        }
        if ($("#<%=ResID %>_收款日期_库思中国银行").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_库思中国银行").val();
        }
        if ($("#<%=ResID %>_收款日期_库润民生银行").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_库润民生银行").val();
        }
        if ($("#<%=ResID %>_收款日期_库威中国银行").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_库威中国银行").val();
        }
        if ($("#<%=ResID %>_收款日期_现金").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_现金").val();
        }
        if ($("#<%=ResID %>_收款日期_支付宝qqsurvey1126com").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_支付宝qqsurvey1126com").val();
        }
        if ($("#<%=ResID %>_收款日期_支付宝kurun123126com").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_支付宝kurun123126com").val();
        }
        if ($("#<%=ResID %>_收款日期_支付宝qqsurvey126com").val() != "") {
            skrq = $("#<%=ResID %>_收款日期_支付宝qqsurvey126com").val();
        }
        $("#<%=ResID %>_最终收款日期").val(skrq);
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
                            <th style="width:19%">收款日期_库思农商银行</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_收款日期_库思农商银行" type="text"  style="width: 95%" /></td>
                            <th style="width:15%">收款日期_库思中国银行</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_收款日期_库思中国银行" type="text"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >收款日期_库润民生银行</th>
                            <td >
                                <input id="<%=ResID %>_收款日期_库润民生银行" type="text" style="width: 95%" /></td>
                            <th >收款日期_库威中国银行</th>
                            <td >
                                <input id="<%=ResID %>_收款日期_库威中国银行" type="text"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >收款日期_现金</th>
                            <td >
                                <input id="<%=ResID %>_收款日期_现金" type="text"  style="width: 95%" /></td>
                            <th >收款日期_支付宝QQ1126</th>
                            <td >
                                <input id="<%=ResID %>_收款日期_支付宝qqsurvey1126com" type="text"style="width: 95%" /></td>
                        </tr><tr>
                            <th >收款日期_支付宝库润126</th>
                            <td >
                                <input id="<%=ResID %>_收款日期_支付宝kurun123126com" type="text"   style="width: 95%" /></td>
                            <th >收款日期_支付宝QQ126</th>
                            <td >
                                <input id="<%=ResID %>_收款日期_支付宝qqsurvey126com" type="text"  style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <th>收款编号</th>
                            <td>
                                <input id="<%=ResID %>_收款编号" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>凭证单号</th>
                            <td>
                              <input id="<%=ResID %>_凭证单号" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                        </tr>
                        <tr>

                            <th>收款</th>
                            <td>
                                <input id="<%=ResID %>_收款"  type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th>销售</th>
                            <td>
                                <input id="<%=ResID %>_销售" type="text" class="easyui-textbox"  style="width: 95%;" /></td>                            
                        </tr>
                        <tr>
                            <th>项目编号</th>
                            <td>
                                <input id="<%=ResID %>_项目编号" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>抵账日期</th>
                            <td>
                            <input id="<%=ResID %>_抵账日期" type="text" class="easyui-datebox" style="width: 95%" /></td>
                        </tr><tr>
                            <th>项目名称</th>
                            <td>
                                <input id="<%=ResID %>_项目名称" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>项目收款额</th>
                            <td>
                                <input id="<%=ResID %>_项目收款额" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr><tr>
                            <th>输入人员</th>
                            <td>
                                <input id="<%=ResID %>_输入人员" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>发票申请单号</th>
                            <td>
                                <input id="<%=ResID %>_发票申请单号" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr> <tr>
                            <th>最终收款日期</th>
                            <td >
                                <input id="<%=ResID %>_最终收款日期" type="text" class="easyui-textbox" readonly="readonly" style="width: 95%;" />
                            </td><th>&nbsp;</th><td >&nbsp;</td>
                        </tr>                
                        <tr>
                            <th>摘要</th>
                            <td colspan="3">
                                <input class="easyui-textbox" id="<%=ResID %>_摘要" style="width: 98%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
