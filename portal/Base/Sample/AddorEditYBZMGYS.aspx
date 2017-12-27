<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditYBZMGYS.aspx.cs" Inherits="Base_Sample_AddorEditYBZMGYS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>样本招募渠道统计表</title>
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
            }
            SetBackgroundColor();
        });

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
                            <th style="width: 130px;">用户名</th>
                            <td colspan="3">
                               <input id="<%=ResID %>_用户名"  class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr>
                        <tr>
                            <th style="width: 130px;">网址</th>
                            <td style="width: 250px;">
                               <input id="<%=ResID %>_网址"  class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th style="width: 130px;">联系方式</th>
                            <td style="width: 250px;"><input id="<%=ResID %>_联系方式"  class="easyui-textbox" style="width: 95%;" /></td>                            
                        </tr>
                        <tr>
                            <th>渠道</th>
                            <td>
                                <input id="<%=ResID %>_渠道"  class="easyui-textbox" style="width: 95%;"  />
                            </td>
                            <th>合作类型</th>
                            <td>
                             <select id="<%=ResID %>_合作类型" class="easyui-combobox" editable="false" style="width: 95%;">  
                                <option value="推广联盟">推广联盟</option><option value="体验任务+CPS">体验任务+CPS</option>
                                <option value="广告联盟">广告联盟</option><option value="积分合作">积分合作</option>
                                <option value="资源互换">资源互换</option>
                             </select>
                            </td>
                        </tr>
                        <tr>  
                            <th>单价</th>
                            <td >
                                <input id="<%=ResID %>_单价"  class="easyui-textbox" style="width: 95%;" />
                            </td>     
                            <th>合作状态</th>
                            <td >
                               <select id="<%=ResID %>_合作状态" class="easyui-combobox"  editable="false" style="width: 95%;">  
                                <option value="">&nbsp;</option><option value="已签">已签</option>
                                <option value="不签">不签</option></select>
                            </td>                        
                        </tr>
                        <tr>
                            <th>结算时间</th>
                            <td>
                                <select id="<%=ResID %>_结算时间" class="easyui-combobox"  editable="false" style="width: 95%;">  
                                <option value="周结">周结</option><option value="月结后付">月结后付</option>
                                <option value="预付">预付</option><option value="预付/后月结">预付/后月结</option>
                                </select>
                            </td>
                            <th>合作时间</th>
                            <td>
                                <input id="<%=ResID %>_合作时间" class="easyui-textbox" style="width: 95%;" />
                            </td>
                        </tr>   
                        <tr>
                            <th>录入</th>
                            <td>
                                <input id="<%=ResID %>_录入" value="<%=UserName %>" class="easyui-textbox" style="width: 95%;" />
                            </td>
                            <th>录入时间</th>
                            <td>
                                <input id="<%=ResID %>_录入时间" value="<%=Time %>"  class="easyui-datebox" style="width: 95%;" />
                            </td>
                        </tr>   
                        <tr>
                            <th>编号</th>
                            <td>
                                <input id="<%=ResID %>_编号" class="easyui-textbox" style="width: 95%;"  />
                            </td>
                            <th>收款人</th>
                            <td>
                                <input id="<%=ResID %>_收款人"   class="easyui-textbox" style="width: 95%;"  />
                            </td>
                        </tr>   
                        <tr>
                            <th>收款账号</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_收款账号" class="easyui-textbox"  data-options="multiline:true"  style="width:630px;height:60px"   />
                            </td>
                        </tr>   
                        <tr>
                            <th>备注</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_备注" class="easyui-textbox" data-options="multiline:true"  style="width:630px;height:60px"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
