<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvCalculationSettings" validateRequest="false" CodeFile="FieldAdvCalculationSettings.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>���㹫ʽѡ��</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="WIDTH: 100%" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b>���㹫ʽѡ������</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 108px; HEIGHT: 13px" align="right" width="108"></TD>
								<TD style="HEIGHT: 13px"></TD>
							</TR>
							<TR height="21">
								<TD align="right" width="108" style="WIDTH: 108px"><asp:Label id="Label2" runat="server">��ǰ��Դ��</asp:Label></TD>
								<TD align="left"><asp:label id="lblResName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD align="right" width="108" style="WIDTH: 108px"><asp:Label id="Label3" runat="server">��ǰ�ֶΣ�</asp:Label></TD>
								<TD align="left"><asp:Label id="lblFieldName" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD align="right" width="108" style="WIDTH: 108px; HEIGHT: 14px"></TD>
								<TD align="left" style="HEIGHT: 14px"></TD>
							</TR>
							<TR>
								<TD align="right" width="108" style="WIDTH: 108px"><asp:Label id="Label9" runat="server">����ʱ����</asp:Label></TD>
								<TD><asp:CheckBox id="chkRunRecordAdd" runat="server" Text="������Դ��Ӽ�¼ʱ����" Checked="True"></asp:CheckBox><FONT face="����">&nbsp;</FONT><asp:CheckBox id="chkRunRecordEdit" runat="server" Text="������Դ�޸ļ�¼ʱ����" Checked="True"></asp:CheckBox><FONT face="����">&nbsp;</FONT><FONT face="����"><BR>
									</FONT><FONT face="����">
										<asp:CheckBox id="chkRunRecordAdd2" runat="server" Checked="True" Text="�����Դ��Ӽ�¼ʱ����"></asp:CheckBox>&nbsp;<asp:CheckBox id="chkRunRecordEdit2" runat="server" Checked="True" Text="�����Դ�޸ļ�¼ʱ����"></asp:CheckBox>&nbsp;<asp:CheckBox id="chkRunRecordDel2" runat="server" Checked="True" Text="�����Դɾ����¼ʱ����"></asp:CheckBox></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 108px; HEIGHT: 13px" align="right" width="108"></TD>
								<TD style="HEIGHT: 13px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 108px; HEIGHT: 20px" align="right" width="108"></TD>
								<TD style="HEIGHT: 20px"><FONT face="����"><asp:Button id="btnConfirm" runat="server" Width="72px" Text="ȷ��"></asp:Button><asp:Button id="btnCancel" runat="server" Width="72px" Text="�˳�"></asp:Button></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
