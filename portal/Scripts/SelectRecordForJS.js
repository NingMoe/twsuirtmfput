var _SelectRecordData=[]
var CommonPath =  window.location.protocol + "//" + window.location.host + "/portal/Base/"
String.prototype.trim = function () {
    //return this.replace(/[(^\s+)(\s+$)]/g,"");//會把字符串中間的空白符也去掉
    //return this.replace(/^\s+|\s+$/g,""); //
    return this.replace(/^\s+/g, "").replace(/\s+$/g, "");
}

function GetQueryKey(q, SelectRecordData) {
    var QueryKeyStr = ""
    if (q.trim() == '') return " "


    if (QueryKeyStr != "") {
        QueryKeyStr += ' or ' + SelectRecordData.idField + " like '%" + q.trim() + "%'";
    }
    var temp = SelectRecordData.ROW_NUMBER_ORDER.split(',');
   
    if (SelectRecordData.QueryKeyField != '') {
        var QueryKey = SelectRecordData.QueryKeyField;
        if (QueryKey.indexOf(',') == -1) {
            for (var i = 0; i < temp.length; i++) {
                if (temp[i].split('=').length > 1) {
                    if (QueryKey == temp[i].split('=')[0]) {
                        QueryKey = temp[i].split('=')[1]
                    }
                }
            }
            QueryKeyStr = QueryKey + " like '%" + q.trim() + "%' ";
        }
        else {
                QueryKeyFields = QueryKey.split(',');
                for (var j = 0; j < QueryKeyFields.length; j++) {
                {
                    if (QueryKeyStr != "") {
                        QueryKeyStr += " or "
                    }
                    var QueryKeyStrABC = QueryKeyFields[j];
                    for (var i = 0; i < temp.length; i++) {
                        if (temp[i].split('=').length > 1) {
                            if (QueryKeyStrABC == temp[i].split('=')[0]) {
                                QueryKeyStrABC = temp[i].split('=')[1];
                            }
                        }
                    }
                    QueryKeyStr += QueryKeyStrABC + " like '%" + q.trim() + "%'";
                }
            }
        }
    }
    if (SelectRecordData.Condition!=undefined) {
        QueryKeyStr = SelectRecordData.Condition + " and (" + QueryKeyStr + ")";
    } else {
        QueryKeyStr =" (" + QueryKeyStr + ")";
    }
  
    return QueryKeyStr;
}

