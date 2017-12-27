<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
      <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
   
    <title>三盟企业管理平台</title>
    <style type="text/css">
        body {
            font: 12px/20px "微软雅黑", "宋体", Arial, sans-serif, Verdana, Tahoma;
            padding: 0;
            margin: 0;
        }

        a:link {
            text-decoration: none;
        }

        a:visited {
            text-decoration: none;
        }

        a:hover {
            text-decoration: underline;
        }

        a:active {
            text-decoration: none;
        }

        .cs-north {
            height: 60px;
        }

        .cs-north-bg {
            width: 100%;
            height: 100%;
            background: url(../../css/themes/gray/images/header_bg.png) repeat-x;
        }

        .cs-north-logo {
            height: 40px;
            margin: 15px 0px 0px 5px;
            display: inline-block;
            color: #000000;
            font-size: 22px;
            font-weight: bold;
            text-decoration: none;
        }

        .cs-west {
            width: 200px;
            padding: 0px;
        }

        .cs-south {
            height: 25px;
            background: url('../../css/themes/pepper-grinder/images/ui-bg_fine-grain_15_ffffff_60x60.png') repeat-x;
            padding: 0px;
            text-align: center;
        }

        .cs-navi-tab {
            padding: 5px;
        }

        .cs-tab-menu {
            width: 120px;
        }

        .cs-home-remark {
            padding: 10px;
        }

        .wrapper {
            float: right;
            height: 30px;
            margin-left: 10px;
        }

        .ui-skin-nav {
            float: right;
            padding: 0;
            margin-right: 10px;
            list-style: none outside none;
            height: 22px;
        }

            .ui-skin-nav .li-skinitem {
                float: left;
                font-size: 12px;
                line-height: 22px;
                margin-left: 10px;
                text-align: center;
            }

                .ui-skin-nav .li-skinitem span {
                    cursor: pointer;
                    width: 10px;
                    height: 10px;
                    display: inline-block;
                }

                    .ui-skin-nav .li-skinitem span.cs-skin-on {
                        border: 1px solid #FFFFFF;
                    }

                    .ui-skin-nav .li-skinitem span.gray {
                        background-color: gray;
                    }

                    .ui-skin-nav .li-skinitem span.pepper-grinder {
                        background-color: #BC3604;
                    }

                    .ui-skin-nav .li-skinitem span.blue {
                        background-color: blue;
                    }

                    .ui-skin-nav .li-skinitem span.cupertino {
                        background-color: #D7EBF9;
                    }

                    .ui-skin-nav .li-skinitem span.dark-hive {
                        background-color: black;
                    }

                    .ui-skin-nav .li-skinitem span.sunny {
                        background-color: #FFE57E;
                    }
    </style>


</head>
<body class="easyui-layout">
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
            $("#CheckTitleResID").val(ResID);
            //给隐藏域赋值
            $("#hiddenNavigationId").val("Navigation" + ResID);
            var myDate = new Date();
            var time = "" + myDate.getFullYear() + myDate.getMonth() + myDate.getDate() + myDate.getHours() + myDate.getMinutes() + myDate.getSeconds() + myDate.getMilliseconds();

            if (Url.trim() == "") {
                Url = "Base/CommonPage/WestModel.aspx?ResID=" + ResID + "&temp=" + time;
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
            $('#westModel_id').load("Base/CommonPage/WestModel.aspx?ResID=<%=resourceInfoID %>", function () {
                $.parser.parse("#westModel_id");
            });
            $("#view_frame").css("width", $("#centerModel_id").width());
            $("#view_frame").css("height", $("#centerModel_id").height());

           
            if ('<%=OpenOrCloseMessageDeliveryState%>'.toLowerCase() == 'true')  GetNewShowMessageID()
        });
         
        function fnParentFormListDialog(url, DialogWidth, DialogHeight, title) {
            if (DialogHeight == 0) DialogHeight = parent.$("#westModel_id").height() + 10;
            if (DialogWidth == 0) {
                DialogWidth = document.documentElement.clientWidth - 190;
                if (DialogWidth > 1200) DialogWidth = 1200;
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
                resizable: true,
                onClose: function () {
                    $('#FromInfo:gt(0)').remove();
                    if ($("#childinfo").val() != "") {
                        $("#childinfo").val("");

                        if (callajax) {
                            Beforcloseajax(infoofchild[0]["ajaxurl"], infoofchild[0]["successinfo"]);
                        }
                        else {
                            if (IsHomebool) {
                            }
                            IsHomebool = false
                        }
                    }
                },
                modal: true
            });
            var index = url.indexOf("?");
            if (index != "-1") {
                url = url + "&height=" + DialogHeight;
            } else {
                url = url + "?height=" + DialogHeight;
            }
            $('#FromInfo')[0].src = encodeURI(url);
            openwindows = $('#divParentContent').dialog('open');

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
         

            function Beforcloseajax(ajaxurl, successinfo) {
                var jsonStr1 = $("#ajaxinfo").val();
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: { "Json": "" + jsonStr1 + "" },
                    url: ajaxurl,
                    success: function (obj) {

                        if (obj.success || obj.success == "true") {
                            // alert(successinfo + "成功！");

                        } else {
                            alert(successinfo + "失败！");
                        }
                        callajax = false;
                    }
                });
            }
         
    </script>
    <div region="north" style="height: 100px; overflow: hidden; border-bottom: 0px;" href="NorthModel.aspx"></div>

    <div region="west" id="westModel_id" split="false" style="width: 185px; padding: 1px; overflow: hidden;">
    </div>
    <div region="center" border="false" style="overflow: hidden; line-height: 18px;" id="centerModel_id">
        <iframe id="view_frame" marginwidth='0' marginheight='0' style="line-height: 18px;" frameborder="0" scrolling="no"></iframe>
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
