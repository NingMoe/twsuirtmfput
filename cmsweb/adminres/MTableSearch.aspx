<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearch" CodeFile="MTableSearch.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>���ͳ�ƺ��й�������</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<script language="javascript">
<!--
function OpenMultiTableSearchResultWindow(){
	if (self.document.forms(0).RECID.value == ""){
		alert("��ѡ����Ч�Ķ��ͳ�ƺ��й������ö��壡");
	}else{
	  //���720���պô�ӡA4ֽ
		//window.open("/cmsweb/adminres/MTableSearchResultSimple.aspx?mtshostid=" + self.document.forms(0).RECID.value + "&mnuresid=" + getUrlParam("mnuresid") + "&mtslogicand=1", '_blank', "left=10,top=10,height=630,width=720,status=no,toolbar=no,menubar=yes,location=no,resizable=yes,scrollbars=yes");
		window.open("/cmsweb/adminres/MTableSearchResult2ColumnII.aspx?mtshostid=" + self.document.forms(0).RECID.value + "&mnuresid=" + getUrlParam("mnuresid") + "&mtslogicand=1", '_blank', "left=10,top=10,height=630,width=720,status=no,toolbar=no,menubar=yes,location=no,resizable=yes,scrollbars=yes");
	}
	return false;
}

function RowLeftClickNoPost(src){
	try{
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			o.children[k].bgColor = "white";
		}
		src.bgColor = "#C4D9F9";
		self.document.forms(0).RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
	}catch(ex){
	}
}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header"><b><asp:label id="lblTitle" runat="server">���ͳ�ƺ��й�������</asp:label></b></TD>
							</TR>
							<TR>
								<TD vAlign="top"><FONT face="����"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></FONT></TD>
							</TR>
							<TR>
								<TD vAlign="top"><FONT face="����"><FONT face="Times New Roman"><asp:button id="btnStartSearch" runat="server" Text="��ʼ��ѯ" Width="76px"></asp:button><asp:button id="btnTempApply" runat="server" Text="��ʱӦ��" Width="76px"></asp:button><asp:button id="btnStart" runat="server" Text="����" Width="76px"></asp:button><asp:button id="btnStop" runat="server" Text="ͣ��" Width="76px"></asp:button></FONT><FONT face="����">&nbsp;</FONT><asp:button id="btnAdd" runat="server" Text="�������" Width="76px"></asp:button><asp:button id="btnEdit" runat="server" Text="�༭����" Width="76px"></asp:button><asp:button id="btnDelete" runat="server" Text="ɾ������" Width="76px"></asp:button><asp:button id="btnCopy" runat="server" Width="76px" Text="��������"></asp:button>
										<asp:button id="btnReport" runat="server" Width="76px" Text="ͳ�Ʊ�����"></asp:button></FONT><asp:button id="btnExit" runat="server" Text="�˳�" Width="76px"></asp:button></TD>
							</TR>
							<TR>
								<TD vAlign="top" height="12"></TD>
							</TR>
							<TR>
								<TD vAlign="top">
									<asp:panel id="panelMove" runat="server" Width="200px">
										<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16"><FONT face="����">&nbsp;</FONT>
										<asp:linkbutton id="lbtnMoveUp" runat="server">�����ƶ�</asp:linkbutton>
										<FONT face="����">&nbsp; </FONT><IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16"><FONT face="����">&nbsp;</FONT>
										<asp:linkbutton id="lbtnMoveDown" runat="server">�����ƶ�</asp:linkbutton>
									</asp:panel></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
