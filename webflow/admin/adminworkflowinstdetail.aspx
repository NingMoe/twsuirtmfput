<%@ Reference Control="~/controls/ctlflowdiagram.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CtlFlowHistory" Src="../controls/CtlFlowHistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CtlFlowDiagram" Src="../controls/CtlFlowDiagram.ascx" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.AdminWorkflowInstDetail" CodeFile="AdminWorkflowInstDetail.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminFlowAdjust</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
		<style>
		INPUT { HEIGHT: 18px; FONT-SIZE: 10px }
		</style>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="99%" border="0" height="100%" align="center">
				<tr class="ListHeader" height="25">
					<td>
						<asp:Button ID="btnRollBack" Runat="server" Text="������ǰһ����"></asp:Button>
						<asp:Button ID="btnRedirect" Runat="server" Text="����ָ��������"></asp:Button>
						<asp:Button ID="btnFinish" Runat="server" Text="��������Ϊ����"></asp:Button>
						<asp:Button ID="btnDelete" Runat="server" Text="ɾ��������ʵ��"></asp:Button>
						<asp:Button ID="btnDisplayForm" Runat="server" Text="������ʵ���ı�"></asp:Button>
					</td>
				</tr>
				<tr>
					<td valign="top"><uc1:CtlFlowDiagram id="CtlFlowDiagram1" runat="server"></uc1:CtlFlowDiagram></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