function SelectRecordsLoad(sort, order, Key, SelectRecordData, obj) {
    
    var SelectRecords = $(SelectRecordData.InitializationStr)
    var ComboGridColumns = setComboGridColumns(SelectRecordData.Columns);

    obj.combogrid("grid").datagrid("reload",
      {
          ResID: SelectRecordData.ResID,
          keyWordValue: SelectRecordData.keyWordValue,
          UserDefinedSql: SelectRecordData.UserDefinedSql,
          OrderByStr: SelectRecordData.OrderByStr,
          Condition: SelectRecordData.Condition,
          ROW_NUMBER_ORDER: SelectRecordData.ROW_NUMBER_ORDER,
          sort: sort,
          order: order,
          'QueryKeystr': GetQueryKey(Key, SelectRecordData)
      });

    //SelectRecords.combogrid({
    //    columns: eval(ComboGridColumns),
    //});

}
function InitializationComboGrid(SelectRecordData) {

    var UserDefinedKey = SelectRecordData.UserDefinedKey == undefined ? "" : SelectRecordData.UserDefinedKey
    var SelectRecords = $(SelectRecordData.InitializationStr)
    var SelectRecordDataid = SelectRecordData.id == undefined ? "" : SelectRecordData.id
    var SelectRecordsValue = $(SelectRecordData.key == undefined ? "" : SelectRecordData.key)
    var SelectRecordQueryKey = $(SelectRecordData.SelectRecordQueryKey == undefined ? "" : SelectRecordData.SelectRecordQueryKey)

    var ThisWidth = SelectRecordData.KeyWidth == undefined ? 150 : SelectRecordData.KeyWidth;
    try {
        if (SelectRecordData.PercentageWidth != undefined) {
            var PercentageWidth = SelectRecordData.PercentageWidth == undefined ? 90 : SelectRecordData.PercentageWidth
            var ThisPercentageWidth = SelectRecords.parent()[0].clientWidth * (parseFloat(PercentageWidth) / 100);
//            if (ThisPercentageWidth > 0)
//                ThisWidth = ThisPercentageWidth
        }
    }
    catch (e) {

    }

    var multiple = SelectRecordData.multiple == undefined ? false : SelectRecordData.multiple
    var pageList = SelectRecordData.pageList == undefined ? [20,30,40,50,100,500] : SelectRecordData.pageList

    var NoPaging = SelectRecordData.NoPaging == undefined ? false : SelectRecordData.NoPaging
    var ComboGridColumns = setComboGridColumns(SelectRecordData.Columns);

    var SelectRecordQueryParams = {
        ResID: SelectRecordData.ResID == undefined ? "" : SelectRecordData.ResID,
        keyWordValue: SelectRecordData.keyWordValue == undefined ? "" : SelectRecordData.keyWordValue,
        UserDefinedSql: SelectRecordData.UserDefinedSql == undefined ? "" : SelectRecordData.UserDefinedSql,
        OrderByStr: SelectRecordData.OrderByStr == undefined ? "" : SelectRecordData.OrderByStr,
        Condition: SelectRecordData.Condition == undefined ? "" : SelectRecordData.Condition,
        ROW_NUMBER_ORDER: SelectRecordData.ROW_NUMBER_ORDER == undefined ? "" : SelectRecordData.ROW_NUMBER_ORDER
    }
    if (NoPaging)
        SelectRecordQueryParams.NoPaging = true
    var CommonPath = window.location.protocol + "//" + window.location.host + "/portal/Base/"
    SelectRecords.combogrid({
        width: ThisWidth,
        height: SelectRecordData.Keyheight == undefined ? 20 : SelectRecordData.Keyheight,
        panelWidth: SelectRecordData.panelWidth == undefined ? 400 : SelectRecordData.panelWidth,
        idField: SelectRecordData.idField == undefined ? "" : SelectRecordData.idField,
        textField: SelectRecordData.textField == undefined ? "" : SelectRecordData.textField,
        multiple: SelectRecordData.multiple == undefined ? false : SelectRecordData.multiple,
        rownumbers: true,
        disabled: SelectRecordData.disabled == undefined ? false : SelectRecordData.disabled,
        selectOnCheck: true,
        checkOnSelect: true,
        pagination: !NoPaging, //是否分页 
        pageSize: SelectRecordData.PageSize == undefined ? 20 : SelectRecordData.PageSize, //每页显示的记录条数，默认为10
        pageList: pageList, //可以设置每页记录条数的列表 
        //fit: true,
        value: SelectRecordData.deafultValue == undefined ? "" : SelectRecordData.deafultValue,
        showFooter: true,
        fitColumns: true,
        url: CommonPath + "Common/CommonGetInfo_ajax.aspx?typeValue=GetDataByUserDefinedSql",
        queryParams: SelectRecordQueryParams,
        onSortColumn: function (sort, order) {
            SelectRecordsLoad(sort, order, "", SelectRecordData, $(this));
        },
        columns: eval(ComboGridColumns),
        keyHandler: {
            up: function () {               //【向上键】押下处理  
                //取得选中行  
                var selected = SelectRecords.combogrid('grid').datagrid('getSelected');
                if (selected) {
                    //取得选中行的rowIndex  
                    var index = SelectRecords.combogrid('grid').datagrid('getRowIndex', selected);
                    //向上移动到第一行为止  
                    if (index > 0) {
                        SelectRecords.combogrid('grid').datagrid('selectRow', index - 1);
                    }
                } else {
                    var rows = SelectRecords.combogrid('grid').datagrid('getRows');
                    SelectRecords.combogrid('grid').datagrid('selectRow', rows.length - 1);
                }
            },
            down: function () {             //【向下键】押下处理  
                //取得选中行  
                var selected = SelectRecords.combogrid('grid').datagrid('getSelected');
                if (selected) {
                    //取得选中行的rowIndex  
                    var index = SelectRecords.combogrid('grid').datagrid('getRowIndex', selected);
                    //向下移动到当页最后一行为止  
                    if (index < SelectRecords.combogrid('grid').datagrid('getData').rows.length - 1) {
                        SelectRecords.combogrid('grid').datagrid('selectRow', index + 1);
                    }
                } else {
                    SelectRecords.combogrid('grid').datagrid('selectRow', 0);
                }
            },
            enter: function () {             //【回车键】押下处理    
                var selected = SelectRecords.combogrid('grid').datagrid('getSelected');
                if (selected) {
                    //取得选中行的rowIndex  
                    var index = SelectRecords.combogrid('grid').datagrid('getRowIndex', selected);
                    SelectRecords.combogrid('grid').datagrid('selectRow', index);
                } else {
                    SelectRecords.combogrid('grid').datagrid('selectRow', 0);
                }
                SelectRecords.combogrid('hidePanel');
            },
            query: function (q) {

                if (SelectRecordData.QueryKeyField != '') {
                    SelectRecordQueryKey.val(q)
                    SelectRecordsLoad('', '', q, SelectRecordData, $(this));
                    SelectRecords.combogrid('grid').datagrid('clearSelections');
                    SelectRecords.combogrid("setValue", q);

                    //if (multiple) {
                    //    SelectRecords.combogrid("setValues", q);
                    //}
                    //else
                    //{
                    //    SelectRecords.combogrid("setValue", q);
                    //}
                }
            }
        },
        loadFilter: function (data) {
            if (data == null) {
                //$(this).datagrid("load");
                //return $(this).datagrid("getData");
                alert('数据集为空！')
                return
            } else {
                return data;
            }
        },
        onChange: function (newValue, oldValue) {
            SelectRecords.val(newValue)
            SelectRecordsValue.val(newValue)
        },
        onUnselect: function (rowIndex, rowData) {
            if (SelectRecordData.HasLastOperation) {
                LastOperation(rowData, SelectRecordData);
            }
        },
        onSelect: function (rowIndex, rowData) {
            if (SelectRecordData.HasFirstOperation) {
                FirstOperation(rowData, SelectRecordData);
            }
            SetNewnewValue(rowIndex, rowData, SelectRecordData)
            SetOtherSelectRecordValue(rowData, SelectRecordData)
            if (SelectRecordData.HasLastOperation) {
                LastOperation(rowData, SelectRecordData);
            }
        },
        onSelectAll: function (rowIndex, rowData) {
            if (multiple) {
                var SelectAllByNoPaging = SelectRecordData.SelectAllByNoPaging == undefined ? "" : SelectRecordData.SelectAllByNoPaging
                if (SelectAllByNoPaging) {
                    if ($.isFunction(window.SelectAllEventByNoPaging) && $.isFunction(window.GetAllFieldValue)) {
                        SelectAllEventByNoPaging(true);
                    }
                }
                else {
                    if ($.isFunction(window.SelectAllEvent))
                        SelectAllEvent(rowIndex, rowData, SelectRecordData);
                }
            }
        },
        onUnselectAll: function (rowIndex, rowData) {
            if (multiple) {
                //debugger
                var SelectAllByNoPaging = SelectRecordData.SelectAllByNoPaging == undefined ? "" : SelectRecordData.SelectAllByNoPaging
                if (SelectAllByNoPaging) {
                    if ($.isFunction(window.SelectAllEventByNoPaging) && $.isFunction(window.GetAllFieldValue)) {
                        SelectAllEventByNoPaging(false);
                    }
                }
                else {
                    if ($.isFunction(window.UnSelectAllEvent))
                        UnSelectAllEvent(rowIndex, rowData, SelectRecordData);
                }
            }

        },
        onLoadSuccess: function () {

            var RecID = SelectRecordData.RecID == undefined ? "" : SelectRecordData.RecID

            var HasLoadSuccessFunion = SelectRecordData.HasLoadSuccessFunion == undefined ? false : SelectRecordData.HasLoadSuccessFunion

            var hasInitializationEvent = SelectRecordData.hasInitializationEvent == undefined ? false : SelectRecordData.hasInitializationEvent

            if (SelectRecordData.deafultValue != undefined && SelectRecordData.deafultValue != "") {
                var multiple = SelectRecordData.multiple == undefined ? false : SelectRecordData.multiple

                // SelectRecords.combogrid('setValue', SelectRecordData.deafultValue)

                if (multiple) {
                    SelectRecords.combogrid('setValues', SelectRecordData.deafultValue)
                }
                else {
                    SelectRecords.combogrid('setValue', SelectRecordData.deafultValue)
                }
            }

            if (RecID != "") {
                SelectRecords.combogrid('setValue', SelectRecordsValue.val())
            }

            // SelectRecords.combogrid('resize', ThisWidth)

            if (hasInitializationEvent) {
                if ($.isFunction(window.initializationEvent))
                    initializationEvent(SelectRecordData)
            }

        },
        onLoadError: function (o) {
            debugger
        },
        error: function (d) {
            debugger
        }
    });
    
    //SelectRecords.combogrid('combo').combo({ height:20})
    if (!NoPaging) {
        var p = SelectRecords.combogrid('grid').datagrid('getPager');
        $(p).pagination({
            pageList: pageList,
            beforePageText: '第',
            pageSize: SelectRecordData.PageSize == undefined ? 20 : SelectRecordData.PageSize,
            afterPageText: '页  共 {pages} 页',
            displayMsg: '从 [{from}] 到 [{to}] 共 [{total}] 条记录'
        });
    }
}

