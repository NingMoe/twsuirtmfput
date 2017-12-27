<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportFormList.aspx.cs" Inherits="Base_ReportFormList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据列表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>     
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
    <script type="text/javascript" src="../Scripts/DataGridFieldStyle.js"></script>
</head>
<body>
    <script type="text/javascript">      
      
        var CommonPath=GetApplicationPath();
        var fieldValue ="[]";
        var loaded = false;
        var centerHeight = $('#content',window.parent.document).height();             
        var keyWordValue = '<%=keyWordValue %>';  
        var titleValue = '<%=titleValue %>';          
        var ResID = '<%=ResID %>';   
        var sysObj ={};
        var title="";
        var width=0;
        var height=0;
        var url="";
        var URLTargets="";
       
        $(document).ready(function () {
            //获取列集合\工具栏
            $.ajax({
                type: "POST",
                url: "../Base/Common/Ajax_Request.aspx?typeValue=GetField&keyWordValue=" + keyWordValue+"&MenuResID=<%=MenuResID %>",
                success: function (fieldValueStr) { 
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    //字段集合
                    fieldValue = fieldValueStrList[0]; 
                    var sysValue = fieldValueStrList[1];
                    //获取添加地址、编辑地址、窗口高度、宽度、按钮权限等
                    sysObj = eval(sysValue);
                    var FootStr=sysObj[0].FootStr;
                    var isShowFoot=false;
                    if (FootStr!="") {
                        isShowFoot=true;
                    }
                    //获取显示Title
                    titleValue=sysObj[0].ShowTitle;
                    //判断按钮权限
                    if(sysObj[0].AddUrl==""||sysObj[0].IsAdd==false||sysObj[0].IsAddRights==false){
                        $("#btnAdd").linkbutton("disable");
                    }
                    //判断按钮权限
                    if(sysObj[0].EidtUrl==""||sysObj[0].IsUpdate==false||sysObj[0].IsUpdateRights==false){
                        $("#btnEdit").linkbutton("disable");
                    }
                    //判断按钮权限
                    if(sysObj[0].IsDelete==false||sysObj[0].IsDeleteRights==false){
                        $("#btnRemove").linkbutton("disable");
                    }                     
                    var UserDefinedToolBars = fieldValueStrList[2];
                    var GridUrl=encodeURI("../Base/Common/Ajax_Request.aspx?typeValue=GetDataOfPage&ResID=<%=ResID %>&keyWordValue=<%=keyWordValue %>&Authority=<%=UserDefinedSql%>&FootStr="+FootStr);
                    //动态设置Grid相关属性
                    $('#dg-<%=keyWordValue %>').datagrid({ 
                        height: centerHeight - 30, 
                        columns: eval(fieldValue), 
                        striped:true,
                        fitColumns:false,
                        showFooter:isShowFoot,
                        singleSelect:!sysObj[0].IsCheckBox,
                        selectOnCheck:sysObj[0].IsCheckBox,
                        checkOnSelect:sysObj[0].IsCheckBox,
                        pageSize:<%=PageSize %>,
                        url:GridUrl
                    });
                }
            });  
        });       
         
        //数据刷新方法
        function fnGridReload() {
            $("#dg-<%=keyWordValue %>").datagrid('reload');
        }

        
        //关闭窗口方法，并刷新
        function closeWindow1(){    
            fnGridReload();
            //$('#win-<%=keyWordValue %>').window('close');  
            window.parent.ParentCloseWindow();     
        }

        //关闭窗口方法，不并刷新
        function closeWindow2(){ 
        ///alert(2);
            //$('#win-<%=keyWordValue %>').window('close');    
            window.parent.ParentCloseWindow();
        }

        //添加按钮事件
        function AddRec(){
            title=sysObj[0].ShowTitle;//标题
            width=sysObj[0].DialogWidth;//窗口宽度
            height=sysObj[0].DialogHeight;//高度
            url=sysObj[0].AddUrl;//添加地址
            URLTargets=sysObj[0].URLTarget;//链接打开方式
            url="Base/"+url;
            if(URLTargets=="_ChildFrm")
                showWindow(title,url,width,height);
            else
                window.open (url, 'newwindow');        
        }

        //修改按钮事件
       function EditRec(){            
            var getRow = $('#dg-<%=keyWordValue %>').datagrid('getSelected');    
            if (getRow==null){
                alert("请选中要编辑的记录");
                return;
            }else{
                title=sysObj[0].ShowTitle;//标题
                width=sysObj[0].DialogWidth;//窗口宽度
                height=sysObj[0].DialogHeight;//高度
                url=sysObj[0].EidtUrl;//添加地址
                URLTargets=sysObj[0].URLTarget;//链接打开方式
                if (url.indexOf("?")==-1) {
                    url+="?RecID="+getRow.ID;
                }else {
                    url+="&RecID="+getRow.ID;
                }
                url="Base/"+url;
                if(URLTargets=="_ChildFrm")
                    showWindow(title,url,width,height);
                else
                    window.open (url, 'newwindow');                             
            }
        
       } 

         //查阅按钮事件
        function ViewRec(type){  
            var getRow = $('#dg-<%=keyWordValue %>').datagrid('getSelected');    
            if (getRow==null){
                alert("请选中要查阅的记录");
                return;
            }else{
                title=sysObj[0].ShowTitle;//标题
                width=sysObj[0].DialogWidth;//窗口宽度
                height=sysObj[0].DialogHeight;//高度
                url=sysObj[0].EidtUrl;//添加地址
                URLTargets=sysObj[0].URLTarget;//链接打开方式
               
                if  (url.indexOf("?")==-1) {
                     url+="?RecID="+getRow.ID+"&SearchType="+type;
                }else{
                     url+="&RecID="+getRow.ID+"&SearchType="+type;
                }
                url="Base/"+url;
                if(URLTargets=="_ChildFrm")
                    showWindow(title,url,width,height);
                else
                    window.open (url, 'newwindow');                             
            }
        } 
      
        //删除按钮事件
        function DeleteRec(){
            var getRow = $('#dg-<%=keyWordValue %>').datagrid('getSelected');    
            if (getRow==null){
                alert("请选中要删除的记录");
                return;
            }
            else{
                if (confirm("确定要删除该条记录吗？")) {
                    $.ajax({
                        type: "POST",
                        url: "../Base/Common/Ajax_Request.aspx?typeValue=DeleteRow&ResID=<%=ResID %>&RecID=" + getRow.ID,
                        success: function (strJSON) { 
                            var obj = eval("(" + strJSON + ")");
                            if (obj.success || obj.success == "true") {
                                alert("删除成功！"); 
                            }else {
                                alert("删除失败！");
                            }
                        }                 
                    });
                }
            }
            fnGridReload();
        }

        //Excel导出功能
        function ExportExcel(_resId,_keyValue){
            alert("正在导出，稍后请选择保存位置。");
            var searchs=$("#<%=ResID %>_ExpWhereStr").val();
            var seaCondition=$('#<%=ResID %>_Search').val();
            //参数较多，先调用后台方法，存储参数，然后在回调导出Excel方法
            $.post("../Base/Common/Ajax_Request.aspx?typeValue=saveExcelData", {conditionStr:searchs,KeyWord:_keyValue,Condition:seaCondition}, function(data){
                var dataObj=eval("("+data+")");   
                if(dataObj.success){
                    // 回调                            
                    window.location.href="../Base/Common/Ajax_Request.aspx?typeValue=Excel&ExcelfileName="+titleValue+"&cookeId="+dataObj.key+"&resId=<%=ResID %>&KeyWord=<%=keyWordValue %>&Authority=<%=UserDefinedSql%>";
                }
            });
        }

        //Excel导出组件，导出列全选事件
        function CheckAllBox() {       
            $("#ExcelColItem :checkbox").each(function () {  
                this.checked =true;
              
            });  
        }
        function unCheckAllBox() {           
            $("#ExcelColItem :checkbox").each(function () {  
                this.checked =false;
            }); 

        }
       
        
        //弹出窗口
        function showWindow(_title,_url,_width,_height) {  
            //window.parent.fnParentFormListDialog(_url,_width,_height,_title);
            var widths=_width==(undefined||0)? 800 : _width;
            var heights=_height==(undefined||0)? 450 : _height;   
            window.parent.fnParentFormListDialog(_url,widths,heights,_title,"dg-<%=keyWordValue %>");          
//            $('#win-<%=keyWordValue %>').window(  
//            {  
//                title : _title,  
//                width :widths,  
//                height :heights,                   
//                content : '<iframe scrolling="no" frameborder="0"  src="'+ _url+ '" style="width:100%;height:98%;"></iframe>',                  
//                left: centerHeight/4,
//                top: 20
//            }); 
        }

        //关键字搜索
        function SearchByKeyWord(_ResId,_keyWord){
            $('#dg-'+_keyWord).datagrid('load',{
                Condition: $('#'+_ResId+'_Search').val()
            });           
        }

        //关键字搜索
        function SearchReset(_ResId,_keyWord){
            $('#'+_ResId+'_Search').textbox("setValue","");
            $("#<%=ResID %>_ExpWhereStr").val("");
            $('#dg-'+_keyWord).datagrid('load',{
                Condition: ""
            });           
        }

        //查询条件组建；_controlId组件Id【适用于高级检索、Excel导出】
        function CondtionBuild(_controlId){
             var searchStr="";
            //遍历查询组件，获取每行的控件及查询值、查询条件
            $("#"+_controlId).find("tr").each(function(){
                var types=$(this).attr("title");//获取行查询类型    
                var rowId=$(this).attr("id");//获取行Id
                var search_Type="";//获取查询类型
                var search_Type1="";//获取查询类型
                var search_Value="";//获取第一项查询值
                var Searchcol="";//获取查询列名
                var search_Values="";//获取第二项查询值  
                var key="";//拼装查询条件
              
                $("input,select",this).each(function(){ //循环获取本行内的控件及值
                    if($(this).is('input')){
                        search_Value=$.trim($(this).val());
                    }else{
                        if($(this).attr("id").substring(0,3)=="sel"){
                            search_Type1=$.trim($(this).val());
                        }else{
                            search_Type=$.trim($(this).val());
                        }
                    }                                       
                });
                               
                if(search_Type1!=""&&search_Type!="" && search_Value!=""){         
                    if(search_Type.indexOf("like") > -1){//如果是模糊查询
                        key = " isnull("+search_Type1+",'') " + search_Type+ " '%" + search_Value + "%' ";
                    }else{
                        key = " isnull("+search_Type1+",'') " + search_Type+ " '" + search_Value + "' ";
                    }
                    if(searchStr!="") searchStr+=" AND ";
                    searchStr += key;
                }else if(search_Type1!=""&&search_Type!=""){
                    if(search_Type.indexOf("like") > -1){//如果是模糊查询
                        key = " isnull("+search_Type1+",'') " + search_Type+ " '' ";
                    }else{
                        key = " isnull("+search_Type1+",'') " + search_Type+ " '' ";
                    }
                    if(searchStr!="") searchStr+=" AND ";
                    searchStr += key;
                }
            });
            return searchStr;          
        }

        //高级搜索
        function SearchByMultiCondition(){   
          
            $('#dio-<%=keyWordValue %>').dialog({               
                title : titleValue+"-高级筛选",                           
                left: centerHeight/3,
                top: 20,                
                buttons:[{
                    iconCls:'icon-cancel',
                    text:'清空',
                    handler:function(){
                        $("#fmSearchItem")[0].reset();
                    }
                },{
                    iconCls:'icon-search',
                    text:'查询',
                    handler:function(){ 

                        //TbSearchItem为查询页面（SearchControls.aspx）中元素ID
                        var searchCondTion=CondtionBuild("TbSearchItem"); //获取函数返回的检索条件                   
                        if(searchCondTion==""){
                            alert("请检查您的筛选条件！");
                            return;
                        }
                        $("#<%=ResID %>_ExpWhereStr").val(searchCondTion);
                        //执行查询
                        $('#dg-<%=keyWordValue %>').datagrid('load',{
                            FilterRules:searchCondTion
                        });
                        //关闭窗口
                        $('#dio-<%=keyWordValue %>').dialog('close');
                    }
                }]

            });

            //动态加载内容
            var _url="CommonControls/SearchControls.aspx?resId=<%=ResID %>&KeyWord=<%=keyWordValue %>";
             
            $('#dio-<%=keyWordValue %>').dialog('refresh',_url);            
        }

    function  DaoRu(){
            showWindow("导入数据","Base/DaoRuList.aspx?typeResID=<%=ResID %>",500,300);
    }


    function PrintRec(){
        var getRow = $('#dg-<%=keyWordValue %>').datagrid('getSelected'); 
        if (getRow==null){
            alert("请选中要打印的记录");
            return;
        }else{
            var printURL="";
            if ("<%=keyWordValue %>"=="BX") {
                printURL="Finance/PrintBXGL.aspx?RecID="+getRow.ID
            }
            if ("<%=keyWordValue %>"=="QK") {
                printURL="Finance/PrintQKGL.aspx?RecID="+getRow.ID
            }
            if (printURL!="") {
                 window.open(printURL, 'newwindow', 'height=600,width=900, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');                        
            }else {
                alert("未做打印，需要打印请联系管理员！");
            }
        }
    }
    </script>  
    <div id="dlg-toolbar">
	    <table cellpadding="0" cellspacing="0" style="width:100%">
		    <tr>
			    <td>
				    <%--<a href="#" id="btnAdd" class="easyui-linkbutton" onclick="AddRec()"  data-options="iconCls:'icon-add',plain:true">添加</a>  
                     <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>               
				    <a href="#" id="btnEdit" class="easyui-linkbutton" onclick="EditRec()" data-options="iconCls:'icon-edit',plain:true">编辑</a>  
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span> 
                    <a href="#" id="btnView" class="easyui-linkbutton" onclick="ViewRec('readonly')" data-options="iconCls:'icon-view',plain:true">查阅</a>  
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>               
				    <a href="#" id="btnRemove" class="easyui-linkbutton" onclick="DeleteRec()" data-options="iconCls:'icon-remove',plain:true">删除</a> 
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>     
                    <a href="#" id="btnDR" class="easyui-linkbutton" onclick="DaoRu()" data-options="iconCls:'icon-add',plain:true">导入</a>--%>
                    <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>
                     <a href="#" class="easyui-linkbutton" onclick="SearchByMultiCondition();"  data-options="iconCls:'icon-filter',plain:true" >统计筛选</a>
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>            
				    <a href="#" id="A1" class="easyui-linkbutton" data-options="iconCls:'icon-Export',plain:true" onclick="ExportExcel('<%=ResID %>','<%=keyWordValue %>')"  >导出</a>
                    <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span> 
                    <a href="#" id="btnView" class="easyui-linkbutton" onclick="ViewRec('readonly')" data-options="iconCls:'icon-view',plain:true">查阅</a>
                    <%--<span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>  
                    <a href="#" id="A2" class="easyui-linkbutton" data-options="iconCls:'icon-Export',plain:true" onclick="PrintRec()"  >打印</a>--%>                    
			    </td>
			    <td style="padding-top:2px; text-align :right">    
                    <input type="hidden" id="<%=ResID %>_ExpWhereStr" />                 
				   <input id="<%=ResID %>_Search" class="easyui-textbox" type="text" style="width:120px"  data-options="prompt:'关键字查询'" />
                    <a href="#" class="easyui-linkbutton" onclick="SearchByKeyWord('<%=ResID %>','<%=keyWordValue %>')"  data-options="iconCls:'icon-search',plain:true" >查询</a>
                    <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>
                    <a href="#" class="easyui-linkbutton" onclick="SearchReset('<%=ResID %>','<%=keyWordValue %>')"  data-options="iconCls:'icon-search',plain:true" >取消查询</a>
                    
			    </td>
		    </tr>
	    </table>
    </div>

    <table class="easyui-datagrid" id="dg-<%=keyWordValue %>"  style="width:100%; border:none; "
			data-options="rownumbers:true,loadMsg:'请稍候,系统正在处理请求...',autoRowHeight:false,pagination:true,method:'post',idField:'ID',border: false, fitColumns:true,fit:true,toolbar:'#dlg-toolbar'"></table>

    <div id="win-<%=keyWordValue %>" style="display:none"  data-options="iconCls:'icon-note',modal:true,closed:false,minimizable:false,maximizable:false,cache:false,collapsible:false,resizable: true,loadingMessage:'正在打开.....'"></div> 
    <div id="dio-<%=keyWordValue %>" style="display:none" data-options="iconCls:'icon-note',modal:true,closed:false,minimizable:false,maximizable:false,cache:false,width:570,height:400,collapsible:false,resizable:true, loadingMessage:'正在打开.....'"></div> 
    <div id="excel-<%=keyWordValue %>" style="display:none" data-options="iconCls:'icon-search',modal:true,closed:false,minimizable:false,maximizable:false,cache:false,width:650,height:430,collapsible:false,resizable:true, loadingMessage:'正在打开.....'"></div> 
 

</body>
</html>
