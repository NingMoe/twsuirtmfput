<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.Migrated_RecordEdit" validateRequest="false" CodeFile="RecordEdit.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>记录编辑</title>
		<meta http-equiv="Pragma" content="no-cache">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
			<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
			<SCRIPT language="JavaScript" src="/cmsweb/script/base.js"></SCRIPT>
			<script language="JavaScript" src="/cmsweb/script/poslib.js"></script>
			<script language="JavaScript" src="/cmsweb/script/menu4.js"></script>
			<script language="JavaScript" src="/cmsweb/script/scrollbutton.js"></script>
			<SCRIPT language="JavaScript" src="/cmsweb/script/CmsScript.js"></SCRIPT>
			<SCRIPT language="JavaScript" src="/cmsweb/script/Valid.js"></SCRIPT>
			<script language="javascript">
			var IsChanged = false;
			//退出，需提醒是否保存
			function DoExit(){
				if( IsChanged && !document.all("lnkSave").disabled)
				{
					if(confirm("是否需要保存？"))
					{ //窗体设计未改变，退出不保存
						if( CheckValue(self.document.forms(0)))
						{
							openProgress();
							document.all("CommandName").value = "save";	
						}	
						else
						{
							return;
						}
					}
					else
					{
						document.all("CommandName").value = "exit";		
					}
				}
				else
				{
					document.all("CommandName").value = "exit";		
				}	
				self.document.forms(0).submit();
			}
<!--
//打开新窗体至指定URL，之前需确认
function MenuItemEntryForPopup(MenuType, MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, MNUTARGET, NoUrlParam, left, top, width, height, menubar, toolbar, location, resizable, scrollbars, titlebar, fullscreen){
	var strUrl = MenuItemGetUrl(MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, NoUrlParam);
	if (strUrl == "NOACTION"){
		return;
	}
	if (MenuType == "POPUP"){
		window.open(strUrl, "_blank", "left="+left+",top="+top+",height="+height+",width="+width+",status=yes,toolbar="+toolbar+",menubar="+menubar+",location="+location+",resizable=" +resizable+",scrollbars="+scrollbars+",titlebar="+titlebar+",fullscreen="+fullscreen);
	}else if (MenuType == "DIALOG"){
		window.showModalDialog(strUrl, "", "dialogHeight:" + height + "px; dialogWidth:" + width + "px; center;yes"); 
	}else if (MenuType == "DIALOGREFRESH"){
		window.showModalDialog(strUrl, "", "dialogHeight:" + height + "px; dialogWidth:" + width + "px; center;yes"); 
		//Form1.submit(); //刷新当前页面
	}else if (MenuType == "POPUPDOC"){
		try{
			formDownFile.action=strUrl;
			formDownFile.submit();	
		}catch(e){
		}
	}
}

