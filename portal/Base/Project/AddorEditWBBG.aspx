<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditWBBG.aspx.cs" Inherits="Base_Project_AddorEditWBBG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
<title>外包公司信息表</title>
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
                            if (key == "填表日期" || key == "结算时间" || key == "代理名称" || key == "代理类型" || key == "结算情况" || key == "项目编号") {
                                $("#<%=ResID %>_" + key).textbox("setValue", jsonList[i][key]);
                            } else {
                                $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                            }
                        }
                        if ("<%=SearchType %>".indexOf("readonly") != -1) {
                            var textInput = $("#div<%=ResID %>FormTable").find("input:text");
                            textInput.textbox({ editable: false });
                            textInput.attr("readonly",true);
                            $("#btnParentSave").hide();
                            SetBackgroundColor();
                        }
                    }
                }
            });
        } else {
            if ("<%=XMBH %>" != "") {
                $("#<%=ResID %>_项目编号").val("<%=XMBH %>");
                $("#<%=ResID %>_督导").val("<%=DD %>");
                $("#<%=ResID %>_项目编号").attr("readonly", true);
                $("#<%=ResID %>_督导").attr("readonly", true);
            }
            if ("<%=DLMC %>" != "") {
                $("#<%=ResID %>_代理名称").val("<%=DLMC %>");
                $("#<%=ResID %>_代理名称").attr("readonly", true);
            }

        }
        LoadCust("<%=ResID %>", "<%=RecID %>");
        LoadCust1("<%=ResID %>", "<%=RecID %>");
        BindSJ();
        SetBackgroundColor();
    });

    function LoadCust(ResID, RecID) {
        var SelectRecordData1 = {
            InitializationStr: "#" + ResID + "_项目编号",
            key: "#" + ResID + "_项目编号",
            ResID: ResID,
            deafultValue: $("#" + ResID + "_项目编号").val(),
            keyWordValue: "LCXMBHCX",
            idField: "项目编号",
            textField: "项目编号",
            RecID: RecID,
            KeyWidth: 233,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 500,
            HasLastOperation: true,
            QueryKeyField: "项目编号,项目名称,客户简称,客户全称",
            UserDefinedSql:"285435593984",
            Columns: "项目编号#项目编号,项目名称#项目名称,销售#销售,客户简称#客户简称,客户全称#客户全称",
            SetValueStr: "项目编号=项目编号",
            ROW_NUMBER_ORDER: ""
        }
        InitializationComboGrid(SelectRecordData1);
    }

    function LoadCust1(ResID, RecID) {
        var SelectRecordData1 = {
            InitializationStr: "#" + ResID + "_代理名称",
            key: "#" + ResID + "_代理名称",
            ResID: ResID,
            deafultValue: $("#" + ResID + "_代理名称").val(),
            keyWordValue: "LCXMBHCX",
            idField: "代理名称",
            textField: "代理名称",
            RecID: RecID,
            KeyWidth: 233,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 500,
            HasLastOperation: true,
            QueryKeyField: "代理名称",
            UserDefinedSql: "283621252187",
            Columns: "代理名称#代理名称",
            SetValueStr: "代理名称=代理名称",
            ROW_NUMBER_ORDER: ""
        }   
        InitializationComboGrid(SelectRecordData1);
    }
    function LastOperation(rowData, SelectRecordData) {
           
    }

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
        if (!check)  return;
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

    //绑定事件
    function BindSJ() {
        $("#<%=ResID %>_有效完成数量").on("keyup", function () { changeZFY();});
        $("#<%=ResID %>_样本单价").on("keyup", function () { changeZFY();});
        $("#<%=ResID %>_其他费用").on("keyup", function () { changeZFY();});
        $("#<%=ResID %>_总费用").attr("readonly", true);
    }
    //总费用=[有效完成数量] * [样本单价] + [其他费用]
    function changeZFY() {
        var yxwcsl = 0;
        if ($("#<%=ResID %>_有效完成数量").val() != "" && !isNaN($("#<%=ResID %>_有效完成数量").val())) {
            yxwcsl = parseFloat($("#<%=ResID %>_有效完成数量").val());
        }
        var ybdj = 0;
        if ($("#<%=ResID %>_样本单价").val() != "" && !isNaN($("#<%=ResID %>_样本单价").val())) {
            ybdj = parseFloat($("#<%=ResID %>_样本单价").val());
        }
        var qtfy = 0;
        if ($("#<%=ResID %>_其他费用").val() != "" && !isNaN($("#<%=ResID %>_其他费用").val())) {
            qtfy = parseFloat($("#<%=ResID %>_其他费用").val());
        }
        var zfy = yxwcsl * ybdj + qtfy;
        $("#<%=ResID %>_总费用").val(Math.round(zfy * 100) / 100);
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
            <div title="外包表格" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 3px; margin: 0px;width:98%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 130px;">项目编号</th>
                            <td style="width: 250px;">
                               <input id="<%=ResID %>_项目编号"  class="box3" style="width: 95%;" />
                            </td>
                            <th style="width: 130px;">督导</th>
                            <td style="width: 250px;">
                                <input id="<%=ResID %>_督导" value="<%=UserName %>"  class="box3" style="width: 95%;" />
                            </td>                            
                        </tr>
                        <tr>
                            <th>外包项目编号</th>
                            <td>
                                <input id="<%=ResID %>_外包项目编号"  class="box3" style="width: 95%;"  />
                            </td>
                            <th>填表日期</th>
                            <td><input id="<%=ResID %>_填表日期"   class="easyui-datebox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>代理名称</th>
                            <td >
                                <input id="<%=ResID %>_代理名称"  class="box3" style="width: 95%;" />
                            </td>     
                            <th>代理类型</th>
                            <td style="width: 250px;">
                                <select id="<%=ResID %>_代理类型"   class="easyui-combobox" style="width:95%;"  >
                                <option value="online">online</option><option value="offline">offline</option>
                            </select></td>                          
                        </tr>
                        <tr>
                            <th>完成数量</th>
                            <td >
                                <input id="<%=ResID %>_完成数量"  class="box3" style="width: 95%;" />
                            </td> 
                            <th>有效完成数量</th>
                            <td >
                                <input id="<%=ResID %>_有效完成数量"  class="box3" style="width: 95%;" />
                            </td> 
                        </tr>   
                        <tr>
                            <th>样本单价</th>
                            <td >
                                <input id="<%=ResID %>_样本单价"  class="box3" style="width: 95%;" />
                            </td> 
                            <th>其他费用</th>
                            <td >
                                <input id="<%=ResID %>_其他费用"  class="box3" style="width: 95%;" />
                            </td> 
                        </tr>   
                        <tr>
                            <th>总费用</th>
                            <td >
                                <input id="<%=ResID %>_总费用"  class="box3" style="width: 95%;" />
                            </td> 
                            <th>结算情况</th>
                            <td >
                                <select id="<%=ResID %>_结算情况"   class="easyui-combobox" style="width:95%;"  >
                                <option value="未结算">未结算</option><option value="已结算">已结算</option>
                                <option value="抵扣">抵扣</option>
                            </select>
                            </td> 
                        </tr>  
                        <tr>
                            <th>结算时间</th><td><input id="<%=ResID %>_结算时间"  class="easyui-datebox" style="width: 95%;" /></td>
                            <th>&nbsp;</th><td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
