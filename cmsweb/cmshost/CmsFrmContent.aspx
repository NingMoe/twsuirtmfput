<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CmsFrmContent" CodeFile="CmsFrmContent.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<%@ Import Namespace="NetReusables"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>CmsFrmContent</TITLE>
		<meta http-equiv="Pragma" content="no-cache">
		<LINK href="../css/cmsstyle.css" type="text/css" rel="stylesheet">
			<SCRIPT language="JavaScript" src="../script/jscommon.js"></SCRIPT>
			<SCRIPT language="JavaScript" src="../script/base.js"></SCRIPT>
			<script language="JavaScript" src="../script/poslib.js"></script>
			<script language="JavaScript" src="../script/menu4.js"></script>
			<script language="JavaScript" src="../script/scrollbutton.js"></script>
			<SCRIPT language="JavaScript" src="../script/CmsScript.js"></SCRIPT>
			<script language="javascript" src="/cmsweb/script/dragDiv.js"></script>
			<script language="javascript">
<!--
var ctrlKeyClicked = false; //���ڶ�ѡ��¼
document.onkeydown=EvtKeyDown //������̵�����¼����
document.onkeyup=EvtKeyUp //������̵�����¼����
//���̻������¼���Ӧ���
function EvtKeyDown(){
	if (event.keyCode == 17){ //����ctrl��
		ctrlKeyClicked=true;
	}
}
//���̻������¼���Ӧ���
function EvtKeyUp(){
	if (event.keyCode == 17){ //����ctrl��
		ctrlKeyClicked=false;
	}
}
function checkAllCheckBox(obj)
{

	var o=obj.parentNode.parentNode.parentNode;
	var selectRecIds = "";
	var strPre = "";	
	
	if(obj.id.indexOf("Host")>-1)
	{
		strPre = "dgridHostTable__ctl";			
	}
	else
	{
		strPre = "dgridSubTable__ctl";		
	}
	for (var k=1;k<o.children.length;k++){
		var j = k+2;
		var chk = document.getElementById(strPre + j + "_cbx");
		if(chk!=null)
		{
			chk.checked = obj.checked;
			
			if(obj.checked)
			{
				if(obj.id.indexOf("Host")>-1)
					selectRecIds =  selectRecIds + o.children[k].RECID + ",";
				else
					selectRecIds =  selectRecIds + o.children[k].RECID2 + ",";
			}
		}
	}
	if(obj.id.indexOf("Host")>-1)
	{
	
		Form1.selectRecId1.value = selectRecIds;
	}
	else
	{

		Form1.selectRecId2.value = selectRecIds;
	}
}
function clickCheckBox(obj)
{
	var strRecIds = "";
	var strPre = "";
	var o=obj.parentNode.parentNode;
	var checkRecId = ""
	if(obj.id.indexOf("Host")>-1)
	{
		strPre = "dgridHostTable";		
		strRecIds = Form1.selectRecId1.value;
		checkRecId =  o.RECID;
	}
	else
	{
		strPre = "dgridSubTable";
		strRecIds = Form1.selectRecId2.value;
		checkRecId =  o.RECID2;
	}
	
	if(obj.checked)
	{		
		strRecIds =  strRecIds + checkRecId + ",";
	}
	else
	{
		strRecIds = strRecIds.replace(checkRecId + ",","");
		var chk = document.getElementById(strPre + "__ctl2_cbx");		
		if(chk!=null)
			chk.checked =false;
	}
	if(obj.id.indexOf("Host")>-1)
	{
		Form1.selectRecId1.value = strRecIds;
	}
	else
	{
		Form1.selectRecId2.value = strRecIds;
	}
	
	event.cancelBubble = true;
}
//���ɹ������õ�Tabscript
function ClickTabScript(){
	var o = event.srcElement;
	if (o.innerHTML=='&nbsp;' || o.innerHTML=='') return;
	if (o.tagName=='TD'){
		if (o.parentNode.parentNode.tagName=='TBODY'){
			for (var i=0;i<o.parentNode.children.length;i++ ){
				if (o.parentNode.children[i].innerHTML!='&nbsp;' && o.parentNode.children[i].innerHTML!=''){
					o.parentNode.children[i].background='../images/menu_r1_c3.gif';
				}
			}
			o.background='../images/menu_r1_c70.gif';
			//if (Form1.TabID_bak.value != o.TabID){ //ֻ���ڸı�Tabʱ���ύ
				Form1.TabID.value = o.TabID;
				Form1.cmsaction.value = "tabclick";
				Form1.content_width.value = self.document.body.clientWidth;
				Form1.content_height.value = self.document.body.clientHeight;
				Form1.submit();
			//}
		}
	}
}

