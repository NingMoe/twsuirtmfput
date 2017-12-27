<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResEmpty.aspx.cs" Inherits="Base_Config_ResEmpty" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
   
    <style type="text/css" >
      body{ font-size:12px;margin:0px; padding:0px; background-color:#ffffff;font-family:微软雅黑,Calibri, Arial, Helvetica, sans-serif; color:#282828;} 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var centerHeight = $('#westModel_id', window.parent.document).height();
        var centerWidth = $('#centerModel_id', window.parent.document).width()-210;
        $(document).ready(function () {

            var keyWordValue = '<%=keyWordValue %>';
            $("#allSearch_panel<%=keyWordValue %>").panel({
                border: true,
                iconCls: 'icon-search',
                collapsible: true
            }); 
            var GridHeight = centerHeight - $("#search_panel1<%=keyWordValue %>").height() - 31;
            $("#centerDivSetHeight<%=keyWordValue %>").css({ height: GridHeight });
            loadResEmptyGrid(GridHeight, '<%=keyWordValue %>');
        });
        function loadResEmptyGrid(GridHeight, keyWordValue) {
            
            $.ajax({
                type: "POST", 
                url: "Ajax_Request.aspx?typeValue=GetfieldByResEmpty&keyWordValue=<%=keyWordValue %>",           
                success: function (fieldValueStr) { 
                $('#CenterGrid' + keyWordValue).datagrid({
                    toolbar: [{ 
                        iconCls: 'icon-edit',
                        text: "修改",
                        disabled: false,  
                        handler: function () {
                            if ($('#CenterGrid' + keyWordValue).datagrid('getSelected') == null || $('#CenterGrid' + keyWordValue).datagrid('getSelected') == "") {
                                alert("没有选择任何行，请选择要修改的记录！");
                                return false;
                            } else { 
                                var dialogUrl = "SystemConfig/EditResEmpty.aspx?ResID=<%=ResID %>&RecID=" + $('#CenterGrid' + keyWordValue).datagrid('getSelected').ID
                                dialogUrl = encodeURI(dialogUrl);
                                fnFormListDialog(dialogUrl, centerWidth - 150, centerHeight -100, "修改信息"); 
                            }
                        } 
                    }],
                    height: GridHeight,
                    nowrap: true,
                    border: true,
                    striped: true,
                    singleSelect: true,
                    pageSize: 10,
                      fit: true,
                      loadFilter: function (data) {
                          if (data == null) {
                              $(this).datagrid("load");
                              return $(this).datagrid("getData");
                          } else {
                              return data;
                          }
                      },
                      url: "Ajax_Request.aspx?typeValue=GetDataByResEmpty&ResID=<%=NodeID %>",
                      pagination: true,
                      showFooter:false,
                      queryParams: {
                          Condition: ''
                      },
                      //fitColumns: true,
                      rownumbers: true,
                      columns: eval(fieldValueStr)
                  }); 
                var p = $('#CenterGrid' + keyWordValue).datagrid('getPager');
                $(p).pagination({
                    pageSize:10,
                      beforePageText: '第',
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
            }
        });
     }

        function changeAllSearchPanelSize(newWidth, keyWordValue) {
            centerWidth = $('#centerModel_id', window.parent.document).width();
            window.parent.$("#view_frame").css("width", centerWidth);
            $("#centerDivSetHeight<%=keyWordValue %>").css({ width: centerWidth });
              $('#CenterGrid<%=keyWordValue %>').datagrid('resize', {
                  width: centerWidth
              });
              $("#allSearch_panel" + keyWordValue).panel('resize', {
                  width: centerWidth
              });
              $("#search_panel" + keyWordValue).panel('resize', {
                  width: centerWidth
              })
              $("#search_panel1" + keyWordValue).panel('resize', {
                  width: centerWidth
              })
        }
        function ClickCheckBox(obj) {
            var check=$(obj).attr("checked"); 
            $("input[name='ch_ResourceTarget']:checked").each(function () {
                debugger
                $(this).attr("checked", 'false');
            })
            $(obj).attr("checked", check);
        }


        function fnFormListDialog(url, DialogWidth, DialogHeight, title) {
            if (DialogHeight == 0) DialogHeight = centerHeight;
            if (DialogWidth == 0) DialogWidth = centerWidth - 50;
            url = "Base/" + url;
            window.parent.fnParentFormListDialog(url, DialogWidth, DialogHeight, title);
        } 
        function fnGridLoad(Gridid) {          
            $("#" + Gridid).datagrid('load', {
                Condition: $("#hidSearchCondition<%=ResID %>").val()
            })
      }
    </script>

        <div title="空资源信息" class="easyui-panel" id="allSearch_panel<%=keyWordValue %>" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;"> 
            <div id="search_panel<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden; display:none;" border="false">
                <div id="search_panel1<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden; padding: 10px;" border="true">
                    <div class="con1">
                        <a class="easyui-linkbutton" iconcls="icon-view" onclick="excReport()">导出</a>
                    </div>
                </div>
            </div>
        </div>
        <div id="centerDivSetHeight<%=keyWordValue %>">
            <table id="CenterGrid<%=keyWordValue %>">
            </table>
        </div>  
    </form>
</body>
</html>
