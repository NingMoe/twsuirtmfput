<%@ Import Namespace="NetReusables"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RecordEditRelPopup" validateRequest="false" CodeFile="RecordEditRelPopup.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>关联表记录编辑</title>
		<meta http-equiv="Pragma" content="no-cache">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="/cmsweb/script/base.js"></SCRIPT>
		<script language="JavaScript" src="/cmsweb/script/poslib.js"></script>
		<script language="JavaScript" src="/cmsweb/script/menu4.js"></script>
		<script language="JavaScript" src="/cmsweb/script/scrollbutton.js"></script>
		<SCRIPT language="JavaScript" src="/cmsweb/script/CmsScript.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="/cmsweb/script/Valid.js"></SCRIPT>
		<script language="javascript">
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
		alert("当前操作需要先选择有效的记录。");
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
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="PADDING-LEFT: 4px; PADDING-TOP: 1px" cellSpacing="0" cellPadding="0" width="774"
				border="0">
				<!--主表数据-->
				<tr>
					<td>
						<TABLE class="toolbar_table" cellSpacing="0" border="0">
							<TR>
								<TD width="8"></TD>
								<TD noWrap align="left" width="60"><asp:image id="Image1" runat="server" ImageUrl="/cmsweb/images/imagebuttons/save.gif"></asp:image>&nbsp;<asp:linkbutton id="lnkSave" Runat="server">保存</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="60"><asp:image id="Image2" runat="server" ImageUrl="/cmsweb/images/imagebuttons/write.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnNew" Runat="server">新建</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="90"><asp:image id="Image3" runat="server" ImageUrl="/cmsweb/images/imagebuttons/write.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnSaveNew" Runat="server">保存并新建</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="60"><asp:image id="Image4" runat="server" ImageUrl="/cmsweb/images/imagebuttons/exit.gif"></asp:image>&nbsp;<asp:linkbutton id="lnkExit" Runat="server">退出</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="55"><asp:image id="Image5" runat="server" ImageUrl="/cmsweb/images/imagebuttons/record_print.gif"></asp:image>&nbsp;<asp:Label ID="lblPrint" runat="server"></asp:Label></TD>
								<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
								</TD>
								<TD noWrap align="left" width="80"><asp:image id="imgExtension" runat="server" ImageUrl="/cmsweb/images/titleicon/associated2.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnExtension" Runat="server">扩展功能</asp:linkbutton></TD>
								<TD align="right">&nbsp;
									<asp:Label id="lblHeader" runat="server"></asp:Label>
									<asp:Label id="lblHeaderAction1" runat="server" ForeColor="Red"></asp:Label></TD>
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