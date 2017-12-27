<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditXMLB.aspx.cs" Inherits="Base_Project_AddorEditXMLB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
<title>项目列表维护界面</title>
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
                            if (key == "本次开票金额" || key == "开票时间") {
                                $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                            } else {
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
                }
            });
        } else {
            if ("<%=XMBH %>" != "") {
                $("#<%=ResID %>_项目编号").textbox("setValue", "<%=XMBH %>");
                $("#<%=ResID %>_销售").textbox("setValue", "<%=XS %>");
                $("#<%=ResID %>_项目名称").textbox("setValue", "<%=XMMC %>");
                $("#<%=ResID %>_客户全称").textbox("setValue", "<%=KHQC %>");
                $("#<%=ResID %>_客户简称").textbox("setValue", "<%=KHJC %>");
                $("#<%=ResID %>_已开票金额").textbox("setValue", "<%=YKPJE %>");
                $("#<%=ResID %>_已开票金额").attr("readonly", true);
                $("#<%=ResID %>_项目编号").attr("readonly", true);
                $("#<%=ResID %>_销售").textbox('textbox').attr("readonly", true);
                $("#<%=ResID %>_项目名称").textbox('textbox').attr("readonly", true);
                $("#<%=ResID %>_客户全称").textbox('textbox').attr("readonly", true);
                $("#<%=ResID %>_客户简称").textbox('textbox').attr("readonly", true);
            }
            if ("<%=FPSQDH %>" != "") {
                $("#<%=ResID %>_发票申请单号").textbox("setValue", "<%=FPSQDH %>");
                $("#<%=ResID %>_发票申请单号").textbox('textbox').attr("readonly", true);
            }
        }
        LoadCus("<%=ResID %>", "<%=RecID %>");
        SetBackgroundColor();
    });

    function LoadCus(ResID, RecID) {
        uSql = "285435593984";
        uColumns = "项目编号#项目编号,项目名称#项目名称,销售#销售,客户简称#客户简称,客户全称#客户全称,实际金额#实际金额,应收款#应收款,已收款#已收款,已开票金额#已开票金额";
        uSetValueStr = "项目编号=项目编号,项目名称=项目名称,销售=销售,客户简称=客户简称,客户全称=客户全称,实际金额=实际金额,应收款=应收款,已收款=已收款,已开票金额=已开票金额";
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
            panelWidth: 550,
            HasLastOperation: true,
            QueryKeyField: "项目编号,项目名称,客户全称,客户简称",
            UserDefinedSql: uSql,
            Columns:uColumns,
            SetValueStr: uSetValueStr,
            ROW_NUMBER_ORDER: ""
        }
        InitializationComboGrid(SelectRecordData1);
    }

    function LastOperation(rowData, SelectRecordData) {
        var xmbh = rowData.项目编号;
        var xhbh = rowData.项目名称;
        var xs = rowData.销售;
        var khjc = rowData.客户简称;
        var khqc = rowData.客户全称;
        var sjje = rowData.实际金额;
        var ysk = rowData.应收款;
        var yskx = rowData.已收款;
        var ykpje = rowData.已开票金额;
        if (xmbh != "undefined" && xmbh != "" && xmbh != null) {
            $("#<%=ResID %>_项目编号").textbox("setValue", xmbh)
        }
        if (xhbh != "undefined" && xhbh != "" && xhbh != null) {
            $("#<%=ResID %>_项目名称").textbox("setValue", xhbh)
        }
        if (xs != "undefined" && xs != "" && xs != null) {
            $("#<%=ResID %>_销售").textbox("setValue", xs)
        }
        if (khjc != "undefined" && khjc != "" && khjc != null) {
            $("#<%=ResID %>_客户简称").textbox("setValue", khjc)
        }
        if (khqc != "undefined" && khqc != "" && khqc != null) {
            $("#<%=ResID %>_客户全称").textbox("setValue", khqc)
        }
        if (sjje != "undefined" && sjje != "" && sjje != null) {
            $("#<%=ResID %>_实际金额").textbox("setValue", sjje)
        }
        if (ysk != "undefined" && ysk != "" && ysk != null) {
            $("#<%=ResID %>_应收款").textbox("setValue", ysk)
        }
        if (yskx != "undefined" && yskx != "" && yskx != null) {
            $("#<%=ResID %>_已收款").textbox("setValue", yskx)
        }
        if (yskx != "undefined" && yskx != "" && yskx != null) {
            $("#<%=ResID %>_已收款").textbox("setValue", yskx)
        } 
        if (ykpje != "undefined" && ykpje != "" && ykpje != null) {
            $("#<%=ResID %>_已开票金额").textbox("setValue", ykpje)
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

    function changeKPSJ() {
        $("#<%=ResID %>_开票时间").val("<%=Time %>")
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
            <div title="项目信息" class="easyui-panel" collapsible="true" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 130px;">销售</th>
                            <td style="width: 250px;">
                             <input id="<%=ResID %>_销售"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px";
                                    data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=xs' " />
                            </td>
                            <th style="width: 130px;">项目编号</th>
                            <td style="width: 250px;">
                                <input id="<%=ResID %>_项目编号"  class="easyui-textbox" style="width: 95%;" /></td>                            
                        </tr>
                        <tr>
                            <th>项目名称</th>
                            <td>
                                <input id="<%=ResID %>_项目名称"  class="easyui-textbox" style="width: 95%;"  />
                            </td>
                            <th>客户简称</th>
                            <td><input id="<%=ResID %>_客户简称"  class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>客户全称</th>
                            <td  colspan="3">
                                <input id="<%=ResID %>_客户全称"  class="easyui-textbox" style="width: 95%;"/>
                            </td>     
                        </tr>
                        <tr><th>开票状态</th>
                            <td >
                               <select id="<%=ResID %>_开票状态"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="已开票">已开票</option>
                                <option value="作废">作废</option>
                                </select>
                            </td>
                                <th>实际金额</th>
                            <td >
                                <input id="<%=ResID %>_实际金额" class="easyui-textbox" style="width: 95%;"/>
                            </td>                  
                        </tr>
                        <tr>
                            <th>应收款</th>
                            <td>
                                <input id="<%=ResID %>_应收款"  class="easyui-textbox" style="width: 95%;"  />
                            </td>
                            <th>已收款</th>
                            <td>
                                <input id="<%=ResID %>_已收款" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr> 
                        <tr>
                            <th>本次开票申请金额</th>
                            <td>
                                <input id="<%=ResID %>_本次开票申请金额"  class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>已开票金额</th>
                            <td>
                                <input id="<%=ResID %>_已开票金额"  readonly="readonly" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr> 
                        <tr>
                            <th>本次开票金额</th>
                            <td>
                                <input id="<%=ResID %>_本次开票金额" class="box3" onkeyup="changeKPSJ()" style="width: 95%;" />
                            </td>
                            <th>开票时间</th>
                            <td>
                                <input id="<%=ResID %>_开票时间"  class="box3" style="width: 95%;" />
                            </td>
                        </tr>   
                        <tr>
                            <th>发票申请单号</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_发票申请单号" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr>   
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