function MenuItemGetUrl(MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, NoUrlParam){
	if (confirmMsg != ""){
		if (confirm(confirmMsg) != true){
			return "NOACTION";
		}
	}
	if (NoUrlParam == 1){
		return url;
	}

	//获取当前选中的记录ID
	var strRecID = "";
	var strHostResID = "<%= CmsFrmContentBase.GetHostResIDInSession(Session, Response)%>";
	var strHostRecID = "<%= CmsFrmContentBase.GetHostRecIDInSession(Request, Session, Response)%>";
	if (MNURESLOCATE == "1"){ //MNURESLOCATE: =1:仅在主表出现；=2:仅在子表出现；=3：仅在主表和子表出现
		//获取当前Session或Ctrl键选中的记录
		strRecID = Form1.RECID.value;
		if (strRecID == ""){
			strRecID = strHostRecID;
		}
	}else if (MNURESLOCATE == "2"){
		strRecID = Form1.RECID2.value;
	}else{
		strRecID = "";
	}

	//校验是否需要选择记录
	if (MNUSELREC == "1" && strRecID == "" ){
		alert('<%=CmsMessage.GetMsg(CmsPass, "RECORD_OPARATE") %>');
		return "NOACTION";
	}

	//组装URL参数
	var strUrl;
	if (url.indexOf("?") > 0){
		strUrl = url + "&timeid=" + Math.round(Math.random() * 10000000000);
	}else{
		strUrl = url + "?timeid=" + Math.round(Math.random() * 10000000000);
	}
	if (IsReportService == 1){
		strUrl = strUrl + "&" + strParamRecID;
	}else{
		strUrl = strUrl + "MenuSection=" + MenuSection + "&MenuKey=" + MenuKey + "&MNURESLOCATE=" + MNURESLOCATE + "&cmsaction=" + cmscmd + "&mnuformresid=" + frmresid + "&mnuhostresid=" + strHostResID + "&mnuhostrecid=" + strHostRecID + "&mnuresid=" + ResID + "&mnuformname=" + escape(frmname);
	}
    if (MNUSELREC == "1"){
        strUrl += "&mnurecid=" + strRecID; //URL需要有记录ID
    }
	return strUrl;
}
var pb_pos=0; var pb_dir=2; var pb_len=0;
var pb_animate = setInterval(progressbar_animate,20);
function progressbar_animate() {
	var elem = document.getElementById('progressbar_progress'); 
	if(elem != null) { 
		if (pb_pos==0) pb_len += pb_dir; 
		if (pb_len>32 || pb_pos>179) pb_pos += pb_dir; 
		if (pb_pos>179) pb_len -= pb_dir; 
		if (pb_pos>179 && pb_len==0) pb_pos=0; 
		elem.style.left = pb_pos; 
		elem.style.width = pb_len;
	}
}
function remove_progressbar_loading() {
	this.clearInterval(pb_animate);
	var targelem = document.getElementById('progressbar_loader_container');
	targelem.style.display='none';
	targelem.style.visibility='hidden';
}
function fnSub(){

   if(document.getElementById("lnkSave").disabled==false){
        document.getElementById('progressbar_loader_container').style.display="";
    }

}
-->

			</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
		    <div runat="server" id="progressbar_loader_container" style="display:none;text-align:center; position:absolute; top:40%; width:100%; left: 0;"><div id="progressbar_loader" style="font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:230px; border:1px solid #5a667b; text-align:left; z-index:2;"><div align="center">正在提交，请不要离开！……</div><div id="progressbar_loader_bg" style="background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:213px; font-size:1px; "><div id="progressbar_progress" style="height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#77A9E0;"> </div></div></div></div>
			<input id="CommandName" type="hidden" name="CommandName">
			<TABLE style="PADDING-LEFT: 4px; PADDING-TOP: 1px; " cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE class="toolbar_table" cellSpacing="0" border="0">
							<TR>
								<TD width="8"></TD>
								<TD vAlign="bottom" noWrap align="left" width="55"><asp:image id="Image1" runat="server" ImageUrl="/cmsweb/images/imagebuttons/save.gif"></asp:image>&nbsp;<asp:linkbutton OnClientClick="fnSub()"   id="lnkSave" Runat="server">保存</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
								</TD>
								<TD noWrap align="left" width="55"><asp:image id="Image2" runat="server" ImageUrl="/cmsweb/images/imagebuttons/write.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnNew" Runat="server">新建</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
								</TD>
								<TD noWrap align="left" width="55"><asp:image id="Image3" runat="server" ImageUrl="/cmsweb/images/imagebuttons/exit.gif"></asp:image>&nbsp;<asp:linkbutton id="lnkExit" Runat="server" Visible="False">退出</asp:linkbutton><asp:hyperlink id="hyExit" runat="server" NavigateUrl="#" onclick='DoExit();' style="CURSOR: hand" >退出</asp:hyperlink></TD>
								<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
								</TD>
								<TD noWrap align="left" width="55"><asp:image id="Image4" runat="server" ImageUrl="/cmsweb/images/imagebuttons/record_print.gif"></asp:image>&nbsp;<asp:Label ID="lblPrint" runat="server"></asp:Label></TD>
								<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
								</TD>
								<TD noWrap align="left" width="80"><asp:image id="imgExtension" runat="server" ImageUrl="/cmsweb/images/titleicon/associated2.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnExtension" Runat="server">扩展功能</asp:linkbutton></TD>
								<TD align="right">&nbsp;
									<asp:label id="lblHeader" runat="server"></asp:label><asp:label id="lblHeaderAction1" runat="server" ForeColor="Red" ></asp:label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>  
			</TABLE> 
		</form>
	</body>
</HTML>


<script language="javascript">
    function openPrint()
    {
        window.open('/cmsweb/cmshost/RecordPrintSimple.aspx?timeid=&MenuSection=MENU_RECORD&MenuKey=MENU_RECPRINT&MNURESLOCATE=1&cmsaction=&mnuformresid=&mnuhostresid=<%=VLng("PAGE_HOSTRESID").ToString %>&mnuhostrecid=<%=VLng("PAGE_HOSTRECID").ToString %>&mnurecid=<%= VLng("PAGE_RECID").ToString %>&mnuresid=<%=VLng("PAGE_RESID").ToString %>&mnuformname=');
    }
</script>