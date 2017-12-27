<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddOrEditReport.aspx.cs" Inherits="Base_SolidWaste_AddOrEditReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>统计报表</title>
    <%=this.GetScript1_4_3%>
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
        <div title="统计报表" class="easyui-panel" collapsible="false" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 13%">联单编号</th>
                            <td style="width: 20%"><input id="<%=ResID %>_联单编号" type="text" class="easyui-numberbox" style="width: 95%;" /></td>
                            <th style="width: 13%">废物产生单位</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物产生单位" type="text" class="easyui-textbox" style="width: 95%;" /> </td>
                        </tr>
                        <tr>
                            <th style="width: 13%">废物代码</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物代码" type="text" class="easyui-textbox" style="width: 95%;"  /></td>
                            <th style="width: 13%">废物名称</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物名称" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">废物运输单位</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物运输单位" type="text" class="easyui-textbox" style="width: 95%;" value="上海杨涵物流有限公司" /></td>
                            <th style="width: 13%">运输工具牌照号</th>
                            <td style="width: 20%"><input id="<%=ResID %>_运输工具牌照号" type="text" class="easyui-textbox" style="width: 95%;" value="沪D-A5609" /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">废物形态</th>
                            <td style="width: 20%">
                                <select id="<%=ResID %>_废物形态" class="easyui-combobox" style="width: 95%;" data-options="panelHeight:'auto'">
                                    <option value="固态">固态</option>
                                    <option value="液态">液态</option>
                                </select>
                            </td>
                            <th style="width: 13%">包装方式</th>
                            <td style="width: 20%">
                                <select id="<%=ResID %>_包装方式" class="easyui-combobox" style="width: 95%;" data-options="panelHeight:'auto'">
                                    <option value="箱装">箱装</option>
                                    <option value="袋装">袋装</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 13%">入库件数</th>
                            <td style="width: 20%"><input id="<%=ResID %>_入库件数" type="text" class="easyui-numberbox" style="width: 95%;" /></td>
                            <th style="width: 13%">废物处理方式</th>
                            <td style="width: 20%">
                                <select id="<%=ResID %>_废物处理方式" class="easyui-combobox" style="width: 95%;" data-options="panelHeight:'auto'">
                                    <option value="焚烧">焚烧</option>
                                    <option value="收集">收集</option>
                                    <option value="物化">物化</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 13%">转移日期</th>
                            <td style="width: 20%"><input id="<%=ResID %>_转移日期" type="text" class="easyui-datebox" style="width: 95%;" /></td>
                            <th style="width: 13%">废物的转移量(吨)</th>
                            <td style="width: 20%"><input id="<%=ResID %>_废物的转移量" type="text" class="easyui-numberbox" style="width: 95%;" data-options="precision:2" /></td>
                        </tr>
                        <tr>
                            <th style="width: 13%">录入时间</th>
                            <td style="width: 20%"><input id="<%=ResID %>_录入时间" type="text" class="easyui-textbox" style="width: 95%;" value="<%=Time %>" /></td>
                            <th style="width: 13%">录入人员</th>
                            <td style="width: 20%"><input id="<%=ResID %>_录入人员" type="text" class="easyui-textbox" style="width: 95%;" value="<%=UserName %>" /></td>
                        </tr>
                    </table>
                </div>
            </div>
    </div>
    </form>
</body>
</html>
