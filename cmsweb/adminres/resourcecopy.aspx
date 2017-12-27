<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceCopy" CodeFile="ResourceCopy.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>资源复制</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>
										<asp:Label id="lblTitle" runat="server">复制资源</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px; HEIGHT: 4px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR height="22">
								<td width="99" align="right" style="WIDTH: 99px">
									<asp:Label id="Label1" runat="server">新资源名称：</asp:Label></td>
								<TD>
									<asp:TextBox id="txtResName" runat="server" Width="244px"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px; HEIGHT: 4px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR height="22">
								<td style="WIDTH: 99px"></td>
								<TD>
									<asp:Button id="btnConfirm" runat="server" Text="确定" Width="80px"></asp:Button>
									<asp:Button id="btnCancle" runat="server" Text="退出" Width="80px"></asp:Button>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px; HEIGHT: 4px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px" align="right" width="99"></TD>
								<TD>
									<asp:Label id="Label2" runat="server">本操作仅复制以下内容：资源定义、字段定义、显示设置、窗体设计、定制窗体设计、下拉框选择项、多选一选择项、自动编码、高级字典、计算公式、定制编码、作为主表和子表的关联表定义。</asp:Label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
