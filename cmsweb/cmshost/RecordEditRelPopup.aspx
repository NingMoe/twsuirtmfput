<%@ Import Namespace="NetReusables"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RecordEditRelPopup" validateRequest="false" CodeFile="RecordEditRelPopup.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>�������¼�༭</title>
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
//���´�����ָ��URL��֮ǰ��ȷ��
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
		//Form1.submit(); //ˢ�µ�ǰҳ��
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

	//��ȡ��ǰѡ�еļ�¼ID
	var strRecID = "";
	var strHostResID = "<%= CmsFrmContentBase.GetHostResIDInSession(Session, Response)%>";
	var strHostRecID = "<%= CmsFrmContentBase.GetHostRecIDInSession(Request, Session, Response)%>";
	if (MNURESLOCATE == "1"){ //MNURESLOCATE: =1:����������֣�=2:�����ӱ���֣�=3������������ӱ����
		//��ȡ��ǰSession��Ctrl��ѡ�еļ�¼
		strRecID = Form1.RECID.value;
		if (strRecID == ""){
			strRecID = strHostRecID;
		}
	}else if (MNURESLOCATE == "2"){
		strRecID = Form1.RECID2.value;
	}else{
		strRecID = "";
	}

	//У���Ƿ���Ҫѡ���¼
	if (MNUSELREC == "1" && strRecID == "" ){
		alert("��ǰ������Ҫ��ѡ����Ч�ļ�¼��");
		return "NOACTION";
	}

	//��װURL����
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
        strUrl += "&mnurecid=" + strRecID; //URL��Ҫ�м�¼ID
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
				<!--��������-->
				<tr>
					<td>
						<TABLE class="toolbar_table" cellSpacing="0" border="0">
							<TR>
								<TD width="8"></TD>
								<TD noWrap align="left" width="60"><asp:image id="Image1" runat="server" ImageUrl="/cmsweb/images/imagebuttons/save.gif"></asp:image>&nbsp;<asp:linkbutton id="lnkSave" Runat="server">����</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="60"><asp:image id="Image2" runat="server" ImageUrl="/cmsweb/images/imagebuttons/write.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnNew" Runat="server">�½�</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="90"><asp:image id="Image3" runat="server" ImageUrl="/cmsweb/images/imagebuttons/write.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnSaveNew" Runat="server">���沢�½�</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="60"><asp:image id="Image4" runat="server" ImageUrl="/cmsweb/images/imagebuttons/exit.gif"></asp:image>&nbsp;<asp:linkbutton id="lnkExit" Runat="server">�˳�</asp:linkbutton></TD>
								<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
								</TD>
								<TD noWrap align="left" width="55"><asp:image id="Image5" runat="server" ImageUrl="/cmsweb/images/imagebuttons/record_print.gif"></asp:image>&nbsp;<asp:Label ID="lblPrint" runat="server"></asp:Label></TD>
								<TD align="center" width="8"><IMG height="18" src="/cmsweb/images/icons/saprator.gif" width="2" align="absMiddle">
								</TD>
								<TD noWrap align="left" width="80"><asp:image id="imgExtension" runat="server" ImageUrl="/cmsweb/images/titleicon/associated2.gif"></asp:image>&nbsp;<asp:linkbutton id="lbtnExtension" Runat="server">��չ����</asp:linkbutton></TD>
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