<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.EmailSmsConfig" CodeFile="EmailSmsConfig.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>FieldAdvanceSetting</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing=0 cellPadding=0>
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header" colSpan="2"><b>�����ʼ����ֻ�����</b></TD>
							</TR>
							<TR height="28">
								<TD style="WIDTH: 165px" align="right" width="165">��ǰ��Դ</TD>
								<TD><asp:label id="lblResName" runat="server"></asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">�ʼ��ֻ��ֶ�������Դ</TD>
								<TD><asp:dropdownlist id="ddlRelHostRes" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<td style="WIDTH: 165px" align="right" width="165">�����ʼ��ֶ�</td>
								<TD><asp:dropdownlist id="ddlEmail" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">�ֻ��ֶ�</TD>
								<TD><asp:dropdownlist id="ddlHandphone" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px" align="right" width="165">�ο��ֶ�</TD>
								<TD><asp:dropdownlist id="ddlRefColumn" runat="server" Width="144px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 165px; HEIGHT: 12px" align="right" width="165"></TD>
								<TD style="HEIGHT: 12px"></TD>
							</TR>
							<TR >
								<TD style="WIDTH: 165px" align="right" width="165"><FONT face=����></FONT></TD>
								<TD ><asp:button id="btnConfirm" runat="server" Width="104px" Text="��������"></asp:button><asp:button id="btnClearSetting" runat="server" Text="�������"></asp:button><asp:button id="btnExit" runat="server" Width="56px" Text="�˳�"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
