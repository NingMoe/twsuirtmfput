<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddOrEditTransportation.aspx.cs" Inherits="Base_SolidWaste_AddOrEditTransportation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>单位信息</title>
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
        <div title="单位信息" class="easyui-panel" collapsible="false" style="overflow: hidden; padding: 3px; margin: 0px;width:100%;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr>
                            <th style="width: 130px">运输单位</th>
                            <td colspan="3" style="width:660px"><input id="<%=ResID %>_运输单位" type="text" class="easyui-textbox" style="width:582px" /> </td>
                        </tr>
                        <tr>
                            <th style="width: 130px">牌照</th>
                            <td style="width: 260px"><input id="<%=ResID %>_牌照" type="text" class="easyui-textbox" style="width:200px" /> </td>
                            <th style="width: 110px">运输人员</th>
                            <td style="width: 260px"><input id="<%=ResID %>_运输人员" type="text" class="easyui-textbox" style="width:200px" /></td>
                        </tr>
                        <tr>
                            <th style="width: 130px">联系人</th>
                            <td style="width: 260px"><input id="<%=ResID %>_联系人" type="text" class="easyui-textbox" style="width:200px" /></td>
                            <th style="width: 110px">联系方式</th>
                            <td style="width: 260px"><input id="<%=ResID %>_联系方式" type="text" class="easyui-numberbox" style="width:200px" /> </td>
                        </tr>
                        <tr>
                            <th style="width: 130px">地址</th>
                            <td colspan="3" style="width:660px"><input id="<%=ResID %>_地址" type="text" class="easyui-textbox" style="width:582px" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
