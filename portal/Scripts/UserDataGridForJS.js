var _EditRow = []
var _EditChange = false
var _IsSort = false
var _headerSearchCondition = "";
var _SetSelectRecordData = "";

function headerSearchInfo(field) {
    if (_DataGridData == undefined) return
    var hasHeaderSearch = _DataGridData.hasHeaderSearch == undefined ? false : _DataGridData.hasHeaderSearch

    if (!hasHeaderSearch) return

    var DataGridObj = $(_DataGridData.InitializationStr)
    var SelectDataqueryParams = _DataGridData.queryParams;
    var SearchCondition = ""
    var vSql = SelectDataqueryParams.UserDefinedSql
    //if (field != "")    //判断回车按钮事件
    //{
    //    if (event.keyCode != 13) return
    //}

    _headerSearchCondition = "";


    $("input[name=headerSearch]:not([areaSearch])").each(function () {
        if ($(this).val() != "") {
            var f = $(this)[0].id.toString().split("_")[1];
            if (SearchCondition != "") SearchCondition += " and "

            if ($(this).val() == "[全部]") {
            }
            else if ($(this).val() == " " || $(this).val() == "[空值]") {
                SearchCondition += ("(" + f + " is null  or " + f + " = '' ) ")
            }
            else if ($(this).val() == "  " || $(this).val() == "[非空值]") {
                SearchCondition += ("(" + f + " is not null  and  " + f + " <> '' ) ")
            }
            else if ($(this).val() == "   " || $(this).val() == "[重复值]") {
                SearchCondition += (" " + f + " in (  SELECT distinct a." + f + " from (" + vSql + ") a, (" + vSql + ") b  where a." + f + "=b." + f + " and a.ID <> b.ID  ) ")
            }
            else {
                SearchCondition += (f + " like '%" + $(this).val() + "%'")
            }
        }
    });

    $("select[name=headerSearch]").each(function () {
        if ($(this).val() != "") {
            var f = $(this)[0].id.toString().split("_")[1];
            if (SearchCondition != "") SearchCondition += " and "

            if ($(this).val() == " ") {
                SearchCondition += ("(" + f + " is null  or " + f + " = '' ) ")
            }
            else if ($(this).val() == "  ") {
                SearchCondition += ("(" + f + " is not null  and  " + f + " <> '' ) ")
            }
            else if ($(this).val() == "不录取(加空值)") {
                var sss = ("(" + (" (" + f + " is null  or " + f + " = '' ) ") + " OR " + f + " = '不录取' )")
                //alert(sss)
                SearchCondition += sss
            }
            else {
                SearchCondition += (f + " = '" + $(this).val() + "'")
            }
        }
    });

    $("input[name=headerSearch][areaSearch=1]").each(function () {
        if ($(this).val() != "") {
            var f = $(this)[0].id.toString().split("_")[1];

            var areaSearchSign = $(this).attr("areaSearchSign")
            var thisValue = $(this).val()


            if (areaSearchSign != "")
                thisValue = areaSearchSign.replace("{v}", $(this).val())


            if (SearchCondition != "") SearchCondition += " and "

            if ($(this).val() == " ") {
                SearchCondition += ("(" + f + " is null  or " + f + " = '' ) ")
            }
            else if ($(this).val() == "  ") {
                SearchCondition += ("(" + f + " is not null  and  " + f + " <> '' ) ")
            }
            else {

                var f2 = $(this).next()[0].id.toString().split("_")[1];
                var fvalue = $(this).next().val()

                if (fvalue == "") {
                    SearchCondition += (f + " like '%" + thisValue + "%'")
                    // alert(f + " like '%" + thisValue + "%'")
                }
                else {
                    if (areaSearchSign != "")
                        fvalue = areaSearchSign.replace("{v}", fvalue)

                    SearchCondition += (f + " between '" + thisValue + "' and '" + fvalue + "' ")
                    //  alert(f + " between '" + thisValue + "' and '" + fvalue + "' ")
                }
            }
        }
    });


    //$("input[textboxname=SelectRecord]").each(function () {
    //    var f = $(this)[0].id.toString().split("_")[1];
    //    var s = $("#headerSearch_" + f).combogrid('getText')
    //  console.info(s)
    //});

    if (_SetSelectRecordData != "") {
        if (SearchCondition == "")
            SearchCondition = _SetSelectRecordData
        else
            SearchCondition = (SearchCondition + " and " + _SetSelectRecordData)
    }

    _headerSearchCondition = SearchCondition;

    SelectDataqueryParams.UserDefinedSql = _UserDefinedSql
    SelectDataqueryParams.Condition = SearchCondition
    //alert(field)
    DataGridObj.datagrid('load', SelectDataqueryParams)
}

