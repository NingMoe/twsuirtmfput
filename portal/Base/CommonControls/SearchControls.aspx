<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchControls.aspx.cs" Inherits="Base_CommonControls_SearchControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>高级查询组件</title>    
</head>
<body>
    <form id="fmSearchItem">
        <%
        if (dt.Rows.Count > 0)
        {
        %>
            <table id="TbSearchItem" border="0" class="table2" style="width:99.9%;margin-top:10px;" >
                <%
                for(int i=0;i<8;i++)
                {
                 %> 
                    <tr title="nvarchar" style="height:35px;" >
                    <td style="width:30%;text-align:right;" ><select class="easyui-combobox" id="sel<%=i %>" style="width:130px" ><option></option>
	    			    <%
                         foreach (System.Data.DataRow item1 in dt.Rows)
                         {
                         %> 
                            <option value="<%=item1["showCol"] %>"><%=item1["showCol"] %></option>
                        <%
                        }
                        %></select>：</td>
	    			    <td><select class="easyui-combobox" style="width:80px" id="s<%=i %>"  name="s<%=i %>">
                            <option value=""></option>
                            <option value="=">等于</option>
                            <option value="<>">不等于</option>
                            <option value=">">大于</option>
                            <option value="<">小于</option>
                            <option value="&gt;=">大于等于</option>
                            <option value="&lt;=">小于等于</option>
                            <option value="like">包含</option>
                            <option value="not like">不包含</option>
	    			        </select><input class="easyui-textbox" style="width:185px;" id="t<%=i %>"  name="t<%=i %>" /></td>
                    </tr>
                 <%
                    }
                %>
            </table>
        <%
            }
        %>
    </form>
</body>
</html>
