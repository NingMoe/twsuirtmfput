<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ConfigRelTables" CodeFile="ConfigRelTables.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>��������Դ����</title>
		<meta http-equiv="Pragma" content="no-cache" />
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">��������Դ��ʾ����</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:Label id="Label8" runat="server">Ŀ����Դ</asp:Label></TD>
								<TD style="HEIGHT: 25px" align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 11px" align="right"></TD>
								<TD style="HEIGHT: 11px" align="left"></TD>
							</TR>
							<TR class="module_header">
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Label id="Label3" runat="server">��������Դ��ʾ����</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right" valign="top"></TD>
								<TD align="left">
									<asp:RadioButton id="rdoShowRelTables" runat="server" Text="��ʾ��������Դ" GroupName="RELTABLES" Checked="True"></asp:RadioButton><FONT face="����"><BR>
										<asp:RadioButton id="rdoNotShowRelTables" runat="server" Text="����ʾ��������Դ" GroupName="RELTABLES"></asp:RadioButton></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="ȷ��"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="ȡ��"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