$.extend($.fn.datagrid.defaults.view, {
    onAfterRender: function (target) {

        if (_DataGridData == undefined) return
        var hasHeaderSearch = _DataGridData.hasHeaderSearch == undefined ? false : _DataGridData.hasHeaderSearch
        var hasHeaderOhertEvent = _DataGridData.hasHeaderOhertEvent == undefined ? false : _DataGridData.hasHeaderOhertEvent

        var HeaderSearchType = _DataGridData.HeaderSearchType == undefined ? "" : _DataGridData.HeaderSearchType

        if (!hasHeaderSearch) return

        if (target.id.indexOf("UserDataGrid_TB_") == -1)
            return

        var dc = $.data(target, 'datagrid').dc;
        if (dc.header2.find('[filter="true"]').length == 0) {
            var header = dc.header1; //固定列表头
            var header2 = dc.header2; // 常规列表头
            var filterRow = $('<tr></tr>');
            var opts = $.data(target, 'datagrid').options;
            var columns = opts.columns;
            var frozenColumns = opts.frozenColumns;
            var t = 0
            var headerSearchStr = ""

            if (frozenColumns.length > 0) {
                $.each(frozenColumns[0], function () {
                    if (!this.checkbox) {
                        var w = header.find('[field="' + this.field + '"] > div').width();
                        headerSearchStr = "";
                        headerSearchStr = SetheaderSearchInput(this.field, w, HeaderSearchType)

                        if (t == 0) {
                            filterRow.append('<td></td><td style="margin-left:auto;margin-right:auto;text-align: center;" >' + headerSearchStr + '</td>');
                        }
                        else {
                            filterRow.append('<td style="margin-left:auto;margin-right:auto;text-align:center" >' + headerSearchStr + '</td>');
                        }
                        t++;
                    }
                    else {
                        header.find('.datagrid-header-check').parent().attr('rowspan', 2)
                    }
                });
                header.find('tbody').append(filterRow);
            }

            filterRow = $('<tr filter="true"></tr>');

            if (columns.length > 0) {
                $.each(columns[0], function () {
                    var w = header2.find('[field="' + this.field + '"] > div').width();

                    //headerSearchStr = '<input name="headerSearch" id="headerSearch_' + this.field + '" onkeyup=headerSearchInfo("' + this.field + '")  style="width:' + w + 'px"/>';

                    headerSearchStr = SetheaderSearchInput(this.field, w, HeaderSearchType)

                    if (this.hfilter) {
                        var a = $('<input field="' + this.field + '" class="easyui-combobox" style="width:' + w + 'px" />');
                        filterRow.append($('<td></td>').append(a));
                        a.data('options', this.hfilter);
                    } else {
                        filterRow.append('<td>' + headerSearchStr + '</td>');
                    }
                });

                header2.find('tbody').append(filterRow);
            }

            var dgData = $(target).datagrid('getData').rows;

            header2.find('input[field]').each(function () {
                var opts = $(this).data('options');
                var field = $(this).attr('field');
                $.extend(opts.options, {
                    onSelect: function (item) {
                        var d = _.filter(dgData, function (row) {
                            return row[field].indexOf(item[opts.options.textField]) > -1;
                        });
                        $(target).datagrid('loadData', d);
                    }
                });

                $(this)[opts.type](opts.options);
            })
        }

        if (hasHeaderOhertEvent) {
            if ($.isFunction(HeaderOhertEvent)) {
                HeaderOhertEvent(_DataGridData)
            }
        }
    }
});


function scrollShow(datagrid) {
    datagrid.prev(".datagrid-view2").children(".datagrid-body").html("<div style='width:" + datagrid.prev(".datagrid-view2").find(".datagrid-header-row").width() + "px;border:solid 0px;height:1px;'></div>");
}


