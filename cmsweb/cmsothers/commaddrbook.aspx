<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.CommAddrbook" CodeFile="CommAddrbook.aspx.vb" %>
<%@ Import Namespace="Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">�ʼ��ֻ���ַ��</TITLE>
		<meta http-equiv="Pragma" content="no-cache" />
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
			<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
			<script language="javascript">
<!--
	function RowLeftClickNoPost(src){
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			o.children[k].bgColor = "white";
		}
		src.bgColor = "#C4D9F9";
		self.document.forms(0).RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
		self.document.forms(0).REC_EMAIL.value = src.REC_EMAIL; //��Ҫ���û�ѡ����к�POST��������
		self.document.forms(0).REC_HANDPHONE.value = src.REC_HANDPHONE; //��Ҫ���û�ѡ����к�POST��������
	}
-->
			</script>
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<input type="hidden" name="RECID"> <input type="hidden" name="REC_EMAIL"> <input type="hidden" name="REC_HANDPHONE">
			<TABLE style="PADDING-LEFT: 4px" cellSpacing="0" cellPadding="2" width="760" border="0">
				<tr>
					<td width="100%" colSpan="3">
						<TABLE class="toolbar_table" cellSpacing="0" width="100%" border="0">
							<TR>
								<TD width="8"></TD>
								<TD noWrap align="left" width="67"><IMG src="/cmsweb/images/icons/xpFolder.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnConfirm" runat="server">ȷ��</asp:linkbutton></TD>
								<TD noWrap align="left" width="60"><IMG src="/cmsweb/images/icons/xpFolder.gif" align="absMiddle" width="16" height="16">
									<asp:linkbutton id="lbtnCancel" runat="server">ȡ��</asp:linkbutton></TD>
								<TD noWrap align="left" width="60"></TD>
								<TD noWrap align="left" width="80"></TD>
								<TD noWrap align="left" width="100"></TD>
								<TD noWrap align="left" width="100"></TD>
								<td>&nbsp;</td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<TD align="left" width="220"><asp:label id="Label1" runat="server">��ѡ�����ʼ��ֻ���Ϣ����Դ��</asp:label></TD>
					<td align="left" width="350"><asp:textbox id="txtSearchValue" runat="server" Width="168px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnSearch" runat="server">��Ϣ��ѯ</asp:linkbutton>&nbsp;<asp:linkbutton id="lbtnCancelSearch" runat="server">ȡ����ѯ</asp:linkbutton>
					</td>
					<TD vAlign="middle" align="left" width="140"><asp:linkbutton id="lbtnAddReceiver" runat="server">����ʼ��Ͷ����ռ���</asp:linkbutton></TD>
				</tr>
				<TR>
					<TD vAlign="top" width="220"><asp:panel id="Panel1" style="OVERFLOW: auto" runat="server" Width="224px" BorderWidth="1"
							Height="442px"><%LoadTreeView(CmsPass, Request, Response)%></asp:panel></TD>
					<TD vAlign="top" width="350"><asp:datagrid id="DataGrid1" runat="server">
							<PagerStyle PageButtonCount="15" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
					<TD vAlign="top" width="140"><asp:linkbutton id="lbtnAddEmailReceiver" runat="server">����ʼ�</asp:linkbutton>&nbsp;
						<asp:linkbutton id="lbtnRemoveEmail" runat="server">�Ƴ��ʼ�</asp:linkbutton><BR>
						<asp:listbox id="lbxEmail" runat="server" Width="136px" Height="212px" DESIGNTIMEDRAGDROP="738"></asp:listbox><BR>
						<asp:linkbutton id="lbtnAddSmsReceiver" runat="server">����ֻ�</asp:linkbutton>&nbsp;
						<asp:linkbutton id="lbtnRemovePhone" runat="server">�Ƴ��ֻ�</asp:linkbutton><BR>
						<asp:listbox id="lbxSms" runat="server" Width="136px" Height="212px"></asp:listbox></TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
