<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvConstant" validateRequest="false" CodeFile="FieldAdvConstant.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>�ֶ����ã�����</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" rel="stylesheet" type="text/css">
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>�ֶθ߼����ã�����</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 123px" align="right" width="123"><FONT face="����"><BR>
									</FONT>
								</TD>
								<TD></TD>
							</TR>
							<TR height="25">
								<TD width="123" align="right" style="WIDTH: 123px">��ǰ��Դ��</TD>
								<TD align="left"><asp:Label id="lblResName" runat="server"></asp:Label></TD>
							</TR>
							<TR height="25">
								<TD width="123" align="right" style="WIDTH: 123px">��ǰ�ֶΣ�</TD>
								<TD align="left"><asp:Label id="lblFieldName" runat="server"></asp:Label></TD>
							</TR>
							<TR height="25">
								<TD width="123" align="right" style="WIDTH: 123px">����ֵ��</TD>
								<TD><asp:TextBox id="txtConstant" runat="server" Width="176px"></asp:TextBox><FONT 
            face=����></FONT></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 123px"></TD>
								<td><FONT face="����"><BR>
									</FONT>
									<asp:Button id="btnConfirm" runat="server" Text="ȷ��" Width="80px"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Text="ȡ��" Width="80px"></asp:Button>
								</td>
							</TR>
							<TR>
								<td colspan="2" height="5"></td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