function SetheaderSearchInput(field, w, HeaderSearchType) {
    var str = '<input name="headerSearch" id="headerSearch_' + field + '" onkeyup=headerSearchInfo("' + field + '") class="headerSearchClass_0"  style="width:' + w + 'px;height:17px;" />';
    if (HeaderSearchType == "" || HeaderSearchType.length == 0)
        return str

    for (var i = 0; i < HeaderSearchType.length; i++) {
        if (HeaderSearchType[i].HeaderName == field) {
            //onblur=headerSearchInfo("' + field + '")  onclick   onchange
            switch (HeaderSearchType[i].HeaderType) {
                case 1:
                    var HeaderDefaultValueStr = HeaderSearchType[i].HeaderDefaultValue == undefined ? "" : " value='" + HeaderSearchType[i].HeaderDefaultValue + "'"
                    var HeaderFormat = HeaderSearchType[i].HeaderFormat == undefined ? "yyyy-MM-dd" : HeaderSearchType[i].HeaderFormat
                    str = '<input name="headerSearch" class="Wdate" ondblclick=WdatePicker({dateFmt:"' + HeaderFormat + '",onpicked:SearchInputblur(this,1)}) id="headerSearch_' + field + '"  onkeyup=headerSearchInfo("' + field + '") onchange=headerSearchInfo("' + field + '") style="width:' + w + 'px" ' + HeaderDefaultValueStr + ' />';
                    return str
                    break;
                case 2:
                    str = '<input name="headerSearch" class="Wdate" ondblclick=WdatePicker({dateFmt:"HH:mm",onpicked:SearchInputblur(this,1)}) id="headerSearch_' + field + '"  onkeyup=headerSearchInfo("' + field + '")  onchange=headerSearchInfo("' + field + '")  style="width:' + w + 'px"/>';
                    return str
                    break;
                case 3:
                    if (HeaderSearchType[i].HeaderOptionValue.length != HeaderSearchType[i].HeaderOptionTitle.length)
                        return str
                    str = '<select  name="headerSearch"  id="headerSearch_' + field + '" onchange=headerSearchInfo("' + field + '")  style="width:' + (w + 5) + 'px;height:23px;">  class="headerSearchClass_3"  ';
                    for (var j = 0; j < HeaderSearchType[i].HeaderOptionTitle.length; j++) {
                        str += '<option value="' + HeaderSearchType[i].HeaderOptionValue[j] + '">' + HeaderSearchType[i].HeaderOptionTitle[j] + '</option>'
                    }
                    str += '</select>'
                    return str
                    break;
                case 4:
                    var HeaderStr = HeaderSearchType[i].HeaderStr == undefined ? "" : HeaderSearchType[i].HeaderStr;
                    if (HeaderStr == "") return str
                    str = HeaderStr.replace(/{field}/g, field).replace(/{width}/g, w / 2.5);
                    // alert(str)
                    break;
                case 5:
                    str = '<span style="width:' + w + 'px;height: 22px;"><input name="SelectRecord" class="headerSearchClass_5"  id="headerSearch_' + field + '" onkeyup=headerSearchInfo("' + field + '")   style="width:' + w + 'px"/></span>';
                    return str
                    break;
            }
        }
    }

    return str
}

function SearchInputblur(obj, type) {

    if ($(obj).val() != "") {
        //var od = d.cal.getDateStr()
        //var nd = d.cal.getNewDateStr()
        //alert(od)
        //alert(nd)
        //if (od != nd)

        $(obj).blur()
        headerSearchInfo("")
    }

    //if (type == 2) {
    //    if ($(obj).val() == "") {
    //        $(obj).blur()
    //        headerSearchInfo("")
    //    }
    //}
    //else {
    //    if ($(obj).val() != "") {
    //        $(obj).blur()
    //        headerSearchInfo("")
    //    }
    //}
}


function ReloadDataGrid(sort, order, Key, DataGridData, obj) {
    //obj.datagrid('load', {
    //    keyWordValue: DataGridData.BaseKey == undefined ? "" : DataGridData.BaseKey,
    //    ResID: DataGridData.BaseResID == undefined ? "" : DataGridData.BaseResID,
    //    UserID: DataGridData.UserID == undefined ? "" : DataGridData.UserID,
    //    CustomQueryStr: DataGridData.BaseCustomQueryStr == undefined ? "" : DataGridData.BaseCustomQueryStr,
    //    QueryKey: DataGridData.BaseQueryKey == undefined ? "" : DataGridData.BaseQueryKey,
    //    Condition: _DataGridSearchCondition
    //});

    var queryParams = DataGridData.queryParams == undefined ? GetCommonQueryParams(DataGridData) : DataGridData.queryParams

    obj.datagrid('load', queryParams)
}



