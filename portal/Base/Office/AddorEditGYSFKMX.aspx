<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditGYSFKMX.aspx.cs" Inherits="Base_Office_AddorEditGYSFKMX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>供应商付款明细</title>
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
            <div title="供应商付款明细" class="easyui-panel" collapsible="false" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 17%">供应商编号</th>
                            <td style="width: 20%">
                                <input id="<%=ResID %>_供应商编号" style="width: 95%;" type="text" class="easyui-textbox" />
                            </td>
                            <th style="width: 17%">供应商名称</th>
                            <td style="width: 20%">
                             <input id="<%=ResID %>_供应商名称" style="width: 95%;" type="text" class="easyui-textbox" /></td>
                        </tr>
                        <tr>
                            <th>付款申请人</th>
                            <td><input id="<%=ResID %>_付款申请人" value="<%=UserName %>"  style="width: 95%;" type="text" class="easyui-textbox" />
                            </td>
                            <th>付款申请时间</th>
                            <td><input id="<%=ResID %>_付款申请时间" class="easyui-datebox"  style="width: 95%;"
                             onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" value="<%=Time%>"  /></td>
                        </tr>
                        <tr>
                            <th>付款金额</th>
                            <td><input id="<%=ResID %>_付款金额" type="text" class="easyui-textbox"  style="width: 95%;" /></td>                           
                            <th>付款方式</th>
                            <td>
                              <select id="<%=ResID %>_付款方式" class="easyui-combobox"  style="width: 95%;">  
                                <option value="">&nbsp;</option><option value="请款">请款</option>
                                <option value="报销">报销</option><option value="备用金现金">备用金现金</option></select>
                            </td>
                        </tr>
                        <tr>
                            <th>收款账户</th>
                            <td><input id="<%=ResID %>_收款账户" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                            <th>付款说明</th>
                            <td><input id="<%=ResID %>_付款说明" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>外包项目编号</th>
                            <td><input id="<%=ResID %>_外包项目编号" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                            <th>供应商付款明细表编号</th>
                            <td><input id="<%=ResID %>_供应商付款明细表编号" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                        </tr>
                        <tr>
                            <th>外包列表金额自动合计</th>
                            <td><input id="<%=ResID %>_外包列表金额自动合计" type="text" class="easyui-textbox"  style="width: 95%;" /></td>
                            <th>&nbsp;</th>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
