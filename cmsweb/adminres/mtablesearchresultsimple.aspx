<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearchResultSimple" CodeFile="MTableSearchResultSimple.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>简单统计表</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td><asp:DataGrid id="DataGrid1" runat="server"></asp:DataGrid></td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
