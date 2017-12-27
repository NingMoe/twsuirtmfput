<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportExcel.aspx.cs" Inherits="Base_CommonControls_ExportExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>通用导出组件</title>    
</head>
<body>    
	<div id="tt" class="easyui-tabs" data-options="tabPosition:'left',headerWidth:100,tabWidth:100,tabHeight:35" style="width:100%;height:100%">
		<div title="① 导出条件" data-options="closable:false" style="padding:10px">
			<div style="text-align:center; border-bottom:1px solid #e9e5e5;color:#ff0000 " ><span>备注：不选择导出条件，则导出表中所有记录！</span></div>
            
            <form id="fmExcelItem">  
                 <%
            if (dt.Rows.Count > 0)
            {
            %>
                <table id="ExcelSearchItem" border="0" class="table2" style="width:99.9%;margin-top:10px;" >

                    <%
                        foreach (System.Data.DataRow item in dt.Rows)
                        {
                            if (item["datetype"].Equals("datetime")) {
                             //日期型查询
                    %> 

                        <tr title="datetime" id="<%=item["id"] %>" style="height:35px;">
                            <td style="text-align:right;"><%=item["showCol"] %>：</td>
                            <td><input id="d1_<%=item["id"] %>"  name="<%=item["Searchcol"] %>" style="width:120px;" class="easyui-datebox" />&nbsp;至&nbsp;
                                <input id="d2_<%=item["id"] %>"  name="<%=item["Searchcol"] %>" style="width:120px;" class="easyui-datebox" /></td>
                        </tr>

                     <%
                            }
                            else if (item["datetype"].Equals("int")) {
                             //数字型查询
                     %> 

                         <tr title="int" id="<%=item["id"] %>" style="height:35px;">
                            <td style="text-align:right;"><%=item["showCol"] %>：</td>
	    			        <td>                            
                                <input class="easyui-numberspinner" id="n1_<%=item["id"] %>"  name="<%=item["Searchcol"] %>" data-options="increment:1" style="width:120px;" />&nbsp;至&nbsp;
                                <input class="easyui-numberspinner" id="n2_<%=item["id"] %>"  name="<%=item["Searchcol"] %>"  data-options="increment:1" style="width:120px;" /></td>
	    		        </tr>
                     <%
                            }//文本查询
                            else
                            { 
                     %> 
                           <tr title="nvarchar" style="height:35px;" >
	    			            <td style="width:20%;text-align:right;" ><%=item["showCol"] %>：</td>
	    			            <td><select class="easyui-combobox" style="width:80px" id="s<%=item["id"] %>"  name="s<%=item["showCol"] %>"><option value=""></option><option value="<%=item["Searchcol"] %>=">等于</option><option value="<%=item["Searchcol"] %> like">包含</option><option value="<%=item["Searchcol"] %> <> ">不等于</option><option value="<%=item["Searchcol"] %> >">大于</option><option value="<%=item["Searchcol"] %> <">小于</option><option value="<%=item["Searchcol"] %> not like">不包含</option></select><input class="easyui-textbox" style="width:185px;" id="t<%=item["id"] %>"  name="t<%=item["showCol"] %>" /></td>
                            </tr>

                     <%
                            }
                        }
                    %>
                </table>
            <%
                }
            %>
          
            </form> 
		</div>
	<%--	<div title="② 导出字段" data-options="closable:false" style="padding:5px;">
            <div style="text-align:center;" ><input type="button" style="width:80px" onclick="CheckAllBox()" id="checkAll" value="全 选" />&nbsp;&nbsp;&nbsp; <input type="button" style="width:80px" id="uncheckAll" onclick="unCheckAllBox()" value="全不选" /></div>
			<ul id="ExcelColItem"  style="width:94%;overflow:hidden; margin-left:-24px; float:left; "  >
              <%
                  if (dt.Rows.Count > 0)
                  {
                      foreach (System.Data.DataRow rw in dtCol.Rows)
                      {
                          //类型判断
                          if (rw["DateType"].Equals("datetime"))
                          {
              %>          
                            <li style="width:49.5%; list-style:none; border:1px solid #e9e5e5; line-height:35px;  float: left;"><input name="chkColumn" type="checkbox" value="convert(varchar(100),<%=rw["columnName"] %>,23)<%=rw["columnName"] %>" id="<%=rw["id"] %>" /><label for="<%=rw["id"] %>"> <%=rw["columnName"] %></label></li>
              <%
                          }else
                          {
                %> 
                            <li style="width:49.5%; list-style:none; border:1px solid #e9e5e5; line-height:35px;  float: left;"><input name="chkColumn" type="checkbox" value="<%=rw["columnName"] %>" id="<%=rw["id"] %>" /><label for="<%=rw["id"] %>"> <%=rw["columnName"] %></label></li>
                <%
                          }
                      }
                  }
             %>
			</ul>
		</div>     --%>
	</div> 
</body>
</html>
