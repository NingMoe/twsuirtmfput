<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddorEditFPSQ.aspx.cs" Inherits="Base_Finance_AddorEditFPSQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2">
    <title>收款记录维护界面</title>
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
                        loadCenterGrid();
                    }
                });
            } else {
                loadCenterGrid();
            }
            LoadPro("<%=ResID %>", "<%=RecID %>");
            SetBackgroundColor();
        });

    function LoadPro(ResID, RecID) {

        var SelectRecordData2 = {
            InitializationStr: "#" + ResID + "_客户名称",
            key: "#" + ResID + "_客户名称",
            ResID: ResID,
            deafultValue: $("#" + ResID + "_客户名称").val(),
            keyWordValue: "LCXMBHCX",
            idField: "客户名称",
            textField: "客户名称",
            RecID: RecID,
            KeyWidth: 225,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 500,
            HasLastOperation: true,
            QueryKeyField: "客户名称,客户方联系人,固定电话,手机,email,其他联系方式,联系地址",
            UserDefinedSql: "401290331886",
            Columns: "客户名称#客户名称,客户方联系人#客户方联系人,固定电话#固定电话,手机#手机,email#email,其他联系方式#其他联系方式,联系地址#联系地址",
            SetValueStr: "客户名称=客户名称,客户方联系人=客户方联系人,固定电话=固定电话,手机=手机,email=email,其他联系方式=其他联系方式,联系地址=联系地址",
            ROW_NUMBER_ORDER: "客户名称=客户全称,客户方联系人=联系人,联系地址=办公地址"
        }
        InitializationComboGrid(SelectRecordData2);

        var SelectRecordData1 = {
            InitializationStr: "#" + ResID + "_客户方联系人",
            key: "#" + ResID + "_客户方联系人",
            ResID: ResID,
            deafultValue: $("#" + ResID + "_客户方联系人").val(),
            keyWordValue: "LCXMBHCX",
            idField: "客户方联系人",
            textField: "客户方联系人",
            RecID: RecID,
            KeyWidth: 225,
            Keyheight: 25,
            PercentageWidth: 70,
            panelWidth: 500,
            HasLastOperation: true,
            QueryKeyField: "客户名称,客户方联系人,固定电话,手机,email,其他联系方式,联系地址",
            UserDefinedSql: "401290331886", 
            Columns: "客户名称#客户名称,客户方联系人#客户方联系人,固定电话#固定电话,手机#手机,email#email,其他联系方式#其他联系方式,联系地址#联系地址",
            SetValueStr: "客户名称=客户名称,客户方联系人=客户方联系人,固定电话=固定电话,手机=手机,email=email,其他联系方式=其他联系方式,联系地址=联系地址",
            ROW_NUMBER_ORDER: "客户名称=客户全称,客户方联系人=联系人,联系地址=办公地址"
        }
        InitializationComboGrid(SelectRecordData1);
    }




    function LastOperation(rowData, SelectRecordData) {
        var khlxr = rowData.客户方联系人;
        if (khlxr != "undefined" && khlxr != "" && khlxr != null) {
            $("#<%=ResID %>_客户方联系人").textbox("setValue", khlxr);
        }
        var khmc = rowData.客户名称;
        if (khmc != "undefined" && khmc != "" && khmc != null) {
            $("#<%=ResID %>_客户名称").textbox("setValue", khmc);
        }
        var kddh = rowData.固定电话;
        if (kddh != "undefined" && kddh != "" && kddh != null) {
            $("#<%=ResID %>_固定电话").textbox("setValue", kddh);
        }
        var sj = rowData.手机
        if (sj != "undefined" && sj != "" && sj != null) {
            $("#<%=ResID %>_手机").textbox("setValue", sj)
        }
        var email = rowData.email;
        if (email != "undefined" && email != "" && email != null) {
            $("#<%=ResID %>_email").textbox("setValue", email);
        }
        var qtlxfs = rowData.其他联系方式;
        if (qtlxfs != "undefined" && qtlxfs != "" && qtlxfs != null) {
            $("#<%=ResID %>_其他联系方式").textbox("setValue", qtlxfs);
        }
        var lxdz = rowData.联系地址;
        if (lxdz != "undefined" && lxdz != "" && lxdz != null) {
            $("#<%=ResID %>_联系地址").textbox("setValue", lxdz);
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
        var rows = $('#CenterGrid').datagrid('getRows');
        var Json2 = "[";
        for (var i = 0; i < rows.length; i++) {
            Json2 += "{";
            Json2 += "'ID':'" + rows[i].ID + "',";
            Json2 += "'销售':'" + rows[i].销售 + "',";
            Json2 += "'项目编号':'" + rows[i].项目编号 + "',";
            Json2 += "'项目名称':'" + rows[i].项目名称 + "',";
            Json2 += "'客户全称':'" + rows[i].客户全称 + "',";
            Json2 += "'实际金额':'" + rows[i].实际金额 + "',";
            Json2 += "'开票时间':'" + rows[i].开票时间 + "',";
            Json2 += "'本次开票申请金额':'" + rows[i].本次开票申请金额 + "',";
            Json2 += "'本次开票金额':'" + rows[i].本次开票金额 + "',";
            Json2 += "'已开票金额':'" + rows[i].已开票金额 + "',";
            Json2 += "'发票申请单号':''";
            Json2 += "},";
        }
        Json2 = Json2.substring(0, Json2.length - 1);
        Json2 += "]";
        $("#btnParentSave").attr("disabled", true);
        $.ajax({
            type: "POST",
            dataType: "json",
            data: { "Json1": "" + jsonStr1 + "", "Json2": "" + Json2 + "" },
            url: "../Common/CommonAjax_Request.aspx?typeValue=SaveFPSQAndXMLB&ResID=<%=ResID %>&RecID=<%=RecID %>",
            success: function (obj) {
                if (obj.success || obj.success == "true") {
                    $("#btnParentSave").attr("disabled", false);
                    alert("保存成功!");
                    window.parent.closeWindow1();
                } else {
                    alert("保存失败,请刷新页面！");
                    $("#btnParentSave").attr("disabled", false);
                }
            }
        });
    }


    function loadCenterGrid() {
        $('#CenterGrid').datagrid({
            title: "项目列表",
            toolbar: [{
                iconCls: 'icon-add',
                text: "添加",
                handler: function () {
                    fnFormListDialog("AddorEditXMLB.aspx", 700, 400, "添加信息");
                }
            }, '-', {
                iconCls: 'icon-add',
                text: "修改",
                handler: function () {
                    var rec = $('#CenterGrid').datagrid('getSelected');
                    if (rec == null) {
                        alert("请选中要修改的记录");
                        return;
                    } else {
                        if (rec.ID != "") {
                            fnFormListDialog("AddorEditXMLB.aspx?RecID=" + rec.ID, 700, 400, "修改信息");
                        } else {
                            alert("请先保存数据后修改！");
                            return;
                        }
                    }
                }
            }, '-', {
                iconCls: 'icon-add',
                text: "删除",
                handler: function () {
                    var rec = $('#CenterGrid').datagrid('getSelected');
                    if (rec == null) {
                        alert("请选择一条记录!")
                    } else {
                        if (confirm("确定要删除该条记录吗？")) {
                            var rowIndex = $('#CenterGrid').datagrid('getRowIndex', rec);
                            $('#CenterGrid').datagrid('deleteRow', rowIndex);
                            var recs = $('#CenterGrid').datagrid("getRows");
                            var KPJE = 0;
                            var xmbhLB = "";
                            var xmmclb = "";
                            for (var i = 0; i < recs.length; i++) {
                                if (recs[i]["本次开票申请金额"] != "" && recs[i]["本次开票申请金额"] != null) {
                                    KPJE += parseFloat(recs[i]["本次开票申请金额"]);
                                }
                                if (i == recs.length - 1) {
                                    xmbhLB += recs[i]["项目编号"];
                                    xmmclb += recs[i]["项目名称"];
                                } else {
                                    xmbhLB += recs[i]["项目编号"] + ",";
                                    xmmclb += recs[i]["项目名称"] + ",";
                                }
                            }
                            $('#401290375198_项目名称').textbox("setValue", xmmclb); 
                            $('#401290375198_本次开票申请金额').textbox("setValue", KPJE);
                            $('#401290375198_项目编号列表').textbox("setValue", xmbhLB);
                        }
                    }
                }
            }],
            nowrap: true,
            border: true,
            striped: true,
            singleSelect: true,
            loadFilter: function (data) {
                if (data == null) {
                    $(this).datagrid("load");
                    return $(this).datagrid("getData");
                } else {
                    return data;
                }
            },
            url: '../Common/Ajax_Request.aspx?typeValue=GetData&ResID=401290344433',
            queryParams: {
                Condition: " and 发票申请单号='" + $("#<%=ResID %>_发票申请单号").val() + "'"
            },
            columns: [[{ field: 'ID', hidden: true },
                { field: '销售', title: '销售', width: 60, align: 'center' },
	            { field: '项目编号', title: '项目编号', width: 125, align: 'center' },
	            { field: '项目名称', title: '项目名称', width: 120, align: 'center' },
                { field: '客户全称', title: '客户全称', width: 100, align: 'center' },
                { field: '实际金额', title: '实际金额', width: 60, align: 'center' },
                { field: '本次开票申请金额', title: '本次开票申请金额', width: 100, align: 'center' },
                { field: '本次开票金额', title: '本次开票金额', width: 80, align: 'center' },
                { field: '已开票金额', title: '已开票金额', width: 80, align: 'center' },
                { field: '开票时间', title: '开票时间', width: 70, align: 'center'}]],
            fitColumns: true,
            height: 300,
            rownumbers: true
        });
    }

    function fnFormListDialog(url, DialogWidth, DialogHeight, title) {
        $('#divParentContent').append($("<iframe scrolling='no' id='FromInfo' frameborder='0' marginwidth='0' marginheight='0' style='width:100%;height:100%;'></iframe")).dialog({
            title: title,
            width: DialogWidth,
            height: DialogHeight,
            cache: false,
            closed: true,
            shadow: false,
            closable: true,
            draggable: false,
            resizable: false,
            modal: true
        });
        var index = url.indexOf("?");
        if (index != "-1") {
            url = url + "&height=" + DialogHeight;
        } else {
            url = url + "?height=" + DialogHeight;
        }
        $('#FromInfo')[0].src = encodeURI(url);
        $('#divParentContent').dialog('open');
    }

    function ParentCloseWindow() {
        $('#divParentContent').dialog('close');
        $('#CenterGrid').dialog('loadData');
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
            <div title="发票申请" class="easyui-panel" collapsible="true" style="overflow: hidden; padding:1px; margin: 0px;width:770px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table1">
                         <tr>
                            <th style="width:15%">开票用公司名称</th>
                            <td style="width: 22%">
                            <select id="<%=ResID %>_开票用公司名称"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="库思">库思</option>
                                <option value="库威">库威</option><option value="库润">库润</option>
                            </select></td>
                            <th style="width:15%">发票申请单号</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_发票申请单号"  editable="false" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >发票类型</th>
                            <td >
                                <select id="<%=ResID %>_发票类型"   class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="普通服务发票">普通服务发票</option>
                                <option value="invoice">invoice</option><option value="可抵扣专用发票">可抵扣专用发票</option>
                            </select></td>
                            <th >本次开票申请金额</th>
                            <td  >
                                <input id="<%=ResID %>_本次开票申请金额" type="text" readonly="readonly"  class="easyui-textbox" style="width: 95%" />
                            </td>
                            <%-- <th>已开票金额</th>
                             <td >
                                <input id="<%=ResID %>_开票金额" type="text" readonly="readonly"  class="easyui-textbox" style="width: 95%" />
                            </td>--%>
                        </tr>
                        <tr><th>外币金额</th>
                             <td >
                                <input id="<%=ResID %>_外币金额" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                            <th>币种</th>
                            <td >
                                <select id="<%=ResID %>_币种" class="easyui-combobox" style="width:95%;" editable="false"  >
                                <option value="">&nbsp;</option><option value="人民币">人民币</option>
                                <option value="美元">美元</option><option value="日元">日元</option>
                                <option value="欧元">欧元</option><option value="英镑">英镑</option>
                            </select></td>
                         </tr>
                        <tr>
                            <th>项目编号列表</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_项目编号列表" type="text" readonly="readonly" class="easyui-textbox"  style="width: 98%;" />
                            </td> 
                        </tr>
                         <tr>
                            <th>项目名称</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_项目名称" type="text" readonly="readonly" class="easyui-textbox"  style="width: 98%;" />
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
                                <input class="easyui-textbox" id="<%=ResID %>_备注" style="width: 98%; height: 60px" data-options="multiline:true" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <table id="CenterGrid" ></table>
         <%--   <div title="invoice页面专用信息" class="easyui-panel" collapsible="true" style="overflow: hidden;  padding:1px; margin: 0px;width:770px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;width:100%;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                         <tr>
                            <th style="width:15%">DATA</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_DATA" type="text" class="easyui-datebox"  style="width: 95%" /></td>
                            <th style="width:15%">InvoiceNo</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_InvoiceNo" type="text" class="easyui-textbox"  style="width: 95%" /></td>
                        </tr><tr>
                            <th >Billto行1</th>
                            <td colspan="3" >
                                <input id="<%=ResID %>_Billto行1" type="text" class="easyui-datebox"  style="width: 98%" /></td>
                        </tr>
                        <tr>
                            <th>Billto行2</th>
                            <td colspan="3" >
                                <input id="<%=ResID %>_Billto行2" type="text" class="easyui-textbox" style="width: 98%" />
                            </td> 
                        </tr>
                        <tr><th>Billto行3</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_Billto行3" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr><tr><th>Billto行4</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_Billto行4" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr><tr><th>PO行1</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_PO行1" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr><tr><th>PO行2</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_PO行2" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr><tr><th>PO行3</th>
                            <td colspan="3"  >
                                <input id="<%=ResID %>_PO行3" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                        </tr> 
                         <tr>
                            <th>CPI</th>
                             <td >
                                <input id="<%=ResID %>_CPI" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                            <th>Completed</th>
                            <td >
                                <input id="<%=ResID %>_Completed" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                         </tr>
                          <tr>
                            <th>TotalAmount</th>
                             <td >
                                <input id="<%=ResID %>_TotalAmount" type="text" class="easyui-textbox" style="width: 95%" />
                            </td>
                            <th>Signature</th>
                            <td >
                                <input id="<%=ResID %>_Signature" type="text" class="easyui-textbox" style="width: 95%;" />
                            </td>
                         </tr>
                          <tr>
                            <th>SignatureEmail</th>
                            <td  colspan="3">
                                <input id="<%=ResID %>_SignatureEmail" type="text" class="easyui-textbox" style="width: 98%;" />
                            </td>
                         </tr>
                    </table>
                </div>
            </div>--%>
        </div>   
        <div closed="true"  class="easyui-window"  id="divParentContent" style="overflow:hidden;"/>
    </form>
</body>
</html>
