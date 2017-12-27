function GetCommonPath() {
    return window.location.protocol + "//" + window.location.host + "/portal/Base/"
}

function GetCommonPathByWW() {
    return window.location.protocol + "//" + "unionsoft.vigorddns.com:88/" + "/portal2016/Base/"
}

 

function CommonAddDefaultTools(obj,keyWordValue, ResID, addUrl, adddisabled, editUrl, editdisabled, deldisabled, DialogWidth, DialogHeight, delStr)
{
    if (delStr.indexOf("添加") == -1)
    {
        var s = [{
            text: '添加',
            iconCls: 'icon-add',
            disabled: adddisabled,
            'handler': function () {
                Add_ToolBar2(addUrl, DialogWidth, DialogHeight)
            }
        }]
        obj.datagrid("addToolbarItem", s)

    }
    if (delStr.indexOf("修改") == -1) {
        var s = [{
            text: '修改',
            iconCls: 'icon-add',
            disabled: editdisabled,
            'handler': function () {
                Edit_ToolBar2(editUrl, DialogWidth, DialogHeight)
            }
        }]
        obj.datagrid("addToolbarItem", s)

    }
    if (delStr.indexOf("删除") == -1) {
        var s = [{
            text: '删除',
            iconCls: 'icon-add',
            disabled: deldisabled,
            'handler': function () {
                Del_ToolBar2(ResID)
            }
        }]
        obj.datagrid("addToolbarItem", eval(s))
    }

    if (delStr.indexOf("导出") == -1) {


    }

    SetHeight()
  
}

function CommonSetUserDefinedToolBars(toolbarDIV, keyWordValue, obj, argIsUseNewEasyui, ResID, addUrl, adddisabled, editUrl, editdisabled, deldisabled, DialogWidth, DialogHeight, IsAddDefaultTools)
{
     
    if (toolbarDIV != "") {
        obj.datagrid({ toolbar: toolbarDIV });
        //onSelectEvent("", _SelectRowData, keyWordValue, obj);
        CommonAddDefaultTools(obj,keyWordValue, ResID, addUrl, adddisabled, editUrl, editdisabled, deldisabled, DialogWidth, DialogHeight, "")
        return
    }

    if (keyWordValue == "") {
        //onSelectEvent("", _SelectRowData, keyWordValue, obj);
        CommonAddDefaultTools(obj,keyWordValue, ResID, addUrl, adddisabled, editUrl, editdisabled, deldisabled, DialogWidth, DialogHeight, "")
        return
    }

    $.ajax({
        type: "POST",
        url: CommonPath + "Common/CommonGetInfo_ajax.aspx?typeValue=CommonGetUserDefinedToolBars",
        data: {
            keyWordValue: keyWordValue,
            argIsUseNewEasyui: argIsUseNewEasyui
        },
        success: function (centerJson) {
            if (centerJson == "^''^***") {
                //onSelectEvent("", _SelectRowData, keyWordValue, obj);
                if (IsAddDefaultTools)
                    CommonAddDefaultTools(obj, keyWordValue, ResID, addUrl, adddisabled, editUrl, editdisabled, deldisabled, DialogWidth, DialogHeight, "")
                return
            }
              
            var AddDefinedToolBars = centerJson.split("***")[0]
            var DelDefinedToolBars = centerJson.split("***")[1]

            if (IsAddDefaultTools)
                CommonAddDefaultTools(obj, keyWordValue, ResID, addUrl, adddisabled, editUrl, editdisabled, deldisabled, DialogWidth, DialogHeight, DelDefinedToolBars)


            if (AddDefinedToolBars != "")
                obj.datagrid("addToolbarItem", eval(AddDefinedToolBars.replace(/\^/g, "")))

            if (DelDefinedToolBars != "") {
                var DelNameStr = DelDefinedToolBars
                if (DelNameStr.indexOf(',') >= 0) {
                    var s = DelNameStr.split(',');
                    for (var i = 0; i < s.length ; i++) {
                        obj.datagrid("removeToolbarItem", s[i])
                    }
                }
                else {
                    obj.datagrid("removeToolbarItem", DelNameStr)
                }
            }
            onSelectEvent("", _SelectRowData, keyWordValue, obj);

        }
    });

}