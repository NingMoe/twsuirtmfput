<%@ Reference Page="~/admin/adminprocessweekanalyse.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.AdminSearch" CodeFile="AdminSearch.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminSearch</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../css/flowstyle.css" rel="stylesheet" type="text/css">
		<script src="../script/jscommon.js" type="text/javascript"></script>
		<script src="../script/base.js" type="text/javascript"></script>
		<script src="../script/FlowCommonScript.js" type="text/javascript"></script>
		<script src="../script/Valid.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="����"></FONT>
			<br>
			<table cellpadding="0" cellspacing="1" border="0" width="500" align="center" class="MemoTable">
				<thead>
					<tr>
						<td colspan="2">�ļ���ѯ</td>
					</tr>
				</thead>
				<tr>
					<td width="80" align="center">��������</td>
					<td><asp:TextBox id="txtWorkflowTitle" runat="server" Width="300"></asp:TextBox></td>
				</tr>
				<tr>
					<td width="80" align="center">����ʱ��(��ʼ)</td>
					<td><asp:TextBox id="txtCreateDate" runat="server" Width="300"></asp:TextBox></td>
				</tr>
				<tr>
					<td width="80" align="center">����ʱ��(����)</td>
					<td><asp:TextBox id="txtCreateDate1" runat="server" Width="300"></asp:TextBox></td>
				</tr>
				<tr>
					<td width="80"></td>
					<td>
						<asp:Button id="btnQuery" runat="server" Text="��ѯ"></asp:Button>
						<input type="button" value="����" onclick="history.back();">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

