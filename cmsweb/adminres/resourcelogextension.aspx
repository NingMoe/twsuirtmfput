<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceLogExtension" CodeFile="ResourceLogExtension.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>日志扩展</title>
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
								<TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">日志扩展</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:Label id="Label8" runat="server">目标资源</asp:Label></TD>
								<TD style="HEIGHT: 25px" align="left"><asp:TextBox id="txtResName" runat="server" Width="196px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:Label id="Label9" runat="server">日志扩展字段1</asp:Label></TD>
								<TD align="left">
									<asp:DropDownList id="ddlCol1" runat="server" Width="196px"></asp:DropDownList>
									<asp:Label id="Label5" runat="server">（长度为50）</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:Label id="Label1" runat="server">日志扩展字段2</asp:Label></TD>
								<TD align="left">
									<asp:DropDownList id="ddlCol2" runat="server" Width="196px"></asp:DropDownList>
									<asp:Label id="Label6" runat="server">（长度为50）</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:Label id="Label2" runat="server">日志扩展字段3</asp:Label></TD>
								<TD align="left">
									<asp:DropDownList id="ddlCol3" runat="server" Width="196px"></asp:DropDownList>
									<asp:Label id="Label7" runat="server">（长度为50）</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:Label id="Label3" runat="server">日志扩展字段4</asp:Label></TD>
								<TD align="left">
									<asp:DropDownList id="ddlCol4" runat="server" Width="196px"></asp:DropDownList>
									<asp:Label id="Label10" runat="server">（长度为50）</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:Label id="Label4" runat="server">日志扩展字段5</asp:Label></TD>
								<TD align="left"><FONT face="宋体">
										<asp:DropDownList id="ddlCol5" runat="server" Width="196px"></asp:DropDownList>
										<asp:Label id="Label11" runat="server">（长度为200）</asp:Label></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:Label id="Label12" runat="server">日志扩展字段6</asp:Label></TD>
								<TD align="left">
									<asp:DropDownList id="ddlCol6" runat="server" Width="196px"></asp:DropDownList></FONT>
									<asp:Label id="Label13" runat="server">（长度为200）</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="保存"></asp:Button>
									<asp:Button id="btnClear" runat="server" Width="80px" Text="清空设置"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="退出"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
