<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YSKTJReport.aspx.cs" Inherits="Base_Report_YSKTJReport" %>
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
    var centerWidth = $('#centerModel_id',window.parent.document).width();
    $(document).ready(function () {
        var GridHeight = centerHeight;
        $("#centerDivSetHeight<%=keyWordValue %>").css({ height: GridHeight });
        loadCenterGrid(GridHeight, '<%=keyWordValue %>');
    });
    function loadCenterGrid(GridHeight, keyWordValue) {
          $('#CenterGrid' + keyWordValue).datagrid({
                height: GridHeight,
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
                url: "../Common/CommonAjax_Request.aspx?typeValue=GetfDataByYSKTJ",
                columns:[[
//					    { title: '月份', field: '月份', width: 100, align: 'center', rowspan: 2 },
					    { title: '本月回款', field: '本月回款', width: 100, align: 'center', rowspan: 2 },
					    { title: '本月开票', field: '本月开票', width: 100, align: 'center', rowspan: 2 },
					    { title: '应收款', width: 100, align: 'center', colspan: 3 }], [
//					    { title: '已确认未开牌', width: 100, align: 'center', colspan: 3 },
//					    { title: '合计', field: '合计', width: 100, align: 'center', rowspan: 2}
					    { title: '三个月内应收款', field: '三个月内应收款', width: 110, align: 'center' },
					    { title: '三个月以上应收款', field: '三个月以上应收款', width: 110, align: 'center' },
					    { title: '应收款总额', field: '应收款总额', width: 110, align: 'center' }
//					    { title: '3个月内未开票金额', field: '3个月内未开票金额', width: 130, align: 'center' },
//                      { title: '3个月以上未开票金额', field: '3个月以上未开票金额', width: 130, align: 'center' },
//                      { title: '已确认未开票合计', field: '已确认未开票合计', width: 130, align: 'center' }
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
        var skrq = $("#seaDate").val();
        if (skrq!="") {
            $("#CenterGrid<%=keyWordValue %>").datagrid('load', {
                SKRQ: skrq
            });
        }
    }
    </script>
     <div class="easyui-panel" id="allSearch_panel<%=keyWordValue %>"
        style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
        <div id="search_panel<%=keyWordValue %>" class="easyui-panel" style="overflow: hidden;"
            border="false">
            <div class="con1">
                &nbsp;&nbsp;收款日期 :<input type="text"  class="easyui-datebox" style="width: 150px;" id="seaDate" />
                &nbsp;<input type="button" value="查询" id="searchBtn" onclick="searchReportList()"  />
                &nbsp;<input type="button" value="导出" id="reportList" onclick="excReport()" />
            </div>
        </div>
    </div>
    <div id="centerDivSetHeight<%=keyWordValue %>" style="height:800px;">
        <table id="CenterGrid<%=keyWordValue %>">
        </table>
    </div>
    <input type="hidden" id="hidSearchCondition<%=keyWordValue %>" />
    </form>
</body>
</html>
