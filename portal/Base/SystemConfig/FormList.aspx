<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormList.aspx.cs" Inherits="Base_Config_FormList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资源列表</title>
</head>
<body>
    <script type="text/javascript">
        var centerHeight = $('#westModel_id',window.parent.document).height()-30;
        var centerWidth = $('#centerModel_id',window.parent.document).width();

        var 详细记录div = "";
        var 详细记录Grid = "";
        var onCollapseTZ=false
        var FatherKey="";
        var FatherID="";
        var FatherResID="";
        var divHeight= 250;

        var RelatedValue="";
        
        var FieldValues = [];
        var MasterTableAssociation = eval('<% = MasterTableAssociationstr %>'); 
        $(document).ready(function () {
            $("#详细记录Panle").resizable({ 
                disabled:false,
                handles:"s",
                minWidth:5,
                minHeight:5,
                maxWidth:700, 
                maxHeight:500,
                edge:10,
                onStopResize:function(e)
                {
                    $("#详细记录Panle" ).css({ height:  e.data.height });
                    $("#centerDivSetHeight<%=ResID %>").css({ height:  e.data.height });
  
                    $("#CenterGrid<%=ResID %>").datagrid('resize', {
                        height: e.data.height 
                    })
                    divHeight =  $("#" + 详细记录div )[0].clientHeight + (e.data.startHeight - e.data.height)-30;
                    
                    $("#" + 详细记录div ).css({ height:  divHeight});
                    $('#' + 详细记录Grid).datagrid('resize', {
                        height:divHeight
                    });
                }
            }); 
            var keyWordValue = '<%=ResID %>';
            $("#allSearch_panel<%=ResID %>").panel({
                border: true,
                iconCls: 'icon-search',
                collapsible: true
            }); 
            var GridHeight = centerHeight-divHeight;
            $("#centerDivSetHeight<%=ResID %>").css({ height: GridHeight });
                 
            loadCenterGrid(GridHeight, '<%=ResID %>');
            $(".easyui-linkbutton").linkbutton({});
            $('#详细记录Panle').panel({       
                onExpand: function(){
                    onCollapseTZ=false
                    $("#" + 详细记录div ).css({ height: divHeight});
                    $('#' + 详细记录Grid).datagrid('resize', {
                        height: divHeight
                    });
                },
                onCollapse: function(){
                    onCollapseTZ=true;
                    $("#" + 详细记录div ).css({ height: centerHeight+150 });
                    $('#' + 详细记录Grid).datagrid('resize', {
                        height: centerHeight
                    });
                }
            });  
        });

        function loadCenterGrid(GridHeight, keyWordValue) {
            var titleValue = '<%=titleValue %>';
            var ResID = '<%=ResID %>';
            $.ajax({
                type: "POST",
                data: {
                    "ColName": '列表配置号,前台标签,关键字,显示Title,参数关键字,资源ID',
                    "ColField": '列表配置号,前台标签,参数关键字,显示Title,参数关键字,值',
                    "ColWidth": '100,300,160,160,160,160'
                },
                url: "Ajax_Request.aspx?typeValue=GetfieldInfo&keyWordValue=<%=ResID %>",   
                success: function (fieldValueStr) {
                    fieldValueStr = "[[" + fieldValueStr + "]]";
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    var fieldValue = fieldValueStrList[0];
                    var sysValue = fieldValueStrList[1]; 
                    var isAddDisabled = false;
                    var isDeleteDisabled=false;
                    var isExportDisabled=false;
                    var IsUpdateDisabled = false;

                    
                    $('#CenterGrid' + keyWordValue).datagrid({
                        toolbar: [{
                            iconCls: 'icon-add',
                            text: "添加",
                            disabled: isAddDisabled,
                            handler: function () {
                                var dialogUrl = "SystemConfig/AddSystemConfig.aspx?ResID=<%=ResID %>&NodeID=<%=NodeID %>";
                                fnFormListDialog(dialogUrl, 0,0,"添加信息"); 
                             }
                        },'-',{
                            iconCls: 'icon-edit',
                            text: "修改",
                            disabled: IsUpdateDisabled,
                            handler: function () {
                                if ($('#CenterGrid' + keyWordValue).datagrid('getSelected') == null || $('#CenterGrid' + keyWordValue).datagrid('getSelected') == "") {
                                    alert("没有选择任何行，请选择要修改的记录！");
                                    return false;
                                } else { 
                                    var dialogUrl = "SystemConfig/AddSystemConfig.aspx?ResID=<%=ResID %>&NodeID=<%=NodeID %>&RecID=" + $('#CenterGrid' + keyWordValue).datagrid('getSelected').ID                                     
                                    fnFormListDialog(dialogUrl, 0,0,"修改信息"); 
                                }
                             }
                        },'-',{
                            iconCls: 'icon-no',
                            disabled: isDeleteDisabled,
                            text: "删除",
                            handler: function () {
                                var rec = $('#CenterGrid' + keyWordValue).datagrid('getSelected');
                                if (rec == null) {
                                    alert("请选择一条要删除的记录!")
                                    return false;
                                }
                                if (confirm("确定要删除该条记录吗？")) {
                                    $.ajax({
                                        type: "POST",
                                        url: "../Common/Ajax_Request.aspx?typeValue=DeleteRow&ResID=<%=ResID %>&RecID=" + rec.ID,
                                        success: function (fieldValue) { 
                                            var obj = eval(fieldValue);
                                            if (obj.success || obj.success == "true") {
                                                alert("删除成功！"); 
                                                $('#CenterGrid' + keyWordValue).datagrid("reload");
                                            }
                                        }
                                    });
                                }
                            }
                        }],
                        height: GridHeight,
                        nowrap: true,
                        border: true,
                        striped: true,
                        singleSelect: true,
                        fit: false,
                        pagination: true,
                        fitColumns: true,
                        rownumbers: true,
                        loadFilter: function (data) {
                            if (data == null) {
                                $(this).datagrid("load");
                                return $(this).datagrid("getData");
                            } else {
                                return data;
                            }
                        },
                      
                        url: "Ajax_Request.aspx?typeValue=GetDataByResourceInfo&ResID=<%=NodeID %>",
                        queryParams: {
                            ResID: '<%=NodeID %>',
                            Condition: ''
                        },
                        columns: eval(fieldValue),
                        onClickRow: function(rowIndex,rowData){ 
                              var ConditionStr=""
                            var key1="<%=QuertTZKey1 %>";
                            var key2="<%=QuertTZKey2 %>";
                            var key3="<%=QuertTZKey3 %>";
                            FieldValues = rowData;
                            $.each(rowData,function(idx,item){ 
                                if(idx=="ID" && item != null ){ 
                                    FatherID=item;
                                }
                                if(idx=="ResID" && item != null ){ 
                                    FatherResID=item;
                                }
                                if(idx==key1 && key1 !="" && item != null ){ 
                                    if (ConditionStr !="") ConditionStr += ' AND '
                                    ConditionStr += idx  + " = '" + item +  "'" ;
                                    // FatherKey=item;
                                }
                                else if(idx==key2 && key2 !="" && item != null ){ 
                                    if (ConditionStr !="") ConditionStr += ' AND '
                                    ConditionStr += idx  + " = '" + item +  "'" ;
                                }
                                else if(idx==key3 && key3 !="" && item != null ){ 
                                    if (ConditionStr !="") ConditionStr += ' AND '
                                    ConditionStr += idx  + " = '" + item +  "'" ;
                                }                    
                            });
                            
                            loadChildList(ConditionStr);

                        }
                    });
                    var p = $('#CenterGrid' + keyWordValue).datagrid('getPager');
                    $(p).pagination({                       
                        pageSize: <%=PageSize %>,                        
                        pageList : [15, 20, 50 ],
                        beforePageText: '第',
                        afterPageText: '页  共 {pages} 页',
                        displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
                    });
                    $("#allSearch_panel" + keyWordValue).panel({
                        onCollapse: function () {
                          
                            $("#centerDivSetHeight<%=ResID %>").css({ height: $('#westModel_id',window.parent.document).height()-20 });
                            $('#CenterGrid<%=ResID %>').datagrid('resize', {
                                height: $('#westModel_id',window.parent.document).height()-25
                            });
                        },
                        onExpand: function () {
                            $("#centerDivSetHeight<%=ResID %>").css({ height: GridHeight });
                            $('#CenterGrid<%=ResID %>').datagrid('resize', {
                                height: GridHeight
                            });
                        }
                    });
                    //左侧树折叠时改变面板的方法
                    $("#westModel_id").panel({
                        onCollapse: function () {
                            $('#CenterGrid<%=ResID %>').datagrid('resize', {
                                width: centerWidth + 170
                            });
                            changeAllSearchPanelSize(centerWidth + 170, keyWordValue);
                        },
                        onExpand: function () {
                            $('#CenterGrid<%=ResID %>').datagrid('resize', {
                                width: centerWidth
                            });
                            changeAllSearchPanelSize(centerWidth, keyWordValue)
                        }
                    });
                }
            });
            loadChildList("");
        }

        function SetAssociatedFields(AssociatedFields)
        { 
            if (AssociatedFields.indexOf(',') > 0)
            {
                var  Fields  = AssociatedFields.split(',')
                var  Fieldstr  = ""
                for (var i = 0; i < Fields.length; i++) {
                    Fieldstr = Fieldstr + " and " + GetAssociatedFieldsStr(Fields[i])
                }
                return Fieldstr
            }
            else
            {
                return GetAssociatedFieldsStr(AssociatedFields)
            }
        }
          
        function GetAssociatedFieldsStr(AssociatedFields){
            var strValue=""; 
            //debugger
            if (AssociatedFields.indexOf('=') > 0)
            {
                var AssociatedField = AssociatedFields.split('=')[0]
                var ValueKey = AssociatedFields.split('=')[1]
                for (var key in FieldValues) {
                    if (key == AssociatedField) 
                    {
                        strValue=  ValueKey  +  " = '" + FieldValues[key]  +  "'"
                        RelatedValue=FieldValues[key];
                        break;
                    }
                }
                if(strValue=="") {strValue=ValueKey+"=''";}
            }
            
            return strValue;
        }

        function loadChildList(ConditionStr) {
            $("#DPListTabs").tabs({
                onSelect: function (title) {
                    var keyWordValue = $("#<%=ResID %>" + title).attr("key");
                    var ResID = $("#<%=ResID %>" + title).attr("ResID");
               
                    详细记录div = "<%=ResID %>" + title ;
                    详细记录Grid = "Grid_" + keyWordValue;

                    $("#" + 详细记录div).css({ height: divHeight });
                    var OpenPageUrl="";
                    if(title=="子模块配置表"){
                        OpenPageUrl="SystemConfig/AddChildResource.aspx";
                    }
                    else if(title=="模块自定义操作配置表"){
                        OpenPageUrl="SystemConfig/AddModuleOperation.aspx";
                    }
                    else if(title=="记录自定义操作配置表"){
                        OpenPageUrl="SystemConfig/AddRecordOperation.aspx";
                    }
                    else if(title=="查询配置表"){
                        OpenPageUrl="SystemConfig/AddSearch.aspx";
                    }
                    else if(title=="列表字段"){
                        OpenPageUrl="SystemConfig/AddListColumn.aspx";
                    }
                    loadCenterAddGrid(title, keyWordValue,ResID, "Grid" + keyWordValue,OpenPageUrl,ConditionStr);
                }
            });
        }
        //加载grid，传这个grid的列 
        function loadCenterAddGrid( titleValue,keyWordValue, ResID, ZhuangxiuAddGridID,OpenPageUrl,ConditionStr) {         
            $.ajax({
                type: "POST",
                url: "Ajax_Request.aspx?typeValue=GetfieldChild&ResID=" + ResID,
                success: function (fieldValueStr) {
                    fieldValueStr = "[[" + fieldValueStr + "]]";
                    var Condition = "" ;
                    if (ConditionStr != "") {
                        Condition =" and " + ConditionStr;
                    }else{
                        Condition =" and 1=1 ";
                    } 
                   
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    var fieldValue = fieldValueStrList[0];
                    var sysValue = fieldValueStrList[1]; 
                    var isAddDisabled = false;
                    var isDeleteDisabled=false;
                    var isExportDisabled=false;
                    var IsUpdateDisabled = false; 

                  $("#" + ZhuangxiuAddGridID).datagrid({
                      toolbar: [{
                          iconCls: 'icon-add',
                          text: '添加',
                          disabled: isAddDisabled,
                          handler: function () {
                              if ($('#CenterGrid<%=ResID%>').datagrid('getSelected')== null || $('#CenterGrid<%=ResID%>').datagrid('getSelected').ID == "" ) {
                                  alert("请选择一条主表记录!")
                                  return false;
                              } 
                              else 
                              {
                                  RelatedValue=$('#CenterGrid<%=ResID%>').datagrid('getSelected').参数关键字;
                                  var dialogUrl = OpenPageUrl+"?ResID="+ResID+"&PResID=<%=ResID %>&RelatedResID=<%=NodeID %>&RelatedValue=" + RelatedValue;
                                  
                                  dialogUrl = encodeURI(dialogUrl);
                                  fnFormListDialog(dialogUrl, 0,0,"添加信息");
                                  
                              } 
                          }
                      },'-',{
                          iconCls: 'icon-edit',
                          text: "修改",
                          disabled: IsUpdateDisabled,
                          handler: function () { 
                              var selected = $("#" + ZhuangxiuAddGridID).datagrid('getSelected');
                              if (selected== null || selected.ID == "" ) {
                                  alert("请选择一条记录!")
                                  return false;
                              }else { 
                                  var dialogUrl = OpenPageUrl+"?ResID="+ResID+"&RecID=" + selected.ID+"&PResID=<%=ResID %>";
                                  if(OpenPageUrl.indexOf("AddSearch.aspx")>=0) dialogUrl = OpenPageUrl+"?ResID="+ResID+"&PResID=<%=ResID %>&RelatedResID=<%=NodeID %>&RelatedValue=" + RelatedValue;
                                  if(OpenPageUrl.indexOf("AddListColumn.aspx")>=0) dialogUrl = OpenPageUrl+"?ResID="+ResID+"&PResID=<%=ResID %>&RelatedResID=<%=NodeID %>&RelatedValue=" + RelatedValue;
                                  dialogUrl = encodeURI(dialogUrl);
                              
                                  fnFormListDialog(dialogUrl, 0,0,"修改信息");
                                 
                              }
                          } 
                      },'-',{
                          iconCls: 'icon-no',
                          disabled: isDeleteDisabled,
                          text: "删除",
                          handler: function (){
                              var selected = $("#" + ZhuangxiuAddGridID).datagrid('getSelected');
                              if (selected == null) {
                                  alert("请选择一条要删除的记录!")
                                  return false;
                              }
                              if (confirm("确定要删除该条记录吗？")) {
                                  $.ajax({
                                      type: "POST",
                                      url: "../Common/Ajax_Request.aspx?typeValue=DeleteRow&ResID="+ResID+"&RecID=" + selected.ID,
                                        success: function (fieldValue) {                                             
                                                $("#" + ZhuangxiuAddGridID).datagrid("reload");                                             
                                        }
                                    });
                                }
                          }
                      }],
                      height: 500,
                      nowrap: true,
                      border: true,
                      striped: true,
                      singleSelect: true,
                      pageSize: "<%=PageSize2 %>",
                      fit: true,
                      pagination: true,
                      fitColumns: true,
                      rownumbers: true,
                      loadFilter: function (data) {
                          if (data == null) {
                              $(this).datagrid("load");
                              return $(this).datagrid("getData");
                          } else { 
                              return data;
                          }
                      },
                      url: "Ajax_Request.aspx?typeValue=GetDataOfPage",
                      queryParams: {  
                          ResID: ResID,
                          keyWordValue: keyWordValue,
                          //Condition: Condition
                          Condition: SetAssociatedFields($("#ChildGridCondition" + keyWordValue).val()) == "" ?  Condition: ' and ' + SetAssociatedFields($("#ChildGridCondition" + keyWordValue).val()) 
                      },
                      columns: eval(fieldValue), //把字符串转成对象。  
                      fitColumns: true,
                      onSortColumn: function (sort, order) {
                          fnGridLoadOfXM(sort, order, KHBH);
                      },
                      rowStyler:function(index,row){//改变行颜色
                           
                      }   
                  });
                  var p = $("#" + ZhuangxiuAddGridID).datagrid('getPager');
                  $(p).pagination({
                      beforePageText: '第',
                      pageSize: <%=PageSize2 %>,
                      afterPageText: '页  共 {pages} 页',
                      displayMsg: '当前显示从 [{from}] 到 [{to}] 共 [{total}] 条记录'
                  });
              }
          });
      }
      function CloseChildWindow() {
          $('#CenterAddWindow').dialog('close');
      } 


      function GetMasterTableAssociationIndex(key)
      {
          if( MasterTableAssociation == "" || MasterTableAssociation == undefined )
              return -1;

          for(var i=0;i<MasterTableAssociation.length;i++)
          {
              if (MasterTableAssociation[i].ChildKeyWord == key)
                  return i;
          }
          return -1;
      }


      function   GetConditionStr(Str)
      {
          var jie = Str.split('&');
          var ConditionStr="";
          for(var i=1;i<jie.length;i++)
          {
              if (jie[i].indexOf("IsUpdate=") == -1 && jie[i].indexOf("IsDelete=") == -1)
              {
                  if (ConditionStr !="") ConditionStr += ' AND '
                  ConditionStr += jie[i].split('=')[0]  + " = '" + jie[i].split('=')[1] +  "'" ;
              }
          }
          if (ConditionStr !="" ) loadChildList(ConditionStr);
      }
      function changeAllSearchPanelSize(newWidth, keyWordValue) {
            
          $("#allSearch_panel"+keyWordValue).panel('resize', {
              width: newWidth
          });
          $("#search_panel"+keyWordValue).panel('resize', {
              width: newWidth
          })
          $("#search_panel1"+keyWordValue).panel('resize', {
              width: newWidth
          })
      } 
      function fnLink(url,DialogWidth,DialogHeight)
      {
          fnParentFormListDialog2(url,DialogWidth,DialogHeight,"基本信息");
      }
      function fnFormListDialog(url,DialogWidth,DialogHeight,title)
      {  
          url=  "Base/"+ url;
          window.parent.fnParentFormListDialog(url,DialogWidth,DialogHeight,title);
      } 
      function fnGridLoad(Gridid)
      { 
          if(Gridid=="CenterGrid<%=ResID %>"){
                $("#"+Gridid).datagrid('load', { 
                    Condition:  $("#hidSearchCondition<%=ResID %>").val() 
            });}
        else{
            $("#" + Gridid).datagrid("reload");}
    }
    </script> 
    <div title="详细记录" collapsible="true" class="easyui-panel" style="overflow: hidden; padding: 0px; border-bottom: none; margin: 0px;" id="详细记录Panle">
        <div id="centerDivSetHeight<%=ResID %>">
            <table id="CenterGrid<%=ResID %>"></table>
        </div>
    </div>
    <div id="DPListTabs">
        <asp:Repeater ID="RepTabList" runat="server">
            <ItemTemplate>
                <div key="<%=ResID %>_<%#Eval("ChildResID") %>" ResID="<%#Eval("ChildResID") %>" title="<%#Eval("ChildResName") %>" id='<%=ResID %><%#Eval("ChildResName") %>' style="padding: 0px;">
                    <table id='Grid<%=ResID %>_<%#Eval("ChildResID") %>'>
                         <input type="hidden" value="<%#Eval("主子表关联字段") %>" id='ChildGridCondition<%=ResID %>_<%#Eval("ChildResID") %>' />
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <input type="hidden" value="" id="DictionaryConditon>" />
    <input id="hidExpCondition" value="" type="hidden" />
    <input type="hidden" id="hidSearchCondition<%=ResID %>" />
    <div id="CenterAddWindow" style="overflow: hidden; position: relative; display: none;"></div>
    <div id="divParentContent2" closed="true" class="easyui-window"  style="overflow: hidden;" />
        <div id="divContent" ></div>
</body>
</html>