function ChangeResOption(){
	var optRes = document.all.item("optionResource")
	if (optRes == null){
		return false;
	}else{
		var curResID = optRes.options(optRes.selectedIndex).value;
		if(curResID == ""){
			return false;
		}else{
			Form1.TabID.value = curResID;
			Form1.cmsaction.value = "tabclick";
			Form1.content_width.value = self.document.body.clientWidth;
			Form1.content_height.value = self.document.body.clientHeight;
			Form1.submit();
		}
	}
}

function RowLeftClickInHostTablePost(src){
	if (ctrlKeyClicked == true){ //��ѡ��¼
		if (Form1.CTRL_PRESSED.value == "0"){
			//ѡ��һ����ѡ��¼ʱ��Ҫ���������ѡ��ļ�¼(����й����������)
			var o=src.parentNode;
			for (var k=1;k<o.children.length;k++){
				if (o.children[k].bgColor_bak == null || o.children[k].bgColor_bak == ""){
					o.children[k].bgColor = "white";
				}else{
					o.children[k].bgColor = o.children[k].bgColor_bak;
				}
			}
			Form1.RECID.value = "";
			Form1.CTRL_PRESSED.value = "1";
		}
		src.bgColor = "#C4D9F9";
		if (Form1.RECID.value == ""){
			Form1.RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
		}else{
			Form1.RECID.value = Form1.RECID.value + "," + src.RECID; //��Ҫ���û�ѡ����к�POST��������
		}
	}else{
		//�����ύ
		//if (Form1.RECID_bak.value != src.RECID){ //�ظ������ǰѡ�еļ�¼��ҳ�治���ύ
			Form1.cmsaction.value = "hostrowclick"; //��Ҫ���û�ѡ����к�POST��������
			Form1.RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
			try{
				Form1.content_width.value = self.document.body.clientWidth;
				Form1.content_height.value = self.document.body.clientHeight;
			}catch(ex){
			}
			Form1.submit();
		//}
	}
}

function RowLeftClickInHostTableNoPost(src){
	if (ctrlKeyClicked == true){
		//��ѡ��¼
		src.bgColor = "#C4D9F9";
		Form1.RECID.value = Form1.RECID.value + "," + src.RECID; //��Ҫ���û�ѡ����к�POST��������
	}else{
		//��ѡ��¼
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			if (o.children[k].bgColor_bak == null || o.children[k].bgColor_bak == ""){
				o.children[k].bgColor = "white";
			}else{
				o.children[k].bgColor = o.children[k].bgColor_bak;
			}
		}
		src.bgColor = "#C4D9F9";
		Form1.RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
		//if (Form1.RECID.value == src.RECID){ //�ٴε��ѡ�еļ�¼����ȡ��ѡ��
		//	src.bgColor = "white";
		//	Form1.RECID.value = "0"; //���ܷſ�ֵ������������˲��ḳֵ
		//}else{ //ѡ�м�¼
		//	src.bgColor = "#C4D9F9";
		//	Form1.RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
		//}
	}
}

function RowLeftClickInSubTableNoPost(src){
	if (ctrlKeyClicked == true){
		//��ѡ��¼
		src.bgColor = "#C4D9F9";
		if (Form1.RECID2.value == ""){
			Form1.RECID2.value = src.RECID2; //��Ҫ���û�ѡ����к�POST��������
		}else{
			Form1.RECID2.value = Form1.RECID2.value + "," + src.RECID2; //��Ҫ���û�ѡ����к�POST��������
		}
	}else{
		//��ѡ��¼
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			if (o.children[k].bgColor_bak == null || o.children[k].bgColor_bak == ""){
				o.children[k].bgColor = "white";
			}else{
				o.children[k].bgColor = o.children[k].bgColor_bak;
			}
		}
		src.bgColor = "#C4D9F9";
		Form1.RECID2.value = src.RECID2; //��Ҫ���û�ѡ����к�POST��������
	}
}