function SelectAllEventByNoPaging(IsSelectAll)
{
     
    var SelectAllByNoPaging = _SelectRecordData.SelectAllByNoPaging == undefined ? "" : _SelectRecordData.SelectAllByNoPaging
     
    var SelectRecordQueryParams = {
        ResID: _SelectRecordData.ResID == undefined ? "" : _SelectRecordData.ResID,
        keyWordValue: _SelectRecordData.keyWordValue == undefined ? "" : _SelectRecordData.keyWordValue,
        UserDefinedSql: _SelectRecordData.UserDefinedSql == undefined ? "" : _SelectRecordData.UserDefinedSql,
        OrderByStr: _SelectRecordData.OrderByStr == undefined ? "" : _SelectRecordData.OrderByStr,
        Condition: _SelectRecordData.Condition == undefined ? "" : _SelectRecordData.Condition,
        ROW_NUMBER_ORDER: _SelectRecordData.ROW_NUMBER_ORDER == undefined ? "" : _SelectRecordData.ROW_NUMBER_ORDER
    }
     
    var multiple = _SelectRecordData.multiple == undefined ? false : _SelectRecordData.multiple

    //if (SelectRecordQueryParams.NoPaging == undefined)
    //    SelectRecordQueryParams.NoPaging = true

    //SelectRecordQueryParams.typeValue = "GetDataByUserDefinedSql"

    if (SelectAllByNoPaging && multiple) {

        if (!IsSelectAll) {
            $(_SelectRecordData.InitializationStr).combogrid('setValues', [])
            return
        }

        $.ajax({
            type: "POST",
            url: "../Common/CommonGetInfo_ajax.aspx?typeValue=GetDataByUserDefinedSql&NoPaging=1",
            data: SelectRecordQueryParams,
            success: function (centerJson) {
                var Obj = eval("[" + centerJson + "]")[0]
                var value = GetAllFieldValue(Obj, _SelectRecordData);
                if (value.length > 0) {
                    $(_SelectRecordData.InitializationStr).combogrid('setValues', value)
                }  
            }
        });
    }
}

