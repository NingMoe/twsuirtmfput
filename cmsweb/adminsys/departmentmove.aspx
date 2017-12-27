<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DepartmentMove" CodeFile="DepartmentMove.aspx.vb" %>
<%@ Import NameSpace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>移动部门</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="490" height="520px" border="0" class="table_level2">
							<TR>
								<TD height="22" class="header_level2" style="WIDTH: 374px"><b>请选择目标部门</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 374px; HEIGHT: 4px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 374px; HEIGHT: 19px"><FONT face="宋体">
										<asp:Button id="btnConfirm" runat="server" Text="确定" Width="88px"></asp:Button>
										<asp:Button id="btnCancel" runat="server" Text="取消" Width="88px"></asp:Button></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 374px; HEIGHT: 4px"></TD>
							</TR>
							<TR height="100%">
								<TD style="WIDTH: 374px"><asp:Panel id="panelDepTree" style="OVERFLOW: auto" Width="100%" Height="100%" runat="server"><%WebTreeDepartment.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/adminsys/DepartmentMove.aspx", "_self", Unionsoft.Platform.AspPage.RStr("depid", Request), CmsPass.Employee.ID, , False)%></asp:Panel></FONT>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
