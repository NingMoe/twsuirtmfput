<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocHistoryList" CodeFile="DocHistoryList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DocHistoryList</title>
		<script language="javascript">
		<!--
			function GetDoc(filepath){
				window.location = "/cmsweb/cmsdocument/DocHistoryList.aspx?docaction=getdoc&file=" + filepath;
			}

			function ShowDoc(filepath){
				window.location = "/cmsweb/cmsdocument/DocHistoryList.aspx?docaction=showdoc&file=" + filepath;
			}
		-->
		</script>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="PADDING-LEFT: 4px" cellSpacing="0" cellPadding="2" width="99%" border="0">
				<!--主表数据-->
				<tr>
					<TD style="WIDTH: 2px" height="22"></TD>
					<TD class="header_level2" height="22"><b><asp:Label ID="lblTitle" runat="server">文档历史版本查阅</asp:Label></b></TD>
				</tr>
				<tr>
					<TD style="WIDTH: 2px; HEIGHT: 22px" vAlign="top"></TD>
					<td valign="top" style="HEIGHT: 22px"><asp:datagrid id="DataGrid1" runat="server" Width="304px">
							<HeaderStyle CssClass="header_level2"></HeaderStyle>
						</asp:datagrid></td>
				</tr>
				<TR>
					<TD style="WIDTH: 2px"></TD>
					<TD><FONT face="宋体"><BR>
							<asp:Button id="btnExit" runat="server" Width="88px" Text="退出"></asp:Button></FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
