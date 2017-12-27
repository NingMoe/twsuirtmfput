<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataDictionary.aspx.cs" Inherits="CommonDictionary_DataDictionary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=GetScript1_4_3  %>
</head>
<body>
<script type="text/javascript">
    $(document).ready(function () { 
        LoadDictionary(true); 
    });


    function LoadDictionary(IsLoadDrp) {
        $.ajax({
            type: "POST",
            url:  GetApplicationPath() + "Base/Common/CommonGetInfo_ajax.aspx?typeValue=GetDictionaryReturnColumn&ResourceID=<%=ResourceID %>&ResourceColumn=<%=ResourceColumn%>&IsMultiselect=<%=Convert.ToInt32(IsMultiselect)%>",
            success: function (fieldValueStr) {
                //debugger
                var fieldValueStrList = fieldValueStr.split("[#]");
                var fieldValue = fieldValueStrList[0];  
                if(IsLoadDrp){
                    var searchField = fieldValueStrList[1];
                    var jsonList = eval("(" + searchField + ")");
               
                    var strHtml = "<option  selected=\"selected\" value='' ></option>";
                    for (var i = 0; i < jsonList.length; i++) {
                        strHtml += "<option value='" + jsonList[i].field + "' >" + jsonList[i].field + "</option>";
                    }
                    $("#drpSearch").html(strHtml)
                }

                $("#OneRowDictionaryGrid").datagrid({
                    nowrap: true,
                    border: true,
                    striped: true, 
                    url:encodeURI( GetApplicationPath() + "Base/Common/CommonGetInfo_ajax.aspx?typeValue=GetDictionaryList&ResourceID=<%=ResourceID %>&ResourceColumn=<%=ResourceColumn%>"),
                    queryParams: {
                        SortField: "",
                        SortBy: "",
                        Condition: encodeURI("<%=Condition %>"+$("#hidSearchCondition<%=ResourceID %>").val()) 
                    },
                    loadFilter: function (data) {
                        if (data == null) {
                            $(this).datagrid("load");
                            return $(this).datagrid("getData");
                        } else {
                            return data;
                        }
                    },
                    columns: eval(fieldValue), //把字符串转成对象。
                    singleSelect:<%=(!IsMultiselect).ToString().ToLower() %>,
                    selectOnCheck:<%=IsMultiselect.ToString().ToLower() %>,
                    checkOnSelect:<%=IsMultiselect.ToString().ToLower() %>,
                    fitColumns: true,
                    rownumbers: true,
                    pagination: true,
                    onDblClickRow: function (rowIndex, rowData) {
                        SelectOneRow(rowIndex);
                    }
                });
                var p = $('#OneRowDictionaryGrid').datagrid('getPager');
                $(p).pagination({
                    pageSize: <%=PageSize %>,
                    beforePageText: '第',
                    afterPageText: '页  共 {pages} 页',
                    displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
                });  
            }
        })
    }
function fnDictionaryGridLoad(SortField, SortBy)
{ 
    $("#OneRowDictionaryGrid").datagrid('load', {
        Condition: encodeURI("<%=Condition %>"+$("#hidSearchCondition<%=ResourceID %>").val())
    });
} 

    function SearchCondition()
    {
        if($("#drpSearch").val()=="") alert('请选择搜索字段！');
        else{
            $("#hidSearchCondition<%=ResourceID %>").val(" and "+ $("#drpSearch").val() +" like '%"+$("#txtSearch").val()+"%'");
            fnDictionaryGridLoad("","");
        }
    }



    function SelectOneRow(RowData)
    { 
        var ControlsID="<%=ResourceID %>"+"_";
        var ChildIndex='<%=ChildIndex %>';
        if(ChildIndex!=-1) ControlsID+= ChildIndex+"_";
         
        for (var key in RowData)
        {  
            var strValue=RowData[key];
            if('<%=IsAppend %>'=='True')
            {
                strValue=  $("#"+ControlsID + key,window.parent.document).val()+","+strValue;
            }
            $("#"+ControlsID + key,window.parent.document).val(strValue);
        }
        window.parent.CloseDictionary();
    }

    function MultiselectRow()
    { 
        GetSelectedDataID();
    }

    //function fnGridLoad(Gridid)
    //{
    //    $("#"+Gridid).datagrid('load', { 
    //        Condition:  $("#drpSearch").val() +" like '%"+$("#txtSearch").val()+"%'"
    //        });
    //    }

    function GetSelectedDataID() {
        var inputList =  $("#divDataDictionary").find("input[name='ID']");
        // var inputList = $('#OneRowDictionaryGrid input[type=checkbox]') 
        var strID = "";
        for (var i = 0; i < inputList.length; i++) {
            if (inputList[i].checked) {
                if ($(inputList[i]).val() != "on") strID += "," + $(inputList[i]).val();
            }
        }
        if (strID.trim() != "") strID = strID.substr(1, strID.length - 1);
        return strID;
    }

 
</script>
    <form id="form1" runat="server">
    <div id="divDataDictionary">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResourceID %>">
        <tr>
            <td style="width:400px;">
                <select id="drpSearch"></select>&nbsp;&nbsp;<input type="text"id="txtSearch" />
                &nbsp;&nbsp;<input type="button" id="btnSearch" value="搜索" onclick="SearchCondition();" />
            </td>
            <td valign="middle">
                &nbsp;<a style="border: 0px;<%=(IsMultiselect?"":"display:none;")%> " href="#">
                <input type="image" id="fnChildSave" src="../../images/qd.jpg" style="padding: 3px 0px 0px 0px; border: 0px; " onclick="MultiselectRow(); return false;" /></a>
                <a style="border: 0px;" href="#" onclick="window.parent.CloseDictionary();">
                    <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;" onclick="return false;" /></a>
            </td>
        </tr>
    </table>
    <div id="divSearch<%=ResourceID %>" >
    </div>
    <table id="OneRowDictionaryGrid"></table>
    </div>
    <input type="hidden" id="hidSearchCondition<%=ResourceID %>" />
    </form>
</body>
</html>
