<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearchResult2Column" CodeFile="MTableSearchResult2Column.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>统计表</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD colspan="4" style="HEIGHT: 30px" align="center">
									<asp:Label id="lblHeader" runat="server" Font-Names="宋体" Font-Size="24px" Font-Bold="True"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="center" colSpan="4"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><asp:Label id="lblF1" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><FONT face="宋体">
										<asp:Label id="lblF1Val" runat="server"></asp:Label></FONT></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left"><FONT face="宋体">
										<asp:Label id="lblF2" runat="server"></asp:Label></FONT></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left"><FONT face="宋体">
										<asp:Label id="lblF2Val" runat="server"></asp:Label></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF3" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF3Val" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF4" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF4Val" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF5" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF5Val" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF6" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF6Val" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF7" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF7Val" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF8" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF8Val" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD colspan="4" style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD colspan="4" style="HEIGHT: 2px" align="left">
									<asp:DataGrid id="DataGrid1" runat="server"></asp:DataGrid></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="left" colSpan="4"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF9" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF9Val" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 100px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF10" runat="server"></asp:Label></TD>
								<TD style="WIDTH: 270px; HEIGHT: 2px" align="left">
									<asp:Label id="lblF10Val" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 12px" align="left" colSpan="4"></TD>
							</TR>
							<TR>
								<TD align="left" style="HEIGHT: 2px" colSpan="4">
									<asp:Label id="lblTail" runat="server"></asp:Label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