//###################################################################################################################
//###################################################################################################################
//���淽�����ǲ˵��¼����
function MenuItemEntryPost(MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, MNUTARGET, NoUrlParam){
	if (confirmMsg != ""){
		if (confirm(confirmMsg) != true){
			return;
		}
	}
	
	//��ȡ��¼ID
	var strRecID;
	if (MNURESLOCATE == "1"){
		
		strRecID = Form1.selectRecId1.value;
		//��ȡ��ǰSession��Ctrl��ѡ�еļ�¼
		if (strRecID == "")
			strRecID = Form1.RECID.value;
		if (strRecID == ""){
			strRecID = "<%= CmsFrmContentBase.GetHostRecIDInSession(Request, Session, Response)%>"; 
		}
	}else if (MNURESLOCATE == "2"){
		strRecID = Form1.selectRecId2.value;
		//��ȡ��ǰSession��Ctrl��ѡ�еļ�¼
		if (strRecID == "")
			strRecID = Form1.RECID2.value;
	}else{
		strRecID = "";
	}

	if (MNUSELREC == "1" && strRecID == "" ){ //����ǿ��Ҫ��ѡ���¼�Ĳ˵�����
		alert('<%=CmsMessage.GetMsg(CmsPass, "RECORD_OPARATE") %>');
		return;
	}
	//alert(ResID);
	Form1.MenuSection.value = MenuSection;
	Form1.MenuKey.value = MenuKey;
	Form1.mnuformresid.value = frmresid;
	Form1.mnuresid.value = ResID;
	Form1.mnurecid.value = strRecID;
	Form1.mnuhostresid.value = "<%= CmsFrmContentBase.GetHostResIDInSession(Session, Response)%>";
	Form1.mnuhostrecid.value = "<%= CmsFrmContentBase.GetHostRecIDInSession(Request, Session, Response)%>";
	Form1.mnuformname.value = frmname;
	Form1.cmsaction.value = cmscmd;
	Form1.MNURESLOCATE.value = MNURESLOCATE;
	Form1.content_width.value = self.document.body.clientWidth;
	Form1.content_height.value = self.document.body.clientHeight;
	Form1.submit();
}

function MenuItemEntryGet(MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, MNUTARGET, NoUrlParam){
	var strUrl = MenuItemGetUrl(MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, NoUrlParam);
	if (strUrl == "NOACTION"){
		return;
	}
	
	if(MNURESLOCATE==1&&Form1.selectRecId1.value!="")
	{	
		strUrl = strUrl +"&selectedrecid=" + Form1.selectRecId1.value;
	}
	if(MNURESLOCATE==2&&Form1.selectRecId2.value!="")
	{	
		strUrl = strUrl +"&selectedrecid=" + Form1.selectRecId2.value;
	}
	if (MNUTARGET == "content" || MNUTARGET == "_self"){
	  strUrl = strUrl + "&backpage=../cmshost/CmsFrmContent.aspx";
	  window.location = strUrl;
	}else if (MNUTARGET == "_parent"){
	  strUrl = strUrl + "&backpage=../cmshost/CmsFrmBody.aspx";
	  window.open(strUrl, MNUTARGET);
	}else{
	  strUrl = strUrl + "&backpage=../cmshost/CmsFrmContent.aspx";
	  window.open(strUrl, MNUTARGET);
	}
}

