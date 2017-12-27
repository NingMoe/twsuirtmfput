<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvOption" validateRequest="false" CodeFile="FieldAdvOption.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>下拉框选择项</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" rel="stylesheet" type="text/css">
		<script language="javascript">
<!--
//调整输入窗体中各个Panel的位置和显示模式
function ListboxOptionClicked(){
  var selIndex = document.all.item("lboxOptValues").selectedIndex;
  var txtSelected = document.all.item("lboxOptValues").options(selIndex).text;
  document.all.item("txtOptValue").value = txtSelected;
}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="700" border="0" class="table_level2" style="WIDTH: 700px">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>下拉框选择项设置</b></TD>
							</TR>
							<TR>
								<TD align="right" width="90" style="WIDTH: 90px; HEIGHT: 12px"><FONT face="宋体"> </FONT>
								</TD>
								<TD height="12"></TD>
							</TR>
							<TR height="25">
								<TD width="90" align="right" style="WIDTH: 90px">当前资源：</TD>
								<TD>
									<asp:TextBox id="txtResName" runat="server" Width="232px"></asp:TextBox></TD>
							</TR>
							<TR height="25">
								<TD width="90" align="right" style="WIDTH: 90px">当前字段：</TD>
								<TD>
									<asp:TextBox id="txtFieldName" runat="server" Width="232px"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right" width="90">
									<asp:Label id="lblOptValue" runat="server">选项值：</asp:Label></TD>
								<TD><FONT face="宋体">
										<asp:TextBox id="txtOptValue" runat="server" Width="496px"></asp:TextBox></FONT></TD>
							</TR>
							<TR height="25">
								<TD width="90" align="right" valign="top" style="WIDTH: 90px; HEIGHT: 406px"></TD>
								<TD valign="top" style="HEIGHT: 406px">
									<table width="100%" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td width="500" valign="top" style="WIDTH: 500px">
												<asp:ListBox id="lboxOptValues" runat="server" Height="420px" Width="496px"></asp:ListBox>
											</td>
											<td valign="top">
												<asp:Button id="btnAdd" runat="server" Width="84px" Text="添加"></asp:Button><FONT face="宋体"><BR>
												</FONT>
												<asp:Button id="btnEdit" runat="server" Width="84px" Text="修改"></asp:Button><FONT face="宋体"><BR>
												</FONT>
												<asp:Button id="btnInsert" runat="server" Width="84px" Text="插入"></asp:Button>
												<br>
												<asp:Button id="btnDel" runat="server" Width="84px" Text="删除"></asp:Button><FONT face="宋体"><BR>
												</FONT><FONT face="宋体">
													<BR>
													<asp:Button id="btnUp" runat="server" Width="84px" Text="上移"></asp:Button><BR>
													<asp:Button id="btnDown" runat="server" Width="84px" Text="下移"></asp:Button><BR>
													<BR>
													<BR>
													<BR>
													<asp:Button id="btnConfirm" runat="server" Width="84px" Text="确定"></asp:Button><BR>
													<asp:Button id="btnCancel" runat="server" Width="84px" Text="取消"></asp:Button>
												</FONT>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