function InitializationUserDataGrid(sort, order, DataGridData) {
    var DataGrid = $(DataGridData.InitializationStr)
    var GridHeight = DataGridData.GridHeight == undefined ? GetGridHeight(DataGridData) : DataGridData.GridHeight
    var GridWidth = DataGridData.GridWidth == undefined ? GetGridWidth(DataGridData) : DataGridData.GridWidth

    var CommonGetDataUrl = DataGridData.CommonGetDataUrl == undefined ? "" : DataGridData.CommonGetDataUrl

    var pageSize = DataGridData.pageSize == undefined ? 20 : DataGridData.pageSize

    var queryParams = DataGridData.queryParams == undefined ? GetCommonQueryParams(DataGridData) : DataGridData.queryParams

    var isPagination = DataGridData.isPagination == undefined ? true : DataGridData.isPagination

   

    //if (toolbarDIV != "") {
    //    obj.datagrid({ toolbar: toolbarDIV });
    //    return
    //}


    DataGrid.parent().css("height", GridHeight)
    DataGrid.datagrid({
        height: GridHeight,
        Width: GridWidth,
        nowrap: true,
        border: true,
        striped: true,
        pageList: [20, 30, 40, 50],
        singleSelect: DataGridData.singleSelect == undefined ? true : DataGridData.singleSelect,
        pageSize: pageSize,
        idField: DataGridData.idField == undefined ? "" : DataGridData.idField,
        fit: true,
        fitColumns: DataGridData.fitColumns == undefined ? true : DataGridData.fitColumns,
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
        url: encodeURI(CommonGetDataUrl),
        queryParams: queryParams,
        columns: DataGridData.BaseColumns == undefined ? "" : eval(DataGridData.BaseColumns),
        frozenColumns: DataGridData.frozenColumnsJson == undefined ? "" : eval(DataGridData.frozenColumnsJson),
        pagination: isPagination,
        showHeader: true,
        showFooter: true,
        rownumbers: true,
        onDblClickCell: function (index, field, value) {
            var hasDblClickCellEvent = DataGridData.hasDblClickCellEvent == undefined ? false : DataGridData.hasDblClickCellEvent
            if (hasDblClickCellEvent) {
                if ($.isFunction(DblClickCellEvent)) {
                    DblClickCellEvent(index, field, value, DataGridData, DataGrid)
                }
            }
            else {
                var IsRowEdit = DataGridData.IsRowEdit == undefined ? false : DataGridData.IsRowEdit
                if (IsRowEdit) {
                    onClickRow(index, field, value)
                }
            }
        },
        onDblClickRow: function (index, row) {
            var hasDblClickRowEvent = DataGridData.hasDblClickRowEvent == undefined ? false : DataGridData.hasDblClickRowEvent
            if (hasDblClickRowEvent) {
                if ($.isFunction(DblClickRowEvent)) {
                    DblClickRowEvent(index, row, DataGridData, DataGrid)
                }
            }
        },
        onSortColumn: function (sort, order) {
            //UserDataGridLoad(sort, order, "", DataGridData, DataGrid)
            _IsSort = true
            ReloadDataGrid(sort, order, "", DataGridData, DataGrid)
            var HasOnSortEvent = DataGridData.HasOnSortEvent == undefined ? false : DataGridData.HasOnSortEvent

            if (HasOnSortEvent) {
                if ($.isFunction(OnSortEvent)) {
                    OnSortEvent(sort, order, DataGridData, DataGrid)
                }
            }

        },
        onLoadError: function (o) {
            debugger
        },
        onLoadSuccess: function (o) {
            if (o.total == 0)
                scrollShow(DataGrid);

            var HasLoadSuccessEvent = DataGridData.HasLoadSuccessEvent == undefined ? false : DataGridData.HasLoadSuccessEvent
            if (HasLoadSuccessEvent) {
                if ($.isFunction(LoadSuccessEvent)) {
                    LoadSuccessEvent(DataGridData)
                }
            }

        },
        onBeforeEdit: function (index, row) {
            // _EditRow = row
            _EditRow = []
            $.extend(_EditRow, row);
            _EditChange = false
        },
        onAfterEdit: function (index, row, changes) {
            var c = getChangesRowinfo()
            for (var key in c) {
                var ov = GetOldValue(key)
                if (ov == null) ov = ""
                if (ov != c[key])
                    _EditChange = true
            }
            if (!_EditChange) reject()
        },
        error: function (o) {
            debugger
        }
    });

    if (isPagination) {
        var Bp = $(DataGrid.datagrid('getPager'));
        Bp.pagination({
            pageSize: pageSize,
            beforePageText: '第',
            afterPageText: '页  共 {pages} 页',
            displayMsg: '显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
        });
    }

    //****************************************
    //var datagridFilter = DataGridData.datagridFilter == undefined ? "" : DataGridData.datagridFilter;
    //var datagridFilterColumn = DataGridData.datagridFilterColumn == undefined ? "" : DataGridData.datagridFilterColumn;

    //if (datagridFilter != "" && datagridFilterColumn != "") {
    //    DataGrid.datagrid({
    //        filterBtnIconCls: 'icon-filter',
    //        remoteFilter: true
    //    });
    //    DataGrid.datagrid('enableFilter');
    //    DataGrid.datagrid('enableFilter', eval(datagridFilterColumn));	 
    //}
    //****************************************


    // setCommonDefinedToolBars(DataGridData, DataGrid);
}

