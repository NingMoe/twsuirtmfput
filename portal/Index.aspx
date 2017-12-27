<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>三盟企业管理平台</title>
    <link href="CSS/Index.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.4.3/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.4.3/themes/icon.css"/> 
    <link rel="stylesheet" type="text/css" href="Scripts/jquery-easyui-1.4.3/demo/demo.css"/> 
    <script type="text/javascript" src="Scripts/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
</head>
<body class="easyui-layout" data-options="fit:true,scroll:'no'">

    <script type="text/javascript">

        var newMessageindex = 0;
        var _UserID = "<%=oUserInfo.ID %>"
        var _GridObj;
        var _hidExpCondition = "";
        var gridID = "";
        var ChildgridID = "";
        var FatherJsonValue = [];
        var bodyWidth = document.documentElement.clientWidth;
        var bodyHeight = document.documentElement.clientHeight;
        var onlyOpenTitle = "<%=resourceInfoTitle %>";
        $(document).ready(function () {

            //初始化时加载树节点
            getTreeNode('<%=resourceInfoID %>', onlyOpenTitle);

            $("body").css("width", bodyWidth);


            if ('<%=OpenOrCloseMessageDeliveryState%>'.toLowerCase() == 'true') GetNewShowMessageID()

            //浏览器窗口变化时,改变大小
            $(window).resize(function () {
                $(document.body).layout('resize');
            });

            $(document.body).layout('panel', 'west').panel({
                onCollapse: function () {
                    var newWindt = bodyWidth;
                    $(document.body).layout('panel', 'center').panel('resize', { width: newWindt });
                },
                onExpand: function () {
                    $(document.body).layout('resize');
                }
            });

            var tabs_content = $("#content");
            tabs_content.tabs({
                border: false,
                fit: true
            });

            //点击顶部导航切换时展示，子菜单
            $("#topNav ul li a").click(function () {
                $(".nav li a.selected").removeClass("selected")
                $(this).addClass("selected");

                //加载树节点
                getTreeNode($(this).attr("id"), $(this).attr("title"));
            });


            //点击一级导航菜单，加载树节点
            function getTreeNode(_resId, _resName) {
                $("#tree_menu").tree({
                    animate: true,
                    dnd: true,
                    url: "Base/Common/JQueryCallService.aspx?typeValue=getTreeJson&ResID=" + _resId + "&UserID=cs&ResNmae=" + _resName,
                    onLoadSuccess: function (node, data){
                        if (data != "") {
                            var newTitle = _resName;
                            $("#westModel_panel").panel({ title: newTitle });
                        }
                        $("#westModel_panel ul:first li:first ul:first li:first div:first").click();
                    },
                    onClick: function (node) {
                        //菜单打开方式
                        var nodeTarget = node.attributes.urlType;
                        //菜单url路径
                        var nodeUrl = node.attributes.urlName;
                        if (nodeTarget == "_parent") {
                            var exists = $('#content').tabs('exists', node.text);
                            if (!exists) {//点击的树节点，如果不存在就新增选项卡
                                if (nodeUrl != "" && nodeUrl != undefined) {
                                    addTabs(node);
                                }
                            } else {//如果存在就选中 
                                tabs_content.tabs("select", node.text);
                            }
                        } else {
                            if (nodeUrl != "" && nodeUrl != undefined) {
                                window.open(nodeUrl, 'MenuWindow');
                            }
                        }
                    }
                });
            }
            var centerHeight = $('#content').height();
            //添加tab页面
            function addTabs(tab) {
                tabs_content.tabs("add", {
                    title: tab.text,
                    iconCls: tab.iconCls,
                    fit: true,
                    border: false,
                    cls: 'pd3',
                    id: 'fromTab',
                    closable: true,
                    content: '<iframe  frameborder="no"  name="formList" scrolling="no" marginheight="0" marginwidth="0" width="100%" height="100%"  src="' + tab.attributes.urlName + '"></iframe>'
                });
            }

            //添加欢迎页面           
            //addTabs({
            //    iconCls: 'icon-home',
            //    text: onlyOpenTitle,
            //    attributes: { 'urlName': 'Welcome.aspx' }
            //});


            //监听右键事件，创建右键菜单
            $('#content').tabs({
                onContextMenu: function (e, title, index) {
                    e.preventDefault();
                    if (index > 0) {
                        $('#mm').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        }).data("tabTitle", title);
                    }
                }
            });
            //右键菜单click
            $("#mm").menu({
                onClick: function (item) {
                    closeTab(item.id);
                }
            });

        });

        //右键菜单关闭事件
        function closeTab(action) {
            var alltabs = $('#content').tabs('tabs');
            var currentTab = $('#content').tabs('getSelected');
            var allTabtitle = [];
            $.each(alltabs, function (i, n) {
                allTabtitle.push($(n).panel('options').title);
            });

            switch (action) {
                case "refresh":
                    var iframe = $(currentTab.panel('options').content);
                    var src = iframe.attr('src');
                    for (var i = 0; i < window.frames.length; i++) {
                        if (window.frames[i].document.location.href.indexOf(src) != -1) {
                            window.frames[i].document.location.href = src;
                        }
                    }
                    //iframe.$('#dg-EMDJL').datagrid("reload");
                    //currentTab.panel('refresh', src);
                    //                    $('#content').tabs('update', {
                    //                        tab: currentTab,
                    //                        options: {
                    //                            content: createFrame(src)
                    //                        }
                    //                    })
                    break;
                case "closeall":
                    $.each(allTabtitle, function (i, n) {
                        if (n != onlyOpenTitle) {
                            $('#content').tabs('close', n);
                        }
                    });
                    break;
                case "closeother":
                    var currtab_title = currentTab.panel('options').title;
                    $.each(allTabtitle, function (i, n) {
                        if (n != currtab_title && n != onlyOpenTitle) {
                            $('#content').tabs('close', n);
                        }
                    });
                    break;
            }
        }

        function reloadPage() {
            _GridObj.datagrid("reload");
        }

        //弹出Window 窗口
        function fnParentFormListDialog(url, DialogWidth, DialogHeight, title, RefreshGridDiv) {
            if (DialogWidth==0) {
                DialogWidth = 800;
            }
            var W = document.documentElement.clientWidth - 100;
            if (DialogHeight == 0) DialogHeight = parent.$("#westModel_panel").height() + 10;
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
                cache: true,
                closed: true,
                shadow: false,
                closable: true,
                draggable: true,
                resizable: false,
                modal: true,
                onClose: function () {
                    $('#FromInfo:gt(0)').remove();
                    var selTab = $('#content').tabs('getSelected');
                    var iframe = $(selTab.panel('options').content);
                    var src = iframe.attr('src');
                    for (var i = 0; i < window.frames.length; i++) {
                        if (window.frames[i].document.location.href.indexOf(src) != -1) {
                            var srcHref = window.frames[i].document.location.href;
                            if (srcHref.substring(srcHref.length - 1, srcHref.length) == "#") {
                                srcHref = srcHref.substring(0, srcHref.length - 1);
                            }
                            //window.frames[i].document.location.href = srcHref + "&time=<%=DateTime.Now.ToString() %>";
                            window.frames[i].$('#' + RefreshGridDiv).datagrid("reload");
                        }
                    }
                }
            });
            
            var index = url.indexOf("?");
            var gridIDstr = ""
            var ChildgridIDstr = ""
            var opendivstr = "";
            if (gridID != "" && url.indexOf("gridID") == -1) {
                gridIDstr = "&gridID=" + gridID
            }

            if (ChildgridID != "" && url.indexOf("ChildgridID") == -1) {
                ChildgridIDstr = "&ChildgridID=" + ChildgridID
            }

            if (url.toLowerCase().indexOf("openDiv") == -1) {
                opendivstr = "&OpenDiv=divParentContent"
            }

            if (index != "-1") {
                url = url + "&height=" + DialogHeight + opendivstr + gridIDstr + ChildgridIDstr;
            } else {
                url = url + "?height=" + DialogHeight + opendivstr + gridIDstr + ChildgridIDstr;
            }
            $('#FromInfo')[0].src = encodeURI(url);
            $('#divParentContent').dialog('open'); 
        }


        function closeWindow1() {
            $('#divParentContent').dialog('close');

        }

        //关闭窗口
        function ParentCloseWindow() {
            $('#divParentContent').dialog('close');
        }

    </script>
    <div data-options="region:'north',split:false" style="height: 40px; overflow: hidden; border: 0; margin-bottom: 1px;">
        <!--顶部导航菜单开始-->
        <div id="topNav" style="height: 39px; border-bottom: 1px solid #c9e6f9; background-color: #DDDDDD;">
            <div class="topleft">
                <a href="javascript:" target="_parent">
                    <img src="images/logo.png" title="永程固废" /></a>
            </div>
            <ul class="nav">
                <asp:Repeater runat="server" ID="titleRepeater">
                    <ItemTemplate>
                        <li>
                            <a href="javascript:" title="<%#Eval("Name")%>" id="<%#Eval("ID")%>">
                                <h2><%#Eval("Name")%></h2>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>  
            <div style="float:right;width:180px;" class="topright1" >
                <ul>
                   <%-- <li><span>
                        <img src="images/Icon/help.png" title="帮助" class="helpimg" /></span><a href="javascript:void(0);">帮助</a></li>--%>
                    <li onclick="fnParentFormListDialog('Base/CommonPage/EidtPwd.aspx', 530,290,'修改密码');"><span>
                        <img src="images/Icon/pwd.png" title="密码" class="helpimg" /></span><a href="javascript:" onclick="fnParentFormListDialog('Base/EidtPwd.aspx', 530,290,'修改密码');">密码</a></li>
                    <li onclick="if(window.confirm('您确定要退出？')){window.location.href='login.aspx';}"><span>
                        <img src="images/Icon/Exit.png" title="退出" class="helpimg" /></span><a href="javascript:void(0);">退出</a></li>
                </ul>
            </div>
            <div class="topright">
            <div class="user"><span><%=CurrentUser.Name %></span>
                <%--<i>待办</i><b><%=DBSXNum %></b>--%>
            </div>
            </div>
        </div>
        <!--顶部导航菜单结束-->
    </div>

    <div data-options="region:'west',split:false,iconCls:'icon-Tree'" title="菜单" id="westModel_panel" style="width: 185px;">
        <ul class="easyui-tree" id="tree_menu"></ul>
    </div>

    <div id="centerModel_id" data-options="region:'center'">
        <div class="easyui-tabs" id="content"></div>
    </div>
    <div id="mm" class="easyui-menu" style="width: 110px;">
        <div data-options="iconCls:'icon-reload'" id="refresh">刷新</div>
        <div class="menu-sep"></div>
        <div data-options="iconCls:'icon-no'" id="closeall">全部关闭</div>
        <div class="menu-sep"></div>
        <div data-options="iconCls:'icon-cut'" id="closeother">除此外关闭</div>
    </div>
    <div closed="true" class="easyui-window" id="divParentContent" style="overflow: hidden;" />
    <div closed="true" class="easyui-window" id="GetNewMessageOfHomePage" style="overflow: hidden;" />
    <div closed="true" class="easyui-window" id="divDictionaryContent" style="overflow: hidden;" />
    <div closed="true" class="easyui-window" id="divZSFAContent" style="overflow: hidden;" />
    <input type="hidden" value="" id="DictionaryConditon" />
    <input type="hidden" value="" id="CheckTitleResID" />
    <input type="hidden" value="" id="childinfo" />
    <input type="hidden" value="" id="ajaxinfo" />
    <input id="hidCheckResID" value="<%=resourceInfoID %>" type="hidden" />
    <style type="text/css">
        /**重写 EasyUi Tree节点样式 开始**/
        #tree_menu li {
            margin: 0;
            list-style: none;
        }

            #tree_menu li div {
                margin: 0;
                padding-top: 5px;
                list-style: none;
                border-bottom: 1px dotted #DEDEDE;
                line-height: 27px;
                height: 27px;
            }

        .tree-expanded {
            float: right;
            background: url('images/icon/rowong.png') no-repeat -5px -2px;
        }

        .tree-collapsed {
            float: right;
            background: url('images/Icon/show.png') no-repeat -12px -8px;
        }

        .tree-folder {
            margin-left: 15px;
        }

        .tree-icon {
            background: url('images/Icon/menu.png') no-repeat -2px -1px;
        }

        #fromTab {
            height: 100%;
            overflow: hidden;
            border: none;
        }
        /**重写 EasyUi Tree节点样式 结束**/
    </style>
</body>
</html>
