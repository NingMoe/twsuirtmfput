<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FormOpen" CodeFile="FormOpen.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FormOpen</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<META http-equiv="expires" content="0">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<script language="JavaScript">
<!--
//打开选择的窗体
function OpenSelDForm(){
	if (document.all.item("ListBox1").value == ""){
		alert("请选择合适的窗体名称！");
		return;
	}
	
	window.returnValue = "$OPENFORM=" + document.all.item("ListBox1").value;
	window.close();
}

//打开选择的窗体
function CreateNewDForm(){
	var strName = new String(document.all.item("txtNewFormName").value)
	strName = strName.replace(" ", "")
	if (strName == ""){
		alert("请输入需要新建的窗体名称！");
		return;
	}
	
	window.returnValue = "$NEWFORM=" + document.all.item("txtNewFormName").value;
	window.close();
}

//删除选中的窗体设计
function DeleteDesignedForm(){
	//获取需要删除的窗体名称
	var selIndex = document.all.item("ListBox1").selectedIndex;
	if (selIndex == -1){
		alert("请选择合适的窗体名称！");
		return;
	}
	var strFormName = document.all.item("ListBox1").options(selIndex).value;
	
	//开始删除窗体
	var rtnVal = window.showModalDialog("/cmsweb/cmsform/FormDelete.aspx?mnuresid=" + getUrlParam("mnuresid") + "&mnuformtype=" + getUrlParam("mnuformtype") + "&mnuformname=" + escape(strFormName), "", "dialogHeight:280px; dialogWidth:280px; center;yes"); 
	if (rtnVal == "$SUCCESS"){ 
		document.all.item("ListBox1").remove(selIndex) //如果删除成功，则删除Listbox中的节点
	}
}

//点击Cancel退出
function DoExit(){
	window.returnValue = "$CANCEL";
	window.close();
}
-->
		</script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:listbox id="ListBox1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server"
				Height="168px" Width="152px"></asp:listbox><INPUT style="Z-INDEX: 102; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 16px; HEIGHT: 24px"
				onclick="OpenSelDForm()" type="button" value="打开">&nbsp; <INPUT style="Z-INDEX: 103; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				onclick="DoExit()" type="button" value="退出"><INPUT style="Z-INDEX: 104; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 192px; HEIGHT: 24px"
				onclick="CreateNewDForm()" type="button" value="创建新窗体"><INPUT id="txtNewFormName" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 192px"
				type="text"> <INPUT style="Z-INDEX: 106; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 48px; HEIGHT: 24px"
				onclick="DeleteDesignedForm()" type="button" value="删除">&nbsp;
		</form>
	</body>
</HTML>