function GetCommonQueryParams(DataGridData) {
    var Q =
        {
            keyWordValue: DataGridData.BaseKey == undefined ? "" : DataGridData.BaseKey,
            ResID: DataGridData.BaseResID == undefined ? "" : DataGridData.BaseResID,
            UserID: DataGridData.UserID == undefined ? "" : DataGridData.UserID,
            CustomQueryStr: DataGridData.BaseCustomQueryStr == undefined ? "" : DataGridData.BaseCustomQueryStr,
            QueryKey: DataGridData.BaseQueryKey == undefined ? "" : DataGridData.BaseQueryKey,
            Condition: _DataGridSearchCondition
        }

    return Q;
}


function GetGridWidth(DataGridData) {
    var PageWidth = 0;
    var PageType = DataGridData.PageType == undefined ? 0 : DataGridData.PageType

    if (PageType == '0')
        PageWidth = parent.$("#centerModel_id").width()
    else if (PageType == '1')
        PageWidth = parent.$("#divParentContent").width() - 11
    else if (PageType == '2')
        PageWidth = parent.$("#divZSFAContent").width() - 11

    return PageWidth;

}


function GetGridHeight(DataGridData) {
    var PageHeight = 0;
    var PageType = DataGridData.PageType == undefined ? 0 : DataGridData.PageType

    if (PageType == '0')
        PageHeight = parent.$("#centerModel_id").height()
    else if (PageType == '1')
        PageHeight = parent.$("#divParentContent").height()
    else if (PageType == '2')
        PageHeight = parent.$("#divZSFAContent").height()

    return PageHeight;

}

