/*打开高级字典的记录选择窗体*/
function OpenAdvDictWindow(strMainResID, strColName, strCtrlName, blnIsMultiTable, strUserName, strUserEnPass, strFilterMainCtrl1, strFilterDictCol1, strFilterMainCtrl2, strFilterDictCol2, strFilterMainCtrl3, strFilterDictCol3,lngMode){
	var ctlName=eval('self.document.forms(0).' + strCtrlName);
	var colValue = new String(ctlName.value);
	colValue = colValue.replace("+", "x__plus");//jbjtemp 临时处理，应该用encode
	//colValue = escape(colValue);
	//var colValue2 = encodeURI(colValue);
	
	var strFilterValue1 = "";
	if (strFilterMainCtrl1 != ""){
		var ctlFilterCol = eval('self.document.forms(0).' + strFilterMainCtrl1);
		strFilterValue1 = new String(ctlFilterCol.value);
	}
	var strFilterValue2 = "";
	if (strFilterMainCtrl2 != ""){
		var ctlFilterCol = eval('self.document.forms(0).' + strFilterMainCtrl2);
		strFilterValue2 = new String(ctlFilterCol.value);
	}
	var strFilterValue3 = "";
	if (strFilterMainCtrl3 != ""){
		var ctlFilterCol = eval('self.document.forms(0).' + strFilterMainCtrl3);
		strFilterValue3 = new String(ctlFilterCol.value);
	}

	//var strUrl = '/cmsweb/adminres/FieldGetAdvDictionary.aspx?dynlogin=1&mnuresid=' + strMainResID + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass) + "&filtercol1=" + escape(strFilterDictCol1) + "&filtercolval1=" + escape(strFilterValue1) + "&filtercol2=" + escape(strFilterDictCol2) + "&filtercolval2=" + escape(strFilterValue2) + "&filtercol3=" + escape(strFilterDictCol3) + "&filtercolval3=" + escape(strFilterValue3) + '&colname=' + escape(strColName) + '&ctrlname=' + escape(strCtrlName) + '&multitab=' + blnIsMultiTable + '&colval=' + escape(colValue) + '&mode=' + escape(lngMode);
	//window.open(strUrl, 'advdict_' + strMainResID, 'left=10,top=10,height=660,width=950,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	var strUrl = '/cmsweb/adminres/FieldGetAdvDictionary.aspx?dynlogin=1@*mnuresid=' + strMainResID + "@*filtercol1=" + escape(strFilterDictCol1) + "@*filtercolval1=" + escape(strFilterValue1) + "@*filtercol2=" + escape(strFilterDictCol2) + "@*filtercolval2=" + escape(strFilterValue2) + "@*filtercol3=" + escape(strFilterDictCol3) + "@*filtercolval3=" + escape(strFilterValue3) + '@*colname=' + escape(strColName) + '@*ctrlname=' + escape(strCtrlName) + '@*multitab=' + blnIsMultiTable + '@*colval=' + escape(colValue) + '@*mode=' + escape(lngMode);
	strUrl="/cmsweb/SvcLogin.aspx?user=" + escape(strUserName) + "&ucode="+ strUserEnPass +"&targetpage=" + strUrl
	
	OpenCenterWindow(strUrl,900,600,'advdict_' + strMainResID);
	return false;
}

/*打开定制编码的记录选择窗体*/
function OpenCustCodeWindow(strUrl, strParam2, strResID, strColName, strCtrlName, blnIsMultiTable, left, top, width, height, strUserName, strUserEnPass){
	var ctlName=eval('self.document.forms(0).' + strCtrlName);
	var colValue = new String(ctlName.value);
	colValue = colValue.replace("+", "x__plus"); //jbjtemp 临时处理，应该用encode
	//colValue = escape(colValue);
	if (strUrl.indexOf("?") > 0){
        strUrl = strUrl + '&';
    }else{
        strUrl = strUrl + '?';
    }
	strUrl = strUrl + 'strParam2=' + strParam2 + '&dynlogin=1&mnuresid=' + strResID + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass) + '&colname=' + escape(strColName) + '&ctrlname=' + escape(strCtrlName) + '&multitab=' + blnIsMultiTable + '&colval=' + escape(colValue);
	window.open(strUrl, 'custcode_' + strResID, 'left=' + left + ',top=' + top + ',height=' + height + ',width=' + width + ',status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	return false;
}

