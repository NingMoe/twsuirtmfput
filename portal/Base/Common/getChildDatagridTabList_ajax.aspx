<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getChildDatagridTabList_ajax.aspx.cs" Inherits="Base_Common_getChildDatagridTabList_ajax" %>
<script src="../../Scripts/GridRowContextMenu.js"></script>
<script src="../../Scripts/CommonJS.js"></script>
<script src="../../Scripts/UserDefinedToolBar_js.js"></script>
<script src="../../Scripts/ToolBarNewJS.js"></script>

<script type="text/javascript">
     
        var MasterTableAssociation = eval('<% = MasterTableAssociationstr %>');
        var _UserID='<%=UserID %>';
        var _UserName='<%=CurrentUser.Name %>';
        var Selectkey="";
        var SelectResid="";
        var _SelectTitle="";
        var _sort="", _order="";
        var CommonPath = GetCommonPath()
        var ChildDataGirdTabList=eval('<%=ChildDataGirdTabList %>');
        var selectRows =eval('[<%=selectRows %>]')[0];
        var _SelectRowData="";
        var ChildGrid="";
        var ChildGridID=""
    $(document).ready(function () {
         
            $("#DPListTabs").tabs({
                height:"<%=tabHeight %>",
                width:"<%=tabWidth %>",
                onSelect: function (title, index) {
                    var keyWordValue = $("#<%=ResID %>" + title).attr("key");
                    Selectkey = keyWordValue
                    var ConditionStr= "";
                    QuerySelectTitle(ConditionStr,title) 
                }
            });
        });

    $.extend($.fn.datagrid.methods, {
        addToolbarItem : function (jq, items) {
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
                        b[0].onclick = eval(btn.handler || function () {});
                        b.linkbutton($.extend({}, btn, {
                            plain : true
                        }));
                    }
                }
            });
        },
        removeToolbarItem : function (jq, param) {
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



    function QuerySelectTitle(ConditionStr,SelectTitle) {
        var keyWordValue = $("#<%=ResID %>" + SelectTitle).attr("key");
        var Master=GetMasterTableAssociationIndex(keyWordValue);
        SelectResid= MasterTableAssociation[Master].ChildResId
        var  详细记录div = "<%=ResID %>" + SelectTitle ;       
        $("#" + 详细记录div ).css({ height: "<%=tabHeight %>" - 35 });
        _SelectTitle=SelectTitle
        loadCenterAddGrid(keyWordValue, SelectTitle, SelectResid, "Grid" + SelectTitle + "_" + SelectResid,ConditionStr);
    }


    function loadCenterAddGrid(keyWordValue, titleValue, ResID, GridID,ConditionStr) {
           
        var CommonGetDataUrl = "../Common/CommonGetInfo_ajax.aspx?typeValue=GetDataByType";
        var index=GetChildDataGirdTabListIndex(keyWordValue);
        var DynamicHeadqueryParamsChild = ChildDataGirdTabList[index].DynamicHeadqueryParamsChild;
        var ChildUserDefinedSql= ChildDataGirdTabList[index].ChildUserDefinedSql;
        var ChildORDERBY=ChildDataGirdTabList[index].ChildORDERBY;
      
        var Master=GetMasterTableAssociationIndex(keyWordValue);
        var con=  SetAssociatedFieldsForDataGrid(MasterTableAssociation[Master].LedgerConditions,selectRows);
        
        var ProcInPutStrByHead =ChildDataGirdTabList[index].ProcInPutStrByHead
        var ProcInPutStr=SetProcInPutStr(ProcInPutStrByHead,selectRows,false)
        ProcInPutStr=ProcInPutStr.replace("#key#",keyWordValue);
        ProcInPutStr=ProcInPutStr.replace(/\|\|/g,"'");
          
        var GetDynamicHeadqueryParams = {
            UserDefinedSql: ChildUserDefinedSql,
            SortField: "",
            SortBy: "",
            Condition: '',
            ROW_NUMBER_ORDER: ChildORDERBY,
            IsExport: 1,
            ProcName: ChildDataGirdTabList[index].ProcName,
            ProcInPutStr:ProcInPutStr,
            DynamicHeadReportStr: JSON.stringify(DynamicHeadqueryParamsChild),
            ByType:0
        }
         
        $.ajax({
            type: "POST",
            url: "../Common/CommonGetInfo_ajax.aspx?typeValue=GetDataOfReportOfDynamicHeadReportColumnStrBySqlStr",
            data: GetDynamicHeadqueryParams,
            success: function (BaseColumns) {

                var BaseColumn = BaseColumns.split("#")[0]
                var datagridFilterColumn = BaseColumns.split("#")[1]
                var frozenColumnsJson = BaseColumns.split("#")[2]
                var GridHeight = $(window).height() - 40
                _BaseColumn = BaseColumn
           
                var ProcInPutStr=SetProcInPutStr(ChildDataGirdTabList[index].ProcInPutStr,selectRows,false)
                ProcInPutStr=ProcInPutStr.replace("#key#",keyWordValue);
                ProcInPutStr=ProcInPutStr.replace(/\|\|/g,"'");

                var ChildSelectDataqueryParams={
                    ByType:0,
                    ProcName: ChildDataGirdTabList[index].ProcName,
                    ProcInPutStr:ProcInPutStr,
                }

                var centerHeight = "<%=tabHeight %>";

                if($("#" + GridID).next().length>0)
                {
                    ChildGrid =  $("#" + GridID)
                    ReloadBaseGrid()
                    ChildGrid.datagrid({
                        columns: eval(BaseColumn)
                    });
                    return
                }
                ChildGridID = GridID;
                ChildGrid =  $("#" + GridID).datagrid({
                    height: centerHeight - 30,
                    nowrap: true,
                    border: true,
                    striped: true,
                    singleSelect: true,
                    pageList: [100, 30, 40, 50],
                    pageSize: "<%=PageSize %>",
                    fit: true,
                    pagination: true,
                    fitColumns: false,
                    rownumbers: true,
                    loadFilter: function (data) {
                        if (data == null) {
                            $(this).datagrid("load");
                            return $(this).datagrid("getData");
                        } else {
                            return data;
                        }

                    },
                    url:CommonGetDataUrl,
                    queryParams: ChildSelectDataqueryParams,
                    columns: eval(BaseColumn), //把字符串转成对象。  
                    fitColumns: true,
                    onSortColumn: function (sort, order) {
                        _sort=sort
                        _order=order
                    },
                    rowStyler:function(index,row){//改变行颜色
                        // return  SetFieldStyle(keyWordValue, index, row, ""); 
                    }
                    
                });
            
                var DefinedToolBars = ChildDataGirdTabList[index].DefinedToolBars;
                if( DefinedToolBars.hasPermission == "1")
                    CommonSetUserDefinedToolBars('',keyWordValue,ChildGrid,0, ResID, DefinedToolBars.addUrl, DefinedToolBars.isAddDisabled, DefinedToolBars.editUrl, DefinedToolBars.IsUpdateDisabled, DefinedToolBars.isDeleteDisabled, DefinedToolBars.DialogWidth, DefinedToolBars.DialogHeight,false)

                var p = $("#" + GridID).datagrid('getPager');
                $(p).pagination({
                    beforePageText: '第',
                    pageSize: <%=PageSize %>,
                    afterPageText: '页  共 {pages} 页',
                    displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
                });
            }
        });
    }

     
    function GetMasterTableAssociationIndex(key) {
        if (MasterTableAssociation == "" || MasterTableAssociation == undefined)
            return -1;

        for (var i = 0; i < MasterTableAssociation.length; i++) {
            if (MasterTableAssociation[i].ChildKeyWord == key)
                return i;
        }
        return -1;
    }

    function GetChildDataGirdTabListIndex(key) {
        if (ChildDataGirdTabList == "" || ChildDataGirdTabList == undefined)
            return -1;

        for (var i = 0; i < ChildDataGirdTabList.length; i++) {
            if (ChildDataGirdTabList[i].ChildKeyWord == key)
                return i;
        }
        return -1;
    }

    function SetProcInPutStr(InPutStr,rows,isEmpty)
    {
        if (MasterTableAssociation == "" || MasterTableAssociation == undefined || rows == undefined)
            return "";
         
        if (InPutStr.indexOf(',') > -1) {
            var AssociatedFields = InPutStr.replace(/\|\|/g,'').replace(/#/g,'')
            var Fields = AssociatedFields.split(',')
            var Fieldstr = InPutStr
            for (var i = 0; i < Fields.length; i++) {

                if(isEmpty)
                {
                    Fieldstr = Fieldstr.replace("#" + Fields[i] + "#", "")
                }
                else
                {
                    var s=GetFieldValue(Fields[i], rows);
                    if(s != undefined)
                        Fieldstr = Fieldstr.replace("#" + Fields[i] + "#", s)
                }
            }
            return Fieldstr
        }
        else {
            var AssociatedFields = InPutStr.replace(/\|\|/g,'').replace(/#/g,'')
            var s=GetFieldValue(AssociatedFields, rows);
            var Fieldstr = InPutStr
            if(isEmpty)
            {
                Fieldstr =Fieldstr.replace("#" + AssociatedFields + "#","")
            }
            else
            {
                if(s != undefined)
                    Fieldstr =Fieldstr.replace("#" + AssociatedFields + "#",s)
            }
            return Fieldstr
        }
    }


    
    function setUserDefinedToolBars(DefinedToolBars)
    { 
        if (DefinedToolBars != "")
            CenterGrid.datagrid("addToolbarItem",eval(DefinedToolBars))
        // InitializeToolBars();
    }

    function GetFieldValue(Field, FieldValues) 
    { 
         for (var key in FieldValues) {
             if (key == Field) {
                 return  FieldValues[key]
             }
        }
    }


    function ReloadBaseGrid() {

        var c = $("#ChildInitialQuery" + _SelectTitle).val() == "" ? "" : " and " + $("#ChildInitialQuery" + _SelectTitle).val().replace(/{/g, "'").replace(/}/g, "'");

        ChildGrid.datagrid('load', {
            ResID: SelectResid,
            keyWordValue: Selectkey,
            Condition: c + " " + CommonConditionStr,
            sort: _sort,
            order: _order
        });
    }

    function SetAssociatedFieldsForDataGrid(AssociatedFields,rows) {

        if (MasterTableAssociation == "" || MasterTableAssociation == undefined || rows == undefined)
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

    function Add_ToolBar2(addUrl,DialogWidth,DialogHeight)
    {
        fnFormListDialog(CommonPath + addUrl  , DialogWidth,DialogHeight,"添加信息");
    }

            function Edit_ToolBar2(editUrl,DialogWidth,DialogHeight)
            {
                var selected= ChildGrid.datagrid('getSelected')
                fnFormListDialog(CommonPath  + editUrl  + "?RecID=" + selected.ID , DialogWidth,DialogHeight,"修改信息");
            }

            function Del_ToolBar2(ResID)
            {
                var  selected= ChildGrid.datagrid('getSelections')

                if (selected != null) {
                    if (window.confirm("确定要删除吗？") == true) {
                        var RecIDStr = "";
                        for(var i=0;i<selected.length;i++)
                        {
                            RecIDStr += "," + selected[i].ID;
                        }
                        if(RecIDStr != "")
                        {
                            $.ajax({
                                type: "POST",
                                url:CommonPath +  "Common/Ajax_Request.aspx?typeValue=DeleteRows&ResID=" + ResID + "&RecIDStr=" + RecIDStr,
                                success: function (ajaxStr) {
                                    var s = ajaxStr[0]
                                    alert("删除成功 " + s.SucessCount + "个，失败" + (s.Count - s.SucessCount) + "个！");
                                    ChildGrid.datagrid("reload");
                                }
                            });
                        }
                    }
                }
                else
                {
                    alert("请选择一条记录!")
                }
            }
      
            function openDialog(url,title,DialogWidth,DialogHeight)
            {
                if (DialogHeight == 0)
                    DialogHeight = parent.$("#westModel_id").height() + 10;
                if (DialogWidth == 0) {
                    DialogWidth = document.documentElement.clientWidth - 190;
                    if (DialogWidth > 1200) DialogWidth = 1200;
                }

                $('#divReportPage').append($("<iframe scrolling='no' id='FromInfo' frameborder='0' marginwidth='0' marginheight='0' style='width:100%;height:100%;'></iframe")).dialog({
                    title: title,
                    width: DialogWidth,
                    height: DialogHeight,
                    cache: false,
                    closed: true,
                    shadow: false,
                    closable: true,
                    draggable: true,
                    resizable: false,
                    onClose: function () {
                        $('#FromInfo:gt(0)').remove();
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
                $('#divReportPage').dialog('open');
            }
</script>


 <div id="DPListTabs">
           <% foreach (MasterTableAssociation t in AssociationList)
               { %>
        <div key="<%=t.ChildKeyWord%>" title="<%=t.ShowTitle %>" id="<%=ResID %><%=t.ShowTitle %>"
                    style="padding: 5px;">
                    <table id="Grid<%=t.ShowTitle %>_<%=t.ChildResId %>">
                        <input type="hidden" value="<%=t.LedgerConditions %>" id="ChildGridCondition<%=t.ShowTitle%>" />
                          <input type="hidden"
                               value="<%=t.InitialQueryStr%>" id="ChildInitialQuery<%=t.ShowTitle %>" />
                    </table>
                </div>
     <% } %>
    </div>