function InitializationUserDataGridOfHasChild(sort, order, DataGridData) {
    var DataGrid = $(DataGridData.InitializationStr)
    var GridHeight = DataGridData.GridHeight == undefined ? 550 : DataGridData.GridHeight
    var GridWidth = DataGridData.GridWidth == undefined ? 1000 : DataGridData.GridWidth

    var CommonGetDataUrl = DataGridData.CommonGetDataUrl == undefined ? "" : DataGridData.CommonGetDataUrl

    var queryParams = DataGridData.queryParams == undefined ? GetCommonQueryParams(DataGridData) : DataGridData.queryParams

    var pageSize = DataGridData.pageSize == undefined ? 20 : DataGridData.pageSize

    var toolbarDIV = DataGridData.toolbarDIV == undefined ? "" : DataGridData.toolbarDIV   
    
    DataGrid.datagrid({
        height: GridHeight,
        Width: GridWidth,
        toolbar: toolbarDIV,
        nowrap: true,
        border: true,
        striped: true,
        singleSelect: DataGridData.singleSelect == undefined ? true : DataGridData.singleSelect,
        pageSize: pageSize,
        fit: true,
        fitColumns: DataGridData.fitColumns == undefined ? true : DataGridData.fitColumns,
        loadFilter: function (data) {
            if (data == null) {
                $(this).datagrid("load");
                return $(this).datagrid("getData");
            } else {
                return data;
            }
        },
        url: encodeURI(CommonGetDataUrl),
        queryParams: queryParams,
        columns: DataGridData.BaseColumns == undefined ? "" : eval(DataGridData.BaseColumns),
        pagination: true,
        showHeader: true,
        showFooter: true,
        rownumbers: true,
        onLoadSuccess: function (o) {
            //if (o.total == 0)
            //    scrollShow(DataGrid);

            var HasLoadSuccessEvent = DataGridData.HasLoadSuccessEvent == undefined ? false : DataGridData.HasLoadSuccessEvent
            if (HasLoadSuccessEvent) {
                if ($.isFunction(LoadSuccessEvent)) {
                    LoadSuccessEvent(DataGridData)
                }
            }
        },
        onSelect: function (index, row) {

        },
        view: detailview,
        detailFormatter: function (BaseIndex, BaseRow) {
            return '<div id="Base_' + BaseIndex + '" style="margin:2px;" name="BaseDDV" ><table class="ddv" id="ddv_' + BaseIndex + '" ></table></div>';
        },
        onExpandRow: function (BaseIndex, BaseRow) {

            // $("div[name=BaseDDV][id!=Base_" + BaseIndex + "]").parent().remove();
            //  $("table[class=ddv]").parent().remove();

            var ExpandRow = $("#ExpandRow").val();
            if (ExpandRow != '') {
                $(this).datagrid("collapseRow", ExpandRow);
                $("#ExpandRow").val('');
            }

            if ($(this).datagrid('getRowDetail', BaseIndex).find('table.ddv').length == 0) {
                var str = '<div id="Base_' + BaseIndex + '" style="margin:2px;" name="BaseDDV" ><table class="ddv" id="ddv_' + BaseIndex + '" ></table></div>'
                $(this).datagrid('getRowDetail', BaseIndex).append(str)
            }


            var ddv = $(this).datagrid('getRowDetail', BaseIndex).find('table.ddv');
            var kuandu = DataGrid.prev(".datagrid-view2").find(".datagrid-header-row").width();

            var getChildDatagridTabListUrl = DataGridData.getChildDatagridTabListUrl == undefined ? GetCommonPath() + encodeURI("Common/getChildDatagridTabList_ajax.aspx?selectRows=" + JSON.stringify(BaseRow) + "&tabHeight=400&tabWidth=" + kuandu) : DataGridData.getChildDatagridTabListUrl
            $.ajax({
                type: "POST",
                url: getChildDatagridTabListUrl,
                data: {
                    "json": JSON.stringify(BaseRow)
                },
                success: function (ajaxStr) {
                    ddv.html(ajaxStr);
                    $("#ExpandRow").val(BaseIndex);
                    DataGrid.datagrid('fixDetailRowHeight', BaseIndex);
                }
            });
        },
        onCollapseRow: function (BaseIndex, BaseRow) {
            $(this).datagrid('getRowDetail', BaseIndex).find('table.ddv').remove();
            $("#ExpandRow").val('');
        },
        onSortColumn: function (sort, order) {
            // UserDataGridLoad(sort, order, "", DataGridData, DataGrid)
            ReloadDataGrid(sort, order, "", DataGridData, DataGrid)
        }, error: function (i) {

        }

    });

    var Bp = $(DataGrid.datagrid('getPager'));

    Bp.pagination({
        pageSize: pageSize,
        beforePageText: '第',
        afterPageText: '页  共 {pages} 页',
        displayMsg: '显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
    });

   // setCommonDefinedToolBarsByTB(DataGridData, DataGrid);
    //setCommonDefinedToolBars(DataGridData, DataGrid);
}

function SetAssociatedFieldsForDataGrid(AssociatedFields, rows) {

    if (MasterTableAssociation == "" || MasterTableAssociation == undefined || FieldValues == undefined)
        return "";

    if (AssociatedFields.indexOf(',') > 0) {
        var Fields = AssociatedFields.split(',')
        var Fieldstr = ""
        for (var i = 0; i < Fields.length; i++) {
            Fieldstr = Fieldstr + " and " + GetAssociatedFieldsStrForDataGrid(Fields[i], rows)
        }
        return Fieldstr
    }
    else {
        return GetAssociatedFieldsStrForDataGrid(AssociatedFields, rows)
    }
}


function GetAssociatedFieldsStrForDataGrid(AssociatedFields, FieldValues) {
    if (AssociatedFields.indexOf('=') > 0) {
        var AssociatedField = AssociatedFields.split('=')[0]
        var ValueKey = AssociatedFields.split('=')[1]
        for (var key in FieldValues) {
            if (key == AssociatedField) {
                return ValueKey + " = '" + FieldValues[key] + "'"
            }
        }
    }
}


function GetOldValue(Searchkey) {
    for (var key in _EditRow) {

        if (key == Searchkey) {
            return _EditRow[key]
        }
    }
    return ""
}
function Get() {



    $.ajax({
        type: "POST",
        url: CommonGetDataUrl,
        // dataType: "json",
        data: queryParams,
        success: function (BaseColumns) {
        }
    })
}