//在界面上的文档框（编辑状态下的Textbox）右边添加"查阅"按钮，点击后跳出窗体提取或打开文档
function OpenDocFileWindow(strResID, strRecID, strUserName, strUserEnPass){
	var strUrl = '/cmsweb/cmsdocument/DocOpen.aspx?dynlogin=1&mnuresid=' + strResID + '&docrecid=' + strRecID + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass);
	window.open(strUrl, 'docfile_' + strResID, 'status=yes,toolbar=yes,menubar=yes,location=yes,resizable=yes,scrollbars=yes');
	return false;
}

//保存主资源记录
function SaveHostRecord(strResID){
    var rtn = CheckValue(self.document.forms(0));
    if (rtn == false){
        return false;
    }
	//为了确保页面刷新为最新数据，这里必须设置为保存命令，等效为在页面上点击“保存”
	self.document.forms(0).isfrom.value = "savehostrec";
	var eventTarget = "lnkSave";
	self.document.forms(0).__EVENTTARGET.value = eventTarget.split("$").join(":");
	self.document.forms(0).__EVENTARGUMENT.value = "";
	self.document.forms(0).submit();
	return false;
}

//添加子资源记录
function AddSubRecord(strHostResID, strHostRecID, strResID, strUserName, strUserEnPass){
	var strUrl = '/cmsweb/cmshost/RecordEditRelPopup.aspx?dynlogin=1&mnuhostresid=' + strHostResID + '&mnuhostrecid=' + strHostRecID + '&mnuresid=' + strResID + '&mnuinmode=3' + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass);
	window.open(strUrl, 'recadd_' + strResID, 'left=10,top=10,height=680,width=950,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	return false;
}

//修改子资源记录
function EditSubRecord(strHostResID, strHostRecID, strResID, strUserName, strUserEnPass){
    var ctlName = eval("self.document.forms(0).RECID3_" + strResID);
	if (ctlName.value == ""){
		alert("请选择需要修改的记录！");
		return false;
	}
	var strUrl = '/cmsweb/cmshost/RecordEditRelPopup.aspx?dynlogin=1&mnuhostresid=' + strHostResID + '&mnuhostrecid=' + strHostRecID + '&mnuresid=' + strResID + '&mnuinmode=5&mnurecid=' + escape(ctlName.value) + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass);
	window.open(strUrl, 'recedit_' + strResID, 'left=10,top=10,height=680,width=950,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	return false;
}

//查阅子资源记录
function ViewSubRecord(strHostResID, strHostRecID, strResID, strUserName, strUserEnPass){
    var ctlName = eval("self.document.forms(0).RECID3_" + strResID);
	if (ctlName.value == ""){
		alert("请选择需要查阅的记录！");
		return false;
	}
	var strUrl = '/cmsweb/cmshost/RecordEditRelPopup.aspx?dynlogin=1&mnuhostresid=' + strHostResID + '&mnuhostrecid=' + strHostRecID + '&mnuresid=' + strResID + '&mnuinmode=13&mnurecid=' + escape(ctlName.value) + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass);
	window.open(strUrl, 'recview_' + strResID, 'left=10,top=10,height=680,width=950,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	return false;
}

