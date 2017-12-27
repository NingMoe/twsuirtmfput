<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditGYSXX.aspx.cs" Inherits="Base_Office_AddorEditGYSXX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>供应商管理</title>
    <%=this.GetScript1_4_3   %>
    <script type="text/javascript" language="javascript">
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
            } else {
                if ("<%=GYSBH %>"!="") {
                    $("#<%=ResID %>_供应商编号").textbox("setValue", "<%=GYSBH %>");
                    $("#<%=ResID %>_供应商名称").textbox("setValue", "<%=GYSMC %>");
                    $("#<%=ResID %>_收款账户").textbox("setValue", "<%=SKZH %>");
                    $("#<%=ResID %>_供应商编号").textbox('textbox').attr("readonly", true);
                    $("#<%=ResID %>_供应商名称").textbox('textbox').attr("readonly", true);
                    $("#<%=ResID %>_收款账户").textbox('textbox').attr("readonly", true);
                }
            }
            SetBackgroundColor();
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
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="btnParentSave"
                        src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnParentSave(); return false;" /></a>
                        &nbsp;<a style="border: 0px;" href="#" onclick="window.parent.closeWindow1();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="供应商管理" class="easyui-panel" collapsible="false" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 13%">负责管理部门</th>
                            <td style="width: 20%">
                                 <input id="<%=ResID %>_负责管理部门"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                    data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=fzglbm' " />
                            </td>
                            <th style="width: 13%">供应商名称</th>
                            <td style="width: 20%">
                            <input id="<%=ResID %>_供应商名称" type="text" class="easyui-textbox" style="width: 95%;" /></td>
                        </tr>
                        <tr>
                             <th>主要服务类型</th>
                            <td>
                                <input id="<%=ResID %>_主要服务类型"  class="easyui-combobox"  style="width: 95%;" panelHeight="180px"; editable="false"
                                    data-options="method:'get',valueField:'id',textField:'text',url: '../Common/CommonAjax_Request.aspx?typeValue=GetDataDictionary&keyCode=zyfwlx' " />
                            </td>
                            <th>其他服务内容</th>
                            <td>
                                <input id="<%=ResID %>_其他服务内容" style="width: 95%;" type="text" class="easyui-textbox" /></td>
                        </tr>
                        <tr>
                            <th>供应商编号</th>
                            <td><input id="<%=ResID %>_供应商编号" type="text" class="easyui-textbox"  style="width: 95%;" /></td>                           
                            <th>收款账户</th>
                            <td><input id="<%=ResID %>_收款账户" type="text" class="easyui-textbox"  style="width: 95%;"/></td>
                        </tr>
                        <tr>
                            <th>登记人</th>
                            <td>
                                <input id="<%=ResID %>_登记人" type="text" class="easyui-textbox"  style="width: 95%;" value="<%=UserName %>" /></td>
                            <th></th>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
