<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSystemConfig.aspx.cs" Inherits="Base_AddSystemConfig" %>

<%@ Register Src="~/Base/CommonControls/SelectRecords_Sql.ascx" TagPrefix="uc1" TagName="SelectRecords_Sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;" onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="列表配置" class="easyui-panel" style="overflow-x: hidden;overflow-y: auto;  border-bottom: none; margin: 0px;height:460px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;">
                    <table width="100%" border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3">
                        <tr> 
                            <th style="width:10%;">列表配置号</th>
                            <td style="width:23%;">
                                <input id="<%=ResID %>_列表配置号" type="text" class="box3" style="width:80%;" readonly="readonly" />
                            </td>
                             <th style="width:10%;">参数关键字</th>
                            <td style="width:23%;">
                                <input id="<%=ResID %>_参数关键字" type="text" class="box3" minput="true" fieldtitle="参数关键字"  style="width: 80%;" />
                            </td>
                            <th style="width:10%;">资源ID</th>
                            <td style="width:23%;">
                                <input id="<%=ResID %>_值" type="text" class="box3" style="width: 80%;" value="<%=NodeID %>" readonly="readonly"/>
                            </td>
                        </tr>
                        <tr> 
                            <th>前台标签</th>
                            <td>
                                <input id="<%=ResID %>_前台标签" type="text" class="box3" minput="true" fieldtitle="前台标签" style="width: 80%;" />
                            </td>
                            <th>显示Title</th>
                            <td>
                                <input id="<%=ResID %>_显示Title" type="text" class="box3" minput="true" fieldtitle="显示Title"  style="width: 80%;" />
                            </td> 
                            <th>数据表名称</th>
                            <td>
                                <input id="<%=ResID %>_数据表名称" type="text" class="box3" style="width:80%;"   />
                            </td>
                        </tr>
                        <tr>
                           <th>添加地址 </th>
                            <td colspan="3">
                                <input id="<%=ResID %>_添加地址" type="text" class="box3" style="width: 93%;" />
                            </td>
                            <th>列表类型 </th>
                            <td>
                                <select id="<%=ResID %>_列表类型" class="box3" fieldtitle="业务类型" style="width: 80%;">
                                    <option>普通列表</option>
                                    <option>文档列表</option>
                                </select>
                            </td>
                        </tr> 
                        <tr> 
                            <th>编辑地址</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_编辑地址" type="text" class="box3" style="width:93%;" />
                            </td>
                            <th>页面打开方式 </th>
                            <td>
                                <select id="<%=ResID %>_页面打开方式" class="box3" fieldtitle="页面打开方式" style="width: 80%;">
                                    <option value="_ChildFrm">弹出子窗口</option>
                                    <option value="_parent">父页面打开</option>
                                    <option value="_blank">打开新页面</option>
                                </select> 
                            </td>  
                        </tr>
                          <tr>                           
                            <th>列表数据筛选sql</th> 
                            <td colspan="2" >
                                  <input id="<%=ResID %>_数据筛选条件" placeholder="如:姓名=$uName,日期=$Date,年龄=10(多条件用逗号隔开)  " type="text" class="box3" style="width:85%;" />&nbsp;<a href="javascript:" onclick="ShowDesc()" >配置说明</a> 
                            </td>
                            <td colspan="3" style="text-align:center; height:30px;">
                                 <input id="<%=ResID %>_启用添加" type="checkbox" />启用添加
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="<%=ResID %>_启用修改" type="checkbox"/>启用修改
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="<%=ResID %>_启用删除" type="checkbox" />启用删除
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="<%=ResID %>_启用导出" type="checkbox" />启用导出                                           
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="<%=ResID %>_是否显示复选框" type="checkbox" />是否显示复选框
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="<%=ResID %>_启用字段宽度设置" type="checkbox" />启用字段宽度                      
                                            <!-- <input id="<%=ResID %>_是否启用行过滤" type="checkbox" />是否启用行过滤-->
                                            <input id="<%=ResID %>_是否排序" type="checkbox" style="display:none" />

                                                                
                            </td>
                        </tr> 
                        <tr>
                            <th>默认排序</th>
                            <td>  
                                <uc1:SelectRecords_Sql runat="server" ID="SelectRecords_Sql1" />
                                 <select id="<%=ResID %>_排序类型" class="box3" fieldtitle="排序类型">
                                     <option value=""></option>
                                    <option value="asc">升序</option>
                                    <option value="desc">倒序</option>
                                </select>
                            </td>
                            <th>弹出层宽度 </th>
                            <td>
                                <input id="<%=ResID %>_弹出层宽度" type="text" class="box3" value="0" style="width: 80%;" />
                            </td>
                            
                            <th>弹出层高度 </th>
                            <td>
                                <input id="<%=ResID %>_弹出层高度" type="text" class="box3" value="0" style="width: 80%;" />
                            </td>
                        </tr> 
                        <tr>
                            <th>附件ResID </th>
                            <td>
                                <input id="<%=ResID %>_附件ResID" type="text" class="box3" style="width: 80%;" /> <a id="ASelectKHMC" href="#" onclick="chooseResource()">选择</a>
                            </td>
                            <th>附件字段 </th>
                            <td>
                                <input id="<%=ResID %>_附件字段" type="text" class="box3" style="width: 80%;" />
                            </td>
                            <th>附件主子表关联字段
                            </th>
                            <td>
                                <input id="<%=ResID %>_附件主子表关联字段" type="text" class="box3" style="width: 80%;" />
                            </td>
                        </tr>
                        <tr>
                            <th>统计字段</th>
                            <td  colspan="5"><input id="<%=ResID %>_统计字段" type="text" class="box3" style="width:95%;" /></td>
                        </tr>

                    </table>
                    
                    <asp:DataList ID="dlCol" runat="server" RepeatColumns="3" RepeatDirection="Vertical" CssClass="" Width="100%" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                        <HeaderTemplate><b>显示字段：</b></HeaderTemplate>
                        <HeaderStyle BackColor="#d8d8d8" />
                        <ItemTemplate>
                            <table border="0" cellspacing="0" cellpadding="0" style="width:99%;" class="table2">
                                <tr>
                                    <td style="width:180px; height:30px;"><input type="checkbox" name="ChkColName" onclick="ValidateColName(this)" id='Chk_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("CD_DISPNAME") %>" <%# (Convert.ToInt32(Eval("IsBe"))==0?"":"checked") %>/><%#Eval("CD_DISPNAME") %></td>
                                    <td style="width:180px;"><input type="text" id='txt_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("CD_ShowName") %>" style="width:80%;" />
                                        <input type="hidden" id='txtDataType_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("DataType") %>" />
                                        <input type="text" id='txtOrder_<%#Eval("CD_RESID") %>_<%#Eval("CD_COLNAME") %>' value="<%#Eval("OrderNum") %>" style="width:15px;" />
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table> 
                        </ItemTemplate>                        
                    </asp:DataList> 
                </div>
            </div>            
        </div>
         <div id="ChooseTYWindow" style="overflow-x:hidden;overflow:scroll;position:relative;display:none;"></div>
         <input type="hidden" value=""  id="DictionaryConditon" />
         <input type="hidden"  id="strColName" />
        <div id="dlgsp" style="overflow:hidden;position:relative;display:none;">
            <table style="text-align:center;" >
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>获取当前人员ID：</td>
                    <td>$ID</td>
                </tr>
                 <tr>
                    <td>获取当前登录人账号：</td>
                    <td>$uID</td>
                </tr>
                 <tr>
                    <td>获取当前登录人姓名：</td>
                    <td>$uName</td>
                </tr>
                 <tr>
                    <td>获取当前登录人部门：</td>
                    <td>$uDepName</td>
                </tr>
                 <tr>
                    <td>获取当前时间：</td>
                    <td>$Date</td>
                </tr>
                 <tr>
                    <td>获取当前年份：</td>
                    <td>$Year</td>
                </tr>
                 <tr>
                    <td>获取当前月份：</td>
                    <td>$Month</td>
                </tr>
                 <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" >&nbsp;例：月份=$Month,姓名=$uName,分类=A类</td>                    
                </tr>
                <tr>
                    <td colspan="2" >&nbsp;例：创建日期=$Date,创建人=$uID,状态=已完成</td>                    
                </tr>
            </table>           
        </div> 
    </form>
    
 <script type="text/javascript">
     
     var jsonStr_Search = "";
     var jsonStr_ResCol = "";
     $(document).ready(function () {

         //如果主表记录ID和主表ResID不为空，并且 RecID 为空 （代表是添加记录），则执行下列方法
         //读取并自动完成主表的记录           
         if ('<%=RecID %>' != "") {
             $.ajax({
                 type: "POST",
                 url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=<%=RecID %>",
                 success: function (centerJson) {
                     var jsonList = eval("(" + centerJson + ")");
                     var zhmc = "";
                     var strValue = "";
                     var strTitle = "";
                     for (var i = 0; i < jsonList.length; i++) {
                         for (var key in jsonList[i]) {

                             if ($("#<%=ResID %>_" + key).attr("type") == "checkbox") {
                                 if (jsonList[i][key] == "1" || jsonList[i][key] == "是" || jsonList[i][key] == "True" || jsonList[i][key] == "true") $("#<%=ResID %>_" + key).attr("checked", "true");
                             }
                             else $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                         }

                     }
                     $("#strColName").val('<%=SearchCol %>');
                 }
             });
         }
     });


     function fnSave() {
         if (CheckValue("div<%=ResID %>FormTable")) {
             if (GetColName()) {
                 var jsonStr1 = "[{";
                 jsonStr1 += GetFromJson("<%=ResID %>");
                 //jsonStr1 += "'Title显示字段':'" + TitleColName + "','取值显示字段':'" + ColName + "'";
                 jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
                 jsonStr1 += "}]";
                 $.ajax({

                     type: "POST",
                     dataType: "json",
                     data: { "Json": "" + jsonStr1 + "", "JsonChild_Search": "" + jsonStr_Search + "", "JsonChild_ResCol": "" + jsonStr_ResCol + "" },
                     url: "Ajax_Request.aspx?typeValue=SaveSysSettingsInfo&ResID=<%=ResID %>&RecID=<%=RecID %>",
                     success: function (obj) {
                         if (obj.success || obj.success == "true") {
                             alert("保存成功!");
                             window.parent.ParentCloseWindow();
                             window.parent.RefreshGrid('CenterGrid<%=ResID %>');
                         } else {
                             alert("保存失败,请刷新页面！");
                         }
                     }
                 });
             }
         }
     }
     function SetColName(strTitle, strValue) {
         var title = strTitle.split(",");
         var value = strValue.split(",");

         $("#strColName").val('<%=SearchCol %>');
         $("input[name='ChkColName']").each(function () {
             for (var i = 0 ; i < value.length; i++) {
                 if (value[i] == this.value) {
                     $(this).attr("checked", 'true');
                     $("input[id='" + this.id.replace("Chk_", "txt_") + "']").val(title[i]);
                 }
             }
         })
     }
     function ValidateColName(obj) {
         $("input[id='" + obj.id + "']:checked").each(function () {
             var result = $("input[id='" + obj.id.replace("Chk_", "txt_") + "']").val()
             if (result == "") {
                 alert("字段“" + obj.value + "”的显示名称不能为空");
                 obj.checked = false;
                 return false;
             }
         })
     }

     function GetColName() {
         jsonStr_Search = "";
         var strColName = $("#strColName").val();
         var KeyWord = $("#<%=ResID %>_参数关键字").val();
         $("input[name='ChkColName']:checked").each(function () {
             var result = $("input[id='" + this.id.replace("Chk_", "txt_") + "']").val();//.attr("value");

             if (result == "") {
                 alert("字段“" + this.value + "”的显示名称不能为空");
                 return false;
             }
             else {
                 var jsonStr1 = "[{";
                 jsonStr1 += "'参数关键字':'" + KeyWord + "','显示字段':'" + result + "','绑定字段':'" + this.value + "','数据类型':'" + $("input[id='" + this.id.replace("Chk_", "txtDataType_") + "']").val() + "','排序号':'" + $("input[id='" + this.id.replace("Chk_", "txtOrder_") + "']").val() + "'";
                 jsonStr1 += "}]";
                 jsonStr_ResCol += "," + jsonStr1;
                 if ((strColName + ",").indexOf(this.value + ",") < 0) {
                     jsonStr1 = "[{";
                     jsonStr1 += "'参数关键字':'" + KeyWord + "','显示字段':'" + result + "','查询字段':'" + this.value + "','数据类型':'" + $("input[id='" + this.id.replace("Chk_", "txtDataType_") + "']").val() + "'";
                     jsonStr1 += "}]";
                     jsonStr_Search += "," + jsonStr1;
                 }


             }
         })
         if (jsonStr_Search != "") jsonStr_Search = jsonStr_Search.substr(1, jsonStr_Search.length - 1);
         if (jsonStr_ResCol != "") jsonStr_ResCol = jsonStr_ResCol.substr(1, jsonStr_ResCol.length - 1);
         return true;
     }

     function chooseResource() {
         var url = encodeURI("../CommonDictionary/ResourceTreeDictionary.aspx?Params=<%=ResID %>_附件ResID=ResID");
         var content = '<iframe src="' + url + '" width="100%" height="99%" frameborder="0" scrolling="no"></iframe>';        
         var win = $('#ChooseTYWindow').dialog({
             title: '资源树字典',
             content: content,
             width: 800,
             height: 425,
             cache: false,
             closed: true,
             shadow: false,
             closable: true,
             modal: true,
             draggable: false,
             resizable: false,
             onClose: function () {
                 $(this).dialog('destroy');//后面可以关闭后的事件  
             }
         });
         win.dialog('open');
     }

     function ShowDesc() {        
         var wins = $('#dlgsp').dialog({
             title: '配置说明',             
             width: 400,
             height: 300,
             cache: false,
             closed: true,
             shadow: false,
             closable: true,
             modal: true,
             draggable: false,
             resizable: false,
             onClose: function () {
                 $(this).dialog('destroy');//后面可以关闭后的事件  
             }
         });
         wins.dialog('open');
     }

    </script>
</body>
</html> 