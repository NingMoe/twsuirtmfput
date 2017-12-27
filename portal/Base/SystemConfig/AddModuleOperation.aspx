<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddModuleOperation.aspx.cs" Inherits="Base_AddModuleOperation" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加模块</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
</head>
<body>
 <script type="text/javascript">
     var TitleColName = "";
     var ColName = "";
        $(document).ready(function () {
            SetBackgroundColor();
            //如果主表记录ID和主表ResID不为空，并且 RecID 为空 （代表是添加记录），则执行下列方法
            //读取并自动完成主表的记录
           
            if ('<%=RecID %>' != "") {
                $.ajax({
                    type: "POST",
                    url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=<%=ResID %>&RecID=<%=RecID %>",
                    success: function (centerJson) {
                        var jsonList = eval("(" + centerJson + ")");
                        for (var i = 0; i < jsonList.length; i++) {
                            for (var key in jsonList[i]) {
                                $("#<%=ResID %>_" + key).val(jsonList[i][key]);
                                if (key == "是否选择行" || key == "是否启用") {
                                    if (jsonList[i][key] == 1)
                                        $("#<%=ResID %>_" + key).attr("checked", "true");
                                }
                            }
                        }
                    }
                });
            }
                SetBackgroundColor();
        });
      
     function fnSave() {
         if (CheckValue("div<%=ResID %>FormTable")) {
             $("#fnChildSave").attr("disabled", true);
             var jsonStr1 = "[{";
             jsonStr1 += GetFromJson("<%=ResID %>");
             jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
             jsonStr1 += "}]"; 
             $.ajax({
                 type: "POST",
                 dataType: "json",
                 data: { "Json": "" + jsonStr1 + "" },
                 url: "../SystemConfig/Ajax_Request.aspx?typeValue=SaveInfo&ResID=<%=ResID %>&RecID=<%=RecID %>",
                    success: function (obj) {
                        if (obj.success || obj.success == "true") {
                            alert("保存成功！");
                            window.parent.ParentCloseWindow();
                            window.parent.RefreshGrid("Grid<%=PResID %>_<%=ResID %>");
                         } else {
                             alert("保存失败,请刷新页面！");
                         }
                    }
                });
             }
     } 
    </script>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y: scroll; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="模块自定义操作配置" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
                <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width: 100%;"> 
                        <tr> 
                            <th style="width:11%">操作编号</th>
                            <td style="width:22%">
                                <input id="<%=ResID %>_操作编号" type="text" class="box3" style="width:80%;" readonly="readonly" />
                            </td>
                             <th style="width:11%">参数关键字</th>
                            <td style="width:22%">
                                <input id="<%=ResID %>_参数关键字" type="text" class="box3" minput="true" fieldtitle="参数关键字" value="<%=RelatedValue %>"  readonly="readonly" style="width: 80%;" />
                            </td>
                            <th style="width:11%">工具名称</th>
                            <td style="width:22%">
                                <input id="<%=ResID %>_工具名称" type="text" class="box3" style="width: 80%;" minput="true" fieldtitle="工具名称" />
                            </td>
                        </tr>
                        <tr> 
                            <th>页面打开方式 </th>
                            <td>
                                <select id="<%=ResID %>_页面打开方式" class="box3" fieldtitle="页面打开方式" style="width: 80%;">
                                    <option value="_ChildFrm">弹出子窗口</option>
                                    <option value="_parent">父页面打开</option>
                                    <option value="_blank">打开新页面</option>
                                </select> 
                            </td>
                            <th>工具图标</th>
                             <td><input id="<%=ResID %>_工具图标" type="text" class="box3" style="width:80%;" /></td>
                              <td style="text-align:center;" colspan="2"><input id="<%=ResID %>_是否启用" type="checkbox" />是否启用
                                 &nbsp;&nbsp;&nbsp;<input id="<%=ResID %>_是否选择行" type="checkbox"/>是否选择行</td> 

                        </tr>
                         <tr>
                             <th>排序号</th>
                             <td><input id="<%=ResID %>_排序号" type="text" class="box3" style="width: 80%;" /></td>
                            <th>页面宽度</th>
                             <td><input id="<%=ResID %>_页面宽度" type="text" class="box3" style="width: 80%;" fieldtitle="页面宽度" fieldtype="num" value="0" /></td>
                            <th>页面高度</th>
                             <td><input id="<%=ResID %>_页面高度" type="text" class="box3" style="width: 80%;" value="0" fieldtitle="页面高度" fieldtype="num" /></td>
                       
                        </tr>
                         <tr> 
                             <th>弹出页面</th>
                             <td colspan="5"><textarea id="<%=ResID %>_弹出页面" type="text" class="box3" style="width: 99%; height:60px; margin-top:3px;margin-bottom:3px;"></textarea></td> 
                              
                            
                        </tr>
                        
                        <tr> 
                             <th>事件代码</th>
                             <td colspan="5"><textarea id="<%=ResID %>_事件代码" placeholder="Js事件，如： function Test(){ alert(1) }" type="text" class="box3" style="width: 99%; height:150px; margin-top:3px;margin-bottom:3px;"></textarea></td>
                              </tr> 
                    </table> 
                </div>
           
        </div>
         <div id="ChooseTYWindow" style="overflow-x:hidden;overflow:scroll;position:relative;display:none;"></div>
         <input type="hidden" value=""  id="DictionaryConditon" />
    </form>
</body>
</html>
<script type="text/javascript">
    $("#div<%=ResID %>FormTable").css("height", document.documentElement.clientHeight);

</script>