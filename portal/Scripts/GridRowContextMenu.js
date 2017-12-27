var createGridHeaderContextMenu = function (e, field) {
    e.preventDefault();
    var grid = $(this);/* grid本身 */
    var headerContextMenu = this.headerContextMenu;/* grid上的列头菜单对象 */
    if (!headerContextMenu) {
        var tmenu = $('<div style="width:100px;"></div>').appendTo('body');
        var asc = $('<div iconCls="icon-asc" field="asc">升序</div>').appendTo(tmenu);
        var desc = $('<div iconCls="icon-desc" field="desc">降序</div>').appendTo(tmenu);
        var filedHTML = $('<div iconCls="icon-columns"></div>');
        var span = $('<span>显示列/隐藏列</span>');
        var spdiv = $('<div></div>');
        var fields = grid.datagrid('getColumnFields');
        for (var i = 0; i < fields.length; i++) {
            var fildOption = grid.datagrid('getColumnOption', fields[i]);
            if (!fildOption.hidden) {
                $('<div style="width:100px;" iconCls="icon-checked" field="' + fields[i] + '"/>').html(fildOption.title).appendTo(spdiv);
            } else {
                $('<div style="width:100px;" iconCls="icon-unchecked" field="' + fields[i] + '"/>').html(fildOption.title).appendTo(spdiv);
            }
        }
        span.appendTo(filedHTML);
        spdiv.appendTo(filedHTML);
        filedHTML.appendTo(tmenu);
        headerContextMenu = this.headerContextMenu = tmenu.menu({
            onClick: function (item) {
                var f = $(this).attr('field')
                var fieldProperty = $(item.target).attr('field');
                if (item.iconCls == 'icon-checked') {
                    grid.datagrid('hideColumn', fieldProperty);
                    $(this).menu('setIcon', {
                        target: item.target,
                        iconCls: 'icon-unchecked'
                    });
                }
                if (item.iconCls == 'icon-unchecked') {
                    grid.datagrid('showColumn', fieldProperty);
                    $(this).menu('setIcon', {
                        target: item.target,
                        iconCls: 'icon-checked'
                    });
                }
                if (item.iconCls == 'icon-asc') {
                    var options = grid.datagrid('options');
                    options.sortName = f;
                    options.sortOrder = fieldProperty;
                    grid.datagrid('reload');
                }
                if (item.iconCls == 'icon-desc') {
                    var options = grid.datagrid('options');
                    options.sortName = f;
                    options.sortOrder = fieldProperty;
                    grid.datagrid('reload');
                }
            }
        });
    }
    headerContextMenu.attr('field', field);
    headerContextMenu.menu('show', {
        left: e.pageX,
        top: e.pageY
    });
};

var RowContextMenu_rowIndex = 0
var RowContextMenu_rowData = []