function setCommonDefinedToolBars(DataGridData, obj) {

    var toolbarDIV = DataGridData.toolbarDIV == undefined ? "" : DataGridData.toolbarDIV

    if (toolbarDIV != "") {
        obj.datagrid({ toolbar: toolbarDIV });
        return
    }
    var AddDefinedToolBars = DataGridData.AddDefinedToolBars == undefined ? "" : DataGridData.AddDefinedToolBars

    var DelDefinedToolBars = DataGridData.DelDefinedToolBars == undefined ? "" : DataGridData.DelDefinedToolBars

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
}


function setCommonDefinedToolBarsByTB(DataGridData, obj) {

    var toolbarDIV = DataGridData.toolbarDIV == undefined ? "" : DataGridData.toolbarDIV

    if (toolbarDIV != "") {
        obj.datagrid({ toolbar: toolbarDIV });
        return
    }
}



function ResizeDataGrid(obj, height) {
    obj.css({ height: height });
    obj.datagrid('resize', {
        height: height
    })
}

//通用导出方法
function CommonReport() {

    if (_DataGridData == undefined) return
    //if (event.srcElement.id == undefined) return

    var ReportGetDataUrl = _DataGridData.ReportGetDataUrl == undefined ? "" : _DataGridData.ReportGetDataUrl

    var ReportGetDataQueryParams = _DataGridData.ReportGetDataQueryParams == undefined ? [] : _DataGridData.ReportGetDataQueryParams

    var SelectReportField = ReportGetDataQueryParams.SelectReportField == undefined ? false : ReportGetDataQueryParams.SelectReportField

    var hasHeaderSearch = _DataGridData.hasHeaderSearch == undefined ? false : _DataGridData.hasHeaderSearch

    if (hasHeaderSearch) {
        var SearchCondition = _headerSearchCondition
        // ReportGetDataQueryParams.UserDefinedSql = _UserDefinedSql
        ReportGetDataQueryParams.Condition = SearchCondition
    }
     
    var index = layer.load(1); //换了种风格
    $.ajax({
        type: "POST",
        url: ReportGetDataUrl,
        dataType: "json",
        data: ReportGetDataQueryParams,
        success: function (centerJson) {
            $.ajax({
                type: "POST",
                url: "../Common/WKSY_GetInfo_ajax.aspx?typeValue=ReportToExcel",
                data: {
                    "keyWordValue": _DataGridData.BaseKey == undefined ? "" : _DataGridData.BaseKey,
                    "ReportKey": _DataGridData.ReportKey == undefined ? "" : _DataGridData.ReportKey,
                    "UserID": "",
                    "JsonData": JSON.stringify(centerJson),
                    "ExcelName": _DataGridData.ExcelName == undefined ? "" : _DataGridData.ExcelName,
                    "DynamicHeadReportStr": _DataGridData.DynamicHeadReportStr == undefined ? "" : _DataGridData.DynamicHeadReportStr,
                    SelectReportField: SelectReportField
                },
                success: function (Result) {
                    if (Result == "") {
                        //关闭
                        layer.close(index);
                        var message = _DataGridData.ReportErrorMessage == undefined ? "导出失败,数据集为空！" : _DataGridData.ReportErrorMessage
                        alert(message)
                    }
                    else {
                        //关闭
                        layer.close(index);
                        alert("导出成功，稍后会自动下载！")
                        window.location = Result
                    }

                }, error: function (i) {
                    //关闭
                    layer.close(index);
                    debugger
                }
            });
        },
        error: function (i) {
            //关闭
            layer.close(index);
            debugger
        }
    });
}

var editIndex = undefined;
var editfieldStr = undefined;

function endEditing(fieldStr) {
    var DataGridObj = $(_DataGridData.InitializationStr)
    if (fieldStr != "") editfieldStr = fieldStr
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    var isSynchronousDatabase = _DataGridData.isSynchronousDatabase == undefined ? false : _DataGridData.isSynchronousDatabase
    if (IsRowEdit) {
        if (editIndex == undefined) { return true }
        if (DataGridObj.datagrid('validateRow', editIndex)) {
            var ed = DataGridObj.datagrid('getEditor', { index: editIndex, field: editfieldStr });
            var productname = $(ed.target).combobox('getText');
            DataGridObj.datagrid('getRows')[editIndex][editfieldStr] = productname;
            DataGridObj.datagrid('endEdit', editIndex);
            if (isSynchronousDatabase && _EditChange) {
                if (confirm("您已修改此行数据，是否要同步到数据库中？")) {
                    SynchronousDatabase(DataGridObj.datagrid('getRows')[editIndex])
                    editIndex = undefined;
                    editfieldStr = undefined;
                    return false;
                }
                else {
                    reject()
                    alert("您已撤销对此行的修改！")
                }
            }
            editIndex = undefined;
            editfieldStr = undefined;
            return true;
        } else {
            return false;
        }
    }
}

