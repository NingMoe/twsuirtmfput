<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditYSKGL.aspx.cs" Inherits="Base_Finance_AddorEditYSKGL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>应收款管理</title>
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
            LoadPro("<%=ResID %>", "<%=RecID %>");
            SetBackgroundColor();
        });


        function LoadPro(ResID, RecID) {
            var SelectRecordData1 = {
                InitializationStr: "#" + ResID + "_发票申请单号",
                key: "#" + ResID + "_发票申请单号",
                ResID: ResID,
                deafultValue: $("#" + ResID + "_发票申请单号").val(),
                keyWordValue: "LCXMBHCX",
                idField: "发票申请单号",
                textField: "发票申请单号",
                RecID: RecID,
                KeyWidth: 238,
                Keyheight: 25,
                PercentageWidth: 70,
                panelWidth: 700,
                HasLastOperation: true,
                QueryKeyField: "开票用公司名称,发票申请单号,申请人,申请日期,客户方联系人,客户名称,固定电话,手机,email,其他联系方式,联系地址,邮编,开票凭证,备注,项目编号列表,项目名称,发票类型,币种,开票金额",
                UserDefinedSql: "401290375198",
                Columns: "开票用公司名称#开票用公司名称,发票申请单号#发票申请单号,申请人#申请人,申请日期#申请日期,客户方联系人#客户方联系人,客户名称#客户名称,固定电话#固定电话,手机#手机,email#email,其他联系方式#其他联系方式,联系地址#联系地址,邮编#邮编,开票凭证#开票凭证,备注#备注,项目编号列表#项目编号列表,项目名称#项目名称,发票类型#发票类型,币种#币种,开票金额#开票金额",
                SetValueStr: "开票用公司名称=开票用公司名称,发票申请单号=发票申请单号,申请人=申请人,申请日期=申请日期,客户方联系人=客户方联系人,客户名称=客户名称,固定电话=固定电话,手机=手机,email=email,其他联系方式=其他联系方式,联系地址=联系地址,邮编=邮编,开票凭证=开票凭证,备注=备注,项目编号列表=项目编号列表,项目名称=项目名称,发票类型=发票类型,币种=币种,开票金额=开票金额",
                ROW_NUMBER_ORDER: "order by 申请日期 DESC"
            }
            InitializationComboGrid(SelectRecordData1);
        }

        function LastOperation(rowData, SelectRecordData) {
            var K1 = rowData.发票申请单号;
            if (K1 != "undefined" && K1 != "" && K1 != null) {
                $("#<%=ResID %>_发票申请单号").textbox("setValue", K1);
            }
            var K2 = rowData.申请人;
            if (K2 != "undefined" && K2 != "" && K2 != null) {
                $("#<%=ResID %>_申请人").textbox("setValue", K2);
            }
            var K3 = rowData.申请日期;
            if (K3 != "undefined" && K3 != "" && K3 != null) {
                $("#<%=ResID %>_申请日期").textbox("setValue", K3);
            }
            var K4 = rowData.客户方联系人;
            if (K4 != "undefined" && K4 != "" && K4 != null) {
                $("#<%=ResID %>_客户方联系人").textbox("setValue", K4);
            }
            var K5 = rowData.客户名称;
            if (K5 != "undefined" && K5 != "" && K5 != null) {
                $("#<%=ResID %>_客户名称").textbox("setValue", K5);
            } 
            var K6 = rowData.固定电话;
            if (K6 != "undefined" && K6 != "" && K6 != null) {
                $("#<%=ResID %>_固定电话").textbox("setValue", K6);
            }
            var K7 = rowData.手机;
            if (K7 != "undefined" && K7 != "" && K7 != null) {
                $("#<%=ResID %>_手机").textbox("setValue", K7);
            }
            var K8 = rowData.email;
            if (K8 != "undefined" && K8 != "" && K8 != null) {
                $("#<%=ResID %>_email").textbox("setValue", K8);
            }
            var K9 = rowData.其他联系方式;
            if (K9 != "undefined" && K9 != "" && K9 != null) {
                $("#<%=ResID %>_其他联系方式").textbox("setValue", K9);
            }
            var L1 = rowData.联系地址;
            if (L1 != "undefined" && L1 != "" && L1 != null) {
                $("#<%=ResID %>_联系地址").textbox("setValue", L1);
            }
            var L2 = rowData.邮编;
            if (L2 != "undefined" && L2 != "" && L2 != null) {
                $("#<%=ResID %>_邮编").textbox("setValue", L2);
            }
            var L3 = rowData.开票凭证;
            if (L3 != "undefined" && L3 != "" && L3 != null) {
                $("#<%=ResID %>_开票凭证").textbox("setValue", L3);
            }
            var L4 = rowData.备注;
            if (L4 != "undefined" && L4 != "" && L4 != null) {
                $("#<%=ResID %>_备注").textbox("setValue", L4);
            }
            var L5 = rowData.项目编号列表;
            if (L5 != "undefined" && L5 != "" && L5 != null) {
                $("#<%=ResID %>_项目编号列表").textbox("setValue", L5);
            }
            var L10 = rowData.项目名称;
            if (L10 != "undefined" && L10 != "" && L10 != null) {
                $("#<%=ResID %>_项目名称").textbox("setValue", L10);
            }
            var L6 = rowData.发票类型;
            if (L6 != "undefined" && L6 != "" && L6 != null) {
                $("#<%=ResID %>_发票类型").textbox("setValue", L6);
            }
            var L7 = rowData.币种;
            if (L7 != "undefined" && L7 != "" && L7 != null) {
                $("#<%=ResID %>_币种").textbox("setValue", L7);
            }
            var L8 = rowData.开票金额;
            if (L8 != "undefined" && L8 != "" && L8 != null) {
                $("#<%=ResID %>_开票金额").textbox("setValue", L8);
            }
            var L9 = rowData.开票用公司名称;
            if (L9 != "undefined" && L9 != "" && L9 != null) {
                $("#<%=ResID %>_开票用公司名称").textbox("setValue", L9);
            }
        }
        // 验证整数或小数
        $.extend($.fn.validatebox.defaults.rules, {
            intOrFloat: {
                validator: function (value) {
                    return /^\d+(\.\d+)?$/i.test(value);
                },
                message: '请输入数字!'
            }
        });

        //保存方法
        function fnParentSave() {
            //验证非空
            var check = $('#form1').form('validate');
            if (!check) { return; }
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
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x:hidden;overflow-y:auto; position: relative; height: 450px; border: none">
            <table width="760px;" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
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
            <div title="应收款管理" class="easyui-panel" collapsible="true" style="overflow: hidden; padding:1px; margin: 0px;width:770px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table1">
                        <tr><th style="width:15%">发票申请单号</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_发票申请单号"  type="text" class="easyui-textbox"  style="width: 98%" /></td>
                        </tr><tr>
                            <th style="width:15%">开票用公司名称</th>
                            <td style="width: 22%">
                            <select id="<%=ResID %>_开票用公司名称"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="库思">库思</option>
                                <option value="库威">库威</option><option value="库润">库润</option>
                            </select></td>
                            <th style="width:12%">发票类型</th>
                            <td style="width: 22%">
                                <select id="<%=ResID %>_发票类型"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="普通服务发票">普通服务发票</option>
                                <option value="invoice">invoice</option><option value="可抵扣专用发票">可抵扣专用发票</option>
                            </select></td>
                        </tr>
                        <tr> <th>已开票金额</th>
                             <td >
                                <input id="<%=ResID %>_开票金额" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                            <th>币种</th>
                            <td >
                                <select id="<%=ResID %>_币种" class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="人民币">人民币</option>
                                <option value="美元">美元</option><option value="日元">日元</option>
                                <option value="欧元">欧元</option><option value="英镑">英镑</option>
                            </select></td>
                         </tr>
                         <tr>   <th>本次开票申请金额</th>
                            <td  colspan="3">
                                <input id="<%=ResID %>_本次开票申请金额" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td> </tr>
                        <tr>
                            <th>项目编号列表</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_项目编号列表" type="text" class="easyui-textbox" style="width: 98%" />
                            </td> 
                        </tr>
                        <tr>
                            <th>项目名称</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_项目名称" type="text" class="easyui-textbox" style="width: 98%" />
                            </td> 
                        </tr>
                        <tr><th>PO单</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_PO单" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr>
                          <tr>
                            <th>申请日期</th>
                            <td >
                                <input id="<%=ResID %>_申请日期" value="<%=Time %>"  type="text" class="easyui-datebox" style="width: 95%" />
                            </td>
                            <th>申请人</th>
                            <td >
                                <input id="<%=ResID %>_申请人" value="<%=UserName %>" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                         </tr>
                          <tr><th>客户名称</th>
                            <td>
                                <input id="<%=ResID %>_客户名称" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th>客户方联系人</th>
                            <td>
                                <input id="<%=ResID %>_客户方联系人" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                         </tr><tr>
                            <th>固定电话</th>
                             <td >
                                <input id="<%=ResID %>_固定电话" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                            <th>手机</th>
                            <td >
                                <input id="<%=ResID %>_手机" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                         </tr><tr>
                            <th>email</th>
                             <td colspan="3"  >
                                <input id="<%=ResID %>_email" type="text" class="easyui-textbox" style="width: 98%" />
                            </td>
                         </tr><tr>
                            <th>其他联系方式</th>
                            <td  colspan="3">
                                <input id="<%=ResID %>_其他联系方式" type="text" class="easyui-textbox" style="width: 98%" />
                            </td>
                         </tr><tr>
                         <th>联系地址</th>
                             <td  colspan="3"  >
                                <input id="<%=ResID %>_联系地址" type="text" class="easyui-textbox" style="width: 98%" />
                            </td>
                         </tr><tr>
                         <th>邮编</th>
                             <td >
                                <input id="<%=ResID %>_邮编" type="text" class="easyui-textbox" style="width: 95%" />
                            </td><th>开票凭证</th>
                             <td >
                                <input id="<%=ResID %>_开票凭证" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                         </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="3">
                            <input class="easyui-textbox" id="<%=ResID %>_备注" style="width: 98%; height: 60px" data-options="multiline:true" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div title="发票确认" class="easyui-panel" collapsible="true" style="overflow: hidden; padding:1px; margin: 0px;width:770px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                         <tr>
                            <th style="width:15%">开票人</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_开票人" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th style="width:12%">开票时间</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_开票时间" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >寄票快递单号</th>
                            <td >
                                <input id="<%=ResID %>_寄票快递单号" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th >寄票人</th>
                            <td ><input id="<%=ResID %>_寄票人" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <th >确认人</th>
                            <td >
                                <input id="<%=ResID %>_确认人" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th >确认时间</th>
                            <td >
                                <input id="<%=ResID %>_确认时间" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <th >催款人员</th>
                            <td >
                                <input id="<%=ResID %>_催款人员" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th >催款日期</th>
                            <td >
                                <input id="<%=ResID %>_催款日期" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                        </tr> 
                        <tr>
                            <th >风险等级</th>
                            <td >
                                <select id="<%=ResID %>_风险等级"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option>
                                <option value="A级（安全）">A级（安全）</option>
                                <option value="B级（危险）">B级（危险）</option>
                                <option value="C级（很危险）">C级（很危险）</option>
                                <option value="D级（呆账）">D级（呆账）</option>
                                </select>
                            </td>
                            <th >预计回款日期</th>
                            <td ><input id="<%=ResID %>_预计回款日期" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                        </tr>
                        <tr>
                            <th>客户方联系人</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_客户方联系人1" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                         </tr>
                        <tr>
                            <th >收款状态</th>
                            <td >
                                <select id="<%=ResID %>_收款状态"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="未收完">未收完</option><option value="坏账">坏账</option>
                                <option value="已结清">已结清</option><option value="已抵账">已抵账</option></select>
                            </td>
                            <th >付款方式</th>
                            <td ><input id="<%=ResID %>_付款方式" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr> <tr>
                            <th >已收款</th>
                            <td >
                                <input id="<%=ResID %>_已收款" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                            <th >应收款</th>
                            <td ><input id="<%=ResID %>_应收款" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr> <tr>
                            <th >收款日期</th>
                            <td >
                                <input id="<%=ResID %>_收款日期" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                            <th >开票状态</th>
                            <td ><select id="<%=ResID %>_开票状态"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="已开票">已开票</option>
                                <option value="作废">作废</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
