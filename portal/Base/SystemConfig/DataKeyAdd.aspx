<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataKeyAdd.aspx.cs" Inherits="Base_SystemConfig_DataKeyAdd" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>数据字典添加</title>
    <%=this.GetScript1_4_3   %>
    <script type="text/javascript">
        $(document).ready(function () {

            if ('<%=RecID %>' != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=<%=RecID %>",
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
                        var zhmc = "";
                        //子项
                        if ('<%=KeyCode%>' == "") {
                            $("#ParentId").show();
                            $("#KeyValue").show();
                            $("#KeyCode").hide();
                        } else {
                            $("#ParentId").hide();
                            $("#KeyValue").hide();
                            $("#KeyCode").show();
                        }
                        for (var i = 0; i < jsonList.length; i++) {
                            for (var key in jsonList[i]) {
                                if (key != "数据字典排序" && key != "ID" && key != "ResID" && key != "创建日期" && key != "创建人") {                                
                                    $("#<%=ResID %>_" + key).textbox("setValue", jsonList[i][key]);
                                } else if (key == "数据字典排序") {
                                    $("#<%=ResID %>_" + key).numberspinner("setValue", jsonList[i][key]); 
                                }
                            }
                        }
                    }
                });
            }
            else {
                if ('<%=ParentKey%>' != "") {
                    $("#ParentId").show();
                    $("#KeyValue").show();
                    $("#KeyCode").hide();
                    $("#<%=ResID %>_父级ID").textbox("setValue", "<%=ParentKey%>");
                } else {
                    $("#ParentId").hide();
                    $("#KeyValue").hide();
                    $("#KeyCode").show();
                }
            }

        });
        function submitForm() {
            var code = $("#<%=ResID %>_数据字典编号").textbox("getText");
            $.ajax({
                type: "POST",
                dataType: "json",
                data: { "Json": "" },
                url: "Ajax_Request.aspx?typeValue=ValidateCode&KeyCode=" + code + "&ID=" + <%=RecID%> + "",
                success: function (obj) {
                    if (!obj.success || obj.success == "false") {
                        $.messager.alert('提示', '该字典编号已存在！', 'info');
                        return;
                    } else {
                        var jsonStr1 = "[{";
                        jsonStr1 += GetFromJson("<%=ResID %>");
                        jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
                        jsonStr1 += "}]";

                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            data: { "Json": "" + jsonStr1 + "" },
                            url: "Ajax_Request.aspx?typeValue=KeyDataSave&ResID=<%=ResID %>&RecID=<%=RecID %>",
                             success: function (obj) {
                                 if (obj.success || obj.success == "true") {
                                     alert("保存成功!");
                                     window.parent.ParentCloseWindow();
                                 } else {
                                     alert("保存失败,请刷新页面！");
                                 }
                             }
                         });
                     }
                }
            });

         }
         function clearForm() {
             $('#ff').form('clear');
         }
    </script>
</head>
<body>
    <div class="easyui-panel" style="width: 100%; height:300px;" >
        <div style="margin-left: 10%; margin-top: 5%;">
            <form id="ff" method="post" runat="server">
                <table cellpadding="5" id="AddMX">
                    <tr id="ParentId">
                        <td>父级ID:</td>
                        <td>
                            <input id="<%=ResID %>_父级ID"  class="easyui-textbox"  style="width:65%"  data-options="required:true"  /></td>
                    </tr>
                    <tr id="KeyCode">
                        <td>数据字典编号:</td>
                        <td>
                            <input id="<%=ResID %>_数据字典编号"  class="easyui-textbox"    style="width:65%"   data-options="required:true,prompt:'如客户类型，编号可写为：KHLX'" /></td>
                    </tr>
                    <tr>
                        <td>数据字典标题:</td>
                        <td>
                            <input id="<%=ResID %>_数据字典标题" class="easyui-textbox"   style="width:65%"  data-options="required:true" /></td>
                    </tr>
                    <tr id="KeyValue">
                         <td>数据字典值:</td>
                        <td>
                            <input id="<%=ResID %>_数据字典值" class="easyui-textbox"   style="width:65%"   data-options="required:true" /></td>
                    </tr>
                    <tr>
                        <td>数据字典排序号:</td>
                        <td>
                            <input id="<%=ResID %>_数据字典排序" class="easyui-numberspinner"   style="width:65%"  data-options="required:true,increment:1" /></td>
                    </tr>
                    <tr>
                        <td>数据字典描述:</td>
                        <td>
                            <input id="<%=ResID %>_数据字典描述" class="easyui-textbox" data-options="multiline:true" style="height:50px; width: 300px;" />
                               <input id="<%=ResID %>_创建日期"value="<%=Time %>" type="hidden" />
                               <input id="<%=ResID %>_创建人"  value="<%=UserName %>" type="hidden" />
                        </td>
                    </tr>
                </table>
            </form>

     
            <div style="text-align: center; padding: 5px">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="submitForm()">保存</a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-cut'" onclick="clearForm()">清空</a>
            </div>
        </div>
</body>
</html>
