<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddChildResource.aspx.cs" Inherits="Base_AddChildResource" %>

<%@ Register Src="~/Base/CommonControls/SelectRecords_Sql.ascx" TagPrefix="uc1" TagName="SelectRecords_Sql" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加子资源</title>
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

                                if (key == "启用添加" || key == "启用编辑" || key == "启用删除" || key == "启用导出" || key == "是否启用") {
                                    if (jsonList[i][key] == 1)
                                        $("#<%=ResID %>_"+key).attr("checked", "true");
                                }
                                else if (key == "子表关键字") {
                                      }
                            }
                        }
                    }
                });
            }
            SetBackgroundColor();
        });
      
     function fnChildSave() {//保存方法
         if (CheckValue("div<%=ResID %>FormTable")) {
             $("#fnChildSave").attr("disabled", true);
             var jsonStr1 = "[{"; 
             jsonStr1 += GetFromJson("<%=ResID %>");
                jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
                jsonStr1 += "}]";
               // alert(jsonStr1);
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: { "Json": "" + jsonStr1 + "" },
                    url: "../Common/CommonAjax_Request.aspx?typeValue=SaveInfo&ResID=<%=ResID %>&RecID=<%=RecID %>",
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

     function openFrm(strUrl,strTitle) {
         $("#ChooseTYWindow").show();
         $("#ChooseTYWindow").dialog({
             width: 800,
             height:400,
             cache: false,
             closed: false,
             shadow: false,
             closable: true,
             modal: true,
             draggable: false,
             title: strTitle,
             resizable: false
         });
         $("#ChooseTYWindow").dialog('open');
         $("#ChooseTYWindow").dialog('refresh', strUrl);
         $.parser.parse($("#ChooseTYWindow"));
     }

     function choose() {
         var ChildResID = $("#<%=ResID %>_子表资源ID").val();
         if (ChildResID == "") {
             alert("请先输入子表资源ID");
         } else {
             var url = encodeURI("../CommonDictionary/ResColDictionary.aspx?ResID=" + ChildResID + "&IsmultiSelect=1&Params=<%=ResID %>_默认数据排序=ColShowName");
             openFrm(url, '资源字段字典');
         }
     }
     function choose1(IsmultiSelect, InputName) {
         var ChildResID = $("#<%=ResID %>_子表资源ID").val();
         if (ChildResID == "") {
             alert("请先输入子表资源ID");
         } else {
             var url = encodeURI("ResRelatedColDictionary.aspx?ResID=" + ChildResID + "&PResID=" + $("#<%=ResID %>_主表资源ID").val() + "&IsmultiSelect=" + IsmultiSelect + "&Params=" + InputName + "=ColShowName");
             openFrm(url, '资源字段关联字典');
         }
     }

     //选择主子表关联字段专用方法
     function ChooseOk(_IsmultiSelect) {

         var parentStrValue = $("input[name='parentRdo']:checked").val();
         var childStrValue = $("input[name='childRdo']:checked").val();
         if (parentStrValue == undefined) {
             alert("请选择主表资源字段");
             return;
         }
         if (childStrValue == undefined) {
             alert("请选择子表资源字段");
             return;
         }
         $('#ChooseTYWindow').dialog('close');

         if (_IsmultiSelect == "0") {
             //主子表关联字段赋值
             $("#<%=ResID %>_台帐主子表关联字段").val(parentStrValue + "=" + childStrValue);
         } else {
             //主子表引用字段赋值
             var nowValue = $("#<%=ResID %>_台帐子表引用字段").val();
             if (nowValue == "")
                 $("#<%=ResID %>_台帐子表引用字段").val(parentStrValue + "=" + childStrValue);
             else
                 $("#<%=ResID %>_台帐子表引用字段").val(nowValue + "," + parentStrValue + "=" + childStrValue);
         }
     }

     function GetSysSettingsData(key, ResID, ResName) {
         $("#<%=ResID %>_子表关键字").val(key);
         $("#<%=ResID %>_子表资源ID").val(ResID);
         $("#<%=ResID %>_子资源名称").val(ResName);
     }
    </script>
    <form id="form1" runat="server">
        <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y: hidden; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnChildSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="子模块配置" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;">
                    <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width:100%;">
                        <tr> 
                            <th>子表配置号</th>
                            <td><input id="<%=ResID %>_子表配置号" type="text" class="box3" readonly="readonly"/></td>
                             <th>主表关键字</th>
                            <td><input id="<%=ResID %>_主表关键字" type="text" class="box3" minput="true" fieldtitle="主表关键字" value="<%=RelatedValue %>"  readonly="readonly"  style="width:80%;"/><span style="color:red;">&nbsp;*</span></td>
                            <th>主表资源ID</th>
                            <td><input id="<%=ResID %>_主表资源ID" type="text" class="box3" value="<%=RelatedResID %>" readonly="readonly" style="width:80%;"/><span style="color:red;">&nbsp;*</span></td>
                        </tr>
                         <tr>
                             <th>子表资源</th>
                             <td> 
                                <input id="<%=ResID %>_子表关键字" type="text" class="box3" style="width:80%;"/>
                                <input id="<%=ResID %>_子资源名称" type="text" class="box3" style="width:80%; display:none;"/><a id="A3" href="#" onclick="openDictionary('SysSettingsDictionary.aspx',0,0,'系统配置字典'); ">选择</a>
                             </td>
                             <th>子表资源ID</th>
                             <td><input id="<%=ResID %>_子表资源ID" type="text"  minput="true" fieldtitle="子表资源ID" class="box3" style="width:80%;"/><span style="color:red;">&nbsp;*</span>
                                
                             </td>
                             <th>显示标题</th>
                             <td><input id="<%=ResID %>_显示标题" minput="true"  fieldtitle="显示标题" type="text" class="box3" style="width:80%;"/><span style="color:red;">&nbsp;*</span></td>
                         </tr> 
                        <tr>
                             <th>子表排序号</th>
                             <td><input id="<%=ResID %>_子表排序号" type="text" class="box3" fieldtitle="显示Title" fieldtype="num"  style="width:80%;"/></td>
                             <th>主子表关联字段</th>
                             <td ><input id="<%=ResID %>_台帐主子表关联字段" type="text" class="box3" style="width:80%;"/>&nbsp;<a id="A1" href="#" onclick="choose1(0,'<%=ResID %>_台帐主子表关联字段')">选择</a></td>
                            <td colspan="2" style=" text-align:center;"><input id="<%=ResID %>_是否启用" type="checkbox" />是否启用&nbsp;&nbsp;&nbsp;
                                <input id="<%=ResID %>_启用添加" type="checkbox" />启用添加 
                            </td>
                        </tr>
                        <tr>
                             <th>台帐子表引用字段</th>
                             <td colspan="3"><input id="<%=ResID %>_台帐子表引用字段" placeholder="如有多个引用字段，请多次选择" type="text" class="box3" style="width:93%;"/>&nbsp;<a id="A2" href="#" onclick="choose1(1,'<%=ResID %>_台帐子表引用字段')">选择</a></td>
                             <td colspan="2" style=" text-align:center;">
                                <input id="<%=ResID %>_启用编辑" type="checkbox" />启用编辑&nbsp;&nbsp;&nbsp;
                                <input id="<%=ResID %>_启用删除" type="checkbox"/>启用删除&nbsp;&nbsp;&nbsp;
                                <input id="<%=ResID %>_启用导出" type="checkbox"/>启用导出
                             </td>
                        </tr>  
                        <tr>
                             <th>默认数据排序</th>
                             <td colspan="3"><input id="<%=ResID %>_默认数据排序" type="text" class="box3" style="width:93%;"/>&nbsp;<a id="ASelectKHMC" href="#" onclick="choose()">选择</a></td>
                             <th>是否有添加修改删除后操作</th>
                             <td><input id="<%=ResID %>_是否有添加修改删除后操作" type="text" class="box3" style="width:80%;"/></td>
                         </tr>
                        <tr>
                             <th>初始筛选条件</th>
                             <td colspan="3"><input id="<%=ResID %>_初始筛选条件" type="text" class="box3" style="width:93%;"/></td>
                             <th>是否有添加修改删除前操作</th>
                             <td><input id="<%=ResID %>_是否有添加修改删除前操作" type="text" class="box3" style="width:80%;"/></td> 
                        </tr>
                    </table>
                    
                    
                </div>
            </div>
        </div>
         <div id="ChooseTYWindow" style="overflow-x:hidden;display:none;"></div>
         <input type="hidden" value=""  id="DictionaryConditon" />
         
        <input type="hidden" value="" id="childForm" /> 
        <input type="hidden" value="" id="ajaxinfo" />
    <div closed="true" class="easyui-window" id="divDictionary" style="overflow: hidden;" />
    </form>
</body>
</html>
<script type="text/javascript">
    $("#div<%=ResID %>FormTable").css("height", document.documentElement.clientHeight);

</script>