//���´�����ָ��URL��֮ǰ��ȷ��
function MenuItemEntryForPopup(MenuType, MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, MNUTARGET, NoUrlParam, left, top, width, height, menubar, toolbar, location, resizable, scrollbars, titlebar, fullscreen){
	var strUrl = MenuItemGetUrl(MenuSection, MenuKey, cmscmd, url, ResID, IsReportService, MNURESLOCATE, MNUSELREC, frmresid, frmname, confirmMsg, NoUrlParam);
	if (strUrl == "NOACTION"){
		return;
	}

	if(Form1.selectRecId1.value!="")
	{
		strUrl = strUrl +"&selectedrecid=" + Form1.selectRecId1.value;
	}
	if (MenuType == "POPUP"){
		window.open(strUrl, MNUTARGET, "left="+left+",top="+top+",height="+height+",width="+width+",status=yes,toolbar="+toolbar+",menubar="+menubar+",location="+location+",resizable=" +resizable+",scrollbars="+scrollbars+",titlebar="+titlebar+",fullscreen="+fullscreen);
	}else if (MenuType == "DIALOG"){
		window.showModalDialog(strUrl, "", "dialogTop:" + top + ";dialogLeft:" + left + ";dialogHeight:" + height + "px; dialogWidth:" + width + "px; center:yes;resizable:" + resizable + ";scroll:" + scrollbars);
	}else if (MenuType == "DIALOGREFRESH"){
		window.showModalDialog(strUrl, "", "dialogTop:" + top + ";dialogLeft:" + left + ";dialogHeight:" + height + "px; dialogWidth:" + width + "px; center:yes;resizable:" + resizable + ";scroll:" + scrollbars);
		Form1.cmsaction.value = "MenuRecordRefresh";
		Form1.submit(); //ˢ�µ�ǰҳ��
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
		alert('<%=CmsMessage.GetMsg(CmsPass, "RECORD_OPARATE") %>');
		return "NOACTION";
	}

	//��װURL����
	var strUrl;
	if (url.indexOf("?") > 0){
		strUrl = url + "&timeid=" + Math.round(Math.random() * 10000000000);
	}
	else{
		strUrl = url + "?timeid=" + Math.round(Math.random() * 10000000000);
	}
	if (IsReportService == 1){
        var strParamRecID = "";
        if (MNUSELREC == "1"){ //URL��Ҫ�м�¼ID
            strParamRecID = "&mnurecid=" + strRecID;
        }
		strUrl = strUrl + strParamRecID;
	}else{
		strUrl = strUrl + "&MenuSection=" + MenuSection + "&MenuKey=" + MenuKey + "&MNURESLOCATE=" + MNURESLOCATE + "&cmsaction=" + cmscmd + "&mnuformresid=" + frmresid + "&mnuhostresid=" + strHostResID + "&mnuhostrecid=" + strHostRecID + "&mnurecid="+strRecID+"&mnuresid=" + ResID + "&mnuformname=" + escape(frmname);
	}
//    if (MNUSELREC == "1"){
//        strUrl += "&mnurecid=" + strRecID; //URL��Ҫ�м�¼ID
//    }
   // alert(strUrl);
	return strUrl;
}
//###################################################################################################################

