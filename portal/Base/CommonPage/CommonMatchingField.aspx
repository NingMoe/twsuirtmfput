<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommonMatchingField.aspx.cs" Inherits="Base_CommonPage_CommonMatchingField" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .ExcelInfo {
            float: left;
            height: 10%;
            width: 100%;
            font-size: 16px;
        }

        .selectSheet {
            width: 30%;
            float: right;
            margin-right: 5%;
        }

            .selectSheet select {
                width: 50%;
            }

        .ExcelInfoPanel {
            padding: 10px;
        }

        .MatchingDiv {
            height: 90%;
            width: 100%;
        }

        .MatchingField {
            float: left;
            height: 90%;
            width: 52%;
            line-height: 80%;
        }

        .ExcelFieldInfo {
            float: right;
            height: 92%;
            width: 48%;
        }

        .ExcelFields {
            height: 100%;
            width: 42%;
            float: left;
        }

        .DataFields {
            height: 100%;
            float: right;
            width: 42%;
        }

        .Btn {
            margin: 0 auto;
            text-align: center;
        }
    </style>

    <%=GetScript1_4_3 %>
    <%-- <script src="/portal/Scripts/jquery-1.8.0.min.js"></script>--%>
    <script src="../../Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="/portal/Scripts/layer-v2.0/layer/layer.js"></script>
    <link href="/portal/Scripts/layer-v2.0/layer/skin/layer.css" rel="stylesheet" />
    <script src="../../Scripts/MyJS.js"></script>
    <script type="text/javascript">

        var OptionModel = eval('[<%=OptionModelStr%>]')[0]

        $(document).ready(function () {
            $("#form1").css("height", $(window).height() - 10)
            $("#div_基本信息").css("height", $(".ExcelFieldInfo").css("height"))
            $("#ExcelFieldInfoPanel").css("height", $(".ExcelFieldInfo").height() - 5)

            //$("#ExcelInfoPanel").css("height", $(".ExcelInfo").height() - 5)

            $("#div_MatchingField").css("height", $(".MatchingField").css("height"))
            $("#MatchingFieldPanel").css("height", $(".MatchingField").height() - 5)

            $(".Btn").css("margin-top", ($("#MatchingFieldPanel").height() / 3.2) + 'px')
            ReadExcelSheets();
        });


        function ReadField() {
            $.ajax({
                type: "POST",
                dataType: "json",
                data: {
                    "argSql": "",
                },
                url: "../Organization/ZTH_GetInfo_ajax.aspx?typeValue=GetOneRowValueBySQL",
                success: function (obj) {

                    var ExcelFields = [];
                    var DataFields = [];
                    $('.ExcelFields').datalist({
                        data: ExcelFields,
                        checkbox: false,
                        //lines: true,
                        //valueField: "dd",
                        //textField: "dd",
                        textFormatter: function (value, row, index) {
                            return value;
                        }
                    });


                    $('.DataFields').datalist({
                        data: DataFields,
                        lines: true,
                        textFormatter: function (value, row, index) {
                        }
                    });

                }
            });
        }

        function ReadExcelSheets() {

            var Docurl = parent.ReadExcelURL

            var baseURL = window.location.protocol + "//" + window.location.host + "/webflow/Document/ToExcel/DoEventByExcel_ajax.aspx"

            var tip = "";
            var IsDown = false;
            var DC_Params = {
                argUserID: '<%=UserID%>',
                argTableResid: '<%=BaseResid%>'
            }

            OptionModel.KeyType = "获取所有工作簿名称";
            OptionModel.ReadSheetStr = ""
            OptionModel.StartRowNum = 1;
            OptionModel.StartColumnNum = 1;
            OptionModel.FilePath = Docurl;
            OptionModel.HasTitle = true;

            DC_Params.GetExcelHelperOptionModelStr = JSON.stringify(OptionModel);

            var index = layer.load(1); //换了种风格
            $.ajax({
                type: "POST",
                data: DC_Params,
                url: encodeURI(baseURL),
                success: function (o) {
                    layer.close(index);
                    var Result = eval('[' + o + ']')[0];
                    if (Result.ErrorStr != "") {
                        alert(Result.ErrorStr)
                    }
                    else {

                        var sheets = eval(Result.Result)
                        var h = "<option value='" + "" + "' >" + "" + "</option>"
                        for (var k in sheets) {
                            h += "<option value='" + sheets[k] + "' >" + sheets[k] + "</option>"
                        }

                        $("#ReadExcelSheets").html(h);
                        //ReadExcelInfo("Sheet1")
                        // Result.Result
                    }
                },
                error: function (o) {
                    layer.close(index);
                    debugger
                }
            })
        }

        function ReadExcelInfo(ReadSheetStr) {

            var Docurl = parent.ReadExcelURL

            var baseURL = window.location.protocol + "//" + window.location.host + "/webflow/Document/ToExcel/DoEventByExcel_ajax.aspx"

            var tip = "";
            var IsDown = false;
            var DC_Params = {
                argUserID: '<%=UserID%>',
                argTableResid: '<%=BaseResid%>'
            }

            OptionModel.KeyType = "获取匹配字段";
            OptionModel.ReadSheetStr = ReadSheetStr
            OptionModel.StartRowNum = 1;
            OptionModel.StartColumnNum = 1;
            OptionModel.FilePath = Docurl;
            OptionModel.HasTitle = true;

            DC_Params.GetExcelHelperOptionModelStr = JSON.stringify(OptionModel);

            var index = layer.load(1); //换了种风格
            $.ajax({
                type: "POST",
                data: DC_Params,
                url: encodeURI(baseURL),
                success: function (o) {
                    layer.close(index);
                    var Result = eval('[' + o + ']')[0];
                    if (Result.ErrorStr != "") {
                        alert(Result.ErrorStr)
                    }
                    else {
                        var obj = eval(Result.Result)
                        var ExcelFields = GetDataListJson(eval(obj[0].ExcelFields)[0]);
                        var DataFields = GetDataListJson(eval(obj[0].DataFields)[0]);

                        $('#ExcelFields_DIV').datalist({
                            data: ExcelFields,
                            //checkbox: true,
                            //lines: true,
                            title: "待绑定字段",
                            height: '100%',
                            valueField: "text",
                            textField: "text",
                            textFormatter: function (value, row, index) {
                                return value;
                            }
                        });


                        $('#DataFields_DIV').datalist({
                            data: DataFields,
                            //checkbox: true,
                            //lines: true,
                            title: "原表字段",
                            height: '100%',
                            valueField: "text",
                            textField: "text",
                            textFormatter: function (value, row, index) {
                                return value;
                            }
                        });

                        $(".Btn").show()
                    }
                },
                error: function (o) {
                    layer.close(index);
                    debugger
                }
            })
        }

        function SelectExcelSheet()
        {
            var s = $("#ReadExcelSheets").val();
            if(s=="")
            {
                alert("表名不能为空！")
                return;
            }
            ReadExcelInfo(s)
        }

        function StartInport() {

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ExcelInfo">
            <div class="ExcelInfoPanel">
                已选Excel文件： <%=ReadExcelName %>

                <%-- <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',plain:true,disabled:false" onclick="AddField()" id="AddField">读取所有表</a>--%>
                <span class="selectSheet">请选择一张工作表：
            <select id="ReadExcelSheets" onchange="SelectExcelSheet()">
            </select>
                </span>
            </div>
        </div>
        <div class="MatchingDiv">
            <div class="MatchingField">
                <div id="div_MatchingField" title="待绑定字段" class="easyui-panel" style="overflow-x: hidden; overflow-y: auto; padding: 5px; margin: 0px; width: 99%">
                    <div class="easyui-panel" border="true" id="MatchingFieldPanel">

                        <div class="ExcelFields">
                            <div id="ExcelFields_DIV"></div>
                        </div>
                        <div class="DataFields">
                            <div id="DataFields_DIV"></div>
                        </div>

                        <div class="Btn" style="margin-top: 25%;display:none">
                            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',iconAlign:'top',plain:true,disabled:false" onclick="AutoMatchingField()" id="AutoMatchingField">自行匹配</a>
                            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',iconAlign:'top',plain:true,disabled:false" onclick="MatchingField()" style="margin-top: 5%" id="MatchingField">添加匹配</a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ExcelFieldInfo">
                <div id="div_基本信息" title="绑定信息" class="easyui-panel" style="overflow-x: hidden; overflow-y: auto; padding: 5px; margin: 0px; width: 99%">
                    <div class="easyui-panel" border="true" id="ExcelFieldInfoPanel">
                        <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                            <tr style="height: 30px; text-align: center">
                                <th style="width: 30%; text-align: center; padding: 0px">Excel字段名
                                </th>
                                <th style="width: 30%; text-align: center; padding: 0px">数据库字段名
                                </th>
                                <th style="width: 20%; text-align: center; padding: 0px">操作
                                </th>
                            </tr>
                            <tbody>
                                <tr style="height: 30px; text-align: center">
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                </tr>
                                <tr style="height: 30px; text-align: center">
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                </tr>
                                <tr style="height: 30px; text-align: center">
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                </tr>
                                <tr style="height: 30px; text-align: center">
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                </tr>
                                <tr style="height: 30px; text-align: center">
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                </tr>
                                <tr style="height: 30px; text-align: center">
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
