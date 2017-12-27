<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.APageTemplate2" CodeFile="APageTemplate2.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>模板窗体02</TITLE>
		<meta http-equiv="Pragma" content="no-cache" />
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">模板窗体02</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:Label id="lblResName" runat="server">资源名称</asp:Label></TD>
								<TD align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="确认"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