function onClickRow(index, field, value) {

    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    if (IsRowEdit) {
        if (editIndex != index) {
            if (endEditing(field)) {
                DataGridObj.datagrid('selectRow', index)
                        .datagrid('beginEdit', index);
                editIndex = index;
            } else {
                DataGridObj.datagrid('selectRow', editIndex);
            }
        }
    }
}

function append() {

    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit

    if (IsRowEdit) {
        if (endEditing("")) {
            DataGridObj.datagrid('appendRow', { status: 'P' });
            editIndex = DataGridObj.datagrid('getRows').length - 1;
            DataGridObj.datagrid('selectRow', editIndex)
                    .datagrid('beginEdit', editIndex);
        }
    }
}

function removeit() {
    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    if (IsRowEdit) {
        if (editIndex == undefined) { return }
        DataGridObj.datagrid('cancelEdit', editIndex)
                .datagrid('deleteRow', editIndex);
        editIndex = undefined;
    }
}

function accept() {
    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    if (IsRowEdit) {
        if (endEditing("")) {
            DataGridObj.datagrid('acceptChanges');
        }
    }
}

function reject() {
    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    if (IsRowEdit) {
        DataGridObj.datagrid('rejectChanges');
        editIndex = undefined;
    }
}

function getChanges() {
    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    if (IsRowEdit) {
        var rows = DataGridObj.datagrid('getChanges');
        alert(rows.length + ' rows are changed!');
    }
}

function getChangesRowinfo() {
    var DataGridObj = $(_DataGridData.InitializationStr)
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    if (IsRowEdit) {
        var rows = DataGridObj.datagrid('getChanges');
        if (rows.length > 0)
            return rows[0]
    }
    return ""
}


function SynchronousDatabase(row) {

    var SaveRecid = (row.ID == undefined ? "" : row.ID)
    if (SaveRecid == "")
        SaveRecid = (row.id == undefined ? "" : row.id)

    var SaveResid = _DataGridData.SaveResid == undefined ? "" : _DataGridData.SaveResid
    var IsRowEdit = _DataGridData.IsRowEdit == undefined ? false : _DataGridData.IsRowEdit
    var isSynchronousDatabase = _DataGridData.isSynchronousDatabase == undefined ? false : _DataGridData.isSynchronousDatabase
    var DataGridObj = $(_DataGridData.InitializationStr)
    if (IsRowEdit && isSynchronousDatabase && SaveResid != "" && SaveRecid != "") {
        var json = ""
        for (var key in row) {
            if (key != "id" && key != "ID" && key != "ck" && key != "rownum") {
                if (json != "") json += ","
                json += ("'" + key + "':'" + row[key] + "'")
            }
        }

        if (json == "") return
        var SaveJson = "[{" + json + "}]"

        $.ajax({
            type: "POST",
            dataType: "json",
            data: { "Json": "" + SaveJson + "" },
            url: "../Common/CommonAjax_Request.aspx?typeValue=SaveInfo&ResID=" + SaveResid + "&RecID=" + SaveRecid + "&UserID=" + _UserID,
            success: function (obj) {
                if (obj.success || obj.success == "true") {
                    alert("数据同步成功！！")
                    DataGridObj.datagrid('reload')
                } else {
                    alert("数据同步失败,请刷新页面！");
                }
            },
            error: function (o) {
                debugger
            }
        });
    }
}


function GetSearchColumnsJson(BaseColumn) {

    var b = eval(BaseColumn)[0];
    var search = []
    for (var i = 0; i < b.length; i++) {
        search.push({
            FieldID: b[i].field,
            FieldName: b[i].field,
            FieldType: 1
        });
    }
}

function StartSeniorSearch(DataGridData, SeniorSearchStr) {

    if (SeniorSearchStr == "") {
        alert("查询条件错误！")
        return
    }


    var DataGridObj = $(DataGridData.InitializationStr)
    var SelectDataqueryParams = DataGridData.queryParams;
    var SearchCondition = ""

    SelectDataqueryParams.Condition = SeniorSearchStr
    DataGridObj.datagrid('load', SelectDataqueryParams)

}