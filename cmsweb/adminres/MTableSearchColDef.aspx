<%@ Import Namespace="Unionsoft.Platform"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearchColDef" CodeFile="MTableSearchColDef.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>��ѯ����</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<script language="javascript">
<!--
function OpenMultiTableSearchResultWindow(MTSHostID){
  //���720���պô�ӡA4ֽ
	window.open("/cmsweb/adminres/MTableSearchResultSimple.aspx?mtshostid=" + MTSHostID + "&mnuresid=" + getUrlParam("mnuresid") + "&mtslogicand=1", '_blank', "left=10,top=10,height=630,width=720,status=no,toolbar=no,menubar=yes,location=no,resizable=yes,scrollbars=yes");
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
			<input type="hidden" name="RECID">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header"><b><asp:label id="lblTitle" runat="server">Label</asp:label></b></TD>
							</TR>
							<TR>
								<TD><FONT face="����"><asp:label id="lblSearchTitle" runat="server">���ñ���</asp:label></FONT>&nbsp;<asp:textbox id="txtMTSearchTitle" runat="server" Width="260px"></asp:textbox><asp:button id="btnSave" runat="server" Width="100px" Text="��������"></asp:button><asp:Button id="btnDelete" runat="server" Width="80px" Text="ɾ������"></asp:Button></TD>
							</TR>
							<TR>
								<TD><FONT face="����"><asp:linkbutton id="lbtnSelHostRes" runat="server">ѡ����Դ</asp:linkbutton></FONT>&nbsp;<asp:dropdownlist id="ddlResList" runat="server" Width="104px"></asp:dropdownlist><asp:dropdownlist id="ddlHostResCol" runat="server" Width="156px"></asp:dropdownlist><asp:label id="Label1" runat="server">��ֵ</asp:label><asp:dropdownlist id="ddlHostConditions" runat="server" Width="72px"></asp:dropdownlist><asp:dropdownlist id="dllDefaultVal" runat="server" Width="100px"></asp:dropdownlist><asp:textbox id="txtColValue" runat="server" Width="90px"></asp:textbox><asp:label id="Label4" runat="server">��</asp:label><asp:dropdownlist id="ddlValCol" runat="server" Width="138px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD vAlign="middle"><asp:button id="btnStartSearch" runat="server" Width="80px" Text="��ʼ��ѯ"></asp:button><asp:button id="btnAddShow" runat="server" Width="80px" Text="�����ʾ"></asp:button><asp:button id="btnAddCond" runat="server" Width="80px" Text="�������"></asp:button><asp:button id="btnAddOrderASC" runat="server" Width="80px" Text="�������"></asp:button><asp:button id="btnAddOrderDesc" runat="server" Width="80px" Text="��ӵ���"></asp:button><asp:button id="btnAddEmail" runat="server" Width="80px" Text="�������"></asp:button><asp:button id="btnAddMobile" runat="server" Width="80px" Text="����ֻ�"></asp:button><asp:button id="btnExit" runat="server" Width="80px" Text="�˳�"></asp:button><FONT face="����"><asp:image id="imgUp" runat="server" ImageUrl="/cmsweb/images/Icons/up.gif"></asp:image></FONT><asp:linkbutton id="lbtnMoveUp" runat="server">�����ƶ�</asp:linkbutton><FONT face="����">&nbsp;
										<asp:image id="imgDown" runat="server" ImageUrl="/cmsweb/images/Icons/up.gif"></asp:image></FONT><FONT face="����">&nbsp;</FONT><asp:linkbutton id="lbtnMoveDown" runat="server">�����ƶ�</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" vAlign="middle"></TD>
							</TR>
							<TR>
								<TD vAlign="middle"><FONT face="����"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
