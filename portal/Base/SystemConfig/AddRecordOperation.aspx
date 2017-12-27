<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRecordOperation.aspx.cs" Inherits="Base_AddRecordOperation" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
</head>
<body>
 <script type="text/javascript" language="javascript">
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
                                if (key == "是否启用") {
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

     function choose() {
         var ChildResID = $("#<%=ResID %>_子表资源ID").val();
         if (ChildResID == "") {
             alert("请先输入子表资源ID");
         } else {
             var url = encodeURI("../CommonDictionary/ResColDictionary.aspx?ResID=<%=RelatedResID %>&key=" + $("#<%=ResID %>_参数关键字").val() + "&IsmultiSelect=0&Params=<%=ResID %>_链接字段=ColShowName");
             $("#ChooseTYWindow").show();
             $("#ChooseTYWindow").dialog({
                 width:500,
                 height:405,
                 cache: false,
                 closed: true,
                 shadow: false,
                 closable: true,
                 modal: true,
                 draggable: false,
                 title: '资源字段字典',
                 resizable: false
             });
             $("#ChooseTYWindow").dialog('open');
             $("#ChooseTYWindow").dialog('refresh', url);
             $.parser.parse($("#ChooseTYWindow"));
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
            <div title="记录自定义操作配置" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;">
                    <table  border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width:100%">
                        <tr> 
                            <th style="width:11%">操作编号</th>
                            <td style="width:22%">
                                <input id="<%=ResID %>_操作编号" type="text" class="box3" readonly="readonly" />
                            </td>
                             <th style="width:11%">参数关键字</th>
                            <td style="width:22%">
                                <input id="<%=ResID %>_参数关键字" type="text" class="box3" minput="true" fieldtitle="参数关键字" value="<%=RelatedValue %>"  readonly="readonly"/>
                            </td> 
                             <td colspan="2" style="text-align:center;"><input id="<%=ResID %>_是否启用" type="checkbox" />是否启用</td>   
                        </tr>
                         <tr>
                            <th>链接字段</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_链接字段" type="text" class="box3" minput="true" fieldtitle="链接字段" style="width:65%;"/>&nbsp;<a id="ASelectKHMC" href="#" onclick="choose()">选择字段</a>&nbsp;&nbsp;&nbsp;<a id="A1" href="#" onclick="choose()">选择历史数据</a>
                            </td>
                            <th style="width:11%">操作类型</th>  
                            <td style="width:22%">
                                  <select id="<%=ResID %>_操作类型" class="box3" minput="true" fieldtitle="操作类型" style="width:80%;">
                                    <option value="跳转链接">跳转链接</option>
                                    <option value="操作">操作</option>
                                    <option value="流程操作">流程操作</option>
                                </select> </td>
                         </tr>
                        <tr> 
                             <th>链接地址</th>
                             <td colspan="3"><input id="<%=ResID %>_链接地址" type="text" class="box3" style="width:80%;"/></td>                             
                            <th>链接跳转方式</th>
                             <td> 
                                  <select id="<%=ResID %>_链接跳转方式" class="box3" style="width:80%;">
                                    <option value="_ChildFrm">弹出子窗口</option>
                                    <option value="_parent">父页面打开</option>
                                    <option value="_blank">打开新页面</option>
                                </select> </td></tr>
                        <tr> 
                            <th>Function名称</th>
                            <td colspan="3"><input id="<%=ResID %>_Function名称" type="text" class="box3" style="width:80%;"/></td>  
                            <th>图标样式</th>  
                            <td><input id="<%=ResID %>_图标样式" type="text" class="box3" style="width:80%;"/></td>
                        </tr>
                        <tr> 
                            <th>链接或方法参数</th>
                            <td colspan="3"><input id="<%=ResID %>_链接或方法参数" type="text" class="box3" style="width:80%;"/></td>    
                            <th>排序</th>
                            <td><input id="<%=ResID %>_排序" type="text" class="box3" fieldtitle="页面宽度" fieldtype="num" /></td> 
                        </tr>
                        <tr>
                            <th>跳转页面标题</th>
                            <td><input id="<%=ResID %>_跳转页面标题" type="text" class="box3" style="width:80%;"/></td>  
                            <th>页面宽度</th>
                            <td><input id="<%=ResID %>_打开的页面宽度" type="text" class="box3" fieldtitle="页面宽度" value="0" fieldtype="num" /></td>                            
                            <th>页面高度</th>
                            <td><input id="<%=ResID %>_打开的页面高度" type="text" class="box3" fieldtitle="页面高度" value="0" fieldtype="num" /></td>
                        </tr>
                    </table>
                </div>
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