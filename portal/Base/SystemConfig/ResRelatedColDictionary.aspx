<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResRelatedColDictionary.aspx.cs" Inherits="Base_Config_ResRelatedColDictionary" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主子表关联字段选择</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />    
</head>
<body>
      <table width="800" >
          <tr>
              <td colspan="2" style="text-align:center;" ><input type="button" value="确认选择" onclick="ChooseOk('<%=Request.QueryString["IsmultiSelect"]%>');"  /><span style="color:#ff6a00" >&nbsp;[主资源字段和子资源字段，关联及引用字段配置]</span> </td>
          </tr>
          
       </table>
    <div  style="overflow: hidden; width:49%; float: right; ">
            <div title="主表 <%=PResName %> 资源字段" class="easyui-panel" >
                  <table id="parentCloum" style="font-size:14px;color:#333333; border-width: 1px;border-color: #999999;border-collapse: collapse;">
                      <tr>
                          <td style="width:10%"></td>
                          <td style="width:20%">序号</td>
                          <td style="width:30%" >内部字段名称</td>
                          <td style="width:40%">显示字段名称</td>
                      </tr>                    
                          <%
                              if (dtParent.Rows.Count>0) {

                                  for (int i = 0; i < dtParent.Rows.Count; i++)
                                  {
                           %>
                                <tr style="height:25px;">
                                    <td><input type="radio" name="parentRdo" value="<%=dtParent.Rows[i]["CD_DISPNAME"] %>" /> </td>
                                    <td><%=i %></td>
                                    <td><%=dtParent.Rows[i]["CD_COLNAME"] %></td>
                                    <td><%=dtParent.Rows[i]["CD_DISPNAME"] %></td>
                                </tr>
                            <%
                                  }
                              }

                          %>
                  </table>
            </div>
        </div>
     <div  style="overflow: hidden; width:49%; float: left; ">
     <div title="子表 <%=ResName %> 资源字段" class="easyui-panel">
                  <table id="childrenCloum" style="font-size:14px;color:#333333; border-width: 1px;border-color: #999999;border-collapse: collapse;">
                      <tr>
                          <td style="width:10%"></td>
                          <td style="width:20%">序号</td>
                          <td style="width:30%" >内部字段名称</td>
                          <td  style="width:40%">显示字段名称</td>
                      </tr>                    
                        <%
                            if (dtChild.Rows.Count>0) {

                                for (int j = 0; j < dtChild.Rows.Count; j++)
                                {
                        %>
                            <tr style="height:25px;" >
                                <td><input type="radio"  name="childRdo" value="<%=dtChild.Rows[j]["CD_DISPNAME"] %>" /> </td>
                                <td><%=j %></td>
                                <td><%=dtChild.Rows[j]["CD_COLNAME"] %></td>
                                <td><%=dtChild.Rows[j]["CD_DISPNAME"] %></td>
                            </tr>
                        <%
                                }
                            }

                        %>
                      
                  </table>
            </div>
         </div>
</body>
</html>
