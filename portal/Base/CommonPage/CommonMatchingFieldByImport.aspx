<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommonMatchingFieldByImport.aspx.cs" Inherits="Base_CommonPage_CommonMatchingFieldByImport" %>

<%@ Register Src="~/Base/CommonControls/UploadFile.ascx" TagPrefix="uc1" TagName="UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/jquery-1.8.0.min.js"></script>
    <script src="/portal/Scripts/layer-v2.0/layer/layer.js"></script>
    <link href="/portal/Scripts/layer-v2.0/layer/skin/layer.css" rel="stylesheet" />
    <script src="/portal/Scripts/CommonCloseWindow.js"></script>
    <script src="../../Scripts/json2.js"></script>
    <script src="../../Scripts/DoExcel.js"></script>
    <style type="text/css">
        .ExcelInfo {
            width: 100%;
            font-size: 16px;
        }

        .selectSheet {
            width: 100%;
            margin-right: 1%;
            font-size: 16px;
            vertical-align: middle;
        }

            .selectSheet select {
                width: 50%;
                height: 25px;
            }

        .ExcelInfoPanel {
            /*padding: 10px;*/
        }

        .MatchingDiv {
            height: 60%;
            width: 100%;
            margin-top: 5px;
        }

        .MatchingField {
            float: left;
            height: 100%;
            width: 52%;
            line-height: 80%;
        }

        .ExcelFieldInfo {
            float: right;
            height: 100%;
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

        #inputdiv input, select {
            width: 13%;
        }

        #AddTR input[type=radio], input[type=checkbox] {
            margin-top: 3%;
        }

        .selectDisable {
            background-color: rgb(220, 220, 220);
        }

        #绑定信息Table img{
            cursor:pointer;
        }

        .GLCheck
        {
            margin-top:5%;
        }
    </style>

    <%=GetScript1_4_3 %>
    <%-- <script src="/portal/Scripts/jquery-1.8.0.min.js"></script>--%>
    <script src="../../Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script src="/portal/Scripts/layer-v2.0/layer/layer.js"></script>
    <link href="/portal/Scripts/layer-v2.0/layer/skin/layer.css" rel="stylesheet" />
    <script src="../../Scripts/MyJS.js"></script>
    <script src="../../Scripts/jqueryTimers.js"></script>
    <script type="text/javascript">
        
        var OptionModel = eval('[<%=OptionModelStr%>]')[0]
        var ReadExcelURL = "";
        var _UserID = '<%=UserID %>';
        var layerIndex = "";
        var SelectReportField = "";
        var UpdateField = "";
        var UpdateFieldList = [];
        var UserTableName = "";
        var SaveTableName = "";
        var MatchingFieldArr=[];
        var ReadSheetStr="";
        var ExcelFields_BD=[];
        var DataFields_BD=[];
        $(document).ready(function () {
            SetBackgroundColor();
            $("#标题").css("background-color",'rgb(220, 220, 220)');
            $("#标题").find("option[optionText='" + '<%=keyWordTitle%>' + "']").attr("selected",true);
            ChangeKey()
            $("#标题").attr("disabled",true)

            $(".ExcelInfo").find(".easyui-panel").panel({
                title: "导入文件信息"
            });
            var divFiles = $("#divFiles").find("tbody:eq(0)").find("tr:eq(0) td");
            $(divFiles[0]).css("width", "30%")
            $(divFiles[1]).css("line-height", "2%")
            $(divFiles[1]).css("width", "15%")
            var toAppend = $('.selectSheetTD');
            var AddTR = $('#AddTR');
            var selectSheetTD = $("<td></td>")
            selectSheetTD.html(toAppend[0])
            $(divFiles[1]).after(selectSheetTD);
            $("#divFiles").find("tbody:eq(0)").find("tr:eq(0)").after(AddTR);
            toAppend.show()
            AddTR.show()
            $(".tableChildList").hide();
            $("#form1").css("height", $(window).height() - 5)
            ChangeZT()
            $('body').oneTime('1ds',function(){
                $(".MatchingDiv").css("height", $("#form1").height() - $(".ExcelInfo").height()-35)
                $("#div_基本信息").css("height", $(".ExcelFieldInfo").css("height"))
                $("#ExcelFieldInfoPanel").css("height", $(".ExcelFieldInfo").height() - 5)
                $("#div_MatchingField").css("height", $(".MatchingField").css("height"))
                $("#MatchingFieldPanel").css("height", $(".MatchingField").height() - 5)
                //$(".Btn").css("margin-top", ($("#MatchingFieldPanel").height() / 4) + 'px')
            });
        });

        function DelField(f1,f2)
        {
            $("#TR_" + f1).remove()
            var Index= GetMatchingFieldAListIndex(f1)
            if(Index>-1) MatchingFieldArr.splice(Index,1);
             
            var FieldIndex = CheckFieldList(f2);
            if(FieldIndex>-1) UpdateFieldList.splice(FieldIndex,1)

            $('#ExcelFields_DIV').datalist('insertRow',{
                index:  $('#ExcelFields_DIV').datalist("getRows").length,	// 索引从0开始
                row: {
                    text: f1,
                }
            });
             
            $('#DataFields_DIV').datalist('insertRow',{
                index:  $('#DataFields_DIV').datalist("getRows").length,	// 索引从0开始
                row: {
                    text: f2,
                }
            });

        }
     
       

        function OtherFunciton() {

            var o = eval('[' + $("#<%=BaseResid%>_Json").val() + ']')
            if ($("[name=FileJson]").length == 0) {
                alert("请先上传Excel文件！")
                return false
            }

            if (o.length > 0) {
                var Docurl = o[0]['*DocHostName*']
                ReadExcelURL = Docurl;
                ReadExcelSheets()
            }
            else {
                alert("请先上传Excel文件！")
                return false
            }
            return true
        }


        function CheckHasField()
        {
            var o = eval('[' + $("#<%=BaseResid%>_Json").val() + ']')
            if ($("[name=FileJson]").length == 0) {
                alert("请先上传Excel文件！")
                return false
            }

            if (o.length > 0) {
                var Docurl = o[0]['*DocHostName*']
                ReadExcelURL = Docurl;
            }
            else {
                alert("请先上传Excel文件！")
                return false
            }
            return true
        }

        function SetGL(n)
        {
            
            if ($("#GL_"+n).attr("checked") == "checked") 
            {
                UpdateFieldList.push( {
                    fieldID: n,
                    fieldStr: n
                })
            }
            else
            {
                var FieldIndex = CheckFieldList(n);
                UpdateFieldList.splice(FieldIndex,1)
            }
        }

        function CheckFieldList(key)
        {
            for (var i = 0; i < UpdateFieldList.length; i++)
            {
                if (UpdateFieldList[i].fieldID == key)
                    return i;
            }
            return -1;
        }

        function ReadExcelSheets() {
            $(".MatchingDiv").css("height", $("#form1").height() - $(".ExcelFieldInfo").height()-35)
            var Docurl = ReadExcelURL
            var baseURL = "../Common/DoEventByExcel_ajax.aspx"
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
                        UpdateFieldList=[]
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

        function ReadExcelInfo() {
            var Docurl = ReadExcelURL
            $("#绑定信息Table").html("")
            var baseURL = "../Common/DoEventByExcel_ajax.aspx"

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
                         ExcelFields_BD = GetDataListJson(eval(obj[0].ExcelFields)[0]);
                          DataFields_BD = GetDataListJson(eval(obj[0].DataFields)[0]);
                          UpdateFieldList=[]
                        $('#ExcelFields_DIV').datalist({
                            data: ExcelFields_BD,
                            //checkbox: true,
                            //lines: true,
                            title: "待绑定字段",
                            height: '100%',
                            idField: "text",
                            valueField: "text",
                            textField: "text",
                            textFormatter: function (value, row, index) {
                                return value;
                            }
                        });


                        $('#DataFields_DIV').datalist({
                            data: DataFields_BD,
                            //checkbox: true,
                            //lines: true,
                            title: "原表字段",
                            height: '100%',
                            idField: "text",
                            valueField: "text",
                            textField: "text",
                            textFormatter: function (value, row, index) {
                                return value;
                            }
                        });

                        $(".Btn").show()
                        ChangeZT()
                    }
                },
                error: function (o) {
                    layer.close(index);
                    debugger
                }
            })
        }

        function SelectExcelSheet() {
            var s = $("#ReadExcelSheets").val();
            if (s == "") {
                alert("表名不能为空！")
                return;
            }
            ReadSheetStr=s;
            ReadExcelInfo()
        }

        function GetExcelFields_BDIndex(n)
        {
            for (var i = 0; i < ExcelFields_BD.length; i++) {
                if ( ExcelFields_BD[i].text==n)
                    return i
            }
            return -1
        }

        function GetDataFields_BDIndex(n)
        {
            for (var i = 0; i < DataFields_BD.length; i++) {
                if ( DataFields_BD[i].text==n)
                    return i
            }
            return -1
        }

        function CheckKey() {

            if (!CheckHasField()) return

            var obj=$('#ExcelFields_DIV').datalist("getRows")

            if(obj.length>0)
            {
                alert("您还有未绑定的字段，不能导入数据！")
                return false;
            }

             
            if ($("#xg").attr("checked") == "checked")
            {
                if($(".GLCheck:checked").length<1 || $(".GLCheck:checked").length > 5 )
                {
                    alert("修改模式下，必须指定关联字段，最多5个，最少1个！")
                    return false;
                }
            }

            if (UserTableName == "") {
                if ($("#显示标题").val() == "") {
                    alert("配置列表未选择！");
                    return false;
                }
                if ($("#关键字").val() == "") {
                    alert("关键字未填写！");
                    return false;
                }
                if ($("#资源ID").val() == "") {
                    alert("资源ID未填写！");
                    return false;
                }
            }

            if ($("#表名").val() == "") {
                alert("表名不能为空！");
                return false;
            }
            SaveTableName = $("#表名").val()
            return true;
        }

        function ChangeKey() {
            var s = $("#标题").val();
            $("#显示标题").val("");
            $("#表名").val("");
            $("#资源ID").val("");
            $("#关键字").val("");
            $("#UserTableName").removeAttr("checked")
            ChangeUserTableName()
            if (s != "") {
                $("#显示标题").val(s.split(",")[3]);
                $("#表名").val(s.split(",")[2]);
                $("#资源ID").val(s.split(",")[1]);
                $("#关键字").val(s.split(",")[0]);
                ChangeZT()
            }
        }


        function ChangeZT() {

            if ($("#xg").attr("checked") == "checked") {
                $("#SWGL").show()
                $(".GLCheck").parent().show()
            }
            else {
               
                $("#SWGL").hide()
                $(".GLCheck").parent().hide()
            }
        }


        function StartMatchingField() {
            if (!CheckKey())
                return
            var argTableResid = "<%=BaseResid%>"

            //var baseURL = window.location.protocol + "//" + window.location.host + "/webflow/Document/ToExcel/DoEventByExcel_ajax.aspx"

            var baseURL = "/portal/Base/Common/DoEventByExcel_ajax.aspx"

            var tip = "【" + $("#显示标题").val() + "】导入成功";

            var IsDown = false;

            var DC_Params = {
                argUserID: '<%=UserID%>'
            }

            var ms = "新增";
            var TableInfo = "";
            UpdateField = GetFieldListStr();
            if ($("#xg").attr("checked") == "checked")
                ms = "修改 (注：该模式下，关联字段的值不会被修改！)"

            if (UserTableName != "") {
                TableInfo = "自定义表名：" + $("#表名").val();
            }
            else {
                TableInfo = "列表配置：" + $("#显示标题").val() + "\n关键字：" + $("#关键字").val() + "\n存储表名：" + $("#表名").val() + "\n表资源ID：" + $("#资源ID").val()
            }

            if (!confirm("请确认以下信息，是否开始导入数据？\n\n模式：" + ms + "\n关联字段：" + UpdateField + "\n\n" + TableInfo ))
                return;

            OptionModel.KeyType = "CommonImportTable";
            OptionModel.ReadSheetStr = ReadSheetStr
            //argTableResid = $("#资源ID").val()
            if ($("#xg").attr("checked") == "checked") {
                if (UpdateField == "") {
                    alert("修改模式下，关联字段不能为空！")
                    return;
                }
            }
            else {
                UpdateField = "";
            }

            DC_Params.UpdateFieldListStr = UpdateField
            DC_Params.UserTableName = UserTableName
            DC_Params.SaveTableName = SaveTableName
            DC_Params.MatchingFieldArrStr = GetMatchingFieldAListStr()
             
            OptionModel.StartRowNum = 1;
            OptionModel.StartColumnNum = 1;
            OptionModel.FilePath = ReadExcelURL;
            OptionModel.HasTitle = true;

            DC_Params.GetExcelHelperOptionModelStr = JSON.stringify(OptionModel);
            DC_Params.argTableResid = argTableResid

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
                        if (IsDown) {
                            alert(Result.ErrorStr)
                        }
                        else {
                            alert(tip + Result.Result)
                        }
                    }
                    <%--  CommonCloseWindow('<%=OpenDiv%>', '<%=gridID%>', "")--%>
                },
                error: function (o) {
                    layer.close(index);
                    debugger
                }
            })
        }

        function GetFieldListStr() {
            var s = "";
            for (var i = 0; i < UpdateFieldList.length; i++) {
                if (s != "")
                    s += ","
                s += UpdateFieldList[i].fieldID
            }
            return s
        }

        function ChangeKey() {
            var s = $("#标题").val();
            $("#显示标题").val("");
            $("#表名").val("");
            $("#资源ID").val("");
            $("#关键字").val("");
            $("#UserTableName").removeAttr("checked")
            ChangeUserTableName()
            if (s != "") {
                $("#显示标题").val(s.split(",")[3]);
                $("#表名").val(s.split(",")[2]);
                $("#资源ID").val(s.split(",")[1]);
                $("#关键字").val(s.split(",")[0]);
                 ChangeZT()
            }
        }

        function ChangeUserTableName() {
            if ($("#UserTableName").attr("checked") == "checked") {
                $("#表名").removeAttr("readonly")
                UserTableName = "1";
                if ($("#xg").attr("checked") == "checked") {
                    $("#GetFieldSpan").show();
                }
                else
                {
                    $("#GetFieldSpan").hide();
                }
            } else {
                $("#表名").attr("readonly", "readonly")
                UserTableName = "";
                $("#GetFieldSpan").hide();
            }
            SetBackgroundColor();
        }

        function AutoMatchingField()
        {
            var obj=$('#ExcelFields_DIV').datalist("getRows")

            var h="";
            var NoFitNum=0;

            for(var i=obj.length-1;i>=0;i--)
            {
                var t=obj[i].text;
                var f2_Index= $('#DataFields_DIV').datalist("getRowIndex",t) 

                if(f2_Index==-1)
                {
                    NoFitNum ++
                }
                else
                {
                    $('#ExcelFields_DIV').datalist("deleteRow",i)
                    $('#DataFields_DIV').datalist("deleteRow",f2_Index)

                    MatchingFieldArr.push( {
                        ExcelFieldName:t,
                        DataFieldName:t
                    })

                    h +=" <tr id='TR_" + t + "' style='height: 30px; text-align: center'>";
                    h +="<td><input class='GLCheck' onchange='SetGL(\"" + t + "\")' type='checkbox' id='GL_" + t + "' /></td>";
                    h +="<td>"+ t + "</td>";
                    h +="<td>"+ t + "</td>";
                    h +="<td  style='padding-top: 1%;'><img src='../../images/del.gif' onclick='DelField(\"" + t + "\",\"" + t + "\")' /></span></td></tr>";
                }
            }
               
            // $('#ExcelFields_DIV').datalist('loadData',{total:0,rows:[]});
            // $('#DataFields_DIV').datalist('loadData',{total:0,rows:[]});

            //$('#ExcelFields_DIV').datalist("deleteRow",i)
            //$('#DataFields_DIV').datalist("deleteRow",f2_Index)

            if( $("#绑定信息Table tr").length==0)
                $("#绑定信息Table").html(h)
            else
                $("#绑定信息Table tr:eq(-1)").after(h)

            ChangeZT()
        }


        function XZMB() {
            var bt='<%=keyWordTitle%>';
            selectFieldByResid("【" + bt + "】", <%=BaseResid%>, $("#关键字").val())
        }

        function ClearMatchingField()
        {
            ReadExcelInfo();
        }

        function MatchingField()
        {
            
            if($('#ExcelFields_DIV').datalist("getSelected")==null||   $('#DataFields_DIV').datalist("getSelected")==null)
            {
                alert("请选择一个字段！")
                return 
            }

            var  f1= $('#ExcelFields_DIV').datalist("getSelected").text
            var  f2= $('#DataFields_DIV').datalist("getSelected").text
            
            if(f1=="" || f2=="")
            {
                alert("请选择一个字段！")
                return 
            }
            var f1_Index= $('#ExcelFields_DIV').datalist("getRowIndex",$('#ExcelFields_DIV').datalist("getSelected")) 
            var f2_Index= $('#DataFields_DIV').datalist("getRowIndex",$('#DataFields_DIV').datalist("getSelected")) 
            $('#ExcelFields_DIV').datalist("deleteRow",f1_Index)
            $('#DataFields_DIV').datalist("deleteRow",f2_Index)

            MatchingFieldArr.push( {
                ExcelFieldName:f1,
                DataFieldName:f2
            })

            var h =" <tr id='TR_" + f1 + "' style='height: 30px; text-align: center'>";
            h +="<td><input class='GLCheck' onchange='SetGL(\"" + f2 + "\")' type='checkbox' id='GL_" + f2 + "' /></td>";
            h +="<td>"+ f1 + "</td>";
            h +="<td>"+ f2 + "</td>";
            h +="<td  style='padding-top: 1%;'><img src='../../images/del.gif' onclick='DelField(\"" + f1 + "\",\"" + f2 + "\")' /></span></td></tr>";

            if( $("#绑定信息Table tr").length==0)
                $("#绑定信息Table").html(h)
            else
                $("#绑定信息Table tr:eq(-1)").after(h)

            ChangeZT()
        }


        function GetMatchingFieldAListStr()
        {
            var fieldstr="";
            for (var i = 0; i < MatchingFieldArr.length; i++) {
                if ( fieldstr !="")
                    fieldstr +=","
                fieldstr += (MatchingFieldArr[i].ExcelFieldName + "#" + MatchingFieldArr[i].DataFieldName)
            }
            return fieldstr
        }

          
        function GetMatchingFieldAListIndex(ExcelFieldName)
        {
            //DataFieldName
            for (var i = 0; i < MatchingFieldArr.length; i++) {
                if ( MatchingFieldArr[i].ExcelFieldName==ExcelFieldName)
                    return i
            }
            return -1
        }

        
        function selectFieldByResid(Title, Resid, KeyWord) {
            parent.OpenLayerByUrl(Title + ' - 选择字段页面', 'Base/CommonPage/SelectReportField.aspx?HideQueryBox=1&BaseResid=' + Resid + '&SearchTitle=' + Title + '&BaseKeyWordValue=' + KeyWord + '&UpdateField=' + "" + '&UserTableName=' + "" + '&SaveTableName=' + "", '900px', '500px', "CommonSearch")
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ExcelInfo">
            <div class="ExcelInfoPanel">
                <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width: 100%;">
                    <tr>
                        <td colspan="3" valign="middle">&nbsp;
                           <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',plain:true,disabled:false" onclick="StartMatchingField()" id="DRSJ">导入Excel数据</a> &nbsp;&nbsp;  
                       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',plain:true,disabled:false" onclick="XZMB()" id="XZMB">生成导入模板</a>
                        </td>
                    </tr>

                </table>
                <uc1:UploadFile runat="server" ID="UploadFile1" />
                <span class="selectSheetTD" style="display: none">
                    <span class="selectSheet">请选择表：<select id="ReadExcelSheets">
                    </select>
                    </span>

                    <a href="javascript:void(0)" class="easyui-linkbutton" style="margin-top: 1%; margin-right: 2%" data-options="iconCls:'icon-excel',plain:true,disabled:false" onclick="SelectExcelSheet()" id="AddField">读取表信息</a>

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

                        <div class="Btn" style="margin-top: 6%; display: none">
                            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',iconAlign:'top',plain:true,disabled:false" onclick="AutoMatchingField()" id="AutoMatchingField">自行匹配</a>
                            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',iconAlign:'top',plain:true,disabled:false" onclick="MatchingField()" style="margin-top: 5%" id="MatchingField">添加匹配</a>

                            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',iconAlign:'top',plain:true,disabled:false" onclick="ClearMatchingField()" style="margin-top: 5%" id="ClearMatchingField">清空匹配</a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ExcelFieldInfo">
                <div id="div_基本信息" title="绑定信息" class="easyui-panel" style="overflow-x: hidden; overflow-y: auto; padding: 5px; margin: 0px; width: 99%">
                    <div class="easyui-panel" border="true" id="ExcelFieldInfoPanel">
                        <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table2">
                            <tr style="height: 30px; text-align: center">
                                 <th style="width: 15%; text-align: center; padding: 0px" id="SWGL">设为关联
                                </th>
                                <th style="width: 35%; text-align: center; padding: 0px">Excel字段名
                                </th>
                                <th style="width: 35%; text-align: center; padding: 0px">数据库字段名
                                </th>
                                <th style="width: 20%; text-align: center; padding: 0px">操作
                                </th>
                            </tr>
                            <tbody id="绑定信息Table">
                              <%-- <tr style="height: 30px;text-align: center;">
                                    <td><input type="checkbox" id="GL_" />
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td>Excel字段名
                                    </td>
                                    <td style="padding-top: 1%;"> 
                                      
                                            <img src="../../images/del.gif" />    </td>
                                </tr>--%>
                            </tbody>
                        </table>
                        <table style="display: none">
                            <tr style="height: 29px; display: none" id="AddTR">
                                <th style="width: 100px;">模式选择</th>
                                <td style="width: 90%;" colspan="3">
                                    <div style="width: 20%; float: left; margin-left: 2%;">
                                        <input type="radio" id="xz" name="r" checked="checked" onchange="ChangeZT()" />新增
                                 <input type="radio" id="xg" name="r" onchange="ChangeZT()" />修改

                                        <span style="display: none">
                                            <input type="checkbox" style="margin-left: 10%;" id="UserTableName" onchange="ChangeUserTableName()" />使用表名</span>
                                        <span id="GetFieldSpan" style="display: none"><a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-excel',plain:true,disabled:false" onclick="GetField()" id="GetField">获取自定义表字段</a></span>
                                    </div>

                                    <div id="inputdiv" style="margin-left: 5%; width: 70%; float: right">
                                        配置列表:
                                <select id="标题" style="margin-left: 1%; margin-right: 2%;width:80%" class="box3" onchange="ChangeKey()" >
                                  <%=optionKeyStr %>
                                </select>
                                        <%--关键字:--%>
                                <input type="hidden" style="margin-left: 1%; margin-right: 2%;" class="box3" id="关键字"  readonly="readonly" />

                                        <input type="hidden" id="显示标题" />

                                       <%-- 存储表名:--%>
                                <input type="hidden" style="margin-left: 1%; margin-right: 2%;" class="box3" id="表名" readonly="readonly" />
                                      <%--  资源ID:  --%> 
                                <input type="hidden" style="margin-left: 1%;" id="资源ID" readonly="readonly" class="box3" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