//删除子资源记录
function DeleteSubRecord(strResID){
    var ctlName = eval("self.document.forms(0).RECID3_" + strResID);
	if (ctlName.value == ""){
		alert("请选择需要删除的记录！");
		return false;
	}
	self.document.forms(0).subtabresid.value = strResID;
	//为了确保页面刷新为最新数据，这里必须设置为保存命令，等效为在页面上点击“保存”
	self.document.forms(0).isfrom.value = "delrelrec";
	var eventTarget = "lnkSave";
	self.document.forms(0).__EVENTTARGET.value = eventTarget.split("$").join(":");
	self.document.forms(0).__EVENTARGUMENT.value = "";
	self.document.forms(0).submit();
	return false;
}

//删除子资源记录
function ClosePopupEditForm(){
	//为了确保页面刷新为最新数据，这里必须设置为保存命令，等效为在页面上点击“保存”
    //window.opener.document.forms(0).isfrom.value='popupfresh';
	var eventTarget = "lnkSave";
	window.opener.document.forms(0).__EVENTTARGET.value = eventTarget.split("$").join(":");
	window.opener.document.forms(0).__EVENTARGUMENT.value = "";
    window.opener.document.forms(0).submit();
    window.close();
}

//提取子资源文档
function SubRecordGetDoc(strResID, strUserName, strUserEnPass){
    var ctlName = eval("self.document.forms(0).RECID3_" + strResID);
	if (ctlName.value == ""){
		alert("请选择需要提取的文档记录！");
		return false;
	}
	var strRecID = ctlName.value;
	var strUrl = '/cmsweb/cmsdocument/DocOpen.aspx?dynlogin=1&mnuresid=' + strResID + '&docrecid=' + strRecID + "&docopenstyle=1" + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass); //docopenstyle=1: 提取文档
	window.open(strUrl, 'docget_' + strResID, 'left=10,top=10,height=200,width=300,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	return false;
}

//在线浏览子记录文档
function SubRecordOnlineViewDoc(strResID, strUserName, strUserEnPass){
    var ctlName = eval("self.document.forms(0).RECID3_" + strResID);
	if (ctlName.value == ""){
		alert("请选择需要在线浏览的文档记录！");
		return false;
	}
	var strRecID = ctlName.value;
	var strUrl = '/cmsweb/cmsdocument/DocOpen.aspx?dynlogin=1&mnuresid=' + strResID + '&docrecid=' + strRecID + "&docopenstyle=2" + "&user=" + escape(strUserName) + "&ucode=" + escape(strUserEnPass); //docopenstyle=2: 在线浏览文档
	window.open(strUrl, 'docview_' + strResID, 'left=10,top=10,height=680,width=950,status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');
	return false;
}

//输入窗体中子资源记录选中的事件响应
function RowLeftClickInSubResTable(src, ResID){
	//单选记录
	var o=src.parentNode;
	for (var k=1;k<o.children.length;k++){
		if (o.children[k].bgColor_bak == null || o.children[k].bgColor_bak == ""){
			o.children[k].bgColor = "white";
		}else{
			o.children[k].bgColor = o.children[k].bgColor_bak;
		}
	}
	src.bgColor = "#C4D9F9";
	var ctlName = eval("self.document.forms(0).RECID3_" + ResID);
	ctlName.value = src.RECID3; //需要将用户选择的行号POST给服务器
}

/*-----------------------------------------------------------------------------------
在屏幕中间打开字典
-----------------------------------------------------------------------------------*/
function OpenCenterWindow(url,width,height,winname){
	var left = (screen.availWidth-width)/2;
	var top = (screen.availHeight-height)/2-20;
	var winname1="winDictionary";
	if(winname != null) winname1=winname;
	var openWin = window.open(url,winname1,"top="+top+",left="+left+",height="+height+",width="+width+",scrollbars=yes,resizable=yes,status=yes");
	try
	{
		openWin.focus();
	}
	catch(e)
	{
		alert(e);
	}
}

function ClickBtuName(name,ResID)
{	
	var ctlName = eval("self.document.forms(0).EditState3_" + ResID);
	ctlName.value = name; //需要将用户选择的行号POST给服务器
}