var createGridRowContextMenu = function (e, rowIndex, rowData) {

    if (_RowContextMenu != null && _RowContextMenu.length > 0) {
        e.preventDefault();
        var grid = $(this);/* grid本身 */
        var rowContextMenu = this.rowContextMenu;/* grid上的列头菜单对象 */
        grid.datagrid('unselectAll').datagrid('selectRow', rowIndex);

        if (Initialize(e, rowIndex, rowData, _keyWordValue, _RowContextMenu[0].KeyWord)) {
            if (!rowContextMenu) {
                RowContextMenu_rowIndex = rowIndex;
                RowContextMenu_rowData = rowData;
                var tmenu = $('<div style="width:' + _MaxMenuWidth + 'px;"></div>').appendTo('body');
                var filedHTML;
                var span;
                var spdiv;
                for (var i = 0; i < _RowContextMenu.length; i++) {

                    if (_RowContextMenu[i].ChildRowContextMenu != null && _RowContextMenu[i].ChildRowContextMenu.length > 0) {
                        filedHTML = $('<div  style="width:' + _RowContextMenu[i].MenuWidth + 'px;" iconCls="' + _RowContextMenu[i].MenuIcon + '">' + "" + '</div>');
                        span = $('<span>' + _RowContextMenu[i].MenuText + '</span>');
                        spdiv = $('<div></div>');

                        for (var j = 0; j < _RowContextMenu[i].ChildRowContextMenu.length; j++) {

                            spdiv.append("<div  style='width:" + _RowContextMenu[i].ChildRowContextMenu[j].MenuWidth + "px;' iconCls='" + _RowContextMenu[i].ChildRowContextMenu[j].MenuIcon + "' onclick=" + _RowContextMenu[i].ChildRowContextMenu[j].MenuEvent.replace(/\|\|/g, "'") + " >" + _RowContextMenu[i].ChildRowContextMenu[j].MenuText + "</div>");
                        }
                        span.appendTo(filedHTML);
                        spdiv.appendTo(filedHTML);
                        filedHTML.appendTo(tmenu);
                    }
                    else if (_RowContextMenu[i].MenuLevel == 1) {
                        filedHTML = $('<div  style="width:' + _RowContextMenu[i].MenuWidth + 'px;" iconCls="' + _RowContextMenu[i].MenuIcon + '">' + _RowContextMenu[i].MenuText + '</div>');
                        filedHTML.appendTo(tmenu);
                    }
                }


                rowContextMenu = this.rowContextMenu = tmenu.menu({
                    onClick: function (item) {
                        var fire = $(item.target).attr('fire');
                        if (fire) {
                            new Function(fire)();   //eval(fire);  eval 也是可行的;
                        }
                        var targetName = $(item.target).attr('name');
                        // SelectActionByTargetName(targetName, _keyWordValue)
                        //  grid.datagrid('getPager').pagination('select', pageNum);
                    }
                });
            } else {
            }
            rowContextMenu.menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    }
};

$.fn.datagrid.defaults.onRowContextMenu = createGridRowContextMenu;
$.fn.datagrid.defaults.onHeaderContextMenu = createGridHeaderContextMenu;



$.extend($.fn.datagrid.methods, {
    addToolbarItem: function (jq, items) {
        return jq.each(function () {
            var dpanel = $(this).datagrid('getPanel');
            var toolbar = dpanel.children("div.datagrid-toolbar");
            if (!toolbar.length) {
                toolbar = $("<div class=\"datagrid-toolbar\"><table cellspacing=\"0\" cellpadding=\"0\"><tr></tr></table></div>").prependTo(dpanel);
                $(this).datagrid('resize');
            }
            var tr = toolbar.find("tr");
            for (var i = 0; i < items.length; i++) {
                var btn = items[i];
                if (btn == "-") {
                    $("<td><div class=\"datagrid-btn-separator\"></div></td>").appendTo(tr);
                } else {
                    var td = $("<td></td>").appendTo(tr);
                    var b = $("<a href=\"javascript:void(0)\"></a>").appendTo(td);
                    b[0].onclick = eval(btn.handler || function () { });
                    b.linkbutton($.extend({}, btn, {
                        plain: true
                    }));
                }
            }
        });
    },
    removeToolbarItem: function (jq, param) {
        return jq.each(function () {
            var dpanel = $(this).datagrid('getPanel');
            var toolbar = dpanel.children("div.datagrid-toolbar");
            var cbtn = null;
            if (typeof param == "number") {
                cbtn = toolbar.find("td").eq(param).find('span.l-btn-text');
            } else if (typeof param == "string") {
                cbtn = toolbar.find("span.l-btn-text:contains('" + param + "')");
            }
            if (cbtn && cbtn.length > 0) {
                cbtn.closest('td').remove();
                cbtn = null;
            }
        });
    }
});


//$.extend($.fn.datagrid.methods, {
//    ChangeToolbarItem: function (jq, param) {
//        return jq.each(function () {
//            var btns = $(this).parent().prev("div.datagrid-toolbar").children("a");
//            var cbtn = null;
//            var name = param.name
//            var type = param.type
//            if (typeof name == "number") {
//                cbtn = btns.eq(name);
//            } else if (typeof name == "string") {
//                var text = null;
//                btns.each(function () {
//                    text = $(this).data().linkbutton.options.text;
//                    if (text == name) {
//                        cbtn = $(this);
//                        text = null;
//                        return;
//                    }
//                });
//            }
//            if (cbtn) {
//                if (type == 0)
//                    $(cbtn).linkbutton('disable');
//                else
//                    $(cbtn).linkbutton('enable');
//            }
//        });
//    }
//});

$.extend($.fn.datagrid.methods, {
    ChangeToolbarItem: function (jq, param) {
        return jq.each(function () {
            var dpanel = $(this).datagrid('getPanel');
            var toolbar = dpanel.children("div.datagrid-toolbar");
            var cbtn = null;
            var name = param.name
            var type = param.type
            if (typeof name == "number") {
                cbtn = toolbar.find("td").eq(name).find('span.l-btn-text');
            } else if (typeof name == "string") {
                cbtn = toolbar.find("span.l-btn-text:contains('" + name + "')");
            }
          
            if (cbtn && cbtn.length > 0) {
                if (type == 0)
                    $(cbtn.closest('a')).linkbutton('disable')
                else
                    $(cbtn.closest('a')).linkbutton('enable')
            }
        });
    }
});

Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份      
        "d+": this.getDate(), //日      
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时      
        "H+": this.getHours(), //小时      
        "m+": this.getMinutes(), //分      
        "s+": this.getSeconds(), //秒      
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度      
        "S": this.getMilliseconds() //毫秒      
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

//批量保存
function BatchSaveWDInfo(SaveResID, SaveRecIDStr, json, tip, UserID, NodeID, OpenDiv, gridID) {
    var jsonStr1 = "[";
    jsonStr1 += json;
    jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
    jsonStr1 += "]";
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "Json": "" + jsonStr1 + "" },
        url: "Common/CommonGetInfo_ajax.aspx?typeValue=BatchSaveWDInfo&ResID=" + SaveResID + "&RecidStr=" + SaveRecIDStr + "&UserID=" + UserID,
        success: function (obj) {
            var s = eval(obj)[0]
            if (s.success || s.success == "true") {
                alert(tip + " 保存成功 " + s.SucessCount + "个，失败" + (s.Count - s.SucessCount) + "个！");
                if (OpenDiv != "") {
                    CommonCloseWindow(OpenDiv, gridID, NodeID)
                }

            } else {
                alert("保存失败,请刷新页面！");
            }
        },
        error: function (dd) {
            debugger
        }
    });
}


