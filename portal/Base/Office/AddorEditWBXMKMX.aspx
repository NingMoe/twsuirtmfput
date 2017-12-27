<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditWBXMKMX.aspx.cs" Inherits="Base_Office_AddorEditWBXMKMX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>外包项目款明细</title>
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
                if ("<%=GYSFKBH %>" != "") {
                    $("#<%=ResID %>_供应商付款明细表编号").textbox("setValue", "<%=GYSFKBH %>");
                    $("#<%=ResID %>_付款申请时间").textbox("setValue", "<%=FKSQSJ %>");
                    $("#<%=ResID %>_付款金额").textbox("setValue", "<%=FKJE %>");
                    $("#<%=ResID %>_供应商付款明细表编号").textbox("textbox").attr("readonly", true);
                    $("#<%=ResID %>_付款申请时间").textbox("textbox").attr("readonly", true);
                    $("#<%=ResID %>_付款金额").textbox("textbox").attr("readonly", true);
                }
            }
            SetBackgroundColor();
            LoadCust("<%=ResID %>", "<%=RecID %>");
            LoadCust1("<%=ResID %>", "<%=RecID %>");
        });

        function LoadCust(ResID, RecID) {
            var SelectRecordData1 = {
                InitializationStr: "#" + ResID + "_外包项目编号",
                key: "#" + ResID + "_外包项目编号",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_外包项目编号").val(),
                keyWordValue: "LCXMBHCX",
                idField: "外包项目编号",
                textField: "外包项目编号",
                RecID: RecID,
                KeyWidth: 250,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 500,
                HasLastOperation: true,
                QueryKeyField: "外包项目编号,外包项目金额,项目编号",
                UserDefinedSql: "382788102093",
                Columns: "外包项目编号#外包项目编号,外包项目金额#外包项目金额,项目编号#项目编号",
                SetValueStr: "外包项目编号=外包项目编号,外包项目金额=外包项目金额,项目编号=项目编号",
                ROW_NUMBER_ORDER: "order by 外包项目编号 DESC "
            }
            InitializationComboGrid(SelectRecordData1);
        }

        function LoadCust1(ResID, RecID) {
            var SelectRecordData1 = {
                InitializationStr: "#" + ResID + "_项目编号",
                key: "#" + ResID + "_项目编号",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_项目编号").val(),
                keyWordValue: "LCXMBHCX",
                idField: "项目编号",
                textField: "项目编号",
                RecID: RecID,
                KeyWidth: 250,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 500,
                HasLastOperation: true,
                QueryKeyField: "项目编号,项目名称,客户简称,客户全称,实际金额",
                UserDefinedSql:"285435593984",
                Columns: "项目编号#项目编号,实际金额#实际金额",
                SetValueStr: "项目编号=项目编号,实际金额=实际金额",
                ROW_NUMBER_ORDER: ""
            } 
            InitializationComboGrid(SelectRecordData1);
        }

        function LastOperation(rowData, SelectRecordData) {
            var wbxmje = rowData.外包项目金额
            var xmbh = rowData.项目编号
            var sjje = rowData.实际金额
            if (wbxmje != "undefined" && wbxmje != "" && wbxmje != null) {
                $("#<%=ResID %>_外包项目金额").textbox("setValue", wbxmje)
            }
            if (xmbh != "undefined" && xmbh != "" && xmbh != null) {
                $("#<%=ResID %>_项目编号").textbox("setValue", xmbh)
            }
            if (sjje != "undefined" && sjje != "" && sjje != null) {
                $("#<%=ResID %>_实际金额").textbox("setValue", sjje)
            }
        }




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
            <div title="外包项目款明细" class="easyui-panel" collapsible="false" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 17%">供应商付款明细表编号</th>
                            <td style="width: 20%">
                                <input id="<%=ResID %>_供应商付款明细表编号" style="width: 95%;" type="text" class="easyui-textbox" />
                            </td>
                             <th style="width: 17%">&nbsp;</th>
                            <td style="width: 20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <th>付款金额</th>
                            <td><input id="<%=ResID %>_付款金额" style="width: 95%;" type="text" class="easyui-textbox" />
                            </td>
                             <th >付款申请时间</th>
                             <td ><input id="<%=ResID %>_付款申请时间" class="easyui-datebox"  style="width: 95%;"
                             onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" /></td>
                         </tr>
                        <tr>
                             <th>外包项目编号</th>
                            <td><input id="<%=ResID %>_外包项目编号" class="easyui-textbox"  style="width: 95%;" /></td>
                            <th>外包项目金额</th>
                            <td><input id="<%=ResID %>_外包项目金额" type="text" class="easyui-textbox"  style="width: 95%;" /></td>                           
                        </tr>
                        <tr>
                            <th>项目编号</th>
                            <td><input id="<%=ResID %>_项目编号"  class="easyui-textbox"  style="width: 95%;" /></td>
                            <th>实际金额</th>
                            <td><input id="<%=ResID %>_实际金额" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                          
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
