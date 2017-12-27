<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.AdminEditPermissoin" CodeFile="AdminEditPermissoin.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.AdminPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminEditPermissoin</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="100%" Align="center" border="1" cellpadding="0" cellspacing="0" class="ListBox">
				<asp:Repeater ID="PermissionList" Runat="server">
					<ItemTemplate>
						<tr height="22">
							<td>
								<img src="../images/worker.gif" border="0" align="absmiddle">
								<%#DataBinder.Eval(Container.DataItem,"OrgName")%>
								<asp:TextBox ID="OrgCode" Runat="server" style="display:none" Width="50"></asp:TextBox>
								<asp:TextBox ID="OrgType" Runat="server" style="display:none" Width="50"></asp:TextBox>
							</td>
							<td width="60" align="center">
								<asp:LinkButton ID="Delete" Runat="server">删除</asp:LinkButton>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
				<tr>
					<td class="ListHeader" height="22" colspan="2">
						<asp:LinkButton id="lnkAdd" runat="server">增加权限</asp:LinkButton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

