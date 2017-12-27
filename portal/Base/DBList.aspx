<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBList.aspx.cs" Inherits="Base_DBList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据列表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

</head>
<body>
    <%=this.GetScript1_4_3   %>
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
            fieldValue = fieldValue + ", {title:'操作',field:'操作',width:100,align:'center' ,formatter:function(value,rowData,rowIndex){return GetUrlLink(\"" + ResID + "\",rowData);}} ]]";
            $('#CenterGrid' + keyWordValue).datagrid({
                height: GridHeight,
                nowrap: true,
                border: true,
                striped: true,
                singleSelect: true,
                pageSize:<%=PageSize %>,
              fit: true,
              loadFilter: function (data) {
                  if (data == null) {
                      $(this).datagrid("load");
                      return $(this).datagrid("getData");
                  } else {
                      return data;
                  }
              },
              url: "Common/FlowDBAjax_Request.aspx?typeValue=GetDataDBList&ResID=" + ResID + "&keyWordValue=" + keyWordValue,
              queryParams: {
                  SortField: "<%=SortField %>",
                  Condition:"",
                  SortBy: "<%=SortBy %>"
              }, 
              columns: eval(fieldValue), //把字符串转成对象。  
              pagination: true,
              fitColumns: true,
              rownumbers: true
          });
          var p = $('#CenterGrid' + keyWordValue).datagrid('getPager');
          $(p).pagination({
              beforePageText: '第',
              pageSize: <%=PageSize %>,
                afterPageText: '页  共 {pages} 页',
                displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
            });

            $("#allSearch_panel" + keyWordValue).panel({
                onCollapse: function () {
                    $("#centerDivSetHeight<%=keyWordValue %>").css({ height: $('#westModel_id',window.parent.document).height()+2});
                    $('#CenterGrid<%=keyWordValue %>').datagrid('resize', {
                        height: $('#westModel_id',window.parent.document).height()-9
                    });
                },
                onExpand: function () {
                    $("#centerDivSetHeight<%=keyWordValue %>").css({ height: GridHeight });
                    $('#CenterGrid<%=keyWordValue %>').datagrid('resize', {
                        height: GridHeight
                    });
                }
            });
            //左侧树折叠时改变面板的方法
            window.parent.$("#westModel_id").panel({
                onCollapse: function () {
                    changeAllSearchPanelSize(centerWidth, keyWordValue);
                },
                onExpand: function () {
                    changeAllSearchPanelSize(centerWidth, keyWordValue);
                }
            });
        }
        function openUrl(LCID,LCRecID)
        {
            var linkUrl="";
            if("<%=keywordParameters %>"=="DBSY")
                {
                    linkUrl="CommonPage/Execution.aspx?Type=transtract&WorklistItemId="+LCID+"&WorkflowInstId="+LCRecID;
                }
                if("<%=keywordParameters %>"=="WFQDRW")
                {
                    linkUrl="CommonPage/Execution.aspx?Type=wfqdrw&WorklistItemId="+LCID;
                }
                if("<%=keywordParameters %>"=="WCLDRW")
                {
                    linkUrl="CommonPage/Execution.aspx?Type=view&WorklistItemId="+LCID;
                }
                window.open(linkUrl)
 
            }
            function GetUrlLink(ResID,rowData) {
                if ("<%=keywordParameters %>"=="DBSY") {
                     return "<a href=\"javascript:openUrl('"+rowData.流程ID+"','"+rowData.流程RecID+"')\"  )>处理</a>";
                }else {
                     return "<a href=\"javascript:openUrl('"+rowData.流程ID+"','"+rowData.流程RecID+"')\"  )>查看</a>";
                }
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
          if(LCMC!=""){Searchcondtion+=" and 流程 like'%"+LCMC+"%'";}
          var LCZT=$("#txtLCZT").val();
          if(LCZT!=""){Searchcondtion+=" and 主题 like'%"+LCZT+"%'";}
          var BeginCJSJ=$("#txtBeginCJSJ").val();
          if(BeginCJSJ!=""){Searchcondtion+=" and 时间>='"+BeginCJSJ+" '";}
          var EndCJSJ=$("#txtEndCJSJ").val();
          if(EndCJSJ!=""){Searchcondtion+=" and 时间< dateadd(day,1,'"+EndCJSJ+"') ";}
          var LCtxtLY=$("#txtLY").val();
           if(LCtxtLY!=""){Searchcondtion+=" and 来源 like'%"+LCtxtLY+"%'";}
          $("#CenterGrid<%=keyWordValue %>").datagrid('load', {
                sqlData:"<%=sql %>",
                SortField:  "id" ,
                SortBy:  "desc" ,
                Condition: Searchcondtion 
            });
        }
    </script>
    <div title="<%=titleValue %>" style="padding: 5px;">
        <div title="&nbsp;<%=titleValue %>" class="easyui-panel" id="allSearch_panel<%=keyWordValue %>"
            style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
            <div id="search_panel<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden;"
                border="false">
                <div id="search_panel1<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden;"
                    border="true">
                    <table style="width: 100%; height: 25px;" border="0">
                        <tr>
                            <td style="width:7%;">&nbsp;流程名称：</td>
                            <td style="width: 12%;"><input type="text" id="txtLCMC" style="width: 95%;" class="easyui-textbox" />
                            </td>
                            <td style="width:5%;">&nbsp;主题：</td>
                            <td style="width: 18%;"><input type="text" id="txtLCZT" style="width: 95%;" class="easyui-textbox" />
                            </td>
                            <td style="width:5%;">&nbsp;来源：</td>
                            <td style="width: 12%;"><input type="text" id="txtLY" style="width: 95%;" class="easyui-textbox" />
                            </td>
                            <td style="width:7%;">&nbsp;创建时间：</td>
                            <td style="width: 10%;">从&nbsp;<input type="text" id="txtBeginCJSJ" style="width: 80%;" class="easyui-datebox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                            </td>
                            <td style="width:10%;">&nbsp;到&nbsp;<input type="text" id="txtEndCJSJ" style="width: 80%;" class="easyui-datebox" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                            </td>
                            <td style="width:2%;"></td>
                            <td style="width:10%;"> <input type='button' value='查询' onclick="fnSearchLC()" style="width: 90%; height: 22px;" /></td>
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
