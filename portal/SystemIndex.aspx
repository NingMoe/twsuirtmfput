<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemIndex.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>     
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
	<title>三盟企业管理平台</title>
	<link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.4.3/themes/gray/easyui.css" />
	<link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.4.3/themes/icon.css" /> 
	<script type="text/javascript" src="Scripts/jquery-easyui-1.4.3/jquery.min.js"></script>
	<script type="text/javascript" src="Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script> 
</head>
<body class="easyui-layout" data-options="fit:true,scroll:'no'">
    <script type="text/javascript">

        var newMessage = 0;
        var openwindows;
        var openwindowsByNewMessag;
        var newMessageindex = 0;
        var NewGet = 0;
        var mask;
        var IsHomebool = false;
        var IsHomeMessage = false;
        var RefreshNodeID = "";
        var RefreshGridDiv = "";
        var SeniorSearchStr = "";

        function setWestModelUrl(ResID, Url) {
            //debugger
            $("#CheckTitleResID").val(ResID);
            //给隐藏域赋值
            $("#hiddenNavigationId").val("Navigation" + ResID);
            var myDate = new Date();
            var time = "" + myDate.getFullYear() + myDate.getMonth() + myDate.getDate() + myDate.getHours() + myDate.getMinutes() + myDate.getSeconds() + myDate.getMilliseconds();

            if (Url.trim() == "") {
                Url = "Base/SystemConfig/ResourceTree.aspx?ResID=<%=resourceInfoID  %>&PID=";
            } 
            $('#westModel_id').load(encodeURI(Url), function () {
                $.parser.parse("#westModel_id");
                parent.$.messager.progress('close');
            });
        }

        $(document).ready(function () {
            var northChageWidth = 1200;
            var FrameWidth = document.documentElement.clientWidth;
            if (FrameWidth > northChageWidth) {
                northChageWidth = FrameWidth;
            }
            $("body").css("width", northChageWidth);
            //$("#westModel_id").css("height", $("#centerModel_id").height());
            $('#westModel_id').load("Base/SystemConfig/ResourceTree.aspx?ResID=<%=resourceInfoID  %>&PID=", function () {
                $.parser.parse("#westModel_id");
            });
            $("#view_frame").css("width", $("#centerModel_id").width());
            $("#view_frame").css("height", $("#centerModel_id").height());
              
        });        
        function fnParentFormListDialog(url, DialogWidth, DialogHeight, title) {
            var W = document.documentElement.clientWidth - 100;
            if (DialogHeight == 0) DialogHeight = parent.$("#westModel_id").height() + 10;
            if (DialogWidth == 0) {
                DialogWidth = W;
            }
            else {
                if (DialogWidth > W) DialogWidth = W;
            }
            $('#divParentContent').append($("<iframe scrolling='no' id='FromInfo' frameborder='0' marginwidth='0' marginheight='0' style='width:100%;height:100%;'></iframe")).dialog({
                title: title,
                width: DialogWidth,
                height: DialogHeight,
                cache: false,
                closed: true,
                shadow: false,
                closable: true,
                draggable: true,
                resizable: false,
                modal: true
            });
            $('#FromInfo')[0].src = encodeURI(url);
            $('#divParentContent').dialog('open');
        }
 
        function ParentCloseWindow() {
            $('#divParentContent').dialog('close');
        }
        function RefreshGrid(ControlsID) {
            $("#view_frame")[0].contentWindow.fnGridLoad(ControlsID);
        }
        function GetSelectedDataID(KeyWord) {
           return $("#view_frame")[0].contentWindow.GetSelectedDataID(KeyWord);
        } 
    </script>

      <div data-options="region:'north',border:false,href:'Base/SystemConfig/NorthModel.aspx'" style="height:110px;overflow:hidden; "> </div>
	<div data-options="region:'west',split:false,title:'菜单栏',iconCls:'icon-search'"  id="westModel_id"  style="width:185px;"></div>	 
	<div data-options="region:'center'"  style="overflow: hidden;border:none; " id="centerModel_id" >
        <iframe id="view_frame" name="formList"  marginwidth='0' marginheight='0'  frameborder="0" scrolling="no"></iframe>
	</div> 
    
    <!-- <div region="south" style="overflow:hidden;" class="indexSoutchDiv"   > 
        <div style="width:100%;padding:0px; text-align:center;">-版权所有 © 2013 西安三盟软件科技有限公司</div>
    </div>-->
    <div closed="true" class="easyui-window" id="divParentContent" style="overflow: hidden;" />
     <div closed="true" class="easyui-window" id="GetNewMessageOfHomePage" style="overflow: hidden;" />
    <div closed="true" class="easyui-window" id="divDictionaryContent" style="overflow: hidden;" />
    <div closed="true" class="easyui-window" id="divZSFAContent" style="overflow: hidden;" />
    <input type="hidden" value="" id="DictionaryConditon" />
    <input type="hidden" value="" id="CheckTitleResID" />
    <input type="hidden" value="" id="childinfo" />
    <input type="hidden" value="" id="ajaxinfo" />
</body>
</html>