function InitializeToolBars() {
   // CenterGrid.datagrid("ChangeToolbarItem", { "name": "修改", "type": "0" })
    switch (_keyWordValue) {
        case "XMCJWPJDR":
            {
                CenterGrid.datagrid("ChangeToolbarItem", { "name": "修改", "type": "0" })
                CenterGrid.datagrid("ChangeToolbarItem", { "name": "删除", "type": "0" })
            }
            break;
    }
}


function Initialize(e, rowIndex, rowData, Key, MenuKey) {
  
    switch (MenuKey) {
        case "Meun_XM":
            {	 
                return rowData.项目经理 == _UserName
            }
            break;
    }
    return false;
}

function InitializeContextMenu(rowContextMenu, Key) {
    switch (key) {
        case "ddd":
            {
                switch (targetName) {
                    case "ddd":
                        {
                            var itemFirst = rowContextMenu.menu('findItem', '首页');
                            rowContextMenu.menu('disableItem', itemFirst.target);
                            rowContextMenu.menu('enableItem', itemPrev.target);

                        }
                        break;
                }
            }
            break;
    }
}

function SelectActionByTargetName(targetName, Key) {
    switch (key) {
        case "ddd":
            {
                switch (targetName) {
                    case "ddd":
                        break;
                }
            }
            break;
    }
}

function CheckTureOrFalse(sql)
{
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "Json": "" + "" + "" },
        url: "Common/CommonGetInfo_ajax.aspx?typeValue=CheckTureOrFalse&sql=" + sql,
        success: function (obj) {
            var s = eval(obj)[0]
            if (s.success || s.success == "true") {
              

            } else {
             
            }
        },
        error: function (dd) {
            debugger
        }
    });
}

//===========用户自定义函数===========
function ChangeXMZT(s) {
     
    if (RowContextMenu_rowData.项目状态 == s)
    {
        alert("项目状态已是：" + s + "！")
        CenterGrid.datagrid("reload")
        return
    }
    var SaveResID = "463765494422"
    var tip="状态更改成功"
    var SaveRecIDStr = RowContextMenu_rowData.ID
    var SaveJson = "[{"
    SaveJson += "'" + "项目状态" + "':'" + s + "'";
    SaveJson += "}],";
    BatchSaveWDInfo(SaveResID, SaveRecIDStr, SaveJson, tip, '', '', '', '', "")
    CenterGrid.datagrid("reload")
}
 


function onSelectEvent(rowIndex, rowData) {

    switch (_keyWordValue) {
        case "XMCJWPJDR":
            {
                if (rowData.项目经理 == _UserName) {
                    // CenterGrid.datagrid("ChangeToolbarItem", {"name":"添加","type":"1"})
                    CenterGrid.datagrid("ChangeToolbarItem", { "name": "修改", "type": "1" })
                    CenterGrid.datagrid("ChangeToolbarItem", { "name": "删除", "type": "1" })
                }
                else {
                    CenterGrid.datagrid("ChangeToolbarItem", { "name": "修改", "type": "0" })
                    CenterGrid.datagrid("ChangeToolbarItem", { "name": "删除", "type": "0" })
                }
            }
            break;
    }
}
