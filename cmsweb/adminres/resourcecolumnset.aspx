<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceColumnSet" CodeFile="ResourceColumnSet.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>�ֶ�����</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="99%" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b>�ֶ�����&nbsp;����Դ��<asp:label id="lblResDispName" runat="server"></asp:label>��</b>
								</TD>
							</TR>
							<tr>
								<td height="5"></td>
							</tr>
							<TR height="22">
								<TD><asp:button id="btnAddCol2" runat="server" Width="72px" Text="�����ֶ�"></asp:button><asp:button id="btnAdvSetting2" runat="server" Text="�߼�����" Width="72px"></asp:button><FONT face="����"><asp:button id="btnDelAdvSetting2" runat="server" Width="96px" Text="ɾ���߼�����"></asp:button>&nbsp;
										<asp:button id="btnCopyColDef2" runat="server" Text="�����ֶ�" Width="72px"></asp:button><asp:button id="btnExit2" runat="server" Text="�˳�" Width="72px"></asp:button>&nbsp;<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;
										<asp:linkbutton id="lbtnMoveup" runat="server">�����ƶ�</asp:linkbutton>&nbsp;
										<asp:LinkButton id="lbtnMoveUp5Step" runat="server">����5λ</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lbtnMoveToFirst" runat="server">������λ</asp:LinkButton>&nbsp;<IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;
										<asp:linkbutton id="lbtnMovedown" runat="server">�����ƶ�</asp:linkbutton>&nbsp;
										<asp:LinkButton id="lbtnMoveDown5Step" runat="server">����5λ</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lbtnMoveToLast" runat="server">����ĩλ</asp:LinkButton></FONT></TD>
							</TR>
							<tr>
								<td height="5"></td>
							</tr>
							<TR height="22">
								<TD><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False"></asp:datagrid></TD>
							</TR>
							<tr>
								<td height="5"></td>
							</tr>
							<TR height="22">
								<TD><asp:button id="btnAddCol" runat="server" Width="72px" Text="�����ֶ�"></asp:button><asp:button id="btnAdvSetting" runat="server" Text="�߼�����" Width="72px"></asp:button><FONT face="����"><asp:button id="btnDelAdvSetting" runat="server" Width="96px" Text="ɾ���߼�����"></asp:button>&nbsp;
										<asp:button id="btnCopyColDef" runat="server" Text="�����ֶ�" Width="72px"></asp:button><asp:button id="btnDelCol" runat="server" Text="ɾ���ֶ�" Width="72px"></asp:button><asp:Button id="btnColShowSet" runat="server" Text="��ʾ����" Width="72px"></asp:Button><asp:Button id="btnInputFormSet" runat="server" Text="���봰��" Width="72px"></asp:Button><asp:Button id="btnRightsSet" runat="server" Text="Ȩ������" Width="72px"></asp:Button>&nbsp;</FONT><asp:button id="btnExit" runat="server" Width="72px" Text="�˳�"></asp:button></TD>
							</TR>
							<TR>
								<TD height="5"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
