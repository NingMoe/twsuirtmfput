<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.BatchSendList" CodeFile="BatchSendList.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>邮件短信群发</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
function RowLeftClickNoPost(src){
	try{
		var o=src.parentNode;
		for (var k=1;k<o.children.length;k++){
			o.children[k].bgColor = "white";
		}
		src.bgColor = "#C4D9F9";
		self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
	}catch(ex){
	}
}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" style="WIDTH: 740px" cellSpacing="0" cellPadding="0" width="740"
							border="0">
							<TR>
								<TD class="header_level2" style="WIDTH: 557px" colSpan="2" height="10"><b><asp:label id="lblTitle" runat="server">邮件短信群发</asp:label></b></TD>
							</TR>
							<TR height="23">
								<td style="WIDTH: 557px" vAlign="top" colSpan="2" height="22">
									<TABLE style="WIDTH: 728px" cellSpacing="0" cellPadding="0" width="728" border="0">
										<TR>
											<TD style="WIDTH: 100%" vAlign="top"><FONT face="宋体"><asp:datagrid id="DataGrid1" runat="server"></asp:datagrid></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100%; HEIGHT: 17px" vAlign="top"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100%" vAlign="top"><FONT face="宋体"><FONT face="Times New Roman"></FONT><asp:button id="btnBSendEmail" runat="server" Text="群发邮件" Width="76px"></asp:button><asp:button id="btnBSendSms" runat="server" Text="群发短信" Width="76px"></asp:button>&nbsp;<asp:button id="btnAdd" runat="server" Text="添加设置" Width="76px"></asp:button><asp:button id="btnEdit" runat="server" Text="编辑设置" Width="76px"></asp:button><asp:button id="btnDelete" runat="server" Text="删除设置" Width="76px"></asp:button>&nbsp;<asp:button id="btnSmsContent" runat="server" Text="短信内容设置" Width="100px"></asp:button><asp:button id="btnEmailContent" runat="server" Text="邮件内容设置" Width="100px"></asp:button><asp:button id="btnEmailServer" runat="server" Width="112px" Text="邮件服务器设置"></asp:button></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100%; HEIGHT: 10px" vAlign="top"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 100%" vAlign="top"><asp:panel id="panelMove" runat="server" Width="176px"><IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16"><FONT face="宋体">&nbsp;</FONT>
													<asp:linkbutton id="lbtnMoveUp" runat="server">向上移动</asp:linkbutton>
													<FONT face="宋体">&nbsp; </FONT><IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16"><FONT face="宋体">&nbsp;</FONT>
													<asp:linkbutton id="lbtnMoveDown" runat="server">向下移动</asp:linkbutton>
												</asp:panel></TD>
										</TR>
									</TABLE>
								</td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
