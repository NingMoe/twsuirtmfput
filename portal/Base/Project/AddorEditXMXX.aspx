<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditXMXX.aspx.cs" Inherits="Base_Project_AddorEditXMXX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
<title>项目执行表维护界面</title>
    <%=this.GetScript1_4_3   %>
<script src="../../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
                            if (key == "实际开始时间" || key == "状态" || key == "实际结束时间" || key == "督导" || key == "编程人员" || key == "销售" || key == "数据处理人" || key == "调查主题" || key == "项目类型") {
                                $("#<%=ResID %>_" + key).textbox("setValue", jsonList[i][key]);
                            } else {
                                $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                            }
                            if (key == "销售提成" && jsonList[i][key] != "" && jsonList[i][key] != null) {
                                var textInput = $("#div<%=ResID %>FormTable").find("input:text");
                                textInput.textbox({ editable: false });
                                $("#btnParentSave").hide();
                                SetBackgroundColor();
                            }
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
        var userTemp = ["客户全称"];
        for (var i = 0; i < userTemp.length; i++) {
            LoadCus("<%=ResID %>", "<%=RecID %>", userTemp[i]);
        }
        BindSJ();
        SetBackgroundColor();

    });

    function LoadCus(ResID, RecID, userName) {
        var uSql = "";
        var uColumns = "";
        var uSetValueStr= "";
        if (userName == "客户全称") {
            uSql = "401290367151";
            uColumns = "客户简称#客户简称,客户全称#客户全称";
            uSetValueStr = "客户简称=客户简称,客户全称=客户全称";
        } else {
            uSql = "316015617541";
            uColumns = userName + "#" + userName;
            uSetValueStr= userName + "=" + userName;
        }
        var SelectRecordData1 = {
            InitializationStr: "#" + ResID + "_" + userName,
            key: "#" + ResID + "_" + userName,
            ResID: ResID,
            deafultValue: $("#" + ResID + "_" + userName).val(),
            keyWordValue: "LCXMBHCX",
            idField: userName,
            textField: userName,
            RecID: RecID,
            KeyWidth: 233,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 550,
            HasLastOperation: true,
            QueryKeyField: userName,
            UserDefinedSql: uSql,
            Columns:uColumns,
            SetValueStr: uSetValueStr,
            ROW_NUMBER_ORDER: ""
        }
        InitializationComboGrid(SelectRecordData1);
    }

    function LastOperation(rowData, SelectRecordData) {
        var khqc = rowData.客户简称;
        if (khqc != "undefined" && khqc != "" && khqc != null) {
            $("#<%=ResID %>_客户简称").val(khqc);
        }
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

    function fnParentSave() {
        $.ajax({
            type: "POST",
            url: encodeURI("../Common/Ajax_Request.aspx?typeValue=GetData&ResID=<%=ResID %>&Condition= and ID!='<%=RecID %>' and 项目编号='" + $("#<%=ResID %>_项目编号").val() + "'"),
            success: function (obj) {
                var jsonList = eval("(" + obj + ")");
                if (jsonList.total == "0") {
                    fnSave();
                } else {
                    alert("项目编号:" + $("#<%=ResID %>_项目编号").val() + "已存在！");
                    return;
                }
            }
        })
    }

    //保存方法
    function fnSave() {
        ChangeHTJE();
        changeXMML();
        changeYSK();
        changeSJJE();
        changeMYYBLJE();
        var check = $('#form1').form('validate');
        if (!check) return;
        if ($("#<%=ResID %>_实际金额").val() != "" && $("#<%=ResID %>_实际金额").val() != "0" && $("#<%=ResID %>_实际金额录入时间").val() == "") {
            $("#<%=ResID %>_实际金额录入时间").val("<%=Time %>")
        }
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

    function BindSJ() {
        $("#<%=ResID %>_会员积分").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_会员样本数量").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_外包成本").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_短信单价").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_短信数量").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_其他成本").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_邮件单价").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_邮件数量").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_兼职成本").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_API成本").on("keyup", function () { ChangeCBHJ(); changeXMML(); });
        $("#<%=ResID %>_样本单价").on("keyup", function () { ChangeHTJE(); changeXMML(); });
        $("#<%=ResID %>_合同样本量").on("keyup", function () { ChangeHTJE(); changeXMML(); });
        $("#<%=ResID %>_系统使用费").on("keyup", function () { ChangeHTJE(); changeXMML(); changeYSK(); });
        $("#<%=ResID %>_其他").on("keyup", function () { ChangeHTJE();  changeXMML(); changeYSK(); });

        //实际金额 = [实际样本单价] * [客户确认样本量] + [客户确认系统使用费] + [客户确认其他费用：其他资料]
        $("#<%=ResID %>_其他资料").on("keyup", function () {changeSJJE();});
        $("#<%=ResID %>_实际样本单价").on("keyup", function () { changeSJJE(); changeXMML(); });
        $("#<%=ResID %>_客户确认样本量").on("keyup", function () { changeSJJE(); changeMYYBLJE(); });
        $("#<%=ResID %>_客户确认系统使用费").on("keyup", function () { changeSJJE(); });

        $("#<%=ResID %>_完成样本量").on("keyup", function () { changeSJJE(); changeXMML(); });

        $("#<%=ResID %>_美元").on("keyup", function () { changeMYYBLJE();});
      

        $("#<%=ResID %>_项目毛利").attr("readonly", true);
        $("#<%=ResID %>_实际金额").attr("readonly", true);
        $("#<%=ResID %>_美元样本量金额").attr("readonly", true);
    }
    //美元样本量金额=[美元]*[客户确认样本量]
    function changeMYYBLJE() {
        var my = 0;
        if ($("#<%=ResID %>_美元").val() != "" && !isNaN($("#<%=ResID %>_美元").val())) {
            my = parseFloat($("#<%=ResID %>_美元").val());
        }
        var khqrybl = 0;
        if ($("#<%=ResID %>_客户确认样本量").val() != "" && !isNaN($("#<%=ResID %>_客户确认样本量").val())) {
            khqrybl = parseFloat($("#<%=ResID %>_客户确认样本量").val());
        }
        var myyblje = my * khqrybl;
        $("#<%=ResID %>_美元样本量金额").val(Math.round(myyblje * 100) / 100);
    }

    //应收款=[实际金额] - [已收款]
    function changeYSK() {
        var sjje = 0;
        if ($("#<%=ResID %>_实际金额").val() != "" && !isNaN($("#<%=ResID %>_实际金额").val())) {
            sjje = parseFloat($("#<%=ResID %>_实际金额").val());
        }
        var ysk = 0;
        if ($("#<%=ResID %>_已收款").val() != "" && !isNaN($("#<%=ResID %>_已收款").val())) {
            ysk = parseFloat($("#<%=ResID %>_已收款").val());
        }
        var yingsk = sjje - ysk;
        $("#<%=ResID %>_应收款").val(Math.round(yingsk * 100) / 100);
    }


    //项目毛利=[实际金额]-[成本合计]
    function changeXMML() {
        var sjje = 0;
        if ($("#<%=ResID %>_实际金额").val() != "" && !isNaN($("#<%=ResID %>_实际金额").val())) {
            sjje = parseFloat($("#<%=ResID %>_实际金额").val());
        }
        var cbhj = 0;
        if ($("#<%=ResID %>_成本合计").val() != "" && !isNaN($("#<%=ResID %>_成本合计").val())) {
            cbhj = parseFloat($("#<%=ResID %>_成本合计").val());
        }
        var xmml = sjje - cbhj;
        $("#<%=ResID %>_项目毛利").val(Math.round(xmml * 100) / 100);
    }

    //实际金额 = [实际样本单价] * [客户确认样本量] + [客户确认系统使用费] + [客户确认其他费用：其他资料]
    function changeSJJE(){
        var sjybdj = 0;
        if ($("#<%=ResID %>_实际样本单价").val() != "" && !isNaN($("#<%=ResID %>_实际样本单价").val())) {
            sjybdj = parseFloat($("#<%=ResID %>_实际样本单价").val());
        }
        var wcybl = 0;
        if ($("#<%=ResID %>_客户确认样本量").val() != "" && !isNaN($("#<%=ResID %>_客户确认样本量").val())) {
            wcybl = parseFloat($("#<%=ResID %>_客户确认样本量").val());
        }
        var xtsyf = 0;
        if ($("#<%=ResID %>_客户确认系统使用费").val() != "" && !isNaN($("#<%=ResID %>_客户确认系统使用费").val())) {
            xtsyf = parseFloat($("#<%=ResID %>_客户确认系统使用费").val());
        }
        var qt = 0;
        if ($("#<%=ResID %>_其他资料").val() != "" && !isNaN($("#<%=ResID %>_其他资料").val())) {
            qt = parseFloat($("#<%=ResID %>_其他资料").val());
        }
        var sjje = sjybdj * wcybl + xtsyf + qt;
        $("#<%=ResID %>_实际金额").val(Math.round(sjje * 100) / 100);
        changeYSK();
    }


    //合同金额=[样本单价]*[合同样本量]+[系统使用费]+[其他]
    function ChangeHTJE() {
        var ybdj = 0;
        if ($("#<%=ResID %>_样本单价").val() != "" && !isNaN($("#<%=ResID %>_样本单价").val())) {
            ybdj = parseFloat($("#<%=ResID %>_样本单价").val());
        }
        var htybl = 0;
        if ($("#<%=ResID %>_合同样本量").val() != "" && !isNaN($("#<%=ResID %>_合同样本量").val())) {
            htybl = parseFloat($("#<%=ResID %>_合同样本量").val());
        }
        var xtsyf = 0;
        if ($("#<%=ResID %>_系统使用费").val() != "" && !isNaN($("#<%=ResID %>_系统使用费").val())) {
            xtsyf = parseFloat($("#<%=ResID %>_系统使用费").val());
        }
        var qt = 0;
        if ($("#<%=ResID %>_其他").val() != "" && !isNaN($("#<%=ResID %>_其他").val())) {
            qt = parseFloat($("#<%=ResID %>_其他").val());
        }
        var htje = ybdj * htybl + xtsyf + qt;
        $("#<%=ResID %>_合同金额").val(Math.round(htje * 100) / 100);
    }

    function ChangeCBHJ() {
        //成本合计=[会员积分]*[会员样本数量]+[外包成本]+[短信单价]*[短信数量]+[其他成本]+[邮件单价]*[邮件数量]+[兼职成本]+[API成本]
        //hyjf*hyybsl+wbcb+dxdj+dxdj+dxsl+qtcb+yjdj*yjsl+jzcb+apicb
        var hyjf = 0;
        if ($("#<%=ResID %>_会员积分").val() != "" && !isNaN($("#<%=ResID %>_会员积分").val())) {
            hyjf = parseFloat($("#<%=ResID %>_会员积分").val());
        }
        
        var hyybsl = 0;
        if ($("#<%=ResID %>_会员样本数量").val() != "" && !isNaN($("#<%=ResID %>_会员样本数量").val())) {
            hyybsl = parseFloat($("#<%=ResID %>_会员样本数量").val());
        }
        var wbcb = 0;
        if ($("#<%=ResID %>_外包成本").val() != "" && !isNaN($("#<%=ResID %>_外包成本").val())) {
            wbcb = parseFloat($("#<%=ResID %>_外包成本").val());
        }
        var dxdj = 0;
        if ($("#<%=ResID %>_短信单价").val() != "" && !isNaN($("#<%=ResID %>_短信单价").val())) {
            dxdj = parseFloat($("#<%=ResID %>_短信单价").val());
        }
        var dxsl = 0;
        if ($("#<%=ResID %>_短信数量").val() != "" && !isNaN($("#<%=ResID %>_短信数量").val())) {
            dxsl = parseFloat($("#<%=ResID %>_短信数量").val());
        }
        var qtcb = 0;
        if ($("#<%=ResID %>_其他成本").val() != "" && !isNaN($("#<%=ResID %>_其他成本").val())) {
            qtcb = parseFloat($("#<%=ResID %>_其他成本").val());
        }
        var yjdj = 0;
        if ($("#<%=ResID %>_邮件单价").val() != "" && !isNaN($("#<%=ResID %>_邮件单价").val())) {
            yjdj = parseFloat($("#<%=ResID %>_邮件单价").val());
        }
        var yjsl = 0;
        if ($("#<%=ResID %>_邮件数量").val() != "" && !isNaN($("#<%=ResID %>_邮件数量").val())) {
            yjsl = parseFloat($("#<%=ResID %>_邮件数量").val());
        } 
        var jzcb = 0;
        if ($("#<%=ResID %>_兼职成本").val() != "" && !isNaN($("#<%=ResID %>_兼职成本").val())) {
            jzcb = parseFloat($("#<%=ResID %>_兼职成本").val());
        }
        var apicb = 0;
        if ($("#<%=ResID %>_API成本").val() != "" && !isNaN($("#<%=ResID %>_API成本").val())) {
            apicb = parseFloat($("#<%=ResID %>_API成本").val());
        }
        var cbhj = hyjf * hyybsl + wbcb + dxdj*dxsl + qtcb + yjdj * yjsl + jzcb + apicb;
        $("#<%=ResID %>_成本合计").val(Math.round(cbhj*100)/100);
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
            <div title="项目信息" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 3px; margin: 0px;width:98%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 130px;">督导</th>
                            <td style="width: 250px;">
                                <input id="<%=ResID %>_督导"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                    data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=pm' " />
                            </td>
                            <th style="width: 130px;">编程人员</th>
                            <td style="width: 250px;"><input id="<%=ResID %>_编程人员"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=pm' " />
                            </td>
                        </tr>
                        <tr>
                            <th>数据处理人</th>
                            <td>
                                <input id="<%=ResID %>_数据处理人"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=DP' " />
                            </td>
                            <th>销售</th>
                            <td>         
                                <input id="<%=ResID %>_销售"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=xs' " />
                            </td>
                        </tr>
                        <tr>
                            <th>项目编号</th>
                            <td >
                                <input id="<%=ResID %>_项目编号"  class="box3" style="width: 95%;" data-options="required:true"/>
                            </td>     
                            <th>调查主题</th>
                            <td >
                                <input id="<%=ResID %>_调查主题"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                    data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=dczt' " />
                            </td>                        
                        </tr>
                        <tr>
                            <th>项目名称</th>
                            <td>
                                <input id="<%=ResID %>_项目名称"  class="box3" style="width: 95%;" data-options="required:true" />
                            </td>
                            <th>调查城市</th>
                            <td>
                                <input id="<%=ResID %>_调查城市" class="box3" style="width: 95%;" />
                            </td>
                        </tr>   
                        <tr>
                            <th>客户简称</th>
                            <td>
                                <input id="<%=ResID %>_客户简称" class="box3" style="width: 95%;"  />
                            </td>
                            <th>客户全称</th>
                            <td>
                                <input id="<%=ResID %>_客户全称"  class="box3" style="width: 95%;" data-options="required:true" />
                            </td>
                        </tr>   
                        <tr>
                            <th>客户方PM联系人</th>
                            <td>
                                <input id="<%=ResID %>_客户方PM联系人" class="box3" style="width: 95%;" data-options="required:true" />
                            </td>
                            <th>录入日期</th>
                            <td>
                                <input id="<%=ResID %>_录入日期" readonly="readonly" value="<%=Time %>" class="box3" style="width: 95%;" data-options="required:true" />
                            </td>
                        </tr>   
                        <tr>
                            <th>项目类型</th>
                            <td>
                                <select id="<%=ResID %>_项目类型" class="easyui-combobox" name="dept" style="width: 95%;">  
                                <option value="国内">国内</option><option value="国外">国外</option></select>
                            </td>
                            <th>美元</th>
                            <td>
                                <input id="<%=ResID %>_美元"  class="box3" style="width: 95%;" />
                            </td>
                        </tr>   
                        <tr>
                            <th>样本单价</th>
                            <td>
                                <input id="<%=ResID %>_样本单价" class="box3" style="width: 95%;"  />
                            </td>
                            <th>合同样本量</th>
                            <td>
                                <input id="<%=ResID %>_合同样本量" class="box3" style="width: 95%;"  />
                            </td>
                        </tr>
                        <tr>
                            <th>系统使用费</th>
                            <td>
                                <input id="<%=ResID %>_系统使用费"  class="box3" style="width: 95%;" />
                            </td>
                            <th>其他</th>
                            <td>
                                <input id="<%=ResID %>_其他" class="box3" style="width: 95%;"  />
                            </td>
                        </tr><tr>
                            <th>合同金额</th>
                            <td>
                                <input id="<%=ResID %>_合同金额"  class="box3" style="width: 95%;" />
                            </td>
                            <th>状态</th>
                            <td>
                                <select id="<%=ResID %>_状态" class="easyui-combobox" name="zttype" style="width: 95%;">  
                                    <option value=""></option>
                                    <option value="坏账">坏账</option>
                                    <option value="抵账">抵账</option>
                                    <option value="未收完">未收完</option>
                                    <option value="已收款">已收款</option>
                                    <option value="已抵账">已抵账</option>
                                    <option value="已结清">已结清</option>
                                    <option value="已抵债">已抵债</option>
                                    <option value="已开票">已开票</option>
                                    <option value="已收款900">已收款900</option>
                                    <option value="已到账">已到账</option>
                                    <option value="不要发票">不要发票</option>
                                    <option value="现金付款已结清">现金付款已结清</option>
                                    <option value="已开">已开</option>
                                    <option value="现金已到账">现金已到账</option>
                                    <option value="重复">重复</option>
                                    <option value="作废">作废</option>
                                    <option value="合并">合并</option>
                                </select>
                            </td>
                        </tr>               
                    </table>
                </div>
            </div>
            <div title="项目执行情况" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 3px; margin: 0px;width:98%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table1">
                         <tr>
                            <th style="width:130px;">实际开始时间</th>
                            <td style="width:245px;">
                                <input id="<%=ResID %>_实际开始时间" class="easyui-datebox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 95%;" />
                            </td>
                            <th style="width:130px;">实际结束时间</th>
                            <td style="width:250px;">
                                <input id="<%=ResID %>_实际结束时间" class="easyui-datebox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" style="width: 95%;" />
                            </td>
                        </tr>
                        <tr>
                            <th>实际样本单价</th>
                            <td>
                            <input id="<%=ResID %>_实际样本单价" class="box3" style="width: 95%;" /></td>
                            <th>客户确认样本量</th>
                            <td>
                            <input id="<%=ResID %>_客户确认样本量" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>客户确认系统使用费</th>
                            <td>
                            <input id="<%=ResID %>_客户确认系统使用费" class="box3" style="width: 95%;" /></td>
                            <th>客户确认其他费用</th>
                            <td>
                            <input id="<%=ResID %>_其他资料" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>实际金额</th>
                            <td>
                            <input id="<%=ResID %>_实际金额" class="box3" style="width: 95%;" /></td>
                            <th>实际金额录入时间</th>
                            <td>
                            <input id="<%=ResID %>_实际金额录入时间" readonly="readonly" class="box3" style="width: 95%;" /></td>
                        </tr> <tr>
                        <th>美元样本量金额</th>
                            <td colspan="3">
                            <input id="<%=ResID %>_美元样本量金额" class="box3" style="width: 98%;" /></td></tr>
                        <tr>

                            <th>亚太进价</th>
                            <td>
                            <input id="<%=ResID %>_亚太进价" class="box3" style="width: 95%;" /></td>
                            <th>亚太出价</th>
                            <td>
                            <input id="<%=ResID %>_亚太出价" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>亚太外包名称</th>
                            <td>
                            <input id="<%=ResID %>_亚太外包名称" class="box3" style="width: 95%;" /></td>
                            <th>亚太样本地区</th>
                            <td>
                            <input id="<%=ResID %>_亚太样本地区" class="box3" style="width: 95%;" /></td>
                        </tr>
                      <tr>
                            <th>备注</th>
                            <td colspan="3">
                                <input class="box3" id="<%=ResID %>_备注" style="width: 98%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                        <tr>
                            <th>会员积分</th>
                            <td>
                            <input id="<%=ResID %>_会员积分"   class="box3" style="width: 95%;" /></td>
                            <th>会员样本数量</th>
                            <td>
                            <input id="<%=ResID %>_会员样本数量" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>外包样本量</th>
                            <td>
                            <input id="<%=ResID %>_外包样本量" class="box3" style="width: 95%;" /></td>
                            <th>外包成本</th>
                            <td>
                            <input id="<%=ResID %>_外包成本" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>短信单价</th>
                            <td>
                            <input id="<%=ResID %>_短信单价" value="0.06" class="box3" style="width: 95%;" /></td>
                            <th>短信数量</th>
                            <td>
                            <input id="<%=ResID %>_短信数量" value="0" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>邮件单价</th>
                            <td>
                            <input id="<%=ResID %>_邮件单价" value="0.01" class="box3" style="width: 95%;" /></td>
                            <th>邮件数量</th>
                            <td>
                            <input id="<%=ResID %>_邮件数量" value="0"  class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>兼职成本</th>
                            <td>
                            <input id="<%=ResID %>_兼职成本" class="box3" style="width: 95%;" /></td>
                            <th>其他成本</th>
                            <td>
                            <input id="<%=ResID %>_其他成本" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>API成本</th>
                            <td>
                            <input id="<%=ResID %>_API成本" class="box3" style="width: 95%;" /></td>
                            <th>API资源完成量</th>
                            <td>
                            <input id="<%=ResID %>_API资源完成量" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>成本合计</th>
                            <td>
                            <input id="<%=ResID %>_成本合计" readonly="readonly" class="box3" style="width: 95%;" /></td>
                            <th>项目毛利</th>
                            <td>
                            <input id="<%=ResID %>_项目毛利" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>开票时间</th>
                            <td>
                            <input id="<%=ResID %>_开票时间" readonly="readonly"   class="box3"  style="width: 95%;" /></td>
                            <th>销售提成</th>
                            <td>
                            <input id="<%=ResID %>_销售提成"   onfocus="WdatePicker({dateFmt:'yyyy-MM'})" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>提成系数</th>
                            <td>
                            <input id="<%=ResID %>_提成系数" class="box3" style="width: 95%;" /></td>
                            <th>已收款</th>
                            <td>
                            <input id="<%=ResID %>_已收款" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>已开票金额</th>
                            <td>
                            <input id="<%=ResID %>_已开票金额" class="box3" style="width: 95%;" /></td>
                            <th>应收款</th>
                            <td>
                            <input id="<%=ResID %>_应收款" readonly="readonly" class="box3" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>收款时间</th>
                            <td  >
                            <input id="<%=ResID %>_收款时间" readonly="readonly"   class="box3"    style="width:  95%;" /></td>
                            <th>日期差</th>
                            <td  >
                            <input id="<%=ResID %>_日期差" readonly="readonly"  class="box3"  style="width:  95%;" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
