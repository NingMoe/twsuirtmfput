﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddOrEditOutsource.aspx.cs" Inherits="Base_SolidWaste_AddOrEditOutsource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>委外信息</title>
    <%=this.GetScript1_4_3   %>
    <script type="text/javascript">
        $(document).ready(function () {
            if ('<%=RecID %>' != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=<%=RecID %>",
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
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
            LoadWasteCode("<%=ResID%>");
            LoadTrans("<%=ResID%>");
            LoadDriver("<%=ResID%>"); 
        });
      
        //保存方法
        function fnParentSave() {
            //验证非空
            var check = $('#form1').form('validate');
            if (!check) return;
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

        function LoadWasteCode(ResID) {
            var SelectRecordData2 = {
                InitializationStr: "#" + ResID + "_废物代码",
                key: "#" + ResID + "_废物代码",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_废物代码").val(),
                keyWordValue: "SW_HazardousWasteCategory",
                idField: "废物代码",
                textField: "废物代码",
                KeyWidth: 218,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 350,
                HasLastOperation: true,
                QueryKeyField: "废物代码,危险废物",
                UserDefinedSql: "564794806054",
                Columns: "废物代码#废物代码,废物名称#危险废物",
                SetValueStr: "废物代码=废物代码,废物名称=危险废物",
                ROW_NUMBER_ORDER: ""
            }
            InitializationComboGrid(SelectRecordData2);
        }


        function LoadTrans(ResID) {
            var SelectRecordData3 = {
                InitializationStr: "#" + ResID + "_运输工具牌照号",
                key: "#" + ResID + "_运输工具牌照号",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_运输工具牌照号").val(),
                keyWordValue: "SW_Transportation",
                idField: "牌照",
                textField: "运输工具牌照号",
                KeyWidth: 220,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 350,
                HasLastOperation: true,
                QueryKeyField: "牌照",
                UserDefinedSql: "564795063461",
                Columns: "运输工具牌照号#牌照,运输人员#运输人员",
                SetValueStr: "运输工具牌照号=运输工具牌照号",
                ROW_NUMBER_ORDER: ""
            }
            InitializationComboGrid(SelectRecordData3);
        }

        function LoadDriver(ResID) {
            var SelectRecordData4 = {
                InitializationStr: "#" + ResID + "_运输人员",
                key: "#" + ResID + "_运输人员",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_运输人员").val(),
                keyWordValue: "SW_Transportation",
                idField: "运输人员",
                textField: "运输人员",
                KeyWidth: 220,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 350,
                HasLastOperation: false,
                QueryKeyField: "运输人员",
                UserDefinedSql: "564795063461",
                Columns: "运输工具牌照号#牌照,运输人员#运输人员",
                SetValueStr: "运输工具牌照号=牌照,运输人员=运输人员",
                ROW_NUMBER_ORDER: ""
            }
            InitializationComboGrid(SelectRecordData4);
        }

        function LastOperation(rowData, SelectRecordData2) {
            var WasteCode = rowData.废物代码;
            var WasteName = rowData.危险废物;
            var Driver = rowData.运输人员;
            if (WasteCode != "undefined" && WasteCode != "" && WasteCode != null) {
                $("#<%=ResID %>_废物代码").textbox("setValue", WasteCode)
            }
            if (WasteName != "undefined" && WasteName != "" && WasteName != null) {
                $("#<%=ResID %>_废物名称").textbox("setValue", WasteName)
            }
            if (Driver != "undefined" && Driver != "" && Driver != null) {
                $("#<%=ResID %>_运输人员").textbox("setValue", Driver)
            }
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="con" id="div<%=ResID %>FormTable" style="overflow: hidden; position: relative; height: 100%; border: none">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
            <tr>
                <td colspan="8" valign="middle">
                    &nbsp;<a style="border: 0px;" href="#">
                        <input type="image" id="btnParentSave" src="../../images/bar_save.gif" style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnParentSave(); return false;" /></a>
                    &nbsp;<a style="border: 0px;" href="#" onclick="window.parent.closeWindow1();">
                        <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;" onclick="return false;" /></a>
                </td>
            </tr>
        </table>
        <div title="委外信息" class="easyui-panel" collapsible="false" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 13%">联单编号</th>
                            <td style="width: 20%"><input id="<%=ResID %>_联单编号" type="text" class="easyui-numberbox" style="width: 95%;" /></td>
                            <th style="width: 13%">产生车间</th>
                            <td style="width: 20%"><input id="<%=ResID %>_处置车间" type="text" class="easyui-textbox" style="width: 95%;" /> </td>
                        </tr>
                        <tr>
                            <th style="width: 13%">废物名称</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物名称" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th style="width: 13%">废物代码</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物代码" type="text" style="width: 95%;"  /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">废物形态</th>
                            <td style="width: 20%">
                                <select id="<%=ResID %>_废物形态" class="easyui-combobox" style="width: 95%;" data-options="panelHeight:'auto'">
                                    <option value="固态">固态</option>
                                    <option value="液态">液态</option>
                                </select>
                            </td>
                            <th style="width: 13%">包装形式</th>
                            <td style="width: 20%">
                                <select id="<%=ResID %>_包装方式" class="easyui-combobox" style="width: 95%;" data-options="panelHeight:'auto'">
                                    <option value="箱装">箱装</option>
                                    <option value="袋装">袋装</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 13%">运输单位</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物运输单位" type="text" class="easyui-textbox" style="width: 95%;" value="上海杨涵物流有限公司" /></td>
                            <th style="width: 13%">运输车牌号</th>
                            <td style="width: 20%">
                                <input id="<%=ResID %>_运输工具牌照号" type="text"  style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">运输人员</th>
                            <td style="width: 20%"><input id="<%=ResID %>_运输人员" type="text" style="width: 95%;" /></td>
                            <th style="width: 13%">经办人</th>
                            <td style="width: 20%"><input id="<%=ResID %>_录入人员" type="text" class="easyui-textbox" style="width: 95%;" value="<%=UserName %>" /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">接收单位</th>
                            <td style="width: 20%"><input id="<%=ResID %>_接收单位" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                            <th style="width: 13%">委外处置日期</th>
                            <td style="width: 20%"><input id="<%=ResID %>_委外处置日期" type="text" class="easyui-textbox" style="width: 95%;" value="<%=Time %>" /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">数量(吨)</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物的转移量" type="text" class="easyui-numberbox" style="width: 95%;" data-options="precision:2" /></td>
                            <th style="width: 13%">处理方式</th>
                            <td style="width: 20%">
                                <select id="<%=ResID %>_废物处理方式" class="easyui-combobox" style="width: 95%;" data-options="panelHeight:'auto'">
                                    <option value="焚烧">焚烧</option>
                                    <option value="收集">收集</option>
                                    <option value="物化">物化</option>
                                    <option value="填埋">填埋</option>
                                </select>
                             </td>
                        </tr>
                        <input id="<%=ResID %>_是否委外" type="hidden" value="是" />
                    </table>
                </div>
            </div>
         </div>
    </form>
</body>
</html>
