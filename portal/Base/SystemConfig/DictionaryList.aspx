<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DictionaryList.aspx.cs" Inherits="Base_SystemConfig_DictionaryList" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <%=this.GetScript1_4_3  %> 
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var centerHeight = document.documentElement.clientHeight;
        var centerWidth = $('#centerModel_id', window.parent.document).width() - 220;
        var GridHeight = 0;
        $(document).ready(function () {

            GridHeight = centerHeight-40;
            $("#centerDivSetHeight<%=keyWordValue %>").css({ height: 465 });
            loadResEmptyGrid(GridHeight, '<%=keyWordValue %>'); 
        });
        function loadResEmptyGrid(GridHeight, keyWordValue) {
            var DialogWidth = 0;
            var DialogHeight = 0;
            var titleValue = '系统配置';  
            $.ajax({
                type: "POST", 
                data: {
                    "ColName": '列表配置号,关键字,显示Title,参数关键字,资源ID',
                    "ColField": '列表配置号,关键字,显示Title,参数关键字,值',
                    "ColWidth": '100,160,160,100,100'
                },
                url: "../Common/Ajax_Request.aspx?typeValue=GetfieldInfo&keyWordValue=<%=keyWordValue %>",   
                success: function (fieldValueStr) {
                    var ColumnsStr = "";
                     ColumnsStr = "{title:'操作',field:'操作',width:30,align:'center' ,formatter:function(value,rowData,rowIndex){return '<a href=\"#\" onclick=SelectedCheckBox(\"' + rowData.参数关键字 + '\",\"'+rowData.值 + '\",\"' + rowData.显示Title + '\")>选择</a>';}},";
                    fieldValueStr = "[[" + ColumnsStr + fieldValueStr + "]]";
                    $('#CenterGrid' + keyWordValue).datagrid({
                        height: GridHeight,
                        nowrap: true,
                        border: true,
                        striped: true,
                        singleSelect: true,
                        pageSize: 10,
                      fit: true,
                      loadFilter: function (data) {
                          if (data == null) {
                              //$(this).datagrid("load");
                              //return $(this).datagrid("getData");
                          } else {
                              return data;
                          }
                      },
                      url: "Ajax_Request.aspx?typeValue=GetDataByUserDefinedSql",
                      pagination: false,
                      showFooter:false,
                      queryParams: {
                          UserDefinedSql:"437496952875",
                          Condition: " and 值='<%=NodeID %>' "
                      },
                      //fitColumns: true,
                      rownumbers: true,
                      columns: eval(fieldValueStr),
                      onDblClickRow: function (rowIndex, rowData) {
                           SelectedCheckBox(rowData);
                      },                      
                  }); 
                var p = $('#CenterGrid' + keyWordValue).datagrid('getPager');
                $(p).pagination({
                    pageSize:10,
                      beforePageText: '第',
                      afterPageText: '页  共 {pages} 页',
                      displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
                  });
            }
        });
        }
        function SelectedCheckBox(key,ResID,ResName) { 
            window.parent.parent.GetSysSettingsData(key, ResID, ResName);
            window.parent.parent.CloseDictionary();
        }
    </script>

        <div id="centerDivSetHeight<%=keyWordValue %>">
            <table id="CenterGrid<%=keyWordValue %>">
            </table>          
        </div> 
    <input type="hidden" value="" id="DictionaryConditon>" />
    <input id="hidExpCondition" value="" type="hidden" />
    <input type="hidden" id="hidSearchCondition<%=ResID %>" />
    <div id="CenterAddWindow" style="overflow: hidden; position: relative; display: none;"></div>
        <div id="divContent" ></div>
    </form>
</body>
</html>
