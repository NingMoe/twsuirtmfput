<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceSyncList" CodeFile="ResourceSyncList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">����ͬ��</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
			<script language="javascript">
<!--
function RowLeftClickNoPost(src){
	var o=src.parentNode;
	for (var k=1;k<o.children.length;k++){
		o.children[k].bgColor = "white";
	}
		
	src.bgColor = "#C4D9F9";
	self.document.forms(0).RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
}
-->
			</script>
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td align="left">
						<TABLE cellSpacing="0" cellPadding="0" width="99%" align="left" border="0">
							<TR vAlign="top">
								<TD vAlign="top" align="left" height="24">
									<TABLE class="toolbar_table" cellSpacing="0" border="0">
										<TR vAlign="middle">
											<TD width="8"></TD>
											<TD noWrap align="left"><asp:label id="lblPageTitle" runat="server"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR height="22">
								<td>
									<table cellSpacing="0" cellPadding="0" width="99%" border="0">
										<TR>
											<TD><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></TD>
										</TR>
										<TR>
											<TD></TD>
										</TR>
										<TR height="22">
											<TD>
												<asp:button id="btnAdd" runat="server" Text="�������" Width="80px"></asp:button><asp:button id="btnEdit" runat="server" Text="�޸�����" Width="80px"></asp:button><asp:button id="btnDelete" runat="server" Text="ɾ������" Width="80px"></asp:button>&nbsp;
												<asp:button id="btnSyncCol" runat="server" Width="80px" Text="ͬ���ֶ�"></asp:button>&nbsp;<asp:button id="btnExit" runat="server" Text="�˳�" Width="80px"></asp:button>&nbsp;
												<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;
												<asp:LinkButton id="lbtnMoveUp" runat="server">�����ƶ�</asp:LinkButton>&nbsp; <IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;
												<asp:LinkButton id="lbtnMoveDown" runat="server">�����ƶ�</asp:LinkButton></TD>
										</TR>
										<TR>
											<TD height="20"></TD>
										</TR>
									</table>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
