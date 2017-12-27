<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OfficeView.aspx.vb" Inherits="Unionsoft.Workflow.Web.cmsdocument_OfficeView" %> 

<html>
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        body
        { 
            margin: 0px;
            overflow:auto;
        	
	        /*font: Times;*/
            font-size:12px;
        }
    </style>
    <script language="javascript">
    function Load(){
  try{
    //以下属性必须设置，实始化iWebOffice
    webform.WebOffice.WebUrl="<%=mServerUrl%>";             //WebUrl:系统服务器路径，与服务器文件交互操作，如保存、打开文档，重要文件
    webform.WebOffice.RecordID="<%=DocumentID%>";            //RecordID:本文档记录编号
    webform.WebOffice.Template="<%=mTemplate%>";            //Template:模板编号
    webform.WebOffice.FileName="<%=mFileName%>";            //FileName:文档名称
    webform.WebOffice.FileType="<%=mFileType%>";            //FileType:文档类型  .doc  .xls  .wps
    webform.WebOffice.UserName="<%=mUserName%>";            //UserName:操作用户名，痕迹保留需要
    webform.WebOffice.EditType=3;                           //EditType:编辑类型  方式一、方式二  <参考技术文档>
                                                            //第一位可以为0,1,2,3 其中:0不可编辑;1可以编辑,无痕迹;2可以编辑,有痕迹,不能修订;3可以编辑,有痕迹,能修订；
                                                            //第二位可以为0,1 其中:0不可批注,1可以批注。可以参考iWebOffice2003的EditType属性，详细参考技术白皮书
    webform.WebOffice.MaxFileSize = 4 * 1024;               //最大的文档大小控制，默认是8M，现在设置成4M。
    webform.WebOffice.Language="CH";					              //Language:多语言支持显示选择   CH简体 TW繁体 EN英文
    webform.WebOffice.ShowWindow = true;                     //控制显示打开或保存文档的进度窗口，默认不显示
    webform.WebOffice.Print="0";                            //	禁止打印
    webform.WebOffice.ShowMenu="2";                         //控制整体菜单显示
    webform.WebOffice.ShowToolBar=1;                        //设置是否显示整个控件工具栏，包括OFFICE的工具栏。0显示    
    //以下为自定义菜单↓
//    webform.WebOffice.AppendMenu("1","打开本地文件(&L)");
//    webform.WebOffice.AppendMenu("2","保存本地文件(&S)");
//    webform.WebOffice.AppendMenu("3","保存远程文件(&U)");
//    webform.WebOffice.AppendMenu("4","-");
//    webform.WebOffice.AppendMenu("5","签名印章(&Q)");
//    webform.WebOffice.AppendMenu("6","验证签章(&Y)");
//    webform.WebOffice.AppendMenu("7","-");
//    webform.WebOffice.AppendMenu("8","保存版本(&B)");
//    webform.WebOffice.AppendMenu("9","打开版本(&D)");
//    webform.WebOffice.AppendMenu("10","-");
//    webform.WebOffice.AppendMenu("11","保存并退出(&E)");
//    webform.WebOffice.AppendMenu("12","-");
//    webform.WebOffice.AppendMenu("13","打印文档(&P)");
    webform.WebOffice.Office2007Ribbon = 0;
    //以上为自定义菜单↑
    webform.WebOffice.DisableMenu("宏(&M);选项(&O)...");    //禁止某个（些）菜单项

   // WebSetRibbonUIXML();                                  //控制OFFICE2007的选项卡显示
    webform.WebOffice.WebOpen();                            //打开该文档    交互OfficeServer  调出文档OPTION="LOADFILE"    调出模板OPTION="LOADTEMPLATE"     <参考技术文档>
    //StatusMsg(webform.WebOffice.Status);                    //状态信息
  }
  catch(e){
    alert(e.description);                                   //显示出错误信息
  }
}
//作用：显示操作状态
function StatusMsg(mString){
    window.status = mString;
}
//作用：退出iWebOffice
function WebSetRibbonUIXML(){
  webform.WebOffice.RibbonUIXML = '' +
  '<customUI xmlns="http://schemas.microsoft.com/office/2006/01/customui">' +
  '  <ribbon startFromScratch="true">'+                    //不显示所有选项卡控制 false显示选项卡；true不显示选项卡
  '    <tabs>'+
  '      <tab idMso="TabReviewWord" visible="false">' +     //关闭视图工具栏
  '      </tab>'+
  '      <tab idMso="TabInsert" visible="false">' +         //关闭插入工具栏
  '      </tab>'+
  '      <tab idMso="TabHome" visible="false">' +           //关闭开始工具栏
  '      </tab>'+
  '    </tabs>' +
  '  </ribbon>' +
  '</customUI>';

/*
    最常用的内置选项卡名称
    选项卡名称      idMso（Excel）      idMso（Word）       idMso（Access）
    开始            TabHome             TabHome             TabHomeAccess
    插入            TabInsert           TabInsert           （none）
    页面布局        TabPageLayoutExcel  TabPageLayoutWord   （none）
    公式            TabFormulas         （none）            （none）
    数据            TabData             （none）            （none）
    视图            TabReview           TabReviewWord       （none）
    创建            （none）            （none）            TabCreate
    外部数据        （none）            （none）            TabExternalData
    数据库工具      （none）            （none）            TabDatabaseTools
*/

/*
    iWebOffice控件的RibbonUIXML属性，是基于OFFICE2007的RibbonX的应用。关于RibbonX的相关资料，需要自己另行查询。
*/
}
function UnLoad(){
  try{
    if (!webform.WebOffice.WebClose()){
      StatusMsg(webform.WebOffice.Status);
    }
    else{
      StatusMsg("关闭文档...");
    }
  }
  catch(e){
    alert(e.description);
  }
}
 
</script>
</head>
<body onload="Load()" onunload="UnLoad()">
    <form id="webform" runat="server">
    <div>
      <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
        <tr>
          <td bgcolor="menu" height="98%" valign="top">
            <!--调用iWebPicture，注意版本号，可用于升级-->
		    <script src="../script/iWebOffice2003.js"></script>
          </td>
        </tr> 
      </table>  
    </div>
    </form>
</body>
</html>

 