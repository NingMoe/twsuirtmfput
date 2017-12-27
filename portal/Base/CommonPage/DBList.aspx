<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBList.aspx.cs" Inherits="Base_DBList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据列表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

</head>
<body>
    <script type="text/javascript">
        var centerHeight = $('#westModel_id',window.parent.document).height();
        var centerWidth = $('#centerModel_id',window.parent.document).width();
        $(document).ready(function () {

            var keyWordValue = '<%=keyWordValue %>';
            $("#allSearch_panel<%=keyWordValue %>").panel({
                border: true,
                iconCls: 'icon-search',
                collapsible: true
            });

            <%--  var GridHeight = centerHeight - $("#search_panel1<%=keyWordValue %>").height() - 35;
            $("#centerDivSetHeight<%=keyWordValue %>").css({ height: GridHeight });--%>
            centerHeight=$(this).height()
            var GridHeight = centerHeight - $("#search_panel1<%=keyWordValue %>").height() - 40;
            $("#centerDivSetHeight<%=keyWordValue %>").css({ height: GridHeight });
            loadCenterGrid(GridHeight, '<%=keyWordValue %>');
        });
        function loadCenterGrid(GridHeight, keyWordValue) {
            var titleValue = '<%=titleValue %>';
            var ResID = '<%=ResID %>';
            var fieldValue = "[[ { field: '流程', title: '流程名称', width: 200, align: 'center', editor: 'text' }";
            fieldValue = fieldValue + ", { field: '主题', title: '流程主题', width: 200, align: 'center',editor: 'text'}";
            fieldValue = fieldValue + ", { field: '来源', title: '流程来源', width: 100, align: 'center',editor: 'text'}";
            fieldValue = fieldValue + ", { field: '时间', title: '创建时间', width: 80, align: 'center',editor: 'text'}";
            fieldValue = fieldValue + ", { field: '流程RecID', title: '流程RecID', width: 80, hidden:true,editor: 'text'}";
            fieldValue = fieldValue + ", { field: '流程ID', title: '流程ID', width: 80, hidden:true,editor: 'text'}";
            // fieldValue = fieldValue + "  ]]";
            fieldValue = fieldValue + ", {title:'操作',field:'操作',width:100,align:'center' ,formatter:function(value,rowData,rowIndex){return GetUrlLink(\"" + ResID + "\",rowData);}} ]]";
            $('#CenterGrid' + keyWordValue).datagrid({
                height: GridHeight,
                nowrap: true,
                border: true,
                striped: true,
                singleSelect: true,
                pageSize:'<%=PageSize %>',
                fit: true,
                loadFilter: function (data) {
                    if (data == null) {
                        $(this).datagrid("load");
                        return $(this).datagrid("getData");
                    } else {
                        return data;
                    }
                },
                url: "../Common/FlowDBAjax_Request.aspx?typeValue=GetDataDBList&ResID=" + ResID + "&keyWordValue=" + keyWordValue,
                queryParams: {
                    sqlData:"<%=sql %>",
                    SortField: "<%=SortField %>",
                    SortBy: "<%=SortBy %>",
                    Condition: ""
                }, 
                columns: eval(fieldValue), //把字符串转成对象。  
                pagination: true,
                fitColumns: true,
                rownumbers: true     
            });
            var p = $('#CenterGrid' + keyWordValue).datagrid('getPager');
            $(p).pagination({
                beforePageText: '第',
                pageSize: '<%=PageSize %>',
                afterPageText: '页  共 {pages} 页',
                displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
            });
             
        }
        function openUrl(LCID,LCRecID)
        {
            //debugger
            var linkUrl="";
            if("<%=keywordParameters %>"=="DBSY")
            {
                linkUrl="Execution.aspx?Type=transtract&WorklistItemId="+LCID+"&WorkflowInstId="+LCRecID;
            }
            if("<%=keywordParameters %>"=="WFQDRW")
            {
                linkUrl="Execution.aspx?Type=wfqdrw&WorklistItemId="+LCID;
            }
            if("<%=keywordParameters %>"=="WCLDRW")
            {
                linkUrl="Execution.aspx?Type=view&WorklistItemId="+LCID;
            }
            window.open (linkUrl) 
        }
        function GetUrlLink(ResID,rowData) {
            return "<a href=\"javascript:openUrl('"+rowData.流程ID+"','"+rowData.流程RecID+"')\"  )>处理</a>";
        }
        function changeAllSearchPanelSize(newWidth, keyWordValue) {
            centerWidth = $('#centerModel_id',window.parent.document).width();
            window.parent.$("#view_frame").css("width", centerWidth);
            $("#centerDivSetHeight<%=keyWordValue %>").css({ width: centerWidth });
            $('#CenterGrid<%=keyWordValue %>').datagrid('resize', {
                width: centerWidth
            });
            $("#allSearch_panel"+keyWordValue).panel('resize', {
                width: centerWidth
            });
            $("#search_panel"+keyWordValue).panel('resize', {
                width: centerWidth
            })
            $("#search_panel1"+keyWordValue).panel('resize', {
                width: centerWidth
            })
        }
        function fnLink(url,DialogWidth,DialogHeight)
        {
            fnFormListDialog(url,DialogWidth,DialogHeight,"基本信息");
        }
        function fnFormListDialog(url,DialogWidth,DialogHeight,title)
        { 
            var index = url.indexOf("?");
            if (index != "-1") {
                url=url + "&SearchType=<%=SearchType %>&NodeID=<%=NodeID %>&keyWordValue=<%=keyWordValue %>"
          } else {
              url=url + "?SearchType=<%=SearchType %>&NodeID=<%=NodeID %>&keyWordValue=<%=keyWordValue %>"
          }
          window.parent.fnParentFormListDialog(url,DialogWidth,DialogHeight,title);
      }
      function ParentCloseWindow(){
          window.parent.ParentCloseWindow();
      }
      function fnSearchLC()
      {
          var Searchcondtion="";
          var LCMC=$("#txtLCMC").val();
          if(LCMC!=""){Searchcondtion+=" and 流程 '%"+LCMC+"%'";}
          var LCZT=$("#txtLCZT").val();
          if(LCZT!=""){Searchcondtion+=" and 主题 like'%"+LCZT+"%'";}
          var BeginCJSJ=$("#txtBeginCJSJ").val();
          if(BeginCJSJ!=""){Searchcondtion+=" and 时间>='"+BeginCJSJ+" '";}
          var EndCJSJ=$("#txtEndCJSJ").val();
          if(EndCJSJ!=""){Searchcondtion+=" and 时间< dateadd(day,1,'"+EndCJSJ+"') ";}
          $("#CenterGrid<%=keyWordValue %>").datagrid('load', { 
              sqlData:"<%=sql %>",
              SortField:  "id" ,
              SortBy:  "desc" ,
              Condition: Searchcondtion 
          });
      } 
    </script>
    <div title="<%=titleValue %>" style="padding: 5px">
        <div title="&nbsp;<%=titleValue %>" class="easyui-panel" id="allSearch_panel<%=keyWordValue %>"
            style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
            <div id="search_panel<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden;"
                border="false">
                <div id="search_panel1<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden;"
                    border="true">
                    <table style="width: 100%; height: 25px;" border="0">
                        <tr>
                            <td style="width: 20px;">&nbsp;
                            </td>
                            <td style="width: 70px;">&nbsp;流程名称：
                            </td>
                            <td style="width: 110px;">&nbsp;<input type="text" id="txtLCMC" style="width: 100px;" class="box3" />
                            </td>
                            <td style="width: 50px;">&nbsp;主题：
                            </td>
                            <td style="width: 110px;">&nbsp;<input type="text" id="txtLCZT" style="width: 100px;" class="box3" />
                            </td>
                            <td style="width: 70px;">&nbsp;创建时间：
                            </td>
                            <td style="width: 120px;">&nbsp;从<input type="text" id="txtBeginCJSJ" style="width: 100px;" class="box3" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                            </td>
                            <td style="width: 120px;">&nbsp;到<input type="text" id="txtEndCJSJ" style="width: 100px;" class="box3" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                            </td>
                            <td>
                                <input type='button' value='查询' onclick="fnSearchLC()" style="width: 60px; height: 22px;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="centerDivSetHeight<%=keyWordValue %>">
            <table id="CenterGrid<%=keyWordValue %>">
            </table>
        </div>
    </div>
</body>
</html>
