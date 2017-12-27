<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DepartmentSelect" CodeFile="DepartmentSelect.aspx.vb" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>部门选择</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" style="PADDING-TOP: 1px">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="400px" border="0" height="520px" class="table_level2">
							<TR>
								<TD height="22" class="header_level2"><b>请选择部门</b></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 4px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 400px" align="left" width="314" height="25px">
									<asp:Button id="btnSelectDep" runat="server" Text="选择部门" Width="80px"></asp:Button>
									<asp:Button id="btnCancel" runat="server" Width="80px" Text="取消"></asp:Button></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 4px"></TD>
							</TR>
							<TR height="100%">
								<TD style="WIDTH: 400px" height="100%" align="left" width="314"><asp:Panel id="panelDepTree" style="OVERFLOW: auto" Width="100%" Height="100%" runat="server"><%WebTreeDepartment.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/adminsys/DepartmentSelect.aspx?employeeid=" & Request.QueryString("employeeid"), "_self",unionsoft.Platform .AspPage.RStr("depid", Request),CmsPass.Employee.ID,,False )%></asp:Panel>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
