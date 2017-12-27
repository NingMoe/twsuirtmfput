<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormChildrenList.aspx.cs" Inherits="Base_FormChildrenList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>显示子资源数据列表</title>    
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>     
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
    <script type="text/javascript" src="../Scripts/DataGridFieldStyle.js"></script>
</head>
<body>
    <script type="text/javascript">      
       
        var fieldValue ="[]";
        var loaded = false;
        var centerHeight = $('#content',window.parent.document).height();  
        
        var _UserID= '<%=oUserInfo.ID %>'
        var keyWordValue = '<%=keyWordValue %>';  
        var titleValue = '<%=titleValue %>'; 
        var _UserName= '<%=oUserInfo.Name %>'
        var ResID = '<%=ResID %>';//主表ResID
        var RecID='';
        var rowDataObj;//主表选中的行记录
        var sysObj ={};
        var childsysObj={};
        var title="";
        var width=0;
        var height=0;
        var url="";
        var URLTargets="";
        $(document).ready(function () {
            //获取主表Grid列集合\工具栏
            $.ajax({
                type: "POST",
                url: "../Base/Common/Ajax_Request.aspx?typeValue=GetField&keyWordValue=" + keyWordValue+"&UserID=<%=oUserInfo.ID %>&MenuResID=<%=MenuResID %>",
                success: function (fieldValueStr) { 
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    //字段集合
                    fieldValue = fieldValueStrList[0]; 
                    //for (var i = 0; i < fieldValueStrList.length; i++) {
                    //    alert(fieldValueStrList[i]);
                    //}
                    var sysValue = fieldValueStrList[1];
                    //获取添加地址、编辑地址、窗口高度、宽度、按钮权限等
                    sysObj = eval(sysValue);

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
                    //判断按钮权限
                    if(sysObj[0].IsExp==false||sysObj[0].IsExpRights==false){
                        $("#btnExport").linkbutton("disable");
                    }
                    var FootStr=sysObj[0].FootStr;
                    var isShowFoot=false;
                    if (FootStr!="") {
                        isShowFoot=true;
                    }
                    var UserDefinedToolBars = fieldValueStrList[2];
                
                    var gridHeight=0;
                    //如果没配置子列表
                    if('<%=IsChild%>'=='False'){
                        gridHeight=centerHeight - 30;
                    }else{
                        gridHeight=centerHeight-220;
                    }
                    
                    if (keyWordValue!="SW_Info_in") {
                         $("#A2").linkbutton("disable");
                    }

                    var GridUrl=encodeURI("../Base/Common/Ajax_Request.aspx?typeValue=GetDataOfPage&ResID=<%=ResID %>&keyWordValue=<%=keyWordValue %>&Authority=<%=UserDefinedSql%>&FootStr="+FootStr);
                    //动态设置，主表Grid相关属性
                    $('#dg-<%=keyWordValue %>').datagrid({
                        height: gridHeight,
                        columns: eval(fieldValue),
                        singleSelect:true,
                        fitColumns:false,
                        url:GridUrl,
                        striped:true,
                        showFooter:isShowFoot,
                        selectOnCheck:sysObj[0].IsCheckBox,
                        checkOnSelect:sysObj[0].IsCheckBox,
                        pageSize:<%=PageSize %>,
                        onClickRow: function(rowIndex,rowData){                           
                            //判断是否有子表选项卡，如果有则选中主表记录后，加载子表记录
                            if('<%=IsChild%>'=='False')return;
                            //获取当前选中的选项卡
                            var tab = $('#DPListTabs').tabs('getSelected');   
                                     
                            var ActiveTitle  = tab.panel('options').title;
                            //将主表选中行，设为全局变量
                            rowDataObj=rowData;
                           
                            //加载子资源列表 ActiveTitle选中的选项卡标题、rowDataObj选中的行记录对象
                           LoadChildrenGrid(ActiveTitle);
                        },
                        rowStyler:function(index,row){
                            if ('<%=keyWordValue %>'=='YSKGL'||'<%=keyWordValue %>'=='YSKGLForXS') {
                                if (row.收款状态=="未收完") {
                                     return "background-color:yellow";
                                }
                                if (row.收款状态=="已结清") {
                                     return "background-color:#B3EE3A";
                                }
                            }
                        }
                    }); 
                }                    
            }); 

            //tab选项卡切换事件
            $('#DPListTabs').tabs({
                border:false,
                onSelect:function(title,index){
                    //切换选项卡后，加载grid
                    LoadChildrenGrid(title);
                     
                }
            });
        });

      
        //主表Grid数据刷新方法
        function fnGridReload(keyWord,ResID) {
            if(keyWord==undefined && ResID==undefined){
                $("#dg-<%=keyWordValue %>").datagrid('reload');
            }
            else{
                $("#dg-" + keyWord + "_" + ResID).datagrid('reload');
            }
        }
        
        //关闭窗口方法，并刷新
        function closeWindow1(keyWord,ResID){
            if(keyWord==undefined && ResID==ResID){
                fnGridReload();
            }
            else{
                fnGridReload(keyWord,ResID);
            }
            //$('#win-<%=keyWordValue %>').window('close');       
            $("#DPListTabs").datagrid('reload'); 
            window.parent.ParentCloseWindow();     
        }

        //关闭窗口方法，不并刷新
        function closeWindow2(){
             window.parent.ParentCloseWindow();     
            //$('#win-<%=keyWordValue %>').window('close');       
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
          
                url+="?RecID="+getRow.ID+"&SearchType="+type;
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
                                fnGridReload();
                            }else {
                                alert("删除失败！");
                            }
                        }                 
                    });
                }
            }
        }

        //Excel导出功能
        function ExportExcel(_resId,_keyValue){
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
//            $('#excel-<%=keyWordValue %>').dialog({               
//                title : titleValue+"-导出Excel",                           
//                left: centerHeight/3,
//                top: 20,                
//                buttons:[{
//                    iconCls:'icon-cancel',
//                    text:'清空',
//                    handler:function(){
//                        $("#fmExcelItem")[0].reset();
//                        unCheckAllBox();
//                    }
//                },{
//                    iconCls:'icon-Export',
//                    text:'导出',
//                    id:'btnExcel',
//                    handler:function(){ 
//                        //获取函数返回的导出筛选条件；ExcelSearchItem为查询页面（SearchControls.aspx）中元素ID
//                        var searchs=CondtionBuild("ExcelSearchItem"); 
//                        
//                        //导出时获取要导出的列
//                        var chk_value =[]; 
////                        $('input[name="chkColumn"]:checked').each(function(){ 
////                            chk_value.push($(this).val()); 
////                        }); 
////                        if(chk_value.length==0){
////                            alert("请选择要导出的字段列");
////                            return;
////                        }
//                        //禁用导出按钮
//                        $("#btnExcel").linkbutton('disable');
//                       
//                        jQuery.messager.show({ 
//                            title:'导出提示', 
//                            msg:'正在加载Excel数据，请您耐心等待....', 
//                            timeout:5000, 
//                            showType:'slide'
//                        }); 
//                        //关闭窗口
//                        $('#excel-<%=keyWordValue %>').dialog('close');
//                        //参数较多，先调用后台方法，存储参数，然后在回调导出Excel方法
//                        $.post("../Base/Common/Ajax_Request.aspx?typeValue=saveExcelData", {conditionStr:searchs,columnStr:chk_value.join(",")}, function(data){
//                            var dataObj=eval("("+data+")");   
//                            if(dataObj.success){
//                                // 回调        
//                                window.location.href="../Base/Common/Ajax_Request.aspx?typeValue=Excel&ExcelfileName="+titleValue+"&cookeId="+dataObj.key+"&resId=<%=ResID %>&KeyWord=<%=keyWordValue %>&Authority=<%=UserDefinedSql%>";
//                            }
//                        });
//                    }
//                }]
//            });
//            //动态加载内容
//            var _url="CommonControls/ExportExcel.aspx?resId=<%=ResID %>&KeyWord=<%=keyWordValue %>";
//             
//            $('#excel-<%=keyWordValue %>').dialog('refresh',_url);    
        
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
            var widths=_width==(undefined||0)? 800 : _width;
            var heights=_height==(undefined||0)? 450 : _height;       
            
             window.parent.fnParentFormListDialog(_url,widths,heights,_title,"dg-<%=keyWordValue %>");        
//            $('#win-<%=keyWordValue %>').window(  
//                {  
//                    title : _title,  
//                    width :widths,  
//                    height :heights,                   
//                    content : '<iframe scrolling="no" frameborder="0"  src="'+ _url+ '" style="width:100%;height:98%;"></iframe>',                  
//                    left: centerHeight/4,
//                    top: 20
//                }); 
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

        //高级搜索加子表查询
        function SearchByMultiCondition(childsysObj){
            if(childsysObj != undefined){
                var Title=childsysObj[0].ShowTitle;//标题
                var KeyWord = childsysObj[0].ENTableName;//子表参数关键字
                var ResValue = childsysObj[0].ResID;//子表资源ID
                if(KeyWord!=undefined){
                    titleValue=Title;
                }
            }
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
                        if(ResValue==undefined && KeyWord==undefined){
                            //执行查询
                            $('#dg-<%=keyWordValue %>').datagrid('load',{
                                FilterRules:searchCondTion
                            });
                            //关闭窗口
                            $('#dio-<%=keyWordValue %>').dialog('close');
                        }
                        else{ //子表查询返回值
                            //执行查询
                            $("#dg-" + KeyWord + "_" + ResValue).datagrid('load',{
                                FilterRules:searchCondTion
                            });
                            //关闭窗口
                            $('#dio-<%=keyWordValue %>').dialog('close');
                        }
                    }
                }]
            });
            //动态加载内容，_ResValue_ 子表资源ID，_ActiveKey_ 子表参数关键值
            if(ResValue==undefined && KeyWord==undefined){
                var _url="CommonControls/SearchControls.aspx?resId=<%=ResID %>&KeyWord=<%=keyWordValue %>";
                $('#dio-<%=keyWordValue %>').dialog('refresh',_url);
            }
            else{   //子表资源进行查询
                var _url="CommonControls/SearchControls.aspx?resId="+ResValue+"&KeyWord="+KeyWord;
                $('#dio-<%=keyWordValue %>').dialog('refresh',_url);
            }
        }

        /******************************************子表Grid数据、列加载方法************************************************/
        function LoadChildrenGrid(_ActiveTitle){

            //获取当前选中的选项卡key
            var ActiveKey=$("#"+ResID+_ActiveTitle).attr("key");
            //获取子表Grid列集合\工具栏
            $.ajax({
                type: "POST",
                url: "../Base/Common/Ajax_Request.aspx?typeValue=GetField&keyWordValue=" + ActiveKey+"&MenuResID=<%=MenuResID %>",
                success: function (fieldValueStr) { 
                    var fieldValueStrList = fieldValueStr.split("[#]");
                    //字段集合
                    var childfieldValue = fieldValueStrList[0]; 
                  
                    var sysValue = fieldValueStrList[1];
                   
                    //获取添加地址、编辑地址、窗口高度、宽度、按钮权限等
                    childsysObj = eval(sysValue);

                    var UserDefinedToolBars = fieldValueStrList[2];

                    //获取当前选中子资源的ResId
                    var ResValue=childsysObj[0].ResID;
                     var FootStr=childsysObj[0].FootStr;
                    var isShowFoot=false;
                    if (FootStr!="") {
                        isShowFoot=true;
                    }
                    //获取主子表关联字段
                    var condtion=$("#ChildGridCondition"+ResValue).val();
                   
                    if(condtion==""){
                        alert("未设置主子表关联字段！");
                        return;
                    }

                    //拆分主子表关联字段
                    var str1=condtion.substring(0,condtion.indexOf("="));//主表字段
                    var str2=condtion.substring(condtion.indexOf("=")+1);//主表字段
                    //获取选中行的关联字段值，将关联字段替换为选中行的值
                    $.each(rowDataObj,function(idx,item){                       
                        if(idx==str1 && item != null ){ 
                            condtion=str2+"="+item;
                        }                           
                    });
                    var ChildAdd=false;
                    var ChildEdit=false;
                    var ChildDel=false;
                    //判断按钮权限
                    if(childsysObj[0].AddUrl==""||childsysObj[0].IsAdd==false||childsysObj[0].IsAddRights==false){
                        ChildAdd=true; //$("#childbtnAdd").linkbutton("disable");
                    }
                    //判断按钮权限
                    if(childsysObj[0].EidtUrl==""||childsysObj[0].IsUpdate==false||childsysObj[0].IsUpdateRights==false){
                        ChildEdit=true; //$("#childbtnEdit").linkbutton("disable");
                    }
                    //判断按钮权限
                    if(childsysObj[0].IsDelete==false||childsysObj[0].IsDeleteRights==false){
                        ChildDel=true; //$("#childbtnRemove").linkbutton("disable");
                    } 
                    
                    //动态设置，子表表Grid相关属性以及获取数据Url
                    var getDataUrl=encodeURI("../Base/Common/Ajax_Request.aspx?typeValue=GetDataOfPage&ResID="+ResValue+"&keyWordValue="+ActiveKey+"&FootStr="+FootStr+"&RelationCondtion="+condtion);
                    $("#dg-"+ActiveKey+"_"+ResValue).datagrid({showFooter:isShowFoot, striped:true, height: 168, columns: eval(childfieldValue),pagination:true, singleSelect:true,pageSize:<%=PageSize %>,url:getDataUrl });
                    
                    //获取Grid分页对象，给分页对象添加组件
                    var pager = $("#dg-"+ActiveKey+"_"+ResValue).datagrid('getPager');
                    pager.pagination({
                        buttons:[{
                            text:'查询',
                            iconCls:'icon-search',
                            handler:function(){
                                SearchByMultiCondition(childsysObj);
                            }
                        },'-',{
                            iconCls:'icon-add',
                            text:'添加',
                            id:'childbtnAdd',
                            disabled:ChildAdd,
                            handler:function(){
                                AddRecID(childsysObj,ResValue);
                            }
                        },'-',{
                            iconCls:'icon-edit',
                            text:'编辑',
                            id:'childbtnEdit',
                            disabled:ChildEdit,
                            handler:function(){
                                EditRecID(childsysObj);
                            }
                        },'-',{
                            iconCls:'icon-remove',
                            text:'删除',
                            id:'childbtnRemove',
                            disabled:ChildDel,
                            handler:function(){
                                DeleteRecID(ActiveKey,ResValue);
                            }
                        },'-']
                    });
                }
            });         
        }    
 
        //子表添加方法
        function AddRecID(childsysObj,childResValue){
            title=childsysObj[0].ShowTitle;//标题
            width=childsysObj[0].DialogWidth;//窗口宽度
            height=childsysObj[0].DialogHeight;//高度
            url=childsysObj[0].AddUrl;//添加地址
            URLTargets=childsysObj[0].URLTarget;//链接打开方式 
            //获取主子表关联字段
            var condtion=$("#ChildGridCondition"+childResValue).val();
            var condtionquote=$("#ChildGridForStr"+childResValue).val(); //获取台帐子表引用字段

            if(condtionquote==""){
                alert("未设置台帐子表引用字段！");
                return;
            }
            var condtionquotes=condtionquote.split(",")
            var inner="";
            var inner2="";
            var innerStr="";
            for(i=0;i<condtionquotes.length;i++){
                inner=condtionquotes[i].substring(0,condtionquotes[i].indexOf("="))  //取主表字段值
                inner2=condtionquotes[i].substring(condtionquotes[i].indexOf("=")+1) //取子表字段值
                //获取主表选中行的关联字段值，将关联字段替换为选中行的值
                $.each(rowDataObj,function(idx,item){ 
                    if(idx==inner){
                        innerStr+=inner2+'='+item+"&"
                        //判断添加路径中是否已经存在参数,并将选中主表记录的，主子表关联字段值，传递到子表添加页面中
                    }                           
                });
            }
            if(url.indexOf("?")>=0){
                url+="&"+innerStr.substring(0,innerStr.length-1);
            }else{
                url+="?"+innerStr.substring(0,innerStr.length-1);
            }

            //在添加页面，通过以下方式获取URL中的参数，并赋值给对应的控件
            //function GetRequest() {
            //    var url = decodeURI(location.search);
            //    if (url.indexOf("?") != -1) {
            //        var str = url.substr(1);
            //        var strs = str.split("&");
            //        for (var i = 0; i < strs.length; i++) {
            //            var parName = strs[i].split("=")[0];//参数名称
            //            var parValue = strs[i].split("=")[1];//参数值 
            //            //根据参数名称和子表Resid，找对应的控件,如果控件存在则把当前参数值赋给此控件
            //            if ($("#<%=ResID %>_" + parName).length > 0) {
            //                if (parValue != "undefined" && parValue != "null")
            //                    $("#<%=ResID %>_" + parName).textbox("setValue", parValue);
            //            }
            //        }
            //    }
            //}
            url="Base/"+url;
            if(URLTargets=="_ChildFrm")
                showWindow(title,url,width,height);
            else
                window.open (url, 'newwindow');      
        }

        //子表修改方法
        function EditRecID(childsysObj){
            var keyword = childsysObj[0].ENTableName;//子表参数关键字
            var ResValue = childsysObj[0].ResID;//子表资源ID
            var getRow=$("#dg-" + keyword + "_" + ResValue).datagrid('getSelected');
            if (getRow==null){
                alert("请选中要编辑的记录");
                return;
            }else{
                title=childsysObj[0].ShowTitle;//标题
                width=childsysObj[0].DialogWidth;//窗口宽度
                height=childsysObj[0].DialogHeight;//高度
                url=childsysObj[0].AddUrl;//添加地址
                URLTargets=childsysObj[0].URLTarget;//链接打开方式
                url+="?keyWordValue="+keyword+"&RecID="+getRow.ID;
                url="Base/"+url;
                if(URLTargets=="_ChildFrm")
                    showWindow(title,url,width,height);
                else
                    window.open (url, 'newwindow');                             
            }
        }

        //子表删除按钮方法,_ActiveKey 子资源参数关键字，_ResValue 子资源ID
        function DeleteRecID(_ActiveKey,_ResValue){
            var getRow=$("#dg-" + _ActiveKey + "_" + _ResValue).datagrid('getSelected');    
            if (getRow==null){
                alert("请选中要删除的记录");
                return;
            }
            else{              
                if (confirm("确定要删除该条记录吗？")) {
                    $.ajax({
                        type: "POST",
                        url: "../Base/Common/Ajax_Request.aspx?typeValue=DeleteRow&ResID="+_ResValue+"&RecID=" + getRow.ID,
                        success: function (strJSON) { 
                            var obj = eval("(" + strJSON + ")");
                            if (obj.success || obj.success == "true") {
                                alert("删除成功！");
                                fnGridReload(_ActiveKey,_ResValue);
                            }else {
                                alert("删除失败！");
                            }
                        }
                    });
                }
            }
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
            if ("<%=keyWordValue %>"=="SW_Info_in") {
                printURL="SolidWaste/PrintInfo.aspx?RecID="+getRow.ID
            }
            if (printURL!="") {
                 window.open(printURL, 'newwindow', 'height=600,width=900, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no');                        
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
				    <a href="#" id="btnAdd" class="easyui-linkbutton" onclick="AddRec()"  data-options="iconCls:'icon-add',plain:true">添加</a>  
                     <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>               
				    <a href="#" id="btnEdit" class="easyui-linkbutton" onclick="EditRec()" data-options="iconCls:'icon-edit',plain:true">编辑</a>  
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span> 
                    <a href="#" id="btnView" class="easyui-linkbutton" onclick="ViewRec('readonly')" data-options="iconCls:'icon-view',plain:true">查阅</a>  
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>               
				    <a href="#" id="btnRemove" class="easyui-linkbutton" onclick="DeleteRec()" data-options="iconCls:'icon-remove',plain:true">删除</a> 
                      <%--<span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>               
                    <a href="#" id="btnDR" class="easyui-linkbutton" onclick="DaoRu()" data-options="iconCls:'icon-add',plain:true">导入</a> 
                      <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>  
				    <a href="#" id="btnExport" class="easyui-linkbutton" data-options="iconCls:'icon-Export',plain:true" onclick="ExportExcel('<%=ResID %>','<%=keyWordValue %>')" >导出</a>        --%>             
			        <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>  
                    <a href="#" id="A2" class="easyui-linkbutton" data-options="iconCls:'icon-Export',plain:true" onclick="PrintRec()"  >打印条码</a>                      
                </td>
			    <td style="padding-top:2px; text-align :right"> 
                    <input type="hidden" id="<%=ResID %>_ExpWhereStr" />
				   <input id="<%=ResID %>_Search" class="easyui-textbox" type="text" style="width:120px"  data-options="prompt:'关键字查询'" />
                    <a href="#" class="easyui-linkbutton" onclick="SearchByKeyWord('<%=ResID %>','<%=keyWordValue %>')"  data-options="iconCls:'icon-search',plain:true" >查询</a>
                    <span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>
                     <a href="#" class="easyui-linkbutton" onclick="SearchReset('<%=ResID %>','<%=keyWordValue %>')"  data-options="iconCls:'icon-search',plain:true" >取消查询</a>
                    <%--<span class="datagrid-btn-separator" style="vertical-align: middle;display:inline-block;float:none"></span>
                     <a href="#" class="easyui-linkbutton" onclick="SearchByMultiCondition();"  data-options="iconCls:'icon-filter',plain:true" >高级查询</a>--%>
			    </td>
		    </tr>
	    </table>
    </div>

    <!------主表数据列表----->
    <table class="easyui-datagrid" id="dg-<%=keyWordValue %>"  style="width:100%; border:none; "
			data-options="rownumbers:true,loadMsg:'请稍候,系统正在处理请求...',autoRowHeight:false,pagination:true,method:'post',idField:'ID',border: true,fitColumns:true,toolbar:'#dlg-toolbar'"></table>

    <!------子表数据列表----->
    <div id="DPListTabs" class="easyui-tabs" collapsible="true" style="width:100%;height:200px">
         <asp:Repeater ID="RepTabList" runat="server">
            <ItemTemplate>
                <div key="<%#Eval("子表关键字") %>" data-child="<%#Eval("子表关键字") %>_<%#Eval("子表资源ID") %>" title="<%#Eval("显示Title") %>" id="<%=ResID %><%#Eval("显示Title") %>" >
                     <table class="easyui-datagrid" id="dg-<%#Eval("子表关键字") %>_<%#Eval("子表资源ID") %>"  style="width:100%; border:none; " data-options="rownumbers:true,loadMsg:'请稍候,系统正在处理请求...',autoRowHeight:false,method:'post',idField:'ID',border: false, fitColumns:true" ></table>
                     <input type="hidden" value="<%#Eval("台帐主子表关联字段") %>"  data-fiter="<%#Eval("初始筛选条件") %>" id='ChildGridCondition<%#Eval("子表资源ID") %>' />
                     <input type="hidden" value="<%#Eval("台帐子表引用字段") %>"  id='ChildGridForStr<%#Eval("子表资源ID") %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>         
    </div>
    <div id="excel-<%=keyWordValue %>" style="display:none" data-options="iconCls:'icon-search',modal:true,closed:false,minimizable:false,maximizable:false,cache:false,width:650,height:430,collapsible:false,resizable:true, loadingMessage:'正在打开.....'"></div> 
    <div id="win-<%=keyWordValue %>" style="display:none"  data-options="iconCls:'icon-note',modal:true,closed:false,minimizable:false,maximizable:false,cache:false,collapsible:false,resizable: true,loadingMessage:'正在打开.....'"></div> 
    <div id="dio-<%=keyWordValue %>" style="display:none" data-options="iconCls:'icon-note',modal:true,closed:false,minimizable:false,maximizable:false,cache:false,width:570,height:400,collapsible:false,resizable:true, loadingMessage:'正在打开.....'"></div> 

</body>
</html>
