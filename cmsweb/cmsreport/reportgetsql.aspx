<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ReportGetSql" CodeFile="ReportGetSql.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>提取报表SQL</title>
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
						<TD colspan="2" class="form_header"><b><asp:Label id=lblTitle runat="server">报表用SQL</asp:Label></b></TD>
					</TR>
        <TR>
          <TD style="WIDTH: 98px; HEIGHT: 12px" vAlign=top align=right></TD>
          <TD style="HEIGHT: 12px" align=left></TD></TR>
					<TR>
						<TD style="WIDTH: 98px; HEIGHT: 25px" align=right valign="top"><asp:Label id=Label8 runat="server">报表用SQL</asp:Label></TD>
						<TD style="HEIGHT: 25px" align=left><asp:TextBox id=txtSql runat="server" Width="580px" Height="404px" TextMode="MultiLine"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 98px" align=right>&nbsp;</TD>
						<TD align=left><asp:Button id=btnExit runat="server" Width="80px" Text="退出"></asp:Button></TD>
					</TR>
				</TABLE>
			</td>
		</tr>
	</TABLE>
	</form>
	</body>
</HTML>