function GridResize()
{
	try
	{
		var width = document.body.clientWidth-10;
		var height = document.body.clientHeight/2-50;
		if (document.getElementById("<%=panelSubTable.ClientID%>")==null) height = document.body.clientHeight-60;
		
		document.getElementById("<%=panelHostTableHeader.ClientID%>").style.width = width;
		document.getElementById("<%=Panel1.ClientID%>").style.width = width;
		document.getElementById("<%=Panel1.ClientID%>").style.height = height;
		document.getElementById("<%=panelPager1.ClientID%>").style.width = width;
		
		if (document.getElementById("<%=panelSubTable.ClientID%>")==null) return;
		
		document.getElementById("<%=panelSubTable.ClientID%>").style.width = width;
		document.getElementById("<%=Panel2.ClientID%>").style.width = width;
		document.getElementById("<%=Panel2.ClientID%>").style.height = document.body.clientHeight/2-85;
		document.getElementById("<%=panelPager2.ClientID%>").style.width = width;
	}
	catch (e){}	
}
//-->
			</script>
	</HEAD>
	<BODY onresize="GridResize()" style="OVERFLOW:auto">
		<form id="formDownFile" style="DISPLAY: none" action="" method="post" target="iformDoc">
		</form>
		<FONT face="����"></FONT>
		<iframe id="iformDoc" style="DISPLAY: none" name="iformDoc" src="about:blank"></iframe>
		<TABLE style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 1px; PADDING-TOP: 1px"
			cellSpacing="0" cellPadding="0" width="100%" border="0">
			<FORM id="Form1" name="Form1" action="../cmshost/CmsFrmContent.aspx" method="post" runat="server">
				<input type="hidden" name="cmsaction"> <input type="hidden" name="TabID"> <input type="hidden" name="content_width">
				<input type="hidden" name="content_height"> <input type="hidden" name="mnuformname">
				<input type="hidden" name="mnuresid"> <input type="hidden" name="mnurecid"> <input type="hidden" name="mnuhostresid">
				<input type="hidden" name="mnuhostrecid"> <input type="hidden" name="MNURESLOCATE">
				<input type="hidden" name="mnuformresid"> <input type="hidden" name="MenuSection">
				<input type="hidden" name="MenuKey"> <input type="hidden" name="RECID"> <input type="hidden" value="0" name="CTRL_PRESSED">
				<input type="hidden" name="RECID2"> <input type="hidden" name="selectRecId1"> <input type="hidden" name="selectRecId2">
				<TBODY>
					<tr>
						<td height="10">
							<asp:panel id="panelHostTableHeader" style="OVERFLOW: hidden" runat="server" Width="704px">
								<TABLE class="cms_toolbar" cellSpacing="0" width="100%" border="0">
									<TR>
										<TD width="1"></TD>
										<TD vAlign="bottom" noWrap align="left"> 
											<asp:Image id="Image1" runat="server" ImageUrl="../images/titleicon/creat.gif" ImageAlign="AbsBottom"></asp:Image>&nbsp;
											<asp:LinkButton id="lbtnHostAdd" runat="server">���</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnHostEdit" runat="server">�޸�</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnHostView" runat="server">����</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnHostDel" runat="server">ɾ��</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnHostRefresh" runat="server">ˢ��</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgHostRec" runat="server" ImageUrl="../images/tree/res_twod.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnHostRec" runat="server">��¼����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%If CmsPass.HostResData.ResTableType = "DOC" Then %>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgHostDoc" runat="server" ImageUrl="../images/tree/res_doc.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnHostDoc" runat="server">�ĵ�����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%End If%>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgHostTools" runat="server" ImageUrl="../images/titleicon/set2.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnHostTools" runat="server">Ӧ�ù���</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%If CmsRights.HasRightsResAction(CmsPass, CmsPass.HostResData.ResID) = True Then %>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgHostRes" runat="server" ImageUrl="../images/titleicon/change2.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnHostRes" runat="server">��Դ����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%End If%>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgHostExt" runat="server" ImageUrl="../images/flow.ico" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnHostExt" runat="server">��չ����</asp:LinkButton>&nbsp;&nbsp;&nbsp; 
										</TD>
										<TD vAlign="bottom" noWrap align="right" width="100%">
											<Pager:CmsPager id="Cmspager1" runat="server"></Pager:CmsPager></TD>
										<TD width="1"></TD>
									</TR>
								</TABLE>
							</asp:panel>
						</td>
					</tr>
					<TR>
						<TD vAlign="top" height="10">
							<asp:panel id="Panel1" style="OVERFLOW: auto" runat="server">
								<asp:DataGrid id="dgridHostTable" runat="server" CssClass="ListTable">
									<PagerStyle Visible="False"></PagerStyle>
									<ItemStyle CssClass="ListTable" Height="50"></ItemStyle>
									<HeaderStyle CssClass="Freezing"></HeaderStyle>
								</asp:DataGrid>
							</asp:panel>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" height="10">
							<asp:panel id="panelPager1" style="Z-INDEX: 139; OVERFLOW: hidden" runat="server" Height="25px">
								<TABLE cellSpacing="0" cellPadding="0" width="100%" bgColor="#e7ebef" border="0">
									<TR>
										<TD vAlign="top" align="left" width="100%">
											<asp:dropdownlist id="ddlColumns" runat="server" Width="88px"></asp:dropdownlist>
											<asp:dropdownlist id="ddlConditions" runat="server" Width="75px"></asp:dropdownlist>
											<asp:textbox id="txtSearchValue" runat="server" Width="140px" ToolTip="�������ѯ����"></asp:textbox>
											<asp:Image id="imgSearchDate1" runat="server" ImageUrl="../images/icon_datetime.gif" ImageAlign="AbsBottom"
												AlternateText="�������������˵�"></asp:Image>&nbsp;
											<asp:LinkButton id="lbtnHostSearch" runat="server">��ʼ��ѯ</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnHostSearchAgain" runat="server">�ٴβ�ѯ</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnHostFTableSearch" runat="server">ȫ���ѯ</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnHostAdvancedSearch" runat="server">�߼���ѯ</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnHostCancel" runat="server">ȡ����ѯ</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lbtnHostStat" runat="server">ͳ��</asp:LinkButton></TD>
										<TD vAlign="middle" align="right" width="200"></TD>
									</TR>
								</TABLE>
							</asp:panel>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="panelSubTable" style="OVERFLOW: hidden" runat="server" Width="704px">
								<TABLE class="cms_toolbar" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD width="1"></TD>
										<TD vAlign="bottom" noWrap align="left">&nbsp;
											<asp:Image id="Image2" runat="server" ImageUrl="../images/titleicon/creat.gif" ImageAlign="AbsBottom"></asp:Image>&nbsp;
											<asp:LinkButton id="lbtnSubAdd" runat="server">���</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnSubEdit" runat="server">�޸�</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnSubView" runat="server">����</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnSubDel" runat="server">ɾ��</asp:LinkButton>&nbsp;&nbsp;
											<asp:LinkButton id="lbtnSubRefresh" runat="server">ˢ��</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgSubRec" runat="server" ImageUrl="../images/tree/res_twod.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnSubRec" runat="server">��¼����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%If CmsPass.RelResData.ResTableType = "DOC" Then %>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgSubDoc" runat="server" ImageUrl="../images/tree/res_doc.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnSubDoc" runat="server">�ĵ�����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%End If%>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgSubTools" runat="server" ImageUrl="../images/titleicon/set2.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnSubTools" runat="server">Ӧ�ù���</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%If CmsRights.HasRightsResAction(CmsPass, CmsPass.RelResData.ResID) = True Then %>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgSubRes" runat="server" ImageUrl="../images/titleicon/change2.gif" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnSubRes" runat="server">��Դ����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<%End If%>
										<TD vAlign="bottom" noWrap align="left">
											<asp:Image id="imgSubExt" runat="server" ImageUrl="../images/flow.ico" ImageAlign="AbsBottom"></asp:Image>
											<asp:LinkButton id="lbtnSubExt" runat="server">��չ����</asp:LinkButton>&nbsp;&nbsp;&nbsp;
										</TD>
										<TD vAlign="bottom" noWrap align="right" width="100%">
											<Pager:CmsPager id="Cmspager2" runat="server"></Pager:CmsPager></TD>
										<TD width="1"></TD>
									</TR>
								</TABLE>
								<TABLE height="25" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD width="100%">
											<DIV id="divTabs" align="left" runat="server"></DIV>
										</TD>
									</TR>
								</TABLE>
							</asp:panel>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:panel id="Panel2" style="OVERFLOW: auto" runat="server">
								<asp:DataGrid id="dgridSubTable" runat="server" CssClass="ListTable">
									<PagerStyle Visible="False"></PagerStyle>
								</asp:DataGrid>
							</asp:panel>
						</TD>
					</TR>
					<tr>
						<TD>
							<asp:panel id="panelPager2" style="Z-INDEX: 119" runat="server" Height="25px">
								<TABLE cellSpacing="0" cellPadding="0" width="100%" bgColor="#e7ebef" border="0">
									<TR>
										<TD vAlign="middle" align="left" width="100%">
											<asp:dropdownlist id="ddlColumnsSub" runat="server" Width="88px"></asp:dropdownlist>
											<asp:dropdownlist id="ddlConditionsSub" runat="server" Width="75px"></asp:dropdownlist>
											<asp:textbox id="txtSearchValueSub" runat="server" Width="140px" ToolTip="�������ѯ����"></asp:textbox>
											<asp:image id="imgSearchDate2" runat="server" ImageUrl="../images/icon_datetime.gif" ImageAlign="AbsBottom"
												AlternateText="�������������˵�"></asp:image>&nbsp;
											<asp:linkbutton id="lbtnSubSearch" runat="server">��ʼ��ѯ</asp:linkbutton>&nbsp;
											<asp:linkbutton id="lbtnSubSearchAgain" runat="server">�ٴβ�ѯ</asp:linkbutton>&nbsp;
											<asp:linkbutton id="lbtnSubFTableSearch" runat="server">ȫ���ѯ</asp:linkbutton>&nbsp;
											<asp:LinkButton id="lbtnSubAdvancedSearch" runat="server">�߼���ѯ</asp:LinkButton>&nbsp;
											<asp:linkbutton id="lbtnSubCancel" runat="server">ȡ����ѯ</asp:linkbutton>&nbsp;
											<asp:linkbutton id="lbtnSubStat" runat="server">ͳ��</asp:linkbutton></TD>
										<TD vAlign="middle" align="right" width="200"></TD>
									</TR>
								</TABLE>
							</asp:panel>
						</TD>
					</tr>
			</FORM>
		</TABLE>
		<script>GridResize();</script>
	</BODY>
</HTML>
