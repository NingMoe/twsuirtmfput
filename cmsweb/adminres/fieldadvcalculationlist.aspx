<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCalculationList" validateRequest="false" CodeFile="FieldAdvCalculationList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>���㹫ʽ�б�</title>
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
	self.document.forms(0).fmlaiid.value = src.fmlaiid; //��Ҫ���û�ѡ����к�POST��������
	self.document.forms(0).fmlcolname.value = src.fmlcolname; //��Ҫ���û�ѡ����к�POST��������
	self.document.forms(0).fmlresid.value = src.fmlresid; //��Ҫ���û�ѡ����к�POST��������
	self.document.forms(0).fmltype.value = src.fmltype; //��Ҫ���û�ѡ����к�POST��������
}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="fmltype"> <input type="hidden" name="fmlcolname"> <input type="hidden" name="fmlresid">
			<input type="hidden" name="fmlaiid">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="99%" border="0">
							<TR>
								<TD class="header_level2" height="22"><b>���㹫ʽ����&nbsp;����Դ��<asp:label id="lblResDispName" runat="server"></asp:label>��</b>
								</TD>
							</TR>
							<tr>
								<td height="4"></td>
							</tr>
							<TR>
								<TD height="5"><asp:button id="btnSetFormula" runat="server" Text="�޸Ĺ�ʽ"></asp:button><asp:button id="btnDelFormula" runat="server" Text="ɾ����ʽ"></asp:button>
									<asp:Button id="btnFmlOption" runat="server" Text="ѡ������"></asp:Button><asp:Button id="btnSchedule" runat="server" Text="��ʱ����"></asp:Button><asp:button id="btnAddVerifyFormula" runat="server" Text="���У�鹫ʽ"></asp:button><asp:button id="btnExit" runat="server" Text="�˳�" Width="72px"></asp:button>
									&nbsp;&nbsp;<IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveup" runat="server">�����ƶ�</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveToFirst" runat="server">������λ</asp:linkbutton>&nbsp;&nbsp;&nbsp;<IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;&nbsp;<asp:linkbutton id="lbtnMovedown" runat="server">�����ƶ�</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnMoveToLast" runat="server">����ĩλ</asp:linkbutton></TD>
							</TR>
							<tr>
								<td height="4"></td>
							</tr>
							<TR>
								<TD height="22"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False"></asp:datagrid></TD>
							</TR>
							<TR>
								<TD height="12"></TD>
							</TR>
							<TR>
								<TD height="5"><FONT face="����">���ϼ��㹫ʽ�����д����Ǹ�����ʽ���������</FONT>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
