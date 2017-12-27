<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dictionary.aspx.cs" Inherits="Dictionary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script type="text/javascript">
    $(document).ready(function () {
        $('#ChooseTYWindow').dialog('setTitle', '请选择一行数据');
        //alert("Common/Ajax_Request.aspx?typeValue=GetDataOfPage&ResID=<%=ResID %>&keyWordValue=<%=keyWordValue %>");
        $.ajax({
              type: "POST",
              url: "Common/Ajax_Request.aspx?typeValue=SearchCondition&keyWordValue=<%=keyWordValue %>",
              success: function (fieldValue) 
              {
                  var html = fieldValue;
                  $("#divSearch<%=keyWordValue %>").html(html);
                  $.ajax({
                type: "POST",
                url: "Common/Ajax_Request.aspx?typeValue=GetField&keyWordValue=<%=keyWordValue %>",
                success: function (fieldValueStr) {
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    var fieldValue = fieldValueStrList[0];
                    fieldValue = fieldValue.substring(2);
                    fieldValue = "[[{title:'选择',field:'选择',width:50,align:'center' ,formatter:function(value,rowData,rowIndex){return '<a href=\"javascript:\" onclick=SelectOneRow('+rowIndex+')>选择</a>';}}," + fieldValue;
                    $("#OneRowDictionaryGrid").datagrid({
                    nowrap: true,
                    border: true,
                    striped: true,
                    singleSelect: true,
                    url: "Common/Ajax_Request.aspx?typeValue=GetDataOfPage&ResID=<%=ResID %>&keyWordValue=<%=keyWordValue %>",
                     queryParams: {
                          SortField: "",
                          SortBy: "",
                          Condition:  $("#hidSearchCondition<%=ResID %>").val() +$("#DictionaryConditon").val()
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
          })

    });
function fnDictionaryGridLoad(SortField, SortBy)
{
    $("#OneRowDictionaryGrid").datagrid('load', { 
        SortField:  SortField ,
        SortBy:  SortBy ,
        Condition:  $("#hidSearchCondition<%=ResID %>").val() +$("#DictionaryConditon").val()
    });
}
    function SelectOneRow(index) { 
        $("#OneRowDictionaryGrid").datagrid('selectRow', index);
        var row = $("#OneRowDictionaryGrid").datagrid('getSelected');
        if (row != null && row != "") {
            var Url = encodeURI("JQueryCallService.aspx?typeValue=getFieldCombo&ResID=<%=ParentResID %>&dispname=<%=dispname %>");
            $.ajax({
                type: "POST",
                url: Url,
                success: function (fieldValueStr) {
                //alert(fieldValueStr);
              debugger
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    var selValue = fieldValueStrList[1];
                    var selFieldList = selValue.split(",");
                    if (selFieldList.length > 0) {
                        for (var i = 0; i < selFieldList.length; i++) {
                            var selNameList = selFieldList[i].split(":");
                            if (selNameList.length > 0) {
                                var ParentFiledName = selNameList[0];
                                var DictionaryValue = selNameList[1];
                                //alert("<%=dispname %>");
                                $("#<%=FromResID %>_" + ParentFiledName).val(row[DictionaryValue]);
                            }
                        }
                    }
                    $('#ChooseTYWindow').dialog('close');
                }
            });
        } else {
            alert("请至少选择一条记录!");
            return;
        }
    }
</script>
    <form id="form1" runat="server">
    <div>
    <div id="divSearch<%=keyWordValue %>" >
    </div>
    <table id="OneRowDictionaryGrid"></table>
    </div>
    </form>
</body>
</html>