function SetNewnewValue(rowIndex, rowData, SelectRecordData) {

    var SetValueStr = SelectRecordData.SetValueStr == undefined ? "" : SelectRecordData.SetValueStr
    var IsmultiSelect = SelectRecordData.IsmultiSelect == undefined ? false : SelectRecordData.IsmultiSelect
    var SetResIDReadOnly = SelectRecordData.SetResIDReadOnly == undefined ? false : SelectRecordData.SetResIDReadOnly

    if (SetValueStr != "") {

        if (IsmultiSelect) {
            if (!SelectRecordData.SetResIDReadOnly) {
                LedgerChildKeyByRowdata(SelectRecordData.ResID, SetValueStr, rowData, true, UserDefinedKey)
            }
            else {
                LedgerChildKeyByRowdata(SelectRecordData.ResID, SetValueStr, rowData, false, UserDefinedKey)
            }
        }
        else {
            LedgerChildKeyByRowdata(SelectRecordData.ResID, SetValueStr, rowData, SetResIDReadOnly, "")
        }
        SetBackgroundColor();
    }
}



function SetOtherSelectRecordValue(Rowdata, SelectRecordData) {

    var SetSelectRecordValue = SelectRecordData.SetSelectRecordValue == undefined ? "" : SelectRecordData.SetSelectRecordValue

    if (SetSelectRecordValue != "") {
        var ResID = SelectRecordData.ResID
        var ChildKey = SetSelectRecordValue
        var ResIDKey = ""
        var RSResIDKey = ""
        if (ChildKey.indexOf(',') == -1) {
            if (ChildKey.indexOf('=') != -1) {
                ResIDKey = ChildKey.split('=')[0];
                RSResIDKey = ChildKey.split('=')[1];
                for (var key in Rowdata) {
                    if (key == RSResIDKey) {
                        $("#" + ResID + '_' + ResIDKey).combogrid('setValue', Rowdata[RSResIDKey]);
                    }
                }
            }
        }
        else {
            var s = ChildKey.split(',');
            for (var j = 0; j < s.length; j++) {
                if (ChildKey.indexOf('=') != -1) {
                    ResIDKey = s[j].split('=')[0];
                    RSResIDKey = s[j].split('=')[1];
                    for (var key in Rowdata) {
                        if (key == RSResIDKey) {
                           $("#" + ResID + '_' + ResIDKey).combogrid('setValue', Rowdata[RSResIDKey]);
                        }
                    }
                }
            }
       }
        SetBackgroundColor();
    }
}

function setComboGridColumns(Columns) {
    var ColumnsStr = "";
    if (Columns.indexOf(",") >= 0) {
        var c = Columns.split(",");
        for (var i = 0; i < c.length; i++) {
            var show = c[i].split("#")[0]
            var key = c[i].split("#")[1]

            if (ColumnsStr == "")
                ColumnsStr += "[[";
            else
                ColumnsStr += ",";

            if (key == "ck") {
                ColumnsStr += "{field:'ck',checkbox:true}";
            }
            else {
                ColumnsStr += "{field: '" + key + "',title:'" + show + "',width:" + 80 + ", sortable: " + true + " ,align:'" + "center" + "'}"
            }
        }
    }
    else {
        var show = Columns.split("#")[0]
        var key = Columns.split("#")[1]

        if (ColumnsStr == "")
            ColumnsStr += "[[";
        else
            ColumnsStr += ",";

        if (key == "ck") {
            ColumnsStr += "{field:'ck',checkbox:true}";
        }
        else {
            ColumnsStr += "{field: '" + key + "',title:'" + show + "',width:" + 80 + ", sortable: " + true + " ,align:'" + "center" + "'}"
        }
    }

    return ColumnsStr == "" ? "" : ColumnsStr += "]]"
}