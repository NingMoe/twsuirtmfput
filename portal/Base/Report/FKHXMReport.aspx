<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FKHXMReport.aspx.cs" Inherits="Base_Report_FKHXMReport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <%=this.GetScript1_4_3   %>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
    var centerHeight = $('#westModel_id',window.parent.document).height();
    var centerWidth = $('#centerModel_id', window.parent.document).width();
    $(document).ready(function () {
        var GridHeight = centerHeight;
        $("#centerDivSetHeight<%=keyWordValue %>").css({ height: GridHeight });
        loadCenterGrid(GridHeight, '<%=keyWordValue %>');
    });
    function loadCenterGrid(GridHeight, keyWordValue) {
          $('#CenterGrid' + keyWordValue).datagrid({
                nowrap: true,
                border: true,
                striped: true,
                singleSelect: true,
                fit: true,
                loadFilter: function (data) {
                    if (data == null) {
                        $(this).datagrid("load");
                        return $(this).datagrid("getData");
                    } else {
                        return data;
                    }
                },
                url: "../Common/CommonAjax_Request.aspx?typeValue=GetDataByFKHXM",
                columns:[[
    				    { title: '客户全称', field: '客户全称', width: 100, align: 'left' },
					    { title: '累计新开项目', field: '累计新开项目', width: 100, align: 'center' },
					    { title: '累计未确认项目', field: '累计未确认项目', width: 100, align: 'center' },
					    { title: '累计已确认未开票项目', field: '累计已确认未开票项目', width: 180, align: 'center' }
				    ]],         			
//              pagination: true,
                showFooter:true,
                fitColumns: true,
                rownumbers: true,
                queryParams: {
                    SortField: "",
                    SortBy: "",
                    Condition: $("#hidSearchCondition<%=keyWordValue %>").val()
                },
                onSortColumn: function (sort, order) 
                {
                    fnGridLoad(sort, order);
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
     
    function fnGridLoad(SortField, SortBy)
    {
        $("#CenterGrid<%=keyWordValue %>").datagrid('load', { 
            SortField:  SortField ,
            SortBy:  SortBy ,
            Condition:  $("#hidSearchCondition<%=keyWordValue %>").val() 
        });
    }
    function searchReportList() {
        var seaBegDate = $("#seaBegDate").val();
        var seaEndDate = $("#seaEndDate").val();
        if (seaBegDate != "" && seaEndDate!="") {
            $("#CenterGrid<%=keyWordValue %>").datagrid('load', {
                seaBegDate: seaBegDate,
                seaEndDate: seaEndDate
            });
        }
    }
    </script>
     <div class="easyui-panel" id="allSearch_panel<%=keyWordValue %>"
        style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
        <div id="search_panel<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden;"
            border="false">
            <div class="con1">
                &nbsp;&nbsp;查询时间 :<input type="text"  class="easyui-datebox" style="width: 150px;" id="seaBegDate" />
                &nbsp;&nbsp;至 :<input type="text"  class="easyui-datebox" style="width: 150px;" id="seaEndDate" />
                &nbsp;<input type="button" value="查询" id="searchBtn" onclick="searchReportList()"  />
                &nbsp;<input type="button" value="导出" id="reportList" onclick="excReport()" />
            </div>
        </div>
    </div>
    <div id="centerDivSetHeight<%=keyWordValue %>" style="height:500px;">
        <table id="CenterGrid<%=keyWordValue %>">
        </table>
    </div>
    <input type="hidden" id="hidSearchCondition<%=keyWordValue %>" />
    </form>
</body>
</html>
