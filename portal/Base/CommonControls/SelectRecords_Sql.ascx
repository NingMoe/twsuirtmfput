<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectRecords_Sql.ascx.cs" Inherits="CommonControls_SelectRecords_Sql" %>

<link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.4.3/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.4.3/themes/icon.css" /> 
<script type="text/javascript" src="../../Scripts/jquery-easyui-1.4.3/jquery.min.js"></script>
<script type="text/javascript" src="../../Scripts/jquery-easyui-1.4.3/jquery.easyui.min.js"></script> 
<script src="../../Scripts/SetRead.js" type="text/javascript" ></script>
<script src="../../Scripts/OtherJs.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    
    var CommonPath =  GetApplicationPath() ; 
    var ResID_KeyWidth<%=this.ClientID %> = Number('<%=ControlWidth %>');
    var GetUrl<%=this.ClientID %> = CommonPath + "Base/Common/Ajax_Request.aspx?typeValue=GetDataOfPage";
    var SetValueStr<%=this.ClientID %> = '<%=SetValueStr %>'
    if (!Array.prototype.forEach) {
        Array.prototype.forEach = function (fun /*, thisp*/) {
            var len = this.length;
            if (typeof fun != "function")
                throw new TypeError();

            var thisp = arguments[1];
            for (var i = 0; i < len; i++) {
                if (i in this)
                    fun.call(thisp, this[i], i, this);
            }
        };
    }

    $(document).ready(function () {  
     
        LoadSender<%=this.ClientID %>("<%=SelectfieldValue %>"); 
    });

    function ChangeCombogrid()
    { //debugger
        if($('#<%=ResID_Key %>').combogrid("getValue")=="" || $('#<%=ResID_Key %>').combogrid("getValue")==undefined)
        { 
            $('#<%=ResID_Key %>').combogrid("setValue", $('#<%=SetValueResID + "_" + ColumnName %>').val());
        }
    }

    function SelectRecordsLoad<%=this.ClientID %>(sort, order, Key)
    { 
        $('#<%=ResID_Key %>').combogrid("grid").datagrid("reload",
            {
                ResID: "<%=SetValueResID %>",
                keyWordValue: "<%=keyWordValue %>",
                UserDefinedSql: "<%=UserDefinedSql %>",
                Condition: $("#SelectRecords<%=SetValueResID %>").val(), 
                sort: sort,
                order:order,
                'QueryKeystr': GetQueryKey<%=this.ClientID %>(Key),
                ROW_NUMBER_ORDER: '<%=ROW_NUMBER_ORDER %>'
            });
    }
    function LoadSender<%=this.ClientID %>(SelectfieldValue) {

        if ("<%=UserDefinedSql %>" != "") {
            GetUrl<%=this.ClientID %> = CommonPath + "Base/Common/CommonGetInfo_ajax.aspx?typeValue=GetDataByUserDefinedSql";
        }

        var vdisabled = false
        if ('<%=IsReadOnly %>'.toLowerCase() == 'true') vdisabled = true;
        if ("<%=SearchType %>".indexOf("readonly") != -1) {
            vdisabled = true;
        }
        
        $('#<%=ResID_Key %>').combogrid({           
            id: ResID_KeyWidth<%=this.ClientID %>,           
            panelWidth:<%=panelWidth %>,
            idField: '<%=idField %>',
            textField: '<%=textField %>',
            multiple: '<%=IsmultiSelect %>'.toLowerCase()=='true',
            rownumbers: true,
            selectOnCheck: true,
            checkOnSelect: true,
            pagination: true,//是否分页 
            pageSize: 20,//每页显示的记录条数，默认为10  
            pageList: [20],//可以设置每页记录条数的列表  
            showFooter: true,
            fitColumns: false,
            url: GetUrl<%=this.ClientID %>,
            queryParams: {
                ResID: "<%=SetValueResID %>",
                keyWordValue: "<%=keyWordValue %>",
                UserDefinedSql: "<%=UserDefinedSql %>", 
                Condition: $("#SelectRecords<%=SetValueResID %>").val(),
                ROW_NUMBER_ORDER: '<%=ROW_NUMBER_ORDER %>'
            },
            onSortColumn: function (sort, order) {
                SelectRecordsLoad<%=this.ClientID %>(sort, order, "");
            }, 
            columns: eval(SelectfieldValue),
            keyHandler: {
                up: function () {               //【向上键】押下处理  
                    //取得选中行  
                    var selected = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getSelected');
                    if (selected) {
                        //取得选中行的rowIndex  
                        var index = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getRowIndex', selected);
                        //向上移动到第一行为止  
                        if (index > 0) {
                            $('#<%=ResID_Key %>').combogrid('grid').datagrid('selectRow', index - 1);
                        }
                    } else {
                        var rows = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getRows');
                        $('#<%=ResID_Key %>').combogrid('grid').datagrid('selectRow', rows.length - 1);
                    }
                },
                down: function () {             //【向下键】押下处理  
                    //取得选中行  
                    var selected = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getSelected');
                    if (selected) {
                        //取得选中行的rowIndex  
                        var index = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getRowIndex', selected);
                        //向下移动到当页最后一行为止  
                        if (index < $('#<%=ResID_Key %>').combogrid('grid').datagrid('getData').rows.length - 1) {
                            $('#<%=ResID_Key %>').combogrid('grid').datagrid('selectRow', index + 1);
                        }
                    } else {
                        $('#<%=ResID_Key %>').combogrid('grid').datagrid('selectRow', 0);
                    }
                },
                enter: function () {             //【回车键】押下处理    
                    var selected = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getSelected');
                    if (selected) {
                        //取得选中行的rowIndex  
                        var index = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getRowIndex', selected);
                        $('#<%=ResID_Key %>').combogrid('grid').datagrid('selectRow', index );
                    } else {
                        $('#<%=ResID_Key %>').combogrid('grid').datagrid('selectRow', 0);
                    }
                    $('#<%=ResID_Key %>').combogrid('hidePanel');
                },
                query: function (q) {    
                    $('#<%=ResID_Key %>').combogrid('grid').datagrid('clearSelections');
                    //动态搜索
                    if ('<%=QueryKeyField %>' != '') {
                        $('#SelectRecordQueryKey<%=SetValueResID %>').val(q)
                        SelectRecordsLoad<%=this.ClientID %>("", "",  $('#SelectRecordQueryKey<%=SetValueResID %>').val());
                        $('#<%=ResID_Key %>').combogrid('grid').datagrid('clearSelections');
                        $('#<%=ResID_Key %>').combogrid("setValue", q);
                    }
                }
            },
            onChange: function (newValue, oldValue) { 
                $('#<%=ResID_Key %>').val(newValue)
                $('#<%=SetValueResID + "_" + ColumnName %>').val(newValue) 
            },
            onSelect: function (rowIndex, rowData) {  
                if ('<%=HasFirstOperation %>'.toLowerCase() == 'true') {
                    FirstOperation();
                } 
                SetNewnewValue<%=this.ClientID %>(rowIndex, rowData) 
                if ('<%=HasLastOperation %>'.toLowerCase() == 'true') {
                    LastOperation();
                }
            },
            onLoadSuccess: function () { 
            },
            error: function (d) {
            }
        });

        var p = $('#<%=ResID_Key %>').combogrid('grid').datagrid('getPager');
        $(p).pagination({
            beforePageText: '第',
            pageSize: <%=PageSize %>,
            afterPageText: '页  共 {pages} 页',
            displayMsg: '共 [{total}] 条记录'
        });
        
    }

    function GetQueryKey<%=this.ClientID %>(q) {
        var QueryKeyStr = ""
        if ( q.trim() == '')  return " "

        if ('<%=QueryKeyField %>' != '') {
                var QueryKey = '<%=QueryKeyField %>';
            if (QueryKey.indexOf(',') == -1) {
                QueryKeyStr = QueryKey + " like '%" + q.trim() + "%'";
            }
            else {
                QueryKeyFields = QueryKey.split(',');
                for (var j = 0; j < QueryKeyFields.length; j++) {
                    {
                        if (QueryKeyStr != "") QueryKeyStr += " or "
                        QueryKeyStr += QueryKeyFields[j] + " like '%" + q.trim() + "%'";
                    }
                }
            }
        }
        if (QueryKeyStr != "")
            QueryKeyStr += ' or ' + '<%=idField %>' + " like '%" + q.trim() + "%'";
        return QueryKeyStr
    }

    function SetNewnewValue<%=this.ClientID %>(rowIndex, rowData) {
    
        if (SetValueStr<%=this.ClientID %> != "") {

            if ('<%=IsmultiSelect %>'.toLowerCase() == 'true') 
            {
                LedgerChildKeyByRowdata("<%=SetValueResID %>", SetValueStr<%=this.ClientID %>, rowData, false,'<%=ResID_Key %>')
            }
            else
            { 
                LedgerChildKeyByRowdata("<%=SetValueResID %>", SetValueStr<%=this.ClientID %>, rowData, false,"")
            }
            SetBackgroundColor();
        }
    }



   

    function reload() {
        $('#<%=ResID_Key %>').combogrid('grid').datagrid('reload');
    }

    function getValue() {
        $('#<%=ResID_Key %>').combogrid('getValue');
    }

    function setValues(vals) {
        $('#<%=ResID_Key %>').combogrid('setValues');
    }
    function QueryKeyWordSearch()
    {
        if ('<%=QueryKeyField %>' != '') {
            $('#<%=ResID_Key %>').combogrid('showPanel');
            SelectRecordsLoad<%=this.ClientID %>("", "", $('#<%=ResID_Key %>').combogrid('getText'));
            $('#<%=ResID_Key %>').combogrid('grid').datagrid('clearSelections');
        }
    }

</script>



<input id="<%=ResID_Key %>" <%=MustWrite %> class="box3" />
 <% if (!string.IsNullOrWhiteSpace(MustWrite))
     { %>
<span style="color: red;">*</span>
<%} %>
<input type="hidden" value="" id="SelectRecords<%=SetValueResID %>" />
<input type="hidden" value="" id='<%=SetValueResID + "_" + ColumnName %>' onpropertychange="ChangeCombogrid()" />
<input type="hidden" value="" id="SelectRecordQueryKey<%=SetValueResID %>" />