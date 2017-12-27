//打开一个窗口
//传入参数:需要打开的窗口的Url
function OpenModalWin(url){
	var left = (screen.availWidth-770)/2;
	var top = (screen.availHeight-500)/2;
	var objWin = window.open(url,"ViewWorkflowDiagram","width=770,height=500,top="+top+",left="+left+",status=yes,resizable=no"); 
	if (objWin!=null)
	{
		try{
			objWin.focus();
		}
		catch(e){}
	}
}

//打开附件
//传入参数:所属分类的ID,附件记录ID,是否为签出(0/1)
function OpenDocFileWindow(strResID, strRecID,strIsCheckOut){
	if (strIsCheckOut==null) strIsCheckOut = "2";			//表示不是来自流程文档表的附件。
	if (strIsCheckOut=="undefined") strIsCheckOut = "0";
	var strUrl = "FileWebAdapter.aspx?ResourceID=" + strResID + "&DocumentID=" + strRecID + "&IsCheckOut=" + strIsCheckOut;
	var left = (screen.availWidth-800)/2;
	var top = (screen.availHeight-700)/2;
	var objDocumentWin = window.open(strUrl, null, "height=700,width=800,top="+top+",left="+left+",status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=no");
	if (objDocumentWin!=null)
	{
		try{
			objDocumentWin.focus();
		}
		catch(e){}
	}
	return false;
}

//显示流程选择
function ShowWorkflowDictionary(url,txtFlowName,txtFlowID){
	var returnvalue = window.showModalDialog(url,'','dialogWidth:770px;dialogHeight:400px;status:no;');
	if (returnvalue!=null)
	{
		var aryValue = returnvalue.split('|');

		txtFlowID.value = aryValue[0];

		txtFlowName.value = aryValue[1];
	}
}

//打开一个流程处理窗体
function OpenWorkflowProcessWindow(url){
	var openWin = window.open(url, "WorkflowWindow", "left=0,top=0,width=770,height=700,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=no");
	try{
		openWin.focus();
	}
	catch(e)
	{}
}

//打开一个流程处理窗体
function OpenWorkflowPrintWindow(url){
	var openWin = window.open(url, "WorkflowPrintWindow", "status=no,toolbar=no,menubar=yes,location=no,resizable=no,scrollbars=no");
	try{
		openWin.focus();
	}
	catch(e)
	{}
}


