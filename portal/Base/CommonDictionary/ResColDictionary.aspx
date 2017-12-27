<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResColDictionary.aspx.cs" Inherits="Base_CommonDictionary_ResColDictionary" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>    
    <style type="text/css" >
      body{ font-size:12px;margin:0px; padding:0px; background-color:#ffffff;font-family:微软雅黑,Calibri, Arial, Helvetica, sans-serif; color:#282828;} 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var centerHeight = document.documentElement.clientHeight;
        var centerWidth = $('#centerModel_id', window.parent.document).width() - 220;
        var GridHeight = 0;
        $(document).ready(function () {              
            loadResEmptyGrid(400, '<%=keyWordValue %>');             
        });
        function loadResEmptyGrid(GridHeight, keyWordValue) {        
            var titleValue = '系统配置';  
            $.ajax({
                type: "POST", 
                data: {
                    "ColName": '内部字段名,显示字段名,排序',
                    "ColField": '内部字段名,显示字段名,排序',
                    "ColWidth": '100,160,50'
                },
                url: "Ajax_Request.aspx?typeValue=GetfieldInfo&keyWordValue=<%=keyWordValue %>",   
                success: function (fieldValueStr) {
                    var ColumnsStr = "";
                    if ('<%=IsmultiSelect %>'.toLowerCase() == 'true') ColumnsStr = "{title:'操作',field:'操作',width:30,align:'center' ,formatter:function(value,rowData,rowIndex){return '<input type=\"checkbox\" name=\"ChkColName\" value=\"内部字段名:' + rowData.内部字段名 + ',显示字段名:' + rowData.显示字段名 + '\"  />';}},";
                    fieldValueStr = "[[" + ColumnsStr + fieldValueStr + "]]";
                    $('#CenterGrid' + keyWordValue).datagrid({
                        toolbar: [{
                            iconCls: 'icon-add',
                            text: "选择", 
                            handler: function () {
                                var rec = $('#CenterGrid' + keyWordValue).datagrid('getSelected');
                                if (rec == null)
                                {
                                    alert("请选择一列");
                                    return;
                                }
                                SelectedCheckBox(rec);
                            }
                        }],                     
                        fitColumns:true,
                        fit:true,                       
                        singleSelect: true,
                        pageSize: 50,
                      loadFilter: function (data) {
                          if (data == null) {
                              $(this).datagrid("load");
                              return $(this).datagrid("getData");
                          } else {
                              return data;
                          }
                      },
                      url: "Ajax_Request.aspx?typeValue=GetDataByUserDefinedSql",
                      pagination: false,
                      showFooter:false,
                      queryParams: {
                          UserDefinedSql: "<%=UserDefinedSql %>",
                          Condition: ''
                      },
                      columns: eval(fieldValueStr),
                      onDblClickRow: function (rowIndex, rowData) {
                          if ('<%=IsmultiSelect %>'.toLowerCase() == 'false') SelectedCheckBox(rowData);
                      }                     
                  }); 
                            
            }
        });
        }
        function SelectedCheckBox(obj) {
            var ColName = "";
            var ColShowName = "";
            if ('<%=IsmultiSelect %>'.toLowerCase() == 'true') {
                $("input[name='ChkColName']:checked").each(function () {
                    var strValue = $(this).val().split(",");
                    for (var i = 0; i < strValue.length; i++) {
                        var str = strValue[i].split(":");
                        if (str[0] == "内部字段名") ColName += "," + str[1];
                        else if (str[0] == "显示字段名") ColShowName += "," + str[1];
                    }

                });

                if (ColName != "") {
                    ColName = ColName.substr(1, ColName.length - 1);
                    ColShowName = ColShowName.substr(1, ColShowName.length - 1);
                }
            }
            else {
                ColName=obj.内部字段名;
                ColShowName = obj.显示字段名;
            }
            var str = '<%=Params %>'.split(',');
            for (var i = 0; i < str.length; i++) {
                if (str[i] != "") { 
                    var strCol = str[i].split("=");
                    if (strCol[1] == "ColName") $("#" + strCol[0]).val(ColName);
                    else if (strCol[1] == "ColShowName") $("#" + strCol[0]).val(ColShowName);
                }
            }           
            $('#ChooseTYWindow').dialog('close');
        }
    </script>
    <table class="easyui-datagrid"  style="width:100%;height:380px; border:none; " id="CenterGrid<%=keyWordValue %>"> </table>     
    <input type="hidden" value="" id="DictionaryConditon>" />
    <input id="hidExpCondition" value="" type="hidden" />
    <input type="hidden" id="hidSearchCondition<%=ResID %>" />
    <div id="CenterAddWindow" style="overflow: hidden; position: relative; display: none;"></div>
        <div id="divContent" ></div>
    </form>
</body>
</html>
