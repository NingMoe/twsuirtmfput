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
//��ѡ��Ĵ���
function OpenSelDForm(){
	if (document.all.item("ListBox1").value == ""){
		alert("��ѡ����ʵĴ������ƣ�");
		return;
	}
	
	window.returnValue = "$OPENFORM=" + document.all.item("ListBox1").value;
	window.close();
}

//��ѡ��Ĵ���
function CreateNewDForm(){
	var strName = new String(document.all.item("txtNewFormName").value)
	strName = strName.replace(" ", "")
	if (strName == ""){
		alert("��������Ҫ�½��Ĵ������ƣ�");
		return;
	}
	
	window.returnValue = "$NEWFORM=" + document.all.item("txtNewFormName").value;
	window.close();
}

//ɾ��ѡ�еĴ������
function DeleteDesignedForm(){
	//��ȡ��Ҫɾ���Ĵ�������
	var selIndex = document.all.item("ListBox1").selectedIndex;
	if (selIndex == -1){
		alert("��ѡ����ʵĴ������ƣ�");
		return;
	}
	var strFormName = document.all.item("ListBox1").options(selIndex).value;
	
	//��ʼɾ������
	var rtnVal = window.showModalDialog("/cmsweb/cmsform/FormDelete.aspx?mnuresid=" + getUrlParam("mnuresid") + "&mnuformtype=" + getUrlParam("mnuformtype") + "&mnuformname=" + escape(strFormName), "", "dialogHeight:280px; dialogWidth:280px; center;yes"); 
	if (rtnVal == "$SUCCESS"){ 
		document.all.item("ListBox1").remove(selIndex) //���ɾ���ɹ�����ɾ��Listbox�еĽڵ�
	}
}

//���Cancel�˳�
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
				onclick="OpenSelDForm()" type="button" value="��">&nbsp; <INPUT style="Z-INDEX: 103; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 80px; HEIGHT: 24px"
				onclick="DoExit()" type="button" value="�˳�"><INPUT style="Z-INDEX: 104; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 192px; HEIGHT: 24px"
				onclick="CreateNewDForm()" type="button" value="�����´���"><INPUT id="txtNewFormName" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 192px"
				type="text"> <INPUT style="Z-INDEX: 106; LEFT: 176px; WIDTH: 80px; POSITION: absolute; TOP: 48px; HEIGHT: 24px"
				onclick="DeleteDesignedForm()" type="button" value="ɾ��">&nbsp;
		</form>
	</body>
</HTML>
